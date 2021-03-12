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
using System.IO;
using OpenCvSharp;

namespace Project01
{
    public partial class OpenSelectForm : Form
    {
        public OpenSelectForm()
        {
            InitializeComponent();
        }

        public string fileName;
        Bitmap bitmap;
        const int RGB = 3, RR = 0, GG = 1, BB = 2;  //RGB 갯수, RGB 순서0,1,2
        public byte[,,] o_inImg;
        public int o_inH, o_inW;


        private void OpenSelectForm_Load(object sender, EventArgs e)
        {
            
        }

        private void OpenSelectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }


        //폴더에서 불러오기
        private void BTN_OpenFolder_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  // 객체 생성
            ofd.DefaultExt = "";
            ofd.Filter = "칼라필터 | *.png; *.jpg; *.bmp; *.if";    //호환파일?
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            fileName = ofd.FileName;

            //파일 -> 비트맵(bitmap)과정
            bitmap = new Bitmap(fileName);
            //BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
            //파일이 어떤 형태인지 알 수 없으므로 바이너리를 사용 할수 없음

            // 중요! 입력이미지의 높이, 폭 알아내기
            //long fsize = new FileInfo(fileName).Length;
            //inH = inW = (int)Math.Sqrt(fsize);
            o_inW = bitmap.Height;    //비트맵은 상하 <-> 좌우의 관계가 반대
            o_inH = bitmap.Width;

            o_inImg = new byte[RGB, o_inH, o_inW]; // 메모리 할당, 3의 역할 : RGB 3개

            //bitmap --> 메모리 과정(로딩)
            for (int i = 0; i < o_inH; i++)
                for (int k = 0; k < o_inW; k++)
                {
                    Color c = bitmap.GetPixel(i, k);    //파일의 칼라값 가져오기
                    o_inImg[RR, i, k] = c.R;
                    o_inImg[GG, i, k] = c.G;
                    o_inImg[BB, i, k] = c.B;
                }

            this.DialogResult = DialogResult.OK;
        }


        
        private void BTN_OpenCVFolder_Click(object sender, EventArgs e)
        {
            
        }
    }
}
