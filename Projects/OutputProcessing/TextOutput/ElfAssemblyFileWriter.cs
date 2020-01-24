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

         // get the address of each symbol in the .data segment, and sort
         // them in ascending order.
         var symbolAddresses = symTable.AllSymbols
            .SelectIf(sym => sym.Address,
               sym => dataSegment.BaseRuntimeDataAddress <= sym.Address && sym.Address < endAddress)
            .OrderBy(num => num);

         while (currAddress < endAddress)
         {
            // are there any addresses in our symbol table that fall between our current
            // address and the next address we'll examine?
            if (symbolAddresses.Any(address => currAddress <= address && address < (currAddress + sizeof(int))))
            {
               // okay, slightly narrow our screen 
               if (symbolAddresses.Any(address => currAddress <= address && address < (currAddress + sizeof(short))))
               {
                  if (symTable.ContainsSymbol(currAddress))
                  {
                     Symbol sym = symTable.GetSymbol(currAddress);
                     writer.Write(sym.LabelName);
                     writer.Write(":\t\t");
                     switch (sym.Size)
                     {
                        case sizeof(byte):
                        {
                           writer.Write(".byte ");
                           sbyte value = dataSegment.ReadSignedByte(currAddress);
                           writer.WriteLine(value.ToString(StringifyAsHexadecimal(value, 2)));
                           currAddress += sizeof(byte);
                           break;
                        }

                        case sizeof(short):
                        {
                           writer.Write(".half ");
                           short value = dataSegment.ReadShort(currAddress);
                           writer.WriteLine(StringifyAsHexadecimal(value, 4));
                           currAddress += sizeof(short);
                           break;
                        }

                        case sizeof(int):
                        {
                           writer.Write(".word ");
                           int value = dataSegment.ReadWord(currAddress);
                           writer.WriteLine(StringifyAsHexadecimal(value, 8));
                           currAddress += sizeof(int);
                           break;
                        }

                        case sizeof(long):
                        {
                           writer.Write(".dword ");
                           long value = dataSegment.ReadLong(currAddress);
                           writer.WriteLine(StringifyAsHexadecimal(value, 16));
                           currAddress += sizeof(int);
                           break;
                        }

                        default:
                        {
                           // just be cheap for now and write out numeric values
                           // (even though this could be an ascii string, in theory).
                           // maybe at some point the assembler will be nice enough
                           // to force strings into .strtable section for smarter
                           // heuristics, but we don't want to force that upon the user
                           // unless the user specifically directs us so.
                           // (or maybe we do?). if someone thinks of a better
                           // algorithm that can be done in the disassembler, I'm
                           // al ears.
                           int numWordsInSize = sym.Size / sizeof(int);
                           int numLeftover = sym.Size % sizeof(int);

                           int numHalfWordsInRemainingBytes = numLeftover / sizeof(int);
                           int numRemainingBytes = numLeftover % sizeof(int);


                           for (int i = 0; i < numWordsInSize; ++i)
                           {
                              writer.Write(".word ");
                              int value = dataSegment.ReadWord(currAddress);
                              writer.WriteLine(StringifyAsHexadecimal(value, 8));
                              currAddress += sizeof(int);
                           }

                           for (int i = 0; i < numHalfWordsInRemainingBytes; ++i)
                           {
                              writer.Write(".half ");
                              short value = dataSegment.ReadShort(currAddress);
                              writer.WriteLine(StringifyAsHexadecimal(value, 4));
                              currAddress += sizeof(short);
                           }

                           for (int i = 0; i < numRemainingBytes; ++i)
                           {
                              writer.Write(".byte ");
                              sbyte value = dataSegment.ReadSignedByte(currAddress);
                              writer.WriteLine(StringifyAsHexadecimal(value, 2));
                              currAddress += sizeof(byte);
                           }

                           break;
                        }

                        // todo: determine if we need to perform alignment
                        // detection (as in theory at this point we could judge
                        // if the new byte offset is on a word boundary).
                     }

                  }
                  else
                  {

                     writer.Write(".byte ");
                     sbyte value = dataSegment.ReadSignedByte(currAddress);
                     writer.WriteLine(value.ToString(StringifyAsHexadecimal(value, 2)));
                     currAddress += sizeof(byte);
                     break;
                  }


               }
               else
               {
                  writer.Write(".half ");
                  short value = dataSegment.ReadShort(currAddress);
                  writer.WriteLine(StringifyAsHexadecimal(value, 4));
                  currAddress += sizeof(short);
               }
            }
            else
            {
               writer.Write(".word ");
               int value = dataSegment.ReadWord(currAddress);
               writer.WriteLine(StringifyAsHexadecimal(value, 8));
               currAddress += sizeof(int);
            }
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

      private static string StringifyAsHexadecimal(long value, int numPlaces)
      {
         return "0x" + value.ToString("x" + numPlaces);
      }

      private static string StringifyAsHexadecimal(int value, int numPlaces)
      {
         return "0x" + value.ToString("x" + numPlaces);
      }

      private static string StringifyAsHexadecimal(short value, int numPlaces)
      {
         return "0x" + value.ToString("x" + numPlaces);
      }

      private static string StringifyAsHexadecimal(sbyte value, int numPlaces)
      {
         return "0x" + value.ToString("x" + numPlaces);
      }

      private readonly DisassembledFileBase m_UnderlyingFile;
   }
}
