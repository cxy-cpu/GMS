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
namespace GMS
{
    public partial class FormChoose : Form
    {
        DataSet ds;
        public FormChoose()
        {
            InitializeComponent();
        }
        public MySqlConnection load()   //连接数据库
        {
            string strcon = "Database=dk2;Data Source=120.48.99.11;port=3306;User Id=dk2;Password=5845331588Zjqq@";
            MySqlConnection mysqlcon = new MySqlConnection(strcon);
            mysqlcon.Open();
            return mysqlcon;
        }

        public void Choose()    //选择学生
        {
            using (MySqlConnection mycon = load()) //显示学生列表
            {
                string sql = "select * from table_stu";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, mycon);
                ds = new DataSet();
                da.Fill(ds, "s_choose");
                mycon.Close();
            }
            int length = ds.Tables[0].Rows.Count;
            for (int i = 0; i < length; i++)
            {
                ListViewItem item = new ListViewItem(ds.Tables[0].Rows[i]["stu_name"].ToString());  //ListView的第一个Item作为主项需要单独添加  
                listView1.Items.Add(item);
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            Choose();
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)   //单选及读取学号
        {
            if (e.CurrentValue == CheckState.Unchecked)
            {
                int count = listView1.Items.Count;
                ListViewItem item = listView1.Items[e.Index];
                if (!item.Checked)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i != e.Index)
                        {
                            ListViewItem item1 = listView1.Items[i];
                            item1.Checked = false;
                            item1.Selected = false;
                        }
                        else { listView1.Items[i].Selected = true; }
                    }
                }
            }
            if (e.CurrentValue == CheckState.Checked)
            {
                e.NewValue = CheckState.Unchecked;
                if (listView1.Items[e.Index].Selected)
                {
                    listView1.Items[e.Index].Selected = false;
                }
            }
            int length = listView1.SelectedItems.Count;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(i + "");
            }
            string sid = "" + ds.Tables[0].Rows[e.Index]["stu_id"].ToString();
            string sname = "" + ds.Tables[0].Rows[e.Index]["stu_name"].ToString();
            StuObject.CurrentStu.Stu_Id = sid;
            StuObject.CurrentStu.Stu_Name = sname;
        }
    }
}
