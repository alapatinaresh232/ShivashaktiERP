using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class frmEducation : Form
    {
        public frmApplication objApplication;
        DataRow[] drs;
        public frmEducation()
        {
            InitializeComponent();
        }
        public frmEducation(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void frmEducation_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 300, Screen.PrimaryScreen.WorkingArea.Y + 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            cmbTypeEdu.SelectedIndex = 0;
            if (drs != null)
            {
                txtExamPassed.Text = drs[0]["ExamPass"].ToString();
                cmbTypeEdu.SelectedIndex = cmbTypeEdu.Items.IndexOf(drs[0]["ExamType"].ToString());
                txtyearofpass_num.Text = drs[0]["YearPass"].ToString();
                txtInst_Name.Text = drs[0]["InstName"].ToString();
                txtInstLocation.Text = drs[0]["InstLocation"].ToString();
                txtSubject.Text = drs[0]["Subject"].ToString();
                txtUniversity.Text = drs[0]["University"].ToString();
                txtPerMarks_percent.Text = drs[0]["PerofPass"].ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
                return;
            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((frmApplication)objApplication).dtEducation.Rows.Remove(drs[0]);
            //till here
            ((frmApplication)objApplication).dtEducation.Rows.Add(new Object[] { "-1", txtExamPassed.Text, cmbTypeEdu.Text, txtyearofpass_num.Text, txtInst_Name.Text, txtInstLocation.Text, txtSubject.Text, txtUniversity.Text, txtPerMarks_percent.Text });
            ((frmApplication)objApplication).GetDGVEducation();
            this.Close();
        }

        private void txtyearofpass_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
