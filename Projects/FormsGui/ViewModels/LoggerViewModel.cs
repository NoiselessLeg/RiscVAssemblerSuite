using Assembler.FormsGui.Commands;
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
         m_ClearLogCmd = new RelayCommand((param) => ClearLogOutput());

         // this is different than how we're doing binding on other view models.
         // theoretically (not that the assembler is doing this now :)) the logger
         // could be running in real time. Other view models create a whole new view
         // model with a backing model once they're done doing what they need to,
         // but here we can't or otherwise we don't get any realtime log values.
         m_Model.PropertyChanged += OnLogTextChanged;
      }

      public ICommand ClearLogCommand
      {
         get { return m_ClearLogCmd; }
      }

      public string LogText
      {
         get { return m_Model.LoggerOutput; }
      }

      public LoggerModel Logger
      {
         get { return m_Model; }
      }

      private void ClearLogOutput()
      {
         m_Model.LoggerOutput = "";
         OnPropertyChanged(nameof(LogText));
      }

      private void OnLogTextChanged(object sender, PropertyChangedEventArgs e)
      {
         OnPropertyChanged(nameof(LogText));
      }

      private readonly RelayCommand m_ClearLogCmd;
      private readonly LoggerModel m_Model;
   }
}
