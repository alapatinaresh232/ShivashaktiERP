using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class TripSheetIssue : Form
    {
        SQLDB objSQLdb = null;
        bool flagUpdate = false;
        public TripSheetIssue()
        {
            InitializeComponent();
        }

        private void TransportDocuments_Load(object sender, EventArgs e)
        {
            GenerateTrnNo();
            FillCategory();
            FillSubCategory();
            FillCompanyData();
            txtFrom.Text = "0";
            txtTo.Text = "0";
        }
        private void FillCategory()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("--SELECT--", "--SELECT--");
            table.Rows.Add("TRIP SHEET", "TRIP SHEET");
            cbCategory.DataSource = table;
            cbCategory.DisplayMember = "type";
            cbCategory.ValueMember = "name";
        }
        private void FillSubCategory()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("--SELECT--", "--SELECT--");
            table.Rows.Add("TRIP SHEET C", "C");
            table.Rows.Add("TRIP SHEET H", "H");
            table.Rows.Add("TRIP SHEET D", "D");
            table.Rows.Add("TRIP SHEET R", "R");
            table.Rows.Add("TRIP SHEET M", "M");
            cbSubcategory.DataSource = table;
            cbSubcategory.DisplayMember = "type";
            cbSubcategory.ValueMember= "name";
        }
        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS where active='T' ";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
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
        private void GenerateTrnNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT ISNULL(MAX(DIH_TRN_NO),0)+1 TrnNo FROM DOC_ISSUE_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTransactionNo.Text = dt.Rows[0]["TrnNo"] + "";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocationData();
            if(cbBranch.SelectedIndex>0)
            {
              AddDataToGrid();
            }
        }
        private void FillLocationData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE+'@'+STATE_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' and active='T' ORDER BY BRANCH_NAME ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "branchCode";
                    //cbLocation.ValueMember = "LOCATION";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GenerateTrnNo();
            //cbCompany.SelectedIndex = 0;
            cbSubcategory.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            txtFrom.Text = "0";
            txtTo.Text = "0";
        }
        private bool CheckData()
        {
            bool flag = true;
            if(txtTransactionNo.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Enter Transaction number!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTransactionNo.Focus();
                return flag;
            }
            if(cbCategory.SelectedIndex==0)
            {
                flag = false;
                MessageBox.Show("Select Document Type Category", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbCategory.Focus();
                return flag;
            }
            if (cbSubcategory.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Select SubCategory", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbSubcategory.Focus();
                return flag;
            }
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Select Company", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbCompany.Focus();
                return flag;
            }
            if (cbBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Select Branch", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbBranch.Focus();
                return flag;
            }
            if (Convert.ToInt32(txtFrom.Text) <= 0)
            {
                flag = false;
                MessageBox.Show("Enter From No", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtFrom.Focus();
                return flag;
            }
            if (Convert.ToInt32(txtTo.Text) <= 0)
            {
                flag = false;
                MessageBox.Show("Enter To No", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTo.Focus();
                return flag;
            }
            if (Convert.ToInt32(txtFrom.Text) > Convert.ToInt32(txtTo.Text))
            {
                flag = false;
                MessageBox.Show("Enter Valid To No", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTo.Focus();
                return flag;
            }
            string strCmd = "SELECT * FROM DOC_ISSUE_DETL WHERE  DID_DOC_PRE_FIX='"+cbSubcategory.SelectedValue.ToString()+"' AND DID_DOC_NO BETWEEN " + txtFrom.Text + " AND " + txtTo.Text;
            objSQLdb = new SQLDB();
            DataTable dt  = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                flag = false;
                MessageBox.Show("Already Exists", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return flag;
            }
            return flag;

        }
        private void AddDataToGrid()
        {
            if (cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                gvProductDetails.Rows.Clear();
                DataTable dt = new DataTable();
                dt = GetTripIssuedData(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0], cbBranch.SelectedValue.ToString().Split('@')[1]);
                try
                {
                    if (dt.Rows.Count > 0)
                    {


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                            cellSNo.Value = (i + 1).ToString();
                            tempRow.Cells.Add(cellSNo);



                            DataGridViewCell cellTripSheetType = new DataGridViewTextBoxCell();
                            cellTripSheetType.Value = dt.Rows[i]["SubCategory"];
                            tempRow.Cells.Add(cellTripSheetType);

                            DataGridViewCell cellIssueDate = new DataGridViewTextBoxCell();
                            cellIssueDate.Value = Convert.ToDateTime(dt.Rows[i]["IssueDate"]).ToShortDateString();
                            tempRow.Cells.Add(cellIssueDate);

                            DataGridViewCell cellDocFrom = new DataGridViewTextBoxCell();
                            cellDocFrom.Value = dt.Rows[i]["DocFrom"];
                            tempRow.Cells.Add(cellDocFrom);

                            DataGridViewCell cellDocTo = new DataGridViewTextBoxCell();
                            cellDocTo.Value = dt.Rows[i]["DocTo"];
                            tempRow.Cells.Add(cellDocTo);

                            DataGridViewCell cellTotalIssued = new DataGridViewTextBoxCell();
                            cellTotalIssued.Value = dt.Rows[i]["TotalIssued"];
                            tempRow.Cells.Add(cellTotalIssued);

                            DataGridViewCell cellUsed = new DataGridViewTextBoxCell();
                            cellUsed.Value = dt.Rows[i]["Used"];
                            tempRow.Cells.Add(cellUsed);

                            DataGridViewCell cellCancelled = new DataGridViewTextBoxCell();
                            cellCancelled.Value = dt.Rows[i]["Cancelled"];
                            tempRow.Cells.Add(cellCancelled);



                            gvProductDetails.Rows.Add(tempRow);
                        }
                    }
                    else
                    {
                        gvProductDetails.Rows.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dt = null;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                int intSave=0;
                if (SaveHeadData() > 0)
                {
                    intSave = SaveDetlData();

                    if (intSave > 0)
                    {
                        flagUpdate = false;
                        MessageBox.Show("Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        GenerateTrnNo();
                        AddDataToGrid();
                    }
                    else
                    {
                        try
                        {
                            string strCmd = " DELETE DOC_ISSUE_HEAD WHERE DIH_TRN_NO=" + Convert.ToInt32(txtTransactionNo.Text) +
                                            " AND DIH_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                            "' AND DIH_BRANCH_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                            "' AND DIH_STATE_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[1] + "'";
                            objSQLdb = new SQLDB();
                            objSQLdb.ExecuteSaveData(strCmd);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    
                }
            }
        }
        private int SaveHeadData()
        {
            int iRes = 0;
            objSQLdb = new SQLDB();
            string strCmd = "";
            string[] strBranch = cbBranch.SelectedValue.ToString().Split('@');
            int total = Convert.ToInt32(txtTo.Text) - Convert.ToInt32(txtFrom.Text);
            try
            {
                if (flagUpdate == false)
                {
                    GenerateTrnNo();

                    strCmd = " INSERT INTO DOC_ISSUE_HEAD(" +
                                "DIH_TRN_NO" +
                                ",DIH_COMP_CODE" +
                                ",DIH_STATE_CODE" +
                                ",DIH_BRANCH_CODE" +
                                ",DIH_DOC_CAT" +
                                ",DIH_DOC_SUB_CAT" +
                                ",DIH_DOC_PRE_FIX" +
                                ",DIH_DOC_ISSUE_FROM" +
                                ",DIH_DOC_ISSUE_TO" +
                                ",DIH_DOC_ISSUE_DATE" +
                                ",DIH_ISSUED_TOT" +
                                ",DIH_CREATED_BY" +
                                ",DIH_CREATED_DATE) " +
                                "VALUES(" + Convert.ToInt32(txtTransactionNo.Text) +
                                ",'" + cbCompany.SelectedValue.ToString() +
                                "','" + strBranch[1] +
                                "','" + strBranch[0] +
                                "','" + cbCategory.SelectedValue +
                                "','" + cbSubcategory.GetItemText(cbSubcategory.SelectedItem) +
                                "','"+ cbSubcategory.SelectedValue+
                                "'," + Convert.ToInt32(txtFrom.Text) +
                                "," + Convert.ToInt32(txtTo.Text) +
                                ",'" + dtpIssueDate.Value.ToString("dd/MMM/yyyy") +
                                "'," + total +
                                ",'" + CommonData.LogUserId +
                                "',getdate())";
                }
                else
                {
                    strCmd = " DELETE DOC_ISSUE_DETL WHERE DID_TRN_NO=" + Convert.ToInt32(txtTransactionNo.Text) +
                                   " AND DID_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                   "' AND DID_BRANCH_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                   "' AND DID_STATE_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[1] + "'";
                    objSQLdb = new SQLDB();
                    objSQLdb.ExecuteSaveData(strCmd);

                    strCmd = "";
                    strCmd = " UPDATE DOC_ISSUE_HEAD SET DIH_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                             "',DIH_STATE_CODE='" + strBranch[1] +
                             "',DIH_BRANCH_CODE='" + strBranch[0] +
                             "',DIH_DOC_CAT='" + cbCategory.SelectedValue.ToString() +
                             "',DIH_DOC_SUB_CAT='" + cbSubcategory.SelectedItem.ToString() +
                             "',DIH_DOC_PRE_FIX='" + cbSubcategory.SelectedValue+
                             "',DIH_DOC_ISSUE_FROM=" + Convert.ToInt32(txtFrom.Text) +
                             ",DIH_DOC_ISSUE_TO=" + Convert.ToInt32(txtTo.Text) +
                             ",DIH_DOC_ISSUE_DATE='" + dtpIssueDate.Value.ToString("dd/MMM/yyyy") +
                             "',DIH_MODIFIED_BY='" + CommonData.LogUserId +
                             "',DIH_MODIFIED_DATE='getdate()' WHERE DIH_TRN_NO=" + Convert.ToInt32(txtTransactionNo.Text);
                }
                objSQLdb = new SQLDB();
                iRes = objSQLdb.ExecuteSaveData(strCmd);
                //if (iRes > 0)
                //{
                //    flagUpdate = false;
                //    MessageBox.Show("Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    btnCancel_Click(null, null);
                //    GenerateTrnNo();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return iRes;
        }
        private int SaveDetlData()
        {
            int iRes = 0;
            try
            {
                string[] strBranch = cbBranch.SelectedValue.ToString().Split('@');
                int iFrom = Convert.ToInt32(txtFrom.Text),iTo=Convert.ToInt32(txtTo.Text) ;
                StringBuilder strCmd =new StringBuilder();
                int siNo = 1;
                for (int i = iFrom; i <= iTo; i++)
                {
                    strCmd.Append( " INSERT INTO DOC_ISSUE_DETL(" +
                                "DID_TRN_NO" +
                                ",DID_SL_NO" +
                                ",DID_COMP_CODE" +
                                ",DID_STATE_CODE" +
                                ",DID_BRANCH_CODE" +
                                ",DID_DOC_CAT" +
                                ",DID_DOC_SUB_CAT" +
                                ",DID_DOC_PRE_FIX" +
                                ",DID_DOC_NO" +
                                ",DID_ORG_DOC_NO" +
                                ",DID_DOC_STATUS" +
                                ",DID_CREATED_BY" +
                                ",DID_CREATED_DATE) " +
                                "VALUES(" + Convert.ToInt32(txtTransactionNo.Text) +
                                "," + (siNo++) + 
                                ",'" + cbCompany.SelectedValue.ToString() +
                                "','" + strBranch[1] +
                                "','" + strBranch[0] +
                                "','" + cbCategory.SelectedValue.ToString() +
                                "','" + cbSubcategory.GetItemText( cbSubcategory.SelectedItem) +
                                "','" +cbSubcategory.SelectedValue.ToString()+
                                "', "  +i+
                                ",'" +(cbSubcategory.SelectedValue.ToString()+""+i)+
                                "','' " +
                                ",'" + CommonData.LogUserId +
                                "',getdate())");
                }
                objSQLdb = new SQLDB();
                iRes = objSQLdb.ExecuteSaveData(strCmd.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (flagUpdate == true)
            //    {
            //        string strCmd = " DELETE DOC_ISSUE_HEAD WHERE DIH_TRN_NO=" + Convert.ToInt32(txtTransactionNo.Text) +
            //                        " AND DIH_COMP_CODE='"+cbCompany.SelectedValue.ToString()+
            //                        "' AND DIH_BRANCH_CODE='"+cbBranch.SelectedValue.ToString().Split('@')[0]+
            //                        "' AND DIH_STATE_CODE='"+cbBranch.SelectedValue.ToString().Split('@')[1]+"'";

            //        strCmd += " DELETE DOC_ISSUE_DETL WHERE DID_TRN_NO="+Convert.ToInt32(txtTransactionNo.Text) +
            //                        " AND DID_COMP_CODE='"+cbCompany.SelectedValue.ToString()+
            //                        "' AND DID_BRANCH_CODE='"+cbBranch.SelectedValue.ToString().Split('@')[0]+
            //                        "' AND DID_STATE_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[1] + "'";
            //        objSQLdb = new SQLDB();
            //        DialogResult result = MessageBox.Show("Do you want to Delete " + txtTransactionNo.Text + " Transaction ?",
            //                                   "CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (result == DialogResult.Yes)
            //        {
            //            int iRes = objSQLdb.ExecuteSaveData(strCmd);
            //            if (iRes > 0)
            //            {
            //                flagUpdate = false;
            //                MessageBox.Show("Deleted Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                btnCancel_Click(null, null);
            //                GenerateTrnNo();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
          
        }
        private DataTable GetTripIssuedData(string sCompCode, string sBranchCode, string sStateCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetTripSheetIssueData", CommandType.StoredProcedure, param);

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
            return ds.Tables[0];
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranch.SelectedIndex > 0)
            {
                AddDataToGrid();
            }
        }

        private void txtFrom_Validated(object sender, EventArgs e)
        {
            if (txtFrom.Text == "")
            {
                txtFrom.Text = "0";
            }
        }

        private void txtTo_Validated(object sender, EventArgs e)
        {
            if (txtTo.Text == "")
            {
                txtTo.Text = "0";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0)
            {
                string strComp = cbCompany.SelectedValue.ToString();
                string strBranch = cbBranch.SelectedValue.ToString().Split('@')[0];
                CommonData.ViewReport = "SSCRM_REP_TRIPSHEET_DOCISSU";
                ReportViewer objReportview = new ReportViewer(strComp,strBranch);
                objReportview.Show();
            }
        }
    }
}
