using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class frmInvoiceVerification : Form
    {
        SQLDB objSQLdb = null;
        private ServiceDeptDB objDB = null;
        private string strECode = string.Empty;
        private string sEmpName = "";
        bool bFlag = false;
        DataGridViewRow RowIndex;

        public frmInvoiceVerification()
        {
            InitializeComponent();
        }

        private void frmInvoiceVerification_Load(object sender, EventArgs e)
        {
            dtpTrnDate.Value = DateTime.Today;
            FillCompanyData();
            FillObservationTypes();
            if (CommonData.BranchType == "BR")
            {
                FillBranchData();
            }
            else
            {
                FillBranchData();
                cbBranch.SelectedIndex = 0;
            }
            if (CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                txtVerifiedBy.Text = CommonData.LogUserId;
                txtVerifiedName.Text = GetEmployeeName(Convert.ToInt32(CommonData.LogUserId));
            }
            dtpDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
        }


        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

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
            cbBranch.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME BranchName,BRANCH_CODE+'@'+ STATE_CODE AS BranCode " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "' and BRANCH_TYPE='BR' ORDER BY BRANCH_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BranchName";
                    cbBranch.ValueMember = "BranCode";
                }


                string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranch.SelectedValue = BranCode;
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

        private void FillObservationTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                strCmd = "SELECT SIBTM_OBSERVATION_CODE Obsrv_Code "+
                         ",SIBTM_OBSERVATION_DESC Obsrv_Desc "+
                         " FROM SERVICES_INVOICEVERF_OBSERVATION_TYPE_MASTER "+
                         " WHERE SIBTM_OBSERVATION_TYPE='SRVINVVERF'";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbObservType.DataSource = dt;
                    cbObservType.DisplayMember = "Obsrv_Desc";
                    cbObservType.ValueMember = "Obsrv_Code";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
        }

        private string GetEmployeeName(Int32 EmpEcode)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER WHERE ECODE=" + EmpEcode + "";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sEmpName = dt.Rows[0]["EName"].ToString();
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
            return sEmpName;

        }


        private void txtVerifiedBy_Validated(object sender, EventArgs e)
        {
            if (txtVerifiedBy.Text.Length > 4)
            {
                txtVerifiedName.Text = GetEmployeeName(Convert.ToInt32(txtVerifiedBy.Text));
            }
            else
            {
                txtVerifiedName.Text = "";
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;
            }
            if (cbBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranch.Focus();
                return flag;
            }
            if (txtCampName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Order No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOrderNo.Focus();
                return flag;
            }
            if (txtOrderNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Order No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOrderNo.Focus();
                return flag;
            }
            if (txtVerifiedName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Verified By Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVerifiedBy.Focus();
                return flag;
            }
            if (gvInvVerificationDetl.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Invoice Observation Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddVerificationDetl.Focus();
                return flag;
            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                try
                {
                    if (SaveInvVerificationHead() > 0)
                    {
                        SaveInvVerificationDetl();
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null,null);
                        txtOrderNo.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        private int SaveInvVerificationHead()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            int iRes = 0;

            try
            {
                strCmd = "SELECT * FROM SERVICES_INVOICEVERF_HEAD "+
                         " WHERE SIVH_COMPANY_CODE='"+ cbCompany.SelectedValue.ToString() +
                         "' and SIVH_BRANCH_CODE='"+ cbBranch.SelectedValue.ToString().Split('@')[0] +
                         "' and SIVH_DOC_MONTH='"+ dtpDocMonth.Value.ToString("MMMyyyy").ToUpper() +
                         "' and SIVH_ORDER_NUMBER='"+ txtOrderNo.Text.ToString() +"'";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                strCmd = "";

                strCmd = "DELETE FROM SERVICES_INVOICEVERF_DETL "+
                          " WHERE SIVD_COMPANY_CODE='"+ cbCompany.SelectedValue.ToString() +
                          "' AND SIVD_BRANCH_CODE='"+ cbBranch.SelectedValue.ToString().Split('@')[0] +
                          "' and SIVD_DOC_MONTH='"+ dtpDocMonth.Value.ToString("MMMyyyy").ToUpper() +
                          "'and SIVD_ORDER_NUMBER='"+ txtOrderNo.Text +"'";

                if (dt.Rows.Count > 0)
                {
                    strCmd += " UPDATE SERVICES_INVOICEVERF_HEAD SET SIVH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                               "',SIVH_STATE_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                               "',SIVH_BRANCH_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                               "',SIVH_ECODE=" + txtVerifiedBy.Text +
                               ",SIVH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                               "',SIVH_LAST_MODIFIED_DATE=getdate() " +
                               " WHERE SIVH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                               "' and SIVH_BRANCH_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                               "' and SIVH_DOC_MONTH='" + dtpDocMonth.Value.ToString("MMMyyyy").ToUpper() +
                               "' and SIVH_ORDER_NUMBER='" + txtOrderNo.Text + "'";
                }
                else
                {

                    strCmd = "INSERT INTO SERVICES_INVOICEVERF_HEAD(SIVH_COMPANY_CODE " +
                                                                 ", SIVH_STATE_CODE " +
                                                                 ", SIVH_BRANCH_CODE " +
                                                                 ", SIVH_FIN_YEAR " +
                                                                 ", SIVH_DOC_MONTH " +
                                                                 ", SIVH_ORDER_NUMBER " +
                                                                 ", SIVH_ECODE " +
                                                                 ", SIVH_CREATED_BY " +
                                                                 ", SIVH_CREATED_DATE " +
                                                                 ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                 "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                                                                 "','" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                                                 "','" + CommonData.FinancialYear +
                                                                 "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                 "','" + txtOrderNo.Text.ToString() +
                                                                 "'," + Convert.ToInt32(txtVerifiedBy.Text) +
                                                                 ",'" + CommonData.LogUserId +
                                                                 "',getdate())";
                }

                if (strCmd.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private int SaveInvVerificationDetl()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;

            try
            {
                if (gvInvVerificationDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvInvVerificationDetl.Rows.Count; i++)
                    {
                        strCmd += "INSERT INTO SERVICES_INVOICEVERF_DETL(SIVD_COMPANY_CODE " +
                                                                     ", SIVD_STATE_CODE " +
                                                                     ", SIVD_BRANCH_CODE " +
                                                                     ", SIVD_FIN_YEAR " +
                                                                     ", SIVD_DOC_MONTH " +
                                                                     ", SIVD_ORDER_NUMBER " +
                                                                     ", SIVD_OBSERVATION_TYPE " +
                                                                     ", SIVD_OBSERVATION_CODE " +
                                                                     ", SIVD_OBSERVATION_REMARKS " +
                                                                     ", SIVD_CREATED_BY " +
                                                                     ", SIVD_CREATED_DATE " +
                                                                     ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                     "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                                                                     "','" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                                                     "','" + CommonData.FinancialYear +
                                                                     "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                     "','" + txtOrderNo.Text.ToString() +
                                                                     "','SRVINVVERF' "+
                                                                     ",'" + gvInvVerificationDetl.Rows[i].Cells["Observation_Code"].Value.ToString() +
                                                                     "','"+ gvInvVerificationDetl.Rows[i].Cells["Remarks"].Value.ToString().Replace("'","") +
                                                                     "','" + CommonData.LogUserId +
                                                                     "',getdate())";
                    }
                }

                if (strCmd.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private void GetInvoiceVerificationDetails()
        {
            objDB = new ServiceDeptDB();
            Hashtable ht = new Hashtable();
            DataTable dtHead = new DataTable();           
            DataTable dtDetl = new DataTable();
            gvInvVerificationDetl.Rows.Clear();

            if (cbBranch.SelectedIndex > 0 && txtOrderNo.Text.Length > 0)
            {
                try
                {
                    ht = objDB.Get_InvoiceVerificationDetl(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0], dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), txtOrderNo.Text.ToString());
                    dtHead = (DataTable)ht["InvVerificationHead"];
                    dtDetl = (DataTable)ht["InvVerificationDetl"];

                    if (dtHead.Rows.Count > 0)
                    {
                        txtSrEcode.Text = dtHead.Rows[0]["SREcode"].ToString();
                        txtSRName.Text = dtHead.Rows[0]["SRName"].ToString(); ;
                        txtVerifiedBy.Text = dtHead.Rows[0]["AoEcode"].ToString();
                        txtVerifiedName.Text = dtHead.Rows[0]["AoName"].ToString();
                        txtGLEcode.Text = dtHead.Rows[0]["GroupEcode"].ToString();
                        txtGLName.Text = dtHead.Rows[0]["GLName"].ToString();
                        txtCampName.Text = dtHead.Rows[0]["CampName"].ToString();

                        if (dtDetl.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDetl.Rows.Count; i++)
                            {
                                gvInvVerificationDetl.Rows.Add();
                                gvInvVerificationDetl.Rows[i].Cells["SlNO"].Value = (i + 1).ToString();
                                gvInvVerificationDetl.Rows[i].Cells["Observation_Code"].Value = dtDetl.Rows[i]["Observ_Code"].ToString();
                                gvInvVerificationDetl.Rows[i].Cells["Observation_Desc"].Value = dtDetl.Rows[i]["Oberv_Desc"].ToString();
                                gvInvVerificationDetl.Rows[i].Cells["Remarks"].Value = dtDetl.Rows[i]["Remarks"].ToString();
                            }
                        }
                    }
                    else
                    {
                        txtSrEcode.Text = "";
                        txtSRName.Text = "";
                        //txtVerifiedBy.Text = "";
                        //txtVerifiedName.Text = "";
                        txtGLEcode.Text = "";
                        txtGLName.Text = "";
                        txtCampName.Text = "";
                        gvInvVerificationDetl.Rows.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void txtOrderNo_Validated(object sender, EventArgs e)
        {
            GetInvoiceVerificationDetails();
        }

        private void btnAddVerificationDetl_Click(object sender, EventArgs e)
        {
            int intRow = 1;
            bool IsItemExisted = false;

            if (cbObservType.SelectedIndex > 0)
            {
                DataGridViewRow tempRow = new DataGridViewRow();

                if (bFlag == true)
                {
                    gvInvVerificationDetl.Rows.Remove(RowIndex);
                }

                if (gvInvVerificationDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvInvVerificationDetl.Rows.Count; i++)
                    {
                        if (gvInvVerificationDetl.Rows[i].Cells["Observation_Code"].Value.ToString().Equals(cbObservType.SelectedValue.ToString()))
                        {
                            IsItemExisted = true;
                        }
                    }
                }
                if (IsItemExisted == false)
                {

                    DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                    if (gvInvVerificationDetl.Rows.Count > 0)
                        cellSlNo.Value = gvInvVerificationDetl.Rows.Count + 1;
                    else
                        cellSlNo.Value = intRow;
                    tempRow.Cells.Add(cellSlNo);

                    DataGridViewCell cellObsrvCode = new DataGridViewTextBoxCell();
                    cellObsrvCode.Value = cbObservType.SelectedValue.ToString();
                    tempRow.Cells.Add(cellObsrvCode);

                    DataGridViewCell cellObsrvType = new DataGridViewTextBoxCell();
                    cellObsrvType.Value = cbObservType.Text.ToString();
                    tempRow.Cells.Add(cellObsrvType);

                    DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                    cellRemarks.Value = txtRemarks.Text.ToString().Replace("\'", "");
                    tempRow.Cells.Add(cellRemarks);

                    gvInvVerificationDetl.Rows.Add(tempRow);
                                                                         
                    
                }

                bFlag = false;
                txtRemarks.Text = "";
                cbObservType.SelectedIndex = 0;
                cbObservType.Focus();                  
            }
            else
            {
                MessageBox.Show("Please Select Observation Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbObservType.Focus();
            }
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOrderNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOrderNo.Text.Length == 0)
            {
                txtSrEcode.Text = "";
                txtSRName.Text = "";
                //txtVerifiedBy.Text = "";
                //txtVerifiedName.Text = "";
                txtGLEcode.Text = "";
                txtGLName.Text = "";
                txtCampName.Text = "";
                gvInvVerificationDetl.Rows.Clear();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtSrEcode.Text = "";
            txtSRName.Text = "";
            //txtVerifiedBy.Text = "";
            //txtVerifiedName.Text = "";
            txtGLEcode.Text = "";
            txtGLName.Text = "";
            txtCampName.Text = "";
            gvInvVerificationDetl.Rows.Clear();
            dtpTrnDate.Value = DateTime.Today;
            txtOrderNo.Text = "";
            cbObservType.SelectedIndex = 0;
            txtRemarks.Text = "";
            txtOrderNo.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvInvVerificationDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvInvVerificationDetl.Columns["Edit"].Index)
                {
                    if (Convert.ToBoolean(gvInvVerificationDetl.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        bFlag = true;
                        RowIndex = gvInvVerificationDetl.Rows[e.RowIndex];                        
                        cbObservType.SelectedValue = gvInvVerificationDetl.Rows[e.RowIndex].Cells["Observation_Code"].Value.ToString();
                        txtRemarks.Text = gvInvVerificationDetl.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();
                        txtRemarks.Focus();
                    }
                }


                if (e.ColumnIndex == gvInvVerificationDetl.Columns["Del"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvInvVerificationDetl.Rows[e.RowIndex];
                        gvInvVerificationDetl.Rows.Remove(dgvr);
                    }
                    if (gvInvVerificationDetl.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvInvVerificationDetl.Rows.Count; i++)
                        {
                            gvInvVerificationDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        }
                    }
                }
            }
            
        }

        private void txtVerifiedBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtVerifiedBy_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtVerifiedBy.Text.Length < 3)
            {
                txtVerifiedName.Text = "";
            }
        }

      
    }
}
