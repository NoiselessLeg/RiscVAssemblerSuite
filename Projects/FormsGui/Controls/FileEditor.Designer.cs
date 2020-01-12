namespace Assembler.FormsGui.Controls
{
   partial class FileEditor
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
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.m_FileTxt = new Assembler.FormsGui.Controls.AssemblyTextBox();
         this.m_LoggerTxtBox = new System.Windows.Forms.TextBox();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.SuspendLayout();
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.m_FileTxt);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.m_LoggerTxtBox);
         this.splitContainer1.Size = new System.Drawing.Size(369, 313);
         this.splitContainer1.SplitterDistance = 204;
         this.splitContainer1.TabIndex = 0;
         // 
         // m_FileTxt
         // 
         this.m_FileTxt.BackColor = System.Drawing.Color.DarkSalmon;
         this.m_FileTxt.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_FileTxt.Location = new System.Drawing.Point(0, 0);
         this.m_FileTxt.Name = "m_FileTxt";
         this.m_FileTxt.Size = new System.Drawing.Size(369, 204);
         this.m_FileTxt.TabIndex = 0;
         // 
         // m_LoggerTxtBox
         // 
         this.m_LoggerTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_LoggerTxtBox.Location = new System.Drawing.Point(0, 0);
         this.m_LoggerTxtBox.Multiline = true;
         this.m_LoggerTxtBox.Name = "m_LoggerTxtBox";
         this.m_LoggerTxtBox.ReadOnly = true;
         this.m_LoggerTxtBox.Size = new System.Drawing.Size(369, 105);
         this.m_LoggerTxtBox.TabIndex = 0;
         // 
         // FileEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.splitContainer1);
         this.Name = "FileEditor";
         this.Size = new System.Drawing.Size(369, 313);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         this.splitContainer1.Panel2.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer1;
      private AssemblyTextBox m_FileTxt;
      private System.Windows.Forms.TextBox m_LoggerTxtBox;
   }
}
