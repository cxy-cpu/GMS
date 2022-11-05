using GMS.utils;
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

namespace GMS
{
    public partial class FrmStuEdit : Form
    {
        public static readonly string connString = "Database=dk2;Data Source=120.48.99.11;port=3306;User Id=dk2;Password=5845331588Zjqq@";
        private string stu_id = null;
        private Action reLoad = null;
        public FrmStuEdit()
        {
            InitializeComponent();
        }

        private void FrmStuEdit_Load(object sender, EventArgs e)
        {
            //加载学生信息
            InitStuInfo();
        }
        private void InitStuInfo()
        {
            //获取到Sid
            if (this.Tag != null)
            {
                TagObject tagObject = (TagObject)this.Tag;
                stu_id = tagObject.StuId;
                reLoad = tagObject.ReLoad;//赋值
            }
            //查询数据
            string sql = "select * from table_stu where stu_id=@stu_id";
            MySqlParameter paraId = new MySqlParameter("@stu_id", stu_id);
            MySqlDataReader dr = SqlHelper.ExecuteReader(sql, paraId);
            //读取数据  只能向前 不能后退 读一条丢一条
            if (dr.Read())
            {
                txtSid.Text = dr["stu_id"].ToString();
                txtStuName.Text = dr["stu_name"].ToString();
                txt_Direction.Text = dr["stu_direction"].ToString();
                string sex = dr["stu_sex"].ToString();

                txtTeacher.Text = dr["teacher_name"].ToString();
                if (sex == "男")
                    rbtMale.Checked = true;
                else
                    rbtFemale.Checked = true;

            }
            dr.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //获取页面信息
            string stuName = txtStuName.Text.Trim();
            string stuid = txtSid.Text.Trim();
            string sex = rbtMale.Checked ? rbtMale.Text : rbtFemale.Text;//三目运算符，只能选中一个性别
            string phone = txtPhone.Text.Trim();
            string direction = txt_Direction.Text.Trim();
            string teacher = txtTeacher.Text.Trim();
            //判空处理
            if (string.IsNullOrEmpty(stuName))
            {
                MessageBox.Show("姓名不能为空!", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(stuid))
            {
                MessageBox.Show("学号不能为空!", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断是否存在 姓名+电话 除这个同学自己，其他同学中是否存在
            string sql = "select count(1) from table_stu where stu_name=@stu_name  and stu_id=@stu_id and stu_direction=@stu_direction";
            MySqlParameter[] paras =
            {
                new MySqlParameter("@stu_name",stuName),
                new MySqlParameter("@stu_direction",direction),
                new MySqlParameter("@stu_id",stuid),
                new MySqlParameter("@teacher_name",teacher)
            };
            //object o = FrmEditStudent.ExecuteScalar(sql, paras);
            //if (o != null && o != DBNull.Value && ((int)o) > 0)
            //{
            //    MessageBox.Show("该学生已经存在!", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            // 修改
            string sqlUpdate = "Update table_stu set stu_name=@stu_name ,stu_direction=@stu_direction,stu_phone=@stu_phone," +
                              " stu_sex=@stu_sex where stu_id=@stu_id";
            MySqlParameter[] parasUpdate =
            {
                new MySqlParameter("@stu_name",stuName),
                new MySqlParameter("@stu_id",stuid),
                new MySqlParameter("@stu_sex",sex),
                new MySqlParameter("@stu_phone",phone),
                new MySqlParameter("@stu_direction",direction),
                new MySqlParameter("@teacher_name",teacher),
            };
            int count = SqlHelper.ExecuteNonQuery(sqlUpdate, parasUpdate);
            if (count > 0)
            {
                MessageBox.Show($"学生:{stuName}修改成功!", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //提示成功后，刷新学生列表 跨页面刷新 使用委托   列表页面定义委托，列表页面加载数据列表这个方法赋值给委托，同时传给修改页面；
                //修改页面定义委托，吧传过来的委托赋值给本页面定义的委托，修改成功后，调用委托
                reLoad.Invoke();
            }
            else
            {
                MessageBox.Show("该学生修改失败，请检查!", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
