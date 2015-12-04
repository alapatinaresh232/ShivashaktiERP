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
    public partial class CostCenter : Form
    {
        SQLDB objSqlDB = null;
        public MajorCostCenter_Details objMajorCostCenterDetails;
        int CostCenterId = 0;
        DataTable dtCostCenterDel = new DataTable();
        int CountID=0;
        DataRow[] drs;
        public CostCenter()
        {
            InitializeComponent();
        }
        public CostCenter(DataTable dtCostCenterDetails, DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
            //dtCostCenterDel = dtCostCenterDetails;
        }
        public CostCenter(int CountId)
        {
            InitializeComponent();
            CountID = CountId;
        }
      

        private void CostCenter_Load(object sender, EventArgs e)
        {
          
            //GenerateCostCenterId();
            cbCostCenterType.SelectedIndex = 0;
            //txtCostCenterId.Text = Convert.ToString(CountID);
                if (drs != null)
                {
                    txtCostCenterId.Text = drs[0]["CostCenterId"].ToString();
                    txtCCName.Text = drs[0]["CostCenterName"].ToString();
                    txtCCShortName.Text = drs[0]["CostCenterShortName"].ToString();
                    cbCostCenterType.Text = drs[0]["CostCenterType"].ToString();
                    txtOpeningBale.Text = drs[0]["CostCenterOB"].ToString();
                }
        }
        //private int GenerateCostCenterId()
        //{
        //    objSqlDB = new SQLDB();
        //    DataTable dt = null;
        //    int CostCenterId = 0;
        //    string strCommand = "";
        //    try
        //    {
        //        strCommand = "SELECT ISNULL(MAX(CC_COST_CENTRE_ID)+1,'1')  AS CostCenterId FROM FA_COST_CENTRE ";
        //        dt = objSqlDB.ExecuteDataSet(strCommand).Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            CostCenterId = Convert.ToInt32(dt.Rows[0][0].ToString());
        //           txtCostCenterId.Text = Convert.ToString(CostCenterId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {

        //        dt = null;
        //        objSqlDB = null;
        //    }
        //    return CostCenterId;
        //}        

        private void cbCostCenterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCostCenterType.SelectedIndex == 0)
            {
                cbCostCenterType.Tag = "C";
            }
            if (cbCostCenterType.SelectedIndex == 1)
            {
                cbCostCenterType.Tag = "T";
            }
        }
        private bool CheckDetails()
        {
            bool blValue = true;
            if (txtCCName.Text.Length == 0)
            {
                MessageBox.Show("Enter Cost Center Name", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtCCName.Focus();
                return blValue;
            }
            if (txtCCShortName.Text.Length == 0)
            {
                MessageBox.Show("Enter Cost Center Short Name", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtCCShortName.Focus();
                return blValue;
            }
            if (cbCostCenterType.SelectedIndex == -1)
            {
                MessageBox.Show("Add Cost Center Details ", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbCostCenterType.Focus();
                return blValue;
            }
            if(txtOpeningBale.Text.Length==0)
            {
                MessageBox.Show("Enter Opening Balance", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOpeningBale.Focus();
                return blValue;
            }
            return blValue;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckDetails())
            {
                if (drs != null)
                {
                    ((MajorCostCenter_Details)objMajorCostCenterDetails).dtCostCenterDetails.Rows.Remove(drs[0]);
                    
                }

                ((MajorCostCenter_Details)objMajorCostCenterDetails).dtCostCenterDetails.Rows.Add(new Object[] { "-1", Convert.ToInt32(txtCostCenterId.Text.ToString().Trim().Replace("'", "").ToUpper())
                  ,txtCCName.Text.ToString().Trim().Replace("'", "").ToUpper(),txtCCShortName.Text.ToString().Trim().Replace("'", "").ToUpper(),cbCostCenterType.Text.ToString()
                  ,cbCostCenterType.Tag.ToString(),txtOpeningBale.Text.Trim()});

               ((MajorCostCenter_Details)objMajorCostCenterDetails).GetCostCenterDetails();                  

                this.Close();
                //GenerateCostCenterId();

            }

        }
     
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtCCName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtCCShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtCostCenterId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOpeningBale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            OpeningBalances btnBalance = new OpeningBalances(((MajorCostCenter_Details)objMajorCostCenterDetails).txtMajorCCId.Text, txtCostCenterId.Text,CommonData.FinancialYear, CommonData.CompanyCode, "COST");
            btnBalance.ShowDialog();
        }

     
    }
}
