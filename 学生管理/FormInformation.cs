using MySql.Data.MySqlClient;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GMS
{
    public partial class FormInformation : Form
    {
        string stu_id = StuObject.CurrentStu.Stu_Id;
        FormMain formOrigin;

        public FormInformation(FormMain form)
        {
            InitializeComponent();
            formOrigin = form;
        }

        public MySqlConnection load()   //连接数据库
        {
            string strcon = "Database=dk2;Data Source=120.48.99.11;port=3306;User Id=dk2;Password=5845331588Zjqq@";
            MySqlConnection mysqlcon = new MySqlConnection(strcon);
            mysqlcon.Open();
            return mysqlcon;
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            if (!("".Equals(stu_id)) && !(null == stu_id))
            {
                Display();
            }
            else
            {
                MessageBox.Show("请选择学生!");
                formOrigin.changePage(this, new FormChoose());
            }
        }

        private void Display()  //信息数据显示
        {
            using (MySqlConnection mycon1 = load()) //个人数据显示
            {
                string sql1 = "select * from table_stu where stu_id='" + stu_id + "'";
                MySqlCommand cmd1 = new MySqlCommand(sql1, mycon1);
                MySqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = stu_id;
                    textBox2.Text = dr["stu_name"].ToString();
                    textBox3.Text = dr["stu_sex"].ToString();
                    textBox4.Text = dr["stu_hometown"].ToString();
                    textBox5.Text = dr["stu_phone"].ToString();
                    textBox6.Text = dr["stu_school"].ToString();
                    textBox7.Text = dr["stu_direction"].ToString();
                    textBox8.Text = dr["stu_introduce"].ToString();
                }
                mycon1.Close();
            }
        }
    }
}
