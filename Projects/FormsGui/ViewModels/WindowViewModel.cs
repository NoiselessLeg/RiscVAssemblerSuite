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

         m_Views.Add(new AssemblyEditorView(viewIdCtr++, mgr, m_Preferences));
         m_Views.Add(new HexExplorerView(viewIdCtr++, mgr));
         m_Views.Add(new DebugView(viewIdCtr++, mgr));

         m_ChangeActiveIdxCmd = new RelayCommand(
            (param) =>
            {
               int? idx = param as int?;
               System.Diagnostics.Debug.Assert(idx.HasValue);
               ActiveViewIndex = idx.Value;
            }, true
         );

         m_ShowPreferencesCmd = new RelayCommand(
            (param) =>
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
            }, true
         );
         
         SubscribeToMessageType(MessageType.ActiveViewRequest, ChangeActiveViewCommand);
         SubscribeToMessageType(MessageType.ShowOptionsRequest, m_ShowPreferencesCmd);
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
               OnPropertyChanged(nameof(ActiveView));
            }
         }
      }

      private int m_ActiveIdx;
      private readonly PreferencesViewModel m_Preferences;
      private readonly ObservableCollection<ViewBase> m_Views;
      private readonly RelayCommand m_ChangeActiveIdxCmd;

      private readonly RelayCommand m_ShowPreferencesCmd;

      private const string PREFS_FILENAME = "prefs.ini";
      
   }
}
