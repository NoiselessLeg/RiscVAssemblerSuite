﻿namespace Assembler.FormsGui.Views
{
   partial class HexExplorerView
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
         this.m_FileTabCtrl = new System.Windows.Forms.TabControl();
         this.SuspendLayout();
         // 
         // m_FileTabCtrl
         // 
         this.m_FileTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_FileTabCtrl.Location = new System.Drawing.Point(0, 0);
         this.m_FileTabCtrl.Name = "m_FileTabCtrl";
         this.m_FileTabCtrl.SelectedIndex = 0;
         this.m_FileTabCtrl.Size = new System.Drawing.Size(441, 320);
         this.m_FileTabCtrl.TabIndex = 0;
         // 
         // HexExplorerView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_FileTabCtrl);
         this.Name = "HexExplorerView";
         this.Size = new System.Drawing.Size(441, 320);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TabControl m_FileTabCtrl;
   }
}
