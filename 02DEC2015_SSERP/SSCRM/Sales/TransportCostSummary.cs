using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSAdmin;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class TransportCostSummary : Form
    {
        SQLDB objSQLdb = null;
        private InvoiceDB objData = null;
        private string strECode = string.Empty;
        bool flage = false;
       
        public TransportCostSummary()
        {
            InitializeComponent();
        }

        private void TransportCostSummary_Load(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth;
            FillCompanyData();
            FillBranchData();            
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            try
            {
                strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                  " FROM USER_BRANCH " +
                                  " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                  " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                  " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                  "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

                cbCompany.SelectedValue = CommonData.CompanyCode;
               
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
        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {                                 
                    FillBranchData();             
            
            }
        }
        //private void FillUserBranchData()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        if (cbCompany.SelectedIndex > 0)
        //        {
        //            string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
        //                                " FROM USER_BRANCH " +
        //                                " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
        //                                " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
        //                                "' AND UB_USER_ID ='" + CommonData.LogUserId + "' AND BRANCH_TYPE = 'BR'" +
        //                                " ORDER BY BRANCH_NAME ASC";
        //            dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
        //        }
        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr[0] = "--Select--";
        //            dr[1] = "--Select--";

        //            dt.Rows.InsertAt(dr, 0);
        //            cbBranches.DataSource = dt;
        //            cbBranches.DisplayMember = "BRANCH_NAME";
        //            cbBranches.ValueMember = "BRANCH_CODE";
        //        }
        //            if (dt.Rows.Count > 0)
        //            {
        //                cbBranches.SelectedIndex = 0;
                        
        //            }
        //            else
        //            {
        //                cbBranches.SelectedIndex = -1;
                       
        //            }


                
              
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }        
        //}


        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT  DISTINCT BRANCH_NAME,BRANCH_CODE FROM BRANCH_MAS  BM "+
                                         "INNER JOIN COMPANY_MAS CM ON COMPANY_CODE=CM_COMPANY_CODE "+
                                        "WHERE  COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND  BM.ACTIVE='T' AND BRANCH_TYPE='BR' " +
                                        " ORDER BY BRANCH_NAME ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "BRANCH_NAME";
                    cbBranches.ValueMember = "BRANCH_CODE";
                }
                if (cbBranches.Items.Count != 0)
                {
                    if (CommonData.BranchType == "BR")
                    {
                        cbBranches.SelectedValue = CommonData.BranchCode;

                        //if (CommonData.LogUserId.ToUpper() == "ADMIN")
                        //{
                        //}
                        //else
                        //{
                            cbCompany.Enabled = false;
                            cbBranches.Enabled = false;
                        //}
                    }
                    else
                    {
                        cbBranches.SelectedIndex = 0;
                        cbCompany.Enabled = true;
                        cbBranches.Enabled = true;
                    }
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
        private void CalculateOwnTotAmount()
        {
            double nRent, nDiesel, nOthers, TotAmount;
            nRent = nDiesel = nOthers = TotAmount = 0;

            if (txtOwnRent.Text.Length > 0)
                nRent = Convert.ToDouble(txtOwnRent.Text);
            if (txtOwnDiesel.Text.Length > 0)
                nDiesel = Convert.ToDouble(txtOwnDiesel.Text);
            if (txtOwnOthers.Text.Length > 0)
            {
                nOthers = Convert.ToDouble(txtOwnOthers.Text);
            }

            TotAmount = Convert.ToDouble(nRent + nDiesel + nOthers);
            txtOwnTotAmt.Text = (TotAmount).ToString("0.00");

        }
        private void CalculateHireTotAmount()
        {
            double nRent, nDiesel, nOthers, TotAmount;
            nRent = nDiesel = nOthers = TotAmount = 0;

            if (txtHireRent.Text.Length > 0)
                nRent = Convert.ToDouble(txtHireRent.Text);
            if (txtHireDiesel.Text.Length > 0)
                nDiesel = Convert.ToDouble(txtHireDiesel.Text);
            if (txtHireOthers.Text.Length > 0)
            {
                nOthers = Convert.ToDouble(txtHireOthers.Text);
            }


            TotAmount = Convert.ToDouble(nRent + nDiesel + nOthers);
            txtHireTotAmt.Text = (TotAmount).ToString("0.00");

        }
        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)         
        {
            if (cbCompany.SelectedIndex>0 && cbBranches.SelectedIndex>0)
            {                      
                FillEmployeeData();               
            }
        }

        private void EcodeSearch()       
        {
           
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
               
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objData.InvLevel_Transport_Cost_GLEcode_Proc(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(), txtDocMonth.Text,txtGLEcode.Text);
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                    txtCampName.Text = dtEmp.Rows[0]["lgm_group_name"].ToString();
                  
                  
                }
                else
                {
                    txtDistanceKM.Text = "";
                    txtOwnDiesel.Text = "";
                    txtOwnRent.Text = "";
                    txtOwnOthers.Text = "";
                    txtHireDiesel.Text = "";
                    txtHireOthers.Text = "";
                    txtHireRent.Text = "";
                    txtOwnTotAmt.Text = "";
                    txtHireTotAmt.Text = "";
                    txtRemarks.Text = "";
                    txtCampName.Text = "";
                    cbEcode.DataSource = null;
                    flage = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
              
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }
        private void FillEmployeeData()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {

                dsEmp = objData.GetTransportCostEcodeList(cbCompany.SelectedValue.ToString(),cbBranches.SelectedValue.ToString(), txtDocMonth.Text);
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                    txtCampName.Text = dtEmp.Rows[0]["lgm_group_name"].ToString();
                }
                else
                {
                    txtDistanceKM.Text = "";
                    txtOwnDiesel.Text = "";
                    txtOwnRent.Text = "";
                    txtOwnOthers.Text = "";
                    txtHireDiesel.Text = "";
                    txtHireOthers.Text = "";
                    txtHireRent.Text = "";
                    txtOwnTotAmt.Text = "";
                    txtHireTotAmt.Text = "";
                    txtRemarks.Text = "";
                    txtCampName.Text = "";
                    cbEcode.DataSource = null;
                    flage = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }
  
        
        private bool CheckDetails()
        {
            bool flag = false;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select CompanyName", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
                return flag;
            }

            if (cbBranches.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select BranchName", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
                return flag;
            }
            if (cbEcode.SelectedIndex ==-1)
            {
                MessageBox.Show("Please Select GL/GC Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGLEcode.Focus();
                return flag;
            }
            if (txtDistanceKM.Text.Length == 0)
            {
                MessageBox.Show("Please Enter PU/SP/TU To Camp Distance", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDistanceKM.Focus();
                return flag;
            }
            return true;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB(); 
            int iRes = 0;
            string strCmd = "";
            if (CheckDetails())
            {
                try       
             
                {
                    if (txtOwnDiesel.Text == "")
                    {
                        txtOwnDiesel.Text = "0";
                    }
                    if (txtOwnRent.Text == "")
                    {
                        txtOwnRent.Text = "0";
                    }
                    if (txtOwnOthers.Text == "")
                    {
                        txtOwnOthers.Text = "0";
                    }
                    if (txtHireDiesel.Text == "")
                    {
                        txtHireDiesel.Text = "0";
                    }
                    if(txtHireOthers.Text == "")
                    {
                    txtHireOthers.Text = "0";
                    }
                    if (txtHireRent.Text == "")
                    {
                        txtHireRent.Text = "0";
                    }
                    if (txtHireTotAmt.Text == "")
                    {
                        txtHireTotAmt.Text = "0"; 
                    }
                    if (txtOwnTotAmt.Text == "")
                    {
                        txtOwnTotAmt.Text = "0";
                    }
                    strECode =((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[1].ToString().Split('-')[0];
                  
                     if (flage == false)
                    {
                        strCmd = "INSERT INTO GCGL_TRANSPORT_COST_HEAD(GCTC_COMP_CODE " +
                                                                      ",GCTC_BRANCH_CODE "+
                                                                      ",GCTC_FIN_YEAR " +
                                                                      ",GCTC_DOC_MONTH " +
                                                                      ",GCTC_TO_BRANCH_CODE " +
                                                                      ",GCTC_TO_EORA_CODE " +
                                                                      ",GCTC_OWN_VEH_RENT " +
                                                                      ",GCTC_OWN_VEH_DIESEL " +
                                                                      ",GCTC_OWN_VEH_OTHER " +
                                                                      ",GCTC_OWN_VEH_TOTAL " +
                                                                      ",GCTC_HIRE_VEH_RENT " +
                                                                      ",GCTC_HIRE_VEH_DIESEL " + 
                                                                      ",GCTC_HIRE_VEH_OTHER " + 
                                                                      ",GCTC_HIRE_VEH_TOTAL " +
                                                                      ",GCTC_REMARKS,GCTC_DIST_TO_CAMP " +
                                                                      ",GCTC_CREATED_BY " +
                                                                      ",GCTC_CREATED_DATE "+
                                                                      ")VALUES('" + CommonData.CompanyCode +
                                                                      "','" + CommonData.BranchCode +
                                                                      "','" + CommonData.FinancialYear + 
                                                                      "','" + txtDocMonth.Text +
                                                                      "','" + cbBranches.SelectedValue.ToString() +
                                                                      "','" + strECode +
                                                                      "'," + txtOwnRent.Text +
                                                                      "," + txtOwnDiesel.Text + 
                                                                      "," + txtOwnOthers.Text +
                                                                      "," + txtOwnTotAmt.Text + 
                                                                      "," + txtHireRent.Text + 
                                                                      "," + txtHireDiesel.Text + 
                                                                      "," + txtHireOthers.Text + 
                                                                      ",'" + txtHireTotAmt.Text + 
                                                                      "','" + txtRemarks.Text +
                                                                      "'," + txtDistanceKM.Text + 
                                                                      ",'" + CommonData.LogUserId + "',getdate())";
                      
                    }
                    else
                    {
                        strCmd = "UPDATE GCGL_TRANSPORT_COST_HEAD SET   GCTC_DOC_MONTH='" + txtDocMonth.Text +
                                                                      "', GCTC_TO_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                                                      "', GCTC_TO_EORA_CODE=" + strECode +
                                                                      ", GCTC_OWN_VEH_RENT=" + txtOwnRent.Text +
                                                                      ", GCTC_OWN_VEH_DIESEL="+txtOwnDiesel.Text+
                                                                      ", GCTC_OWN_VEH_OTHER="+txtOwnOthers.Text+
                                                                      ", GCTC_OWN_VEH_TOTAL="+txtOwnTotAmt.Text+
                                                                      ", GCTC_HIRE_VEH_RENT="+txtHireRent.Text+
                                                                      ", GCTC_HIRE_VEH_DIESEL="+txtHireDiesel.Text+
                                                                      ", GCTC_HIRE_VEH_OTHER="+txtHireOthers.Text+
                                                                      ", GCTC_HIRE_VEH_TOTAL="+txtHireTotAmt.Text+
                                                                      ", GCTC_REMARKS='"+txtRemarks.Text +
                                                                      "', GCTC_DIST_TO_CAMP="+txtDistanceKM.Text+
                                                                      ", GCTC_MODIFIED_BY='" + CommonData.LogUserId +
                                                                      "',GCTC_MODIFIED_DATE=getdate()"+
                                                                      " WHERE GCTC_BRANCH_CODE='" + CommonData.BranchCode +
                                                                      "' and GCTC_TO_BRANCH_CODE='"+cbBranches.SelectedValue.ToString() +
                                                                      "' and GCTC_DOC_MONTH='" + txtDocMonth.Text +
                                                                      "' AND GCTC_TO_EORA_CODE=" + strECode + "";
                       
                    }
                     if (strCmd.Length > 10)
                     {
                         iRes = objSQLdb.ExecuteSaveData(strCmd);
                     }

                }
                   
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not saved", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

    

        private void txtOwnRent_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOwnRent.Text != "")
            {
                CalculateOwnTotAmount();
            }
            else
            {
                txtOwnRent.Text = ""; 
            }
        }

        private void txtOwnRent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOwnDiesel_KeyPress(object sender, KeyPressEventArgs e)
        {
          

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOwnDiesel_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOwnDiesel.Text != "")
            {
                CalculateOwnTotAmount();
            }
            else
            {
                txtOwnDiesel.Text = "";
            }
        }

        private void txtOwnOthers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b')
            {
                if (!char.IsLetterOrDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOwnOthers_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOwnOthers.Text != "")
            {
                CalculateOwnTotAmount();
            }
            else
            {
                txtOwnOthers.Text = ""; 
            }
        }

        private void txtHireRent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtHireRent_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtHireRent.Text != "")
            {
                CalculateHireTotAmount();
            }
            else
            {
                txtHireRent.Text = ""; 
            }
        }

        private void txtHireDiesel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtHireDiesel_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtHireDiesel.Text != "")
            {
                CalculateHireTotAmount();
            }
            else
            {
                txtHireDiesel.Text = "";
            }
        }

        private void txtHireOthers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtHireOthers_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtHireOthers.Text != "")
            {
                CalculateHireTotAmount();
            }
            else
            {
                txtHireOthers.Text = ""; 
            }
        }

        private void txtGLEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtGLEcode.Text.ToString().Trim().Length > 0)
            {
                EcodeSearch();
            }
            else
            {               
                FillEmployeeData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             objSQLdb = new SQLDB();
             int ivals = 0;
            if (txtGLEcode.Text != "")
            {
                try
                {
                   
                    string strQryD = "DELETE FROM GCGL_TRANSPORT_COST_HEAD WHERE " +
                                       " GCTC_TO_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() + "' AND GCTC_DOC_MONTH='" + txtDocMonth.Text + "' AND GCTC_TO_EORA_CODE='" + txtGLEcode.Text + "'";

                     ivals = objSQLdb.ExecuteSaveData(strQryD);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (ivals > 0)
                    MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data Not deleted", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnCancel_Click(null, null);
                flage = false;
            }
        }

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
           // cbCompany.SelectedValue = CommonData.CompanyCode;
            //txtDocMonth.Text = CommonData.DocMonth;
           //cbBranches.SelectedIndex = 0;
            txtGLEcode.Text = "";
            //cbEcode.SelectedIndex = -1;
            //txtCampName.Text = "";
            txtDistanceKM.Text = "";
            txtOwnDiesel.Text = "";
            txtOwnRent.Text = "";
            txtOwnOthers.Text = "";
            txtHireDiesel.Text = "";
            txtHireOthers.Text = "";
            txtHireRent.Text = "";
            txtOwnTotAmt.Text = "";
            txtHireTotAmt.Text = "";
            txtRemarks.Text = "";
            flage = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)8))
            {
                SearchItems(cbEcode, ref e);
            }
            else
                e.Handled = false;           

        }
        private void SearchItems(ComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            sb.Append(acComboBox.Text.Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Substring(selectionEnd));

            index = acComboBox.FindString(sb.ToString());

            if (index == -1)
                e.Handled = false;
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                e.Handled = true;
            }
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
       {
             flage = false;
             objSQLdb = new SQLDB();
             objData = new InvoiceDB();
             objData = new InvoiceDB();

             DataSet ds = null;
            

           if (cbEcode.SelectedIndex > -1)
           {
               strECode =((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[1].ToString().Split('-')[0];
               if (cbEcode.SelectedIndex > -1)
               {
                   DataSet dsEmp = objData.InvLevel_Transport_Cost_GLEcode_Proc(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(), txtDocMonth.Text,strECode);
                   DataTable dtEmp = dsEmp.Tables[0];
                   if (dtEmp.Rows.Count > 0)
                   {
                       txtCampName.Text = dtEmp.Rows[0]["lgm_group_name"].ToString();
                   }
                   else
                   {
                       txtCampName.Text = "";
                   }
               }

               if (strECode != "")
               {             
                   try
                       
                   {
                       ds = objData.GetTranportCostDetails(CommonData.BranchCode, txtDocMonth.Text,Convert.ToInt32(strECode));
                       
                       
                       if (ds.Tables[0].Rows.Count > 0)
                       {
                           flage = true;
                           txtDistanceKM.Text = ds.Tables[0].Rows[0]["DistanceToCamp"].ToString();
                           txtOwnDiesel.Text = ds.Tables[0].Rows[0]["OwnDiesel"].ToString();
                           txtOwnRent.Text = ds.Tables[0].Rows[0]["OwnRent"].ToString();
                           txtOwnOthers.Text = ds.Tables[0].Rows[0]["OwnOther"].ToString();
                           txtHireDiesel.Text = ds.Tables[0].Rows[0]["HireDiesel"].ToString();
                           txtHireOthers.Text = ds.Tables[0].Rows[0]["HireOther"].ToString();
                           txtHireRent.Text = ds.Tables[0].Rows[0]["HireRent"].ToString();
                           txtOwnTotAmt.Text = ds.Tables[0].Rows[0]["OwmTotAmt"].ToString();
                           txtHireTotAmt.Text = ds.Tables[0].Rows[0]["HireTotAmt"].ToString();
                           txtRemarks.Text = ds.Tables[0].Rows[0]["Remarsks"].ToString();

                       }
                       else
                       {
                           txtDistanceKM.Text = "";
                           txtOwnDiesel.Text = "";
                           txtOwnRent.Text = "";
                           txtOwnOthers.Text = "";
                           txtHireDiesel.Text = "";
                           txtHireOthers.Text = "";
                           txtHireRent.Text = "";
                           txtOwnTotAmt.Text = "";
                           txtHireTotAmt.Text = "";
                           txtRemarks.Text = "";
                           flage = false;


                       }

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
           }
            //MessageBox.Show("Please Select GL/GC BranchName", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void txtDistanceKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

    

      
    }
}
