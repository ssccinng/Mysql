using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mysql
{
    public partial class Newscs : Form
    {
        int id;
        public Newscs(int id)
        {
            this.id = id;
            InitializeComponent();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Newscs_Load(object sender, EventArgs e)
        {
            string str = "Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            // where usename
            string strcmd = string.Format("select * from article where id={0}", id);
            MySqlCommand cmd = new MySqlCommand(strcmd, con);

            MySqlDataReader ae = cmd.ExecuteReader();
            if (ae.HasRows) 
            ae.Read();
            
            textBox1.Text = (string)ae["content"];
            label1.Text = (string)ae["title"];
            label2.Text = string.Format("作者:{0}            浏览次数:{1}", ae["author"], ae["readtime"]);

        }
    }
}
