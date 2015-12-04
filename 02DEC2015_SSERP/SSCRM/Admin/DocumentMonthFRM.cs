using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class DocumentMonthFRM : Form
    {
        private SQLDB objData = null;
         public DocumentMonthFRM()
        {
            InitializeComponent();
        }

        private void DocumentMonthFRM_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 120, Screen.PrimaryScreen.WorkingArea.Y + 130);
            //this.StartPosition = FormStartPosition.CenterScreen;
            lblPhysicalBranch.Text = CommonData.BranchCode + ":" + CommonData.BranchName;
            txtPreDocMonth.Text = Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper();
            txtPreFromDate.Text = Convert.ToDateTime(CommonData.DocFDate).ToString("dd/MMM/yyyy");
            txtPreToDate.Text = Convert.ToDateTime(CommonData.DocTDate).ToString("dd/MMM/yyyy");
            dtpMonth.Value = Convert.ToDateTime(Convert.ToDateTime(txtPreDocMonth.Text).AddMonths(1).ToString("MMMyyyy").ToUpper());
            dtpFromDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocTDate).ToString("dd/MMM/yyyy")).AddDays(1);
            dtpTodate.Value = Convert.ToDateTime(dtpFromDate.Value.AddMonths(1).AddDays(-1).ToString("dd/MMM/yyyy"));
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            chkCopyEcodes.Checked = true;
           
        }
        
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            int recCnt = 0;
            try
            {
                if (CheckData())
                {
                    recCnt = CreateDocMonth();
                    if (recCnt > 0)
                    {
                        CommonData.DocMonth = dtpMonth.Value.ToString("MMMyyyy").ToUpper();
                        CommonData.DocFDate = dtpFromDate.Value.ToString("dd/MM/yyyy");
                        CommonData.DocFDate = dtpTodate.Value.ToString("dd/MM/yyyy");
                        MDIParent mdi = new MDIParent();
                        mdi.statusStrip.Items[0].Text = "USER: " + CommonData.LogUserId;
                        mdi.statusStrip.Items[1].Text = mdi.statusStrip.Items[1].Text + " : " + CommonData.DocMonth.ToString();
                        mdi.statusStrip.Items["dmFromDate"].Text = " From: " + CommonData.DocFDate.ToString();
                        mdi.statusStrip.Items["dmToDate"].Text = " To: " + CommonData.DocTDate.ToString();
                        mdi.Refresh();
                        mdi = null;
                    }
                    else
                    {
                        strSQl = " UPDATE document_month set status = 'R'" +
                                         " WHERE company_code = '" + CommonData.CompanyCode +
                                         "' AND fin_year = '" + CommonData.FinancialYear +
                                         "' AND UPPER(document_month) = '" + CommonData.DocMonth + "'";
                        recCnt = objData.ExecuteSaveData(strSQl);

                        strSQl = " DELETE FROM document_month WHERE company_code='" + CommonData.CompanyCode +
                                    "' AND  fin_year='" + CommonData.FinancialYear +
                                    "' AND UPPER(document_month)='" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() + "' ";
                        recCnt = objData.ExecuteSaveData(strSQl);

                        //strSQl = " DELETE FROM LevelGroup_map WHERE lgm_company_code ='" + CommonData.CompanyCode +
                        //            "' AND UPPER(lgm_branch_code) ='" + CommonData.BranchCode +
                        //            "' AND UPPER(lgm_doc_month) ='" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() + "' ";
                        //recCnt = objData.ExecuteSaveData(strSQl);

                        //strSQl = " DELETE FROM LevelsSource_Dest_map  " +
                        //          " WHERE UPPER(lsdm_company_code) = '" + CommonData.CompanyCode +
                        //          "' AND UPPER(lsdm_branch_code) = '" + CommonData.BranchCode +
                        //          "' AND UPPER(lsdm_doc_month) = '" + CommonData.DocMonth + "'";
                        //recCnt = objData.ExecuteSaveData(strSQl);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            objData = null;
           
        }
        private int CreateDocMonth()
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            int recCnt = 0;

            try
            {

                //strSQl = " UPDATE document_month set status = 'C'" +
                //             " WHERE company_code = '" + CommonData.CompanyCode +
                //             "' AND fin_year = '" + CommonData.FinancialYear +
                //             "' AND document_month = '" + CommonData.DocMonth + "'";

                //recCnt = objData.ExecuteSaveData(strSQl);
                recCnt = 0;
                strSQl = " INSERT INTO document_month(company_code, fin_year, document_month, start_date, end_date, status)" +
                              " VALUES('" + CommonData.CompanyCode +
                              "', '" + CommonData.FinancialYear +
                              "', '" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() +
                              "', '" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy") +
                              "', '" + Convert.ToDateTime(dtpTodate.Value).ToString("dd/MMM/yyyy") +
                              "','R')";

                recCnt = objData.ExecuteSaveData(strSQl);

                if (chkCopyEcodes.Checked == true)
                {
                    recCnt = 0;
                    strSQl = " INSERT INTO LevelGroup_map " +
                               " SELECT  lgm_company_code " +
                                 ", lgm_branch_code " +
                                 ", lgm_log_branch_code " +
                                 ", lgm_state_code " +
                                 ", '" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() + "'" +
                                 ", lgm_group_ecode" +
                                 ", lgm_source_ecode " +
                                 ", lgm_group_name " +
                                 ", '" + CommonData.LogUserId + "'" +
                                 ", getdate() " +
                                 " FROM LevelGroup_map " +
                                 " WHERE UPPER(lgm_company_code) ='" + CommonData.CompanyCode +
                                 //"' AND UPPER(lgm_branch_code) = '" + CommonData.BranchCode +
                                 "' AND UPPER(lgm_doc_month) = '" + CommonData.DocMonth + "'";
                    recCnt = objData.ExecuteSaveData(strSQl);
                    recCnt = 0;
                    strSQl = " INSERT INTO LevelsSource_Dest_map " +
                               " SELECT  lsdm_company_code " +
                                 ", lsdm_branch_code " +
                                 ", lsdm_log_branch_code " +
                                 ", lsdm_state_code " +
                                 ", '" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() + "'" +
                                 ", lsdm_source_elevel_id" +
                                 ", lsdm_source_ecode " +
                                 ", lsdm_dest_elevel_id " +
                                 ", lsdm_dest_ecode " +
                                 ", lsdm_secode_isgroup " +
                                 ", '" + CommonData.LogUserId + "'" +
                                 ", getdate() " +
                                 " FROM LevelsSource_Dest_map " +
                                 " WHERE UPPER(lsdm_company_code) ='" + CommonData.CompanyCode +
                                 //"' AND UPPER(lsdm_branch_code) = '" + CommonData.BranchCode +
                                 "' AND UPPER(lsdm_doc_month) = '" + CommonData.DocMonth + "'";

                    recCnt = objData.ExecuteSaveData(strSQl);

                }

                if (recCnt > 0)
                {
                    MessageBox.Show("Document month successfully created!", "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objData = null;
            }
            return recCnt;
        }
        private bool CheckData()
        {
            bool blValue = true;
            int result = -1;
            DateTime preDocMonth = new DateTime(Convert.ToDateTime(Convert.ToDateTime(txtPreDocMonth.Text).ToString("MM/yyyy")).Year, Convert.ToDateTime(Convert.ToDateTime(txtPreDocMonth.Text).ToString("MM/yyyy")).Month, 1, 0, 0, 0);
            DateTime nextDocMonth = new DateTime(dtpMonth.Value.Year, dtpMonth.Value.Month, 1, 0, 0, 0);
           
            DateTime dateDocFDate = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateDocTDate = new DateTime(dtpTodate.Value.Year, dtpTodate.Value.Month, dtpTodate.Value.Day, 0, 0, 0);

            System.TimeSpan diff1 = nextDocMonth.Subtract(preDocMonth);
            if (diff1.Days > 31)
            {
                MessageBox.Show("Invalid Document Month!", "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpFromDate.Focus();
                return blValue;

            }
            
            result = DateTime.Compare(dateDocTDate, dateDocFDate);
            if (result !=1 )
            {
                MessageBox.Show("Invalid Next Document Month Deatils!", "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpFromDate.Focus();
                return blValue;

            }

            DateTime dateCurrentDate = new DateTime(Convert.ToDateTime(CommonData.CurrentDate).Year, Convert.ToDateTime(CommonData.CurrentDate).Month, Convert.ToDateTime(CommonData.CurrentDate).Day, 0, 0, 0);
            result = DateTime.Compare(dateCurrentDate, dateDocFDate);

            if (result != 1)
            {
                MessageBox.Show("Invalid Next Document Month Deatils!", "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpFromDate.Focus();
                return blValue;

            }
            // \n Previous month will be close \n Copy Hierarchal ECODE data to Next month. 
            DialogResult Stage1 = MessageBox.Show("Do you want to create new " + dtpMonth.Text.ToString() + " Document Month?",
                       "Document Month", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            blValue = false;
            if (Stage1 == DialogResult.Yes)
            {
                DialogResult Stage2 = MessageBox.Show("Are you sure continew to create new " + dtpMonth.Text.ToString() + " Document Month?",
                   "Document Month", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Stage2 == DialogResult.Yes)
                {
                    blValue = true;
                }
            }

            return blValue;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            int recCnt = 0;
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete " + dtpMonth.Text.ToString()+ " Document Month ?",
                                       "Document Month", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    strSQl = " UPDATE document_month set status = 'R'" +
                                        " WHERE company_code = '" + CommonData.CompanyCode +
                                        "' AND fin_year = '" + CommonData.FinancialYear +
                                        "' AND UPPER(document_month) = '" + CommonData.DocMonth + "'";
                    recCnt = objData.ExecuteSaveData(strSQl);

                    strSQl = " DELETE FROM document_month WHERE company_code='" + CommonData.CompanyCode +
                                "' AND  fin_year='" + CommonData.FinancialYear +
                                "' AND UPPER(document_month)='" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() + "' ";

                    recCnt = objData.ExecuteSaveData(strSQl);

                    //strSQl = " DELETE FROM LevelGroup_map WHERE lgm_company_code ='" + CommonData.CompanyCode +
                    //            "' AND UPPER(lgm_branch_code) ='" + CommonData.BranchCode +
                    //            "' AND UPPER(lgm_doc_month) ='" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() + "' ";
                    //recCnt = objData.ExecuteSaveData(strSQl);

                    //strSQl = " DELETE FROM LevelsSource_Dest_map " +
                    //          " WHERE UPPER(lsdm_company_code) = '" + CommonData.CompanyCode +
                    //          "' AND UPPER(lsdm_branch_code) = '" + CommonData.BranchCode +
                    //          "' AND UPPER(lsdm_doc_month) = '" + dtpMonth.Value.ToString("MMMyyyy").ToUpper() + "'";
                    //recCnt = objData.ExecuteSaveData(strSQl);

                    if (recCnt > 0)
                    {
                        MessageBox.Show("Document month deleted!", "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Document month data!", "Document Month", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
        }

        private void DocumentMonthFRM_Activated(object sender, EventArgs e)
        {
            DateTime dateDocToDate = new DateTime(Convert.ToDateTime(CommonData.DocTDate).Year, Convert.ToDateTime(CommonData.DocTDate).Month, Convert.ToDateTime(CommonData.DocTDate).Day, 0, 0, 0);
            DateTime dateCurrentDate = new DateTime(Convert.ToDateTime(CommonData.CurrentDate).Year, Convert.ToDateTime(CommonData.CurrentDate).Month, Convert.ToDateTime(CommonData.CurrentDate).Day, 0, 0, 0);
            int result = DateTime.Compare(dateCurrentDate, dateDocToDate);

            if (result >= 0)
            {
                btnSave.Enabled = true;
                btnDelete.Enabled = true;

            }
        }

    }
}
