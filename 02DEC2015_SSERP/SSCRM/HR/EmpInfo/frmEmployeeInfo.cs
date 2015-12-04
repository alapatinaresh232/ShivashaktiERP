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
using System.Data.SqlClient;
using System.IO;

namespace SSCRM
{
    public partial class frmEmployeeInfo : Form
    {
        SQLDB objSQLdb = null;
        public frmEmployeeInfo()
        {
            InitializeComponent();
        }

        private void frmEmployeeInfo_Load(object sender, EventArgs e)
        {
            
        }

        private DataSet GetEmployeeDetails(Int32 Ecode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEoraCode", DbType.Int32, Ecode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmployeeEoraInfo_ERP", CommandType.StoredProcedure, param);

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

        private DataSet GetEmployeeRejoinDetails(Int32 ApplNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {


                param[0] = objSQLdb.CreateParameter("@xApplNo", DbType.Int32, ApplNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmpRejoinHist", CommandType.StoredProcedure, param);

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

        private void FillEmployeeDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            dgvFreeProduct.Rows.Clear();
            ClearControls();
            if (txtEcodeSearch.Text.Length > 4)
            {

                try
                {
                    dt = GetEmployeeDetails(Convert.ToInt32(txtEcodeSearch.Text)).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtApplNo.Text = dt.Rows[0]["ApplNo"].ToString();
                        txtPresEcode.Text = dt.Rows[0]["PresCode"].ToString();
                        txtMigCode.Text = dt.Rows[0]["MigCode"].ToString();
                        txtEoraType.Text = dt.Rows[0]["Eora_Type"].ToString();

                        txtWorkStatus.Text = dt.Rows[0]["WorkingStatus"].ToString();
                        txtEName.Text = dt.Rows[0]["Name"].ToString();
                        txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                        try
                        {
                            if (dt.Rows[0]["EmpDOB"].ToString() != "")
                                txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["EmpDOB"]).ToString("dd/MM/yyyy");
                        }
                        catch
                        {
                            txtDOB.Text = "";
                        }
                        try
                        {
                            if (dt.Rows[0]["EmpDOJ"].ToString() != "")
                                txtDOJ.Text = Convert.ToDateTime(dt.Rows[0]["EmpDOJ"]).ToString("dd/MM/yyyy");
                        }
                        catch { txtDOJ.Text = ""; }
                        txtQualif.Text = dt.Rows[0]["Qualification"].ToString();
                        txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                        txtDesig.Text = dt.Rows[0]["EmpDesig"].ToString();
                        txtCompany.Text = dt.Rows[0]["CompanyName"].ToString();
                        txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
                        txtMailId.Text = dt.Rows[0]["EmailId"].ToString();
                        txtContactNo.Text = dt.Rows[0]["MobileNo"].ToString();
                        txtApprovalNo.Text = dt.Rows[0]["ApprNo"].ToString();
                        txtBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();
                        try
                        {
                            if (dt.Rows[0]["ApprDate"].ToString() != "")
                                txtApprDate.Text = Convert.ToDateTime(dt.Rows[0]["ApprDate"]).ToString("dd/MM/yyyy");
                        }
                        catch { txtApprDate.Text = ""; }

                        if (dt.Rows[0]["Photo"].ToString() != "")
                            GetImage((byte[])dt.Rows[0]["Photo"]);
                        else
                            picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;


                        txtPresHouseNo.Text = dt.Rows[0]["PreHNo"].ToString();
                        txtPresLandMark.Text = dt.Rows[0]["Pres_LandMark"].ToString();
                        txtPresMandal.Text = dt.Rows[0]["Pres_Mandal"].ToString();
                        txtPersVillage.Text = dt.Rows[0]["Pres_Vill"].ToString();
                        txtPresDistrict.Text = dt.Rows[0]["Pres_District"].ToString();
                        txtPresState.Text = dt.Rows[0]["Pres_State"].ToString();
                        txtPersPin.Text = dt.Rows[0]["Pres_Pin"].ToString();
                        PresentPhone_num.Text = dt.Rows[0]["Pres_Phone"].ToString();

                        txtPermentPhNo_num.Text = dt.Rows[0]["Perm_Phone"].ToString();
                        txtPermi_HNo.Text = dt.Rows[0]["Perm_HNo"].ToString();
                        txtPermi_LandMark.Text = dt.Rows[0]["Perm_LandMark"].ToString();
                        txtPermin_District.Text = dt.Rows[0]["Perm_District"].ToString();
                        txtPermin_Village.Text = dt.Rows[0]["Perm_Vill"].ToString();
                        txtPermin_Mandal.Text = dt.Rows[0]["Perm_Mandal"].ToString();
                        txtPermin_State.Text = dt.Rows[0]["Perm_State"].ToString();
                        txtPermin_Pin.Text = dt.Rows[0]["Perm_Pin"].ToString();

                        txtEmer_HNo.Text = dt.Rows[0]["Cont_Pers_HNo"].ToString();
                        txtEmer_LandMark.Text = dt.Rows[0]["Cont_Pers_LandMark"].ToString();
                        txtEmer_Mandal.Text = dt.Rows[0]["Cont_Pers_Mandal"].ToString();
                        txtEmer_Village.Text = dt.Rows[0]["Cont_Pers_Village"].ToString();
                        txtEmer_District.Text = dt.Rows[0]["Cont_Pers_District"].ToString();
                        txtEmer_State.Text = dt.Rows[0]["Cont_Pers_State"].ToString();
                        txtEmer_Pin.Text = dt.Rows[0]["Cont_Pers_Pin"].ToString();

                        txtContPhNo_num.Text = dt.Rows[0]["Cont_Pers_Phone_Res"].ToString();
                        txtContPhNo1_optional.Text = dt.Rows[0]["Cont_Pers_Phone_Office"].ToString();
                        txtContactName.Text = dt.Rows[0]["Cont_Pers_Name"].ToString();

                        TabAddress.SelectedIndex = 0;



                    }
                    else
                    {
                        ClearControls();
                        MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEcodeSearch.Focus();

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

                objSQLdb = new SQLDB();
                if (txtApplNo.Text.Length > 0)
                {
                    try
                    {
                        dt = GetEmployeeRejoinDetails(Convert.ToInt32(txtApplNo.Text)).Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = i + 1;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellApplNo = new DataGridViewTextBoxCell();
                            cellApplNo.Value = dt.Rows[i]["ApplNo"]; ;
                            tempRow.Cells.Add(cellApplNo);

                            DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                            cellEcode.Value = dt.Rows[i]["PrevEcode"]; ;
                            tempRow.Cells.Add(cellEcode);

                            DataGridViewCell cellDoj = new DataGridViewTextBoxCell();
                            cellDoj.Value = Convert.ToDateTime(dt.Rows[i]["PrevDoj"]).ToString("dd-MMM-yyyy"); ;
                            tempRow.Cells.Add(cellDoj);

                            DataGridViewCell cellPEcode = new DataGridViewTextBoxCell();
                            cellPEcode.Value = dt.Rows[i]["PresEcode"]; ;
                            tempRow.Cells.Add(cellPEcode);

                            DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                            cellRemarks.Value = dt.Rows[i]["Remarks"]; ;
                            tempRow.Cells.Add(cellRemarks);

                            dgvFreeProduct.Rows.Add(tempRow);
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
            else
            {
                ClearControls();
            }
        }
        private void ClearControls()
        {
            txtApplNo.Text = "";
            txtPresEcode.Text = "";
            txtMigCode.Text = "";
            txtEoraType.Text = "";
            txtWorkStatus.Text = "";
            txtEName.Text = "";
            txtFatherName.Text = "";
            txtDOB.Text = "";
            txtDOJ.Text = "";

            txtQualif.Text = "";
            txtDept.Text = "";
            txtDesig.Text = "";
            txtCompany.Text = "";
            txtBranch.Text = "";
            txtMailId.Text = "";
            txtContactNo.Text = "";
            txtApprovalNo.Text = "";
            txtApprDate.Text = "";
            picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;

            txtPresHouseNo.Text = "";
            txtPresLandMark.Text = "";
            txtPresMandal.Text = "";
            txtPersVillage.Text = "";
            txtPresDistrict.Text = "";
            txtPresState.Text = "";
            txtPersPin.Text = "";
            PresentPhone_num.Text = "";

            txtPermentPhNo_num.Text = "";
            txtPermi_HNo.Text = "";
            txtPermi_LandMark.Text = "";
            txtPermin_District.Text = "";
            txtPermin_Village.Text = "";
            txtPermin_Mandal.Text = "";
            txtPermin_State.Text = "";
            txtPermin_Pin.Text = "";

            txtEmer_HNo.Text = "";
            txtEmer_LandMark.Text = "";
            txtEmer_Mandal.Text = "";
            txtEmer_Village.Text = "";
            txtEmer_District.Text = "";
            txtEmer_State.Text = "";
            txtEmer_Pin.Text = "";
            txtContPhNo_num.Text = "";
            txtContPhNo1_optional.Text = "";
            txtContactName.Text = "";

            txtBloodGroup.Text = "";
            txtEcodeSearch.Focus();
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


        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            FillEmployeeDetails();
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
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            ClearControls();
            txtEcodeSearch.Focus();
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void txtPresState_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

       
        //public void EnableTab(TabPage page, bool enable)
        //{
        //    EnableControls(page.Controls, enable);
        //}
        //private void EnableControls(Control.ControlCollection ctls, bool enable)
        //{
        //    foreach (Control ctl in ctls)
        //    {
        //        ctl.Enabled = enable;
        //        EnableControls(ctl.Controls, enable);
        //    }
        //}
      

    }
}
