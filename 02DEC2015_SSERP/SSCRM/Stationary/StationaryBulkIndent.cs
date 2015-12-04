using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using System.IO;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class StationaryBulkIndent : Form
    {
        private SQLDB objSQLDB = null;
        private bool blIsCellQty = true;
        private bool flage = false;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;      
        string strCompany = "";
        private string BrType = string.Empty;       
        private string Branches = string.Empty;        
          string fistBrcode = "";
          string endBrcode = "";


        public StationaryBulkIndent()
        {
            InitializeComponent();
        }
        string BranchCode = CommonData.BranchCode, CompanyCode = CommonData.CompanyCode, FinYear = CommonData.FinancialYear;
        int IndentNumber = 0;

        private void StationaryBulkIndent_Load(object sender, EventArgs e)
        {
            dtIndentDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            

            txtItemsCount.Text = "0.00";
            txtIndentAmt.Text = "0.00";
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            if (IndentNumber == 0)
                txtIndentNo.Text = GenerateIndentNo();
            else
            {
                txtIndentNo.Text = IndentNumber.ToString();
                //txtIndentNo_Validated(null, null);
            }
            tvBranches.Nodes.Clear();
            gvIndentDetails.Rows.Clear();
            txtBrCount.Text = "";
            FillUserCompanyData();
            chkCompAll.Enabled = true;
            chkBRAll.Enabled = true;
            clbCompany.Enabled = true;
            tvBranches.Enabled = true;
            flage = false;

        }
        private void FillUserCompanyData()
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            //string strCommand = "";
            clbCompany.Items.Clear();
            try
            {

                dt = Get_UserCompanyBranchFilterCursor("",CommonData.LogUserId,"","PARENT").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["COMPANY_CODE"].ToString();
                        oclBox.Text = item["COMPANY_NAME"].ToString();
                        clbCompany.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillIndentCompanyData()
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            //string strCommand = "";
            clbCompany.Items.Clear();
            try
            {

                dt = Get_IndentCompanyBranchFilterCursor(Convert.ToInt32(txtIndentNo.Text),FinYear , "PARENT").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["SIH_COMPANY_CODE"].ToString();
                        oclBox.Text = item["COMPANY_NAME"].ToString();
                        //clbCompany.Items.Add(oclBox);
                        clbCompany.Items.Add(oclBox,CheckState.Checked);             
                       oclBox = null;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillUserBranchData()
        {
            tvBranches.Nodes.Clear();           
            DataSet ds = new DataSet();
            ds = Get_UserCompanyBranchFilterCursor(strCompany, CommonData.LogUserId, "", "BR TYPE");
            //TreeNode tNode;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["BranchType"].ToString(), ds.Tables[0].Rows[i]["BRANCH_TYPE"].ToString());
                         DataSet dschild = new DataSet();
                         dschild = Get_UserCompanyBranchFilterCursor(strCompany, CommonData.LogUserId, ds.Tables[0].Rows[i]["BranchType"].ToString(), "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }
                    }
                }
               
                      
                     tvBranches.Nodes[0].Expand();                   
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillIndentBranchData()
        {
            tvBranches.Nodes.Clear();
            DataSet ds = new DataSet();
            ds = Get_IndentCompanyBranchFilterCursor(Convert.ToInt32(txtIndentNo.Text), FinYear, "BR TYPE");
            //TreeNode tNode;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["BranchType"].ToString(), ds.Tables[0].Rows[i]["BRANCH_TYPE"].ToString());
                        DataSet dschild = new DataSet();
                        dschild = Get_IndentCompanyBranchFilterCursor(Convert.ToInt32(txtIndentNo.Text),FinYear , "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["SIH_BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }
                        tvBranches.Nodes[i].Checked = true;
                    }
                }


                tvBranches.Nodes[0].Expand();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private string GenerateIndentNo()
        {
            objSQLDB = new SQLDB();
            string sIndNo = string.Empty;
            try
            {

                string sqlText = "SELECT ISNULL(MAX(SIH_INDENT_NUMBER),0)+1 FROM STATIONARY_INDENT_HEAD";
                sIndNo = objSQLDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return sIndNo;
        }
        private DataSet Get_UserCompanyBranchFilterCursor(string sCompCode, string sLogUserId, string sBranchtType, string sGetType)
        {
            objSQLDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLDB.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLDB.CreateParameter("@sUser", DbType.String, sLogUserId, ParameterDirection.Input);
                param[2] = objSQLDB.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[3] = objSQLDB.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLDB.ExecuteDataSet("UserCompBranchCursor_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLDB = null;
            }
            return ds;
        }
        private DataSet Get_IndentCompanyBranchFilterCursor(int IndentNo, string Finyear, string sGetType)
        {
            objSQLDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLDB.CreateParameter("@sIndentNo", DbType.Int32, IndentNo, ParameterDirection.Input);
                param[1] = objSQLDB.CreateParameter("@sFinyear", DbType.String, Finyear, ParameterDirection.Input);               
                param[2] = objSQLDB.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLDB.ExecuteDataSet("GetIndentCompBranchCursor", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLDB = null;
            }
            return ds;
        }
        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("StationaryIndentBulk");
            ItemSearch.objStationaryBulkIndent = this;
            ItemSearch.ShowDialog();
            txtItemsCount.Text = gvIndentDetails.Rows.Count.ToString();
        }

        private void CalculateTotals()
        {
            txtItemsCount.Text = gvIndentDetails.Rows.Count.ToString();
            if (gvIndentDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    if (gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value.ToString() != "" && gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        txtIndentAmt.Text = Convert.ToDouble(Convert.ToDouble(txtIndentAmt.Text) + Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value)).ToString("f");
                    }
                }
            }
            else
            {
                txtIndentAmt.Text = "0.00";
            }
        }      
        public void CalculateTot()
        {
            double iTotalAmt = 0;
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    iTotalAmt += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value);
                else
                    iTotalAmt += 0;
            }
            txtIndentAmt.Text = iTotalAmt.ToString();
        }
        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
            CalculateTot();
        }
        private bool Checkdata()
        {
            bool blCheck = true;
            bool blSource = false;
            for (int i = 0; i < clbCompany.Items.Count; i++)
            {
                if (clbCompany.GetItemCheckState(i) == CheckState.Checked)
                {
                    blSource = true;
                    break;
                }
            }          
            if (clbCompany.Items.Count == 0 || blSource == false)
            {
                MessageBox.Show("Select Atleast One Company", "Bulk Indent", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                blSource = false;
                clbCompany.Focus();
                return blCheck;
            }
            blSource = false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    if (tvBranches.Nodes[i].Nodes[j].Checked == true)
                    {
                        blSource = true;
                        break;
                    }
                }
            }
            if (tvBranches.Nodes.Count == 0 || blSource == false)
            {
                MessageBox.Show("Select Atleast One Branch", "Bulk Indent", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                clbCompany.Focus();
                return blCheck;
            }
            if (gvIndentDetails.Rows.Count == 0)
            {
                MessageBox.Show("Select Atlease One item!", "Bulk Indent", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }          
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[0].Cells["ReqQty"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("Please Enter ReqQty", "Bulk Indent", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }          

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            int dvals = 0;
            int ival = 0;
            string strQry = "", strQryD = "";
            if (Checkdata())
            {
                if (txtIndentAmt.Text.Length == 0)
                {
                    txtIndentAmt.Text = "0.00";
                }

                try
                {

                    if( tvBranches.Nodes.Count>0)
                    {
                        Branches = "";
                        if(flage==false)
                        {
                        txtIndentNo.Text = GenerateIndentNo();
                        }
                        for (int i = 0; i < tvBranches.Nodes.Count; i++)
                        {
                            for (int ivals = 0; ivals < tvBranches.Nodes[i].Nodes.Count; ivals++)
                            {
                                objSQLDB = new SQLDB();
                                CompanyCode = "";
                                BranchCode = "";
                             
                                if (tvBranches.Nodes[i].Nodes[ivals].Checked == true)
                                {
                                    Branches = tvBranches.Nodes[i].Nodes[ivals].Name.ToString();

                                    string[] CompanyBranchcode = Branches.Split('@');
                                    CompanyCode = CompanyBranchcode[1];
                                    BranchCode = CompanyBranchcode[0];
                                    DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT SIH_INDENT_NUMBER FROM STATIONARY_INDENT_HEAD WHERE " +
                                       "SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND SIH_FIN_YEAR='" + FinYear + "' AND " +
                                        "SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text));
                                    if (dsExist.Tables[0].Rows.Count == 0)
                                    {
                                       
                                        strQry = " INSERT INTO STATIONARY_INDENT_HEAD" +
                                                    "(SIH_COMPANY_CODE" +
                                                    ",SIH_BRANCH_CODE" +
                                                    ",SIH_STATE_CODE" +
                                                    ",SIH_FIN_YEAR" +
                                                    ",SIH_INDENT_NUMBER" +
                                                    ",SIH_INDENT_DATE" +
                                                    ",SIH_INDENT_AMOUNT" +
                                                    ",SIH_INDENT_STATUS" +
                                                    ",SIH_INDENT_TYPE" +
                                                    ",SIH_INDENT_APPROVED_BY_MGR" +
                                                    ",SIH_APPROVED_BY_MGR_DATE" +
                                                    ",SIH_INDENT_APPROVED_BY_HEAD" +
                                                    ",SIH_APPROVED_BY_HEAD_DATE" +
                                                    ",SIH_CREATED_BY" +
                                                    ",SIH_CREATED_DATE)" +
                                                    " VALUES " +
                                                    "('" + CompanyCode +
                                                    "','" + BranchCode +
                                                    "','" + BranchCode.Substring(3, 2) +
                                                    "','" + FinYear +
                                                    "','" + Convert.ToInt32(txtIndentNo.Text) +
                                                    "','" + Convert.ToDateTime(dtIndentDate.Value).ToString("dd/MMM/yyyy") +
                                                    "','" + Convert.ToDouble(txtIndentAmt.Text) +
                                                    "','A','BR','" + CommonData.LogUserEcode +
                                                    "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                    "','" + CommonData.LogUserEcode +
                                                    "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                    "','" + CommonData.LogUserId +
                                                    "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                    "')";
                                    }
                                    else
                                    {

                                        //if (flage == true)
                                        //{
                                            strQry = " UPDATE STATIONARY_INDENT_HEAD" +
                                                        " SET SIH_INDENT_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                        "',SIH_INDENT_AMOUNT=" + Convert.ToDouble(txtIndentAmt.Text) +
                                                        " WHERE   SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) + " AND  SIH_FIN_YEAR='" + FinYear + "'";

                                            strQry += " DELETE FROM STATIONARY_INDENT_DETL WHERE SID_COMPANY_CODE='" + CompanyCode + "' AND SID_BRANCH_CODE='" + BranchCode +
                                                       "' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                                        //}
                                       
                                    }


                                     ival = objSQLDB.ExecuteSaveData(strQry);
                                    if (ival > 0)
                                    {
                                        if (gvIndentDetails.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < gvIndentDetails.Rows.Count; j++)
                                            {
                                                if (gvIndentDetails.Rows[j].Cells["ReqQty"].Value.ToString() == "")
                                                    gvIndentDetails.Rows[j].Cells["ReqQty"].Value = "0";
                                                if (gvIndentDetails.Rows[j].Cells["Amount"].Value.ToString() == "")
                                                    gvIndentDetails.Rows[i].Cells["Amount"].Value = "0.00";
                                                if (gvIndentDetails.Rows[j].Cells["AvailableQty"].Value.ToString() == "")
                                                    gvIndentDetails.Rows[j].Cells["AvailableQty"].Value = "0";
                                                if (gvIndentDetails.Rows[j].Cells["ApprovedQty"].Value.ToString() == "")
                                                    gvIndentDetails.Rows[j].Cells["ApprovedQty"].Value = "0";
                                                if (gvIndentDetails.Rows[j].Cells["ReqQty"].Value.ToString().Trim() != "0"
                                                    || gvIndentDetails.Rows[j].Cells["ApprovedQty"].Value.ToString().Trim() != "0")
                                                {
                                                    strQryD += " INSERT INTO STATIONARY_INDENT_DETL" +
                                                                "(SID_COMPANY_CODE" +
                                                                ",SID_STATE_CODE" +
                                                                ",SID_BRANCH_CODE" +
                                                                ",SID_FIN_YEAR" +
                                                                ",SID_INDENT_NUMBER" +
                                                                ",SID_INDENT_SL_NO" +
                                                                ",SID_ITEM_ID" +
                                                                ",SID_ITEM_AVAILABLE_QTY" +
                                                                ",SID_ITEM_REQ_QTY" +
                                                                ",SID_ITEM_HO_RECON_QTY" +
                                                                ",SID_ITEM_PRICE" +
                                                                ",SID_ITEM_AMOUNT" +
                                                                ",SID_INDENT_REMARKS)" +
                                                                " VALUES(" +
                                                                "'" + CompanyCode +
                                                                "','" + BranchCode.Substring(3, 2) +
                                                                "','" + BranchCode +
                                                                "','" + FinYear +
                                                                "','" + Convert.ToInt32(txtIndentNo.Text) +
                                                                "','" + Convert.ToInt32(j + 1) +
                                                                "','" + gvIndentDetails.Rows[j].Cells["ItemID"].Value +
                                                                "','" + gvIndentDetails.Rows[j].Cells["AvailableQty"].Value +
                                                                "','" + gvIndentDetails.Rows[j].Cells["ReqQty"].Value +
                                                                "','" + gvIndentDetails.Rows[j].Cells["ApprovedQty"].Value +
                                                                "','" + gvIndentDetails.Rows[j].Cells["Price"].Value +
                                                                "'," + Convert.ToDouble(gvIndentDetails.Rows[j].Cells["Amount"].Value) +
                                                                ",'" + gvIndentDetails.Rows[j].Cells["Remarks"].Value +
                                                                "')";
                                                }
                                            }
                                             dvals = objSQLDB.ExecuteSaveData(strQryD);
                                            objSQLDB = null;
                                            strQryD = "";

                                        }
                                    }
                                }
                               
                            }
                            if (ival > 0 && dvals > 0)
                            {
                                MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                gvIndentDetails.Rows.Clear();

                                StationaryBulkIndent_Load(null, null);

                            }
                            else
                                //strQry = " DELETE FROM STATIONARY_INDENT_HEAD" +
                                //        " WHERE SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) +
                                //        " AND SIH_BRANCH_CODE='" + BranchCode + "'";

                                MessageBox.Show("Data Not saved ", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                         

                    }
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

        private void txtIndentNo_Validating(object sender, CancelEventArgs e)
        {
            if (txtIndentNo.Text != "")
            {
                objSQLDB = new SQLDB();
                string sqlQry = " SELECT * FROM STATIONARY_INDENT_HEAD   WHERE SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) + " AND  SIH_FIN_YEAR='"+FinYear+"'";
                //WHERE SIH_COMPANY_CODE='" + CompanyCode +"' AND SIH_BRANCH_CODE='" + BranchCode + "' AND
               // SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) + ";";
                sqlQry += " SELECT * FROM STATIONARY_INDENT_DETL A " +
                    "INNER JOIN STATIONARY_ITEMS_MASTER B ON A.SID_ITEM_ID=B.SIM_ITEM_CODE " +
                   "WHERE SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text)+ " AND  SID_FIN_YEAR='"+FinYear+"'";;
                    //"WHERE SID_COMPANY_CODE='" + CompanyCode +
                    //"' AND SID_BRANCH_CODE='" + BranchCode +
                    //"' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                 DataSet ds = objSQLDB.ExecuteDataSet(sqlQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int ival = 0; ival < ds.Tables[0].Rows.Count; ival++)
                    {                       
                        FinYear = ds.Tables[0].Rows[ival]["SIH_FIN_YEAR"].ToString();
                        FillIndentCompanyData();
                        FillIndentBranchData();
                        chkCompAll.Enabled = false;
                        chkBRAll.Enabled = false;
                        clbCompany.Enabled = false;
                        tvBranches.Enabled = false;
                    }
                    
                    dtIndentDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["SIH_INDENT_DATE"]);
                    txtIndentAmt.Text = ds.Tables[0].Rows[0]["SIH_INDENT_AMOUNT"].ToString();
                    FinYear = ds.Tables[0].Rows[0]["SIH_FIN_YEAR"].ToString();                 
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        gvIndentDetails.Rows.Clear();
                        GetBindData(ds.Tables[1]);
                        flage = true;
                    }
                }
                else
                {
                    //txtIndentDate.Text = "";
                    txtIndentAmt.Text = "";
                    txtItemsCount.Text = "";
                    chkCompAll.Enabled = true;
                    chkBRAll.Enabled = true;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    clbCompany.Enabled = true;
                    tvBranches.Enabled = true;
                    FinYear = CommonData.FinancialYear;
                    FillUserCompanyData();
                    tvBranches.Nodes.Clear();
                    gvIndentDetails.Rows.Clear();
                    flage = false;

                }
            }
        }
        public void GetBindData(DataTable dt)
        {
            gvIndentDetails.Rows.Clear();
            int intRow = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fistBrcode = dt.Rows[i]["SID_BRANCH_CODE"].ToString();
              if( i!=0)
              {
                  endBrcode = dt.Rows[i - 1]["SID_BRANCH_CODE"].ToString(); 
              }
                if(i==0)
                {
                    endBrcode = dt.Rows[i]["SID_BRANCH_CODE"].ToString(); 
                }
                if (fistBrcode == endBrcode)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                    cellItemID.Value = dt.Rows[i]["SID_ITEM_ID"].ToString();
                    tempRow.Cells.Add(cellItemID);

                    DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                    cellItemName.Value = dt.Rows[i]["SIM_ITEM_NAME"].ToString();
                    tempRow.Cells.Add(cellItemName);

                    DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                    cellAvailQty.Value = dt.Rows[i]["SID_ITEM_AVAILABLE_QTY"].ToString();
                    tempRow.Cells.Add(cellAvailQty);

                    DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                    cellReqQty.Value = dt.Rows[i]["SID_ITEM_REQ_QTY"].ToString();
                    tempRow.Cells.Add(cellReqQty);

                    DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                    cellApprQty.Value = dt.Rows[i]["SID_ITEM_HO_RECON_QTY"].ToString();
                    tempRow.Cells.Add(cellApprQty);

                    DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                    cellRate.Value = Convert.ToDouble(dt.Rows[i]["SID_ITEM_PRICE"]).ToString("f");
                    tempRow.Cells.Add(cellRate);

                    DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                    cellAmount.Value = dt.Rows[i]["SID_ITEM_AMOUNT"].ToString();
                    tempRow.Cells.Add(cellAmount);

                    DataGridViewCell cellAvailQty1 = new DataGridViewTextBoxCell();
                    cellAvailQty1.Value = dt.Rows[i]["SID_ITEM_AVAILABLE_QTY"].ToString();
                    tempRow.Cells.Add(cellAvailQty1);

                    DataGridViewCell cellRem = new DataGridViewTextBoxCell();
                    cellRem.Value = dt.Rows[i]["SID_INDENT_REMARKS"].ToString();
                    tempRow.Cells.Add(cellRem);
                    intRow = intRow + 1;
                    gvIndentDetails.Rows.Add(tempRow);


                    //    gvIndentDetails.Columns[5].ReadOnly = false;
                    //else
                    //{
                    //    gvIndentDetails.Columns[3].ReadOnly = false;
                    //    gvIndentDetails.Columns[4].ReadOnly = false;
                    //}
                }
                //else
                //{
                //    return;
                //}
        }
             
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46 && blIsCellQty == true)
            {
                e.Handled = true;
                return;
            }

            //to allow decimals only teak plant
            if (intCurrentCell == 3 && e.KeyChar == 46 && blIsCellQty == true)
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && blIsCellQty == true)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 4)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        gvIndentDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
                //CalculateTotals();
            }
            if (e.ColumnIndex == gvIndentDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    //string ProductId = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["ProductId"].Index].Value.ToString();
                    DataGridViewRow dgvr = gvIndentDetails.Rows[e.RowIndex];
                    gvIndentDetails.Rows.Remove(dgvr);
                    OrderSlNo();
                }
            }
        }
        private void OrderSlNo()
        {
            if (gvIndentDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    gvIndentDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                }
            }
        }

        private void gvIndentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells["ReqQty"].Value) != "")
                {
                    gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value = gvIndentDetails.Rows[e.RowIndex].Cells["ReqQty"].Value.ToString();
                    if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value) >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) >= 0)
                    {
                        gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value));
                        gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");

                    }
                }
                else
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
            }
            else if (e.ColumnIndex == 3)
            {
                gvIndentDetails.Rows[e.RowIndex].Cells["AvailableQty"].Value = gvIndentDetails.Rows[e.RowIndex].Cells["DBAvailableQty"].Value;
            }
            else if (e.ColumnIndex == 5)
            {
                if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value) >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) >= 0)
                {
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value));
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                }
            }
            CalculateTot();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            if (txtIndentNo.Text != "" && flage==true)
            {
                  DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                  if (dlgResult == DialogResult.Yes)
                  {
                      string strQryD = " DELETE FROM STATIONARY_INDENT_DETL WHERE  SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) + " AND  SID_FIN_YEAR='" + FinYear + "'";
                      strQryD += " DELETE FROM STATIONARY_INDENT_HEAD WHERE  SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) + " AND  SIH_FIN_YEAR='" + FinYear + "'";
                      int ivals = objSQLDB.ExecuteSaveData(strQryD);
                      if (ivals > 0)
                          MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      else
                          MessageBox.Show("Data Not deleted", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
            }
            StationaryBulkIndent_Load(null, null);

            gvIndentDetails.Rows.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
            StationaryBulkIndent_Load(null, null);
            chkBRAll.Checked = false;
            chkCompAll.Checked = false;
        }


        private void gvIndentDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            //if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 3)
            //{
            //    TextBox txtQty = e.Control as TextBox;
            //    if (txtQty != null)
            //    {
            //        txtQty.MaxLength = 6;
            //        txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
            //    }
            //}
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 5)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 10;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 9)
            {
                blIsCellQty = false;
            }

        }

        private void chkCompAll_CheckedChanged(object sender, EventArgs e)
        {
         
            if (chkCompAll.Checked == true)
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Checked);
                }
                 GetSelectedCompany();               
                  FillUserBranchData();      
            }
            else
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Unchecked);
                }              
                    tvBranches.Nodes.Clear();
                    gvIndentDetails.Rows.Clear();
                    StationaryBulkIndent_Load(null, null);     
              
            }
        }

        private void chkBRAll_CheckedChanged(object sender, EventArgs e)
        {

            if (chkBRAll.Checked == true)
            {
                for (int i = 0; i < tvBranches.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                    {
                        tvBranches.Nodes[i].Nodes[j].Checked = true;

                    }
                }
            }
            else
            {
                for (int i = 0; i < tvBranches.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                    {
                        tvBranches.Nodes[i].Nodes[j].Checked = false;
                    }
                }
            }
        }

        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvBranches.Nodes.Clear();
            GetSelectedCompany();
            if (strCompany.Length >= 2)
            {
                FillUserBranchData();
            }
            else
            {
                tvBranches.Nodes.Clear();
            }
        }
        private void GetSelectedCompany()
        {
            strCompany = "";
            for (int i = 0; i < clbCompany.Items.Count; i++)
            {
                if (clbCompany.GetItemCheckState(i) == CheckState.Checked)
                {
                    strCompany += "" + ((SSAdmin.NewCheckboxListItem)(clbCompany.Items[i])).Tag.ToString() + ",";
                }
            }

            if (strCompany.Length >= 2)
            {
                strCompany = strCompany.TrimEnd(',');
            }
            else
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }
        private void GetBranchsValues()
        {

            bool isBranchType = false;
            BrType = "";
            Branches = "";       
           
                for (int i = 0; i < tvBranches.Nodes.Count; i++)
                {                   
                        for (int ival = 0; ival < tvBranches.Nodes[i].Nodes.Count; ival++)
                        {
                            if (tvBranches.Nodes[i].Nodes[ival].Checked == true)
                            {
                                if (Branches != string.Empty)
                                    Branches += ",";
                                Branches += tvBranches.Nodes[i].Nodes[ival].Name.ToString();
                                Branches += tvBranches.Nodes[i].Nodes[ival].Tag.ToString();
                                isBranchType = true;
                            }
                        }                    
              

                if (isBranchType == true)
                {
                    if (BrType != string.Empty)
                        BrType += ",";
                    BrType += tvBranches.Nodes[0].Nodes[i].Name.ToString();
                }
                isBranchType = false;
            }

        }

        private void tvBranches_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);

            tvBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvBranches.EndUpdate();
        }

        private void clbCompany_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //FillUserCompanyData();
        }

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            int Count = 0;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int ival = 0; ival < tvBranches.Nodes[i].Nodes.Count; ival++)
                {
                    if (tvBranches.Nodes[i].Nodes[ival].Checked == true)
                    {
                        Count++;

                    }
                    //if (tvBranches.Nodes[i].Nodes[ival].Checked == false)
                    //{
                    //    Count--;

                    //}
                }
                txtBrCount.Text = Convert.ToString(Count);
            }
              
        }
    }
}


