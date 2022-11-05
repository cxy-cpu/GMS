
namespace GMS
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("基本信息");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("总结报告");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("报告批复");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("学生管理", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("请假审批");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("费用审批");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("其他事务");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("事务管理", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("项目信息");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("项目进度");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("项目报告");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("项目管理", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("成员管理");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("权限管理");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("系统信息");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("系统管理", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(1178, 744);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.HotTracking = true;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "1.1";
            treeNode1.Text = "基本信息";
            treeNode2.Name = "1.3";
            treeNode2.Text = "总结报告";
            treeNode3.Name = "1.4";
            treeNode3.Text = "报告批复";
            treeNode4.Name = "1";
            treeNode4.Text = "学生管理";
            treeNode5.Name = "2.2";
            treeNode5.Text = "请假审批";
            treeNode6.Name = "2.3";
            treeNode6.Text = "费用审批";
            treeNode7.Name = "2.4";
            treeNode7.Text = "其他事务";
            treeNode8.Name = "2";
            treeNode8.Text = "事务管理";
            treeNode9.Name = "3.1";
            treeNode9.Text = "项目信息";
            treeNode10.Name = "3.2";
            treeNode10.Text = "项目进度";
            treeNode11.Name = "3.4";
            treeNode11.Text = "项目报告";
            treeNode12.Name = "3";
            treeNode12.Text = "项目管理";
            treeNode13.Name = "4.2";
            treeNode13.Text = "成员管理";
            treeNode14.Name = "4.3";
            treeNode14.Text = "权限管理";
            treeNode15.Name = "4.4";
            treeNode15.Text = "系统信息";
            treeNode16.Name = "4";
            treeNode16.Text = "系统管理";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8,
            treeNode12,
            treeNode16});
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.Size = new System.Drawing.Size(199, 744);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 744);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMain";
            this.Text = "课题组管理系统-导师端";
            this.Load += new System.EventHandler(this.主界面_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
    }
}

