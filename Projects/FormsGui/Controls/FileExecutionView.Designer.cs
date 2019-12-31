namespace Assembler.FormsGui.Controls
{
   partial class FileExecutionView
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
         this.m_TerminalTxtBox = new System.Windows.Forms.TextBox();
         this.m_TopHalfSplit = new System.Windows.Forms.SplitContainer();
         this.m_DisassemblyTxtBox = new System.Windows.Forms.RichTextBox();
         this.m_RegisterData = new System.Windows.Forms.DataGridView();
         this.m_HorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
         this.executionViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.registerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.registerValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).BeginInit();
         this.m_TopHalfSplit.Panel1.SuspendLayout();
         this.m_TopHalfSplit.Panel2.SuspendLayout();
         this.m_TopHalfSplit.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_RegisterData)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).BeginInit();
         this.m_HorizontalSplitContainer.Panel1.SuspendLayout();
         this.m_HorizontalSplitContainer.Panel2.SuspendLayout();
         this.m_HorizontalSplitContainer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.executionViewModelBindingSource)).BeginInit();
         this.SuspendLayout();
         // 
         // m_TerminalTxtBox
         // 
         this.m_TerminalTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_TerminalTxtBox.Location = new System.Drawing.Point(0, 0);
         this.m_TerminalTxtBox.Multiline = true;
         this.m_TerminalTxtBox.Name = "m_TerminalTxtBox";
         this.m_TerminalTxtBox.Size = new System.Drawing.Size(490, 101);
         this.m_TerminalTxtBox.TabIndex = 0;
         // 
         // m_TopHalfSplit
         // 
         this.m_TopHalfSplit.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_TopHalfSplit.Location = new System.Drawing.Point(0, 0);
         this.m_TopHalfSplit.Name = "m_TopHalfSplit";
         // 
         // m_TopHalfSplit.Panel1
         // 
         this.m_TopHalfSplit.Panel1.Controls.Add(this.m_DisassemblyTxtBox);
         // 
         // m_TopHalfSplit.Panel2
         // 
         this.m_TopHalfSplit.Panel2.Controls.Add(this.m_RegisterData);
         this.m_TopHalfSplit.Size = new System.Drawing.Size(490, 261);
         this.m_TopHalfSplit.SplitterDistance = 245;
         this.m_TopHalfSplit.TabIndex = 1;
         // 
         // m_DisassemblyTxtBox
         // 
         this.m_DisassemblyTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_DisassemblyTxtBox.Location = new System.Drawing.Point(0, 0);
         this.m_DisassemblyTxtBox.Name = "m_DisassemblyTxtBox";
         this.m_DisassemblyTxtBox.Size = new System.Drawing.Size(245, 261);
         this.m_DisassemblyTxtBox.TabIndex = 0;
         this.m_DisassemblyTxtBox.Text = "";
         // 
         // m_RegisterData
         // 
         this.m_RegisterData.AutoGenerateColumns = false;
         this.m_RegisterData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.m_RegisterData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.registerNameDataGridViewTextBoxColumn,
            this.registerValueDataGridViewTextBoxColumn});
         this.m_RegisterData.DataSource = this.executionViewModelBindingSource;
         this.m_RegisterData.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_RegisterData.Location = new System.Drawing.Point(0, 0);
         this.m_RegisterData.Name = "m_RegisterData";
         this.m_RegisterData.Size = new System.Drawing.Size(241, 261);
         this.m_RegisterData.TabIndex = 0;
         // 
         // m_HorizontalSplitContainer
         // 
         this.m_HorizontalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_HorizontalSplitContainer.Location = new System.Drawing.Point(0, 0);
         this.m_HorizontalSplitContainer.Name = "m_HorizontalSplitContainer";
         this.m_HorizontalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // m_HorizontalSplitContainer.Panel1
         // 
         this.m_HorizontalSplitContainer.Panel1.Controls.Add(this.m_TopHalfSplit);
         // 
         // m_HorizontalSplitContainer.Panel2
         // 
         this.m_HorizontalSplitContainer.Panel2.Controls.Add(this.m_TerminalTxtBox);
         this.m_HorizontalSplitContainer.Size = new System.Drawing.Size(490, 366);
         this.m_HorizontalSplitContainer.SplitterDistance = 261;
         this.m_HorizontalSplitContainer.TabIndex = 2;
         // 
         // executionViewModelBindingSource
         // 
         this.executionViewModelBindingSource.DataMember = "Registers";
         this.executionViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.ExecutionViewModel);
         // 
         // registerNameDataGridViewTextBoxColumn
         // 
         this.registerNameDataGridViewTextBoxColumn.DataPropertyName = "RegisterName";
         this.registerNameDataGridViewTextBoxColumn.HeaderText = "Register";
         this.registerNameDataGridViewTextBoxColumn.Name = "registerNameDataGridViewTextBoxColumn";
         this.registerNameDataGridViewTextBoxColumn.ReadOnly = true;
         // 
         // registerValueDataGridViewTextBoxColumn
         // 
         this.registerValueDataGridViewTextBoxColumn.DataPropertyName = "RegisterValue";
         this.registerValueDataGridViewTextBoxColumn.HeaderText = "Value";
         this.registerValueDataGridViewTextBoxColumn.Name = "registerValueDataGridViewTextBoxColumn";
         // 
         // FileExecutionView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_HorizontalSplitContainer);
         this.Name = "FileExecutionView";
         this.Size = new System.Drawing.Size(490, 366);
         this.m_TopHalfSplit.Panel1.ResumeLayout(false);
         this.m_TopHalfSplit.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).EndInit();
         this.m_TopHalfSplit.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_RegisterData)).EndInit();
         this.m_HorizontalSplitContainer.Panel1.ResumeLayout(false);
         this.m_HorizontalSplitContainer.Panel2.ResumeLayout(false);
         this.m_HorizontalSplitContainer.Panel2.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).EndInit();
         this.m_HorizontalSplitContainer.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.executionViewModelBindingSource)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TextBox m_TerminalTxtBox;
      private System.Windows.Forms.SplitContainer m_TopHalfSplit;
      private System.Windows.Forms.RichTextBox m_DisassemblyTxtBox;
      private System.Windows.Forms.DataGridView m_RegisterData;
      private System.Windows.Forms.SplitContainer m_HorizontalSplitContainer;
      private System.Windows.Forms.BindingSource executionViewModelBindingSource;
      private System.Windows.Forms.DataGridViewTextBoxColumn registerNameDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn registerValueDataGridViewTextBoxColumn;
   }
}
