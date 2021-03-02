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
    public partial class BrightForm : Form
    {
        int ValueMax, ValueMin;
        public BrightForm(int max, int min)
        {
            InitializeComponent();
            ValueMax = max;
            ValueMin = min;
            BrightValue.Maximum = ValueMax;
            BrightValue.Minimum = ValueMin;
            BrightNum.Maximum = ValueMax;
            BrightNum.Minimum = ValueMin;
        }

        private void BrightValue_ValueChanged(object sender, EventArgs e)
        {
            BrightNum.Value = BrightValue.Value;
        }

        private void BrightNum_ValueChanged(object sender, EventArgs e)
        {
            BrightValue.Value = (int) BrightNum.Value;
            //최대,최소값 넘겨서 작성시 조정
            if (ValueMax < BrightNum.Value)
                BrightNum.Value = ValueMax;
            if (ValueMin > BrightNum.Value)
                BrightNum.Value = ValueMin;
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
