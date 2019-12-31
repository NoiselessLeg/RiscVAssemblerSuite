using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls.Custom
{


   public abstract class BaseMenuBarElement
   {
      public abstract ToolStripItem ToMenuItem();
   }

   public class MenuBarActionElement : BaseMenuBarElement
   {
      public MenuBarActionElement(string txt, ICommand executeCmd)
      {
         m_Text = txt;
         m_Cmd = executeCmd;
         m_ShortcutKeys = Keys.None;
      }

      public MenuBarActionElement(string txt, ICommand executeCmd, object cmdParam) 
      {
         m_Text = txt;
         m_Cmd = executeCmd;
         m_CmdParam = cmdParam;
         m_ShortcutKeys = Keys.None;
      }

      public MenuBarActionElement(string txt, ICommand executeCmd, Keys shortcutKeys)
      {
         m_Text = txt;
         m_Cmd = executeCmd;
         m_ShortcutKeys = shortcutKeys;
      }

      public MenuBarActionElement(string txt, ICommand executeCmd, object cmdParam, Keys shortcutKeys)
      {
         m_Text = txt;
         m_Cmd = executeCmd;
         m_CmdParam = cmdParam;
         m_ShortcutKeys = shortcutKeys;
      }

      public MenuBarActionElement(string txt, ICommand executeCmd, object cmdParam, object predParam)
      {
         m_Text = txt;
         m_Cmd = executeCmd;
         m_CmdParam = cmdParam;
         m_PredParams = predParam;
         m_ShortcutKeys = Keys.None;
      }

      public MenuBarActionElement(string txt, ICommand executeCmd, object cmdParam, object predParam, Keys shortcutKeys)
      {
         m_Text = txt;
         m_Cmd = executeCmd;
         m_CmdParam = cmdParam;
         m_PredParams = predParam;
         m_ShortcutKeys = shortcutKeys;
      }

      public override ToolStripItem ToMenuItem()
      {
         var menuItem = new ToolStripMenuItem();

         menuItem.Text = Text;
         menuItem.Click += OnMenuItemClick;
         menuItem.Paint += OnMenuItemPaint;
         if (m_ShortcutKeys != Keys.None)
         {
            menuItem.ShortcutKeys = m_ShortcutKeys;
            menuItem.ShowShortcutKeys = true;
         }

         return menuItem;
      }

      public string Text
      {
         get { return m_Text; }
      }

      private void OnMenuItemClick(object sender, EventArgs e)
      {
         m_Cmd.Execute(m_CmdParam);
      }

      private void OnMenuItemPaint(object sender, PaintEventArgs e)
      {
         var menuItem = sender as ToolStripMenuItem;
         System.Diagnostics.Debug.Assert(menuItem != null);
         menuItem.Enabled = m_Cmd.CanExecute(m_PredParams);
      }

      private readonly string m_Text;
      private readonly ICommand m_Cmd;
      private readonly object m_CmdParam;
      private readonly object m_PredParams;
      private readonly Keys m_ShortcutKeys;

   }

   public class CompositeMenuBarElement : BaseMenuBarElement
   {
      public CompositeMenuBarElement(string txt, IEnumerable<BaseMenuBarElement> subMenus)
      {
         m_Text = txt;
         m_SubMenus = subMenus;
      }

      public override ToolStripItem ToMenuItem()
      {
         var menuItem = new ToolStripMenuItem();
         menuItem.Text = Text;

         foreach (var subMenu in m_SubMenus)
         {
            menuItem.DropDownItems.Add(subMenu.ToMenuItem());
         }

         return menuItem;
      }
      
      public string Text
      {
         get { return m_Text; }
      }

      private readonly string m_Text;
      private IEnumerable<BaseMenuBarElement> m_SubMenus;
   }

   public class SeparatorMenuBarElement : BaseMenuBarElement
   {
      public override ToolStripItem ToMenuItem()
      {
         return new ToolStripSeparator();
      }
   }

   public class MenuBarContext
   {
      public MenuBarContext()
      {
         m_Menus = new List<BaseMenuBarElement>();
      }

      public IEnumerable<ToolStripItem> AsToolStripItems()
      {
         var itemList = new List<ToolStripItem>();
         foreach (var element in m_Menus)
         {
            itemList.Add(element.ToMenuItem());
         }

         return itemList;
      }

      public void AddMenuBarElement(BaseMenuBarElement elem)
      {
         m_Menus.Add(elem);
      }

      private readonly List<BaseMenuBarElement> m_Menus;

   }
}
