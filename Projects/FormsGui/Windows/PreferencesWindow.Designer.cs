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
         this.m_WindowLayout = new System.Windows.Forms.TableLayoutPanel();
         this.m_BtnLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
         this.m_CancelBtn = new System.Windows.Forms.Button();
         this.m_OkBtn = new System.Windows.Forms.Button();
         this.m_OptionsTabs = new System.Windows.Forms.TabControl();
         this.m_WindowLayout.SuspendLayout();
         this.m_BtnLayoutPanel.SuspendLayout();
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
         this.m_WindowLayout.Size = new System.Drawing.Size(800, 450);
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
         this.m_BtnLayoutPanel.Size = new System.Drawing.Size(794, 30);
         this.m_BtnLayoutPanel.TabIndex = 0;
         // 
         // m_CancelBtn
         // 
         this.m_CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.m_CancelBtn.Location = new System.Drawing.Point(716, 3);
         this.m_CancelBtn.Name = "m_CancelBtn";
         this.m_CancelBtn.Size = new System.Drawing.Size(75, 23);
         this.m_CancelBtn.TabIndex = 0;
         this.m_CancelBtn.Text = "Cancel";
         this.m_CancelBtn.UseVisualStyleBackColor = true;
         // 
         // m_OkBtn
         // 
         this.m_OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.m_OkBtn.Location = new System.Drawing.Point(635, 3);
         this.m_OkBtn.Name = "m_OkBtn";
         this.m_OkBtn.Size = new System.Drawing.Size(75, 23);
         this.m_OkBtn.TabIndex = 1;
         this.m_OkBtn.Text = "OK";
         this.m_OkBtn.UseVisualStyleBackColor = true;
         // 
         // m_OptionsTabs
         // 
         this.m_OptionsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_OptionsTabs.Location = new System.Drawing.Point(3, 3);
         this.m_OptionsTabs.Name = "m_OptionsTabs";
         this.m_OptionsTabs.SelectedIndex = 0;
         this.m_OptionsTabs.Size = new System.Drawing.Size(794, 408);
         this.m_OptionsTabs.TabIndex = 1;
         // 
         // PreferencesWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
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
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel m_WindowLayout;
      private System.Windows.Forms.FlowLayoutPanel m_BtnLayoutPanel;
      private System.Windows.Forms.Button m_CancelBtn;
      private System.Windows.Forms.Button m_OkBtn;
      private System.Windows.Forms.TabControl m_OptionsTabs;
   }
}