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
    public partial class FormReply : Form
    {
        string stu_id = StuObject.CurrentStu.Stu_Id;
        FormMain formOrigin;
        public FormReply(FormMain form)
        {
            InitializeComponent();
            formOrigin = form;
        }

        private void FormReply_Load(object sender, EventArgs e)
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

        }
    }
}
