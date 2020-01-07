namespace Assembler.FormsGui.Windows
{
   partial class AboutWindow
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
         this.m_LogoBox = new System.Windows.Forms.PictureBox();
         this.label1 = new System.Windows.Forms.Label();
         this.m_OkBtn = new System.Windows.Forms.Button();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.m_LogoBox)).BeginInit();
         this.SuspendLayout();
         // 
         // m_LogoBox
         // 
         this.m_LogoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.m_LogoBox.Image = ((System.Drawing.Image)(resources.GetObject("m_LogoBox.Image")));
         this.m_LogoBox.Location = new System.Drawing.Point(283, 12);
         this.m_LogoBox.Name = "m_LogoBox";
         this.m_LogoBox.Size = new System.Drawing.Size(128, 128);
         this.m_LogoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
         this.m_LogoBox.TabIndex = 0;
         this.m_LogoBox.TabStop = false;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 12);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(258, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "RISC-V Assembler, Disassembler, and Simulator Suite";
         // 
         // m_OkBtn
         // 
         this.m_OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.m_OkBtn.Location = new System.Drawing.Point(336, 266);
         this.m_OkBtn.Name = "m_OkBtn";
         this.m_OkBtn.Size = new System.Drawing.Size(75, 23);
         this.m_OkBtn.TabIndex = 2;
         this.m_OkBtn.Text = "OK";
         this.m_OkBtn.UseVisualStyleBackColor = true;
         this.m_OkBtn.Click += new System.EventHandler(this.m_OkBtn_Click);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(12, 87);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(70, 13);
         this.label2.TabIndex = 3;
         this.label2.Text = "GUI Version: ";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(12, 48);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(263, 13);
         this.label3.TabIndex = 4;
         this.label3.Text = "Developed by John Lewis, released under MIT license";
         // 
         // AboutWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(423, 301);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.m_OkBtn);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.m_LogoBox);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "AboutWindow";
         this.Text = "About";
         ((System.ComponentModel.ISupportInitialize)(this.m_LogoBox)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox m_LogoBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button m_OkBtn;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
   }
}