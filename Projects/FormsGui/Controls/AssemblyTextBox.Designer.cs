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
         this.m_FileTxtBox = new ICSharpCode.TextEditor.TextEditorControl();
         this.SuspendLayout();
         // 
         // m_FileTxtBox
         // 
         this.m_FileTxtBox.ConvertTabsToSpaces = true;
         this.m_FileTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_FileTxtBox.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.None;
         this.m_FileTxtBox.IsReadOnly = false;
         this.m_FileTxtBox.Location = new System.Drawing.Point(0, 0);
         this.m_FileTxtBox.Name = "m_FileTxtBox";
         this.m_FileTxtBox.ShowMatchingBracket = false;
         this.m_FileTxtBox.Size = new System.Drawing.Size(392, 324);
         this.m_FileTxtBox.TabIndent = 3;
         this.m_FileTxtBox.TabIndex = 0;
         // 
         // AssemblyTextBox
         // 
         this.Controls.Add(this.m_FileTxtBox);
         this.Name = "AssemblyTextBox";
         this.Size = new System.Drawing.Size(392, 324);
         this.ResumeLayout(false);

      }

      #endregion

      private ICSharpCode.TextEditor.TextEditorControl m_FileTxtBox;
   }
}
