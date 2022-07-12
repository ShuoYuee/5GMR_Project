

namespace MR.ConsoleClient
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
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnDisConnect = new System.Windows.Forms.Button();
            this.btnSendBuf1 = new System.Windows.Forms.Button();
            this.BtnSendBuf2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Pwd = new System.Windows.Forms.TextBox();
            this.CreateBtn = new System.Windows.Forms.Button();
            this.boxListTeam = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(213, 79);
            this.BtnConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(100, 29);
            this.BtnConnect.TabIndex = 0;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnDisConnect
            // 
            this.BtnDisConnect.Location = new System.Drawing.Point(373, 79);
            this.BtnDisConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDisConnect.Name = "BtnDisConnect";
            this.BtnDisConnect.Size = new System.Drawing.Size(100, 29);
            this.BtnDisConnect.TabIndex = 1;
            this.BtnDisConnect.Text = "DisConnect";
            this.BtnDisConnect.UseVisualStyleBackColor = true;
            this.BtnDisConnect.Click += new System.EventHandler(this.BtnDisConnect_Click);
            // 
            // btnSendBuf1
            // 
            this.btnSendBuf1.Location = new System.Drawing.Point(80, 244);
            this.btnSendBuf1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSendBuf1.Name = "btnSendBuf1";
            this.btnSendBuf1.Size = new System.Drawing.Size(100, 29);
            this.btnSendBuf1.TabIndex = 2;
            this.btnSendBuf1.Text = "LoginGame";
            this.btnSendBuf1.UseVisualStyleBackColor = true;
            this.btnSendBuf1.Click += new System.EventHandler(this.btnSendBuf1_Click);
            // 
            // BtnSendBuf2
            // 
            this.BtnSendBuf2.Location = new System.Drawing.Point(497, 244);
            this.BtnSendBuf2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSendBuf2.Name = "BtnSendBuf2";
            this.BtnSendBuf2.Size = new System.Drawing.Size(100, 29);
            this.BtnSendBuf2.TabIndex = 2;
            this.BtnSendBuf2.Text = "SendBuf2";
            this.BtnSendBuf2.UseVisualStyleBackColor = true;
            this.BtnSendBuf2.Click += new System.EventHandler(this.btnSendBuf1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "帐号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 191);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "密码";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(133, 186);
            this.textBox_Name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(132, 25);
            this.textBox_Name.TabIndex = 5;
            // 
            // textBox_Pwd
            // 
            this.textBox_Pwd.Location = new System.Drawing.Point(339, 186);
            this.textBox_Pwd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_Pwd.Name = "textBox_Pwd";
            this.textBox_Pwd.Size = new System.Drawing.Size(132, 25);
            this.textBox_Pwd.TabIndex = 6;
            // 
            // CreateBtn
            // 
            this.CreateBtn.Location = new System.Drawing.Point(288, 250);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateBtn.TabIndex = 7;
            this.CreateBtn.Text = "註冊";
            this.CreateBtn.UseVisualStyleBackColor = true;
            this.CreateBtn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // boxListTeam
            // 
            this.boxListTeam.FormattingEnabled = true;
            this.boxListTeam.Items.AddRange(new object[] {
            "A隊",
            "B隊"});
            this.boxListTeam.Location = new System.Drawing.Point(553, 184);
            this.boxListTeam.Name = "boxListTeam";
            this.boxListTeam.Size = new System.Drawing.Size(121, 23);
            this.boxListTeam.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.boxListTeam);
            this.Controls.Add(this.CreateBtn);
            this.Controls.Add(this.textBox_Pwd);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnSendBuf2);
            this.Controls.Add(this.btnSendBuf1);
            this.Controls.Add(this.BtnDisConnect);
            this.Controls.Add(this.BtnConnect);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnDisConnect;
        private System.Windows.Forms.Button btnSendBuf1;
        private System.Windows.Forms.Button BtnSendBuf2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_Pwd;
        private System.Windows.Forms.Button CreateBtn;
        private System.Windows.Forms.ComboBox boxListTeam;
    }
}