using Assembler;
using Assembler.Common;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
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
         m_MsgMgr = new MessageManager();
         m_Views = new ObservableCollection<ViewBase>();
         
         m_Views.Add(new AssemblyEditorView(m_MsgMgr));
         m_Views.Add(new HexExplorerView(m_MsgMgr));
         m_Views.Add(new DebugView(m_MsgMgr));
      }

      public ObservableCollection<ViewBase> ViewList
      {
         get { return m_Views; }
      }

      private readonly MessageManager m_MsgMgr;
      private readonly ObservableCollection<ViewBase> m_Views;
      private readonly RelayCommand m_SaveFileCommand;
      private readonly RelayCommand m_AssembleCommand;
      
   }
}
