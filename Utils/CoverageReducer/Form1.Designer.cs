namespace CoverageReducer
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
            this.btnBBL = new System.Windows.Forms.Button();
            this.txtBBL = new System.Windows.Forms.TextBox();
            this.lblBBL = new System.Windows.Forms.Label();
            this.btnDir = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.lblDir = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBBL
            // 
            this.btnBBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBBL.Location = new System.Drawing.Point(349, 12);
            this.btnBBL.Name = "btnBBL";
            this.btnBBL.Size = new System.Drawing.Size(38, 20);
            this.btnBBL.TabIndex = 17;
            this.btnBBL.Text = "...";
            this.btnBBL.UseVisualStyleBackColor = true;
            this.btnBBL.Click += new System.EventHandler(this.BtnBBL_Click);
            // 
            // txtBBL
            // 
            this.txtBBL.Location = new System.Drawing.Point(79, 12);
            this.txtBBL.Name = "txtBBL";
            this.txtBBL.Size = new System.Drawing.Size(263, 20);
            this.txtBBL.TabIndex = 16;
            // 
            // lblBBL
            // 
            this.lblBBL.AutoSize = true;
            this.lblBBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBBL.Location = new System.Drawing.Point(11, 12);
            this.lblBBL.Name = "lblBBL";
            this.lblBBL.Size = new System.Drawing.Size(69, 17);
            this.lblBBL.TabIndex = 15;
            this.lblBBL.Text = "BBL file:";
            // 
            // btnDir
            // 
            this.btnDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDir.Location = new System.Drawing.Point(349, 38);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(38, 20);
            this.btnDir.TabIndex = 20;
            this.btnDir.Text = "...";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.BtnDir_Click);
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(79, 38);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(263, 20);
            this.txtDir.TabIndex = 19;
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.Location = new System.Drawing.Point(11, 38);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(67, 17);
            this.lblDir.TabIndex = 18;
            this.lblDir.Text = "BBC dir:";
            // 
            // btnOutput
            // 
            this.btnOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutput.Location = new System.Drawing.Point(349, 64);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(38, 20);
            this.btnOutput.TabIndex = 23;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.BtnOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(79, 64);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(263, 20);
            this.txtOutput.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Output:";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(14, 149);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(373, 142);
            this.txtLog.TabIndex = 26;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(181, 128);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(41, 17);
            this.lblLog.TabIndex = 25;
            this.lblLog.Text = "LOG";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(14, 102);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(373, 23);
            this.btnStart.TabIndex = 24;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 305);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.btnBBL);
            this.Controls.Add(this.txtBBL);
            this.Controls.Add(this.lblBBL);
            this.Name = "Form1";
            this.Text = "Coverage Reducer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBBL;
        private System.Windows.Forms.TextBox txtBBL;
        private System.Windows.Forms.Label lblBBL;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnStart;
    }
}

