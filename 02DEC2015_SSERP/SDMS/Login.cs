using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using SSCRMDB;
using SSAdmin;
namespace SDMS
{
    public partial class Login : Form
    {
        private Security objData = null;
        private UtilityDB objUtil = null;
        private SQLDB objSQLdb = null;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            double currentversion = 17.14;
            lblversion.Text = currentversion.ToString("0.00"); ;

            double updateVersion = GetLatestAppVersion();
            char CurrentVersionStatus = 'C';
            CurrentVersionStatus = GetVersionStatus(currentversion);
            //if (currentversion < updateVersion)
            //{
            //    //btnOK.Enabled = false;
            //    MessageBox.Show("UPDATE VERSION AVAILABLE PLEASE UPDATE FIRST", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //if (CurrentVersionStatus == 'C' && currentversion < updateVersion)
            //{
            //    btnOK.Enabled = false;
            //    btnOK.Visible = false;
            //    //MessageBox.Show("CURRENTLY APPLICATION LOCKED BY IT. PLEASE CONTACT IT DEPARTMENT FOR DETAILS", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //if (CurrentVersionStatus == 'C' && updateVersion == 0.00)
            //{
            //    btnOK.Enabled = false;
            //    btnOK.Visible = false;
            //    MessageBox.Show("CURRENTLY APPLICATION LOCKED BY IT. PLEASE CONTACT IT DEPARTMENT FOR DETAILS", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //if (CurrentVersionStatus == 'E')
            //{
            //    btnOK.Enabled = false;
            //    btnOK.Visible = false;
            //    MessageBox.Show("Application Unable to connect to the Server, Please Check Your Internet Connection", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private char GetVersionStatus(double currentversion)
        {
            char status = 'C';
            string sqlSelect = "SELECT AV_ACTIVE_STATUS FROM APPLICATION_VERSION WHERE AV_VERSION_NUMBER = '" + currentversion.ToString("0.00") + "'";
            objSQLdb = new SQLDB();
            try
            {
                status = Convert.ToChar(objSQLdb.ExecuteDataSet(sqlSelect, CommandType.Text).Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                status = 'E';
            }
            finally
            {

                objSQLdb = null;
            }
            return status;
        }

        private double GetLatestAppVersion()
        {
            double Version = 0.00;
            string sqlSelect = "SELECT MAX(AV_VERSION_NUMBER) FROM APPLICATION_VERSION WHERE AV_ACTIVE_STATUS = 'R'";
            objSQLdb = new SQLDB();
            try
            {
                Version = Convert.ToDouble(objSQLdb.ExecuteDataSet(sqlSelect).Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                Version = 0;
            }
            finally
            {
                
                objSQLdb = null;
            }
            return Version;
        }

        private void FillCompanyData()
        {
            DataSet ds = null;
            objData = new Security();
            try
            {
                ds = new DataSet();

                ds = objData.GetCompanyDataSet();
                DataTable dtCompany = ds.Tables[0];
                if (dtCompany.Rows.Count > 0)
                {                    
                    cbCompany.DisplayMember = "CM_Company_Name";
                    cbCompany.ValueMember = "CM_Company_Code";
                    cbCompany.DataSource = dtCompany;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
            }

        }

        private void FillUserBranch()
        {
            objUtil = new UtilityDB();
            try
            {
                DataTable dtUB = objUtil.dtUserBranch(txtUserId.Text.ToString());
                //if (dtUB.Rows.Count == 0)
                dtUB.Rows.Add(CommonData.BranchCode + '@' + CommonData.CompanyCode + '@' + CommonData.BranchName + '@' + CommonData.CompanyName + '@' + CommonData.StateCode, CommonData.BranchName);


                cbUserBranch.DisplayMember = "branch_name";
                cbUserBranch.ValueMember = "branch_Code";
                cbUserBranch.DataSource = dtUB;

                //cbUserBranch.SelectedValue = CommonData.BranchCode + '@' + CommonData.CompanyCode + '@' + CommonData.BranchName + '@' + CommonData.CompanyName + '@' + CommonData.StateCode;

                dtUB = null;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objUtil = null;

            }

        }

        private void FillBranchData()
        {
            DataSet ds = null;
            objData = new Security();
            try
            {
                ds = new DataSet();
                ds = objData.UserBranchList(cbCompany.SelectedValue.ToString());
                DataTable dtCompany = ds.Tables[0];
                DataView dv1 = dtCompany.DefaultView;
                dv1.RowFilter = " Active = 'T' ";
                DataTable dtBR = dv1.ToTable();
                if (dtBR.Rows.Count > 0)
                {
                    cbBranch.DisplayMember = "branch_name";
                    cbBranch.ValueMember = "branch_Code";
                    cbBranch.DataSource = dtBR;
                }
                dtCompany = null;
                dv1 = null;
                dtBR = null;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
            }

        }

        private void FillDocumentMonthData()
        {
            objUtil = new UtilityDB();
            try
            {
                string[] strBranchCode;
                DataTable dt;
                if (txtUserId.Text.Contains("admin"))
                {
                    strBranchCode = cbCompany.SelectedValue.ToString().Split('@');
                    dt = objUtil.dtCompanyDocumentMonth(strBranchCode[0]);
                }
                else
                {
                    strBranchCode = cbUserBranch.SelectedValue.ToString().Split('@');
                    dt = objUtil.dtCompanyDocumentMonth(strBranchCode[1]);
                }                
                if (dt.Rows.Count > 0)
                {
                    cbDocmentMonth.DisplayMember = "DocMonth";
                    cbDocmentMonth.ValueMember = "FinYear";
                    cbDocmentMonth.DataSource = dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objUtil = null;

            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void btnOK_Click(object sender, EventArgs e)
        //{
        //    Hashtable htComp = new Hashtable();
        //    objData = new Security();

        //    try
        //    {
        //        if (CheckData())
        //        {
        //            DataTable dt = objData.UserLogin(txtUserId.Text.ToString(), txtPassword.Text.ToString());
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (txtUserId.Text.ToString().Trim().ToUpper() == "ADMIN")
        //                {
        //                    CommonData.CompanyCode = cbCompany.SelectedValue.ToString();
        //                    CommonData.CompanyName = cbCompany.Text.ToString();
        //                    CommonData.BranchCode = cbBranch.SelectedValue.ToString();
        //                    CommonData.BranchName = cbBranch.Text.ToString();
        //                    CommonData.StateCode = cbBranch.SelectedValue.ToString().Substring(3, 2);
        //                    CommonData.LogUserId = txtUserId.Text.ToString().Trim();
        //                }
        //                else
        //                    CommonData.SetCompanyDetails(dt.Rows[0]["companycode"].ToString(), dt.Rows[0]["companyname"].ToString(), dt.Rows[0]["userid"].ToString());
        //                dt.Dispose();
        //                dt = null;
        //                htComp = objData.GetCompanyDocumentMonth();
        //                if (htComp.Count > 0)
        //                {
        //                    CommonData.SetCompanyDocMonthDetails(htComp["DocMonth"].ToString()
        //                        , Convert.ToDateTime(htComp["DocFDate"]).ToString("dd/MM/yyyy")
        //                        , Convert.ToDateTime(htComp["DocTDate"]).ToString("dd/MM/yyyy")
        //                        , htComp["FYear"].ToString()
        //                        , Convert.ToDateTime(htComp["CurDate"]).ToString("dd/MMM/yyyy"));
        //                    string strComp = CommonData.CompanyName;
        //                    if (strComp.ToString().Length > 0)
        //                    {
        //                        MDIParent MDIForm = new MDIParent();
        //                        MDIForm.Text = CommonData.CompanyName + '-' + CommonData.BranchName;
        //                        MDIForm.Show();
        //                        this.Hide();
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("No Document month for " + cbCompany.Text.ToString() +
        //                        "\nPlease contact to IT - Department", "SS CRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Invalid User ID, Password. ", "SS CRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.ToString().IndexOf("network") > -1)
        //            MessageBox.Show("Database connection problem, Please check network.\nContact to IT Department");
        //        else
        //            MessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {

        //        objData = null;
        //        htComp = null;
        //    }
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {

            objUtil = new UtilityDB();

            try
            {
                if (CheckData())
                {
                    if (cbDocmentMonth.SelectedIndex > -1)
                    {

                        if (txtUserId.Text.ToString().Trim().ToUpper() == "ADMIN")
                        {
                            CommonData.CompanyCode = cbCompany.SelectedValue.ToString();
                            CommonData.CompanyName = cbCompany.Text.ToString();
                            CommonData.BranchCode = cbBranch.SelectedValue.ToString();
                            CommonData.BranchType = cbBranch.Text.ToString().Substring(cbBranch.Text.Length - 3, 2);
                            CommonData.BranchName = cbBranch.Text.ToString();
                            CommonData.StateCode = cbBranch.SelectedValue.ToString().Substring(3, 2);
                            CommonData.LogUserId = txtUserId.Text.ToString().Trim();
                            CommonData.DocMonth = cbDocmentMonth.SelectedText.ToString();
                            
                        }
                        else
                        {
                            string[] strBranchCode = cbUserBranch.SelectedValue.ToString().Split('@');
                            CommonData.BranchCode = strBranchCode[0].ToString();
                            CommonData.CompanyCode = strBranchCode[1].ToString();
                            CommonData.BranchName = strBranchCode[2].ToString();
                            CommonData.CompanyName = strBranchCode[3].ToString();
                            CommonData.StateCode = strBranchCode[4].ToString();
                            CommonData.BranchType = strBranchCode[2].Substring(strBranchCode[2].Length - 3, 2);

                        }
                        string[] strArrDocMon = cbDocmentMonth.SelectedValue.ToString().Split('@');
                        CommonData.SetCompanyDocMonthDetails(cbDocmentMonth.Text.ToString()
                            , Convert.ToDateTime(strArrDocMon[1]).ToString("dd/MM/yyyy")
                            , Convert.ToDateTime(strArrDocMon[2]).ToString("dd/MM/yyyy")
                            , strArrDocMon[0].ToString()
                            , CommonData.CurrentDate);
                        string strComp = CommonData.CompanyName;
                        if (strComp.ToString().Length > 0)
                        {
                            objUtil.SetInvoiceDataExistedForPostMonth();
                            MDIParent MDIForm = new MDIParent();
                            MDIForm.Text = CommonData.CompanyName + '-' + CommonData.BranchName;
                            MDIForm.Show();
                            InsertUserHistory("LOGIN");
                            this.Hide();
                        }
                        objUtil = null;
                    }
                    else
                    {
                        MessageBox.Show("No Document month for " + cbCompany.Text.ToString() +
                            "\nPlease contact to IT - Department", "SS CRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().IndexOf("network") > -1)
                    MessageBox.Show("Database connection problem, Please check network.\nContact to IT Department");
                else
                    MessageBox.Show(ex.Message.ToString());
            }
            finally
            {

                objData = null;
            }
        }

        private void InsertUserHistory(string STATUS)
        {
            try
            {
                objSQLdb = new SQLDB();
                string sqlInsert = "INSERT INTO USER_HISTORY (UH_USER_ID,UH_DATE_TIME,UH_STATUS) VALUES('" + CommonData.LogUserId + "',GETDATE(),'" + STATUS+"')";

                int intRec = objSQLdb.ExecuteSaveData(sqlInsert);
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objSQLdb = null;
                
            }
        }

        private bool CheckData()
        {
            bool dBool = true;
            if (txtUserId.Text.ToString().Trim() == string.Empty)
            {
                MessageBox.Show("Enter User Id!", "SS CRM");
                txtUserId.Focus();
                dBool = false;
                return dBool;
            }
            if (txtPassword.Text.ToString().Trim() == string.Empty)
            {
                MessageBox.Show("Enter Pasword!", "SS CRM");
                txtPassword.Focus();
                dBool = false;
                return dBool;
            }


            return dBool;
        }

        private void Login_Enter(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > -1)
            {
                FillBranchData();
                FillDocumentMonthData();
            }
        }


        private void CompanyDetails(bool TrueFalse)
        {
            lblCompany.Visible = TrueFalse;
            lblBranch.Visible = TrueFalse;
            cbCompany.Visible = TrueFalse;
            cbBranch.Visible = TrueFalse;
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtPassword.Text.ToString().Trim().Length > 3)
            //{
            //    cbDocmentMonth.DataSource = null;
            //    cbDocmentMonth.Items.Clear();
            //    if (txtUserId.Text.ToString().Trim().ToUpper() == "ADMIN")
            //    {
            //        if (cbCompany.Items.Count == 0)
            //            FillCompanyData();

            //        CompanyDetails(true);
            //    }
            //    else
            //    {
            //        CompanyDetails(false);
            //    }
            //}
        }

        private void txtPassword_Validated(object sender, EventArgs e)
        {
            cbDocmentMonth.DataSource = null;
            cbDocmentMonth.Items.Clear();
            if (txtUserId.Text.ToString().Trim().ToUpper() == "ADMIN")
            {
                if (cbCompany.Items.Count == 0)
                    FillCompanyData();

                CompanyDetails(true);
            }
            else
            {
                CompanyDetails(false);
            }
            Hashtable htComp = new Hashtable();
            objData = new Security();
            try
            {
                if (txtUserId.Text.ToString().Trim().Length > 2 || txtPassword.Text.ToString().Trim().Length > 2)
                {
                    if (cbUserBranch.DataSource != null)
                        cbUserBranch.DataSource = null;
                    cbUserBranch.Items.Clear();

                    if (cbDocmentMonth.DataSource != null)
                        cbDocmentMonth.DataSource = null;
                    cbDocmentMonth.Items.Clear();
                    DataTable dt = objData.UserLogin(txtUserId.Text.ToString(), txtPassword.Text.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (!txtUserId.Text.Contains("admin"))
                        {
                            FillUserBranch();
                        }
                        htComp = objData.GetCompanyDocumentMonth();
                        if (htComp.Count > 0)
                        {
                            CommonData.SetCompanyDocMonthDetails(htComp["DocMonth"].ToString()
                                        , Convert.ToDateTime(htComp["DocFDate"]).ToString("dd/MM/yyyy")
                                        , Convert.ToDateTime(htComp["DocTDate"]).ToString("dd/MM/yyyy")
                                        , htComp["FYear"].ToString()
                                        , Convert.ToDateTime(htComp["CurDate"]).ToString("dd/MMM/yyyy"));
                            string strComp = CommonData.CompanyName;

                            CommonData.SetCompanyDetails(dt.Rows[0]["companycode"].ToString(), dt.Rows[0]["companyname"].ToString(), dt.Rows[0]["userid"].ToString());
                        }
                        FillDocumentMonthData();
                    }
                    else
                    {
                        MessageBox.Show("Invalid User ID, Password. ", "SS CRM Log In", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    dt.Dispose();
                    dt = null;
                }
                else
                {
                    MessageBox.Show("Enter Log In Information!", "SS CRM Log In");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().IndexOf("network") > -1)
                    MessageBox.Show("Database connection problem, Please check network.\nContact to IT Department");
                else
                    MessageBox.Show(ex.Message.ToString());
            }
            finally
            {

                objData = null;
            }

        }

        private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUserBranch.Items.Count > 1)
                FillDocumentMonthData();
        }        
    }
}
