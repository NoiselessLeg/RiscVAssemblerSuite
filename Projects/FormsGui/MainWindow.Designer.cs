namespace Assembler.FormsGui
{
   partial class MainWindow
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
         this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.newAssemblerFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openAssemblerFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveAssemblerFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveAssemblerFileAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.fromCompiledFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.assemblerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.assembleFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
         this.m_DisplayPanel = new Assembler.FormsGui.DisplayPanel();
         this.m_MenuStrip.SuspendLayout();
         this.m_LayoutPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_MenuStrip
         // 
         this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.assemblerToolStripMenuItem,
            this.helpToolStripMenuItem});
         this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
         this.m_MenuStrip.Name = "m_MenuStrip";
         this.m_MenuStrip.Size = new System.Drawing.Size(784, 24);
         this.m_MenuStrip.TabIndex = 0;
         this.m_MenuStrip.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAssemblerFileToolStripMenuItem,
            this.openAssemblerFileToolStripMenuItem,
            this.saveAssemblerFileToolStripMenuItem,
            this.saveAssemblerFileAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // newAssemblerFileToolStripMenuItem
         // 
         this.newAssemblerFileToolStripMenuItem.Name = "newAssemblerFileToolStripMenuItem";
         this.newAssemblerFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
         this.newAssemblerFileToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
         this.newAssemblerFileToolStripMenuItem.Text = "New Assembler File";
         this.newAssemblerFileToolStripMenuItem.Click += new System.EventHandler(this.newAssemblerFileToolStripMenuItem_Click);
         // 
         // openAssemblerFileToolStripMenuItem
         // 
         this.openAssemblerFileToolStripMenuItem.Name = "openAssemblerFileToolStripMenuItem";
         this.openAssemblerFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openAssemblerFileToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
         this.openAssemblerFileToolStripMenuItem.Text = "Open Assembler File";
         this.openAssemblerFileToolStripMenuItem.Click += new System.EventHandler(this.openAssemblerFileToolStripMenuItem_Click);
         // 
         // saveAssemblerFileToolStripMenuItem
         // 
         this.saveAssemblerFileToolStripMenuItem.Name = "saveAssemblerFileToolStripMenuItem";
         this.saveAssemblerFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
         this.saveAssemblerFileToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
         this.saveAssemblerFileToolStripMenuItem.Text = "Save Assembler File";
         this.saveAssemblerFileToolStripMenuItem.Click += new System.EventHandler(this.saveAssemblerFileToolStripMenuItem_Click);
         // 
         // saveAssemblerFileAsToolStripMenuItem
         // 
         this.saveAssemblerFileAsToolStripMenuItem.Name = "saveAssemblerFileAsToolStripMenuItem";
         this.saveAssemblerFileAsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
         this.saveAssemblerFileAsToolStripMenuItem.Text = "Save Assembler File As";
         this.saveAssemblerFileAsToolStripMenuItem.Click += new System.EventHandler(this.saveAssemblerFileAsToolStripMenuItem_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
         // 
         // importToolStripMenuItem
         // 
         this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromCompiledFileToolStripMenuItem});
         this.importToolStripMenuItem.Name = "importToolStripMenuItem";
         this.importToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
         this.importToolStripMenuItem.Text = "Import";
         this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
         // 
         // fromCompiledFileToolStripMenuItem
         // 
         this.fromCompiledFileToolStripMenuItem.Name = "fromCompiledFileToolStripMenuItem";
         this.fromCompiledFileToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
         this.fromCompiledFileToolStripMenuItem.Text = "From Compiled File";
         this.fromCompiledFileToolStripMenuItem.Click += new System.EventHandler(this.fromCompiledFileToolStripMenuItem_Click);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(222, 6);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
         this.exitToolStripMenuItem.Text = "Exit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // editToolStripMenuItem
         // 
         this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.preferencesToolStripMenuItem});
         this.editToolStripMenuItem.Name = "editToolStripMenuItem";
         this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
         this.editToolStripMenuItem.Text = "Edit";
         // 
         // undoToolStripMenuItem
         // 
         this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
         this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
         this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
         this.undoToolStripMenuItem.Text = "Undo";
         // 
         // redoToolStripMenuItem
         // 
         this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
         this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
         this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
         this.redoToolStripMenuItem.Text = "Redo";
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
         // 
         // preferencesToolStripMenuItem
         // 
         this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
         this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
         this.preferencesToolStripMenuItem.Text = "Preferences";
         this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
         // 
         // assemblerToolStripMenuItem
         // 
         this.assemblerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assembleFileToolStripMenuItem});
         this.assemblerToolStripMenuItem.Name = "assemblerToolStripMenuItem";
         this.assemblerToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
         this.assemblerToolStripMenuItem.Text = "Assembler";
         // 
         // assembleFileToolStripMenuItem
         // 
         this.assembleFileToolStripMenuItem.Name = "assembleFileToolStripMenuItem";
         this.assembleFileToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.assembleFileToolStripMenuItem.Text = "Assemble File";
         this.assembleFileToolStripMenuItem.Click += new System.EventHandler(this.assembleFileToolStripMenuItem_Click);
         // 
         // helpToolStripMenuItem
         // 
         this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
         this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
         this.helpToolStripMenuItem.Text = "Help";
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
         this.aboutToolStripMenuItem.Text = "About";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
         // 
         // m_LayoutPanel
         // 
         this.m_LayoutPanel.ColumnCount = 1;
         this.m_LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.m_LayoutPanel.Controls.Add(this.m_MenuStrip, 0, 0);
         this.m_LayoutPanel.Controls.Add(this.m_DisplayPanel, 0, 1);
         this.m_LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_LayoutPanel.Location = new System.Drawing.Point(0, 0);
         this.m_LayoutPanel.Name = "m_LayoutPanel";
         this.m_LayoutPanel.RowCount = 2;
         this.m_LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.m_LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.m_LayoutPanel.Size = new System.Drawing.Size(784, 561);
         this.m_LayoutPanel.TabIndex = 3;
         // 
         // m_DisplayPanel
         // 
         this.m_DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_DisplayPanel.Location = new System.Drawing.Point(3, 27);
         this.m_DisplayPanel.Name = "m_DisplayPanel";
         this.m_DisplayPanel.Size = new System.Drawing.Size(778, 891);
         this.m_DisplayPanel.TabIndex = 1;
         // 
         // MainWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(784, 561);
         this.Controls.Add(this.m_LayoutPanel);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.m_MenuStrip;
         this.Name = "MainWindow";
         this.Text = "rASM";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
         this.m_MenuStrip.ResumeLayout(false);
         this.m_MenuStrip.PerformLayout();
         this.m_LayoutPanel.ResumeLayout(false);
         this.m_LayoutPanel.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.MenuStrip m_MenuStrip;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem newAssemblerFileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openAssemblerFileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveAssemblerFileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveAssemblerFileAsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem assemblerToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem fromCompiledFileToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem assembleFileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
      private DisplayPanel m_DisplayPanel;
      private System.Windows.Forms.TableLayoutPanel m_LayoutPanel;
      private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
   }
}

