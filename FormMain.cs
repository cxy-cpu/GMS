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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        public  void changePage(Form originPage,Form targetPage)
        {
            targetPage.TopLevel = false;
            this.splitContainer1.Panel2.Controls.Remove(originPage);
            this.splitContainer1.Panel2.Controls.Add(targetPage);
            targetPage.Show();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.CollapseAll();
            e.Node.Expand();
            if (e.Node.Name == "1") //显示学生管理窗体
            {
                this.splitContainer1.Panel2.Controls.Clear();//清空之前的内容
                FormChoose job = new FormChoose();//子窗体
                job.Dock = System.Windows.Forms.DockStyle.Fill;
                job.TopLevel = false;
                this.splitContainer1.Panel2.Controls.Add(job);//加载子窗体
                job.Show();
            }
            if (e.Node.Name == "1.1") //显示学生信息窗体
            {
                this.splitContainer1.Panel2.Controls.Clear();//清空之前的内容
                FormInformation job = new FormInformation(this);//子窗体
                job.Dock = System.Windows.Forms.DockStyle.Fill;
                job.TopLevel = false;
                this.splitContainer1.Panel2.Controls.Add(job);//加载子窗体
                job.Show();
            }
            if (e.Node.Name == "1.3") //显示学生报告窗体
            {
                this.splitContainer1.Panel2.Controls.Clear();//清空之前的内容
                FormReport job = new FormReport(this);//子窗体
                job.Dock = System.Windows.Forms.DockStyle.Fill;
                job.TopLevel = false;
                this.splitContainer1.Panel2.Controls.Add(job);//加载子窗体
                job.Show();
            }
            if (e.Node.Name == "1.4") //显示报告批复窗体
            {
                this.splitContainer1.Panel2.Controls.Clear();//清空之前的内容
                FormReply job = new FormReply(this);//子窗体
                job.Dock = System.Windows.Forms.DockStyle.Fill;
                job.TopLevel = false;
                this.splitContainer1.Panel2.Controls.Add(job);//加载子窗体
                job.Show();
            }
            if (e.Node.Name == "4.2") //显示成员管理窗体
            {
                this.splitContainer1.Panel2.Controls.Clear();//清空之前的内容
                FrmStuList job = new FrmStuList();//子窗体
                job.Dock = System.Windows.Forms.DockStyle.Fill;
                job.TopLevel = false;
                this.splitContainer1.Panel2.Controls.Add(job);//加载子窗体
                job.Show();
            }
        }
        

        private void 主界面_Load(object sender, EventArgs e)
        {

        }
    }
}
