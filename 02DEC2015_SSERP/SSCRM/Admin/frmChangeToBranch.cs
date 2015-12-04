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
namespace SSCRM
{
    public partial class frmChangeToBranch : Form
    {
        private Security objData = null;
        private UtilityDB objUtil = null;
        

        string strBCode = string.Empty;
        
        public frmChangeToBranch()
        {
            InitializeComponent();
        }

        private void frmChangeToBranch_Load(object sender, EventArgs e)
        {
            
            lblPresentBranch.Text = CommonData.BranchCode + ":" + CommonData.BranchName;
            lblPresentDocMonth.Text = CommonData.DocMonth;
            Hashtable htComp = new Hashtable();
            //FilluserDetails();
            cbDocmentMonth.DataSource = null;
            cbDocmentMonth.Items.Clear();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                cbBranch.Visible = true;
                cbCompany.Visible = true;
                FillCompanyData();
                FillBranchData();
                FillDocumentMonthData();
                //CompanyDetails(true);
            }
            else
            {
                cbUserBranch.Visible = true;
                lblCompany.Visible = false;
                FillUserBranch();
                FillDocumentMonthData();
                //CompanyDetails(false);
            }
            //Hashtable htComp = new Hashtable();
            //objData = new Security();
            //try
            //{
            //    if (CommonData.LogUserId != null)
            //    {
            //        if (cbUserBranch.DataSource != null)
            //            cbUserBranch.DataSource = null;
            //        cbUserBranch.Items.Clear();

            //        if (cbDocmentMonth.DataSource != null)
            //            cbDocmentMonth.DataSource = null;
            //        cbDocmentMonth.Items.Clear();
            //        //DataTable dt = objData.UserLogin(CommonData.LogUserId.ToString(), txtPassword.Text.ToString());
            //        //if (0)
            //        //{
            //            if (!CommonData.LogUserId.Contains("admin"))
            //            {
            //                FillUserBranch();
            //            }
            //            htComp = objData.GetCompanyDocumentMonth();
            //            if (htComp.Count > 0)
            //            {
            //                CommonData.SetCompanyDocMonthDetails(htComp["DocMonth"].ToString()
            //                            , Convert.ToDateTime(htComp["DocFDate"]).ToString("dd/MM/yyyy")
            //                            , Convert.ToDateTime(htComp["DocTDate"]).ToString("dd/MM/yyyy")
            //                            , htComp["FYear"].ToString()
            //                            , Convert.ToDateTime(htComp["CurDate"]).ToString("dd/MMM/yyyy"));
            //                string strComp = CommonData.CompanyName;

