using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using SSCRM.App_Code;


namespace SSCRM
{
    public partial class MajorCostCenter_Details : Form
    {
        SQLDB objSqlDB = null;      
        string StrCommand = null;
        int CenterId = 0;
        private bool flagUpdate = false;
        public DataTable dtCostCenterDetails = new DataTable();
        public MajorCostCenter_Details()
        {
            InitializeComponent();
        }
        private void MajorCostCenter_Details_Load(object sender, EventArgs e)
        {
             txtConpanyName.Text = CommonData.CompanyName;
             //GenerateMajorCostCenterId();
            #region "CREATE PRODUCT_DETAILS TABLE"
            dtCostCenterDetails.Columns.Add("SlNo_CostCenter");
            dtCostCenterDetails.Columns.Add("CostCenterId");
            dtCostCenterDetails.Columns.Add("CostCenterName");
            dtCostCenterDetails.Columns.Add("CostCenterShortName");
            dtCostCenterDetails.Columns.Add("CostCenterType");
            
            dtCostCenterDetails.Columns.Add("CostCenterTag");
            dtCostCenterDetails.Columns.Add("CostCenterOB");
            #endregion
            //DataTable dtCCNameDetails = objSqlDB.ExecuteDataSet("SELECT MCC_MAJOR_COST_CENTRE_ID , MCC_MAJOR_COST_CENTRE_NAME FROM FA_MAJOR_COST_CENTRE").Tables[0];
            //DataTable dtCCSnameDetails = objSqlDB.ExecuteDataSet("SELECT MCC_MAJOR_COST_CENTRE_ID,MCC_SHORT_NAME FROM FA_MAJOR_COST_CENTRE").Tables[0];
            //UtilityLibrary.AutoCompleteTextBox(txtMajorCostCenterName, dtCCNameDetails, "", "MCC_MAJOR_COST_CENTRE_NAME");
            //UtilityLibrary.AutoCompleteTextBox(txtMajorCostCenterSName, dtCCSnameDetails, "", "MCC_SHORT_NAME");
           
            FillMajorCostNames();  

        }
        private void FillMajorCostNames()
        {
            string strCommand = "";
            try
            {
                objSqlDB = new SQLDB();
                strCommand = "SELECT MCC_MAJOR_COST_CENTRE_ID,MCC_MAJOR_COST_CENTRE_NAME FROM FA_MAJOR_COST_CENTRE where mcc_company_code='" + CommonData.CompanyCode + "'";
                DataTable dtCCNameDetails = objSqlDB.ExecuteDataSet(strCommand).Tables[0];
                UtilityLibrary.AutoCompleteTextBox(txtMajorCostCenterName, dtCCNameDetails, "", "MCC_MAJOR_COST_CENTRE_NAME");
                objSqlDB = null;

                strCommand = "";
                objSqlDB = new SQLDB();
                strCommand = "SELECT MCC_MAJOR_COST_CENTRE_ID,MCC_SHORT_NAME FROM FA_MAJOR_COST_CENTRE where mcc_company_code='" + CommonData.CompanyCode + "'";

                DataTable dtCCSNameDetails = objSqlDB.ExecuteDataSet(strCommand).Tables[0];

                UtilityLibrary.AutoCompleteTextBox(txtMajorCostCenterSName, dtCCSNameDetails, "", "MCC_SHORT_NAME");
                objSqlDB = null;
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlDB = null;

            }
        }
        private void GenerateMajorCostCenterId()
        {
            objSqlDB = new SQLDB();
            DataTable dt = null;
            string strCommand = "";
            try
            {
                string BranchCode = CommonData.BranchCode;
                //string MajorCCId = "EXP" + BranchCode + "-";
                string MajorCCId = txtMajorCostCenterName.Text.ToString().Substring(0,4) +""+BranchCode ;

                //strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(MCC_MAJOR_COST_CENTRE_ID,'" + MajorCCId + "'),14,15)),0) + 1 " +
                //    " FROM FA_MAJOR_COST_CENTRE WHERE MCC_COMPANY_CODE='" + CommonData.CompanyCode + "' ";

                //dt = objSqlDB.ExecuteDataSet(strCommand).Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                txtMajorCCId.Text = MajorCCId;//+ Convert.ToString(Convert.ToInt32(dt.Rows[0][0]).ToString());
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                dt = null;
                objSqlDB = null;
            }
        }

