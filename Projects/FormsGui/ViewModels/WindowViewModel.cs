using Assembler;
using Assembler.Common;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using Assembler.FormsGui.Views;
using Assembler.FormsGui.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.ViewModels
{
   public class WindowViewModel : MessagingViewModel
   {
      public WindowViewModel() :
         base(MessageManager.GetInstance())
      {

         m_Preferences = new PreferencesViewModel();
         try
         {
            var settingsLoader = new SettingsFileLoader(PREFS_FILENAME);
            m_Preferences.LoadFromStorage(settingsLoader);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Preferences Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         }

         m_Views = new ObservableCollection<ViewBase>();

         int viewIdCtr = 0;

         MessageManager mgr = MessageManager.GetInstance();
         var assemblyEditorView = new AssemblyEditorView(viewIdCtr++, mgr, m_Preferences);
         var dbgView = new DebugView(viewIdCtr++, mgr);
         var hexEditorView = new HexExplorerView(viewIdCtr++, mgr);

         m_Views.Add(assemblyEditorView);
         m_Views.Add(dbgView);
         m_Views.Add(hexEditorView);

         m_ChangeActiveIdxCmd = new RelayCommand<int>((param) => ActiveViewIndex = param, true);
         m_NewFileCmd = new RelayCommand(() => SendFileMessage(MessageType.CreateFileRequest), true);
         m_OpenFileCmd = new RelayCommand(() => SendFileMessage(MessageType.OpenFileRequest), true);
         m_SaveFileCmd = new RelayCommand(() => SendFileMessage(MessageType.SaveFileRequest), true);
         m_SaveFileAsCmd = new RelayCommand(() => SendFileMessage(MessageType.SaveFileAsRequest), true);
         m_DisassembleFileCmd = new RelayCommand(() => SendFileMessage(MessageType.DisassembleFileRequest), true);
         m_AssembleFileCmd = new RelayCommand(() => SendFileMessage(MessageType.AssembleFileRequest), true);
         m_CloseWindowCmd = new RelayCommand(() => SendFileMessage(MessageType.WindowClosingNotification), true);

         m_ShowPreferencesCmd = new RelayCommand(() => ShowPreferences(), true);
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

      public ICommand CreateNewFileCommand
      {
         get { return m_NewFileCmd; }
      }

      public ICommand OpenFileCommand
      {
         get { return m_OpenFileCmd; }
      }

      public ICommand SaveFileCommand
      {
         get { return m_SaveFileCmd; }
      }

      public ICommand SaveFileAsCommand
      {
         get { return m_SaveFileAsCmd; }
      }

      public ICommand ShowPreferencesCommand
      {
         get { return m_ShowPreferencesCmd; }
      }

      public ICommand AssembleFileCommand
      {
         get { return m_AssembleFileCmd; }
      }

      public ICommand DisassembleCommand
      {
         get { return m_DisassembleFileCmd; }
      }

      public ICommand CloseWindowCommand
      {
         get { return m_CloseWindowCmd; }
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
               OnPropertyChanged(nameof(ActiveView));
            }
         }
      }

      private void ShowPreferences()
      {
         var prefsWindow = new PreferencesWindow(m_Preferences);
         DialogResult dr = prefsWindow.ShowDialog();
         switch (dr)
         {
            case DialogResult.OK:
            {
               var fileSaver = new SettingsFileSaver();
               m_Preferences.CloneValues(prefsWindow.ActivePreferences);
               m_Preferences.SaveSettings(PREFS_FILENAME, fileSaver);
               break;
            }
         }
      }

      private void SendFileMessage(MessageType action)
      {
         BroadcastMessage(new BasicMessage(action));
      }

      private int m_ActiveIdx;
      private readonly PreferencesViewModel m_Preferences;
      private readonly ObservableCollection<ViewBase> m_Views;
      private readonly RelayCommand<int> m_ChangeActiveIdxCmd;

      private readonly RelayCommand m_NewFileCmd;
      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_SaveFileAsCmd;
      private readonly RelayCommand m_DisassembleFileCmd;
      private readonly RelayCommand m_AssembleFileCmd;
      private readonly RelayCommand m_CloseWindowCmd;

      private readonly RelayCommand m_ShowPreferencesCmd;

      private const string PREFS_FILENAME = "prefs.ini";
      
   }
}
