using Assembler.Common;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.DataModels
{
   public class LoggerModel : NotifyPropertyChangedBase, ILogger
   {
      public string LoggerOutput
      {
         get { return m_LogOutput; }
         set
         {
            if (m_LogOutput != value)
            {
               m_LogOutput = value;
               OnPropertyChanged();
            }
         }
      }

      public void Log(LogLevel level, string str)
      {
         var sb = new StringBuilder();
         switch (level)
         {
            case LogLevel.DebugFine:
            {
               sb.Append("DEBUG:\t\t");
               break;
            }
            case LogLevel.Info:
            {
               sb.Append("INFO:\t\t");
               break;
            }
            case LogLevel.Warning:
            {
               sb.Append("WARNING:\t\t");
               break;
            }
            case LogLevel.Critical:
            {
               sb.Append("CRITICAL:\t\t");
               break;
            }
         }
         sb.Append(str);
         sb.Append(Environment.NewLine);
         LoggerOutput += sb.ToString();
      }

      private string m_LogOutput;
   }
}
