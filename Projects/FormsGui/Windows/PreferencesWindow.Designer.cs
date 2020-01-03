namespace Assembler.FormsGui.Windows
{
   partial class PreferencesWindow
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.m_WindowLayout = new System.Windows.Forms.TableLayoutPanel();
         this.m_BtnLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
         this.m_CancelBtn = new System.Windows.Forms.Button();
         this.m_OkBtn = new System.Windows.Forms.Button();
         this.m_OptionsTabs = new System.Windows.Forms.TabControl();
         this.m_EditorOptionsPage = new System.Windows.Forms.TabPage();
         this.m_EditorOptionsLayout = new System.Windows.Forms.TableLayoutPanel();
         this.m_ShowLineNumsChkBox = new System.Windows.Forms.CheckBox();
         this.m_UseSpacesChkBox = new System.Windows.Forms.CheckBox();
         this.m_NumSpacesTxtBox = new System.Windows.Forms.TextBox();
         this.m_NumSpacesLbl = new System.Windows.Forms.Label();
         this.m_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         this.preferencesViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_WindowLayout.SuspendLayout();
         this.m_BtnLayoutPanel.SuspendLayout();
         this.m_OptionsTabs.SuspendLayout();
         this.m_EditorOptionsPage.SuspendLayout();
         this.m_EditorOptionsLayout.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.preferencesViewModelBindingSource)).BeginInit();
         this.SuspendLayout();
         // 
         // m_WindowLayout
         // 
         this.m_WindowLayout.ColumnCount = 1;
         this.m_WindowLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.m_WindowLayout.Controls.Add(this.m_BtnLayoutPanel, 0, 1);
         this.m_WindowLayout.Controls.Add(this.m_OptionsTabs, 0, 0);
         this.m_WindowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_WindowLayout.Location = new System.Drawing.Point(0, 0);
         this.m_WindowLayout.Name = "m_WindowLayout";
         this.m_WindowLayout.RowCount = 2;
         this.m_WindowLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.22222F));
         this.m_WindowLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.777778F));
         this.m_WindowLayout.Size = new System.Drawing.Size(320, 450);
         this.m_WindowLayout.TabIndex = 0;
         // 
         // m_BtnLayoutPanel
         // 
         this.m_BtnLayoutPanel.Controls.Add(this.m_CancelBtn);
         this.m_BtnLayoutPanel.Controls.Add(this.m_OkBtn);
         this.m_BtnLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_BtnLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
         this.m_BtnLayoutPanel.Location = new System.Drawing.Point(3, 417);
         this.m_BtnLayoutPanel.Name = "m_BtnLayoutPanel";
         this.m_BtnLayoutPanel.Size = new System.Drawing.Size(314, 30);
         this.m_BtnLayoutPanel.TabIndex = 0;
         // 
         // m_CancelBtn
         // 
         this.m_CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.m_CancelBtn.Location = new System.Drawing.Point(236, 3);
         this.m_CancelBtn.Name = "m_CancelBtn";
         this.m_CancelBtn.Size = new System.Drawing.Size(75, 23);
         this.m_CancelBtn.TabIndex = 0;
         this.m_CancelBtn.Text = "Cancel";
         this.m_CancelBtn.UseVisualStyleBackColor = true;
         // 
         // m_OkBtn
         // 
         this.m_OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.m_OkBtn.Location = new System.Drawing.Point(155, 3);
         this.m_OkBtn.Name = "m_OkBtn";
         this.m_OkBtn.Size = new System.Drawing.Size(75, 23);
         this.m_OkBtn.TabIndex = 1;
         this.m_OkBtn.Text = "OK";
         this.m_OkBtn.UseVisualStyleBackColor = true;
         // 
         // m_OptionsTabs
         // 
         this.m_OptionsTabs.Controls.Add(this.m_EditorOptionsPage);
         this.m_OptionsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_OptionsTabs.Location = new System.Drawing.Point(3, 3);
         this.m_OptionsTabs.Name = "m_OptionsTabs";
         this.m_OptionsTabs.SelectedIndex = 0;
         this.m_OptionsTabs.Size = new System.Drawing.Size(314, 408);
         this.m_OptionsTabs.TabIndex = 1;
         // 
         // m_EditorOptionsPage
         // 
         this.m_EditorOptionsPage.Controls.Add(this.m_EditorOptionsLayout);
         this.m_EditorOptionsPage.Location = new System.Drawing.Point(4, 22);
         this.m_EditorOptionsPage.Name = "m_EditorOptionsPage";
         this.m_EditorOptionsPage.Size = new System.Drawing.Size(306, 382);
         this.m_EditorOptionsPage.TabIndex = 0;
         this.m_EditorOptionsPage.Text = "Assembly Editor Options";
         this.m_EditorOptionsPage.UseVisualStyleBackColor = true;
         // 
         // m_EditorOptionsLayout
         // 
         this.m_EditorOptionsLayout.ColumnCount = 2;
         this.m_EditorOptionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.m_EditorOptionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.m_EditorOptionsLayout.Controls.Add(this.m_ShowLineNumsChkBox, 0, 0);
         this.m_EditorOptionsLayout.Controls.Add(this.m_UseSpacesChkBox, 0, 1);
         this.m_EditorOptionsLayout.Controls.Add(this.m_NumSpacesTxtBox, 1, 2);
         this.m_EditorOptionsLayout.Controls.Add(this.m_NumSpacesLbl, 0, 2);
         this.m_EditorOptionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_EditorOptionsLayout.Location = new System.Drawing.Point(0, 0);
         this.m_EditorOptionsLayout.Name = "m_EditorOptionsLayout";
         this.m_EditorOptionsLayout.RowCount = 3;
         this.m_EditorOptionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
         this.m_EditorOptionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
         this.m_EditorOptionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
         this.m_EditorOptionsLayout.Size = new System.Drawing.Size(306, 382);
         this.m_EditorOptionsLayout.TabIndex = 0;
         // 
         // m_ShowLineNumsChkBox
         // 
         this.m_ShowLineNumsChkBox.Anchor = System.Windows.Forms.AnchorStyles.None;
         this.m_ShowLineNumsChkBox.AutoSize = true;
         this.m_ShowLineNumsChkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.m_EditorOptionsLayout.SetColumnSpan(this.m_ShowLineNumsChkBox, 2);
         this.m_ShowLineNumsChkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.preferencesViewModelBindingSource, "ShowLineNumbers", true));
         this.m_ShowLineNumsChkBox.Location = new System.Drawing.Point(95, 55);
         this.m_ShowLineNumsChkBox.Name = "m_ShowLineNumsChkBox";
         this.m_ShowLineNumsChkBox.Size = new System.Drawing.Size(115, 17);
         this.m_ShowLineNumsChkBox.TabIndex = 0;
         this.m_ShowLineNumsChkBox.Text = "Show line numbers";
         this.m_ShowLineNumsChkBox.UseVisualStyleBackColor = true;
         // 
         // m_UseSpacesChkBox
         // 
         this.m_UseSpacesChkBox.Anchor = System.Windows.Forms.AnchorStyles.None;
         this.m_UseSpacesChkBox.AutoSize = true;
         this.m_UseSpacesChkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.m_EditorOptionsLayout.SetColumnSpan(this.m_UseSpacesChkBox, 2);
         this.m_UseSpacesChkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.preferencesViewModelBindingSource, "ReplaceTabsWithSpaces", true));
         this.m_UseSpacesChkBox.Location = new System.Drawing.Point(76, 182);
         this.m_UseSpacesChkBox.Name = "m_UseSpacesChkBox";
         this.m_UseSpacesChkBox.Size = new System.Drawing.Size(154, 17);
         this.m_UseSpacesChkBox.TabIndex = 1;
         this.m_UseSpacesChkBox.Text = "Use spaces instead of tabs";
         this.m_UseSpacesChkBox.UseVisualStyleBackColor = true;
         this.m_UseSpacesChkBox.CheckedChanged += new System.EventHandler(this.m_UseSpacesChkBox_CheckedChanged);
         // 
         // m_NumSpacesTxtBox
         // 
         this.m_NumSpacesTxtBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.m_NumSpacesTxtBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.preferencesViewModelBindingSource, "NumSpacesToReplaceTabWith", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.m_NumSpacesTxtBox.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.preferencesViewModelBindingSource, "ReplaceTabsWithSpaces", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.m_NumSpacesTxtBox.Location = new System.Drawing.Point(156, 308);
         this.m_NumSpacesTxtBox.Name = "m_NumSpacesTxtBox";
         this.m_NumSpacesTxtBox.Size = new System.Drawing.Size(74, 20);
         this.m_NumSpacesTxtBox.TabIndex = 2;
         this.m_NumSpacesTxtBox.Validating += new System.ComponentModel.CancelEventHandler(this.m_NumSpacesTxtBox_Validating);
         // 
         // m_NumSpacesLbl
         // 
         this.m_NumSpacesLbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
         this.m_NumSpacesLbl.AutoSize = true;
         this.m_NumSpacesLbl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.preferencesViewModelBindingSource, "ReplaceTabsWithSpaces", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.m_NumSpacesLbl.Location = new System.Drawing.Point(45, 305);
         this.m_NumSpacesLbl.Name = "m_NumSpacesLbl";
         this.m_NumSpacesLbl.Size = new System.Drawing.Size(105, 26);
         this.m_NumSpacesLbl.TabIndex = 3;
         this.m_NumSpacesLbl.Text = "Number of spaces to substitute tab with:";
         this.m_NumSpacesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // m_ErrorProvider
         // 
         this.m_ErrorProvider.ContainerControl = this;
         // 
         // preferencesViewModelBindingSource
         // 
         this.preferencesViewModelBindingSource.DataSource = typeof(Assembler.FormsGui.ViewModels.PreferencesViewModel);
         // 
         // PreferencesWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(320, 450);
         this.Controls.Add(this.m_WindowLayout);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "PreferencesWindow";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.Text = "Preferences";
         this.m_WindowLayout.ResumeLayout(false);
         this.m_BtnLayoutPanel.ResumeLayout(false);
         this.m_OptionsTabs.ResumeLayout(false);
         this.m_EditorOptionsPage.ResumeLayout(false);
         this.m_EditorOptionsLayout.ResumeLayout(false);
         this.m_EditorOptionsLayout.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.preferencesViewModelBindingSource)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel m_WindowLayout;
      private System.Windows.Forms.FlowLayoutPanel m_BtnLayoutPanel;
      private System.Windows.Forms.Button m_CancelBtn;
      private System.Windows.Forms.Button m_OkBtn;
      private System.Windows.Forms.TabControl m_OptionsTabs;
      private System.Windows.Forms.TabPage m_EditorOptionsPage;
      private System.Windows.Forms.TableLayoutPanel m_EditorOptionsLayout;
      private System.Windows.Forms.CheckBox m_ShowLineNumsChkBox;
      private System.Windows.Forms.CheckBox m_UseSpacesChkBox;
      private System.Windows.Forms.TextBox m_NumSpacesTxtBox;
      private System.Windows.Forms.Label m_NumSpacesLbl;
      private System.Windows.Forms.BindingSource preferencesViewModelBindingSource;
      private System.Windows.Forms.ErrorProvider m_ErrorProvider;
   }
}