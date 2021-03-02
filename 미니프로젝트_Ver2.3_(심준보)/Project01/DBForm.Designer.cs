
namespace Project01
{
    partial class DBForm
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
            this.BTN_DownLoad_from_DB = new System.Windows.Forms.Button();
            this.BTN_UpLoad_to_DB = new System.Windows.Forms.Button();
            this.BTN_openFile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.TB_FileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTN_DownLoad_from_DB
            // 
            this.BTN_DownLoad_from_DB.Font = new System.Drawing.Font("굴림", 12F);
            this.BTN_DownLoad_from_DB.Location = new System.Drawing.Point(489, 52);
            this.BTN_DownLoad_from_DB.Name = "BTN_DownLoad_from_DB";
            this.BTN_DownLoad_from_DB.Size = new System.Drawing.Size(190, 44);
            this.BTN_DownLoad_from_DB.TabIndex = 0;
            this.BTN_DownLoad_from_DB.Text = "DB에서 불러오기";
            this.BTN_DownLoad_from_DB.UseVisualStyleBackColor = true;
            this.BTN_DownLoad_from_DB.Click += new System.EventHandler(this.BTN_DownLoad_from_DB_Click);
            // 
            // BTN_UpLoad_to_DB
            // 
            this.BTN_UpLoad_to_DB.Font = new System.Drawing.Font("굴림", 12F);
            this.BTN_UpLoad_to_DB.Location = new System.Drawing.Point(489, 217);
            this.BTN_UpLoad_to_DB.Name = "BTN_UpLoad_to_DB";
            this.BTN_UpLoad_to_DB.Size = new System.Drawing.Size(190, 44);
            this.BTN_UpLoad_to_DB.TabIndex = 1;
            this.BTN_UpLoad_to_DB.Text = "DB에 저장하기";
            this.BTN_UpLoad_to_DB.UseVisualStyleBackColor = true;
            this.BTN_UpLoad_to_DB.Click += new System.EventHandler(this.BTN_UpLoad_to_DB_Click);
            // 
            // BTN_openFile
            // 
            this.BTN_openFile.Font = new System.Drawing.Font("굴림", 12F);
            this.BTN_openFile.Location = new System.Drawing.Point(489, 152);
            this.BTN_openFile.Name = "BTN_openFile";
            this.BTN_openFile.Size = new System.Drawing.Size(190, 44);
            this.BTN_openFile.TabIndex = 2;
            this.BTN_openFile.Text = "파일선택";
            this.BTN_openFile.UseVisualStyleBackColor = true;
            this.BTN_openFile.Click += new System.EventHandler(this.BTN_openFile_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("굴림", 12F);
            this.textBox1.Location = new System.Drawing.Point(27, 161);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(396, 30);
            this.textBox1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("굴림", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(27, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(396, 28);
            this.comboBox1.TabIndex = 4;
            // 
            // TB_FileName
            // 
            this.TB_FileName.Font = new System.Drawing.Font("굴림", 12F);
            this.TB_FileName.Location = new System.Drawing.Point(221, 226);
            this.TB_FileName.Name = "TB_FileName";
            this.TB_FileName.Size = new System.Drawing.Size(202, 30);
            this.TB_FileName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F);
            this.label1.Location = new System.Drawing.Point(137, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(419, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "---------------불러오기---------------";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F);
            this.label2.Location = new System.Drawing.Point(137, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(419, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "---------------저장하기---------------";
            // 
            // DBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 292);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_FileName);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BTN_openFile);
            this.Controls.Add(this.BTN_UpLoad_to_DB);
            this.Controls.Add(this.BTN_DownLoad_from_DB);
            this.Name = "DBForm";
            this.Text = "DBForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DBForm_FormClosed);
            this.Load += new System.EventHandler(this.DBForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_DownLoad_from_DB;
        private System.Windows.Forms.Button BTN_UpLoad_to_DB;
        private System.Windows.Forms.Button BTN_openFile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox TB_FileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}