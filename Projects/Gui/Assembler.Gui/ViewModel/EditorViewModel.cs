using Assembler.Gui.Controls;
using Assembler.Gui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Gui.ViewModel
{
    class EditorViewModel : ObservableObject
    {
        public EditorViewModel()
        {
            m_Project = new AssemblyProject();
        }

        public IEnumerable<AssemblyFileTab> FileTabs
        {
            get
            {
                foreach (AssemblyFile file in m_Project.CurrentProjectFiles)
                {
                    yield return new AssemblyFileTab(file);
                }
            }
        }

        private AssemblyProject m_Project;
    }
}
