using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;
using SSCRMDB;

namespace SSCRM
{
    public partial class LicenceDetails : Form
    {
        public StockPointDetails objStockPointDetl;
        DataRow[] drs;
        private SQLDB objSQLDB = null;
        public LicenceDetails()
        {
            InitializeComponent();
        }

        public LicenceDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        private void LicenceDetails_Load(object sender, EventArgs e)
        {
            cmbLicType_optional.SelectedIndex = 0;
            cmbLicStatus_optional.SelectedIndex = 0;
            cmbWorkStatus.SelectedIndex = 0;
            dtVldFrm.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtVldTo.Value = Convert.ToDateTime(CommonData.CurrentDate);
            if (drs != null)
            {
                cmbLicType_optional.Text = drs[0][2].ToString();
                cmbLicStatus_optional.Text = drs[0][6].ToString();
                txtLicenceNo.Text = drs[0][3].ToString();
                dtVldFrm.Value = Convert.ToDateTime(drs[0][4]);
                dtVldTo.Value = Convert.ToDateTime(drs[0][5]);
                txtEcode.Text = drs[0][7].ToString();
                txtEmpName.Text = drs[0][8].ToString();
                cmbWorkStatus.SelectedItem = drs[0][9].ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(groupBox6, toolTip1))
                return;
            if (drs != null)
                ((StockPointDetails)objStockPointDetl).dtLicence.Rows.Remove(drs[0]);
            int iCnt = ((StockPointDetails)objStockPointDetl).dtLicence.Rows.Count;
            ((StockPointDetails)objStockPointDetl).dtLicence.Rows.Add(new Object[] { iCnt + 1, "", cmbLicType_optional.Text, txtLicenceNo.Text, dtVldFrm.Value, dtVldTo.Value, cmbLicStatus_optional.Text, txtEcode.Text,txtEmpName.Text,cmbWorkStatus.SelectedItem });
            ((StockPointDetails)objStockPointDetl).GetLiceince();
            this.Close();
        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcode.Text.Length > 4)
            {
                GetEmpName(txtEcode.Text);                
            }
            else
            {
                txtEmpName.Text = "";                
            }
        }

        private void GetEmpName(string strEcode)
        {
            
            DataTable EmpData = new DataTable();
            try
            {
                objSQLDB = new SQLDB();
                string strSql = "SELECT CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME NAME,DESIG,EMP_DOJ,FATHER_NAME FROM EORA_MASTER WHERE ECODE = " + strEcode;
                EmpData = objSQLDB.ExecuteDataSet(strSql).Tables[0];
                if(EmpData.Rows.Count>0)
                    txtEmpName.Text = EmpData.Rows[0]["NAME"].ToString();
                else
                    txtEmpName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            
        }
    }
}
