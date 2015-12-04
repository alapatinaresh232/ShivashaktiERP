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
    public partial class VehicleLoanForm : Form
    {
        private SQLDB objDB = null;
        bool IsAvailable = false;
        private bool modifyAllw = true;
        private bool modifyIncen = true;
        public VehicleLoanForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VehicleLoanForm_Load(object sender, EventArgs e)
        {
            dtpDLVatidTo.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpRegDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpDLValidFrom.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtVehicleNo.CharacterCasing = CharacterCasing.Upper;
            txtRCName.CharacterCasing = CharacterCasing.Upper;
            txtVehicleCost.CharacterCasing = CharacterCasing.Upper;
            txtLicenceNo.CharacterCasing = CharacterCasing.Upper;

            cbOrigRCStatus.SelectedIndex = 0;
            cbVehicleStatus.SelectedIndex = 0;
            cbOwnership.SelectedIndex = 0;
            FillVehicleType();
            FillVahicleMake();
            FillVehicleModel();
            cbVehicleType.SelectedIndex = 0;
            txtRCName.Text = cbRCName.Text;
            //FillVahicleMake();
        }

        private void FillVehicleType()
        {
            DataTable dtVecType = null;
            dtVecType = GetVehicleType();
            if (dtVecType.Rows.Count > 0)
            {
                cbVehicleType.DataSource = dtVecType;
                cbVehicleType.DisplayMember = "VM_VEHICLE_TYPE";
                cbVehicleType.ValueMember = "VM_VEHICLE_TYPE";
                cbVehicleType.SelectedIndex = 0;
            }
        }

        private DataTable GetVehicleType()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string sqlText = "SELECT DISTINCT VM_VEHICLE_TYPE FROM VEHICLE_MASTER";
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

        private void FillVahicleMake()
        {
            DataTable dtVecMake = null;
            dtVecMake = GetVehicleMake();
            if (dtVecMake.Rows.Count > 0)
            {
                cbVehicleMake.DataSource = dtVecMake;
                cbVehicleMake.DisplayMember = "VM_VEHICLE_MAKE";
                cbVehicleMake.ValueMember = "VM_VEHICLE_MAKE";
                cbVehicleMake.SelectedIndex = 0;
            }

        }

        private DataTable GetVehicleMake()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string sqlText = "SELECT DISTINCT VM_VEHICLE_MAKE FROM VEHICLE_MASTER WHERE VM_VEHICLE_TYPE = '" + cbVehicleType.SelectedValue.ToString() + "'";
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

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
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
                    meHRISDataofBirth.Text = Convert.ToDateTime(dt.Rows[0]["EMP_DOB"]).ToString("dd/MM/yyyy");
                    meHRISDateOfJoin.Text = Convert.ToDateTime(dt.Rows[0]["EMP_DOJ"]).ToString("dd/MM/yyyy");

                    DataSet dsPhoto = new DataSet();
                    dsPhoto = objDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + txtDsearch.Text);

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                        GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                    else
                        picEmpPhoto.BackgroundImage = null;
                    ControlsEnableDisable(false);
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
                    IsAvailable = true;

                    txtVehicleNo.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_REG_NUMBER"].ToString();
                    
                    dtpRegDate.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_VEHICLE_REG_DATE"].ToString());
                    cbVehicleMake.SelectedValue = dtVeclData.Rows[0]["VM_VEHICLE_MAKE"].ToString();
                    cbVehicleModel.SelectedValue = dtVeclData.Rows[0]["HVLH_VEHICLE_MAKERS_CLASS"].ToString();
                    if (dtVeclData.Rows[0]["HVLH_OWNERSHIP"].ToString() == "OWN")
                        cbOwnership.SelectedIndex = 1;
                    else
                        cbOwnership.SelectedIndex = 0;
                    txtRCName.Text = dtVeclData.Rows[0]["HVLH_REGD_OWNER"].ToString();
                    if (dtVeclData.Rows[0]["HVLH_DRIVING_LICENCE"].ToString().Trim() == "")
                    {
                        txtLicenceNo.Text = dtVeclData.Rows[0]["HVLH_LEARNING_LICENCE"].ToString();
                        chkLLR.Checked = true;
                    }
                    else
                        txtLicenceNo.Text = dtVeclData.Rows[0]["HVLH_DRIVING_LICENCE"].ToString();
                    if (dtVeclData.Rows[0]["HVLH_DRIVING_VALID_TO"].ToString().Trim() == "")
                    {
                        dtpDLVatidTo.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_LEARNING_VALID_TO"].ToString());
                        chkLLR.Checked = false;
                    }
                    else
                        dtpDLVatidTo.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_DRIVING_VALID_TO"].ToString());
                    if (dtVeclData.Rows[0]["HVLH_DRIVING_VALID_FROM"].ToString().Trim() == "")
                        dtpDLValidFrom.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_LEARNING_VALID_FROM"].ToString());
                    else
                        dtpDLValidFrom.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_DRIVING_VALID_FROM"].ToString());
                    txtLoanSanctioned.Text = dtVeclData.Rows[0]["HVLH_LOAN_AMT"].ToString();
                    txtDepositAmt.Text = dtVeclData.Rows[0]["HVLH_EMPLOYEE_DEPOSIT"].ToString();
                    txtMonthlyRecAmt.Text = dtVeclData.Rows[0]["HVLH_EMI"].ToString();
                    cbVehicleStatus.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_CLASS"].ToString();
                    txtVehicleCost.Text = dtVeclData.Rows[0]["HVLH_VEHICLE_COST"].ToString();
                    txtAmtRecoveredTillDate.Text = dtVeclData.Rows[0]["HVLH_LOAN_RECOVERY_CUTOFFDATE"].ToString();
                    txtEngineNo.Text = dtVeclData.Rows[0]["HVLH_ENGINE_NO"].ToString();
                    txtChassisNo.Text = dtVeclData.Rows[0]["HVLH_CHASSIS_NO"].ToString();
                    //cbOrigRCStatus.SelectedText = dtVeclData.Rows[0]["HVLH_RC_CLASS"].ToString();
                    cbVehicleType.Text = dtVeclData.Rows[0]["VM_VEHICLE_TYPE"].ToString();
                    if (dtVeclData.Rows[0]["HVLH_PETROL_REQUEST_FLAG"].ToString() == "Y")
                    {
                        cbPetrolAllow.SelectedIndex = 0;
                        dtpPetrolAllowReqFrom.Value = Convert.ToDateTime(dtVeclData.Rows[0]["HVLH_PETROL_INCN_REQUEST_DATE"].ToString());
                        modifyAllw = true;
                    }
                    else if (dtVeclData.Rows[0]["HVLH_PETROL_REQUEST_FLAG"].ToString() == "N")
                    {
                        cbPetrolAllow.SelectedIndex = 1;
                        modifyAllw = true;
                    }
                    else
                    {
                        modifyAllw = false;
                    }

                    if (dtVeclData.Rows[0]["HVLH_INCN_REQUEST_FLAG"].ToString() == "Y")
                    {
                        cbIncentiveFlag.SelectedIndex = 0;
                        modifyIncen = true;
                    }
                    else if(dtVeclData.Rows[0]["HVLH_INCN_REQUEST_FLAG"].ToString() == "N")
                    {
                        cbIncentiveFlag.SelectedIndex = 1;
                        modifyIncen = true;
                    }
                    else
                    {
                        modifyIncen = false;
                    }
                    txtIncentiveReqAmt.Text = dtVeclData.Rows[0]["HVLH_INCN_REQUEST_AMT"].ToString();

                    if (CommonData.LogUserId.ToUpper() != "ADMIN" && CommonData.LogUserRole != "MANAGEMENT")
                        ControlsEnableDisable(true);
                }
                else
                {
                    ClearForm();
                    IsAvailable = false;
                    ControlsEnableDisable(false);
                }
            }
            else
            {
                MessageBox.Show("Enter Valid Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ControlsEnableDisable(bool bRes)
        {
            if (bRes == true)
            {
                cbOwnership.Enabled = false;
                txtVehicleNo.Enabled = false;
                txtRCName.Enabled = false;
                dtpRegDate.Enabled = false;
                cbVehicleMake.Enabled = false;
                cbVehicleModel.Enabled = false;
                txtLicenceNo.Enabled = false;
                dtpDLVatidTo.Enabled = false;
                txtLoanSanctioned.Enabled = false;
                txtDepositAmt.Enabled = false;
                txtMonthlyRecAmt.Enabled = false;
                cbVehicleStatus.Enabled = false;
                cbOrigRCStatus.Enabled = false;
                cbVehicleType.Enabled = false;
                txtVehicleCost.Enabled = false;
                txtAmtRecoveredTillDate.Enabled = false;
                txtIncentiveReqAmt.Enabled = false;
                cbIncentiveFlag.Enabled = false;
                dtpPetrolAllowReqFrom.Enabled = false;
                dtpDLValidFrom.Enabled = false;
                cbPetrolAllow.Enabled = false;
                cbRCName.Visible = false;
                chkLLR.Enabled = false;
            }
            else
            {
                cbOwnership.Enabled = true;
                txtVehicleNo.Enabled = true;
                txtRCName.Enabled = true;
                dtpRegDate.Enabled = true;
                cbVehicleMake.Enabled = true;
                cbVehicleModel.Enabled = true;
                txtLicenceNo.Enabled = true;
                dtpDLVatidTo.Enabled = true;
                txtLoanSanctioned.Enabled = true;
                txtDepositAmt.Enabled = true;
                txtMonthlyRecAmt.Enabled = true;
                cbVehicleStatus.Enabled = true;
                cbOrigRCStatus.Enabled = true;
                cbVehicleType.Enabled = true;
                txtVehicleCost.Enabled = true;
                txtAmtRecoveredTillDate.Enabled = true;
                txtIncentiveReqAmt.Enabled = true;
                cbIncentiveFlag.Enabled = true;
                dtpPetrolAllowReqFrom.Enabled = true;
                dtpDLValidFrom.Enabled = true;
                cbPetrolAllow.Enabled = true;
                cbRCName.Visible = true;
                chkLLR.Enabled = true;
            }
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
                string sqlText = "SELECT * FROM HR_VEHICLE_LOAN_HEAD" +
                                 " INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS"+
                                 " WHERE HVLH_EORA_CODE IN ('" + txtDsearch.Text + 
                                 "','" + txtEoraCode.Text + "','" + sAcode + "');";
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
                string sqlText = "SELECT ECODE,ISNULL(HAAM_EMP_CODE,ECODE) EORACODE,MEMBER_NAME,FATHER_NAME,EMP_DOB,EMP_DOJ,DESIG,EM.BRANCH_CODE,BRANCH_NAME,EM.company_code,CM_COMPANY_NAME" +
                                 " FROM EORA_MASTER EM INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE INNER JOIN COMPANY_MAS CM ON CM_COMPANY_CODE = EM.company_code " +
                                 " LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = EM.ECODE " +
                                 "WHERE ECODE = '" + txtDsearch.Text + "';";
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

        private void txtLoanSanctioned_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtDepositAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtMonthlyRecAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtInsentiveAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                //if (cbOwnership.SelectedIndex == 0)
                //txt
                objDB = new SQLDB();
                int ires = 0;
                string sqlText = "";
                string strOwnerShip = "";
                string strRCName = "";
                int strIncAmt = 0;
                //Int32 VehicleCost = 0;
                if (cbIncentiveFlag.SelectedIndex == 0 && txtIncentiveReqAmt.Text.ToString().Replace(" ","").Length == 0)
                {
                    txtIncentiveReqAmt.Text = "0";
                }
                try
                {
                    if (cbOwnership.SelectedIndex == 0)
                        strOwnerShip = "LOAN";
                    if (cbOwnership.SelectedIndex == 1)
                        strOwnerShip = "OWN";
                    if (txtIncentiveReqAmt.Text.ToString().Length == 0)
                    {
                        strIncAmt = Convert.ToInt32(txtIncentiveReqAmt.Text.ToString());
                    }

                    try { Convert.ToDouble(txtVehicleCost.Text); }
                    catch { txtVehicleCost.Text = "0"; }
                    try { Convert.ToDouble(txtLoanSanctioned.Text); }
                    catch { txtLoanSanctioned.Text = "0"; }
                    try { Convert.ToDouble(txtDepositAmt.Text); }
                    catch { txtDepositAmt.Text = "0"; }
                    try { Convert.ToDouble(txtAmtRecoveredTillDate.Text); }
                    catch { txtAmtRecoveredTillDate.Text = "0"; }
                    try { Convert.ToDouble(txtMonthlyRecAmt.Text); }
                    catch { txtMonthlyRecAmt.Text = "0"; }
                    
                    if (cbOwnership.SelectedIndex == 0)
                    {
                        if (cbVehicleStatus.SelectedIndex == 0)
                            strRCName = cbRCName.Text.ToString();
                        else
                            strRCName = txtRCName.Text.ToString();
                    }
                    else
                        strRCName = txtRCName.Text.ToString();
                    if (IsAvailable == false)
                    {
                        sqlText = " INSERT INTO HR_VEHICLE_LOAN_HEAD(HVLH_COMPANY_CODE,HVLH_BRANCH_CODE,HVLH_EORA_CODE,HVLH_VEHICLE_REG_NUMBER" +
                                        ",HVLH_VEHICLE_REG_DATE,HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_CLASS,HVLH_OWNERSHIP,HVLH_REGD_OWNER" +
                                        ",HVLH_VEHICLE_COST,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE,HVLH_EMI,HVLH_MONTHS"+
                                        ", HVLH_ENGINE_NO, HVLH_CHASSIS_NO";

                        if (chkLLR.Checked == true)
                            sqlText += ",HVLH_LEARNING_LICENCE,HVLH_LEARNING_VALID_FROM,HVLH_LEARNING_VALID_TO";
                        else
                            sqlText += ",HVLH_DRIVING_LICENCE,HVLH_DRIVING_VALID_FROM,HVLH_DRIVING_VALID_TO";

                        if (cbIncentiveFlag.Text == "YES")
                            sqlText += ",HVLH_INCN_REQUEST_FLAG,HVLH_INCN_REQUEST_AMT";
                        else
                            sqlText += ",HVLH_INCN_REQUEST_FLAG,HVLH_INCN_REQUEST_AMT";

                        if (cbPetrolAllow.Text == "YES")
                            sqlText += ",HVLH_PETROL_REQUEST_FLAG,HVLH_PETROL_INCN_REQUEST_DATE";
                        else
                            sqlText += ",HVLH_PETROL_REQUEST_FLAG";

                        sqlText += ",HVLH_CREATED_BY,HVLH_CREATED_DATE) " +
                                    "VALUES('" + CommonData.CompanyCode + "','" + CommonData.BranchCode + "','" + txtEoraCode.Text + "','" + txtVehicleNo.Text.Replace(" ","") + "'" +
                                    ",'" + Convert.ToDateTime(dtpRegDate.Value).ToString("dd/MMM/yyyy") + "','" + cbVehicleModel.SelectedValue.ToString() + "'" +
                                    ",'" + cbVehicleType.SelectedValue.ToString() + "','" + strOwnerShip + "'" +
                                    ",'" + strRCName + "','" + Convert.ToDouble(txtVehicleCost.Text) + "','" + Convert.ToDouble(txtLoanSanctioned.Text) + "'" +
                                    ",'" + Convert.ToDouble(txtDepositAmt.Text) + "','" + Convert.ToDouble(txtAmtRecoveredTillDate.Text) + "','" + Convert.ToDouble(txtMonthlyRecAmt.Text) + "'" +
                                    ",'0','" + txtEngineNo.Text.Replace(" ", "") + "','" + txtChassisNo.Text.Replace(" ", "") + "'";
                        sqlText += ",'" + txtLicenceNo.Text.ToString() + "','" + Convert.ToDateTime(dtpDLValidFrom.Value).ToString("dd/MMM/yyyy") + "','" + Convert.ToDateTime(dtpDLVatidTo.Value).ToString("dd/MMM/yyyy") + "'";

                        if (cbIncentiveFlag.SelectedIndex == 0)
                            sqlText += ",'Y','" + strIncAmt + "'";
                        else
                            sqlText += ",'N','0'";

                        if (cbPetrolAllow.SelectedIndex == 0)
                            sqlText += ",'Y','" + Convert.ToDateTime(dtpPetrolAllowReqFrom.Value).ToString("dd/MMM/yyyy") + "'";
                        else
                            sqlText += ",'N'";

                        sqlText += ",'" + CommonData.LogUserId + "',getdate()); ";
                    }
                    else
                    {
                        sqlText = " UPDATE HR_VEHICLE_LOAN_HEAD SET HVLH_VEHICLE_REG_NUMBER='" + txtVehicleNo.Text.Replace(" ", "") + "',HVLH_VEHICLE_REG_DATE='" + Convert.ToDateTime(dtpRegDate.Value).ToString("dd/MMM/yyyy") +
                            "',HVLH_VEHICLE_MAKERS_CLASS='" + cbVehicleModel.SelectedValue.ToString() +
                            "',HVLH_VEHICLE_CLASS='" + cbVehicleType.SelectedValue.ToString() +
                            "',HVLH_OWNERSHIP='" + strOwnerShip +
                            "',HVLH_REGD_OWNER='" + strRCName +
                            "',HVLH_VEHICLE_COST='" + Convert.ToDecimal(txtVehicleCost.Text) +
                            "',HVLH_LOAN_AMT='" + Convert.ToDecimal(txtLoanSanctioned.Text) +
                            "',HVLH_EMPLOYEE_DEPOSIT='" + Convert.ToDecimal(txtDepositAmt.Text) +
                            "',HVLH_LOAN_RECOVERY_CUTOFFDATE='" + Convert.ToDecimal(txtAmtRecoveredTillDate.Text) +
                            "',HVLH_EMI='" + Convert.ToDecimal(txtMonthlyRecAmt.Text) +
                            "',HVLH_MONTHS='0', HVLH_ENGINE_NO='" + txtEngineNo.Text.Replace(" ", "") +
                            "', HVLH_CHASSIS_NO='" + txtChassisNo.Text.Replace(" ", "") + "'";
                        if (chkLLR.Checked == true)
                            sqlText += ",HVLH_LEARNING_LICENCE='" + txtLicenceNo.Text.ToString().Replace(" ","") + "',HVLH_LEARNING_VALID_FROM='" + Convert.ToDateTime(dtpDLValidFrom.Value).ToString("dd/MMM/yyyy") + "',HVLH_LEARNING_VALID_TO='" + Convert.ToDateTime(dtpDLVatidTo.Value).ToString("dd/MMM/yyyy") + "'";
                        else
                            sqlText += ",HVLH_DRIVING_LICENCE='" + txtLicenceNo.Text.ToString().Replace(" ", "") + "',HVLH_DRIVING_VALID_FROM='" + Convert.ToDateTime(dtpDLValidFrom.Value).ToString("dd/MMM/yyyy") + "',HVLH_DRIVING_VALID_TO='" + Convert.ToDateTime(dtpDLVatidTo.Value).ToString("dd/MMM/yyyy") + "'";
                        if (modifyIncen == true)
                        {
                            if (cbIncentiveFlag.Text == "YES")
                                sqlText += ",HVLH_INCN_REQUEST_FLAG='Y',HVLH_INCN_REQUEST_AMT='" + Convert.ToDecimal(txtIncentiveReqAmt.Text) + "'";
                            else
                                sqlText += ",HVLH_INCN_REQUEST_FLAG='N',HVLH_INCN_REQUEST_AMT=0";
                        }
                        if (modifyAllw == true)
                        {
                            if (cbPetrolAllow.Text == "YES")
                                sqlText += ",HVLH_PETROL_REQUEST_FLAG='Y',HVLH_PETROL_INCN_REQUEST_DATE='" + Convert.ToDateTime(dtpPetrolAllowReqFrom.Value).ToString("dd/MMM/yyyy") + "'";
                            else
                                sqlText += ",HVLH_PETROL_REQUEST_FLAG='N'";
                        }
                        sqlText += ",HVLH_MODIFIED_BY='" + CommonData.LogUserId +
                                   "',HVLH_MODIFIED_DATE=getdate()" + 
                                   " WHERE HVLH_EORA_CODE IN ('" + txtDsearch.Text + "','" + txtEoraCode.Text + "')";
                    }
                    objDB = new SQLDB();
                    ires = objDB.ExecuteSaveData(sqlText);

                    if (ires > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSCRM :: VehicleLoan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSCRM :: VehicleLoan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    if (ex.ToString().Contains("FK_HVLH_VEHICLE_MODEL") == true)
                        MessageBox.Show(txtVehicleNo.Text+" Allready Exist!","SSCRM Vehicle Info",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }

            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbOwnership.Text != "COMPANY LOAN")
            {
                if (txtVehicleNo.Text.Trim() == "" || txtRCName.Text.Trim() == "" || txtMonthlyRecAmt.Text.Trim() == ""
                    || txtLoanSanctioned.Text.Trim() == "" || txtLicenceNo.Text.Trim() == "" || txtDepositAmt.Text.Trim() == "")
                {
                    flag = false;
                    MessageBox.Show("Enter All Required fields to Continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return flag;
                }
            }
            else
            {
                if (txtVehicleNo.Text.Trim() == "" ||  txtMonthlyRecAmt.Text.Trim() == ""
                 || txtLoanSanctioned.Text.Trim() == "" || txtLicenceNo.Text.Trim() == "" || txtDepositAmt.Text.Trim() == "")
                {
                    flag = false;
                    MessageBox.Show("Enter All Required fields to Continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return flag;
                }
            }
            if (txtVehicleCost.Text == "")
            {
                flag = false;
                MessageBox.Show("Enter Vehicle Cost to Continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return flag;
            }
            if (cbIncentiveFlag.SelectedIndex == 0)
            {
                
            }
            return flag;
        }

        private void cbVehicleMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleMake.Items.Count > 0)
            {
                if (cbVehicleMake.SelectedIndex > -1)
                {
                    FillVehicleModel();
                }
            }
        }

        private void FillVehicleModel()
        {
            DataTable dtVecModel = null;
            dtVecModel = GetVehicleModel();
            if (dtVecModel.Rows.Count > 0)
            {
                cbVehicleModel.DataSource = dtVecModel;
                cbVehicleModel.DisplayMember = "VM_VEHICLE_MODEL";
                cbVehicleModel.ValueMember = "VM_VEHICLE_MODEL";
                cbVehicleModel.SelectedIndex = 0;
            }
        }

        private DataTable GetVehicleModel()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string sqlText = "SELECT DISTINCT VM_VEHICLE_MODEL FROM VEHICLE_MASTER WHERE VM_VEHICLE_MAKE = '" + cbVehicleMake.SelectedValue.ToString() + "'";
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

        private void cbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleType.Items.Count > 0)
            {
                if (cbVehicleType.SelectedIndex > -1)
                {
                    FillVahicleMake();
                }
            }
        }

        private void txtLoanSanctioned_KeyUp(object sender, KeyEventArgs e)
        {
            double loanAmt = 0;
            double Deposit = 0;
            if (txtLoanSanctioned.Text != "")
                loanAmt = Convert.ToDouble(txtLoanSanctioned.Text);
            else
                loanAmt = 0;
            if (txtDepositAmt.Text != "")
                Deposit = Convert.ToDouble(txtDepositAmt.Text);
            else
                Deposit = 0;

            txtVehicleCost.Text = Convert.ToDouble(loanAmt + Deposit).ToString("f");
        }

        private void cbOwnership_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOwnership.SelectedIndex == 0)
            {
                txtVehicleCost.ReadOnly = true;
                //txtRCName.Text = "SHIVASHAKTI";
                txtRCName.ReadOnly = true;
                txtAmtRecoveredTillDate.ReadOnly = false;
                txtLoanSanctioned.Text = "";
                txtLoanSanctioned.ReadOnly = false;
                txtDepositAmt.Text = "";
                txtDepositAmt.ReadOnly = false;
                txtMonthlyRecAmt.Text = "";
                txtAmtRecoveredTillDate.Text = "";
                txtMonthlyRecAmt.ReadOnly = false;
                cbOrigRCStatus.Enabled = true;
                cbOrigRCStatus.SelectedIndex = 0;
                cbOrigRCStatus.Enabled = false;
                cbIncentiveFlag.SelectedIndex = 1;
                cbPetrolAllow.SelectedIndex = 0;
                dtpPetrolAllowReqFrom.Value = dtpDLValidFrom.Value;
                txtIncentiveReqAmt.Text = "0";
                lblIncentAmt.Visible = false;
                lblIncentiveFlag.Visible = false;
                cbIncentiveFlag.Visible = false;
                txtIncentiveReqAmt.Visible = false;
                cbRCName.Visible = true;
                cbRCName.SelectedIndex = 0;
            }
            else
            {
                txtVehicleCost.ReadOnly = false;
                txtLoanSanctioned.ReadOnly = true;
                txtDepositAmt.ReadOnly = true;
                txtRCName.Text = "";
                txtRCName.ReadOnly = false;
                txtMonthlyRecAmt.ReadOnly = true;
                cbOrigRCStatus.SelectedIndex = 1;
                cbOrigRCStatus.Enabled = false;
                txtAmtRecoveredTillDate.ReadOnly = true;
                cbOrigRCStatus.SelectedIndex = 1;
                cbOrigRCStatus.Enabled = false;
                txtLoanSanctioned.Text = "0";
                txtDepositAmt.Text = "0";
                txtMonthlyRecAmt.Text = "0";
                txtAmtRecoveredTillDate.Text = "0";
                cbIncentiveFlag.SelectedIndex = 0;
                txtIncentiveReqAmt.Text = "";
                lblIncentAmt.Visible = true;
                lblIncentiveFlag.Visible = true;
                cbPetrolAllow.SelectedIndex = 0;
                dtpPetrolAllowReqFrom.Value = dtpDLValidFrom.Value;
                cbIncentiveFlag.Visible = true;
                txtIncentiveReqAmt.Visible = true;
                cbRCName.Visible = false;
            }
        }

        private void txtDepositAmt_KeyUp(object sender, KeyEventArgs e)
        {
            double loanAmt = 0;
            double Deposit = 0;
            if (txtLoanSanctioned.Text != "")
                loanAmt = Convert.ToDouble(txtLoanSanctioned.Text);
            else
                loanAmt = 0;
            if (txtDepositAmt.Text != "")
                Deposit = Convert.ToDouble(txtDepositAmt.Text);
            else
                Deposit = 0;

            txtVehicleCost.Text = Convert.ToDouble(loanAmt + Deposit).ToString();
        }

        private void ClearForm()
        {
            txtEngineNo.Text = "";
            txtChassisNo.Text = "";
            txtVehicleNo.Text = "";
            txtRCName.Text = "";
            dtpRegDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            cbVehicleMake.SelectedIndex = 0;
            //cbVehicleModel.SelectedIndex = 0;
            txtLicenceNo.Text = "";
            dtpDLVatidTo.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtLoanSanctioned.Text = "";
            txtDepositAmt.Text = "";
            txtMonthlyRecAmt.Text = "";
            cbVehicleStatus.SelectedIndex = 0;
            cbOrigRCStatus.SelectedIndex = 0;
            cbVehicleType.SelectedIndex = 0;
            txtVehicleCost.Text = "";
            txtAmtRecoveredTillDate.Text = "";
        }

        private void dtpDLValidFrom_ValueChanged(object sender, EventArgs e)
        {
            dtpPetrolAllowReqFrom.Value = dtpDLValidFrom.Value;
        }

        private void txtVehicleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            //if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar.Equals(' ')))
            //{
            //    e.Handled = true;
            //    return;
            //}
        }

        private void txtLicenceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtAmtRecoveredTillDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtIncentiveReqAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void cbRCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRCName.Text = cbRCName.Text.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEngineNo.Text = "";
            txtChassisNo.Text = "";
            //cbOrigRCStatus.Enabled = false;
            txtVehicleCost.ReadOnly = true;
            //txtRCName.Text = "SHIVASHAKTI";
            txtRCName.ReadOnly = true;
            txtVehicleNo.Text = "";
            dtpRegDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtLicenceNo.Text = "";
            dtpDLValidFrom.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpDLVatidTo.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtVehicleCost.Text = "";
            dtpPetrolAllowReqFrom.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtAmtRecoveredTillDate.ReadOnly = false;
            txtLoanSanctioned.Text = "";
            txtLoanSanctioned.ReadOnly = false;
            txtDepositAmt.Text = "";
            txtDepositAmt.ReadOnly = false;
            txtMonthlyRecAmt.Text = "";
            txtAmtRecoveredTillDate.Text = "";
            txtMonthlyRecAmt.ReadOnly = false;
            //cbOrigRCStatus.Enabled = true;
            cbOrigRCStatus.SelectedIndex = 0;
            //cbOrigRCStatus.Enabled = false;
            cbIncentiveFlag.SelectedIndex = 1;
            cbPetrolAllow.SelectedIndex = 0;
            dtpPetrolAllowReqFrom.Value = dtpDLValidFrom.Value;
            txtIncentiveReqAmt.Text = "0";
            lblIncentAmt.Visible = false;
            lblIncentiveFlag.Visible = false;
            cbIncentiveFlag.Visible = false;
            txtIncentiveReqAmt.Visible = false;
            cbRCName.Visible = true;
            cbRCName.SelectedIndex = 0;
            cbOwnership.SelectedIndex = 0;
            cbVehicleStatus.SelectedIndex = 0;
            chkLLR.Checked = false;


            txtDsearch.Text = "";
            txtMemberName.Text = "";
            txtFatherName.Text = "";
            txtHRISBranch.Text = "";
            txtHRISDesig.Text = "";
            txtEoraCode.Text = "";
            meHRISDataofBirth.Text = "";
            meHRISDateOfJoin.Text = "";
            picEmpPhoto.BackgroundImage = null;

            ControlsEnableDisable(false);
        }

        private void cbVehicleStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOwnership.SelectedIndex == 0)
            {
                if (cbVehicleStatus.SelectedIndex == 1)
                {
                    cbRCName.Visible = false;
                    txtRCName.ReadOnly = false;
                }
                else
                {
                    cbRCName.Visible = true;
                    txtRCName.ReadOnly = true;
                }
            }
        }

        private void cbIncentiveFlag_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbIncentiveFlag.SelectedIndex == 0)
            {
                txtIncentiveReqAmt.Text = "";
                txtIncentiveReqAmt.Visible = true;
            }
            else
            {
                txtIncentiveReqAmt.Text = "0";
                txtIncentiveReqAmt.Visible = false;
            }
        }

    }
}
