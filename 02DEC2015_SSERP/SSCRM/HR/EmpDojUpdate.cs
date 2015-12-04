using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using SSCRMDB;

namespace SSCRM
{
    public partial class EmpDOjUpdate : Form
    {
        SQLDB objDB = null;                        
        public EmpDOjUpdate()
        {
            InitializeComponent();
        }
        private void Employee_Doj_Update_Load(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                //dtpDOB.Enabled = false;
                //txtNameEdit.ReadOnly = true;
                //txtFatherEdit.ReadOnly = true;
            }
        }
        private void txtEcode_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sqlText = "";
            objDB = new SQLDB();
            if (txtName.Text != "" && txtEcode.Text != "")
            {
                try
                {                                         
                    
                    sqlText += " UPDATE HR_APPL_MASTER_HEAD SET HAMH_NAME = '" + txtNameEdit.Text.ToUpper() + "'" +
                                ",HAMH_DOB='" + Convert.ToDateTime(dtpDOB.Value).ToString("dd/MMM/yyyy") + "'" +
                                ",HAMH_DOJ='" + Convert.ToDateTime(dtTPDoj.Value).ToString("dd/MMM/yyyy") + "'" +
                                ",HAMH_FORH_NAME='" + txtFatherEdit.Text.ToUpper() + "'" +
                                " WHERE HAMH_EORA_CODE=" + txtEcode.Text;
                    sqlText += " UPDATE HR_APPL_CHECK SET HAED_NAME=HAMH_NAME,HAED_FATHER_NAME=HAMH_FORH_NAME,HAED_DATEOFBIRTH=HAMH_DOB" +
                               " FROM HR_APPL_MASTER_HEAD WHERE HAMH_APPL_NUMBER = HAED_APPL_NUMBER AND HAMH_EORA_CODE=" + txtEcode.Text;
                    sqlText += " UPDATE EORA_MASTER SET EMP_DOB='" + Convert.ToDateTime(dtpDOB.Value).ToString("dd/MMM/yyyy") + "', EMP_DOJ='" + Convert.ToDateTime(dtTPDoj.Value).ToString("dd/MMM/yyyy") + "'," +
                                "HRIS_EMP_NAME='" + txtNameEdit.Text.ToUpper() + "',MEMBER_NAME='" + txtNameEdit.Text.ToUpper() + "',FATHER_NAME='" + txtFatherEdit.Text.ToUpper() + "' WHERE ECODE=" + txtEcode.Text;
                    objDB = new SQLDB();
                    i = objDB.ExecuteSaveData(sqlText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }
                if (i > 0)
                {
                    
                    MessageBox.Show("Data Saved Successfully", "Employee Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "Employee Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Please enter the details");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            txtName.Clear();
            txtDob.Clear();
            txtFatherName.Clear();
            txtDesg.Clear();
            txtBranch.Clear();
            txtEcode.Clear();
            txtDoj.Clear();
            //txtAge.Clear();
            txtNameEdit.Clear();
            txtFatherEdit.Clear();
            dtTPDoj.Value = DateTime.Today;
            dtpDOB.Value = DateTime.Today;
        }

        private void showEmpDetails()
        {
            if (txtEcode.Text != "")
            {

                objDB = new SQLDB();
                DataSet ds = new DataSet();
                string sqlText = "";
                try
                {
                    sqlText = " SELECT HRIS_EMP_NAME,EMP_DOJ,EMP_DOB,FATHER_NAME,BRANCH_NAME,desig_name " +
                            "FROM EORA_MASTER EM INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                            "INNER JOIN DESIG_MAS ON EM.DESG_ID = desig_code WHERE	EM.ECODE = " + txtEcode.Text;
                    ds = objDB.ExecuteDataSet(sqlText);                    
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtName.Text = ds.Tables[0].Rows[0]["HRIS_EMP_NAME"].ToString();
                        txtFatherName.Text = ds.Tables[0].Rows[0]["FATHER_NAME"].ToString();
                        txtDesg.Text = ds.Tables[0].Rows[0]["desig_name"].ToString();
                        txtBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                        txtDob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOB"]).ToShortDateString();
                        txtDoj.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOJ"]).ToShortDateString();
                        //int age = DateTime.Today.Year - Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOB"].ToString()).Year;
                        //txtAge.Text = age.ToString();
                        dtpDOB.CustomFormat = "dd/MM/yyyy";
                        dtTPDoj.CustomFormat = "dd/MM/yyyy";
                        dtTPDoj.Value = Convert.ToDateTime(txtDoj.Text);
                        dtpDOB.Value = Convert.ToDateTime(txtDob.Text);
                        txtNameEdit.Text = ds.Tables[0].Rows[0]["HRIS_EMP_NAME"].ToString();
                        txtFatherEdit.Text = ds.Tables[0].Rows[0]["FATHER_NAME"].ToString();
                    }
                    else
                    {
                        //MessageBox.Show("Please Enter Valid Ecode");
                        txtName.Clear();
                        txtDob.Clear();
                        txtFatherName.Clear();
                        txtDesg.Clear();
                        txtBranch.Clear();
                        //txtEcode.Clear();
                        txtDoj.Clear();
                        //txtAge.Clear();
                        txtNameEdit.Clear();
                        txtFatherEdit.Clear();
                        dtTPDoj.Value = DateTime.Today;
                        dtpDOB.Value = DateTime.Today;
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
                txtName.Clear();
                txtDob.Clear();
                txtFatherName.Clear();
                txtDesg.Clear();
                txtBranch.Clear();
                txtDoj.Clear();
                //txtAge.Clear();
                txtNameEdit.Clear();
                txtFatherEdit.Clear();
                dtTPDoj.Value = DateTime.Today;
                dtpDOB.Value = DateTime.Today;
            }
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            showEmpDetails();
        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
        
    }
}
