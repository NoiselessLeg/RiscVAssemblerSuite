namespace Assembler.FormsGui.Views
{
   partial class AssemblyEditorView
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
         this.m_EditorLoggerSplitContainer = new System.Windows.Forms.SplitContainer();
         this.m_OpenFileTabs = new System.Windows.Forms.TabControl();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
         this.m_NumericValue = new Assembler.FormsGui.Controls.BindableToolStripStatusLabel();
         this.m_LogTxt = new System.Windows.Forms.TextBox();
         this.m_TabRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.m_CloseCurrTabItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllToRightItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllToLeftItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_CloseAllItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         ((System.ComponentModel.ISupportInitialize)(this.m_EditorLoggerSplitContainer)).BeginInit();
         this.m_EditorLoggerSplitContainer.Panel1.SuspendLayout();
         this.m_EditorLoggerSplitContainer.Panel2.SuspendLayout();
         this.m_EditorLoggerSplitContainer.SuspendLayout();
         this.statusStrip1.SuspendLayout();
         this.m_TabRightClickMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_EditorLoggerSplitContainer
         // 
         this.m_EditorLoggerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_EditorLoggerSplitContainer.Location = new System.Drawing.Point(0, 0);
         this.m_EditorLoggerSplitContainer.Name = "m_EditorLoggerSplitContainer";
         this.m_EditorLoggerSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // m_EditorLoggerSplitContainer.Panel1
         // 
         this.m_EditorLoggerSplitContainer.Panel1.Controls.Add(this.m_OpenFileTabs);
         // 
         // m_EditorLoggerSplitContainer.Panel2
         // 
         this.m_EditorLoggerSplitContainer.Panel2.Controls.Add(this.statusStrip1);
         this.m_EditorLoggerSplitContainer.Panel2.Controls.Add(this.m_LogTxt);
         this.m_EditorLoggerSplitContainer.Size = new System.Drawing.Size(446, 376);
         this.m_EditorLoggerSplitContainer.SplitterDistance = 272;
         this.m_EditorLoggerSplitContainer.TabIndex = 1;
         // 
         // m_OpenFileTabs
         // 
         this.m_OpenFileTabs.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_OpenFileTabs.Location = new System.Drawing.Point(0, 0);
         this.m_OpenFileTabs.Name = "m_OpenFileTabs";
         this.m_OpenFileTabs.SelectedIndex = 0;
         this.m_OpenFileTabs.Size = new System.Drawing.Size(446, 272);
         this.m_OpenFileTabs.TabIndex = 2;
         this.m_OpenFileTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl_OnCurrentTabChanged);
         this.m_OpenFileTabs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TabControl_OnMouseUp);
         // 
         // statusStrip1
         // 
         this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.m_NumericValue});
         this.statusStrip1.Location = new System.Drawing.Point(0, 78);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(446, 22);
         this.statusStrip1.TabIndex = 2;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // toolStripStatusLabel1
         // 
         this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
         this.toolStripStatusLabel1.Size = new System.Drawing.Size(95, 17);
         this.toolStripStatusLabel1.Text = "Active tab index:";
         // 
         // m_NumericValue
         // 
         this.m_NumericValue.Name = "m_NumericValue";
         this.m_NumericValue.Size = new System.Drawing.Size(13, 17);
         this.m_NumericValue.Text = "0";
         // 
         // m_LogTxt
         // 
         this.m_LogTxt.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_LogTxt.Location = new System.Drawing.Point(0, 0);
         this.m_LogTxt.Multiline = true;
         this.m_LogTxt.Name = "m_LogTxt";
         this.m_LogTxt.ReadOnly = true;
         this.m_LogTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.m_LogTxt.Size = new System.Drawing.Size(446, 100);
         this.m_LogTxt.TabIndex = 1;
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
         // AssemblyEditorView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_EditorLoggerSplitContainer);
         this.Name = "AssemblyEditorView";
         this.Size = new System.Drawing.Size(446, 376);
         this.m_EditorLoggerSplitContainer.Panel1.ResumeLayout(false);
         this.m_EditorLoggerSplitContainer.Panel2.ResumeLayout(false);
         this.m_EditorLoggerSplitContainer.Panel2.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_EditorLoggerSplitContainer)).EndInit();
         this.m_EditorLoggerSplitContainer.ResumeLayout(false);
         this.statusStrip1.ResumeLayout(false);
         this.statusStrip1.PerformLayout();
         this.m_TabRightClickMenu.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.SplitContainer m_EditorLoggerSplitContainer;
      private System.Windows.Forms.TextBox m_LogTxt;
      private System.Windows.Forms.TabControl m_OpenFileTabs;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
      private Controls.BindableToolStripStatusLabel m_NumericValue;
      private System.Windows.Forms.ContextMenuStrip m_TabRightClickMenu;
      private System.Windows.Forms.ToolStripMenuItem m_CloseCurrTabItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllToRightItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllToLeftItem;
      private System.Windows.Forms.ToolStripMenuItem m_CloseAllItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
   }
}
