using System;
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
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }
        

        private void timer1_Tick(object sender, EventArgs e)    //1틱 0.1초 추정
        {
            timer1.Enabled = true;
            progressBar1.Increment(10);  //진행률 표시 타이머 틱당 진행률 2증가
            ProgressPer.Text = progressBar1.Value + "%";

            if(progressBar1.Value == 100)   //100% 다차면
            {
                timer1.Enabled = false;

                this.Close();
            }
        }
    }
}
