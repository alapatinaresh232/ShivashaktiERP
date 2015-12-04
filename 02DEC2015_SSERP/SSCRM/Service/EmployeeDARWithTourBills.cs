using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class EmployeeDARWithTourBills : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        private string strECode = "", sCompany = "", sBranch = "", sTrnNo = "", sRefNo = "", sActivityDate = "", sFinYear = "";
        private bool flagUpdate = false, flagText = true;
        double dFareAmt = 0, dLocalConvAmt = 0, dPhoneBillAmt = 0, dDaAmt = 0, dLodgeAmt = 0, dTotAmt = 0, dDaDays = 0;
        Int32 intCurrentRow = 0, intCurrentCell = 0;
        
        public EmployeeDARWithTourBills()
        {
            InitializeComponent();
        }

        private void EmployeeDARWithTourBills_Load(object sender, EventArgs e)
        {
            FillActivityTypes();
            FillCompanyData();         
            
            FillEmployeeData();
            EcodeSearch();
            dtpTrnDate.Value = DateTime.Today;
            cbCompany_SelectedIndexChanged(null,null);
            if (cbEcode.SelectedIndex != -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                if (strECode.Length > 2)
                {
                    FillEmployeeActivityDetails(Convert.ToInt32(strECode), Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy"));
                }
            }
        }

        private DataTable dtActivityType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("xx", "Select Activity Type");
            table.Rows.Add("SERVICE ACTIVITY", "SERVICE ACTIVITY");
            table.Rows.Add("FARMER MEETING", "FARMER MEETING");
            table.Rows.Add("DEMO PLOTS", "DEMO PLOTS");
            table.Rows.Add("PRODUCT PROMOTIONS", "PRODUCT PROMOTIONS");
            table.Rows.Add("SCHOOL VISIT ACTIVITY", "SCHOOL VISIT ACTIVITY");            
            table.Rows.Add("OTHER ACTIVITY", "OTHER ACTIVITY");
            return table;
        }

        private void FillActivityTypes()
        {
            try
            {
                cbActivityType.DataSource = dtActivityType();
                cbActivityType.DisplayMember = "name";
                cbActivityType.ValueMember = "type";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
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

            }
        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();           
            cbBranch.DataSource = null;
            string BranCode = "", strCommand = "";
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    strCommand = "SELECT DISTINCT BRANCH_NAME BranchName,BRANCH_CODE+'@'+ STATE_CODE AS BranCode " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "' and BRANCH_TYPE in ('BR','HO') ORDER BY BRANCH_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BranchName";
                    cbBranch.ValueMember = "BranCode";
                }

                BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranch.SelectedValue = BranCode;

                dt = null;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            DataTable dtEmp = new DataTable();
            if (cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                try
                {
                    string[] strBranCode = cbBranch.SelectedValue.ToString().Split('@');
                    Cursor.Current = Cursors.WaitCursor;
                    cbEcode.DataSource = null;
                    cbEcode.Items.Clear();
                    dtEmp = objServicedb.ServiceLevelEcodeSearch_Get(cbCompany.SelectedValue.ToString(), strBranCode[0], CommonData.DocMonth, txtEcodeSearch.Text.ToString()).Tables[0];
                   

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

        public void FillEmployeeActivityDetails(Int32 EmpEcode,string sActdate)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            SqlParameter[] param = new SqlParameter[2];
            DataTable dt = new DataTable();
            DataTable dtTourDetl = new DataTable();
            gvActivityDetails.Rows.Clear();
          
            if (Convert.ToString(EmpEcode).Length > 2)
            {
                try
                {
                    param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, EmpEcode, ParameterDirection.Input);
                    param[1] = objSQLdb.CreateParameter("@xActivityDate", DbType.String, sActdate, ParameterDirection.Input);

                    dt = objSQLdb.ExecuteDataSet("ServiceEmployeeDARDetl_Get", CommandType.StoredProcedure, param).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                       
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvActivityDetails.Rows.Add();
                            gvActivityDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvActivityDetails.Rows[i].Cells["CompanyCode"].Value = dt.Rows[i]["sr_company_Code"].ToString();
                            gvActivityDetails.Rows[i].Cells["BranchCode"].Value = dt.Rows[i]["sr_branch_Code"].ToString();
                            gvActivityDetails.Rows[i].Cells["Fin_Year"].Value = dt.Rows[i]["sr_fin_year"].ToString();
                            gvActivityDetails.Rows[i].Cells["Activity_Trn_No"].Value = dt.Rows[i]["sr_Trn_No"].ToString();
                            gvActivityDetails.Rows[i].Cells["Activity_Ref_No"].Value = dt.Rows[i]["sr_Ref_No"].ToString();
                            gvActivityDetails.Rows[i].Cells["ActivityType"].Value = dt.Rows[i]["sr_Activity_Type"].ToString();
                            gvActivityDetails.Rows[i].Cells["SubActivityName"].Value = dt.Rows[i]["sr_sub_Activity_Type"].ToString();
                            gvActivityDetails.Rows[i].Cells["CustomerName"].Value = dt.Rows[i]["sr_customer_name"].ToString();
                            gvActivityDetails.Rows[i].Cells["Cust_Address"].Value = dt.Rows[i]["sr_Address"].ToString();
                            gvActivityDetails.Rows[i].Cells["FromLocation"].Value = dt.Rows[i]["sr_from_location"].ToString();
                            gvActivityDetails.Rows[i].Cells["ToLocation"].Value = dt.Rows[i]["sr_to_location"].ToString();
                            gvActivityDetails.Rows[i].Cells["ModeOfJourney"].Value = dt.Rows[i]["sr_Mode_of_journey"].ToString();
                            gvActivityDetails.Rows[i].Cells["NoOfKm"].Value = dt.Rows[i]["sr_no_of_km"].ToString();
                            gvActivityDetails.Rows[i].Cells["LodgeAmt"].Value = dt.Rows[i]["sr_lodge_amt"].ToString();
                            gvActivityDetails.Rows[i].Cells["DaAmt"].Value = dt.Rows[i]["sr_da_amt"].ToString();
                            gvActivityDetails.Rows[i].Cells["FareAmt"].Value = dt.Rows[i]["sr_fare_amt"].ToString();
                            gvActivityDetails.Rows[i].Cells["LocalConvAmt"].Value = dt.Rows[i]["sr_local_conv_amt"].ToString();
                            gvActivityDetails.Rows[i].Cells["OtherAmt"].Value = dt.Rows[i]["sr_other_amt"].ToString();
                            gvActivityDetails.Rows[i].Cells["TotalAmt"].Value = dt.Rows[i]["sr_total_amt"].ToString();
                            gvActivityDetails.Rows[i].Cells["Remarks"].Value = dt.Rows[i]["sr_Activity_Remarks"].ToString();
                        }

                        txtRemarks.Text = dt.Rows[0]["sr_Remarks"].ToString();
                        CalculateTotals();                       
                       
                    }
                    else
                    {                       
                        gvActivityDetails.Rows.Clear();                                                                    
                        txtDaAmt.Text = "";
                        txtLocalConv.Text = "";
                        txtFareAmt.Text = "";
                        txtLodgeAmt.Text = "";
                        txtOtherAmt.Text = "";
                        txtTotAmount.Text = "";
                        txtNoOfKM.Text = "";
                        txtRemarks.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }               
            }
          
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
                if (cbBranch.SelectedIndex == -1)
                {
                    cbBranch.SelectedIndex = 0;
                }
                sCompany = cbCompany.SelectedValue.ToString();
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 0)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;
            }
            if (cbBranch.SelectedIndex == 0 || cbBranch.SelectedIndex==-1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranch.Focus();
                return flag;
            }
            if (cbEcode.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
                return flag;
            }

            if (dtpTrnDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpTrnDate.Focus();
                return flag;
            }

            if (cbActivityType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Activity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbActivityType.Focus();
                return flag;
            }

            return flag;
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            sActivityDate = "";
            if (CheckData() == true)
            {
                sCompany = cbCompany.SelectedValue.ToString();
                sBranch = cbBranch.SelectedValue.ToString();
                strECode = cbEcode.SelectedValue.ToString();
                sActivityDate = Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy");

                if (cbActivityType.SelectedIndex == 1)
                {
                    ActivityServiceUpdate ActSerUpdate = new ActivityServiceUpdate(sCompany,sBranch,strECode,sActivityDate);
                    ActSerUpdate.objEmployeeDARWithTourBills = this;
                    ActSerUpdate.Show();
                }
                else if (cbActivityType.SelectedIndex == 2)
                {
                    FarmerMeetingForm FarmerMeet = new FarmerMeetingForm(sCompany, sBranch, strECode, sActivityDate);
                    FarmerMeet.objEmployeeDARWithTourBills = this;
                    FarmerMeet.Show();
                }
                else if (cbActivityType.SelectedIndex == 3)
                {
                    frmDemoPlots DemoPlot = new frmDemoPlots(sCompany, sBranch, strECode, sActivityDate);
                    DemoPlot.objEmployeeDARWithTourBills = this;
                    DemoPlot.Show();
                }
                else if (cbActivityType.SelectedIndex == 4)
                {
                    frmProductPromotion ProdProm = new frmProductPromotion(sCompany, sBranch, strECode, sActivityDate);
                    ProdProm.objEmployeeDARWithTourBills = this;
                    ProdProm.Show();
                }
                else if (cbActivityType.SelectedIndex == 5)
                {
                    frmSchoolVisits SchoolVisit = new frmSchoolVisits(sCompany, sBranch, strECode, sActivityDate);
                    SchoolVisit.objEmployeeDARWithTourBills = this;
                    SchoolVisit.Show();
                }
                else if (cbActivityType.SelectedIndex == 6)
                {
                    ServiceActivities OtherActivity = new ServiceActivities(sCompany, sBranch, strECode, sActivityDate);
                    OtherActivity.objEmployeeDARWithTourBills = this;
                    OtherActivity.Show();
                }
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbBranch.SelectedIndex>0)
            sBranch = cbBranch.SelectedValue.ToString();
            EcodeSearch();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpTrnDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex != -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                if (strECode.Length > 2)
                {
                    FillEmployeeActivityDetails(Convert.ToInt32(strECode), Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy"));
                }
            }
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex != -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                if (strECode.Length > 2)
                {
                    FillEmployeeActivityDetails(Convert.ToInt32(strECode), Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy"));
                }
            }
        }

       
        private void CalculateTotalAmount()
        {
            double FairAmt, LodgeAmt, PhoneBillAmt, DaAmt, LocalConv, TotAmount;
            FairAmt = LodgeAmt = PhoneBillAmt = DaAmt = LocalConv = TotAmount = 0;

            if (txtFareAmt.Text.Length > 0)
            {
                FairAmt = Convert.ToDouble(txtFareAmt.Text);
            }
            if (txtLodgeAmt.Text.Length > 0)
            {
                LodgeAmt = Convert.ToDouble(txtLodgeAmt.Text);
            }

            if (txtDaAmt.Text.Length > 0)
            {
                DaAmt = Convert.ToDouble(txtDaAmt.Text);
            }

            if (txtLocalConv.Text.Length > 0)
            {
                LocalConv = Convert.ToDouble(txtLocalConv.Text);
            }

            TotAmount = Convert.ToDouble(FairAmt + LodgeAmt + PhoneBillAmt + DaAmt + LocalConv);

            txtTotAmount.Text = TotAmount.ToString("0");
           
        }

        private void gvActivityDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvActivityDetails.Columns["Edit"].Index)
                {
                    sActivityDate = Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy");
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                    sTrnNo = gvActivityDetails.Rows[e.RowIndex].Cells["Activity_Trn_No"].Value.ToString();
                    sRefNo = gvActivityDetails.Rows[e.RowIndex].Cells["Activity_Ref_No"].Value.ToString();
                    if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["ActivityType"].Value) == "SERVICE ACTIVITY")
                    {
                        sCompany = gvActivityDetails.Rows[e.RowIndex].Cells["CompanyCode"].Value.ToString();
                        sBranch = gvActivityDetails.Rows[e.RowIndex].Cells["BranchCode"].Value.ToString();
                        sFinYear = gvActivityDetails.Rows[e.RowIndex].Cells["Fin_Year"].Value.ToString();

                        ActivityServiceUpdate ActSerUpdate = new ActivityServiceUpdate(sCompany, sBranch, strECode,sFinYear,sTrnNo,sRefNo);
                        ActSerUpdate.objEmployeeDARWithTourBills = this;
                        ActSerUpdate.Show();
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["ActivityType"].Value) == "FARMER MEET")
                    {
                        FarmerMeetingForm FarmerMeet = new FarmerMeetingForm(sCompany, sBranch, strECode, sActivityDate, sTrnNo, sRefNo);
                        FarmerMeet.objEmployeeDARWithTourBills = this;
                        FarmerMeet.Show();                        
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["ActivityType"].Value) == "PRODUCT PROMOTION")
                    {
                        frmProductPromotion ProdProm = new frmProductPromotion(sCompany, sBranch, strECode, sActivityDate, sTrnNo, sRefNo);
                        ProdProm.objEmployeeDARWithTourBills = this;
                        ProdProm.Show();                      
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["ActivityType"].Value) == "OTHER ACTIVITY")
                    {
                        ServiceActivities OtherActivity = new ServiceActivities(sCompany, sBranch, strECode, sActivityDate, sTrnNo, sRefNo);
                        OtherActivity.objEmployeeDARWithTourBills = this;
                        OtherActivity.Show();                     
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["ActivityType"].Value) == "SCHOOL VISITS")
                    {
                        frmSchoolVisits SchoolVisit = new frmSchoolVisits(sCompany, sBranch, strECode, sActivityDate, sTrnNo, sRefNo);
                        SchoolVisit.objEmployeeDARWithTourBills = this;
                        SchoolVisit.Show();                       
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["ActivityType"].Value) == "DEMO PLOTS")
                    {
                        frmDemoPlots DemoPlot = new frmDemoPlots(sCompany, sBranch, strECode, sActivityDate, sTrnNo, sRefNo);
                        DemoPlot.objEmployeeDARWithTourBills = this;
                        DemoPlot.Show();                      
                    }
                }
            }
        }

        private void txtNoOfKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtLodgeAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtDaAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtFairAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtLocalConv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtPhoneBillAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtLodgeAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtDaAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtFairAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtLocalConv_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtPhoneBillAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strActivity = "", strCmd = "", strSubActivity = "", strSelect = "";
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            int iRes = 0;
            DialogResult result = MessageBox.Show("Do you want to delete Selected Record ?",
                                   "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {

                    if (gvActivityDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvActivityDetails.Rows.Count; i++)
                        {
                            if ((bool)gvActivityDetails.Rows[i].Cells["Select"].EditedFormattedValue == true)
                            {
                                strActivity = gvActivityDetails.Rows[i].Cells["ActivityType"].Value.ToString();
                                strSubActivity = gvActivityDetails.Rows[i].Cells["SubActivityName"].Value.ToString();
                                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                                sTrnNo = gvActivityDetails.Rows[i].Cells["Activity_Trn_No"].Value.ToString();
                                sRefNo = gvActivityDetails.Rows[i].Cells["Activity_Ref_No"].Value.ToString();

                                if (strActivity == "SERVICE ACTIVITY")
                                {
                                    sCompany = gvActivityDetails.Rows[i].Cells["CompanyCode"].Value.ToString();
                                    sBranch = gvActivityDetails.Rows[i].Cells["BranchCode"].Value.ToString();
                                    sFinYear = gvActivityDetails.Rows[i].Cells["Fin_Year"].Value.ToString();

                                    strCmd += "exec ServicesTNAUpdateStmtFor_DAR '" + sCompany + "','" + sBranch + "','" + sFinYear +
                                                "','" + sTrnNo + "'," + Convert.ToInt32(sRefNo) + "";
                                    strCmd += "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY "+
                                             " WHERE STBDA_ECODE="+ strECode +
                                             " and STBDA_TOUR_DATE='"+ Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                             "' and STBDA_ACTIVITY='"+ strActivity +
                                             "' and STBDA_SUB_ACTIVITY='"+ strSubActivity +
                                             "' and STBDA_AGNST_TRN_NUMBER='"+ sTrnNo +
                                             "' and STBDA_ACTIVITY_CODE='"+ sRefNo +"'";
                                }

                                if (strActivity == "FARMER MEET")
                                {
                                    strSelect = "SELECT SFMH_TRN_NUMBER TrnNo,SFMH_COND_BY_ECODE EmpCode,SFMH_CREATED_BY CreatedBy " +
                                                " FROM SERVICES_FARMER_MEET_HEAD WHERE SFMH_TRN_NUMBER='" + sTrnNo + "'";
                                    dt = objSQLdb.ExecuteDataSet(strSelect).Tables[0];

                                    if (dt.Rows.Count > 0)
                                    {
                                        if (dt.Rows[0]["EmpCode"].ToString().Equals(strECode) || dt.Rows[0]["CreatedBy"].ToString() == strECode)
                                        {
                                            strCmd += "DELETE FROM SERVICES_FARMER_MEET_PRODUCTS WHERE SFMP_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += " DELETE FROM SERVICES_FARMER_MEET_ATTENDENTS WHERE SFMA_TRN_NUMBER='" + sTrnNo + "'";
                                            strCmd += " DELETE FROM SERVICES_FARMER_MEET_IMAGES WHERE SFMI_TRN_NUMBER= '" + sTrnNo + "'";
                                            strCmd += " DELETE FROM SERVICES_FARMER_MEET_HEAD WHERE SFMH_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                                             " WHERE STBDA_ECODE=" + strECode +
                                             " and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                             "' and STBDA_ACTIVITY='" + strActivity +
                                             "' and STBDA_SUB_ACTIVITY='" + strSubActivity +
                                             "' and STBDA_AGNST_TRN_NUMBER='" + sTrnNo +
                                             "' and STBDA_ACTIVITY_CODE='" + sRefNo + "'";
                                        }
                                    }
                                    strSelect = "";
                                }
                                if (strActivity == "PRODUCT PROMOTION")
                                {
                                    strSelect = "SELECT  SPPH_COND_BY_ECODE EmpCode,SPPH_CREATED_BY CreatedBy " +
                                                " from SERVICES_PRODUCT_PROMOTION_HEAD WHERE SPPH_TRN_NUMBER='" + sTrnNo + "'";
                                    dt = objSQLdb.ExecuteDataSet(strSelect).Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (dt.Rows[0]["EmpCode"].ToString() == strECode || dt.Rows[0]["CreatedBy"].ToString() == strECode)
                                        {
                                            strCmd += "DELETE FROM SERVICES_PRODUCT_PROMOTION_ATTENDENTS WHERE SPPA_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += " DELETE FROM SERVICES_PRODUCT_PROMOTION_ITEMS WHERE SPPI_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += " DELETE FROM SERVICES_PRODUCT_PROMOTION_HEAD WHERE SPPH_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                                                       " WHERE STBDA_ECODE=" + strECode +
                                                        " and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                        "' and STBDA_ACTIVITY='" + strActivity +
                                                        "' and STBDA_SUB_ACTIVITY='" + strSubActivity +
                                                        "' and STBDA_AGNST_TRN_NUMBER='" + sTrnNo +
                                                        "' and STBDA_ACTIVITY_CODE='" + sRefNo + "'";

                                        }
                                    }
                                    strSelect = "";
                                }
                                if (strActivity == "OTHER ACTIVITY")
                                {
                                    strCmd += "DELETE FROM SERVICES_DAR_DETL WHERE SDD_TRN_NUMBER='" + sTrnNo +
                                               "'and SDD_DAILY_ACTIVITY='" + strSubActivity + "' ";

                                    strCmd += "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                                             " WHERE STBDA_ECODE=" + strECode +
                                             " and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                             "' and STBDA_ACTIVITY='" + strActivity +
                                             "' and STBDA_SUB_ACTIVITY='" + strSubActivity +
                                             "' and STBDA_AGNST_TRN_NUMBER='" + sTrnNo +
                                             "' and STBDA_ACTIVITY_CODE='" + sRefNo + "'";

                                }
                                if (strActivity == "SCHOOL VISITS")
                                {
                                    strSelect = "SELECT SSVH_TRN_NUMBER TrnNo,SSVH_COND_BY_ECODE EmpCode " +
                                                ",SSVH_CREATED_BY CreatedBy from SERVICES_SCHOOL_VISIT_HEAD " +
                                                " WHERE SSVH_TRN_NUMBER='" + sTrnNo + "'";

                                    dt = objSQLdb.ExecuteDataSet(strSelect).Tables[0];

                                    if (dt.Rows[0]["EmpCode"].ToString() == strECode || dt.Rows[0]["CreatedBy"].ToString() == strECode)
                                    {
                                        strCmd += "DELETE FROM SERVICES_SCHOOL_VISIT_ATTENDENTS WHERE SSVA_TRN_NUMBER='" + sTrnNo + "'";


                                        strCmd += " DELETE FROM SERVICES_SCHOOL_VISIT_GIFTS WHERE SSVG_TRN_NUMBER='" + sTrnNo + "'";

                                        strCmd += " DELETE FROM SERVICES_SCHOOL_VISIT_PRODUCTS WHERE SSVP_TRN_NUMBER='" + sTrnNo + "'";

                                        strCmd += " DELETE FROM SERVICES_SCHOOL_VISIT_HEAD WHERE SSVH_TRN_NUMBER='" + sTrnNo + "'";

                                        strCmd += "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                                             " WHERE STBDA_ECODE=" + strECode +
                                             " and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                             "' and STBDA_ACTIVITY='" + strActivity +
                                             "' and STBDA_SUB_ACTIVITY='" + strSubActivity +
                                             "' and STBDA_AGNST_TRN_NUMBER='" + sTrnNo +
                                             "' and STBDA_ACTIVITY_CODE='" + sRefNo + "'";
                                    }
                                }
                                if (strActivity == "DEMO PLOTS" && strSubActivity == "DEMO PLOTS")
                                {
                                    strSelect = "SELECT SDPH_COND_BY_ECODE EmpCode,SDPH_CREATED_BY CreatedBy " +
                                                ",isnull(SDPH_STATUS,'WORKING') Status FROM SERVICES_DEMO_PLOTS_HEAD " +
                                                " WHERE SDPH_TRN_NUMBER='" + sTrnNo + "'";
                                    dt = objSQLdb.ExecuteDataSet(strSelect).Tables[0];
                                    if (dt.Rows[0]["EmpCode"].ToString() == strECode || dt.Rows[0]["CreatedBy"].ToString() == strECode && dt.Rows[0]["Status"].ToString() == "WORKING")
                                    {
                                        strSelect = "SELECT SDPR_TRN_NUMBER,SDPR_TRN_TYPE from SERVICES_DEMO_PLOTS_RESULT " +
                                                    " WHERE SDPR_TRN_NUMBER='" + sTrnNo + "'";
                                        dt = objSQLdb.ExecuteDataSet(strSelect).Tables[0];
                                        if (dt.Rows.Count == 1)
                                        {                                            
                                            strCmd += " DELETE FROM SERVICES_DEMO_PLOTS_RESULT WHERE SDPR_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += " DELETE FROM SERVICES_DEMO_PLOTS_ATTENDENTS WHERE SDPA_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += " DELETE FROM SERVICES_DEMO_PLOTS_PRODUCTS WHERE SDPP_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += " DELETE FROM SERVICES_DEMO_PLOTS_HEAD WHERE SDPH_TRN_NUMBER='" + sTrnNo + "'";

                                            strCmd += "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                                                       " WHERE STBDA_ECODE=" + strECode +
                                                       " and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                       "' and STBDA_ACTIVITY='" + strActivity +
                                                       "' and STBDA_SUB_ACTIVITY='" + strSubActivity +
                                                       "' and STBDA_AGNST_TRN_NUMBER='" + sTrnNo +
                                                       "' and STBDA_ACTIVITY_CODE='" + sRefNo + "'";
                                        }
                                    }
                                }
                                if (strActivity == "DEMO PLOTS" && strSubActivity == "DEMO PLOT OBSERVATION")
                                {
                                    strCmd += " DELETE FROM SERVICES_DEMO_PLOTS_RESULT " +
                                              " WHERE SDPR_TRN_NUMBER='" + sTrnNo +
                                              "'and SDPR_AO_ECODE=" + strECode +
                                              " and SDPR_OBSERV_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") + "'";

                                    strCmd += "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                                                       " WHERE STBDA_ECODE=" + strECode +
                                                       " and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                       "' and STBDA_ACTIVITY='" + strActivity +
                                                       "' and STBDA_SUB_ACTIVITY='" + strSubActivity +
                                                       "' and STBDA_AGNST_TRN_NUMBER='" + sTrnNo +
                                                       "' and STBDA_ACTIVITY_CODE='" + sRefNo + "'";
                                }

                                if (strCmd.Length > 10)
                                {
                                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                                    if (iRes > 0)
                                    {
                                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        FillEmployeeActivityDetails(Convert.ToInt32(strECode), Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy"));
                                        if (gvActivityDetails.Rows.Count == 0)
                                        {
                                            strCmd = "";
                                            objSQLdb = new SQLDB();
                                            strCmd = "DELETE FROM SERVICES_TOUR_BILLS_DETL_ACTIVITY " +
                                                       " WHERE STBDA_ECODE=" + strECode +
                                                       " and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") + "'";                                                      
                                            
                                            strCmd += " DELETE FROM SERVICES_TOUR_BILLS_HEAD " +
                                                      " WHERE STBH_ECODE=" + strECode +
                                                     " and STBH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") + "'";
                                            
                                            iRes = objSQLdb.ExecuteSaveData(strCmd);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
       

        private bool CheckDetails()
        {
            bool bFlag = true;

            if (cbEcode.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Employee", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
                return bFlag;
                
            }
            if (gvActivityDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Atleast One Activity","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbActivityType.Focus();
                return bFlag;
                                
            }
            //if (gvActivityDetails.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gvActivityDetails.Rows.Count; i++)
            //    {
            //        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["FareAmt"].Value) == "")
            //        {
            //            gvActivityDetails.Rows[i].Cells["FareAmt"].Value = "0";
            //        }
            //        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["DaAmt"].Value) == "")
            //        {
            //            gvActivityDetails.Rows[i].Cells["DaAmt"].Value = "0";
            //        }
            //        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["LocalConvAmt"].Value) == "")
            //        {
            //            gvActivityDetails.Rows[i].Cells["LocalConvAmt"].Value = "0";
            //        }
            //        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["LodgeAmt"].Value) == "")
            //        {
            //            gvActivityDetails.Rows[i].Cells["LodgeAmt"].Value = "0";
            //        }
            //        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["TotalAmt"].Value) == "")
            //        {
            //            gvActivityDetails.Rows[i].Cells["TotalAmt"].Value = "0";
            //        }
            //        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["NoOfKm"].Value) == "")
            //        {
            //            gvActivityDetails.Rows[i].Cells["NoOfKm"].Value = "0";
            //        }
            //        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["OtherAmt"].Value) == "")
            //        {
            //            gvActivityDetails.Rows[i].Cells["OtherAmt"].Value = "0";
            //        }
            //    }
            //}
          
            return bFlag;
            
        }
    

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            if (CheckDetails() == true)
            {
                if (SaveTourBillHeadDetails() > 0)
                {
                    if (SaveTourBillActiviytDetl() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gvActivityDetails.Rows.Clear();                                      
                       
                        txtDaAmt.Text = "";
                        txtLocalConv.Text = "";
                        txtFareAmt.Text = "";
                        txtLodgeAmt.Text = "";
                        txtTotAmount.Text = "";
                        txtNoOfKM.Text = "";
                        txtRemarks.Text = "";
                        flagUpdate = false;
                        dtpTrnDate.Value = DateTime.Today;
                        FillEmployeeActivityDetails(Convert.ToInt32(strECode),Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy"));
                    }
                    else
                    {
                        string strCmd = "DELETE FROM SERVICES_TOUR_BILLS_HEAD WHERE STBH_ECODE=" + strECode +
                                        " and STBH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") + "'";
                        int iRes = objSQLdb.ExecuteSaveData(strCmd);
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
                              " WHERE STBDA_ECODE='" + cbEcode.SelectedValue +
                              "' and STBDA_TOUR_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") + "'";

                if (txtNoOfKM.Text.Length == 0)
                    txtNoOfKM.Text = "0";
                if (txtLocalConv.Text.Length == 0)
                    txtLocalConv.Text = "0";
                if (txtLodgeAmt.Text.Length == 0)
                    txtLodgeAmt.Text = "0";
                if (txtOtherAmt.Text.Length == 0)
                    txtOtherAmt.Text = "0";
                if (txtFareAmt.Text.Length == 0)
                    txtFareAmt.Text = "0";
                if (txtDaAmt.Text.Length == 0)
                    txtDaAmt.Text = "0";
                if (txtTotAmount.Text.Length == 0)
                    txtTotAmount.Text = "0";

                string strCmd = "SELECT * FROM SERVICES_TOUR_BILLS_HEAD WHERE STBH_ECODE='" + cbEcode.SelectedValue +
                                "' and STBH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") + "'";
                DataTable dt = new DataTable();
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count>0)
                {
                    strCommand += " UPDATE SERVICES_TOUR_BILLS_HEAD SET STBH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                "',STBH_DOC_MONTH='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                "',STBH_ECODE=" + cbEcode.SelectedValue +                               
                                ",STBH_TOUR_BILL_AMOUNT=" + txtTotAmount.Text +
                                ",STBH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                "',STBH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "',STBH_LAST_MODIFIED_DATE=getdate(),STBH_TOT_DA_AMT="+ Convert.ToDouble(txtDaAmt.Text) +
                                ",STBH_TOT_FARE_AMT="+ Convert.ToDouble(txtFareAmt.Text) +
                                ",STBH_TOT_LODGE_AMT="+ Convert.ToDouble(txtLodgeAmt.Text) +
                                ",STBH_TOT_LOCAL_CONV_AMT="+ Convert.ToDouble(txtLocalConv.Text) +
                                ",STBH_TOT_OTHER_AMT=" + Convert.ToDouble(txtOtherAmt.Text) +
                                ",STBH_NO_OF_KM="+ Convert.ToDouble(txtNoOfKM.Text) +
                                ",STBH_REMARKS='"+ txtRemarks.Text.ToString().Replace("'","") +
                                "' WHERE STBH_ECODE=" + cbEcode.SelectedValue + 
                                " AND STBH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") + "'";

                }
                else
                {                 

                    strCommand = "INSERT INTO SERVICES_TOUR_BILLS_HEAD(STBH_COMPANY_CODE " +
                                                                    ", STBH_STATE_CODE " +
                                                                    ", STBH_BRANCH_CODE " +
                                                                    ", STBH_DOC_MONTH " +
                                                                    ", STBH_TRN_TYPE " +                                                                   
                                                                    ", STBH_TRN_DATE " +
                                                                    ", STBH_ECODE " +
                                                                    ", STBH_CREATED_BY " +
                                                                    ", STBH_CREATED_DATE " +                                                                   
                                                                    ", STBH_TOUR_BILL_AMOUNT " +
                                                                    ", STBH_TOT_DA_AMT "+
                                                                    ", STBH_TOT_FARE_AMT "+
                                                                    ", STBH_TOT_LODGE_AMT "+
                                                                    ", STBH_TOT_LOCAL_CONV_AMT "+
                                                                    ", STBH_TOT_OTHER_AMT " +
                                                                    ", STBH_NO_OF_KM "+
                                                                    ", STBH_REMARKS "+
                                                                    ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                    "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                                                                    "','" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                                                    "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                    "','SERVICETOURBILL' "+
                                                                    ",'" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                    "'," + Convert.ToInt32(cbEcode.SelectedValue) +
                                                                    ",'" + CommonData.LogUserId +
                                                                    "',getdate() "+
                                                                    "," + Convert.ToDouble(txtTotAmount.Text).ToString("0.00") +
                                                                    ","+ Convert.ToDouble(txtDaAmt.Text).ToString("0.00") +
                                                                    ","+ Convert.ToDouble(txtFareAmt.Text).ToString("0.00") +
                                                                    ","+ Convert.ToDouble(txtLodgeAmt.Text).ToString("0.00") +
                                                                    ","+ Convert.ToDouble(txtLocalConv.Text).ToString("0.00") +
                                                                    ","+ Convert.ToDouble(txtOtherAmt.Text).ToString("0.00") +
                                                                    ","+ Convert.ToDouble(txtNoOfKM.Text).ToString("0.00") +
                                                                    ",'"+ txtRemarks.Text.ToString().Replace("'","") +"')";

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
              

        private int SaveTourBillActiviytDetl()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {                              
                if (gvActivityDetails.Rows.Count > 0)
                {                   

                    for (int i = 0; i < gvActivityDetails.Rows.Count; i++)
                    {
                        CalculateActivityWiseAmounts();

                        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["FareAmt"].Value) == "")                        
                            gvActivityDetails.Rows[i].Cells["FareAmt"].Value = "0";
                        
                        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["DaAmt"].Value) == "")                        
                            gvActivityDetails.Rows[i].Cells["DaAmt"].Value = "0";
                        
                        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["LocalConvAmt"].Value) == "")                        
                            gvActivityDetails.Rows[i].Cells["LocalConvAmt"].Value = "0";
                        
                        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["LodgeAmt"].Value) == "")                        
                            gvActivityDetails.Rows[i].Cells["LodgeAmt"].Value = "0";
                        
                        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["TotalAmt"].Value) == "")                        
                            gvActivityDetails.Rows[i].Cells["TotalAmt"].Value = "0";
                        
                        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["NoOfKm"].Value) == "")                        
                            gvActivityDetails.Rows[i].Cells["NoOfKm"].Value = "0";
                        
                        if (Convert.ToString(gvActivityDetails.Rows[i].Cells["OtherAmt"].Value) == "")                        
                            gvActivityDetails.Rows[i].Cells["OtherAmt"].Value = "0";                        

                        if (gvActivityDetails.Rows[i].Cells["FromLocation"].Value == null)
                            gvActivityDetails.Rows[i].Cells["FromLocation"].Value = "";
                        if (gvActivityDetails.Rows[i].Cells["ToLocation"].Value == null)
                            gvActivityDetails.Rows[i].Cells["ToLocation"].Value = "";
                        if (gvActivityDetails.Rows[i].Cells["ModeOfJourney"].Value == null)
                            gvActivityDetails.Rows[i].Cells["ModeOfJourney"].Value = "";
                        if (gvActivityDetails.Rows[i].Cells["Remarks"].Value == null)
                            gvActivityDetails.Rows[i].Cells["Remarks"].Value = "";

                        strCommand += "INSERT INTO SERVICES_TOUR_BILLS_DETL_ACTIVITY(STBDA_COMPANY_CODE " +
                                                                                 ", STBDA_BRANCH_CODE " +
                                                                                 ", STBDA_STATE_CODE " +                                                                                
                                                                                 ", STBDA_TRN_TYPE " +
                                                                                 ", STBDA_DOC_MONTH " +
                                                                                 ", STBDA_ECODE " +
                                                                                 ", STBDA_TOUR_DATE " +                                                                                 
                                                                                 ", STBDA_SL_NO " +                                                                               
                                                                                 ", STBDA_FROM_LOCATION " +
                                                                                 ", STBDA_TO_LOCATION " +                                                                                 
                                                                                 ", STBDA_MODE_OF_JOURNEY " +
                                                                                 ", STBDA_FARE " +
                                                                                 ", STBDA_DA " +
                                                                                 ", STBDA_LODGE " +
                                                                                 ", STBDA_OTHER_AMT " +
                                                                                 ", STBDA_CONV " +
                                                                                 ", STBDA_TOTAL " +
                                                                                 ", STBDA_AGNST_TRN_TYPE " +
                                                                                 ", STBDA_AGNST_TRN_NUMBER " +                                                                                 
                                                                                 ", STBDA_ACTIVITY "+
                                                                                 ", STBDA_SUB_ACTIVITY "+
                                                                                 ", STBDA_ACTIVITY_CODE "+
                                                                                 ", STBDA_DA_DAYS "+
                                                                                 ", STBDA_NO_OF_KM "+
                                                                                 ", STBDA_REMARKS "+
                                                                                 ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                                 "','" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                                                                 "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +                                                                                
                                                                                 "','SERVICETOURBILL','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                                 "',"+ Convert.ToInt32(cbEcode.SelectedValue) +
                                                                                 ",'" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +                                                                                 
                                                                                 "'," + Convert.ToInt32(gvActivityDetails.Rows[i].Cells["SLNO"].Value) +
                                                                                 ",'" + gvActivityDetails.Rows[i].Cells["FromLocation"].Value.ToString().Replace("'","").ToUpper() +
                                                                                 "','" + gvActivityDetails.Rows[i].Cells["ToLocation"].Value.ToString().Replace("'", "").ToUpper() +
                                                                                 "','" + gvActivityDetails.Rows[i].Cells["ModeOfJourney"].Value.ToString().Replace("'", "").ToUpper() +
                                                                                 "'," + Convert.ToDouble(gvActivityDetails.Rows[i].Cells["FareAmt"].Value).ToString("0.00") +
                                                                                 "," + Convert.ToDouble(gvActivityDetails.Rows[i].Cells["DaAmt"].Value).ToString("0.00") +
                                                                                 "," + Convert.ToDouble(gvActivityDetails.Rows[i].Cells["LodgeAmt"].Value).ToString("0.00") +
                                                                                 "," + Convert.ToDouble(gvActivityDetails.Rows[i].Cells["OtherAmt"].Value).ToString("0.00") +
                                                                                 "," + Convert.ToDouble(gvActivityDetails.Rows[i].Cells["LocalConvAmt"].Value).ToString("0.00") +
                                                                                 "," + Convert.ToDouble(gvActivityDetails.Rows[i].Cells["TotalAmt"].Value).ToString("0.00") +
                                                                                 ",'" + gvActivityDetails.Rows[i].Cells["SubActivityName"].Value.ToString() +
                                                                                 "','" + gvActivityDetails.Rows[i].Cells["Activity_Trn_No"].Value.ToString() +
                                                                                 "','" + gvActivityDetails.Rows[i].Cells["ActivityType"].Value.ToString() +
                                                                                 "','" + gvActivityDetails.Rows[i].Cells["SubActivityName"].Value.ToString() +
                                                                                 "','" + gvActivityDetails.Rows[i].Cells["Activity_Ref_No"].Value.ToString() +
                                                                                 "',"+ dDaDays.ToString("0.00") +
                                                                                 ","+ gvActivityDetails.Rows[i].Cells["NoOfKm"].Value +
                                                                                 ",'"+ gvActivityDetails.Rows[i].Cells["Remarks"].Value.ToString().Replace("'","") +"')";
                    }
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

        private void CalculateActivityWiseAmounts()
        {
            Int32 GridRowCount = 0 ;
            dFareAmt = 0; dLocalConvAmt = 0; dPhoneBillAmt = 0; dDaAmt = 0; dLodgeAmt = 0; dTotAmt = 0; dDaDays = 0;
            double Days = 1;
            GridRowCount = gvActivityDetails.Rows.Count;

            if (txtLocalConv.Text.Length > 0)
            {
                dLocalConvAmt = Convert.ToDouble(txtLocalConv.Text);
                if (dLocalConvAmt != 0)
                    dLocalConvAmt = dLocalConvAmt / GridRowCount;
            }
            if (txtDaAmt.Text.Length > 0)
            {
                dDaAmt = Convert.ToDouble(txtDaAmt.Text);
                if (dDaAmt != 0)
                    dDaAmt = dDaAmt / GridRowCount;
            }
            if (txtLodgeAmt.Text.Length > 0)
            {
                dLodgeAmt = Convert.ToDouble(txtLodgeAmt.Text);
                if (dLodgeAmt != 0)
                    dLodgeAmt = dLodgeAmt / GridRowCount;
            }
           
            if (txtFareAmt.Text.Length > 0)
            {
                dFareAmt = Convert.ToDouble(txtFareAmt.Text);
                if (dFareAmt != 0)
                    dFareAmt = dFareAmt / GridRowCount;
            }
            if (txtTotAmount.Text.Length > 0)
            {
                dTotAmt = Convert.ToDouble(txtTotAmount.Text);
                if (dTotAmt != 0)
                    dTotAmt = dTotAmt / GridRowCount;
            }

            dDaDays = Days / GridRowCount;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvActivityDetails.Rows.Clear();              
           
            txtDaAmt.Text = "";
            txtLocalConv.Text = "";
            txtFareAmt.Text = "";
            txtLodgeAmt.Text = "";
            txtTotAmount.Text = "";
            txtNoOfKM.Text = "";
            txtRemarks.Text = "";
            flagUpdate = false;
            dtpTrnDate.Value = DateTime.Today;
            if (strECode.Length > 3)
                FillEmployeeActivityDetails(Convert.ToInt32(strECode), Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy"));
        }

        private void gvActivityDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double TotalAmount = 0;

            if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["DaAmt"].Value) != "")
            {
                if (Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["DaAmt"].Value) >= 0)
                {
                    TotalAmount += Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["DaAmt"].Value);
                    gvActivityDetails.Rows[e.RowIndex].Cells["TotalAmt"].Value = TotalAmount;

                }
            }
            if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["LocalConvAmt"].Value) != "")
            {
                if (Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["LocalConvAmt"].Value) >= 0)
                {
                    TotalAmount += Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["LocalConvAmt"].Value);
                    gvActivityDetails.Rows[e.RowIndex].Cells["TotalAmt"].Value = TotalAmount;
                }
            }
            if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["LodgeAmt"].Value) != "")
            {
                if (Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["LodgeAmt"].Value) >= 0)
                {
                    TotalAmount += Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["LodgeAmt"].Value);
                    gvActivityDetails.Rows[e.RowIndex].Cells["TotalAmt"].Value = TotalAmount;
                }
            }
            if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["OtherAmt"].Value) != "")
            {
                if (Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["OtherAmt"].Value) >= 0)
                {
                    TotalAmount += Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["OtherAmt"].Value);
                    gvActivityDetails.Rows[e.RowIndex].Cells["TotalAmt"].Value = TotalAmount;
                }
            }
            if (Convert.ToString(gvActivityDetails.Rows[e.RowIndex].Cells["FareAmt"].Value) != "")
            {
                if (Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["FareAmt"].Value) >= 0)
                {
                    TotalAmount += Convert.ToDouble(gvActivityDetails.Rows[e.RowIndex].Cells["FareAmt"].Value);
                    gvActivityDetails.Rows[e.RowIndex].Cells["TotalAmt"].Value = TotalAmount;
                }
            }           

            CalculateTotals();
        }

        private void CalculateTotals()
        {
            double dTotAmount = 0, dFareAmt = 0, dDaAmt = 0, dLocalConvAmt = 0, dLodgeAmt = 0, dOtherAmt = 0, dNoOfKM = 0;

            if (gvActivityDetails.Rows.Count > 0)
            {
                for (int nRow = 0; nRow < gvActivityDetails.Rows.Count; nRow++)
                {

                    if (Convert.ToString(gvActivityDetails.Rows[nRow].Cells["TotalAmt"].Value) != "")
                    {
                        dTotAmount += Convert.ToDouble(gvActivityDetails.Rows[nRow].Cells["TotalAmt"].Value.ToString());
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[nRow].Cells["LocalConvAmt"].Value) != "")
                    {
                        dLocalConvAmt += Convert.ToDouble(gvActivityDetails.Rows[nRow].Cells["LocalConvAmt"].Value.ToString());
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[nRow].Cells["FareAmt"].Value) != "")
                    {
                        dFareAmt += Convert.ToDouble(gvActivityDetails.Rows[nRow].Cells["FareAmt"].Value.ToString());
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[nRow].Cells["LodgeAmt"].Value) != "")
                    {
                        dLodgeAmt += Convert.ToDouble(gvActivityDetails.Rows[nRow].Cells["LodgeAmt"].Value.ToString());
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[nRow].Cells["DaAmt"].Value) != "")
                    {
                        dDaAmt += Convert.ToDouble(gvActivityDetails.Rows[nRow].Cells["DaAmt"].Value.ToString());
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[nRow].Cells["OtherAmt"].Value) != "")
                    {
                        dOtherAmt += Convert.ToDouble(gvActivityDetails.Rows[nRow].Cells["OtherAmt"].Value.ToString());
                    }
                    if (Convert.ToString(gvActivityDetails.Rows[nRow].Cells["NoOfKm"].Value) != "")
                    {
                        dNoOfKM += Convert.ToDouble(gvActivityDetails.Rows[nRow].Cells["NoOfKm"].Value.ToString());
                    }                    
                }
            }

            txtTotAmount.Text = dTotAmount.ToString("f");
            txtFareAmt.Text = dFareAmt.ToString("f");
            txtLodgeAmt.Text = dLodgeAmt.ToString("f");
            txtDaAmt.Text = dDaAmt.ToString("f");
            txtOtherAmt.Text = dOtherAmt.ToString("f");
            txtLocalConv.Text = dLocalConvAmt.ToString("f");
            txtNoOfKM.Text = dNoOfKM.ToString("f");
            
        }

        
       

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) && (flagText == false))
            {
                e.Handled = true;
                return;
            }
                         
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && flagText == false)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void gvActivityDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            flagText = true;

            if (this.gvActivityDetails.CurrentCell.ColumnIndex == gvActivityDetails.Columns["NoOfKm"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }           
            }
            if (this.gvActivityDetails.CurrentCell.ColumnIndex == gvActivityDetails.Columns["FareAmt"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if (this.gvActivityDetails.CurrentCell.ColumnIndex == gvActivityDetails.Columns["DaAmt"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if (this.gvActivityDetails.CurrentCell.ColumnIndex == gvActivityDetails.Columns["LodgeAmt"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if (this.gvActivityDetails.CurrentCell.ColumnIndex == gvActivityDetails.Columns["OtherAmt"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

            if (this.gvActivityDetails.CurrentCell.ColumnIndex == gvActivityDetails.Columns["LocalConvAmt"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

           
           
        }

    }
}
