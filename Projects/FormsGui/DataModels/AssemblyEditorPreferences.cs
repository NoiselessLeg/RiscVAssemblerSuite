using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.DataModels
{
   public class AssemblyEditorPreferences
   {
      public AssemblyEditorPreferences()
      {
         // needs to be non-zero.
         NumSpacesToReplaceTabWith = 1;
      }
      
      public void SaveSettings(IO.SettingsFileSaver fileSaver)
      {
         fileSaver.AddParameter("EditorPrefs", nameof(ReplaceTabsWithSpaces), ReplaceTabsWithSpaces);
         fileSaver.AddParameter("EditorPrefs", nameof(NumSpacesToReplaceTabWith), NumSpacesToReplaceTabWith);
         fileSaver.AddParameter("EditorPrefs", nameof(ShowLineNumbers), ShowLineNumbers);
      }

      public bool ReplaceTabsWithSpaces { get; set; }
      public int NumSpacesToReplaceTabWith { get; set; }
      public bool ShowLineNumbers { get; set; }

   }
}
