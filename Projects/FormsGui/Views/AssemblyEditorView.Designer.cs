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
         this.m_EditorLoggerSplitContainer = new System.Windows.Forms.SplitContainer();
         this.m_OpenFileTabs = new System.Windows.Forms.TabControl();
         this.m_LogTxt = new System.Windows.Forms.TextBox();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
         this.m_NumericValue = new Controls.BindableToolStripStatusLabel();
         ((System.ComponentModel.ISupportInitialize)(this.m_EditorLoggerSplitContainer)).BeginInit();
         this.m_EditorLoggerSplitContainer.Panel1.SuspendLayout();
         this.m_EditorLoggerSplitContainer.Panel2.SuspendLayout();
         this.m_EditorLoggerSplitContainer.SuspendLayout();
         this.statusStrip1.SuspendLayout();
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
         // EditorView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_EditorLoggerSplitContainer);
         this.Name = "EditorView";
         this.Size = new System.Drawing.Size(446, 376);
         this.m_EditorLoggerSplitContainer.Panel1.ResumeLayout(false);
         this.m_EditorLoggerSplitContainer.Panel2.ResumeLayout(false);
         this.m_EditorLoggerSplitContainer.Panel2.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_EditorLoggerSplitContainer)).EndInit();
         this.m_EditorLoggerSplitContainer.ResumeLayout(false);
         this.statusStrip1.ResumeLayout(false);
         this.statusStrip1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.SplitContainer m_EditorLoggerSplitContainer;
      private System.Windows.Forms.TextBox m_LogTxt;
      private System.Windows.Forms.TabControl m_OpenFileTabs;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
      private Controls.BindableToolStripStatusLabel m_NumericValue;
   }
}
