using Assembler.Common;
using Assembler.Disassembler.InstructionGenerators;
using Assembler.OutputProcessing;
using System.Collections.Generic;
using System.IO;

namespace Assembler.Disassembler
{
   internal class TextFileGenerator
   {
      /// <summary>
      /// Generates text output from the disassembled .JEF binary.
      /// </summary>
      /// <param name="outputFileName">The file to write to.</param>
      /// <param name="file">The disassembled .JEF file.</param>
      public void GenerateOutput(string outputFileName, DisassembledFile file)
      {
         IEnumerable<InstructionData> txtInstructions = DisassemblerServices.GenerateInstructionData(file.SymbolTable, file.TextSegment);
         using (var writer = new StreamWriter(File.Open(outputFileName, FileMode.Create)))
         {
            GenerateDataSegment(writer, file.SymbolTable, file.DataSegment);
            OutputTextSegment(writer, txtInstructions);
            writer.WriteLine(".extern extern_data " + file.ExternSegmentSize);
         }
      }

      /// <summary>
      /// Writes out the .data segment of the file.
      /// </summary>
      /// <param name="writer">The StreamWriter to use while writing the file.</param>
      /// <param name="symTable">The reverse lookup symbol table to use for symbol mapping.</param>
      /// <param name="dataSegment">The accessor to the file's data segment.</param>
      private void GenerateDataSegment(StreamWriter writer, ReverseSymbolTable symTable, DataSegmentAccessor dataSegment)
      {
         int currAddress = dataSegment.BaseRuntimeDataAddress;
         writer.WriteLine(".data");

         // first, see if there's a label associated with the data element.
         if (symTable.ContainsSymbol(currAddress))
         {
            Symbol sym = symTable.GetSymbol(currAddress);
            // if so, write it out.
            writer.Write(sym.LabelName);
            writer.Write(":\t\t");

            switch (sym.Size)
            {
               case sizeof(byte):
               {
                  writer.Write(".byte ");
                  byte value = dataSegment.ReadUnsignedByte(currAddress);
                  writer.WriteLine(value);
                  break;
               }

               case sizeof(short):
               {
                  writer.Write(".half ");
                  short value = dataSegment.ReadShort(currAddress);
                  writer.WriteLine(value);
                  break;
               }

               case sizeof(int):
               {
                  writer.Write(".word ");
                  int value = dataSegment.ReadWord(currAddress);
                  writer.WriteLine(value);
                  break;
               }

               case sizeof(long):
               {
                  writer.Write(".dword ");
                  long value = dataSegment.ReadLong(currAddress);
                  writer.WriteLine(value);
                  break;
               }

               default:
               {
                  writer.Write(".asciiz ");
                  string value = dataSegment.ReadString(currAddress);
                  string processedValue = ProcessString(value);
                  writer.WriteLine(processedValue);
                  break;
               }
            }

            currAddress += sym.Size;
         }

#if false
         int currAlignment = CommonConstants.DEFAULT_ALIGNMENT;
         int processedByteCount = 0;
         foreach (MetadataElement elem in dataSegment.Metadata)
         {
            // don't do any processing if we're looking at padding bytes.
            if (processedByteCount % currAlignment == 0)
            {
               // first, see if there's a label associated with the data element.
               if (symTable.ContainsSymbol(currAddress))
               {
                  // if so, write it out.
                  writer.Write(symTable.GetLabel(currAddress));
                  writer.Write(":\t\t");
               }
               else
               {
                  writer.Write("\t\t\t");
               }

               // determine what to write out.
               switch (elem.TypeCode)
               {
                  case ObjectTypeCode.Byte:
                  {
                     writer.Write(".byte ");
                     byte value = dataSegment.ReadUnsignedByte(currAddress);
                     writer.WriteLine(value);
                     processedByteCount += sizeof(byte);
                     break;
                  }

                  case ObjectTypeCode.Half:
                  {
                     writer.Write(".half ");
                     short value = dataSegment.ReadShort(currAddress);
                     writer.WriteLine(value);
                     processedByteCount += sizeof(short);
                     break;
                  }

                  case ObjectTypeCode.Word:
                  {
                     writer.Write(".word ");
                     int value = dataSegment.ReadWord(currAddress);
                     writer.WriteLine(value);
                     processedByteCount += sizeof(int);
                     break;
                  }

                  case ObjectTypeCode.Dword:
                  {
                     writer.Write(".dword ");
                     long value = dataSegment.ReadLong(currAddress);
                     writer.WriteLine(value);
                     processedByteCount += sizeof(long);
                     break;
                  }

                  case ObjectTypeCode.String:
                  {
                     writer.Write(".asciiz ");
                     string value = dataSegment.ReadString(currAddress);
                     string processedValue = ProcessString(value);
                     writer.WriteLine(processedValue);
                     processedByteCount += value.Length;

                     // account for a null terminating byte here.
                     ++processedByteCount;
                     break;
                  }

                  case ObjectTypeCode.AlignmentChange:
                  {
                     currAlignment = elem.Alignment;
                     break;
                  }
               }
            }
            else
            {
               // otherwise, assume we're at a padding byte and don't print it.
               ++processedByteCount;
            }
            
            currAddress += elem.Size;
         }
#endif

         }

      /// <summary>
      /// Processes a string for any special characters, and wraps it in quotes.
      /// </summary>
      /// <param name="parsedString">The string parsed from the .JEF file.</param>
      /// <returns>A string that is parsed for escape characters and wrapped in double-quotes.</returns>
      private string ProcessString(string parsedString)
      {
         string processedString = parsedString;
         processedString = processedString.Replace("\\", "\\\\");
         processedString = processedString.Replace("\n", "\\n");
         processedString = processedString.Replace("\t", "\\t");
         processedString = processedString.Replace("\"", "\\\"");
         processedString = processedString.Replace("\0", "\\0");
         return '\"' + processedString + '\"';
      }

      /// <summary>
      /// Writes out the .text segment of the file.
      /// </summary>
      /// <param name="writer">The StreamWriter to use while writing the file.</param>
      /// <param name="symTable">The reverse lookup symbol table to use for symbol mapping.</param>
      /// <param name="textSegment">The accessor to the file's text segment.</param>
      private void OutputTextSegment(StreamWriter writer, IEnumerable<InstructionData> instructions)
      {
         writer.WriteLine(".text");
         foreach (InstructionData instruction in instructions)
         {
            writer.WriteLine(instruction.Instruction);
         }
      }
   }
}
