using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class ServicesTourExpenses : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        string strECode = string.Empty;
               
        bool flagUpdate = false;
      

        public ServicesTourExpenses()
        {
            InitializeComponent();
        }

        private void ServicesTourExpenses_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            if (CommonData.BranchType == "BR")
            {
                FillBranchData();
                EcodeSearch();
            }
            else
            {
                cbBranch.SelectedIndex = 0;
            }
            lblFrmDate.Visible = false;
            lblToDate.Visible = false;
            dtpFrmDate.Visible = false;
            dtpToDate.Visible = false;
            
            FillEmployeeData();
           

            dtpTrnDate.Value = DateTime.Today;
            //dtpToDate.Value = DateTime.Today;
            //dtpFrmDate.Value = DateTime.Today;
            dtpDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);

            
            gvEmpTourBillDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
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
                cbCompany.SelectedValue = CommonData.CompanyCode;
                dt = null;

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

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+ STATE_CODE AS BranCode " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' and BRANCH_TYPE='BR' ORDER BY BRANCH_NAME ";

                     
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
                    cbBranch.ValueMember = "BranCode";
                }

                string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranch.SelectedValue = BranCode;


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

        private void GenerateTransactionNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            if (cbBranch.SelectedIndex > 0)
            {
                try
                {

                    string finyear = CommonData.FinancialYear.Substring(2, 2) + CommonData.FinancialYear.Substring(7, 2);
                    string strNewNo = cbBranch.SelectedValue.ToString().Split('@')[0] + finyear + "TE-";

                    string strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(STBH_TRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 " +
                                        " FROM SERVICES_TOUR_BILLS_HEAD " +
                                        " WHERE STBH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND STBH_BRANCH_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[0] + "' ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTrnNo.Text = strNewNo + Convert.ToInt32(dt.Rows[0][0]).ToString("000000");
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


        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
        }

        private void FillEmployeeData()
        {
            objServicedb = new ServiceDeptDB();
            DataSet dsEmp = null;
            try
            {
                //string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();

                dsEmp = objServicedb.Get_EcodesforService();
                DataTable dtEmp = dsEmp.Tables[0];
                //dtMapEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objServicedb = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void EcodeSearch()
        {
            objServicedb = new ServiceDeptDB();
            DataSet dsEmp = null;
            if (cbBranch.SelectedIndex > 0)
            {
                try
                {
                    string[] strBranCode = cbBranch.SelectedValue.ToString().Split('@');
                    Cursor.Current = Cursors.WaitCursor;
                    cbEcode.DataSource = null;
                    cbEcode.Items.Clear();
                    dsEmp = objServicedb.ServiceLevelEcodeSearch_Get(cbCompany.SelectedValue.ToString(), strBranCode[0], Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(), txtEcodeSearch.Text.ToString());
                    DataTable dtEmp = dsEmp.Tables[0];

                    //dtMappedEmp = dsEmp.Tables[0];
                    if (dtEmp.Rows.Count > 0)
                    {
                        cbEcode.DataSource = dtEmp;
                        cbEcode.DisplayMember = "ENAME";
                        cbEcode.ValueMember = "ECODE";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (cbEcode.SelectedIndex > -1)
                    {
                        cbEcode.SelectedIndex = 0;
                        strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                    }
                    objServicedb = null;
                    Cursor.Current = Cursors.Default;
                }
            }

        }




        private void GetEmpActivityDates()
        {
            objServicedb = new ServiceDeptDB();
            DataTable dt = new DataTable();
            gvEmpTourBillDetails.Rows.Clear();
            if (cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0 && cbEcode.SelectedIndex > -1)
            {
                try
                {
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                    if (strECode.Length > 0)
                    {

                        dt = objServicedb.Get_ServiceEmpAllActivityDates(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0], Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(), DateTime.Today, DateTime.Today, strECode).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                gvEmpTourBillDetails.Rows.Add();
                                gvEmpTourBillDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                                gvEmpTourBillDetails.Rows[i].Cells["ActivityDate"].Value = Convert.ToDateTime(dt.Rows[i]["ActivityDate"]).ToString("dd/MMM/yyyy");
                                gvEmpTourBillDetails.Rows[i].Cells["DeptTime"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["fromLocation"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["ToLocation"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["NoOfKm"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["ModeOfJourney"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["FareAmt"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["LodgeAmt"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["DAAmt"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["LocalConvAmt"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["PhoneBillAmt"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["TotalAmt"].Value = "";
                                gvEmpTourBillDetails.Rows[i].Cells["Remarks"].Value = "";
                            }

                            CalculateTotAmt();
                            CalculateTotDAAmt();
                            CalculateTotFairAmt();
                            CalculateTotKMtravelled();
                            CalculateTotLocalConvAmt();
                            CalculateTotLodgeAmt();
                            CalculateTotPhoneBillAmt();

                            btnSave.Enabled = true;
                        }
                        else
                        {
                            //MessageBox.Show("Selected Emp Not Doing Any Activities","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);

                                                      
                            gvEmpTourBillDetails.Rows.Clear();

                            txtTotDaAmt.Text = "";
                            txtTotFairAmt.Text = "";
                            txtTotAmount.Text = "";
                            txtTotLocalConv.Text = "";
                            txtTotLodgeAmt.Text = "";
                            txtTotNoOfKm.Text = "";
                            txtTotPhoneBillAmt.Text = "";
                            txtApprovedByEcode.Text = "";
                            txtApprovedName.Text = "";
                            txtAprAmt.Text = "";
                            txtRecEmpName.Text = "";
                            txtRecomAmt.Text = "";
                            txtRecommendedEcode.Text = "";
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objServicedb = null;
                    dt = null;
                }
            }
            else
            {
               
               
                gvEmpTourBillDetails.Rows.Clear();

                btnSave.Enabled = true;
                if (cbEcode.Items.Count != 0)
                    cbEcode.SelectedIndex = 0;
                txtApprovedByEcode.Text = "";
                txtApprovedName.Text = "";
                txtAprAmt.Text = "";
                txtRecEmpName.Text = "";
                txtRecomAmt.Text = "";
                txtRecommendedEcode.Text = "";
                txtTotAmount.Text = "";
                txtTotDaAmt.Text = "";
                txtTotFairAmt.Text = "";
                txtTotLocalConv.Text = "";
                txtTotLodgeAmt.Text = "";
                txtTotNoOfKm.Text = "";
                txtTotPhoneBillAmt.Text = "";
            }
        }

 


       
        private bool CheckDetails()
        {
            bool flag = true;

           
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;

            }
            if (cbBranch.SelectedIndex == 0 || cbBranch.SelectedIndex==-1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranch.Focus();
                return flag;

            }
             if (txtTrnNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Contact IT Department","Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }

            if (cbEcode.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Ecode", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
                return flag;
            }

            if (txtRecEmpName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Recommended Emp Ecode", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRecommendedEcode.Focus();
                return flag;
            }
            if (txtApprovedName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Approved Emp Ecode", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApprovedByEcode.Focus();
                return flag;
            }
            if (txtRecomAmt.Text.Length == 0 )
            {
                flag = false;
                MessageBox.Show("Please Enter Recommended Amount", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRecomAmt.Focus();
                return flag;
            }
            if (txtAprAmt.Text.Length == 0 )
            {
                flag = false;
                MessageBox.Show("Please Enter Approved Amount", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAprAmt.Focus();
                return flag;
            }

            if (gvEmpTourBillDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Month And Employee", "Tour Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;

            }
            return flag;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckDetails() == true)
            {
                if (SaveTourBillHeadDetails() > 0)
                {
                    if (SaveTourBillDetl() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        flagUpdate = false;
                        GenerateTransactionNo();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
               

        private int SaveTourBillHeadDetails()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";           
            int iRes = 0;
            try
            {

                strCommand = "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                            " WHERE STBDA_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                            "' AND STBDA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";     

                strCommand += "DELETE FROM SERVICES_TOUR_BILLS_DETL " +
                            " WHERE STBD_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +                            
                            "' AND STBD_TRN_NUMBER='" + txtTrnNo.Text.ToString() +"'";

                if (txtRecomAmt.Text.Length == 0)
                    txtRecomAmt.Text = "0";
                if (txtAprAmt.Text.Length == 0)
                    txtAprAmt.Text = "0";

                if (flagUpdate == true)
                {
                    strCommand += "UPDATE SERVICES_TOUR_BILLS_HEAD SET STBH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                "', STBH_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                "', STBH_ECODE=" + cbEcode.SelectedValue +
                                ", STBH_RECOMENDED_BY_ECODE=" + Convert.ToInt32(txtRecommendedEcode.Text) +
                                ", STBH_APPROVED_BY_ECODE=" + Convert.ToInt32(txtApprovedByEcode.Text) +
                                ", STBH_RECOMENDED_AMOUNT=" + txtRecomAmt.Text +
                                ", STBH_APPROVED_AMOUNT=" + txtAprAmt.Text +
                                ", STBH_TOUR_BILL_AMOUNT=" + txtTotAmount.Text +
                                ", STBH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                "',STBH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "', STBH_LAST_MODIFIED_DATE=getdate() " +
                                " WHERE STBH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                                        
                }
                else
                {
                    GenerateTransactionNo();
                    objSQLdb = new SQLDB();

                    strCommand = "INSERT INTO SERVICES_TOUR_BILLS_HEAD(STBH_COMPANY_CODE " +
                                                                    ", STBH_STATE_CODE " +
                                                                    ", STBH_BRANCH_CODE " +
                                                                    ", STBH_DOC_MONTH " +
                                                                    ", STBH_TRN_TYPE " +
                                                                    ", STBH_TRN_NUMBER " +
                                                                    ", STBH_TRN_DATE " +
                                                                    ", STBH_ECODE " +
                                                                    ", STBH_CREATED_BY " +
                                                                    ", STBH_CREATED_DATE " +
                                                                    ", STBH_RECOMENDED_BY_ECODE "+
                                                                    ", STBH_APPROVED_BY_ECODE "+
                                                                    ", STBH_RECOMENDED_AMOUNT "+
                                                                    ", STBH_APPROVED_AMOUNT "+
                                                                    ", STBH_TOUR_BILL_AMOUNT "+
                                                                    ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                    "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                                                                    "','" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                                                    "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                    "','SERVICETOURBILL','" + txtTrnNo.Text.ToString() +
                                                                    "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                    "'," + Convert.ToInt32(cbEcode.SelectedValue) +
                                                                    ",'" + CommonData.LogUserId +
                                                                    "',getdate(),"+ Convert.ToInt32(txtRecommendedEcode.Text) +
                                                                    ","+ Convert.ToInt32(txtApprovedByEcode.Text) +
                                                                    ","+ Convert.ToDouble(txtRecomAmt.Text) +
                                                                    ","+ Convert.ToDouble(txtAprAmt.Text) +
                                                                    ","+ Convert.ToDouble(txtTotAmount.Text) +")";

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
            return iRes;

        }

        private int SaveTourBillDetl()
        {
            objSQLdb = new SQLDB();
            int iRec = 0;
            string strCommand = "";
            Int32 nSLNO = 1;

            try
            {             

                if (gvEmpTourBillDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                    {
                        if (gvEmpTourBillDetails.Rows[i].Cells["fromLocation"].Value.ToString() != "")
                        {
                            strCommand += "INSERT INTO SERVICES_TOUR_BILLS_DETL(STBD_COMPANY_CODE " +
                                                                            ", STBD_STATE_CODE " +
                                                                            ", STBD_BRANCH_CODE " +
                                                                            ", STBD_DOC_MONTH " +
                                                                            ", STBD_TRN_NUMBER " +
                                                                            ", STBD_TRN_TYPE " +
                                                                            ", STBD_SL_NO " +
                                                                            ", STBD_TOUR_DATE " +
                                                                            ", STBD_MODE_OF_JOURNEY " +
                                                                            ", STBD_NO_OF_KM " +
                                                                            ", STBD_DEPATURE_TIME " +
                                                                            ", STBD_FROM_LOCATION " +
                                                                            ", STBD_TO_LOCATION " +
                                                                            ", STBD_FARE " +
                                                                            ", STBD_LODGE " +
                                                                            ", STBD_DA " +
                                                                            ", STBD_CONV " +
                                                                            ", STBD_PHONE_BILLS " +
                                                                            ", STBD_TOTAL " +
                                                                            ", STBD_REMARKS " +
                                                                            ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                            "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                                                                            "','" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                                                            "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                            "','" + txtTrnNo.Text.ToString() +
                                                                            "','SERVICETOURBILL'," + nSLNO +
                                                                            ",'" + Convert.ToDateTime(gvEmpTourBillDetails.Rows[i].Cells["ActivityDate"].Value).ToString("dd/MMM/yyyy") +
                                                                            "','" + gvEmpTourBillDetails.Rows[i].Cells["ModeOfJourney"].Value.ToString() +
                                                                            "'," + Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["NoOfKm"].Value) +
                                                                            ",'" + gvEmpTourBillDetails.Rows[i].Cells["DeptTime"].Value.ToString() +
                                                                            "','" + gvEmpTourBillDetails.Rows[i].Cells["fromLocation"].Value.ToString() +
                                                                            "','" + gvEmpTourBillDetails.Rows[i].Cells["ToLocation"].Value.ToString() +
                                                                            "'," + Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["FareAmt"].Value) +
                                                                            "," + Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["LodgeAmt"].Value) +
                                                                            "," + Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["DAAmt"].Value) +
                                                                            "," + Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["LocalConvAmt"].Value) +
                                                                            "," + Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["PhoneBillAmt"].Value) +
                                                                            "," + Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["TotalAmt"].Value) +
                                                                            ",'" + gvEmpTourBillDetails.Rows[i].Cells["Remarks"].Value.ToString() + "')";
                        }
                    }
                }



                if (strCommand.Length > 10)
                {
                    iRec = objSQLdb.ExecuteSaveData(strCommand);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRec;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {           
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                EcodeSearch();
            }
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                GetEmpActivityDates();
            }

        }

   
        private void txtRecommendedEcode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            if (txtRecommendedEcode.Text != "")
            {
                try
                {
                    strCommand = "SELECT MEMBER_NAME " +
                                " FROM EORA_MASTER " +
                                " WHERE ECODE=" + Convert.ToInt32(txtRecommendedEcode.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtRecEmpName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();

                    }
                    else
                    {
                        txtRecEmpName.Text = "";
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
                txtRecEmpName.Text = "";
            }
        }

        private void txtApprovedByEcode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            if (txtApprovedByEcode.Text != "")
            {
                try
                {
                    strCommand = "SELECT MEMBER_NAME " +
                                " FROM EORA_MASTER " +
                                " WHERE ECODE=" + Convert.ToInt32(txtApprovedByEcode.Text) + "";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtApprovedName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();

                    }
                    else
                    {
                        txtApprovedName.Text = "";
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
                txtApprovedName.Text = "";
            }

        }

        private void txtRecommendedEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtApprovedByEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRecomAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAprAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranch.SelectedIndex > 0)
            {
                GenerateTransactionNo();
                EcodeSearch();
                gvEmpTourBillDetails.Rows.Clear();

               
                if (cbEcode.Items.Count != 0)
                    cbEcode.SelectedIndex = 0;
                txtApprovedByEcode.Text = "";
                txtApprovedName.Text = "";
                txtAprAmt.Text = "";
                txtRecEmpName.Text = "";
                txtRecomAmt.Text = "";
                txtRecommendedEcode.Text = "";
                txtTotAmount.Text = "";
                txtTotDaAmt.Text = "";
                txtTotFairAmt.Text = "";
                txtTotLocalConv.Text = "";
                txtTotLodgeAmt.Text = "";
                txtTotNoOfKm.Text = "";
                txtTotPhoneBillAmt.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            //cbCompany.SelectedIndex = 0;
            //cbBranch.SelectedIndex = -1;
            dtpTrnDate.Value = DateTime.Today;
            GenerateTransactionNo();

            
            gvEmpTourBillDetails.Rows.Clear();

            btnSave.Enabled = true;
            if(cbEcode.Items.Count!=0)
            cbEcode.SelectedIndex = 0;
            txtApprovedByEcode.Text = "";
            txtApprovedName.Text = "";
            txtAprAmt.Text = "";
            txtRecEmpName.Text = "";
            txtRecomAmt.Text = "";
            txtRecommendedEcode.Text = "";
            txtTotAmount.Text = "";
            txtTotDaAmt.Text = "";
            txtTotFairAmt.Text = "";
            txtTotLocalConv.Text = "";
            txtTotLodgeAmt.Text = "";
            txtTotNoOfKm.Text = "";
            txtTotPhoneBillAmt.Text = "";
           

        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

   

        #region "CALCULATE TOTALS"

        private void CalculateTotFairAmt()
        {
            double TotFairAmt = 0;

            if (gvEmpTourBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpTourBillDetails.Rows[i].Cells["FareAmt"].Value) != "")
                    {
                        TotFairAmt += Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["FareAmt"].Value.ToString());
                    }
                }
            }

            txtTotFairAmt.Text = TotFairAmt.ToString("f");
        }

        private void CalculateTotLodgeAmt()
        {
            double TotLodgeAmt = 0;

            if (gvEmpTourBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpTourBillDetails.Rows[i].Cells["LodgeAmt"].Value) != "")
                    {
                        TotLodgeAmt += Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["LodgeAmt"].Value.ToString());
                    }
                }
            }

            txtTotLodgeAmt.Text = TotLodgeAmt.ToString("f");

        }

        private void CalculateTotDAAmt()
        {
            double TotDAAmt = 0;

            if (gvEmpTourBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpTourBillDetails.Rows[i].Cells["DAAmt"].Value) != "")
                    {
                        TotDAAmt += Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["DAAmt"].Value.ToString());
                    }
                }
            }

            txtTotDaAmt.Text = TotDAAmt.ToString("f");
        }

        private void CalculateTotLocalConvAmt()
        {
            double TotLocalConv = 0;

            if (gvEmpTourBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpTourBillDetails.Rows[i].Cells["LocalConvAmt"].Value) != "")
                    {
                        TotLocalConv += Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["LocalConvAmt"].Value.ToString());
                    }
                }
            }

            txtTotLocalConv.Text = TotLocalConv.ToString("f");
        }

        private void CalculateTotPhoneBillAmt()
        {
            double TotPhoneBillAmt = 0;

            if (gvEmpTourBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpTourBillDetails.Rows[i].Cells["PhoneBillAmt"].Value) != "")
                    {
                        TotPhoneBillAmt += Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["PhoneBillAmt"].Value.ToString());
                    }
                }
            }

            txtTotPhoneBillAmt.Text = TotPhoneBillAmt.ToString("f");
        }


        private void CalculateTotAmt()
        {
            double TotAmt = 0;

            if (gvEmpTourBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpTourBillDetails.Rows[i].Cells["TotalAmt"].Value) != "")
                    {
                        TotAmt += Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["TotalAmt"].Value.ToString());
                    }
                }
            }

            txtTotAmount.Text = TotAmt.ToString("f");
        }

        private void CalculateTotKMtravelled()
        {
            double TotNoOfKM = 0;

            if (gvEmpTourBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpTourBillDetails.Rows[i].Cells["NoOfKm"].Value) != "")
                    {
                        TotNoOfKM += Convert.ToDouble(gvEmpTourBillDetails.Rows[i].Cells["NoOfKm"].Value);
                    }
                }
            }

            txtTotNoOfKm.Text = TotNoOfKM.ToString("f");
        }

        #endregion

        private void gvEmpTourBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvEmpTourBillDetails.Columns["Edit"].Index)
                {
                    if (Convert.ToBoolean(gvEmpTourBillDetails.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        DataGridViewRow dgvr = gvEmpTourBillDetails.Rows[e.RowIndex];

                        frmActivityExpensesDetails ExpensesDetl = new frmActivityExpensesDetails(dgvr);
                        ExpensesDetl.dtpActivityDate.Value = Convert.ToDateTime(gvEmpTourBillDetails.Rows[e.RowIndex].Cells["ActivityDate"].Value.ToString());
                        ExpensesDetl.txtFrmLocation.Text=gvEmpTourBillDetails.Rows[e.RowIndex].Cells["fromLocation"].Value.ToString();
                        ExpensesDetl.txtToLocation.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["ToLocation"].Value.ToString();
                        ExpensesDetl.txtNoOfKM.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["NoOfKm"].Value.ToString();
                        ExpensesDetl.txtPhoneBillAmt.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["PhoneBillAmt"].Value.ToString();
                        ExpensesDetl.txtModeOfJourney.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["ModeOfJourney"].Value.ToString();
                        ExpensesDetl.txtTotAmount.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["TotalAmt"].Value.ToString();
                        ExpensesDetl.txtDaAmt.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["DAAmt"].Value.ToString();
                        ExpensesDetl.txtLocalConv.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["LocalConvAmt"].Value.ToString();
                        ExpensesDetl.txtLodgeAmt.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["LodgeAmt"].Value.ToString();
                        ExpensesDetl.txtFairAmt.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["FareAmt"].Value.ToString();
                        if (gvEmpTourBillDetails.Rows[e.RowIndex].Cells["DeptTime"].Value.ToString() != "")
                        {
                            ExpensesDetl.txtHours.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["DeptTime"].Value.ToString().Split('.')[0];
                            ExpensesDetl.txtMinutes.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["DeptTime"].Value.ToString().Split('.')[1];
                        }
                        ExpensesDetl.txtRemarks.Text = gvEmpTourBillDetails.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();
                        ExpensesDetl.objServicesTourExpenses = this;
                        ExpensesDetl.ShowDialog();

                    }
                    CalculateTotAmt();
                    CalculateTotDAAmt();
                    CalculateTotFairAmt();
                    CalculateTotKMtravelled();
                    CalculateTotLocalConvAmt();
                    CalculateTotLodgeAmt();
                    CalculateTotPhoneBillAmt();
                    if (gvEmpTourBillDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvEmpTourBillDetails.Rows.Count; i++)
                        {
                            gvEmpTourBillDetails.Rows[i].Cells["SLNO"].Value =( i + 1).ToString();
                        }
                    }
                }


            }
        }

        private void dtpDocMonth_ValueChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex >= -1)
            {
                if (flagUpdate == false)
                {
                    GetEmpActivityDates();
                }
            }
        }

        private void GetEmpTourExpensesDetails()
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;

            DataTable dtHead;

            if (txtTrnNo.Text.Length > 0)
            {
                try
                {

                    ht = objServicedb.GetEmpTourBillDetails(txtTrnNo.Text.ToString());
                    dtHead = (DataTable)ht["TourBillHead"];

                    FillEmpTourBillHead(dtHead);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objServicedb = null;
                    ht = null;
                }
            }
        }

        private void FillEmpTourBillHead(DataTable dt)
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;
            DataTable dtTourDetl;
            try
            {
                ht = objServicedb.GetEmpTourBillDetails(txtTrnNo.Text.ToString());
                dtTourDetl = (DataTable)ht["TourBillDetl"];

                if (dt.Rows.Count > 0)
                {
                    btnSave.Enabled = true;
                    flagUpdate = true;

                    cbCompany.SelectedValue = dt.Rows[0]["CompCode"].ToString();
                    cbBranch.SelectedValue = dt.Rows[0]["BranCode"].ToString();
                    dtpDocMonth.Value = Convert.ToDateTime(dt.Rows[0]["DocMonth"].ToString());
                    dtpTrnDate.Value = Convert.ToDateTime(dt.Rows[0]["TrnDate"].ToString());
                    cbEcode.SelectedValue = dt.Rows[0]["Ecode"].ToString();
                    txtApprovedByEcode.Text = dt.Rows[0]["ApprEcode"].ToString();
                    txtRecommendedEcode.Text = dt.Rows[0]["RecEcode"].ToString();
                    txtApprovedName.Text = dt.Rows[0]["ApprvEmpName"].ToString();
                    txtRecEmpName.Text = dt.Rows[0]["RecEmpName"].ToString();
                    txtRecomAmt.Text = dt.Rows[0]["RecAmt"].ToString();
                    txtAprAmt.Text = dt.Rows[0]["ApprAmt"].ToString();

                    FillEmpTourBillDetl(dtTourDetl);
                }
                else
                {
                    flagUpdate = false;
                    //cbCompany.SelectedIndex = 0;
                    //cbBranch.SelectedIndex = -1;
                                       
                    dtpTrnDate.Value = DateTime.Today;

                   

                    gvEmpTourBillDetails.Rows.Clear();

                    cbEcode.SelectedIndex = -1;
                    txtApprovedByEcode.Text = "";
                    txtApprovedName.Text = "";
                    txtAprAmt.Text = "";
                    txtRecEmpName.Text = "";
                    txtRecomAmt.Text = "";
                    txtRecommendedEcode.Text = "";
                    txtTotAmount.Text = "";
                    txtTotDaAmt.Text = "";
                    txtTotFairAmt.Text = "";
                    txtTotLocalConv.Text = "";
                    txtTotLodgeAmt.Text = "";
                    txtTotNoOfKm.Text = "";
                    txtTotPhoneBillAmt.Text = "";
                    GenerateTransactionNo();                    
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServicedb = null;
                ht = null;
                
            }
        }


        private void FillEmpTourBillDetl(DataTable dtDetl)
        {
            gvEmpTourBillDetails.Rows.Clear();

            if (dtDetl.Rows.Count > 0)
            {
                for (int i = 0; i < dtDetl.Rows.Count; i++)
                {
                    gvEmpTourBillDetails.Rows.Add();
                    gvEmpTourBillDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["ActivityDate"].Value = Convert.ToDateTime(dtDetl.Rows[i]["TourDate"]).ToString("dd/MMM/yyyy");
                    gvEmpTourBillDetails.Rows[i].Cells["DeptTime"].Value = Convert.ToDouble(dtDetl.Rows[i]["DeptTime"].ToString()).ToString("00.00");
                    gvEmpTourBillDetails.Rows[i].Cells["fromLocation"].Value = dtDetl.Rows[i]["FromLocation"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["ToLocation"].Value = dtDetl.Rows[i]["ToLocation"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["NoOfKm"].Value = dtDetl.Rows[i]["NoOfKm"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["ModeOfJourney"].Value = dtDetl.Rows[i]["ModeOfJourney"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["FareAmt"].Value = dtDetl.Rows[i]["TotFareAmt"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["LodgeAmt"].Value = dtDetl.Rows[i]["LodgeAmt"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["DAAmt"].Value = dtDetl.Rows[i]["DaAmt"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["LocalConvAmt"].Value = dtDetl.Rows[i]["ConvAmt"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["PhoneBillAmt"].Value = dtDetl.Rows[i]["PhBillAmt"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["TotalAmt"].Value = dtDetl.Rows[i]["TotAmt"].ToString();
                    gvEmpTourBillDetails.Rows[i].Cells["Remarks"].Value = dtDetl.Rows[i]["Remarks"].ToString();                   
                }
            }
            CalculateTotAmt();
            CalculateTotDAAmt();
            CalculateTotFairAmt();
            CalculateTotKMtravelled();
            CalculateTotLocalConvAmt();
            CalculateTotLodgeAmt();
            CalculateTotPhoneBillAmt();
        }

       

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text.Length > 0)
            {
                GetEmpTourExpensesDetails();
            }
            else
            {

                flagUpdate = false;
                //cbCompany.SelectedIndex = 0;
                //cbBranch.SelectedIndex = -1;
                dtpTrnDate.Value = DateTime.Today;

                
                gvEmpTourBillDetails.Rows.Clear();

                cbEcode.SelectedIndex = -1;
                txtApprovedByEcode.Text = "";
                txtApprovedName.Text = "";
                txtAprAmt.Text = "";
                txtRecEmpName.Text = "";
                txtRecomAmt.Text = "";
                txtRecommendedEcode.Text = "";
                txtTotAmount.Text = "";
                txtTotDaAmt.Text = "";
                txtTotFairAmt.Text = "";
                txtTotLocalConv.Text = "";
                txtTotLodgeAmt.Text = "";
                txtTotNoOfKm.Text = "";
                txtTotPhoneBillAmt.Text = "";
                GenerateTransactionNo();
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";

            if (txtTrnNo.Text != "" && flagUpdate==true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                    "Tour Expenses", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        

                        strCommand = "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY WHERE STBDA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                       

                        strCommand += "DELETE FROM SERVICES_TOUR_BILLS_DETL WHERE STBD_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                       

                        strCommand += "DELETE FROM SERVICES_TOUR_BILLS_HEAD WHERE STBH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        iRes = objSQLdb.ExecuteSaveData(strCommand);
                                              

                        MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null,null);
                        
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {
                    MessageBox.Show(" Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

      
    }
}
