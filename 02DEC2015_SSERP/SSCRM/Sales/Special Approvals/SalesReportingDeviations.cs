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
using SSAdmin;

namespace SSCRM
{
    public partial class SalesReportingDeviations : Form
    {
        SQLDB objSQLdb = null;
        InvoiceDB objData = null;
        bool IsUpdate = false;
        public SalesReportingDeviations()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void SalesReportingDeviations_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            objSQLdb = new SQLDB();
            try
            {
                DataTable dtDev = objSQLdb.ExecuteDataSet("SELECT SRDM_DEVIATION_TYPE FROM SALES_REPORTING_DEV_MAS WHERE SRDM_COMP_CODE = '" + CommonData.CompanyCode + "'").Tables[0];

                cbDevType.DataSource = null;
                if (dtDev.Rows.Count > 0)
                {
                    cbDevType.DataSource = dtDev;
                    cbDevType.DisplayMember = "SRDM_DEVIATION_TYPE";
                    cbDevType.ValueMember = "SRDM_DEVIATION_TYPE";

                    cbDevType.SelectedIndex = 0;
                    //cbDevType.SelectedValue = CommonData.DocMonth;

                }
            }
            catch { }

            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
            {
                btnDelete.Enabled = true;
            }
            else
                btnDelete.Enabled = false;

            try
            {
                DataTable dtfinYear = objSQLdb.ExecuteDataSet("SELECT DISTINCT FY_FIN_YEAR FROM FIN_YEAR").Tables[0];
                cbFinYear.DataSource = null;
                if (dtfinYear.Rows.Count > 0)
                {
                    cbFinYear.DataSource = dtfinYear;
                    cbFinYear.DisplayMember = "FY_FIN_YEAR";
                    cbFinYear.ValueMember = "FY_FIN_YEAR";

                    cbFinYear.SelectedIndex = 0;
                    cbFinYear.SelectedValue = CommonData.FinancialYear;

                }
            }
            catch { }

            try
            {
                if (cbFinYear.SelectedIndex > -1)
                {
                    DataTable dtDocMonth = objSQLdb.ExecuteDataSet("SELECT DISTINCT document_month FROM document_month WHERE fin_year = '" + cbFinYear.SelectedValue.ToString() + "'").Tables[0];
                    cbDocMonth.DataSource = null;
                    if (dtDocMonth.Rows.Count > 0)
                    {
                        cbDocMonth.DataSource = dtDocMonth;
                        cbDocMonth.DisplayMember = "document_month";
                        cbDocMonth.ValueMember = "document_month";

                        cbDocMonth.SelectedIndex = 0;
                        cbDocMonth.SelectedValue = CommonData.DocMonth;

                    }
                }
            }
            catch { }



        }

