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
    public partial class FrmStuList : Form
    {
        private static bool IsDragging = false; //用于指示当前是不是在拖拽状态
        private Point StartPoint = new Point(0, 0); //记录鼠标按下去的坐标, new是为了拿到空间, 两个0无所谓的
        private Point OffsetPoint = new Point(0, 0);    //记录动了多少距离,然后给窗体Location赋值,要设置Location,必须用一个Point结构体,不能

        private Action reLoad = null;
        //连接数据库
        public static readonly string connString = "Database=dk2;Data Source=120.48.99.11;port=3306;User Id=dk2;Password=5845331588Zjqq@";

        public FrmStuList()
        {
            InitializeComponent();
        }

        private void LoadAllStudentList()
        {
            string sql = "select stu_id,stu_name,stu_sex,stu_phone,stu_direction,teacher_name from table_stu ";

            //加载数据
            DataTable dtStudents = SqlHelper.GetDataTable(sql);
            //绑定数据
            dgvStudent.DataSource = dtStudents;
        }
        private void FrmStuList_Load(object sender, EventArgs e)
        {
            LoadAllStudentList(); //加载所有学生信息
        }

       
        /// <summary>
        /// 执行查询，返回SqlDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteReader(string sql, MySqlParameter paraId, params MySqlParameter[] paras)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paras);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }

        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRow dr = (dgvStudent.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;//获取行数据的绑定对象
                //获取点击的单元格
                DataGridViewCell cell = dgvStudent.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //判断点击的是修改还是删除列
                if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "修改")
                {
                    //修改操作 打开修改页面，并把stuID给传过去
                    //传值 1.构造函数 2.Tag（最优）   3.公有变量（尽量不用)

                    reLoad = LoadAllStudentList;//赋值给委托
                                                //string stuName = txtStuName.Text.Trim();
                    string stuid = dr["stu_id"].ToString();
                    FrmStuEdit frmEdit = new FrmStuEdit();
                    //传值
                    frmEdit.Tag = new TagObject()
                    {
                        StuId = stuid,
                        ReLoad = reLoad
                    };

                    //frmEdit.pubStuId = stuId;
                    frmEdit.MdiParent = this.MdiParent;  //指定修改页面的父容器
                    frmEdit.Show();//顶级窗体   要MDI窗体  

                }
                else if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "删除")
                {
                    if (MessageBox.Show("您确定要删除该学生信息吗？", "删除学生提示",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string stuid = dr["stu_id"].ToString();
                        //真删除 delete  where StuId
                        string sqlDel0 = "delete from table_stu where stu_id=@stu_id";
                        MySqlParameter para = new MySqlParameter("@stu_id", stuid);
                        int count = SqlHelper.ExecuteNonQuery(sqlDel0, para);
                        if (count > 0)
                        {
                            MessageBox.Show("该学生信息删除成功！", "删除学生提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //DataGridView的数据没有刷新，手动刷新
                            DataTable dtStudents = (DataTable)dgvStudent.DataSource;
                            //dgvStudent.DataSource = null;
                            dtStudents.Rows.Remove(dr);
                            dgvStudent.DataSource = dtStudents;
                        }
                        else
                        {
                            MessageBox.Show("该学生信息删除失败！", "删除学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }
    }
}
