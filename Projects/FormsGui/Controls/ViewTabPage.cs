using Assembler.FormsGui.Common;
using Assembler.FormsGui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public class ViewTabPage<TViewControl>: TabPage
      where TViewControl : Control, IFileBindable, new()
   {
      public ViewTabPage(TViewControl ctrl) :
         base(ctrl.FileName)
      {
         m_PrimaryCtrl = ctrl;
         m_PrimaryCtrl.Dock = DockStyle.Fill;
         Controls.Add(m_PrimaryCtrl);
         DataBindings.Add(new Binding(nameof(Text), m_PrimaryCtrl, nameof(m_PrimaryCtrl.FileName), true, DataSourceUpdateMode.OnPropertyChanged));
      }

      public TViewControl PrimaryControl
      {
         get { return m_PrimaryCtrl; }
      }

      private readonly TViewControl m_PrimaryCtrl;
   }
}
