using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class frmSpecialApprovals : Form
    {
        SQLDB objSQLdb = null;
        private Security objSecurity = null;
        InvoiceDB objInvdb = null;
        private bool flagUpdate = false;

        public frmSpecialApprovals()
        {
            InitializeComponent();
        }

        private void frmSpecialApprovals_Load(object sender, EventArgs e)
        {
            txtTrnNo.Text = GenerateTrnNo().ToString();
            dtpApprDate.Value = DateTime.Today;
            dtpTrnDate.Value = DateTime.Today;
            cbApprType.SelectedIndex = 0;
        }

        private void btnAddDocDetails_Click(object sender, EventArgs e)
        {
            frmAddDocumentDetails DocDetails = new frmAddDocumentDetails("SPECIAL_APPROVALS");
            DocDetails.objfrmSpecialApprovals = this;
            DocDetails.ShowDialog();
        }

        private string GenerateTrnNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string sTrnNo = "";
            try
            {
                string strCmd = "SELECT ISNULL(MAX(SAP_TRN_NO),0)+1 TrnNo FROM SPECIAL_APPROVALS_HEAD "+
                                " WHERE SAP_COMP_CODE='"+ CommonData.CompanyCode +
                                "' and SAP_BRANCH_CODE='" + CommonData.BranchCode + "'";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sTrnNo = dt.Rows[0]["TrnNo"].ToString();
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

            return sTrnNo;

        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtTrnNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Contact IT Department","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtTrnNo.Focus();
                return flag;
            }
            if (dtpTrnDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Trn Date Is Less Than Or Equal to ToDay's Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpTrnDate.Focus();
                return flag;
            }
            if (txtReqEmpName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Requested Employee Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReqbyEcode.Focus();
                return flag;
            }
            if (txtPurpose.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Purpose", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPurpose.Focus();
                return flag;
            }
            if (txtNoOfReq.Text.Length == 0 || txtNoOfReq.Text.Equals("0"))
            {
                flag = false;
                MessageBox.Show("Please Enter No.Of Requests Value", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoOfReq.Focus();
                return flag;
            }
            if (txtReqAmt.Text.Length == 0 || txtReqAmt.Text.Equals("0"))
            {
                flag = false;
                MessageBox.Show("Please Enter Request Amount Value", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReqAmt.Focus();
                return flag;
            }
            if (txtApprAmt.Text.Length == 0 || txtApprAmt.Text.Equals("0"))
            {
                flag = false;
                MessageBox.Show("Please Enter Approved Amount Value", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApprAmt.Focus();
                return flag;
            }

            if (dtpApprDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Approved Date Is Less Than Or Equal to ToDay's Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpTrnDate.Focus();
                return flag;
            }
            if (cbApprType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Approval Method Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbApprType.Focus();
                return flag;
            }
            if (txtApprEmpName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Approved Employee Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApprEcode.Focus();
                return flag;
            }
            if (rtbDescription.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Description", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbDescription.Focus();
                return flag;
            }
            if (txtRemarks.Text.Length == 0 || txtRemarks.Text.Length < 10)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRemarks.Focus();
                return flag;
            }
            if (cbApprType.SelectedIndex == 2)
            {
                if (gvDocumentDetl.Rows.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Attach Document Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAddDocDetails.Focus();
                    return flag;
                }
            }

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                try
                {
                    if (SaveApprovalHead() > 0)
                    {
                        SaveApprovalDetail();
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        #region "INSERT AND UPDATE DATA"

        private int SaveApprovalHead()
        {
            string strCmd = "";
            int iRes = 0;
            objSQLdb = new SQLDB();

            try
            {
                if (flagUpdate == true)
                {
                    strCmd = "UPDATE SPECIAL_APPROVALS_HEAD SET SAP_COMP_CODE='"+ CommonData.CompanyCode +
                             "', SAP_BRANCH_CODE='"+ CommonData.BranchCode +"', SAP_DOC_MONTH='"+ CommonData.DocMonth +
                             "', SAP_FIN_YEAR='"+ CommonData.FinancialYear +
                             "', SAP_TRN_DATE='"+ Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                             "', SAP_EORA_CODE="+ Convert.ToInt32(txtReqbyEcode.Text) +
                             ",  SAP_PURPOUSE ='"+ txtPurpose.Text.ToString().Replace("'","") +
                             "', SAP_NO_OF_REQUESTS="+ Convert.ToInt32(txtNoOfReq.Text) +
                             ",  SAP_REQ_AMT ="+ Convert.ToDouble(txtReqAmt.Text) +
                             ",  SAP_APPR_AMT="+ Convert.ToDouble(txtApprAmt.Text) +
                             ",  SAP_APPROVED_DATE='"+ Convert.ToDateTime(dtpApprDate.Value).ToString("dd/MMM/yyyy") +
                             "', SAP_APPROVED_BY="+ Convert.ToInt32(txtApprEcode.Text) +
                             ",  SAP_APPROVAL_METHOD ='"+ cbApprType.Text.ToString() +
                             "', SAP_TRN_DESC ='"+ rtbDescription.Text.ToString().Replace("'"," ") +
                             "', SAP_REMARKS='"+ txtRemarks.Text.ToString().Replace("'"," ") +
                             "', SAP_MODIFIED_BY='"+ CommonData.LogUserId +
                             "', SAP_MODIFIED_DATE=getdate() "+
                             "   WHERE SAP_BRANCH_CODE='" + CommonData.BranchCode +
                             "'  AND SAP_TRN_NO="+ txtTrnNo.Text +"";
                }
                else
                {
                    txtTrnNo.Text = GenerateTrnNo().ToString();
                    objSQLdb = new SQLDB();

                    strCmd = "INSERT INTO SPECIAL_APPROVALS_HEAD(SAP_COMP_CODE " +
                                                              ", SAP_BRANCH_CODE " +
                                                              ", SAP_DOC_MONTH " +
                                                              ", SAP_FIN_YEAR " +
                                                              ", SAP_TRN_NO " +
                                                              ", SAP_TRN_DATE " +
                                                              ", SAP_EORA_CODE " +
                                                              ", SAP_PURPOUSE " +
                                                              ", SAP_NO_OF_REQUESTS " +
                                                              ", SAP_REQ_AMT " +
                                                              ", SAP_APPR_AMT " +
                                                              ", SAP_APPROVED_DATE " +
                                                              ", SAP_APPROVED_BY " +
                                                              ", SAP_APPROVAL_METHOD " +
                                                              ", SAP_CREATED_BY " +
                                                              ", SAP_CREATED_DATE " +
                                                              ", SAP_TRN_DESC " +
                                                              ", SAP_REMARKS " +
                                                              ")VALUES " +
                                                              "('" + CommonData.CompanyCode +
                                                              "','" + CommonData.BranchCode +
                                                              "','" + CommonData.DocMonth +
                                                              "','" + CommonData.FinancialYear +
                                                              "'," + Convert.ToInt32(txtTrnNo.Text) +
                                                              ",'" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                              "'," + Convert.ToInt32(txtReqbyEcode.Text) +
                                                              ",'" + txtPurpose.Text.ToString().Replace("'", " ") +
                                                              "'," + Convert.ToInt32(txtNoOfReq.Text) +
                                                              "," + Convert.ToInt32(txtReqAmt.Text) +
                                                              "," + Convert.ToInt32(txtApprAmt.Text) +
                                                              ",'" + Convert.ToDateTime(dtpApprDate.Value).ToString("dd/MMM/yyyy") +
                                                              "'," + Convert.ToInt32(txtApprEcode.Text) +
                                                              ",'" + cbApprType.Text.ToString() +
                                                              "','" + CommonData.LogUserId +
                                                              "',getdate() " +
                                                              ",'" + rtbDescription.Text.ToString().Replace("\'", " ") +
                                                              "','" + txtRemarks.Text.ToString().Replace("'", " ") + "')";
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

        private int SaveApprovalDetail()
        {
            string strCmd = "";
            objSQLdb = new SQLDB();
            int iRes = 0;
            
            try
            {
                
                byte[] arr;
                ImageConverter converter = new ImageConverter();

                strCmd = "DELETE FROM SPECIAL_APPROVAL_DOC_DETL "+
                        " WHERE SAPD_COMP_CODE='"+ CommonData.CompanyCode +
                        "' and SAPD_BRANCH_CODE='"+ CommonData.BranchCode +
                        "' and SAPD_TRN_NO="+ Convert.ToInt32(txtTrnNo.Text) +" ";
                iRes = objSQLdb.ExecuteSaveData(strCmd);
                strCmd = "";
                iRes = 0;

                if (gvDocumentDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDocumentDetl.Rows.Count; i++)
                    {
                       arr = (byte[])gvDocumentDetl.Rows[i].Cells["DocImage"].Value;

                        strCmd = "INSERT INTO SPECIAL_APPROVAL_DOC_DETL(SAPD_COMP_CODE " +
                                                                     ", SAPD_BRANCH_CODE " +
                                                                     ", SAPD_TRN_NO " +
                                                                     ", SAPD_SL_NO " +
                                                                     ", SAPD_DOC_NAME " +
                                                                     ", SAPD_DOC_DESC " +
                                                                     ", SAPD_DOC_IMG " +
                                                                     ", SAPD_CREATED_BY " +
                                                                     ", SAPD_CREATED_DATE " +
                                                                     ")values " +
                                                                     "('" + CommonData.CompanyCode +
                                                                     "','" + CommonData.BranchCode +
                                                                     "'," + Convert.ToInt32(txtTrnNo.Text) +
                                                                     "," + Convert.ToInt32(gvDocumentDetl.Rows[i].Cells["SLNO"].Value) +
                                                                     ",'" + gvDocumentDetl.Rows[i].Cells["DocumentName"].Value.ToString() +
                                                                     "','" + gvDocumentDetl.Rows[i].Cells["DocumentDesc"].Value.ToString() +
                                                                     "',@Image,'" + CommonData.LogUserId +
                                                                     "',getdate())";
                   


                        string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                        objSecurity = new Security();
                        SqlConnection Con = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                        SqlCommand  SqlCom = new SqlCommand(strCmd, Con);

                        SqlCom.Parameters.Add(new SqlParameter("@Image", (object)arr));
                        Con.Open();
                        iRes = SqlCom.ExecuteNonQuery();
                        Con.Close();
                        arr = null;
                       strCmd = "";
                    }
                }
               
                //if (strCmd.Length > 10)
                //{
                //    objSQLdb.ExecuteSaveData(strCmd);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;

        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtTrnNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtReqbyEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

      
        private void txtNoOfReq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtReqAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

     

        private void txtApprAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtApprEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void GetReqEmployeeName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            if (txtReqbyEcode.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+desig_name+')' EName FROM EORA_MASTER " +
                                    " INNER JOIN DESIG_MAS ON desig_code=DESG_ID " +
                                    " WHERE ECODE=" + txtReqbyEcode.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtReqEmpName.Text = dt.Rows[0]["EName"].ToString();
                    }
                    else
                    {
                        txtReqEmpName.Text = "";
                        MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReqbyEcode.Focus();
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

        private void GetApprovedEmployeeName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtApprEcode.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+desig_name+')' EName FROM EORA_MASTER "+
                                    " INNER JOIN DESIG_MAS ON desig_code=DESG_ID " +
                                    " WHERE ECODE=" + txtApprEcode.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtApprEmpName.Text = dt.Rows[0]["EName"].ToString();
                    }
                    else
                    {
                        txtApprEmpName.Text = "";
                        MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtApprEcode.Focus();
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

        private void txtReqbyEcode_Validated(object sender, EventArgs e)
        {
            GetReqEmployeeName();
        }

        private void txtApprEcode_Validated(object sender, EventArgs e)
        {
            GetApprovedEmployeeName();
        }

        private void gvDocumentDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] Arr;
            if (e.ColumnIndex == gvDocumentDetl.Columns["ImgView"].Index)
            {
                Arr = null;
                Arr = (byte[])gvDocumentDetl.Rows[e.RowIndex].Cells["DocImage"].Value;
                frmDisplayImage ImgView = new frmDisplayImage(Arr);
                ImgView.objfrmSpecialApprovals = this;
                ImgView.ShowDialog();
            }
           
            if (e.ColumnIndex == gvDocumentDetl.Columns["Del"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvDocumentDetl.Rows[e.RowIndex];
                    gvDocumentDetl.Rows.Remove(dgvr);
                }
                if (gvDocumentDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDocumentDetl.Rows.Count; i++)
                    {
                        gvDocumentDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtTrnNo.Text = GenerateTrnNo().ToString();
            dtpTrnDate.Value = DateTime.Today;
            dtpApprDate.Value = DateTime.Today;
            txtApprEcode.Text = "";
            txtApprEmpName.Text = "";
            txtReqbyEcode.Text = "";
            txtReqEmpName.Text = "";
            txtNoOfReq.Text = "";
            txtReqAmt.Text = "";
            txtApprAmt.Text = "";
            cbApprType.SelectedIndex = 0;
            txtPurpose.Text = "";
            txtRemarks.Text = "";
            rtbDescription.Text = "";
            gvDocumentDetl.Rows.Clear();
          
        }

        private void GetSpecialApprovalDetails()
        {
            objInvdb = new InvoiceDB();
            Hashtable ht;

            DataTable dtApprovalHead;
            DataTable dtApprovalDetl;

            if (txtTrnNo.Text.Length > 0)
            {
                try
                {

                    ht = objInvdb.Get_SpecialApprovalDetails(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToInt32(txtTrnNo.Text));
                    dtApprovalHead = (DataTable)ht["SPApprovalHead"];
                    dtApprovalDetl = (DataTable)ht["SPApprovalDetail"];

                    FillSPApprovalHead(dtApprovalHead, dtApprovalDetl);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    txtTrnNo.Text = GenerateTrnNo().ToString();
                }
                finally
                {
                    objInvdb = null;
                    ht = null;
                }
            }
            else
            {
                txtTrnNo.Text = GenerateTrnNo().ToString();
                dtpTrnDate.Value = DateTime.Today;
                dtpApprDate.Value = DateTime.Today;
                txtApprEcode.Text = "";
                txtApprEmpName.Text = "";
                txtReqbyEcode.Text = "";
                txtReqEmpName.Text = "";
                txtNoOfReq.Text = "";
                txtReqAmt.Text = "";
                txtApprAmt.Text = "";
                cbApprType.SelectedIndex = 0;
                txtPurpose.Text = "";
                txtRemarks.Text = "";
                rtbDescription.Text = "";
                gvDocumentDetl.Rows.Clear();
            }

        }

        private void FillSPApprovalHead(DataTable dtHead,DataTable dtDetl)
        {
            gvDocumentDetl.Rows.Clear();

            try
            {
                if (dtHead.Rows.Count > 0)
                {
                    if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dtHead.Rows[0]["Createddate"]).ToString("dd/MM/yyyy"))).TotalDays > 5)
                    {
                        if (CommonData.LogUserId.ToUpper() != "ADMIN")
                        {
                            btnSave.Enabled = false;
                            btnDelete.Enabled = false;
                            btnCancel.Enabled = false;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnDelete.Enabled = true;
                            btnCancel.Enabled = true;
                        }
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;
                        btnCancel.Enabled = true;
                    }

                        flagUpdate = true;

                        dtpTrnDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"].ToString());
                        txtReqbyEcode.Text = dtHead.Rows[0]["RequestedEcode"].ToString();
                        txtReqEmpName.Text = dtHead.Rows[0]["ReqEmpName"].ToString();
                        txtPurpose.Text = dtHead.Rows[0]["Purpose"].ToString();
                        txtNoOfReq.Text = dtHead.Rows[0]["NoOfReqs"].ToString();
                        txtApprAmt.Text = dtHead.Rows[0]["ApprAmt"].ToString();
                        txtReqAmt.Text = dtHead.Rows[0]["ReqAmt"].ToString();
                        dtpApprDate.Value = Convert.ToDateTime(dtHead.Rows[0]["ApprDate"].ToString());
                        txtApprEcode.Text = dtHead.Rows[0]["ApprEcode"].ToString();
                        txtApprEmpName.Text = dtHead.Rows[0]["ApprEmpName"].ToString();
                        txtRemarks.Text = dtHead.Rows[0]["Remarks"].ToString();
                        rtbDescription.Text = dtHead.Rows[0]["Description"].ToString();
                        cbApprType.Text = dtHead.Rows[0]["ApprovalMethod"].ToString();

                        if (dtDetl.Rows.Count > 0)
                        {
                            FillSPApprovalDetail(dtDetl);
                        }                    
                }
                else
                {
                    flagUpdate = false;
                    txtTrnNo.Text = GenerateTrnNo().ToString();
                    dtpTrnDate.Value = DateTime.Today;
                    dtpApprDate.Value = DateTime.Today;
                    txtApprEcode.Text = "";
                    txtApprEmpName.Text = "";
                    txtReqbyEcode.Text = "";
                    txtReqEmpName.Text = "";
                    txtNoOfReq.Text = "";
                    txtReqAmt.Text = "";
                    txtApprAmt.Text = "";
                    cbApprType.SelectedIndex = 0;
                    txtPurpose.Text = "";
                    txtRemarks.Text = "";
                    rtbDescription.Text = "";
                    gvDocumentDetl.Rows.Clear();
                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());                
            }
            
        }
        private void FillSPApprovalDetail(DataTable dt)
        {
            gvDocumentDetl.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvDocumentDetl.Rows.Add();
                    gvDocumentDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                    gvDocumentDetl.Rows[i].Cells["DocumentName"].Value = dt.Rows[i]["DocumentName"].ToString();
                    gvDocumentDetl.Rows[i].Cells["DocumentDesc"].Value = dt.Rows[i]["DocDesc"].ToString();
                    gvDocumentDetl.Rows[i].Cells["DocImage"].Value = dt.Rows[i]["DocImage"];

                }
            }
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {

            if (txtTrnNo.Text.Length > 0)
            {
                GetSpecialApprovalDetails();
            }
            else
            {
                flagUpdate = false;
                txtTrnNo.Text = GenerateTrnNo().ToString();
                dtpTrnDate.Value = DateTime.Today;
                dtpApprDate.Value = DateTime.Today;
                txtApprEcode.Text = "";
                txtApprEmpName.Text = "";
                txtReqbyEcode.Text = "";
                txtReqEmpName.Text = "";
                txtNoOfReq.Text = "";
                txtReqAmt.Text = "";
                txtApprAmt.Text = "";
                cbApprType.SelectedIndex = 0;
                txtPurpose.Text = "";
                txtRemarks.Text = "";
                rtbDescription.Text = "";
                gvDocumentDetl.Rows.Clear();
               
            }
           
        }

        private void txtPurpose_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            if (txtTrnNo.Text.Length > 0 && flagUpdate == true)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        strCmd = "DELETE FROM SPECIAL_APPROVAL_DOC_DETL WHERE SAPD_COMP_CODE='" + CommonData.CompanyCode +
                                 "' and SAPD_BRANCH_CODE='" + CommonData.BranchCode +
                                 "' and SAPD_TRN_NO=" + Convert.ToInt32(txtTrnNo.Text) + " ";
                        strCmd += "DELETE FROM SPECIAL_APPROVALS_HEAD WHERE SAP_COMP_CODE='" + CommonData.CompanyCode +
                                   "' and SAP_BRANCH_CODE='" + CommonData.BranchCode +
                                   "' and SAP_TRN_NO=" + Convert.ToInt32(txtTrnNo.Text) + "";
                        if (strCmd.Length > 10)
                        {
                            iRes = objSQLdb.ExecuteSaveData(strCmd);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                    }
                }
            }
        }

     
    }
}
