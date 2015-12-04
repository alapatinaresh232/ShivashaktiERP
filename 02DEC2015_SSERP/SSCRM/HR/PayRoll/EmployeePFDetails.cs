using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;


namespace SSCRM
{
    public partial class EmployeePFDetails : Form
    {
        SQLDB objSQLdb = null;
        int empApplNo = 0;
        int TrnNo = 0;
        bool flagUpdate = false;

        public EmployeePFDetails()
        {
            InitializeComponent();
        }

        private void EmployeePFDetails_Load(object sender, EventArgs e)
        {
            dtpEffDate.Value = DateTime.Today;

        }

        private void GetEmpPfDetails(Int32 iEcode)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            gvEmpPFDetails.Rows.Clear();
            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    strCmd = "SELECT PPD_PF_NO PFNo " +
                                         ",PPD_PF_BASIC PFBasic " +
                                         ",PPD_EFF_DATE EFFDate " +
                                         ",PPD_TRN_ID " +
                                         ",HAMH_APPL_NUMBER ApplNumber " +
                                         ",dept_name DeptName " +
                                         ",HRIS_EMP_NAME EmpName " +
                                         ",DESIG EmpDesig " +
                                         ",CM_COMPANY_NAME CompName " +
                                         ",BRANCH_NAME	BranchName " +
                                         " FROM PR_PF_DATA " +
                                         " INNER JOIN EORA_MASTER EM ON ECODE=PPD_EORA_CODE " +
                                         " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                                         " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = EM.company_code " +
                                         " INNER JOIN Dept_Mas ON dept_code = DEPT_ID " +
                                         " INNER JOIN DESIG_MAS ON desig_code = DESG_ID " +
                                         " INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE = ECODE " +
                                         " WHERE PPD_EORA_CODE=" + iEcode + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        TrnNo = Convert.ToInt32(dt.Rows[0]["PPD_TRN_ID"].ToString());
                        empApplNo = Convert.ToInt32(dt.Rows[0]["ApplNumber"].ToString());
                        txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                        txtComp.Text = dt.Rows[0]["CompName"].ToString();
                        txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
                        txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                        txtHRISDesig.Text = dt.Rows[0]["EmpDesig"].ToString();
                        //txtPfNo.Text=dt.Rows[0]["PFNo"].ToString();
                        //txtPFBasic.Text=dt.Rows[0]["PFBasic"].ToString();
                        //dtpEffDate.Value = Convert.ToDateTime(dt.Rows[0]["EFFDate"]).ToString("dd/MM/yyyy");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvEmpPFDetails.Rows.Add();
                            gvEmpPFDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvEmpPFDetails.Rows[i].Cells["EmpPFNo"].Value = dt.Rows[i]["PFNo"].ToString();
                            gvEmpPFDetails.Rows[i].Cells["EmpPFBasic"].Value = dt.Rows[i]["PFBasic"].ToString();
                            gvEmpPFDetails.Rows[i].Cells["EffDate"].Value = Convert.ToDateTime(dt.Rows[i]["EFFDate"].ToString()).ToShortDateString();
                        }
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        strCmd = "SELECT HAMH_APPL_NUMBER ApplNumber " +
                                      ", dept_name DeptName " +
                                      ", HRIS_EMP_NAME EmpName " +
                                      ", DESIG EmpDesig " +
                                      ", CM_COMPANY_NAME CompName " +
                                      ", BRANCH_NAME	BranchName " +
                                      " FROM EORA_MASTER EM " +
                                      " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                                      " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = EM.company_code " +
                                      " INNER JOIN Dept_Mas ON dept_code = DEPT_ID " +
                                      " INNER JOIN DESIG_MAS ON desig_code = DESG_ID " +
                                      " INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE = ECODE " +
                                      " WHERE ECODE = "+ iEcode +"";
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            empApplNo = Convert.ToInt32(dt.Rows[0]["ApplNumber"].ToString());
                            txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                            txtComp.Text = dt.Rows[0]["CompName"].ToString();
                            txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
                            txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                            txtHRISDesig.Text = dt.Rows[0]["EmpDesig"].ToString();



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
           
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                GetEmpPfDetails(Convert.ToInt32(txtEcodeSearch.Text));
            }
            else
            {
                txtEName.Text = string.Empty;
                txtHRISDesig.Text = string.Empty;
                txtDept.Text = string.Empty;
                txtComp.Text = string.Empty;
                txtBranch.Text = string.Empty;
                gvEmpPFDetails.Rows.Clear();
                txtPFBasic.Text = "";
                txtPfNo.Text = "";
                dtpEffDate.Value = DateTime.Today;

            }

        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode","Employee PF Form",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtEName.Focus();
            }
            return flag;
        }
             

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";

            if (CheckData() == true)
            {
                try
                {
                    if (flagUpdate == true)
                    {
                        strCommand = "UPDATE PR_PF_DATA SET PPD_APPL_NO=" + empApplNo +
                                                          ",PPD_PF_NO='" + txtPfNo.Text +
                                                          "',PPD_PF_BASIC=" + Convert.ToDouble(txtPFBasic.Text).ToString("0") +
                                                          ",PPD_EFF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                                          "',PPD_MODIFIED_BY='" + CommonData.LogUserId +
                                                          "',PPD_MODIFIED_DATE='" + CommonData.CurrentDate +
                                                          "' WHERE PPD_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                                          " AND PPD_TRN_ID=" + Convert.ToInt32(TrnNo) + "";
                        flagUpdate = false;
                    }
                    else
                    {
                        strCommand = "INSERT INTO PR_PF_DATA(PPD_APPL_NO " +
                                                           ",PPD_EORA_CODE " +
                                                           ",PPD_PF_NO " +
                                                           ",PPD_PF_BASIC " +
                                                           ",PPD_EFF_DATE " +
                                                           ",PPD_CREATED_BY " +
                                                           ",PPD_CREATED_DATE " +
                                                           ")VALUES(" + empApplNo +
                                                           "," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                           ",'" + txtPfNo.Text +
                                                           "'," + Convert.ToDouble(txtPFBasic.Text).ToString("f") +
                                                           ",'" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                                           "','" + CommonData.LogUserId +
                                                           "','" + CommonData.CurrentDate + "')";
                    }
                    iRes = objSQLdb.ExecuteSaveData(strCommand);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagUpdate = false;
                    //GetEmpPfDetails(Convert.ToInt32(txtEcodeSearch.Text));
                    btnCancel_Click(null, null);
                    
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = string.Empty;
            txtEName.Text = string.Empty;
            txtHRISDesig.Text = string.Empty;
            txtDept.Text = string.Empty;
            txtComp.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtPfNo.Text = string.Empty;
            txtPFBasic.Text = string.Empty;
            dtpEffDate.Value = DateTime.Today;
            gvEmpPFDetails.Rows.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void gvEmpPFDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvEmpPFDetails.Columns["Edit"].Index)
            {
                txtPfNo.Text = gvEmpPFDetails.Rows[e.RowIndex].Cells["EmpPFNo"].Value.ToString();
                txtPFBasic.Text = gvEmpPFDetails.Rows[e.RowIndex].Cells["EmpPFBasic"].Value.ToString();
                dtpEffDate.Value = Convert.ToDateTime(gvEmpPFDetails.Rows[e.RowIndex].Cells["EffDate"].Value);
                txtPfNo.Focus();
                
                flagUpdate = true;
            }
            if (e.ColumnIndex == gvEmpPFDetails.Columns["Delete"].Index)
            {
                objSQLdb = new SQLDB();
                int iResult = 0;

                 DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dlgResult == DialogResult.Yes)
                 {
                     try
                     {
                         string strDelete = "DELETE FROM PR_PF_DATA WHERE PPD_TRN_ID=" + Convert.ToInt32(TrnNo) + " ";
                         iResult = objSQLdb.ExecuteSaveData(strDelete);

                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.ToString());
                     }
                   
                     if (iResult > 0)
                     {
                         flagUpdate = false;
                         GetEmpPfDetails(Convert.ToInt32(txtEcodeSearch.Text));
                         MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //btnCancel_Click(null, null);
                         

                     }
                     else
                     {
                         MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
                

            }
        }
       

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPFBasic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

  
       
    }
}
