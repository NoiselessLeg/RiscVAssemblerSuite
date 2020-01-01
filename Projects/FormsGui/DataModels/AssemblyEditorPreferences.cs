using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.DataModels
{
   public class AssemblyEditorPreferences
   {
      public bool ReplaceTabsWithSpaces { get; set; }
      public int NumSpacesToReplaceTabWith { get; set; }
      public bool ShowLineNumbers { get; set; }

   }
}
