using Assembler.FormsGui.Utility;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public enum RegisterDisplayType
   {
      Decimal,
      Hexadecimal
   }

   public class ZeroRegisterViewModel : RegisterViewModel
   {
      public ZeroRegisterViewModel():
         base(0)
      {
      }

      public override int Value
      {
         get { return 0; }
         set { }
      }
   }


   public class RegisterViewModel : BaseViewModel, IRegister
   {
      public RegisterViewModel(int registerIdx)
      {
         m_RegisterName = Common.ReverseRegisterMap.GetStringifiedRegisterValue(registerIdx);
         Value = 0;
      }

      public RegisterDisplayType DisplayType
      {
         get { return m_DisplayType; }
         set
         {
            if (m_DisplayType != value)
            {
               m_DisplayType = value;
               OnPropertyChanged();
               OnPropertyChanged(nameof(Value));
               OnPropertyChanged(nameof(ValueStr));
            }
         }
      }

      public string ValueStr
      {
         get
         {
            string dispStr = string.Empty;
            switch (m_DisplayType)
            {
               case RegisterDisplayType.Decimal:
               {
                  dispStr = Value.ToString();
                  break;
               }

               case RegisterDisplayType.Hexadecimal:
               {
                  dispStr = "0x" + Value.ToString("x4");
                  break;
               }
            }

            return dispStr;
         }
         set
         {
            if (Common.IntExtensions.TryParseEx(value, out int iVal))
            {
               if (Value != iVal)
               {
                  Value = iVal;
                  OnPropertyChanged();
               }
            }
         }
      }

      public string RegisterName
      {
         get { return m_RegisterName; }
      }

      public virtual int Value
      {
         get { return m_RegValue; }
         set
         {
            if (m_RegValue != value)
            {
               m_RegValue = value;
               OnPropertyChanged();
               OnPropertyChanged(nameof(ValueStr));
            }
         }
      }
      
      private readonly string m_RegisterName;
      private int m_RegValue;
      private RegisterDisplayType m_DisplayType;
   }
}
