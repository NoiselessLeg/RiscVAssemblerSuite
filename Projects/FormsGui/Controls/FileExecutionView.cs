using Assembler.FormsGui.ViewModels;
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
   public partial class FileExecutionView : UserControl
   {
      public FileExecutionView()
      {
         // default constructor should never be called. only used for designer
         System.Diagnostics.Debug.Assert(false);
         InitializeComponent();
      }

      public FileExecutionView(ExecutionViewModel evm)
      {
         m_ExViewModel = evm;
         InitializeComponent();


      }

      private readonly ExecutionViewModel m_ExViewModel;
   }
}
