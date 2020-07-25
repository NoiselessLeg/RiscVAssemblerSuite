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


   public class RegisterViewModel : BaseViewModel, IRegister<int>
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
                  dispStr = "0x" + Value.ToString("x8");
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




   public class FloatingPointRegisterViewModel : BaseViewModel, IRegister<float>
   {
      public FloatingPointRegisterViewModel(int registerIdx)
      {
         m_RegisterName = Common.ReverseRegisterMap.GetStringifiedFloatingPtRegisterValue(registerIdx);
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
                  dispStr = Value.ToString("0.0######");
                  break;
               }

               case RegisterDisplayType.Hexadecimal:
               {
                  byte[] bytes = BitConverter.GetBytes(Value);
                  int iVal = BitConverter.ToInt32(bytes, 0);
                  dispStr = "0x" + iVal.ToString("x8");
                  break;
               }
            }

            return dispStr;
         }
         set
         {
            if (Common.FltExtensions.TryParseEx(value, out float fVal))
            {
               if (Value != fVal)
               {
                  Value = fVal;
                  OnPropertyChanged();
               }
            }
         }
      }

      public string RegisterName
      {
         get { return m_RegisterName; }
      }

      public virtual float Value
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
      private float m_RegValue;
      private RegisterDisplayType m_DisplayType;
   }
}
