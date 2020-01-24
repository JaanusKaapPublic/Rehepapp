namespace CoverageMain
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
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnExec = new System.Windows.Forms.Button();
            this.txtExec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBaseDir = new System.Windows.Forms.Button();
            this.txtBaseDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInputDir = new System.Windows.Forms.Button();
            this.txtInputDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnDoneDir = new System.Windows.Forms.Button();
            this.txtDoneDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOutputDir = new System.Windows.Forms.Button();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPreCmd = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBBL
            // 
            this.btnBBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBBL.Location = new System.Drawing.Point(505, 12);
            this.btnBBL.Name = "btnBBL";
            this.btnBBL.Size = new System.Drawing.Size(38, 20);
            this.btnBBL.TabIndex = 17;
            this.btnBBL.Text = "...";
            this.btnBBL.UseVisualStyleBackColor = true;
            this.btnBBL.Click += new System.EventHandler(this.BtnBBL_Click);
            // 
            // txtBBL
            // 
            this.txtBBL.Location = new System.Drawing.Point(127, 12);
            this.txtBBL.Name = "txtBBL";
            this.txtBBL.Size = new System.Drawing.Size(372, 20);
            this.txtBBL.TabIndex = 16;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(13, 12);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(69, 17);
            this.lblOutput.TabIndex = 15;
            this.lblOutput.Text = "BBL file:";
            // 
            // btnExec
            // 
            this.btnExec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExec.Location = new System.Drawing.Point(505, 38);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(38, 20);
            this.btnExec.TabIndex = 20;
            this.btnExec.Text = "...";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.BtnExec_Click);
            // 
            // txtExec
            // 
            this.txtExec.Location = new System.Drawing.Point(127, 38);
            this.txtExec.Name = "txtExec";
            this.txtExec.Size = new System.Drawing.Size(372, 20);
            this.txtExec.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Executable:";
            // 
            // btnBaseDir
            // 
            this.btnBaseDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaseDir.Location = new System.Drawing.Point(505, 64);
            this.btnBaseDir.Name = "btnBaseDir";
            this.btnBaseDir.Size = new System.Drawing.Size(38, 20);
            this.btnBaseDir.TabIndex = 23;
            this.btnBaseDir.Text = "...";
            this.btnBaseDir.UseVisualStyleBackColor = true;
            this.btnBaseDir.Click += new System.EventHandler(this.BtnBaseDir_Click);
            // 
            // txtBaseDir
            // 
            this.txtBaseDir.Location = new System.Drawing.Point(127, 64);
            this.txtBaseDir.Name = "txtBaseDir";
            this.txtBaseDir.Size = new System.Drawing.Size(372, 20);
            this.txtBaseDir.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Base dir:";
            // 
            // btnInputDir
            // 
            this.btnInputDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInputDir.Location = new System.Drawing.Point(505, 90);
            this.btnInputDir.Name = "btnInputDir";
            this.btnInputDir.Size = new System.Drawing.Size(38, 20);
            this.btnInputDir.TabIndex = 26;
            this.btnInputDir.Text = "...";
            this.btnInputDir.UseVisualStyleBackColor = true;
            this.btnInputDir.Click += new System.EventHandler(this.BtnInputDir_Click);
            // 
            // txtInputDir
            // 
            this.txtInputDir.Location = new System.Drawing.Point(127, 90);
            this.txtInputDir.Name = "txtInputDir";
            this.txtInputDir.Size = new System.Drawing.Size(372, 20);
            this.txtInputDir.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Input files dir:";
            // 
            // txtCmd
            // 
            this.txtCmd.Location = new System.Drawing.Point(127, 275);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(416, 20);
            this.txtCmd.TabIndex = 28;
            this.txtCmd.Text = "\"%INPUT%\"";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Arguments:";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(127, 301);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(47, 20);
            this.txtTime.TabIndex = 30;
            this.txtTime.Text = "16";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "Wait time:";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(18, 385);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(527, 213);
            this.txtLog.TabIndex = 33;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(260, 358);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(41, 17);
            this.lblLog.TabIndex = 32;
            this.lblLog.Text = "LOG";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 332);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(527, 23);
            this.btnStart.TabIndex = 31;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnDoneDir
            // 
            this.btnDoneDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoneDir.Location = new System.Drawing.Point(505, 116);
            this.btnDoneDir.Name = "btnDoneDir";
            this.btnDoneDir.Size = new System.Drawing.Size(38, 20);
            this.btnDoneDir.TabIndex = 36;
            this.btnDoneDir.Text = "...";
            this.btnDoneDir.UseVisualStyleBackColor = true;
            this.btnDoneDir.Click += new System.EventHandler(this.BtnDoneDir_Click);
            // 
            // txtDoneDir
            // 
            this.txtDoneDir.Location = new System.Drawing.Point(127, 116);
            this.txtDoneDir.Name = "txtDoneDir";
            this.txtDoneDir.Size = new System.Drawing.Size(372, 20);
            this.txtDoneDir.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 34;
            this.label6.Text = "Done dir:";
            // 
            // btnOutputDir
            // 
            this.btnOutputDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutputDir.Location = new System.Drawing.Point(505, 142);
            this.btnOutputDir.Name = "btnOutputDir";
            this.btnOutputDir.Size = new System.Drawing.Size(38, 20);
            this.btnOutputDir.TabIndex = 39;
            this.btnOutputDir.Text = "...";
            this.btnOutputDir.UseVisualStyleBackColor = true;
            this.btnOutputDir.Click += new System.EventHandler(this.BtnOutputDir_Click);
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Location = new System.Drawing.Point(127, 142);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(372, 20);
            this.txtOutputDir.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 17);
            this.label7.TabIndex = 37;
            this.label7.Text = "Output dir:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 17);
            this.label8.TabIndex = 40;
            this.label8.Text = "Pre-cmd:";
            // 
            // txtPreCmd
            // 
            this.txtPreCmd.Location = new System.Drawing.Point(127, 177);
            this.txtPreCmd.Multiline = true;
            this.txtPreCmd.Name = "txtPreCmd";
            this.txtPreCmd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreCmd.Size = new System.Drawing.Size(416, 92);
            this.txtPreCmd.TabIndex = 41;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 610);
            this.Controls.Add(this.txtPreCmd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnOutputDir);
            this.Controls.Add(this.txtOutputDir);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDoneDir);
            this.Controls.Add(this.txtDoneDir);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnInputDir);
            this.Controls.Add(this.txtInputDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBaseDir);
            this.Controls.Add(this.txtBaseDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.txtExec);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBBL);
            this.Controls.Add(this.txtBBL);
            this.Controls.Add(this.lblOutput);
            this.Name = "Form1";
            this.Text = "Coverage Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBBL;
        private System.Windows.Forms.TextBox txtBBL;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.TextBox txtExec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBaseDir;
        private System.Windows.Forms.TextBox txtBaseDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInputDir;
        private System.Windows.Forms.TextBox txtInputDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnDoneDir;
        private System.Windows.Forms.TextBox txtDoneDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOutputDir;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPreCmd;
    }
}

