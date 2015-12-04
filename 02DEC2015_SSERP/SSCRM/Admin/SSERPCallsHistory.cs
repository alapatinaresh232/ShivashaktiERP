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
    public partial class SSERPCallsHistory : Form
    {
        SQLDB objDB = null;
        public SSERPCallsHistory()
        {
            InitializeComponent();
        }

        private void SSERPCallsHistory_Load(object sender, EventArgs e)
        {
            dtpMonth.Value = DateTime.Today;
            cbStatus.SelectedIndex = 0;
            
            FillCompanyData();

        }
        private void FillCompanyData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objDB.ExecuteDataSet(strCmd).Tables[0];

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
                objDB = null;
                dt = null;
            }


        }
        private void FillBranchData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS  WHERE ACTIVE='T'"+
                        "  AND BRANCH_TYPE='BR' AND COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";
                    dt = objDB.ExecuteDataSet(strCommand).Tables[0];
                }


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BRANCH_CODE";
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

     
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
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
        

        private void txtUserEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                if (txtUserEcodeSearch.Text != "")
                {
                    strCmd = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE=" + txtUserEcodeSearch.Text.ToString() + "";
                    dt = objDB.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtUserName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    }
                    else
                    {
                        txtUserName.Text = "";
                        txtManagerEcodeSearch.Text = "";
                        txtContactNo.Text = "";
                        txtManagerName.Text = "";
                        //txtErpProblem.Text = "";
                        //txtErpSolution.Text = "";
                        //txtErpReason.Text = "";
                        cbStatus.SelectedIndex = 0;
                    }
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

        private void txtManagerEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                if (txtManagerEcodeSearch.Text != "")
                {
                    strCmd = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE=" + txtManagerEcodeSearch.Text.ToString() + "";
                    dt = objDB.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtManagerName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    }
                    else
                    {
                        //txtErpProblem.Text = "";
                        txtManagerName.Text = "";
                        //txtErpSolution.Text = "";
                        //txtErpReason.Text = "";
                        //cbStatus.SelectedIndex = 0;
                    }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex =0;
            cbBranch.SelectedIndex =-1;
            cbStatus.SelectedIndex = 0;
            dtpMonth.Value = DateTime.Today;
            txtUserEcodeSearch.Text = "";
            txtManagerEcodeSearch.Text = "";
            txtUserName.Text = "";
            txtManagerName.Text = "";
            txtErpProblem.Text = "";
            txtErpSolution.Text = "";
            txtErpReason.Text = "";
            txtContactNo.Text = "";

        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( cbStatus.SelectedIndex==1)
            {  
              
                txtErpReason.Visible = false;
                lblErpReason.Visible = false;

            }
            else if(cbStatus.SelectedIndex==2)
            {
                
                txtErpReason.Visible = true;
                lblErpReason.Visible = true;

            }
        }
        private bool CheckData()
        {
            bool Chkflag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                cbStatus.Focus();
                return Chkflag;
            }
            if (cbBranch.SelectedIndex == 0)
            {
                MessageBox.Show("Select Branch", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                cbStatus.Focus();
                return Chkflag;
            }
            else if (txtUserEcodeSearch.Text.Length == 0)
            {
                MessageBox.Show("Enter UserECode", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                txtUserEcodeSearch.Focus();
                return Chkflag;
            }
            //else if (txtContactNo.Text.Length == 0)
            //{
                         
            //    MessageBox.Show("Enter Contact No", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Chkflag = false;
            //    txtUserEcodeSearch.Focus();
            //    return Chkflag;
            //}
            else if (txtManagerEcodeSearch.Text.Length == 0)
            {
                MessageBox.Show("Enter ManagerECode", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                txtManagerEcodeSearch.Focus();
                return Chkflag;
            }
            else if (txtErpProblem.Text.Length== 0)
            {
                MessageBox.Show("  Please Enter Erp Problem", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                cbStatus.Focus();
                return Chkflag;
            }
            else if (txtErpSolution.Text.Length == 0)
            {
                MessageBox.Show("  Please Enter Erp Solution", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                cbStatus.Focus();
                return Chkflag;
            }          

             else if (cbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Select Status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                cbStatus.Focus();
                return Chkflag;
            }          

            else if (cbStatus.SelectedIndex == 2)
            {
                if (txtErpReason.Text == "")
                {
                    MessageBox.Show("Enter Erp Reason", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Chkflag = false;

                    txtErpReason.Focus();
                    return Chkflag;
                }
         
            }




            return Chkflag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            int Ival = 0;
            objDB = new SQLDB();
            //DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                if (txtContactNo.Text == "")
                {
                    txtContactNo.Text = "0";
                }
              
                if (CheckData())
                {
                    strCmd = "INSERT INTO SSERP_CALLS_HISTORY( SCH_COMP_CODE" +
                                                             ",SCH_BRANCH_CODE" +
                                                             ",SCH_USER_ECODE" +
                                                             ",SCH_MGR_ECODE" +
                                                             ",SCH_ERP_PROBLEM" +
                                                             ",SCH_SOLUTION_GIVEN" +
                                                             ",SCH_STATUS";
                    if (cbStatus.Text.Equals("NOTSOLVED"))
                    {
                        strCmd += ",SCH_NOT_SOLVED_REASON";
                    }

                    strCmd +=",SCH_USER_CONTACT"+
                             ",SCH_ISSUE_DATE" +
                             ",SCH_CREATED_BY" +
                             ",SCH_CREATED_DATE)" +
                             " VALUES('" + cbCompany.SelectedValue.ToString() +
                             "','" + cbBranch.SelectedValue.ToString() +
                             "'," + Convert.ToInt32(txtUserEcodeSearch.Text.ToString()) +
                             "," + Convert.ToInt32(txtManagerEcodeSearch.Text.ToString()) +
                             ",'" + txtErpProblem.Text.ToString() +
                             "','" + txtErpSolution.Text.ToString() +
                             "','" + cbStatus.SelectedItem.ToString() + "'";
                    if (cbStatus.Text.Equals("NOTSOLVED"))
                    {
                        strCmd += ",'" + txtErpReason.Text.ToString() + "'";
                    }

                    strCmd += ","+txtContactNo.Text.ToString()+
                              ",'" + Convert.ToDateTime(dtpMonth.Value).ToString("dd/MMM/yyyy") +
                              "','" + CommonData.LogUserId +
                              "',getdate())";
                    if (strCmd.Length > 5)
                    {
                        Ival = objDB.ExecuteSaveData(strCmd);
                    }
                    if (Ival > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                    }
                   
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }           
           else if (cbCompany.SelectedIndex == 0)
            {
                cbBranch.DataSource = null;
                cbStatus.SelectedIndex = 0;
                dtpMonth.Value = DateTime.Today;
                txtUserEcodeSearch.Text = "";
                txtManagerEcodeSearch.Text = "";
                txtUserName.Text = "";
                txtManagerName.Text = "";
                txtErpProblem.Text = "";
                txtErpSolution.Text = "";
                txtErpReason.Text = "";
            }
        }

    }
}
