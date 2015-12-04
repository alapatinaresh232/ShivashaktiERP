using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using SSCRMDB;
using SSTrans;
using SSAdmin;
using SSCRM.App_Code;


namespace SSCRM
{
    public partial class frmEmpAwardsEntry : Form
    {
        SQLDB objSQLdb = null;
        Security objSecurity = null;
        
        Int32 nTrnId = 0;
        bool flagUpdate = false;
        string strPhoto = "";


        public frmEmpAwardsEntry()
        {
            InitializeComponent();
        }

        private void frmEmpAwardsEntry_Load(object sender, EventArgs e)
        {
            txtEventName.Visible = false;
            chkAddEvent.Checked = false;
            btnAdd.Visible = false;

            dtpAwardDate.Value = DateTime.Today;
            FillFinYear();
            FillCompanyData();
            cbFinYear.SelectedValue = CommonData.FinancialYear;
            FillEventDetails();


            try
            {
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet("SELECT DISTINCT HEA_AWARD_TYPE "+
                                                       " FROM HR_APPL_EMP_AWARDS_REWARDS "+
                                                       " ORDER BY HEA_AWARD_TYPE asc").Tables[0];
                UtilityLibrary.AutoCompleteTextBox(txtAwardType, dt, "", "HEA_AWARD_TYPE");
                objSQLdb = null;
            }
            catch { }
            finally { objSQLdb = null; }

            try
            {
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet("SELECT DISTINCT HEA_AWARD_NAME " +
                                                       " FROM HR_APPL_EMP_AWARDS_REWARDS " +
                                                       " ORDER BY HEA_AWARD_NAME asc").Tables[0];
                UtilityLibrary.AutoCompleteTextBox(txtAwardName, dt, "", "HEA_AWARD_NAME");
                objSQLdb = null;
            }
            catch { }
            finally { objSQLdb = null; } 
        }

        private void FillFinYear()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                string strCmd = "SELECT DISTINCT FY_FIN_YEAR  FROM FIN_YEAR";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbFinYear.DataSource = dt;
                    cbFinYear.DisplayMember = "FY_FIN_YEAR";
                    cbFinYear.ValueMember = "FY_FIN_YEAR";
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
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
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
                   
        private void FillZonesList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";

            if (cbCompany.SelectedIndex > 0)
            {
                try
                {
                    strCommand = "SELECT DISTINCT ABM_STATE Zone FROM AUDIT_BRANCH_MAS " +
                                 " WHERE ABM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                 "' union " +
                                 " SELECT DISTINCT STATE_CODE Zone FROM BRANCH_MAS " +
                                 " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

                        cbZones.DataSource = dt;
                        cbZones.DisplayMember = "Zone";
                        cbZones.ValueMember = "Zone";
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
                cbZones.DataSource = null;
            }
        }

        private void FillRegionsList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";

            if (cbZones.SelectedIndex > 0)
            {
                try
                {
                    strCommand = "SELECT DISTINCT ABM_REGION Region FROM AUDIT_BRANCH_MAS " +
                                " WHERE ABM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                "' and ABM_STATE='" + cbZones.SelectedValue.ToString() + 
                                "' union SELECT DISTINCT DISTRICT Region FROM BRANCH_MAS " +
                                " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                "'and STATE_CODE='" + cbZones.SelectedValue.ToString() +
                                "' and DISTRICT is not null and DISTRICT!=''";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

                        cbRegion.DataSource = dt;
                        cbRegion.DisplayMember = "Region";
                        cbRegion.ValueMember = "Region";
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
                cbRegion.DataSource = null;
            }
        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbCompany.SelectedIndex > 0 && cbZones.SelectedIndex > 0 && cbRegion.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE as branchCode " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " INNER JOIN AUDIT_BRANCH_MAS ON ABM_BRANCH_CODE=UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' AND ABM_STATE='" + cbZones.SelectedValue.ToString() +
                                        "' AND ABM_REGION='" + cbRegion.SelectedValue.ToString() +
                                        "' union " +
                                        " SELECT DISTINCT BRANCH_NAME,BRANCH_CODE as branchCode " +
                                        " FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "'and STATE_CODE='" + cbZones.SelectedValue.ToString() +
                                        "'and DISTRICT='" + cbRegion.SelectedValue.ToString() +
                                        "'AND BRANCH_CODE NOT IN (SELECT ABM_BRANCH_CODE FROM AUDIT_BRANCH_MAS)" +
                                        " ORDER BY BRANCH_NAME ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillZonesList();
            }
            else
            {
                cbZones.DataSource = null;
                cbRegion.DataSource = null;
                cbLocation.DataSource = null;
            }
        }

