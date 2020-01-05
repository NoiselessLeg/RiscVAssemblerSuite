using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   class BindableToolStripStatusLabel : ToolStripStatusLabel, IBindableComponent
   {
      public BindableToolStripStatusLabel():
         base()
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }

      [Browsable(false)]
      public BindingContext BindingContext
      {
         get { return m_Ctx; }
         set { m_Ctx = value; }
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public ControlBindingsCollection DataBindings
      {
         get { return m_DataBindings; }
      }

      private readonly ControlBindingsCollection m_DataBindings;
      private BindingContext m_Ctx;
   }
}
