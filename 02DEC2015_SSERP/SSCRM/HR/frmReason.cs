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

namespace SSCRM
{
    public partial class frmReason : Form
    {
        string CompanyCode = "",BranchCode="";
        int iAppliNo;
        public SQLDB objSQLDB;
        public frmApprovedStatus objApprovedStatus;
        public frmReason()
        {
            InitializeComponent();
        }
        public frmReason(string CompCode,string BrCode,int iApplNo)
        {
            InitializeComponent();
            CompanyCode = CompCode;
            BranchCode = BrCode;
            iAppliNo = iApplNo;
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
            try
            {
                objSQLDB = new SQLDB();
                string sReason = "UPDATE HR_APPL_MASTER_HEAD SET HAMH_APPL_PENDING_REASON='" + txtReason.Text + "' WHERE HAMH_COMPANY_CODE='" + CompanyCode + "' AND HAMH_BRANCH_CODE='" + BranchCode + "' and HAMH_APPL_NUMBER=" + iAppliNo;
                int iRetVal = objSQLDB.ExecuteSaveData(sReason);
                if (iRetVal > 0)
                    MessageBox.Show("Reason updated successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Reason Not updated successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objSQLDB = null;
                ((frmApprovedStatus)objApprovedStatus).GetPendingData();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
