using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public class BufferedDataGridView : DataGridView
   {
      public BufferedDataGridView()
      {
         base.DoubleBuffered = true;
      }
   }
}
