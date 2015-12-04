using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class Damaged_Stock_Received_Details : Form
    {
        SQLDB objSQLdb = null;
        private int iForm = 0;
        public Damaged_Stock_Received_Details()
        {
            InitializeComponent();
        }
        public Damaged_Stock_Received_Details(int iType)
        {
            iForm = iType;
            InitializeComponent();
        }

        private void Damaged_Stock_Received_Details_Load(object sender, EventArgs e)
        {
            FillBranchData();
            FillSingleProducts();
            objSQLdb = new SQLDB();
            FromdtpDocMonth.Value = DateTime.Today;
            TodtpDocMonth.Value = DateTime.Today;

            if (iForm == 1)
                this.Text = "Damage Stock Details";
            if (iForm == 2)
                this.Text = "Refill Statement";
            if (iForm == 3)
            {
                this.Text = "Form N Register";
                cbUnit.Visible = false;
                lblBranch.Visible = false;
                FillFCOProducts();
                lblFromMonth.Text = "From Date";
                lblToMonth.Text = "To Date";
                FromdtpDocMonth.CustomFormat = "dd/MMM/yyyy";
                TodtpDocMonth.CustomFormat = "dd/MMM/yyyy";
            }
                    

        }
        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            
            try
            {

                DataTable dtUnits = objSQLdb.ExecuteDataSet("SELECT BRANCH_NAME,BRANCH_CODE  FROM BRANCH_MAS " +
                                                        " WHERE  ACTIVE='T' Order by BRANCH_NAME ").Tables[0];
                UtilityLibrary.AutoCompleteComboBox(cbUnit, dtUnits, "", "BRANCH_NAME");
                if (dtUnits.Rows.Count > 0)
                {
                    DataRow dr = dtUnits.NewRow();
                    dr[0] = "ALL";
                    dr[1] = "ALL";

                    dtUnits.Rows.InsertAt(dr, 0);

                    cbUnit.DataSource = dtUnits;
                    cbUnit.DisplayMember = "BRANCH_NAME";
                    cbUnit.ValueMember = "BRANCH_CODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                //dt = null;
            }

        }

        private void FillFCOProducts()
        {         
            objSQLdb = new SQLDB();
            cbProbuct.DataSource = null ;

            //DataTable dt = new DataTable();


            try
            {
                DataTable dtProducts = objSQLdb.ExecuteDataSet("SELECT DISTINCT PM_PRODUCT_NAME,PM_PRODUCT_ID FROM PRODUCT_MAS where PM_PRODUCT_TYPE = 'SNGPK' "
                                      +" and PM_BRAND_ID in('VIJETHA','SUPREME','WARRIOR')  ORDER BY PM_PRODUCT_NAME").Tables[0];
                UtilityLibrary.AutoCompleteComboBox(cbProbuct, dtProducts, "", "PM_PRODUCT_NAME");

                if (dtProducts.Rows.Count > 0)
                {
                    DataRow row = dtProducts.NewRow();
                    row[0] = "ALL";
                    row[1] = "ALL";
                    dtProducts.Rows.InsertAt(row, 0);
                    cbProbuct.DataSource = dtProducts;
                    cbProbuct.DisplayMember = "PM_PRODUCT_NAME";
                    cbProbuct.ValueMember = "PM_PRODUCT_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLdb = null;
            }

        }

        private void FillSingleProducts()
        {
            objSQLdb = new SQLDB();

            //DataTable dt = new DataTable();


            try
            {
                DataTable dtProducts = objSQLdb.ExecuteDataSet("SELECT DISTINCT PM_PRODUCT_NAME,PM_PRODUCT_ID FROM PRODUCT_MAS where PM_PRODUCT_TYPE = 'SNGPK' ORDER BY PM_PRODUCT_NAME").Tables[0];
                UtilityLibrary.AutoCompleteComboBox(cbProbuct, dtProducts, "", "PM_PRODUCT_NAME");
                
                if (dtProducts.Rows.Count > 0)
                {
                    DataRow row = dtProducts.NewRow();
                    row[0] = "ALL";
                    row[1] = "ALL";
                    dtProducts.Rows.InsertAt(row, 0);
                    cbProbuct.DataSource = dtProducts;
                    cbProbuct.DisplayMember = "PM_PRODUCT_NAME";
                    cbProbuct.ValueMember = "PM_PRODUCT_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLdb = null;
            }
        }

        private void cbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            // {
            //          objSQLdb = new SQLDB();

            //          string strCommand = "SELECT BRANCH_NAME,BRANCH_CODE  FROM BRANCH_MAS " +
            //                              " WHERE  ACTIVE='T' Order by BRANCH_NAME ";
            //                 //" WHERE SPGH_DOCUMENT_MONTH BETWEEN '" + FromdtpDocMonth.Value.ToString() + "' AND '" + TodtpDocMonth.Value.ToString() + "'";

            //          DataTable dtUnits = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

            //        UtilityLibrary.AutoCompleteComboBox(cbUnit, dtUnits, "", "BRANCH_NAME");
            //          objSQLdb = null;
            //      }
            //      catch { }
            //      finally
            //      {
            //          objSQLdb = null;

            //      }           

        }

        private void cbProbuct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try

            // {
            //      objSQLdb = new SQLDB();

            //      string StrCommand = "SELECT DISTINCT PM_PRODUCT_ID,PM_PRODUCT_NAME FROM PRODUCT_MAS " +
            //                 "INNER JOIN SP_GRN_DETL ON SPGD_COMPANY_CODE=PM_COMPANY_CODE INNER JOIN SP_GRN_HEAD ON SPGH_COMPANY_CODE=PM_COMPANY_CODE " +
            //                 " WHERE PM_PRODUCT_TYPE IN('SNGPK') ";
            //      DataTable dtProducts = objSQLdb.ExecuteDataSet(StrCommand).Tables[0];
                
            //      UtilityLibrary.AutoCompleteComboBox(cbProbuct, dtProducts, "", "PM_PRODUCT_NAME");
            //      objSQLdb = null;
            //  }
            //  catch { }
            //  finally
            //  {
            //      objSQLdb = null;

            //  }
        }

        private void cbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);        
        }

        private void cbProbuct_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);        
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(cbUnit.SelectedValue.ToString()+"\n"+cbProbuct.SelectedValue.ToString());
            string sRepType = "";
            if (cbUnit.SelectedValue.ToString() != "ALL")
                sRepType = "DEST";
            if (iForm == 1)
            {
                CommonData.ViewReport = "SP_GRN_DAMAGE_STOCK_DETLS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString()
                                                    , FromdtpDocMonth.Value.ToString("MMMyyyy"), TodtpDocMonth.Value.ToString("MMMyyyy")
                                                    , cbUnit.SelectedValue.ToString(), cbProbuct.SelectedValue.ToString(), sRepType);
                objReportview.Show();
            }
            if (iForm == 2)
            {
                CommonData.ViewReport = "SP_REFILL_STATEMENT_DETLS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString()
                                                    , FromdtpDocMonth.Value.ToString("MMMyyyy"), TodtpDocMonth.Value.ToString("MMMyyyy")
                                                    , cbUnit.SelectedValue.ToString(), cbProbuct.SelectedValue.ToString(), sRepType);
                objReportview.Show();
            }
            if (iForm == 3)
            {
                CommonData.ViewReport = "SSERP_REP_SP_FORM_N_REGISTER";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), cbProbuct.SelectedValue.ToString()
                                                    , FromdtpDocMonth.Value.ToString("dd/MMM/yyyy").ToUpper(), TodtpDocMonth.Value.ToString("dd/MMM/yyyy").ToUpper()
                                                    ,"FORM_N_REG");
                objReportview.Show();
            }
        }
    }
}
