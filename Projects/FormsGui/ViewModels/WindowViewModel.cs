using Assembler;
using Assembler.Common;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.ViewModels
{
   public class WindowViewModel
   {
      public WindowViewModel()
      {
         m_Views = new ObservableCollection<IBasicView>();
         m_SaveFileCommand = new RelayCommand(param => SaveFile(param as AssemblyFileViewModel));
         m_AssembleCommand = new RelayCommand(param => AssembleFile(param as AssemblyFileViewModel));
      }

      private void SaveFile(AssemblyFileViewModel avm)
      {
         avm.SaveFile();
      }

      private void AssembleFile(AssemblyFileViewModel avm)
      {
      }
      
      public ICommand SaveFileCommand
      {
         get { return m_SaveFileCommand; }
      }

      public ICommand AssembleFileCommand
      {
         get { return m_AssembleCommand; }
      }

      public ObservableCollection<IBasicView> ViewList
      {
         get { return m_Views; }
      }

      private readonly ObservableCollection<IBasicView> m_Views;
      private readonly RelayCommand m_SaveFileCommand;
      private readonly RelayCommand m_AssembleCommand;
      
   }
}
