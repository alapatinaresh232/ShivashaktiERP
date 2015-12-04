using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;


namespace SSCRM
{
    public partial class AgentApprovalLetters : Form
    {
        private SQLDB objDB = null;
        private int iFormType = 0;
        public AgentApprovalLetters()
        {
            InitializeComponent();
        }
        public AgentApprovalLetters(int iForm)
        {
            iFormType = iForm;
            InitializeComponent();
        }

        private void AgentApprovalLetters_Load(object sender, EventArgs e)
        {
            if (iFormType == 1)
            {
                this.Text = "Employee Salary Structures";
                cbReportType.Visible = false;
                lblrep.Visible = false;
            }                
            else if (iFormType == 2)
            {
                this.Text = "Employee Leave Details";
                cbReportType.Visible = true;
                lblrep.Visible = true;

            }
            else
            {
                this.Text = "Agent Approval Letters";
                cbReportType.Visible = false;
                lblrep.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtEoraCode_num.Text.ToString().Trim().Length > 4)
            {
                if (iFormType == 0)
                {
                    try
                    {
                        objDB = new SQLDB();
                        string sqlText = "";
                        int approvalno = 0;
                        sqlText = "SELECT isnull(HAMH_APPROVAL_NO,0) FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE = '" + txtEoraCode_num.Text + "'";
                        approvalno = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString());
                        if (approvalno > 0)
                        {
                            ReportViewer childForm = new ReportViewer(approvalno);
                            CommonData.ViewReport = "ApprovedDetails";
                            childForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("No Approval Letter Found With " + approvalno, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (iFormType == 1)
                {
                    try
                    {
                        objDB = new SQLDB();
                        string sqlText = "";
                        int approvalno = 0;
                        sqlText = "SELECT isnull(HAMH_APPL_NUMBER,0) FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE = '" + txtEoraCode_num.Text + "'";
                        approvalno = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString());
                        if (approvalno > 0)
                        {
                            ReportViewer childForm = new ReportViewer(approvalno);
                            CommonData.ViewReport = "Employee_Salary_Structure_Details";
                            childForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Salary Structure Not Found" + approvalno, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (iFormType == 2)
                {
                    try
                    {
                        //if (cbReportType.SelectedItem.ToString() == "COFF")
                        //{
                        //    ReportViewer childForm = new ReportViewer(Convert.ToInt32(txtEoraCode_num.Text));
                        //    CommonData.ViewReport = "SSCRM_REP_COFF";
                        //    childForm.Show();
                        //}
                        //else
                        //{
                            ReportViewer childForm = new ReportViewer(Convert.ToInt32(txtEoraCode_num.Text), cbReportType.SelectedItem.ToString());
                            CommonData.ViewReport = "SSCRM_REP_EMP_LEAVE_DETAILS";
                            childForm.Show();
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Incorrect Agent Code", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
