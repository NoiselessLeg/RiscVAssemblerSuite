namespace Assembler.PrototypeGui
{
    partial class AssemblyEditorTabPage
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
            this.m_AsmBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_AsmBox
            // 
            this.m_AsmBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_AsmBox.Location = new System.Drawing.Point(0, 0);
            this.m_AsmBox.Multiline = true;
            this.m_AsmBox.Name = "m_AsmBox";
            this.m_AsmBox.Size = new System.Drawing.Size(567, 468);
            this.m_AsmBox.TabIndex = 0;
            this.m_AsmBox.TextChanged += new System.EventHandler(this.m_AsmBox_TextChanged);
            // 
            // AssemblyEditorTabPage
            // 
            this.Controls.Add(this.m_AsmBox);
            this.Name = "AssemblyEditorTabPage";
            this.Size = new System.Drawing.Size(567, 468);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TextBox m_AsmBox;
    }
}
