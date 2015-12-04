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
    public partial class ProductLicenceDetails : Form
    {
        SQLDB objSQLdb = null;
        IndentDB objInvDb = null;


        public ProductLicenceDetails()
        {
            InitializeComponent();
        }

        private void ProductLicenceDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillStates();
            cbStatus.SelectedIndex = 0;
            FillProductLicenceDetails();
           
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME ,CM_COMPANY_CODE FROM COMPANY_MAS where active='T'";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

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

       
        private void FillStates()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT sm_state,sm_state_code FROM state_mas";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbState.DataSource = dt;
                    cbState.DisplayMember = "sm_state";
                    cbState.ValueMember = "sm_state_code";
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

        private void FillDistricts()
        {
              objSQLdb = new SQLDB();
             DataTable dt = new DataTable();
             cbDistrict.DataSource = null;
            try
            {
                if (cbState.SelectedIndex > 0)
                {
                    string strCmd = "SELECT DISTINCT(district) "+
                                    ",CDDistrict " +
                                    ",sm_state_code "+
                                    " FROM VILLAGEMASTERUKEY "+
                                    " INNER JOIN state_mas ON sm_state_code= CDState " +
                                    " WHERE sm_state_code='"+ cbState.SelectedValue.ToString() +
                                    "' ORDER BY district ";
                    dt=objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["district"] = "<--Select-->";
                    dr["district"] = "<--Select-->";
                    dt.Rows.InsertAt(dr, 0);

                    cbDistrict.DataSource = dt;
                    cbDistrict.DisplayMember = "district";
                    cbDistrict.ValueMember = "CDDistrict";
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

        private void FillProductLicenceDetails()
        {
            objInvDb = new IndentDB();
            DataTable dt=new DataTable();
            gvProdLicenceDetails.Rows.Clear();
            int intRow = 1;
            string sCompCode = cbCompany.SelectedValue.ToString();
            string sStateCode = cbState.SelectedValue.ToString();
            int sDistrict = 0;
            string sStatus="";
            if (cbDistrict.SelectedIndex > 0)
            {
                sDistrict = Convert.ToInt32( cbDistrict.SelectedValue.ToString());
            }
            else
            {
                sDistrict = 0;
            }
            if (cbStatus.SelectedIndex >0)
                sStatus = cbStatus.SelectedItem.ToString();
            else
                sStatus = "";
           
            try
            {

                dt = objInvDb.Get_ProductLicenceDetails(sCompCode, sStateCode, sDistrict, sStatus).Tables[0];
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellProdLicenceNo = new DataGridViewTextBoxCell();
                        cellProdLicenceNo.Value = dt.Rows[i]["plm_licence_number"];
                        tempRow.Cells.Add(cellProdLicenceNo);

                        DataGridViewCell cellComapanyName = new DataGridViewTextBoxCell();
                        cellComapanyName.Value = dt.Rows[i]["CM_COMPANY_NAME"];
                        tempRow.Cells.Add(cellComapanyName);

                        DataGridViewCell cellState = new DataGridViewTextBoxCell();
                        cellState.Value = dt.Rows[i]["sm_state"];
                        tempRow.Cells.Add(cellState);

                        DataGridViewCell cellDistrict = new DataGridViewTextBoxCell();
                        cellDistrict.Value = sDistrict + "";
                        tempRow.Cells.Add(cellDistrict);

                        DataGridViewCell cellProdlicenceIssDate = new DataGridViewTextBoxCell();
                        cellProdlicenceIssDate.Value = Convert.ToDateTime(dt.Rows[i]["plm_licence_issu_date"]).ToShortDateString();
                        tempRow.Cells.Add(cellProdlicenceIssDate);

                        DataGridViewCell cellProdLicenceExpDate = new DataGridViewTextBoxCell();
                        cellProdLicenceExpDate.Value = Convert.ToDateTime(dt.Rows[i]["plm_licence_valid_date"]).ToShortDateString();
                        tempRow.Cells.Add(cellProdLicenceExpDate);

                        DataGridViewCell cellProdLicenceStatus = new DataGridViewTextBoxCell();
                        cellProdLicenceStatus.Value = dt.Rows[i]["Status"]; 
                        tempRow.Cells.Add(cellProdLicenceStatus);


                        gvProdLicenceDetails.Rows.Add(tempRow);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objInvDb = null;
                dt = null;

            }

        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillProductLicenceDetails();
            }
        }

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDistricts();
            if (cbState.SelectedIndex > 0)
            {
                FillProductLicenceDetails();
            }
            
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvProdLicenceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string strLicenceNo = gvProdLicenceDetails.Rows[e.RowIndex].Cells["ProdLicenceNo"].Value.ToString();
                if (e.ColumnIndex == gvProdLicenceDetails.Columns["Edit"].Index)
                {

                    ProductLicence ProdLicence = new ProductLicence(strLicenceNo);
                    ProdLicence.objProductLicenceDetails = this;
                    ProdLicence.ShowDialog();

                }
            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductLicenceDetails();
        }

        private void cbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductLicenceDetails();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbState.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            gvProdLicenceDetails.Rows.Clear();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string sComp = "", sState = "", sDist = "", sStatus = "";
            if (cbCompany.SelectedIndex > 0)
                sComp = cbCompany.SelectedValue.ToString();
            if(cbState.SelectedIndex>0)
                sState = cbState.SelectedValue.ToString();
            if (cbDistrict.SelectedIndex > 0)
                sDist = cbDistrict.SelectedValue.ToString();
            if (cbStatus.SelectedIndex > 0)
                sStatus = cbStatus.Text.ToString();
            ReportViewer childReportViewer = new ReportViewer(sComp, sState, sDist, sStatus, "");
            CommonData.ViewReport = "SSCRM_REP_PRODUCT_LICENCES_DETAILS";
            childReportViewer.Show();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {           
            gvProdLicenceDetails.ClearSelection();
            int rowIndex = 0;
            foreach (DataGridViewRow row in gvProdLicenceDetails.Rows)
            {
                if (row.Cells[1].Value.ToString().Contains(txtSearch.Text) == true)
                {
                    rowIndex = row.Index;
                    gvProdLicenceDetails.CurrentCell = gvProdLicenceDetails.Rows[rowIndex].Cells[1];
                    break;
                }
            }
        }  


    }
}
