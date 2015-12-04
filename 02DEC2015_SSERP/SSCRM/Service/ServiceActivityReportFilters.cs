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
using SSTrans;
using SSAdmin;
using System.Collections;
using System.Configuration;
using System.IO;


namespace SSCRM
{
    public partial class ServiceActivityReportFilters : Form
    {
        InvoiceDB objInv = null;
        public Security objSecurity = new Security();
        SQLDB objSQLdb = null;
        private ReportViewer childReportViewer = null;       

        double dFrmQty = 0, dToQty = 0;
        private string Company = "", Branches = "", DocumentMonth = "", Strstate = "", sDistrict = "", sMandal = "", sVillage = "",
                       strStatus = "", sCamps = "", sActivity = "", sCategoryCode = "";
        private string sChkComp = "", sChkBran = "", sChkDocMonth = "", sChkDistrict = "", sChkVill = "";
        
        public ServiceActivityReportFilters()
        {
            InitializeComponent();
        }

        private void ServiceActivityReportFilters_Load(object sender, EventArgs e)
        {
            FillBranches();
            FillDocumentMonths();
            sCategoryCode = "001";
            cbProdType.SelectedIndex = 0;
            cbReportType.SelectedIndex = 0;
            cbUnitsType.SelectedIndex = 0;
            FillActivityTypes(sCategoryCode);
        }
        private void FillBranches()
        {
            objInv = new InvoiceDB();
            DataSet ds = new DataSet();

            try
            {
                ds = objInv.UserBranchCursor_Get("", "", "PARENT");
               
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tvBranches.Nodes.Add("COMPANY AND BRANCHES", "COMPANY AND BRANCHES");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvBranches.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());

                        DataSet dschild = new DataSet();
                        dschild = objInv.UserBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");

                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[0].Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }

                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.tvBranches.SelectedNode = tvBranches.Nodes[0];
                    this.tvBranches.SelectedNode.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void FillDocumentMonths()
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            DataSet dschild = new DataSet();
            string strSQL = "";
            try
            {
                strSQL = "SELECT DISTINCT FY_FIN_YEAR FROM FIN_YEAR";
                ds = objSQLdb.ExecuteDataSet(strSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tvDocMonth.Nodes.Add("Document Months", "Document Months");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvDocMonth.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString(), ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString());
                        strSQL = "";
                        strSQL = "SELECT DISTINCT DOCUMENT_MONTH,start_date FROM DOCUMENT_MONTH WHERE FIN_YEAR = '" + ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString() + "' ORDER BY START_DATE ASC";
                        dschild = objSQLdb.ExecuteDataSet(strSQL);
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvDocMonth.Nodes[0].Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString(), dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString());
                            }
                        }
                    }

                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.tvDocMonth.SelectedNode = tvDocMonth.Nodes[0];
                    this.tvDocMonth.SelectedNode.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillActivityTypes(string CategoryCode)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbActivityTypes.Items.Clear();
            try
            {
                strCmd = "SELECT DISTINCT SAM_ACTIVITY_SHORT_NAME ActivityName "+
                         " FROM SERVICES_ACTIVITIES_MAS "+
                         "INNER JOIN SERVICES_TNA ON TNA_ACTIVITY_ID=SAM_ACTIVITY_ID "+
                         " where SAM_CATEGORY_ID in (" + CategoryCode + ") ";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["ActivityName"].ToString();
                        oclBox.Text = item["ActivityName"].ToString();
                        clbActivityTypes.Items.Add(oclBox);
                        oclBox = null;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillStates(string sCompCode,string sBrancode,string sDocMonth)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbState.Items.Clear();

            try
            {
                if (sCompCode.Length > 1 && sBrancode.Length > 1 && sDocMonth.Length > 1)
                {
                    strCmd = "SELECT distinct CM_STATE State " +
                             " FROM SALES_INV_HEAD " +
                             " INNER JOIN CUSTOMER_MAS  ON CM_FARMER_ID=SIH_FARMER_ID " +
                             " WHERE SIH_COMPANY_CODE IN (" + sCompCode + ") AND "+
                             " SIH_BRANCH_CODE IN(" + sBrancode + ") AND SIH_DOCUMENT_MONTH IN(" + sDocMonth + ") " +
                             " ORDER BY CM_STATE ASC";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["State"].ToString();
                        oclBox.Text = item["State"].ToString();
                        clbState.Items.Add(oclBox);
                        oclBox = null;
                    }
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillDistrictsList(string CompCode,string BranCode,string DocMonth,string State)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbDistrict.Items.Clear();

            try
            {
                State = State.TrimEnd(',');
                if(CompCode.Length>1 && BranCode.Length>1 && DocMonth.Length>1 && State.Length>1)
                {
                strCmd = "SELECT DISTINCT CM_DISTRICT District "+
                         " FROM SALES_INV_HEAD "+
                         " INNER JOIN CUSTOMER_MAS  ON CM_FARMER_ID=SIH_FARMER_ID "+
                         " WHERE SIH_COMPANY_CODE IN ("+ CompCode +") AND SIH_BRANCH_CODE IN("+ BranCode +") "+
                         " AND SIH_DOCUMENT_MONTH IN("+ DocMonth +") AND CM_STATE IN ("+ State +") "+
                         " ORDER BY CM_DISTRICT ASC";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
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
        }

        private void FillMandalsList(string CompCode, string BranCode, string DocMonth, string State,string District)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbMandal.Items.Clear();

            try
            {
                State = State.TrimEnd(',');
                District = District.TrimEnd(',');

                if (CompCode.Length > 1 && BranCode.Length > 1 && DocMonth.Length > 1 && State.Length > 1 && District.Length>1)
                {
                    strCmd = "SELECT DISTINCT CM_MANDAL Mandal " +
                             " FROM SALES_INV_HEAD " +
                             " INNER JOIN CUSTOMER_MAS  ON CM_FARMER_ID=SIH_FARMER_ID " +
                             " WHERE SIH_COMPANY_CODE IN (" + CompCode + ") AND SIH_BRANCH_CODE IN(" + BranCode + ") " +
                             " AND SIH_DOCUMENT_MONTH IN(" + DocMonth + ") AND CM_STATE IN (" + State + ") " +
                             " and CM_DISTRICT in(" + District + ") ORDER BY CM_MANDAL ASC";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
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
        }

        private void FillVillagesList(string CompCode, string BranCode, string DocMonth, string State, string District,string Mandal)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbVillage.Items.Clear();

            try
            {
                State = State.TrimEnd(',');
                District = District.TrimEnd(',');
                Mandal = Mandal.TrimEnd(',');

                if (CompCode.Length > 1 && BranCode.Length > 1 && DocMonth.Length > 1 && State.Length > 1 && District.Length > 1)
                {
                    strCmd = "SELECT DISTINCT CM_VILLAGE Village " +
                             " FROM SALES_INV_HEAD " +
                             " INNER JOIN CUSTOMER_MAS  ON CM_FARMER_ID=SIH_FARMER_ID " +
                             " WHERE SIH_COMPANY_CODE IN (" + CompCode + ") AND SIH_BRANCH_CODE IN(" + BranCode + ") " +
                             " AND SIH_DOCUMENT_MONTH IN(" + DocMonth + ") AND CM_STATE IN (" + State + ") " +
                             " and CM_DISTRICT in(" + District + ") and CM_MANDAL in (" + Mandal + ") ORDER BY CM_VILLAGE ASC";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
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
        }

        private bool checkdata()
        {
            bool blVil = false;
            bool flag = false;


            for (int k = 0; k < tvBranches.Nodes.Count; k++)
            {

                for (int j = 0; j < tvBranches.Nodes[k].Nodes.Count; j++)
                {
                    for (int i = 0; i < tvBranches.Nodes[k].Nodes[j].Nodes.Count; i++)
                    {
                        if (tvBranches.Nodes[k].Nodes[j].Nodes[i].Checked == true)
                        {
                            blVil = true;
                        }
                    }
                }

            }
            if (blVil == false)
            {
                MessageBox.Show("Select Branches For User", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            blVil = false;
            for (int k = 0; k < tvDocMonth.Nodes.Count; k++)
            {

                for (int j = 0; j < tvDocMonth.Nodes[k].Nodes.Count; j++)
                {
                    for (int i = 0; i < tvDocMonth.Nodes[k].Nodes[j].Nodes.Count; i++)
                    {
                        if (tvDocMonth.Nodes[k].Nodes[j].Nodes[i].Checked == true)
                        {
                            blVil = true;
                        }
                    }
                }

            }
            if (blVil == false)
            {
                MessageBox.Show("Select Document Month For User", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }



            if (cbReportType.SelectedIndex == 1 || cbReportType.SelectedIndex == 2 || cbReportType.SelectedIndex == 4 || cbReportType.SelectedIndex == 5)
            {
                if (clbDistrict.Items.Count > 0)
                {
                    for (int i = 0; i < clbDistrict.Items.Count; i++)
                    {
                        if (clbDistrict.GetItemCheckState(i) == CheckState.Checked)
                        {
                            flag = true;
                            break;
                        }
                    }
                }

                if (clbDistrict.Items.Count == 0 || flag == false)
                {
                    blVil = false;
                    MessageBox.Show("Select Atleast One District", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clbDistrict.Focus();
                    return blVil;
                }
                if (cbReportType.SelectedIndex == 2 || cbReportType.SelectedIndex == 5)
                {
                    flag = false;
                    if (clbVillage.Items.Count > 0)
                    {
                        for (int i = 0; i < clbVillage.Items.Count; i++)
                        {
                            if (clbVillage.GetItemCheckState(i) == CheckState.Checked)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }

                    if (clbVillage.Items.Count == 0 || flag == false)
                    {
                        blVil = false;
                        MessageBox.Show("Select Atleast One Village", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clbVillage.Focus();
                        return blVil;
                    }
                }
            }

            GetSelectedValues();

            if (chkCompAct.Checked == false && chkPendingAct.Checked == false && chkFutureAct.Checked == false)
            {
                flag = false;
                MessageBox.Show("Select Activity Status", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkCompAct.Focus();
                return flag;
            }
            if (cbReportType.SelectedIndex == 6 || cbReportType.SelectedIndex == 7)
            {
                flag = false;
                if (clbCamps.Items.Count > 0)
                {
                    for (int i = 0; i < clbCamps.Items.Count; i++)
                    {
                        if (clbCamps.GetItemCheckState(i) == CheckState.Checked)
                        {
                            flag = true;
                            break;
                        }
                    }
                }

                if (clbCamps.Items.Count == 0 || flag == false)
                {
                    blVil = false;
                    MessageBox.Show("Please Select Atleast One Camp", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clbCamps.Focus();
                    return blVil;
                }
            }
            if (clbActivityTypes.CheckedItems.Count == 0)
            {
                blVil = false;
                MessageBox.Show("Please Select Activity", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clbActivityTypes.Focus();
                return blVil;
            }
            //if (cbReportType.SelectedIndex == 0 || cbReportType.SelectedIndex == 1 || cbReportType.SelectedIndex==2)
            //{
            //    if (Branches.Length > 30)
            //    {
            //        blVil = false;
            //        MessageBox.Show("Please Select One or three Branches Only", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return blVil;
            //    }
            //}

            return blVil;
        }


        private void GetSelectedTeakQtyDetails()
        {
            dFrmQty = dToQty = 0;

            if (txtFrm.Text.Length > 0 || txtTo.Text.Length > 0)
            {
                if (cbUnitsType.SelectedIndex == 0)
                {
                    dFrmQty = 0;
                    dToQty = 99999;
                }

                else if (cbUnitsType.SelectedIndex == 1)
                {
                    dFrmQty = Convert.ToDouble(txtFrm.Text.ToString());
                    dToQty = Convert.ToDouble(txtTo.Text.ToString());
                }
                else if (cbUnitsType.SelectedIndex == 2)
                {
                    dFrmQty = Convert.ToDouble(txtFrm.Text.ToString());
                    dToQty = Convert.ToDouble(txtFrm.Text.ToString());
                }
                else if (cbUnitsType.SelectedIndex == 3)
                {
                    dFrmQty = Convert.ToDouble(txtFrm.Text.ToString());
                    dToQty = 99999;
                }
                else if (cbUnitsType.SelectedIndex == 4)
                {
                    dFrmQty = 0;
                    dToQty = Convert.ToDouble(txtFrm.Text.ToString());
                }
            }
        }

        private void GetSelectedValues()
        {
            Company = "";
            Branches = "";
            strStatus = "";
            DocumentMonth = "";
            sCamps = "";
            sActivity = "";


            bool iscomp = false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (Branches != string.Empty)
                                Branches += ",";
                            Branches += tvBranches.Nodes[i].Nodes[j].Nodes[k].Name.ToString();
                            iscomp = true;
                        }
                    }

                    if (iscomp == true)
                    {
                        if (Company != string.Empty)
                            Company += ",";
                        Company += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                    }
                    iscomp = false;
                }
            }

            DocumentMonth = "";
            for (int i = 0; i < tvDocMonth.Nodes.Count; i++)
            {
                for (int j = 0; j < tvDocMonth.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvDocMonth.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvDocMonth.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (DocumentMonth != string.Empty)
                                DocumentMonth += ",";
                            DocumentMonth += tvDocMonth.Nodes[i].Nodes[j].Nodes[k].Name.ToString();

                        }
                    }
                }
            }

            sDistrict = "";

            for (int i = 0; i < clbDistrict.Items.Count; i++)
            {
                if (clbDistrict.GetItemCheckState(i) == CheckState.Checked)
                {
                    sDistrict += ((NewCheckboxListItem)clbDistrict.Items[i]).Tag.ToString() + ',';
                }
            }
            

            sVillage = "";


            for (int i = 0; i < clbVillage.Items.Count; i++)
            {
                if (clbVillage.GetItemCheckState(i) == CheckState.Checked)
                {
                    sVillage += ((NewCheckboxListItem)clbVillage.Items[i]).Tag.ToString() + ',';
                }
            }
            
            //Camps
            for (int i = 0; i < clbCamps.Items.Count; i++)
            {
                if (clbCamps.GetItemCheckState(i) == CheckState.Checked)
                {
                    sCamps += ((NewCheckboxListItem)clbCamps.Items[i]).Tag.ToString() + ',';
                }
            }

                //ActivityTypes
            for (int i = 0; i < clbActivityTypes.Items.Count; i++)
            {
                if (clbActivityTypes.GetItemCheckState(i) == CheckState.Checked)
                {
                    sActivity += ((NewCheckboxListItem)clbActivityTypes.Items[i]).Tag.ToString() + ',';
                }
            }


            if (chkCompAct.Checked == true)
            {
                strStatus = "COMPLETED";
            }
            if (chkFutureAct.Checked == true)
            {
                strStatus = "FUTURE ACTIVITY";
            }
            if (chkPendingAct.Checked == true)
            {
                strStatus = "PENDING ACTIVITY";
            }
            if (chkCompAct.Checked == true && chkPendingAct.Checked == true)
            {
                strStatus = "COMPLETED,PENDING ACTIVITY";
            }
            if (chkCompAct.Checked == true && chkFutureAct.Checked == true)
            {
                strStatus = "COMPLETED,FUTURE ACTIVITY";
            }
            if (chkPendingAct.Checked == true && chkFutureAct.Checked == true)
            {
                strStatus = "PENDING ACTIVITY,FUTURE ACTIVITY";
            }
            if (chkCompAct.Checked == true && chkPendingAct.Checked == true && chkFutureAct.Checked == true)
            {
                strStatus = "ALL";
            }


            sDistrict = sDistrict.TrimEnd(',');
            sVillage = sVillage.TrimEnd(',');
            sCamps = sCamps.TrimEnd(',');
            sActivity = sActivity.TrimEnd(',');

        }

        private DataSet GetServiceCampGroups(string CompCode, string BranCode, string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ServiceCampGroupsList", CommandType.StoredProcedure, param);

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
        private void FillCampGroups(string CompCode, string BranCode, string DocMonth)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            clbCamps.Items.Clear();
            try
            {
                dt = GetServiceCampGroups(CompCode, BranCode, DocMonth).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["lgm_group_name"].ToString();
                        oclBox.Text = item["lgm_group_name"].ToString();
                        clbCamps.Items.Add(oclBox);
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
                objSQLdb = null;
                dt = null;
            }
        }
        
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (checkdata() == true)
            {
                Int32 frmQty = 0, ToQty = 0;
               
                GetSelectedValues();
                GetSelectedTeakQtyDetails();
                if (dFrmQty != 0 || dToQty != 0)
                {

                }
                else
                {
                    dFrmQty = 0;
                    dToQty = 99999;
                }
                if (strStatus.Length == 0)
                {
                    strStatus = "COMPLETED,PENDING ACTIVITY";
                }

                 frmQty = Convert.ToInt32(dFrmQty);
                 ToQty = Convert.ToInt32(dToQty);
                
                if (cbReportType.SelectedIndex == 0)
                {
                    CommonData.ViewReport = "SERVICES_ACTIVITY_STATUS_LIST";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, "","","", cbProdType.Text.ToString(),sActivity,strStatus,Convert.ToString(frmQty),Convert.ToString(ToQty),"ALL");
                    childReportViewer.Show();
                }
                else if (cbReportType.SelectedIndex == 1)
                {
                    CommonData.ViewReport = "DISTRICT_WISE_SERVICES_ACTIVITY_STATUS_LIST";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, sDistrict, "", "ALL", cbProdType.Text.ToString(), sActivity, strStatus, Convert.ToString(frmQty), Convert.ToString(ToQty), "DISTRICT_WISE");
                    childReportViewer.Show();
                }
                else if (cbReportType.SelectedIndex == 2)
                {
                    CommonData.ViewReport = "VILLAGE_WISE_SERVICES_ACTIVITY_STATUS_LIST";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, "ALL", "", sVillage, cbProdType.Text.ToString(), sActivity, strStatus, Convert.ToString(frmQty), Convert.ToString(ToQty), "VILLAGE_WISE");
                    childReportViewer.Show();
                }
                else if (cbReportType.SelectedIndex == 3)
                {
                    CommonData.ViewReport = "SERVICES_ACTIVITIES_SUMMARY";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, "ALL", "", "ALL", cbProdType.Text.ToString(), sActivity, strStatus, Convert.ToString(frmQty), Convert.ToString(ToQty), "ALL");
                    childReportViewer.Show();

                }
                else if (cbReportType.SelectedIndex == 4)
                {
                    CommonData.ViewReport = "DISTRICT_WISE_SERVICES_ACTIVITIES_SUMMARY";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, sDistrict, "", "ALL", cbProdType.Text.ToString(), sActivity, strStatus, Convert.ToString(frmQty), Convert.ToString(ToQty), "DISTRICT_WISE");
                    childReportViewer.Show();
                }
                else if (cbReportType.SelectedIndex == 5)
                {
                    CommonData.ViewReport = "VILLAGE_WISE_SERVICES_ACTIVITIES_SUMMARY";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, "ALL", "", sVillage, cbProdType.Text.ToString(), sActivity, strStatus, Convert.ToString(frmQty), Convert.ToString(ToQty), "VILLAGE_WISE");
                    childReportViewer.Show();
                }
                else if (cbReportType.SelectedIndex == 6)
                {
                    CommonData.ViewReport = "CAMP_WISE_SERVICE_ACTIVITIES_LIST";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, "ALL", sCamps, sVillage, cbProdType.Text.ToString(), sActivity, strStatus, Convert.ToString(frmQty), Convert.ToString(ToQty), "CAMP_WISE");
                    childReportViewer.Show();
                }
                else if (cbReportType.SelectedIndex == 7)
                {
                    CommonData.ViewReport = "CAMP_WISE_SERVICE_ACTIVITY_SUMMARY";
                    childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, "ALL",sCamps, sVillage, cbProdType.Text.ToString(), sActivity, strStatus, Convert.ToString(frmQty), Convert.ToString(ToQty), "CAMP_WISE");
                    childReportViewer.Show();
                }
            }

        }      


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

    

        private void clbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Strstate = "";
            clbDistrict.Items.Clear();
            clbMandal.Items.Clear();
            clbVillage.Items.Clear();

            for (int i = 0; i < clbState.Items.Count; i++)
            {
                if (clbState.GetItemCheckState(i) == CheckState.Checked)
                {
                    Strstate += "'" + ((NewCheckboxListItem)clbState.Items[i]).Text + "',";
                }
            }
            if (Strstate.Length > 0)
            {
                FillDistrictsList(sChkComp, sChkBran, sChkDocMonth, Strstate);
                
            }
            else
            {
                clbDistrict.Items.Clear();
                clbMandal.Items.Clear();
                clbVillage.Items.Clear();                
            }
        }

        

        private void clbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            clbMandal.Items.Clear();
            clbVillage.Items.Clear();

            sChkDistrict = "";
            for (int i = 0; i < clbDistrict.Items.Count; i++)
            {
                if (clbDistrict.GetItemCheckState(i) == CheckState.Checked)
                {
                    sChkDistrict += "'" + ((NewCheckboxListItem)clbDistrict.Items[i]).Text + "',";
                }
            }
            if (sChkDistrict.Length > 0)
            {
                FillMandalsList(sChkComp, sChkBran, sChkDocMonth, Strstate, sChkDistrict);
                
            }
            else
            {                
                clbMandal.Items.Clear();
                clbVillage.Items.Clear();                              
            }

        }

       
        
        private void clbMandal_SelectedIndexChanged(object sender, EventArgs e)
        {
            sMandal = "";
            clbVillage.Items.Clear();

            for (int i = 0; i < clbMandal.Items.Count; i++)
            {
                if (clbMandal.GetItemCheckState(i) == CheckState.Checked)
                {
                    sMandal +="'" + ((NewCheckboxListItem)clbMandal.Items[i]).Text + "',";
                }
            }
            if (sMandal.Length > 0)
            {
                FillVillagesList(sChkComp, sChkBran, sChkDocMonth, Strstate, sChkDistrict, sMandal);               
            }
            else
            {
                clbVillage.Items.Clear();                
            }
        }

      


        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);

            clbState.Items.Clear();
            clbDistrict.Items.Clear();
            clbMandal.Items.Clear();
            clbVillage.Items.Clear();
            GetSelectedValues();           

            tvBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvBranches.EndUpdate();

            sChkComp = "";
            sChkBran = "";


            bool iscomp = false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (sChkBran != string.Empty)
                                sChkBran += ",";
                            sChkBran += "'" + tvBranches.Nodes[i].Nodes[j].Nodes[k].Name.ToString() + "'";
                            iscomp = true;

                        }
                    }

                    if (iscomp == true)
                    {
                        if (sChkComp != string.Empty)
                            sChkComp += ",";
                        sChkComp += "'" + tvBranches.Nodes[i].Nodes[j].Name.ToString() + "'";
                    }
                    iscomp = false;
                }
            }

            FillStates(sChkComp, sChkBran, sChkDocMonth);
            FillCampGroups(Company,Branches,DocumentMonth);

        }

        private void tvDocMonth_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);
            GetSelectedValues();
            clbState.Items.Clear();
            clbDistrict.Items.Clear();
            clbMandal.Items.Clear();
            clbVillage.Items.Clear();
           
            tvDocMonth.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvDocMonth.EndUpdate();

            sChkDocMonth = "";
            for (int i = 0; i < tvDocMonth.Nodes.Count; i++)
            {
                for (int j = 0; j < tvDocMonth.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvDocMonth.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvDocMonth.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (sChkDocMonth != string.Empty)
                                sChkDocMonth += ",";
                            sChkDocMonth += "'" + tvDocMonth.Nodes[i].Nodes[j].Nodes[k].Name.ToString() + "'";

                        }
                    }
                }
            }

            FillStates(sChkComp, sChkBran, sChkDocMonth);
            FillCampGroups(Company, Branches, DocumentMonth);
        }

              
        private void cbUnitsType_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if (cbUnitsType.SelectedIndex == 2 || cbUnitsType.SelectedIndex == 3 || cbUnitsType.SelectedIndex == 4)
            {
                lblTo.Visible = false;
                txtTo.Visible = false;
                lblFrm.Text = "No.";
            }
            else if (cbUnitsType.SelectedIndex == 0)
            {
                lblTo.Visible = false;
                txtTo.Visible = false;
                lblFrm.Visible = false;
                txtFrm.Visible = false;
            }
            else
            {
                lblTo.Visible = true;
                txtTo.Visible = true;
                lblFrm.Visible = true;
                txtFrm.Visible = true;
                lblFrm.Text = "From";
            }
            
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

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDistrict);
        }

        private void txtMandalSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtMandalSearch.Text.ToString(), clbMandal);
        }

        private void txtVilSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtVilSearch.Text.ToString(), clbVillage);
        }
               

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReportType.SelectedIndex == 0 || cbReportType.SelectedIndex == 3 || cbReportType.SelectedIndex==6 || cbReportType.SelectedIndex==7)
            {
                grpStates.Enabled = false;
                grpDistricts.Enabled = false;
                grpMandals.Enabled = false;
                grpVillages.Enabled = false;
            }
            else
            {
                grpStates.Enabled = true;
                grpDistricts.Enabled = true;
                grpMandals.Enabled = true;
                grpVillages.Enabled = true;
            }
        }

        private void cbProdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            sCategoryCode = "";
            if (cbProdType.Text == "TEAK")
            {
                sCategoryCode = "'001'";
                FillActivityTypes(sCategoryCode);
            }
            if (cbProdType.Text == "OTHERS")
            {
                sCategoryCode = "'004','006','007','008','009'";
                FillActivityTypes(sCategoryCode);

            }
        }

        private void chkActivities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivities.Checked == true)
            {
                for (int iVar = 0; iVar < clbActivityTypes.Items.Count; iVar++)
                {
                    clbActivityTypes.SetItemCheckState(iVar, CheckState.Checked);
                }
            }
            else
            {
                for (int iVar = 0; iVar < clbActivityTypes.Items.Count; iVar++)
                {
                    clbActivityTypes.SetItemCheckState(iVar, CheckState.Unchecked);
                }
            }
        }

        private void chkCamp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCamp.Checked == true)
            {
                for (int iVar = 0; iVar < clbCamps.Items.Count; iVar++)
                {
                    clbCamps.SetItemCheckState(iVar, CheckState.Checked);
                }

            }
            else
            {
                for (int iVar = 0; iVar < clbCamps.Items.Count; iVar++)
                {
                    clbCamps.SetItemCheckState(iVar, CheckState.Unchecked);
                }
            }
        }
              
     
    }
}
