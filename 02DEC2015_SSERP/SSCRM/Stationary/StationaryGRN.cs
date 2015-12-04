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
    public partial class StationaryGRN : Form
    {
        SQLDB objSQLDB = new SQLDB();
        bool flagText = false;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        DateTime dtpGRNDate;
        string strFinYear = "";
       
        public StationaryGRN()
        {
            InitializeComponent();
        }
        private void StationaryGRN_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            UtilityLibrary.PopulateControl(cbCompany, objSQLDB.ExecuteDataSet(" SELECT  CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS INNER JOIN BRANCH_MAS ON CM_COMPANY_CODE=COMPANY_CODE WHERE BRANCH_TYPE='ST'").Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            UtilityLibrary.PopulateControl(cbSupplierCode, objSQLDB.ExecuteDataSet("SELECT SM_SUPPLIER_CODE,SM_SUPPLIER_NAME FROM SUPPLIER_MASTER").Tables[0].DefaultView, 0, 0, "-- Select --","-- Select --");
            objSQLDB = null;
            dtDC.Value = System.DateTime.Now;
            dtGrn.Value = System.DateTime.Now;
            dtPO.Value = System.DateTime.Now;
            getFinYear();
            //txtGrnNo.Text = GenerateGrnNo();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("StationaryGRN");
            ItemSearch.objStationaryGRN = this;
            ItemSearch.ShowDialog();
        }
        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
        }
        //private void txtSupCode_Validated(object sender, EventArgs e)
        //{
        //    objSQLDB = new SQLDB();
        //    DataSet ds = objSQLDB.ExecuteDataSet("SELECT * FROM SUPPLIER_MASTER WHERE SM_SUPPLIER_CODE='" + cbSupplierCode.SelectedValue.ToString() + "'");
        //    if (ds.Tables[0].Rows.Count > 0)
        //        txtSupplyerName.Text = ds.Tables[0].Rows[0]["SM_SUPPLIER_NAME"].ToString();
        //    else
        //        txtSupplyerName.Text = "";
        //    objSQLDB = null;
        //}
        private void txtDCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }
        private void gvIndentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                try
                {
                    if (gvIndentDetails.Rows[e.RowIndex].Cells["DcBull"].Value != null)
                    {
                        if (UtilityFunctions.IsNumeric(gvIndentDetails.Rows[e.RowIndex].Cells["DcBull"].Value.ToString()) == false)
                            gvIndentDetails.Rows[e.RowIndex].Cells["DcBull"].Value = null;
                    }
                    if (gvIndentDetails.Rows[e.RowIndex].Cells["Accepted"].Value != null)
                    {
                        if (UtilityFunctions.IsNumeric(gvIndentDetails.Rows[e.RowIndex].Cells["Accepted"].Value.ToString()) == false)
                            gvIndentDetails.Rows[e.RowIndex].Cells["Accepted"].Value = null;
                    }
                    if ((gvIndentDetails.Rows[e.RowIndex].Cells["DcBull"].Value != null) && (gvIndentDetails.Rows[e.RowIndex].Cells["Accepted"].Value != null))
                    {
                        int ival = Convert.ToInt32(gvIndentDetails.Rows[e.RowIndex].Cells["DcBull"].Value) - Convert.ToInt32(gvIndentDetails.Rows[e.RowIndex].Cells["Accepted"].Value);
                        gvIndentDetails.Rows[e.RowIndex].Cells["Shortage"].Value = ival.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter valid numbers", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == false)
                return;
            objSQLDB = new SQLDB();
            try
            {
               
                lblBranchCode.Text = objSQLDB.ExecuteDataSet("SELECT BRANCH_CODE FROM BRANCH_MAS WHERE BRANCH_TYPE='ST' AND COMPANY_CODE='" + cbCompany.SelectedValue + "'").Tables[0].Rows[0][0].ToString();

                DataSet DsExist = objSQLDB.ExecuteDataSet("SELECT * FROM STATIONARY_GRN_HEAD WHERE SGH_COMPANY_CODE='" + cbCompany.SelectedValue + 
                                                          "' AND SGH_BRANCH_CODE='" + lblBranchCode.Text + "' AND SGH_FIN_YEAR='"+strFinYear+
                                                          "' AND SGH_GRN_NO=" + txtGrnNo.Text + "" );
                int iRetVal = 0;
                if (DsExist.Tables[0].Rows.Count == 0)
                {
                    txtGrnNo.Text = GenerateGrnNo();

                    string sqlQry = " INSERT INTO STATIONARY_GRN_HEAD (SGH_COMPANY_CODE" +
                                                                      ",SGH_BRANCH_CODE" +
                                                                      ",SGH_SUPPLIER_CODE " +
                                                                      ",SGH_GRN_NO" +
                                                                      ",SGH_DC_OR_BILL_NO " +
                                                                      ",SGH_PO_NO " +
                                                                      ",SGH_GRN_DATE " +
                                                                      ",SGH_DC_OR_BILL_DATE " +
                                                                      ",SGH_PO_DATE " +
                                                                      ",SGH_FIN_YEAR " +
                                                                      ",SGH_CREATED_BY" +
                                                                      ",SGH_CREATED_DATE)VALUES" +
                                                                      "('" + cbCompany.SelectedValue +
                                                                      "','" + lblBranchCode.Text +
                                                                      "','" + cbSupplierCode.SelectedValue +
                                                                      "'," + txtGrnNo.Text +
                                                                      "," + txtDCNo.Text +
                                                                      ",'" + txtPo.Text +
                                                                      "','" + Convert.ToDateTime(dtGrn.Value).ToString("dd/MMM/yyyy") +
                                                                      "','" + Convert.ToDateTime(dtDC.Value).ToString("dd/MMM/yyyy") +
                                                                      "','" + Convert.ToDateTime(dtPO.Value).ToString("dd/MMM/yyyy") +
                                                                      "','" + strFinYear +
                                                                      "','" + CommonData.LogUserId +
                                                                      "',Getdate())";
                    iRetVal = objSQLDB.ExecuteSaveData(sqlQry);
                }
                else
                {
                    string sqlQry = "DELETE FROM STATIONARY_GRN_DETL WHERE  SGD_COMPANY_CODE='" + cbCompany.SelectedValue +
                                                                     "' AND SGD_BRANCH_CODE='" + lblBranchCode.Text +
                                                                     "' AND SGD_FIN_YEAR='" + strFinYear +
                                                                     "' AND SGD_GRN_NO=" + txtGrnNo.Text;
                    sqlQry += " UPDATE STATIONARY_GRN_HEAD SET SGH_SUPPLIER_CODE='" + cbSupplierCode.SelectedValue +
                                                               "',SGH_DC_OR_BILL_NO=" + txtDCNo.Text +
                                                               ",SGH_PO_NO='" + txtPo.Text +
                                                               "',SGH_GRN_DATE='" + Convert.ToDateTime(dtGrn.Value).ToString("dd/MMM/yyyy") +
                                                               "',SGH_DC_OR_BILL_DATE='" + Convert.ToDateTime(dtDC.Value).ToString("dd/MMM/yyyy") +
                                                               "',SGH_PO_DATE='" + Convert.ToDateTime(dtPO.Value).ToString("dd/MMM/yyyy") +                                                                                                                    
                                                               "',SGH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                                               "',SGH_LAST_MODIFIED_DATE=Getdate() " +
                                                               "  WHERE SGH_COMPANY_CODE='" + cbCompany.SelectedValue +
                                                               "' AND SGH_BRANCH_CODE='" + lblBranchCode.Text +
                                                               "' AND SGH_FIN_YEAR='" + strFinYear +
                                                               "' AND SGH_GRN_NO=" + txtGrnNo.Text;

                    iRetVal = objSQLDB.ExecuteSaveData(sqlQry);
                }
                string sqlQryd = "";
                if (iRetVal > 0)
                {
                    for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                    {
                        if ((gvIndentDetails.Rows[i].Cells["DcBull"].Value != null) && (gvIndentDetails.Rows[i].Cells["Accepted"].Value != null))
                        {                           
                            sqlQryd += " INSERT INTO STATIONARY_GRN_DETL(SGD_COMPANY_CODE "+
                                                                         ",SGD_BRANCH_CODE "+
                                                                         ",SGD_FIN_YEAR " +
                                                                         ",SGD_GRN_NO "+
                                                                         ",SGD_ITEM_ID "+
                                                                         ",SGD_DC_OR_BILL_QTY "+
                                                                         ",SGD_RECIEVED_QTY "+
                                                                         ",SGD_EXCESS_SHORTAGE "+
                                                                         ",SGD_NUMBERING_FROM " +
                                                                         ",SGD_NUMBERING_TO " +
                                                                         ",SGD_REMARKS) VALUES";
                                                         sqlQryd += "('" + cbCompany.SelectedValue + 
                                                                          "','" + lblBranchCode.Text +
                                                                          "','" + strFinYear + 
                                                                          "'," + txtGrnNo.Text +
                                                                          "," + gvIndentDetails.Rows[i].Cells["ItemID"].Value +
                                                                          "," + gvIndentDetails.Rows[i].Cells["DcBull"].Value +
                                                                          "," + gvIndentDetails.Rows[i].Cells["Accepted"].Value + 
                                                                          "," + gvIndentDetails.Rows[i].Cells["Shortage"].Value +
                                                                          "," + gvIndentDetails.Rows[i].Cells["FrmNo"].Value +
                                                                          "," + gvIndentDetails.Rows[i].Cells["ToNo"].Value + 
                                                                          ",'" + gvIndentDetails.Rows[i].Cells["Remarks"].Value + "')";
                        }
                    }
                    int iRetVald = objSQLDB.ExecuteSaveData(sqlQryd);
                    if (iRetVald > 0)
                    {
                        MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Data Not saved ", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool CheckData()
        {
            bool blValue = true;
            if (Convert.ToDateTime(CommonData.CurrentDate) > dtpGRNDate.AddDays(Convert.ToInt32(CommonData.LogUserBackDays)))
            {
                MessageBox.Show("You are not allowed to Enter/Update for backdays Data","Stationary :: GRN",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                blValue = false;
            }
            if (Convert.ToDateTime(CommonData.CurrentDate) > dtGrn.Value.AddDays(Convert.ToInt32(CommonData.LogUserBackDays)))
            {
                MessageBox.Show("You are not allowed to Enter/Update for backdays Data", "Stationary :: GRN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blValue = false;
            }
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Company");
                blValue = false;
                cbCompany.Focus();
                return blValue;
            }
            if (txtGrnNo.Text.Length == 0)
            {
                MessageBox.Show("Please Enter GRN Number");
                blValue = false;
                txtGrnNo.Focus();
                return blValue;
            }
            if (txtDCNo.Text.Length == 0)
            {
                MessageBox.Show("Please Enter DC Number");
                blValue = false;
                txtDCNo.Focus();
                return blValue;
            }
            if (txtPo.Text.Length == 0)
            {
                MessageBox.Show("Please Enter PO Number");
                blValue = false;
                txtPo.Focus();
                return blValue;
            }
            if (txtSupplyerName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Select Sepplier Code");
                blValue = false;
                txtSupplyerName.Focus();
                return blValue;
            }
            bool blInvDtl = true;
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (Convert.ToString(gvIndentDetails.Rows[i].Cells["DcBull"].Value) == "")
                {
                    blInvDtl = false;
                }
                if (Convert.ToString(gvIndentDetails.Rows[i].Cells["Accepted"].Value) == "")
                {
                    blInvDtl = false;
                }
            }
            if (blInvDtl == false)
            {
                blValue = false;
                MessageBox.Show("Enter Item details");
            return false;
        }
        return blValue;
    }
    private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        objSQLDB = new SQLDB();
        string SqlCommand = "";
        DataTable dt = new DataTable();
        if (cbCompany.SelectedIndex > 0)
        {
            if (txtGrnNo.Text != "")
            {
                
                GetStationaryGrnDetails();
            }
            else
            {

                txtGrnNo.Text = GenerateGrnNo();
            }
        }
        else
        {
            //cbCompany.SelectedIndex = 0;
            txtGrnNo.Text = "";
            //txtGrnNo.Text=  GenerateGrnNo();
            lblBranchCode.Text = "";
            txtDCNo.Text = "";
            txtPo.Text = "";
            //dtDC.Value = System.DateTime.Now;
            //dtGrn.Value = System.DateTime.Now;
            //dtPO.Value = System.DateTime.Now;
            cbSupplierCode.SelectedIndex =-1;
            txtSupplyerName.Text = "";
            gvIndentDetails.Rows.Clear();
           
          
        }
    }
        private string GenerateGrnNo()
        {
            getFinYear();
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();          
            string SqlCommand = "";
         
            string sGRNNo = string.Empty;
            if (cbCompany.SelectedIndex > 0)
            {
                try
                {
                    SqlCommand = "SELECT BRANCH_CODE FROM BRANCH_MAS WHERE BRANCH_TYPE='ST' AND COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dt = objSQLDB.ExecuteDataSet(SqlCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    lblBranchCode.Text = dt.Rows[0]["BRANCH_CODE"].ToString();
                }
                try
                {
                    string sqlText = "select isnull(max(SGH_GRN_NO),0)+1 from STATIONARY_GRN_HEAD WHERE SGH_COMPANY_CODE='" + cbCompany.SelectedValue +
                           // "' AND SGH_BRANCH_CODE='" + lblBranchCode.Text + 
                            "' AND SGH_FIN_YEAR='" + strFinYear + "'";
                    sGRNNo = objSQLDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }               
            }
            else
            {
                txtGrnNo.Text = "";
            }
            return sGRNNo;
        }
        private string getFinYear()
        {
            string str1 = "01/JAN/" + (dtGrn.Value.Year);
            string str2 = "31/MAR/" + (dtGrn.Value.Year);
            string str3 = "01/APR/" + (dtGrn.Value.Year);
            string str4 = "31/DEC/" + (dtGrn.Value.Year);
            strFinYear = "";
            string str11 = dtGrn.Value.ToString("dd/MMM/yyyy");
            if (Convert.ToDateTime(str11) >= Convert.ToDateTime(str1) && Convert.ToDateTime(str11) <= Convert.ToDateTime(str2))
            {
                strFinYear = (dtGrn.Value.Year - 1) + "-" + dtGrn.Value.Year;
            }

            else
                strFinYear = dtGrn.Value.Year + "-" + (dtGrn.Value.Year + 1);
            return strFinYear;
        }

        private void txtGrnNo_Validated(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                if (txtGrnNo.Text != "")
                {
                    GetStationaryGrnDetails();
                }
                else
                {
                    btnCancel_Click(null, null);
                }
            }
            else
            {
                MessageBox.Show("Please Select Company", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void GetStationaryGrnDetails()
        {
            dtpGRNDate = DateTime.Now;
               objSQLDB = new SQLDB();
              string SqlCommand = "";
              DataTable dt = new DataTable();
              if (cbCompany.SelectedIndex > 0)
              {

                  try
                  {
                      SqlCommand = "SELECT BRANCH_CODE FROM BRANCH_MAS WHERE BRANCH_TYPE='ST' AND COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show(ex.ToString());
                  }
                  dt = objSQLDB.ExecuteDataSet(SqlCommand).Tables[0];

                  if (dt.Rows.Count > 0)
                  {
                      lblBranchCode.Text = dt.Rows[0]["BRANCH_CODE"].ToString();
                  }
              }
            string SqlStr = "SELECT * FROM STATIONARY_GRN_HEAD WHERE SGH_COMPANY_CODE='" + cbCompany.SelectedValue +
                                                              "' AND SGH_BRANCH_CODE='" + lblBranchCode.Text +
                                                              "' AND SGH_FIN_YEAR='" + strFinYear +
                                                              "' AND  SGH_GRN_NO=" + txtGrnNo.Text;

            SqlStr += "SELECT A.*,B.SIM_ITEM_NAME From STATIONARY_GRN_DETL A  INNER JOIN STATIONARY_ITEMS_MASTER B ON A.SGD_ITEM_ID=B.SIM_ITEM_CODE " +
                                                             "  WHERE SGD_COMPANY_CODE='" + cbCompany.SelectedValue +
                                                             "' AND SGD_BRANCH_CODE='" + lblBranchCode.Text +
                                                             "' AND SGD_FIN_YEAR='" + strFinYear +
                                                             "' AND SGD_GRN_NO=" + txtGrnNo.Text;
            DataSet dsData = objSQLDB.ExecuteDataSet(SqlStr);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                dtGrn.Value = Convert.ToDateTime(dsData.Tables[0].Rows[0]["SGH_GRN_DATE"]);
                dtpGRNDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["SGH_GRN_DATE"]);
                dtDC.Value = Convert.ToDateTime(dsData.Tables[0].Rows[0]["SGH_DC_OR_BILL_DATE"]);
                dtPO.Value = Convert.ToDateTime(dsData.Tables[0].Rows[0]["SGH_PO_DATE"]);
                txtDCNo.Text = dsData.Tables[0].Rows[0]["SGH_DC_OR_BILL_NO"].ToString();
                txtPo.Text = dsData.Tables[0].Rows[0]["SGH_PO_NO"].ToString();
                //txtSupCode.Text = dsData.Tables[0].Rows[0]["SGH_SUPPLIER_CODE"].ToString();
                cbSupplierCode.SelectedValue = dsData.Tables[0].Rows[0]["SGH_SUPPLIER_CODE"].ToString();
                //txtSupCode_Validated(null, null);
                gvIndentDetails.Rows.Clear();
                if (dsData.Tables[1].Rows.Count > 0)
                    GetBindData(dsData.Tables[1]);
            }
            else
            {
                //btnCancel_Click(null, null);
                //txtGrnNo.Text = "";
                dtpGRNDate = DateTime.Now;
                txtGrnNo.Text=  GenerateGrnNo();
                lblBranchCode.Text = "";
                txtDCNo.Text = "";
                txtPo.Text = "";
                //dtDC.Value = System.DateTime.Now;
                //dtGrn.Value = System.DateTime.Now;
                //dtPO.Value = System.DateTime.Now;
                cbSupplierCode.SelectedIndex = 0;
                txtSupplyerName.Text = "";
                gvIndentDetails.Rows.Clear(); 
                objSQLDB = null;
            }
        }
            
           
            
        public void GetBindData(DataTable dt)
        {
            int intRow = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                cellItemID.Value = dt.Rows[i]["SGD_ITEM_ID"].ToString();
                tempRow.Cells.Add(cellItemID);

                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                cellItemName.Value = dt.Rows[i]["SIM_ITEM_NAME"].ToString();
                tempRow.Cells.Add(cellItemName);

                DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                cellAvailQty.Value = dt.Rows[i]["SGD_DC_OR_BILL_QTY"].ToString();
                tempRow.Cells.Add(cellAvailQty);

                DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                cellReqQty.Value = dt.Rows[i]["SGD_RECIEVED_QTY"].ToString();
                tempRow.Cells.Add(cellReqQty);

                DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                cellApprQty.Value = dt.Rows[i]["SGD_EXCESS_SHORTAGE"].ToString();
                tempRow.Cells.Add(cellApprQty);

                DataGridViewCell cellFrmNo = new DataGridViewTextBoxCell();
                cellFrmNo.Value = dt.Rows[i]["SGD_NUMBERING_FROM"].ToString();
                tempRow.Cells.Add(cellFrmNo);

                DataGridViewCell cellToNo = new DataGridViewTextBoxCell();
                cellToNo.Value = dt.Rows[i]["SGD_NUMBERING_TO"].ToString();
                tempRow.Cells.Add(cellToNo);

                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                cellAmount.Value = dt.Rows[i]["SGD_REMARKS"].ToString();
                tempRow.Cells.Add(cellAmount);
                intRow = intRow + 1;
                gvIndentDetails.Rows.Add(tempRow);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            txtGrnNo.Text = "";
           //txtGrnNo.Text=  GenerateGrnNo();
            lblBranchCode.Text = "";
            txtDCNo.Text = "";          
            txtPo.Text = "";
            dtDC.Value = System.DateTime.Now;
            dtGrn.Value = System.DateTime.Now;
            dtPO.Value = System.DateTime.Now;
            cbSupplierCode.SelectedIndex = 0;
            txtSupplyerName.Text = "";
            gvIndentDetails.Rows.Clear();
        }

        private void cbSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSupplierCodes();
        }
        private void FillSupplierCodes()
        {
            objSQLDB = new SQLDB();

            string SqlCommand = "";
            if (cbSupplierCode.SelectedIndex > 0)
            {
                DataTable dt = new DataTable();
                try
                {
                    SqlCommand = "SELECT SM_SUPPLIER_CODE,SM_SUPPLIER_NAME FROM SUPPLIER_MASTER WHERE SM_SUPPLIER_CODE='" + cbSupplierCode.Text + "'";
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dt = objSQLDB.ExecuteDataSet(SqlCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtSupplyerName.Text = dt.Rows[0]["SM_SUPPLIER_NAME"].ToString();
                }
                else
                {
                    txtSupplyerName.Text = "";
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            if (txtGrnNo.Text!="")
            {
                string strQryD = "DELETE FROM STATIONARY_GRN_HEAD WHERE SGH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                                           "' AND SGH_BRANCH_CODE='" + lblBranchCode.Text +
                                                                           "' AND SGH_FIN_YEAR='" + strFinYear +
                                                                           "' AND SGH_GRN_NO='" + txtGrnNo.Text.ToString() + "'";

                strQryD += "DELETE FROM STATIONARY_GRN_DETL WHERE  SGD_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                         "' AND SGD_BRANCH_CODE='" + lblBranchCode.Text +
                                                         "' AND SGD_FIN_YEAR='" + strFinYear +
                                                         "' AND SGD_GRN_NO='" + txtGrnNo.Text.ToString() + "'";
                int ivals = objSQLDB.ExecuteSaveData(strQryD);
                if (ivals > 0)
                {
                    MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                    gvIndentDetails.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Data Not deleted", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
         
        }

        private void dtGrn_ValueChanged(object sender, EventArgs e)
        {
             
            //if (cbCompany.SelectedIndex > 0)
            //{
                if (txtGrnNo.Text != "")
                {
                    getFinYear();
                    GetStationaryGrnDetails();
                }
                else
                {
                    txtGrnNo.Text = GenerateGrnNo(); 
                    //btnCancel_Click(null, null);
                   
                }
            //}
            //else
            //{
            //    MessageBox.Show("Please Select Company", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSCRM_REP_STATIONARY_GRN";
            ReportViewer oReportViewer = new ReportViewer();
            oReportViewer.Show();
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {

            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) && (flagText == false))
            {
                e.Handled = true;
                return;
            }

            //to allow decimals only teak plant
            if (intCurrentCell == 3 && e.KeyChar == 46)
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && flagText == false)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

      
        private void gvIndentDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //blIsCellQty = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            //if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 3)
            //{
            //    TextBox txtQty = e.Control as TextBox;
            //    if (txtQty != null)
            //    {
            //        flagText = false;
            //        txtQty.MaxLength = 6;
            //        txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
            //    }
            //}
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 3)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 7;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 7;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
         
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 8)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = true;
                    txtQty.MaxLength = 200;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
        }

      
    }
}
