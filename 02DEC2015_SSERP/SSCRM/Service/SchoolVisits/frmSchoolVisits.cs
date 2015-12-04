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
    public partial class frmSchoolVisits : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        bool flagUpdate = false;

        private string strECode = "", sCompCode = "", sBranCode = "", sEcode = "", sActivityDate = "", strTrnNo = "", strRefNo = "";
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills = null;
      
        public DataTable dtProdDemoDetails = new DataTable();
        public DataTable dtAttEmpDetails = new DataTable();
        public DataTable dtStudentDetails = new DataTable();
        public DataTable dtSchoolStaffDetl = new DataTable();
        public DataTable dtGiftDetails = new DataTable();

        public frmSchoolVisits()
        {
            InitializeComponent();
        }
        public frmSchoolVisits(string CompCode, string BranCode, string sEmpEcode, string sActDate)
        {
            InitializeComponent();
            sCompCode = CompCode;
            sBranCode = BranCode;
            sEcode = sEmpEcode;
            sActivityDate = sActDate;
        }
        public frmSchoolVisits(string CompCode, string BranCode, string sEmpEcode, string sActDate, string sTrnNo, string sRefNo)
        {
            InitializeComponent();
            sCompCode = CompCode;
            sBranCode = BranCode;
            sEcode = sEmpEcode;
            sActivityDate = sActDate;
            strTrnNo = sTrnNo;
            strRefNo = sRefNo;
        }

        private void frmSchoolVisits_Load(object sender, EventArgs e)
        {
            gvDemoDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            gvAttendedEmpDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            gvStaffDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            gvStudentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            gvGiftDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);

           
            dtpTrnDate.Value = DateTime.Today;

            grpr1.Visible = true;
            grpr2.Visible = false;
            
            FillCompanyData();
            FillBranchData();
            if (CommonData.BranchType == "BR")
            {
                GenerateTransactionNo();
            }
            #region "CREATE PRODUCT_DEMO_DETAILS TABLE"
            dtProdDemoDetails.Columns.Add("SLNO_Product");
            dtProdDemoDetails.Columns.Add("ProductID");
            dtProdDemoDetails.Columns.Add("prodName");
            dtProdDemoDetails.Columns.Add("Category");
            dtProdDemoDetails.Columns.Add("StudentsCnt");
            dtProdDemoDetails.Columns.Add("DemosCnt");
            dtProdDemoDetails.Columns.Add("Remarks_Demo");
            #endregion

            #region "CREATE EMP_DETAILS TABLE"
            dtAttEmpDetails.Columns.Add("SlNo_Emp");
            dtAttEmpDetails.Columns.Add("Ecode");
            dtAttEmpDetails.Columns.Add("EmpName");
            dtAttEmpDetails.Columns.Add("Desig");
            dtAttEmpDetails.Columns.Add("Dept");
            #endregion

            #region "CREATE STUDENTS TABLE"
            dtStudentDetails.Columns.Add("SLNo_Student");
            dtStudentDetails.Columns.Add("StudentName");
            dtStudentDetails.Columns.Add("StudentRel");
            dtStudentDetails.Columns.Add("StudentRelName");
            dtStudentDetails.Columns.Add("StudentHNo");
            dtStudentDetails.Columns.Add("StudentLandMark");
            dtStudentDetails.Columns.Add("StudentVillage");
            dtStudentDetails.Columns.Add("StudentMandal");
            dtStudentDetails.Columns.Add("StudentDistrict");
            dtStudentDetails.Columns.Add("StudentState");
            dtStudentDetails.Columns.Add("StudentPin");
            dtStudentDetails.Columns.Add("StudentMobileNo");
            dtStudentDetails.Columns.Add("StudentRemarks");
            #endregion

            #region "CREATE GIFT_DETAILS TABLE"
            dtGiftDetails.Columns.Add("SLNo_Gift");
            dtGiftDetails.Columns.Add("GiftStudentname");
            dtGiftDetails.Columns.Add("StDesig");
            dtGiftDetails.Columns.Add("GiftStRel");
            dtGiftDetails.Columns.Add("GiftStRelName");
            dtGiftDetails.Columns.Add("GiftStudentHNo");
            dtGiftDetails.Columns.Add("GiftStLandMark");
            dtGiftDetails.Columns.Add("GiftStVillage");
            dtGiftDetails.Columns.Add("GiftStMandal");
            dtGiftDetails.Columns.Add("GiftStDistrict");
            dtGiftDetails.Columns.Add("GiftStState");
            dtGiftDetails.Columns.Add("GiftStPin");
            dtGiftDetails.Columns.Add("Quiz");
            dtGiftDetails.Columns.Add("Rank");
            dtGiftDetails.Columns.Add("GiftName");
            dtGiftDetails.Columns.Add("GiftStMobileNo");
            dtGiftDetails.Columns.Add("GiftRemarks");
            #endregion

            #region "CREATE SCHOOL_STAFF TABLE"
            dtSchoolStaffDetl.Columns.Add("SlNo_Staff");
            dtSchoolStaffDetl.Columns.Add("StaffName");
            dtSchoolStaffDetl.Columns.Add("StaffDesig");
            dtSchoolStaffDetl.Columns.Add("StaffMobileNo");
            dtSchoolStaffDetl.Columns.Add("StaffRemarks");
            #endregion


            FillEmployeeData();
            EcodeSearch();

            if (sCompCode.Length > 0 && sBranCode.Length > 0 && sEcode.Length > 0 && sActivityDate.Length > 0)
            {
                cbCompany.SelectedValue = sCompCode;
                cbBranches.SelectedValue = sBranCode;
                txtEcodeSearch.Text = sEcode;
                if (sEcode.Length > 1)
                    txtEcodeSearch_KeyUp(null,null);
                cbEcode.SelectedValue = sEcode;
                dtpTrnDate.Value = Convert.ToDateTime(sActivityDate);
                dtpTrnDate.Enabled = false;
                cbEcode.Enabled = false;
                txtEcodeSearch.ReadOnly = true;
                txtTrnNo.ReadOnly = true;
                GenerateTransactionNo();
                if(strTrnNo.Length==0)
                txtTrnNo.CausesValidation = false;
            }
            else
            {
                txtTrnNo.ReadOnly = false;
                dtpTrnDate.Enabled = true;
                cbEcode.Enabled = true;
                txtEcodeSearch.ReadOnly = false;
                txtTrnNo.CausesValidation = true;
            }
            if (strTrnNo.Length > 0)
            {
                txtTrnNo.Text = strTrnNo;
                txtTrnNo_KeyUp(null, null);
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
            }
          
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

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+ STATE_CODE AS BranCode " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "' and BRANCH_TYPE IN ('BR','HO') ORDER BY BRANCH_NAME ASC";

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
                    cbBranches.ValueMember = "BranCode";
                }


                string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranches.SelectedValue = BranCode;
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
            if (cbBranches.SelectedIndex > 0)
            {
                try
                {

                    string[] BranCode = cbBranches.SelectedValue.ToString().Split('@');
                    string finyear = CommonData.FinancialYear.Substring(2, 2) + CommonData.FinancialYear.Substring(7, 2);
                    string strNewNo = BranCode[0] + finyear + "SV-";

                    string strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(SSVH_TRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 " +
                                        " FROM SERVICES_SCHOOL_VISIT_HEAD " +
                                        " WHERE SSVH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND SSVH_BRANCH_CODE='" + BranCode[0] + "' ";
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


        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                GenerateTransactionNo();
                EcodeSearch();
            }
            else
            {
               
                //cbCompany.SelectedIndex = 0;
                //cbBranches.SelectedIndex = -1;               
                txtEcodeSearch.Text = "";
                txtSchoolName.Text = "";
                cbEcode.SelectedIndex = -1;
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtPrincipalDesig.Text = "";
                txtPrincipalName.Text = "";
                txtPhoneNo.Text = "";

                dtGiftDetails.Rows.Clear();
                dtSchoolStaffDetl.Rows.Clear();
                dtStudentDetails.Rows.Clear();
                dtProdDemoDetails.Rows.Clear();
                dtGiftDetails.Rows.Clear();

                gvAttendedEmpDetails.Rows.Clear();
                gvStaffDetails.Rows.Clear();
                gvDemoDetails.Rows.Clear();
                gvStudentDetails.Rows.Clear();
                gvGiftDetails.Rows.Clear();

            }
        }


       
        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                grpr1.Visible = false;
                grpr2.Visible = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            grpr1.Visible = true;
            grpr2.Visible = false;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
                cbBranches.SelectedIndex = 0;
            }
        }

        private void btnAddProductDemoDetails_Click(object sender, EventArgs e)
        {
            if (txtSchoolName.Text != "")
            {

                SVProductDemoDetails ProdDemoDetl = new SVProductDemoDetails();
                ProdDemoDetl.objfrmSchoolVisits = this;
                ProdDemoDetl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Enter School Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchoolName.Focus();
            }
           
        }

        private void btnAddEmpdetails_Click(object sender, EventArgs e)
        {
            if (txtSchoolName.Text != "")
            {
                SVAttendedEmpDetails EmpDetl = new SVAttendedEmpDetails();
                EmpDetl.objfrmSchoolVisits = this;
                EmpDetl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Enter School Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchoolName.Focus();
            }
        }

        private void btnAddStudentDetails_Click(object sender, EventArgs e)
        {
            if (dtStudentDetails.Rows.Count > 0)
            {
                string Village = dtStudentDetails.Rows[0]["StudentVillage"].ToString();
                string Mandal = dtStudentDetails.Rows[0]["StudentMandal"].ToString();
                string District = dtStudentDetails.Rows[0]["StudentDistrict"].ToString();
                string State = dtStudentDetails.Rows[0]["StudentState"].ToString();
                string Pin = dtStudentDetails.Rows[0]["StudentPin"].ToString();

                SVAttendedStudentDetails StudentDetl = new SVAttendedStudentDetails(Village,Mandal,District,State,Pin);
                StudentDetl.objfrmSchoolVisits = this;
                StudentDetl.ShowDialog();
            }
            else
            {

                SVAttendedStudentDetails StudentDetl = new SVAttendedStudentDetails();
                StudentDetl.objfrmSchoolVisits = this;
                StudentDetl.ShowDialog();
            }

        }
        private void btnAddGiftDetails_Click(object sender, EventArgs e)
        {
            if (dtGiftDetails.Rows.Count > 0)
            {
                string Village = dtGiftDetails.Rows[0]["GiftStVillage"].ToString();
                string Mandal = dtGiftDetails.Rows[0]["GiftStMandal"].ToString();
                string District = dtGiftDetails.Rows[0]["GiftStDistrict"].ToString();
                string State = dtGiftDetails.Rows[0]["GiftStState"].ToString();
                string Pin = dtGiftDetails.Rows[0]["GiftStPin"].ToString();
               
                SVGiftDetails GiftDetl = new SVGiftDetails(Village, Mandal, District, State, Pin);
                GiftDetl.objfrmSchoolVisits = this;
                GiftDetl.ShowDialog();
            }
            else
            {
                SVGiftDetails GiftDetl = new SVGiftDetails();
                GiftDetl.objfrmSchoolVisits = this;
                GiftDetl.ShowDialog();
            }
        }

        private void btnAddStaffDetails_Click(object sender, EventArgs e)
        {
            SVAttendedSchoolEmpDetails SchoolEmpDetl = new SVAttendedSchoolEmpDetails();
            SchoolEmpDetl.objfrmSchoolVisits = this;
            SchoolEmpDetl.ShowDialog();

        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
            }
            else if (cbBranches.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
            }
            else if (txtSchoolName.Text.Length== 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter School Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchoolName.Focus();
            }
            else if (cbEcode.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Conducted Emp Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbEcode.Focus();
            }

            else if (txtPrincipalName.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter School Conducted Employee Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrincipalName.Focus();
            }

            else if (txtVillage.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Village Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }
            else if (gvDemoDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Demo Product Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;

            }
            //else if (txtPrincipalDesig.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter School Conducted Emp Desig", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtPrincipalDesig.Focus();
            //}
            //else if (txtPhoneNo.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter Phone Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtPhoneNo.Focus();
            //}

            return bFlag;
        }
     
        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            
            if (SaveHeadDetails() > 0)
            {
                if (SaveProductDemoDetails() > 0)
                {
                    SaveGiftDetails();
                    SaveAttendentsDetails();
                    MessageBox.Show("Data Saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagUpdate = false;
                    GenerateTransactionNo();
                    btnCancel_Click(null,null);
                    grpr1.Visible = true;
                    grpr2.Visible = false;
                   
                    if (sEcode.Length > 0 && sActivityDate.Length > 0)
                    {
                        objEmployeeDARWithTourBills.FillEmployeeActivityDetails(Convert.ToInt32(sEcode), sActivityDate);
                        this.Close();
                        this.Dispose();
                    }

                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string strCommand = "DELETE FROM SERVICES_SCHOOL_VISIT_HEAD WHERE SSVH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                    int iResult = objSQLdb.ExecuteSaveData(strCommand);
                }
            }
            else
            {
                MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);               
              
            }
        }
        #region "SAVE AND UPDATE DATA"

        private int SaveHeadDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";

            try
            {

                strCommand = "DELETE FROM SERVICES_SCHOOL_VISIT_ATTENDENTS WHERE SSVA_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";
                
                strCommand += " DELETE FROM SERVICES_SCHOOL_VISIT_GIFTS WHERE SSVG_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";
                
                strCommand += " DELETE FROM SERVICES_SCHOOL_VISIT_PRODUCTS WHERE SSVP_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";
                
                string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');

                if (flagUpdate == true)
                {
                    strCommand += " UPDATE SERVICES_SCHOOL_VISIT_HEAD SET SSVH_COMPANY_CODE ='"+ cbCompany.SelectedValue.ToString() +
                                   "',SSVH_STATE_CODE='"+ strBranCode[1] +"',SSVH_BRANCH_CODE='"+ strBranCode[0] +
                                   "',SSVH_DOC_MONTH='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                   "',SSVH_TRN_DATE='"+Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy")+
                                   "',SSVH_SCHOOL_NAME='"+ txtSchoolName.Text.ToString() +
                                   "',SSVH_COND_BY_ECODE="+ Convert.ToInt32(cbEcode.SelectedValue) +
                                   ",SSVH_ADDR='"+ txtHouseNo.Text.ToString() +"',SSVH_LAND_MARK='"+ txtLandMark.Text.ToString() +
                                   "',SSVH_VILLAGE='"+ txtVillage.Text.ToString() +
                                   "',SSVH_MANDAL='"+ txtMandal.Text.ToString() +
                                   "',SSVH_DISTRICT='"+ txtDistrict.Text.ToString() +
                                   "',SSVH_STATE='"+ txtState.Text.ToString() +
                                   "',SSVH_PIN='"+ txtPin.Text.ToString() +
                                   "',SSVH_CONT_PERSON_NAME='"+ txtPrincipalName.Text.ToString() +
                                   "',SSVH_CONT_PERSON_DESG='"+ txtPrincipalDesig.Text.ToString() +
                                   "',SSVH_CONT_PERSON_PHONE='"+ txtPhoneNo.Text.ToString() +
                                   "' WHERE SSVH_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";
                }
                else if (flagUpdate == false)
                {

                    strCommand = "INSERT INTO SERVICES_SCHOOL_VISIT_HEAD(SSVH_COMPANY_CODE " +
                                                                      ", SSVH_STATE_CODE " +
                                                                      ", SSVH_BRANCH_CODE " +
                                                                      ", SSVH_DOC_MONTH " +
                                                                      ", SSVH_TRN_TYPE " +
                                                                      ", SSVH_TRN_NUMBER " +
                                                                      ", SSVH_TRN_DATE " +
                                                                      ", SSVH_COND_BY_ECODE " +
                                                                      ", SSVH_SCHOOL_NAME " +
                                                                      ", SSVH_ADDR " +
                                                                      ", SSVH_LAND_MARK " +
                                                                      ", SSVH_VILLAGE " +
                                                                      ", SSVH_MANDAL " +
                                                                      ", SSVH_DISTRICT " +
                                                                      ", SSVH_STATE " +
                                                                      ", SSVH_PIN " +
                                                                      ", SSVH_CONT_PERSON_NAME " +
                                                                      ", SSVH_CONT_PERSON_DESG " +
                                                                      ", SSVH_CONT_PERSON_PHONE " +
                                                                      ", SSVH_CREATED_BY " +
                                                                      ", SSVH_AUTHORIZED_BY " +
                                                                      ", SSVH_CREATED_DATE " +
                                                                      ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                      "','" + strBranCode[1] +
                                                                      "','" + strBranCode[0] +
                                                                      "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                      "','SCHOOL VISIT','" + txtTrnNo.Text.ToString() +
                                                                      "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                      "'," + Convert.ToInt32(cbEcode.SelectedValue) +
                                                                      ",'" + txtSchoolName.Text.ToString() +
                                                                      "','" + txtHouseNo.Text.ToString() +
                                                                      "','" + txtLandMark.Text.ToString() +
                                                                      "','" + txtVillage.Text.ToString() +
                                                                      "','" + txtMandal.Text.ToString() +
                                                                      "','" + txtDistrict.Text.ToString() +
                                                                      "','" + txtState.Text.ToString() +
                                                                      "','" + txtPin.Text.ToString() +
                                                                      "','" + txtPrincipalName.Text.ToString() +
                                                                      "','" + txtPrincipalDesig.Text.ToString() +
                                                                      "','" + txtPhoneNo.Text.ToString() +
                                                                      "','" + CommonData.LogUserId +
                                                                      "','',getdate())";
                }
                if (strCommand != "")
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

        private int SaveProductDemoDetails()
        {
            objSQLdb = new SQLDB();
            int iRec = 0;
            string strCmd = "";
            try
            {
                string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                if (gvDemoDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDemoDetails.Rows.Count; i++)
                    {

                        strCmd += "INSERT INTO SERVICES_SCHOOL_VISIT_PRODUCTS(SSVP_COMPANY_CODE " +
                                                                          ", SSVP_BRANCH_CODE " +
                                                                          ", SSVP_STATE_CODE " +
                                                                          ", SSVP_DOC_MONTH " +
                                                                          ", SSVP_TRN_TYPE " +
                                                                          ", SSVP_TRN_NUMBER " +
                                                                          ", SSVP_PRODUCT_ID " +
                                                                          ", SSVP_STUDENTS_COUNT " +
                                                                          ", SSVP_DEMOS_COUNT " +
                                                                          ", SSVP_REMARKS " +
                                                                          ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                          "','" + strBranCode[0] +
                                                                          "','" + strBranCode[1] +
                                                                          "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                          "','SCHOOL VISIT','" + txtTrnNo.Text.ToString() +
                                                                          "','"+ gvDemoDetails.Rows[i].Cells["ProductID"].Value.ToString() +
                                                                          "',"+ Convert.ToInt32(gvDemoDetails.Rows[i].Cells["StudentsCnt"].Value) +
                                                                          ","+ Convert.ToInt32(gvDemoDetails.Rows[i].Cells["DemosCnt"].Value) +
                                                                          ",'"+ gvDemoDetails.Rows[i].Cells["Remarks_Demo"].Value.ToString() +"')";
                    }
                }
                if (strCmd != "")
                {
                    iRec = objSQLdb.ExecuteSaveData(strCmd);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRec;
        }

        private int SaveGiftDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');


                if (gvGiftDetails.Rows.Count > 0)
                {

                    for (int i = 0; i < gvGiftDetails.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvGiftDetails.Rows[i].Cells["StudentRank"].Value) == "")
                        {
                            gvGiftDetails.Rows[i].Cells["StudentRank"].Value = "0";
                        }

                        strCommand += "INSERT INTO SERVICES_SCHOOL_VISIT_GIFTS(SSVG_COMPANY_CODE " +
                                                                           ", SSVG_STATE_CODE " +
                                                                           ", SSVG_BRANCH_CODE " +
                                                                           ", SSVG_DOC_MONTH " +
                                                                           ", SSVG_TRN_TYPE " +
                                                                           ", SSVG_TRN_NUMBER " +
                                                                           ", SSVG_ATTND_TYPE " +
                                                                           ", SSVG_SL_NO " +
                                                                           ", SSVG_NAME " +
                                                                           ", SSVG_DESIG " +
                                                                           ", SSVG_MOBILE_NUMBER " +
                                                                           ", SSVG_RELA_TYPE " +
                                                                           ", SSVG_RELA_NAME " +
                                                                           ", SSVG_HOUSE_NO " +
                                                                           ", SSVG_LANDMARK " +
                                                                           ", SSVG_VILLAGE " +
                                                                           ", SSVG_MANDAL " +
                                                                           ", SSVG_DISTRICT " +
                                                                           ", SSVG_STATE " +
                                                                           ", SSVG_PIN " +
                                                                           ", SSVG_QUIZ " +
                                                                           ", SSVG_RANK " +
                                                                           ", SSVG_GIFT " +
                                                                           ", SSVG_REMARKS " +
                                                                           ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                           "','" + strBranCode[1] + "','" + strBranCode[0] +
                                                                           "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                           "','SCHOOL VISIT','" + txtTrnNo.Text.ToString() +
                                                                           "','OTHERS'," + Convert.ToInt32(gvGiftDetails.Rows[i].Cells["SLNo_Gift"].Value) +
                                                                           ",'" + gvGiftDetails.Rows[i].Cells["GiftStudentname"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["StDesig"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStMobileNo"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStRel"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStRelName"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStudentHNo"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStLandMark"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStVillage"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStMandal"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStDistrict"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStState"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftStPin"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["Quiz"].Value.ToString() +
                                                                           "'," + Convert.ToInt32(gvGiftDetails.Rows[i].Cells["StudentRank"].Value) +
                                                                           ",'" + gvGiftDetails.Rows[i].Cells["GiftName"].Value.ToString() +
                                                                           "','" + gvGiftDetails.Rows[i].Cells["GiftRemarks"].Value.ToString() + "')";
                    }
                }

                if (strCommand != "")
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

        private int SaveAttendentsDetails()
        {
            objSQLdb = new SQLDB();
            int iRes1 = 0;
            string strCommand = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                if (gvStudentDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvStudentDetails.Rows.Count; i++)
                    {

                        sb.Append("INSERT INTO SERVICES_SCHOOL_VISIT_ATTENDENTS(SSVA_COMPANY_CODE " +
                                                                              ", SSVA_STATE_CODE " +
                                                                              ", SSVA_BRANCH_CODE " +
                                                                              ", SSVA_DOC_MONTH " +
                                                                              ", SSVA_TRN_TYPE " +
                                                                              ", SSVA_TRN_NUMBER " +
                                                                              ", SSVA_ATTND_TYPE " +
                                                                              ", SSVA_SL_NO " +
                                                                              ", SSVA_NAME " +
                                                                              ", SSVA_MOBILE_NUMBER " +
                                                                              ", SSVA_RELA_TYPE " +
                                                                              ", SSVA_RELA_NAME " +
                                                                              ", SSVA_HOUSE_NO " +
                                                                              ", SSVA_LANDMARK " +
                                                                              ", SSVA_VILLAGE " +
                                                                              ", SSVA_MANDAL " +
                                                                              ", SSVA_DISTRICT " +
                                                                              ", SSVA_STATE " +
                                                                              ", SSVA_PIN " +
                                                                              ", SSVA_REMARKS " +
                                                                              ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                              "','" + strBranCode[1] +
                                                                              "','" + strBranCode[0] +
                                                                              "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                              "','SCHOOL VISIT'" +
                                                                              " ,'" + txtTrnNo.Text.ToString() +
                                                                              "','STUDENTS' " +
                                                                              "," + Convert.ToInt32(gvStudentDetails.Rows[i].Cells["SLNo_Student"].Value) +
                                                                              ",'" + gvStudentDetails.Rows[i].Cells["StudentName"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentMobileNo"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentRel"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentRelName"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentHNo"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentLandMark"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentVillage"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentMandal"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentDistrict"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentState"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentPin"].Value.ToString() +
                                                                              "','" + gvStudentDetails.Rows[i].Cells["StudentRemarks"].Value.ToString() + "')");
                    }
                }
                    if (gvStaffDetails.Rows.Count > 0)
                    {
                        for (int j = 0; j < gvStaffDetails.Rows.Count; j++)
                        {
                            sb.Append("INSERT INTO SERVICES_SCHOOL_VISIT_ATTENDENTS(SSVA_COMPANY_CODE "+
                                                                                 ", SSVA_STATE_CODE "+
                                                                                 ", SSVA_BRANCH_CODE "+
                                                                                 ", SSVA_DOC_MONTH "+
                                                                                 ", SSVA_TRN_TYPE "+
                                                                                 ", SSVA_TRN_NUMBER "+
                                                                                 ", SSVA_ATTND_TYPE "+
                                                                                 ", SSVA_SL_NO "+
                                                                                 ", SSVA_NAME "+
                                                                                 ", SSVA_DESIG "+
                                                                                 ", SSVA_MOBILE_NUMBER "+
                                                                                 ", SSVA_REMARKS "+
                                                                                 ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                              "','" + strBranCode[1] +
                                                                              "','" + strBranCode[0] +
                                                                              "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                              "','SCHOOL VISIT'" +
                                                                              " ,'" + txtTrnNo.Text.ToString() +
                                                                              "','SCHOOL_STAFF' " +
                                                                              "," + Convert.ToInt32(gvStaffDetails.Rows[j].Cells["SlNo_Staff"].Value)
                                                                              +",'"+ gvStaffDetails.Rows[j].Cells["StaffName"].Value.ToString() +
                                                                              "','"+ gvStaffDetails.Rows[j].Cells["StaffDesig"].Value.ToString() +
                                                                              "','"+ gvStaffDetails.Rows[j].Cells["StaffMobileNo"].Value.ToString() +
                                                                              "','"+ gvStaffDetails.Rows[j].Cells["StaffRemarks"].Value.ToString() +"')");

                        }
                    }

                    if (gvAttendedEmpDetails.Rows.Count > 0)
                    {
                        for (int k = 0; k < gvAttendedEmpDetails.Rows.Count; k++)
                        {
                            sb.Append("INSERT INTO SERVICES_SCHOOL_VISIT_ATTENDENTS(SSVA_COMPANY_CODE " +
                                                                               ", SSVA_STATE_CODE " +
                                                                               ", SSVA_BRANCH_CODE " +
                                                                               ", SSVA_DOC_MONTH " +
                                                                               ", SSVA_TRN_TYPE " +
                                                                               ", SSVA_TRN_NUMBER " +
                                                                               ", SSVA_ATTND_TYPE " +
                                                                               ", SSVA_SL_NO " +
                                                                               ", SSVA_ECODE "+
                                                                               ", SSVA_NAME " +
                                                                               ", SSVA_DESIG " +                                                                                
                                                                               ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                            "','" + strBranCode[1] +
                                                                            "','" + strBranCode[0] +
                                                                            "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                            "','SCHOOL VISIT'" +
                                                                            " ,'" + txtTrnNo.Text.ToString() +
                                                                            "','COMPANY_STAFF' " +
                                                                            "," + Convert.ToInt32(gvAttendedEmpDetails.Rows[k].Cells["SlNo_Emp"].Value)
                                                                            + "," + Convert.ToInt32(gvAttendedEmpDetails.Rows[k].Cells["Ecode"].Value) +
                                                                            ",'" + gvAttendedEmpDetails.Rows[k].Cells["EmpName"].Value.ToString() +
                                                                            "','" + gvAttendedEmpDetails.Rows[k].Cells["Desig"].Value.ToString() + "')");
                        }
                    }
                

                if (sb.Length != 0)
                {
                    strCommand += sb.ToString().Substring(0, sb.ToString().Length);
                }
                if (strCommand != "")
                {
                    iRes1 = objSQLdb.ExecuteSaveData(strCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes1;
        }
        #endregion

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VilSearch = new VillageSearch("frmSchoolVisits");
            VilSearch.objfrmSchoolVisits = this;
            VilSearch.Show();

        }



        #region "GRIDVIEW DETAILS"      

        public void GetEmpDetails()
        {
            int intRow = 1;
            gvAttendedEmpDetails.Rows.Clear();
            try
            {

                for (int i = 0; i < dtAttEmpDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtAttEmpDetails.Rows[i]["SlNo_Emp"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellEmpEcode = new DataGridViewTextBoxCell();
                    cellEmpEcode.Value = dtAttEmpDetails.Rows[i]["Ecode"];
                    tempRow.Cells.Add(cellEmpEcode);

                    DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                    cellEmpName.Value = dtAttEmpDetails.Rows[i]["EmpName"];
                    tempRow.Cells.Add(cellEmpName);

                    DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                    cellEmpDesig.Value = dtAttEmpDetails.Rows[i]["Desig"];
                    tempRow.Cells.Add(cellEmpDesig);

                    DataGridViewCell cellEmpDept = new DataGridViewTextBoxCell();
                    cellEmpDept.Value = dtAttEmpDetails.Rows[i]["Dept"];
                    tempRow.Cells.Add(cellEmpDept);


                    intRow = intRow + 1;
                    gvAttendedEmpDetails.Rows.Add(tempRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void GetStudentDetails()
        {
            int intRow = 1;
            gvStudentDetails.Rows.Clear();
            try
            {              

                for (int i = 0; i < dtStudentDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtStudentDetails.Rows[i]["SlNo_Student"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellStudentName = new DataGridViewTextBoxCell();
                    cellStudentName.Value = dtStudentDetails.Rows[i]["StudentName"];
                    tempRow.Cells.Add(cellStudentName);

                    DataGridViewCell cellStudentRel = new DataGridViewTextBoxCell();
                    cellStudentRel.Value = dtStudentDetails.Rows[i]["StudentRel"];
                    tempRow.Cells.Add(cellStudentRel);

                    DataGridViewCell cellStudentRelName = new DataGridViewTextBoxCell();
                    cellStudentRelName.Value = dtStudentDetails.Rows[i]["StudentRelName"];
                    tempRow.Cells.Add(cellStudentRelName);

                    DataGridViewCell cellStudentHouseNo = new DataGridViewTextBoxCell();
                    cellStudentHouseNo.Value = dtStudentDetails.Rows[i]["StudentHNo"];
                    tempRow.Cells.Add(cellStudentHouseNo);

                    DataGridViewCell cellStudentLandMark = new DataGridViewTextBoxCell();
                    cellStudentLandMark.Value = dtStudentDetails.Rows[i]["StudentLandMark"];
                    tempRow.Cells.Add(cellStudentLandMark);

                    DataGridViewCell cellStudentVillage = new DataGridViewTextBoxCell();
                    cellStudentVillage.Value = dtStudentDetails.Rows[i]["StudentVillage"];
                    tempRow.Cells.Add(cellStudentVillage);

                    DataGridViewCell cellStudentMandal = new DataGridViewTextBoxCell();
                    cellStudentMandal.Value = dtStudentDetails.Rows[i]["StudentMandal"];
                    tempRow.Cells.Add(cellStudentMandal);

                    DataGridViewCell cellStudentDistrict = new DataGridViewTextBoxCell();
                    cellStudentDistrict.Value = dtStudentDetails.Rows[i]["StudentDistrict"];
                    tempRow.Cells.Add(cellStudentDistrict);

                    DataGridViewCell cellStudentState = new DataGridViewTextBoxCell();
                    cellStudentState.Value = dtStudentDetails.Rows[i]["StudentState"];
                    tempRow.Cells.Add(cellStudentState);

                    DataGridViewCell cellStudentPin = new DataGridViewTextBoxCell();
                    cellStudentPin.Value = dtStudentDetails.Rows[i]["StudentPin"];
                    tempRow.Cells.Add(cellStudentPin);

                    DataGridViewCell cellStudentMobileNo = new DataGridViewTextBoxCell();
                    cellStudentMobileNo.Value = dtStudentDetails.Rows[i]["StudentMobileNo"];
                    tempRow.Cells.Add(cellStudentMobileNo);


                    DataGridViewCell cellStudentRemarks = new DataGridViewTextBoxCell();
                    cellStudentRemarks.Value = dtStudentDetails.Rows[i]["StudentRemarks"];
                    tempRow.Cells.Add(cellStudentRemarks);

                    intRow = intRow + 1;
                    gvStudentDetails.Rows.Add(tempRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    
        public void GetProductDetails()
        {
            int intRow = 1;
            gvDemoDetails.Rows.Clear();
            try
            {
                for (int i = 0; i < dtProdDemoDetails.Rows.Count; i++)
                {

                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtProdDemoDetails.Rows[i]["SLNO_Product"] = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                    cellProductId.Value = dtProdDemoDetails.Rows[i]["ProductID"];
                    tempRow.Cells.Add(cellProductId);
                   
                    DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                    cellProductName.Value = dtProdDemoDetails.Rows[i]["prodName"];
                    tempRow.Cells.Add(cellProductName);

                    DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                    cellCategoryName.Value = dtProdDemoDetails.Rows[i]["Category"];
                    tempRow.Cells.Add(cellCategoryName);

                    DataGridViewCell cellStudentCnt= new DataGridViewTextBoxCell();
                    cellStudentCnt.Value = dtProdDemoDetails.Rows[i]["StudentsCnt"];
                    tempRow.Cells.Add(cellStudentCnt);

                    DataGridViewCell cellDemosCnt = new DataGridViewTextBoxCell();
                    cellDemosCnt.Value = dtProdDemoDetails.Rows[i]["DemosCnt"];
                    tempRow.Cells.Add(cellDemosCnt);
                    
                    DataGridViewCell cellProdRemarks = new DataGridViewTextBoxCell();
                    cellProdRemarks.Value = dtProdDemoDetails.Rows[i]["Remarks_Demo"];
                    tempRow.Cells.Add(cellProdRemarks);

                    intRow = intRow + 1;
                    gvDemoDetails.Rows.Add(tempRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void GetGiftDetails()
        {
            int intRow = 1;
            gvGiftDetails.Rows.Clear();
            try
            {

                for (int i = 0; i < dtGiftDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtGiftDetails.Rows[i]["SLNo_Gift"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellGiftStudentName = new DataGridViewTextBoxCell();
                    cellGiftStudentName.Value = dtGiftDetails.Rows[i]["GiftStudentname"];
                    tempRow.Cells.Add(cellGiftStudentName);

                    DataGridViewCell cellDesig = new DataGridViewTextBoxCell();
                    cellDesig.Value = dtGiftDetails.Rows[i]["StDesig"];
                    tempRow.Cells.Add(cellDesig);

                    DataGridViewCell cellStudentRel = new DataGridViewTextBoxCell();
                    cellStudentRel.Value = dtGiftDetails.Rows[i]["GiftStRel"];
                    tempRow.Cells.Add(cellStudentRel);

                    DataGridViewCell cellStudentRelName = new DataGridViewTextBoxCell();
                    cellStudentRelName.Value = dtGiftDetails.Rows[i]["GiftStRelName"];
                    tempRow.Cells.Add(cellStudentRelName);

                    DataGridViewCell cellStudentHouseNo = new DataGridViewTextBoxCell();
                    cellStudentHouseNo.Value = dtGiftDetails.Rows[i]["GiftStudentHNo"];
                    tempRow.Cells.Add(cellStudentHouseNo);

                    DataGridViewCell cellStudentLandMark = new DataGridViewTextBoxCell();
                    cellStudentLandMark.Value = dtGiftDetails.Rows[i]["GiftStLandMark"];
                    tempRow.Cells.Add(cellStudentLandMark);

                    DataGridViewCell cellStudentVillage = new DataGridViewTextBoxCell();
                    cellStudentVillage.Value = dtGiftDetails.Rows[i]["GiftStVillage"];
                    tempRow.Cells.Add(cellStudentVillage);

                    DataGridViewCell cellStudentMandal = new DataGridViewTextBoxCell();
                    cellStudentMandal.Value = dtGiftDetails.Rows[i]["GiftStMandal"];
                    tempRow.Cells.Add(cellStudentMandal);

                    DataGridViewCell cellStudentDistrict = new DataGridViewTextBoxCell();
                    cellStudentDistrict.Value = dtGiftDetails.Rows[i]["GiftStDistrict"];
                    tempRow.Cells.Add(cellStudentDistrict);

                    DataGridViewCell cellStudentState = new DataGridViewTextBoxCell();
                    cellStudentState.Value = dtGiftDetails.Rows[i]["GiftStState"];
                    tempRow.Cells.Add(cellStudentState);

                    DataGridViewCell cellStudentPin = new DataGridViewTextBoxCell();
                    cellStudentPin.Value = dtGiftDetails.Rows[i]["GiftStPin"];
                    tempRow.Cells.Add(cellStudentPin);

                    DataGridViewCell cellQuiz = new DataGridViewTextBoxCell();
                    cellQuiz.Value = dtGiftDetails.Rows[i]["Quiz"];
                    tempRow.Cells.Add(cellQuiz);

                    DataGridViewCell cellRank = new DataGridViewTextBoxCell();
                    cellRank.Value = dtGiftDetails.Rows[i]["Rank"];
                    tempRow.Cells.Add(cellRank);

                    DataGridViewCell cellGiftName = new DataGridViewTextBoxCell();
                    cellGiftName.Value = dtGiftDetails.Rows[i]["GiftName"];
                    tempRow.Cells.Add(cellGiftName);

                    DataGridViewCell cellStudentMobileNo = new DataGridViewTextBoxCell();
                    cellStudentMobileNo.Value = dtGiftDetails.Rows[i]["GiftStMobileNo"];
                    tempRow.Cells.Add(cellStudentMobileNo);


                    DataGridViewCell cellGiftRemarks = new DataGridViewTextBoxCell();
                    cellGiftRemarks.Value = dtGiftDetails.Rows[i]["GiftRemarks"];
                    tempRow.Cells.Add(cellGiftRemarks);

                    intRow = intRow + 1;
                    gvGiftDetails.Rows.Add(tempRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void GetSchoolStaffDetails()
        {
            int intRow = 1;
            gvStaffDetails.Rows.Clear();
            for (int i = 0; i < dtSchoolStaffDetl.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtSchoolStaffDetl.Rows[i]["SlNo_Staff"] = intRow;
                tempRow.Cells.Add(cellSLNO);


                DataGridViewCell cellStaffName = new DataGridViewTextBoxCell();
                cellStaffName.Value = dtSchoolStaffDetl.Rows[i]["StaffName"];
                tempRow.Cells.Add(cellStaffName);

                DataGridViewCell cellStaffDesig = new DataGridViewTextBoxCell();
                cellStaffDesig.Value = dtSchoolStaffDetl.Rows[i]["StaffDesig"];
                tempRow.Cells.Add(cellStaffDesig);

                DataGridViewCell cellStaffMobileNo = new DataGridViewTextBoxCell();
                cellStaffMobileNo.Value = dtSchoolStaffDetl.Rows[i]["StaffMobileNo"];
                tempRow.Cells.Add(cellStaffMobileNo);

                DataGridViewCell cellStaffRemarks = new DataGridViewTextBoxCell();
                cellStaffRemarks.Value = dtSchoolStaffDetl.Rows[i]["StaffRemarks"];
                tempRow.Cells.Add(cellStaffRemarks);


                intRow = intRow + 1;
                gvStaffDetails.Rows.Add(tempRow);

            }
        }

        #endregion

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                EcodeSearch();
            }
            else
            {
                FillEmployeeData();
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
            DataSet dsEmp = null;
            if (cbBranches.SelectedIndex > 0)
            {
                try
                {
                    string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                    Cursor.Current = Cursors.WaitCursor;
                    cbEcode.DataSource = null;
                    cbEcode.Items.Clear();
                    dsEmp = objServicedb.ServiceLevelEcodeSearch_Get(cbCompany.SelectedValue.ToString(), strBranCode[0], CommonData.DocMonth, txtEcodeSearch.Text.ToString());
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

        }

        #region "GET DATA FOR UPDATE"

        private void GetSchoolVisitDetails()
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;

            DataTable dtSVHead;

            if (txtTrnNo.Text.Length > 21)
            {
                try
                {
                    ht = objServicedb.GetServiceSchoolVisitDetails(txtTrnNo.Text.ToString());
                    dtSVHead = (DataTable)ht["SchoolVisitHead"];

                    FillHeadDetails(dtSVHead);
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

        private void FillHeadDetails(DataTable dtHead)
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;
            DataTable dtSVProdDemoDetails;
            DataTable dtEmpDetails;
            DataTable dtSchoolEmpDetails;
            DataTable dtStudentDetails;
            DataTable dtSVGiftDetl;

            if (txtTrnNo.Text.Length > 21)
            {
                try
                {

                    ht = objServicedb.GetServiceSchoolVisitDetails(txtTrnNo.Text.ToString());

                    dtSVProdDemoDetails = (DataTable)ht["SchoolVisitProductDetails"];
                    dtEmpDetails = (DataTable)ht["AttendentEmpDetails"];
                    dtSchoolEmpDetails = (DataTable)ht["SchoolStaffDetails"];
                    dtStudentDetails = (DataTable)ht["StudentDetails"];
                    dtSVGiftDetl = (DataTable)ht["GiftDetails"];


                    if (dtHead.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        string stECode = dtHead.Rows[0]["Ecode"] + "";
                        cbCompany.SelectedValue = dtHead.Rows[0]["CompCode"].ToString(); ;
                        cbBranches.SelectedValue = dtHead.Rows[0]["BranCode"].ToString();
                       
                        dtpTrnDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"].ToString());
                        cbEcode.SelectedValue = stECode;
                        txtEcodeSearch.Text = stECode;
                        txtSchoolName.Text = dtHead.Rows[0]["SchoolName"].ToString();
                        txtHouseNo.Text = dtHead.Rows[0]["Address"].ToString();
                        txtVillage.Text = dtHead.Rows[0]["Village"].ToString();
                        txtMandal.Text = dtHead.Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dtHead.Rows[0]["District"].ToString();
                        txtState.Text = dtHead.Rows[0]["State"].ToString();
                        txtPin.Text = dtHead.Rows[0]["Pin"].ToString();
                        txtPrincipalName.Text = dtHead.Rows[0]["SchoolHeadName"].ToString();
                        txtPrincipalDesig.Text = dtHead.Rows[0]["Desig"].ToString();
                        txtPhoneNo.Text = dtHead.Rows[0]["PhoneNo"].ToString();

                    }
                    else
                    {
                        flagUpdate = false;

                        GenerateTransactionNo();
                        txtEcodeSearch.Text = "";
                        txtSchoolName.Text = "";
                        cbEcode.SelectedIndex = -1;
                        dtpTrnDate.Value = DateTime.Today;
                        txtVillage.Text = "";
                        txtMandal.Text = "";
                        txtDistrict.Text = "";
                        txtState.Text = "";
                        txtPin.Text = "";
                        txtHouseNo.Text = "";
                        txtLandMark.Text = "";
                        txtPrincipalDesig.Text = "";
                        txtPrincipalName.Text = "";
                        txtPhoneNo.Text = "";

                        dtGiftDetails.Rows.Clear();
                        dtSchoolStaffDetl.Rows.Clear();
                        dtStudentDetails.Rows.Clear();
                        dtProdDemoDetails.Rows.Clear();
                        dtGiftDetails.Rows.Clear();

                        gvAttendedEmpDetails.Rows.Clear();
                        gvStaffDetails.Rows.Clear();
                        gvDemoDetails.Rows.Clear();
                        gvStudentDetails.Rows.Clear();
                        gvGiftDetails.Rows.Clear();
                    }

                    FillProductDemoDetails(dtSVProdDemoDetails);
                    FillStudentDetails(dtStudentDetails);
                    FillEmpDetails(dtEmpDetails);
                    FillSchoolEmpDetails(dtSchoolEmpDetails);
                    FillGiftDetails(dtSVGiftDetl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            

        }

        private void FillProductDemoDetails(DataTable dtProduct)
        {
            dtProdDemoDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 21)
            {
                try
                {
                    if (dtProduct.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        for (int i = 0; i < dtProduct.Rows.Count; i++)
                        {

                            dtProdDemoDetails.Rows.Add(new Object[] {"-1", dtProduct.Rows[i]["ProductId"].ToString(),
                                                                       dtProduct.Rows[i]["ProductName"].ToString(),                                                                      
                                                                       dtProduct.Rows[i]["CategoryName"].ToString(),
                                                                       Convert.ToInt32(dtProduct.Rows[i]["StudentsCount"]),
                                                                       dtProduct.Rows[i]["DemosCount"].ToString(), 
                                                                       dtProduct.Rows[i]["Remarks"].ToString()});                                                                                                                                                           
                                                                       

                            GetProductDetails();


                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }           


        }

        private void FillEmpDetails(DataTable dtEmp)
        {
            dtAttEmpDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 21)
            {
                try
                {
                    if (dtEmp.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        for (int i = 0; i < dtEmp.Rows.Count; i++)
                        {
                          
                            dtAttEmpDetails.Rows.Add(new Object[] {"-1", dtEmp.Rows[i]["Ecode"].ToString(),
                                                                       dtEmp.Rows[i]["EmpName"].ToString(),                                                                      
                                                                       dtEmp.Rows[i]["EmpDesig"].ToString(),
                                                                       dtEmp.Rows[i]["DeptName"].ToString() });


                            GetEmpDetails();


                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }           
        }

        private void FillSchoolEmpDetails(DataTable dtSchoolEmp)
        {
            dtSchoolStaffDetl.Rows.Clear();

            if (txtTrnNo.Text.Length > 21)
            {
                try
                {
                    if (dtSchoolEmp.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        for (int i = 0; i < dtSchoolEmp.Rows.Count; i++)
                        {
                            dtSchoolStaffDetl.Rows.Add(new Object[] {"-1", dtSchoolEmp.Rows[i]["Name"].ToString(),
                                                                       dtSchoolEmp.Rows[i]["Desig"].ToString(),                                                                      
                                                                       dtSchoolEmp.Rows[i]["MobileNo"].ToString(),
                                                                       dtSchoolEmp.Rows[i]["Remarks"].ToString() });
                            GetSchoolStaffDetails();


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void FillStudentDetails(DataTable dtStudents)
        {
            dtStudentDetails.Rows.Clear();
            if (txtTrnNo.Text.Length >21)
            {
                try
                {
                    if (dtStudents.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        for (int i = 0; i < dtStudents.Rows.Count; i++)
                        {

                            dtStudentDetails.Rows.Add(new Object[] {"-1", dtStudents.Rows[i]["Name"].ToString(),
                                                                       dtStudents.Rows[i]["RelType"].ToString(),
                                                                       dtStudents.Rows[i]["RelName"].ToString(),
                                                                       dtStudents.Rows[i]["HouseNo"].ToString(),
                                                                       dtStudents.Rows[i]["LandMark"].ToString(),
                                                                       dtStudents.Rows[i]["Village"].ToString(),
                                                                       dtStudents.Rows[i]["Mandal"].ToString(),                                                                      
                                                                       dtStudents.Rows[i]["District"].ToString(),
                                                                       dtStudents.Rows[i]["State"].ToString(),
                                                                       dtStudents.Rows[i]["Pin"].ToString(),
                                                                       dtStudents.Rows[i]["MobileNo"].ToString(),
                                                                       dtStudents.Rows[i]["Remarks"].ToString()});
                            GetStudentDetails();


                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }           
        }

        private void FillGiftDetails(DataTable dtGift)
        {
            dtGiftDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 21)
            {
                try
                {
                    if (dtGift.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        for (int i = 0; i < dtGift.Rows.Count; i++)
                        {
                           
                            
                            dtGiftDetails.Rows.Add(new Object[] {"-1", dtGift.Rows[i]["Name"].ToString(),
                                                                       dtGift.Rows[i]["Desig"].ToString(),
                                                                       dtGift.Rows[i]["RelType"].ToString(),
                                                                       dtGift.Rows[i]["RelName"].ToString(),
                                                                       dtGift.Rows[i]["HouseNo"].ToString(),
                                                                       dtGift.Rows[i]["LandMark"].ToString(),
                                                                       dtGift.Rows[i]["Village"].ToString(),                                                                      
                                                                       dtGift.Rows[i]["Mandal"].ToString(),
                                                                       dtGift.Rows[i]["District"].ToString(),
                                                                       dtGift.Rows[i]["State"].ToString(),
                                                                       dtGift.Rows[i]["Pin"].ToString(),
                                                                       dtGift.Rows[i]["QuizName"].ToString(),
                                                                       dtGift.Rows[i]["Rank"].ToString(),
                                                                       dtGift.Rows[i]["GiftName"].ToString(),
                                                                       dtGift.Rows[i]["MobileNo"].ToString(),
                                                                       dtGift.Rows[i]["Remarks"].ToString()});
                            GetGiftDetails();


                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }           
        }

        #endregion

        private void txtTrnNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTrnNo.Text.Length > 21)
            {
                
                GetSchoolVisitDetails();
            }
            else
            {
                flagUpdate = false;
                //cbCompany.SelectedIndex = 0;
                //cbBranches.SelectedIndex = -1;
                //GenerateTransactionNo();
                txtEcodeSearch.Text = "";
                txtSchoolName.Text = "";
                cbEcode.SelectedIndex = -1;
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtPrincipalDesig.Text = "";
                txtPrincipalName.Text = "";
                txtPhoneNo.Text = "";

                dtGiftDetails.Rows.Clear();
                dtSchoolStaffDetl.Rows.Clear();
                dtStudentDetails.Rows.Clear();
                dtProdDemoDetails.Rows.Clear();
                dtGiftDetails.Rows.Clear();

                gvAttendedEmpDetails.Rows.Clear();
                gvStaffDetails.Rows.Clear();
                gvDemoDetails.Rows.Clear();
                gvStudentDetails.Rows.Clear();
                gvGiftDetails.Rows.Clear();
            }
        }

        #region "EDITING AND DELETING GRID DETAILS"

        private void gvDemoDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvDemoDetails.Columns["Edit_ProdDemoDetails"].Index)
                {
                    if (Convert.ToBoolean(gvDemoDetails.Rows[e.RowIndex].Cells["Edit_ProdDemoDetails"].Selected) == true)
                    {
                       
                        int SlNo = Convert.ToInt32(gvDemoDetails.Rows[e.RowIndex].Cells[gvDemoDetails.Columns["SLNO_Product"].Index].Value);
                        DataRow[] dr = dtProdDemoDetails.Select("SLNO_Product=" + SlNo);

                        SVProductDemoDetails ProdDemoDetl = new SVProductDemoDetails(dr);
                        ProdDemoDetl.objfrmSchoolVisits = this;
                        ProdDemoDetl.ShowDialog();

                       
                    }

                }

                if (e.ColumnIndex == gvDemoDetails.Columns["Del_ProdDemoDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvDemoDetails.Rows[e.RowIndex].Cells[gvDemoDetails.Columns["SLNO_Product"].Index].Value);
                        DataRow[] dr = dtProdDemoDetails.Select("SLNO_Product=" + SlNo);
                        dtProdDemoDetails.Rows.Remove(dr[0]);
                        GetProductDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void gvAttendedEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               
                if (e.ColumnIndex == gvAttendedEmpDetails.Columns["Del_EmpDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvAttendedEmpDetails.Rows[e.RowIndex].Cells[gvAttendedEmpDetails.Columns["SlNo_Emp"].Index].Value);
                        DataRow[] dr = dtAttEmpDetails.Select("SlNo_Emp=" + SlNo);
                        dtAttEmpDetails.Rows.Remove(dr[0]);
                        GetEmpDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void gvStaffDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvStaffDetails.Columns["Edit_StaffDetl"].Index)
                {
                    if (Convert.ToBoolean(gvStaffDetails.Rows[e.RowIndex].Cells["Edit_StaffDetl"].Selected) == true)
                    {
                        
                        int SlNo = Convert.ToInt32(gvStaffDetails.Rows[e.RowIndex].Cells[gvStaffDetails.Columns["SlNo_Staff"].Index].Value);
                        DataRow[] dr = dtSchoolStaffDetl.Select("SlNo_Staff=" + SlNo);

                        SVAttendedSchoolEmpDetails StaffDetl = new SVAttendedSchoolEmpDetails(dr);
                        StaffDetl.objfrmSchoolVisits = this;
                        StaffDetl.ShowDialog();
                       
                    }

                }

                if (e.ColumnIndex == gvStaffDetails.Columns["Del_StaffDetl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvStaffDetails.Rows[e.RowIndex].Cells[gvStaffDetails.Columns["SlNo_Staff"].Index].Value);
                        DataRow[] dr = dtSchoolStaffDetl.Select("SlNo_Staff=" + SlNo);
                        dtSchoolStaffDetl.Rows.Remove(dr[0]);
                        GetSchoolStaffDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void gvGiftDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvGiftDetails.Columns["Edit_GiftDetl"].Index)
                {
                    if (Convert.ToBoolean(gvGiftDetails.Rows[e.RowIndex].Cells["Edit_GiftDetl"].Selected) == true)
                    {
                       
                        int SlNo = Convert.ToInt32(gvGiftDetails.Rows[e.RowIndex].Cells[gvGiftDetails.Columns["SLNo_Gift"].Index].Value);
                        DataRow[] dr = dtGiftDetails.Select("SLNo_Gift=" + SlNo);

                        SVGiftDetails GiftDetl = new SVGiftDetails(dr);
                        GiftDetl.objfrmSchoolVisits = this;
                        GiftDetl.ShowDialog();


                    }

                }

                if (e.ColumnIndex == gvGiftDetails.Columns["Del_GiftDetl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvGiftDetails.Rows[e.RowIndex].Cells[gvGiftDetails.Columns["SLNo_Gift"].Index].Value);
                        DataRow[] dr = dtGiftDetails.Select("SLNo_Gift=" + SlNo);
                        dtGiftDetails.Rows.Remove(dr[0]);
                        GetGiftDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void gvStudentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvStudentDetails.Columns["Edit_StudentDetl"].Index)
                {
                    if (Convert.ToBoolean(gvStudentDetails.Rows[e.RowIndex].Cells["Edit_StudentDetl"].Selected) == true)
                    {
                       
                        int SlNo = Convert.ToInt32(gvStudentDetails.Rows[e.RowIndex].Cells[gvStudentDetails.Columns["SLNo_Student"].Index].Value);
                        DataRow[] dr = dtStudentDetails.Select("SLNo_Student=" + SlNo);

                        SVAttendedStudentDetails StudentDetl = new SVAttendedStudentDetails(dr);
                        StudentDetl.objfrmSchoolVisits = this;
                        StudentDetl.ShowDialog();

                    }

                }

                if (e.ColumnIndex == gvStudentDetails.Columns["Del_StudentDetl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvStudentDetails.Rows[e.RowIndex].Cells[gvStudentDetails.Columns["SLNo_Student"].Index].Value);
                        DataRow[] dr = dtStudentDetails.Select("SLNo_Student=" + SlNo);
                        dtStudentDetails.Rows.Remove(dr[0]);
                        GetStudentDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }

        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            //cbCompany.SelectedIndex = 0;
            //cbBranches.SelectedIndex = -1;

            dtpTrnDate.Value = DateTime.Today;            
            txtEcodeSearch.Text = "";
            txtSchoolName.Text = "";
            cbEcode.SelectedIndex = -1;
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            txtHouseNo.Text = "";
            txtLandMark.Text = "";
            txtPrincipalDesig.Text = "";
            txtPrincipalName.Text = "";
            txtPhoneNo.Text = "";

            dtGiftDetails.Rows.Clear();
            dtSchoolStaffDetl.Rows.Clear();
            dtStudentDetails.Rows.Clear();
            dtProdDemoDetails.Rows.Clear();
            dtGiftDetails.Rows.Clear();

            gvAttendedEmpDetails.Rows.Clear();
            gvStaffDetails.Rows.Clear();
            gvDemoDetails.Rows.Clear();
            gvStudentDetails.Rows.Clear();
            gvGiftDetails.Rows.Clear();
            GenerateTransactionNo();
        }

        private void txtSchoolName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);           
        }

        private void txtPrincipalName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtPrincipalDesig_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar!=',')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtHouseNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iResult = 0;

            string strCommand = "";


            if (txtTrnNo.Text.Length > 21 && flagUpdate==true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                    "School Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        
                        strCommand = "DELETE FROM SERVICES_SCHOOL_VISIT_ATTENDENTS WHERE SSVA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                       

                        strCommand += " DELETE FROM SERVICES_SCHOOL_VISIT_GIFTS WHERE SSVG_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";                      

                        strCommand += " DELETE FROM SERVICES_SCHOOL_VISIT_PRODUCTS WHERE SSVP_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                       
                        strCommand += " DELETE FROM SERVICES_SCHOOL_VISIT_HEAD WHERE SSVH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        if (strCommand.Length > 10)
                        {
                            iResult = objSQLdb.ExecuteSaveData(strCommand);
                        }
                        if (iResult > 0)
                        {
                            MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            GenerateTransactionNo();
                            btnCancel_Click(null, null);
                            grpr1.Visible = true;
                            grpr2.Visible = false;
                        }
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

        private void dtpTrnDate_ValueChanged(object sender, EventArgs e)
        {

        }

       
             
    }
}
