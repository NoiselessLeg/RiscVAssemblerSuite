using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class PreferencesViewModel : BaseViewModel
   {
      public PreferencesViewModel()
      {
         m_EditorSettings = new AssemblyEditorPreferences();
      }

      public void CloneValues(PreferencesViewModel other)
      {
         ReplaceTabsWithSpaces = other.ReplaceTabsWithSpaces;
         NumSpacesToReplaceTabWith = other.NumSpacesToReplaceTabWith;
         ShowLineNumbers = other.ShowLineNumbers;
      }

      public void LoadFromStorage(SettingsFileLoader loader)
      {
         m_EditorSettings.ShowLineNumbers = loader.GetParameter<bool>(nameof(ShowLineNumbers));
         m_EditorSettings.ReplaceTabsWithSpaces = loader.GetParameter<bool>(nameof(ReplaceTabsWithSpaces));
         m_EditorSettings.NumSpacesToReplaceTabWith = loader.GetParameter<int>(nameof(NumSpacesToReplaceTabWith));
      }

      public void SaveSettings(string fileName, SettingsFileSaver fileSaver)
      {
         m_EditorSettings.SaveSettings(fileSaver);
         fileSaver.CommitToExternalStorage(fileName);
      }

      public bool ReplaceTabsWithSpaces
      {
         get { return m_EditorSettings.ReplaceTabsWithSpaces; }
         set
         {
            if (m_EditorSettings.ReplaceTabsWithSpaces != value)
            {
               m_EditorSettings.ReplaceTabsWithSpaces = value;
               OnPropertyChanged();
            }
         }
      }

      public int NumSpacesToReplaceTabWith
      {
         get { return m_EditorSettings.NumSpacesToReplaceTabWith; }
         set
         {
            if (m_EditorSettings.NumSpacesToReplaceTabWith != value)
            {
               m_EditorSettings.NumSpacesToReplaceTabWith = value;
               OnPropertyChanged();
            }
         }
      }

      public bool ShowLineNumbers
      {
         get { return m_EditorSettings.ShowLineNumbers; }
         set
         {
            if (m_EditorSettings.ShowLineNumbers != value)
            {
               m_EditorSettings.ShowLineNumbers = value;
               OnPropertyChanged();
            }
         }
      }


      private readonly AssemblyEditorPreferences m_EditorSettings;
   }
}
