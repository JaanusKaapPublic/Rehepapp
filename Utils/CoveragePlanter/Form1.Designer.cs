namespace CoveragePlanter
{
    partial class Form1
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnBBL = new System.Windows.Forms.Button();
            this.txtBBL = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnDir = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.lblDir = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 122);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(373, 142);
            this.txtLog.TabIndex = 17;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(179, 101);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(41, 17);
            this.lblLog.TabIndex = 16;
            this.lblLog.Text = "LOG";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 75);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(373, 23);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnBBL
            // 
            this.btnBBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBBL.Location = new System.Drawing.Point(347, 12);
            this.btnBBL.Name = "btnBBL";
            this.btnBBL.Size = new System.Drawing.Size(38, 20);
            this.btnBBL.TabIndex = 14;
            this.btnBBL.Text = "...";
            this.btnBBL.UseVisualStyleBackColor = true;
            this.btnBBL.Click += new System.EventHandler(this.BtnBBL_Click);
            // 
            // txtBBL
            // 
            this.txtBBL.Location = new System.Drawing.Point(77, 12);
            this.txtBBL.Name = "txtBBL";
            this.txtBBL.Size = new System.Drawing.Size(263, 20);
            this.txtBBL.TabIndex = 13;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(9, 12);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(69, 17);
            this.lblOutput.TabIndex = 12;
            this.lblOutput.Text = "BBL file:";
            // 
            // btnDir
            // 
            this.btnDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDir.Location = new System.Drawing.Point(347, 38);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(38, 20);
            this.btnDir.TabIndex = 11;
            this.btnDir.Text = "...";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.BtnDir_Click);
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(77, 38);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(263, 20);
            this.txtDir.TabIndex = 10;
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.Location = new System.Drawing.Point(9, 38);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(34, 17);
            this.lblDir.TabIndex = 9;
            this.lblDir.Text = "Dir:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 276);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBBL);
            this.Controls.Add(this.txtBBL);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.lblDir);
            this.Name = "Form1";
            this.Text = "Coverage Planter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnBBL;
        private System.Windows.Forms.TextBox txtBBL;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label lblDir;
    }
}

