using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class AddReplacementDetails : Form
    {
        ServiceDB objServiceDB;
        
        DataSet ds;
        public ActivityServiceUpdate objActivityServiceUpdate;
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;
        SQLDB objSQLdb;
        private string BranchCode = "", ActivityName = "", ProductName = "", Qty = "", strActQty = "", sOrderNo = "",sEcode="",sActivityDate="";
        private Int32 ActivityID = 0;
      
       DataTable dtActDetl = new DataTable();
       string DeptCode = "800000";

        public AddReplacementDetails()
        {
            InitializeComponent();
        }
      
        public AddReplacementDetails(DataTable dtInvDetl,string sActName,Int32 nActId,string strEcode,string strDate)
        {
            InitializeComponent();
            dtActDetl = dtInvDetl;
            ActivityID = nActId;
            ActivityName = sActName;
            sEcode = strEcode;
            sActivityDate = strDate;
            BranchCode = dtActDetl.Rows[0]["BranCode"].ToString();

            txtServiceSearch.Text = sEcode;
            if (sEcode.Length > 1)
                txtServiceSearch_KeyUp(null, null);
            if (sEcode != "")
            {
                cmbEmployee.SelectedValue = sEcode;
            }

            if (sActivityDate.Length > 0)
            {
                txtServiceSearch.ReadOnly = true;
                cmbEmployee.Enabled = false;
                dtpActivityDate.Enabled = false;
                dtpActivityDate.Value = Convert.ToDateTime(sActivityDate);
            }
            else
            {
                txtServiceSearch.ReadOnly = false;
                cmbEmployee.Enabled = true;
                dtpActivityDate.Enabled = true;
            }

            if (Convert.ToString(ActivityID).Equals("20"))
            {
                lblQty.Text = "Counting Qty";
                lblApprBy.Visible = false;
                txtEcodeSearch.Visible = false;
                txtEName.Visible = false;
                this.Text = "Counting Details";
            }
            else
            {
                this.Text = "Replacement Details";
                lblQty.Text = "Replaced Qty";
                lblApprBy.Visible = true;
                txtEcodeSearch.Visible = true;
                txtEName.Visible = true;
            }
        }

        private void AddReplacementDetails_Load(object sender, EventArgs e)
        {
            objServiceDB = new ServiceDB();
          
            string strCmd = "";
            DataTable dt = new DataTable();
            ds = objServiceDB.GetECodesforService(BranchCode, DeptCode);
            FillCompanyData();
            cbCompany.Enabled = false;
            cmbBranch.Enabled = false;
            txtProdName.ReadOnly = true;
            cmbFinYear.Enabled = false;
            txtActivityName.ReadOnly = true;
            txtActualQty.ReadOnly = true;
            txtInvNo.ReadOnly = true;
            dtpActivityDate.Value = DateTime.Today;
                     
            objServiceDB = null;
            UtilityLibrary.PopulateControl(cmbBranch, ds.Tables[1].DefaultView, 1, 0, "-- Please Select --", 0);

            objSQLdb = new SQLDB();
            if (dtActDetl.Rows.Count>0)
            {
                objSQLdb = new SQLDB();
                strCmd = "SELECT TNA_COMPANY_CODE "+
                          ",TNA_BRANCH_CODE, TNA_QTY "+
                          ",TNA_ORDER_NUMBER,TNA_INVOICE_NUMBER "+
                          ",(TNA_QTY-sum(TNA_ACTIVITY_QTY)) as Qty  "+
                          " FROM SERVICES_TNA "+
                          " WHERE TNA_COMPANY_CODE='"+ CommonData.CompanyCode +
                          "' AND TNA_BRANCH_CODE='"+dtActDetl.Rows[0]["BranCode"].ToString()+
                          "' AND TNA_INVOICE_NUMBER="+ dtActDetl.Rows[0]["InvNo"].ToString() +
                          " AND TNA_ATTEND_BY_ECODE IS not null "+
                          " and TNA_PRODUCT_ID='TKPRYL0000' and TNA_ACTIVITY_ID IN ('13','18','19') " +
                          " GROUP BY TNA_COMPANY_CODE,TNA_BRANCH_CODE,TNA_QTY,TNA_ORDER_NUMBER,TNA_INVOICE_NUMBER";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtActualQty.Text = dt.Rows[0]["Qty"].ToString();
                }
                else
                {
                    txtActualQty.Text = dtActDetl.Rows[0]["Qty"].ToString();
                }
                
                cmbBranch.SelectedValue = dtActDetl.Rows[0]["BranCode"].ToString();
                cmbBranch_SelectedIndexChanged(null, null);
                txtInvNo.Text = dtActDetl.Rows[0]["InvNo"].ToString();
                txtProdName.Text = dtActDetl.Rows[0]["ProdName"].ToString();
                txtActivityName.Text = ActivityName;              
                cmbFinYear.Text = dtActDetl.Rows[0]["FinYear"].ToString();            
                                           
            }
        }                        

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }
                cbCompany.SelectedValue = CommonData.CompanyCode;
                dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
        }


        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                GetECodeData(cmbBranch.SelectedValue.ToString());
            }
        }

      
        public void GetECodeData(string BranchCode)
        {
            objServiceDB = new ServiceDB();
           
            DataSet dsData = objServiceDB.GetECodesforService(BranchCode,DeptCode);
            objServiceDB = null;
            UtilityLibrary.PopulateControl(cmbEmployee, dsData.Tables[0].DefaultView, 1, 0, "-- Please select --", 0);
            //if (sCheckVal == "1")
            //{
            //    if (sEcode != "")
            //        cmbEmployee.SelectedValue = sEcode.Split('-')[0].ToString();
            //}            
        }


        private bool CheckData()
        {
            bool flag = true;
            double dProdQty = 0, dActQty = 0;

            if (cmbEmployee.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Employee", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEmployee.Focus();
                return flag;
            }
            if (dtpActivityDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Activity Date Should Be Less than toDay's Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpActivityDate.Focus();
                return flag;
            }
            if (txtQty.Text != "")
                dActQty = Convert.ToDouble(txtQty.Text);
            if (txtActualQty.Text != "")
                dProdQty = Convert.ToDouble(txtActualQty.Text);

            //if (Convert.ToString(ActivityID).Equals("20"))
            //{
            //}
            //else
            //{
            //    if (dActQty > dProdQty)
            //    {
            //        flag = false;
            //        MessageBox.Show("Replaced Quantity less than or equal to Purchased Quantity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtQty.Focus();
            //        return flag;
            //    }
            //}
            if (Convert.ToString(ActivityID).Equals("20"))
            {
            }
            else
            {
                if (txtEName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Approved Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtEcodeSearch.Focus();
                    return flag;
                }
            }

            return flag;

        }
        
   
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRetVal = 0;
            string strCmd = "";
            if (CheckData() == true)
            {
                try
                {
                    if (txtQty.Text.Length == 0)
                    {
                        txtQty.Text = "0";
                    }
                    if (txtEName.Text.Length == 0)
                    {
                        txtEcodeSearch.Text = "0";
                    }

                    strCmd = "INSERT INTO SERVICES_TNA(TNA_COMPANY_CODE " +
                                                    ", TNA_STATE_CODE " +
                                                    ", TNA_BRANCH_CODE " +
                                                    ", TNA_FIN_YEAR " +
                                                    ", TNA_DOC_MONTH " +
                                                    ", TNA_ORDER_NUMBER " +
                                                    ", TNA_INVOICE_NUMBER " +
                                                    ", TNA_INVOICE_SL_NO " +
                                                    ", TNA_PRODUCT_ID " +
                                                    ", TNA_ACTIVITY_ID " +
                                                    ", TNA_QTY " +                                                    
                                                    ", TNA_ACTUAL_DATE " +
                                                    ", TNA_ATTEND_BY_ECODE " +
                                                    ", TNA_FARMER_REMARKS " +
                                                    ", TNA_ACTIVITY_QTY " +
                                                    ", TNA_CREATED_BY "+
                                                    ", TNA_CREATED_DATE "+
                                                    ",TNA_TARGET_DATE "+
                                                    ",TNA_REPL_APPROVED_BY "+
                                                    ",TNA_FARMER_OPINION "+
                                                    ",TNA_AO_SUGGESTION "+
                                                    ")values " +
                                                    "('"+cbCompany.SelectedValue.ToString() +
                                                    "','"+ dtActDetl.Rows[0]["StateCode"].ToString() +
                                                    "','" + dtActDetl.Rows[0]["BranCode"].ToString() +
                                                    "','" + dtActDetl.Rows[0]["FinYear"].ToString() +
                                                    "','" + dtActDetl.Rows[0]["DocMonth"].ToString() +
                                                    "','" + dtActDetl.Rows[0]["OrderNo"].ToString() +
                                                    "'," + Convert.ToInt32(dtActDetl.Rows[0]["InvNo"]) +
                                                    "," + Convert.ToInt32(dtActDetl.Rows[0]["InvSlNo"]) +
                                                    ",'" + dtActDetl.Rows[0]["ProdId"].ToString() + 
                                                    "',"+Convert.ToInt32(ActivityID)+
                                                    "," + Convert.ToDouble(dtActDetl.Rows[0]["Qty"].ToString()) +
                                                    ",'"+Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MMM/yyyy") +
                                                    "'," + Convert.ToInt32(cmbEmployee.SelectedValue)+ 
                                                    ",'"+ txtRemarks.Text.ToString().Replace("'"," ") +
                                                    "',"+ Convert.ToDouble(txtQty.Text).ToString("0.00") +
                                                    ",'"+ CommonData.LogUserId +
                                                    "',getdate() "+
                                                    ",'" + Convert.ToDateTime(dtActDetl.Rows[0]["TargetDate"]).ToString("dd/MMM/yyyy") + 
                                                    "',"+ Convert.ToInt32(txtEcodeSearch.Text) +
                                                    ",'"+ txtFarmerOpinion.Text.ToString().Replace("'","") +
                                                    "','"+ txtAOSuggestion.Text.ToString().Replace("'","") +"') ";



                    if (strCmd.Length > 10)
                    {
                        iRetVal = objSQLdb.ExecuteSaveData(strCmd);
                    }
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRetVal > 0)
                {
                    //if (sEcode.Length > 0 && sActivityDate.Length > 0)
                    //{
                    //    objEmployeeDARWithTourBills.FillEmployeeActivityDetails(Convert.ToInt32(sEcode), sActivityDate);
                    //}

                    ((ActivityServiceUpdate)objActivityServiceUpdate).GetAllInvoicdeData(dtActDetl.Rows[0]["OrderNo"].ToString(), dtActDetl.Rows[0]["InvNo"].ToString());
                    this.Close();
                   
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQty.Text = "";
            dtpActivityDate.Value = DateTime.Today;
            txtRemarks.Text = "";
            cmbEmployee.SelectedIndex = 0;
            txtFarmerOpinion.Text = "";
            txtAOSuggestion.Text = "";

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER " +
                                    " WHERE ECODE=" + txtEcodeSearch.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EName"].ToString();
                    }
                    else
                    {
                        txtEName.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                    dt = null;
                }
            }
            else
            {
                txtEName.Text = "";
            }
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtServiceSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtServiceSearch.Text.ToString().Trim().Length > 1)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void EcodeSearch()
        {
            SQLDB objData = new SQLDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cmbEmployee.DataSource = null;
                cmbEmployee.Items.Clear();
                dsEmp = objData.ExecuteDataSet("SELECT top 10 * FROM(SELECT ECODE,CAST(ECODE AS VARCHAR)+' - '+MEMBER_NAME+' ('+DESIG+')' ENAME,DESIG  " +
                                "FROM EORA_MASTER WHERE DEPT_ID='800000' AND MEMBER_NAME IS NOT NULL AND EORA = 'E') EMP WHERE ENAME LIKE '%" + txtServiceSearch.Text.ToString().Trim() + "%'");
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cmbEmployee.DataSource = dtEmp;
                    cmbEmployee.DisplayMember = "ENAME";
                    cmbEmployee.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cmbEmployee.SelectedIndex > -1)
                {
                    cmbEmployee.SelectedIndex = 0;
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void FillEmployeeData()
        {
            objServiceDB = new ServiceDB();
            ds = objServiceDB.GetECodesforService(BranchCode, DeptCode);
            UtilityLibrary.PopulateControl(cmbEmployee, ds.Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            objServiceDB = null;

        }

                           
    }
}
