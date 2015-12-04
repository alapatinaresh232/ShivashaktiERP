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
using SSAdmin;
using System.IO;
namespace SSCRM
{
    public partial class VehicleLegalNotice : Form
    {

        private SQLDB objDB = null;
        string sCompCode = string.Empty;
        string sBranchCode = string.Empty;
        DateTime sLastNotice = DateTime.Now.AddDays(-360);
        //string s
        public VehicleLegalNotice()
        {
            InitializeComponent();
        }

        private void VehicleLegalNotice_Load(object sender, EventArgs e)
        {
            gvNotices.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            gvNotices.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
        }

        private void txtDsearch_Validated(object sender, EventArgs e)
        {
            if (txtDsearch.Text.ToString().Length > 4)
            {
                DataTable dt = null;
                DataTable dtVeclData = null;
                
                dt = GetEmpData();
                objDB = new SQLDB();
                if (dt.Rows.Count > 0)
                {
                    txtMemberName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    txtFatherName.Text = dt.Rows[0]["FATHER_NAME"].ToString();
                    txtHRISBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                    txtEoraCode.Text = dt.Rows[0]["EORACODE"].ToString();
                    txtHRISDesig.Text = dt.Rows[0]["DESIG"].ToString();
                    sCompCode = dt.Rows[0]["company_code"].ToString();
                    sBranchCode = dt.Rows[0]["BRANCH_CODE"].ToString();
                    meHRISDataofBirth.Text = Convert.ToDateTime(dt.Rows[0]["EMP_DOB"]).ToString("dd/MM/yyyy");
                    meHRISDateOfJoin.Text = Convert.ToDateTime(dt.Rows[0]["EMP_DOJ"]).ToString("dd/MM/yyyy");
                    txtStatus.Text = dt.Rows[0]["sStatus"].ToString();
                    txtAddress.Text = "";

                    if (dt.Rows[0]["HouseNo"].ToString().Length > 0)
                        txtAddress.Text = dt.Rows[0]["HouseNo"].ToString();
                    if (dt.Rows[0]["LandMark"].ToString().Length > 0 && txtAddress.Text.Length==0)
                        txtAddress.Text = dt.Rows[0]["LandMark"].ToString();
                    else if(dt.Rows[0]["LandMark"].ToString().Length > 0 && txtAddress.Text.Length>0)
                        txtAddress.Text += ", "+dt.Rows[0]["LandMark"].ToString();

                    if (dt.Rows[0]["Village"].ToString().Length > 0 && txtAddress.Text.Length == 0)
                        txtAddress.Text = dt.Rows[0]["Village"].ToString();
                    else if (dt.Rows[0]["Village"].ToString().Length > 0 && txtAddress.Text.Length > 0)
                        txtAddress.Text += ", " + dt.Rows[0]["Village"].ToString();

                    if (dt.Rows[0]["Mandal"].ToString().Length > 0 && txtAddress.Text.Length == 0)
                        txtAddress.Text = dt.Rows[0]["Mandal"].ToString();
                    else if (dt.Rows[0]["Mandal"].ToString().Length > 0 && txtAddress.Text.Length > 0)
                        txtAddress.Text += ", " + dt.Rows[0]["Mandal"].ToString();

                    if (dt.Rows[0]["District"].ToString().Length > 0 && txtAddress.Text.Length == 0)
                        txtAddress.Text = dt.Rows[0]["District"].ToString();
                    else if (dt.Rows[0]["District"].ToString().Length > 0 && txtAddress.Text.Length > 0)
                        txtAddress.Text += ", " + dt.Rows[0]["District"].ToString();

                    if (dt.Rows[0]["State"].ToString().Length > 0 && txtAddress.Text.Length == 0)
                        txtAddress.Text = dt.Rows[0]["State"].ToString();
                    else if (dt.Rows[0]["State"].ToString().Length > 0 && txtAddress.Text.Length > 0)
                        txtAddress.Text += ", " + dt.Rows[0]["State"].ToString();

                    if (dt.Rows[0]["PinNo"].ToString().Length > 0 && txtAddress.Text.Length == 0)
                        txtAddress.Text = dt.Rows[0]["PinNo"].ToString();
                    else if (dt.Rows[0]["PinNo"].ToString().Length > 0 && txtAddress.Text.Length > 0)
                        txtAddress.Text += ", " + dt.Rows[0]["PinNo"].ToString();

                    txtAddress.Text = txtAddress.Text.Replace(System.Environment.NewLine, "");
                    txtEmailId.Text = dt.Rows[0]["eMailId"].ToString();
                    txtContNo.Text = dt.Rows[0]["MobileNo"].ToString();

                    DataSet dsPhoto = new DataSet();
                    dsPhoto = objDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + txtDsearch.Text);

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                        GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                    else
                        picEmpPhoto.BackgroundImage = null;
                    //ControlsEnableDisable(false);
                }
                else
                {
                    txtMemberName.Text = "";
                    txtFatherName.Text = "";
                    txtHRISBranch.Text = "";
                    txtHRISDesig.Text = "";
                    txtEoraCode.Text = "";
                    meHRISDataofBirth.Text = "";
                    meHRISDateOfJoin.Text = "";
                    picEmpPhoto.BackgroundImage = null;
                }
                dtVeclData = GetEmpVehcleData();
                if (dtVeclData.Rows.Count > 0)
                {
                   // IsAvailable = true;

                    txtVehicleNo.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_REG_NUMBER"].ToString();
                    try { txtRegDate.Text = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_VEHICLE_REG_DATE"].ToString()).ToString("dd-MMM-yyyy"); }
                    catch { txtRegDate.Text = ""; }
                    txtVehType.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_CLASS"].ToString();
                    txtVehMake.Text = dtVeclData.Rows[0]["VM_VEHICLE_MAKE"].ToString();
                    txtVehModel.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_MAKERS_CLASS"].ToString();
                    txtEngNo.Text = dtVeclData.Rows[0]["HVLH_ENGINE_NO"].ToString();
                    txtChassisNo.Text = dtVeclData.Rows[0]["HVLH_CHASSIS_NO"].ToString();
                    
                    //if (dtVeclData.Rows[0]["HVLH_OWNERSHIP"].ToString() == "OWN")
                    //    cbOwnership.SelectedIndex = 1;
                    //else
                    //    cbOwnership.SelectedIndex = 0;
                    txtRCName.Text = dtVeclData.Rows[0]["HVLH_REGD_OWNER"].ToString();
                    //if (dtVeclData.Rows[0]["HVLH_DRIVING_LICENCE"].ToString().Trim() == "")
                    //{
                    //    txtLicenceNo.Text = dtVeclData.Rows[0]["HVLH_LEARNING_LICENCE"].ToString();
                    //    chkLLR.Checked = true;
                    //}
                    //else
                    //    txtLicenceNo.Text = dtVeclData.Rows[0]["HVLH_DRIVING_LICENCE"].ToString();
                    //if (dtVeclData.Rows[0]["HVLH_DRIVING_VALID_TO"].ToString().Trim() == "")
                    //{
                    //    dtpDLVatidTo.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_LEARNING_VALID_TO"].ToString());
                    //    chkLLR.Checked = false;
                    //}
                    //else
                    //    dtpDLVatidTo.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_DRIVING_VALID_TO"].ToString());
                    //if (dtVeclData.Rows[0]["HVLH_DRIVING_VALID_FROM"].ToString().Trim() == "")
                    //    dtpDLValidFrom.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_LEARNING_VALID_FROM"].ToString());
                    //else
                    //    dtpDLValidFrom.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_DRIVING_VALID_FROM"].ToString());
                    txtLoanSanctioned.Text = dtVeclData.Rows[0]["HVLH_LOAN_AMT"].ToString();
                    txtDepositAmt.Text = dtVeclData.Rows[0]["HVLH_EMPLOYEE_DEPOSIT"].ToString();
                    txtMonthlyRecAmt.Text = dtVeclData.Rows[0]["HVLH_EMI"].ToString();
                    //cbVehicleStatus.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_CLASS"].ToString();
                    txtVehicleCost.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_COST"].ToString();
                    try {Convert.ToDouble(dtVeclData.Rows[0]["sRecovery"].ToString());
                    txtAmtRecoveredTillDate.Text = (Convert.ToDouble(dtVeclData.Rows[0]["HVLH_LOAN_RECOVERY_CUTOFFDATE"].ToString()) + Convert.ToDouble(dtVeclData.Rows[0]["sRecovery"].ToString())).ToString("f");}
                        catch{txtAmtRecoveredTillDate.Text=dtVeclData.Rows[0]["HVLH_LOAN_RECOVERY_CUTOFFDATE"].ToString();}
                    
                    txtBalanceAmt.Text = (Convert.ToDouble(dtVeclData.Rows[0]["HVLH_LOAN_AMT"].ToString()) - Convert.ToDouble(txtAmtRecoveredTillDate.Text)).ToString("f");
                    //cbOrigRCStatus.SelectedText = dtVeclData.Rows[0]["HVLH_RC_CLASS"].ToString();
                    //cbVehicleType.Text = dtVeclData.Rows[0]["VM_VEHICLE_TYPE"].ToString();
                    //if (dtVeclData.Rows[0]["HVLH_PETROL_REQUEST_FLAG"].ToString() == "Y")
                    //{
                    //    cbPetrolAllow.SelectedIndex = 0;
                    //    dtpPetrolAllowReqFrom.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_PETROL_INCN_REQUEST_DATE"].ToString());
                    //    modifyAllw = true;
                    //}
                    //else if (dtVeclData.Rows[0]["HVLH_PETROL_REQUEST_FLAG"].ToString() == "N")
                    //{
                    //    cbPetrolAllow.SelectedIndex = 1;
                    //    modifyAllw = true;
                    //}
                    //else
                    //{
                    //    modifyAllw = false;
                    //}

                    //if (dtVeclData.Rows[0]["HVLH_INCN_REQUEST_FLAG"].ToString() == "Y")
                    //{
                    //    cbIncentiveFlag.SelectedIndex = 0;
                    //    modifyIncen = true;
                    //}
                    //else if (dtVeclData.Rows[0]["HVLH_INCN_REQUEST_FLAG"].ToString() == "N")
                    //{
                    //    cbIncentiveFlag.SelectedIndex = 1;
                    //    modifyIncen = true;
                    //}
                    //else
                    //{
                    //    modifyIncen = false;
                    //}
                    //txtIncentiveReqAmt.Text = dtVeclData.Rows[0]["HVLH_INCN_REQUEST_AMT"].ToString();

                    //if (CommonData.LogUserId.ToUpper() != "ADMIN" && CommonData.LogUserRole != "MANAGEMENT")
                    //    ControlsEnableDisable(true);
                }
                else
                {
                    ClearForm();
                    //IsAvailable = false;
                    //ControlsEnableDisable(false);
                }
                
                FillLegalNoticetoGrid();
            }
            else
            {
                ClearForm();
                MessageBox.Show("Enter Valid Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void FillLegalNoticetoGrid()
        {
            sLastNotice = Convert.ToDateTime("01-01-1999");
            DataTable dtLegNotice = null;
            gvNotices.Rows.Clear();
            dtLegNotice = GetLegalNoticeData();
            if (dtLegNotice.Rows.Count > 0)
            {
                foreach (DataRow dr in dtLegNotice.Rows)
                {
                    gvNotices.Rows.Add();
                    int intCurRow = gvNotices.Rows.Count - 1;
                    gvNotices.Rows[intCurRow].Cells["dataGridViewTextBoxColumn1"].Value = intCurRow + 1;
                    if (Convert.ToDateTime(dr["NoticeDate"] + "") > sLastNotice)
                        sLastNotice = Convert.ToDateTime(dr["NoticeDate"] + "");
                    gvNotices.Rows[intCurRow].Cells["sDate"].Value = Convert.ToDateTime(dr["NoticeDate"] + "").ToString("dd-MMM-yyyy");
                    gvNotices.Rows[intCurRow].Cells["sNoticeNo"].Value = dr["NoticeNo"] + "";
                    gvNotices.Rows[intCurRow].Cells["sCreatedBy"].Value = dr["CreatedBy"] + "";
                    gvNotices.Rows[intCurRow].Cells["sRemarks"].Value = dr["Remarks"] + "";
                }
            }

        }

        private DataTable GetLegalNoticeData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string sqlText = "SELECT HVLLN_CREATED_DATE NoticeDate, HVLLN_NOTICE_REF_NO NoticeNo" +
                                    ", HVLLN_CREATED_BY+'--'+MEMBER_NAME CreatedBy, HVLLN_REMARKS Remarks" +
                                    " FROM HR_VEHICLE_LOAN_LGL_NOTICE" +
                                    " INNER JOIN USER_MASTER ON UM_USER_ID = HVLLN_CREATED_BY" +
                                    " INNER JOIN EORA_MASTER ON ECODE = UM_ECODE" +
                                    " WHERE HVLLN_EORA_CODE = " + txtEoraCode.Text;
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return dt;
            }
            finally
            {
                objDB = null;
                dt = null;
            }
            //throw new NotImplementedException();
        }
        private void ClearForm()
        {
            txtChassisNo.Text = string.Empty;
            txtEngNo.Text = string.Empty;
            txtBalanceAmt.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            sCompCode = string.Empty;
            sBranchCode = string.Empty;
            picEmpPhoto.BackgroundImage = null;
            txtAddress.Text = "";
            txtEmailId.Text = "";
            txtContNo.Text = "";
            txtFatherName.Text = "";
            txtHRISBranch.Text = "";
            txtHRISDesig.Text = "";
            txtMemberName.Text = "";
            meHRISDataofBirth.Text = "";
            meHRISDateOfJoin.Text = "";
            txtVehicleNo.Text = "";
            txtRCName.Text = "";
            txtRegDate.Text = "";
            txtVehType.Text = "";
            txtVehModel.Text = "";
            txtVehMake.Text = "";
            txtStatus.Text = "";
            //txtDsearch.Text = "";
            //dtpRegDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            //cbVehicleMake.SelectedIndex = 0;
            //cbVehicleModel.SelectedIndex = 0;
            //txtLicenceNo.Text = "";
            //dtpDLVatidTo.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtLoanSanctioned.Text = "";
            txtDepositAmt.Text = "";
            txtMonthlyRecAmt.Text = "";
            //cbVehicleStatus.SelectedIndex = 0;
            //cbOrigRCStatus.SelectedIndex = 0;
            //cbVehicleType.SelectedIndex = 0;
            txtVehicleCost.Text = "";
            txtAmtRecoveredTillDate.Text = "";
            gvNotices.Rows.Clear();
        }

        private DataTable GetEmpVehcleData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string sAcode = "";
                try
                {
                    sAcode = objDB.ExecuteDataSet(" SELECT HAAM_AGENT_CODE FROM HR_APPL_A2E_MIGRATION " +
                                                            "WHERE HAAM_EMP_CODE = " + txtDsearch.Text).Tables[0].Rows[0][0].ToString();
                }
                catch
                {
                    sAcode = txtDsearch.Text;
                }
                if (sAcode == "")
                    sAcode = txtDsearch.Text;
                string sqlText = "exec VehicleLoanInfo_Get " + txtEoraCode.Text + "," + sAcode;
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return dt;
            }
            finally
            {
                objDB = null;
                dt = null;
            }
        }



        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                picEmpPhoto.BackgroundImage = newImage;
                this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private DataTable GetEmpData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string sqlText = "exec VehicleForm_EmpDetls_Get " + txtDsearch.Text;
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return dt;
            }
            finally
            {
                objDB = null;
                dt = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtDsearch.Text.Length < 5)
            {
                ClearForm();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtEoraCode.Focus();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            string sqlText = string.Empty;
            int iRows = 0;
            try
            {
                if (CheckData())
                {
                    sqlText = "INSERT INTO HR_VEHICLE_LOAN_LGL_NOTICE " +
                                "(HVLLN_COMPANY_CODE" +
                                ", HVLLN_BRANCH_CODE" +
                                ", HVLLN_EORA_CODE" +
                                ", HVLLN_EX_DESIG" +
                                ", HVLLN_MEMBER_NAME" +
                                ", HVLLN_FATHER_NAME" +
                                ", HVLLN_ADDRESS" +
                                ", HVLLN_VEHICLE_TYPE" +
                                ", HVLLN_VEHICLE_MAKE" +
                                ", HVLLN_VEHICLE_MODEL" +
                                ", HVLLN_VEHICLE_REG_NO" +
                                ", HVLLN_VEHICLE_ENG_NO" +
                                ", HVLLN_VEHICLE_CHASSIS_NO" +
                                ", HVLLN_LOAN_AMT" +
                                ", HVLLN_RECOVERED_AMT" +
                                ", HVLLN_BALANCE_AMT" +
                                ", HVLLN_AUTH_SIGN" +
                                ", HVLLN_CREATED_BY" +
                                ", HVLLN_CREATED_DATE" +
                                ", HVLLN_REMARKS)" +
                                " VALUES('" + sCompCode +
                                "','" + sBranchCode +
                                "'," + txtEoraCode.Text +
                                ",'" + txtHRISDesig.Text +
                                "','" + txtMemberName.Text +
                                "','" + txtFatherName.Text +
                                "','" + txtAddress.Text.Replace("'", "").Replace(System.Environment.NewLine, "") +
                                "','" + txtVehType.Text +
                                "','" + txtVehMake.Text +
                                "','" + txtVehModel.Text +
                                "','" + txtVehicleNo.Text +
                                "','" + txtEngNo.Text + 
                                "','" + txtChassisNo.Text + 
                                "'," + txtLoanSanctioned.Text +
                                "," + txtAmtRecoveredTillDate.Text +
                                "," + (Convert.ToDouble(txtLoanSanctioned.Text) - Convert.ToDouble(txtAmtRecoveredTillDate.Text)) +
                                ",'AUTHSIGN','" + CommonData.LogUserId +
                                "',getdate(),'" + txtRemarks.Text.Replace("'", "") + "') ";
                    sqlText += " EXEC GenerateLegalNoticeNo " + txtEoraCode.Text;

                    iRows = objDB.ExecuteSaveData(sqlText);
                }

                if (iRows > 0)
                {
                    MessageBox.Show("Legal Invoice Generation Successfull", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to Generate Legal Notice", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally { objDB = null; }
            FillLegalNoticetoGrid();
            txtEoraCode.Focus();
        }

        private string GenerateNoticeNo()
        {
            throw new NotImplementedException();
        }

        private bool CheckData()
        {
            objDB = new SQLDB();
            if (DateTime.Now.AddDays(-15) < sLastNotice)
            {
                MessageBox.Show("Notice Allready Generated", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtStatus.Text != "LEFT")
            {
                MessageBox.Show("Employee was not left, Notice Generation Restricted to left Employees Only", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToDouble(txtLoanSanctioned.Text.ToString()) == 0)
            {
                MessageBox.Show("Invalid Loan Amount", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEngNo.Text.Length == 0 || txtChassisNo.Text.Length == 0)
            {
                MessageBox.Show("Vehicle Engine No / Chassis No not Valid", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string sAccess = string.Empty;
            try
            {
                sAccess = objDB.ExecuteDataSet("SELECT UB_STATUS FROM USER_BRANCH WHERE UB_USER_ID='" + CommonData.LogUserId + "' AND UB_BRANCH_CODE='" + sBranchCode + "'").Tables[0].Rows[0][0].ToString();
                if (sAccess.Length == 0)
                {
                    MessageBox.Show("You have no Authorization to generate Legal Notice for this branch employee.", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("You have no Authorization to generate Legal Notice for this branch employee.", "Legal :: Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }

        private void gvNotices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvNotices.Columns["Print"].Index && e.RowIndex >= 0)
            {
                CommonData.ViewReport = "Vehicle_Loan_Legal_Notice_Print";
                ReportViewer objReportview = new ReportViewer(txtEoraCode.Text.ToString(), gvNotices.Rows[e.RowIndex].Cells["sNoticeNo"].Value.ToString());
                objReportview.Show();
            }
        }

    }
}
