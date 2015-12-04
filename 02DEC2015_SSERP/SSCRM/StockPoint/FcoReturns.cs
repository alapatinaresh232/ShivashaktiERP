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
    public partial class FcoReturns : Form
    {
        DataTable dtCategory;
        SQLDB objSQLDB = new SQLDB();
        private bool isUpdate = false;
        public FcoReturns()
        {
            InitializeComponent();
        }
        private void FcoReturns_Load(object sender, EventArgs e)
        {
            gvFcoReturns.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);

            GetBindData();
            dtCategory = new DataTable();
            dtCategory.Columns.Add("SlNo");
            dtCategory.Columns.Add("CATEGORY_ID");
            dtCategory.Columns.Add("CATEGORY_NAME");
            dtCategory.Columns.Add("VALIDFROM");
            dtCategory.Columns.Add("VALIDTO");
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT CATEGORY_ID,CATEGORY_NAME FROM CATEGORY_MASTER");
            objSQLDB = null;
            UtilityLibrary.PopulateControl(cmbCategory, ds.Tables[0].DefaultView, 1, 0, "--Please Select--", 0);
            dtFrom.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtTo.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtDocMonth.Text = CommonData.DocMonth;
        }
        public void GetBindData()
        {
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT *FROM BRANCH_MAS WHERE BRANCH_TYPE='SP' AND ACTIVE='T'");
            UtilityLibrary.PopulateControl(cmbBranch, ds.Tables[0].DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSQLDB = null;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataView dv = dtCategory.DefaultView;
            if (dv.Table.Rows.Count > 0)
            {
                DataTable dt;
                dv.RowFilter = "CATEGORY_ID=" + cmbCategory.SelectedValue;
                dt = dv.ToTable();
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["VALIDTO"]).AddDays(1) > Convert.ToDateTime(dtTo.Value))
                    {
                        MessageBox.Show("Please Select the Valid Dates", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    dtFrom.Value = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["VALIDTO"]).AddDays(1);
                }
            }
            gvFcoReturns.Rows.Clear();
            dtCategory.Rows.Add(new Object[] { dtCategory.Rows.Count + 1, cmbCategory.SelectedValue, cmbCategory.Text, dtFrom.Value, dtTo.Value });
            GetFcoProducts();
        }
        public void GetFcoProducts()
        {
            int intRow = 1;
            gvFcoReturns.Rows.Clear();
            for (int i = 0; i < dtCategory.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellExamPass = new DataGridViewTextBoxCell();
                cellExamPass.Value = dtCategory.Rows[i]["CATEGORY_ID"];
                tempRow.Cells.Add(cellExamPass);

                DataGridViewCell cellYearPass = new DataGridViewTextBoxCell();
                cellYearPass.Value = dtCategory.Rows[i]["CATEGORY_NAME"];
                tempRow.Cells.Add(cellYearPass);

                DataGridViewCell cellInstName = new DataGridViewTextBoxCell();
                cellInstName.Value = Convert.ToDateTime(dtCategory.Rows[i]["VALIDFROM"]).ToString("dd/MMM/yyyy");
                tempRow.Cells.Add(cellInstName);

                DataGridViewCell cellInstLocation = new DataGridViewTextBoxCell();
                cellInstLocation.Value = Convert.ToDateTime(dtCategory.Rows[i]["VALIDTO"]).ToString("dd/MMM/yyyy");
                tempRow.Cells.Add(cellInstLocation);

                intRow = intRow + 1;
                gvFcoReturns.Rows.Add(tempRow);
            }
        }
        private void gvFcoReturns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvFcoReturns.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvFcoReturns.Rows[e.RowIndex].Cells[gvFcoReturns.Columns["SlNo"].Index].Value);
                        DataRow[] dr = dtCategory.Select("SlNo=" + SlNo);
                        dtCategory.Rows.Remove(dr[0]);
                        GetFcoProducts();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                if (cmbBranch.SelectedIndex > 0)
                {
                    objSQLDB = new SQLDB();
                    DataSet ds = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_HEAD_MAS WHERE SPHM_CODE='" + cmbBranch.SelectedValue + "'");
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        MessageBox.Show("Please enter Stock point details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    objSQLDB = null;
                    GetMaxId();
                    btnClear_Click(null, null);
                }
            }
        }
        public void GetMaxId()
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet("SELECT ISNULL(MAX(SPHT_TRN_NUMBER),0)+1 FROM SP_HEAD_TRN WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='FCO RETURNS'");
                objSQLDB = null;
                txtTrnNo.Text = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDesig.Text = "";
            txtDesigRe.Text = "";
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtCity.Text = "";
            txtDept.Text = "";
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtName.Text = "";
            txtRemarks.Text = "";
            txtState.Text = "";
            gvFcoReturns.Rows.Clear();
            GetMaxId();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlQry = "";
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_HEAD_MAS WHERE SPHM_CODE='" + cmbBranch.SelectedValue + "'");
            if (ds.Tables[0].Rows[0][0].ToString() == "0")
            {
                MessageBox.Show("Please enter Stock point details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Check() == true)
            {
                DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_HEAD_TRN WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='FCO RETURNS' AND SPHT_TRN_NUMBER=" + txtTrnNo.Text);
                if (dsExist.Tables[0].Rows[0][0].ToString() == "0")
                {
                    GetMaxId();
                    sqlQry = "INSERT INTO SP_HEAD_TRN (SPHT_CODE,SPHT_TRN_DATE,SPHT_TRN_TYPE,SPHT_TRN_NUMBER,SPHT_SUBMITTED_BY_ECODE,SPHT_SUBMITTED_TO_NAME," +
                                "SPHT_SUBMITTED_TO_DESG,SPHT_SUBMITTED_TO_DEPT,SPHT_SUBMITTED_TO_ADDR1,SPHT_SUBMITTED_TO_ADDR2,SPHT_SUBMITTED_TO_CITY,SPHT_SUBMITTED_TO_STATE," +
                                "SPHT_REMARKS,SPHT_CREATED_BY,SPHT_AUTHORIZED_BY,SPHT_CREATED_DATE) VALUES " +
                                "('" + cmbBranch.SelectedValue + "','" + Convert.ToDateTime(dtTrnDate.Value).ToString("dd/MMM/yyyy") + "','FCO RETURNS'," + txtTrnNo.Text +
                                "," + txtEcodeSearch.Text + ",'" + txtName.Text.ToUpper() + "','" + txtDesigRe.Text.ToUpper() + "','" + txtDept.Text.ToUpper() + "','" + txtAddress.Text.ToUpper() + "','" + txtAddress1.Text.ToUpper() +
                                "','" + txtCity.Text.ToUpper() + "','" + txtState.Text.ToUpper() + "','" + txtRemarks.Text.ToUpper() + "','" + CommonData.LogUserId + "','" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "')";
                }
                else
                {
                    sqlQry = "UPDATE SP_HEAD_TRN SET SPHT_TRN_DATE='" + Convert.ToDateTime(dtTrnDate.Value).ToString("dd/MMM/yyyy") + "',SPHT_SUBMITTED_BY_ECODE=" + txtEcodeSearch.Text +
                            ",SPHT_SUBMITTED_TO_NAME='" + txtName.Text.ToUpper() + "',SPHT_SUBMITTED_TO_DESG='" + txtDesigRe.Text.ToUpper() + "',SPHT_SUBMITTED_TO_DEPT='" + txtDept.Text.ToUpper() +
                            "',SPHT_SUBMITTED_TO_ADDR1='" + txtAddress.Text.ToUpper() + "',SPHT_SUBMITTED_TO_ADDR2='" + txtAddress1.Text.ToUpper() + "',SPHT_SUBMITTED_TO_CITY='" + txtCity.Text.ToUpper() +
                            "',SPHT_SUBMITTED_TO_STATE='" + txtState.Text.ToUpper() + "',SPHT_REMARKS='" + txtRemarks.Text.ToUpper() + "',SPHT_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                            "',SPHT_LAST_MODIFIED_DATE='" + CommonData.CurrentDate + "' WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='FCO RETURNS' AND SPHT_TRN_NUMBER=" + txtTrnNo.Text;
                    sqlQry += " DELETE FROM SP_FCO_VALID_DETAILS WHERE SPFC_CODE='" + cmbBranch.SelectedValue + "' AND SPFC_TRN_NUMBER=" + txtTrnNo.Text;
                }
                for (int i = 0; i < gvFcoReturns.Rows.Count; i++)
                {
                    sqlQry += " INSERT INTO SP_FCO_VALID_DETAILS(SPFC_CODE,SPFC_TRN_DATE,SPFC_TRN_NUMBER,SPFC_SUBMITTED_BY_ECODE,SPFC_CATEGORY_ID,SPFC_VALID_FROM,SPFC_VALID_TO)" +
                        "VALUES ('" + cmbBranch.SelectedValue + "','" + Convert.ToDateTime(dtTrnDate.Value).ToString("dd/MMM/yyyy") + "'," + txtTrnNo.Text + "," + txtEcodeSearch.Text +
                        ",'" + gvFcoReturns.Rows[i].Cells[1].Value + "','" + Convert.ToDateTime(gvFcoReturns.Rows[i].Cells[3].Value).ToString("dd/MMM/yyyy") + "','" + Convert.ToDateTime(gvFcoReturns.Rows[i].Cells[4].Value).ToString("dd/MMM/yyyy") + "')";
                }
                objSQLDB = new SQLDB();
                int iretval = objSQLDB.ExecuteSaveData(sqlQry);
                objSQLDB = null;
                if (iretval > 0)
                    MessageBox.Show("Inserted Data Successfully", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data not Inserted", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
            }
        }
        public bool Check()
        {
            bool ibool = true;
            if (txtEcodeSearch.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Ecode", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Inspecter Name", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (isUpdate == true)
            {
                if (txtDesig.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Designation", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (txtDept.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Department", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if ((txtAddress.Text.Trim() == "") && (txtAddress1.Text.Trim() == "") && (txtCity.Text.Trim() == "") && (txtState.Text.Trim() == ""))
                {
                    MessageBox.Show("Please enter Address", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //if (txtRemarks.Text.Trim() == "")
                //{
                //    MessageBox.Show("Please enter Remarks", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
            }
            if (gvFcoReturns.RowCount == 0)
            {
                MessageBox.Show("Please enter Submitted Products", "FCO Returns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return ibool;
        }
        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text != "")
            {
                objSQLDB = new SQLDB();
                string sqlQry = "SELECT * FROM SP_HEAD_TRN WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='FCO RETURNS' AND SPHT_TRN_NUMBER=" + txtTrnNo.Text;
                sqlQry += "SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 0)) SLNO,SPFC_CATEGORY_ID as CATEGORY_ID,CATEGORY_NAME,SPFC_VALID_FROM as VALIDFROM,SPFC_VALID_TO as VALIDTO FROM SP_FCO_VALID_DETAILS A INNER JOIN CATEGORY_MASTER B ON A.SPFC_CATEGORY_ID=B.CATEGORY_ID WHERE SPFC_CODE='" + cmbBranch.SelectedValue + "' AND SPFC_TRN_NUMBER=" + txtTrnNo.Text;
                DataSet dsExist = objSQLDB.ExecuteDataSet(sqlQry);
                objSQLDB = null;
                if (dsExist.Tables[0].Rows.Count > 0)
                {
                    isUpdate = true;
                    dtTrnDate.Value = Convert.ToDateTime(dsExist.Tables[0].Rows[0]["SPHT_TRN_DATE"]);
                    txtEcodeSearch.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_BY_ECODE"].ToString();
                    txtEcodeSearch_KeyUp(null, null);
                    txtName.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_NAME"].ToString();
                    txtDesigRe.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_DESG"].ToString();
                    txtDept.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_DEPT"].ToString();
                    txtAddress.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_ADDR1"].ToString();
                    txtAddress1.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_ADDR2"].ToString();
                    txtCity.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_CITY"].ToString();
                    txtState.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_STATE"].ToString();
                    txtRemarks.Text = dsExist.Tables[0].Rows[0]["SPHT_REMARKS"].ToString();
                    gvFcoReturns.Rows.Clear();
                    dtCategory = dsExist.Tables[1];
                    GetFcoProducts();
                    
                }
                else
                {
                    isUpdate = false;
                    gvFcoReturns.Rows.Clear();
                    txtName.Text = "";
                    txtDesigRe.Text = "";
                    txtEcodeSearch.Text = "";
                    txtEName.Text = "";
                    txtDesig.Text = "";
                    txtDept.Text = "";
                    txtAddress.Text = "";
                    txtAddress1.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtRemarks.Text = "";
                }
            }
        }
        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet("SELECT * FROM EORA_MASTER WHERE ECODE=" + txtEcodeSearch.Text);
                objSQLDB = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEName.Text = ds.Tables[0].Rows[0]["MEMBER_NAME"].ToString();
                    txtDesig.Text = ds.Tables[0].Rows[0]["DESIG"].ToString();
                }
                else
                {
                    txtEName.Text = "";
                    txtDesig.Text = "";
                }
            }
        }
    }
}
