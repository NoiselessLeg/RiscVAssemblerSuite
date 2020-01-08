using Assembler.FormsGui.ViewModels;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assembler.FormsGui.Services;

namespace Assembler.FormsGui.Controls
{
   public partial class AssemblyTextBox : UserControl
   {
      public AssemblyTextBox()
      {
         InitializeComponent();
      }

      public AssemblyTextBox(AssemblyFileViewModel avm,
                             PreferencesViewModel preferences) :
         this()
      {
         preferencesViewModelBindingSource.DataSource = preferences;
         assemblyFileViewModelBindingSource.DataSource = avm;

         if (!s_TriedToOpenSyntaxFile)
         {
            try
            {
               // this expects a directory for a path. not the actual file?
               string syntaxFilePath = GetSyntaxPathName();
               string syntaxFileDir = Path.GetDirectoryName(syntaxFilePath);

               var fsm = new FileSyntaxModeProvider(syntaxFileDir);
               HighlightingManager.Manager.AddSyntaxModeFileProvider(fsm);
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error: " + ex.Message);
               Console.WriteLine("Stack trace:\n" + ex.StackTrace);
               MessageBox.Show("Failed to load syntax file for highlighting. Keyword highlighting will be disabled!",
                  "File Editor Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               s_TriedToOpenSyntaxFile = true;
            }
         }

         m_FileTxtBox.SetHighlighting("Assembly");
      }

      private static string GetSyntaxPathName()
      {
         byte[] syntaxDefPath = Properties.Resources.Assembly;
         return StreamService.WriteToTemporaryFile(syntaxDefPath);
      }

      private static bool s_TriedToOpenSyntaxFile;
   }
}
