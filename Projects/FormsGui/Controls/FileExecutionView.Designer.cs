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
         this.m_TopHalfSplit = new System.Windows.Forms.SplitContainer();
         this.m_DisassemblyTxtBox = new ICSharpCode.TextEditor.TextEditorControl();
         this.m_RegisterData = new System.Windows.Forms.DataGridView();
         this.m_HorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.pauseExecutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.terminateExecutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.RegName = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.jefFileViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.executionViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_ConsoleTxt = new Assembler.FormsGui.Controls.ConsoleTextBox();
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).BeginInit();
         this.m_TopHalfSplit.Panel1.SuspendLayout();
         this.m_TopHalfSplit.Panel2.SuspendLayout();
         this.m_TopHalfSplit.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_RegisterData)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).BeginInit();
         this.m_HorizontalSplitContainer.Panel1.SuspendLayout();
         this.m_HorizontalSplitContainer.Panel2.SuspendLayout();
         this.m_HorizontalSplitContainer.SuspendLayout();
         this.menuStrip1.SuspendLayout();
         this.tableLayoutPanel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.jefFileViewModelBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.executionViewModelBindingSource)).BeginInit();
         this.SuspendLayout();
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
         this.m_TopHalfSplit.Size = new System.Drawing.Size(515, 288);
         this.m_TopHalfSplit.SplitterDistance = 257;
         this.m_TopHalfSplit.TabIndex = 1;
         // 
         // m_DisassemblyTxtBox
         // 
         this.m_DisassemblyTxtBox.ConvertTabsToSpaces = true;
         this.m_DisassemblyTxtBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.jefFileViewModelBindingSource, "DisassembledText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.m_DisassemblyTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_DisassemblyTxtBox.IsReadOnly = false;
         this.m_DisassemblyTxtBox.Location = new System.Drawing.Point(0, 0);
         this.m_DisassemblyTxtBox.Name = "m_DisassemblyTxtBox";
         this.m_DisassemblyTxtBox.ShowMatchingBracket = false;
         this.m_DisassemblyTxtBox.ShowVRuler = false;
         this.m_DisassemblyTxtBox.Size = new System.Drawing.Size(257, 288);
         this.m_DisassemblyTxtBox.TabIndex = 0;
         this.m_DisassemblyTxtBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnCodeWindowClicked);
         // 
         // m_RegisterData
         // 
         this.m_RegisterData.AllowUserToAddRows = false;
         this.m_RegisterData.AllowUserToDeleteRows = false;
         this.m_RegisterData.AutoGenerateColumns = false;
         this.m_RegisterData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.m_RegisterData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.m_RegisterData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RegName,
            this.valueDataGridViewTextBoxColumn});
         this.m_RegisterData.DataSource = this.executionViewModelBindingSource;
         this.m_RegisterData.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_RegisterData.Location = new System.Drawing.Point(0, 0);
         this.m_RegisterData.MultiSelect = false;
         this.m_RegisterData.Name = "m_RegisterData";
         this.m_RegisterData.Size = new System.Drawing.Size(254, 288);
         this.m_RegisterData.TabIndex = 0;
         this.m_RegisterData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnDataGridMouseClick);
         // 
         // m_HorizontalSplitContainer
         // 
         this.m_HorizontalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_HorizontalSplitContainer.Location = new System.Drawing.Point(3, 27);
         this.m_HorizontalSplitContainer.Name = "m_HorizontalSplitContainer";
         this.m_HorizontalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // m_HorizontalSplitContainer.Panel1
         // 
         this.m_HorizontalSplitContainer.Panel1.Controls.Add(this.m_TopHalfSplit);
         // 
         // m_HorizontalSplitContainer.Panel2
         // 
         this.m_HorizontalSplitContainer.Panel2.Controls.Add(this.m_ConsoleTxt);
         this.m_HorizontalSplitContainer.Size = new System.Drawing.Size(515, 405);
         this.m_HorizontalSplitContainer.SplitterDistance = 288;
         this.m_HorizontalSplitContainer.TabIndex = 2;
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.pauseExecutionToolStripMenuItem,
            this.stepToolStripMenuItem,
            this.terminateExecutionToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(521, 24);
         this.menuStrip1.TabIndex = 1;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // testToolStripMenuItem
         // 
         this.testToolStripMenuItem.Name = "testToolStripMenuItem";
         this.testToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
         this.testToolStripMenuItem.Text = "Start Execution";
         this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
         // 
         // pauseExecutionToolStripMenuItem
         // 
         this.pauseExecutionToolStripMenuItem.Name = "pauseExecutionToolStripMenuItem";
         this.pauseExecutionToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
         this.pauseExecutionToolStripMenuItem.Text = "Pause Execution";
         // 
         // stepToolStripMenuItem
         // 
         this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
         this.stepToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
         this.stepToolStripMenuItem.Text = "Step";
         // 
         // terminateExecutionToolStripMenuItem
         // 
         this.terminateExecutionToolStripMenuItem.Name = "terminateExecutionToolStripMenuItem";
         this.terminateExecutionToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
         this.terminateExecutionToolStripMenuItem.Text = "Terminate Execution";
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.ColumnCount = 1;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.tableLayoutPanel1.Controls.Add(this.m_HorizontalSplitContainer, 0, 1);
         this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
         this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 2;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel1.Size = new System.Drawing.Size(521, 435);
         this.tableLayoutPanel1.TabIndex = 1;
         // 
         // RegName
         // 
         this.RegName.DataPropertyName = "RegisterName";
         this.RegName.HeaderText = "Register Name";
         this.RegName.Name = "RegName";
         this.RegName.ReadOnly = true;
         // 
         // jefFileViewModelBindingSource
         // 
         this.jefFileViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.JefFileViewModel);
         // 
         // valueDataGridViewTextBoxColumn
         // 
         this.valueDataGridViewTextBoxColumn.DataPropertyName = "ValueStr";
         this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
         this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
         // 
         // executionViewModelBindingSource
         // 
         this.executionViewModelBindingSource.DataMember = "Registers";
         this.executionViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.ExecutionViewModel);
         // 
         // m_ConsoleTxt
         // 
         this.m_ConsoleTxt.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_ConsoleTxt.Location = new System.Drawing.Point(0, 0);
         this.m_ConsoleTxt.Name = "m_ConsoleTxt";
         this.m_ConsoleTxt.Size = new System.Drawing.Size(515, 113);
         this.m_ConsoleTxt.TabIndex = 0;
         // 
         // FileExecutionView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.tableLayoutPanel1);
         this.Name = "FileExecutionView";
         this.Size = new System.Drawing.Size(521, 435);
         this.m_TopHalfSplit.Panel1.ResumeLayout(false);
         this.m_TopHalfSplit.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).EndInit();
         this.m_TopHalfSplit.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_RegisterData)).EndInit();
         this.m_HorizontalSplitContainer.Panel1.ResumeLayout(false);
         this.m_HorizontalSplitContainer.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).EndInit();
         this.m_HorizontalSplitContainer.ResumeLayout(false);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.tableLayoutPanel1.ResumeLayout(false);
         this.tableLayoutPanel1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.jefFileViewModelBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.executionViewModelBindingSource)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion
      private System.Windows.Forms.SplitContainer m_TopHalfSplit;
      private System.Windows.Forms.DataGridView m_RegisterData;
      private System.Windows.Forms.SplitContainer m_HorizontalSplitContainer;
      private System.Windows.Forms.BindingSource executionViewModelBindingSource;
      private ConsoleTextBox m_ConsoleTxt;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.ToolStripMenuItem pauseExecutionToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem terminateExecutionToolStripMenuItem;
      private ICSharpCode.TextEditor.TextEditorControl m_DisassemblyTxtBox;
      private System.Windows.Forms.BindingSource jefFileViewModelBindingSource;
      private System.Windows.Forms.DataGridViewTextBoxColumn RegName;
      private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
   }
}
