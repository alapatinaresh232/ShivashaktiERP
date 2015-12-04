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

namespace SSCRM
{
    public partial class PetrolAllowReport : Form
    {
        private InvoiceDB objInv = null;
        private Master objMaster = null;
        private SQLDB objDA = null;
        private ReportViewer childReportViewer = null;
        public PetrolAllowReport()
        {
            InitializeComponent();
        }

        private void PetrolAllowReport_Load(object sender, EventArgs e)
        {
            cbletterType.SelectedIndex = 0;
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                FillAllRefNo();
            }
            else
            {
                FillUserBranchRefNo();
            }
        }

        private void FillUserBranchRefNo()
        {
            objDA = new SQLDB();
            DataTable dt = new DataTable();
            string sqlText = "";
            clbApprovedDocs.Items.Clear();
            try
            {
                if (cbletterType.SelectedIndex == 0)
                {
                    sqlText = "SELECT CAST(HVLH_PETROL_APPR_REF_NO AS VARCHAR)+'--'+'('+CAST(HVLH_EORA_CODE AS VARCHAR)+'-'+MEMBER_NAME+')' REF_NO" +
                                ",HVLH_PETROL_APPR_REF_NO FROM HR_VEHICLE_LOAN_HEAD INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE " +
                                "WHERE HVLH_BRANCH_CODE IN (SELECT UB_BRANCH_CODE FROM USER_BRANCH WHERE UB_USER_ID = '" + CommonData.LogUserId +
                                "' and HVLH_PETROL_APPR_REF_NO is not null and HVLH_PETROL_REQUEST_FLAG='A') ORDER BY HVLH_PETROL_APPR_REF_NO DESC";
                }
                else if (cbletterType.SelectedIndex == 1)
                {
                    sqlText = "SELECT CAST(HVLH_INCN_APPR_REF_NO AS VARCHAR)+'--'+'('+CAST(HVLH_EORA_CODE AS VARCHAR)+'-'+MEMBER_NAME+')' REF_NO" +
                                ",HVLH_INCN_APPR_REF_NO FROM HR_VEHICLE_LOAN_HEAD INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE " +
                                "WHERE HVLH_BRANCH_CODE IN (SELECT UB_BRANCH_CODE FROM USER_BRANCH WHERE UB_USER_ID = '" + CommonData.LogUserId +
                                "' and HVLH_INCN_APPR_REF_NO is not null and HVLH_INCN_REQUEST_FLAG='A') ORDER BY HVLH_INCN_APPR_REF_NO DESC";
                }
                dt = objDA.ExecuteDataSet(sqlText).Tables[0];
                if(dt.Rows.Count>0)
                {
                    FillRefTolist(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDA = null;
            }
        }

        private void FillRefTolist(DataTable dt)
        {
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["REF_NO"] + "" != "")
                {
                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    if (cbletterType.SelectedIndex == 0)
                    {
                        oclBox.Tag = dataRow["HVLH_PETROL_APPR_REF_NO"].ToString();
                    }
                    else if (cbletterType.SelectedIndex == 1)
                    {
                        oclBox.Tag = dataRow["HVLH_INCN_APPR_REF_NO"].ToString();
                    }
                    oclBox.Text = dataRow["REF_NO"].ToString();
                    clbApprovedDocs.Items.Add(oclBox);
                    oclBox = null;
                }
            }
        }

