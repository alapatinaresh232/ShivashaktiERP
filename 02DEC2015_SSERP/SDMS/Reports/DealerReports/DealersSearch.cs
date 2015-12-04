using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SSCRMDB;
using SSAdmin;
using System.Windows.Forms;

namespace SDMS
{
    public partial class DealersSearch : Form
    {
        SQLDB objData=null;
        string strCmp = "",strState="";
        
        String strChkCmp = "";
        String strChkState = "";
        String strChkDistrict = "";
        String strChkBusType = "";
        String strChkFirmType = "";
        Security objSecurity = null;
        public DealersSearch()
        {
            InitializeComponent();
        }

        private void DealersSearch_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            //FillStates("");
            //FillDistricts("");
            FillBussinessType();
            FillFirmType();
            cbReportType.SelectedIndex = 0;

            chkStateAll.Enabled = false;
            chkDistrictAll.Enabled = false;


            if (txtCreditLimitLesser.Text.Length == 0)
            {
                txtCreditLimitLesser.Text = "0";
            }
            if (txtCreditLimitGreater.Text.Length == 0)
            {
                txtCreditLimitGreater.Text = "9999999";
            }
            if (txtDalerFrom.Text.Length == 0)
            {
                txtDalerFrom.Text = "0";
            }
            if (txtDalerTo.Text.Length == 0)
            {
                txtDalerTo.Text = "99";
            }
        }

        
        private void FillCompanyData()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT DISTINCT(CM_COMPANY_NAME),CM_COMPANY_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE in (SELECT DAMH_COMPANY_CODE FROM DL_APPL_MASTER_HEAD)";

