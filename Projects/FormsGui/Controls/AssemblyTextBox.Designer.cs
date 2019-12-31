namespace Assembler.FormsGui.Controls
{
   partial class AssemblyTextBox
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
         this.m_FileTxtBox = new System.Windows.Forms.RichTextBox();
         this.SuspendLayout();
         // 
         // m_FileTxtBox
         // 
         this.m_FileTxtBox.DetectUrls = false;
         this.m_FileTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_FileTxtBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.m_FileTxtBox.Location = new System.Drawing.Point(0, 0);
         this.m_FileTxtBox.Name = "m_FileTxtBox";
         this.m_FileTxtBox.Size = new System.Drawing.Size(392, 324);
         this.m_FileTxtBox.TabIndex = 0;
         this.m_FileTxtBox.Text = "";
         // 
         // EditorTextBox
         // 
         this.Controls.Add(this.m_FileTxtBox);
         this.Name = "EditorTextBox";
         this.Size = new System.Drawing.Size(392, 324);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.RichTextBox m_FileTxtBox;
   }
}
