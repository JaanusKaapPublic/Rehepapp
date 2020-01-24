namespace CoverageMinimizer
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
            this.btnOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCoverage = new System.Windows.Forms.Button();
            this.txtCoverage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOutput
            // 
            this.btnOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutput.Location = new System.Drawing.Point(503, 64);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(38, 20);
            this.btnOutput.TabIndex = 53;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.BtnOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(125, 64);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(372, 20);
            this.txtOutput.TabIndex = 52;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 17);
            this.label7.TabIndex = 51;
            this.label7.Text = "Results dir:";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(16, 201);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(527, 213);
            this.txtLog.TabIndex = 47;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(258, 174);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(41, 17);
            this.lblLog.TabIndex = 46;
            this.lblLog.Text = "LOG";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(14, 122);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(527, 23);
            this.btnStart.TabIndex = 45;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(125, 91);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(47, 20);
            this.txtCount.TabIndex = 44;
            this.txtCount.Text = "16";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 17);
            this.label5.TabIndex = 43;
            this.label5.Text = "Count:";
            // 
            // btnCoverage
            // 
            this.btnCoverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoverage.Location = new System.Drawing.Point(503, 12);
            this.btnCoverage.Name = "btnCoverage";
            this.btnCoverage.Size = new System.Drawing.Size(38, 20);
            this.btnCoverage.TabIndex = 42;
            this.btnCoverage.Text = "...";
            this.btnCoverage.UseVisualStyleBackColor = true;
            this.btnCoverage.Click += new System.EventHandler(this.BtnCoverage_Click);
            // 
            // txtCoverage
            // 
            this.txtCoverage.Location = new System.Drawing.Point(125, 12);
            this.txtCoverage.Name = "txtCoverage";
            this.txtCoverage.Size = new System.Drawing.Size(372, 20);
            this.txtCoverage.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 40;
            this.label3.Text = "Coverage dir:";
            // 
            // btnInput
            // 
            this.btnInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInput.Location = new System.Drawing.Point(503, 38);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(38, 20);
            this.btnInput.TabIndex = 56;
            this.btnInput.Text = "...";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.BtnInput_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(125, 38);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(372, 20);
            this.txtInput.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 54;
            this.label1.Text = "Input files dir:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 425);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCoverage);
            this.Controls.Add(this.txtCoverage);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.Text = "Coverage Minimizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCoverage;
        private System.Windows.Forms.TextBox txtCoverage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
    }
}

