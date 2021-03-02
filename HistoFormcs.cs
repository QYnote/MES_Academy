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
    public partial class HistoFormcs : Form
    {
        public HistoFormcs(long[] rh, long[] gh, long[] bh)
        {
            InitializeComponent();
            rHisto = rh;
            gHisto = gh;
            bHisto = bh;
        }

        long[] rHisto, gHisto, bHisto;

        private void HistoFormcs_Load(object sender, EventArgs e)
        {
            chart1.Visible = true;  //차트를 off 해놨다가 화면에 띄움
            //차트타입 변경하기
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            //차트 색상 변경하기
            chart1.Series[0].Color = Color.Red;
            chart1.Series[1].Color = Color.Green;
            chart1.Series[2].Color = Color.Blue;

            //실 데이터 입력하기
            for(int i=0; i<256; i++)
            {
                chart1.Series[0].Points.AddXY(i, rHisto[i]);
                chart1.Series[1].Points.AddXY(i, gHisto[i]);
                chart1.Series[2].Points.AddXY(i, bHisto[i]);
            }
        }
    }
}
