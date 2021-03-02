using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;    //save용도


namespace Project01
{
    public partial class Focus_BT : Form
    {
        public Focus_BT()
        {
            InitializeComponent();
        }

        //전역변수부
        public byte[,,] outImg = null;
        public int outH, outW;
        byte[,,] inImg = null;
        int inH, inW;
        OpenSelectForm opFrm;


        Bitmap paper;
        const int RGB = 3, RR = 0, GG = 1, BB = 2;  //RGB 갯수, RGB 순서0,1,2

        bool MouseOnOff = false;
        int MStartX, MStartY, MEndX, MEndY;
        bool RangeSelect = false;


        //메뉴 이벤트 처리부
        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openImg();
        }
        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImg();
        }

        private void Load_BT_Click(object sender, EventArgs e)
        {
            openImg();
        }
        private void Save_BT_Click(object sender, EventArgs e)
        {
            SaveImg();
        }


        //화소점처리 처리부
        private void 동일이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equal_img();
        }
        private void OriginalBT_Click(object sender, EventArgs e)
        {
            equal_img();
        }

        private void 밝게어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bright_image();
        }
        private void BrightBT_Click(object sender, EventArgs e)
        {
            bright_image();
        }

        private void Gamma_BT_Click(object sender, EventArgs e)
        {
            gamma();
        }

        private void Gray_BT_Click(object sender, EventArgs e)
        {
            gray_img();
        }

        private void Reverse_BT_Click(object sender, EventArgs e)
        {
            reverse_img();
        }

        private void Posterising_BT_Click(object sender, EventArgs e)
        {
            posterising();        //버그 수정중 : 2021-02-08 ~ 2021-02-10 완
        }

        private void FocusBT_Click(object sender, EventArgs e)
        {
            focus();
        }
        private void 그레이스케일ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gray_img();
        }
        private void 반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverse_img();
        }

        private void 감마ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamma();
        }

        private void 포스터라이징ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //posterising();        버그 수정중 : 2021-02-08~2021-02-10
        }
        private void 범위강조ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            focus();
        }
        private void ColorChange_Click(object sender, EventArgs e)
        {
            changeColor_img();  //채도 변경
        }

        //기하학 처리
        private void 확대ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom_in_Img();
        }

        private void 축소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom_out_Img();
        }
        private void 이동ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            move_Img();
        }
        private void 가로반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Row_Mirror_Img();
        }
        private void 세로반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            High_Mirror_Img();
        }

        //기하학 버튼
        private void ZoomIn_BT_Click(object sender, EventArgs e)
        {
            zoom_in_Img();
        }

        private void ZoomOut_BT_Click(object sender, EventArgs e)
        {
            zoom_out_Img();
        }

        private void Move_BT_Click(object sender, EventArgs e)
        {
            move_Img();
        }

        private void RowMirror_BT_Click(object sender, EventArgs e)
        {
            Row_Mirror_Img();
        }

        private void HighMirror_BT_Click(object sender, EventArgs e)
        {
            High_Mirror_Img();
        }
        //화소영역처리
        private void 엠보싱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            emboss_image();
        }
        private void 블러링ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blurr_image();
        }
        private void 가우시안ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gaussian_image();
        }
        private void 샤프닝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharp_image();
        }
        private void 고주파필터링ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hFilter_image();
        }
        private void 수직에지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vecticalEdge_image();
        }
        private void 수평에지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            horizonEdge_image();
        }
        //5*5
        private void 엠보싱ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            emboss5_image();
        }
        private void 유사연산자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            homogen_image();
        }


        //화소영역 버튼
        private void Emboss3_BT_Click(object sender, EventArgs e)
        {
            emboss_image();
        }

        private void Emboss5_BT_Click(object sender, EventArgs e)
        {
            emboss5_image();
        }

        private void Blurr_BT_Click(object sender, EventArgs e)
        {
            blurr_image();
        }

        private void Gausian_BT_Click(object sender, EventArgs e)
        {
            gaussian_image();
        }

        private void Sharp_BT_Click(object sender, EventArgs e)
        {
            sharp_image();
        }

        private void HFillter_BT_Click(object sender, EventArgs e)
        {
            hFilter_image();
        }

        private void VectEnge_BT_Click(object sender, EventArgs e)
        {
            vecticalEdge_image();
        }

        private void HorEdge_BT_Click(object sender, EventArgs e)
        {
            horizonEdge_image();
        }

        private void Homogen_BT_Click(object sender, EventArgs e)
        {
            homogen_image();
        }
        //히스토그램
        private void 그리기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draw_Histogram();
        }
        private void DrawHisto_BT_Click(object sender, EventArgs e)
        {
            draw_Histogram();
        }
        private void Stretch_BT_Click(object sender, EventArgs e)
        {
            histo_Strech_img();
        }
        private void AndIn_BT_Click(object sender, EventArgs e)
        {
            histo_Endin_img();
        }
        private void Equalized_BT_Click(object sender, EventArgs e)
        {
            equalized_img();
        }
        //효과 영역선택
        private void 효과영역선택ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MouseOnOff = true;
        }
        private void RangeSelectBT_Click(object sender, EventArgs e)
        {
            MouseOnOff = true;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel2.Text = "X : " + e.X + ", Y : " + e.Y;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!MouseOnOff)
                return;
            MStartX = e.X; MStartY = e.Y;

        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!MouseOnOff)
                return;
            MEndX = e.X; MEndY = e.Y;

            //아래서 위로 선택시
            if (MStartX > MEndX)
            {
                int imsi;

                imsi = MStartX;
                MStartX = MEndX;
                MEndX = imsi;
            }
            if (MStartY > MEndY)
            {
                int imsi;

                imsi = MStartY;
                MStartY = MEndY;
                MEndY = imsi;
            }
            //드래그를 화면 밖에서 완료할 경우
            if (MEndX < 0)
                MEndX = 0;
            if (MEndY < 0)
                MEndY = 0;
            if (MEndX > inH)
                MEndX = inH;
            if (MEndY > inW)
                MEndY = inW;

            //바꿀 효과

            MouseOnOff = false;
            RangeSelect = true;
        }

        //단축키
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        SaveImg();
                        break;
                    case Keys.O:
                        openImg();
                        break;
                }
            }
        }

        //공통 함수부
        void openImg()
        {
            opFrm = new OpenSelectForm();
            opFrm.ShowDialog();

            inImg = opFrm.o_inImg;  //연 파일 배열
            inH = opFrm.o_inH;      //연 파일 높이
            inW = opFrm.o_inW;      //연 파일 폭

            equal_img();
        }


        private void SaveImg()
        {
            SaveSelectForm svFrm = new SaveSelectForm(outImg, outH, outW);
            svFrm.ShowDialog();

            toolStripStatusLabel1.Text = svFrm.saveFname + "으로 저장됨.";
        }

        void displayImg()
        {
            // 벽, 게시판, 종이 크기 조절
            paper = new Bitmap(outH, outW); // 종이
            pictureBox1.Size = new Size(outH, outW); // 캔버스
            if (outH + 550 < 1050 && outW + 130 < 770)
                this.Size = new Size(1050, 770); // 벽
            else if (outH + 550 < 1050 && outW + 130 >= 770)
                this.Size = new Size(1050, outW + 130);
            else if (outH + 550 >= 1050 && outW + 130 < 770)
                this.Size = new Size(outH + 550, 770);
            else
                this.Size = new Size(outH + 550, outW + 130);


            Color pen; // 펜(콕콕 찍을 용도)
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    byte r = outImg[RR, i, k]; // 잉크(색상값)
                    byte g = outImg[GG, i, k]; // 잉크(색상값)
                    byte b = outImg[BB, i, k]; // 잉크(색상값)

                    pen = Color.FromArgb(r, g, b); // 펜에 잉크 묻히기
                    paper.SetPixel(i, k, pen); // 종이에 콕 찍기
                }
            pictureBox1.Image = paper; // 게시판에 종이를 붙이기.

            toolStripStatusLabel1.Text =    //아랫줄에 파일크기 적기
                outH.ToString() + "x" + outW.ToString() + "  " + opFrm.fileName;
        }
        
        //영상처리 함수부

        void equal_img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        outImg[rgb, i, k] = inImg[rgb, i, k];
                    }
            /////////////////////////////////////////////
            displayImg();
        }

        //화소점 처리

        void bright_image()
        {
            if (inImg == null)
                return;

            BrightForm brightform = new BrightForm(255, -255);
            if (brightform.ShowDialog() == DialogResult.Cancel)
                return;

            int value = (int)brightform.BrightNum.Value;

            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if (inImg[rgb, i, k] + value > 255)
                            outImg[rgb, i, k] = 255;
                        else if (inImg[rgb, i, k] + value < 0)
                            outImg[rgb, i, k] = 0;
                        else
                            outImg[rgb, i, k] = (byte)(inImg[rgb, i, k] + value);
                    }
            /////////////////////////////////////////////
            displayImg();
        }

        //그레이
        void gray_img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향

            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            if (RangeSelect)
            {
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if ((MStartX < i && i < MEndX) && (MStartY < k && k < MEndY))
                        {
                            int hap = inImg[RR, i, k] + inImg[GG, i, k] + inImg[BB, i, k];
                            byte rgb = (byte)(hap / RGB);

                            outImg[RR, i, k] = rgb;
                            outImg[GG, i, k] = rgb;
                            outImg[BB, i, k] = rgb;
                        }
                        else
                            for (int rgb = 0; rgb < RGB; rgb++)
                                outImg[rgb, i, k] = inImg[rgb, i, k];
                    }
                RangeSelect = false;
            }
            else
            {
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        int hap = inImg[RR, i, k] + inImg[GG, i, k] + inImg[BB, i, k];
                        byte rgb = (byte)(hap / RGB);

                        outImg[RR, i, k] = rgb;
                        outImg[GG, i, k] = rgb;
                        outImg[BB, i, k] = rgb;
                    }
            }

            /////////////////////////////////////////////
            displayImg();
        }

        void reverse_img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            if (RangeSelect)
            {
                for (int rgb = 0; rgb < RGB; rgb++)
                    for (int i = 0; i < inH; i++)
                        for (int k = 0; k < inW; k++)
                        {
                            if ((MStartX < i && i < MEndX) && (MStartY < k && k < MEndY))
                                outImg[rgb, i, k] = (byte)(255 - inImg[rgb, i, k]);

                            else
                                outImg[rgb, i, k] = inImg[rgb, i, k];
                        }
                RangeSelect = false;
            }
            else
                for (int rgb = 0; rgb < RGB; rgb++)
                    for (int i = 0; i < inH; i++)
                        for (int k = 0; k < inW; k++)
                            outImg[rgb, i, k] = (byte)(255 - inImg[rgb, i, k]);

            /////////////////////////////////////////////
            displayImg();
        }

        //감마
        void gamma()
        {
            if (inImg == null)
                return;

            GammaForm gammaform = new GammaForm(2, 0);
            if (gammaform.ShowDialog() == DialogResult.Cancel)
                return;

            double value = (double)gammaform.BrightNum.Value;

            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        outImg[rgb, i, k] = (byte)(255 * Math.Pow(((double)inImg[rgb, i, k] / 255), value));
                    }


            /////////////////////////////////////////////
            displayImg();
        }

        //포스터라이징                         //오류 발견 2021-02-08 ~ 2021-02-10 완료
        void posterising()
        {
            if (inImg == null)
                return;

            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            byte cutLevel = 40;       //경계값
            int cut = 255 / cutLevel;   //나눈수


            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    //칼라 -> 흑백 -> 칼라
                    int hap = inImg[RR, i, k] + inImg[GG, i, k] + inImg[BB, i, k];
                    int avg = hap / RGB;                    //흑백 값

                    //구간 나누기
                    for (int m = 0; m <= cut; m++)
                        if (cutLevel * m <= avg && avg < cutLevel * (m + 1))
                        {
                            if (avg < cutLevel)
                            {
                                avg = 0;
                                break;
                            }

                            else
                            {
                                avg = (byte)(cutLevel * m);
                                break;
                            }
                        }
                    

                    //RGB에 비율 정하기
                    double RRate = (double)inImg[RR, i, k] / hap;   //칼라에서 R비율
                    double GRate = (double)inImg[GG, i, k] / hap;   //칼라에서 G비율
                    double BRate = (double)inImg[BB, i, k] / hap;   //칼라에서 B비율

                    outImg[RR, i, k] = (byte)(avg * RGB * RRate);
                    outImg[GG, i, k] = (byte)(avg * RGB * GRate);
                    outImg[BB, i, k] = (byte)(avg * RGB * BRate);

                }

            /////////////////////////////////////////////
            displayImg();
        }

        //범위 강조
        void focus()                //************************적용 안됨 수정 필요
        {
            if (inImg == null)
                return;

            int focusMax = 200;
            int focusMin = 230;

            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;

            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (inImg[rgb, i, k] < focusMax && inImg[rgb, i, k] > focusMin)
                            outImg[rgb, i, k] = 255;
                        else
                            outImg[rgb, i, k] = inImg[rgb, i, k];
                    }
            /////////////////////////////////////////////
            displayImg();
        }
        //채도 변경
        void changeColor_img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            Color c;    //한점 색상 모델
            double hh, ss, vv;  //색상, 채도, 밝기
            int rr, gg, bb;     //RGB

            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    rr = inImg[RR, i, k];
                    gg = inImg[GG, i, k];
                    bb = inImg[BB, i, k];

                    //RGB -> HSV(HSB)변환
                    c = Color.FromArgb(rr, gg, bb);
                    hh = c.GetHue();
                    ss = c.GetSaturation();
                    vv = c.GetBrightness();

                    //(핵심)채도 올리기
                    ss += 0.2;  //채도 0.2 증가

                    //HSV -> RGB 변환
                    HsvToRgb(hh, ss, vv, out rr, out gg, out bb);

                    outImg[RR, i, k] = (byte)rr;
                    outImg[GG, i, k] = (byte)gg;
                    outImg[BB, i, k] = (byte)bb;
                }
            /////////////////////////////////////////////
            displayImg();
        }

        //HSV -> RGB변환 함수 교수님 방식
        void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        //double은 받는 값들, out 은 내보내는 값
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {
                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;
                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;
                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;
                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;
                    default:
                        R = G = B = V;
                        break;
                }
            }
            r = CheckRange((int)(R * 255.0));
            g = CheckRange((int)(G * 255.0));
            b = CheckRange((int)(B * 255.0));

            int CheckRange(int i)
            {
                if (i < 0) return 0;
                if (i > 255) return 255;
                return i;
            }
        }

        //기하학처리
        //확대
        void zoom_in_Img()
        {
            if (inImg == null)
                return;

            double scale = 1.5;

            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = (int)(inH * scale); outW = (int)(inW * scale);
            outImg = new byte[RGB, outH, outW];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        outImg[rgb, i, k] = inImg[rgb, (int)(i / scale), (int)(k / scale)];
                    }
            /////////////////////////////////////////////
            displayImg();
        }

        //축소
        void zoom_out_Img()
        {
            if (inImg == null)
                return;

            double scale = 2;

            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = (int)(inH / scale); outW = (int)(inW / scale);
            outImg = new byte[RGB, outH, outW];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        outImg[rgb, i, k] = inImg[rgb, (int)(i * scale), (int)(k * scale)];
                    }
            /////////////////////////////////////////////
            displayImg();
        }

        //이동
        void move_Img()
        {
            if (inImg == null)
                return;

            int high = 100; //세로
            int row = 150;  //가로

            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;

            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                    {
                        if (i < high || k < row)
                            outImg[rgb, i, k] = 0;
                        else
                            outImg[rgb, i, k] = inImg[rgb, i - high, k - row];
                    }
            /////////////////////////////////////////////
            displayImg();
        }

        void Row_Mirror_Img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                        outImg[rgb, i, k] = inImg[rgb, outH - i - 1, k];
            /////////////////////////////////////////////
            displayImg();
        }



        void High_Mirror_Img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                        outImg[rgb, i, k] = inImg[rgb, i, outW - k - 1];
            /////////////////////////////////////////////
            displayImg();
        }

        

        //화소영역처리
        //엠보싱
        void emboss_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 3;
            double[,] mask = { { -1.0, 0.0, 0.0 },
                               {  0.0, 0.0, 0.0 },
                               {  0.0, 0.0, 1.0 } };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }

            //후처리 : mask의 합이 0이면 127 더하기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpOutput[rgb, i, k] += 127.0;


            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            d = 255;
                        else if (d < 0)
                            d = 0.0;

                        outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        



        //블러링
        void blurr_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 3;
            double[,] mask = { { 1/9.0, 1/9.0, 1/9.0 },
                               { 1/9.0, 1/9.0, 1/9.0 },
                               { 1/9.0, 1/9.0, 1/9.0 } };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }

            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            d = 255;
                        else if (d < 0)
                            d = 0.0;
                        else
                            outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        



        //가우시안
        void gaussian_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 3;
            double[,] mask = { { 1/16.0, 1/8.0, 1/16.0 },
                               {  1/8.0, 1/4.0,  1/8.0 },
                               { 1/16.0, 1/8.0, 1/16.0 } };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }

            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            d = 255;
                        else if (d < 0)
                            d = 0.0;
                        else
                            outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        //샤프닝
        void sharp_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 3;
            double[,] mask = { { -1, -1, -1},
                               { -1,  9, -1},
                               { -1, -1, -1} };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }


            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            outImg[rgb, i, k] = 255;
                        else if (d < 0)
                            outImg[rgb, i, k] = 0;
                        else
                            outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        //고주파 필터링
        void hFilter_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 3;
            double[,] mask = { { -1/9.0, -1/9.0, -1/9.0 },
                               { -1/9.0,  8/9.0, -1/9.0 },
                               { -1/9.0, -1/9.0, -1/9.0 } };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImg[rgb, i, k];



            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }
            //후처리 : mask의 합이 0이면 127 더하기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpOutput[rgb, i, k] += 127.0;

            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            d = 255;
                        else if (d < 0)
                            d = 0.0;
                        else
                            outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        //수직 엣지
        void vecticalEdge_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 3;
            double[,] mask = { {  0, 0, 0},
                               { -1, 1, 0},
                               {  0, 0, 0} };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }

            //후처리 : mask의 합이 0이면 127 더하기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpOutput[rgb, i, k] += 127.0;

            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            outImg[rgb, i, k] = 255;
                        else if (d < 0)
                            outImg[rgb, i, k] = 0;
                        else
                            outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        //수평 엣지
        void horizonEdge_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 3;
            double[,] mask = { { 0, -1, 0},
                               { 0,  1, 0},
                               { 0,  0, 0} };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 2, inW + 2];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 2; i++)
                    for (int k = 0; k < inW + 2; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 1, k + 1] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }

            //후처리 : mask의 합이 0이면 127 더하기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpOutput[rgb, i, k] += 127.0;

            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            outImg[rgb, i, k] = 255;
                        else if (d < 0)
                            outImg[rgb, i, k] = 0;
                        else
                            outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        //5*5
        //엠보싱
        void emboss5_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 5;
            double[,] mask = { { -1.0, 0.0, 0.0, 0.0, 0.0 },
                               {  0.0, 0.0, 0.0, 0.0, 0.0 },
                               {  0.0, 0.0, 0.0, 0.0, 0.0 },
                               {  0.0, 0.0, 0.0, 0.0, 0.0 },
                               {  0.0, 0.0, 0.0, 0.0, 1.0 } };

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 4, inW + 4];  //위아래 2칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값(127)으로 초기화
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 4; i++)
                    for (int k = 0; k < inW + 4; k++)
                        tmpInput[rgb, i, k] = 127.0;
            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 2, k + 2] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        for (int m = 0; m < mSize; m++)
                            for (int n = 0; n < mSize; n++)
                                S += tmpInput[rgb, i + m, k + n] * mask[m, n];
                        tmpOutput[rgb, i, k] = S;
                        S = 0.0;    //S초기화
                    }

            //후처리 : mask의 합이 0이면 127 더하기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpOutput[rgb, i, k] += 127.0;

            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double d = tmpOutput[rgb, i, k];
                        if (d > 255)
                            d = 255;
                        else if (d < 0)
                            d = 0.0;
                        else
                            outImg[rgb, i, k] = (byte)d;
                    }

            /////////////////////////////////////////////
            displayImg();
        }

        //유사 연산자
        void homogen_image()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //화소 여역처리
            //!중요 마스크 결정
            const int mSize = 5;

            //임시 입출력 메모리 할당
            double[,,] tmpInput = new double[RGB, inH + 4, inW + 4];  //위아래 1칸씩 추가공간 마련
            double[,,] tmpOutput = new double[RGB, outH, outW];

            //임시 입력 중간값인 평균 값으로 초기화
            int[] imsiSum = new int[RGB];
            byte[] imsiAvg = new byte[RGB];

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        imsiSum[rgb] += inImg[rgb, i, k];

            for (int rgb = 0; rgb < RGB; rgb++)
                imsiAvg[rgb] = (byte)(imsiSum[rgb] / (inW * inH));

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH + 4; i++)
                    for (int k = 0; k < inW + 4; k++)
                        tmpInput[rgb, i, k] = imsiAvg[rgb];

            //입력 -> 임시 입력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpInput[rgb, i + 2, k + 2] = inImg[rgb, i, k];

            // *** 진짜 영상처리 알고리즘을 구현 ***
            double S = 0.0;

            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double Max = 0;

                        for (int m = 0; m < mSize; m++)
                        {
                            for (int n = 0; n < mSize; n++)
                            {
                                S = Math.Abs(tmpInput[rgb, (i + 2), (k + 2)] - tmpInput[rgb, i + m, k + n]);


                                if (Max < S)
                                    Max = S;
                            }
                        }

                        tmpOutput[rgb, i, k] = Max;
                        S = 0.0;    //S초기화
                    }

            //후처리 : mask의 합이 0이면 127 더하기
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        tmpOutput[rgb, i, k] += 127.0;


            //임시 출력 -> 원래 출력
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        if (tmpOutput[rgb, i, k] > 255)
                            tmpOutput[rgb, i, k] = 255;
                        else if (tmpOutput[rgb, i, k] < 0)
                            tmpOutput[rgb, i, k] = 0.0;
                        else
                            outImg[rgb, i, k] = (byte)tmpOutput[rgb, i, k];
                    }


            /////////////////////////////////////////////
            displayImg();
        }

        //히스토그램
        //히스토그램 그리기
        void draw_Histogram()
        {
            long[] rHisto = new long[256];
            long[] gHisto = new long[256];
            long[] bHisto = new long[256];

            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    rHisto[outImg[RR, i, k]]++;
                    gHisto[outImg[GG, i, k]]++;
                    bHisto[outImg[BB, i, k]]++;
                }

            HistoFormcs hform = new HistoFormcs(rHisto, gHisto, bHisto);

            hform.ShowDialog();
        }
        
        //스트레칭
        void histo_Strech_img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //수식 : outImg = (inImg - min) / (max - min) *255.0
            byte[] min_Value = new byte[RGB];
            byte[] max_Value = new byte[RGB];

            //호환성을 위해 임의값 지정이 아닌 제일 처음값 지정
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                min_Value[rgb] = inImg[rgb, 0, 0];
                max_Value[rgb] = inImg[rgb, 0, 0];

                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {

                        if (min_Value[rgb] > inImg[rgb, i, k])
                            min_Value[rgb] = inImg[rgb, i, k];

                        if (max_Value[rgb] < inImg[rgb, i, k])
                            max_Value[rgb] = inImg[rgb, i, k];
                    }
            }
            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        outImg[rgb, i, k] = (byte)((double)(inImg[rgb, i, k] - min_Value[rgb]) / (max_Value[rgb] - min_Value[rgb]) * 255.0);
                    }
            /////////////////////////////////////////////
            displayImg();
        }

        //앤드인 탐색
        void histo_Endin_img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //수식 : outImg = (inImg - min) / (max - min) *255.0
            byte[] min_Value = new byte[RGB];
            byte[] max_Value = new byte[RGB];
            //호환성을 위해 임의값 지정이 아닌 제일 처음값 지정

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                min_Value[rgb] = inImg[rgb, 0, 0];
                max_Value[rgb] = inImg[rgb, 0, 0];

                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {

                        if (min_Value[rgb] > inImg[rgb, i, k])
                            min_Value[rgb] = inImg[rgb, i, k];

                        if (max_Value[rgb] < inImg[rgb, i, k])
                            max_Value[rgb] = inImg[rgb, i, k];
                    }
            }
            //min,max값을 강제로 변경
            for (int rgb = 0; rgb < RGB; rgb++)
            {
                min_Value[rgb] += 50;
                max_Value[rgb] -= 50;
            }

            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                    {
                        double value = ((double)(inImg[rgb, i, k] - min_Value[rgb]) / (max_Value[rgb] - min_Value[rgb]) * 255.0);
                        if (value > 255)
                            outImg[rgb, i, k] = 255;
                        else if (value < 0)
                            outImg[rgb, i, k] = 0;
                        else
                            outImg[rgb, i, k] = (byte)value;
                    }
            /////////////////////////////////////////////
            displayImg();
        }

        void equalized_img()
        {
            if (inImg == null)
                return;
            // 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
            outH = inH; outW = inW;
            outImg = new byte[RGB, outH, outW];

            //1단계, 히스토그램 생성
            ulong[,] hist = new ulong[RGB, 256];  //ulong 호환을 위해 사용 범위 확대, 최적화시 줄이기
            byte iMax = 255; //inImg[0, 0];    //최대 명도값
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < 256; i++)
                    for (int k = 0; k < 256; k++)
                        hist[rgb, inImg[rgb, i, k]]++;

            //2단계 누적합 생성
            ulong[,] sumHisto = new ulong[RGB,256];
            ulong[] sum_val = new ulong[RGB];

            for (int rgb = 0; rgb < RGB; rgb++)
            {
                sum_val[rgb] = 0;

                for (int i = 0; i < 256; i++)
                {
                    sum_val[rgb] += hist[rgb, i];
                    sumHisto[rgb, i] = sum_val[rgb];
                }
            }
            
            double[] n = new double[256];
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < 256; i++)
                    n[i] = (double)sumHisto[rgb, i] / (outW * outH) * iMax;

            // *** 진짜 영상처리 알고리즘을 구현 ***
            for (int rgb = 0; rgb < RGB; rgb++)
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                        outImg[rgb, i, k] = (byte)n[inImg[rgb, i, k]];
            /////////////////////////////////////////////
            displayImg();
        }
    }

    
    
    
}
