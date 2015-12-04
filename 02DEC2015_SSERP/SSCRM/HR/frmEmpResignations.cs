using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;



namespace SSCRM
{
    public partial class frmEmpResignations : Form
    {
        SQLDB objDB = null;
        public frmEmpResignations()
        {
            InitializeComponent();

        }

        private void FillEmpDetails()
        {
            if (txtEcodeSearch.Text != "")
            {
                objDB = new SQLDB();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[1];
               
                try
                {
                    param[0] = objDB.CreateParameter("@xEcode", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);
                    ds = objDB.ExecuteDataSet("GetEmpDetailsForRollback", CommandType.StoredProcedure, param);

                    //sqlText = "SELECT HRIS_EMP_NAME,HAMH_APPL_NUMBER,DESIG,BRANCH_NAME,EMP_DOJ,EMP_DOB,dept_name,CM_COMPANY_NAME,FATHER_NAME " +
                    //          "FROM EORA_MASTER EM INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                    //          "INNER JOIN Dept_Mas DM ON DM.dept_code=EM.DEPT_ID " +
                    //          "INNER JOIN COMPANY_MAS CM ON CM.CM_COMPANY_CODE=EM.company_code " +
                    //          "INNER JOIN HR_APPL_MASTER_HEAD ON ECODE=HAMH_EORA_CODE " +
                    //          " WHERE EM.ECODE = " + txtEcodeSearch.Text;

                    //ds = objDB.ExecuteDataSet(sqlText);
                    
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtApplNo.Text = ds.Tables[0].Rows[0]["HAMH_APPL_NUMBER"].ToString();
                            txtMemberName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                            txtFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                            txtDesig.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                            txtDept.Text = ds.Tables[0].Rows[0]["dept_name"].ToString();
                            txtBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                            txtComp.Text = ds.Tables[0].Rows[0]["CM_COMPANY_NAME"].ToString();
                            txtDataofBirth.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Dob"]).ToString("dd/MM/yyyy");
                            txtDateOfJoin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Doj"]).ToString("dd/MM/yyyy");
                            txtStatus.Text = ds.Tables[0].Rows[0]["Status"].ToString();
                            if (ds.Tables[0].Rows[0]["HAMH_LEFT_DATE"].ToString().Length != 0)
                            {
                                dtpLeftdt.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["HAMH_LEFT_DATE"]);
                            }
                            txtReson.Text=ds.Tables[0].Rows[0]["HAMH_LEFT_REASON"].ToString();
                            
                        }
                        else
                        {
                            txtApplNo.Text = "";
                            txtMemberName.Text = "";
                            txtDesig.Text = "";
                            txtFatherName.Text = "";
                            txtDept.Text = "";
                            txtDataofBirth.Text = "";
                            txtDateOfJoin.Text = "";
                            txtBranch.Text = "";
                            txtComp.Text = "";
                            txtReson.Text = "";
                            txtStatus.Text = "";
                            dtpLeftdt.Value = DateTime.Today;

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
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            FillEmpDetails();
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtApplNo.Text = "";
            txtMemberName.Text = "";
            txtDesig.Text = "";
            txtFatherName.Text = "";
            txtDept.Text = "";
            txtDataofBirth.Text = "";
            txtDateOfJoin.Text = "";
            txtBranch.Text = "";
            txtComp.Text = "";
            txtEcodeSearch.Text = "";
            dtpLeftdt.Value = DateTime.Today;
            txtStatus.Text = "";
            txtReson.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            string strLastMapped;
            if (CheckData() == true)
            {
                if (txtStatus.Text == "WORKING" || txtStatus.Text == "PENDING")
                {
                    if (Convert.ToDateTime(dtpLeftdt.Value) <= DateTime.Today)
                    {
                       DialogResult dlgResult = MessageBox.Show("Do you want to left this person?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                       if (dlgResult == DialogResult.Yes)
                       {
                        strLastMapped = objDB.ExecuteDataSet("exec Get_Last_Mapped '" + txtEcodeSearch.Text + "'").Tables[0].Rows[0][0].ToString();

                       
                            if (strLastMapped != "")
                            {
                                if (Convert.ToDateTime(dtpLeftdt.Value) > Convert.ToDateTime(strLastMapped))
                                {
                                    double dDays = (Convert.ToDateTime(dtpLeftdt.Value) - Convert.ToDateTime(strLastMapped)).TotalDays;
                                    if (dDays > 45 )
                                    {
                                        int iRetVal = 0;
                                        try
                                        {
                                            string sReason = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_LEFT_REASON='" + txtReson.Text.Replace("'","") +
                                                "',HAMH_WORKING_STATUS='L', HAMH_LEFT_DATE='" + Convert.ToDateTime(dtpLeftdt.Value).ToString("dd/MMM/yyyy") +
                                                "',HAMH_LEFT_APPROVAL_ECODE=" + CommonData.LogUserEcode +
                                                ",HAMH_LEFT_UPDATED_DATE=GETDATE() WHERE HAMH_APPL_NUMBER=" + txtApplNo.Text.ToString() + ";";
                                            sReason += " UPDATE EORA_MASTER SET EORA = 'L' WHERE ECODE =" + txtEcodeSearch.Text.ToString();
                                            sReason += " EXEC Amsbd_BioTransfer_InsDel_OD " + txtEcodeSearch.Text + ", '', 'DEL_IN_ALL_DEV'";
                                            sReason += " EXEC LeftRollBackUpdate "+txtEcodeSearch.Text+","+txtApplNo.Text+",'"+CommonData.LogUserId+
                                                "','','"+Convert.ToDateTime(dtpLeftdt.Value).ToString("dd/MMM/yyyy")+"','','LEFT'";
                                            iRetVal = objDB.ExecuteSaveData(sReason);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.ToString());
                                        }
                                        finally
                                        {
                                            objDB = null;
                                        }
                                        if (iRetVal > 0)
                                        {
                                            MessageBox.Show("Updated successfully.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtApplNo.Text = "";
                                            txtMemberName.Text = "";
                                            txtDesig.Text = "";
                                            txtFatherName.Text = "";
                                            txtDept.Text = "";
                                            txtDataofBirth.Text = "";
                                            txtDateOfJoin.Text = "";
                                            txtBranch.Text = "";
                                            txtComp.Text = "";
                                            txtEcodeSearch.Text = "";
                                            dtpLeftdt.Value = DateTime.Today;
                                            txtStatus.Text = "";
                                            txtReson.Text = "";
                                        }
                                        else
                                            MessageBox.Show("Update Failure.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Employee Mapped in " + strLastMapped + "\nCannot Update to Left", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }

                            else
                            {
                                int iRetVal = 0;
                                try
                                {
                                    string sReason = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_LEFT_REASON='" + txtReson.Text +
                                        "',HAMH_WORKING_STATUS='L',HAMH_LEFT_DATE='" + Convert.ToDateTime(dtpLeftdt.Value).ToString("dd/MMM/yyyy") +
                                        "',HAMH_LEFT_APPROVAL_ECODE=" + CommonData.LogUserEcode +
                                        ",HAMH_LEFT_UPDATED_DATE=GETDATE() WHERE HAMH_APPL_NUMBER=" + txtApplNo.Text.ToString() + ";";
                                    sReason += " UPDATE EORA_MASTER SET EORA = 'L' WHERE ECODE =" + txtEcodeSearch.Text.ToString();
                                    sReason += " EXEC Amsbd_BioTransfer_InsDel_OD " + txtEcodeSearch.Text + ", '', 'DEL_IN_ALL_DEV'";
                                    sReason += " EXEC LeftRollBackUpdate " + txtEcodeSearch.Text + "," + txtApplNo.Text + ",'" + CommonData.LogUserId +
                                        "','','" + Convert.ToDateTime(dtpLeftdt.Value).ToString("dd/MMM/yyyy") + "','','LEFT'";
                                    iRetVal = objDB.ExecuteSaveData(sReason);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                                finally
                                {
                                    objDB = null;
                                }
                                if (iRetVal > 0)
                                {
                                    MessageBox.Show("Updated successfully.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtApplNo.Text = "";
                                    txtMemberName.Text = "";
                                    txtDesig.Text = "";
                                    txtFatherName.Text = "";
                                    txtDept.Text = "";
                                    txtDataofBirth.Text = "";
                                    txtDateOfJoin.Text = "";
                                    txtBranch.Text = "";
                                    txtComp.Text = "";
                                    txtEcodeSearch.Text = "";
                                    dtpLeftdt.Value = DateTime.Today;
                                    txtStatus.Text = "";
                                    txtReson.Text = "";
                                }
                                else
                                    MessageBox.Show("Update Failure.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                       
                    }

                    }
                    else
                        MessageBox.Show("Enter Valid Date.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else
                    MessageBox.Show("Employee Not in Working Status.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtEcodeSearch.Focus();
        }
       private bool CheckData()
        {
            bool bFlag = true;
            if (txtMemberName.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Valid Ecode", "frmEmpResignations", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeSearch.Focus();
                return bFlag;
            }

            if (txtReson.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Reason for Left", "frmEmpResignations", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReson.Focus();
                return bFlag;
            }

            return bFlag;
        }
        
       
    }
}
