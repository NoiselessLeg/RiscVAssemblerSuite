using Assembler.Common;
using Assembler.OutputProcessing.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assembler.OutputProcessing
{
   /// <summary>
   /// Represents a .JEF file.
   /// </summary>
   internal class JefFile
   {
      private enum ArchitectureTypes
      {
         Bit32 = 0x01,
         Bit64 = 0x02
      }

      /// <summary>
      /// Parses a .JEF file for all relevent assembly data.
      /// </summary>
      /// <param name="fileName">The path of the file to parse.</param>
      /// <returns>A structure representing all the relevant data in the file.</returns>
      public static JefFile ParseFile(string fileName)
      {
         using (var fileReader = new EndianReader(File.OpenRead(fileName), Endianness.LittleEndian))
         {
            FileHeader hdr = ReadHeader(fileReader);
            System.Diagnostics.Debug.Assert(fileReader.BaseStream.Position == 0x30);

            int dataMdataSize = hdr.DataOffset - hdr.DataMetadataOffset;
            IEnumerable<MetadataElement> dataMetaData = ReadMetadataSection(fileReader, dataMdataSize);

            int dataSize = hdr.TextOffset - hdr.DataOffset;
            IEnumerable<byte> dataElems = ParseDataSegment(fileReader, dataMetaData);

            int textSize = hdr.ExternOffset - hdr.TextOffset;
            IEnumerable<int> textElems = ParseTextSection(fileReader, textSize);

            // extern segment should just be all zeroes, so read the number of bytes as specified in the size.
            int externSize = hdr.SymTblOffset - hdr.ExternOffset;
            IEnumerable<byte> externElems = fileReader.ReadBytes(externSize);

            // symbol table runs from the starting offset to the end of the file, so just calculate the delta.
            int symTblSize = (int)(fileReader.BaseStream.Length - hdr.SymTblOffset);
            ReverseSymbolTable symTbl = ParseSymbolTableSection(fileReader, symTblSize);

            return new JefFile(hdr.BaseDataAddress, hdr.BaseTextAddress, hdr.BaseExternAddress, dataMetaData,
                               dataElems, textElems, externElems, symTbl);
         }
      }

      /// <summary>
      /// Gets the metadata associated with the .data segment of this file.
      /// </summary>
      public IEnumerable<MetadataElement> DataMetadata => m_DataMetadata;

      /// <summary>
      /// Gets the list of all elements in the file's .data segment, as bytes.
      /// </summary>
      public IEnumerable<byte> DataElements => m_DataElems;

      /// <summary>
      /// Gets a list of all instructions in the file's .text segment.
      /// </summary>
      public IEnumerable<int> TextElements => m_TextElems;

      /// <summary>
      /// Gets a list of all elements in the file's .extern segment.
      /// </summary>
      public IEnumerable<byte> ExternElements => m_ExternElems;

      /// <summary>
      /// Gets the runtime base .data segment address.
      /// </summary>
      public int BaseDataAddress => m_BaseDataAddress;

      /// <summary>
      /// Gets the runtime base .text segment address.
      /// </summary>
      public int BaseTextAddress => m_BaseTextAddress;

      /// <summary>
      /// Gets the runtime base .extern segment address.
      /// </summary>
      public int BaseExternAddress => m_BaseExternAddress;

      /// <summary>
      /// Gets the extracted symbol table, with labels and addresses.
      /// </summary>
      public ReverseSymbolTable SymbolTable => m_SymTbl;

      /// <summary>
      /// Creates a representation of a .JEF file in memory.
      /// </summary>
      /// <param name="baseDataAddress">The base .data segment address.</param>
      /// <param name="baseTextAddress">The base .text segment address.</param>
      /// <param name="baseExternAddress">The base .extern segment address.</param>
      /// <param name="metadataElems">An IEnumerable of all the data metadata elements.</param>
      /// <param name="dataElements">An IEnumerable of all the data elements, represented as bytes.</param>
      /// <param name="textElements">An IEnumerable of all 32-bit instructions in the .JEF file.</param>
      /// <param name="externElements">An IEnumerable of all the .extern elements in the .JEF file.</param>
      /// <param name="symTable">A reconstructed SymbolTable instance from the .JEF file.</param>
      private JefFile(int baseDataAddress, int baseTextAddress, int baseExternAddress, IEnumerable<MetadataElement> metadataElems,
                        IEnumerable<byte> dataElements, IEnumerable<int> textElements, IEnumerable<byte> externElements,
                        ReverseSymbolTable symTable)
      {
         m_BaseDataAddress = baseDataAddress;
         m_BaseTextAddress = baseTextAddress;
         m_BaseExternAddress = baseExternAddress;
         m_DataMetadata = metadataElems;
         m_DataElems = dataElements;
         m_TextElems = textElements;
         m_ExternElems = externElements;
         m_SymTbl = symTable;
      }

      /// <summary>
      /// Reads the .data metadata section of the JEF file.
      /// </summary>
      /// <param name="reader">The BinaryReader instance used to read the file.</param>
      /// <param name="sectionSize">The size of the section, in bytes.</param>
      /// <returns>An IEnumerable of metadata elements that describes all elements in the .data section.</returns>
      private static IEnumerable<MetadataElement> ReadMetadataSection(BinaryReader reader, int sectionSize)
      {
         var elemList = new List<MetadataElement>();
         int totalBytesRead = 0;
         while (totalBytesRead < sectionSize)
         {
            // read the first byte to determine what the typecode is.
            ObjectTypeCode typeCodeByte = (ObjectTypeCode)reader.ReadByte();
            int size = 0;
            ++totalBytesRead;

            // depending on the typecode, the size may be variable. otherwise, it is fixed
            // to the size of the type represented by the typecode.
            switch (typeCodeByte)
            {
               case ObjectTypeCode.Byte:
               {
                  size = 1;
                  break;
               }

               case ObjectTypeCode.Dword:
               {
                  size = 8;
                  break;
               }

               case ObjectTypeCode.Half:
               {
                  size = 2;
                  break;
               }

               case ObjectTypeCode.Word:
               {
                  size = 4;
                  break;
               }

               // strings include a size word afterward.
               // otherwise, just read a byte.
               case ObjectTypeCode.String:
               {
                  size = reader.ReadInt32();
                  totalBytesRead += 4;
                  break;
               }

               default:
               {
                  throw new Exception("Unrecognized typecode \"0x" + typeCodeByte + "\" in disassembled file.");
               }
            }

            var dataElem = new MetadataElement(typeCodeByte, size);
            elemList.Add(dataElem);
         }

         return elemList;
      }

      /// <summary>
      /// Parses a .data segment in the JEF file.
      /// </summary>
      /// <param name="reader">The BinaryReader instance used to read the file.</param>
      /// <param name="metadata">The metadata associated with the data segment.</param>
      /// <param name="dataSize">The size of the .data segment in bytes.</param>
      /// <returns>An IEnumerable of bytes that represent the data in the segment.</returns>
      private static IEnumerable<byte> ParseDataSegment(BinaryReader reader, IEnumerable<MetadataElement> metadata)
      {
         var dataList = new List<byte>();
         foreach (MetadataElement elem in metadata)
         {
            switch (elem.TypeCode)
            {
               case ObjectTypeCode.Byte:
               {
                  byte dataByte = reader.ReadByte();
                  dataList.Add(dataByte);
                  break;
               }

               case ObjectTypeCode.Dword:
               {
                  long value = reader.ReadInt64();
                  byte[] valueBytes = BitConverter.GetBytes(value);
                  dataList.AddRange(valueBytes);
                  break;
               }

               case ObjectTypeCode.Half:
               {
                  short value = reader.ReadInt16();
                  byte[] valueBytes = BitConverter.GetBytes(value);
                  dataList.AddRange(valueBytes);
                  break;
               }

               case ObjectTypeCode.Word:
               {
                  int value = reader.ReadInt32();
                  byte[] valueBytes = BitConverter.GetBytes(value);
                  dataList.AddRange(valueBytes);
                  break;
               }

               case ObjectTypeCode.String:
               {
                  byte[] strBytes = reader.ReadBytes(elem.Size);
                  dataList.AddRange(strBytes);
                  break;
               }
            }
         }

         return dataList;
      }

      /// <summary>
      /// Parses a .text segment of a .JEF file.
      /// </summary>
      /// <param name="reader">The BinaryReader instance used to read the file.</param>
      /// <param name="textSize">The size of the .text segment, in bytes.</param>
      /// <returns>An IEnumerable of instructions that compose the .text segment.</returns>
      private static IEnumerable<int> ParseTextSection(BinaryReader reader, int textSize)
      {
         var textElems = new List<int>();
         int textElemItr = 0;
         while (textElemItr < textSize)
         {
            int elem = reader.ReadInt32();
            textElems.Add(elem);
            textElemItr += sizeof(int);
         }

         return textElems;
      }

      /// <summary>
      /// Parses the symtbl segment of the file, and reconstructs a reverse lookup symbol table.
      /// </summary>
      /// <param name="reader">The reader to parse the file with.</param>
      /// <param name="sectionSize">The size of the symbol table area, in bytes.</param>
      /// <returns>A populated symbol reverse lookup table.</returns>
      private static ReverseSymbolTable ParseSymbolTableSection(BinaryReader reader, int sectionSize)
      {
         // for every symbol, read the first byte. this dictates how long the symbol name is.
         var symTable = new ReverseSymbolTable();
         int readBytes = 0;
         while (readBytes < sectionSize)
         {
            byte symStrSize = reader.ReadByte();
            ++readBytes;

            byte[] symbolName = reader.ReadBytes(symStrSize);
            readBytes += symStrSize;

            string symNameStr = Encoding.ASCII.GetString(symbolName);
            int symbolAddress = reader.ReadInt32();
            readBytes += sizeof(int);

            symTable.AddSymbol(symbolAddress, symNameStr);
         }

         return symTable;
      }

      /// <summary>
      /// Reads the header of a .JEF file.
      /// </summary>
      /// <param name="fileReader">The BinaryReader instance used to read the file with.</param>
      /// <returns>A FileHeader structure containing information useable by the other segment parsers.</returns>
      private static FileHeader ReadHeader(BinaryReader fileReader)
      {
         // read the first four bytes and ensure this is a valid .JEF file.
         var hdr = new FileHeader();
         byte[] preamble = fileReader.ReadBytes(4);

         byte[] EXPECTED_PREAMBLE = { 0x7F, 0x4A, 0x45, 0x46 };

         if (!preamble.SequenceEqual(EXPECTED_PREAMBLE))
         {
            throw new ArgumentException("Could not find expected .JEF preamble in provided file.");
         }

         //TODO: might use this later, not sure.
         hdr.ArchitectureType = (ArchitectureTypes)fileReader.ReadByte();

         //JAL 4/18: endianness is fixed to little. this is now a spare byte.
         byte spareByte = fileReader.ReadByte();

         hdr.BaseDataAddress = fileReader.ReadInt32();
         hdr.BaseTextAddress = fileReader.ReadInt32();
         hdr.BaseExternAddress = fileReader.ReadInt32();

         // to calculate the absolute offset of any section:
         // get the relative data offset, and add our current position to it.
         // since we had already advanced our stream position by sizeof(int) bytes
         // in the stream, subtract that since the offset is calculated as the distance
         // between the beginning of where the offset is declared, and where the section actually begins.
         int relDataMdataOffset = fileReader.ReadInt32();
         hdr.DataMetadataOffset = (int)(fileReader.BaseStream.Position + relDataMdataOffset - sizeof(int));

         int relDataOffset = fileReader.ReadInt32();
         hdr.DataOffset = (int)(fileReader.BaseStream.Position + relDataOffset - sizeof(int));

         int relTextOffset = fileReader.ReadInt32();
         hdr.TextOffset = (int)(fileReader.BaseStream.Position + relTextOffset - sizeof(int));

         // read the spare word.
         int spare = fileReader.ReadInt32();

         int relExternOffset = fileReader.ReadInt32();
         hdr.ExternOffset = (int)(fileReader.BaseStream.Position + relExternOffset - sizeof(int));

         int relSymTblOffset = fileReader.ReadInt32();
         hdr.SymTblOffset = (int)(fileReader.BaseStream.Position + relSymTblOffset - sizeof(int));

         // read the spare bytes, and ignore them.
         fileReader.ReadBytes(6);

         return hdr;
      }

      /// <summary>
      /// A structure that represents the .JEF file header.
      /// </summary>
      private struct FileHeader
      {
         public ArchitectureTypes ArchitectureType;
         public int BaseDataAddress;
         public int BaseTextAddress;
         public int BaseExternAddress;
         public int DataMetadataOffset;
         public int DataOffset;
         public int TextOffset;
         public int ExternOffset;
         public int SymTblOffset;
      }

      private readonly int m_BaseDataAddress;
      private readonly int m_BaseTextAddress;
      private readonly int m_BaseExternAddress;
      private readonly IEnumerable<MetadataElement> m_DataMetadata;
      private readonly IEnumerable<byte> m_DataElems;
      private readonly IEnumerable<byte> m_ExternElems;
      private readonly IEnumerable<int> m_TextElems;
      private readonly ReverseSymbolTable m_SymTbl;
   }
}
