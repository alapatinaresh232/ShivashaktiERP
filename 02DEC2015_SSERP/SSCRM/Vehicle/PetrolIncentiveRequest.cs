using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;
using System.IO;

namespace SSCRM
{
    public partial class PetrolIncentiveRequest : Form
    {
        SQLDB objSQLdb = null;
        public PetrolIncentiveRequest()
        {
            InitializeComponent();
        }

        private void txtDsearch_Validated(object sender, EventArgs e)
        {
            if (txtDsearch.Text.Length > 4)
            {
                objSQLdb = new SQLDB();
                SqlParameter[] param = new SqlParameter[1];
                DataTable dt = new DataTable();
                DataSet dsPhoto = new DataSet();
                try
                {
                    param[0] = objSQLdb.CreateParameter("@EORACODE", DbType.Int32, txtDsearch.Text, ParameterDirection.Input);

                    dt = objSQLdb.ExecuteDataSet("GetEmpVehDetails", CommandType.StoredProcedure, param).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtMemberName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                        txtDeptName.Text = dt.Rows[0]["dept_name"] + "";
                        txtDesgName.Text = dt.Rows[0]["desig_name"] + "";
                        txtVehRegNo.Text = dt.Rows[0]["HVLH_VEHICLE_REG_NUMBER"] + "";
                        txtRegDate.Text = dt.Rows[0]["HVLH_VEHICLE_REG_DATE"] + "";
                        txtVehModel.Text = dt.Rows[0]["hvlh_vehicle_makers_class"] + "";
                        txtLicNo.Text = dt.Rows[0]["LICENCE"] + "";
                        txtPetrolIncStatus.Text = dt.Rows[0]["STATUS"] + "";


                       
                        dsPhoto = objSQLdb.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + txtDsearch.Text);

                        if (dsPhoto.Tables[0].Rows.Count > 0)
                            GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                        else
                            picEmpPhoto.BackgroundImage = null;
                    }
                    else
                    {
                        txtMemberName.Text = "";
                        txtDeptName.Text = "";
                        txtDesgName.Text = "";
                        txtVehRegNo.Text = "";
                        txtRegDate.Text = "";
                        txtVehModel.Text = "";
                        txtLicNo.Text = "";
                        txtPetrolIncStatus.Text = "";

                        picEmpPhoto.BackgroundImage = null;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    param = null;
                    objSQLdb = null;
                    dt = null;
                    dsPhoto = null;
                }
            }
            else
            {
                MessageBox.Show("Enter Valid Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDsearch.Text = "";
            txtMemberName.Text = "";
            txtDeptName.Text = "";
            txtDesgName.Text = "";
            txtVehRegNo.Text = "";
            txtRegDate.Text = "";
            txtVehModel.Text = "";
            txtLicNo.Text = "";
            txtPetrolIncStatus.Text = "";
            picEmpPhoto.BackgroundImage = null;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {

            if ((txtPetrolIncStatus.Text == "APPROVED" || txtPetrolIncStatus.Text == "NO") && txtMemberName.Text.Length > 0 )
            {
                objSQLdb = new SQLDB();
                int iRes = 0;
                string strCmd = "UPDATE HR_VEHICLE_LOAN_HEAD SET HVLH_PETROL_REQUEST_FLAG='Y' WHERE HVLH_EORA_CODE=" + txtDsearch.Text;
                try
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Updated Successfully", "SSCRM :: Petrol Incentive Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Cannot Process Petrol Request", "SSCRM :: Petrol Incentive Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(" Approval In Process ", "SSCRM :: Petrol Incentive Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

       
    }
}
