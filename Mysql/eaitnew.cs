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
    public partial class eaitnew : Form
    {
        int mode = 0;
        public eaitnew(string author)
        {
            this.author = author;

            InitializeComponent();
        }
        string author;
        int id;
        string title;
        public eaitnew(string title,  string author, int id)
        {
            this.author = author;
            this.id = id;
            this.title = title;
            mode = 1;
            InitializeComponent();
        }

        void init()
        {
            
            
            MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接
            con.Open();
            string strcmd = string.Format("select * from article where id={0}", id);

            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataReader ae = cmd.ExecuteReader();
            ae.Read();
            textBox1.Text = ae["content"].ToString();
            con.Close();
            textBox2.Text = title;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void confirm_Click(object sender, EventArgs e)
        {
            if (mode == 0)
            {
                MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接
                con.Open();
                string strcmd = string.Format("INSERT INTO article(title,content,author,date)VALUES('{0}','{1}','{2}','1970/01/01');", textBox2.Text, textBox1.Text, author);

                MySqlCommand cmd = new MySqlCommand(strcmd, con);

                cmd.ExecuteNonQuery();
                con.Close();
                Close();
            }
            else
            {
                MySqlConnection con = new MySqlConnection("Server=localhost;User ID=root;Password=123456;Database=test;CharSet=gbk");//实例化链接
                con.Open();
                string strcmd = string.Format("update article set title = '{0}',content = '{1}',author = '{2}' where id ={3}", textBox2.Text, textBox1.Text, author, id);

                MySqlCommand cmd = new MySqlCommand(strcmd, con);

                cmd.ExecuteNonQuery();
                con.Close();
                Close();
            }
        }

        private void quxiao_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void eaitnew_Load(object sender, EventArgs e)
        {
            if (mode == 1) init();
        }
    }
}
