using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using System.Windows.Forms;

namespace SDMS
{
    public partial class FAOutstanding : Form
    {
        SQLDB objData = null;
        string strCmp = "", strState = "";
        DealerInfo objDATA = null; 

        String strChkCmp = "";
        String strChkState = "";
        String strChkDistrict, strChkMandal, strChkVillage = "";

        String strChkBusType = "";
        String strChkFirmType = "";
        string strFromDate, strToDate = "";


        Security objSecurity = null;
        public FAOutstanding()
        {
            InitializeComponent();
        }

        private void FAOutstanding_Load(object sender, EventArgs e)
        {
            cbReportType.SelectedIndex = 0;
            FillStates();
            if (txtOUTStandintAmtLesser.Text.Length == 0)
            {
                txtOUTStandintAmtLesser.Text = "0";
            }
            if (txtOUTStandintAmtGrater.Text.Length == 0)
            {
                txtOUTStandintAmtGrater.Text = "9999999";
            }
            if (txtDelayFrom.Text.Length == 0)
            {
                txtDelayFrom.Text = "0";
            }
            if (txtDelayTo.Text.Length == 0)
            {
                txtDelayTo.Text = "999";
            }
             strFromDate = "01012013";
             strToDate = "01012014";

            chkDistrictAll.Enabled = false;
            chkMandalAll.Enabled = false;
            chkVillageAll.Enabled = false;

        }
        private void FillStates()
        {
            DataTable dt = null;
            objData = new SQLDB();
            objDATA = new DealerInfo();
            string strCmd = "";
            clbState.Items.Clear();
            strChkState = "";
            strChkDistrict = "";
            try
            {
                dt = objDATA.GetDlStates_Proc().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["code"].ToString();
                        oclBox.Text = item["state"].ToString();
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
            objDATA = new DealerInfo();
            clbDistrict.Items.Clear();
            strChkDistrict = "";
            string strCmd = "";   
            try
            {
                if (sState.Length > 0)
                {
                    sState = sState.Remove(sState.Length - 1);
                    strCmd = " SELECT DISTINCT DAMH_FIRM_HEAD_DISTRICT District FROM DL_APPL_MASTER_HEAD WHERE  DAMH_FIRM_HEAD_STATE IN (SELECT sm_state FROM state_mas WHERE sm_state_code in (" + sState + "))";
                    //dt = objDATA.GetDlVillages(sState, string.Empty, string.Empty).Tables[0];
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["District"].ToString();
                        oclBox.Text = item["District"].ToString();
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
        private void FillMandal(string sState,string sDistrict)
        {
            DataTable dt = new DataTable();
            objData = new SQLDB();
            objDATA = new DealerInfo();
            clbMandal.Items.Clear();
            //strChkDistrict = "";
            string strCmd = "";

            
            try
            {
                if (sState.Length > 0 && sDistrict.Length>0)
                {
                    sState = sState.Remove(sState.Length - 1);
                    sDistrict = sDistrict.Remove(sDistrict.Length - 1);
                    strCmd = "SELECT DISTINCT DAMH_FIRM_HEAD_MANDAL Mandal FROM DL_APPL_MASTER_HEAD WHERE  DAMH_FIRM_HEAD_DISTRICT IN (" + sDistrict + ")";

                   // dt = objDATA.GetDlVillages(sState, sDistrict, string.Empty).Tables[0];
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["Mandal"].ToString();
                        oclBox.Text = item["Mandal"].ToString();
                        clbMandal.Items.Add(oclBox);
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
        private void FillVillage(string sState, string sDistrict,string sMandal)
        {
            DataTable dt = null;
            objData = new SQLDB();
            objDATA = new DealerInfo();
            clbVillage.Items.Clear();
            //strChkDistrict = "";
            string strCmd = "";
                       
            try
            {
                if (sState.Length > 0 && sDistrict.Length > 0 && sMandal.Length>0)
                {
                    sState = sState.Remove(sState.Length - 1);
                    sDistrict = sDistrict.Remove(sDistrict.Length - 1);
                    sMandal = sMandal.Remove(sMandal.Length - 1);
                    strCmd = "SELECT DISTINCT DAMH_FIRM_HEAD_VILL_OR_TOWN Village FROM DL_APPL_MASTER_HEAD WHERE  DAMH_FIRM_HEAD_MANDAL IN (" + sMandal + ")";

                    //dt = objDATA.GetDlVillages(sState, sDistrict, sMandal).Tables[0];
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["Village"].ToString();
                        oclBox.Text = item["Village"].ToString();
                        clbVillage.Items.Add(oclBox);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void chkStateAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStateAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbState.Items.Count; iVar++)
                {
                    clbState.SetItemCheckState(iVar, CheckState.Checked);
                }
                strState = "";
                strChkState = "ALL";
                for (int iVar = 0; iVar < clbState.Items.Count; iVar++)
                {
                    strState += "'"+ ((NewCheckboxListItem)clbState.Items[iVar]).Tag + "',";
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
                clbMandal.Items.Clear();
                clbVillage.Items.Clear();
                chkDistrictAll.Checked = false;
                chkDistrictAll.Enabled = false;
                chkMandalAll.Checked = false;
                chkMandalAll.Enabled = false;
                chkVillageAll.Checked = false;
                chkVillageAll.Enabled = false;
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
                clbMandal.Items.Clear();
                clbVillage.Items.Clear();
                chkDistrictAll.Enabled = false;
                chkMandalAll.Enabled = false;
                chkVillageAll.Enabled = false;

                chkDistrictAll.Checked = false;
                chkMandalAll.Checked = false;
                chkVillageAll.Checked = false;
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
                for (int iVar = 0; iVar < clbDistrict.Items.Count; iVar++)
                {
                    strChkDistrict += "'" + ((NewCheckboxListItem)clbDistrict.CheckedItems[iVar]).Tag + "',";
                }
                //strChkDistrict = "";
                FillMandal(strChkState, strChkDistrict);
                chkMandalAll.Enabled = true;
            }
            else
            {
                for (int iVar = 0; iVar < clbDistrict.Items.Count; iVar++)
                {
                    clbDistrict.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkDistrict = "";
                clbMandal.Items.Clear();
                clbVillage.Items.Clear();
                chkMandalAll.Enabled = false;
                chkVillageAll.Enabled = false;

                chkMandalAll.Checked = false;
                chkVillageAll.Checked = false;
            }
        }

        private void clbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkDistrict = "";
            for (int iVar = 0; iVar < clbDistrict.CheckedItems.Count; iVar++)
            {
                strChkDistrict += "'" + ((NewCheckboxListItem)clbDistrict.CheckedItems[iVar]).Tag + "',";
            }
            if (strChkDistrict.Length > 0)
            {
                FillMandal(strChkState, strChkDistrict);
                chkMandalAll.Enabled = true;
            }
            else
            {
                clbMandal.Items.Clear();
                clbVillage.Items.Clear();
                chkVillageAll.Enabled = false;
                chkMandalAll.Enabled = false;

                chkMandalAll.Checked = false;
                chkVillageAll.Checked = false;
            }
        }

        private void chkMandalAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMandalAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbMandal.Items.Count; iVar++)
                {
                    clbMandal.SetItemCheckState(iVar, CheckState.Checked);
                }
                strChkMandal = "";
                for (int iVar = 0; iVar < clbMandal.Items.Count; iVar++)
                {
                    strChkMandal += "'" + ((NewCheckboxListItem)clbMandal.Items[iVar]).Tag + "',";
                }
                //strChkMandal = "ALL";
                //strChkMandal = "";
                FillVillage(strChkState, strChkDistrict, strChkMandal);
                chkVillageAll.Enabled = true;
            }
            else
            {
                for (int iVar = 0; iVar < clbMandal.Items.Count; iVar++)
                {
                    clbMandal.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkMandal = "";
                clbVillage.Items.Clear();
                chkVillageAll.Enabled = false;
                
                chkVillageAll.Checked = false;
            }
        }

        private void clbMandal_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkMandal = "";
            for (int iVar = 0; iVar < clbMandal.CheckedItems.Count; iVar++)
            {
                strChkMandal+= "'"+ clbMandal.CheckedItems[iVar].ToString() + "',";
            }
            if (strChkMandal.Length > 0)
            {
                FillVillage(strChkState, strChkDistrict,strChkMandal);
                chkVillageAll.Enabled = true;
            }
            else
            {
                clbVillage.Items.Clear();
                chkVillageAll.Enabled = false;

                chkVillageAll.Checked = false;
            }
        }

        private void chkVillageAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVillageAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbVillage.Items.Count; iVar++)
                {
                    clbVillage.SetItemCheckState(iVar, CheckState.Checked);
                }
                strChkVillage = "";
                for (int iVar = 0; iVar < clbVillage.Items.Count; iVar++)
                {
                    strChkVillage+= "'" + ((NewCheckboxListItem)clbVillage.Items[iVar]).Tag + "',";
                }
                strChkVillage = "ALL";
            }
            else
            {
                for (int iVar = 0; iVar < clbVillage.Items.Count; iVar++)
                {
                    clbVillage.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkVillage = "";
            }
        }

        private void clbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkVillage = "";
            for (int iVar = 0; iVar < clbVillage.CheckedItems.Count; iVar++)
            {
                strChkVillage += clbVillage.CheckedItems[iVar].ToString() + ",";
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

            //if (txtOUTStandintAmtLesser.Text.Length == 0)
            //{
            //    txtOUTStandintAmtLesser.Text = "0";
            //}
            //if (txtOUTStandintAmtGrater.Text.Length == 0)
            //{
            //    txtOUTStandintAmtGrater.Text = "9999999";
            //}
            //if (txtDelayFrom.Text.Length == 0)
            //{
            //    txtDelayFrom.Text = "0";
            //}
            //if (txtDelayTo.Text.Length == 0)
            //{
            //    txtDelayTo.Text = "999";
            //}
            //string strFromDate = "01012013";
            //string strToDate = "01012014";

            if (cbReportType.SelectedIndex == 0)
            {
                ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode, strChkState, strChkDistrict, strChkBusType, strChkFirmType,
                    ((float)Convert.ToDouble(txtDelayFrom.Text)), ((float)Convert.ToDouble(txtDelayTo.Text)),
                    ((float)Convert.ToDouble(txtOUTStandintAmtLesser.Text)), ((float)Convert.ToDouble(txtOUTStandintAmtGrater.Text)),
                    ((float)Convert.ToDouble(strFromDate)), ((float)Convert.ToDouble(strToDate)), "DETAILED");
                CommonData.ViewReport = "DL_OUTSTANDING_AGE_REP";
                childReportViewer.Show();
            }
            else if (cbReportType.SelectedIndex == 1)
            {
                ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode, strChkState, strChkDistrict, strChkBusType, strChkFirmType,
                    ((float)Convert.ToDouble(txtDelayFrom.Text)), ((float)Convert.ToDouble(txtDelayTo.Text)),
                    ((float)Convert.ToDouble(txtOUTStandintAmtLesser.Text)), ((float)Convert.ToDouble(txtOUTStandintAmtGrater.Text)),
                    ((float)Convert.ToDouble(strFromDate)), ((float)Convert.ToDouble(strToDate)), "DETAILED");
                CommonData.ViewReport = "SATL_OS_DETL_Report_ModelTwo";
                childReportViewer.Show();
            }
        }

        private void txtOUTStandintAmtLesser_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtOUTStandintAmtGrater_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtDelayFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtDelayTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

    }
}
