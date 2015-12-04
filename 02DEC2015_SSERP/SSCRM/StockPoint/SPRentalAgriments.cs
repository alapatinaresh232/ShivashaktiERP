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
    public partial class SPRentalAgriments : Form
    {
        private StockPointDB objSPDB = null;
        private ServiceDeptDB objServicedb = null;
        private SQLDB objDB = null;
        ReportViewer childReportViewer = null;
        private bool isUpdate = false;       
        DateTime dtpStDate;
        DateTime dtpEndDate;

        public SPRentalAgriments()
        {
            InitializeComponent();
        }

        private void SPRentalAgriments_Load(object sender, EventArgs e)
        {
            gvLicence.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            FillCompanyData();            
            FillRentalAgrimentDataToGrid();
            CalculateRentPerc();
            getMonthlyIncPer();
            cmbTrnType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            dtpFromDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtTrnNo.Text = GenNewTrnNo();
            cbCompany.SelectedValue = CommonData.CompanyCode;
            cbCompany_SelectedIndexChanged(null, null);
            cmbBranch.SelectedValue = CommonData.BranchCode;
        }

        private void FillCompanyData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objDB.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
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
                objDB = null;
                dt = null;
            }
        }       



        private void FillBranchList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetBranchList();
                cmbBranch.DataSource = dt;
                cmbBranch.DisplayMember = "BRANCH_NAME";
                cmbBranch.ValueMember = "BRANCH_CODE";
                if (dt.Rows.Count > 0)
                    cmbBranch.SelectedIndex = 0;
                

                FillAddress();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillAddress()       
        {
            objDB = new SQLDB();
            try
            {
                if (cmbBranch.SelectedIndex >= 0)
                {
                   string[] str = cmbBranch.SelectedValue.ToString().Split('@');

                   DataSet dsRentalAgriAddData = objDB.ExecuteDataSet(" SELECT SPRAD_GODOWN_ADDRESS  BranchAdd " +
                                                  ",SPRAD_GODOWN_LOCATION Location " +
                                                  ",SPRAD_GODOWN_MONDAL Mandal " +
                                                  ",SPRAD_GODOWN_DISTRICT District  " +
                                                  ",SPRAD_GODOWN_STATE State " +
                                                  ",SPRAD_GODOWN_PIN  Pin " +
                                                  //",SPRAD_OWNER_NAME  OwnerName " +
                                                  //",SPRAD_MOBILE_NO  MobileNo " +
                                                  //",SPRAD_BANK_ACC_NO  BankAccNo " +
                                                  //",SPRAD_AREA_SFT  Area " +
                                                  //",SPRAD_CAPACITY_BAGS  Bags  " +

                                                  " FROM SP_RENTAL_AGRIMENTS_DETL WHERE SPRAD_BRANCH_CODE= '" + str[1] + "'");
                     if (dsRentalAgriAddData.Tables[0].Rows.Count ==0)
                     {
                         dsRentalAgriAddData = objDB.ExecuteDataSet("SELECT BRANCH_ADDRESS BranchAdd" +
                                                     ",LOCATION Location"+
                                                     ",MANDAL Mandal"+
                                                     ",DISTRICT District"+
                                                     ",STATE State"+
                                                     ",PIN Pin"+
                                                     " FROM BRANCH_MAS WHERE ACTIVE='T' AND BRANCH_CODE='" + str[1] + "'");
                         txtPreMonthlyRent.Text = "";

                     }         
                       if (dsRentalAgriAddData.Tables[0].Rows.Count>0)
                       {
                        txtBranchAdd.Text = dsRentalAgriAddData.Tables[0].Rows[0]["BranchAdd"].ToString();
                        txtLocation.Text = dsRentalAgriAddData.Tables[0].Rows[0]["Location"].ToString();
                        txtMandal.Text = dsRentalAgriAddData.Tables[0].Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dsRentalAgriAddData.Tables[0].Rows[0]["District"].ToString();
                        txtState.Text = dsRentalAgriAddData.Tables[0].Rows[0]["State"].ToString();
                        txtPin.Text = dsRentalAgriAddData.Tables[0].Rows[0]["Pin"].ToString();
                        //if (gvLicence.Rows.Count > 0)
                        //{
                        //    txtOwnerName.Text = dsRentalAgriAddData.Tables[0].Rows[0]["OwnerName"].ToString();
                        //    txtMobileNo.Text = dsRentalAgriAddData.Tables[0].Rows[0]["MobileNo"].ToString();
                        //    txtBankAccNo.Text = dsRentalAgriAddData.Tables[0].Rows[0]["BankAccNo"].ToString();
                        //    txtArea.Text = dsRentalAgriAddData.Tables[0].Rows[0]["Area"].ToString();
                        //    txtBags.Text = dsRentalAgriAddData.Tables[0].Rows[0]["Bags"].ToString();
                        //}
                       


                       }
                       else
                       {
                           txtBranchAdd.Text = "";
                           txtLocation.Text = "";
                           txtMandal.Text = "";
                           txtDistrict.Text = "";
                           txtState.Text = "";
                           txtPin.Text = "";
                           txtPreMonthlyRent.Text = "";
                           txtMobileNo.Text = "";
                           txtBankAccNo.Text = "";
                           txtArea.Text = "";
                           txtBags.Text = "";
                       }
                    
                 
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private DataTable GetBranchList()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            string sqlText = "";
            try
            {

                if (CommonData.LogUserId.ToUpper() == "ADMIN")
                    sqlText = "SELECT COMPANY_CODE+'@'+BRANCH_CODE+'@'+BRANCH_TYPE BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS " +
                              " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                               "' ORDER BY BRANCH_NAME ASC";
                else
                    sqlText = "SELECT COMPANY_CODE+'@'+BRANCH_CODE+'@'+BRANCH_TYPE BRANCH_CODE,BRANCH_NAME FROM USER_BRANCH " +
                               " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                               " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                               "' AND UB_USER_ID = '" + CommonData.LogUserId +
                               "' ORDER BY BRANCH_NAME ASC ";
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        private string GenNewTrnNo()
        {
            string iMax = "0";
            objDB = new SQLDB();
            string[] sBrDetl = null;
            sBrDetl = cmbBranch.SelectedValue.ToString().Split('@');
            string sqlText = "SELECT ISNULL(MAX(SPRAD_TRN_NO),0)+1 FROM SP_RENTAL_AGRIMENTS_DETL WHERE SPRAD_BRANCH_CODE = '" + sBrDetl[1] + "'";
            try
            {
                iMax = objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDB = null;
            }
            return iMax;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.Items.Count > 0 && cmbBranch.SelectedIndex > -1 && cmbBranch.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                FillAddress();

                FillRentalAgrimentDataToGrid();
                CalculateRentPerc();
                getMonthlyIncPer();
               
                txtTrnNo.Text = GenNewTrnNo();
            }
            
        }
      
        private void CalculateRentPerc()
        {

            txtPreMonthlyRent.Text = "0";

            if (gvLicence.Rows.Count > 0)
            {
                for (int i = 0; i < gvLicence.Rows.Count; i++)
                {
                    dtpStDate = Convert.ToDateTime(gvLicence.Rows[i].Cells["ValidTo"].Value.ToString());
                    if (i != 0)
                        dtpEndDate = Convert.ToDateTime(gvLicence.Rows[i - 1].Cells["ValidTo"].Value.ToString());
                    if (i == 0)
                        dtpEndDate = Convert.ToDateTime(gvLicence.Rows[i].Cells["ValidTo"].Value.ToString());


                    if (txtPreMonthlyRent.Text.Equals("0"))
                    {
                        if (dtpEndDate >= dtpStDate)
                        {
                            if (dtpStDate < dtpFromDate.Value)
                            {
                                txtPreMonthlyRent.Text = gvLicence.Rows[i].Cells["MonthlyRent"].Value.ToString();
                                txtOwnerName.Text = gvLicence.Rows[i].Cells["OwnerName"].Value.ToString();
                              
                                txtArea.Text = gvLicence.Rows[i].Cells["Area"].Value.ToString();
                                txtBags.Text = gvLicence.Rows[i].Cells["Bags"].Value.ToString();
                                txtMobileNo.Text = gvLicence.Rows[i].Cells["OwnerMobileNo"].Value.ToString();
                                txtBankAccNo.Text = gvLicence.Rows[i].Cells["BankAccNo"].Value.ToString();
                            }
                            else if (dtpStDate == dtpFromDate.Value)
                            {
                                txtPreMonthlyRent.Text = gvLicence.Rows[i].Cells["MonthlyRent"].Value.ToString();
                                txtOwnerName.Text = gvLicence.Rows[i].Cells["OwnerName"].Value.ToString();
                                txtMobileNo.Text = gvLicence.Rows[i].Cells["OwnerMobileNo"].Value.ToString();
                                txtBankAccNo.Text = gvLicence.Rows[i].Cells["BankAccNo"].Value.ToString();
                                txtArea.Text = gvLicence.Rows[i].Cells["Area"].Value.ToString();
                                txtBags.Text = gvLicence.Rows[i].Cells["Bags"].Value.ToString();
                                return;
                            }

                        }
                    }
                    else
                    {
                        if (dtpStDate >= dtpEndDate)
                        {
                            if (dtpStDate < dtpFromDate.Value)
                            {
                                txtPreMonthlyRent.Text = gvLicence.Rows[i].Cells["MonthlyRent"].Value.ToString();
                            }
                            else if (dtpStDate == dtpFromDate.Value)
                            {
                                txtPreMonthlyRent.Text = gvLicence.Rows[i].Cells["MonthlyRent"].Value.ToString();
                                return;
                            }

                        }

                    }



                }
               
            }
        }
        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateRentPerc();
            getMonthlyIncPer();
           
            //this.gvLicence.Sort(this.gvLicence.Columns["ValidFrom"], ListSortDirection.Ascending);
        }        

        private void FillRentalAgrimentDataToGrid()
        {
            objSPDB = new StockPointDB();
            DataTable dtSpRntDetl = new DataTable();
            string[] sBrDetl = null;
            sBrDetl = cmbBranch.SelectedValue.ToString().Split('@');
            dtSpRntDetl = objSPDB.SPRentalAgrimentDetl_Get(sBrDetl[0],sBrDetl[1],0).Tables[0];
            gvLicence.Rows.Clear();
            if (dtSpRntDetl.Rows.Count > 0)
            {
                for (int i = 0; i < dtSpRntDetl.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                    cellSlNo.Value = i + 1;
                    tempRow.Cells.Add(cellSlNo);

                    DataGridViewCell cellBrCode = new DataGridViewTextBoxCell();
                    cellBrCode.Value = dtSpRntDetl.Rows[i]["SPRAD_BRANCH_CODE"];
                    tempRow.Cells.Add(cellBrCode);

                    DataGridViewCell cellTrnNo = new DataGridViewTextBoxCell();
                    cellTrnNo.Value = dtSpRntDetl.Rows[i]["SPRAD_TRN_NO"];
                    tempRow.Cells.Add(cellTrnNo);

                    DataGridViewCell cellTrnType = new DataGridViewTextBoxCell();
                    cellTrnType.Value = dtSpRntDetl.Rows[i]["SPRAD_TRN_TYPE"];
                    tempRow.Cells.Add(cellTrnType);

                    DataGridViewCell cellVFrom = new DataGridViewTextBoxCell();
                    cellVFrom.Value = Convert.ToDateTime(dtSpRntDetl.Rows[i]["SPRAD_FROM"]).ToString("dd-MMM-yyyy").ToUpper();
                    tempRow.Cells.Add(cellVFrom);

                    DataGridViewCell cellVTo = new DataGridViewTextBoxCell();
                    cellVTo.Value = Convert.ToDateTime(dtSpRntDetl.Rows[i]["SPRAD_TO"]).ToString("dd-MMM-yyyy").ToUpper();
                    tempRow.Cells.Add(cellVTo);

                    DataGridViewCell cellAddrs = new DataGridViewTextBoxCell();
                    cellAddrs.Value = dtSpRntDetl.Rows[i]["SPRAD_GODOWN_ADDRESS"];
                    tempRow.Cells.Add(cellAddrs);

                    DataGridViewCell cellLoc = new DataGridViewTextBoxCell();
                    cellLoc.Value = dtSpRntDetl.Rows[i]["SPRAD_GODOWN_LOCATION"];
                    tempRow.Cells.Add(cellLoc);

                    DataGridViewCell cellDistr = new DataGridViewTextBoxCell();
                    cellDistr.Value = dtSpRntDetl.Rows[i]["SPRAD_GODOWN_DISTRICT"];
                    tempRow.Cells.Add(cellDistr);

                    DataGridViewCell cellMand = new DataGridViewTextBoxCell();
                    cellMand.Value = dtSpRntDetl.Rows[i]["SPRAD_GODOWN_MONDAL"];
                    tempRow.Cells.Add(cellMand);

                    DataGridViewCell cellState = new DataGridViewTextBoxCell();
                    cellState.Value = dtSpRntDetl.Rows[i]["SPRAD_GODOWN_STATE"];
                    tempRow.Cells.Add(cellState);

                    DataGridViewCell cellPin = new DataGridViewTextBoxCell();
                    cellPin.Value = dtSpRntDetl.Rows[i]["SPRAD_GODOWN_PIN"];
                    tempRow.Cells.Add(cellPin);

                    DataGridViewCell cellOwner = new DataGridViewTextBoxCell();
                    cellOwner.Value = dtSpRntDetl.Rows[i]["SPRAD_OWNER_NAME"];
                    tempRow.Cells.Add(cellOwner);
                  
                    DataGridViewCell cellSDAmt = new DataGridViewTextBoxCell();
                    cellSDAmt.Value = dtSpRntDetl.Rows[i]["SPRAD_SD_AMT"];
                    tempRow.Cells.Add(cellSDAmt);

                    DataGridViewCell cellMnthlyAmt = new DataGridViewTextBoxCell();
                    cellMnthlyAmt.Value = dtSpRntDetl.Rows[i]["SPRAD_MTLY_RNT_AMT"];
                    tempRow.Cells.Add(cellMnthlyAmt);

                    DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                    cellStatus.Value = dtSpRntDetl.Rows[i]["SPRAD_AGGRIMENT_STATUS"];
                    tempRow.Cells.Add(cellStatus);

                    DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                    cellRemarks.Value = dtSpRntDetl.Rows[i]["SPRAD_REMARKS"];
                    tempRow.Cells.Add(cellRemarks);
                    DataGridViewCell cellArea = new DataGridViewTextBoxCell();
                    cellArea.Value = dtSpRntDetl.Rows[i]["SPRAD_AREA_SFT"];
                    tempRow.Cells.Add(cellArea);

                    DataGridViewCell cellBags = new DataGridViewTextBoxCell();
                    cellBags.Value = dtSpRntDetl.Rows[i]["SPRAD_CAPACITY_BAGS"];
                    tempRow.Cells.Add(cellBags);

                    DataGridViewCell cellMobileNo = new DataGridViewTextBoxCell();
                    cellMobileNo.Value = dtSpRntDetl.Rows[i]["SPRAD_MOBILE_NO"];
                    tempRow.Cells.Add(cellMobileNo);
                    DataGridViewCell cellBankAccNo = new DataGridViewTextBoxCell();
                    cellBankAccNo.Value = dtSpRntDetl.Rows[i]["SPRAD_BANK_ACC_NO"];
                    tempRow.Cells.Add(cellBankAccNo);


                    gvLicence.Rows.Add(tempRow);
                }
            }
            objSPDB = null;
            dtSpRntDetl = null;
        }

        private bool CheckData()
        {
            bool bflag = true;
            if (cmbBranch.Items.Count == 0 || cmbBranch.SelectedIndex < 0)
            {
                MessageBox.Show("Select Stock Point", "SP RentalAgriments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbTrnType.Items.Count == 0 || cmbTrnType.SelectedIndex < 0)
            {
                MessageBox.Show("Select Transaction Type", "SP RentalAgriments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                cmbTrnType.Focus();
            }
            if (Convert.ToDateTime(dtpFromDate.Value) >= Convert.ToDateTime(dtpToDate.Value))
            {
                MessageBox.Show("Invalid From/To Date", "SP RentalAgriments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtBranchAdd.Text.Length == 0)
            {
                MessageBox.Show("Enter Stock Point Address", "SP RentalAgriments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                txtBranchAdd.Focus();
            }
            if (txtDistrict.Text.Length == 0 || txtLocation.Text.Length == 0 || txtMandal.Text.Length == 0 || txtPin.Text.Length == 0 || txtState.Text.Length == 0)
            {
                MessageBox.Show("Enter Location/District/Mandal/State/Pin Address", "SP RentalAgriments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVillSearch.Focus();
                return false;
            }
            if (txtMonthlyRent.Text.Length == 0)
            {
                MessageBox.Show("Enter Stock Point Monthly Rental Amount", "SP RentalAgriments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                txtMonthlyRent.Focus();
            }
            if (txtSDAmt.Text.Length == 0)
            {
                MessageBox.Show("Enter Stock Point Security Deposit Amount Details", "SP RentalAgriments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                txtSDAmt.Focus();
            }
            return bflag;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            objSPDB = new StockPointDB();
            string sqlText = "";
            int iRes = 0;
            objDB = new SQLDB();
            if (CheckData()==true)
            {                
                try
                {
                    string[] sBrDetl = null;
                    sBrDetl = cmbBranch.SelectedValue.ToString().Split('@');
                    string sStatus = "";
                    if (cmbStatus.SelectedIndex == 0)
                        sStatus = "R";
                    else if (cmbStatus.SelectedIndex == 1)
                        sStatus = "C";
                    if (txtArea.Text.Length == 0)
                    {
                        txtArea.Text = "0"; 
                    }
                    if (txtBags.Text.Length == 0)
                    {
                        txtBags.Text = "0"; 
                    }

                    if (cmbTrnType.Text.Equals("NEW"))
                    {
                        sqlText += "UPDATE BRANCH_MAS SET BRANCH_ADDRESS='"+ txtBranchAdd.Text.ToString() +
                                    "',LOCATION='"+ txtLocation.Text.ToString() +
                                    "',MANDAL='"+ txtMandal.Text.ToString() +
                                    "',DISTRICT='"+ txtDistrict.Text.ToString() +
                                    "',STATE='"+ txtState.Text.ToString() +
                                    "',PIN='"+ txtPin.Text.ToString() +
                                    "',LAST_MODIFIED_BY='"+ CommonData.LogUserId +
                                    "',LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "' WHERE COMPANY_CODE='" + sBrDetl[0] + 
                                    "' AND BRANCH_CODE='" + sBrDetl[1] + "'";
                    }

                    if (isUpdate == false)
                    {
                        sqlText += " UPDATE SP_RENTAL_AGRIMENTS_DETL SET SPRAD_AGGRIMENT_STATUS='C' WHERE SPRAD_BRANCH_CODE='" + sBrDetl[1] + "'";
                        sqlText += " INSERT INTO SP_RENTAL_AGRIMENTS_DETL(SPRAD_COMPANY_CODE" +
                                    ",SPRAD_BRANCH_CODE" +
                                    ",SPRAD_TRN_NO" +
                                    ",SPRAD_TRN_TYPE" +
                                    ",SPRAD_FROM" +
                                    ",SPRAD_TO" +
                                    ",SPRAD_GODOWN_ADDRESS" +
                                    ",SPRAD_GODOWN_LOCATION" +
                                    ",SPRAD_GODOWN_MONDAL" +
                                    ",SPRAD_GODOWN_DISTRICT" +
                                    ",SPRAD_GODOWN_STATE" +
                                    ",SPRAD_GODOWN_PIN" +
                                    ",SPRAD_AGGRIMENT_STATUS" +
                                    ",SPRAD_OWNER_NAME" +
                                    ",SPRAD_MOBILE_NO" +
                                    ",SPRAD_BANK_ACC_NO" +
                                    ",SPRAD_SD_AMT" +
                                    ",SPRAD_MTLY_RNT_AMT" +
                                    ",SPRAD_CREATED_BY" +
                                    ",SPRAD_CREATED_DATE "+
                                    ",SPRAD_REMARKS "+
                                    ",SPRAD_AREA_SFT "+
                                    ",SPRAD_CAPACITY_BAGS)" +
                                    " VALUES(" +
                                    "'" + sBrDetl[0] +
                                    "','" + sBrDetl[1] +
                                    "','" + txtTrnNo.Text.ToUpper() +
                                    "','" + cmbTrnType.Text.ToUpper() +
                                    "','" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                    "','" + txtBranchAdd.Text.ToUpper() +
                                    "','" + txtLocation.Text.ToUpper() +
                                    "','" + txtMandal.Text.ToUpper() +
                                    "','" + txtDistrict.Text.ToUpper() +
                                    "','" + txtState.Text.ToUpper() +
                                    "','" + txtPin.Text.ToUpper() +
                                    "','" + sStatus +
                                    "','" + txtOwnerName.Text.ToUpper() +
                                    "','" + txtMobileNo.Text.ToUpper() +
                                    "','" + txtBankAccNo.Text.ToUpper() +
                                    "'," + Convert.ToInt32(txtSDAmt.Text) +
                                    "," + Convert.ToInt32(txtMonthlyRent.Text) +
                                    ",'" + CommonData.LogUserId +
                                    "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "','"+ txtRemarks.Text.ToString().Replace("'","") +"',"+ txtArea.Text.ToString() +
                                    ","+txtBags.Text.ToString()+") ";
                    }
                    else
                    {
                        sqlText += " UPDATE SP_RENTAL_AGRIMENTS_DETL SET SPRAD_TRN_TYPE='" + cmbTrnType.Text.ToUpper() + "'" +
                                    ",SPRAD_FROM='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy") + "'" +
                                    ",SPRAD_TO='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") + "'" +
                                    ",SPRAD_GODOWN_ADDRESS='" + txtBranchAdd.Text.ToUpper() + "'" +
                                    ",SPRAD_GODOWN_LOCATION='" + txtLocation.Text.ToUpper() + "'" +
                                    ",SPRAD_GODOWN_MONDAL='" + txtMandal.Text.ToUpper() + "'" +
                                    ",SPRAD_GODOWN_DISTRICT='" + txtDistrict.Text.ToUpper() + "'" +
                                    ",SPRAD_GODOWN_STATE='" + txtState.Text.ToUpper() + "'" +
                                    ",SPRAD_GODOWN_PIN='" + txtPin.Text.ToUpper() + "'" +
                                    ",SPRAD_OWNER_NAME='" + txtOwnerName.Text.ToUpper() + "'" +
                                    ",SPRAD_MOBILE_NO='" + txtMobileNo.Text.ToUpper() + "'" +
                                    ",SPRAD_BANK_ACC_NO='" + txtBankAccNo.Text.ToUpper() + "'" +
                                    ",SPRAD_AGGRIMENT_STATUS='" + sStatus + "'" +
                                    ",SPRAD_SD_AMT=" + txtSDAmt.Text.ToUpper() +
                                    ",SPRAD_MTLY_RNT_AMT=" + txtMonthlyRent.Text.ToUpper() +
                                    ",SPRAD_REMARKS='"+ txtRemarks.Text.ToString().Replace("'","") +
                                    "',SPRAD_AREA_SFT=" + txtArea.Text.ToUpper() +
                                    ",SPRAD_CAPACITY_BAGS=" + txtBags.Text.ToUpper() +
                                    ",SPRAD_LAST_MODIFIED_BY='"+ CommonData.LogUserId +
                                    "',SPRAD_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "' WHERE SPRAD_BRANCH_CODE='" + sBrDetl[1] +
                                    "' AND SPRAD_TRN_NO=" + txtTrnNo.Text;
                    }

                    if (sqlText.Length > 5)
                    {
                        iRes = objDB.ExecuteSaveData(sqlText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }

                if (iRes == 0)
                {
                    MessageBox.Show("Data Not Saved", "SP Rental Agriment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data Saved Successfully", "SP Rental Agriment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
            }
        }

    
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbBranch.SelectedIndex = 0;
            FillRentalAgrimentDataToGrid();
            CalculateRentPerc();
            getMonthlyIncPer();         
            txtTrnNo.Text = GenNewTrnNo();
            cmbTrnType.SelectedIndex = 0;
            dtpFromDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate); 
            txtBranchAdd.Text = "";
            txtLocation.Text = "";
            txtDistrict.Text = "";
            txtLocation.Text = "";
            txtMandal.Text = "";
            txtMonthlyRent.Text = "";
            txtOwnerName.Text = "";
            txtPin.Text = "";
            txtSDAmt.Text = "";
            txtState.Text = "";
            isUpdate = false;
            txtTrnNo.Enabled = true;
            txtRemarks.Text = "";
            txtVillSearch.Text = "";
            txtBags.Text = "";
            txtArea.Text="";
            txtPreMonthlyRent.Text = "";
            txtRentIncper.Text = "";
            txtMobileNo.Text = "";
            txtBankAccNo.Text = "";
        }

        private void gvLicence_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex>-1 && (e.ColumnIndex == 21 || e.ColumnIndex == 22))
            {
                if (e.ColumnIndex == gvLicence.Columns["Edit"].Index)
                {

                    DataGridViewRow tempRow = new DataGridViewRow();
                    tempRow = gvLicence.Rows[e.RowIndex];
                    txtTrnNo.Text = tempRow.Cells["TrnNo"].Value.ToString();
                    cmbTrnType.SelectedText = tempRow.Cells["TrnType"].Value.ToString();
                    dtpFromDate.Value = Convert.ToDateTime(tempRow.Cells["ValidFrom"].Value);
                    dtpToDate.Value = Convert.ToDateTime(tempRow.Cells["ValidTo"].Value);
                    //for (int i = 0; i < cmbBranch.Items.Count; i++)
                    //{
                    //    string[] sBrDetl = null;
                    //    DataRowView cmbItem = (DataRowView)cmbBranch.Items[i];
                    //    sBrDetl = cmbItem[0].ToString().Split('@');
                    //    //sBrDetl = cmbItem.Value.ToString().Split('@');
                    //    if (sBrDetl[1] == tempRow.Cells["BranchCode"].Value.ToString())
                    //    {
                    //        cmbBranch.SelectedIndex = i;
                    //    }
                    //}
                    if (tempRow.Cells["SPLD_LICENCE_STATUS"].Value.ToString() == "RUNNING")
                        cmbStatus.SelectedIndex = 0;
                    else
                        cmbStatus.SelectedIndex = 1;
                    txtBranchAdd.Text = tempRow.Cells["Address"].Value.ToString();
                    txtDistrict.Text = tempRow.Cells["Dist"].Value.ToString();
                    txtLocation.Text = tempRow.Cells["Location"].Value.ToString();
                    txtMandal.Text = tempRow.Cells["Mandal"].Value.ToString();
                    txtMonthlyRent.Text = tempRow.Cells["MonthlyRent"].Value.ToString();
                    txtOwnerName.Text = tempRow.Cells["OwnerName"].Value.ToString();                   
                    txtPin.Text = tempRow.Cells["Pin"].Value.ToString();
                    txtSDAmt.Text = tempRow.Cells["SDAmt"].Value.ToString();
                    txtState.Text = tempRow.Cells["State"].Value.ToString();
                    txtRemarks.Text = tempRow.Cells["Remarks"].Value.ToString();
                    txtArea.Text = tempRow.Cells["Area"].Value.ToString();
                    txtBags.Text = tempRow.Cells["Bags"].Value.ToString();
                    txtMobileNo.Text = tempRow.Cells["OwnerMobileNo"].Value.ToString();
                    txtBankAccNo.Text = tempRow.Cells["BankAccNo"].Value.ToString();
                    txtTrnNo.Enabled = false;
                    isUpdate = true;
                }
            }
        }

        private void txtSDAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch vilsearch = new VillageSearch("SPRentalAgriments");
            vilsearch.objSPRentalAgriments = this;
            vilsearch.ShowDialog();
        }

        private void FillAddressData(string svilsearch)
        {
            Hashtable htParam = null;
            objServicedb = new ServiceDeptDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            if (txtVillSearch.Text != "")
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (svilsearch.Trim().Length >= 0)

                        htParam = new Hashtable();
                    htParam.Add("sVillage", svilsearch);
                    htParam.Add("sDistrict", strDist);



                    htParam.Add("sCDState", CommonData.StateCode);
                    dsVillage = new DataSet();
                    dsVillage = objServicedb.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    if (dtVillage.Rows.Count == 1)
                    {
                        txtLocation.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                        txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                        txtState.Text = dtVillage.Rows[0]["State"].ToString();
                        txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();

                    }
                    else if (dtVillage.Rows.Count > 1)
                    {
                        txtLocation.Text = "";
                        txtMandal.Text = "";
                        txtDistrict.Text = "";
                        txtState.Text = "";
                        txtPin.Text = "";

                        FillAddressComboBox(dtVillage);
                    }

                    else
                    {
                        htParam = new Hashtable();
                        htParam.Add("sVillage", "%" + svilsearch);
                        htParam.Add("sDistrict", strDist);
                        dsVillage = new DataSet();
                        dsVillage = objServicedb.GetVillageDataSet(htParam);
                        dtVillage = dsVillage.Tables[0];
                        FillAddressComboBox(dtVillage);


                    }


                    Cursor.Current = Cursors.Default;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objServicedb = null;
                    Cursor.Current = Cursors.Default;

                }
            }
            else
            {

                txtLocation.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
            }

        }
        private void FillAddressComboBox(DataTable dt)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));


            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});

            cbVillage.DataBindings.Clear();
            cbVillage.DataSource = dataTable;
            cbVillage.DisplayMember = "Panchayath";
            cbVillage.ValueMember = "StateID";

        }

        private void txtVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillSearch.Text.Length == 0)
                {
                    //cbVillage.DataSource = null;
                    //cbVillage.DataBindings.Clear();
                    //cbVillage.Items.Clear();
                    //txtLocation.Text = "";
                    //txtState.Text = "";
                    //txtDistrict.Text = "";
                    //txtMandal.Text = "";
                    //txtPin.Text = "";

                }

                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {

                    FillAddressData(txtVillSearch.Text);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > -1)
            {
                if (this.cbVillage.Items[cbVillage.SelectedIndex].ToString() != "")
                {
                    txtLocation.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";

                }
            }
        }

        private void txtMonthlyRent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchList();
            }
            else
            {
                cmbBranch.DataSource = null;
            }
        }

        private void txtMonthlyRent_KeyUp(object sender, KeyEventArgs e)
        {
            getMonthlyIncPer();
        }
        private void getMonthlyIncPer()
        {
            if ( txtMonthlyRent.Text.Length!=0)
            {
                if (txtPreMonthlyRent.Text.Length != 0)
                {
                    double RentIncPer = 0;
                    double PrevoiusRent = 0;
                    double MonthlyRent = 0;

                    PrevoiusRent = Convert.ToDouble(txtPreMonthlyRent.Text.ToString());
                    MonthlyRent = Convert.ToDouble(txtMonthlyRent.Text.ToString());
                    if (PrevoiusRent == MonthlyRent)
                    {
                        RentIncPer = 0;
                        txtRentIncper.Text = RentIncPer.ToString("f");
                    }
                    else
                    {
                        if (PrevoiusRent != 0)
                        {
                            RentIncPer = ((MonthlyRent - PrevoiusRent) / PrevoiusRent * 100);
                            txtRentIncper.Text = RentIncPer.ToString("f");
                        }
                        else
                        {
                            txtRentIncper.Text = "0";
                        }

                    }
                }
            }
            else
            {
              txtRentIncper.Text="0";
            }
        }

        private void txtMonthlyRent_TextChanged(object sender, EventArgs e)
        {
            getMonthlyIncPer();
        }

        private void txtOwnerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                if (cmbBranch.SelectedIndex > -1)
                {

                    CommonData.ViewReport = "SSERP_REP_SP_RENTAL_AGRIMENT_DETAILS";
                    childReportViewer = new ReportViewer(cbCompany.SelectedValue.ToString(), cmbBranch.SelectedValue.ToString().Split('@')[1], "", "", "", "");
                    childReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
     
       
    }
}
