
namespace SexyBaseball.Server
{
    partial class MonohUIForm
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
            this.btnClear = new System.Windows.Forms.Button();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.buttonSetWinResult = new System.Windows.Forms.Button();
            this.winResultSingleField = new System.Windows.Forms.TextBox();
            this.winResultTitle1 = new System.Windows.Forms.Label();
            this.winResultSingle = new System.Windows.Forms.Label();
            this.winResultDouble = new System.Windows.Forms.Label();
            this.winResultDoubleField = new System.Windows.Forms.TextBox();
            this.winResultTriple = new System.Windows.Forms.Label();
            this.winResultTripleField = new System.Windows.Forms.TextBox();
            this.winResultHomeRun = new System.Windows.Forms.Label();
            this.winResultHomeRunField = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(131, 368);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.Location = new System.Drawing.Point(27, 23);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.Size = new System.Drawing.Size(588, 332);
            this.richTextBox_Log.TabIndex = 2;
            this.richTextBox_Log.Text = "";
            // 
            // buttonSetWinResult
            // 
            this.buttonSetWinResult.Location = new System.Drawing.Point(668, 152);
            this.buttonSetWinResult.Name = "buttonSetWinResult";
            this.buttonSetWinResult.Size = new System.Drawing.Size(75, 23);
            this.buttonSetWinResult.TabIndex = 3;
            this.buttonSetWinResult.Text = "Set";
            this.buttonSetWinResult.UseVisualStyleBackColor = true;
            this.buttonSetWinResult.Click += new System.EventHandler(this.buttonSetWinResult_Click);
            // 
            // winResultSingleField
            // 
            this.winResultSingleField.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.winResultSingleField.Location = new System.Drawing.Point(734, 40);
            this.winResultSingleField.Name = "winResultSingleField";
            this.winResultSingleField.Size = new System.Drawing.Size(58, 22);
            this.winResultSingleField.TabIndex = 4;
            this.winResultSingleField.Text = "50";
            this.winResultSingleField.Validated += new System.EventHandler(this.winResultSingleField_Validated);
            // 
            // winResultTitle1
            // 
            this.winResultTitle1.AutoSize = true;
            this.winResultTitle1.Location = new System.Drawing.Point(666, 23);
            this.winResultTitle1.Name = "winResultTitle1";
            this.winResultTitle1.Size = new System.Drawing.Size(61, 12);
            this.winResultTitle1.TabIndex = 5;
            this.winResultTitle1.Text = "Win Results";
            // 
            // winResultSingle
            // 
            this.winResultSingle.AutoSize = true;
            this.winResultSingle.Location = new System.Drawing.Point(621, 45);
            this.winResultSingle.Name = "winResultSingle";
            this.winResultSingle.Size = new System.Drawing.Size(34, 12);
            this.winResultSingle.TabIndex = 6;
            this.winResultSingle.Text = "Single";
            // 
            // winResultDouble
            // 
            this.winResultDouble.AutoSize = true;
            this.winResultDouble.Location = new System.Drawing.Point(621, 73);
            this.winResultDouble.Name = "winResultDouble";
            this.winResultDouble.Size = new System.Drawing.Size(39, 12);
            this.winResultDouble.TabIndex = 8;
            this.winResultDouble.Text = "Double";
            // 
            // winResultDoubleField
            // 
            this.winResultDoubleField.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.winResultDoubleField.Location = new System.Drawing.Point(734, 68);
            this.winResultDoubleField.Name = "winResultDoubleField";
            this.winResultDoubleField.Size = new System.Drawing.Size(58, 22);
            this.winResultDoubleField.TabIndex = 7;
            this.winResultDoubleField.Text = "30";
            this.winResultDoubleField.Validated += new System.EventHandler(this.winResultDoubleField_Validated);
            // 
            // winResultTriple
            // 
            this.winResultTriple.AutoSize = true;
            this.winResultTriple.Location = new System.Drawing.Point(621, 101);
            this.winResultTriple.Name = "winResultTriple";
            this.winResultTriple.Size = new System.Drawing.Size(33, 12);
            this.winResultTriple.TabIndex = 10;
            this.winResultTriple.Text = "Triple";
            // 
            // winResultTripleField
            // 
            this.winResultTripleField.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.winResultTripleField.Location = new System.Drawing.Point(734, 96);
            this.winResultTripleField.Name = "winResultTripleField";
            this.winResultTripleField.Size = new System.Drawing.Size(58, 22);
            this.winResultTripleField.TabIndex = 9;
            this.winResultTripleField.Text = "15";
            this.winResultTripleField.Validated += new System.EventHandler(this.winResultTripleField_Validated);
            // 
            // winResultHomeRun
            // 
            this.winResultHomeRun.AutoSize = true;
            this.winResultHomeRun.Location = new System.Drawing.Point(621, 129);
            this.winResultHomeRun.Name = "winResultHomeRun";
            this.winResultHomeRun.Size = new System.Drawing.Size(53, 12);
            this.winResultHomeRun.TabIndex = 12;
            this.winResultHomeRun.Text = "HomeRun";
            // 
            // winResultHomeRunField
            // 
            this.winResultHomeRunField.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.winResultHomeRunField.Location = new System.Drawing.Point(734, 124);
            this.winResultHomeRunField.Name = "winResultHomeRunField";
            this.winResultHomeRunField.Size = new System.Drawing.Size(58, 22);
            this.winResultHomeRunField.TabIndex = 11;
            this.winResultHomeRunField.Text = "5";
            this.winResultHomeRunField.Validated += new System.EventHandler(this.winResultHomeRunField_Validated);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(623, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "StopServer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StopServer_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(704, 181);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "StartServer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Start_Click);
            // 
            // MonohUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.winResultHomeRun);
            this.Controls.Add(this.winResultHomeRunField);
            this.Controls.Add(this.winResultTriple);
            this.Controls.Add(this.winResultTripleField);
            this.Controls.Add(this.winResultDouble);
            this.Controls.Add(this.winResultDoubleField);
            this.Controls.Add(this.winResultSingle);
            this.Controls.Add(this.winResultTitle1);
            this.Controls.Add(this.winResultSingleField);
            this.Controls.Add(this.buttonSetWinResult);
            this.Controls.Add(this.richTextBox_Log);
            this.Controls.Add(this.btnClear);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MonohUIForm";
            this.ShowInTaskbar = true;
            this.Text = "MonohUIForm";
            this.Load += new System.EventHandler(this.MonohUIForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.Button buttonSetWinResult;
        private System.Windows.Forms.TextBox winResultSingleField;
        private System.Windows.Forms.Label winResultTitle1;
        private System.Windows.Forms.Label winResultSingle;
        private System.Windows.Forms.Label winResultDouble;
        private System.Windows.Forms.TextBox winResultDoubleField;
        private System.Windows.Forms.Label winResultTriple;
        private System.Windows.Forms.TextBox winResultTripleField;
        private System.Windows.Forms.Label winResultHomeRun;
        private System.Windows.Forms.TextBox winResultHomeRunField;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}