                dt = objData.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //clbCompany.Items.Add(item["CM_COMPANY_NAME"].ToString(), false);
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["CM_COMPANY_CODE"].ToString();
                        oclBox.Text = item["CM_COMPANY_NAME"].ToString();
                        clbCompany.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }
        private void FillStates(string sCmp)
        {
            //string[] sCmps
            DataTable dt = null;
            objData = new SQLDB();
            string strCmd = "";
            clbState.Items.Clear();
            strChkState = "";
            strChkDistrict = "";
            if (sCmp.Length > 0)
            {
                sCmp = sCmp.Remove(sCmp.Length - 1);
                strCmd = "SELECT DISTINCT(DAMH_FIRM_HEAD_STATE),BM.STATE_CODE StateCode FROM DL_APPL_MASTER_HEAD INNER JOIN BRANCH_MAS BM ON STATE = DAMH_FIRM_HEAD_STATE WHERE DAMH_COMPANY_CODE IN("+sCmp+")";
            }
            //else
            //{
            //    //strCmd = " SELECT  DISTINCT(STATE) FROM BRANCH_MAS WHERE BRANCH_CODE in(SELECT damh_branch_code FROM DL_APPL_MASTER_HEAD)";
            //    strCmd = "SELECT DISTINCT(DAMH_FIRM_HEAD_STATE),BM.STATE_CODE StateCode FROM DL_APPL_MASTER_HEAD INNER JOIN BRANCH_MAS BM ON STATE = DAMH_FIRM_HEAD_STATE";
            //}
            try
            {
                dt = objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["DAMH_FIRM_HEAD_STATE"].ToString();
                        oclBox.Text = item["DAMH_FIRM_HEAD_STATE"].ToString();
                        clbState.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }
        private void FillDistricts(string sState)
        {
            DataTable dt = null;
            objData = new SQLDB();
            clbDistrict.Items.Clear();
            strChkDistrict = "";
            string strCmd = "";
            if (sState.Length > 0)
            {
                sState = sState.Remove(sState.Length - 1);
                strCmd = "SELECT DISTINCT(DAMH_FIRM_HEAD_DISTRICT) FROM DL_APPL_MASTER_HEAD WHERE  DAMH_FIRM_HEAD_STATE IN (" + sState + ")";
            }
            //else
            //{
            //    strCmd = "SELECT DISTINCT(DAMH_FIRM_HEAD_DISTRICT) FROM DL_APPL_MASTER_HEAD";
            //}
            try
            {
                dt = objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //clbDistrict.Items.Add(item["LOCATION"].ToString(), false);
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["DAMH_FIRM_HEAD_DISTRICT"].ToString();
                        oclBox.Text = item["DAMH_FIRM_HEAD_DISTRICT"].ToString();
                        clbDistrict.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }
        private void FillBussinessType()
        {
            DataTable dt = null;
            objData = new SQLDB();
            clbBussinesType.Items.Clear();
            strChkBusType = "";
            string strCmd = "";
            try
            {
                strCmd = "SELECT DISTINCT(DAMH_BUSINESS_TYPE) FROM DL_APPL_MASTER_HEAD";
                dt = objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //clbBussinesType.Items.Add(item["DAMH_BUSINESS_TYPE"].ToString(), false);
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["DAMH_BUSINESS_TYPE"].ToString();
                        oclBox.Text = item["DAMH_BUSINESS_TYPE"].ToString();
                        clbBussinesType.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }
        private void FillFirmType()
        {
            DataTable dt = null;
            objData = new SQLDB();
            clbFirmType.Items.Clear();
            strChkFirmType = "";
            string strCmd = "";
            try
            {
                strCmd = "SELECT DISTINCT(damh_firm_type) FROM DL_APPL_MASTER_HEAD";
                dt = objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //clbFirmType.Items.Add(item["damh_firm_type"].ToString(), false);
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["damh_firm_type"].ToString();
                        oclBox.Text = item["damh_firm_type"].ToString();
                        clbFirmType.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }
        private void chkCompanyAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompanyAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                {
                    clbCompany.SetItemCheckState(iVar, CheckState.Checked);
                }
               
                strChkCmp = "ALL";
                for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                {
                    strCmp += "'" + ((NewCheckboxListItem)clbCompany.Items[iVar]).Tag + "',";
                }
                FillStates(strCmp);
                chkStateAll.Enabled = true;
            }
            else
            {
                for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                {
                    clbCompany.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkCmp = "";
                clbState.Items.Clear();
                clbDistrict.Items.Clear();
                chkStateAll.Enabled = false;
                chkDistrictAll.Enabled = false;
            }
        }

        private void chkBussinessTypeAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBussinessTypeAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbBussinesType.Items.Count; iVar++)
                {
                    clbBussinesType.SetItemCheckState(iVar, CheckState.Checked);
                }
                strChkBusType = "";
                //for (int iVar = 0; iVar < clbBussinesType.Items.Count; iVar++)
                //{
                //    strChkBusType += "'" + ((NewCheckboxListItem)clbBussinesType.Items[iVar]).Tag + "',";
                //}
                strChkBusType = "ALL";
            }
            else
            {
                for (int iVar = 0; iVar < clbBussinesType.Items.Count; iVar++)
                {
                    clbBussinesType.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkBusType = "";
            }
        }

        private void chkFirmTypeAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFirmTypeAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbFirmType.Items.Count; iVar++)
                {
                    clbFirmType.SetItemCheckState(iVar, CheckState.Checked);
                }
                strChkFirmType = "";
                //for (int iVar = 0; iVar < clbFirmType.Items.Count; iVar++)
                //{
                //    strChkFirmType += "'" + ((NewCheckboxListItem)clbFirmType.Items[iVar]).Tag + "',";
                //}
                strChkFirmType = "ALL";
            }
            else
            {
                for (int iVar = 0; iVar < clbFirmType.Items.Count; iVar++)
                {
                    clbFirmType.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkFirmType = "";
            }
        }

        private void chkStateAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStateAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbState.Items.Count; iVar++)
                {
                    clbState.SetItemCheckState(iVar, CheckState.Checked);
                }
                strChkState = "";
                strChkState = "ALL";
                for (int iVar = 0; iVar < clbState.Items.Count; iVar++)
                {
                    strState += "'" + ((NewCheckboxListItem)clbState.Items[iVar]).Tag + "',";
                }
                FillDistricts(strState);
                chkDistrictAll.Enabled = true;
            }
            else
            {
                for (int iVar = 0; iVar < clbState.Items.Count; iVar++)
                {
                    clbState.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkState = "";
                clbDistrict.Items.Clear();
                chkDistrictAll.Enabled = false;
            }
        }

        private void chkDistrictAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDistrictAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbDistrict.Items.Count; iVar++)
                {
                    clbDistrict.SetItemCheckState(iVar, CheckState.Checked);
                }
                strChkDistrict = "";
                //for (int iVar = 0; iVar < clbDistrict.Items.Count; iVar++)
                //{
                //    strChkDistrict += "'" + ((NewCheckboxListItem)clbDistrict.Items[iVar]).Tag + "',";
                //}
                strChkDistrict = "ALL";
            }
            else
            {
                for (int iVar = 0; iVar < clbDistrict.Items.Count; iVar++)
                {
                    clbDistrict.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkDistrict = "";
            }
        }

        private void txtDalerFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtDalerTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void restrctingToDigits(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCreditLimitLesser_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtCreditLimitGreater_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkCmp = "";
            for (int iVar = 0; iVar < clbCompany.CheckedItems.Count; iVar++)
            {
                strChkCmp += "'" + ((NewCheckboxListItem)clbCompany.CheckedItems[iVar]).Tag + "',";
            }
            if (strChkCmp.Length > 0)
            {
                FillStates(strChkCmp);
                chkStateAll.Enabled = true;
            }
            else
            {
                clbState.Items.Clear();
                clbDistrict.Items.Clear();
                chkStateAll.Enabled = false;
                chkDistrictAll.Enabled = false;
            }
        }
       

        private void clbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkState = "";
            for (int iVar = 0; iVar < clbState.CheckedItems.Count; iVar++)
            {
                strChkState += "'" + ((NewCheckboxListItem)clbState.CheckedItems[iVar]).Tag + "',";
            }
            if (strChkState.Length > 0)
            {
                FillDistricts(strChkState);
                chkDistrictAll.Enabled = true;
            }
            else
            {
                clbDistrict.Items.Clear();
                chkDistrictAll.Enabled = false;
            }
        }
        
        
        private void clbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
             strChkDistrict= "";
            for (int iVar = 0; iVar < clbDistrict.CheckedItems.Count; iVar++)
            {
                strChkDistrict += "'" + clbDistrict.CheckedItems[iVar].ToString() + "',";
            }
        }
        private void clbBussinesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkBusType = "";
            for (int iVar = 0; iVar < clbBussinesType.CheckedItems.Count; iVar++)
            {
                strChkBusType += "'" + clbBussinesType.CheckedItems[iVar].ToString() + "',";
            }
        }

        private void clbFirmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkFirmType = "";
            for (int iVar = 0; iVar < clbFirmType.CheckedItems.Count; iVar++)
            {
                strChkFirmType += "'" + clbFirmType.CheckedItems[iVar].ToString() + "',";
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            strChkCmp = strChkCmp.TrimEnd(',');
            strChkState = strChkState.TrimEnd(',');
            strChkDistrict = strChkDistrict.TrimEnd(',');
            strChkBusType = strChkBusType.TrimEnd(',');
            strChkFirmType = strChkFirmType.TrimEnd(',');

            //if (txtCreditLimitLesser.Text.Length == 0)
            //{
            //    txtCreditLimitLesser.Text = "0";
            //}
            //if (txtCreditLimitGreater.Text.Length == 0)
            //{
            //    txtCreditLimitGreater.Text = "9999999";
            //}
            //if (txtDalerFrom.Text.Length == 0)
            //{
            //    txtDalerFrom.Text = "0";
            //}
            //if (txtDalerTo.Text.Length == 0)
            //{
            //    txtDalerTo.Text = "99";
            //}

            //MessageBox.Show(strChkCmp+"\n"+strChkState+"\n"+strChkDistrict+"\n"+strChkBusType+"\n"+strChkFirmType+"\n"+txtCreditLimitGreater.Text+"----"+txtCreditLimitLesser.Text+"\n"+txtDalerFrom.Text+"----"+txtDalerTo.Text);

            ReportViewer childReportViewer = new ReportViewer(strChkCmp, strChkState, strChkDistrict, strChkBusType, strChkFirmType, ((float)Convert.ToDouble(txtCreditLimitLesser.Text)), ((float)Convert.ToDouble(txtCreditLimitGreater.Text)), ((float)Convert.ToDouble(txtDalerFrom.Text)), ((float)Convert.ToDouble(txtDalerTo.Text)), cbReportType.SelectedItem.ToString());
            //ReportViewer childReportViewer = new ReportViewer(strChkCmp, strChkState, strChkDistrict, strChkBusType, strChkFirmType, ((float)0), ((float)9999999), ((float)0), ((float)99), cbReportType.SelectedItem.ToString());
            CommonData.ViewReport = "Dealers Checked List";
            childReportViewer.Show();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       

       

        

       
    }
}
