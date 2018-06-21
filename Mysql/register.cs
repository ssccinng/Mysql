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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && (textBox2.Text == textBox3.Text))
            {
                reg();
            }
            else
            {
                MessageBox.Show("部分值为空！");
            }
        }

        void reg()
        {

            MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链
            con.Open();//开启连接
            string strcmd1 = string.Format("select * from account where usename='" + textBox1.Text + "'");
            MySqlCommand cmd = new MySqlCommand(strcmd1, con);
            MySqlDataReader ae = cmd.ExecuteReader();
            if (ae.HasRows)
            {
                MessageBox.Show("重名!");
                return;
            }
            con.Close();

            con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链
            con.Open();
            string strcmd = string.Format("INSERT INTO account (usename,pwd,isadmin,count) VALUES('{0}', '{1}', 0, 0)", textBox1.Text, textBox2.Text);
            MySqlCommand cmd1 = new MySqlCommand(strcmd, con);
            int count = cmd1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("注册成功！");
            this.Close();
        }

        private void register_Load(object sender, EventArgs e)
        {

        }
    }
}
