using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Simulation
{
   public class RegisterManager
   {
      public RegisterManager(int defaultPcRegisterValue, 
                             int defaultStackPtrValue)
      {
         m_BasicRegisters = new Register<int>[InterpreterCommon.MAX_BASIC_REGISTERS];

         m_OriginalIntRegisters = new int[m_BasicRegisters.Count];

         for (int i = 0; i < InterpreterCommon.MAX_BASIC_REGISTERS; ++i)
         {
            if (i == 0)
            {
               m_BasicRegisters.Add(new ZeroRegister());
            }
            else
            {
               m_BasicRegisters.Add(new Register<int>());
            }

            m_OriginalIntRegisters[i] = m_BasicRegisters[i].Value;
         }

         m_BasicRegisters[InterpreterCommon.PC_REGISTER].Value = defaultPcRegisterValue;
         m_BasicRegisters[InterpreterCommon.SP_REGISTER].Value = defaultStackPtrValue;

         m_OriginalIntRegisters[InterpreterCommon.PC_REGISTER] = defaultPcRegisterValue;
         m_OriginalIntRegisters[InterpreterCommon.SP_REGISTER] = defaultStackPtrValue;


         m_FpRegisters = new Register<float>[InterpreterCommon.MAX_FLOATING_PT_REGISTERS];
         m_OriginalFpRegisters = new float[m_FpRegisters.Count];

         for (int i = 0; i < InterpreterCommon.MAX_FLOATING_PT_REGISTERS; ++i)
         {
            m_FpRegisters.Add(new Register<float>());
            m_OriginalFpRegisters[i] = m_FpRegisters[i].Value;
         }
      }

      public RegisterManager(IList<IRegister<int>> intRegisters, 
                             IList<IRegister<float>> fltRegisters,
                             int defaultPcRegValue,
                             int defaultSpRegValue)
      {
         if (intRegisters.Count != InterpreterCommon.MAX_BASIC_REGISTERS)
         {
            throw new ArgumentException("intRegisters must have " + InterpreterCommon.MAX_BASIC_REGISTERS + " available elements.");
         }

         m_BasicRegisters = intRegisters;

         m_OriginalIntRegisters = new int[intRegisters.Count];
         
         for (int i = 0; i < InterpreterCommon.MAX_BASIC_REGISTERS; ++i)
         {
            m_OriginalIntRegisters[i] = m_BasicRegisters[i].Value;
         }

         m_BasicRegisters[InterpreterCommon.PC_REGISTER].Value = defaultPcRegValue;
         m_BasicRegisters[InterpreterCommon.SP_REGISTER].Value = defaultSpRegValue;

         m_OriginalIntRegisters[InterpreterCommon.PC_REGISTER] = defaultPcRegValue;
         m_OriginalIntRegisters[InterpreterCommon.SP_REGISTER] = defaultSpRegValue;

         if (fltRegisters.Count != InterpreterCommon.MAX_FLOATING_PT_REGISTERS)
         {
            throw new ArgumentException("fltRegisters must have " + InterpreterCommon.MAX_FLOATING_PT_REGISTERS + " available elements.");
         }

         m_FpRegisters = fltRegisters;

         m_OriginalFpRegisters = new float[fltRegisters.Count];

         for (int i = 0; i < InterpreterCommon.MAX_FLOATING_PT_REGISTERS; ++i)
         {
            m_OriginalFpRegisters[i] = m_FpRegisters[i].Value;
         }
      }

      public int GetOriginalValue(int regIdx)
      {
         return m_OriginalIntRegisters[regIdx];
      }

      public IList<IRegister<int>> UserIntRegisters
      {
         get { return m_BasicRegisters; }
      }

      public IList<IRegister<float>> UserFloatingPointRegisters
      {
         get { return m_FpRegisters; }
      }

      public void RestoreOriginalRegisterValues()
      {
         for (int i = 0; i < InterpreterCommon.MAX_BASIC_REGISTERS; ++i)
         {
            m_BasicRegisters[i].Value = m_OriginalIntRegisters[i];
         }

         for (int i = 0; i < InterpreterCommon.MAX_FLOATING_PT_REGISTERS; ++i)
         {
            m_FpRegisters[i].Value = m_OriginalFpRegisters[i];
         }
      }

      private readonly IList<IRegister<int>> m_BasicRegisters;
      private readonly IList<IRegister<float>> m_FpRegisters;

      private readonly int[] m_OriginalIntRegisters;
      private readonly float[] m_OriginalFpRegisters;
   }
}