            //                CommonData.SetCompanyDetails(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), CommonData.LogUserId.ToString());
            //            }
            //            FillDocumentMonthData();
            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("Invalid User ID, Password. ", "SS CRM Log In", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        //}
            //        //dt.Dispose();
            //        //dt = null;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Log In Information!", "SS CRM Log In");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.ToString().IndexOf("network") > -1)
            //        MessageBox.Show("Database connection problem, Please check network.\nContact to IT Department");
            //    else
            //        MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{

            //    objData = null;
            //}

        }
        private void FilluserDetails()
        {
            Hashtable htComp = new Hashtable();
            objData = new Security();
            try
            {
                if (CommonData.LogUserId.Length > 2)
                {
                    if (cbUserBranch.DataSource != null)
                        cbUserBranch.DataSource = null;
                    cbUserBranch.Items.Clear();

                    if (cbDocmentMonth.DataSource != null)
                        cbDocmentMonth.DataSource = null;
                    cbDocmentMonth.Items.Clear();
                    
                    if (CommonData.LogUserId != null)
                    {
                        if (CommonData.LogUserId != "admin")
                        {
                            FillUserBranch();
                            FillDocumentMonthData();
                        }
                        else
                        {
                            FillCompanyData();
                            FillBranchData();
                            FillDocumentMonthData();
                        }

                        //htComp = objData.GetCompanyDocumentMonth();
                        //if (htComp.Count > 0)
                        //{
                        //    CommonData.SetCompanyDocMonthDetails(htComp["DocMonth"].ToString()
                        //                , Convert.ToDateTime(htComp["DocFDate"]).ToString("dd/MM/yyyy")
                        //                , Convert.ToDateTime(htComp["DocTDate"]).ToString("dd/MM/yyyy")
                        //                , htComp["FYear"].ToString()
                        //                , Convert.ToDateTime(htComp["CurDate"]).ToString("dd/MMM/yyyy"));
                        //    string strComp = CommonData.CompanyName;

                        //    CommonData.SetCompanyDetails(CommonData.CompanyCode, CommonData.CompanyName, CommonData.LogUserId);
                        //}
                        FillDocumentMonthData();
                    }
                    else
                    {
                        MessageBox.Show("Invalid User ID, Password. ", "SS CRM Log In", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    
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
        private void FillUserBranch()
        {
            objUtil = new UtilityDB();
            try
            {
                DataTable dtUB = objUtil.dtUserBranch(CommonData.LogUserId);
                //if (dtUB.Rows.Count == 0)
                //dtUB.Rows.Add(CommonData.BranchCode + '@' + CommonData.CompanyCode + '@' + CommonData.BranchName + '@' + CommonData.CompanyName, CommonData.BranchName);


                cbUserBranch.DisplayMember = "branch_name";
                cbUserBranch.ValueMember = "branch_Code";
                cbUserBranch.DataSource = dtUB;

                cbUserBranch.SelectedValue = CommonData.BranchCode + '@' + CommonData.CompanyCode + '@' + CommonData.BranchName + '@' + CommonData.CompanyName + '@' + CommonData.StateCode + '@' + CommonData.BranchType;

                dtUB = null;
                FillDocumentMonthData();
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
        private void FillDocumentMonthData()
        {
            objUtil = new UtilityDB();
            try
            {
                string[] strBranchCode;
                string strCompanyCode;
                DataTable dt;
                if (CommonData.LogUserId.Contains("admin"))
                {
                    strCompanyCode = cbCompany.SelectedValue.ToString();
                    dt = objUtil.dtCompanyDocumentMonth(strCompanyCode);
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

                    cbDocmentMonth.SelectedText = CommonData.DocMonth;
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

                    cbCompany.SelectedValue = CommonData.CompanyCode;
                    
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > -1)
            {
                FillBranchData();
                FillDocumentMonthData();
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

                    cbBranch.SelectedValue = CommonData.BranchCode;
                }
                dtCompany = null;
                dv1 = null;
                dtBR = null;
                FillDocumentMonthData();
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
        private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUserBranch.Items.Count > 1)
                FillDocumentMonthData();
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDocumentMonthData();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            objUtil = new UtilityDB();

            try
            {
                //if (CheckData())
                //{
                    if (cbDocmentMonth.SelectedIndex > -1)
                    {

                        if (CommonData.LogUserId.ToUpper() == "ADMIN")
                        {
                            CommonData.CompanyCode = cbCompany.SelectedValue.ToString();
                            CommonData.CompanyName = cbCompany.Text.ToString();
                            CommonData.BranchCode = cbBranch.SelectedValue.ToString();
                            CommonData.BranchName = cbBranch.Text.ToString();
                            CommonData.StateCode = cbBranch.SelectedValue.ToString().Substring(3, 2);
                            //CommonData.LogUserId = txtUserId.Text.ToString().Trim();
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
                            //MDIParent MDIForm = new MDIParent();
                            this.ParentForm.Text = CommonData.CompanyName + '-' + CommonData.BranchName;
                            //MDIForm.Show();
                            this.ParentForm.Refresh();
                            this.frmChangeToBranch_Load(null,null);
                            //this.Refresh();
                            this.Close();
                            //this.Hide();
                        }
                        objUtil = null;
                    }
                    else
                    {
                        MessageBox.Show("No Document month for " + cbCompany.Text.ToString() +
                            "\nPlease contact to IT - Department", "SS CRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                //}
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

    }

}
     

    

