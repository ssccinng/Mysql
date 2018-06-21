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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool check(MySqlDataReader ae)
        {
            while(ae.HasRows)
            {
                ae.Read();
                if ((string)ae["pwd"] == textBox2.Text)
                {
                    return true;
                }
                ae.NextResult();
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            // where usename
            string strcmd = string.Format("select * from account where usename=\"{0}\"", textBox1.Text);
            MySqlCommand cmd = new MySqlCommand(strcmd, con);

            MySqlDataReader ae = cmd.ExecuteReader();
            if (!ae.HasRows)
            {
                MessageBox.Show("账号不存在!");
                    return;
            }
            if (check(ae))
            {
                Form1.islogin = true;
                Form1.username = textBox1.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("密码有误!");
            }

            //label1.Text = ae["usename"].ToString();
        }

        private void login_Load(object sender, EventArgs e)
        {
            
            //var a = 
        }
    }
}
