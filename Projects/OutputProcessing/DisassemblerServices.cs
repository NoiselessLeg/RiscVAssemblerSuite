﻿using Assembler.Common;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.TextOutput;
using Assembler.OutputProcessing.TextOutput.InstructionGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
{
   public class DisassemblerServices
   {
      public static IEnumerable<InstructionData> GenerateInstructionData(ReverseSymbolTable symTable,
                                                                         TextSegmentAccessor textSegment,
                                                                         SourceDebugData dbgData)
      {
         IEnumerable<InstructionData> ret = null;
         if (File.Exists(dbgData.SourceFilePath))
         {
            try
            {
               ret = GenerateInstructionDataWithSource(symTable, textSegment, dbgData);
            }
            catch (Exception)
            {
               // if anything goes wrong, try just parsing the file with no source.
               ret = GenerateInstructionDataWithNoSource(symTable, textSegment);
            }
         }
         else
         {
            ret = GenerateInstructionDataWithNoSource(symTable, textSegment);
         }

         return ret;
      }

      private static IEnumerable<InstructionData> GenerateInstructionDataWithSource(ReverseSymbolTable symTable,
                                                                                    TextSegmentAccessor textSegment,
                                                                                    SourceDebugData dbgData)
      {
         using (var reader = File.OpenText(dbgData.SourceFilePath))
         {
            var instructions = new List<InstructionData>();
            int currPgrmCtr = textSegment.StartingSegmentAddress;
            foreach (DisassembledInstruction inst in textSegment.RawInstructions)
            {
               IParameterStringifier stringifier = InstructionTextMap.GetParameterStringifier(inst.InstructionType);
               string formattedInstruction = stringifier.GetFormattedInstruction(currPgrmCtr, inst, symTable);
               string originalSourceLine = string.Empty;
               if (dbgData.IsSourceTextAssociatedWithAddress(currPgrmCtr))
               {
                  int sourceLineNum = dbgData.GetLineNumberAssociatedWithAddress(currPgrmCtr);
                  originalSourceLine = reader.ReadLineAt(sourceLineNum);
                  originalSourceLine = originalSourceLine.Trim();
               }
               var instructionElem = new InstructionData(inst.InstructionWord, currPgrmCtr, formattedInstruction, originalSourceLine);
               instructions.Add(instructionElem);
               currPgrmCtr += sizeof(int);
            }

            return instructions;
         }
               
      }

      private static IEnumerable<InstructionData> GenerateInstructionDataWithNoSource(ReverseSymbolTable symTable,
                                                                                      TextSegmentAccessor textSegment)
      {
         var instructions = new List<InstructionData>();
         int currPgrmCtr = textSegment.StartingSegmentAddress;
         foreach (DisassembledInstruction inst in textSegment.RawInstructions)
         {
            IParameterStringifier stringifier = InstructionTextMap.GetParameterStringifier(inst.InstructionType);
            string formattedInstruction = stringifier.GetFormattedInstruction(currPgrmCtr, inst, symTable);
            var instructionElem = new InstructionData(inst.InstructionWord, currPgrmCtr, formattedInstruction, string.Empty);
            instructions.Add(instructionElem);
            currPgrmCtr += sizeof(int);
         }

         return instructions;
      }

   }

   
}
