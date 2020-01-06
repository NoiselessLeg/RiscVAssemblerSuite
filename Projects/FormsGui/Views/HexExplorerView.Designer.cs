namespace Assembler.FormsGui.Views
{
   partial class HexExplorerView
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.m_FileTabCtrl = new System.Windows.Forms.TabControl();
         this.m_NoFilesAssembledLbl = new System.Windows.Forms.Label();
         this.m_TabRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.m_CloseCurrTabItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllToRightItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllToLeftItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_TabRightClickMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_FileTabCtrl
         // 
         this.m_FileTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_FileTabCtrl.Location = new System.Drawing.Point(0, 0);
         this.m_FileTabCtrl.Name = "m_FileTabCtrl";
         this.m_FileTabCtrl.SelectedIndex = 0;
         this.m_FileTabCtrl.Size = new System.Drawing.Size(441, 320);
         this.m_FileTabCtrl.TabIndex = 0;
         this.m_FileTabCtrl.Visible = false;
         this.m_FileTabCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TabControl_OnMouseUp);
         // 
         // m_NoFilesAssembledLbl
         // 
         this.m_NoFilesAssembledLbl.AutoSize = true;
         this.m_NoFilesAssembledLbl.Location = new System.Drawing.Point(0, 0);
         this.m_NoFilesAssembledLbl.Name = "m_NoFilesAssembledLbl";
         this.m_NoFilesAssembledLbl.Size = new System.Drawing.Size(148, 13);
         this.m_NoFilesAssembledLbl.TabIndex = 1;
         this.m_NoFilesAssembledLbl.Text = "Assemble a file to see content";
         // 
         // m_TabRightClickMenu
         // 
         this.m_TabRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_CloseCurrTabItem,
            this.m_CloseAllToRightItem,
            this.m_CloseAllToLeftItem,
            this.m_CloseAllItem});
         this.m_TabRightClickMenu.Name = "m_TabRightClickMenu";
         this.m_TabRightClickMenu.Size = new System.Drawing.Size(186, 92);
         // 
         // m_CloseCurrTabItem
         // 
         this.m_CloseCurrTabItem.Name = "m_CloseCurrTabItem";
         this.m_CloseCurrTabItem.Size = new System.Drawing.Size(185, 22);
         this.m_CloseCurrTabItem.Text = "Close tab";
         this.m_CloseCurrTabItem.Click += new System.EventHandler(this.OnCloseTabClicked);
         // 
         // m_CloseAllToRightItem
         // 
         this.m_CloseAllToRightItem.Name = "m_CloseAllToRightItem";
         this.m_CloseAllToRightItem.Size = new System.Drawing.Size(185, 22);
         this.m_CloseAllToRightItem.Text = "Close all tabs to right";
         this.m_CloseAllToRightItem.Click += new System.EventHandler(this.OnCloseAllTabsToRightClicked);
         // 
         // m_CloseAllToLeftItem
         // 
         this.m_CloseAllToLeftItem.Name = "m_CloseAllToLeftItem";
         this.m_CloseAllToLeftItem.Size = new System.Drawing.Size(185, 22);
         this.m_CloseAllToLeftItem.Text = "Close all tabs to left";
         this.m_CloseAllToLeftItem.Click += new System.EventHandler(this.OnCloseAllTabsToLeftClicked);
         // 
         // m_CloseAllItem
         // 
         this.m_CloseAllItem.Name = "m_CloseAllItem";
         this.m_CloseAllItem.Size = new System.Drawing.Size(185, 22);
         this.m_CloseAllItem.Text = "Close all tabs";
         this.m_CloseAllItem.Click += new System.EventHandler(this.OnCloseAllTabsClicked);
         // 
         // HexExplorerView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_NoFilesAssembledLbl);
         this.Controls.Add(this.m_FileTabCtrl);
         this.Name = "HexExplorerView";
         this.Size = new System.Drawing.Size(441, 320);
         this.m_TabRightClickMenu.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TabControl m_FileTabCtrl;
      private System.Windows.Forms.Label m_NoFilesAssembledLbl;
      private System.Windows.Forms.ContextMenuStrip m_TabRightClickMenu;
      private System.Windows.Forms.ToolStripMenuItem m_CloseCurrTabItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllToRightItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllToLeftItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllItem;
   }
}
