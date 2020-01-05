using System.Collections.Generic;

namespace Assembler.OutputProcessing
{
   /// <summary>
   /// Represents a disassembled instruction from a file.
   /// </summary>
   public class DisassembledInstruction
   {
      public DisassembledInstruction(int rawInstructionWord, InstructionType type, IEnumerable<int> instParams)
      {
         m_InstructionWord = rawInstructionWord;
         m_Type = type;
         m_Params = instParams;
      }

      /// <summary>
      /// Gets the raw 32-bit word that describes the instruction.
      /// </summary>
      public int InstructionWord => m_InstructionWord;

      /// <summary>
      /// Gets the instruction type.
      /// </summary>
      public InstructionType InstructionType => m_Type;

      /// <summary>
      /// Gets the parameters associated with the instruction.
      /// </summary>
      public IEnumerable<int> Parameters => m_Params;


      private readonly int m_InstructionWord;
      private readonly InstructionType m_Type;
      private readonly IEnumerable<int> m_Params;
   }
}
