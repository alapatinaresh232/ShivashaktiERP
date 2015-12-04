using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using SSCRMDB;

namespace SSCRM
{
    public partial class StockPersonDetails : Form
    {
        SQLDB objSqlDb = null;

        public Physicalstkcount objPhystkfrm = null;
        string strBranCode = "";

        public StockPersonDetails()
        {
            InitializeComponent();
        }
        public StockPersonDetails(string sBranCode)
        {
            InitializeComponent();
            strBranCode = sBranCode;
        }

       

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                FillEmployeeDetails();
            }
            else
            {
                txtEcodeSearch.Text = "";
                cbEname.Text = "";
                txtDesig.Text = "";
                txtDept.Text = "";
                dtpDoj.Text = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtDept.Text = "";
            txtDesig.Text = "";
            dtpDoj.Text = "";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        
        private void AddEmpDetailsToGrid(DataGridView dgvStkEmpdetails)
        {
            try
            {
                int intRow = 0;
                intRow = dgvStkEmpdetails.Rows.Count + 1;
                bool isItemExisted = false;

                if (objPhystkfrm.gvStockDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < objPhystkfrm.gvStockDetails.Rows.Count; i++)
                    {
                        if (cbEname.Text.ToString().Split('-')[0].Equals(objPhystkfrm.gvStockDetails.Rows[i].Cells["Ecode1"].Value.ToString()))
                        {
                            isItemExisted = true;
                        }
                    }
                }
                if (isItemExisted == false)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();


                    DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                    cellSlNo.Value = intRow;
                    tempRow.Cells.Add(cellSlNo);

                    DataGridViewCell CellEcode = new DataGridViewTextBoxCell();
                    CellEcode.Value = cbEname.Text.ToString().Split('-')[0];
                    tempRow.Cells.Add(CellEcode);

                    DataGridViewCell CellName = new DataGridViewTextBoxCell();
                    CellName.Value = cbEname.Text.ToString().Split('-')[1];
                    tempRow
                        
                        
                        .Cells.Add(CellName);                   

                    DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                    cellEmpDesig.Value = txtDesig.Text.ToString();
                    tempRow.Cells.Add(cellEmpDesig);


                    DataGridViewCell Celldoj = new DataGridViewTextBoxCell();
                    Celldoj.Value = dtpDoj.Value.ToString("dd/MMM/yyyy");
                    tempRow.Cells.Add(Celldoj);

                    intRow = intRow + 1;
                    dgvStkEmpdetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            DataGridView dgvStkEmpdetails = null;
            if (txtEcodeSearch.Text != "")
            {

                dgvStkEmpdetails = ((Physicalstkcount)objPhystkfrm).gvStockDetails;
                AddEmpDetailsToGrid(dgvStkEmpdetails);
                this.Close();
           
            }
            else
            {
                MessageBox.Show("Details Not saved");
                txtEcodeSearch.Focus();
            }
        }

        public DataSet GetEmployeeEcodes(string BranCodes,string EmpName)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSqlDb.CreateParameter("@sBranCode", DbType.String, BranCodes, ParameterDirection.Input);
                param[1] = objSqlDb.CreateParameter("@sECodeName", DbType.String, EmpName, ParameterDirection.Input);

                ds = objSqlDb.ExecuteDataSet("Get_StockEmpDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSqlDb = null;
            }
            return ds;
        }

        private void FillEmployeeDetails()
        {
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            cbEname.DataBindings.Clear();

            try
            {

                dt = GetEmployeeEcodes(strBranCode,txtEcodeSearch.Text.ToString()).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    cbEname.DataSource = dt;
                    cbEname.DisplayMember = "EmpName";
                    cbEname.ValueMember = "EmpDetl";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlDb = null;
                dt = null;
            }

        }

        private void StockPersonDetails_Load(object sender, EventArgs e)
        {
            FillEmployeeDetails();
        }

        private void cbEname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEname.SelectedIndex > -1)
            {
                txtDesig.Text = ((System.Data.DataRowView)(cbEname.SelectedItem)).Row.ItemArray[1].ToString().Split('@')[2];
                dtpDoj.Text = ((System.Data.DataRowView)(cbEname.SelectedItem)).Row.ItemArray[1].ToString().Split('@')[3];
            }
            txtDept.Text = "FINANCE & ACCOUNTS";
        }


        
    }
}
