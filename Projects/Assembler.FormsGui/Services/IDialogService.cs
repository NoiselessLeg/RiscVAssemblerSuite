using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Services
{
   public interface IDialogService
   {
      bool ShowOpenFileDialog(DialogOptions options, out string fileName);
      bool ShowSaveFileDialog(DialogOptions options, out string fileName);
      void ShowErrorDialog(string caption, string error);
   }
}
