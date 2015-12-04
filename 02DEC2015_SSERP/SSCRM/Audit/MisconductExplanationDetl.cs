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
    public partial class MisconductExplanationDetl : Form
    {
        SQLDB objSQLdb = null;
        public MisconductForm objMisconductForm;
        public AuditMisconductBranch objAuditMisconductBranch;
        DataRow[] drs;
        string strFormType = "";

        public MisconductExplanationDetl(string sFormType)
        {
            InitializeComponent();
            strFormType = sFormType;
        }

        public MisconductExplanationDetl(string sFrmType,DataRow[] dr)
        {
            InitializeComponent();
            strFormType = sFrmType;
            drs = dr;
        }

        private void MisconductExplanationDetl_Load(object sender, EventArgs e)
        {
            FillDeptDetails();


            if (drs != null)
            {
                txtEcodeSearch.Text = drs[0]["EmpCode_Expl"].ToString();
                txtEname.Text = drs[0]["EmpName_Expl"].ToString();
                cbDept.SelectedValue = drs[0]["DeptCode_Expl"].ToString();
                cbDesig.SelectedValue = drs[0]["DesigCode_Expl"].ToString();
                rtbExplanation.Text = drs[0]["Explanation"].ToString();
            }

        }

        private void FillDeptDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                string strCmd = "SELECT dept_desc ,dept_code  FROM Dept_Mas";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbDept.DataSource = dt;
                    cbDept.DisplayMember = "dept_desc";
                    cbDept.ValueMember = "dept_code";
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

        private void FillDesigComboBox()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbDesig.DataSource = null;
            try
            {
                if (cbDept.SelectedIndex > 0)
                {
                    
                    string strCmd = "SELECT desig_desc, desig_code FROM DESIG_MAS " +
                                    " WHERE dept_code=" + Convert.ToInt32(cbDept.SelectedValue.ToString()) +
                                    " ORDER BY desig_desc";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbDesig.DataSource = dt;
                    cbDesig.DisplayMember = "desig_desc";
                    cbDesig.ValueMember = "desig_code";
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


        private void GetEmpName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME EmpName,DEPT_ID DeptId,DESG_ID DesigCode "+
                                    " FROM EORA_MASTER "+
                                    " WHERE ECODE= "+ Convert.ToInt32(txtEcodeSearch.Text) +"";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtEname.Text = dt.Rows[0]["EmpName"].ToString();
                        cbDept.SelectedValue = dt.Rows[0]["DeptId"].ToString();
                        cbDesig.SelectedValue = dt.Rows[0]["DesigCode"].ToString();
                    }
                    else
                    {
                        txtEname.Text = "";
                        cbDept.SelectedIndex = 0;
                        cbDesig.SelectedIndex = -1;
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
            else
            {
                txtEname.Text = "";
            }

        }


        private bool CheckData()
        {
            bool bFlag = true;

            if (txtEname.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Valid Ecode","Misconduct Form",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                txtEcodeSearch.Text = "";
                txtEcodeSearch.Focus();
                return bFlag;
            }
            if (cbDept.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Employee Department", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbDept.Focus();
                return bFlag;
            }
            if (cbDesig.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Employee Designation", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbDesig.Focus();
                return bFlag;
            }
            if (rtbExplanation.Text.Length <= 20)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Explanation", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rtbExplanation.Focus();
                return bFlag;
            }

            return bFlag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (CheckData() == true)
            {
                if (strFormType.Equals("HO"))
                {

                    if (drs != null)
                    {
                        ((MisconductForm)objMisconductForm).dtExplDetl.Rows.Remove(drs[0]);
                    }


                    ((MisconductForm)objMisconductForm).dtExplDetl.Rows.Add(new object[] { "-1",txtEcodeSearch.Text,cbDesig.SelectedValue.ToString(),cbDept.SelectedValue.ToString(),
                                               txtEname.Text.ToString(),cbDesig.Text.ToString(),cbDept.Text.ToString(),rtbExplanation.Text.Replace("\'", "") });
                    ((MisconductForm)objMisconductForm).GetExplanationDetails();
                }
                else if (strFormType.Equals("BRANCH"))
                {
                    if (drs != null)
                    {
                        ((AuditMisconductBranch)objAuditMisconductBranch).dtExplDetl.Rows.Remove(drs[0]);
                    }


                    ((AuditMisconductBranch)objAuditMisconductBranch).dtExplDetl.Rows.Add(new object[] { "-1",txtEcodeSearch.Text,cbDesig.SelectedValue.ToString(),cbDept.SelectedValue.ToString(),
                                               txtEname.Text.ToString(),cbDesig.Text.ToString(),cbDept.Text.ToString(),rtbExplanation.Text.Replace("\'", "") });
                    ((AuditMisconductBranch)objAuditMisconductBranch).GetExplanationDetails();
                }

                    
                this.Close();
               
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                GetEmpName();
            }
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDept.SelectedIndex > 0)
            {
                FillDesigComboBox();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            cbDept.SelectedIndex = 0;
            cbDesig.SelectedIndex = 0;
            rtbExplanation.Text = "";
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
    }
}
