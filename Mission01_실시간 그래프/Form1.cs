using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Mission01_실시간_그래프
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        String connStr = "Server=192.168.56.101;Uid=winuser;Pwd=4321;Database=hardware_db;Charset=UTF8";
        MySqlConnection conn; // 교량
        MySqlCommand cmd; // 트럭
        String sql = "";  // 물건박스
        MySqlDataReader reader; // 트럭이 가져올 끈

        private void MainForm_Load(object sender, EventArgs e)
        {
            //<1> 데이터베이스 연결 (교량 건설) + <2> 트럭 준비
            conn = new MySqlConnection(connStr);
            conn.Open();
            cmd = new MySqlCommand("", conn);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //<4> 데이터베이스 해제 (교량 철거)
            conn.Close();
            MessageBox.Show("잘가세요~ 도비는 자유에요~");
        }

        int[] temperAry = new int[30];
        int[] humiAry = new int[30];

        private void timer1_Tick(object sender, EventArgs e)
        {
            //DB에서 온도 받아오기
            sql = "SELECT t_temper FROM temper ORDER BY t_date DESC LIMIT 1";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            reader.Read();
            int temper = (int) reader["t_temper"];
            reader.Close();
            //DB에서 습도 받아오기
            sql = "SELECT h_humi FROM humi ORDER BY h_date DESC LIMIT 1";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            reader.Read();
            int humi = (int)reader["h_humi"];
            reader.Close();

            //실시간 불러왔을때 가장 오래된 데이터 지우고 새로 받을 준비
            for(int i=0; i<temperAry.Length - 1; i++)
            {
                temperAry[i] = temperAry[i + 1];
                humiAry[i] = humiAry[i + 1];
            }
            //배열 마지막에 최신데이터 삽입
            temperAry[temperAry.Length - 1] = temper;
            humiAry[humiAry.Length - 1] = humi;

            //차트 그리기(구글로 예쁘게)
            //가로
            chart1.ChartAreas[0].AxisX.Maximum = 30;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 5;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            //세로
            chart1.ChartAreas[0].AxisY.Maximum = 400;
            chart1.ChartAreas[0].AxisY.Minimum = -200;
            chart1.ChartAreas[0].AxisY.Interval = 100;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;



            //온도 그래프타입
            chart1.Series[0].ChartType =
                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[0].Name = "온도";
            chart1.Series[0].Color = Color.Orange;    //온도 선 색 : 주황색
            //습도 그래프타입
            chart1.Series[1].ChartType =
                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].Name = "습도";
            chart1.Series[1].Color = Color.SkyBlue;    //습도 선 색 : 하늘색
            chart1.Series[1].BorderWidth = 2;             //선 두께

            //초기화
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            //그리기
            for (int i = 0; i < temperAry.Length - 1; i++)
            {
                chart1.Series[0].Points.AddXY(i, temperAry[i]);
                chart1.Series[1].Points.AddXY(i, humiAry[i]);
            }
        }
    }
}
