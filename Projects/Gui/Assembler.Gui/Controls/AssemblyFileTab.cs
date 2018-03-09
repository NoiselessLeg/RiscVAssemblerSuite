using Assembler.Gui.Model;
using System.Windows.Controls;

namespace Assembler.Gui.Controls
{
    class AssemblyFileTab : TabItem
    {
        public AssemblyFileTab(AssemblyFile file)
        {
            Header = file.FileName;
            Content = file.FileText;
        }
    }
}