        private void cbZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbZones.SelectedIndex > 0)
            {
                FillRegionsList();
            }
            else
            {
                cbRegion.DataSource = null;
                cbLocation.DataSource = null;
            }
        }

        private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbZones.SelectedIndex > 0 && cbRegion.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else
            {
                cbLocation.DataSource = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

      
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pbDocPic.Image = null;
            pbDocPic.Height = 280;
            pbDocPic.Width = 430;
            splitContainer1.Panel2.AutoScroll = false;
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            flagUpdate = false;
           
            if (txtEcodeSearch.Text.Length > 3)
            {
                try
                {
                    strCommand = "SELECT MEMBER_NAME,DESIG,company_code+'@'+BRANCH_CODE AS Val " +
                                        " FROM EORA_MASTER WHERE ECODE= " + Convert.ToInt32(txtEcodeSearch.Text) + "  ";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtEmpDesg.Text = dt.Rows[0]["DESIG"].ToString();
                        FillEmpAwardDetails();
                    }
                    else
                    {
                        txtEName.Text = "";
                        txtEmpDesg.Text = "";
                        gvEmpAwardDetl.Rows.Clear();
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
                txtEName.Text = "";
                txtEmpDesg.Text = "";
                gvEmpAwardDetl.Rows.Clear();
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.AutoScroll = true;
            ImageBrowser img = new ImageBrowser("IMAGE_DETL", pbDocPic, "DOCUMENT");
            img.ShowDialog();
           
        }
        private bool CheckData()
        {
            bool flag = true;
            chkAddEvent.Checked = false;
            txtEventName.Visible = false;
            cbEventName.Visible = true;

            if (dtpAwardDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Award Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpAwardDate.Focus();
                return flag;

            }
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;
            }
            if (cbZones.SelectedIndex == 0 || cbZones.SelectedIndex==-1)
            {
                flag = false;
                MessageBox.Show("Please Select Zone", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbZones.Focus();
                return flag;
            }
            if (cbRegion.SelectedIndex == 0 || cbRegion.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Region", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbRegion.Focus();
                return flag;
            }
            if (cbLocation.SelectedIndex == 0 || cbLocation.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLocation.Focus();
                return flag;
            }
            if (cbLocation.SelectedIndex == 0 || cbLocation.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLocation.Focus();
                return flag;
            }
            if (cbEventName.SelectedIndex == 0 || cbEventName.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Event Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEventName.Focus();
                return flag;
            }
            if (txtEName.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeSearch.Focus();
                return flag;
            }
            if (txtAwardType.Text.Length < 5)
            {
                flag = false;
                MessageBox.Show("Please Enter Award Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAwardType.Focus();
                return flag;
            }
            if (txtAwardName.Text.Length < 5)
            {
                flag = false;
                MessageBox.Show("Please Enter Award Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAwardName.Focus();
                return flag;
            }
            if (txtPerfDetails.Text.Length == 0 && txtPerfPnts.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Performane or Points", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPerfDetails.Focus();
                return flag;
            }
            if (txtPerfDetails.Text.Length == 0 && txtPerfPnts.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Worth Of Gift Or Cheque", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTripName.Focus();
                return flag;
            }

            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;
            Image img;
            
            if (CheckData() == true)
            {
                try
                {
                    if (txtCash.Text.Length == 0)
                    {
                        txtCash.Text = "0";
                    }
                    if (txtPerfPnts.Text.Length == 0)
                    {
                        txtPerfPnts.Text = "0";
                    }
                    if (strPhoto.Length>0)
                    {
                        img = pbDocPic.BackgroundImage;
                    }
                    else
                    {
                        img = pbDocPic.Image;
                    }
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    
                        arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                   
                    if (flagUpdate == true)
                    {
                        strCmd = "UPDATE HR_APPL_EMP_AWARDS_REWARDS SET HEA_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                  "',HEA_BRANCH_CODE='" + cbLocation.SelectedValue.ToString() +
                                  "',HEA_ZONE='" + cbZones.SelectedValue.ToString() +
                                  "',HEA_REGION='" + cbRegion.SelectedValue.ToString() +
                                  "',HEA_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                  "',HEA_DOC_MONTH='" + Convert.ToDateTime(dtpAwardDate.Value).ToString("MMMyyyy").ToUpper() +
                                  "',HEA_DATE='" + Convert.ToDateTime(dtpAwardDate.Value).ToString("dd/MMM/yyyy") +
                                  "',HEA_EVENT_ID='" + cbEventName.SelectedValue.ToString() +
                                  "',HEA_EVENT_NAME='" + cbEventName.Text.ToString() +
                                  "',HEA_AWARD_TYPE='" + txtAwardType.Text.ToString().Replace("'", "") +
                                  "',HEA_AWARD_NAME='" + txtAwardName.Text.ToString().Replace("'", "") +
                                  "',HEA_PERF_DETAILS='" + txtPerfDetails.Text.ToString().Replace("'", "") +
                                  "',HEA_TRIP_NAME='" + txtTripName.Text.ToString().Replace("'", "") +
                                  "',HEA_CASH=" + Convert.ToDouble(txtCash.Text) +
                                  ",HEA_MEMENTO_TYPE='" + txtMementoType.Text.ToString().Replace("'", "") + "'";

                        if (arr.Length > 2)
                        {
                            strCmd += ", HEA_IMAGE=@Image";
                        }
                        strCmd += ",HEA_MODIFIED_BY='" + CommonData.LogUserId + "',HEA_MODIFIED_DATE=getdate()" +
                                ",HEA_PERF_POINTS=" + Convert.ToDouble(txtPerfPnts.Text) +
                                " where HEA_ID=" + nTrnId +
                                " and HEA_EMP_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " ";

                        flagUpdate = false;
                    }
                    else
                    {                       
                        

                        strCmd = "INSERT INTO HR_APPL_EMP_AWARDS_REWARDS(HEA_COMP_CODE " +
                                                                      ", HEA_BRANCH_CODE " +
                                                                      ", HEA_ZONE " +
                                                                      ", HEA_REGION " +
                                                                      ", HEA_FIN_YEAR " +
                                                                      ", HEA_DOC_MONTH " +
                                                                      ", HEA_DATE " +
                                                                      ", HEA_EVENT_ID " +
                                                                      ", HEA_EVENT_NAME " +
                                                                      ", HEA_EMP_ECODE " +
                                                                      ", HEA_AWARD_TYPE " +
                                                                      ", HEA_AWARD_NAME " +
                                                                      ", HEA_PERF_DETAILS " +
                                                                      ", HEA_PERF_POINTS " +
                                                                      ", HEA_TRIP_NAME " +
                                                                      ", HEA_CASH " +
                                                                      ", HEA_MEMENTO_TYPE ";
                        if (arr.Length > 4)
                        {
                            strCmd += ",HEA_IMAGE ";
                        }

                        strCmd += ", HEA_CREATED_BY, HEA_CREATED_DATE " +
                                                                      ")values('" + cbCompany.SelectedValue.ToString() +
                                                                      "','" + cbLocation.SelectedValue.ToString() +
                                                                      "','" + cbZones.SelectedValue.ToString() +
                                                                      "','" + cbRegion.SelectedValue +
                                                                      "','" + cbFinYear.SelectedValue.ToString() +
                                                                      "','" + Convert.ToDateTime(dtpAwardDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                      "','" + Convert.ToDateTime(dtpAwardDate.Value).ToString("dd/MMM/yyyy") +
                                                                      "','" + cbEventName.SelectedValue.ToString() +
                                                                      "','" + cbEventName.Text.ToString() +
                                                                      "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                                      ",'" + txtAwardType.Text.ToString().Replace("'", "") +
                                                                      "','" + txtAwardName.Text.ToString().Replace("'", "") +
                                                                      "','" + txtPerfDetails.Text.ToString().Replace("'", "") +
                                                                      "'," + Convert.ToDouble(txtPerfPnts.Text).ToString("0.00") +
                                                                      ",'" + txtTripName.Text.ToString().Replace("'", "") +
                                                                      "'," + Convert.ToDouble(txtCash.Text).ToString("0.00") +
                                                                      ",'" + txtMementoType.Text.ToString().Replace("'", "") + "'";
                        if (arr.Length > 4)
                        {
                            strCmd += ", @Image";
                        }

                        strCmd += ",'" + CommonData.LogUserId + "',getdate())";
                    }

                    string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                    objSecurity = new Security();
                    SqlConnection Con = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                    SqlCommand SqlCom = new SqlCommand(strCmd, Con);
                    if (arr.Length > 4)
                    {
                        SqlCom.Parameters.Add(new SqlParameter("@Image", (object)arr));
                    }
                    Con.Open();
                    iRes = SqlCom.ExecuteNonQuery();
                    Con.Close();
                    arr = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagUpdate = false;
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkAddEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddEvent.Checked == true)
            {
                txtEventName.Visible = true;
                cbEventName.Visible = false;
                btnAdd.Visible = true;
            }
            else
            {
                txtEventName.Visible = false;
                cbEventName.Visible = true;
                btnAdd.Visible = false;

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            txtEventName.Text = "";
            txtEventName.Visible = false;
            cbEventName.Visible = true;
            chkAddEvent.Checked = false;
            //cbCompany.SelectedIndex = 0;
            cbZones.SelectedIndex = 0;
            cbRegion.SelectedIndex = -1;
            cbLocation.SelectedIndex = -1;
            dtpAwardDate.Value = DateTime.Today;
            txtMementoType.Text = "";
            pbDocPic.Image = null;
            txtAwardName.Text = "";
            txtAwardType.Text = "";
            txtCash.Text = "";
            txtPerfDetails.Text = "";
            txtPerfPnts.Text = "";
            gvEmpAwardDetl.Rows.Clear();
            txtEcodeSearch.Text = "";
            txtEmpDesg.Text = "";
            txtEName.Text = "";
            txtTripName.Text = "";
            cbFinYear.SelectedValue = CommonData.FinancialYear;
            pbDocPic.Height = 280;
            pbDocPic.Width = 430;
            splitContainer1.Panel2.AutoScroll = false;
          
            //cbEventName.SelectedIndex = 0;

        }

        private void FillEventDetails()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            cbEventName.DataBindings.Clear();
            try
            {
                strCmd = "SELECT EM_EVENT_ID EventId,EM_EVENT_NAME EventName "+
                         " FROM EVENT_MASTER ORDER BY EM_EVENT_NAME asc";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["EventId"] = 0;
                    row["EventName"] = "--Select--";

                    dt.Rows.InsertAt(row,0);

                    cbEventName.DataSource = dt;
                    cbEventName.ValueMember = "EventId";
                    cbEventName.DisplayMember = "EventName";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Int32 nEventId = 0, iRes = 0;
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            if (txtEventName.Text.Length > 5)
            {
                try
                {
                    strCmd = "SELECT isnull(max(EM_EVENT_ID),0)+1 EventId FROM EVENT_MASTER";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        nEventId = Convert.ToInt32(dt.Rows[0]["EventId"]);
                    }

                    string strInsert = "INSERT INTO EVENT_MASTER(EM_EVENT_ID " +
                                                           ", EM_EVENT_NAME) " +
                                                           " VALUES " +
                                                           "(" + nEventId +
                                                           ",'" + txtEventName.Text.ToString().Replace("'", "") + "')";
                    if (strInsert.Length > 5)
                        iRes = objSQLdb.ExecuteSaveData(strInsert);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    FillEventDetails();
                    chkAddEvent.Checked = false;
                    cbEventName.Visible = true;
                    txtEventName.Visible = false;
                    btnAdd.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Event Name","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtEventName.Focus();

            }
        }

        private DataSet GetEmpPreviousAwardDetl(string sFinYear, Int32 nEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFinYear, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, nEcode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmpAwardDetails", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }



        private void FillEmpAwardDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvEmpAwardDetl.Rows.Clear();
            string strPerf = "", strGift = "";

            try
            {
                dt = GetEmpPreviousAwardDetl(cbFinYear.SelectedValue.ToString(),Convert.ToInt32(txtEcodeSearch.Text)).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["PerfPoints"].ToString().Equals("0.00"))
                        {
                            strPerf = dt.Rows[i]["PerfDetails"].ToString();
                        }
                        else
                        {
                            strPerf = dt.Rows[i]["PerfDetails"].ToString() + "" + dt.Rows[i]["PerfPoints"].ToString();
                        }
                        if (dt.Rows[i]["GiftCash"].ToString().Equals("0.00"))
                        {
                            strGift = dt.Rows[i]["TripName"].ToString();
                        }
                        else
                        {
                            strGift = dt.Rows[i]["TripName"].ToString() + "" + dt.Rows[i]["GiftCash"].ToString();
                        }
                     

                        gvEmpAwardDetl.Rows.Add();
                        gvEmpAwardDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvEmpAwardDetl.Rows[i].Cells["TrnId"].Value = dt.Rows[i]["TrnId"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["CompCode"].Value = dt.Rows[i]["CompCode"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["BranCode"].Value = dt.Rows[i]["BranCode"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["Zone"].Value = dt.Rows[i]["Zone"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["Region"].Value = dt.Rows[i]["Region"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["DocMonth"].Value = dt.Rows[i]["DocMonth"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["PerfDetail"].Value = dt.Rows[i]["PerfDetails"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["Points"].Value = dt.Rows[i]["PerfPoints"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["TripName"].Value = dt.Rows[i]["TripName"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["Cash"].Value = dt.Rows[i]["GiftCash"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["EventId"].Value = dt.Rows[i]["EventId"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["AwardType"].Value = dt.Rows[i]["AwardType"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["BackDays"].Value = dt.Rows[i]["BackDays"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["EmpImage"].Value = dt.Rows[i]["EmpImage"];
                        gvEmpAwardDetl.Rows[i].Cells["FinYear"].Value = dt.Rows[i]["FinYear"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["EventName"].Value = dt.Rows[i]["EventName"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["AwardName"].Value = dt.Rows[i]["AwardName"].ToString();
                        gvEmpAwardDetl.Rows[i].Cells["AwardDate"].Value = Convert.ToDateTime(dt.Rows[i]["AwardDate"].ToString()).ToString("dd/MMM/yyyy");
                        gvEmpAwardDetl.Rows[i].Cells["Performance"].Value = strPerf;
                        gvEmpAwardDetl.Rows[i].Cells["WorthOfGiftCheque"].Value = strGift;
                        gvEmpAwardDetl.Rows[i].Cells["Memento"].Value = dt.Rows[i]["MementoType"].ToString();

                        strGift = "";
                        strPerf = "";


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvEmpAwardDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtEventName.Text = "";
                txtEventName.Visible = false;
                cbEventName.Visible = true;
                chkAddEvent.Checked = false;
                //cbCompany.SelectedIndex = 0;
                cbZones.SelectedIndex = 0;
                cbRegion.SelectedIndex = -1;
                cbLocation.SelectedIndex = -1;
                dtpAwardDate.Value = DateTime.Today;
                txtMementoType.Text = "";
                pbDocPic.Image = null;
                txtAwardName.Text = "";
                txtAwardType.Text = "";
                txtCash.Text = "";
                txtPerfDetails.Text = "";
                txtPerfPnts.Text = "";

                txtTripName.Text = "";
                cbFinYear.SelectedValue = CommonData.FinancialYear;

                if (Convert.ToDouble(gvEmpAwardDetl.Rows[e.RowIndex].Cells["BackDays"].Value.ToString()) > 7)
                {                   
                   
                    MessageBox.Show("This Data Can Not Modify!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                    
                }
                else
                {
                    flagUpdate = true;


                    nTrnId = Convert.ToInt32(gvEmpAwardDetl.Rows[e.RowIndex].Cells["TrnId"].Value);
                    cbCompany.SelectedValue = gvEmpAwardDetl.Rows[e.RowIndex].Cells["CompCode"].Value.ToString();
                    cbZones.SelectedValue = gvEmpAwardDetl.Rows[e.RowIndex].Cells["Zone"].Value.ToString();
                    cbRegion.SelectedValue = gvEmpAwardDetl.Rows[e.RowIndex].Cells["Region"].Value.ToString();
                    cbLocation.SelectedValue = gvEmpAwardDetl.Rows[e.RowIndex].Cells["BranCode"].Value.ToString();
                    cbEventName.SelectedValue = gvEmpAwardDetl.Rows[e.RowIndex].Cells["EventId"].Value.ToString();
                    txtAwardType.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["AwardType"].Value.ToString();
                    txtAwardType.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["AwardType"].Value.ToString();
                    txtAwardName.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["AwardName"].Value.ToString();
                    txtPerfDetails.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["PerfDetail"].Value.ToString();
                    txtPerfPnts.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["Points"].Value.ToString();
                    txtTripName.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["TripName"].Value.ToString();
                    txtCash.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["Cash"].Value.ToString();
                    txtMementoType.Text = gvEmpAwardDetl.Rows[e.RowIndex].Cells["Memento"].Value.ToString();
                    cbFinYear.SelectedValue = gvEmpAwardDetl.Rows[e.RowIndex].Cells["FinYear"].Value.ToString();
                    dtpAwardDate.Value = Convert.ToDateTime(gvEmpAwardDetl.Rows[e.RowIndex].Cells["AwardDate"].Value.ToString());

                     strPhoto = gvEmpAwardDetl.Rows[e.RowIndex].Cells["EmpImage"].Value.ToString();

                     if ((gvEmpAwardDetl.Rows[e.RowIndex].Cells["EmpImage"].Value.ToString()) != "")
                     {
                         GetImage((byte[])gvEmpAwardDetl.Rows[e.RowIndex].Cells["EmpImage"].Value);
                         splitContainer1.Panel2.AutoScroll = true;
                     }
                     else
                     {
                         pbDocPic.Height = 280;
                         pbDocPic.Width = 430;
                         splitContainer1.Panel2.AutoScroll = false;                        
                     }
                }

            }
        }

        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                int imgWidth = 0;
                int imghieght = 0;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                imgWidth = newImage.Width;
                imghieght = newImage.Height;

                pbDocPic.Width = imgWidth;
                pbDocPic.Height = imghieght;
                pbDocPic.Image = newImage;


            }
            catch (Exception ex)
            {
            }
        }

        private void txtPerfPnts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
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
