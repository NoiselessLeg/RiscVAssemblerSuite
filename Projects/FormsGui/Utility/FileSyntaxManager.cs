using Assembler.FormsGui.Services;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Utility
{
   public static class FileSyntaxManager
   {
      public static void Init()
      {
         try
         {
            byte[] syntaxDefPath = Properties.Resources.Assembly;
            s_SyntaxFilePath = StreamService.WriteToTemporaryFile(syntaxDefPath);

            // this expects a directory for a path. not the actual file?
            string syntaxFileDir = Path.GetDirectoryName(s_SyntaxFilePath);

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
      }

      public static void Dispose()
      {
         if (File.Exists(s_SyntaxFilePath))
         {
            File.Delete(s_SyntaxFilePath);
         }
      }

      private static string s_SyntaxFilePath;
   }
}
