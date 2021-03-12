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
using MySql.Data.MySqlClient;

namespace Project01
{
    public partial class DBForm : Form
    {
        public String saveFname;
        public byte[,,] o_inImg;
        public int o_inH, o_inW;
        byte[,,] S_outImg;
        int S_outH, S_outW;
        const int RGB = 3, RR = 0, GG = 1, BB = 2;  //RGB 갯수, RGB 순서0,1,2

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
        Bitmap bitmap;

        

        public DBForm(byte[,,] m_outImg, int m_outH, int m_outW)
        {
            S_outImg = m_outImg;
            S_outH = m_outH;
            S_outW = m_outW;
            InitializeComponent();

        }

        private void DBForm_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            cmd = new MySqlCommand("", conn);


            //콤보박스리스트에 목록 채워 넣기
            sql = "SELECT f_id, f_name, f_extname, f_fsize FROM blob_table"; // 짐 싸기
            cmd.CommandText = sql;  // 짐을 트럭에 싣기
            reader = cmd.ExecuteReader(); // 짐을 서버에 부어넣고, 끈으로 묶어서 끈만 가져옴.
            String f_name, f_extname; // 톡!하고 땡겨올 짐을 담을 변수.
            int f_id;
            long f_fsize;
            // 끈을 톡!하고 당기기
            string[] file_list = { };

            while (reader.Read())
            {
                f_id = (int)reader["f_id"];
                f_name = (String)reader["f_name"];
                f_extname = (string)reader["f_extname"];
                f_fsize = (long)reader["f_fsize"];

                //파일 고유명 / 파일이름.확장자 / 파일크기
                string str = f_id + "/" + f_name + "." + f_extname;
                str += "/" + f_fsize;
                Array.Resize(ref file_list, file_list.Length + 1);  //배열 크기 1개 증가(배열타입 신경 x)
                file_list[file_list.Length - 1] = str;

            }

            reader.Close();
            comboBox1.Items.AddRange(file_list);    //리스트에 추가
            //추가 완료


            //temp폴더 비우기
        }

        private void DBForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }

        private void BTN_DownLoad_from_DB_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("파일이 없습니다.");
                return;
            }

            //DB에서 불러오기
            string selectStr = comboBox1.SelectedItem.ToString();
            int f_id = int.Parse(selectStr.Split('/')[0]);

            sql = "SELECT f_id, f_name, f_extname, f_fsize, f_data FROM blob_table WHERE f_id = " + f_id;

            cmd.CommandText = sql;  // 짐을 트럭에 싣기
            reader = cmd.ExecuteReader(); // 짐을 서버에 부어넣고, 끈으로 묶어서 끈만 가져옴.
            //DB에서 불러올 준비 완료


            //메모리상에 파일 가져오기
            reader.Read();  //한건만 조회됨
            string f_name = reader["f_name"].ToString();
            string f_extname = reader["f_extname"].ToString();
            int f_fsize = int.Parse(reader["f_fsize"].ToString());
            byte[] f_data = new byte[f_fsize];
            reader.GetBytes(reader.GetOrdinal("f_data"), 0, f_data, 0, f_fsize);
            //메모리상에 파일 가져온 상태

            //C:\Windows\\Temp
            //임시 파일 생성
            string full_name = "C:\\img\\" + f_name + "." + f_extname;      //경로\\파일명.확장자
            FileStream fs = new FileStream(full_name, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(f_data, 0, (int)f_fsize);
            fs.Close();
            //임시파일 생성 종료


            //이미지 출력 준비
            //파일 -> 비트맵(bitmap)과정
            bitmap = new Bitmap(full_name);      //Bitmap(경로 + 파일명.확장자)
            // 중요! 입력이미지의 높이, 폭 알아내기
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
            //이미지 출력 준비 종료

            reader.Close();
            if (System.IO.File.Exists(full_name))
            {
                try
                {
                    System.IO.File.Delete(full_name);
                }
                catch(System.IO.IOException )
                {

                }
                    
            }

            this.DialogResult = DialogResult.OK;
        }

        private void BTN_openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            String full_name = ofd.FileName;
            textBox1.Text = full_name;
        }

        private void BTN_UpLoad_to_DB_Click(object sender, EventArgs e)
        {
            /*
                CREATE TABLE blob_table(
	                f_id INT NOT NULL PRIMARY KEY,	    -- 파일 순서
	                -- UUID, GUID (MySQL, C#에서 사용) char(36) 중복이 안되는 문자 랜덤 생성
	                f_name VARCHAR(50) NOT NULL,		-- 이름
	                f_extname VARCHAR(10) NOT NULL,	    -- 확장자
                	f_fsize BIGINT NOT NULL,			-- 사이즈
                	f_data LONGBLOB 				    -- 데이터
                );
             */

            //파일 경로에서 파일 이름, 확장자 추출
            String full_name = textBox1.Text.ToString();
            // c:\\images\\pet_raw\\cat256_01.raw
            String[] tmp = full_name.Split('\\');
            String tmp1 = tmp[tmp.Length - 1];  // cat256_01.raw
            String[] tmp2 = tmp1.Split('.');
            

            String i_fname;
            if (TB_FileName.Text == "")
                i_fname = tmp2[0]; // cat256_01
            else
                i_fname = TB_FileName.Text;

            String i_extname = tmp2[1]; // raw
            //추출 완료


            long i_fsize = new FileInfo(full_name).Length;
            Random rnd = new Random();
            int i_id = rnd.Next(int.MinValue, int.MaxValue);
            // 이미지 테이블(부모 테이블)에 Insert
            // <3> 물건을 준비해서, 트럭에 실어서 다리 건너 부어넣기.
            sql = "INSERT INTO blob_table(f_id, f_name, f_extname, f_fsize, f_data) VALUES (";
            sql += i_id + ", '" + i_fname + "', '" + i_extname + "', " + i_fsize + ",";
            sql += "@BLOB_DATA" + ")";  //@나중에 집어넣겠다.

            //파일 준비
            FileStream fs = new FileStream(full_name, FileMode.Open, FileAccess.Read);
            byte[] blob_data = new byte[i_fsize];   //파일크기만큼 배열 준비
            fs.Read(blob_data, 0, (int)i_fsize);    //파일을 0번 배열부터 i_size 만큼 읽기
            fs.Close();

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@BLOB_DATA", blob_data);   //앞에 문장에 뒤에꺼를 이어붙여라

            cmd.CommandText = sql;  // 짐을 트럭에 싣기
            reader = cmd.ExecuteReader(); // 짐을 서버에 부어넣고, 끈으로 묶어서 끈만 가져옴.



            this.DialogResult = DialogResult.OK;
        }
    }
}
