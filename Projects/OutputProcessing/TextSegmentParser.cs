using Assembler.OutputProcessing.ParamDecoding;
using System;
using System.Collections.Generic;

namespace Assembler.OutputProcessing
{
   internal class TextSegmentParser
   {
      /// <summary>
      /// Generates disassembled instructions from the 32-bit integers in the object file.
      /// </summary>
      /// <param name="textElems">The binary .text segment.</param>
      /// <returns>An IEnumerable of disassembled instructions.</returns>
      public IEnumerable<DisassembledInstruction> ParseTextSegment(IEnumerable<int> textElems)
      {
         var instructionList = new List<DisassembledInstruction>();
         foreach (int elem in textElems)
         {
            InstructionType iType = GetInstructionType(elem);

            IParameterDecoder paramDecoder = ParamDecoderTable.GetDecoderFor(iType);
            IEnumerable<int> parameters = paramDecoder.DecodeParameters(elem);

            instructionList.Add(new DisassembledInstruction(elem, iType, parameters));
         }

         return instructionList;
      }

      /// <summary>
      /// Gets the instruction type based on the opcode (among potential other bits in the instruction).
      /// </summary>
      /// <param name="instruction">The 32-bit instruction word to process.</param>
      /// <returns>The enumerated type representing the instruction.</returns>
      private static InstructionType GetInstructionType(int instruction)
      {
         InstructionType instType;

         // bitmask the first seven bits. this is our opcode.
         int opcode = (instruction & 0x7F);

         switch (opcode)
         {
            // LUI opcode.
            case 0x37:
            {
               instType = InstructionType.Lui;
               break;
            }

            // AUIPC opcode
            case 0x17:
            {
               instType = InstructionType.Auipc;
               break;
            }

            // JAL opcode
            case 0x6F:
            {
               instType = InstructionType.Jal;
               break;
            }

            // JALR opcode
            case 0x67:
            {
               instType = InstructionType.Jalr;
               break;
            }

            // branch opcode
            case 0x63:
            {
               instType = GetBranchInstructionType(instruction);
               break;
            }

            // load opcode
            case 0x03:
            {
               instType = GetLoadInstructionType(instruction);
               break;
            }

            // store opcode
            case 0x23:
            {
               instType = GetStoreInstructionType(instruction);
               break;
            }

            // immediate instruction opcode
            case 0x13:
            {
               instType = GetImmediateInstructionType(instruction);
               break;
            }

            // register instruction opcode
            case 0x33:
            {
               instType = GetRegisterInstructionType(instruction);
               break;
            }

            // ecall opcode.
            case 0x73:
            {
               int ebreakMask = instruction & 0x100000;
               if (ebreakMask == 0)
               {
                  instType = InstructionType.Ecall;
               }
               else
               {
                  instType = InstructionType.Ebreak;
               }
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized instruction \"0x" + instruction.ToString("x8") + "\"");
            }
         }



         return instType;
      }

      /// <summary>
      /// Determines what type of branch instruction this instruction represents based
      /// on the function code in the instruction.
      /// </summary>
      /// <param name="instruction">The 32-bit instruction word to process.</param>
      /// <returns>The enumerated type representing the instruction.</returns>
      private static InstructionType GetBranchInstructionType(int instruction)
      {
         // get the function code by masking bit offsets 12, 13, 14
         int functionCode = (instruction & 0x7000) >> 12;

         InstructionType type;
         switch (functionCode)
         {
            case 0x00:
            {
               type = InstructionType.Beq;
               break;
            }

            case 0x01:
            {
               type = InstructionType.Bne;
               break;
            }

            case 0x04:
            {
               type = InstructionType.Blt;
               break;
            }

            case 0x05:
            {
               type = InstructionType.Bge;
               break;
            }

            case 0x06:
            {
               type = InstructionType.Bltu;
               break;
            }

            case 0x07:
            {
               type = InstructionType.Bgeu;
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized branch function code " + functionCode);
            }
         }

         return type;
      }

      /// <summary>
      /// Determines what type of load instruction this instruction represents based
      /// on the function code in the instruction.
      /// </summary>
      /// <param name="instruction">The 32-bit instruction word to process.</param>
      /// <returns>The enumerated type representing the instruction.</returns>
      private static InstructionType GetLoadInstructionType(int instruction)
      {
         // get the function code by masking bit offsets 12, 13, 14
         int functionCode = (instruction & 0x7000) >> 12;

         InstructionType type;
         switch (functionCode)
         {
            case 0x00:
            {
               type = InstructionType.Lb;
               break;
            }

            case 0x01:
            {
               type = InstructionType.Lh;
               break;
            }

            case 0x02:
            {
               type = InstructionType.Lw;
               break;
            }

            case 0x04:
            {
               type = InstructionType.Lbu;
               break;
            }

            case 0x05:
            {
               type = InstructionType.Lhu;
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized load function code " + functionCode);
            }
         }

         return type;
      }

