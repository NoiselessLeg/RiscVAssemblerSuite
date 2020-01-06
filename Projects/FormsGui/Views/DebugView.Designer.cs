namespace Assembler.FormsGui.Views
{
   partial class DebugView
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
         this.m_OpenFilesTabCtrl = new System.Windows.Forms.TabControl();
         this.m_NoFileAssembledLbl = new System.Windows.Forms.Label();
         this.m_TabRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.m_CloseCurrTabItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllToRightItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllToLeftItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.m_TabRightClickMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_OpenFilesTabCtrl
         // 
         this.m_OpenFilesTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_OpenFilesTabCtrl.Location = new System.Drawing.Point(0, 0);
         this.m_OpenFilesTabCtrl.Name = "m_OpenFilesTabCtrl";
         this.m_OpenFilesTabCtrl.SelectedIndex = 0;
         this.m_OpenFilesTabCtrl.Size = new System.Drawing.Size(469, 388);
         this.m_OpenFilesTabCtrl.TabIndex = 0;
         this.m_OpenFilesTabCtrl.Visible = false;
         this.m_OpenFilesTabCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TabControl_OnMouseUp);
         // 
         // m_NoFileAssembledLbl
         // 
         this.m_NoFileAssembledLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_NoFileAssembledLbl.AutoSize = true;
         this.m_NoFileAssembledLbl.ForeColor = System.Drawing.SystemColors.ControlText;
         this.m_NoFileAssembledLbl.Location = new System.Drawing.Point(0, 0);
         this.m_NoFileAssembledLbl.Name = "m_NoFileAssembledLbl";
         this.m_NoFileAssembledLbl.Size = new System.Drawing.Size(148, 13);
         this.m_NoFileAssembledLbl.TabIndex = 1;
         this.m_NoFileAssembledLbl.Text = "Assemble a file to see content";
         // 
         // m_TabRightClickMenu
         // 
         this.m_TabRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_CloseCurrTabItem,
            this.m_CloseAllToRightItem,
            this.m_CloseAllToLeftItem,
            this.toolStripSeparator1,
            this.m_CloseAllItem});
         this.m_TabRightClickMenu.Name = "m_TabRightClickMenu";
         this.m_TabRightClickMenu.Size = new System.Drawing.Size(186, 98);
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
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
         // 
         // DebugView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_NoFileAssembledLbl);
         this.Controls.Add(this.m_OpenFilesTabCtrl);
         this.Name = "DebugView";
         this.Size = new System.Drawing.Size(469, 388);
         this.m_TabRightClickMenu.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TabControl m_OpenFilesTabCtrl;
      private System.Windows.Forms.Label m_NoFileAssembledLbl;
      private System.Windows.Forms.ContextMenuStrip m_TabRightClickMenu;
      private System.Windows.Forms.ToolStripMenuItem m_CloseCurrTabItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllToRightItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllToLeftItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
   }
}
