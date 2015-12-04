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
    public partial class frmLeftInfo : Form
    {
        public frmViewDetails objfrmViewDetails;
        SQLDB objSQLDB;
        public frmLeftInfo()
        {
            InitializeComponent();
        }
        public string CompanyCode = "", BranchCode = "", sEcode = "";
        public int AppliNo = 0;
        public frmLeftInfo(string CCode, string BCode, int applno, string Ecode)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            AppliNo = applno;
            sEcode = Ecode;
        }
        private void frmLeftInfo_Load(object sender, EventArgs e)
        {
            dtpLeftdt.Value = System.DateTime.Now;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            DialogResult dlgResult = MessageBox.Show("Do you want save the left this person?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                string strLastMapped = objSQLDB.ExecuteDataSet("exec Get_Last_Mapped '" + sEcode + "'").Tables[0].Rows[0][0].ToString();
                if (strLastMapped != "")
                {
                    
                    if (Convert.ToDateTime(dtpLeftdt.Value) > Convert.ToDateTime(strLastMapped))
                    {
                        double dDays = (Convert.ToDateTime(dtpLeftdt.Value) - Convert.ToDateTime(strLastMapped)).TotalDays;
                        //if (dDays > 30)
                        //{
                            int iRetVal = 0;
                            try
                            {
                                string sReason = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_LEFT_REASON='" + txtReason.Text +
                                    "',HAMH_WORKING_STATUS='L',HAMH_LEFT_DATE='" + Convert.ToDateTime(dtpLeftdt.Value).ToString("dd/MMM/yyyy") +
                                    "',HAMH_LEFT_APPROVAL_ECODE=" + CommonData.LogUserEcode +
                                    ", HAMH_LEFT_UPDATED_DATE=GETDATE() WHERE HAMH_APPL_NUMBER=" + AppliNo + ";";
                                sReason += " UPDATE EORA_MASTER SET EORA = 'L' WHERE ECODE =" + sEcode;
                                sReason += " EXEC Amsbd_BioTransfer_InsDel_OD " + sEcode + ", '', 'DEL_IN_ALL_DEV'";
                                iRetVal = objSQLDB.ExecuteSaveData(sReason);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            finally
                            {
                                objSQLDB = null;
                            }
                            if (iRetVal > 0)
                                MessageBox.Show("Reason updated successfully.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("Reason Not update.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ((frmViewDetails)objfrmViewDetails).GetDataBuind();
                            this.Close();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Employee Mapped in "+ Convert.ToDateTime(strLastMapped).ToString("MMMyyyy") +"\nCannot Update to Left", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Employee Mapped in " + Convert.ToDateTime(strLastMapped).ToString("MMMyyyy") + "\nCannot Update to Left", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    int iRetVal = 0;
                    try
                    {
                        string sReason = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_LEFT_REASON='" + txtReason.Text +
                            "',HAMH_WORKING_STATUS='L',HAMH_LEFT_DATE='" + Convert.ToDateTime(dtpLeftdt.Value).ToString("dd/MMM/yyyy") +
                            "',HAMH_LEFT_APPROVAL_ECODE=" + CommonData.LogUserEcode +
                            " WHERE HAMH_APPL_NUMBER=" + AppliNo + ";";
                        sReason += " UPDATE EORA_MASTER SET EORA = 'L' WHERE ECODE =" + sEcode;
                        iRetVal = objSQLDB.ExecuteSaveData(sReason);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objSQLDB = null;
                    }
                    if (iRetVal > 0)
                        MessageBox.Show("Reason updated successfully.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Reason Not update.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ((frmViewDetails)objfrmViewDetails).GetDataBuind();
                    this.Close();
                }
            }
        }
    }
}
