
namespace GMS
{
    partial class FrmStuList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvStudent = new System.Windows.Forms.DataGridView();
            this.stu_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stu_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stu_sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stu_phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stu_direction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Delete = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStudent
            // 
            this.dgvStudent.AllowUserToAddRows = false;
            this.dgvStudent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvStudent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvStudent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stu_id,
            this.stu_name,
            this.stu_sex,
            this.stu_phone,
            this.stu_direction,
            this.teacher_name,
            this.Edit,
            this.Delete});
            this.dgvStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudent.Location = new System.Drawing.Point(0, 0);
            this.dgvStudent.Name = "dgvStudent";
            this.dgvStudent.RowHeadersVisible = false;
            this.dgvStudent.RowHeadersWidth = 51;
            this.dgvStudent.RowTemplate.Height = 27;
            this.dgvStudent.Size = new System.Drawing.Size(990, 409);
            this.dgvStudent.TabIndex = 1;
            this.dgvStudent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudent_CellContentClick);
            // 
            // stu_id
            // 
            this.stu_id.DataPropertyName = "stu_id";
            this.stu_id.HeaderText = "学号";
            this.stu_id.MinimumWidth = 6;
            this.stu_id.Name = "stu_id";
            // 
            // stu_name
            // 
            this.stu_name.DataPropertyName = "stu_name";
            this.stu_name.HeaderText = "姓名";
            this.stu_name.MinimumWidth = 6;
            this.stu_name.Name = "stu_name";
            // 
            // stu_sex
            // 
            this.stu_sex.DataPropertyName = "stu_sex";
            this.stu_sex.HeaderText = "性别";
            this.stu_sex.MinimumWidth = 6;
            this.stu_sex.Name = "stu_sex";
            // 
            // stu_phone
            // 
            this.stu_phone.DataPropertyName = "stu_phone";
            this.stu_phone.HeaderText = "手机";
            this.stu_phone.MinimumWidth = 6;
            this.stu_phone.Name = "stu_phone";
            // 
            // stu_direction
            // 
            this.stu_direction.DataPropertyName = "stu_direction";
            this.stu_direction.HeaderText = "研究方向";
            this.stu_direction.MinimumWidth = 6;
            this.stu_direction.Name = "stu_direction";
            // 
            // teacher_name
            // 
            this.teacher_name.DataPropertyName = "teacher_name";
            this.teacher_name.HeaderText = "导师姓名";
            this.teacher_name.MinimumWidth = 6;
            this.teacher_name.Name = "teacher_name";
            // 
            // Edit
            // 
            dataGridViewCellStyle3.NullValue = "修改";
            this.Edit.DefaultCellStyle = dataGridViewCellStyle3;
            this.Edit.HeaderText = "修改";
            this.Edit.MinimumWidth = 6;
            this.Edit.Name = "Edit";
            this.Edit.Text = "修改";
            // 
            // Delete
            // 
            dataGridViewCellStyle4.NullValue = "删除";
            this.Delete.DefaultCellStyle = dataGridViewCellStyle4;
            this.Delete.HeaderText = "删除";
            this.Delete.MinimumWidth = 6;
            this.Delete.Name = "Delete";
            // 
            // FrmStuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 409);
            this.Controls.Add(this.dgvStudent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStuList";
            this.Text = "FrmStuList";
            this.Load += new System.EventHandler(this.FrmStuList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStudent;
        private System.Windows.Forms.DataGridViewTextBoxColumn stu_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn stu_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn stu_sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn stu_phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn stu_direction;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_name;
        private System.Windows.Forms.DataGridViewLinkColumn Edit;
        private System.Windows.Forms.DataGridViewLinkColumn Delete;
    }
}