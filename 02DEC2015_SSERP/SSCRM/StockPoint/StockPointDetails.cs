using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class StockPointDetails : Form
    {
        SQLDB objSQLDB;
        private string strECode = string.Empty;
        public StockPointDetails()
        {
            InitializeComponent();
        }
        public DataTable dtLicence;
        private void StockPointDetails_Load(object sender, EventArgs e)
        {
            gvLicence.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            GetBindData();
            cmbFrmM.SelectedIndex = 0;
            cmbFrmN.SelectedIndex = 0;
            cmbRocCpy.SelectedIndex = 0;
            cmbFocProd.SelectedIndex = 0;
            cmbVatCpy.SelectedIndex = 0;
            cmbBoardStatus.SelectedIndex = 0;
            cmbBordPrice.SelectedIndex = 0;
            cmdAddPlaceBusiness.SelectedIndex = 0;
            dtLicence = new DataTable();
            dtLicence.Columns.Add("SlNo");
            dtLicence.Columns.Add("SPLD_CODE");
            dtLicence.Columns.Add("SPLD_LICENCE_TYPE");
            dtLicence.Columns.Add("SPLD_LICENCE_NUMBER");
            dtLicence.Columns.Add("SPLD_LICENCE_VALID_FROM");
            dtLicence.Columns.Add("SPLD_LICENCE_VALID_TO");
            dtLicence.Columns.Add("SPLD_LICENCE_STATUS");
            dtLicence.Columns.Add("SPLD_AUTH_ECODE");
            dtLicence.Columns.Add("MEMBER_NAME");
            dtLicence.Columns.Add("SPLD_AUTH_STATUS");
            rbNew.Checked = true;
            rbExists.Checked = false;
        }
        public void GetBindData()
        {
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT *FROM BRANCH_MAS WHERE BRANCH_TYPE='SP' AND ACTIVE='T'");
            UtilityLibrary.PopulateControl(cmbBranch, ds.Tables[0].DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSQLDB = null;
        }
        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet("SELECT *FROM BRANCH_MAS WHERE BRANCH_TYPE='SP' AND ACTIVE='T' AND BRANCH_CODE='" + cmbBranch.SelectedValue + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBranchAdd.Text = ds.Tables[0].Rows[0]["BRANCH_ADDRESS"].ToString();
                    txtLocation.Text = ds.Tables[0].Rows[0]["LOCATION"].ToString();
                    txtMandal.Text = ds.Tables[0].Rows[0]["MANDAL"].ToString();
                    txtDistrict.Text = ds.Tables[0].Rows[0]["DISTRICT"].ToString();
                    txtState.Text = ds.Tables[0].Rows[0]["STATE"].ToString();
                    txtPin.Text = ds.Tables[0].Rows[0]["PIN"].ToString();
                    lblCmp.Text = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();                    
                    GetEmployeeData();
                    objSQLDB = new SQLDB();
                    DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT *FROM SP_HEAD_MAS WHERE SPHM_CODE='" + cmbBranch.SelectedValue + "'");
                    if (dsExist.Tables[0].Rows.Count > 0)
                    {
                        txtEcode.Text = dsExist.Tables[0].Rows[0]["SPHM_ECODE"].ToString();
                        cbEcode_SelectedIndexChanged(null, null);
                        cmbFrmN.Text = dsExist.Tables[0].Rows[0]["SPHM_FORM_N"].ToString();
                        cmbFrmM.Text = dsExist.Tables[0].Rows[0]["SPHM_FORM_M"].ToString();
                        cmbRocCpy.Text = dsExist.Tables[0].Rows[0]["SPHM_ROC_COPY"].ToString();
                        cmbBoardStatus.Text = dsExist.Tables[0].Rows[0]["SPHM_NAME_BOARD"].ToString();
                        cmbBordPrice.Text = dsExist.Tables[0].Rows[0]["SPHM_STOCK_PRICE_BOARD"].ToString();
                        cmbFocProd.Text = dsExist.Tables[0].Rows[0]["SPHM_FCO_PRODUCTS"].ToString();
                        cmbVatCpy.Text = dsExist.Tables[0].Rows[0]["SPHM_VAT_COPY"].ToString();
                        txtLicNo.Text = dsExist.Tables[0].Rows[0]["SPHM_VAT_LICENCE_NO"].ToString();
                    }
                    else
                    {
                        cmbFrmN.SelectedIndex = 0;
                        cmbFrmM.SelectedIndex = 0;
                        cmbRocCpy.SelectedIndex = 0;
                        cmbBoardStatus.SelectedIndex = 0;
                        cmbBordPrice.SelectedIndex = 0;
                        cmbFocProd.SelectedIndex = 0;
                        cmbVatCpy.SelectedIndex = 0;
                        txtLicNo.Text = "";
                    }
                    objSQLDB = new SQLDB();
                    dtLicence.Rows.Clear();
                    DataSet dsExistlIC = objSQLDB.ExecuteDataSet("SELECT SPLD_LICENCE_SLNO SLNO,SPLD_CODE,SPLD_LICENCE_TYPE,SPLD_LICENCE_NUMBER" +
                                                ",SPLD_LICENCE_VALID_FROM,SPLD_LICENCE_VALID_TO,SPLD_LICENCE_STATUS,SPLD_AUTH_ECODE" +
                                                ",CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME MEMBER_NAME,SPLD_AUTH_STATUS " +
                                                "FROM SP_LICENCE_DETL LEFT JOIN EORA_MASTER ON ECODE = SPLD_AUTH_ECODE " +
                                                "WHERE SPLD_CODE='" + cmbBranch.SelectedValue + "' ORDER BY SPLD_LICENCE_VALID_TO DESC");
                    if (dsExistlIC.Tables[0].Rows.Count > 0)
                    {
                        dtLicence.Rows.Clear();
                        dtLicence = dsExistlIC.Tables[0];
                        GetLiceince();
                    }
                    else
                    {
                        gvLicence.Rows.Clear();
                    }
                }
                objSQLDB = null;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //private void EcodeSearch()
        //{
        //    objSQLDB = new SQLDB();
        //    DataSet dsEmp = null;
        //    string sqlText = "";
        //    try
        //    {
        //        strECode = "";
        //        sqlText = "SELECT ECODE,'('+cast(ECODE as varchar)+') - '+MEMBER_NAME ENAME FROM EORA_MASTER A INNER JOIN HR_APPL_MASTER_HEAD B ON A.ECODE=B.HAMH_EORA_CODE WHERE A.EORA IN('A','E') AND HAMH_WORKING_STATUS='W' AND BRANCH_CODE IN (SELECT BRANCH_CODE FROM BRANCH_MAS  WHERE BRANCH_TYPE='SP' AND ACTIVE='T' AND BRANCH_CODE='" + cmbBranch.SelectedValue + "')";
        //        Cursor.Current = Cursors.WaitCursor;
        //        cbEcode.DataSource = null;
        //        cbEcode.Items.Clear();
        //        dsEmp = objSQLDB.ExecuteDataSet(sqlText);
        //        DataTable dtEmp = dsEmp.Tables[0];
        //        if (dtEmp.Rows.Count > 0)
        //        {
        //            cbEcode.DataSource = dtEmp;
        //            cbEcode.DisplayMember = "ENAME";
        //            cbEcode.ValueMember = "ECODE";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if (cbEcode.SelectedIndex > -1)
        //        {
        //            cbEcode.SelectedIndex = 0;
        //            strECode = cbEcode.SelectedValue.ToString();
        //        }
        //        objSQLDB = null;
        //        Cursor.Current = Cursors.Default;
        //    }
        //}
        public void GetEmployeeData()
        {
            if (txtEcode.Text.Length != 0)
            {
                if (txtEcode.Text.ToString() != "")
                    strECode = txtEcode.Text.ToString();              
                
                objSQLDB = new SQLDB();
                DataSet dsEmployee = objSQLDB.ExecuteDataSet("SELECT * FROM EORA_MASTER A INNER JOIN HR_APPL_MASTER_HEAD B ON A.ECODE=B.HAMH_EORA_CODE WHERE A.EORA IN('A','E') AND ECODE =" + strECode);
                objSQLDB = null;
                if (dsEmployee.Tables[0].Rows.Count > 0)
                {
                    txtEmpName.Text = dsEmployee.Tables[0].Rows[0]["MEMBER_NAME"].ToString();
                    txtEName.Text = dsEmployee.Tables[0].Rows[0]["MEMBER_NAME"].ToString();
                    txtFName.Text = dsEmployee.Tables[0].Rows[0]["FATHER_NAME"].ToString();
                    txtDesig.Text = dsEmployee.Tables[0].Rows[0]["DESIG"].ToString();
                    txtDoj.Text = Convert.ToDateTime(dsEmployee.Tables[0].Rows[0]["EMP_DOJ"]).ToString("dd/MMM/yyyy");

                    PresentAddCtr.HouseNo = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_HNO"].ToString();
                    PresentAddCtr.LandMark = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_LANDMARK"].ToString();
                    PresentAddCtr.Village = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_VILL_OR_TOWN"].ToString();
                    PresentAddCtr.Mondal = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_MANDAL"].ToString();
                    PresentAddCtr.District = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_DISTRICT"].ToString();
                    PresentAddCtr.State = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_STATE"].ToString();
                    PresentAddCtr.Pin = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_PIN"].ToString();
                    PresentPhone_num.Text = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_PHONE"].ToString();
                    PermentAddCtr.HouseNo = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_HNO"].ToString();
                    PermentAddCtr.LandMark = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_LANDMARK"].ToString();
                    PermentAddCtr.Village = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_VILL_OR_TOWN"].ToString();
                    PermentAddCtr.Mondal = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_MANDAL"].ToString();
                    PermentAddCtr.District = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_DISTRICT"].ToString();
                    PermentAddCtr.State = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_STATE"].ToString();
                    PermentAddCtr.Pin = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_PIN"].ToString();
                    txtPermentPhNo_num.Text = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_PERM_ADDR_PHONE"].ToString();
                    txtContactName.Text = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_NAME"].ToString();
                    ContactAddCtr.HouseNo = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_HNO"].ToString();
                    ContactAddCtr.LandMark = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_LANDMARK"].ToString();
                    ContactAddCtr.Village = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_VILL_OR_TOWN"].ToString();
                    ContactAddCtr.Mondal = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_MANDAL"].ToString();
                    ContactAddCtr.District = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_DISTRICT"].ToString();
                    ContactAddCtr.State = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_STATE"].ToString();
                    ContactAddCtr.Pin = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_PIN"].ToString();
                    txtContPhNo_num.Text = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_PHONE_RES"].ToString();
                    txtContPhNo1_optional.Text = dsEmployee.Tables[0].Rows[0]["HAMH_ADD_CONTPERS_ADDR_PHONE_OFF"].ToString();
                }
            }
            else
            {
                txtEName.Text = "";
                txtFName.Text = "";
                txtDesig.Text = "";
                //Present
                PresentAddCtr.HouseNo = "";
                PresentAddCtr.LandMark = "";
                PresentAddCtr.Village = "";
                PresentAddCtr.Mondal = "";
                PresentAddCtr.District = "";
                PresentAddCtr.State = "";
                PresentAddCtr.Pin = "";
                PresentPhone_num.Text = "";
                //Premenent
                PermentAddCtr.HouseNo = "";
                PermentAddCtr.LandMark = "";
                PermentAddCtr.Village = "";
                PermentAddCtr.Mondal = "";
                PermentAddCtr.District = "";
                PermentAddCtr.State = "";
                PermentAddCtr.Pin = "";
                txtPermentPhNo_num.Text = "";
                //Contact 
                ContactAddCtr.HouseNo = "";
                ContactAddCtr.LandMark = "";
                ContactAddCtr.Village = "";
                ContactAddCtr.Mondal = "";
                ContactAddCtr.District = "";
                ContactAddCtr.State = "";
                ContactAddCtr.Pin = "";
                txtContPhNo_num.Text = "";
            }
        }
        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmployeeData();
        }
        private void chkPermanent_Click(object sender, EventArgs e)
        {
            if (chkPermanent.Checked == true)
            {
                PermentAddCtr.HouseNo = PresentAddCtr.HouseNo;
                PermentAddCtr.LandMark = PresentAddCtr.LandMark;
                PermentAddCtr.Village = PresentAddCtr.Village;
                PermentAddCtr.Mondal = PresentAddCtr.Mondal;
                PermentAddCtr.District = PresentAddCtr.District;
                PermentAddCtr.State = PresentAddCtr.State;
                PermentAddCtr.Pin = PresentAddCtr.Pin;
                txtPermentPhNo_num.Text = PresentPhone_num.Text;
            }
            else
            {
                PermentAddCtr.HouseNo = "";
                PermentAddCtr.LandMark = "";
                PermentAddCtr.Village = "";
                PermentAddCtr.Mondal = "";
                PermentAddCtr.District = "";
                PermentAddCtr.State = "";
                PermentAddCtr.Pin = "";
                txtPermentPhNo_num.Text = "";
            }
        }
        private void chkContact_Click(object sender, EventArgs e)
        {
            if (chkContact.Checked == true)
            {
                ContactAddCtr.HouseNo = PresentAddCtr.HouseNo;
                ContactAddCtr.LandMark = PresentAddCtr.LandMark;
                ContactAddCtr.Village = PresentAddCtr.Village;
                ContactAddCtr.Mondal = PresentAddCtr.Mondal;
                ContactAddCtr.District = PresentAddCtr.District;
                ContactAddCtr.State = PresentAddCtr.State;
                ContactAddCtr.Pin = PresentAddCtr.Pin;
                txtContPhNo_num.Text = PresentPhone_num.Text;
            }
            else
            {
                ContactAddCtr.HouseNo = "";
                ContactAddCtr.LandMark = "";
                ContactAddCtr.Village = "";
                ContactAddCtr.Mondal = "";
                ContactAddCtr.District = "";
                ContactAddCtr.State = "";
                ContactAddCtr.Pin = "";
                txtContPhNo_num.Text = "";
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string strSql = "";
            int iretVal = 0;
            if (CheckData())
            {
                DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_HEAD_MAS WHERE SPHM_CODE='" + cmbBranch.SelectedValue + "'");
                if (txtPin.Text.Length == 0)
                    txtPin.Text = "0";
                if (PresentAddCtr.Pin.Length == 0)
                    PresentAddCtr.Pin = "0";
                if (PermentAddCtr.Pin.Length == 0)
                    PermentAddCtr.Pin = "0";
                if (ContactAddCtr.Pin.Length == 0)
                    ContactAddCtr.Pin = "0";
                if (dsExist.Tables[0].Rows[0][0].ToString() == "0")
                {
                    strSql = " INSERT INTO SP_HEAD_MAS(SPHM_CODE,SPHM_ECODE,SPHM_FORM_N,SPHM_FORM_M,SPHM_ROC_COPY,SPHM_NAME_BOARD,SPHM_STOCK_PRICE_BOARD,SPHM_FCO_PRODUCTS," +
                       "SPHM_VAT_LICENCE_NO,SPHM_VAT_COPY,SPHM_ADDT_PLACE_BISINS,SPHM_CREATED_BY,SPHM_AUTHORIZED_BY,SPHM_CREATED_DATE)VALUES";
                    strSql += "('" + cmbBranch.SelectedValue + "'," + txtEcode.Text + ",'" + cmbFrmN.Text + "','" + cmbFrmM.Text + "','" + cmbRocCpy.Text + "','" + cmbBoardStatus.Text +
                        "','" + cmbBordPrice.Text + "','" + cmbFocProd.Text + "','" + txtLicNo.Text + "','" + cmbVatCpy.Text + "','" + cmdAddPlaceBusiness.Text + "','" + CommonData.LogUserId + "','" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "')";
                }
                else
                {
                    strSql = " UPDATE SP_HEAD_MAS SET SPHM_ECODE=" + txtEcode.Text + ",SPHM_FORM_N='" + cmbFrmN.Text + "',SPHM_FORM_M='" + cmbFrmM.Text + "',SPHM_ROC_COPY='" + cmbRocCpy.Text + "',SPHM_NAME_BOARD='" + cmbBoardStatus.Text + "',SPHM_STOCK_PRICE_BOARD='" + cmbBordPrice.Text +
                        "',SPHM_FCO_PRODUCTS='" + cmbFocProd.Text + "',SPHM_VAT_LICENCE_NO='" + txtLicNo.Text + "',SPHM_VAT_COPY='" + cmbVatCpy.Text + "',SPHM_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',SPHM_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                        "' WHERE SPHM_CODE = '" + cmbBranch.SelectedValue + "'";
                }
                if (gvLicence.Rows.Count > 0)
                {
                    strSql += " DELETE FROM SP_LICENCE_DETL WHERE SPLD_CODE='" + cmbBranch.SelectedValue + "'";
                    for (int i = 0; i < gvLicence.Rows.Count; i++)
                    {
                        if (gvLicence.Rows[i].Cells["AuthorisedEcode"].Value.ToString().Trim().Length == 0)
                            gvLicence.Rows[i].Cells["AuthorisedEcode"].Value = 0;
                        if (rbExists.Checked == true)
                        {
                            if (gvLicence.Rows.Count - 1 == i)
                            {
                                strSql += " INSERT INTO SP_LICENCE_DETL (SPLD_CODE,SPLD_LICENCE_TYPE,SPLD_LICENCE_SLNO" +
                                    ",SPLD_LICENCE_NUMBER,SPLD_LICENCE_VALID_FROM,SPLD_LICENCE_VALID_TO,SPLD_LICENCE_STATUS" +
                                    ",SPLD_LICENCE_ADDRESS,SPLD_LICENCE_LOCATION,SPLD_LICENCE_MONDAL,SPLD_LICENCE_DISTRICT" +
                                    ",SPLD_LICENCE_STATE,SPLD_LICENCE_PIN,SPLD_AUTH_ECODE,SPLD_AUTH_STATUS)VALUES(" +
                                    "'" + cmbBranch.SelectedValue + "','" + gvLicence.Rows[i].Cells["SPLD_LICENCE_TYPE"].Value.ToString() +
                                    "'," + Convert.ToInt32(i + 1) + ",'" + gvLicence.Rows[i].Cells["SPLD_LICENCE_NUMBER"].Value.ToString() +
                                    "','" + Convert.ToDateTime(gvLicence.Rows[i].Cells["SPLD_LICENCE_VALID_FROM"].Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(gvLicence.Rows[i].Cells["SPLD_LICENCE_VALID_TO"].Value).ToString("dd/MMM/yyyy") +
                                    "','" + gvLicence.Rows[i].Cells["SPLD_LICENCE_STATUS"].Value.ToString() + "','" + txtBranchAdd.Text +
                                    "','" + txtLocation.Text + "','" + txtMandal.Text + "','" + txtDistrict.Text + "','" + txtState.Text +
                                    "'," + txtPin.Text + "," + gvLicence.Rows[i].Cells["AuthorisedEcode"].Value.ToString() +
                                    ",'" + gvLicence.Rows[i].Cells["SPLD_AUTH_STATUS"].Value.ToString() + "')";
                            }
                            else
                            {
                                strSql += " INSERT INTO SP_LICENCE_DETL (SPLD_CODE,SPLD_LICENCE_TYPE,SPLD_LICENCE_SLNO" +
                                    ",SPLD_LICENCE_NUMBER,SPLD_LICENCE_VALID_FROM,SPLD_LICENCE_VALID_TO,SPLD_LICENCE_STATUS" +
                                    ",SPLD_LICENCE_ADDRESS,SPLD_LICENCE_LOCATION,SPLD_LICENCE_MONDAL,SPLD_LICENCE_DISTRICT" +
                                    ",SPLD_LICENCE_STATE,SPLD_LICENCE_PIN,SPLD_AUTH_ECODE,SPLD_AUTH_STATUS)VALUES(" +
                                    "'" + cmbBranch.SelectedValue + "','" + gvLicence.Rows[i].Cells["SPLD_LICENCE_TYPE"].Value.ToString() +
                                    "'," + Convert.ToInt32(i + 1) + ",'" + gvLicence.Rows[i].Cells["SPLD_LICENCE_NUMBER"].Value.ToString() +
                                    "','" + Convert.ToDateTime(gvLicence.Rows[i].Cells["SPLD_LICENCE_VALID_FROM"].Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(gvLicence.Rows[i].Cells["SPLD_LICENCE_VALID_TO"].Value).ToString("dd/MMM/yyyy") +
                                    "','CLOSED','" + txtBranchAdd.Text + "','" + txtLocation.Text + "','" + txtMandal.Text + "','" + txtDistrict.Text +
                                    "','" + txtState.Text + "'," + txtPin.Text + "," + gvLicence.Rows[i].Cells["AuthorisedEcode"].Value.ToString() +
                                    ",'" + gvLicence.Rows[i].Cells["EmpStatus"].Value.ToString() + "')";
                            }
                        }
                        else
                        {
                            strSql += " INSERT INTO SP_LICENCE_DETL (SPLD_CODE,SPLD_LICENCE_TYPE,SPLD_LICENCE_SLNO" +
                                    ",SPLD_LICENCE_NUMBER,SPLD_LICENCE_VALID_FROM,SPLD_LICENCE_VALID_TO,SPLD_LICENCE_STATUS" +
                                    ",SPLD_LICENCE_ADDRESS,SPLD_LICENCE_LOCATION,SPLD_LICENCE_MONDAL,SPLD_LICENCE_DISTRICT" +
                                    ",SPLD_LICENCE_STATE,SPLD_LICENCE_PIN,SPLD_AUTH_ECODE,SPLD_AUTH_STATUS)VALUES(" +
                                    "'" + cmbBranch.SelectedValue + "','" + gvLicence.Rows[i].Cells["SPLD_LICENCE_TYPE"].Value.ToString() +
                                    "'," + Convert.ToInt32(i + 1) + ",'" + gvLicence.Rows[i].Cells["SPLD_LICENCE_NUMBER"].Value.ToString() +
                                    "','" + Convert.ToDateTime(gvLicence.Rows[i].Cells["SPLD_LICENCE_VALID_FROM"].Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(gvLicence.Rows[i].Cells["SPLD_LICENCE_VALID_TO"].Value).ToString("dd/MMM/yyyy") +
                                    "','" + gvLicence.Rows[i].Cells["SPLD_LICENCE_STATUS"].Value.ToString() + "','" + txtBranchAdd.Text +
                                    "','" + txtLocation.Text + "','" + txtMandal.Text + "','" + txtDistrict.Text + "','" + txtState.Text +
                                    "'," + txtPin.Text + "," + gvLicence.Rows[i].Cells["AuthorisedEcode"].Value.ToString() +
                                    ",'" + gvLicence.Rows[i].Cells["EmpStatus"].Value.ToString() + "')";
                        }

                    }
                }
                string strBrnch = " UPDATE BRANCH_MAS SET BRANCH_ADDRESS='" + txtBranchAdd.Text + "',STATE='" + txtState.Text + "',DISTRICT='" + txtDistrict.Text + "',MANDAL='" + txtMandal.Text + "',LOCATION='" + txtLocation.Text + "',PIN='" + txtPin.Text + "' WHERE BRANCH_CODE='" + cmbBranch.SelectedValue + "'";
                string stremp = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_ADD_PRES_ADDR_HNO='" + PresentAddCtr.HouseNo + "',HAMH_ADD_PRES_ADDR_LANDMARK='" + PresentAddCtr.LandMark + "',HAMH_ADD_PRES_ADDR_VILL_OR_TOWN='" + PresentAddCtr.Village + "',HAMH_ADD_PRES_ADDR_MANDAL='" + PresentAddCtr.Mondal + "',HAMH_ADD_PRES_ADDR_DISTRICT='" +
                    PresentAddCtr.District + "',HAMH_ADD_PRES_ADDR_STATE='" + PresentAddCtr.State + "',HAMH_ADD_PRES_ADDR_PIN='" + PresentAddCtr.Pin + "',HAMH_ADD_PRES_ADDR_PHONE='" + PresentPhone_num.Text + "',HAMH_ADD_PERM_ADDR_HNO='" + PermentAddCtr.HouseNo + "',HAMH_ADD_PERM_ADDR_LANDMARK='" + PermentAddCtr.LandMark +
                    "',HAMH_ADD_PERM_ADDR_VILL_OR_TOWN='" + PermentAddCtr.Village + "',HAMH_ADD_PERM_ADDR_MANDAL='" + PermentAddCtr.Mondal + "',HAMH_ADD_PERM_ADDR_DISTRICT='" + PermentAddCtr.District + "',HAMH_ADD_PERM_ADDR_STATE='" + PermentAddCtr.State + "',HAMH_ADD_PERM_ADDR_PIN='" + PermentAddCtr.Pin + "',HAMH_ADD_PERM_ADDR_PHONE='" +
                    txtPermentPhNo_num.Text + "',HAMH_ADD_CONTPERS_NAME='" + txtContactName.Text + "',HAMH_ADD_CONTPERS_ADDR_HNO='" + ContactAddCtr.HouseNo + "',HAMH_ADD_CONTPERS_ADDR_LANDMARK='" + ContactAddCtr.LandMark + "',HAMH_ADD_CONTPERS_ADDR_VILL_OR_TOWN='" + ContactAddCtr.Village + "',HAMH_ADD_CONTPERS_ADDR_MANDAL='" + ContactAddCtr.Mondal
                    + "',HAMH_ADD_CONTPERS_ADDR_DISTRICT='" + ContactAddCtr.District + "',HAMH_ADD_CONTPERS_ADDR_STATE='" + ContactAddCtr.State + "',HAMH_ADD_CONTPERS_ADDR_PIN='" + ContactAddCtr.Pin + "',HAMH_ADD_CONTPERS_ADDR_PHONE_RES='" + txtContPhNo_num.Text + "',HAMH_ADD_CONTPERS_ADDR_PHONE_OFF='" + txtContPhNo1_optional.Text + "' WHERE HAMH_EORA_CODE=" + txtEcode.Text;

                iretVal = objSQLDB.ExecuteSaveData(strSql + strBrnch + stremp);
                objSQLDB = null;
                if (iretVal > 0)
                    MessageBox.Show("Inserted Data Successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data not Inserted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
            }
        }
        private bool CheckData()
        {
            bool bFlag = true;
            if (cmbBranch.SelectedIndex <= 0)
            {
                MessageBox.Show("Please Select StockPoint", "SP :: Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEmpName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please Enter Incharge Ecode", "SP :: Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //if (gvLicence.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please Enter Atleast One licence", "SP :: Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            if (cmbFrmN.SelectedIndex < 0 || cmbFrmM.SelectedIndex < 0 || cmbRocCpy.SelectedIndex < 0 || cmbFocProd.SelectedIndex < 0 || cmbBoardStatus.SelectedIndex < 0 || cmdAddPlaceBusiness.SelectedIndex < 0 || cmbBordPrice.SelectedIndex < 0 || cmbVatCpy.SelectedIndex<0)
            {
                MessageBox.Show("Incomplete Statutory Details", "SP :: Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmdAddPlaceBusiness.SelectedIndex == 0 && txtLicNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Enter Vat licence No", "SP :: Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return bFlag;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbBranch.SelectedIndex = 0;
            cmbFrmM.SelectedIndex = 0;
            cmbFrmN.SelectedIndex = 0;
            cmbRocCpy.SelectedIndex = 0;
            cmbFocProd.SelectedIndex = 0;
            cmbVatCpy.SelectedIndex = 0;
            cmbBoardStatus.SelectedIndex = 0;
            cmbBordPrice.SelectedIndex = 0;
            txtState.Text = "";
            txtPin.Text = "";
            txtPermentPhNo_num.Text = "";
            txtMandal.Text = "";
            txtLocation.Text = "";
            txtLicNo.Text = "";
            txtFName.Text = "";
            txtEName.Text = "";
            txtDoj.Text = "";
            txtDistrict.Text = "";
            txtDesig.Text = "";
            txtContPhNo1_optional.Text = "";
            txtContPhNo_num.Text = "";
            txtContactName.Text = "";
            txtBranchAdd.Text = "";
            txtEmpName.Text = "";
            txtEcode.Text = "";
            //Present
            PresentAddCtr.HouseNo = "";
            PresentAddCtr.LandMark = "";
            PresentAddCtr.Village = "";
            PresentAddCtr.Mondal = "";
            PresentAddCtr.District = "";
            PresentAddCtr.State = "";
            PresentAddCtr.Pin = "";
            PresentPhone_num.Text = "";
            //Premenent
            PermentAddCtr.HouseNo = "";
            PermentAddCtr.LandMark = "";
            PermentAddCtr.Village = "";
            PermentAddCtr.Mondal = "";
            PermentAddCtr.District = "";
            PermentAddCtr.State = "";
            PermentAddCtr.Pin = "";
            txtPermentPhNo_num.Text = "";
            //Contact 
            ContactAddCtr.HouseNo = "";
            ContactAddCtr.LandMark = "";
            ContactAddCtr.Village = "";
            ContactAddCtr.Mondal = "";
            ContactAddCtr.District = "";
            ContactAddCtr.State = "";
            ContactAddCtr.Pin = "";
            txtContPhNo_num.Text = "";
            gvLicence.Rows.Clear();
        }
        public void GetLiceince()
        {
            int intRow = 1;
            gvLicence.Rows.Clear();
            for (int i = 0; i < dtLicence.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = dtLicence.Rows[i]["SLNO"]; ;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellExamPass = new DataGridViewTextBoxCell();
                cellExamPass.Value = dtLicence.Rows[i]["SPLD_LICENCE_TYPE"];
                tempRow.Cells.Add(cellExamPass);

                DataGridViewCell cellYearPass = new DataGridViewTextBoxCell();
                cellYearPass.Value = dtLicence.Rows[i]["SPLD_LICENCE_NUMBER"];
                tempRow.Cells.Add(cellYearPass);

                DataGridViewCell cellInstName = new DataGridViewTextBoxCell();
                cellInstName.Value = Convert.ToDateTime(dtLicence.Rows[i]["SPLD_LICENCE_VALID_FROM"]).ToString("dd/MMM/yyyy");
                tempRow.Cells.Add(cellInstName);

                DataGridViewCell cellInstLocation = new DataGridViewTextBoxCell();
                cellInstLocation.Value = Convert.ToDateTime(dtLicence.Rows[i]["SPLD_LICENCE_VALID_TO"]).ToString("dd/MMM/yyyy");
                tempRow.Cells.Add(cellInstLocation);

                DataGridViewCell cellSubject = new DataGridViewTextBoxCell();
                cellSubject.Value = dtLicence.Rows[i]["SPLD_LICENCE_STATUS"];
                tempRow.Cells.Add(cellSubject);

                DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                cellEcode.Value = dtLicence.Rows[i]["SPLD_AUTH_ECODE"];
                tempRow.Cells.Add(cellEcode);

                DataGridViewCell cellEname = new DataGridViewTextBoxCell();
                cellEname.Value = dtLicence.Rows[i]["MEMBER_NAME"];
                tempRow.Cells.Add(cellEname);

                DataGridViewCell cellWStatus = new DataGridViewTextBoxCell();
                cellWStatus.Value = dtLicence.Rows[i]["SPLD_AUTH_STATUS"];
                tempRow.Cells.Add(cellWStatus);

                intRow = intRow + 1;
                gvLicence.Rows.Add(tempRow);
            }
        }
        private void btnAdd_Lice_Click(object sender, EventArgs e)
        {
            LicenceDetails objLicenceDetails = new LicenceDetails();
            objLicenceDetails.objStockPointDetl = this;
            objLicenceDetails.ShowDialog();
        }
        private void gvLicence_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvLicence.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvLicence.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvLicence.Rows[e.RowIndex].Cells[gvLicence.Columns["SlNo"].Index].Value);
                        DataRow[] dr = dtLicence.Select("SlNo=" + SlNo);
                        LicenceDetails objLicenceDetails = new LicenceDetails(dr);
                        objLicenceDetails.objStockPointDetl = this;
                        objLicenceDetails.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvLicence.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvLicence.Rows[e.RowIndex].Cells[gvLicence.Columns["SlNo"].Index].Value);
                        DataRow[] dr = dtLicence.Select("SlNo=" + SlNo);
                        dtLicence.Rows.Remove(dr[0]);
                        GetLiceince();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void rbNew_Click(object sender, EventArgs e)
        {
            if (rbNew.Checked == true)
                rbExists.Checked = false;
        }
        private void rbExists_Click(object sender, EventArgs e)
        {
            if (rbExists.Checked == true)
                rbNew.Checked = false;
        }
        private void btnHamali_Click(object sender, EventArgs e)
        {
            HamaliCharges objHamaliCharges = new HamaliCharges(cmbBranch.SelectedValue.ToString(), cmbBranch.Text, lblCmp.Text);
            objHamaliCharges.objStockPointDetl = this;
            objHamaliCharges.ShowDialog();
        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcode.Text.Length > 4)
            {
                txtEmpName.Text = GetEmpName(txtEcode.Text);
                GetEmployeeData();
            }
            else
            {
                txtEmpName.Text = "";
                txtEName.Text = "";
                txtDesig.Text = "";
                txtDoj.Text = "";
                txtFName.Text = "";
            }
        }

        private string GetEmpName(string strEcode)
        {
            string strName = "";
            DataTable EmpData = new DataTable();
            try
            {
                objSQLDB = new SQLDB();
                string strSql = "SELECT CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME NAME,DESIG,EMP_DOJ,FATHER_NAME FROM EORA_MASTER WHERE ECODE = " + strEcode;
                EmpData = objSQLDB.ExecuteDataSet(strSql).Tables[0];
                txtEmpName.Text = EmpData.Rows[0]["NAME"].ToString();
                txtEName.Text = EmpData.Rows[0]["NAME"].ToString();
                txtDesig.Text = EmpData.Rows[0]["DESIG"].ToString();
                txtDoj.Text = Convert.ToDateTime(EmpData.Rows[0]["EMP_DOJ"].ToString()).ToString("dd/MMM/yyyy").ToUpper();
                txtFName.Text = EmpData.Rows[0]["FATHER_NAME"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            return strName;
        }

        private void cmdAddPlaceBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmdAddPlaceBusiness.SelectedIndex == 1)
            {
                txtLicNo.Text = "";
                txtLicNo.Enabled = false;
            }
            else
                txtLicNo.Enabled = true;
        }
    }
}
