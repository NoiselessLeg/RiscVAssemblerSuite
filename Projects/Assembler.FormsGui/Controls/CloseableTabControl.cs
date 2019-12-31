using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{

   public class CloseableTabControl : TabControl
   {
      public CloseableTabControl():
         base()
      {
         InitializeComponent();
      }

      public Image CloseIcon
      {
         get { return m_Icon; }
         set
         {
            // if we had an icon before, reduce the tab page width
            // 
            if (m_Icon != null)
            {
               foreach (TabPage page in TabPages)
               {
                  page.Size = new Size(page.Size.Width - m_Icon.Width, page.Size.Height);
               }
            }

            m_Icon = value;
            if (m_Icon != null)
            {
               foreach (TabPage page in TabPages)
               {
                  page.Size = new Size(page.Size.Width + m_Icon.Width, page.Size.Height);
               }
            }
         }
      }

      protected override void OnControlAdded(ControlEventArgs e)
      {
         ControlEventArgs newArgs = e;
         if (e.Control.GetType() == typeof(TabPage))
         {
            var newTabPage = new PaddingTabPage(e.Control as TabPage);
            newArgs = new ControlEventArgs(newTabPage);
         }
         base.OnControlAdded(newArgs);
      }

      protected override void OnDrawItem(DrawItemEventArgs e)
      {
         base.OnDrawItem(e);
         TabPage page = TabPages[e.Index];
         Rectangle rect = GetTabRect(e.Index);
         rect.Inflate(-2, -2);
         e.Graphics.DrawImage(m_Icon, rect.Right - m_Icon.Width, rect.Top + ((rect.Height - m_Icon.Height) / 2));
         e.Graphics.DrawString(page.Text, page.Font, Brushes.Black, rect);
      }
      
      private void InitializeComponent()
      {
         this.SuspendLayout();
         // 
         // CloseableTabControl
         // 
         this.DrawMode = TabDrawMode.OwnerDrawFixed;
         this.ResumeLayout(false);
      }

      private Image m_Icon;

      private class PaddingTabPage : TabPage
      {
         public PaddingTabPage(TabPage other)
         {
            Text = other.Text;
         }

         public override string Text
         {
            get
            {
               return base.Text;
            }
            set
            {
               base.Text = value + "   ";
            }
         }
      }
   }
}
