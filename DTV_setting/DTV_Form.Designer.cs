namespace DTV_setting
{
    partial class DTV_Form
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
            this.btSetDTV = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.txtAmplitude = new System.Windows.Forms.TextBox();
            this.groupBoxSetting = new System.Windows.Forms.GroupBox();
            this.groupBoxSCPI = new System.Windows.Forms.GroupBox();
            this.txtSCPICommands = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btEXMSetDTV = new System.Windows.Forms.Button();
            this.groupBoxSetting.SuspendLayout();
            this.groupBoxSCPI.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSetDTV
            // 
            this.btSetDTV.Location = new System.Drawing.Point(12, 323);
            this.btSetDTV.Name = "btSetDTV";
            this.btSetDTV.Size = new System.Drawing.Size(177, 45);
            this.btSetDTV.TabIndex = 0;
            this.btSetDTV.Text = "SET DTV - ON";
            this.btSetDTV.UseVisualStyleBackColor = true;
            this.btSetDTV.Click += new System.EventHandler(this.btSetDTV_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Frequency(MHz):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amplitude:";
            // 
            // txtFrequency
            // 
            this.txtFrequency.Location = new System.Drawing.Point(100, 32);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(71, 20);
            this.txtFrequency.TabIndex = 3;
            this.txtFrequency.Text = "587.143";
            // 
            // txtAmplitude
            // 
            this.txtAmplitude.Location = new System.Drawing.Point(100, 73);
            this.txtAmplitude.Name = "txtAmplitude";
            this.txtAmplitude.Size = new System.Drawing.Size(71, 20);
            this.txtAmplitude.TabIndex = 4;
            // 
            // groupBoxSetting
            // 
            this.groupBoxSetting.Controls.Add(this.label1);
            this.groupBoxSetting.Controls.Add(this.txtAmplitude);
            this.groupBoxSetting.Controls.Add(this.label2);
            this.groupBoxSetting.Controls.Add(this.txtFrequency);
            this.groupBoxSetting.Location = new System.Drawing.Point(18, 12);
            this.groupBoxSetting.Name = "groupBoxSetting";
            this.groupBoxSetting.Size = new System.Drawing.Size(232, 118);
            this.groupBoxSetting.TabIndex = 5;
            this.groupBoxSetting.TabStop = false;
            this.groupBoxSetting.Text = "Setting";
            // 
            // groupBoxSCPI
            // 
            this.groupBoxSCPI.Controls.Add(this.txtSCPICommands);
            this.groupBoxSCPI.Location = new System.Drawing.Point(19, 137);
            this.groupBoxSCPI.Name = "groupBoxSCPI";
            this.groupBoxSCPI.Size = new System.Drawing.Size(485, 134);
            this.groupBoxSCPI.TabIndex = 6;
            this.groupBoxSCPI.TabStop = false;
            this.groupBoxSCPI.Text = "SCPI Commands";
            // 
            // txtSCPICommands
            // 
            this.txtSCPICommands.AcceptsReturn = true;
            this.txtSCPICommands.Location = new System.Drawing.Point(8, 19);
            this.txtSCPICommands.Multiline = true;
            this.txtSCPICommands.Name = "txtSCPICommands";
            this.txtSCPICommands.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSCPICommands.Size = new System.Drawing.Size(471, 108);
            this.txtSCPICommands.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 37);
            this.label3.TabIndex = 7;
            this.label3.Text = "CMW500";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(361, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 37);
            this.label4.TabIndex = 8;
            this.label4.Text = "EXM";
            // 
            // btEXMSetDTV
            // 
            this.btEXMSetDTV.Location = new System.Drawing.Point(305, 323);
            this.btEXMSetDTV.Name = "btEXMSetDTV";
            this.btEXMSetDTV.Size = new System.Drawing.Size(193, 45);
            this.btEXMSetDTV.TabIndex = 9;
            this.btEXMSetDTV.Text = "SET DTV - ON";
            this.btEXMSetDTV.UseVisualStyleBackColor = true;
            this.btEXMSetDTV.Click += new System.EventHandler(this.btEXMSetDTV_Click);
            // 
            // DTV_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 391);
            this.Controls.Add(this.btEXMSetDTV);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBoxSCPI);
            this.Controls.Add(this.groupBoxSetting);
            this.Controls.Add(this.btSetDTV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DTV_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DTV Setting";
            this.Load += new System.EventHandler(this.DTV_setting);
            this.groupBoxSetting.ResumeLayout(false);
            this.groupBoxSetting.PerformLayout();
            this.groupBoxSCPI.ResumeLayout(false);
            this.groupBoxSCPI.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSetDTV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.TextBox txtAmplitude;
        private System.Windows.Forms.GroupBox groupBoxSetting;
        private System.Windows.Forms.GroupBox groupBoxSCPI;
        private System.Windows.Forms.TextBox txtSCPICommands;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btEXMSetDTV;
    }
}

