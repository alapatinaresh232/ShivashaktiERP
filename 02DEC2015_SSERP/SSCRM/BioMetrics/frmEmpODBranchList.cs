using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SSCRMDB;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;


namespace SSCRM
{
    public partial class frmEmpODBranchList : Form
    {
        SQLDB objDB = null;
        DataGridViewRow dgvr;
        private frmEmpODDetails objEmpODDetails = null;

        public frmEmpODBranchList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            DataSet ds = new DataSet();
            if (CheckData() == true)
            {
                string Sqltext = "select SUBSTRING(badgenumber,5,10) as ecode from AMSDB.dbo.userinfo  where SUBSTRING(badgenumber,5,10) ='"+txtEcode.Text.ToString()+"'  ";
                ds = objDB.ExecuteDataSet(Sqltext);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ecode"].ToString() != "")
                    {
                        frmEmpODDetails EmpCompanyDetails = new frmEmpODDetails();
                        EmpCompanyDetails.objEmpODBranchList = this;
                        EmpCompanyDetails.Show();
                    }
                }
                else
                    MessageBox.Show("Enrollment Details Not Found. Please Enroll Your Ecode", "frmEmpODBranchList", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ShowEmpDetails()
        {
            if (txtEcode.Text != "")
            {
                objDB = new SQLDB();
                DataSet ds = new DataSet();
                DataSet dsPhoto = new DataSet();
                string sqlText = "";
                try
                {

                    sqlText = "SELECT HRIS_EMP_NAME,HAMH_APPL_NUMBER,DESIG,BRANCH_NAME,FATHER_NAME " +
                              "FROM EORA_MASTER EM INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE "+
                              "INNER JOIN HR_APPL_MASTER_HEAD ON ECODE=HAMH_EORA_CODE " +
                              " WHERE EM.ECODE = "+ txtEcode.Text;
                                   

                    ds = objDB.ExecuteDataSet(sqlText);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtName.Text = ds.Tables[0].Rows[0]["HRIS_EMP_NAME"].ToString();
                        txtName.Tag = ds.Tables[0].Rows[0]["HAMH_APPL_NUMBER"].ToString();
                        txtFatherName.Text = ds.Tables[0].Rows[0]["FATHER_NAME"].ToString();
                        txtDesg.Text = ds.Tables[0].Rows[0]["DESIG"].ToString();
                        txtBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();

                        dsPhoto = objDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + txtEcode.Text);

                        if (dsPhoto.Tables[0].Rows.Count > 0)
                            GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                        else
                            pictureBox1.BackgroundImage = null;

                    }
                    else
                    {
                        txtName.Clear();
                        txtFatherName.Clear();
                        txtDesg.Clear();
                        txtBranch.Clear();
                        pictureBox1.BackgroundImage = null;
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
                txtFatherName.Clear();
                txtDesg.Clear();
                txtBranch.Clear();
                pictureBox1.BackgroundImage = null;
            }
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            ShowEmpDetails();
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

        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                pictureBox1.BackgroundImage = newImage;
                this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmEmpODBranchList_Load(object sender, EventArgs e)
        {
            
        }

        private DataSet GetDetails(Int32 Ecode)
        {
            DataSet ds = new DataSet();
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            try
            {
                param[0] = objDB.CreateParameter("@sEcode", DbType.Int32, Ecode, ParameterDirection.Input);

                ds = objDB.ExecuteDataSet("HR_EMP_OD_BRANCH_DETAILS", CommandType.StoredProcedure, param);

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
        public void FillEmpODGridDetails()
       {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            gvEmployeeList.Rows.Clear();
            
            if (txtEcode.Text.Length > 0)
               {
                   try
                   {
                       dt = GetDetails(Convert.ToInt32(txtEcode.Text)).Tables[0];
                       for (int i = 0; i < dt.Rows.Count; i++)
                       {
                           DataGridViewRow tempRow = new DataGridViewRow();
                           DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                           cellSLNO.Value = i + 1;
                           tempRow.Cells.Add(cellSLNO);

                           DataGridViewCell cellCompany = new DataGridViewTextBoxCell();
                           cellCompany.Value = dt.Rows[i]["CM_COMPANY_NAME"]; ;
                           tempRow.Cells.Add(cellCompany);

                           DataGridViewCell cellBranchcode = new DataGridViewTextBoxCell();
                           cellBranchcode.Value = dt.Rows[i]["HOBL_OD_BRANCH_CODE"]; ;
                           tempRow.Cells.Add(cellBranchcode);

                           DataGridViewCell cellBranch = new DataGridViewTextBoxCell();
                           cellBranch.Value = dt.Rows[i]["BRANCH_NAME"]; ;
                           tempRow.Cells.Add(cellBranch);

                           DataGridViewCell cellDvSlNo = new DataGridViewTextBoxCell();
                           cellDvSlNo.Value = dt.Rows[i]["HOBL_ICLOCK_SLNO"]; ;
                           tempRow.Cells.Add(cellDvSlNo);


                           gvEmployeeList.Rows.Add(tempRow);

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
        }

        private void txtEcode_TextChanged(object sender, EventArgs e)
        {
            FillEmpODGridDetails();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcode.Text = "";
            txtName.Text = "";
            txtFatherName.Text = "";
            txtDesg.Text = "";
            txtBranch.Text = "";
            pictureBox1.BackgroundImage = null;
            gvEmployeeList.Rows.Clear();
        }

        private void gvEmployeeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ires = 0;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvEmployeeList.Columns["Del"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want to Delete this Record ", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        dgvr = gvEmployeeList.Rows[e.RowIndex];
                        objDB = new SQLDB();
                        string strcmd = "";

                        try
                        {
                            strcmd = " Delete from HR_EMP_OD_BRANCH_LIST where HOBL_OD_BRANCH_CODE='"+gvEmployeeList.Rows[e.RowIndex].Cells["BRNCODE"].Value+
                                "' AND HOBL_EORA_CODE='"+txtEcode.Text.ToString()+"' ";

                            strcmd += " EXEC Amsbd_BioTransfer_InsDel_OD " + txtEcode.Text.ToString() +
                                             ",'" + gvEmployeeList.Rows[e.RowIndex].Cells["DvSLNO"].Value+ "','DELETE'";
                                                  
                            objDB = new SQLDB();
                            ires = objDB.ExecuteSaveData(strcmd);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (ires > 0)
                        {
                            MessageBox.Show("Data Deleted Successfully");
                            FillEmpODGridDetails();

                        }
                        else
                        {
                            MessageBox.Show("Data not Deleted");
                        }
                    }
                }

            }
        }
        private bool CheckData()
        {
            bool bFlag = true;
            if (txtName.Text.Length ==0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Valid Ecode", "frmEmpODBranchList", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcode.Focus();
                return bFlag;
            }
            
            return bFlag;
        }
       
    }
}
