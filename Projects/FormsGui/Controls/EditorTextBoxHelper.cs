using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Controls
{
   public static class EditorTextBoxHelper
   {
      public static int GetFirstNonWhitespaceColumn(this IEnumerable<TextWord> line)
      {
         int retVal = -1;
         IEnumerator<TextWord> lineEnum = line.GetEnumerator();
         lineEnum.Reset();
         bool foundChar = false;
         int colItr = 0;
         while (lineEnum.MoveNext() &&
                !foundChar)
         {
            if (!lineEnum.Current.IsWhiteSpace)
            {
               foundChar = true;
               retVal = colItr;
            }

            ++colItr;
         }

         return retVal;
      }
   }
}
