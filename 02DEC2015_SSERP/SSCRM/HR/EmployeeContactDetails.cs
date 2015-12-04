using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
using System.IO;
namespace SSCRM
{
    public partial class EmployeeContactDetails : Form
    {
        SQLDB objDb = null;
        bool updateFlag = false;
        string strmemberName = "";
        string strRelation = "";
        string strDob = "";
        string strresiding = "";
        string strDepending = "";
        string strOccupation = "";
        bool flageValue = false;


        public EmployeeContactDetails()
        {
            InitializeComponent();
        }



        private void EmployeeContactDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();

            dgvFamilyDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);



        }
        private void FillCompanyData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS where active='T'";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
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
                objDb = null;
                dt = null;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();
        }
        private void FillBranchData()
        {
            if (cbCompany.SelectedIndex > 0)
            {
                objDb = new SQLDB();
                DataTable dt = new DataTable();
                cbLocation.DataSource = null;
                try
                {
                    if (cbCompany.SelectedIndex > 0)
                    {
                        string strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' and COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' ORDER BY BRANCH_NAME ASC ";
                        dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";
                        dr[1] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);
                        cbLocation.DataSource = dt;
                        cbLocation.DisplayMember = "BRANCH_NAME";
                        cbLocation.ValueMember = "branchCode";
                        //cbLocation.ValueMember = "LOCATION";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                    dt = null;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDsearch_Validated(object sender, EventArgs e)
        {
            if (txtDsearch.Text.Length > 4)
            {
                objDb = new SQLDB();
                DataTable dt = new DataTable();
                DataSet dsPhoto = new DataSet();
                string strCmd = "SELECT ECODE,MEMBER_NAME,desig_name,dept_name,ISNULL(HAMH_APPL_NUMBER, 0) APPLNO,COMPANY_CODE,BRANCH_CODE " +
                                         "FROM EORA_MASTER EM INNER JOIN Dept_Mas ON dept_code= DEPT_ID " +
                                        "INNER JOIN DESIG_MAS ON DESG_ID=desig_code " +
                                        "INNER JOIN HR_APPL_MASTER_HEAD ON ECODE=HAMH_EORA_CODE " +
                                         "WHERE ECODE=" + Convert.ToInt32(txtDsearch.Text);
                try
                {
                    dt = objDb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        txtApplNo.Text = dt.Rows[0]["APPLNO"] + "";
                        txtMemberName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                        txtDeptName.Text = dt.Rows[0]["dept_name"] + "";
                        txtDesgName.Text = dt.Rows[0]["desig_name"] + "";
                        cbCompany.SelectedValue = dt.Rows[0]["COMPANY_CODE"] + "";
                        cbLocation.SelectedValue = dt.Rows[0]["BRANCH_CODE"] + "";
                        strCmd = "";

                        strCmd = "SELECT HECD_EMP_DOB,HECD_EMP_DOM,HECD_EMP_MOBILE_NO,HECD_EXT_NO" +
                            ",HECD_EMP_EMAIL_ID FROM HR_EMP_CONTACT_DETL WHERE HECD_EORA_CODE=" + Convert.ToInt32(txtDsearch.Text);
                        objDb = new SQLDB();
                        dt = objDb.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            updateFlag = true;
                            txtMobileNo.Text = dt.Rows[0]["HECD_EMP_MOBILE_NO"] + "";
                            txtExtensionNo.Text = dt.Rows[0]["HECD_EXT_NO"] + "";
                            txtEmail.Text = dt.Rows[0]["HECD_EMP_EMAIL_ID"] + "";
                            if (dt.Rows[0]["HECD_EMP_DOB"].ToString() != "")
                            {
                                dtpDOB.Value = Convert.ToDateTime(dt.Rows[0]["HECD_EMP_DOB"] + "");
                            }
                            else
                            {
                                DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
                                dtpDOB.Value = dtimeDefault;
                            }
                            if (dt.Rows[0]["HECD_EMP_DOM"].ToString() != "")
                            {
                                dtpDOM.Value = Convert.ToDateTime(dt.Rows[0]["HECD_EMP_DOM"] + "");
                            }
                            else
                            {
                                DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
                                dtpDOM.Value = dtimeDefault;
                            }


                        }
                        else
                        {
                            txtMobileNo.Text = "";
                            txtEmail.Text = "";
                            DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
                            dtpDOB.Value = dtimeDefault;
                            dtpDOM.Value = dtimeDefault;
                        }
                        //if (dt.Rows[0]["HECD_EMP_DOB"].ToString() != "")
                        //{
                        //    dtpDOB.Value = Convert.ToDateTime(dt.Rows[0]["HECD_EMP_DOB"] + "");
                        //}
                        //else
                        //{
                        //    DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
                        //    dtpDOB.Value = dtimeDefault;
                        //}
                        //if (dt.Rows[0]["HECD_EMP_DOM"].ToString() != "")
                        //{
                        //    dtpDOM.Value = Convert.ToDateTime(dt.Rows[0]["HECD_EMP_DOM"] + "");
                        //}
                        //else
                        //{
                        //    DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
                        //    dtpDOM.Value = dtimeDefault;
                        //}

                        dsPhoto = objDb.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + txtDsearch.Text);

                        if (dsPhoto.Tables[0].Rows.Count > 0)
                            GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                        else
                            picEmpPhoto.BackgroundImage = null;

                        FillFamilyDetailsToGrid();
                    }
                    else
                        updateFlag = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                    dt = null;
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
                picEmpPhoto.BackgroundImage = newImage;
                this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private int SaveFamilyMembersDetails()
        {
            int iVal = 0;
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                if (dgvFamilyDetails.Rows.Count > 0)
                {

                    strCommand = " DELETE FROM HR_APPL_FAMILY_DETL WHERE HAFD_APPL_NUMBER = " + txtApplNo.Text + " ";
                    iVal = objDb.ExecuteSaveData(strCommand);
                    strCommand = "";


                    for (int i = 0; i < dgvFamilyDetails.Rows.Count; i++)
                    {
                        strCommand += "INSERT INTO HR_APPL_FAMILY_DETL(HAFD_COMPANY_CODE" +
                                                                        ",HAFD_BRANCH_CODE" +
                                                                        ",HAFD_APPL_NUMBER" +
                                                                        ",HAFD_APPL_SL_NUMBER" +
                                                                        " ,HAFD_FAMILY_MEMBER_RELATIONSHIP" +
                                                                        ",HAFD_FAMILY_MEMBER_NAME" +
                                                                        ",HAFD_FAMILY_MEMBER_DOB" +
                                                                        ",HAFD_RESIDING_WITH_APPLICANT" +
                                                                        ",HAFD_DEPENDING_ON_APPLICANT" +
                                                                        ",HAFD_FAMILY_MEMBER_OCCUPATION " +
                                                                        ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                        "','" + cbLocation.SelectedValue.ToString() +
                                                                        "'," + Convert.ToInt32(txtApplNo.Text) +
                                                                        "," + Convert.ToInt32(dgvFamilyDetails.Rows[i].Cells["SLNO"].Value) +
                                                                        ",'" + dgvFamilyDetails.Rows[i].Cells["HAFD_FAMILY_MEMBER_RELATIONSHIP"].Value +
                                                                        "','" + dgvFamilyDetails.Rows[i].Cells["HAFD_FAMILY_MEMBER_NAME"].Value +
                                                                        "','" + Convert.ToDateTime(dgvFamilyDetails.Rows[i].Cells["HAFD_FAMILY_MEMBER_DOB"].Value).ToString("dd/MMM/yyyy") +
                                                                        "','" + dgvFamilyDetails.Rows[i].Cells["HAFD_RESIDING_WITH_APPLICANT"].Value +
                                                                        "','" + dgvFamilyDetails.Rows[i].Cells["HAFD_DEPENDING_ON_APPLICANT"].Value +
                                                                        "','" + dgvFamilyDetails.Rows[i].Cells["HAFD_FAMILY_MEMBER_OCCUPATION"].Value + "')";

                    }

                    if (strCommand.Length > 10)
                    {
                        iVal = objDb.ExecuteSaveData(strCommand);
                    }
                    if (iVal > 0)
                    {
                        return iVal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iVal;

        }




        private void btnSave_Click(object sender, EventArgs e)
        {

            if (CheckData())
            {
                objDb = new SQLDB();
                string strCmd = "";
                int iRes = 0;
                if (updateFlag == false)
                {
                    strCmd = "insert into HR_EMP_CONTACT_DETL(HECD_APPL_NO,HECD_EORA_CODE,HECD_BRANCH_CODE";
                    if (Convert.ToDateTime(dtpDOB.Value) > Convert.ToDateTime("01/JAN/1900"))
                    {
                        strCmd += ",HECD_EMP_DOB";
                    }
                    if (Convert.ToDateTime(dtpDOM.Value) > Convert.ToDateTime("01/JAN/1900"))
                    {
                        strCmd += ",HECD_EMP_DOM";
                    }
                    strCmd += ",HECD_EMP_MOBILE_NO" +
                        ",HECD_EXT_NO" +
                        ",HECD_EMP_EMAIL_ID" +
                        ",HECD_CREATED_BY,HECD_CREATED_DATE) " +
                        "values(" + Convert.ToInt32(txtApplNo.Text) +
                        "," + Convert.ToInt32(txtDsearch.Text) +
                        ",'" + cbLocation.SelectedValue + "'";
                    if (Convert.ToDateTime(dtpDOB.Value) > Convert.ToDateTime("01/JAN/1900"))
                    {
                        strCmd += ",'" + dtpDOB.Value.ToString("dd/MMM/yyyy") + "'";
                    }
                    if (Convert.ToDateTime(dtpDOM.Value) > Convert.ToDateTime("01/JAN/1900"))
                    {
                        strCmd += ",'" + dtpDOM.Value.ToString("dd/MMM/yyyy") + "'";
                    }
                    strCmd += ",'" + txtMobileNo.Text +
                        "','" + txtExtensionNo.Text +
                        "','" + txtEmail.Text +
                        "','" + CommonData.LogUserId +
                        "','" + System.DateTime.Now.ToString("dd/MMM/yyyy") + "')";
                }
                else
                {
                    strCmd = "update HR_EMP_CONTACT_DETL set HECD_APPL_NO=" + Convert.ToInt32(txtApplNo.Text) +
                                ",HECD_BRANCH_CODE='" + cbLocation.SelectedValue + "'";
                    if (Convert.ToDateTime(dtpDOB.Value) > Convert.ToDateTime("01/JAN/1900"))
                        strCmd += ",HECD_EMP_DOB='" + dtpDOB.Value.ToString("dd/MMM/yyyy") + "'";
                    else
                        strCmd += ",HECD_EMP_DOB=NULL";
                    if (Convert.ToDateTime(dtpDOM.Value) > Convert.ToDateTime("01/JAN/1900"))
                        strCmd += ",HECD_EMP_DOM='" + dtpDOM.Value.ToString("dd/MMM/yyyy") + "'";
                    else
                        strCmd += ",HECD_EMP_DOM=NULL";
                    strCmd += ",HECD_EMP_MOBILE_NO='" + txtMobileNo.Text +
                                "',HECD_EXT_NO='" + txtExtensionNo.Text +
                                "',HECD_EMP_EMAIL_ID='" + txtEmail.Text +
                                "',HECD_MODIFIED_BY='" + CommonData.LogUserId +
                                "',HECD_MODIFIED_DATE='" + System.DateTime.Now.ToString("dd/MMM/yyyy") +
                                "' WHERE HECD_EORA_CODE=" + Convert.ToInt32(txtDsearch.Text);

                }
                try
                {
                    iRes = objDb.ExecuteSaveData(strCmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                }
                if (iRes > 0)
                {
                        SaveFamilyMembersDetails();
                    
                        MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateFlag = false;
                        btnClear_Click(null, null);
                    
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }




        }
        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                cbCompany.Focus();
                return flag;
            }
            else if (cbLocation.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                cbLocation.Focus();
                return flag;
            }
            else if (txtMobileNo.Text.Length == 0 || txtMobileNo.Text.Length != 10)
            {
                MessageBox.Show("Enter Valid Mobile NO", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                txtMobileNo.Focus();
                return flag;
            }
            else if (txtDsearch.Text.Length == 0)
            {
                MessageBox.Show("Enter Ecode", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                txtDsearch.Focus();
                return flag;
            }
            else if (txtEmail.Text.Length > 0)
            {
                if (!UtilityFunctions.IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Enter Valid Email", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                    txtEmail.Focus();
                    return flag;
                }
            }
            return flag;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbLocation.SelectedIndex = -1;
            txtApplNo.Text = "";
            txtDsearch.Text = "";
            txtMemberName.Text = "";
            txtDeptName.Text = "";
            txtDesgName.Text = "";
            txtMobileNo.Text = "";
            txtExtensionNo.Text = "";
            txtEmail.Text = "";
            DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
            dtpDOB.Value = dtimeDefault;
            dtpDOM.Value = dtimeDefault;
            picEmpPhoto.BackgroundImage = null;
            dgvFamilyDetails.Rows.Clear();

            updateFlag = false;
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToLower(e.KeyChar);
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            txtDsearch_Validated(null, null);
            //FillFamilyDetailsToGrid();

        }
        private void FillFamilyDetailsToGrid()
        {
            objDb = new SQLDB();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string strCommand = "";
            int intRow = 1;
            dgvFamilyDetails.Rows.Clear();
            try
            {
                strCommand = "SELECT HAFD_FAMILY_MEMBER_NAME,HAFD_FAMILY_MEMBER_RELATIONSHIP,HAFD_FAMILY_MEMBER_DOB" +
                                                       ",HAFD_RESIDING_WITH_APPLICANT,HAFD_DEPENDING_ON_APPLICANT,HAFD_FAMILY_MEMBER_OCCUPATION " +
                                                       " FROM EORA_MASTER INNER JOIN HR_APPL_MASTER_HEAD ON  HAMH_EORA_CODE=ECODE " +
                                                       "INNER JOIN HR_APPL_FAMILY_DETL  ON HAFD_APPL_NUMBER=HAMH_APPL_NUMBER " +
                                                       "WHERE ECODE=" + Convert.ToInt32(txtDsearch.Text) + "";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow temprow = new DataGridViewRow();

                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = (intRow);

                        temprow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMembename = new DataGridViewTextBoxCell();
                        cellMembename.Value = dt.Rows[i]["HAFD_FAMILY_MEMBER_NAME"].ToString();
                        temprow.Cells.Add(cellMembename);

                        DataGridViewCell cellRelationship = new DataGridViewTextBoxCell();
                        cellRelationship.Value = dt.Rows[i]["HAFD_FAMILY_MEMBER_RELATIONSHIP"].ToString();
                        temprow.Cells.Add(cellRelationship);

                        DataGridViewCell cellDateOfBirth = new DataGridViewTextBoxCell();
                        cellDateOfBirth.Value = Convert.ToDateTime(dt.Rows[i]["HAFD_FAMILY_MEMBER_DOB"]).ToShortDateString();
                        temprow.Cells.Add(cellDateOfBirth);

                        DataGridViewCell cellResiding = new DataGridViewTextBoxCell();
                        cellResiding.Value = dt.Rows[i]["HAFD_RESIDING_WITH_APPLICANT"].ToString();
                        temprow.Cells.Add(cellResiding);

                        DataGridViewCell cellDepending = new DataGridViewTextBoxCell();
                        cellDepending.Value = dt.Rows[i]["HAFD_DEPENDING_ON_APPLICANT"].ToString();
                        temprow.Cells.Add(cellDepending);

                        DataGridViewCell cellOccupation = new DataGridViewTextBoxCell();
                        cellOccupation.Value = dt.Rows[i]["HAFD_FAMILY_MEMBER_OCCUPATION"].ToString();
                        temprow.Cells.Add(cellOccupation);

                        intRow = intRow + 1;
                        dgvFamilyDetails.Rows.Add(temprow);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                objDb = null;
                ds = null;
                dt = null;
            }

        }





        private void dgvFamilyDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == dgvFamilyDetails.Columns["Edit"].Index)
                {

                    DataGridViewRow dgvr = dgvFamilyDetails.Rows[e.RowIndex];
                    string memberName = dgvFamilyDetails.Rows[e.RowIndex].Cells["HAFD_FAMILY_MEMBER_NAME"].Value.ToString();
                    string relatiom = dgvFamilyDetails.Rows[e.RowIndex].Cells["HAFD_FAMILY_MEMBER_RELATIONSHIP"].Value.ToString();
                    DateTime dob = Convert.ToDateTime(dgvFamilyDetails.Rows[e.RowIndex].Cells["HAFD_FAMILY_MEMBER_DOB"].Value);
                    string residing = dgvFamilyDetails.Rows[e.RowIndex].Cells["HAFD_RESIDING_WITH_APPLICANT"].Value.ToString();
                    string depending = dgvFamilyDetails.Rows[e.RowIndex].Cells["HAFD_DEPENDING_ON_APPLICANT"].Value.ToString();
                    string occupation = dgvFamilyDetails.Rows[e.RowIndex].Cells["HAFD_FAMILY_MEMBER_OCCUPATION"].Value.ToString();




                    frmFamily familyDetils = new frmFamily("EmployeeContactDetails", memberName, relatiom, dob, residing, depending, occupation, dgvr);
                    familyDetils.objEmpContactDetails = this;
                    familyDetils.ShowDialog();


                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmFamily familyDetils = new frmFamily("EmployeeContactDetails");
            familyDetils.objEmpContactDetails = this;
            familyDetils.ShowDialog();
        }

       
    }
}