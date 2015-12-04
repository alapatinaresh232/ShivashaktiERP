using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSTrans;
using SSCRMDB;
using SSAdmin;

namespace SSCRM
{
    public partial class frmDCandDCRDetails : Form
    {
        SQLDB objSQLdb = null;
        public TripSheet objTripSheet;

        public frmDCandDCRDetails()
        {
            InitializeComponent();
        }

        private void frmDCandDCRDetails_Load(object sender, EventArgs e)
        {
            cbCompany.Enabled = false;
            cbLocation.Enabled = false;

            FillCompanyData();
            FillBranchData();
            dtpDoctMnth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));

            FillDCDetails();
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            try
            {
                 strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
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

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' ORDER BY BRANCH_NAME ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "BRANCH_CODE";

                }

                cbLocation.SelectedValue = CommonData.BranchCode;
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
                cbLocation.SelectedIndex = 0;
            }
        }

        private DataSet GetDCTranNosForTripSheet(string CompCode, string BranchCode, string DocMonth, string TrnDate, string sType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sTrnDate", DbType.String, TrnDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sType", DbType.String, sType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_DCTrnNos_For_TripSheet", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        private void FillDCDetails()
        {
            tvDcDetails.Nodes.Clear();
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();

            try
            {
                ds = GetDCTranNosForTripSheet(cbCompany.SelectedValue.ToString(), cbLocation.SelectedValue.ToString(), Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper(), "", "PARENT");
                TreeNode tNode;


                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvDcDetails.Nodes.Add(Convert.ToDateTime(ds.Tables[0].Rows[i]["TrnDate"]).ToString("dd/MMM/yyyy"), Convert.ToDateTime(ds.Tables[0].Rows[i]["TrnDate"]).ToString("dd/MMM/yyyy"));
                        DataSet dschild = new DataSet();
                        dschild = GetDCTranNosForTripSheet(cbCompany.SelectedValue.ToString(), cbLocation.SelectedValue.ToString(), Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(ds.Tables[0].Rows[i]["TrnDate"]).ToString("dd/MMM/yyyy"), "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvDcDetails.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["ValueMember"].ToString(), dschild.Tables[0].Rows[j]["DisMember"].ToString());

                            }
                        }
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
                ds = null;
            }
        }
       


        private bool CheckData()
        {
            bool bFlag = true;

            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }
            if (cbLocation.SelectedIndex == 0 || cbLocation.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }
            if (dtpDoctMnth.Value > DateTime.Today)
            {
                bFlag = false;
                MessageBox.Show("Please Select Valid Document Month", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }
            if (tvDcDetails.Nodes.Count > 0)
            {
                bFlag = false;
                for (int i = 0; i < tvDcDetails.Nodes.Count; i++)
                {

                    for (int j = 0; j < tvDcDetails.Nodes[i].Nodes.Count; j++)
                    {
                        if (tvDcDetails.Nodes[i].Nodes[j].Checked == true)
                        {
                            bFlag = true;
                        }
                    }

                }
                if (bFlag == false)
                {
                    MessageBox.Show("Select Atleast One Dc Transactions", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return bFlag;
                }
            }
            else
            {
                bFlag = false;
                MessageBox.Show("Please Select Valid Company and Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;

            }



            return bFlag;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvDcDetl = null;

            if (CheckData() == true)
            {
                dgvDcDetl = ((TripSheet)objTripSheet).gvDCorDCSTDetl;
                AddDCDetailsToGrid(dgvDcDetl);

                ((TripSheet)objTripSheet).CalculateTotalUnits();
                objTripSheet.CalculationForQTY();
                
              
            }
        }

            

        #region "GRIDVIEW DETAILS"

        private void AddDCDetailsToGrid(DataGridView dgvDcDetl)
        {
            string[] strArrDcDetl = null;
            bool isDCExist = false;
            int intRow = 0;
            intRow = dgvDcDetl.Rows.Count + 1;

            if (CheckData() == true)
            {
                try
                {

                    for (int i = 0; i < tvDcDetails.Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvDcDetails.Nodes[i].Nodes.Count; j++)
                        {
                            if (tvDcDetails.Nodes[i].Nodes[j].Checked == true)
                            {
                                string stDcDetail = tvDcDetails.Nodes[i].Nodes[j].Name.ToString();
                                strArrDcDetl = stDcDetail.Split('~');
                                for (int nRow = 0; nRow < dgvDcDetl.Rows.Count; nRow++)
                                {
                                    if (dgvDcDetl.Rows[nRow].Cells["DCorDCSTNo"].Value.ToString().Trim() == strArrDcDetl[0].ToString().Trim())
                                    {
                                        isDCExist = true;
                                        break;
                                    }
                                }
                                if (isDCExist == false)
                                {

                                    DataGridViewRow tempRow = new DataGridViewRow();

                                    DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                                    cellSlNo.Value = intRow;
                                    tempRow.Cells.Add(cellSlNo);

                                    DataGridViewCell cellDCNo = new DataGridViewTextBoxCell();
                                    cellDCNo.Value = strArrDcDetl[0].ToString();
                                    tempRow.Cells.Add(cellDCNo);

                                    DataGridViewCell cellRefNo = new DataGridViewTextBoxCell();
                                    cellRefNo.Value = strArrDcDetl[1].ToString();
                                    tempRow.Cells.Add(cellRefNo);

                                    DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                                    cellQty.Value = strArrDcDetl[2].ToString();
                                    tempRow.Cells.Add(cellQty);

                                    intRow = intRow + 1;
                                    dgvDcDetl.Rows.Add(tempRow);
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.Close();
            }
           
        }
               
        #endregion


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpDoctMnth_ValueChanged(object sender, EventArgs e)
        {
            FillDCDetails();
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocation.SelectedIndex > 0)
            {
                FillDCDetails();
            }
        }

        private void tvDcDetails_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);

            tvDcDetails.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvDcDetails.EndUpdate();
        }

       

    }
}
