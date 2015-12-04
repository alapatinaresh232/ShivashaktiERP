using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;
using SSTrans;
namespace SSCRM
{
    public partial class Calibration_Certificate : Form
    {
        private SQLDB objDB = null;      
        bool flagUpdate = false;
        private bool blIsCellQty = true;
        DateTime dtpStDate;
        DateTime dtpEndDate;
        DateTime dtpMaxDate;
        int Max = 0;
        public Calibration_Certificate()
        {
            InitializeComponent();
        }

        private void Calibration_Certificate_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            string AssetType = "WEIGHING MACHINE";
            txtAssetType.Text = AssetType;
            GetAssetSlNoDetails();
        }
        private void GetAssetSlNoDetails()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                strCmd = "SELECT DISTINCT FAMR_ASSET_SL_NO  FROM FIXED_ASSETS_MOVEMENT_REG " +
                    " INNER JOIN BRANCH_MAS ON BRANCH_CODE = FAMR_TO_BRANCH_CODE  " +
                    " INNER JOIN FIXED_ASSETS_HEAD ON FAH_ASSET_SL_NO=FAMR_ASSET_SL_NO " +
                    " WHERE FAMR_TO_BRANCH_CODE ='" + CommonData.BranchCode + "'  AND FAH_ASSET_TYPE='WEIGHING MACHINE' AND FAMR_STATUS ='WORKING' " +
                    " AND EXISTS (SELECT  AssetSlNo FROM (SELECT FAMR_ASSET_SL_NO AssetSlNo, MAX(FAMR_GIVEN_DATE) IssueDate " +
                    "FROM FIXED_ASSETS_MOVEMENT_REG GROUP BY FAMR_ASSET_SL_NO) tempMov WHERE AssetSlNo = FAMR_ASSET_SL_NO AND FAMR_GIVEN_DATE = IssueDate)";

