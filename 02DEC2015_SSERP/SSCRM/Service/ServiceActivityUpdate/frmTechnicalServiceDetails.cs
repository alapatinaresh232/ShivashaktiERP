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
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class frmTechnicalServiceDetails : Form
    {
        ServiceDB objServiceDB;
        DataSet ds;
        public ActivityServiceUpdate objActivityServiceUpdate;
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;
        SQLDB objSQLdb = null;
        private string BranchCode = "", sActivityID = "", InvoiceNo = "", sCheckVal = "", FinYear = "",
            sOrderNo = "", strCmd = "", sEcode = "",sActDate="",sProductId="";
        DateTime dtpTarDate;
        private DateTime dtpActDate;
        DataTable dt = new DataTable();

        string DeptCode = "800000";

        public frmTechnicalServiceDetails(string BCode, string FYear, string ActID, string InvNo, string OrderNo,string strEcode,string sdate,string stProdId)
        {
            InitializeComponent();
            BranchCode = BCode;
            sActivityID = ActID;
            InvoiceNo = InvNo;
            FinYear = FYear;
            sOrderNo = OrderNo;
            sEcode = strEcode;
            sActDate = sdate;
            sProductId = stProdId;
        }

        private void frmTechnicalServiceDetails_Load(object sender, EventArgs e)
        {
            cbActivityMode.SelectedIndex = 0;
            dtpActivityDate.Value = DateTime.Today;
            objServiceDB = new ServiceDB();
            ds = objServiceDB.GetECodesforService(BranchCode, DeptCode);

            try
            {
                EcodeSearch();
                FillEmployeeData();
                objSQLdb = new SQLDB();

                strCmd = "exec Get_ServiceActivityDetails '" + CommonData.CompanyCode + "','" + BranchCode + "','" + FinYear + "','" + InvoiceNo + "','" + sOrderNo + "'," + sActivityID + ",'"+ sProductId +"'";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows[0]["AttendedEcode"].ToString() != "")
                {
                    sEcode = dt.Rows[0]["AttendedEcode"].ToString();
                }
                txtServiceSearch.Text = sEcode;
                if (sEcode.Length > 1)
                    txtServiceSearch_KeyUp(null, null);
                if (sEcode != "")
                {
                    cmbEmployee.SelectedValue = sEcode;
                }
                txtOrderNo.Text = sOrderNo;
                txtInvNo.Text = InvoiceNo;
                txtProdName.Text = dt.Rows[0]["ProductName"].ToString();
                txtProdName.Tag = dt.Rows[0]["ProductId"].ToString();
                txtActivityName.Text = dt.Rows[0]["ActivityName"].ToString();
                txtInvQty.Text = dt.Rows[0]["Qty"].ToString();

                if (sActDate.Length != 0)
                {
                    txtServiceSearch.ReadOnly = true;
                    cmbEmployee.Enabled = false;
                    dtpActivityDate.Enabled = false;
                    dtpActivityDate.Value = Convert.ToDateTime(sActDate);
                }
                else
                {
                    txtServiceSearch.ReadOnly = false;
                    cmbEmployee.Enabled = true;
                    dtpActivityDate.Enabled = true;
                }
                if (dt.Rows[0]["ActualDate"].ToString() != "")
                {
                    dtpActivityDate.Value = Convert.ToDateTime(dt.Rows[0]["ActualDate"].ToString());
                }

                txtIrrigationSuggestion.Text = dt.Rows[0]["Suggestion_For_Irrigation"].ToString();
                txtManuringSuggestion.Text = dt.Rows[0]["Suggestion_For_manuring"].ToString();
                txtObservDisease.Text = dt.Rows[0]["Observ_Disease"].ToString();
                txtObservIrrigation.Text = dt.Rows[0]["Observ_Irrigation"].ToString();
                txtObservManuring.Text = dt.Rows[0]["Observ_Manuring"].ToString();
                txtObservPrunining.Text = dt.Rows[0]["Observ_Pruning"].ToString();
                txtObservWeeding.Text = dt.Rows[0]["Observ_Weeding"].ToString();
                txtPruningSuggestion.Text = dt.Rows[0]["Suggestion_For_Pruning"].ToString();
                txtWeedingSuggeston.Text = dt.Rows[0]["Suggestion_For_Weeding"].ToString();
                txtSugForDiseases.Text = dt.Rows[0]["Suggestion_For_Diseases"].ToString();
                txtPlantAge.Text = dt.Rows[0]["PlantAge"].ToString();
                txtPlantHeight.Text = dt.Rows[0]["PlantHeight"].ToString();
                txtPlantWidth.Text = dt.Rows[0]["PlantWidth"].ToString();                             
                txtRemarks.Text = dt.Rows[0]["FarmerRemarks"].ToString();
                if (dt.Rows[0]["ActivityMode"].ToString() != "")
                    cbActivityMode.Text = dt.Rows[0]["ActivityMode"].ToString();
                else
                    cbActivityMode.SelectedIndex = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                
        }


        public void GetECodeData(string BranchCode)
        {
            objServiceDB = new ServiceDB();

            DataSet dsData = objServiceDB.GetECodesforService(BranchCode, DeptCode);
            objServiceDB = null;
            UtilityLibrary.PopulateControl(cmbEmployee, dsData.Tables[0].DefaultView, 1, 0, "-- Please select --", 0);
            if (sCheckVal == "1")
            {
                if (sEcode != "")
                    cmbEmployee.SelectedValue = sEcode.Split('-')[0].ToString();
            }
        }

        private void EcodeSearch()
        {
            SQLDB objData = new SQLDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cmbEmployee.DataSource = null;
                cmbEmployee.Items.Clear();
                dsEmp = objData.ExecuteDataSet("SELECT top 10 * FROM(SELECT ECODE,CAST(ECODE AS VARCHAR)+' - '+MEMBER_NAME+' ('+DESIG+')' ENAME,DESIG  " +
                                "FROM EORA_MASTER WHERE DEPT_ID='800000' AND MEMBER_NAME IS NOT NULL AND EORA = 'E') EMP WHERE ENAME LIKE '%" + txtServiceSearch.Text.ToString().Trim() + "%'");
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cmbEmployee.DataSource = dtEmp;
                    cmbEmployee.DisplayMember = "ENAME";
                    cmbEmployee.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cmbEmployee.SelectedIndex > -1)
                {
                    cmbEmployee.SelectedIndex = 0;
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }
        
        private bool CheckData()
        {
            bool flag = true;
           
            if (cmbEmployee.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Employee", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEmployee.Focus();
                return flag;
            }
            if (dtpActivityDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Activity Date Should Be Less than toDay's Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpActivityDate.Focus();
                return flag;
            }
                      
            return flag;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRetVal = 0;
            string sActivityMode = "";

            if (CheckData() == true)
            {
                try
                {
                    if (cbActivityMode.SelectedIndex > 0)
                    {
                        sActivityMode = cbActivityMode.Text.ToString();
                    }
                    else
                    {
                        sActivityMode = "";
                    }
                    if (txtPlantAge.Text.Length == 0)
                    {
                        txtPlantAge.Text = "0";
                    }
                    if (txtPlantHeight.Text.Length == 0)
                    {
                        txtPlantHeight.Text = "0";
                    }
                    if (txtPlantWidth.Text.Length == 0)
                    {
                        txtPlantWidth.Text = "0";
                    }

                    string[] sqlEcode = cmbEmployee.Text.ToString().Split('-');

                    string sqlUpdate = "UPDATE SERVICES_TNA SET TNA_ACTUAL_DATE='" + Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MMM/yyyy") +
                                        "',TNA_ATTEND_BY_ECODE=" + cmbEmployee.SelectedValue +
                                        ",TNA_FARMER_REMARKS='" + txtRemarks.Text.ToString().Replace("'", " ") +                                      
                                       "',TNA_CREATED_BY='" + CommonData.LogUserId + 
                                       "', TNA_CREATED_DATE=getdate() " +                                      
                                       ", TNA_ACTIVITY_MODE='" + sActivityMode +                                      
                                       "',TNA_PLANT_AGE="+ Convert.ToInt32(txtPlantAge.Text) +
                                       ",TNA_PLANT_HEIGHT="+ Convert.ToInt32(txtPlantHeight.Text) +
                                       ",TNA_PLANT_WIDTH="+ Convert.ToInt32(txtPlantWidth.Text) +
                                       ",TNA_OBS_DISEASES='"+ txtObservDisease.Text.ToString().Replace("'","") +
                                       "',TNA_OBS_MANURING='"+ txtObservManuring.Text.ToString().Replace("'","") +
                                       "',TNA_OBS_WEEDING='"+ txtObservWeeding.Text.ToString() +
                                       "',TNA_OBS_PRUNING='"+ txtObservPrunining.Text.ToString().Replace("'","") +
                                       "',TNA_OBS_IRRIGATION='"+ txtObservIrrigation.Text.ToString().Replace("'","") +
                                       "',TNA_SUGG_FOR_DISEASES='"+ txtSugForDiseases.Text.ToString().Replace("'","") +
                                       "',TNA_SUGG_FOR_MANURING='"+ txtManuringSuggestion.Text.ToString().Replace("'","") +
                                       "',TNA_SUGG_FOR_WEEDING='"+ txtWeedingSuggeston.Text.ToString().Replace("'","") +
                                       "',TNA_SUGG_FOR_PRUNING='"+ txtPruningSuggestion.Text.ToString().Replace("'","") +
                                       "',TNA_SUGG_FOR_IRRIGATION='"+ txtIrrigationSuggestion.Text.ToString().Replace("'","") +
                                       "' WHERE TNA_BRANCH_CODE='" + BranchCode + "' AND TNA_FIN_YEAR='" + FinYear +
                                       "' AND TNA_ACTIVITY_ID=" + sActivityID + " and TNA_PRODUCT_ID='" + txtProdName.Tag.ToString() + 
                                       "' AND TNA_INVOICE_NUMBER=" + InvoiceNo;

                  
                    if (sqlUpdate.Length > 5)
                    {
                        iRetVal = objSQLdb.ExecuteSaveData(sqlUpdate);
                    }

                    //if (sEcode.Length > 0 && sActDate.Length > 0)
                    //{
                    //    objEmployeeDARWithTourBills.FillEmployeeActivityDetails(Convert.ToInt32(sEcode), sActDate);
                    //}

                    ((ActivityServiceUpdate)objActivityServiceUpdate).GetAllInvoicdeData(sOrderNo, InvoiceNo);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIrrigationSuggestion.Text = "";
            txtManuringSuggestion.Text = "";
            txtObservDisease.Text = "";
            txtObservIrrigation.Text = "";
            txtObservManuring.Text = "";
            txtObservPrunining.Text = "";
            txtObservWeeding.Text = "";
            txtPruningSuggestion.Text = "";
            txtWeedingSuggeston.Text = "";
            txtSugForDiseases.Text = "";
            txtPlantAge.Text = "";
            txtPlantHeight.Text = "";
            txtPlantWidth.Text = "";
            cbActivityMode.SelectedIndex = 0;
            dtpActivityDate.Value = DateTime.Today;
            txtRemarks.Text = "";
        }

        private void txtPlantAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPlantHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPlantWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void FillEmployeeData()
        {
            objServiceDB = new ServiceDB();
            ds = objServiceDB.GetECodesforService(BranchCode, DeptCode);
            UtilityLibrary.PopulateControl(cmbEmployee, ds.Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            objServiceDB = null;

        }


        private void txtServiceSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtServiceSearch.Text.ToString().Trim().Length > 1)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void txtServiceSearch_Validated(object sender, EventArgs e)
        {
            if (txtServiceSearch.Text.ToString().Trim().Length > 1)
                EcodeSearch();
            else
                FillEmployeeData();
        }

      
    }
}
