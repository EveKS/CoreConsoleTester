namespace Processing
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sSStatusBar = new System.Windows.Forms.StatusStrip();
            this.tSCPU = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSRAM = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tTHints = new System.Windows.Forms.ToolTip(this.components);
            this.fLPMainPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.sSStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // sSStatusBar
            // 
            this.sSStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSCPU,
            this.tSRAM,
            this.tSPage});
            this.sSStatusBar.Location = new System.Drawing.Point(0, 355);
            this.sSStatusBar.Name = "sSStatusBar";
            this.sSStatusBar.ShowItemToolTips = true;
            this.sSStatusBar.Size = new System.Drawing.Size(284, 22);
            this.sSStatusBar.SizingGrip = false;
            this.sSStatusBar.TabIndex = 2;
            this.sSStatusBar.Text = "statusStrip1";
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
            this.tSPage.Size = new System.Drawing.Size(40, 17);
            this.tSPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tSPage.ToolTipText = "Page File Usage";
            // 
            // tTHints
            // 
            this.tTHints.ShowAlways = true;
            // 
            // fLPMainPanel
            // 
            this.fLPMainPanel.AutoScroll = true;
            this.fLPMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLPMainPanel.Location = new System.Drawing.Point(0, 0);
            this.fLPMainPanel.Name = "fLPMainPanel";
            this.fLPMainPanel.Size = new System.Drawing.Size(284, 355);
            this.fLPMainPanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 377);
            this.Controls.Add(this.fLPMainPanel);
            this.Controls.Add(this.sSStatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Connecting...";
            this.TopMost = true;
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
        private System.Windows.Forms.ToolTip tTHints;
        private System.Windows.Forms.FlowLayoutPanel fLPMainPanel;
    }
}

