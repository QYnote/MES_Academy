
namespace Project01
{
    partial class BrightForm
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
            this.BrightValue = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BrightNum = new System.Windows.Forms.NumericUpDown();
            this.OK_BT = new System.Windows.Forms.Button();
            this.Cancle_BT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BrightValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightNum)).BeginInit();
            this.SuspendLayout();
            // 
            // BrightValue
            // 
            this.BrightValue.Location = new System.Drawing.Point(34, 41);
            this.BrightValue.Maximum = 255;
            this.BrightValue.Minimum = -255;
            this.BrightValue.Name = "BrightValue";
            this.BrightValue.Size = new System.Drawing.Size(186, 56);
            this.BrightValue.TabIndex = 0;
            this.BrightValue.ValueChanged += new System.EventHandler(this.BrightValue_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(190, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "밝게";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "어둡게";
            // 
            // BrightNum
            // 
            this.BrightNum.Font = new System.Drawing.Font("굴림", 15F);
            this.BrightNum.Location = new System.Drawing.Point(260, 41);
            this.BrightNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BrightNum.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.BrightNum.Name = "BrightNum";
            this.BrightNum.Size = new System.Drawing.Size(120, 36);
            this.BrightNum.TabIndex = 3;
            this.BrightNum.ValueChanged += new System.EventHandler(this.BrightNum_ValueChanged);
            // 
            // OK_BT
            // 
            this.OK_BT.Font = new System.Drawing.Font("맑은 고딕", 13F);
            this.OK_BT.Location = new System.Drawing.Point(85, 93);
            this.OK_BT.Name = "OK_BT";
            this.OK_BT.Size = new System.Drawing.Size(95, 39);
            this.OK_BT.TabIndex = 4;
            this.OK_BT.Text = "확인";
            this.OK_BT.UseVisualStyleBackColor = true;
            this.OK_BT.Click += new System.EventHandler(this.OK_BT_Click);
            // 
            // Cancle_BT
            // 
            this.Cancle_BT.Font = new System.Drawing.Font("맑은 고딕", 13F);
            this.Cancle_BT.Location = new System.Drawing.Point(226, 93);
            this.Cancle_BT.Name = "Cancle_BT";
            this.Cancle_BT.Size = new System.Drawing.Size(95, 39);
            this.Cancle_BT.TabIndex = 5;
            this.Cancle_BT.Text = "취소";
            this.Cancle_BT.UseVisualStyleBackColor = true;
            this.Cancle_BT.Click += new System.EventHandler(this.Cancle_BT_Click);
            // 
            // BrightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(392, 146);
            this.Controls.Add(this.Cancle_BT);
            this.Controls.Add(this.OK_BT);
            this.Controls.Add(this.BrightNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BrightValue);
            this.Name = "BrightForm";
            this.Text = "조절창";
            ((System.ComponentModel.ISupportInitialize)(this.BrightValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar BrightValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OK_BT;
        private System.Windows.Forms.Button Cancle_BT;
        public System.Windows.Forms.NumericUpDown BrightNum;
    }
}