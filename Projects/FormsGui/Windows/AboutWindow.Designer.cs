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
         this.label3 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.m_VersionLbl = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.m_LogoBox)).BeginInit();
         this.tableLayoutPanel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_LogoBox
         // 
         this.m_LogoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.m_LogoBox.Image = ((System.Drawing.Image)(resources.GetObject("m_LogoBox.Image")));
         this.m_LogoBox.Location = new System.Drawing.Point(292, 3);
         this.m_LogoBox.Name = "m_LogoBox";
         this.tableLayoutPanel1.SetRowSpan(this.m_LogoBox, 4);
         this.m_LogoBox.Size = new System.Drawing.Size(128, 128);
         this.m_LogoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
         this.m_LogoBox.TabIndex = 0;
         this.m_LogoBox.TabStop = false;
         // 
         // label1
         // 
         this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(3, 12);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(258, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "RISC-V Assembler, Disassembler, and Simulator Suite";
         // 
         // m_OkBtn
         // 
         this.m_OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.m_OkBtn.Location = new System.Drawing.Point(345, 275);
         this.m_OkBtn.Name = "m_OkBtn";
         this.m_OkBtn.Size = new System.Drawing.Size(75, 23);
         this.m_OkBtn.TabIndex = 2;
         this.m_OkBtn.Text = "OK";
         this.m_OkBtn.UseVisualStyleBackColor = true;
         this.m_OkBtn.Click += new System.EventHandler(this.m_OkBtn_Click);
         // 
         // label3
         // 
         this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(3, 49);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(263, 13);
         this.label3.TabIndex = 4;
         this.label3.Text = "Developed by John Lewis, released under MIT license";
         // 
         // label4
         // 
         this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(3, 116);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(268, 26);
         this.label4.TabIndex = 5;
         this.label4.Text = "Assembly text editor XSHD file based on Assembly.xshd\r\nwritten by Ezra Altahan";
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.ColumnCount = 2;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
         this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
         this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
         this.tableLayoutPanel1.Controls.Add(this.m_LogoBox, 1, 0);
         this.tableLayoutPanel1.Controls.Add(this.m_VersionLbl, 0, 2);
         this.tableLayoutPanel1.Controls.Add(this.m_OkBtn, 1, 7);
         this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
         this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
         this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
         this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 8;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(423, 301);
         this.tableLayoutPanel1.TabIndex = 7;
         // 
         // m_VersionLbl
         // 
         this.m_VersionLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.m_VersionLbl.AutoSize = true;
         this.m_VersionLbl.Location = new System.Drawing.Point(3, 86);
         this.m_VersionLbl.Name = "m_VersionLbl";
         this.m_VersionLbl.Size = new System.Drawing.Size(97, 13);
         this.m_VersionLbl.TabIndex = 6;
         this.m_VersionLbl.Text = "GUI Version: X.X.X";
         // 
         // label2
         // 
         this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.label2.AutoSize = true;
         this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
         this.label2.Location = new System.Drawing.Point(3, 153);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(413, 26);
         this.label2.TabIndex = 7;
         this.label2.Text = "Assembly text editor: ICSharpCode.TextEditor - originally developed by SharpDevel" +
    "op; \r\nreleased under GPL";
         // 
         // label5
         // 
         this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.label5.AutoSize = true;
         this.tableLayoutPanel1.SetColumnSpan(this.label5, 2);
         this.label5.Location = new System.Drawing.Point(3, 197);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(403, 13);
         this.label5.TabIndex = 8;
         this.label5.Text = "Hex editor control developed originally by  Bernhard Elbl; released under MIT lic" +
    "ense";
         // 
         // AboutWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(423, 301);
         this.Controls.Add(this.tableLayoutPanel1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "AboutWindow";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "About";
         ((System.ComponentModel.ISupportInitialize)(this.m_LogoBox)).EndInit();
         this.tableLayoutPanel1.ResumeLayout(false);
         this.tableLayoutPanel1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.PictureBox m_LogoBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button m_OkBtn;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.Label m_VersionLbl;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label5;
   }
}