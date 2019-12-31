namespace Assembler.FormsGui.Controls
{
   partial class ConsoleTextBox
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
         this.m_UnderlyingTxt = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // m_UnderlyingTxt
         // 
         this.m_UnderlyingTxt.AcceptsReturn = true;
         this.m_UnderlyingTxt.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_UnderlyingTxt.Location = new System.Drawing.Point(0, 0);
         this.m_UnderlyingTxt.Multiline = true;
         this.m_UnderlyingTxt.Name = "m_UnderlyingTxt";
         this.m_UnderlyingTxt.Size = new System.Drawing.Size(150, 150);
         this.m_UnderlyingTxt.TabIndex = 1;
         // 
         // ConsoleTextBox
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_UnderlyingTxt);
         this.Name = "ConsoleTextBox";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox m_UnderlyingTxt;
   }
}