        private void btnAddCostCenteretails_Click(object sender, EventArgs e)
        {
            if (txtMajorCostCenterName.Text != "")
            {
                CenterId = 0;
                CenterId = (gvCostCenterDetails.Rows.Count) + 1;

                CostCenter objCostCenter = new CostCenter(CenterId);
                objCostCenter.objMajorCostCenterDetails = this;
                objCostCenter.Show();
            }
            else
            {
                MessageBox.Show("Please Enter MajorCostCenterName", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMajorCostCenterName.Focus();
            }
        }

        public void GetCostCenterDetails()
        {
            int intRow = 1;
            gvCostCenterDetails.Rows.Clear();

            try
            {
                if (dtCostCenterDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCostCenterDetails.Rows.Count; i++)
                    {

                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtCostCenterDetails.Rows[i]["SlNo_CostCenter"] = intRow;
                        tempRow.Cells.Add(cellSLNO);


                        DataGridViewCell cellCostCenterId = new DataGridViewTextBoxCell();
                        cellCostCenterId.Value = dtCostCenterDetails.Rows[i]["CostCenterId"].ToString();
                        tempRow.Cells.Add(cellCostCenterId);

                        DataGridViewCell cellCostCenterName = new DataGridViewTextBoxCell();
                        cellCostCenterName.Value = dtCostCenterDetails.Rows[i]["CostCenterName"].ToString();
                        tempRow.Cells.Add(cellCostCenterName);

                        DataGridViewCell cellCostCenterShortName = new DataGridViewTextBoxCell();
                        cellCostCenterShortName.Value = dtCostCenterDetails.Rows[i]["CostCenterShortName"].ToString();
                        tempRow.Cells.Add(cellCostCenterShortName);

                        DataGridViewCell cellCostCenterType = new DataGridViewTextBoxCell();
                        cellCostCenterType.Value = dtCostCenterDetails.Rows[i]["CostCenterType"].ToString();
                        tempRow.Cells.Add(cellCostCenterType);

                        

                        DataGridViewCell cellCostCenterTag = new DataGridViewTextBoxCell();
                        cellCostCenterTag.Value = dtCostCenterDetails.Rows[i]["CostCenterTag"].ToString();
                        tempRow.Cells.Add(cellCostCenterTag);

                        DataGridViewCell cellCostCenterOpeningBal = new DataGridViewTextBoxCell();
                        cellCostCenterOpeningBal.Value = dtCostCenterDetails.Rows[i]["CostCenterOB"].ToString();
                        tempRow.Cells.Add(cellCostCenterOpeningBal);

                       
                        intRow = intRow + 1;
                        gvCostCenterDetails.Rows.Add(tempRow);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private bool CheckDetails()
        {
            bool blValue = true;
            if (txtMajorCostCenterName.Text.Length == 0)
            {
                MessageBox.Show("Enter MajorCost Center Name", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtMajorCostCenterName.Focus();
                return blValue;
            }
            if (txtMajorCostCenterSName.Text.Length == 0)
            {
                MessageBox.Show("Enter MajorCost Center Short Name", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtMajorCostCenterSName.Focus();
                return blValue;
            }
            if (txtOpeningBal.Text.Length == 0)
            {
                MessageBox.Show("Enter Opening Balance", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOpeningBal.Focus();
                return blValue;
            }
            //if (gvCostCenterDetails.Rows.Count == 0)
            //{
            //    MessageBox.Show("Add Cost Center Details ", "Major Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    txtMajorCostCenterSName.Focus();
            //    return blValue;
            //}
            return blValue;
        }
        private int SaveMajorCostCenterDetails()
        {
            int iRes = 0;
            objSqlDB = new SQLDB();
            if (flagUpdate == true)
            {
                for (int i = 0; i < gvCostCenterDetails.Rows.Count; i++)
                {

                    StrCommand = "DELETE FROM FA_COST_CENTRE WHERE CC_COST_CENTRE_ID='" + gvCostCenterDetails.Rows[i].Cells["CostCenterId"].Value.ToString() +
                                                              "' AND CC_COMPANY_CODE='" + CommonData.CompanyCode +
                                                              "'AND CC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString() + "' ";
                    iRes = objSqlDB.ExecuteSaveData(StrCommand);
                    StrCommand = "";

                }              
            }

            try
            {
                if (flagUpdate == true)
                {
                    StrCommand = "UPDATE FA_MAJOR_COST_CENTRE SET MCC_COMPANY_CODE='" + CommonData.CompanyCode +
                                                                    "',MCC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString().Trim().Replace( " ", "").ToUpper() +
                                                                    "', MCC_MAJOR_COST_CENTRE_NAME='" + txtMajorCostCenterName.Text.ToString().Trim().Replace("'", "").ToUpper() +
                                                                    "',MCC_SHORT_NAME='" + txtMajorCostCenterSName.Text.ToString().Trim().Replace("'", "").ToUpper() +
                                                                    "',MCC_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                                                    "',MCC_LAST_MODIFIED_DATE=getdate()" +
                                                                    "  WHERE MCC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString() +
                                                                    "' AND MCC_COMPANY_CODE='" + CommonData.CompanyCode + "'";


                }
                if (flagUpdate == false)
                {
                    GenerateMajorCostCenterId();
                    StrCommand = "INSERT INTO FA_MAJOR_COST_CENTRE (MCC_COMPANY_CODE" +
                                                                    ",MCC_MAJOR_COST_CENTRE_ID" +
                                                                    ",MCC_MAJOR_COST_CENTRE_NAME" +
                                                                    ",MCC_SHORT_NAME" +
                                                                    ",MCC_CREATED_BY" +
                                                                    ",MCC_CREATED_DATE)VALUES" +
                                                                    "('" + CommonData.CompanyCode +
                                                                    "','" + txtMajorCCId.Text.ToString().Trim().Replace(" ", "").ToUpper() +
                                                                    "','" + txtMajorCostCenterName.Text.ToString().Trim().Replace("'", "").ToUpper() +
                                                                    "','" + txtMajorCostCenterSName.Text.ToString().Trim().Replace("'", "").ToUpper() +
                                                                    "','" + CommonData.LogUserId + "',getdate())";
                }
                if (StrCommand.Length > 10)
                {
                    objSqlDB = new SQLDB();
                    iRes = objSqlDB.ExecuteSaveData(StrCommand);
                }

                SaveCostCenterDetails();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;
        }

        private void SaveCostCenterDetails()
        {
            int iRes = 0;
            objSqlDB = new SQLDB();
            StrCommand = "";
            try
            {
                for (int i = 0; i < gvCostCenterDetails.Rows.Count; i++)
                {
                    StrCommand += "INSERT INTO FA_COST_CENTRE (CC_COMPANY_CODE" +
                                                                    ",CC_COST_CENTRE_ID" +
                                                                    ",CC_COST_CENTRE_NAME" +
                                                                    ",CC_SHORT_NAME" +
                                                                    ",CC_MAJOR_COST_CENTRE_ID" +
                                                                    ",CC_TYPE " +
                                                                    ",CC_CREATED_BY" +
                                                                    ",CC_CREATED_DATE)VALUES" +
                                                                    "('" + CommonData.CompanyCode +
                                                                    "'," + gvCostCenterDetails.Rows[i].Cells["CostCenterId"].Value +
                                                                    ",'" + gvCostCenterDetails.Rows[i].Cells["CostCenterName"].Value +
                                                                    "','" + gvCostCenterDetails.Rows[i].Cells["CostCenterShortName"].Value +
                                                                    "','" + txtMajorCCId.Text.ToString() +
                                                                    "','" + gvCostCenterDetails.Rows[i].Cells["CostCenterTag"].Value +
                                                                    "','" + CommonData.LogUserId + "',getdate())";
                }
                if (StrCommand.Length > 10)
                {
                    iRes = objSqlDB.ExecuteSaveData(StrCommand);
                }


            }
            catch (Exception ex)
            {
                StrCommand = "DELETE FROM FA_MAJOR_COST_CENTRE WHERE MCC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString() +
                                                              "' AND MCC_COMPANY_CODE='" + CommonData.CompanyCode + "' ";
                MessageBox.Show(ex.ToString());
            }
           
        }    

       
        private void gvCostCenterDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == gvCostCenterDetails.Columns["Edit_CostCenterDel"].Index)
                {
                    if (Convert.ToBoolean(gvCostCenterDetails.Rows[e.RowIndex].Cells["Edit_CostCenterDel"].Selected) == true)
                    {


                        Int32 SlNo = Convert.ToInt32(gvCostCenterDetails.Rows[e.RowIndex].Cells["SlNo_CostCenter"].Value);
                        DataRow[] dr = dtCostCenterDetails.Select("SlNo_CostCenter=" + SlNo);

                        CostCenter objCostCenter = new CostCenter(dtCostCenterDetails, dr);
                        objCostCenter.objMajorCostCenterDetails = this;
                        objCostCenter.ShowDialog();

                    }

                }
                if (e.ColumnIndex == gvCostCenterDetails.Columns["Del_CostCenterDel"].Index)
                {
                    if (Convert.ToBoolean(gvCostCenterDetails.Rows[e.RowIndex].Cells["Del_CostCenterDel"].Selected) == true)
                    {
                        DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.Yes)
                        {
                            DataGridViewRow dgvr = gvCostCenterDetails.Rows[e.RowIndex];
                            gvCostCenterDetails.Rows.Remove(dgvr);
                        }


                        if (gvCostCenterDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvCostCenterDetails.Rows.Count; i++)
                            {
                                gvCostCenterDetails.Rows[i].Cells["SlNo_CostCenter"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }

            }
        }

        private void txtMajorCostCenterName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsLetter((e.KeyChar)))
            //    {
            //        e.Handled = true;
            //    }
            //}
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtCostCenterSName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private DataSet GetCostCenterDetails(string MajorCCId, string MajorCCName, string MajorCCSName)
        {
            objSqlDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSqlDB.CreateParameter("@xMajorCostCenterId", DbType.String, MajorCCId, ParameterDirection.Input);
                param[1] = objSqlDB.CreateParameter("@xMajorCostCenterName", DbType.String, MajorCCName, ParameterDirection.Input);
                param[2] = objSqlDB.CreateParameter("@xMajorCostCenterSName", DbType.String, MajorCCSName, ParameterDirection.Input);
                ds = objSqlDB.ExecuteDataSet("GetCostCenterDetails", CommandType.StoredProcedure, param);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSqlDB = null;
            }
            return ds;
        }


        private void GetMajorCostCenterDetails(string MajorId, string MajorName, string MajorSname)
        {
            objSqlDB = new SQLDB();
            DataTable dtMajorCCDel = new DataTable();
            gvCostCenterDetails.Rows.Clear();

            if (txtMajorCCId.Text != "" || txtMajorCostCenterName.Text != "" || txtMajorCostCenterSName.Text != "")
            {

                try
                {
                    dtMajorCCDel = GetCostCenterDetails(MajorId, MajorName, MajorSname).Tables[0];

                    if (dtMajorCCDel.Rows.Count > 0)
                    {
                        btnDelete.Enabled = false;
                        flagUpdate = true;
                        txtMajorCCId.Text = dtMajorCCDel.Rows[0]["MajorCostCenterId"].ToString();
                        txtMajorCostCenterName.Text = dtMajorCCDel.Rows[0]["MajorCostCenterName"].ToString();
                        txtMajorCostCenterSName.Text = dtMajorCCDel.Rows[0]["MajorcostCenterShortName"].ToString();
                        if (dtMajorCCDel.Rows[0]["CostMajorid"].ToString() != "")
                        {
                            FillCostCenterDetails(dtMajorCCDel);
                        }

                    }
                    else
                    {
                        //txtMajorCCId.Text = "";
                        //txtMajorCostCenterName.Text = "";
                        //txtMajorCostCenterSName.Text = "";
                        //gvCostCenterDetails.Rows.Clear();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSqlDB = null;

                }
            }

        }        
      
        private void FillCostCenterDetails(DataTable dtCCDel)
        {
            gvCostCenterDetails.Rows.Clear();
            dtCostCenterDetails.Rows.Clear();

            if (txtMajorCCId.Text.Length > 0 || txtMajorCostCenterName.Text.Length>0 || txtMajorCostCenterSName.Text.Length>0)
            {
                try
                {
                    if (dtCCDel.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        for (int i = 0; i < dtCCDel.Rows.Count; i++)
                        {
                            dtCostCenterDetails.Rows.Add(new Object[] {"-1", dtCCDel.Rows[i]["CostCenterId"].ToString(),
                                                                       dtCCDel.Rows[i]["costCenterName"].ToString(),                                                                      
                                                                       dtCCDel.Rows[i]["CostCentershortName"].ToString(),
                                                                       dtCCDel.Rows[i]["CostCenterType"].ToString(),

                                                                       dtCCDel.Rows[i]["CostCenterTag"].ToString()});
                            GetCostCenterDetails();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        private void txtMajorCCId_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtMajorCCId.Text != "")
                {
                    
                    GetMajorCostCenterDetails(txtMajorCCId.Text.ToString(),"","");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       
     

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSqlDB = new SQLDB();
            int iRes = 0;
            StrCommand = "";

            if (CheckDetails() == true)
            {
                try
                {
                    if (SaveMajorCostCenterDetails() > 0)
                    {
                        
                            MessageBox.Show("Major Cost Center Data Saved \n MajorCCId: '" + txtMajorCCId.Text.ToString() + "'", "Major Cost Center Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            SaveOpeningBalancesForMajor();
                            SaveOpeningBalancesForSubCost();
                            //GenerateMajorCostCenterId();
                            btnClear_Click(null, null);
                            txtMajorCCId.Focus();
                            FillMajorCostNames();
                           
                        //}
                        //else
                        //{

                        //    StrCommand = "DELETE FROM FA_MAJOR_COST_CENTRE WHERE MCC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString() +
                        //                                       "' AND MCC_COMPANY_CODE='" + CommonData.CompanyCode + "' ";
                        //    iRes = objSqlDB.ExecuteSaveData(StrCommand);
                        //    StrCommand = "";
                        //    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                    }

                    else
                    {
                        StrCommand = "DELETE FROM FA_MAJOR_COST_CENTRE WHERE MCC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString() +
                                                              "' AND MCC_COMPANY_CODE='" + CommonData.CompanyCode + "' ";
                        iRes = objSqlDB.ExecuteSaveData(StrCommand);
                        StrCommand = "";
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void SaveOpeningBalancesForMajor()
        {
            try
            {
                string strCMD = " delete FA_MAJOR_COST_CENTRE_BALANCES where MCCB_COMPANY_CODE='" + CommonData.CompanyCode + "' and MCCB_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text + "' and MCCB_FIN_YEAR='" + CommonData.FinancialYear + "'";
                
                int iMonth = 4;
                for (int i = 0; i < 12; i++)
                {
                    if (iMonth == 4)
                    {
                        strCMD += "insert into FA_MAJOR_COST_CENTRE_BALANCES(MCCB_COMPANY_CODE,MCCB_MAJOR_COST_CENTRE_ID,MCCB_FIN_YEAR,MCCB_MONTH,MCCB_YEAR,MCCB_OPENING_BALANCE,MCCB_DEBIT_AMOUNT,MCCB_CREDIT_AMOUNT,MCCB_CLOSING_BALANCE,MCCB_CREATED_BY,MCCB_CREATED_DATE) values(" +
                         " '" + CommonData.CompanyCode + "','" + txtMajorCCId.Text + "','" + CommonData.FinancialYear + "','" + iMonth + "','" + CommonData.FinancialYear.Split('-')[0] +
                                            "','" + txtOpeningBal.Text + "',0,0,'" + txtOpeningBal.Text + "','" + CommonData.LogUserId + "',getdate() )";
                    }
                    else
                    {
                        if (iMonth > 12)
                            iMonth = 1;
                        if (iMonth < 4)
                            strCMD += "insert into FA_MAJOR_COST_CENTRE_BALANCES(MCCB_COMPANY_CODE,MCCB_MAJOR_COST_CENTRE_ID,MCCB_FIN_YEAR,MCCB_MONTH,MCCB_YEAR,MCCB_OPENING_BALANCE,MCCB_DEBIT_AMOUNT,MCCB_CREDIT_AMOUNT,MCCB_CLOSING_BALANCE,MCCB_CREATED_BY,MCCB_CREATED_DATE) values(" +
                             " '" + CommonData.CompanyCode + "','" + txtMajorCCId.Text + "','" + CommonData.FinancialYear + "','" + iMonth + "','" + CommonData.FinancialYear.Split('-')[1] +
                                                "','" + txtOpeningBal.Text + "',0,0,'" + txtOpeningBal.Text + "','" + CommonData.LogUserId + "',getdate() )";
                        else
                            strCMD += "insert into FA_MAJOR_COST_CENTRE_BALANCES(MCCB_COMPANY_CODE,MCCB_MAJOR_COST_CENTRE_ID,MCCB_FIN_YEAR,MCCB_MONTH,MCCB_YEAR,MCCB_OPENING_BALANCE,MCCB_DEBIT_AMOUNT,MCCB_CREDIT_AMOUNT,MCCB_CLOSING_BALANCE,MCCB_CREATED_BY,MCCB_CREATED_DATE) values(" +
                                " '" + CommonData.CompanyCode + "','" + txtMajorCCId.Text + "','" + CommonData.FinancialYear + "','" + iMonth + "','" + CommonData.FinancialYear.Split('-')[0] +
                                    "','" + txtOpeningBal.Text + "',0,0,'" + txtOpeningBal.Text + "','" + CommonData.LogUserId + "',getdate() )";
                    }
                    iMonth++;
                }
              
                objSqlDB = new SQLDB();
                objSqlDB.ExecuteDataSet(strCMD);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void SaveOpeningBalancesForSubCost()
        {
            try
            {
                 string strCMD="";
                for (int j = 0; j < gvCostCenterDetails.Rows.Count;j++ )
                {
                    strCMD = " delete FA_COST_CENTRE_BALANCES where CCB_COMPANY_CODE='" + CommonData.CompanyCode + "' and CCB_COST_CENTRE_ID='" + gvCostCenterDetails.Rows[j].Cells["CostCenterId"].Value +
                    "' and CCB_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text + "' and CCB_FIN_YEAR='" + CommonData.FinancialYear + "'";

                    int iMonth = 4;
                    for (int i = 0; i < 12; i++)
                    {
                        if (iMonth == 4)
                        {
                            strCMD += "insert into FA_COST_CENTRE_BALANCES(CCB_COMPANY_CODE,CCB_COST_CENTRE_ID,CCB_MAJOR_COST_CENTRE_ID,CCB_FIN_YEAR,CCB_MONTH,CCB_YEAR,CCB_OPENING_BALANCE,CCB_DEBIT_AMOUNT,CCB_CREDIT_AMOUNT,CCB_CLOSING_BALANCE,CCB_CREATED_BY,CCB_CREATED_DATE) values(" +
                             " '" + CommonData.CompanyCode + "','" + gvCostCenterDetails.Rows[j].Cells["CostCenterId"].Value + "','" + txtMajorCCId.Text + "','" + CommonData.FinancialYear + "','" + iMonth + "','" + CommonData.FinancialYear.Split('-')[0] +
                                                "','" + gvCostCenterDetails.Rows[j].Cells["OpenBal"].Value + "',0,0,'" + gvCostCenterDetails.Rows[j].Cells["OpenBal"].Value + "','" + CommonData.LogUserId + "',getdate() )";
                        }
                        else
                        {
                            if (iMonth > 12)
                                iMonth = 1;
                            if (iMonth < 4)
                                strCMD += "insert into FA_COST_CENTRE_BALANCES(CCB_COMPANY_CODE,CCB_COST_CENTRE_ID,CCB_MAJOR_COST_CENTRE_ID,CCB_FIN_YEAR,CCB_MONTH,CCB_YEAR,CCB_OPENING_BALANCE,CCB_DEBIT_AMOUNT,CCB_CREDIT_AMOUNT,CCB_CLOSING_BALANCE,CCB_CREATED_BY,CCB_CREATED_DATE) values(" +
                                 " '" + CommonData.CompanyCode + "','" + gvCostCenterDetails.Rows[j].Cells["CostCenterId"].Value + "','" + txtMajorCCId.Text + "','" + CommonData.FinancialYear + "','" + iMonth + "','" + CommonData.FinancialYear.Split('-')[1] +
                                                    "','" + gvCostCenterDetails.Rows[j].Cells["OpenBal"].Value + "',0,0,'" + gvCostCenterDetails.Rows[j].Cells["OpenBal"].Value + "','" + CommonData.LogUserId + "',getdate() )";
                            else
                                strCMD += "insert into FA_COST_CENTRE_BALANCES(CCB_COMPANY_CODE,CCB_COST_CENTRE_ID,CCB_MAJOR_COST_CENTRE_ID,CCB_FIN_YEAR,CCB_MONTH,CCB_YEAR,CCB_OPENING_BALANCE,CCB_DEBIT_AMOUNT,CCB_CREDIT_AMOUNT,CCB_CLOSING_BALANCE,CCB_CREATED_BY,CCB_CREATED_DATE) values(" +
                                    " '" + CommonData.CompanyCode + "','" + gvCostCenterDetails.Rows[j].Cells["CostCenterId"].Value + "','" + txtMajorCCId.Text + "','" + CommonData.FinancialYear + "','" + iMonth + "','" + CommonData.FinancialYear.Split('-')[0] +
                                        "','" + gvCostCenterDetails.Rows[j].Cells["OpenBal"].Value + "',0,0,'" + gvCostCenterDetails.Rows[j].Cells["OpenBal"].Value + "','" + CommonData.LogUserId + "',getdate() )";
                        }
                        iMonth++;
                    }
                }

                objSqlDB = new SQLDB();
                if(strCMD.Length>10)
                {
                objSqlDB.ExecuteDataSet(strCMD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMajorCCId.Text = "";
            txtMajorCostCenterSName.Text = "";
            txtMajorCostCenterName.Text = "";
            txtOpeningBal.Text = "";
            gvCostCenterDetails.Rows.Clear();
            dtCostCenterDetails.Rows.Clear();
            //GenerateMajorCostCenterId();
            flagUpdate = false;
            //txtConpanyName.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSqlDB = new SQLDB();
            DataTable dt = new DataTable();
            int iRes = 0;
            string strCommand = "";

            if (txtMajorCCId.Text != "" && flagUpdate == true)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        strCommand += "DELETE FROM FA_COST_CENTRE WHERE CC_COMPANY_CODE='" + CommonData.CompanyCode +
                                              "' AND CC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString() + "' ";

                        strCommand += "DELETE FROM FA_MAJOR_COST_CENTRE WHERE MCC_MAJOR_COST_CENTRE_ID='" + txtMajorCCId.Text.ToString() +
                                                                                  "' AND MCC_COMPANY_CODE='" + CommonData.CompanyCode + "' ";

                        if (strCommand.Length > 10)
                        {
                            iRes = objSqlDB.ExecuteSaveData(strCommand);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objSqlDB = null;

                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                        gvCostCenterDetails.Rows.Clear();
                        flagUpdate = false;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void txtMajorCCId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtMajorCostCenterName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtMajorCostCenterName.Text != "")
                {

                    GetMajorCostCenterDetails("",txtMajorCostCenterName.Text.ToString(),"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtMajorCostCenterSName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtMajorCostCenterSName.Text != "")
                {

                    GetMajorCostCenterDetails("","",txtMajorCostCenterSName.Text.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtMajorCostCenterName_TextChanged(object sender, EventArgs e)
        {
            //txtMajorCCId.Text = "";
            //txtMajorCostCenterSName.Text = "";
            //gvCostCenterDetails.Rows.Clear();
        }

        private void txtMajorCostCenterSName_TextChanged(object sender, EventArgs e)
        {
            //txtMajorCCId.Text = "";
            //txtMajorCostCenterName.Text = "";
            //gvCostCenterDetails.Rows.Clear();

        }

        private void txtOpeningBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            OpeningBalances btnBalance = new OpeningBalances(txtMajorCCId.Text, CommonData.FinancialYear,CommonData.CompanyCode,"MAJOR");
            btnBalance.ShowDialog();
        }
      
    }
}


       
    

