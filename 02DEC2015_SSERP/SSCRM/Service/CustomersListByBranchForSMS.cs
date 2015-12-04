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
    public partial class CustomersListByBranchForSMS : Form
    {
        SQLDB objDB = null;
        InvoiceDB objInvDB = null;
        string sType = "", sState = "", sDist = "";
        public CustomersListByBranchForSMS()
        {
            InitializeComponent();
        }

        private void CustomersListByBranchForSMS_Load(object sender, EventArgs e)
        {
            FillCompanyDdl();
        }

        private void FillCompanyDdl()
        {
            objDB = new SQLDB();
            try
            {
                DataTable dtCpy = objDB.ExecuteDataSet("SELECT CM_Company_Code, CM_Company_Name FROM Company_Mas WHERE ACTIVE = 'T'" +
                                                        " ORDER BY CM_Company_Name", CommandType.Text).Tables[0];
                //objSecurity.GetCompanyDataSet().Tables[0];
                UtilityLibrary.PopulateControl(cbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDB = null;
            }
            
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranch.SelectedIndex > 0)
            {
                sType = "S";
                objInvDB = new InvoiceDB();
                try
                {
                    DataTable dtStates = objInvDB.GetVillagesDdlForCustList(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), "", "", "", "", "", "", "", 0, 0, sType).Tables[0];
                    UtilityLibrary.PopulateControl(cbState, dtStates.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objInvDB = null;
                }

            }
            else
            {
                sType = "";
                cbState.DataSource = null;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                objDB = new SQLDB();
                try
                {
                    DataTable dtBranch = objDB.ExecuteDataSet("SELECT branch_code as branch_code, branch_name  as branch_name,branch_Type,ACTIVE FROM branch_mas " +
                                                            "WHERE branch_name<>'' AND BRANCH_TYPE='BR' AND upper(company_code)=Upper('" + cbCompany.SelectedValue.ToString() + "') Order by branch_name").Tables[0];
                    UtilityLibrary.PopulateControl(cbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }
            }
            else
            {
                cbBranch.DataSource = null;
            }
            
        }

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbState.SelectedIndex > 0)
            {
                sType = "D";
                objInvDB = new InvoiceDB();
                try
                {
                    DataTable dtDist = objInvDB.GetVillagesDdlForCustList(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbState.SelectedValue.ToString(), "", "", "", "", "", "", 0, 0, sType).Tables[0];
                    UtilityLibrary.PopulateControl(cbDistrict, dtDist.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objInvDB = null;
                }

            }
            else
            {
                cbDistrict.DataSource = null;
                sType = "";
            }
        }

        private void cbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDistrict.SelectedIndex > 0)
            {
                sType = "V";
                objInvDB = new InvoiceDB();
                try
                {
                    DataTable dtVill = objInvDB.GetVillagesDdlForCustList(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbState.SelectedValue.ToString()
                        , cbDistrict.SelectedValue.ToString(), "", "", "", "", "", 0, 0, sType).Tables[0];
                    UtilityLibrary.PopulateControl(cbVillage, dtVill.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objInvDB = null;
                }

            }
            else
            {
                sType = "";
                cbVillage.DataSource = null;
            }
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > 0)
            {
                sType = "ALLC";
                objInvDB = new InvoiceDB();
                try
                {
                    DataTable dtAllC = objInvDB.GetVillagesDdlForCustList(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbState.SelectedValue.ToString()
                        , cbDistrict.SelectedValue.ToString(), cbVillage.SelectedValue.ToString(), "", "", "", "", 0, 0, sType).Tables[0];
                    //UtilityLibrary.PopulateControl(cbVillage, dtAllC.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objInvDB = null;
                }

            }
            else
            {
                sType = "";
                
            }
        }
    }
}
