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
         this.components = new System.ComponentModel.Container();
         this.m_FileTxtBox = new ICSharpCode.TextEditor.TextEditorControl();
         this.preferencesViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.assemblyFileViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.preferencesViewModelBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.assemblyFileViewModelBindingSource)).BeginInit();
         this.SuspendLayout();
         // 
         // m_FileTxtBox
         // 
         this.m_FileTxtBox.ConvertTabsToSpaces = true;
         this.m_FileTxtBox.DataBindings.Add(new System.Windows.Forms.Binding("ConvertTabsToSpaces", this.preferencesViewModelBindingSource, "ReplaceTabsWithSpaces", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.m_FileTxtBox.DataBindings.Add(new System.Windows.Forms.Binding("ShowLineNumbers", this.preferencesViewModelBindingSource, "ShowLineNumbers", true));
         this.m_FileTxtBox.DataBindings.Add(new System.Windows.Forms.Binding("TabIndent", this.preferencesViewModelBindingSource, "NumSpacesToReplaceTabWith", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, "1"));
         this.m_FileTxtBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.assemblyFileViewModelBindingSource, "FileText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.m_FileTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_FileTxtBox.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.None;
         this.m_FileTxtBox.IsReadOnly = false;
         this.m_FileTxtBox.Location = new System.Drawing.Point(0, 0);
         this.m_FileTxtBox.Name = "m_FileTxtBox";
         this.m_FileTxtBox.ShowMatchingBracket = false;
         this.m_FileTxtBox.Size = new System.Drawing.Size(392, 324);
         this.m_FileTxtBox.TabIndent = 3;
         this.m_FileTxtBox.TabIndex = 0;
         this.m_FileTxtBox.TextChanged += new System.EventHandler(this.OnTextChanged);
         // 
         // preferencesViewModelBindingSource
         // 
         this.preferencesViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.PreferencesViewModel);
         // 
         // assemblyFileViewModelBindingSource
         // 
         this.assemblyFileViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.AssemblyFileViewModel);
         // 
         // AssemblyTextBox
         // 
         this.Controls.Add(this.m_FileTxtBox);
         this.Name = "AssemblyTextBox";
         this.Size = new System.Drawing.Size(392, 324);
         ((System.ComponentModel.ISupportInitialize)(this.preferencesViewModelBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.assemblyFileViewModelBindingSource)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private ICSharpCode.TextEditor.TextEditorControl m_FileTxtBox;
      private System.Windows.Forms.BindingSource preferencesViewModelBindingSource;
      private System.Windows.Forms.BindingSource assemblyFileViewModelBindingSource;
   }
}