      /// <summary>
      /// Determines what type of store instruction this instruction represents based
      /// on the function code in the instruction.
      /// </summary>
      /// <param name="instruction">The 32-bit instruction word to process.</param>
      /// <returns>The enumerated type representing the instruction.</returns>
      private static InstructionType GetStoreInstructionType(int instruction)
      {
         // get the function code by masking bit offsets 12, 13, 14
         int functionCode = (instruction & 0x7000) >> 12;

         InstructionType type;
         switch (functionCode)
         {
            case 0x00:
            {
               type = InstructionType.Sb;
               break;
            }

            case 0x01:
            {
               type = InstructionType.Sh;
               break;
            }

            case 0x02:
            {
               type = InstructionType.Sw;
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized store function code " + functionCode);
            }
         }

         return type;
      }

      /// <summary>
      /// Determines what type of immediate instruction this instruction represents based
      /// on the primary and secondary function codes in the instruction.
      /// </summary>
      /// <param name="instruction">The 32-bit instruction word to process.</param>
      /// <returns>The enumerated type representing the instruction.</returns>
      private static InstructionType GetImmediateInstructionType(int instruction)
      {
         // get the function code by masking bit offsets 12, 13, 14
         int functionCode = (instruction & 0x7000) >> 12;

         InstructionType type;
         switch (functionCode)
         {
            case 0x00:
            {
               type = InstructionType.Addi;
               break;
            }

            case 0x01:
            {
               type = InstructionType.Slli;
               break;
            }

            case 0x02:
            {
               type = InstructionType.Slti;
               break;
            }

            case 0x03:
            {
               type = InstructionType.Sltiu;
               break;
            }

            case 0x04:
            {
               type = InstructionType.Xori;
               break;
            }

            case 0x05:
            {
               // 0x05 is unique, need to get the 30th bit offset to determine 
               int secondaryFuncCode = (instruction & 0x40000000) >> 30;
               switch (secondaryFuncCode)
               {
                  case 0x00:
                  {
                     type = InstructionType.Srli;
                     break;
                  }
                  case 0x01:
                  {
                     type = InstructionType.Srai;
                     break;
                  }

                  default:
                  {
                     throw new ArgumentException("Unrecognized immediate instruction type " + instruction);
                  }
               }
               break;
            }

            case 0x06:
            {
               type = InstructionType.Ori;
               break;
            }

            case 0x07:
            {
               type = InstructionType.Andi;
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized immediate function code " + functionCode);
            }
         }

         return type;
      }

      /// <summary>
      /// Determines what type of register instruction this instruction represents based
      /// on the primary and secondary function codes in the instruction.
      /// </summary>
      /// <param name="instruction">The 32-bit instruction word to process.</param>
      /// <returns>The enumerated type representing the instruction.</returns>
      private static InstructionType GetRegisterInstructionType(int instruction)
      {
         // get the function code by masking bit offsets 12, 13, 14
         int functionCode = (instruction & 0x7000) >> 12;

         InstructionType type;
         switch (functionCode)
         {
            case 0x00:
            {
               // 0x00 is unique, need to get the 30th bit offset to determine 
               int secondaryFuncCode = (instruction & 0x40000000) >> 30;
               switch (secondaryFuncCode)
               {
                  case 0x00:
                  {
                     type = InstructionType.Add;
                     break;
                  }
                  case 0x01:
                  {
                     type = InstructionType.Sub;
                     break;
                  }

                  default:
                  {
                     throw new ArgumentException("Unrecognized register instruction type " + instruction);
                  }
               }
               break;
            }

            case 0x01:
            {
               type = InstructionType.Sll;
               break;
            }

            case 0x02:
            {
               type = InstructionType.Slt;
               break;
            }

            case 0x03:
            {
               type = InstructionType.Sltu;
               break;
            }

            case 0x04:
            {
               type = InstructionType.Xor;
               break;
            }

            case 0x05:
            {
               // 0x05 is unique, need to get the 30th bit offset to determine 
               int secondaryFuncCode = (instruction & 0x40000000) >> 30;
               switch (secondaryFuncCode)
               {
                  case 0x00:
                  {
                     type = InstructionType.Srl;
                     break;
                  }
                  case 0x01:
                  {
                     type = InstructionType.Sra;
                     break;
                  }

                  default:
                  {
                     throw new ArgumentException("Unrecognized immediate instruction type " + instruction);
                  }
               }
               break;
            }

            case 0x06:
            {
               type = InstructionType.Or;
               break;
            }

            case 0x07:
            {
               type = InstructionType.And;
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized immediate function code " + functionCode);
            }
         }

         return type;
      }
   }
}
