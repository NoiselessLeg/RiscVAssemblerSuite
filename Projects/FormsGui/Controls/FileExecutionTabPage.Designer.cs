namespace Assembler.FormsGui.Controls
{
   partial class FileExecutionTabPage
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
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
         this.m_TopHalfSplit = new System.Windows.Forms.SplitContainer();
         this.m_SrcGrid = new Assembler.FormsGui.Controls.BufferedDataGridView();
         this.jefFileViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_RegisterData = new Assembler.FormsGui.Controls.BufferedDataGridView();
         this.RegName = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.m_RegisterGridCtxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.m_ShowDecValuesItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_ShowHexValuesItem = new System.Windows.Forms.ToolStripMenuItem();
         this.executionViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_HorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.m_DataSegmentGrdView = new Assembler.FormsGui.Controls.BufferedDataGridView();
         this.BaseAddressStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word0Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word1Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word2Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word3Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.m_DataGridCtxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.m_ShowDataElemsAsDecimalBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.m_ShowDataElemsAsHexBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.m_ConsoleTxt = new Assembler.FormsGui.Controls.ConsoleTextBox();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.m_StartBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.m_PauseBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.m_ResumeBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.m_StepBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.m_TerminateBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.IsBreakpointApplied = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.programCounterLocationStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.rawBytesStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.OriginalText = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.instructionTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).BeginInit();
         this.m_TopHalfSplit.Panel1.SuspendLayout();
         this.m_TopHalfSplit.Panel2.SuspendLayout();
         this.m_TopHalfSplit.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_SrcGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.jefFileViewModelBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_RegisterData)).BeginInit();
         this.m_RegisterGridCtxMenu.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.executionViewModelBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).BeginInit();
         this.m_HorizontalSplitContainer.Panel1.SuspendLayout();
         this.m_HorizontalSplitContainer.Panel2.SuspendLayout();
         this.m_HorizontalSplitContainer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_DataSegmentGrdView)).BeginInit();
         this.m_DataGridCtxMenu.SuspendLayout();
         this.menuStrip1.SuspendLayout();
         this.tableLayoutPanel1.SuspendLayout();
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
         this.m_TopHalfSplit.Panel1.Controls.Add(this.m_SrcGrid);
         // 
         // m_TopHalfSplit.Panel2
         // 
         this.m_TopHalfSplit.Panel2.Controls.Add(this.m_RegisterData);
         this.m_TopHalfSplit.Size = new System.Drawing.Size(515, 152);
         this.m_TopHalfSplit.SplitterDistance = 257;
         this.m_TopHalfSplit.TabIndex = 1;
         // 
         // m_SrcGrid
         // 
         this.m_SrcGrid.AllowUserToAddRows = false;
         this.m_SrcGrid.AllowUserToDeleteRows = false;
         dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
         dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
         dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightGray;
         dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
         this.m_SrcGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
         this.m_SrcGrid.AutoGenerateColumns = false;
         this.m_SrcGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.m_SrcGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.m_SrcGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsBreakpointApplied,
            this.programCounterLocationStrDataGridViewTextBoxColumn,
            this.rawBytesStrDataGridViewTextBoxColumn,
            this.OriginalText,
            this.instructionTextDataGridViewTextBoxColumn});
         this.m_SrcGrid.DataMember = "InstructionList";
         this.m_SrcGrid.DataSource = this.jefFileViewModelBindingSource;
         dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
         dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
         dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
         dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
         dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
         dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
         this.m_SrcGrid.DefaultCellStyle = dataGridViewCellStyle2;
         this.m_SrcGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_SrcGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
         this.m_SrcGrid.Location = new System.Drawing.Point(0, 0);
         this.m_SrcGrid.Name = "m_SrcGrid";
         this.m_SrcGrid.RowHeadersVisible = false;
         this.m_SrcGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
         this.m_SrcGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
         this.m_SrcGrid.ShowEditingIcon = false;
         this.m_SrcGrid.Size = new System.Drawing.Size(257, 152);
         this.m_SrcGrid.TabIndex = 0;
         this.m_SrcGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnBreakpointSet);
         // 
         // jefFileViewModelBindingSource
         // 
         this.jefFileViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.DisassembledFileViewModel);
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
         this.m_RegisterData.ContextMenuStrip = this.m_RegisterGridCtxMenu;
         this.m_RegisterData.DataSource = this.executionViewModelBindingSource;
         this.m_RegisterData.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_RegisterData.Location = new System.Drawing.Point(0, 0);
         this.m_RegisterData.MultiSelect = false;
         this.m_RegisterData.Name = "m_RegisterData";
         this.m_RegisterData.RowHeadersVisible = false;
         this.m_RegisterData.Size = new System.Drawing.Size(254, 152);
         this.m_RegisterData.TabIndex = 0;
         // 
         // RegName
         // 
         this.RegName.DataPropertyName = "RegisterName";
         this.RegName.HeaderText = "Register Name";
         this.RegName.Name = "RegName";
         this.RegName.ReadOnly = true;
         // 
         // valueDataGridViewTextBoxColumn
         // 
         this.valueDataGridViewTextBoxColumn.DataPropertyName = "ValueStr";
         this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
         this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
         // 
         // m_RegisterGridCtxMenu
         // 
         this.m_RegisterGridCtxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ShowDecValuesItem,
            this.m_ShowHexValuesItem});
         this.m_RegisterGridCtxMenu.Name = "m_RegisterGridCtxMenu";
         this.m_RegisterGridCtxMenu.Size = new System.Drawing.Size(247, 48);
         // 
         // m_ShowDecValuesItem
         // 
         this.m_ShowDecValuesItem.Checked = true;
         this.m_ShowDecValuesItem.CheckOnClick = true;
         this.m_ShowDecValuesItem.CheckState = System.Windows.Forms.CheckState.Checked;
         this.m_ShowDecValuesItem.Name = "m_ShowDecValuesItem";
         this.m_ShowDecValuesItem.Size = new System.Drawing.Size(246, 22);
         this.m_ShowDecValuesItem.Text = "Display all values in decimal";
         this.m_ShowDecValuesItem.ToolTipText = "Shows all register values in decimal";
         // 
         // m_ShowHexValuesItem
         // 
         this.m_ShowHexValuesItem.CheckOnClick = true;
         this.m_ShowHexValuesItem.Name = "m_ShowHexValuesItem";
         this.m_ShowHexValuesItem.Size = new System.Drawing.Size(246, 22);
         this.m_ShowHexValuesItem.Text = "Display all values in hexadecimal";
         // 
         // executionViewModelBindingSource
         // 
         this.executionViewModelBindingSource.DataMember = "Registers";
         this.executionViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.ExecutionViewModel);
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
         this.m_HorizontalSplitContainer.Panel2.Controls.Add(this.splitContainer1);
         this.m_HorizontalSplitContainer.Size = new System.Drawing.Size(515, 405);
         this.m_HorizontalSplitContainer.SplitterDistance = 152;
         this.m_HorizontalSplitContainer.TabIndex = 2;
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
         this.splitContainer1.Panel1.Controls.Add(this.m_DataSegmentGrdView);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.m_ConsoleTxt);
         this.splitContainer1.Size = new System.Drawing.Size(515, 249);
         this.splitContainer1.SplitterDistance = 114;
         this.splitContainer1.TabIndex = 1;
         // 
         // m_DataSegmentGrdView
         // 
         this.m_DataSegmentGrdView.AllowUserToAddRows = false;
         this.m_DataSegmentGrdView.AllowUserToDeleteRows = false;
         this.m_DataSegmentGrdView.AllowUserToResizeRows = false;
         this.m_DataSegmentGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.m_DataSegmentGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.m_DataSegmentGrdView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BaseAddressStr,
            this.Word0Str,
            this.Word1Str,
            this.Word2Str,
            this.Word3Str});
         this.m_DataSegmentGrdView.ContextMenuStrip = this.m_DataGridCtxMenu;
         this.m_DataSegmentGrdView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_DataSegmentGrdView.Location = new System.Drawing.Point(0, 0);
         this.m_DataSegmentGrdView.Name = "m_DataSegmentGrdView";
         this.m_DataSegmentGrdView.RowHeadersVisible = false;
         this.m_DataSegmentGrdView.Size = new System.Drawing.Size(515, 114);
         this.m_DataSegmentGrdView.TabIndex = 0;
         // 
         // BaseAddressStr
         // 
         this.BaseAddressStr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.BaseAddressStr.DataPropertyName = "BaseAddressStr";
         this.BaseAddressStr.HeaderText = "Address";
         this.BaseAddressStr.Name = "BaseAddressStr";
         this.BaseAddressStr.ReadOnly = true;
         this.BaseAddressStr.Width = 70;
         // 
         // Word0Str
         // 
         this.Word0Str.DataPropertyName = "Word0Str";
         this.Word0Str.HeaderText = "Value (+0)";
         this.Word0Str.Name = "Word0Str";
         // 
         // Word1Str
         // 
         this.Word1Str.DataPropertyName = "Word1Str";
         this.Word1Str.HeaderText = "Value (+4)";
         this.Word1Str.Name = "Word1Str";
         // 
         // Word2Str
         // 
         this.Word2Str.DataPropertyName = "Word2Str";
         this.Word2Str.HeaderText = "Value (+8)";
         this.Word2Str.Name = "Word2Str";
         // 
         // Word3Str
         // 
         this.Word3Str.DataPropertyName = "Word3Str";
         this.Word3Str.HeaderText = "Value (+C)";
         this.Word3Str.Name = "Word3Str";
         // 
         // m_DataGridCtxMenu
         // 
         this.m_DataGridCtxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ShowDataElemsAsDecimalBtn,
            this.m_ShowDataElemsAsHexBtn});
         this.m_DataGridCtxMenu.Name = "m_RegisterGridCtxMenu";
         this.m_DataGridCtxMenu.Size = new System.Drawing.Size(247, 48);
         // 
         // m_ShowDataElemsAsDecimalBtn
         // 
         this.m_ShowDataElemsAsDecimalBtn.CheckOnClick = true;
         this.m_ShowDataElemsAsDecimalBtn.Name = "m_ShowDataElemsAsDecimalBtn";
         this.m_ShowDataElemsAsDecimalBtn.Size = new System.Drawing.Size(246, 22);
         this.m_ShowDataElemsAsDecimalBtn.Text = "Display all values in decimal";
         this.m_ShowDataElemsAsDecimalBtn.ToolTipText = "Shows all register values in decimal";
         // 
         // m_ShowDataElemsAsHexBtn
         // 
         this.m_ShowDataElemsAsHexBtn.Checked = true;
         this.m_ShowDataElemsAsHexBtn.CheckOnClick = true;
         this.m_ShowDataElemsAsHexBtn.CheckState = System.Windows.Forms.CheckState.Checked;
         this.m_ShowDataElemsAsHexBtn.Name = "m_ShowDataElemsAsHexBtn";
         this.m_ShowDataElemsAsHexBtn.Size = new System.Drawing.Size(246, 22);
         this.m_ShowDataElemsAsHexBtn.Text = "Display all values in hexadecimal";
         // 
         // m_ConsoleTxt
         // 
         this.m_ConsoleTxt.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_ConsoleTxt.Location = new System.Drawing.Point(0, 0);
         this.m_ConsoleTxt.Name = "m_ConsoleTxt";
         this.m_ConsoleTxt.Size = new System.Drawing.Size(515, 131);
         this.m_ConsoleTxt.TabIndex = 0;
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_StartBtn,
            this.m_PauseBtn,
            this.m_ResumeBtn,
            this.m_StepBtn,
            this.m_TerminateBtn});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(521, 24);
         this.menuStrip1.TabIndex = 1;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // m_StartBtn
         // 
         this.m_StartBtn.Name = "m_StartBtn";
         this.m_StartBtn.Size = new System.Drawing.Size(98, 20);
         this.m_StartBtn.Text = "Start Execution";
         this.m_StartBtn.Click += new System.EventHandler(this.OnStartButtonClicked);
         // 
         // m_PauseBtn
         // 
         this.m_PauseBtn.Name = "m_PauseBtn";
         this.m_PauseBtn.Size = new System.Drawing.Size(105, 20);
         this.m_PauseBtn.Text = "Pause Execution";
         // 
         // m_ResumeBtn
         // 
         this.m_ResumeBtn.Name = "m_ResumeBtn";
         this.m_ResumeBtn.Size = new System.Drawing.Size(116, 20);
         this.m_ResumeBtn.Text = "Resume Execution";
         // 
         // m_StepBtn
         // 
         this.m_StepBtn.Name = "m_StepBtn";
         this.m_StepBtn.Size = new System.Drawing.Size(42, 20);
         this.m_StepBtn.Text = "Step";
         // 
         // m_TerminateBtn
         // 
         this.m_TerminateBtn.Name = "m_TerminateBtn";
         this.m_TerminateBtn.Size = new System.Drawing.Size(126, 20);
         this.m_TerminateBtn.Text = "Terminate Execution";
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
         // IsBreakpointApplied
         // 
         this.IsBreakpointApplied.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.IsBreakpointApplied.DataPropertyName = "IsBreakpointApplied";
         this.IsBreakpointApplied.HeaderText = "Bkpt";
         this.IsBreakpointApplied.Name = "IsBreakpointApplied";
         this.IsBreakpointApplied.Width = 35;
         // 
         // programCounterLocationStrDataGridViewTextBoxColumn
         // 
         this.programCounterLocationStrDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.programCounterLocationStrDataGridViewTextBoxColumn.DataPropertyName = "ProgramCounterLocationStr";
         this.programCounterLocationStrDataGridViewTextBoxColumn.HeaderText = "Program Counter Address";
         this.programCounterLocationStrDataGridViewTextBoxColumn.Name = "programCounterLocationStrDataGridViewTextBoxColumn";
         this.programCounterLocationStrDataGridViewTextBoxColumn.ReadOnly = true;
         this.programCounterLocationStrDataGridViewTextBoxColumn.Width = 139;
         // 
         // rawBytesStrDataGridViewTextBoxColumn
         // 
         this.rawBytesStrDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.rawBytesStrDataGridViewTextBoxColumn.DataPropertyName = "RawBytesStr";
         this.rawBytesStrDataGridViewTextBoxColumn.HeaderText = "Inst. Word";
         this.rawBytesStrDataGridViewTextBoxColumn.Name = "rawBytesStrDataGridViewTextBoxColumn";
         this.rawBytesStrDataGridViewTextBoxColumn.ReadOnly = true;
         this.rawBytesStrDataGridViewTextBoxColumn.Width = 75;
         // 
         // OriginalText
         // 
         this.OriginalText.DataPropertyName = "OriginalInstructionSourceText";
         this.OriginalText.HeaderText = "Original Instruction";
         this.OriginalText.Name = "OriginalText";
         this.OriginalText.ReadOnly = true;
         // 
         // instructionTextDataGridViewTextBoxColumn
         // 
         this.instructionTextDataGridViewTextBoxColumn.DataPropertyName = "InstructionText";
         this.instructionTextDataGridViewTextBoxColumn.HeaderText = "Instruction";
         this.instructionTextDataGridViewTextBoxColumn.Name = "instructionTextDataGridViewTextBoxColumn";
         this.instructionTextDataGridViewTextBoxColumn.ReadOnly = true;
         // 
         // FileExecutionTabPage
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.tableLayoutPanel1);
         this.Name = "FileExecutionTabPage";
         this.Size = new System.Drawing.Size(521, 435);
         this.m_TopHalfSplit.Panel1.ResumeLayout(false);
         this.m_TopHalfSplit.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).EndInit();
         this.m_TopHalfSplit.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_SrcGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.jefFileViewModelBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_RegisterData)).EndInit();
         this.m_RegisterGridCtxMenu.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.executionViewModelBindingSource)).EndInit();
         this.m_HorizontalSplitContainer.Panel1.ResumeLayout(false);
         this.m_HorizontalSplitContainer.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).EndInit();
         this.m_HorizontalSplitContainer.ResumeLayout(false);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_DataSegmentGrdView)).EndInit();
         this.m_DataGridCtxMenu.ResumeLayout(false);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.tableLayoutPanel1.ResumeLayout(false);
         this.tableLayoutPanel1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion
      private System.Windows.Forms.SplitContainer m_TopHalfSplit;
      private Assembler.FormsGui.Controls.BufferedDataGridView m_RegisterData;
      private System.Windows.Forms.SplitContainer m_HorizontalSplitContainer;
      private System.Windows.Forms.BindingSource executionViewModelBindingSource;
      private ConsoleTextBox m_ConsoleTxt;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem m_StartBtn;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private Assembler.FormsGui.Controls.BindableToolStripMenuItem m_PauseBtn;
      private Assembler.FormsGui.Controls.BindableToolStripMenuItem m_StepBtn;
      private Assembler.FormsGui.Controls.BindableToolStripMenuItem m_TerminateBtn;
      private System.Windows.Forms.BindingSource jefFileViewModelBindingSource;
      private System.Windows.Forms.DataGridViewTextBoxColumn RegName;
      private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
      private System.Windows.Forms.ContextMenuStrip m_RegisterGridCtxMenu;
      private System.Windows.Forms.ToolStripMenuItem m_ShowDecValuesItem;
      private System.Windows.Forms.ToolStripMenuItem m_ShowHexValuesItem;
      private Assembler.FormsGui.Controls.BufferedDataGridView m_SrcGrid;
      private Assembler.FormsGui.Controls.BindableToolStripMenuItem m_ResumeBtn;
      private System.Windows.Forms.SplitContainer splitContainer1;
      private Assembler.FormsGui.Controls.BufferedDataGridView m_DataSegmentGrdView;
      private System.Windows.Forms.ContextMenuStrip m_DataGridCtxMenu;
      private System.Windows.Forms.ToolStripMenuItem m_ShowDataElemsAsDecimalBtn;
      private System.Windows.Forms.ToolStripMenuItem m_ShowDataElemsAsHexBtn;
      private System.Windows.Forms.DataGridViewTextBoxColumn BaseAddressStr;
      private System.Windows.Forms.DataGridViewTextBoxColumn Word0Str;
      private System.Windows.Forms.DataGridViewTextBoxColumn Word1Str;
      private System.Windows.Forms.DataGridViewTextBoxColumn Word2Str;
      private System.Windows.Forms.DataGridViewTextBoxColumn Word3Str;
      private System.Windows.Forms.DataGridViewCheckBoxColumn IsBreakpointApplied;
      private System.Windows.Forms.DataGridViewTextBoxColumn programCounterLocationStrDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn rawBytesStrDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn OriginalText;
      private System.Windows.Forms.DataGridViewTextBoxColumn instructionTextDataGridViewTextBoxColumn;
   }
}
