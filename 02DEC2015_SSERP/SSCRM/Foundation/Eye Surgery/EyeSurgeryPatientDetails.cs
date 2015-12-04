using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class EyeSurgeryPatientDetails : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        bool flagUpdate = false;
        public EyeSurgeryList objEyeSurgeryList = null;
       Int32 TrnNo = 0;

        public EyeSurgeryPatientDetails()
        {
            InitializeComponent();
        }
        public EyeSurgeryPatientDetails(Int32 iTrnNo)
        {
            InitializeComponent();
            flagUpdate = true;
            TrnNo = iTrnNo;
        }

        private void EyeSurgeryPatientDetails_Load(object sender, EventArgs e)
        {
            FillHospitalData();
            FillSurgeryList();
            GenerateTrnNo();

            dtpSurgeryDate.Value = DateTime.Today;
            dtpPreOpDate.Value = DateTime.Today;
            //cbSurgery.SelectedIndex = 0;
            //cbHospital.SelectedIndex = 1;
            
            cbGender.SelectedIndex = 1;
            cbEye.SelectedIndex = 0;
            if (flagUpdate == true)
            {
                txtMrdNo.ReadOnly = true;
                FillPatientSurgeryDetails(TrnNo);
            }
        }

        private void FillHospitalData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT HM_ID,HM_NAME FROM HOSPITAL_MAS ";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbHospital.DataSource = dt;
                    cbHospital.DisplayMember = "HM_NAME";
                    cbHospital.ValueMember = "HM_ID";
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

        private void GenerateTrnNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT ISNULL(MAX(FSH_ID),0)+1 FROM FOUNDATION_SURGERY_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTrnNo.Text = dt.Rows[0][0].ToString();
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

        private void btnVilSearch_Click(object sender, EventArgs e)
        {
            VillageSearch vilSearch = new VillageSearch("EyeSurgeryPatientDetails");
            vilSearch.objEyeSurgeryPatientDetails = this;
            vilSearch.ShowDialog();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void FillSurgeryList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT  SM_ID,SM_SURGERY_NAME FROM SURGERY_MAS ORDER BY SM_SURGERY_NAME ASC";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbSurgery.DataSource = dt;
                    cbSurgery.DisplayMember = "SM_SURGERY_NAME";
                    cbSurgery.ValueMember = "SM_ID";
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


        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbHospital.SelectedIndex = 1;
            GenerateTrnNo();

            txtVillage.Text = string.Empty;
            txtDistrict.Text = "";
            txtMandal.Text = "";
            txtState.Text = "";
            txtPatientAge.Text = "";
            txtPin.Text = "";
            cbGender.SelectedIndex = 1;
            cbRelation.SelectedIndex = 1;
            cbSurgery.SelectedIndex = 0;
            txtPatientName.Text = "";
            txtPatientFOrHName.Text = "";
            txtMrdNo.Text = "";
            txtHouseNo.Text = "";
            txtAddress.Text = "";
            cbSurgery.SelectedIndex = 0;
            cbEye.SelectedIndex = 0;
            dtpPreOpDate.Value = DateTime.Today;
            dtpSurgeryDate.Value = DateTime.Today;

        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbHospital.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Hospital","Eye Surgery Patient Details",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbHospital.Focus();
            }
            else if (txtMrdNo.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter MrdNo", "Eye Surgery Patient Details",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtMrdNo.Focus();
            }
            else if (txtPatientName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Patient Name", "Eye Surgery Patient Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPatientName.Focus();
            }
            else if (txtPatientFOrHName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Patient Relation Name", "Eye Surgery Patient Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPatientFOrHName.Focus();
            }
            else if (txtVillage.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Village Details", "Eye Surgery Patient Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVillage.Focus();
            }
            //else if (cbSurgery.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select Surgery Type", "Eye Surgery Patient Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbSurgery.Focus();
            //}
            //else if (cbEye.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select Eye", "Eye Surgery Patient Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbEye.Focus();
            //}

            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            string strEye = "";
            string strSurgery = "";
            if (CheckData() == true)
            
            {
                try
                {
                    
                    if (cbEye.SelectedIndex == 0)
                    {                       
                        cbEye.SelectedItem = strEye;
                    }
                    else if (cbEye.SelectedIndex > 0)
                    {
                        strEye = cbEye.SelectedItem.ToString();
                    }
                    if (cbSurgery.SelectedIndex == 0)
                    {
                        cbSurgery.Text = strSurgery;
                    }
                    else if (cbSurgery.SelectedIndex > 0)
                    {
                        strSurgery = cbSurgery.Text.ToString();
                    }


                    if (flagUpdate == true)
                    {
                        strCommand = "UPDATE FOUNDATION_SURGERY_HEAD SET FSH_HOSPITAL_ID=" + Convert.ToInt32(cbHospital.SelectedValue.ToString()) +
                                    ", FSH_MRD_NO ='" + txtMrdNo.Text.ToString() + "', FSH_OP_NAME ='" + txtPatientName.Text.ToString() +
                                    "', FSH_FORH ='" + cbRelation.SelectedItem.ToString() + "', FSH_FORH_NAME='" + txtPatientFOrHName.Text.ToString() +
                                    "', FSH_AGE =" + Convert.ToInt32(txtPatientAge.Text.ToString()) + ", FSH_GENDER ='" + cbGender.SelectedItem.ToString() +
                                    "', FSH_ADDRESS='" + txtAddress.Text.ToString() + "', FSH_HNO='" + txtHouseNo.Text.ToString() +
                                    "', FSH_VILLAGE ='" + txtVillage.Text.ToString() + "', FSH_MANDAL ='" + txtMandal.Text.ToString() +
                                    "', FSH_DISTRICT ='" + txtDistrict.Text.ToString() + "', FSH_STATE ='" + txtState.Text.ToString() +
                                    "', FSH_PIN ='" + txtPin.Text.ToString() +
                                    "', FSH_POP_DATE='" + Convert.ToDateTime(dtpPreOpDate.Value).ToString("dd/MMM/yyyy") +
                                    "', FSH_SURGERY_DATE='" + Convert.ToDateTime(dtpSurgeryDate.Value).ToString("dd/MMM/yyyy") +
                                    "', FSH_SURGERY_NAME ='" + strSurgery + "', FSH_EYE ='" +strEye +
                                    "', FSH_MODIFIED_BY='" + CommonData.LogUserId + "',FSH_MODIFIED_DATE= getdate() "+
                                     //"' WHERE FSH_ID=" + Convert.ToInt32(txtTrnNo.Text) + "";
                                    " WHERE FSH_MRD_NO='" + txtMrdNo.Text.ToString() + "'";
                       
                    }
                    else if (flagUpdate == false)
                    {
                        strCommand = "INSERT INTO FOUNDATION_SURGERY_HEAD(FSH_HOSPITAL_ID " +
                                                                       ", FSH_MRD_NO " +
                                                                       ", FSH_OP_NAME " +
                                                                       ", FSH_FORH " +
                                                                       ", FSH_FORH_NAME " +
                                                                       ", FSH_AGE " +
                                                                       ", FSH_GENDER " +
                                                                       ", FSH_ADDRESS " +
                                                                       ", FSH_HNO " +
                                                                       ", FSH_VILLAGE " +
                                                                       ", FSH_MANDAL " +
                                                                       ", FSH_DISTRICT " +
                                                                       ", FSH_STATE " +
                                                                       ", FSH_PIN " +
                                                                       ", FSH_POP_DATE " +
                                                                       ", FSH_SURGERY_DATE " +
                                                                       ", FSH_SURGERY_NAME " +
                                                                       ", FSH_EYE " +
                                                                       ", FSH_CREATED_BY " +
                                                                       ", FSH_CREATED_DATE " +
                                                                       ")VALUES(" + Convert.ToInt32(cbHospital.SelectedValue.ToString()) +
                                                                       ",'" + txtMrdNo.Text.ToString().Trim() +
                                                                       "','" + txtPatientName.Text.ToString() +
                                                                       "','" + cbRelation.SelectedItem.ToString() +
                                                                       "','" + txtPatientFOrHName.Text.ToString() +
                                                                       "'," + Convert.ToInt32(txtPatientAge.Text.ToString()) +
                                                                       ",'" + cbGender.SelectedItem.ToString() +
                                                                       "','" + txtAddress.Text.ToString() +
                                                                       "','" + txtHouseNo.Text.ToString() +
                                                                       "','" + txtVillage.Text.ToString() +
                                                                       "','" + txtMandal.Text.ToString() +
                                                                       "','" + txtDistrict.Text.ToString() +
                                                                       "','" + txtState.Text.ToString() +
                                                                       "','" + txtPin.Text.ToString() +
                                                                       "','" + Convert.ToDateTime(dtpPreOpDate.Value).ToString("dd/MMM/yyyy") +
                                                                       "','" + Convert.ToDateTime(dtpSurgeryDate.Value).ToString("dd/MMM/yyyy") +
                                                                       "','" + cbSurgery.Text.ToString() +
                                                                       "','" + cbEye.SelectedItem.ToString() +
                                                                       "','" + CommonData.LogUserId +"', getdate())";
                    }
                    iRes = objSQLdb.ExecuteSaveData(strCommand);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Sucessfully", "Eye Surgery Details", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    btnCancel_Click(null, null);
                    if (flagUpdate == true)
                    {
                        this.Close();
                        this.Dispose();
                        objEyeSurgeryList.FillSurgeryList();                        
                    }
                    flagUpdate = false;
                    //GenerateTrnNo();

                    //((EyeSurgeryList)objEyeSurgeryList).FillSurgeryList();

                }
                else
                {
                    MessageBox.Show("Data Not Saved", "Eye Surgery Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FillAddressData(string svilsearch)
        {
            objServicedb = new ServiceDeptDB();

            DataTable dtVillage = new DataTable();
            if (txtVillage.Text != "")
            {
                try
                {
                    dtVillage = objServicedb.ServiceVillageSearch_Get(svilsearch).Tables[0];

                    if (dtVillage.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtVillage.Rows.Count; i++)
                        {
                            if (txtVillage.Text.Equals(dtVillage.Rows[i]["PANCHAYAT"].ToString()))
                            {
                                txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                                txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                                txtState.Text = dtVillage.Rows[0]["State"].ToString();
                                txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();

                            }

                        }
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objServicedb = null;
                    dtVillage = null;


                }
            }
            else
            {
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
            }

        }

        private void txtVillage_TextChanged(object sender, EventArgs e)
        {
            //if (txtVillage.Text != "")
            //{
            //    FillAddressData(txtVillage.Text.ToString());
            //}
            //else
            //{
            //    txtMandal.Text = "";
            //    txtDistrict.Text = "";
            //    txtState.Text = "";
            //    txtPin.Text = "";
            //}
        }
       
        private void FillPatientSurgeryDetails(Int32 nTrnNo)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = " SELECT FSH_ID TrnNo,FSH_HOSPITAL_ID Hosp_Id "+
                                    ", FSH_MRD_NO MRD_No, FSH_OP_NAME Patient_Name "+
                                    ", FSH_FORH Forh,FSH_FORH_NAME Forh_Name "+
                                    ", FSH_AGE Age,FSH_GENDER Gender,FSH_ADDRESS Address "+
                                    ", FSH_HNO House_No, FSH_VILLAGE Village,FSH_MANDAL Mandal "+
                                    ", FSH_DISTRICT District,FSH_STATE State, FSH_PIN Pin "+
                                    ", FSH_POP_DATE Pre_Op_Date,FSH_SURGERY_DATE Surgery_Date "+
                                    ", FSH_SURGERY_NAME Surgery_Name,FSH_EYE Eye "+
                                    "  FROM FOUNDATION_SURGERY_HEAD "+
                                    " WHERE FSH_ID= "+ nTrnNo +" ";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    flagUpdate = true;

                    txtTrnNo.Text = Convert.ToString(nTrnNo);
                    cbHospital.SelectedValue = dt.Rows[0]["Hosp_Id"].ToString();
                    txtMrdNo.Text = dt.Rows[0]["MRD_No"].ToString();
                    txtPatientName.Text = dt.Rows[0]["Patient_Name"].ToString();
                    cbRelation.SelectedItem = dt.Rows[0]["Forh"].ToString();
                    txtPatientFOrHName.Text = dt.Rows[0]["Forh_Name"].ToString();
                    txtPatientAge.Text = dt.Rows[0]["Age"].ToString();
                    cbGender.SelectedItem = dt.Rows[0]["Gender"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();
                    txtVillage.Text = dt.Rows[0]["Village"].ToString();
                    txtMandal.Text = dt.Rows[0]["Mandal"].ToString();
                    txtDistrict.Text = dt.Rows[0]["District"].ToString();
                    txtState.Text = dt.Rows[0]["State"].ToString();
                    txtPin.Text = dt.Rows[0]["Pin"].ToString();
                    txtHouseNo.Text = dt.Rows[0]["House_No"].ToString();
                    cbSurgery.Text = dt.Rows[0]["Surgery_Name"].ToString();
                    dtpPreOpDate.Value = Convert.ToDateTime(dt.Rows[0]["Pre_Op_Date"]);
                    dtpSurgeryDate.Value = Convert.ToDateTime(dt.Rows[0]["Surgery_Date"]);
                    cbEye.SelectedItem = dt.Rows[0]["Eye"].ToString();


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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;

             DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                    "Demo Plots", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

             if (result == DialogResult.Yes)
             {
                 try
                 {
                     string strCmd = "DELETE FROM FOUNDATION_SURGERY_HEAD WHERE FSH_ID=" + Convert.ToInt32(txtTrnNo.Text) + "";
                     iRes = objSQLdb.ExecuteSaveData(strCmd);
                     if (iRes > 0)
                     {
                         MessageBox.Show("Data Deleted Sucessfully", "Patient Surgery Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         btnCancel_Click(null,null);
                     }

                 }

                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.ToString());
                     MessageBox.Show("Data Not Deleted ", "Patient Surgery Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }
             else
             {
                 MessageBox.Show("Data Not Deleted ", "Patient Surgery Details", MessageBoxButtons.OK, MessageBoxIcon.Error);

             }

        }

        private void txtPatientAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtHouseNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
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
