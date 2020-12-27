namespace Cherry.Desktop
{
    partial class SiennaSimpleContainer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.siennaSplit = new SiennaSplit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(93, 150);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(100, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 150);
            this.panel2.TabIndex = 2;
            // 
            // siennaSplit
            // 
            this.siennaSplit.BackColor = System.Drawing.Color.Sienna;
            this.siennaSplit.DisableColor = System.Drawing.SystemColors.Control;
            this.siennaSplit.Dock = System.Windows.Forms.DockStyle.Right;
            this.siennaSplit.Location = new System.Drawing.Point(93, 0);
            this.siennaSplit.Name = "siennaSplit";
            this.siennaSplit.Size = new System.Drawing.Size(7, 150);
            this.siennaSplit.TabIndex = 1;
            this.siennaSplit.TabStop = false;
            // 
            // SiennaContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.siennaSplit);
            this.Controls.Add(this.panel2);
            this.Name = "SiennaContainer";
            this.Size = new System.Drawing.Size(300, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SiennaSplit siennaSplit;
    }
}
