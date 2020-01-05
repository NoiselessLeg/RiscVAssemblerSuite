namespace Assembler.FormsGui
{
   partial class DisplayPanel
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
         this.m_TabCtrl = new System.Windows.Forms.TabControl();
         this.m_EditorPage = new System.Windows.Forms.TabPage();
         this.m_ExecutionPage = new System.Windows.Forms.TabPage();
         this.m_HexViewPage = new System.Windows.Forms.TabPage();
         this.m_TabCtrl.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_TabCtrl
         // 
         this.m_TabCtrl.Controls.Add(this.m_EditorPage);
         this.m_TabCtrl.Controls.Add(this.m_ExecutionPage);
         this.m_TabCtrl.Controls.Add(this.m_HexViewPage);
         this.m_TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_TabCtrl.Location = new System.Drawing.Point(0, 0);
         this.m_TabCtrl.Name = "m_TabCtrl";
         this.m_TabCtrl.SelectedIndex = 0;
         this.m_TabCtrl.Size = new System.Drawing.Size(606, 436);
         this.m_TabCtrl.TabIndex = 2;
         this.m_TabCtrl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnTabSelection);
         // 
         // m_EditorPage
         // 
         this.m_EditorPage.Location = new System.Drawing.Point(4, 22);
         this.m_EditorPage.Name = "m_EditorPage";
         this.m_EditorPage.Size = new System.Drawing.Size(598, 410);
         this.m_EditorPage.TabIndex = 0;
         this.m_EditorPage.Text = "Editor View";
         this.m_EditorPage.UseVisualStyleBackColor = true;
         // 
         // m_ExecutionPage
         // 
         this.m_ExecutionPage.Location = new System.Drawing.Point(4, 22);
         this.m_ExecutionPage.Name = "m_ExecutionPage";
         this.m_ExecutionPage.Size = new System.Drawing.Size(598, 410);
         this.m_ExecutionPage.TabIndex = 1;
         this.m_ExecutionPage.Text = "Execution View";
         this.m_ExecutionPage.UseVisualStyleBackColor = true;
         // 
         // m_HexViewPage
         // 
         this.m_HexViewPage.Location = new System.Drawing.Point(4, 22);
         this.m_HexViewPage.Name = "m_HexViewPage";
         this.m_HexViewPage.Size = new System.Drawing.Size(598, 410);
         this.m_HexViewPage.TabIndex = 2;
         this.m_HexViewPage.Text = "Hex Explorer";
         this.m_HexViewPage.UseVisualStyleBackColor = true;
         // 
         // DisplayPanel
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_TabCtrl);
         this.Name = "DisplayPanel";
         this.Size = new System.Drawing.Size(606, 436);
         this.m_TabCtrl.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TabControl m_TabCtrl;
      private System.Windows.Forms.TabPage m_EditorPage;
      private System.Windows.Forms.TabPage m_ExecutionPage;
      private System.Windows.Forms.TabPage m_HexViewPage;
   }
}
