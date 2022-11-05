using GMS;
using GMS.util;
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
    public partial class FormReport : Form
    {
        FormMain formOrigin;
        public FormReport(FormMain form)
        {
            InitializeComponent();
            formOrigin = form;
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            //获取所有周数
            InitWeeks();
            //timer获取当前系统时间
            timer1.Interval = 1000; //1s
            timer1.Start();
            if(!("".Equals(StuObject.CurrentStu.Stu_Id))&&!(null==StuObject.CurrentStu.Stu_Id))
            {
                toolStripLabel6.Text = StuObject.CurrentStu.Stu_Id;
                toolStripLabel8.Text = StuObject.CurrentStu.Stu_Name;
            }
            else
            {
                MessageBox.Show("请选择学生!");
                formOrigin.changePage(this, new FormChoose());
                
            }
        }

        /// <summary>
        /// 显示周数
        /// </summary>
        private void InitWeeks()
        {
            string sql = " select distinct(dk_week_no) from table_dk_main where dk_week_no is not null order by dk_week_no";
            DataTable dataTable = SqlHelper.GetDataTable(sql);
            cboWeeks.ComboBox.DataSource = dataTable;
            cboWeeks.ComboBox.DisplayMember = "dk_week_no";
        }

        /// <summary>
        /// 显示天数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select dk_week_of_day from table_dk_main where dk_week_no = @dk_week_no order by dk_week_of_day ";
            MySqlParameter para = new MySqlParameter("@dk_week_no", cboWeeks.Text);
            DataTable dataTable = SqlHelper.GetDataTable(sql, para);
            cboDays.ComboBox.DataSource = dataTable;
            cboDays.ComboBox.DisplayMember = "dk_week_of_day";
            //添加一行项
            DataRow dataRow = dataTable.NewRow();
            dataRow["dk_week_of_day"] = 0;//添加一行"0"项
            dataTable.Rows.InsertAt(dataRow, 0);//插入到第一个
            cboDays.SelectedIndex = 0;//默认选第一个
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.SelectedItem.ToString());
        }

        /// <summary>
        /// 查看报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string stu_id = null;
        private string stu_name = null;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            //任务情况-----------------------------------------------------------------------------------------------------------------------------
            string fileUrl = "";
      
            string sql = "select * from table_tasks a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no and dk_week_of_day = @dk_week_of_day ";
            string sql1 = "select * from table_tasks a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no order by dk_week_of_day";
            MySqlParameter[] paras =
            {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
                new MySqlParameter("@dk_week_of_day",cboDays.Text)
            };
            MySqlParameter[] paras1 =
           {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
            };
            //查询某一周的每一天
            if (cboDays.SelectedIndex == 0)
            {
                DataTable data1 = SqlHelper.GetDataTable(sql1, paras1);
                int length = data1.Rows.Count;
                if (length > 0)
                {
                    listBox1.Items.Add($"第{cboWeeks.Text}周");
                    for (int i = 0; i < length; i++)
                    {
                        listBox1.Items.Add($"第{data1.Rows[i]["dk_week_of_day"]}天的任务为:{data1.Rows[i]["task_content"]}\n");
                    }
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl = data1.Rows[0]["task_completion_content"].ToString();
                        if (fileUrl.Contains("7.zhangjian.link"))
                        {
                            richTextBox1.LoadFile(HttpUtil.HttpDownload(fileUrl), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox1.Text = "获取数据有误:" + fileUrl;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl = data1.Rows[i]["task_completion_content"].ToString();
                            if (fileUrl.Contains("7.zhangjian.link"))
                            {
                                richTextBox1.AppendText("\n---------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox1.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox1.Text = "获取数据有误:" + fileUrl;
                            }
                        }
                    }
                }
            }
            //查询某一周的某一天
            else
            {
                DataTable data = SqlHelper.GetDataTable(sql, paras);
                int length = data.Rows.Count;
                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        listBox1.Items.Add($"第{cboDays.Text}天的任务为:{data.Rows[i]["task_content"]}\n");
                    }
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl = data.Rows[0]["task_completion_content"].ToString();
                        if (fileUrl.Contains("7.zhangjian.link"))
                        {
                            richTextBox1.LoadFile(HttpUtil.HttpDownload(fileUrl), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox1.Text = "获取数据有误:" + fileUrl;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl = data.Rows[i]["task_completion_content"].ToString();
                            if (fileUrl.Contains("7.zhangjian.link"))
                            {
                                richTextBox1.AppendText("\n---------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox1.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox1.Text = "获取数据有误:" + fileUrl;
                            }
                        }
                    }
                }
            }
            //学习问题汇总-----------------------------------------------------------------------------------------------------------------------------
            string fileUrl1 = "";
            string sql2 = "select * from table_problems a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no and dk_week_of_day = @dk_week_of_day ";
            string sql3 = "select * from table_problems a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no order by dk_week_of_day";
            MySqlParameter[] paras2 =
            {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
                new MySqlParameter("@dk_week_of_day",cboDays.Text)
            };
            MySqlParameter[] paras3 =
           {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
            };
            //查询某一周的每一天
            if (cboDays.SelectedIndex == 0)
            {
                DataTable data1 = SqlHelper.GetDataTable(sql3, paras3);
                int length = data1.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl1 = data1.Rows[0]["problems_content"].ToString();
                        if (fileUrl1.Contains("7.zhangjian.link"))
                        {
                            richTextBox2.LoadFile(HttpUtil.HttpDownload(fileUrl1), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox2.Text = "获取数据有误:" + fileUrl1;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl1 = data1.Rows[i]["problems_content"].ToString();
                            if (fileUrl1.Contains("7.zhangjian.link"))
                            {
                                richTextBox2.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl1), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox2.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox2.Text = "获取数据有误:" + fileUrl1;
                            }
                        }
                    }
                }
            }
            //查询某一周的某一天
            else
            {
                DataTable data = SqlHelper.GetDataTable(sql2, paras2);
                int length = data.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl1 = data.Rows[0]["problems_content"].ToString();
                        if (fileUrl1.Contains("7.zhangjian.link"))
                        {
                            richTextBox2.LoadFile(HttpUtil.HttpDownload(fileUrl1), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox2.Text = "获取数据有误:" + fileUrl1;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl1 = data.Rows[i]["problems_content"].ToString();
                            if (fileUrl1.Contains("7.zhangjian.link"))
                            {
                                richTextBox2.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl1), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox2.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox2.Text = "获取数据有误:" + fileUrl1;
                            }
                        }
                    }
                }
            }
            //问题解决情况-----------------------------------------------------------------------------------------------------------------------------
            string fileUrl2 = "";
            string sql4 = "select * from table_problems a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no and dk_week_of_day = @dk_week_of_day ";
            string sql5 = "select * from table_problems a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no order by dk_week_of_day";
            MySqlParameter[] paras4 =
            {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
                new MySqlParameter("@dk_week_of_day",cboDays.Text)
            };
            MySqlParameter[] paras5 =
           {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
            };
            //查询某一周的每一天
            if (cboDays.SelectedIndex == 0)
            {
                DataTable data1 = SqlHelper.GetDataTable(sql5, paras5);
                int length = data1.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl2 = data1.Rows[0]["problem_completion_content"].ToString();
                        if (fileUrl2.Contains("7.zhangjian.link"))
                        {
                            richTextBox3.LoadFile(HttpUtil.HttpDownload(fileUrl2), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox3.Text = "获取数据有误:" + fileUrl2;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl2 = data1.Rows[i]["problem_completion_content"].ToString();
                            if (fileUrl2.Contains("7.zhangjian.link"))
                            {
                                richTextBox3.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl2), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox3.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox3.Text = "获取数据有误:" + fileUrl2;
                            }
                        }
                    }
                }
            }
            //查询某一周的某一天
            else
            {
                DataTable data = SqlHelper.GetDataTable(sql4, paras4);
                int length = data.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl2 = data.Rows[0]["problem_completion_content"].ToString();
                        if (fileUrl2.Contains("7.zhangjian.link"))
                        {
                            richTextBox3.LoadFile(HttpUtil.HttpDownload(fileUrl2), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox3.Text = "获取数据有误:" + fileUrl2;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl2 = data.Rows[i]["problem_completion_content"].ToString();
                            if (fileUrl2.Contains("7.zhangjian.link"))
                            {
                                richTextBox3.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl2), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox3.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox3.Text = "获取数据有误:" + fileUrl2;
                            }
                        }
                    }
                }
            }
            //本周专业收获-----------------------------------------------------------------------------------------------------------------------------
            string fileUrl3 = "";
            string sql6 = "select * from table_gains a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no and dk_week_of_day = @dk_week_of_day ";
            string sql7 = "select * from table_gains a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no order by dk_week_of_day";
            MySqlParameter[] paras6 =
            {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
                new MySqlParameter("@dk_week_of_day",cboDays.Text)
            };
            MySqlParameter[] paras7 =
           {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
            };
            //查询某一周的每一天
            if (cboDays.SelectedIndex == 0)
            {
                DataTable data1 = SqlHelper.GetDataTable(sql7, paras7);
                int length = data1.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl3 = data1.Rows[0]["gains_content"].ToString();
                        if (fileUrl3.Contains("7.zhangjian.link"))
                        {
                            richTextBox4.LoadFile(HttpUtil.HttpDownload(fileUrl3), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox4.Text = "获取数据有误:" + fileUrl3;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl3 = data1.Rows[i]["gains_content"].ToString();
                            if (fileUrl3.Contains("7.zhangjian.link"))
                            {
                                richTextBox4.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl3), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox4.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox4.Text = "获取数据有误:" + fileUrl3;
                            }
                        }
                    }
                }
            }
            //查询某一周的某一天
            else
            {
                DataTable data = SqlHelper.GetDataTable(sql6, paras6);
                int length = data.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl3 = data.Rows[0]["gains_content"].ToString();
                        if (fileUrl3.Contains("7.zhangjian.link"))
                        {
                            richTextBox4.LoadFile(HttpUtil.HttpDownload(fileUrl3), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox4.Text = "获取数据有误:" + fileUrl3;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl3 = data.Rows[i]["gains_content"].ToString();
                            if (fileUrl3.Contains("7.zhangjian.link"))
                            {
                                richTextBox4.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl3), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox4.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox4.Text = "获取数据有误:" + fileUrl3;
                            }
                        }
                    }
                }
            }
            //文献阅读理解-----------------------------------------------------------------------------------------------------------------------------
            string fileUrl4 = "";
            string sql8 = "select * from table_literatures a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no and dk_week_of_day = @dk_week_of_day ";
            string sql9 = "select * from table_literatures a JOIN table_dk_main b on(a.dk_id = b.dk_id) " +
                "where stu_id = @stu_id and dk_week_no = @dk_week_no order by dk_week_of_day";
            MySqlParameter[] paras8 =
            {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
                new MySqlParameter("@dk_week_of_day",cboDays.Text)
            };
            MySqlParameter[] paras9 =
           {
                new MySqlParameter("@stu_id",StuObject.CurrentStu.Stu_Id),
                new MySqlParameter("@dk_week_no",cboWeeks.Text),
            };
            //查询某一周的每一天
            if (cboDays.SelectedIndex == 0)
            {
                DataTable data1 = SqlHelper.GetDataTable(sql9, paras9);
                int length = data1.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl4 = data1.Rows[0]["literature_understand"].ToString();
                        string literature_name = data1.Rows[0]["literature_name"].ToString();
                        string literature_author = data1.Rows[0]["literature_author"].ToString();
                        if (fileUrl4.Contains("7.zhangjian.link"))
                        {
                            richTextBox5.AppendText("\n" + literature_name+"——"+literature_author + "\n");
                            richTextBox5.LoadFile(HttpUtil.HttpDownload(fileUrl4), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox5.Text = "获取数据有误:" + fileUrl4;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl4 = data1.Rows[i]["literature_understand"].ToString();
                            string literature_name = data1.Rows[i]["literature_name"].ToString();
                            string literature_author = data1.Rows[i]["literature_author"].ToString();
                            if (fileUrl4.Contains("7.zhangjian.link"))
                            {
                                richTextBox5.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                richTextBox5.AppendText("\n" + literature_name + "——" + literature_author + "\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl4), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox5.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox5.Text = "获取数据有误:" + fileUrl4;
                            }
                        }
                    }
                }
            }
            //查询某一周的某一天
            else
            {
                DataTable data = SqlHelper.GetDataTable(sql8, paras8);
                int length = data.Rows.Count;
                if (length > 0)
                {
                    //如果数据库查出来只有一个文件，那么直接显示
                    if (length == 1)
                    {
                        fileUrl4 = data.Rows[0]["literature_understand"].ToString();
                        if (fileUrl4.Contains("7.zhangjian.link"))
                        {
                            richTextBox5.LoadFile(HttpUtil.HttpDownload(fileUrl4), RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            richTextBox5.Text = "获取数据有误:" + fileUrl4;
                        }
                    }
                    //否则每个文件遍历合并
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            fileUrl4 = data.Rows[i]["literature_understand"].ToString();
                            if (fileUrl4.Contains("7.zhangjian.link"))
                            {
                                richTextBox5.AppendText("\n-------------------------------------------------------------------------------------------------------\n");
                                RichTextBox rtfSecond = new RichTextBox();
                                rtfSecond.LoadFile(HttpUtil.HttpDownload(fileUrl4), RichTextBoxStreamType.RichText);
                                rtfSecond.Select(rtfSecond.Rtf.Length, 0);
                                richTextBox5.SelectedRtf = rtfSecond.Rtf;
                            }
                            else
                            {
                                richTextBox5.Text = "获取数据有误:" + fileUrl4;
                            }
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripLabel10.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
