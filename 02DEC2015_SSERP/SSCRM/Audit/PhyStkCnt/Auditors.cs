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
    public partial class Auditors : Form
    {
        DataGridViewRow dgvr;
        SQLDB objSqlDb = null;
        
        public Physicalstkcount objPhystkfrm = null;

        public Auditors()
        {
            InitializeComponent();
        }

        public DataSet GetAuditEmployeeEcodes(string EmpName)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSqlDb.CreateParameter("@sECodeName", DbType.String, EmpName, ParameterDirection.Input);

                ds = objSqlDb.ExecuteDataSet("Get_AuditEmployeeEcodes", CommandType.StoredProcedure, param);
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


        private void FillAuditEmployeeDetails()
        {
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            cbEname.DataBindings.Clear();

            try
            {

                dt = GetAuditEmployeeEcodes(txtEcodeSearch.Text.ToString()).Tables[0];

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

        private void Auditors_Load(object sender, EventArgs e)
        {
            FillAuditEmployeeDetails();
           
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                FillAuditEmployeeDetails();
            }
            else
            {
                txtEcodeSearch.Text = "";
                cbEname.Text = "";
                txtauditDesig.Text = "";
                txtDept.Text = "";
                dtpDoj.Text = "";
            }
        }

        private void cbEname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEname.SelectedIndex > -1)
            {
                txtauditDesig.Text = ((System.Data.DataRowView)(cbEname.SelectedItem)).Row.ItemArray[1].ToString().Split('@')[2];
                dtpDoj.Text = ((System.Data.DataRowView)(cbEname.SelectedItem)).Row.ItemArray[1].ToString().Split('@')[3];
            }
            txtDept.Text = "INTERNAL AUDIT";
           
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            FillAuditEmployeeDetails();
        }

        public void AddAuditDeatilsGrid(DataGridView dgvAuditorDetails)
        {
            try
            {
                bool isItemExisted = false;
                if (objPhystkfrm.gvAuditorDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < objPhystkfrm.gvAuditorDetails.Rows.Count; i++)
                    {
                        if (cbEname.Text.ToString().Split('-')[0].Equals(objPhystkfrm.gvAuditorDetails.Rows[i].Cells["Ecode"].Value.ToString()))
                        {
                            isItemExisted = true;
                        }
                    }
                }
                if (isItemExisted == false)
                {


                    DataGridViewRow temprow = new DataGridViewRow();

                    DataGridViewCell Cellsno = new DataGridViewTextBoxCell();
                    Cellsno.Value = dgvAuditorDetails.Rows.Count + 1;
                    temprow.Cells.Add(Cellsno);

                    DataGridViewCell CellEcode = new DataGridViewTextBoxCell();
                    CellEcode.Value = cbEname.Text.ToString().Split('-')[0];
                    temprow.Cells.Add(CellEcode);

                    DataGridViewCell CellName = new DataGridViewTextBoxCell();
                    CellName.Value = cbEname.Text.ToString().Split('-')[1];
                    temprow.Cells.Add(CellName);

                    DataGridViewCell CellDesig = new DataGridViewTextBoxCell();
                    CellDesig.Value = txtauditDesig.Text.ToString();
                    temprow.Cells.Add(CellDesig);


                    DataGridViewCell Celldoj = new DataGridViewTextBoxCell();
                    Celldoj.Value = dtpDoj.Value.ToString("dd/MMM/yyyy");
                    temprow.Cells.Add(Celldoj);


                    dgvAuditorDetails.Rows.Add(temprow);


                    if (dgvAuditorDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvAuditorDetails.Rows.Count; i++)
                        {
                            dgvAuditorDetails.Rows[i].Cells["SlNo1"].Value = i + 1;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvAuditorDetails = null;
            if (txtEcodeSearch.Text != null)
            {
                
                    dgvAuditorDetails = ((Physicalstkcount)objPhystkfrm).gvAuditorDetails;
                    AddAuditDeatilsGrid(dgvAuditorDetails);
                    this.Close(); 
               
            }
            else
            {
                MessageBox.Show("Audit Details not saved");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            cbEname.Text = "";
            txtauditDesig.Text = "";
            txtDept.Text = "";
            dtpDoj.Text="";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }
       
      
    }
}
