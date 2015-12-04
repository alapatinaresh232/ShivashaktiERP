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
    public partial class frmShortCourse : Form
    {
        public frmApplication objApplication;
        DataRow[] drs;
        public frmShortCourse()
        {
            InitializeComponent();
        }
        public frmShortCourse(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShortCourse_Load(object sender, EventArgs e)
        {
            txtPerofMarks_percent_optional.Text = "50";
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 300, Screen.PrimaryScreen.WorkingArea.Y + 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            if (drs != null)
            {
                txtCoursename.Text = drs[0]["CourseName"].ToString();
                txtYearpass.Text = drs[0]["YearofPass"].ToString();
                txtInstName.Text = drs[0]["Insti_name"].ToString();
                txtInstLocation.Text = drs[0]["Insti_Location"].ToString();
                txtSubject.Text = drs[0]["Subject"].ToString();
                txtDuration.Text = drs[0]["Duration"].ToString();
                txtPerofMarks_percent_optional.Text = drs[0]["PerofMarks"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
                return;
            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((frmApplication)objApplication).dtShortCourse.Rows.Remove(drs[0]);
            //till here
            double pers = 0;
            if (txtPerofMarks_percent_optional.Text.Length > 0)
                pers = Convert.ToDouble(txtPerofMarks_percent_optional.Text);
            ((frmApplication)objApplication).dtShortCourse.Rows.Add(new Object[] { "-1", txtCoursename.Text, txtYearpass.Text, txtInstName.Text, txtInstLocation.Text, txtSubject.Text, txtDuration.Text, pers.ToString("f") });
            ((frmApplication)objApplication).GetDGVShortCourse();
            this.Close();
        }
    }
}
