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
    public partial class AssetsPrintBySystemId : Form
    {
        SQLDB objSQLdb = null;

        public AssetsPrintBySystemId()
        {
            InitializeComponent();
        }

        private void AssetsPrintBySystemId_Load(object sender, EventArgs e)
        {
            FillCompanyData();
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
          
            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE "+
                                " FROM USER_BRANCH "+
                                " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE "+
                                " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE "+
                                " WHERE UB_USER_ID ='"+ CommonData.LogUserId +
                                "' ORDER BY CM_COMPANY_NAME";
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
            cbBranches.DataSource = null;
            //cbAssetType.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE "+
                                        " FROM USER_BRANCH "+
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE "+
                                        " WHERE COMPANY_CODE ='"+ cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='"+ CommonData.LogUserId +
                                        "' ORDER BY BRANCH_NAME ";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "BRANCH_NAME";
                    cbBranches.ValueMember = "BRANCH_CODE";
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
        private void FillAssetType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbAssetType.DataSource = null;
            try
            {
               
                if (cbBranches.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT(FAH_ASSET_TYPE) FROM FIXED_ASSETS_HEAD " +
                                        " INNER JOIN FIXED_ASSETS_MOVEMENT_REG ON FAMR_ASSET_SL_NO=FAH_ASSET_SL_NO " +
                                        " WHERE FAMR_TO_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() + "'";
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
        private void FillAssetDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvAssetDetails.Rows.Clear();
            try
            {
                if (cbAssetType.SelectedIndex > 0)
                {
                    string strCmd = "SELECT FAMR_ASSET_SL_NO AssetSlNo "+
                                    ",CAST(FAMR_TO_ECODE as VARCHAR)+'-'+MEMBER_NAME EmpName "+
                                    ",FAH_CONT_NO MobelNo " +
                                    ",FAH_MODEL AssetModel "+
                                    " FROM FIXED_ASSETS_MOVEMENT_REG a " +
                                    " INNER JOIN FIXED_ASSETS_HEAD ON FAH_ASSET_SL_NO=FAMR_ASSET_SL_NO " +
                                    " LEFT JOIN EORA_MASTER ON ECODE=FAMR_TO_ECODE " +
                                    " WHERE a.FAMR_TRN_NO = (SELECT top 1 b.FAMR_TRN_NO FROM FIXED_ASSETS_MOVEMENT_REG b " +
                                    " WHERE b.FAMR_ASSET_SL_NO = a.FAMR_ASSET_SL_NO  ORDER BY b.FAMR_TRN_NO desc) " +
                                    " AND FAMR_TO_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() + 
                                    "'AND FAH_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() + "'";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvAssetDetails.Rows.Add();

                        gvAssetDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvAssetDetails.Rows[i].Cells["AssetSlNo"].Value = dt.Rows[i]["AssetSlNo"].ToString();
                        gvAssetDetails.Rows[i].Cells["AssetModel"].Value = dt.Rows[i]["AssetModel"].ToString();
                        gvAssetDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["EmpName"].ToString();
                        gvAssetDetails.Rows[i].Cells["MobileNo"].Value = dt.Rows[i]["MobelNo"].ToString();
                       
                    }
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
            }
            else
            {
                cbBranches.DataSource = null;
                cbAssetType.SelectedIndex = -1;
                gvAssetDetails.Rows.Clear();
            }

        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                FillAssetType();
            }
            else
            {
                cbAssetType.SelectedIndex = -1;
                gvAssetDetails.Rows.Clear();
            }
           
        }

        private void cbAssetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssetType.SelectedIndex > 0)
            {
                FillAssetDetails();
            }
            else
            {
                gvAssetDetails.Rows.Clear();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvAssetDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvAssetDetails.Columns["Print"].Index)
                {
                    CommonData.ViewReport = "REP_FIXED_ASSETS_CONFIG_MAINTENANCE_DETL";
                    ReportViewer childReportViewer = new ReportViewer("","",gvAssetDetails.Rows[e.RowIndex].Cells["AssetSlNo"].Value.ToString(),"");
                    childReportViewer.Show();
                }
            }
        }




    }
}
