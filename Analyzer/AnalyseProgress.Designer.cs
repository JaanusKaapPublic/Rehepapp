namespace Analyzer
{
    partial class AnalyseProgress
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
            this.currentFile = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // currentFile
            // 
            this.currentFile.AutoSize = true;
            this.currentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.currentFile.Location = new System.Drawing.Point(190, 12);
            this.currentFile.Name = "currentFile";
            this.currentFile.Size = new System.Drawing.Size(60, 24);
            this.currentFile.TabIndex = 5;
            this.currentFile.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Currently analysing:";
            // 
            // progress
            // 
            this.progress.AutoSize = true;
            this.progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.progress.Location = new System.Drawing.Point(315, 46);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(170, 31);
            this.progress.TabIndex = 6;
            this.progress.Text = "ANALYSING";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(16, 98);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(765, 454);
            this.txtLog.TabIndex = 7;
            // 
            // AnalyseProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 573);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.currentFile);
            this.Controls.Add(this.label5);
            this.Name = "AnalyseProgress";
            this.Text = "AnalyseProgress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label currentFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label progress;
        private System.Windows.Forms.TextBox txtLog;
    }
}