        private void FillAllRefNo()
        {
            objDA = new SQLDB();
            DataTable dt = new DataTable();
            string sqlText = "";
            clbApprovedDocs.Items.Clear();
            try
            {
                if (cbletterType.SelectedIndex == 0)
                {
                    sqlText = "SELECT CAST(HVLH_PETROL_APPR_REF_NO AS VARCHAR)+'--('+CAST(HVLH_EORA_CODE AS VARCHAR)+'-'+MEMBER_NAME+')' REF_NO" +
                                ",HVLH_PETROL_APPR_REF_NO FROM HR_VEHICLE_LOAN_HEAD INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE " +
                                "WHERE HVLH_PETROL_APPR_REF_NO IS NOT NULL ORDER BY HVLH_PETROL_APPR_REF_NO DESC";
                }
                else if (cbletterType.SelectedIndex == 1)
                {
                    sqlText = "SELECT CAST(HVLH_INCN_APPR_REF_NO AS VARCHAR)+'--('+CAST(HVLH_EORA_CODE AS VARCHAR)+'-'+MEMBER_NAME+')' REF_NO" +
                                ",HVLH_INCN_APPR_REF_NO FROM HR_VEHICLE_LOAN_HEAD INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE " +
                                "WHERE HVLH_INCN_APPR_REF_NO IS NOT NULL ORDER BY HVLH_INCN_APPR_REF_NO DESC";
                }
                dt = objDA.ExecuteDataSet(sqlText).Tables[0];
                if(dt.Rows.Count>0)
                {
                    FillRefTolist(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDA = null;
            }

        }

        private void clbApprovedDocs_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            //for (int i = 0; i < clbApprovedDocs.Items.Count; i++)
            //{
            //    if (e.Index != i)
            //        clbApprovedDocs.SetItemCheckState(i, CheckState.Unchecked);
            //}
            //if (e.NewValue == CheckState.Checked)
            //{
               // GetApprovedData();
            //}
            //else
            //{
            //    lstEcodes.Items.Clear();
            //}
        }

        private void GetApprovedData()
        {
            string SqlQry = "";
            string strRefnos = "";
            //lstEcodes.Items.Clear();
            for (int i = 0; i < clbApprovedDocs.Items.Count; i++)
            {
                if (clbApprovedDocs.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (strRefnos != "")
                        strRefnos += ",";
                    strRefnos += "'" + ((SSAdmin.NewCheckboxListItem)(clbApprovedDocs.Items[i])).Tag.ToString() + "'";
                }                
            }
            if (strRefnos.Length > 10)
            {
                if (cbletterType.SelectedIndex == 0)
                    SqlQry = "SELECT HVLH_EORA_CODE FROM HR_VEHICLE_LOAN_HEAD INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_PETROL_APPR_REF_NO IN (" + strRefnos + ");";
                else if (cbletterType.SelectedIndex == 1)
                    SqlQry = "SELECT HVLH_EORA_CODE FROM HR_VEHICLE_LOAN_HEAD INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_INCN_APPR_REF_NO IN (" + strRefnos + ");";

                objDA = new SQLDB();
                DataSet dsOld = null;
                dsOld = objDA.ExecuteDataSet(SqlQry);
                objDA = null;
                
                foreach (DataRow dr in dsOld.Tables[0].Rows)
                {
                    //lstEcodes.Items.Add(dr[0].ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                string strRefnos = "";
                //lstEcodes.Items.Clear();
                for (int i = 0; i < clbApprovedDocs.Items.Count; i++)
                {
                    if (clbApprovedDocs.GetItemCheckState(i) == CheckState.Checked)
                    {
                        if (strRefnos != "")
                            strRefnos += ",";
                        strRefnos += ((SSAdmin.NewCheckboxListItem)(clbApprovedDocs.Items[i])).Tag.ToString();
                    }
                }

                if (cbletterType.SelectedIndex == 0)
                {
                    childReportViewer = new ReportViewer(strRefnos);
                    CommonData.ViewReport = "PETROL_ALLOWANCE_APPROVAL";
                    childReportViewer.Show();
                }
                else if (cbletterType.SelectedIndex == 1)
                {
                    childReportViewer = new ReportViewer(strRefnos);
                    CommonData.ViewReport = "VEHICLE_INCENTIVE_APPROVAL";
                    childReportViewer.Show();
                }
            }
        }

        private bool CheckData()
        {
            bool blVil = false;
            for (int i = 0; i < clbApprovedDocs.Items.Count; i++)
            {
                if (clbApprovedDocs.GetItemCheckState(i) == CheckState.Checked)
                    blVil = true;
            }
            if (blVil == false)
            {
                MessageBox.Show("Select Letter Ref No", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            return blVil;
        }

        private void clbApprovedDocs_Validated(object sender, EventArgs e)
        {
           // GetApprovedData();
        }

        private void cbletterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                FillAllRefNo();
            }
            else
            {
                FillUserBranchRefNo();
            }
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbApprovedDocs);
        }

        private void SearchEcode(string searchString, ListBox cbEcodes)
        {
            if (searchString.Trim().Length > 0)
            {
                for (int i = 0; i < cbEcodes.Items.Count; i++)
                {
                    if (cbEcodes.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
                    {
                        if (((System.Data.DataRowView)(cbEcodes.Items[i])).Row.ItemArray[1].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }
                    else  // for checkbox list search
                    {
                        if (cbEcodes.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }

                }
            }
        }
    }
}
