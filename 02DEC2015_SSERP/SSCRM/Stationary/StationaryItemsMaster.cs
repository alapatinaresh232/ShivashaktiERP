using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class StationaryItemsMaster : Form
    {
        private SQLDB objSQLDB = null;
        private bool IsModify = false;
        public StationaryBrGRN objStationaryBranchGRN;
        string strCompany = "", strBranch = "", strState = "", strCategory = "";

        public StationaryItemsMaster()
        {
            InitializeComponent();
        }

        private void StationaryItemsMaster_Load(object sender, EventArgs e)
        {
            txtItemId.ReadOnly = true;
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
            FillCompanyData();
            FillItemCategory();
             // objSQLDB = new SQLDB();
             //DataTable dtAccMas = new DataTable();              
             // dtAccMas = objSQLDB.ExecuteDataSet("SELECT SIM_ITEM_CODE,SIM_ITEM_NAME FROM STATIONARY_ITEMS_MASTER "+
             //               "INNER JOIN STATIONARY_ITEMS_CATEGORY ON SIM_ITEM_CATEGORY=SIC_CATEGORY_ID "+
             //               " WHERE SIM_COMPANY_CODE='"+cbCompany.SelectedValue.ToString()+
             //               "'AND  SIC_CATEGORY_ID='"+cbCategory.SelectedValue.ToString()+"'").Tables[0];
            //UtilityLibrary.AutoCompleteTextBox(txtItemDesc, dtAccMas, ""," SIM_ITEM_NAME");
            objSQLDB = null;

            FillStationaryItemDetailsToGrid("", "", "", "");

            txtItemId.Text = GenerateItemID().ToString();
        }

        private int GenerateItemID()
        {
            objSQLDB = new SQLDB();
            int iItemID = 0;
            string strSQLText = "SELECT ISNULL(MAX(SIM_ITEM_CODE),0)+1 FROM STATIONARY_ITEMS_MASTER";
            try
            {
                iItemID = Convert.ToInt32(objSQLDB.ExecuteDataSet(strSQLText).Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //objSQLDB = null;
                return iItemID;
            }
            finally
            {
                objSQLDB = null;
            }
            return iItemID;
        }
        private void FillItemCategory()
        {
            objSQLDB = new SQLDB();
            DataTable dt = null;
            try
            {
                string sqlText = "";
                sqlText = "SELECT SIC_CATEGORY_ID,SIC_CATEGORY_NAME FROM STATIONARY_ITEMS_CATEGORY";
                dt = objSQLDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "ALL";
                    dr[1] = "ALL";
                    dt.Rows.InsertAt(dr, 0);

                    cbCategory.DataSource = dt;
                    cbCategory.DisplayMember = "SIC_CATEGORY_NAME";
                    cbCategory.ValueMember = "SIC_CATEGORY_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                //objSQLDB = null;
            }
        }
        private void FillCompanyData()
        {
            cbCompany.DataSource = null;
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS where active='T'";

                dt = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "ALL";
                    dr[1] = "ALL";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //objSQLDB = null;
                dt = null;
            }
        }
        private void FillSateData()
        {
            cbState.DataSource = null;
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";

            try
            {
                if (cbCompany.SelectedIndex ==0)
                {
                    strCommand = "SELECT DISTINCT sm_state_code,sm_state FROM state_mas ";
                }
                else
                {
                    if (cbCompany.SelectedIndex > 0)
                    {
                        strCommand = " SELECT DISTINCT sm_state_code,sm_state FROM state_mas INNER JOIN BRANCH_MAS ON sm_state_code=STATE_CODE " +
                                                       " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() + "' ";
                    }
                }
                if (strCommand.Length > 5)
                {
                    dt = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "ALL";
                    dr[1] = "ALL";

                    dt.Rows.InsertAt(dr, 0);
                    cbState.DataSource = dt;
                    cbState.DisplayMember = "sm_state";
                    cbState.ValueMember = "sm_state_code";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //objSQLDB = null;
                dt = null;
            }
        }
        private void FillBranchData()
        {
            cbLocation.DataSource = null;
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                if (cbState.SelectedIndex ==0)
                {
                    strCmd = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' ";
                }
                else
                {
                    if ((cbCompany.SelectedIndex > 0) && (cbState.SelectedIndex > 0))
                    {
                        strCmd = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' " +
                            " and COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                             "' and STATE_CODE='" + cbState.SelectedValue.ToString() +
                            "' ORDER BY BRANCH_NAME ASC ";
                    }
                }
                if (strCmd.Length > 4)
                {
                    dt = objSQLDB.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "ALL";
                    dr[1] = "ALL";

                    dt.Rows.InsertAt(dr, 0);
                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "branchCode";
                    //cbLocation.ValueMember = "LOCATION";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
                dt = null;
                strCmd = "";
            }

        }


        //private void FillItemsToGrid()
        //{
        //    gvIndentDetails.Rows.Clear();
        //    DataTable dtItems = new DataTable();
        //    //dtItems = GetStationaryItemDetails();
        //   GetStationaryItemDetails(cbCategory.SelectedValue.ToString(),cbCompany.SelectedValue.ToString(),cbState.SelectedValue.ToString(),cbLocation.SelectedValue.ToString());

        //    for (int i = 0; i < dtItems.Rows.Count; i++)
        //    {
        //        DataGridViewRow tempRow = new DataGridViewRow();
        //        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
        //        cellSLNO.Value = i + 1;
        //        tempRow.Cells.Add(cellSLNO);

        //        DataGridViewCell cellItemCompany = new DataGridViewTextBoxCell();
        //        cellItemCompany.Value = dtItems.Rows[i]["st_company_code"].ToString();
        //        tempRow.Cells.Add(cellItemCompany);

        //        DataGridViewCell cellItemCompanyName = new DataGridViewTextBoxCell();
        //        cellItemCompanyName.Value = dtItems.Rows[i]["st_company_name"].ToString();
        //        tempRow.Cells.Add(cellItemCompanyName);

        //        DataGridViewCell cellItemState = new DataGridViewTextBoxCell();
        //        cellItemState.Value = dtItems.Rows[i]["st_state_code"].ToString();
        //        tempRow.Cells.Add(cellItemState);

        //        DataGridViewCell cellItemStateName = new DataGridViewTextBoxCell();
        //        cellItemStateName.Value = dtItems.Rows[i]["st_state_name"].ToString();
        //        tempRow.Cells.Add(cellItemStateName);

        //        DataGridViewCell cellItemBranchCode = new DataGridViewTextBoxCell();
        //        cellItemBranchCode.Value = dtItems.Rows[i]["st_branch_code"].ToString();
        //        tempRow.Cells.Add(cellItemBranchCode);

        //        DataGridViewCell cellItemBranchName = new DataGridViewTextBoxCell();
        //        cellItemBranchName.Value = dtItems.Rows[i]["st_branch_name"].ToString();
        //        tempRow.Cells.Add(cellItemBranchName);

        //        DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
        //        cellItemID.Value = dtItems.Rows[i]["st_item_code"].ToString();
        //        tempRow.Cells.Add(cellItemID);

        //        DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
        //        cellItemName.Value = dtItems.Rows[i]["st_item_name"].ToString();
        //        tempRow.Cells.Add(cellItemName);

        //        DataGridViewCell cellItemCategory = new DataGridViewTextBoxCell();
        //        cellItemCategory.Value = dtItems.Rows[i]["st_category"].ToString();
        //        tempRow.Cells.Add(cellItemCategory);

        //        DataGridViewCell cellItemPrice = new DataGridViewTextBoxCell();
        //        cellItemPrice.Value = Convert.ToDouble(dtItems.Rows[i]["st_item_price"].ToString()).ToString("f");
        //        tempRow.Cells.Add(cellItemPrice);

        //        DataGridViewCell cellItemStatus = new DataGridViewTextBoxCell();
        //        if (dtItems.Rows[i]["st_active"].ToString() == "T")
        //            cellItemStatus.Value = "ACTIVE";
        //        else
        //            cellItemStatus.Value = "DEACTIVE";
        //        //cellItemStatus.Value = dtItems.Rows[i]["SIM_ACTIVE"].ToString();



        //        tempRow.Cells.Add(cellItemStatus);

        //        gvIndentDetails.Rows.Add(tempRow);
        //    }
        //}


        private DataTable Get_Stationary_Item_Details(string sCatageoriId, string sCompCode, string sState, string sBCod)
        {
            objSQLDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            DataTable dtDtl = new DataTable();

            try
            {
                param[0] = objSQLDB.CreateParameter("@xItemCatagory", DbType.String, sCatageoriId, ParameterDirection.Input);
                param[1] = objSQLDB.CreateParameter("@cmp_cd", DbType.String, sCompCode, ParameterDirection.Input);
                param[2] = objSQLDB.CreateParameter("@StateCode", DbType.String, sState, ParameterDirection.Input);
                param[3] = objSQLDB.CreateParameter("@xBranchCode", DbType.String, sBCod, ParameterDirection.Input);
                dt = objSQLDB.ExecuteDataSet("Get_Stationary_Item_Details", CommandType.StoredProcedure, param).Tables[0];
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
            return dt;
        }
        private void FillStationaryItemDetailsToGrid(string CatageoriId, string CompCode, string State, string BranchCode)
        {
            gvIndentDetails.Rows.Clear();
            objSQLDB = new SQLDB();
            DataTable dtItems = null;
           
            try
            {
                dtItems = Get_Stationary_Item_Details(CatageoriId, CompCode, State, BranchCode);
                if (dtItems.Rows.Count > 0)
                {
                    UtilityLibrary.AutoCompleteTextBox(txtItemDesc, dtItems, "", "st_item_name");
                    for (int i = 0; i < dtItems.Rows.Count; i++)

                    {

                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = (i + 1).ToString();
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellItemCompany = new DataGridViewTextBoxCell();
                        cellItemCompany.Value = dtItems.Rows[i]["st_company_code"].ToString();
                        tempRow.Cells.Add(cellItemCompany);

                        DataGridViewCell cellItemState = new DataGridViewTextBoxCell();
                        cellItemState.Value = dtItems.Rows[i]["st_state_code"].ToString();
                        tempRow.Cells.Add(cellItemState);

                        DataGridViewCell cellItemBranchCode = new DataGridViewTextBoxCell();
                        cellItemBranchCode.Value = dtItems.Rows[i]["st_branch_code"].ToString();
                        tempRow.Cells.Add(cellItemBranchCode);

                        DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                        cellItemID.Value = dtItems.Rows[i]["st_item_code"].ToString();
                        tempRow.Cells.Add(cellItemID);

                        DataGridViewCell cellItemCategory = new DataGridViewTextBoxCell();
                        cellItemCategory.Value = dtItems.Rows[i]["st_category"].ToString();
                        tempRow.Cells.Add(cellItemCategory);

                        DataGridViewCell cellItemCompanyName = new DataGridViewTextBoxCell();
                        cellItemCompanyName.Value = dtItems.Rows[i]["st_company_name"].ToString();
                        tempRow.Cells.Add(cellItemCompanyName);                       

                        DataGridViewCell cellItemStateName = new DataGridViewTextBoxCell();
                        cellItemStateName.Value = dtItems.Rows[i]["st_state_name"].ToString();
                        tempRow.Cells.Add(cellItemStateName);                       

                        DataGridViewCell cellItemBranchName = new DataGridViewTextBoxCell();
                        cellItemBranchName.Value = dtItems.Rows[i]["st_branch_name"].ToString();
                        tempRow.Cells.Add(cellItemBranchName);
                                               
                        DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                        cellItemName.Value = dtItems.Rows[i]["st_item_name"].ToString();
                        tempRow.Cells.Add(cellItemName);
                                              

                        //DataGridViewCell cellItemCategoryName = new DataGridViewTextBoxCell();
                        //cellItemCategoryName.Value = dtItems.Rows[i]["st_categoryName"].ToString();
                        //tempRow.Cells.Add(cellItemCategoryName);

                        DataGridViewCell cellItemPrice = new DataGridViewTextBoxCell();
                        cellItemPrice.Value = Convert.ToDouble(dtItems.Rows[i]["st_item_price"].ToString()).ToString("f");
                        tempRow.Cells.Add(cellItemPrice);

                        DataGridViewCell cellItemStatus = new DataGridViewTextBoxCell();
                        if (dtItems.Rows[i]["st_active"].ToString() == "T")
                            cellItemStatus.Value = "ACTIVE";
                        else
                            cellItemStatus.Value = "DEACTIVE";
                        //cellItemStatus.Value = dtItems.Rows[i]["SIM_ACTIVE"].ToString();

                        tempRow.Cells.Add(cellItemStatus);

                        gvIndentDetails.Rows.Add(tempRow);
                       
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                dtItems = null;
                objSQLDB = null;
            }
        }
      
        //private DataTable GetItems()
        //{
        //    objSQLDB = new SQLDB();
        //    DataTable dt = null;
        //try
        //{
        //    string sqlText = "";
        //    sqlText = "SELECT" +
        //                " SIM_ITEM_CODE" +
        //                ",SIM_ITEM_NAME" +
        //                ",SIM_ITEM_CATEGORY" +
        //                ",SIM_ITEM_PRICE" +
        //                ",SIM_WITH_COMPANY" +
        //                ",SIM_ACTIVE" +
        //                ",SIM_COMPANY_CODE" +
        //                ",CM_COMPANY_NAME" +
        //                ",SIM_STATE" +
        //                ",sm_state" +
        //                ",SIM_BRANCH_CODE" +
        //                ",BRANCH_NAME" +
        //                " FROM STATIONARY_ITEMS_MASTER " +
        //                " LEFT JOIN  COMPANY_MAS ON SIM_COMPANY_CODE=CM_COMPANY_CODE " +
        //                " LEFT JOIN  BRANCH_MAS ON SIM_BRANCH_CODE=BRANCH_CODE " +
        //                " LEFT JOIN  state_mas ON SIM_STATE=sm_state_code " +
        //                " WHERE SIM_ITEM_CATEGORY = '" + cbCategory.SelectedValue.ToString() + "'";
        //    dt = objSQLDB.ExecuteDataSet(sqlText).Tables[0];
        //    return dt;
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.ToString());
        //    return dt;
        //}
        //finally
        //{
        //    dt = null;
        //    objSQLDB = null;
        //}
        //}

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {               
                    if (e.ColumnIndex == gvIndentDetails.Columns["Edit"].Index)
                    {
                        IsModify = true;
                        txtItemId.Enabled = false; 

                        txtItemId.Text = gvIndentDetails.Rows[e.RowIndex].Cells["ItemID"].Value.ToString();
                        txtItemDesc.Text = gvIndentDetails.Rows[e.RowIndex].Cells["ItemName"].Value.ToString();
                        cbCategory.SelectedValue = gvIndentDetails.Rows[e.RowIndex].Cells["Category"].Value.ToString();

                        if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["CompanyCode"].Index].Value) == ""
                            || Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["CompanyCode"].Index].Value) == " ")
                        {
                            cbCompany.SelectedValue = "ALL";
                        }
                        else
                        {
                            if (cbCompany.Items.Count != 0)
                                cbCompany.SelectedValue = gvIndentDetails.Rows[e.RowIndex].Cells["CompanyCode"].Value.ToString();
                        }


                        if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["StateCode"].Index].Value) == ""
                            || Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["StateCode"].Index].Value) == " ")
                        {
                            cbState.SelectedValue = "ALL";
                        }
                        else
                        {
                            if (cbState.Items.Count != 0)
                                cbState.SelectedValue = gvIndentDetails.Rows[e.RowIndex].Cells["StateCode"].Value.ToString();
                        }

                        if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["BranchCode"].Index].Value) == ""
                            || Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["BranchCode"].Index].Value) == " ")
                        {
                            cbLocation.SelectedValue = "ALL";
                        }
                        else
                        {
                            if (cbLocation.Items.Count != 0)
                                cbLocation.SelectedValue = gvIndentDetails.Rows[e.RowIndex].Cells["BranchCode"].Value.ToString();
                        }

                        txtItemPrice.Text = gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value.ToString();

                        if (gvIndentDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "ACTIVE")
                        {
                            chkActive.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            chkActive.CheckState = CheckState.Unchecked;
                        }
                                               
                    }
               
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbLocation.SelectedIndex = 0;
            cbState.SelectedIndex = 0;
            //txtItemId.Text = "";
            txtItemDesc.Text = "";
            txtItemPrice.Text = "";           
            IsModify = false;
            chkActive.CheckState = CheckState.Checked;
            txtItemId.Text = GenerateItemID().ToString();
            FillStationaryItemDetailsToGrid("","","","");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            int iRes = 0;
            string sStatus = "";
            if (chkActive.CheckState == CheckState.Checked)
                sStatus = "T";
            else
                sStatus = "F";
            if (CheckData())
            {
                try
                {
                    if (cbCompany.SelectedIndex > 0)
                        strCompany = cbCompany.SelectedValue.ToString();
                    else
                        strCompany = "";
                    
                    if (cbState.SelectedIndex > 0)                    
                        strState = cbState.SelectedValue.ToString();                    
                    else                    
                        strState = "";
                    
                    if (cbLocation.SelectedIndex > 0)
                        strBranch = cbLocation.SelectedValue.ToString();
                    else
                        strBranch = "";

                    if (IsModify == false)
                    {
                        txtItemId.Text = GenerateItemID().ToString();
                        sqlText = " INSERT INTO STATIONARY_ITEMS_MASTER" +
                                    "(SIM_ITEM_CODE" +
                                    ",SIM_ITEM_NAME" +
                                    ",SIM_ITEM_CATEGORY" +
                                    ",SIM_ITEM_PRICE" +
                                    ",SIM_ACTIVE" +
                                    ",SIM_COMPANY_CODE" +
                                    ",SIM_STATE" +
                                    ",SIM_BRANCH_CODE" +
                                    ",SIM_CREATED_BY" +
                                    ",SIM_CREATED_DATE)" +
                                    " VALUES(" +
                                    "'" + txtItemId.Text.ToString() +
                                    "','" + txtItemDesc.Text +
                                    "','" + cbCategory.SelectedValue.ToString() +
                                    "','" + Convert.ToDouble(txtItemPrice.Text.ToString()).ToString("f") +
                                    "','" + sStatus + "','"+ strCompany +"','"+ strState +"','"+ strBranch +
                                    "','" + CommonData.LogUserId +
                                    "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "')";
                    }
                    else
                    {
                        sqlText = " UPDATE STATIONARY_ITEMS_MASTER SET" +
                                    " SIM_ITEM_NAME = '" + txtItemDesc.Text +
                                    "',SIM_ITEM_CATEGORY = '" + cbCategory.SelectedValue.ToString() +
                                    "',SIM_ITEM_PRICE = '" + Convert.ToDouble(txtItemPrice.Text.ToString()).ToString("f") +
                                    "',SIM_ACTIVE = '" + sStatus +            
                                    "',SIM_COMPANY_CODE = '" + strCompany + 
                                    "',SIM_STATE = '" + strState +                      
                                    "',SIM_BRANCH_CODE = '" + strBranch +
                                    "',SIM_LAST_MODIFIED_BY = '" + CommonData.LogUserId +
                                    "',SIM_LAST_MODIFIED_DATE = '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "' WHERE SIM_ITEM_CODE = '" + txtItemId.Text.ToString() + "'";
                    }
                    objSQLDB = new SQLDB();
                    if (sqlText.Length > 10)
                        iRes = objSQLDB.ExecuteSaveData(sqlText);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Succesfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemId.Text = GenerateItemID().ToString();
                        //FillItemCategory();
                        FillCompanyData();
                        FillSateData();
                        FillBranchData();
                        cbCategory.SelectedIndex = 1;
                        cbCompany.SelectedIndex = 0;
                        cbState.SelectedIndex = 0;
                        cbLocation.SelectedIndex =0;
                    
                        txtItemPrice.Text = "";
                        txtItemDesc.Text = "";                       
                      
                        IsModify =false;
                        chkActive.CheckState = CheckState.Checked;
                        FillStationaryItemDetailsToGrid("", "", "", "");

                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool CheckData()
        {
            bool bFlag = true;
            if (txtItemDesc.Text.Trim().Length < 4)
            {
                bFlag = false;
                MessageBox.Show("Enter Valid Item Discription/Name!", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bFlag;
            }
            if (txtItemPrice.Text.Trim().Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Enter Item Price!", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bFlag;
            }
            if (cbCategory.SelectedIndex == 0 || cbCategory.SelectedIndex==-1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Category", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bFlag;
            }
            return bFlag;
        }

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();

            if (IsModify == false)
            {
                if (cbCategory.SelectedIndex > 0)
                    strCategory = cbCategory.SelectedValue.ToString();
                else
                    strCategory = "";

                if (cbCompany.SelectedIndex > 0 )
                    strCompany = cbCompany.SelectedValue.ToString();
                else
                    strCompany = "";

                if (cbState.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strState = cbState.SelectedValue.ToString();
                else
                    strState = "";

                if (cbLocation.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strBranch = cbLocation.SelectedValue.ToString();
                else
                    strBranch = "";
             
                    FillStationaryItemDetailsToGrid(strCategory, strCompany, strState, strBranch);           

              
            }

        }
       
        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSateData();
            if (IsModify == false)
            {                
                txtItemDesc.Text = "";
                txtItemPrice.Text = "";
                txtItemsCount.Text = "";
                txtIndentAmt.Text = "";
                 
                if (cbCategory.SelectedIndex > 0)
                    strCategory = cbCategory.SelectedValue.ToString();
                else
                    strCategory = "";

                if (cbCompany.SelectedIndex > 0)
                    strCompany = cbCompany.SelectedValue.ToString();
                else
                    strCompany = "";

                if (cbState.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strState = cbState.SelectedValue.ToString();
                else
                    strState = "";

                if (cbLocation.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strBranch = cbLocation.SelectedValue.ToString();
                else
                    strBranch = "";
            
                    FillStationaryItemDetailsToGrid(strCategory, strCompany, strState, strBranch);
            
            }            
                
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsModify == false)
            {
                txtItemDesc.Text = "";
                txtItemPrice.Text = "";
                txtItemsCount.Text = "";
                txtIndentAmt.Text = "";

                if (cbCategory.SelectedIndex > 0)
                    strCategory = cbCategory.SelectedValue.ToString();
                else
                    strCategory = "";

                if (cbCompany.SelectedIndex > 0)
                    strCompany = cbCompany.SelectedValue.ToString();
                else
                    strCompany = "";

                if (cbState.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strState = cbState.SelectedValue.ToString();
                else
                    strState = "";

                if (cbLocation.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strBranch = cbLocation.SelectedValue.ToString();
                else
                    strBranch = "";
          
                FillStationaryItemDetailsToGrid(strCategory, strCompany, strState, strBranch);
               
            }
           
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsModify == false)
            {
                txtItemDesc.Text = "";
                txtItemPrice.Text = "";
                txtItemsCount.Text = "";
                txtIndentAmt.Text = "";


                if (cbCategory.SelectedIndex > 0)
                    strCategory = cbCategory.SelectedValue.ToString();
                else
                    strCategory = "";

                if (cbCompany.SelectedIndex > 0)
                    strCompany = cbCompany.SelectedValue.ToString();
                else
                    strCompany = "";

                if (cbState.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strState = cbState.SelectedValue.ToString();
                else
                    strState = "";

                if (cbLocation.SelectedIndex > 0 && cbCompany.SelectedIndex > 0)
                    strBranch = cbLocation.SelectedValue.ToString();
                else
                    strBranch = "";
            
                    FillStationaryItemDetailsToGrid(strCategory, strCompany, strState, strBranch);
               
            }


        }

       
    }
}
