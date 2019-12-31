using Assembler;
using Assembler.Common;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
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
   public class WindowViewModel : NotifyPropertyChangedBase
   {
      public WindowViewModel()
      {
         m_MsgQueue = new ObservableQueue<IBasicMessage>();
         m_MsgQueue.ItemEnqueued += OnExternalMsgEnqueued;
         m_MsgMgr = new MessageManager();
         m_MsgQueueId = m_MsgMgr.RegisterMessageQueue(m_MsgQueue);

         m_Views = new ObservableCollection<ViewBase>();

         int viewIdCtr = 0;
         m_Views.Add(new AssemblyEditorView(viewIdCtr++, m_MsgMgr));
         m_Views.Add(new HexExplorerView(viewIdCtr++, m_MsgMgr));
         m_Views.Add(new DebugView(viewIdCtr++, m_MsgMgr));

         m_ChangeActiveIdxCmd = new RelayCommand(
            (param) =>
            {
               int? idx = param as int?;
               System.Diagnostics.Debug.Assert(idx.HasValue);
               ActiveViewIndex = idx.Value;
            }
         );
      }

      public IBasicView ActiveView
      {
         get { return ViewList[ActiveViewIndex]; }
      }

      public ObservableCollection<ViewBase> ViewList
      {
         get { return m_Views; }
      }

      public ICommand ChangeActiveViewCommand
      {
         get { return m_ChangeActiveIdxCmd; }
      }

      public int ActiveViewIndex
      {
         get { return m_ActiveIdx; }
         private set
         {
            if (m_ActiveIdx != value)
            {
               m_ActiveIdx = value;
               OnPropertyChanged();
               OnPropertyChanged("ActiveView");
            }
         }
      }

      private void OnExternalMsgEnqueued(object sender, EventArgs e)
      {
         IBasicMessage msg = m_MsgQueue.Dequeue();
         switch (msg.MessageType)
         {
            case MessageType.ActiveViewRequest:
               msg.HandleMessage(ChangeActiveViewCommand);
               break;
         }

      }

      private int m_ActiveIdx;
      private readonly int m_MsgQueueId;
      private readonly MessageManager m_MsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_MsgQueue;

      private readonly ObservableCollection<ViewBase> m_Views;
      private readonly RelayCommand m_ChangeActiveIdxCmd;
      
   }
}
