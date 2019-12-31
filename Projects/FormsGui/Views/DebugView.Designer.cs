namespace Assembler.FormsGui.Views
{
   partial class DebugView
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
         this.m_OpenFilesTabCtrl = new System.Windows.Forms.TabControl();
         this.SuspendLayout();
         // 
         // m_OpenFilesTabCtrl
         // 
         this.m_OpenFilesTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_OpenFilesTabCtrl.Location = new System.Drawing.Point(0, 0);
         this.m_OpenFilesTabCtrl.Name = "m_OpenFilesTabCtrl";
         this.m_OpenFilesTabCtrl.SelectedIndex = 0;
         this.m_OpenFilesTabCtrl.Size = new System.Drawing.Size(469, 388);
         this.m_OpenFilesTabCtrl.TabIndex = 0;
         // 
         // DebugView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_OpenFilesTabCtrl);
         this.Name = "DebugView";
         this.Size = new System.Drawing.Size(469, 388);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TabControl m_OpenFilesTabCtrl;
   }
}
