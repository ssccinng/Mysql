using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Mysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool islogin = false;
        public static string username = "";
        //public static bool islogin = true;
        int page = 1;
        string[] wanttoshow = { };
        //string str = ;
        Label[,] news = new Label[10, 3];

        void init()
        {
            for (int i = 0; i < 10; ++i)
            {

                for (int j = 0; j < 3;++j)
                {
                    news[i, j] = new Label();
                    news[i, j].Location =  new Point(50 + j * 200, 70 + i * 20);
                    news[i, j].Width = 90;
                    news[i, j].Height = 20;
                    //news[i, j].Text = (i * j).ToString();
                    news[i, j].Visible = true;
                    Controls.Add(news[i, j]);
                }
                news[i, 0].Click += Form1_Click;
            }
            pageupdate();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Newscs a = new Newscs((int)(((Label)sender).Tag) / 20);
            MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接
            con.Open();
            int redt = (int.Parse(news[(((int)((Label)sender).Tag)) % 20, 2].Text) + 1);
            string strcmd = "update article set readtime =" + redt.ToString() + " where id=" + ((int)((Label)sender).Tag / 20).ToString();
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            int count = cmd.ExecuteNonQuery();
            news[(((int)((Label)sender).Tag)) % 20, 2].Text = (redt).ToString();

            con.Close();
            a.ShowDialog();

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            update();
            init();
            updata();

            //ada.Fill(aa);
        }

        void updata()
        {
            MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接

            con.Open();//开启连接
            // where usename
            string strcmd = string.Format("select * from article");
            MySqlCommand cmd = new MySqlCommand(strcmd, con);

            MySqlDataReader ae = cmd.ExecuteReader();
            //MySqlDataAdapter ada = new MySqlDataAdapter(cmd);

            //DataSet aa = new DataSet();
            string test = "";

            int index = 0;
            ae.Read();
            while (ae.HasRows)
            {

                test += (string)ae["title"] + "," + (string)ae["author"] + "," + ae["readtime"].ToString() + "," + ae["id"].ToString();

                //ae.NextResult();
                //ae.ne
                if (ae.Read()) test += ";";
                else break;
            }
            wanttoshow = test.Split(';');
            //wanttoshow[0] = "4465464,4654,654";
            pageupdate();
            con.Close();
        }

        private void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login a = new login();
            if (!a.Visible)
            {
                a.ShowDialog();
                update();
            }
            
        }

        public void update()
        {
            loginmenu.Enabled = !islogin;
            exitmenu.Enabled = adminmenu.Enabled = islogin;
        }

        private void exitmenu_Click(object sender, EventArgs e)
        {
            islogin = false;
            username = "";
            update();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (page > 1)
            {
                page--;
                pageupdate();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (page < (wanttoshow.Length - 1) / 10 + 1)
            {
                page++;
                pageupdate();
            }
        }

        void pageupdate()
        {
            label1.Text = string.Format("第{0}页 共{1}页", page, (wanttoshow.Length - 1) / 10 + 1);
            for (int i = 0; i < 10; ++i)
            {
                if ((page - 1) * 10 + i >= wanttoshow.Length)
                {
                    news[i, 0].Enabled = news[i, 1].Enabled = news[i, 2].Enabled = false;
                    news[i, 0].Visible = news[i, 1].Visible = news[i, 2].Visible = false;
                }
                else
                {
                    news[i, 0].Enabled = news[i, 1].Enabled = news[i, 2].Enabled = true;
                    news[i, 0].Visible = news[i, 1].Visible = news[i, 2].Visible = true;

                    string[] aa = wanttoshow[10 * (page - 1) + i].Split(',');
                    news[i, 0].Tag = int.Parse(aa[3]) * 20 + i;
                    for (int k = 0; k < 3; ++k)
                    {
                        news[i, k].Text = aa[k];
                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void 注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new register().ShowDialog(); ;

        }

        private void adminmenu_Click(object sender, EventArgs e)
        {
            new admin(username).ShowDialog();
            updata();
        }
    }

}
