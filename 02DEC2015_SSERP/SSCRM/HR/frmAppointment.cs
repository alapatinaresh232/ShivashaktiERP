using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
using SSTrans;

namespace SSCRM
{
    public partial class frmAppointment : Form
    {
        public SQLDB objSQLDB;
        public frmApplication objApplication;
        public HRInfo objHrInfo;
        public frmAppointment()
        {
            InitializeComponent();
        }
        public string CompanyCode = "", BranchCode = "", EoraOCde = "";
        public int iApplNo = 0;

        public frmAppointment(string CCode, string BCode, int ApplNo, string ECode)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            iApplNo = ApplNo;
            EoraOCde = ECode;
        }

        private void frmAppointment_Load(object sender, EventArgs e)
        {
            TextAuto();
            objSQLDB = new SQLDB();
            DataView dvDesg = objSQLDB.ExecuteDataSet("SELECT Branch_code,Branch_name From BRANCH_MAS", CommandType.Text).Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbBranch, dvDesg, 1, 0, "--PLEASE SELECT--", 0);

            lblEoraNo.Text = EoraOCde;
            string sAppint = "SELECT * FROM HR_APPL_APPOINTMENT A INNER JOIN HR_APPL_MASTER_HEAD B ON A.HRAP_APPL_NUMBER=B.HAMH_APPL_NUMBER WHERE  HAMH_EORA_TYPE='E' AND  HRAP_APPL_NUMBER=" + iApplNo;
            DataTable dt = objSQLDB.ExecuteDataSet(sAppint, CommandType.Text).Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtApplNo.Text = dt.Rows[0]["HRAP_LTR_REF_NO"].ToString();
                dtpRefDt.Value = Convert.ToDateTime(dt.Rows[0]["HRAP_LTR_REF_DATE"]);
                txtReportto.Text = dt.Rows[0]["HRAP_REPO_TO_ECODE"].ToString();
                cmbBranch.SelectedValue = dt.Rows[0]["HRAP_REPO_TO_BRANCH_CODE"].ToString();
                dtpEffectedDt.Value = Convert.ToDateTime(dt.Rows[0]["HRAP_EFF_DATE"]);
                txtBasic_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_BASIC"]).ToString("0.00");
                txtHRA_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_HRA"]).ToString("0.00");
                txtCCA_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_CCA"]).ToString("0.00");
                txtConv_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_CONV_ALW"]).ToString("0.00");
                txtSpl_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_SPL_ALW"]).ToString("0.00");
                txtUnif_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_UNF_ALW"]).ToString("0.00");
                txtChild_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_CH_ED_ALW"]).ToString("0.00");
                txtMedical_num.Text = Convert.ToDecimal(dt.Rows[0]["HRAP_MED_REIMB"]).ToString("0.00");
            }
            else
            {
                string sVal = "";
                if (System.DateTime.Now.Month <= 3)
                    sVal = System.DateTime.Now.AddYears(-1).ToString("yy") + "-" + System.DateTime.Now.ToString("yy");
                else
                    sVal = CommonData.FinancialYear;
                    //sVal = System.DateTime.Now.ToString("yy") + "-" + System.DateTime.Now.ToString("yy");
                txtApplNo.Text = CompanyCode + "/HR/" + sVal + "/APPT/" + EoraOCde;
            }
            objSQLDB = null;
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            objHrInfo = new HRInfo();
            try
            {
                string sRet = "";
                string sAppint = "SELECT COUNT(*) FROM dbo.HR_APPL_APPOINTMENT A INNER JOIN HR_APPL_MASTER_HEAD B ON A.HRAP_APPL_NUMBER=B.HAMH_APPL_NUMBER WHERE HRAP_APPL_NUMBER=" + iApplNo;
                DataTable dt = objSQLDB.ExecuteDataSet(sAppint, CommandType.Text).Tables[0];
                if (dt.Rows[0][0].ToString() == "0")
                {
                    string strAppoint = CompanyCode + "," + BranchCode + "," + iApplNo + "," + EoraOCde + "," + txtApplNo.Text + "," + dtpRefDt.Value + "," +
                        dtpEffectedDt.Value + "," + txtReportto.Text.Split('-')[0].ToString() + "," + cmbBranch.SelectedValue + "," + txtBasic_num.Text + "," +
                        txtHRA_num.Text + "," + txtCCA_num.Text + "," + txtConv_num.Text + "," + txtSpl_num.Text + "," + txtUnif_num.Text + "," + txtChild_num.Text + "," + txtMedical_num.Text + "," + CommonData.LogUserId;
                    sRet = objHrInfo.SaveAppointment(101, strAppoint.Split(','));

                }
                else
                {
                    string SReportto = "";
                    if (txtReportto.Text.Contains('-'))
                        SReportto = txtReportto.Text.Split('-')[0].ToString();
                    else
                        SReportto = txtReportto.Text;

                    string strAppoint = CompanyCode + "," + BranchCode + "," + iApplNo + "," + EoraOCde + "," + txtApplNo.Text + "," + dtpRefDt.Value + "," +
                        dtpEffectedDt.Value + "," + SReportto + "," + cmbBranch.SelectedValue + "," + txtBasic_num.Text + "," +
                        txtHRA_num.Text + "," + txtCCA_num.Text + "," + txtConv_num.Text + "," + txtSpl_num.Text + "," + txtUnif_num.Text + "," + txtChild_num.Text + "," + txtMedical_num.Text + "," + CommonData.LogUserId;
                    objHrInfo = new HRInfo();
                    sRet = objHrInfo.SaveAppointment(102, strAppoint.Split(','));
                }
                if (sRet == "Saved")
                    MessageBox.Show("This record inserted successfully.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (sRet == "Update")
                    MessageBox.Show("This record Updated successfully.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show(sRet, "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult dlgResult = MessageBox.Show("Do you want print Appointment letter?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    ReportViewer childForm = new ReportViewer(iApplNo);
                    CommonData.ViewReport = "AppointmentLetter";
                    childForm.Show();
                }
                ResetFields(this);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {                
                objHrInfo = null;
                objSQLDB = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void ResetFields(Control form)
        {
            foreach (Control ctrl in form.Controls)
            {
                if (ctrl.Controls.Count > 0)
                    ResetFields(ctrl);
                Reset(ctrl);
            }
        }

        public static void Reset(Control ctrl)
        {
            if (ctrl is TextBox)
            {
                TextBox tb = (TextBox)ctrl;
                if (tb != null)
                {
                    tb.ResetText();
                }
            }
            else if (ctrl is ComboBox)
            {
                ComboBox dd = (ComboBox)ctrl;
                if (dd != null)
                {
                    dd.SelectedIndex = 0;
                }
            }
        }

        public void TextAuto()
        {
            objHrInfo = new HRInfo();
            DataTable dt = objHrInfo.GetNameandEcode().Tables[0];
            objHrInfo = null;
            AutoCompleteStringCollection local = new AutoCompleteStringCollection();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                local.Add(dt.Rows[i]["Data"].ToString());
            }
            txtReportto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtReportto.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtReportto.AutoCompleteCustomSource = local;
        }

        private void txtHRA_num_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBasic_num_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtConv_num_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCCA_num_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
