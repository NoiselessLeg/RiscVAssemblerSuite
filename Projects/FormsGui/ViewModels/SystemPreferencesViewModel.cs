using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class SystemPreferencesViewModel : BaseViewModel
   {
      public SystemPreferencesViewModel()
      {
         m_AsmEditorPrefs = new EditorPreferencesViewModel();
      }

      public EditorPreferencesViewModel AssemblyEditorPreferences
      {
         get { return m_AsmEditorPrefs; }
      }

      private readonly EditorPreferencesViewModel m_AsmEditorPrefs;
   }
}
