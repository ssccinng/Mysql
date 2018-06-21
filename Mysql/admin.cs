using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mysql
{
    public partial class admin : Form
    {
        string username;
        string[] wanttoshow = { };
        MySqlDataReader ue;

        public admin(string a)
        {
            this.username = a;
           

            InitializeComponent();
            user.Text = username;
        }
        int page = 1;
        void init()
        {
            
            for (int i = 0; i < 10; ++i)
            {
                
                for (int j = 0; j < 2;++j)
                {
                    news[i, j] = new Label();
                    cz[i, j] = new Button();
                    news[i, j].Location = new Point(140 + j * 100, 90 + i * 20);
                    news[i, j].Width = 90;
                    news[i, j].Height = 20;
                    cz[i, j].Location = new Point(350 + j * 50, 85 + i * 20);
                    cz[i, j].Width = 20;
                    cz[i, j].Height = 20;
                    //news[i, j].Text = (i * j).ToString();
                    news[i, j].Visible = true;
                    cz[i, j].Visible = true;
                    Controls.Add(news[i, j]);
                    Controls.Add(cz[i, j]);
                }
                cz[i, 0].Click += edit_Click;
                cz[i, 1].Click += del_Click;
            }
            MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接
            con.Open();//开启连接
            string strcmd = string.Format("select * from account where usename='{0}'", username);
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            ue = cmd.ExecuteReader();
            ue.Read();
            updata();
        }

        void updata()
        {

            MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接

            con.Open();//开启连接
            // where usename
            string strcmd = string.Format("select * from article" + ((int.Parse(ue["isadmin"].ToString()) == 0) ? " where author='" + ((string)ue["usename"]) + "'" : ""));
            MySqlCommand cmd = new MySqlCommand(strcmd, con);

            MySqlDataReader ae = cmd.ExecuteReader();
            //MySqlDataAdapter ada = new MySqlDataAdapter(cmd);

            //DataSet aa = new DataSet();
            string test = "";

            int index = 0;
            ae.Read();
            while (ae.HasRows)
            {

                test += (string)ae["title"] + "," + (string)ae["author"] + "," + ae["id"].ToString();

                if (ae.Read()) { test += ";"; }
                else break;
            }
            wanttoshow = test.Split(';');
            //wanttoshow[0] = "4465464,4654,654";
            pageupdate();
            con.Close();
        }

        private void del_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接
            con.Open();
            string strcmd = "DELETE FROM article WHERE id =" + (((int)((Button)sender).Tag) / 20).ToString();
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            cmd.ExecuteNonQuery();
            con.Close();
            updata();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            int a = ((int)((Button)sender).Tag);
            new eaitnew(news[a % 20, 0].Text, username, a / 20).ShowDialog();
        }

        void pageupdate()
        {
            if (wanttoshow[0] == "")
            {
                wanttoshow = new string[0];
            }
            label1.Text = string.Format("第{0}页 共{1}页", page, (wanttoshow.Length - 1) / 10 + 1);
            for (int i = 0; i < 10; ++i)
            {
                if ((page - 1) * 10 + i >= wanttoshow.Length)
                {
                    news[i, 0].Enabled = news[i, 1].Enabled = cz[i, 0].Enabled = cz[i, 1].Enabled = false;
                    news[i, 0].Visible = news[i, 1].Visible = cz[i, 0].Visible = cz[i, 1].Visible = false;
                }
                else
                {
                    news[i, 0].Enabled = news[i, 1].Enabled = cz[i, 0].Enabled = cz[i, 1].Enabled = true;
                    news[i, 0].Visible = news[i, 1].Visible = cz[i, 0].Visible = cz[i, 1].Visible = true;

                    string[] aa = wanttoshow[10 * (page - 1) + i].Split(',');
                    cz[i, 0].Tag = cz[i, 1].Tag = int.Parse(aa[2]) * 20 + i;
                    for (int k = 0; k < 2; ++k)
                    {
                        news[i, k].Text = aa[k];
                    }
                }
            }
        }

        //string str = ;
        Label[,] news = new Label[10, 2];

        Button[,] cz = new Button[10, 2];
        private void admin_Load(object sender, EventArgs e)
        {
            init();
        }

        private void create_Click(object sender, EventArgs e)
        {
            new eaitnew(username).ShowDialog();
            updata();

        }
    }
}
