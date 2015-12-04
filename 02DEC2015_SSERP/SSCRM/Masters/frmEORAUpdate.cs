using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using SSCRM.App_Code;
using System.IO;
namespace SSCRM
{
    public partial class frmEORAMasterUpdate : Form
    {
        private SQLDB objSQLDB = null;
        HRInfo objHrInfo;
        DataSet dsHrisEora = null;
        DataSet dsDesgLvl = null;
        DataTable dt = null;
        DataTable dtDesg = null;
        public frmEORAMasterUpdate()
        {
            InitializeComponent();
        }
        private void frmEORAMasterUpdate_Load(object sender, EventArgs e)
        {
            //objSQLDB = new SQLDB();
            //dsHrisEora = objSQLDB.ExecuteDataSet("GetHRISDataforEORAMas", CommandType.StoredProcedure);
            //objSQLDB = null;
            GetPopup();
            cbddlBranch.SelectedValue = CommonData.BranchCode;
            cbddlBranch.Enabled = false;
        }
        public void GetPopup()
        {
            string sqlQry = "SELECT LevelsDesig_mas.LDM_DESIG_ID as desig_id, LevelsDesig_mas.ldm_designations, LevelsDesig_mas.ldm_elevel_id as elevel_id " +
                " FROM [dbo].[LevelsDesig_mas] WHERE dbo.LevelsDesig_mas.ldm_company_code = '" + CommonData.CompanyCode + "' ORDER BY ldm_designations ";
            objSQLDB = new SQLDB();
            dsDesgLvl = objSQLDB.ExecuteDataSet(sqlQry);
            UtilityLibrary.PopulateControl(cbddlEORADesig, objSQLDB.ExecuteDataSet(sqlQry).Tables[0].DefaultView, 1, 0, "--Please Select--", 0);
            objSQLDB = null;

            objHrInfo = new HRInfo();
            DataTable dtBranch = objHrInfo.GetAllBranchList(CommonData.CompanyCode, "", "").Tables[0];
            UtilityLibrary.PopulateControl(cbddlBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objHrInfo = null;
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
                objSQLDB = new SQLDB();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = objSQLDB.CreateParameter("@EORACODE", DbType.String, txtEcodeSearch.Text.ToString(), ParameterDirection.Input);
                //dt = objSQLDB.ExecuteDataSet("GetEORADataforEORAMaster", CommandType.StoredProcedure, param).Tables[0];
                //if (dt.Rows.Count == 0)
                //{
                    dt = objSQLDB.ExecuteDataSet("GetHRISDataforEORAMas", CommandType.StoredProcedure, param).Tables[0];
                //}
                //DataView dvFilter = dsHrisEora.Tables[0].DefaultView;
                //dvFilter.RowFilter = "ID=" + txtEcodeSearch.Text; 
                //dt = dvFilter.ToTable();
                //dt = dsHrisEora.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    
                    txtEORACode.Text = dt.Rows[0]["EORA_CODE"].ToString();
                    txtMemberName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    txtFatherName.Text = dt.Rows[0]["FATHER_NAME"].ToString();
                    meHRISDataofBirth.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                    meHRISDateOfJoin.Text = Convert.ToDateTime(dt.Rows[0]["DOJ"]).ToString("dd/MM/yyyy");
                    txtHRISDesig.Text = dt.Rows[0]["DESIG_NAME"].ToString();
                    txtHRISBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                    objSQLDB = new SQLDB();
                    DataSet dsPhoto = objSQLDB.ExecuteDataSet("SELECT HAMH_MY_PHOTO FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE=" + txtEcodeSearch.Text);

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                    {
                        if (dsPhoto.Tables[0].Rows[0][0].ToString() != "")
                            GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAMH_MY_PHOTO"]);
                        else
                            picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;
                    }
                    else
                        picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;

                    objSQLDB = null;
                    DateTime zeroTime = new DateTime(1, 1, 1);
                    DateTime a = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy,MM,dd"));
                    DateTime b = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                    TimeSpan ival = a - b;
                    int years = (zeroTime + ival).Year - 1;
                    txtHRISAge.Text = years.ToString();
                    cbddlEORADesig.Focus();
                }
                else
                {
                    txtEcodeSearch.Focus();
                }
                objSQLDB = null;
                
            }
            else
            {
                txtEcodeSearch.Focus();
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
            }
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtMemberName.Text.ToString() != "" && txtEORACode.Text.ToString() != "")
            {
                if (cbddlEORADesig.SelectedIndex != 0)
                {
                    try
                    {
                        objSQLDB = new SQLDB();
                        DataSet dsEM = objSQLDB.ExecuteDataSet("SELECT ECODE,DEPT_ID FROM EORA_MASTER WHERE ECODE=" + txtEORACode.Text);
                        DataSet dsLGM = objSQLDB.ExecuteDataSet("SELECT DISTINCT BRANCH_NAME FROM LevelGroup_map INNER JOIN BRANCH_MAS ON lgm_branch_code = BRANCH_CODE " +
                                                "WHERE lgm_group_ecode = '" + txtEcodeSearch.Text + "' AND lgm_doc_month = '" + CommonData.DocMonth + "' AND lgm_branch_code != '" + CommonData.BranchCode + "' UNION SELECT DISTINCT BRANCH_NAME FROM LevelGroup_map " +
                                                "INNER JOIN BRANCH_MAS ON lgm_branch_code = BRANCH_CODE WHERE lgm_source_ecode = '" + txtEORACode.Text + "' AND lgm_doc_month = '" + CommonData.DocMonth + "' AND lgm_branch_code != '" + CommonData.BranchCode + "'");
                        
                        //if (dsEM.Tables[0].Rows[0]["DEPT_ID"].ToString() == "1200000" || dsEM.Tables[0].Rows[0]["DEPT_ID"].ToString() == "700000")
                        //{
                            if (dsLGM.Tables[0].Rows.Count == 0)
                            {
                                string sqlUpdate = "";
                                
                                if (dsEM.Tables[0].Rows.Count > 0)
                                {
                                    if(dsEM.Tables[0].Rows[0]["DEPT_ID"].ToString() != "1200000")
                                    {
                                        MessageBox.Show("Update Only for Sales Department","SSCRM",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                        return;
                                    }                               
                                    sqlUpdate = "UPDATE EORA_MASTER SET BRANCH_CODE='" + CommonData.BranchCode + "',DEPT_ID='1200000',MEMBER_NAME='" + dt.Rows[0]["MEMBER_NAME"].ToString() +
                                        "',EORA='" + dt.Rows[0]["EORA"].ToString() + "',HRIS_EMP_NAME='" + dt.Rows[0]["MEMBER_NAME"].ToString() + "',DESG_ID='" + dtDesg.Rows[0]["DESIG_ID"].ToString() + "',DESIG='" +
                                        dtDesg.Rows[0]["ldm_designations"].ToString() + "',EMP_DOJ='" + Convert.ToDateTime(dt.Rows[0]["DOJ"]).ToString("dd/MMM/yyyy") + "',EMP_DOB='" + Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MMM/yyyy") + "',FATHER_NAME='" + dt.Rows[0]["FATHER_NAME"].ToString() +
                                        "',elevel_id='" + dtDesg.Rows[0]["elevel_id"].ToString() + "',company_code='" + CommonData.CompanyCode + "',edu_qualification='" + dt.Rows[0]["QUALIFICATION"].ToString() + "' WHERE ECODE=" + txtEORACode.Text;
                                }
                                else
                                {
                                    sqlUpdate = "INSERT INTO EORA_MASTER (BRANCH_CODE,DEPT_ID,ECODE,MEMBER_NAME,EORA,HRIS_EMP_NAME,DESG_ID,DESIG,EMP_DOJ,EMP_DOB,FATHER_NAME,ELEVEL_ID,COMPANY_CODE,EDU_QUALIFICATION)" +
                                        "SELECT '" + CommonData.BranchCode + "','1200000'," + txtEORACode.Text + ",'" + dt.Rows[0]["MEMBER_NAME"].ToString() + "','" + dt.Rows[0]["EORA"].ToString() +
                                        "','" + dt.Rows[0]["MEMBER_NAME"].ToString() + "','" + dtDesg.Rows[0]["DESIG_ID"].ToString() + "','" + dtDesg.Rows[0]["ldm_designations"].ToString() + "','" + Convert.ToDateTime(dt.Rows[0]["DOJ"]).ToString("dd/MMM/yyyy") +
                                        "','" + Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MMM/yyyy") + "','" + dt.Rows[0]["FATHER_NAME"].ToString() + "','" + dtDesg.Rows[0]["elevel_id"].ToString() + "','" + CommonData.CompanyCode +
                                        "','" + dt.Rows[0]["QUALIFICATION"].ToString() + "'";
                                }
                                int intRec = objSQLDB.ExecuteSaveData(sqlUpdate);
                                if (intRec > 0)
                                {
                                    MessageBox.Show("Data saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                                MessageBox.Show(txtEORACode.Text + " Mapped in " + dsLGM.Tables[0].Rows[0][0].ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtEcodeSearch.Text = "";
                        //}
                        //else
                        //    MessageBox.Show(txtEcodeSearch.Text+" Not in Sales Department\n You can Update Only Sales Department\nData not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        objSQLDB = null;
                        btnClear_Click(null, null);
                    }
                }
                else
                    MessageBox.Show("Select Designation", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            else
            {
                MessageBox.Show("Emp Name Not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            txtEcodeSearch.Focus();
           
        }

        private void cbddlEORADesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbddlEORADesig.SelectedIndex > 0)
            {
                DataView dvDeg = dsDesgLvl.Tables[0].DefaultView;
                dvDeg.RowFilter = "desig_id=" + cbddlEORADesig.SelectedValue;
                dtDesg = dvDeg.ToTable();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtEORACode.Text = "";
            txtMemberName.Text = "";
            txtFatherName.Text = "";
            meHRISDataofBirth.Text = "";
            meHRISDateOfJoin.Text = "";
            txtHRISDesig.Text = "";
            txtHRISBranch.Text = "";
            txtHRISAge.Text = "";
            cbddlEORADesig.SelectedIndex = 0;
            //picEmpPhoto.BackgroundImage = System.Drawing.Color.AliceBlue;
            //picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;
        }
    }
}
