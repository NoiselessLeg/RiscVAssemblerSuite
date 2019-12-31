using Assembler.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public partial class ConsoleTextBox : UserControl, ITerminal
   {
      public ConsoleTextBox()
      {
         InitializeComponent();
      }

      public void PrintChar(char c)
      {
      }

      public void PrintInt(int value)
      {
         throw new NotImplementedException();
      }

      public void PrintString(string value)
      {
         throw new NotImplementedException();
      }

      public char ReadChar()
      {
         throw new NotImplementedException();
      }

      public int ReadInt()
      {
         throw new NotImplementedException();
      }

      public string ReadString()
      {
         throw new NotImplementedException();
      }
   }
}
