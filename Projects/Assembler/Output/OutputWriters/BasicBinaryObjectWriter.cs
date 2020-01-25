using Assembler.Common;
using Assembler.Output.ObjFileComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assembler.Output.OutputWriters
{
   /// <summary>
   /// A class that represents a very basic output writer. This writes the data with no formatting,
   /// other than prefixing the type of data it is writing. This should be used for debugging purposes.
   /// </summary>
   internal class BasicBinaryObjectWriter : IObjectFileWriter
   {
      /// <summary>
      /// Outputs all data in the BasicObjectFile to the specified format.
      /// </summary>
      /// <param name="fileName">The file path to generate the output at.</param>
      /// <param name="file">The data that will be written to the file.</param>
      public void WriteObjectFile(string fileName, BasicObjectFile file)
      {
         using (FileStream fs = File.Open(fileName, FileMode.Create))
         {
            using (MemoryStream tmpStrm = new MemoryStream())
            {
               // write the .data segment metadata
               SegmentData dataMdataInfo;
               dataMdataInfo.SegmentName = ".dmdta";
               dataMdataInfo.StartingOffset = CalculateSegmentOffset(tmpStrm.Position);
               dataMdataInfo.SegmentSize = WriteMetadataToFile(tmpStrm, file.DataElements);

               // write the actual .data segment
               SegmentData dataInfo;
               dataInfo.SegmentName = ".data";
               dataInfo.StartingOffset = CalculateSegmentOffset(tmpStrm.Position);
               dataInfo.SegmentSize = WriteDataToFile(tmpStrm, file.DataElements);

               SegmentData textInfo;
               textInfo.SegmentName = ".text";
               textInfo.StartingOffset = CalculateSegmentOffset(tmpStrm.Position);
               textInfo.SegmentSize = WriteDataToFile(tmpStrm, file.TextElements);

               // write the .extern segment.
               SegmentData externInfo;
               externInfo.SegmentName = ".extern";
               externInfo.StartingOffset = CalculateSegmentOffset(tmpStrm.Position);
               externInfo.SegmentSize = WriteDataToFile(tmpStrm, file.ExternElements);

               // write the symbol table
               SegmentData symInfo;
               symInfo.SegmentName = ".symtbl";
               symInfo.StartingOffset = CalculateSegmentOffset(tmpStrm.Position);
               symInfo.SegmentSize = WriteSymbolTableToFile(tmpStrm, file.SymbolTable);

               // write the source map table segment.
               SegmentData srcMapInfo;
               srcMapInfo.SegmentName = ".srcmap";
               srcMapInfo.StartingOffset = CalculateSegmentOffset(tmpStrm.Position);
               srcMapInfo.SegmentSize = WriteSourceMappingInfoToFile(tmpStrm, file.DebugData);

               // add the various sizes.
               // the order matters, as this is the order in which they are written to the header.
               var sizeList = new List<SegmentData>();
               sizeList.Add(dataMdataInfo);
               sizeList.Add(dataInfo);
               sizeList.Add(textInfo);
               sizeList.Add(srcMapInfo);
               sizeList.Add(externInfo);
               sizeList.Add(symInfo);

               // write the actual file header, now that we know our absolute offsets
               WriteHeader(fs, sizeList);

               // copy the temp stream to the actual file stream.
               tmpStrm.Seek(0, SeekOrigin.Begin);
               tmpStrm.CopyTo(fs);
            }
         }
      }

      /// <summary>
      /// Writes the actual elements of a segment to a stream.
      /// </summary>
      /// <param name="stream">The Stream object to write to.</param>
      /// <param name="elements">The IEnumerable of elements to write out.</param>
      /// <returns>The number of bytes written.</returns>
      private int WriteDataToFile(Stream stream, IEnumerable<IObjectFileComponent> elements)
      {
         long startPos = stream.Position;
         foreach (IObjectFileComponent elem in elements)
         {
            elem.WriteDataToFile(stream);
         }
         long newPos = stream.Position;

         return (int)(newPos - startPos);
      }

      private int WriteSymbolTableToFile(Stream stream, SymbolTable symTable)
      {
         long startPos = stream.Position;
         foreach (Symbol elem in symTable.Symbols)
         {
            // write the length of the symbol name, first.
            stream.WriteByte((byte)elem.LabelName.Length);

            // write the symbol name itself.
            stream.Write(Encoding.ASCII.GetBytes(elem.LabelName), 0, Encoding.ASCII.GetByteCount(elem.LabelName));
            byte[] byteRepresentation = ToByteArray(elem.Address);

            stream.Write(byteRepresentation, 0, byteRepresentation.Length);
         }
         long newPos = stream.Position;

         return (int)(newPos - startPos);
      }

      private int WriteSourceMappingInfoToFile(Stream stream, SourceDebugData dbgData)
      {
         byte[] srcFileNameBytes = Encoding.ASCII.GetBytes(dbgData.SourceFilePath);
         byte[] nullTerminatedByteArr = new byte[srcFileNameBytes.Length + 1];
         Array.Copy(srcFileNameBytes, nullTerminatedByteArr, srcFileNameBytes.Length);

         long startPos = stream.Position;
         stream.Write(nullTerminatedByteArr, 0, nullTerminatedByteArr.Length);

         foreach (SourceLineInformation srcLine in dbgData.SourceInfo)
         {
            short lineNum = (short)srcLine.SourceFileLineNumber;
            int pgmCtrAddr = srcLine.TextSegmentAddress;
            byte[] lineNumBytes = ToByteArray(lineNum);
            byte[] pgmCtrBytes = ToByteArray(pgmCtrAddr);
            stream.Write(lineNumBytes, 0, lineNumBytes.Length);
            stream.Write(pgmCtrBytes, 0, pgmCtrBytes.Length);
         }
         long newPos = stream.Position;

         return (int)(newPos - startPos);
      }


      /// <summary>
      /// Writes metadata about a segment to a stream.
      /// </summary>
      /// <param name="stream">The Stream object to write to.</param>
      /// <param name="elements">The IEnumerable of elements to write out the metadata of.</param>
      /// <returns>The number of bytes written.</returns>
      private int WriteMetadataToFile(Stream stream, IEnumerable<IObjectFileComponent> elements)
      {
         long startPos = stream.Position;
         foreach (IObjectFileComponent elem in elements)
         {
            elem.WriteMetadataToFile(stream);
         }
         long newPos = stream.Position;

         return (int)(newPos - startPos);
      }

      /// <summary>
      /// Writes the JEF file header to the file.
      /// </summary>
      /// <param name="fs">The FileStream object to write to.</param>
      /// <param name="segmentSizes">An IEnumerable of segment sizes, in the order that they appear in the file by the standard.</param>
      private void WriteHeader(Stream fs, IEnumerable<SegmentData> segmentSizes)
      {
         // write the magic four bytes
         fs.WriteByte(0x7F);

         // the lamest output format type.
         fs.Write(Encoding.ASCII.GetBytes("JEF"), 0, Encoding.ASCII.GetByteCount("JEF"));

         // we're using 32-bits for now, so set this to one.
         // maybe if we deal with 64 bits, make this 2.
         fs.WriteByte(0x01);

         // write the endianness byte.
         // JAL 4/18: set to 0, spare bit (endianness will always be LITTLE).
         fs.WriteByte(0x00);

         // write the runtime starting addresses.
         // TODO: eventually make this configurable
         byte[] dataRuntimeAddr = ToByteArray(CommonConstants.BASE_DATA_ADDRESS);
         byte[] textRuntimeAddr = ToByteArray(CommonConstants.BASE_TEXT_ADDRESS);
         byte[] externRuntimeAddr = ToByteArray(CommonConstants.BASE_EXTERN_ADDRESS);
         fs.Write(dataRuntimeAddr, 0, dataRuntimeAddr.Length);
         fs.Write(textRuntimeAddr, 0, textRuntimeAddr.Length);
         fs.Write(externRuntimeAddr, 0, externRuntimeAddr.Length);

         // write the offset of the first segment after the header, since this is known.
#if false
         byte[] dataMetadataOffset = ToByteArray(0x30 - 0x12);
         fs.Write(dataMetadataOffset, 0, dataMetadataOffset.Length);
#endif

         // calculate the relative offsets to the various segments.
         // the first segment should begin at 0x30, and we should start writing
         // this section at offset 0x12. 
         int writePosition = 0x12;

         foreach (SegmentData segInfo in segmentSizes)
         {
            if (segInfo.SegmentSize != UNUSED_SECTION_BYTE_SIZE)
            {
               int relativeOffset = segInfo.StartingOffset - writePosition;
               byte[] relativeOffsetBytes = ToByteArray(relativeOffset);
               fs.Write(relativeOffsetBytes, 0, relativeOffsetBytes.Length);
            }
            else
            {
               byte[] zBytes = new byte[] { 0, 0, 0, 0 };
               fs.Write(zBytes, 0, zBytes.Length);
            }
            writePosition += sizeof(int);
         }
#if false
         // do everything BUT the last segment size, since that would add an extra
         // word to the preamble (and only gives us the offset of the end of the file).
         for (int i = 0; i < segmentSizes.Count() - 1; ++i)
         {
            SegmentData segInfo = segmentSizes.ElementAt(i);

            // if the size is not the sentry value,
            // calculate the relative byte offset in the file
            // of the section. otherwise, just write zeroes for the offset.
            if (segInfo.SegmentSize != UNUSED_SECTION_BYTE_SIZE)
            {
               int relativeOffset = segInfo.StartingOffset - writePosition;

               byte[] relativeOffsetBytes = ToByteArray(relativeOffset);
               fs.Write(relativeOffsetBytes, 0, relativeOffsetBytes.Length);
            }
            else
            {
               byte[] zBytes = new byte[] { 0, 0, 0, 0 };
               fs.Write(zBytes, 0, zBytes.Length);
            }

            writePosition += sizeof(int);
         }
#endif

         System.Diagnostics.Debug.Assert(writePosition == 0x2A);

         // write the spare values.
         byte[] dummyValues = new byte[6];
         fs.Write(dummyValues, 0, dummyValues.Length);
      }

      /// <summary>
      /// Gets the provided 16 bit value as a byte array.
      /// </summary>
      /// <param name="param">The value to convert to bytes.</param>
      private static byte[] ToByteArray(short param)
      {
         byte[] byteRep = BitConverter.GetBytes(param);

         // if the architecture we're assembling on is not our desired endianness,
         // flip the byte array.
         if (!BitConverter.IsLittleEndian)
         {
            Array.Reverse(byteRep);
         }
         return byteRep;
      }

      /// <summary>
      /// Gets the provided 32 bit value as a byte array.
      /// </summary>
      /// <param name="param">The value to convert to bytes.</param>
      private static byte[] ToByteArray(int param)
      {
         byte[] byteRep = BitConverter.GetBytes(param);

         // if the architecture we're assembling on is not our desired endianness,
         // flip the byte array.
         if (!BitConverter.IsLittleEndian)
         {
            Array.Reverse(byteRep);
         }
         return byteRep;
      }

      private static int CalculateSegmentOffset(long streamPos)
      {
         const int STARTING_OFFSET = 0x30;
         return STARTING_OFFSET + (int)streamPos;
      }

      private struct SegmentData
      {
         public string SegmentName;
         public int StartingOffset;
         public int SegmentSize;
      }

      private const int UNUSED_SECTION_BYTE_SIZE = -1;
   }
}
