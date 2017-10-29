namespace Processing.UserControls
{
    partial class ProcessInfoControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.sSStatusBar = new System.Windows.Forms.StatusStrip();
            this.tSCPU = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSRAM = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.bProcessName = new System.Windows.Forms.Button();
            this.sSStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // sSStatusBar
            // 
            this.sSStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSCPU,
            this.tSRAM,
            this.tSPage});
            this.sSStatusBar.Location = new System.Drawing.Point(0, 28);
            this.sSStatusBar.Name = "sSStatusBar";
            this.sSStatusBar.ShowItemToolTips = true;
            this.sSStatusBar.Size = new System.Drawing.Size(250, 22);
            this.sSStatusBar.SizingGrip = false;
            this.sSStatusBar.TabIndex = 3;
            this.sSStatusBar.Text = "statusStrip";
            // 
            // tSCPU
            // 
            this.tSCPU.AutoSize = false;
            this.tSCPU.AutoToolTip = true;
            this.tSCPU.BackColor = System.Drawing.SystemColors.Control;
            this.tSCPU.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSCPU.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tSCPU.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tSCPU.Name = "tSCPU";
            this.tSCPU.Size = new System.Drawing.Size(40, 17);
            this.tSCPU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tSCPU.ToolTipText = "CPU Utilization";
            // 
            // tSRAM
            // 
            this.tSRAM.AutoSize = false;
            this.tSRAM.AutoToolTip = true;
            this.tSRAM.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSRAM.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tSRAM.Name = "tSRAM";
            this.tSRAM.Size = new System.Drawing.Size(65, 17);
            this.tSRAM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tSRAM.ToolTipText = "Commit Charge";
            // 
            // tSPage
            // 
            this.tSPage.AutoSize = false;
            this.tSPage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSPage.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tSPage.Name = "tSPage";
            this.tSPage.Size = new System.Drawing.Size(65, 17);
            this.tSPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tSPage.ToolTipText = "Page File Usage";
            // 
            // bProcessName
            // 
            this.bProcessName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bProcessName.Location = new System.Drawing.Point(0, 0);
            this.bProcessName.Name = "bProcessName";
            this.bProcessName.Size = new System.Drawing.Size(250, 28);
            this.bProcessName.TabIndex = 4;
            this.bProcessName.Text = "process";
            this.bProcessName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bProcessName.UseVisualStyleBackColor = true;
            // 
            // ProcessInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bProcessName);
            this.Controls.Add(this.sSStatusBar);
            this.Name = "ProcessInfoControl";
            this.Size = new System.Drawing.Size(250, 50);
            this.sSStatusBar.ResumeLayout(false);
            this.sSStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip sSStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel tSCPU;
        private System.Windows.Forms.ToolStripStatusLabel tSRAM;
        private System.Windows.Forms.ToolStripStatusLabel tSPage;
        private System.Windows.Forms.Button bProcessName;
    }
}
