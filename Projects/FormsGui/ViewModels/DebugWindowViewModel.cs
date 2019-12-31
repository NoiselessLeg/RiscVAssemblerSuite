using Assembler.Disassembler;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Utility;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class DebugWindowViewModel : NotifyPropertyChangedBase
   {
      public DebugWindowViewModel()
      {
         m_FileProc = new JefFileProcessor();
         m_RunFileCmd = new RelayCommand(
            (param) => LoadFile(param as string)
         );
      }


      public ICommand RunFileCommand
      {
         get { return m_RunFileCmd; }
      }

      private void LoadFile(string fileName)
      {
         DisassembledFile file = m_FileProc.ProcessJefFile(fileName, m_LoggerVm.Logger);
         

      }

      private readonly RelayCommand m_RunFileCmd;
      private readonly JefFileProcessor m_FileProc;
      private readonly LoggerViewModel m_LoggerVm;

      private readonly ObservableCollection<ExecutionViewModel> m_ExecutingFiles;
   }
}
