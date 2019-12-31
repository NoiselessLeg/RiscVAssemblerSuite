namespace Assembler.PrototypeGui
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
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assembleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FormSplitter = new System.Windows.Forms.SplitContainer();
            this.m_TabCtrl = new System.Windows.Forms.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_LogConsole = new Assembler.PrototypeGui.ConsoleControl();
            this.m_MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_FormSplitter)).BeginInit();
            this.m_FormSplitter.Panel1.SuspendLayout();
            this.m_FormSplitter.Panel2.SuspendLayout();
            this.m_FormSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileToolStripMenuItem,
            this.m_EditToolStripMenuItem,
            this.m_ProjectToolStripMenuItem});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Size = new System.Drawing.Size(838, 24);
            this.m_MenuStrip.TabIndex = 0;
            this.m_MenuStrip.Text = "menuStrip1";
            // 
            // m_FileToolStripMenuItem
            // 
            this.m_FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.saveFileAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.m_FileToolStripMenuItem.Name = "m_FileToolStripMenuItem";
            this.m_FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.m_FileToolStripMenuItem.Text = "File";
            // 
            // m_EditToolStripMenuItem
            // 
            this.m_EditToolStripMenuItem.Name = "m_EditToolStripMenuItem";
            this.m_EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.m_EditToolStripMenuItem.Text = "Edit";
            // 
            // m_ProjectToolStripMenuItem
            // 
            this.m_ProjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assembleToolStripMenuItem});
            this.m_ProjectToolStripMenuItem.Name = "m_ProjectToolStripMenuItem";
            this.m_ProjectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.m_ProjectToolStripMenuItem.Text = "Project";
            // 
            // assembleToolStripMenuItem
            // 
            this.assembleToolStripMenuItem.Name = "assembleToolStripMenuItem";
            this.assembleToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.assembleToolStripMenuItem.Text = "Assemble";
            this.assembleToolStripMenuItem.Click += new System.EventHandler(this.assembleToolStripMenuItem_Click);
            // 
            // m_FormSplitter
            // 
            this.m_FormSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_FormSplitter.Location = new System.Drawing.Point(0, 24);
            this.m_FormSplitter.Name = "m_FormSplitter";
            this.m_FormSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_FormSplitter.Panel1
            // 
            this.m_FormSplitter.Panel1.Controls.Add(this.m_TabCtrl);
            // 
            // m_FormSplitter.Panel2
            // 
            this.m_FormSplitter.Panel2.Controls.Add(this.m_LogConsole);
            this.m_FormSplitter.Panel2.Controls.Add(this.statusStrip1);
            this.m_FormSplitter.Size = new System.Drawing.Size(838, 503);
            this.m_FormSplitter.SplitterDistance = 353;
            this.m_FormSplitter.TabIndex = 1;
            // 
            // m_TabCtrl
            // 
            this.m_TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabCtrl.Location = new System.Drawing.Point(0, 0);
            this.m_TabCtrl.Name = "m_TabCtrl";
            this.m_TabCtrl.SelectedIndex = 0;
            this.m_TabCtrl.Size = new System.Drawing.Size(838, 353);
            this.m_TabCtrl.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 124);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(838, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            // 
            // saveFileAsToolStripMenuItem
            // 
            this.saveFileAsToolStripMenuItem.Name = "saveFileAsToolStripMenuItem";
            this.saveFileAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveFileAsToolStripMenuItem.Text = "Save File As";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // m_LogConsole
            // 
            this.m_LogConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_LogConsole.Location = new System.Drawing.Point(0, 0);
            this.m_LogConsole.Name = "m_LogConsole";
            this.m_LogConsole.Size = new System.Drawing.Size(838, 124);
            this.m_LogConsole.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 527);
            this.Controls.Add(this.m_FormSplitter);
            this.Controls.Add(this.m_MenuStrip);
            this.Name = "MainWindow";
            this.Text = "MiASMa";
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            this.m_FormSplitter.Panel1.ResumeLayout(false);
            this.m_FormSplitter.Panel2.ResumeLayout(false);
            this.m_FormSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_FormSplitter)).EndInit();
            this.m_FormSplitter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assembleToolStripMenuItem;
        private System.Windows.Forms.SplitContainer m_FormSplitter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl m_TabCtrl;
        private ConsoleControl m_LogConsole;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

