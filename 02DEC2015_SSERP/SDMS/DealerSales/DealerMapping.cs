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
using System.Data.SqlClient;
using System.Collections;

namespace SDMS
{
    public partial class DealerMapping : Form
    {
        SQLDB objSQLDB = null;
        DealerInfo objDLDB = null;
        string strDCode, strECode = "";
        
        public DealerMapping()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDealerSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDealerSearch.Text.ToString().Trim().Length > 0)
                DealerSearch();
            else
            {
                cbDealer.SelectedIndex = -1;
                strDCode = "";
            }
        }
        private void DealerSearch()
        {
            string sCompCode = CommonData.CompanyCode;
            string sFirmName = "";
            objSQLDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbDealer.DataSource = null;
                cbDealer.Items.Clear();

                param[0] = objSQLDB.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLDB.CreateParameter("@xDealerName", DbType.String, txtDealerSearch.Text, ParameterDirection.Input);
                param[2] = objSQLDB.CreateParameter("@xFirmName", DbType.String, sFirmName, ParameterDirection.Input);
                ds = objSQLDB.ExecuteDataSet("DL_GetDealersListSearch", CommandType.StoredProcedure, param);

                DataTable dtDealer = ds.Tables[0];

                if (dtDealer.Rows.Count > 0)
                {
                    cbDealer.DataSource = dtDealer;
                    cbDealer.DisplayMember = "FirmName";
                    cbDealer.ValueMember = "DealerCode";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbDealer.SelectedIndex > -1)
                {
                    cbDealer.SelectedIndex = 0;
                    strDCode = ((System.Data.DataRowView)(cbDealer.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objSQLDB = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void DealerMapping_Load(object sender, EventArgs e)
        {
            FillDesignations();
        }

        private void FillDesignations()
        {

            objSQLDB = new SQLDB();
            objDLDB = new DealerInfo();
                DataTable dt = new DataTable();
                cbDesig.DataSource = null;
                try
                {
                   

                    dt = objDLDB.DesigInShanm();
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["desig_name"] = "--Select--";
                        dr["desig_name"] = "--Select--";           
                        dt.Rows.InsertAt(dr, 0);

                        cbDesig.DataSource = dt;
                        cbDesig.DisplayMember = "desig_name";
                        cbDesig.ValueMember = "DESG_ID";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLDB = null;
                    dt = null;
                }
            
            
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            GetSONames();
        }

        private void cbDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSONames();
        }
        private void GetSONames()
        {
            objDLDB = new DealerInfo();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = null;
            cbEcode.DataSource = null;
            try
            {
                string strSRName = txtEcodeSearch.Text.ToString();
                //cbGCEcode.Items.Clear();
                if (cbDesig.SelectedIndex > 0)
                {

                    dt = objDLDB.SOEcodeSearchByDesig_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, cbDesig.SelectedValue.ToString(), strSRName);


                }

                if (dt.Rows.Count > 0)
                {
                    cbEcode.DataSource = dt;
                    cbEcode.DisplayMember = "EmpName";
                    cbEcode.ValueMember = "ECODE";
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objDLDB = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                AddDataToGrid();
            }
        }

        private void AddDataToGrid()
        {
            gvEmpDetails.Rows.Add();
            gvEmpDetails.Rows[gvEmpDetails.Rows.Count - 1].Cells["SNo"].Value = gvEmpDetails.Rows.Count;
            gvEmpDetails.Rows[gvEmpDetails.Rows.Count - 1].Cells["Ecode"].Value = cbEcode.SelectedValue.ToString();
            gvEmpDetails.Rows[gvEmpDetails.Rows.Count - 1].Cells["DlName"].Value = ((DataRowView)cbEcode.SelectedItem)[1].ToString() ;
            gvEmpDetails.Rows[gvEmpDetails.Rows.Count - 1].Cells["desigId"].Value = ((DataRowView)cbDesig.SelectedItem)[1].ToString();
            gvEmpDetails.Rows[gvEmpDetails.Rows.Count - 1].Cells["Desig"].Value = ((DataRowView)cbDesig.SelectedItem)[0].ToString();
        }

        private bool CheckData()
        {
            bool flag = true;
            if(cbDealer.SelectedIndex<-1)
            {
                flag = false;
                MessageBox.Show("Please Enter Dealer", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDealerSearch.Focus();
            }
            if (cbDesig.SelectedIndex < 0)
            {
                flag = false;
                MessageBox.Show("Please Select Desig", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDealerSearch.Focus();
            }
            if (cbEcode.SelectedIndex < -1)
            {
                flag = false;
                MessageBox.Show("Please Enter ECode", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDealerSearch.Focus();
            }
            for (int iVar = 0; iVar < gvEmpDetails.Rows.Count;iVar++ )
            {
                if (gvEmpDetails.Rows[iVar].Cells["Ecode"].Value.ToString() == cbEcode.SelectedValue.ToString() || gvEmpDetails.Rows[iVar].Cells["desigId"].Value.ToString() == cbDesig.SelectedValue.ToString())
                {
                    flag = false;

                }
            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(gvEmpDetails.Rows.Count>0)
            {
                if (SaveEmpMapping() > 0)
                {

                    MessageBox.Show("Data Saved Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnCancel_Click(null, null);

                }
                else
                {
                    MessageBox.Show("Data Not Saved", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private int SaveEmpMapping()
        {
            int iRes = 0;
            try
            {
                string strSQL = " DELETE DLSourceToDest_Map WHERE stdm_dealer_code= "+cbDealer.SelectedValue.ToString()
                                + " and stdm_company_code='" + CommonData.CompanyCode+
                                "' and stdm_branch_code='"+CommonData.BranchCode+
                                "' and stdm_state_code='" + CommonData.StateCode+"'";

                objSQLDB = new SQLDB();
                objSQLDB.ExecuteSaveData(strSQL);
                string strSQL1 = "";
                for (int iVar = 0; iVar < gvEmpDetails.Rows.Count;iVar++ )
                {
                    strSQL += " insert into DLSourceToDest_Map(stdm_company_code,stdm_branch_code,stdm_state_code,stdm_dealer_code,stdm_emp_code,stdm_desig_id,stdm_valid_from"+
                        ",stdm_created_by,stdm_created_date) values('"+CommonData.CompanyCode+"','"+CommonData.BranchCode+"','"+CommonData.StateCode+
                        "',"+Convert.ToInt32( cbDealer.SelectedValue.ToString())+","+Convert.ToInt32(cbEcode.SelectedValue.ToString())+",'"+Convert.ToInt32(cbDesig.SelectedValue.ToString())+"','"+dtpValidFromDate.Value.ToString("dd/MMM/yyyy")+
                        "','"+CommonData.LogUserId+"',getdate())";
                    strSQL1 += " insert into DLSourceToDest_Map_Hist(stdmh_company_code,stdmh_branch_code,stdmh_state_code,stdmh_dealer_code,stdmh_emp_code,stdmh_desig_id,stdmh_valid_from" +
                        ",stdmh_created_by,stdmh_created_date) values('" + CommonData.CompanyCode + "','" + CommonData.BranchCode + "','" + CommonData.StateCode +
                        "'," + Convert.ToInt32(cbDealer.SelectedValue.ToString()) + "," + Convert.ToInt32(cbEcode.SelectedValue.ToString()) + ",'" + Convert.ToInt32(cbDesig.SelectedValue.ToString()) + "','" + dtpValidFromDate.Value.ToString("dd/MMM/yyyy") +
                        "','" + CommonData.LogUserId + "',getdate())";
                }

                objSQLDB = new SQLDB();
                objSQLDB.ExecuteSaveData(strSQL + strSQL1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private void gvEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvEmpDetails.Rows[e.RowIndex].Cells["Del"].ColumnIndex)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                tempRow = gvEmpDetails.Rows[e.RowIndex];
                gvEmpDetails.Rows.Remove(tempRow);
                for (int i = 0; i < gvEmpDetails.Rows.Count; i++)
                {
                    gvEmpDetails.Rows[i].Cells["SNo"].Value = i + 1;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtDealerSearch.Text = "";
            txtEcodeSearch.Text = "";
            cbDesig.SelectedIndex = 0;
            gvEmpDetails.Rows.Clear();
        }
    }
}
