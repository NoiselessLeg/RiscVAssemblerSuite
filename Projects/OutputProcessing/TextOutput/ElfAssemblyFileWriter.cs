using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.TextOutput
{
   public class ElfAssemblyFileWriter : IAssemblyFileWriter
   {
      public ElfAssemblyFileWriter(DisassembledFileBase underlyingFile)
      {
         m_UnderlyingFile = underlyingFile;
      }

      public void GenerateOutputFile(string outputFileName)
      {
         IEnumerable<InstructionData> txtInstructions =
            DisassemblerServices.GenerateInstructionData(m_UnderlyingFile.SymbolTable, m_UnderlyingFile.TextSegment);
         using (var writer = new StreamWriter(File.Open(outputFileName, FileMode.Create)))
         {
            GenerateDataSegment(writer, m_UnderlyingFile.SymbolTable, m_UnderlyingFile.DataSegment);
            OutputTextSegment(writer, txtInstructions);
            writer.WriteLine(".extern extern_data " + m_UnderlyingFile.ExternSegmentSize);
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

         int endAddress = currAddress + dataSegment.SegmentSize;

         while (currAddress < endAddress)
         {
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
                     currAddress += value.Length;
                     break;
                  }
               }
            }

            currAddress += sizeof(byte);
         }

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

      private readonly DisassembledFileBase m_UnderlyingFile;
   }
}
