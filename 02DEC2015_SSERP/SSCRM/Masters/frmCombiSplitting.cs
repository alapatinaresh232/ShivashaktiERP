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

namespace SSCRM
{
    public partial class frmCombiSplitting : Form
    {
        SQLDB objSQLdb = null;
        double TotAmount = 0;
        double TotPoints = 0;
        string strCombiId = "";
        double CombiTotPrice = 0;
        double CombiTotPoints = 0;
        string strFinYear = "";

        public frmCombiSplitting()
        {
            InitializeComponent();
        }

        private void frmCombiSplitting_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            dtpDocMonth.Value = DateTime.Today;

            gvCombiProdDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);

            gvSingleProdDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                " FROM USER_BRANCH " +
                                " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                "' ORDER BY CM_COMPANY_NAME";
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

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbUserBranch.DataSource = null;

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' AND BRANCH_TYPE='BR' "+
                                        " ORDER BY BRANCH_NAME ";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbUserBranch.DataSource = dt;
                    cbUserBranch.DisplayMember = "BRANCH_NAME";
                    cbUserBranch.ValueMember = "BRANCH_CODE";
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();
        }

        private void FillCombiProductsToGrid()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            gvCombiProdDetails.Rows.Clear();
            gvSingleProdDetails.Rows.Clear();

            if (cbCompany.SelectedIndex > 0)
            {
                try
                {
                    strCommand = "SELECT DISTINCT SID_PRODUCT_ID CombiId " +
                                ",PM_PRODUCT_NAME CombiProdName " +
                                //",SID_AMOUNT Amount " +
                                //",SID_QTY ProdQty " +
                                ",SID_PRICE ProdPrice " +
                                ",SID_PRODUCT_POINTS/SID_QTY CombiPoints " +
                                ",SID_FIN_YEAR finYear " +
                                " FROM SALES_INV_HEAD " +
                                " INNER JOIN SALES_INV_DETL ON SID_COMPANY_CODE=SIH_COMPANY_CODE " +
                                " AND SID_BRANCH_CODE=SIH_BRANCH_CODE  " +
                                " AND SID_INVOICE_NUMBER=SIH_INVOICE_NUMBER AND SID_PRODUCT_ID LIKE '%'+ 'COMB' +'%' " +
                                " INNER JOIN PRODUCT_MAS ON PM_PRODUCT_ID=SID_PRODUCT_ID " +
                                " WHERE SIH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                "' AND SIH_BRANCH_CODE='" + cbUserBranch.SelectedValue.ToString() +
                                "' AND SIH_DOCUMENT_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                "' and SID_QTY>0 ORDER BY PM_PRODUCT_NAME";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvCombiProdDetails.Rows.Add();
                            gvCombiProdDetails.Rows[i].Cells["CombiSlNo"].Value = (i + 1).ToString();
                            gvCombiProdDetails.Rows[i].Cells["CombiId"].Value = dt.Rows[i]["CombiId"].ToString();
                            gvCombiProdDetails.Rows[i].Cells["FinYear"].Value = dt.Rows[i]["finYear"].ToString();
                            gvCombiProdDetails.Rows[i].Cells["CombiName"].Value = dt.Rows[i]["CombiProdName"].ToString();
                            gvCombiProdDetails.Rows[i].Cells["CombiPrice"].Value = Convert.ToDouble(dt.Rows[i]["ProdPrice"]).ToString("0.00");
                            gvCombiProdDetails.Rows[i].Cells["CombiPoints"].Value = Convert.ToDouble(dt.Rows[i]["CombiPoints"]).ToString("0.000");


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

        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbCompany.Focus();
            }
            else if (cbUserBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbUserBranch.Focus();
            }
            else if (gvSingleProdDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Combi Product", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            string strDelete = "";

            if (CheckData() == true)
            {

                try
                {
                    CalculateTotAmount();
                    CalculateTotPoints();
                    if (gvSingleProdDetails.Rows.Count > 0)
                    {

                        for (int i = 0; i < gvSingleProdDetails.Rows.Count; i++)
                        {
                            if (strCombiId.Equals(gvSingleProdDetails.Rows[i].Cells["SCombiProdId"].Value.ToString()))
                            {

                                if (TotAmount.Equals(CombiTotPrice))
                                {
                                    if (TotPoints.Equals(CombiTotPoints))
                                    {
                                        strDelete = "DELETE FROM COMBI_SPLITTING WHERE CS_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                                    "' AND CS_BRANCH_CODE='" + cbUserBranch.SelectedValue.ToString() +
                                                    "' AND CS_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                    "' AND CS_FIN_YEAR='" + strFinYear + "' AND CS_COMBI_ID='" + strCombiId +
                                                    "' AND CS_COMBI_PRICE='" + CombiTotPrice +
                                                    "' AND CS_COMBI_POINTS='" + CombiTotPoints + "'";

                                        iRes = objSQLdb.ExecuteSaveData(strDelete);


                                        strCommand += "INSERT INTO COMBI_SPLITTING(CS_COMP_CODE " +
                                                                               ", CS_BRANCH_CODE " +
                                                                               ", CS_DOC_MONTH " +
                                                                               ", CS_FIN_YEAR " +
                                                                               ", CS_COMBI_ID " +
                                                                               ", CS_COMBI_PRICE " +
                                                                               ", CS_COMBI_POINTS " +
                                                                               ", CS_SL_NO " +
                                                                               ", CS_SINGLE_PRODUCT_ID " +
                                                                               ", CS_SINGLE_PRODUCT_AMT " +
                                                                               ", CS_SINGLE_PRODUCT_POINTS " +
                                                                               ", CS_SINGLE_PRODUCT_TOTAL_POINTS " +
                                                                               ", CS_SINGLE_PRODUCT_PRICE " +
                                                                               ", CS_SINGLE_PRODUCT_QTY " +
                                                                               ", CS_CREATED_BY " +
                                                                               ", CS_CREATED_DATE " +
                                                                               ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                               "','" + cbUserBranch.SelectedValue.ToString() +
                                                                               "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                               "','" + strFinYear +
                                                                               "','" + gvSingleProdDetails.Rows[i].Cells["SCombiProdId"].Value.ToString() +
                                                                               "','" + CombiTotPrice + "','" + CombiTotPoints +
                                                                               "'," + Convert.ToInt32(gvSingleProdDetails.Rows[i].Cells["SlNo"].Value) +
                                                                               ",'" + gvSingleProdDetails.Rows[i].Cells["SingleProdId"].Value.ToString() +
                                                                               "','" + gvSingleProdDetails.Rows[i].Cells["SingleProdMrp"].Value.ToString() +
                                                                               "','" + gvSingleProdDetails.Rows[i].Cells["SinProdPoints"].Value.ToString() +
                                                                               "','" + gvSingleProdDetails.Rows[i].Cells["SinProdTotPoints"].Value.ToString() +
                                                                               "','" + gvSingleProdDetails.Rows[i].Cells["ProdTotMRP"].Value.ToString() +
                                                                               "','" + gvSingleProdDetails.Rows[i].Cells["prodQty"].Value.ToString() +
                                                                               "','" + CommonData.LogUserId +
                                                                               "',getdate())";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Combi Points Must equals to the sum of Individuals", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Combi Price Must equals to the sum of Individuals", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                        }
                    }

                    if (strCommand.Length > 10)
                    {
                        iRes = objSQLdb.ExecuteSaveData(strCommand);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    try
                    {
                        strCommand = "exec Process_Combi_Splitting '" + cbCompany.SelectedValue.ToString() +
                                        "','" + cbUserBranch.SelectedValue.ToString() +
                                        "','','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                        "',0,'','','BRANCH_MONTH_WISE'";
                        objSQLdb.ExecuteSaveData(strCommand);
                    }
                    catch
                    {

                    }
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gvSingleProdDetails.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpDocMonth_ValueChanged(object sender, EventArgs e)
        {
            FillCombiProductsToGrid();
        }

        private DataSet GetSingleProductDetails(string CompCode, string BranchCode, string DocMonth, string sCombiId, double nPrice,double nPoints)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xCombiId", DbType.String, sCombiId, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xPrice", DbType.Double, nPrice, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xPoints", DbType.Double, nPoints, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("Get_SingleProductsByCombiId_For_CombiSplitting", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }


        private void FillSingleProductsToGrid(string sCombiId, double CmbPrice,double CmbPoints)
        {
            DataTable dt = new DataTable();
            gvSingleProdDetails.Rows.Clear();          
            try
            {
                dt = GetSingleProductDetails(cbCompany.SelectedValue.ToString(), cbUserBranch.SelectedValue.ToString(), Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(), sCombiId,CmbPrice,CmbPoints).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvSingleProdDetails.Rows.Add();
                        gvSingleProdDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                        gvSingleProdDetails.Rows[i].Cells["SCombiProdId"].Value = dt.Rows[i]["CombiProdId"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["SingleProdId"].Value = dt.Rows[i]["SingleProdId"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["SingleProdName"].Value = dt.Rows[i]["SingleProdName"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["Category"].Value = dt.Rows[i]["CategoryName"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["prodQty"].Value = dt.Rows[i]["Qty"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["SingleProdMrp"].Value = dt.Rows[i]["ProdMrp"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["SinProdPoints"].Value = dt.Rows[i]["ProdPoints"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["ProdTotMRP"].Value = dt.Rows[i]["SingleProdTotalAmt"].ToString();
                        gvSingleProdDetails.Rows[i].Cells["SinProdTotPoints"].Value = dt.Rows[i]["SingleProdTotalPts"].ToString();



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void gvCombiProdDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            strFinYear = "";
            strCombiId = "";
            CombiTotPrice = 0;
            CombiTotPoints = 0;

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvCombiProdDetails.Columns["Edit"].Index)
                {
                    strCombiId = gvCombiProdDetails.Rows[e.RowIndex].Cells["CombiId"].Value.ToString();
                    strFinYear = gvCombiProdDetails.Rows[e.RowIndex].Cells["FinYear"].Value.ToString();
                    CombiTotPrice = Convert.ToDouble(gvCombiProdDetails.Rows[e.RowIndex].Cells["CombiPrice"].Value.ToString());
                    CombiTotPoints = Convert.ToDouble(gvCombiProdDetails.Rows[e.RowIndex].Cells["CombiPoints"].Value.ToString());

                    FillSingleProductsToGrid(strCombiId,CombiTotPrice,CombiTotPoints);

                }
            }
        }

        private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUserBranch.SelectedIndex > 0)
            {
                FillCombiProductsToGrid();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbUserBranch.SelectedIndex = -1;
            dtpDocMonth.Value = DateTime.Today;
            gvCombiProdDetails.Rows.Clear();
            gvSingleProdDetails.Rows.Clear();

        }

        private void CalculateTotAmount()
        {
            TotAmount = 0;
            if (gvSingleProdDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvSingleProdDetails.Rows.Count; i++)
                {
                    TotAmount += Convert.ToDouble(gvSingleProdDetails.Rows[i].Cells["ProdTotMRP"].Value.ToString());
                }
            }
        }

        private void CalculateTotPoints()
        {
            TotPoints = 0;

            if (gvSingleProdDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvSingleProdDetails.Rows.Count; i++)
                {
                    TotPoints += Convert.ToDouble(gvSingleProdDetails.Rows[i].Cells["SinProdTotPoints"].Value.ToString());
                }
            }

        }



        private void gvSingleProdDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double ProdTotPoints = 0;
            double ProdTotAmount = 0;
            if (Convert.ToString(gvSingleProdDetails.Rows[e.RowIndex].Cells["SingleProdMrp"].Value) != "")
            {
                ProdTotAmount = Convert.ToDouble(gvSingleProdDetails.Rows[e.RowIndex].Cells["prodQty"].Value) * Convert.ToDouble(gvSingleProdDetails.Rows[e.RowIndex].Cells["SingleProdMrp"].Value);
                gvSingleProdDetails.Rows[e.RowIndex].Cells["ProdTotMRP"].Value = ProdTotAmount.ToString("0.0");

            }
            else
            {
                gvSingleProdDetails.Rows[e.RowIndex].Cells["SingleProdMrp"].Value = 0;
            }

            if (Convert.ToString(gvSingleProdDetails.Rows[e.RowIndex].Cells["SinProdPoints"].Value) != "")
            {
                ProdTotPoints = Convert.ToDouble(gvSingleProdDetails.Rows[e.RowIndex].Cells["prodQty"].Value) * Convert.ToDouble(gvSingleProdDetails.Rows[e.RowIndex].Cells["SinProdPoints"].Value);
                gvSingleProdDetails.Rows[e.RowIndex].Cells["SinProdTotPoints"].Value = ProdTotPoints.ToString("0.0");

            }
            else
            {
                gvSingleProdDetails.Rows[e.RowIndex].Cells["SinProdPoints"].Value = 0;
            }

        }


        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
        }

        private void gvSingleProdDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }




    }
}
