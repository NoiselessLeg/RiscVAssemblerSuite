using Assembler.Common;
using Assembler.Disassembler.InstructionGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Disassembler
{
    class TextFileGenerator
    {
        /// <summary>
        /// Generates text output from the disassembled .JEF binary.
        /// </summary>
        /// <param name="outputFileName">The file to write to.</param>
        /// <param name="file">The disassembled .JEF file.</param>
        public void GenerateOutput(string outputFileName, DisassembledFile file)
        {
            using (var writer = new StreamWriter(File.Open(outputFileName, FileMode.Create)))
            {
                GenerateDataSegment(writer, file.SymbolTable, file.DataSegment);
                GenerateTextSegment(writer, file.SymbolTable, file.TextSegment);
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
            foreach (MetadataElement elem in dataSegment.Metadata)
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
                        byte value = dataSegment.ReadByte(currAddress);
                        writer.WriteLine(value);
                        break;
                    }

                    case ObjectTypeCode.Half:
                    {
                        writer.Write(".half ");
                        short value = dataSegment.ReadShort(currAddress);
                        writer.WriteLine(value);
                        break;
                    }

                    case ObjectTypeCode.Word:
                    {
                        writer.Write(".word ");
                        int value = dataSegment.ReadWord(currAddress);
                        writer.WriteLine(value);
                        break;
                    }

                    case ObjectTypeCode.Dword:
                    {
                        writer.Write(".dword ");
                        long value = dataSegment.ReadLong(currAddress);
                        writer.WriteLine(value);
                        break;
                    }

                    case ObjectTypeCode.String:
                    {
                        writer.Write(".asciiz ");
                        string value = dataSegment.ReadString(currAddress);
                        string processedValue = ProcessString(value);
                        writer.WriteLine(processedValue);
                        break;
                    }
                }

                currAddress += elem.Size;
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
        /// Writes out the .data segment of the file.
        /// </summary>
        /// <param name="writer">The StreamWriter to use while writing the file.</param>
        /// <param name="symTable">The reverse lookup symbol table to use for symbol mapping.</param>
        /// <param name="dataSegment">The accessor to the file's data segment.</param>
        private void GenerateTextSegment(StreamWriter writer, ReverseSymbolTable symTable, TextSegmentAccessor dataSegment)
        {
            int currPgrmCtr = dataSegment.StartingSegmentAddress;
            writer.WriteLine(".text");
            foreach (DisassembledInstruction inst in dataSegment.RawInstructions)
            {
                IParameterStringifier stringifier = InstructionTextMap.GetParameterStringifier(inst.InstructionType);
                string formattedInstruction = stringifier.GetFormattedInstruction(currPgrmCtr, inst, symTable);
                writer.WriteLine(formattedInstruction);
                currPgrmCtr += sizeof(int);
            }
        }
    }
}
