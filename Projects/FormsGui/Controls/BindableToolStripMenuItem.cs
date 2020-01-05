using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public class BindableToolStripMenuItem : ToolStripMenuItem, IBindableComponent
   {
      public BindableToolStripMenuItem() :
         base()
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }

      public BindableToolStripMenuItem(string text) :
         base(text)
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }

      public BindableToolStripMenuItem(System.Drawing.Image image) :
         base(image)
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }

      public BindableToolStripMenuItem(string text, System.Drawing.Image image) :
         base(text, image)
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }

      public BindableToolStripMenuItem(string text, System.Drawing.Image image, EventHandler onClick) :
         base(text, image, onClick)
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }

      public BindableToolStripMenuItem(string text, System.Drawing.Image image, params ToolStripItem[] dropDownItems) :
         base(text, image, dropDownItems)
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }
      
      public BindableToolStripMenuItem(string text, System.Drawing.Image image, EventHandler onClick, Keys shortcutKeys) :
         base(text, image, onClick, shortcutKeys)
      {
         m_Ctx = new BindingContext();
         m_DataBindings = new ControlBindingsCollection(this);
      }

      public BindableToolStripMenuItem(string text, System.Drawing.Image image, EventHandler onClick, string name) :
         base(text, image, onClick, name)
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
