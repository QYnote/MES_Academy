
namespace Project01
{
    partial class GammaForm
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
            this.Cancle_BT = new System.Windows.Forms.Button();
            this.OK_BT = new System.Windows.Forms.Button();
            this.BrightNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BrightBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.BrightNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancle_BT
            // 
            this.Cancle_BT.Font = new System.Drawing.Font("맑은 고딕", 13F);
            this.Cancle_BT.Location = new System.Drawing.Point(227, 96);
            this.Cancle_BT.Name = "Cancle_BT";
            this.Cancle_BT.Size = new System.Drawing.Size(95, 39);
            this.Cancle_BT.TabIndex = 11;
            this.Cancle_BT.Text = "취소";
            this.Cancle_BT.UseVisualStyleBackColor = true;
            this.Cancle_BT.Click += new System.EventHandler(this.Cancle_BT_Click);
            // 
            // OK_BT
            // 
            this.OK_BT.Font = new System.Drawing.Font("맑은 고딕", 13F);
            this.OK_BT.Location = new System.Drawing.Point(86, 96);
            this.OK_BT.Name = "OK_BT";
            this.OK_BT.Size = new System.Drawing.Size(95, 39);
            this.OK_BT.TabIndex = 10;
            this.OK_BT.Text = "확인";
            this.OK_BT.UseVisualStyleBackColor = true;
            this.OK_BT.Click += new System.EventHandler(this.OK_BT_Click);
            // 
            // BrightNum
            // 
            this.BrightNum.DecimalPlaces = 1;
            this.BrightNum.Font = new System.Drawing.Font("굴림", 15F);
            this.BrightNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.BrightNum.Location = new System.Drawing.Point(261, 44);
            this.BrightNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BrightNum.Name = "BrightNum";
            this.BrightNum.Size = new System.Drawing.Size(120, 36);
            this.BrightNum.TabIndex = 9;
            this.BrightNum.ValueChanged += new System.EventHandler(this.BrightNum_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(172, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "어둡게";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "밝게";
            // 
            // BrightBar
            // 
            this.BrightBar.LargeChange = 2;
            this.BrightBar.Location = new System.Drawing.Point(35, 44);
            this.BrightBar.Maximum = 20;
            this.BrightBar.Name = "BrightBar";
            this.BrightBar.Size = new System.Drawing.Size(186, 56);
            this.BrightBar.TabIndex = 6;
            this.BrightBar.Value = 5;
            this.BrightBar.ValueChanged += new System.EventHandler(this.BrightValue_ValueChanged);
            // 
            // GammaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 160);
            this.Controls.Add(this.Cancle_BT);
            this.Controls.Add(this.OK_BT);
            this.Controls.Add(this.BrightNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BrightBar);
            this.Name = "GammaForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.BrightNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancle_BT;
        private System.Windows.Forms.Button OK_BT;
        public System.Windows.Forms.NumericUpDown BrightNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar BrightBar;
    }
}