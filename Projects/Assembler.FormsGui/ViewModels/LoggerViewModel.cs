using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class LoggerViewModel : NotifyPropertyChangedBase
   {
      public LoggerViewModel()
      {
         m_Model = new LoggerModel();
         m_Model.PropertyChanged += OnLogTextChanged;
      }

      public string LogText
      {
         get { return m_Model.LoggerOutput; }
      }

      public LoggerModel Logger
      {
         get { return m_Model; }
      }

      private void OnLogTextChanged(object sender, PropertyChangedEventArgs e)
      {
         OnPropertyChanged(nameof(LogText));
      }

      private readonly LoggerModel m_Model;
   }
}
