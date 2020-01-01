using Assembler.FormsGui.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class EditorPreferencesViewModel : BaseViewModel
   {
      public EditorPreferencesViewModel()
      {
         m_Model = new AssemblyEditorPreferences();
      }

      public bool ReplaceTabsWithSpaces
      {
         get { return m_Model.ReplaceTabsWithSpaces; }
         set
         {
            if (m_Model.ReplaceTabsWithSpaces != value)
            {
               m_Model.ReplaceTabsWithSpaces = value;
               OnPropertyChanged();
            }
         }
      }

      public int NumSpacesToReplaceTabWith
      {
         get { return m_Model.NumSpacesToReplaceTabWith; }
         set
         {
            if (m_Model.NumSpacesToReplaceTabWith != value)
            {
               m_Model.NumSpacesToReplaceTabWith = value;
               OnPropertyChanged();
            }
         }
      }

      public bool ShowLineNumbers
      {
         get { return m_Model.ShowLineNumbers; }
         set
         {
            if (m_Model.ShowLineNumbers != value)
            {
               m_Model.ShowLineNumbers = value;
               OnPropertyChanged();
            }
         }
      }


      private readonly AssemblyEditorPreferences m_Model;
   }
}
