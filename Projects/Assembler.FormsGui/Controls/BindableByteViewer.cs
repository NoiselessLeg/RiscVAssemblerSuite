using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Controls
{
   public class BindableByteViewer : ByteViewer
   {
      public DisplayMode DisplayMode
      {
         get { return GetDisplayMode(); }
         set { SetDisplayMode(value); }
      }

      public string File
      {
         get { return m_File; }
         set
         {
            SetFile(value);
            m_File = value;
         }
      }

      public byte[] Bytes
      {
         get { return GetBytes(); }
         set { SetBytes(value); }
      }

      private string m_File;
   }
}
