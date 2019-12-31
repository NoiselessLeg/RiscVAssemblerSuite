using Assembler.FormsGui.Utility;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class RegisterViewModel : NotifyPropertyChangedBase
   {
      public RegisterViewModel(int registerIdx, ExecutionContext ctx)
      {
         m_Idx = registerIdx;
         m_Ctx = ctx;
         m_RegisterName = Common.ReverseRegisterMap.GetStringifiedRegisterValue(registerIdx);
      }

      public string RegisterName
      {
         get { return m_RegisterName; }
      }

      public int RegisterValue
      {
         get { return m_Ctx.UserRegisters[m_Idx].Value; }
         set
         {
            if (m_Ctx.UserRegisters[m_Idx].Value != value)
            {
               m_Ctx.UserRegisters[m_Idx].Value = value;
               OnPropertyChanged();
            }
         }
      }

      private readonly string m_RegisterName;
      private readonly int m_Idx;
      private readonly ExecutionContext m_Ctx;
   }
}
