using Assembler.OutputProcessing.ParamDecoding;
using System;
using System.Collections.Generic;

namespace Assembler.OutputProcessing
{
   internal class TextSegmentParser
   {
      enum BaseOpcodes
      {
         Lui = 0x37,
         Auipc = 0x17,
         Jal = 0x6F,
         Jalr = 0x67,
         Branch = 0x63,
         Load = 0x03,
         Store = 0x23,
         Immediate = 0x13,
         Register = 0x33,
         Environment = 0x73,
         FloatOp = 0x53
      }

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
         BaseOpcodes opcode = ReadBaseOpcode(instruction);
         switch (opcode)
         {
            case BaseOpcodes.Lui:
            {
               instType = InstructionType.Lui;
               break;
            }
            
            case BaseOpcodes.Auipc:
            {
               instType = InstructionType.Auipc;
               break;
            }
            
            case BaseOpcodes.Jal:
            {
               instType = InstructionType.Jal;
               break;
            }
            
            case BaseOpcodes.Jalr:
            {
               instType = InstructionType.Jalr;
               break;
            }
            
            case BaseOpcodes.Branch:
            {
               instType = GetBranchInstructionType(instruction);
               break;
            }
            
            case BaseOpcodes.Load:
            {
               instType = GetLoadInstructionType(instruction);
               break;
            }
            
            case BaseOpcodes.Store:
            {
               instType = GetStoreInstructionType(instruction);
               break;
            }
            
            case BaseOpcodes.Immediate:
            {
               instType = GetImmediateInstructionType(instruction);
               break;
            }

            // register instruction opcode
            case BaseOpcodes.Register:
            {
               instType = GetRegisterInstructionType(instruction);
               break;
            }
            
            case BaseOpcodes.Environment:
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

            case BaseOpcodes.FloatOp:
            {
               instType = GetFloatingPointOpInstructionType(instruction);
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
         uint uInst = (uint)instruction;
         uint func7Code = (uInst & 0xFE000000) >> 25;

         InstructionType type;
         switch (func7Code)
         {
            case 0x0:
            {
               // get the function code by masking bit offsets 12, 13, 14
               int func3Code = (instruction & 0x7000) >> 12;
               switch (func3Code)
               {
                  case 0x0:
                  {
                     type = InstructionType.Add;
                     break;
                  }

                  case 0x1:
                  {
                     type = InstructionType.Slli;
                     break;
                  }

                  case 0x2:
                  {
                     type = InstructionType.Slt;
                     break;
                  }

                  case 0x3:
                  {
                     type = InstructionType.Sltu;
                     break;
                  }

                  case 0x4:
                  {
                     type = InstructionType.Xor;
                     break;
                  }

                  case 0x5:
                  {
                     type = InstructionType.Srl;
                     break;
                  }

                  case 0x6:
                  {
                     type = InstructionType.Or;
                     break;
                  }

                  case 0x7:
                  {
                     type = InstructionType.And;
                     break;
                  }

                  default:
                     throw new InvalidOperationException("Should never get to this point, exhausted all three bits");
               }
               
               break;
            }

            case 0x1:
            {
               // get the function code by masking bit offsets 12, 13, 14
               int func3Code = (instruction & 0x7000) >> 12;
               switch (func3Code)
               {
                  case 0x0:
                  {
                     type = InstructionType.Mul;
                     break;
                  }

                  case 0x1:
                  {
                     type = InstructionType.Mulh;
                     break;
                  }

                  case 0x2:
                  {
                     type = InstructionType.Mulhsu;
                     break;
                  }

                  case 0x3:
                  {
                     type = InstructionType.Mulhu;
                     break;
                  }

                  case 0x4:
                  {
                     type = InstructionType.Div;
                     break;
                  }
                  case 0x5:
                  {
                     type = InstructionType.Divu;
                     break;
                  }

                  case 0x6:
                  {
                     type = InstructionType.Rem;
                     break;
                  }

                  case 0x7:
                  {
                     type = InstructionType.Remu;
                     break;
                  }

                  default:
                  {
                     throw new ArgumentException("Unrecognized register instruction type " + instruction);
                  }
               }
               break;
            }

            case 0x20:
            {
               // get the function code by masking bit offsets 12, 13, 14
               int func3Code = (instruction & 0x7000) >> 12;
               switch (func3Code)
               {
                  case 0x0:
                  {
                     type = InstructionType.Sub;
                     break;
                  }

                  case 0x5:
                  {
                     type = InstructionType.Sra;
                     break;
                  }

                  default:
                  {
                     throw new ArgumentException("Unrecognized register instruction type " + instruction);
                  }
               }
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized register instruction type " + instruction);
            }
         }

         return type;
      }

      private static InstructionType GetFloatingPointOpInstructionType(int instruction)
      {
         // C# doesn't realize at this point that this doesn't really matter
         // (we just want to use this as a mask to get the upper 32 bits
         const uint FUNC_MASK = 0xF8000000;
         uint uInstruction = (uint)instruction;
         uint funcCode = ((uInstruction & FUNC_MASK)) >> 27;

         InstructionType type;
         switch (funcCode)
         {
            case 0x0:
            {
               type = InstructionType.FaddS;
               break;
            }

            case 0x1:
            {
               type = InstructionType.FsubS;
               break;
            }
            case 0x2:
            {
               type = InstructionType.FmulS;
               break;
            }

            case 0x3:
            {
               type = InstructionType.FdivS;
               break;
            }

            case 0x5:
            {
               // get the function code by masking bit offsets 12, 13, 14
               int func3Code = (instruction & 0x7000) >> 12;
               switch (func3Code)
               {
                  case 0x0:
                  {
                     type = InstructionType.FminS;
                     break;
                  }

                  case 0x1:
                  {
                     type = InstructionType.FmaxS;
                     break;
                  }

                  default:
                  {
                     throw new ArgumentException("Unrecognized floating point instruction type " + instruction);
                  }
               }
               break;
            }

            case 0xB:
            {
               type = InstructionType.FsqrtS;
               break;
            }

            case 0x18:
            {
               type = InstructionType.FcvtWS;
               break;
            }

            case 0x1A:
            {
               type = InstructionType.FcvtSW;
               break;
            }

            default:
            {
               throw new ArgumentException("Unrecognized floating point function code " + funcCode + ".");
            }
         }

         return type;

      }

      private static BaseOpcodes ReadBaseOpcode(int instruction)
      {
         // bitmask the first seven bits. this is our opcode.
         return (BaseOpcodes)(instruction & 0x7F);
      }
   }
}
