﻿namespace Assembler.FormsGui.Controls
{
   partial class FileExecutionTabPage
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

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
         this.m_RegisterGridCtxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.m_ShowDecValuesItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_ShowHexValuesItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_HorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
         this.m_TopHalfSplit = new System.Windows.Forms.SplitContainer();
         this.m_RegTabCtrl = new System.Windows.Forms.TabControl();
         this.m_UserRegTabPage = new System.Windows.Forms.TabPage();
         this.m_FltPtRegTabPage = new System.Windows.Forms.TabPage();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.m_DataGridCtxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.m_ShowDataElemsAsDecimalBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.m_ShowDataElemsAsHexBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.m_StartBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.dataSegmentBindingSrc = new System.Windows.Forms.BindingSource(this.components);
         this.m_SrcGrid = new Assembler.FormsGui.Controls.BufferedDataGridView();
         this.IsBreakpointApplied = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.programCounterLocationStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.instructionTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.rawBytesStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.SourceLineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.OriginalText = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.jefFileViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_UsrRegisterData = new Assembler.FormsGui.Controls.BufferedDataGridView();
         this.RegName = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.usrRegisterBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_FpRegisterData = new Assembler.FormsGui.Controls.BufferedDataGridView();
         this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.fpRegBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_DataSegmentGrdView = new Assembler.FormsGui.Controls.BufferedDataGridView();
         this.BaseAddressStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word0Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word1Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word2Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Word3Str = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.m_ConsoleTxt = new Assembler.FormsGui.Controls.ConsoleTextBox();
         this.m_PauseBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.m_ResumeBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.m_StepBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.m_TerminateBtn = new Assembler.FormsGui.Controls.BindableToolStripMenuItem();
         this.m_RegisterGridCtxMenu.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).BeginInit();
         this.m_HorizontalSplitContainer.Panel1.SuspendLayout();
         this.m_HorizontalSplitContainer.Panel2.SuspendLayout();
         this.m_HorizontalSplitContainer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).BeginInit();
         this.m_TopHalfSplit.Panel1.SuspendLayout();
         this.m_TopHalfSplit.Panel2.SuspendLayout();
         this.m_TopHalfSplit.SuspendLayout();
         this.m_RegTabCtrl.SuspendLayout();
         this.m_UserRegTabPage.SuspendLayout();
         this.m_FltPtRegTabPage.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.m_DataGridCtxMenu.SuspendLayout();
         this.menuStrip1.SuspendLayout();
         this.tableLayoutPanel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSegmentBindingSrc)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_SrcGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.jefFileViewModelBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_UsrRegisterData)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.usrRegisterBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_FpRegisterData)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.fpRegBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_DataSegmentGrdView)).BeginInit();
         this.SuspendLayout();
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
         this.m_TopHalfSplit.Panel2.Controls.Add(this.m_RegTabCtrl);
         this.m_TopHalfSplit.Size = new System.Drawing.Size(515, 152);
         this.m_TopHalfSplit.SplitterDistance = 257;
         this.m_TopHalfSplit.TabIndex = 1;
         // 
         // m_RegTabCtrl
         // 
         this.m_RegTabCtrl.Controls.Add(this.m_UserRegTabPage);
         this.m_RegTabCtrl.Controls.Add(this.m_FltPtRegTabPage);
         this.m_RegTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_RegTabCtrl.Location = new System.Drawing.Point(0, 0);
         this.m_RegTabCtrl.Name = "m_RegTabCtrl";
         this.m_RegTabCtrl.SelectedIndex = 0;
         this.m_RegTabCtrl.Size = new System.Drawing.Size(254, 152);
         this.m_RegTabCtrl.TabIndex = 1;
         // 
         // m_UserRegTabPage
         // 
         this.m_UserRegTabPage.Controls.Add(this.m_UsrRegisterData);
         this.m_UserRegTabPage.Location = new System.Drawing.Point(4, 22);
         this.m_UserRegTabPage.Name = "m_UserRegTabPage";
         this.m_UserRegTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.m_UserRegTabPage.Size = new System.Drawing.Size(246, 126);
         this.m_UserRegTabPage.TabIndex = 0;
         this.m_UserRegTabPage.Text = "User Registers";
         this.m_UserRegTabPage.UseVisualStyleBackColor = true;
         // 
         // m_FltPtRegTabPage
         // 
         this.m_FltPtRegTabPage.Controls.Add(this.m_FpRegisterData);
         this.m_FltPtRegTabPage.Location = new System.Drawing.Point(4, 22);
         this.m_FltPtRegTabPage.Name = "m_FltPtRegTabPage";
         this.m_FltPtRegTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.m_FltPtRegTabPage.Size = new System.Drawing.Size(246, 126);
         this.m_FltPtRegTabPage.TabIndex = 1;
         this.m_FltPtRegTabPage.Text = "Floating Point Registers";
         this.m_FltPtRegTabPage.UseVisualStyleBackColor = true;
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
            this.instructionTextDataGridViewTextBoxColumn,
            this.rawBytesStrDataGridViewTextBoxColumn,
            this.SourceLineNumber,
            this.OriginalText});
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
         // instructionTextDataGridViewTextBoxColumn
         // 
         this.instructionTextDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.instructionTextDataGridViewTextBoxColumn.DataPropertyName = "InstructionText";
         this.instructionTextDataGridViewTextBoxColumn.HeaderText = "Synthesized Instruction";
         this.instructionTextDataGridViewTextBoxColumn.Name = "instructionTextDataGridViewTextBoxColumn";
         this.instructionTextDataGridViewTextBoxColumn.ReadOnly = true;
         this.instructionTextDataGridViewTextBoxColumn.Width = 129;
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
         // SourceLineNumber
         // 
         this.SourceLineNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.SourceLineNumber.DataPropertyName = "SourceLineNumber";
         this.SourceLineNumber.HeaderText = "Source Line Number";
         this.SourceLineNumber.Name = "SourceLineNumber";
         this.SourceLineNumber.ReadOnly = true;
         this.SourceLineNumber.Width = 118;
         // 
         // OriginalText
         // 
         this.OriginalText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         this.OriginalText.DataPropertyName = "OriginalInstructionSourceText";
         this.OriginalText.HeaderText = "Source Line";
         this.OriginalText.Name = "OriginalText";
         this.OriginalText.ReadOnly = true;
         // 
         // jefFileViewModelBindingSource
         // 
         this.jefFileViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.DisassembledFileViewModel);
         // 
         // m_UsrRegisterData
         // 
         this.m_UsrRegisterData.AllowUserToAddRows = false;
         this.m_UsrRegisterData.AllowUserToDeleteRows = false;
         this.m_UsrRegisterData.AutoGenerateColumns = false;
         this.m_UsrRegisterData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.m_UsrRegisterData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.m_UsrRegisterData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RegName,
            this.valueDataGridViewTextBoxColumn});
         this.m_UsrRegisterData.ContextMenuStrip = this.m_RegisterGridCtxMenu;
         this.m_UsrRegisterData.DataSource = this.usrRegisterBindingSource;
         this.m_UsrRegisterData.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_UsrRegisterData.Location = new System.Drawing.Point(3, 3);
         this.m_UsrRegisterData.MultiSelect = false;
         this.m_UsrRegisterData.Name = "m_UsrRegisterData";
         this.m_UsrRegisterData.RowHeadersVisible = false;
         this.m_UsrRegisterData.Size = new System.Drawing.Size(240, 120);
         this.m_UsrRegisterData.TabIndex = 0;
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
         // usrRegisterBindingSource
         // 
         this.usrRegisterBindingSource.DataMember = "Registers";
         this.usrRegisterBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.ExecutionViewModel);
         // 
         // m_FpRegisterData
         // 
         this.m_FpRegisterData.AllowUserToAddRows = false;
         this.m_FpRegisterData.AllowUserToDeleteRows = false;
         this.m_FpRegisterData.AutoGenerateColumns = false;
         this.m_FpRegisterData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.m_FpRegisterData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.m_FpRegisterData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
         this.m_FpRegisterData.ContextMenuStrip = this.m_RegisterGridCtxMenu;
         this.m_FpRegisterData.DataSource = this.fpRegBindingSource;
         this.m_FpRegisterData.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_FpRegisterData.Location = new System.Drawing.Point(3, 3);
         this.m_FpRegisterData.MultiSelect = false;
         this.m_FpRegisterData.Name = "m_FpRegisterData";
         this.m_FpRegisterData.RowHeadersVisible = false;
         this.m_FpRegisterData.Size = new System.Drawing.Size(240, 120);
         this.m_FpRegisterData.TabIndex = 1;
         // 
         // dataGridViewTextBoxColumn1
         // 
         this.dataGridViewTextBoxColumn1.DataPropertyName = "RegisterName";
         this.dataGridViewTextBoxColumn1.HeaderText = "Register Name";
         this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
         this.dataGridViewTextBoxColumn1.ReadOnly = true;
         // 
         // dataGridViewTextBoxColumn2
         // 
         this.dataGridViewTextBoxColumn2.DataPropertyName = "ValueStr";
         this.dataGridViewTextBoxColumn2.HeaderText = "Value";
         this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
         // 
         // fpRegBindingSource
         // 
         this.fpRegBindingSource.DataMember = "Registers";
         this.fpRegBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.ExecutionViewModel);
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
         // m_ConsoleTxt
         // 
         this.m_ConsoleTxt.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_ConsoleTxt.Location = new System.Drawing.Point(0, 0);
         this.m_ConsoleTxt.Name = "m_ConsoleTxt";
         this.m_ConsoleTxt.Size = new System.Drawing.Size(515, 131);
         this.m_ConsoleTxt.TabIndex = 0;
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
         // FileExecutionTabPage
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.tableLayoutPanel1);
         this.Name = "FileExecutionTabPage";
         this.Size = new System.Drawing.Size(521, 435);
         this.VisibleChanged += new System.EventHandler(this.FileExecutionTabPage_VisibleChanged);
         this.m_RegisterGridCtxMenu.ResumeLayout(false);
         this.m_HorizontalSplitContainer.Panel1.ResumeLayout(false);
         this.m_HorizontalSplitContainer.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_HorizontalSplitContainer)).EndInit();
         this.m_HorizontalSplitContainer.ResumeLayout(false);
         this.m_TopHalfSplit.Panel1.ResumeLayout(false);
         this.m_TopHalfSplit.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_TopHalfSplit)).EndInit();
         this.m_TopHalfSplit.ResumeLayout(false);
         this.m_RegTabCtrl.ResumeLayout(false);
         this.m_UserRegTabPage.ResumeLayout(false);
         this.m_FltPtRegTabPage.ResumeLayout(false);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.m_DataGridCtxMenu.ResumeLayout(false);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.tableLayoutPanel1.ResumeLayout(false);
         this.tableLayoutPanel1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSegmentBindingSrc)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_SrcGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.jefFileViewModelBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_UsrRegisterData)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.usrRegisterBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_FpRegisterData)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.fpRegBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_DataSegmentGrdView)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion
      private System.Windows.Forms.SplitContainer m_TopHalfSplit;
      private Assembler.FormsGui.Controls.BufferedDataGridView m_UsrRegisterData;
      private System.Windows.Forms.SplitContainer m_HorizontalSplitContainer;
      private System.Windows.Forms.BindingSource usrRegisterBindingSource;
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
      private System.Windows.Forms.DataGridViewTextBoxColumn instructionTextDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn rawBytesStrDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn SourceLineNumber;
      private System.Windows.Forms.DataGridViewTextBoxColumn OriginalText;
      private System.Windows.Forms.TabControl m_RegTabCtrl;
      private System.Windows.Forms.TabPage m_UserRegTabPage;
      private System.Windows.Forms.TabPage m_FltPtRegTabPage;
      private BufferedDataGridView m_FpRegisterData;
      private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
      private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
      private System.Windows.Forms.BindingSource fpRegBindingSource;
      private System.Windows.Forms.BindingSource dataSegmentBindingSrc;
   }
}
