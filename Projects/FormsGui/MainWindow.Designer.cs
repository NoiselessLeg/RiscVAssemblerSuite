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
         this.m_TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_TabCtrl.Location = new System.Drawing.Point(0, 24);
         this.m_TabCtrl.Name = "m_TabCtrl";
         this.m_TabCtrl.SelectedIndex = 0;
         this.m_TabCtrl.Size = new System.Drawing.Size(800, 426);
         this.m_TabCtrl.TabIndex = 1;
         this.m_TabCtrl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnTabSelection);
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
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip m_MenuStrip;
      private System.Windows.Forms.TabControl m_TabCtrl;
   }
}

