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
using System.Drawing.Imaging;    //save용도
using System.IO;

namespace Project01
{
    public partial class SaveSelectForm : Form
    {
        Focus_BT mainFrm = new Focus_BT();  //메인폼
        
        public String saveFname;
        string i_user = "winuserName";
        byte[,,] S_outImg;
        int S_outH, S_outW;
        const int RGB = 3, RR = 0, GG = 1, BB = 2;  //RGB 갯수, RGB 순서0,1,2

        public SaveSelectForm(byte[,,] m_outImg, int m_outH, int m_outW)
        {
            S_outImg = m_outImg;
            S_outH = m_outH;
            S_outW = m_outW;
            InitializeComponent();

        }

        //MySQL 불러오기 위한 문장
        String connStr = "Server=192.168.56.101;" +
            "Uid=winuser;" +
            "Pwd=4321;" +
            "Database=blob_db;" +
            "Charset=UTF8";
        MySqlConnection conn; // 교량
        MySqlCommand cmd; // 트럭
        String sql = "";  // 물건박스
        MySqlDataReader reader; // 트럭이 가져올 끈


        private void SaveSelectForm_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            cmd = new MySqlCommand("", conn);
        }

        private void SaveSelectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }


        //저장 방식 선택
        //폴더에 저장하기
        private void BTN_OpenFolder_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "";
            sfd.Filter = "PNG File(*.png) | *.png";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            saveFname = sfd.FileName;
            Bitmap image = new Bitmap(S_outH, S_outW); // 빈 비트맵(종이) 준비
            for (int i = 0; i < S_outH; i++)
                for (int k = 0; k < S_outW; k++)
                {
                    Color c;
                    int r, g, b;
                    r = S_outImg[0, i, k];
                    g = S_outImg[1, i, k];
                    b = S_outImg[2, i, k];
                    c = Color.FromArgb(r, g, b);
                    image.SetPixel(i, k, c);  // 종이에 콕콕 찍기
                }
            // 상단에 using System.Drawing.Imaging; 추가해야 함
            image.Save(saveFname, ImageFormat.Png); // 종이를 PNG로 저장

            this.DialogResult = DialogResult.OK;
        }

        
    }
}
