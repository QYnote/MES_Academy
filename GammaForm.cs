﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01
{
    public partial class GammaForm : Form
    {
        int ValueMax, ValueMin;

        public GammaForm(int max, int min)
        {
            InitializeComponent();
            ValueMax = max * 10;
            ValueMin = min;
            BrightBar.Maximum = ValueMax;
            BrightBar.Minimum = ValueMin;
            BrightNum.Maximum = max;
            BrightNum.Minimum = min;
        }

        private void BrightNum_ValueChanged(object sender, EventArgs e)
        {
            BrightBar.Value = (int) BrightNum.Value * 10;
            //최대,최소값 넘겨서 작성시 조정
            if (BrightNum.Maximum < BrightNum.Value)
                BrightNum.Value = BrightNum.Maximum;
            if (BrightNum.Minimum > BrightNum.Value)
                BrightNum.Value = BrightNum.Minimum;
        }
        private void BrightValue_ValueChanged(object sender, EventArgs e)
        {
            BrightNum.Value = (decimal)(BrightBar.Value/10);
        }
        private void OK_BT_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Cancle_BT_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
