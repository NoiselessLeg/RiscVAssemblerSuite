namespace Assembler.FormsGui
{
   partial class MainWindow
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
         this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
         this.m_TabCtrl = new System.Windows.Forms.TabControl();
         this.m_EditorTabPage = new System.Windows.Forms.TabPage();
         this.m_DbgTabPage = new System.Windows.Forms.TabPage();
         this.m_EditorView = new Assembler.FormsGui.Views.EditorView();
         this.m_DbgView = new Assembler.FormsGui.Views.DebugView();
         this.m_HexExplorerTabPage = new System.Windows.Forms.TabPage();
         this.m_HexExplorerView = new Assembler.FormsGui.Views.HexExplorerView();
         this.m_TabCtrl.SuspendLayout();
         this.m_EditorTabPage.SuspendLayout();
         this.m_DbgTabPage.SuspendLayout();
         this.m_HexExplorerTabPage.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_MenuStrip
         // 
         this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
         this.m_MenuStrip.Name = "m_MenuStrip";
         this.m_MenuStrip.Size = new System.Drawing.Size(800, 24);
         this.m_MenuStrip.TabIndex = 0;
         this.m_MenuStrip.Text = "menuStrip1";
         // 
         // m_TabCtrl
         // 
         this.m_TabCtrl.Controls.Add(this.m_EditorTabPage);
         this.m_TabCtrl.Controls.Add(this.m_DbgTabPage);
         this.m_TabCtrl.Controls.Add(this.m_HexExplorerTabPage);
         this.m_TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_TabCtrl.Location = new System.Drawing.Point(0, 24);
         this.m_TabCtrl.Name = "m_TabCtrl";
         this.m_TabCtrl.SelectedIndex = 0;
         this.m_TabCtrl.Size = new System.Drawing.Size(800, 426);
         this.m_TabCtrl.TabIndex = 1;
         this.m_TabCtrl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnTabSelection);
         // 
         // m_EditorTabPage
         // 
         this.m_EditorTabPage.Controls.Add(this.m_EditorView);
         this.m_EditorTabPage.Location = new System.Drawing.Point(4, 22);
         this.m_EditorTabPage.Name = "m_EditorTabPage";
         this.m_EditorTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.m_EditorTabPage.Size = new System.Drawing.Size(792, 400);
         this.m_EditorTabPage.TabIndex = 0;
         this.m_EditorTabPage.Text = "Editor";
         this.m_EditorTabPage.UseVisualStyleBackColor = true;
         // 
         // m_DbgTabPage
         // 
         this.m_DbgTabPage.Controls.Add(this.m_DbgView);
         this.m_DbgTabPage.Location = new System.Drawing.Point(4, 22);
         this.m_DbgTabPage.Name = "m_DbgTabPage";
         this.m_DbgTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.m_DbgTabPage.Size = new System.Drawing.Size(792, 400);
         this.m_DbgTabPage.TabIndex = 1;
         this.m_DbgTabPage.Text = "Debug";
         this.m_DbgTabPage.UseVisualStyleBackColor = true;
         // 
         // m_EditorView
         // 
         this.m_EditorView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_EditorView.Location = new System.Drawing.Point(3, 3);
         this.m_EditorView.Name = "m_EditorView";
         this.m_EditorView.Size = new System.Drawing.Size(786, 394);
         this.m_EditorView.TabIndex = 0;
         // 
         // m_DbgView
         // 
         this.m_DbgView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_DbgView.Location = new System.Drawing.Point(3, 3);
         this.m_DbgView.Name = "m_DbgView";
         this.m_DbgView.Size = new System.Drawing.Size(786, 394);
         this.m_DbgView.TabIndex = 0;
         // 
         // m_HexExplorerTabPage
         // 
         this.m_HexExplorerTabPage.Controls.Add(this.m_HexExplorerView);
         this.m_HexExplorerTabPage.Location = new System.Drawing.Point(4, 22);
         this.m_HexExplorerTabPage.Name = "m_HexExplorerTabPage";
         this.m_HexExplorerTabPage.Size = new System.Drawing.Size(792, 400);
         this.m_HexExplorerTabPage.TabIndex = 2;
         this.m_HexExplorerTabPage.Text = "Hex Explorer";
         this.m_HexExplorerTabPage.UseVisualStyleBackColor = true;
         // 
         // m_HexExplorerView
         // 
         this.m_HexExplorerView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_HexExplorerView.Location = new System.Drawing.Point(0, 0);
         this.m_HexExplorerView.Name = "m_HexExplorerView";
         this.m_HexExplorerView.Size = new System.Drawing.Size(792, 400);
         this.m_HexExplorerView.TabIndex = 0;
         // 
         // MainWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.m_TabCtrl);
         this.Controls.Add(this.m_MenuStrip);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.m_MenuStrip;
         this.Name = "MainWindow";
         this.Text = "rASM";
         this.m_TabCtrl.ResumeLayout(false);
         this.m_EditorTabPage.ResumeLayout(false);
         this.m_DbgTabPage.ResumeLayout(false);
         this.m_HexExplorerTabPage.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip m_MenuStrip;
      private System.Windows.Forms.TabControl m_TabCtrl;
      private System.Windows.Forms.TabPage m_EditorTabPage;
      private System.Windows.Forms.TabPage m_DbgTabPage;
      private Views.DebugView m_DbgView;
      private Views.EditorView m_EditorView;
      private System.Windows.Forms.TabPage m_HexExplorerTabPage;
      private Views.HexExplorerView m_HexExplorerView;
   }
}