        private void cbFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFinYear.SelectedIndex > -1)
            {
                objSQLdb = new SQLDB();
                DataTable dtDocMonth = objSQLdb.ExecuteDataSet("SELECT DISTINCT document_month, dbo.fysortno(document_month) SortNo FROM document_month WHERE fin_year = '" + cbFinYear.SelectedValue.ToString() +
                                        "' order by dbo.fysortno(document_month) desc").Tables[0];
                cbDocMonth.DataSource = null;
                if (dtDocMonth.Rows.Count > 0)
                {
                    cbDocMonth.DataSource = dtDocMonth;
                    cbDocMonth.DisplayMember = "document_month";
                    cbDocMonth.ValueMember = "document_month";

                    cbDocMonth.SelectedIndex = 0;                    

                }
            }
            else
                cbDocMonth.DataSource = null;
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            txtEmpName.Text = "";
            txtDesig.Text = "";
            txtGroups.Text = "";
            txtRemarks.Text = "";
        }

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            FillEmployeeDetails();
        }

        private void FillEmployeeDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            objData = new InvoiceDB();
            if (txtEcodeSearch.Text.Length > 0)
            {
                try
                {
                    dt = objData.Get_SalesReportingDevByEcode(Convert.ToInt32(txtEcodeSearch.Text), cbDocMonth.SelectedValue.ToString()).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objData = null;
                    objSQLdb = null;
                }

                if (dt.Rows.Count > 0)
                {
                    txtEmpName.Text = dt.Rows[0]["EName"].ToString();
                    txtDesig.Text = dt.Rows[0]["Desig"].ToString();
                    txtGroups.Text = dt.Rows[0]["Groups"].ToString();
                    txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                    cbDevType.SelectedValue = dt.Rows[0]["DevType"].ToString();
                    if (dt.Rows[0]["DevType"].ToString() != "")
                        IsUpdate = true;
                    else
                        IsUpdate = false;
                }
                else
                    IsUpdate = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objSQLdb = new SQLDB();
                int iRec = 0;
                try
                {
                    string sqlText = " INSERT INTO SALES_REPORTING_DEVIATIONS" +
                                        "(SRD_COMPANY_CODE" +
                                        ", SRD_BRANCH_CODE" +
                                        ", SRD_FIN_YEAR" +
                                        ", SRD_DOC_MONTH" +
                                        ", SRD_EORA_CODE" +
                                        ", SRD_DIVIATION_TYPE" +
                                        ", SRD_REMARKS" +
                                        ", SRD_CREATED_BY" +
                                        ", SRD_CREATED_DATE) " +
                                        "SELECT '" + CommonData.CompanyCode +
                                        "','" + CommonData.BranchCode +
                                        "','" + cbFinYear.SelectedValue.ToString() +
                                        "','" + cbDocMonth.SelectedValue.ToString() +
                                        "'," + txtEcodeSearch.Text +
                                        ",'" + cbDevType.SelectedValue.ToString() +
                                        "','" + txtRemarks.Text +
                                        "','" + CommonData.LogUserId + "',GETDATE()" +
                                        " WHERE NOT EXISTS(SELECT * FROM" +
                                        " SALES_REPORTING_DEVIATIONS WHERE" +
                                        " SRD_EORA_CODE=" + txtEcodeSearch.Text +
                                        " AND SRD_DOC_MONTH='" + cbDocMonth.SelectedValue.ToString() + "')";

                    sqlText += " UPDATE SALES_REPORTING_DEVIATIONS SET SRD_DIVIATION_TYPE='" + cbDevType.SelectedValue.ToString() +
                                        "', SRD_REMARKS='" + txtRemarks.Text + "', SRD_MODIFIED_BY='" + CommonData.LogUserId +
                                        "', SRD_MODIFIED_DATE=GETDATE() WHERE SRD_EORA_CODE=" + txtEcodeSearch.Text +
                                        " AND SRD_DOC_MONTH='" + cbDocMonth.SelectedValue.ToString() + "'";

                    iRec = objSQLdb.ExecuteSaveData(sqlText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                }

                if (iRec > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            FillGridData();
        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (txtEmpName.Text == "")
            {
                bFlag = false;
                MessageBox.Show("Invalid Data", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bFlag;
            }
            return bFlag;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtEmpName.Text = "";
            txtGroups.Text = "";
            txtRemarks.Text = "";
            txtDesig.Text = "";
            IsUpdate = false;
            FillGridData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objSQLdb = new SQLDB();
                int iRec = 0;
                try
                {
                    string sqlText = "DELETE FROM SALES_REPORTING_DEVIATIONS WHERE SRD_EORA_CODE=" + txtEcodeSearch.Text +
                                        " AND SRD_DOC_MONTH = '" + cbDocMonth.SelectedValue.ToString() + "'";
                    iRec = objSQLdb.ExecuteSaveData(sqlText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                }

                if (iRec > 0)
                {
                    MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void cbDocMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmployeeDetails();
            FillGridData();
        }

        private void FillGridData()
        {
            objSQLdb = new SQLDB();
            string sqlText = "";
            gvProductDetails.Rows.Clear();
            DataTable dt = new DataTable();
            sqlText = " SELECT SRD_COMPANY_CODE, SRD_BRANCH_CODE, SRD_FIN_YEAR " +
                        ", SRD_DOC_MONTH, SRD_EORA_CODE, MEMBER_NAME, ldm_designations" +
                        ", SRD_DIVIATION_TYPE, SRD_REMARKS " +
                        "FROM SALES_REPORTING_DEVIATIONS INNER JOIN EORA_MASTER ON ECODE = SRD_EORA_CODE" +
                        " INNER JOIN LevelsDesig_mas ON ldm_company_code = company_code and LDM_DESIG_ID = DESG_ID" +
                        " WHERE SRD_BRANCH_CODE='" + CommonData.BranchCode + 
                        "' AND SRD_DOC_MONTH='" + CommonData.DocMonth + "'";

            try { dt = objSQLdb.ExecuteDataSet(sqlText).Tables[0]; }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { objSQLdb = null; }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = i + 1;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellDocMonth = new DataGridViewTextBoxCell();
                    cellDocMonth.Value = dt.Rows[i]["SRD_DOC_MONTH"];
                    tempRow.Cells.Add(cellDocMonth);

                    DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                    cellEcode.Value = dt.Rows[i]["SRD_EORA_CODE"];
                    tempRow.Cells.Add(cellEcode);

                    DataGridViewCell cellEName = new DataGridViewTextBoxCell();
                    cellEName.Value = dt.Rows[i]["MEMBER_NAME"];
                    tempRow.Cells.Add(cellEName);

                    DataGridViewCell cellGroups = new DataGridViewTextBoxCell();
                    cellGroups.Value = "";
                    tempRow.Cells.Add(cellGroups);

                    DataGridViewCell cellDev = new DataGridViewTextBoxCell();
                    cellDev.Value = dt.Rows[i]["SRD_DIVIATION_TYPE"];
                    tempRow.Cells.Add(cellDev);

                    DataGridViewCell cellRem = new DataGridViewTextBoxCell();
                    cellRem.Value = dt.Rows[i]["SRD_REMARKS"];
                    tempRow.Cells.Add(cellRem);

                    gvProductDetails.Rows.Add(tempRow);
                }
            }
        }
    }
}