                dt = objDB.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    //row[1] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cmbAssetSlNo.DataSource = dt;
                    cmbAssetSlNo.DisplayMember = "FAMR_ASSET_SL_NO";
                    cmbAssetSlNo.ValueMember = "FAMR_ASSET_SL_NO";
                }


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
        private bool CheckData()
        {
            bool bflag = true;
            if (cmbAssetSlNo.SelectedIndex == 0)
            {
                MessageBox.Show("Select Asset SlNO", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtCertificateNo.Text == "")
            {
                MessageBox.Show("Enter Certificate NO", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtWeight.Text == "")
            {
                MessageBox.Show("Enter Weight ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtCertCost.Text == "")
            {
                MessageBox.Show("Enter Certificate Cost ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtContactName.Text == "")
            {
                MessageBox.Show("Enter Owner Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMobileNo.Text == "")
            {
                MessageBox.Show("Enter mobile No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (Convert.ToDateTime(dtpFromDate.Value) >= Convert.ToDateTime(dtpToDate.Value))
            {
                MessageBox.Show("Invalid From/To Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return bflag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string sqlText = "";
            int iRes = 0;
            int iSlNo = 0;
            //iSlNo = gvLicence.Rows.Count + 1;         
            objDB = new SQLDB();
            if (CheckData() == true)
            {
                try
                {
                    if (flagUpdate == true)
                    {
                        iSlNo = gvLicence.Rows.Count; 

                        sqlText = "UPDATE  WEIGH_MACHINE_CALI_CERT SET WMCS_COMP_CODE='" + CommonData.CompanyCode +
                                                                    "', WMCS_BRANCH_CODE='" + CommonData.BranchCode +
                                                                    "',WMCS_STATE_CODE ='" + CommonData.StateCode +
                                                                    "',WMCS_SL_NO=" + iSlNo +
                                                                    ", WMCS_CERTIFICATE_NO=" + txtCertificateNo.Text +
                                                                    ", WMCS_CERTIFICATE_COST=" + txtCertCost.Text +
                                                                    ", WMCS_WEIGHT_KGS=" + txtWeight.Text +
                                                                    ", WMCS_VALID_FROM='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy") +
                                                                    "',WMCS_VALID_TO='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +                                                                  
                                                                    "',WMCS_CONTACT_NAME='" + txtContactName.Text +
                                                                    "',WMCS_MOBILE_NO=" +txtMobileNo.Text +  
                                                                    ", WMCS_MODIFIED_BY='" + CommonData.LogUserId +
                                                                    "',WMCS_MODIFIED_DATE=getdate()" +
                                                                    "  WHERE WMCS_ASSET_SLNO='" + cmbAssetSlNo.SelectedValue.ToString() +
                                                                    "' AND WMCS_COMP_CODE='" + CommonData.CompanyCode +
                                                                    "' AND WMCS_BRANCH_CODE='" + CommonData.BranchCode +
                                                                    "' AND WMCS_CERTIFICATE_NO=" + txtCertificateNo.Text + "";
                    }
                    else
                    {                      
                        iSlNo = gvLicence.Rows.Count + 1;                     

                        sqlText += " UPDATE WEIGH_MACHINE_CALI_CERT SET WMCS_STATUS='CLOSED'  WHERE WMCS_ASSET_SLNO='" + cmbAssetSlNo.SelectedValue.ToString() +
                                                                      "' AND WMCS_VALID_TO= (SELECT CAST( MAX(WMCS_VALID_TO) AS DATE) AS WMCS_VALID_TO FROM WEIGH_MACHINE_CALI_CERT " +
                                                                      " WHERE WMCS_ASSET_SLNO='" + cmbAssetSlNo.SelectedValue.ToString() + "')";                     
                        sqlText += "INSERT INTO WEIGH_MACHINE_CALI_CERT( WMCS_COMP_CODE" +
                                                                       ",WMCS_STATE_CODE" +
                                                                       ",WMCS_BRANCH_CODE" +
                                                                       ",WMCS_ASSET_SLNO" +
                                                                       ",WMCS_SL_NO " +
                                                                       ",WMCS_CERTIFICATE_NO" +
                                                                       ",WMCS_CERTIFICATE_COST" +
                                                                       ",WMCS_WEIGHT_KGS " +
                                                                       ",WMCS_VALID_FROM " +
                                                                       ",WMCS_VALID_TO " +
                                                                       ",WMCS_CONTACT_NAME" +
                                                                       ",WMCS_MOBILE_NO" +
                                                                       ",WMCS_STATUS " +
                                                                       ",WMCS_CREATED_BY " +
                                                                       ",WMCS_CREATED_DATE)VALUES(" +
                                                                       "'" + CommonData.CompanyCode +
                                                                       "','" + CommonData.StateCode +
                                                                       "','" + CommonData.BranchCode +
                                                                       "','" + cmbAssetSlNo.SelectedValue.ToString() +
                                                                       "'," + iSlNo +
                                                                       "," + txtCertificateNo.Text +
                                                                       "," + txtCertCost.Text +
                                                                       "," + txtWeight.Text +
                                                                       ",'" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy") +
                                                                       "','" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                                                       "','" + txtContactName.Text +
                                                                       "'," + txtMobileNo.Text +
                                                                       ",'RUNNING' " +
                                                                       ",'" + CommonData.LogUserId +
                                                                       "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "')";
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
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbAssetSlNo.SelectedIndex = 0;
            txtAssetMake.Text = "";
            txtAssetModel.Text = "";
            txtCertificateNo.Text = "";
            txtCertCost.Text = "";
            txtWeight.Text = "";
            txtContactName.Text = "";
            txtMobileNo.Text = "";
            dtpFromDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            gvLicence.Rows.Clear();
            flagUpdate = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cmbAssetSlNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (cmbAssetSlNo.SelectedIndex > 0)
            {
                FillCerificationDataToGrid();
                try
                {
                    objDB = new SQLDB();
                    string strCmd = "SELECT FAH_ASSET_MAKE,FAH_MODEL FROM FIXED_ASSETS_HEAD " +
                                    "INNER JOIN FIXED_ASSETS_MOVEMENT_REG ON FAMR_ASSET_SL_NO=FAH_ASSET_SL_NO " +
                                    "WHERE FAMR_ASSET_SL_NO='" + cmbAssetSlNo.SelectedValue.ToString() + "'";
                    dt = objDB.ExecuteDataSet(strCmd).Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        txtAssetMake.Text = dt.Rows[0]["FAH_ASSET_MAKE"].ToString();
                        txtAssetModel.Text = dt.Rows[0]["FAH_MODEL"].ToString();
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

            }
            else
            {
                btnCancel_Click(null, null);
            }

        }

        private void txtCertificateNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsDigit((e.KeyChar)))
            //    {
            //        e.Handled = true;
            //    }
            //}
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_KeyPress(sender, e);
            //if (e.KeyChar != '\b' & (e.KeyChar == 46))
            //{
            //    if ((!char.IsDigit((e.KeyChar)) & (e.KeyChar) != -1)) 
            //    {
            //        e.Handled = true;
            //    }
            //}
            // //checks to make sure only 1 decimal is allowed
            //if (e.KeyChar == 46)
            //{
            //    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
            //        e.Handled = true;
               
            //}

        }

        private DataSet CalibrationCertificateDetails(string AssetSlNo)
        {
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objDB.CreateParameter("@xAssetSlNo", DbType.String, AssetSlNo, ParameterDirection.Input);

                ds = objDB.ExecuteDataSet("Get_Calibration_Certificate_details", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objDB = null;
            }
            return ds;
        }
        private DataSet CertificateNumberDetails(string AssetSlNo,int CertNo)
        {
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objDB.CreateParameter("@xAssetSlNo", DbType.String, AssetSlNo, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@xCertificateNo", DbType.Int32, CertNo, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("Get_Calibration_Certificate_NO_Deatails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objDB = null;
            }
            return ds;
        }
        private void FillCerificationDataToGrid()
        {

            objDB = new SQLDB();
            DataTable dt = new DataTable();
            gvLicence.Rows.Clear();
            try
            {
                dt = CalibrationCertificateDetails(cmbAssetSlNo.SelectedValue.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (dt.Rows.Count > 0)
            {
                dtpMaxDate = Convert.ToDateTime(dt.Rows[0]["ValidTo"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {                  
                    gvLicence.Rows.Add();
                    gvLicence.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                    gvLicence.Rows[i].Cells["CompanyCode"].Value = dt.Rows[i]["CompanyCode"].ToString();
                    gvLicence.Rows[i].Cells["StateCode"].Value = dt.Rows[i]["StateCode"].ToString();
                    gvLicence.Rows[i].Cells["BranchCode"].Value = dt.Rows[i]["BranchCode"].ToString();
                    gvLicence.Rows[i].Cells["AssetSlNo"].Value = dt.Rows[i]["AssetSlNo"].ToString();
                    gvLicence.Rows[i].Cells["AssetName"].Value = dt.Rows[i]["AssetName"].ToString();
                    gvLicence.Rows[i].Cells["AssetModel"].Value = dt.Rows[i]["AssetModel"].ToString();
                    gvLicence.Rows[i].Cells["CertificateNo"].Value = dt.Rows[i]["CertificateNo"].ToString();
                    gvLicence.Rows[i].Cells["CertificateCost"].Value = dt.Rows[i]["CertificateCost"].ToString();
                    gvLicence.Rows[i].Cells["Weight"].Value = dt.Rows[i]["Weight"].ToString();
                    gvLicence.Rows[i].Cells["ContactName"].Value = dt.Rows[i]["ContactName"].ToString();
                    gvLicence.Rows[i].Cells["MobileNo"].Value = dt.Rows[i]["MobileNo"].ToString();
                    gvLicence.Rows[i].Cells["ValidFrom"].Value = Convert.ToDateTime(dt.Rows[i]["ValidFrom"]).ToString("dd-MMM-yyyy").ToUpper();
                    gvLicence.Rows[i].Cells["ValidTo"].Value = Convert.ToDateTime(dt.Rows[i]["ValidTo"]).ToString("dd-MMM-yyyy").ToUpper();
                    gvLicence.Rows[i].Cells["Status"].Value = dt.Rows[i]["CalbrateStatus"].ToString();
                //    //if (dt.Rows.Count - 1 == i)
                //    //{
                //    //    gvLicence.Rows[i].Cells["Status"].Value = "RUNNING";
                //    //}
                //    //else
                //    //{
                //    //    gvLicence.Rows[i].Cells["Status"].Value = "CLOSED";
                //    //}
                //      dtpStDate = Convert.ToDateTime(gvLicence.Rows[i].Cells["ValidTo"].Value.ToString());
                //      if (dtpMaxDate <= dtpStDate)
                //      {
                //          dtpMaxDate = dtpStDate;
                //          Max = i;
                //      }                              
                //}
                //for (int i = 0; i < gvLicence.Rows.Count;i++ )
                //{
                //    if (Max!=i)
                //    {
                //        gvLicence.Rows[i].Cells["Status"].Value = "CLOSED";
                //    }
                //    else
                //    {
                //        gvLicence.Rows[i].Cells["Status"].Value = "RUNNING";
                //    }
               }
            }
                

            objDB = null;
            dt = null;
        }
        private void FillCerificationDetails()
        {

            objDB = new SQLDB();
            DataTable dt = new DataTable();
            //gvLicence.Rows.Clear();
            if (cmbAssetSlNo.SelectedIndex > 0 & txtCertificateNo.Text != "")
            {

                try
                {
                    dt = CertificateNumberDetails(cmbAssetSlNo.SelectedValue.ToString(), Convert.ToInt32(txtCertificateNo.Text)).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (dt.Rows.Count > 0)
                {
                    dtpMaxDate = Convert.ToDateTime(dt.Rows[0]["ValidTo"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                      
                        cmbAssetSlNo.SelectedValue = dt.Rows[i]["AssetSlNo"].ToString();
                        txtAssetMake.Text = dt.Rows[i]["AssetName"].ToString();
                        txtAssetModel.Text = dt.Rows[i]["AssetModel"].ToString();
                        txtCertificateNo.Text = dt.Rows[i]["CertificateNo"].ToString();
                        txtCertCost.Text = dt.Rows[i]["CertificateCost"].ToString();
                        txtWeight.Text = dt.Rows[i]["Weight"].ToString();
                        txtContactName.Text = dt.Rows[i]["ContactName"].ToString();
                        txtMobileNo.Text = dt.Rows[i]["MobileNo"].ToString();
                        dtpFromDate.Value = Convert.ToDateTime(dt.Rows[i]["ValidFrom"].ToString().ToUpper());
                        dtpToDate.Value = Convert.ToDateTime(dt.Rows[i]["ValidTo"].ToString().ToUpper());
                        flagUpdate = true;

                      

                    }                   
                }
                else
                {
                    txtCertCost.Text = "";
                    txtWeight.Text = "";
                    txtContactName.Text = "";
                    txtMobileNo.Text = "";
                    dtpFromDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                    dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                    flagUpdate = false;

                }


                objDB = null;
                dt = null;
            }
        }

        private void gvLicence_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if ((e.ColumnIndex == gvLicence.Columns["Edit"].Index))
                {
                    if (gvLicence.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "RUNNING")
                    {
                        cmbAssetSlNo.Text = gvLicence.Rows[e.RowIndex].Cells["AssetSlNo"].Value.ToString();
                        txtAssetMake.Text = gvLicence.Rows[e.RowIndex].Cells["AssetName"].Value.ToString();
                        txtAssetModel.Text = gvLicence.Rows[e.RowIndex].Cells["AssetModel"].Value.ToString();
                        txtCertificateNo.Text = gvLicence.Rows[e.RowIndex].Cells["CertificateNo"].Value.ToString();
                        txtCertCost.Text = gvLicence.Rows[e.RowIndex].Cells["CertificateCost"].Value.ToString();
                        txtWeight.Text = gvLicence.Rows[e.RowIndex].Cells["Weight"].Value.ToString();
                        dtpFromDate.Value = Convert.ToDateTime(gvLicence.Rows[e.RowIndex].Cells["ValidFrom"].Value.ToString());
                        dtpToDate.Value = Convert.ToDateTime(gvLicence.Rows[e.RowIndex].Cells["ValidTo"].Value.ToString());
                        txtContactName.Text = gvLicence.Rows[e.RowIndex].Cells["ContactName"].Value.ToString();
                        txtMobileNo.Text = gvLicence.Rows[e.RowIndex].Cells["MobileNo"].Value.ToString();
                        flagUpdate = true;
                    }
                    else
                    {
                        MessageBox.Show("This Data CanNot Be Manuplated", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        private void txtCertCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOwnerName_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
           
                e.KeyChar = char.ToUpper(e.KeyChar);
            
        }

        private void txtCertificateNo_Validated(object sender, EventArgs e)
        {
            FillCerificationDetails();
            
        }

        private void txtCertificateNo_KeyUp(object sender, KeyEventArgs e)
        {
            //FillCerificationDetails();
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) || (blIsCellQty == false))
            {
                e.Handled = true;
                return;
            }           
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && e.KeyChar < 48 || e.KeyChar > 57 && e.KeyChar != 8 && e.KeyChar != 46 || (blIsCellQty == false))
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }
    }
}

