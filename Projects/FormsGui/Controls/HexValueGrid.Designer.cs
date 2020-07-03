namespace Assembler.FormsGui.Controls
{
   partial class HexValueGrid
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
         this.m_HexEditor = new Be.Windows.Forms.HexBox();
         this.SuspendLayout();
         // 
         // m_HexEditor
         // 
         this.m_HexEditor.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_HexEditor.Font = new System.Drawing.Font("Segoe UI", 9F);
         this.m_HexEditor.LineInfoVisible = true;
         this.m_HexEditor.Location = new System.Drawing.Point(0, 0);
         this.m_HexEditor.Name = "m_HexEditor";
         this.m_HexEditor.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
         this.m_HexEditor.Size = new System.Drawing.Size(303, 282);
         this.m_HexEditor.StringViewVisible = true;
         this.m_HexEditor.TabIndex = 0;
         this.m_HexEditor.UseFixedBytesPerLine = true;
         this.m_HexEditor.VScrollBarVisible = true;
         // 
         // HexValueGrid
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_HexEditor);
         this.Name = "HexValueGrid";
         this.Size = new System.Drawing.Size(303, 282);
         this.ResumeLayout(false);

      }

      #endregion

      private Be.Windows.Forms.HexBox m_HexEditor;
   }
}
