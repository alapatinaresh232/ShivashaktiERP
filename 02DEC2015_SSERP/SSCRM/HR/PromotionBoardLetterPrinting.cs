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

namespace SSCRM
{
    public partial class PromotionBoardLetterPrinting : Form
    {

        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;

        public PromotionBoardLetterPrinting()
        {
            InitializeComponent();
        }

        private void PromotionBoardLetterPrinting_Load(object sender, EventArgs e)
        {
            cbFilterType.SelectedIndex = 0;
            FillCompanyData();
            dtpFrmDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;

            gvEmpPromotionDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                  System.Drawing.FontStyle.Regular);

        }


        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS " +
                                " WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME ASC";
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
            cbBranches.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT BRANCH_NAME ,BRANCH_CODE ,BRANCH_TYPE " +
                                        " FROM BRANCH_MAS where COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND BRANCH_TYPE IN('BR') AND ACTIVE='T' Order by BRANCH_NAME ASC";
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

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME EName FROM EORA_MASTER WHERE ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EName"].ToString();
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

        private void cbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterType.SelectedIndex == 1)
            {
                txtEcodeSearch.ReadOnly = true;

                cbCompany.Enabled = true;
                cbBranches.Enabled = true;
                dtpFrmDate.Enabled = true;
                dtpToDate.Enabled = true;
                txtEcodeSearch.Text = "";

                gvEmpPromotionDetails.Rows.Clear();
                
            }
            else if (cbFilterType.SelectedIndex == 2)
            {
                cbCompany.Enabled = false;
                cbBranches.Enabled = false;
                dtpFrmDate.Enabled = false;
                dtpToDate.Enabled = false;

                cbCompany.SelectedIndex = 0;
                cbBranches.SelectedIndex = -1;
                dtpFrmDate.Value = DateTime.Today;
                dtpToDate.Value = DateTime.Today;
                txtEcodeSearch.ReadOnly = false;
                txtEcodeSearch.Focus();
                gvEmpPromotionDetails.Rows.Clear();
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbFilterType.SelectedIndex > 0)
            {
                if (cbFilterType.SelectedIndex == 1)
                {
                    if (cbCompany.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbCompany.Focus();
                        return flag;
                    }
                    else if (cbBranches.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbBranches.Focus();
                    }
                    else if (dtpFrmDate.Value > dtpToDate.Value)
                    {
                        flag = false;
                        MessageBox.Show("Please Select Valid Dates", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }
                }
                else
                {
                    if (txtEName.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEName.Focus();

                    }

                }
            }
            else
            {
                flag = false;
                MessageBox.Show("Please Select Filter By","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbFilterType.Focus();
                return flag;
            }

            return flag;
        }


        private void btnDisplayPromotionDetails_Click(object sender, EventArgs e)
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            gvEmpPromotionDetails.Rows.Clear();
            if (CheckData() == true)
            {
                try
                {
                    if (cbFilterType.SelectedIndex == 1)
                    {
                        string CompCode = cbCompany.SelectedValue.ToString();
                        string BranCode = cbBranches.SelectedValue.ToString();
                        DateTime FromDate = dtpFrmDate.Value;
                        DateTime ToDate = dtpToDate.Value;

                        dt = objHRdb.GetPromotionDetlByDate(CompCode, BranCode, FromDate, ToDate).Tables[0];
                    }
                    else
                    {
                        dt = objHRdb.GetPromotionDetlByEcode(Convert.ToInt32(txtEcodeSearch.Text)).Tables[0];
                    }

                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvEmpPromotionDetails.Rows.Add();

                            gvEmpPromotionDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvEmpPromotionDetails.Rows[i].Cells["PromotionCode"].Value = dt.Rows[i]["PromotionCode"].ToString();
                            gvEmpPromotionDetails.Rows[i].Cells["ApplNo"].Value = dt.Rows[i]["ApplNo"].ToString();
                            gvEmpPromotionDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["Ecode"].ToString();
                            gvEmpPromotionDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["EmpName"].ToString();
                            gvEmpPromotionDetails.Rows[i].Cells["DeptName"].Value = dt.Rows[i]["DeptName"].ToString();
                            gvEmpPromotionDetails.Rows[i].Cells["LetterType"].Value = dt.Rows[i]["PromotionName"].ToString();
                            gvEmpPromotionDetails.Rows[i].Cells["LetterRefNo"].Value = dt.Rows[i]["LetterRefNo"].ToString();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvEmpPromotionDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            int iRes = 0;
            try
            {
                string strCmd = "SELECT HLPH_LETTER_REF_NO,HLPH_APPL_NO FROM "+
                                "HR_PB_LETTER_PRINT_HIST "+
                                " WHERE HLPH_APPL_NO="+ Convert.ToInt32(gvEmpPromotionDetails.Rows[e.RowIndex].Cells["ApplNo"].Value) +
                                " AND HLPH_LETTER_REF_NO='"+ gvEmpPromotionDetails.Rows[e.RowIndex].Cells["LetterRefNo"].Value.ToString()+
                                "' AND HLPH_PB_TYPE='"+gvEmpPromotionDetails.Rows[e.RowIndex].Cells["PromotionCode"].Value.ToString() +"' ";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //DialogResult dlgResult = MessageBox.Show("Do you want Print This Letter?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if(dlgResult==DialogResult.Yes)
            //{
                if (e.ColumnIndex == gvEmpPromotionDetails.Columns["Print"].Index)
                {
                    if (gvEmpPromotionDetails.Rows[e.RowIndex].Cells["LetterType"].Value.ToString() == "APPOINTMENT"
                        && gvEmpPromotionDetails.Rows[e.RowIndex].Cells["DeptName"].Value.ToString() == "SALES")
                    {
                        if (dt.Rows.Count == 0)
                        {
                            try
                            {
                                CommonData.ViewReport = "PRINT_SALES_APPT_LETTER";
                                ReportViewer childReportViewer = new ReportViewer(Convert.ToInt32(gvEmpPromotionDetails.Rows[e.RowIndex].Cells["ApplNo"].Value).ToString(), gvEmpPromotionDetails.Rows[e.RowIndex].Cells["LetterRefNo"].Value.ToString());
                                childReportViewer.Show();

                                string strInsert = "INSERT INTO HR_PB_LETTER_PRINT_HIST(HLPH_APPL_NO " +
                                                                                     ", HLPH_PB_TYPE " +
                                                                                     ", HLPH_LETTER_REF_NO " +
                                                                                     ", HLPH_PRINT_DATE " +
                                                                                     ")VALUES" +
                                                                                     "(" + Convert.ToInt32(gvEmpPromotionDetails.Rows[e.RowIndex].Cells["ApplNo"].Value) +
                                                                                     ",'" + gvEmpPromotionDetails.Rows[e.RowIndex].Cells["PromotionCode"].Value.ToString() +
                                                                                     "','" + gvEmpPromotionDetails.Rows[e.RowIndex].Cells["LetterRefNo"].Value.ToString() +
                                                                                     "',getdate())";

                                if (strInsert.Length > 10)
                                {
                                    iRes = objSQLdb.ExecuteSaveData(strInsert);
                                }


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            //if (iRes > 0)
                            //{
                            //    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}

                        }
                        else
                        {
                            MessageBox.Show("Letter Already Printed", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (gvEmpPromotionDetails.Rows[e.RowIndex].Cells["LetterType"].Value.ToString() == "TRANSFER")
                    {
                        if (dt.Rows.Count == 0)
                        {
                            ReportViewer cldReportViewer = new ReportViewer("", "", gvEmpPromotionDetails.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvEmpPromotionDetails.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvEmpPromotionDetails.Rows[e.RowIndex].Cells["LetterRefNo"].Value.ToString(), "TRN");
                            CommonData.ViewReport = "PROMOTION_BOARD_TRN_LETTER";
                            cldReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("Letter Already Printed", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            //}
        }

      
        
    }
}
