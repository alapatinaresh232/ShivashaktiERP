using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class frmBranchMasterUpdate : Form
    {
        SQLDB objData = null;
        ServiceDeptDB objServicedb = null;

        public frmBranchMasterUpdate()
        {
            InitializeComponent();
        }


        private void frmBranchMasterUpdate_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            cbESIApp.SelectedIndex = 0;

        }
               
        private void FillCompanyData()
        {
            objData = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            try
            {
                if (CommonData.LogUserId == "admin" || CommonData.LogUserId == "ADMIN")
                {
                    strCommand = "select CM_COMPANY_NAME,CM_COMPANY_CODE from COMPANY_MAS WHERE ACTIVE='T'";
                }
                else
                {
                    strCommand = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                       " FROM USER_BRANCH " +
                                       " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                       " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                       " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                       "' ORDER BY CM_COMPANY_NAME";
                }

                dt = objData.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
                      
        }


        private void FillLocationdata()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
                        
            try
            {                

                //if (cbCompany.SelectedIndex > 0)
                //{
                //    strCommand = "select BRANCH_NAME,BRANCH_CODE from BRANCH_MAS where COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";
                //    dt = objData.ExecuteDataSet(strCommand).Tables[0];
                //}

                if (cbCompany.SelectedIndex > 0)
                {

                    if (CommonData.LogUserId.ToUpper() == "ADMIN")
                    {
                        strCommand = "SELECT  BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS " +
                                  " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                   "' and ACTIVE='T' ORDER BY BRANCH_NAME ASC";
                    }
                    else
                    {
                        strCommand = "SELECT distinct BRANCH_CODE,BRANCH_NAME FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                   "' AND UB_USER_ID = '" + CommonData.LogUserId +
                                   "' ORDER BY BRANCH_NAME ASC ";
                    }

                    dt = objData.ExecuteDataSet(strCommand).Tables[0];
                }


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "BRANCH_CODE";
                }
               
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
               
            }
           
        }
        private bool CheckData()
        {
            bool flag = true;

            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Select Company", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
               
            }

            else if (cbLocation.SelectedIndex == 0)
            {
                MessageBox.Show("Select Location", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLocation.Focus();
                flag = false;
            }
            else if (txtAddress.Text == string.Empty)
            {
                flag = false;
                MessageBox.Show("Enter Address", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
               
            }
            else if (cbESIApp.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Select ESI Applicable", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbESIApp.Focus();
               
            }
            else if (txtEcodeSearch.Text.Length > 0 && txtEcodeSearch.Text != "0")
            {
                if (txtEName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Valid Ecode", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEcodeSearch.Focus();

                }
            }
            //else if (txtState.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter State", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtState.Focus();
            //    flag = false;
            //}
            //else if (txtDistrict.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter District", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtDistrict.Focus();
            //    flag = false;
            //}
            //else if (txtMandal.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter Mandal", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtMandal.Focus();
            //    flag = false;
            //}
            //else if (txtPin.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter Pincode", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPin.Focus();
            //    flag = false;
            //}
            //else if (cbESIApp.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Select ESI Applicable", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cbESIApp.Focus();
            //    flag = false;
            //}
            //else if (txtEmailId.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter EmailId", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtEmailId.Focus();
            //    flag = false;
            //}
            //else if (txtTinNo.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter TIN No", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtTinNo.Focus();
            //    flag = false;
            //}
            //else if (txtCstNo.Text == string.Empty)
            //{
            //    flag = false;
            //    MessageBox.Show("Enter CSTNo", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtCstNo.Focus();           
            //}
            //else if (txtExRegNo.Text == string.Empty)
            //{
            //    flag = false;
            //    MessageBox.Show("Enter  Excise RegNo", "Branch Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtExRegNo.Focus();          
            //}
            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            objData = new SQLDB();
            string strActive = "T";
            string strCommand = "";

            if (CheckData() == true)
            {

                try
                {
                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = "F";

                    if (txtEcodeSearch.Text.Length == 0)
                    {
                        txtEcodeSearch.Text = "0";
                    }


                    strCommand = "update BRANCH_MAS set BRANCH_ADDRESS='" + txtAddress.Text.ToString().Replace("'", "") +
                       "', LOCATION='" + txtLocation.Text.ToString().ToUpper() +
                       "',STATE='" + txtState.Text.ToUpper() +
                       "', DISTRICT='" + txtDistrict.Text.ToUpper() +
                       "', MANDAL='" + txtMandal.Text.ToUpper() +
                       "', PIN='" + txtPin.Text +
                       "', ESI_APPLICABLE='" + cbESIApp.SelectedItem.ToString() +
                       "',BRANCH_MAIL_ID='" + txtEmailId.Text +
                       "',ACTIVE='" + strActive +
                       "', TIN_NO='" + txtTinNo.Text +
                       "', CST_NO='" + txtCstNo.Text +
                       "', BM_HR_EMAIL='" + txtBrHrMail.Text.Replace(" ", "") +
                       "', BM_REG_HR_MAIL='" + txtRegHrMail.Text.Replace(" ", "") +
                       "', BM_TRAINER_MAIL='" + txtTrainerMail.Text.Replace(" ", "") +
                       "', BM_INCH_MAIL='" + txtInchMail.Text.Replace(" ", "") +
                       "', LAST_MODIFIED_BY='" + CommonData.LogUserId +
                       "', LAST_MODIFIED_DATE=GETDATE()" +
                       ", EXCISE_REG_NO='" + txtExRegNo.Text +
                       "',BRANCH_HEAD_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                       ",BM_CONT_PHNO='"+ txtContactNo.Text.ToString() +
                       "',STATE_CODE='"+ txtState.Tag.ToString() +
                       "' where BRANCH_CODE='" + cbLocation.SelectedValue.ToString() + "' ";

                    if (strCommand.Length > 5)
                    {
                        iRes = objData.ExecuteSaveData(strCommand);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
           
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillLocationdata();
            }
            else
            {
               cbLocation.DataSource = null;
               btnCancel_Click(null, null);
                              
            }
           
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            cbESIApp.SelectedIndex = 0;
                       
            try
            {
                if (cbLocation.SelectedIndex > 0)
                {
                    string strCommand = "select BRANCH_ADDRESS "+
                                        ",LOCATION "+
                                        ",STATE "+
                                        ",DISTRICT "+
                                        ",MANDAL "+
                                        ",PIN,ESI_APPLICABLE "+
                                        ",BRANCH_MAIL_ID "+
                                        ",BM_HR_EMAIL "+
                                        ",BM_REG_HR_MAIL "+
                                        ",BM_TRAINER_MAIL "+
                                        ",BM_INCH_MAIL "+
                                        ",TIN_NO "+
                                        ",CST_NO "+
                                        ",EXCISE_REG_NO "+
                                        ",ACTIVE "+
                                        ",BRANCH_HEAD_ECODE "+
                                        ",MEMBER_NAME "+
                                        ",BM_CONT_PHNO "+
                                        ",STATE_CODE "+
                                        " from BRANCH_MAS BM " +
                                        " LEFT JOIN EORA_MASTER ON ECODE=BRANCH_HEAD_ECODE "+
                                        " where BM.BRANCH_CODE='" + cbLocation.SelectedValue.ToString() + "'";
                    dt = objData.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    txtAddress.Text = dt.Rows[0]["BRANCH_ADDRESS"].ToString();
                    txtLocation.Text = dt.Rows[0]["LOCATION"].ToString();
                    txtMandal.Text = dt.Rows[0]["MANDAL"].ToString();
                    txtDistrict.Text = dt.Rows[0]["DISTRICT"].ToString();
                    txtState.Text = dt.Rows[0]["STATE"].ToString();
                    txtPin.Text = dt.Rows[0]["PIN"].ToString();
                    cbESIApp.SelectedItem = dt.Rows[0]["ESI_APPLICABLE"].ToString();
                    txtEmailId.Text = dt.Rows[0]["BRANCH_MAIL_ID"].ToString();
                    txtBrHrMail.Text = dt.Rows[0]["BM_HR_EMAIL"].ToString();
                    txtRegHrMail.Text = dt.Rows[0]["BM_REG_HR_MAIL"].ToString();
                    txtTrainerMail.Text = dt.Rows[0]["BM_TRAINER_MAIL"].ToString();
                    txtInchMail.Text = dt.Rows[0]["BM_INCH_MAIL"].ToString();
                    txtTinNo.Text = dt.Rows[0]["TIN_NO"].ToString();
                    txtCstNo.Text = dt.Rows[0]["CST_NO"].ToString();
                    txtEcodeSearch.Text = dt.Rows[0]["BRANCH_HEAD_ECODE"].ToString();
                    txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    txtContactNo.Text = dt.Rows[0]["BM_CONT_PHNO"].ToString();
                    txtState.Tag = dt.Rows[0]["STATE_CODE"].ToString();

                    if (dt.Rows[0]["ACTIVE"].ToString() == "T")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    txtExRegNo.Text = dt.Rows[0]["EXCISE_REG_NO"].ToString();
                }
                else
                {
                    txtAddress.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";
                    txtTinNo.Text = "";
                    txtCstNo.Text = "";
                    txtEmailId.Text = "";
                    txtExRegNo.Text = "";
                    cbESIApp.SelectedIndex = 0;
                    chkActive.Checked = false;
                    txtLocation.Text = "";
                    txtEcodeSearch.Text = "";
                    txtEName.Text = "";
                    txtVillSearch.Text = "";
                    cbVillage.DataSource = null;
                    txtBrHrMail.Text = "";
                    txtContactNo.Text = "";
                    txtTrainerMail.Text = "";
                    txtRegHrMail.Text = "";
                    txtInchMail.Text = "";
                    txtVillSearch.Text = "";
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }                   
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            //cbLocation.SelectedIndex = 0;
            txtAddress.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            txtTinNo.Text = "";
            txtCstNo.Text = "";
            txtEmailId.Text = "";
            txtExRegNo.Text = "";
            cbESIApp.SelectedIndex = 0;
            chkActive.Checked = false;
            txtLocation.Text = "";
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtVillSearch.Text = "";
            cbVillage.DataSource = null;
            txtBrHrMail.Text = "";
            txtContactNo.Text = "";
            txtTrainerMail.Text = "";
            txtRegHrMail.Text = "";
            txtInchMail.Text = "";
            txtVillSearch.Text = "";

        }
               
        public  bool isEmailCheck(string eMail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}"
                             +@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" 
                             + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex reEx = new Regex(strRegex);
            if (reEx.IsMatch(eMail))
                return (true);
            else
                return (false);
        }

        private void txtEmailId_Validating(object sender, CancelEventArgs e)
        {
            if (!isEmailCheck(txtEmailId.Text))
            {
                if (txtEmailId.Text.Length >0)
                {
                    MessageBox.Show("Enter valid Email id");
                    txtEmailId.Focus();
                    return;
                }
                
            }

        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
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

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();

            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    string strCommand = "SELECT MEMBER_NAME,DESIG,HAMH_APPL_NUMBER " +
                                        " FROM EORA_MASTER " +
                                        " INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE=ECODE " +
                                        " WHERE ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "  ";

                    dt = objData.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    }
                    else
                    {                       
                        txtEName.Text = "";
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
        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch vilsearch = new VillageSearch("BranchMasterUpdate");
            vilsearch.objfrmBranchMasterUpdate = this;
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
                        txtState.Tag = dtVillage.Rows[0]["CDState"].ToString();

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
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();
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

        private void txtVillSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter((e.KeyChar)))
                {
                    e.Handled = true;
                }
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
                    txtState.Tag = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";


                }
            }

        }

      

     
     }
}
