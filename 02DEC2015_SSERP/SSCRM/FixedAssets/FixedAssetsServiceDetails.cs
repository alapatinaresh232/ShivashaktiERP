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

namespace SSCRM
{
    public partial class FixedAssetsServiceDetails : Form
    {
        SQLDB objSQLdb = null;
        FixedAssetsDB objAssetdb = null;
     
        Int32 TrnNo = 0;
        private bool flagUpdate = false;

        public FixedAssetsServiceDetails()
        {
            InitializeComponent();
        }

        private void FixedAssetsServiceDetails_Load(object sender, EventArgs e)
        {
           
                FillCompanyData();
                FillBranchData();

                //FillAssetType();
              
        }


        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME "+
                                " FROM COMPANY_MAS WHERE ACTIVE='T' "+
                                " ORDER BY CM_COMPANY_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

                cbCompany.SelectedValue = CommonData.CompanyCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }

        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            cbAssetType.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT BRANCH_NAME ,BRANCH_CODE  FROM BRANCH_MAS " +
                                        " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' Order by BRANCH_NAME ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BRANCH_CODE";
                }

                cbBranch.SelectedValue = CommonData.BranchCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }

        private void FillAssetType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbAssetId.DataSource = null;
            cbServiceType.DataSource = null;
            cbAssetType.DataSource = null;
            try
            {
                //string strCommand = "SELECT  DISTINCT(FAM_ASSET_TYPE) FROM FIXED_ASSETS_MAS "+
                //                    " ORDER BY FAM_ASSET_TYPE ASC";
                if (cbBranch.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT(FAH_ASSET_TYPE) FROM FIXED_ASSETS_HEAD " +
                                        " INNER JOIN FIXED_ASSETS_MOVEMENT_REG ON FAMR_ASSET_SL_NO=FAH_ASSET_SL_NO " +
                                        " WHERE FAMR_TO_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "'";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbAssetType.DataSource = dt;
                    cbAssetType.DisplayMember = "FAH_ASSET_TYPE";
                    cbAssetType.ValueMember = "FAH_ASSET_TYPE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;

            }

        }


        private void FillServiceTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbServiceType.DataSource = null;
            try
            {
                if (cbAssetType.SelectedIndex > 0)
                {
                    string strCommand = "SELECT FASM_ID,FASM_SERVICE_CATEGORY FROM FIXED_ASSETS_SERVICE_MAS " +
                                        " WHERE FASM_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() + "' ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["FASM_SERVICE_CATEGORY"] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbServiceType.DataSource = dt;
                    cbServiceType.DisplayMember = "FASM_SERVICE_CATEGORY";
                    cbServiceType.ValueMember = "FASM_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;

            }

        }

        private void FillAssetSlNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbAssetId.DataSource = null;
            try
            {
                if (cbBranch.SelectedIndex > 0 && cbAssetType.SelectedIndex > 0)
                {
                    string strCmd = "SELECT FAMR_ASSET_SL_NO " +
                                    " FROM FIXED_ASSETS_MOVEMENT_REG a " +
                                    " INNER JOIN FIXED_ASSETS_HEAD ON FAH_ASSET_SL_NO=FAMR_ASSET_SL_NO " +
                                    " WHERE a.FAMR_TRN_NO = (SELECT top 1 b.FAMR_TRN_NO FROM FIXED_ASSETS_MOVEMENT_REG b " +
                                    " WHERE b.FAMR_ASSET_SL_NO = a.FAMR_ASSET_SL_NO  ORDER BY b.FAMR_TRN_NO desc) " +
                                    " AND a.FAMR_TO_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() +
                                    "' AND FAH_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() + "'";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["FAMR_ASSET_SL_NO"] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbAssetId.DataSource = dt;
                    cbAssetId.DisplayMember = "FAMR_ASSET_SL_NO";
                    cbAssetId.ValueMember = "FAMR_ASSET_SL_NO";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
                cbBranch.SelectedIndex = 0;
            }
        }
        private void cbAssetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssetType.SelectedIndex > 0)
            {
                FillServiceTypes();
                FillAssetSlNo();
            }
        }
        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranch.SelectedIndex > 0)
            {
                FillAssetType();
                //FillAssetSlNo();
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
            }
            else if (cbBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranch.Focus();
            }
            else if (cbAssetType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbAssetType.Focus();
            }
            else if (cbAssetId.SelectedIndex == 0 || cbAssetId.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select  Asset Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbAssetType.Focus();
            }
            else if (cbServiceType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Service Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbServiceType.Focus();
            }
            //else if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //}

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            DataTable dt = new DataTable();
            string strCommand = "";
            if (CheckData() == true)
            {
                try
                {
                    if (txtServiceCost.Text.Length == 0)
                    {
                        txtServiceCost.Text = "0";
                    }

                    if (flagUpdate == true)
                    {
                        strCommand = "UPDATE FIXED_ASSETS_SERVICE_REG SET FASR_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "',FASR_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() +
                                        "',FASR_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() +
                                        "',FASR_ASSET_SL_NO='" + cbAssetId.SelectedValue.ToString() +
                                        "',FASR_SERVICE_TYPE='" + cbServiceType.Text.ToString() +
                                        "',FASR_SERVICE_DATE='" + Convert.ToDateTime(dtpServiceDate.Value).ToString("dd/MMM/yyyy") +
                                        "',FASR_SERVICE_COST=" + Convert.ToDouble(txtServiceCost.Text).ToString("0.00") +
                                        ",FASR_REMARKS='" + txtRemarks.Text.ToString() +
                                        "',FASR_MODIFIED_BY='" + CommonData.LogUserId +
                                        "',FASR_MODIFIED_DATE=getdate()" +
                                        " WHERE FASR_TRN_NO= " + TrnNo + "";
                        flagUpdate = false;
                    }
                    else
                    {

                        strCommand = "SELECT ISNULL(MAX(FASR_TRN_NO), 0)+1 FROM FIXED_ASSETS_SERVICE_REG";
                        TrnNo = Convert.ToInt32(objSQLdb.ExecuteDataSet(strCommand).Tables[0].Rows[0][0].ToString());

                        strCommand = "";


                        strCommand = "INSERT INTO FIXED_ASSETS_SERVICE_REG(FASR_COMP_CODE " +
                                                                        ", FASR_BRANCH_CODE " +
                                                                        ", FASR_ASSET_SL_NO " +
                                                                        ", FASR_ASSET_TYPE " +
                                                                        ", FASR_TRN_NO " +
                                                                        ", FASR_SERVICE_TYPE " +
                                                                        ", FASR_SERVICE_DATE " +
                                                                        ", FASR_SERVICE_COST " +
                                                                        ", FASR_REMARKS " +
                                                                        ", FASR_CREATED_BY " +
                                                                        ", FASR_CREATED_DATE " +
                                                                        ")VALUES " +
                                                                        "('" + cbCompany.SelectedValue.ToString() +
                                                                        "','" + cbBranch.SelectedValue.ToString() +
                                                                        "','" + cbAssetId.Text.ToString() +
                                                                        "','" + cbAssetType.SelectedValue.ToString() +
                                                                        "'," + TrnNo +
                                                                        ",'" + cbServiceType.Text.ToString() +
                                                                        "','" + Convert.ToDateTime(dtpServiceDate.Value).ToString("dd/MMM/yyyy") +
                                                                        "'," + Convert.ToDouble(txtServiceCost.Text).ToString("0.00") +
                                                                        ",'" + txtRemarks.Text.ToString() +
                                                                        "','" + CommonData.LogUserId +
                                                                        "',getdate())";
                    }

                    if (strCommand.Length > 10)
                    {
                        iRes = objSQLdb.ExecuteSaveData(strCommand);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                    flagUpdate = false;
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            cbCompany.SelectedIndex = 0;
            cbBranch.SelectedIndex = 0;
            cbAssetType.SelectedIndex = 0;
            cbAssetId.SelectedIndex = 0;
            dtpServiceDate.Value = DateTime.Today;
            cbServiceType.SelectedIndex = 0;
            txtRemarks.Text = "";
            txtServiceCost.Text = "";
            gvAssetServiceDetails.Rows.Clear();


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GetPreviousServiceDetails()
        {
            objAssetdb = new FixedAssetsDB();
            DataTable dt = new DataTable();
            gvAssetServiceDetails.Rows.Clear();

            //string CompCode = cbCompany.SelectedValue.ToString();
            //string BranCode = cbBranch.SelectedValue.ToString();
            //string AssetType = cbAssetType.SelectedValue.ToString();
            if (cbAssetId.SelectedIndex > 0)
            {

                try
                {

                    dt = objAssetdb.GetFixedAssetsServiceDetails(cbAssetId.SelectedValue.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvAssetServiceDetails.Rows.Add();

                            gvAssetServiceDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvAssetServiceDetails.Rows[i].Cells["TransactionNo"].Value = dt.Rows[i]["ServiceTrnNo"].ToString();
                            gvAssetServiceDetails.Rows[i].Cells["AssetId"].Value = dt.Rows[i]["AssetSLNO"].ToString();
                            gvAssetServiceDetails.Rows[i].Cells["AssetType"].Value = dt.Rows[i]["AssetType"].ToString();
                            gvAssetServiceDetails.Rows[i].Cells["ServiceType"].Value = dt.Rows[i]["ServiceType"].ToString();
                            gvAssetServiceDetails.Rows[i].Cells["ServiceDate"].Value = Convert.ToDateTime(dt.Rows[i]["ServiceDate"].ToString()).ToString("dd/MMM/yyyy");
                            gvAssetServiceDetails.Rows[i].Cells["ServiceCost"].Value = dt.Rows[i]["ServiceCost"].ToString();
                            gvAssetServiceDetails.Rows[i].Cells["ServiceRemarks"].Value = dt.Rows[i]["ServiceRemarks"].ToString();

                        }


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objAssetdb = null;
                    dt = null;
                }
            }
        }

        private void cbAssetId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssetId.SelectedIndex > 0)
            {
                GetPreviousServiceDetails();

            }
            else
            {
                gvAssetServiceDetails.Rows.Clear();
            }
        }

        private void gvAssetServiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvAssetServiceDetails.Columns["Edit"].Index)
            {
                flagUpdate = true;

                //cbServiceType.Enabled = false;
                TrnNo = Convert.ToInt32(gvAssetServiceDetails.Rows[e.RowIndex].Cells["TransactionNo"].Value.ToString());
                cbServiceType.Text = gvAssetServiceDetails.Rows[e.RowIndex].Cells["ServiceType"].Value.ToString();
                txtServiceCost.Text = gvAssetServiceDetails.Rows[e.RowIndex].Cells["ServiceCost"].Value.ToString();
                txtRemarks.Text = gvAssetServiceDetails.Rows[e.RowIndex].Cells["ServiceRemarks"].Value.ToString();
                dtpServiceDate.Value = Convert.ToDateTime(gvAssetServiceDetails.Rows[e.RowIndex].Cells["ServiceDate"].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            int iRec = 0;
            if (cbAssetId.SelectedIndex > 0 && cbServiceType.SelectedIndex > 0)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        string strCmd = "DELETE FROM FIXED_ASSETS_SERVICE_REG " +
                                       " WHERE FASR_ASSET_SL_NO='" + cbAssetId.SelectedValue.ToString() +
                                       "' AND FASR_SERVICE_TYPE='" + cbServiceType.Text.ToString() + "'";

                        iRec = objSQLdb.ExecuteSaveData(strCmd);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (iRec > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select Asset id and Service Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
              

    }
}
