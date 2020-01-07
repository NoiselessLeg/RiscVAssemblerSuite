using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Services
{
   class Win32DialogService : IDialogService
   {

      public bool ShowOpenFileDialog(DialogOptions options, out string fileName)
      {
         bool openSuccess = false;
         using (var ofd = new OpenFileDialog())
         {
            ofd.Filter = options.FileFilter;
            ofd.FileName = options.DefaultFileName;
            ofd.AddExtension = true;
            ofd.Title = options.WindowTitle;
            DialogResult retVal = ofd.ShowDialog();
            if (retVal == DialogResult.OK)
            {
               openSuccess = true;
               fileName = ofd.FileName;
            }
            else
            {
               fileName = string.Empty;
            }
         }

         return openSuccess;
      }

      public bool ShowSaveFileDialog(DialogOptions options, out string fileName)
      {
         bool dialogSuccess = false;
         
         using (var sfd = new SaveFileDialog())
         {
            sfd.Filter = options.FileFilter;
            sfd.FileName = options.DefaultFileName;
            sfd.Title = options.WindowTitle;
            sfd.AddExtension = true;
            DialogResult retVal = sfd.ShowDialog();
            if (retVal == DialogResult.OK)
            {
               dialogSuccess = true;
               fileName = sfd.FileName;
            }
            else
            {
               fileName = string.Empty;
            }
         }

         return dialogSuccess;
      }

      public void ShowErrorDialog(string caption, string error)
      {
         MessageBox.Show(error, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
   }
}
