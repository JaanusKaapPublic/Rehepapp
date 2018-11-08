namespace Analyzer
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
            this.label1 = new System.Windows.Forms.Label();
            this.idaFile = new System.Windows.Forms.TextBox();
            this.idaBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.addDirBtn = new System.Windows.Forms.Button();
            this.outputBtn = new System.Windows.Forms.Button();
            this.outputFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.peDir = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IDA pro executable:";
            // 
            // idaFile
            // 
            this.idaFile.Location = new System.Drawing.Point(120, 10);
            this.idaFile.Name = "idaFile";
            this.idaFile.Size = new System.Drawing.Size(398, 20);
            this.idaFile.TabIndex = 1;
            // 
            // idaBtn
            // 
            this.idaBtn.Location = new System.Drawing.Point(524, 8);
            this.idaBtn.Name = "idaBtn";
            this.idaBtn.Size = new System.Drawing.Size(75, 23);
            this.idaBtn.TabIndex = 2;
            this.idaBtn.Text = "Open";
            this.idaBtn.UseVisualStyleBackColor = true;
            this.idaBtn.Click += new System.EventHandler(this.idaBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Dir to analyse:";
            // 
            // addDirBtn
            // 
            this.addDirBtn.Location = new System.Drawing.Point(524, 37);
            this.addDirBtn.Name = "addDirBtn";
            this.addDirBtn.Size = new System.Drawing.Size(75, 23);
            this.addDirBtn.TabIndex = 8;
            this.addDirBtn.Text = "Open";
            this.addDirBtn.UseVisualStyleBackColor = true;
            this.addDirBtn.Click += new System.EventHandler(this.addDirBtn_Click);
            // 
            // outputBtn
            // 
            this.outputBtn.Location = new System.Drawing.Point(524, 66);
            this.outputBtn.Name = "outputBtn";
            this.outputBtn.Size = new System.Drawing.Size(75, 23);
            this.outputBtn.TabIndex = 13;
            this.outputBtn.Text = "Open";
            this.outputBtn.UseVisualStyleBackColor = true;
            this.outputBtn.Click += new System.EventHandler(this.outputBtn_Click);
            // 
            // outputFile
            // 
            this.outputFile.Location = new System.Drawing.Point(120, 68);
            this.outputFile.Name = "outputFile";
            this.outputFile.Size = new System.Drawing.Size(398, 20);
            this.outputFile.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Output file:";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(12, 119);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(619, 23);
            this.startBtn.TabIndex = 14;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // peDir
            // 
            this.peDir.Location = new System.Drawing.Point(120, 37);
            this.peDir.Name = "peDir";
            this.peDir.Size = new System.Drawing.Size(398, 20);
            this.peDir.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 150);
            this.Controls.Add(this.peDir);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.outputBtn);
            this.Controls.Add(this.outputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addDirBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.idaBtn);
            this.Controls.Add(this.idaFile);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Ebajalg analyser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idaFile;
        private System.Windows.Forms.Button idaBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addDirBtn;
        private System.Windows.Forms.Button outputBtn;
        private System.Windows.Forms.TextBox outputFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox peDir;
    }
}

