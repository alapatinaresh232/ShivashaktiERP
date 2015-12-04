using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSAdmin;
using SSTrans;
using SSCRMDB;
using SSCRM.App_Code;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class EyeCampPatientDetails : Form
    {
        InvoiceDB objInvoiceData = null;
        bool updateFlag = false;
        SQLDB objDb = null;
        int iTrnNo=0;
        public EyeCampPatientDetails()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void EyeCampPatientDetails_Load(object sender, EventArgs e)
        {
            cbRelation.SelectedIndex = 0;
            cbMORF.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            updateFlag = false;
            txtPatientSlno.Text = "";
            txtPatientName.Text = "";
            txtPatientFatherName.Text = "";
            txtPatientHNo.Text = "";
            txtPatientLandmark.Text = "";
            txtPatientVill.Text = "";
            txtPatientVilSearch.Text = "";
            txtPatientMandal.Text = "";
            txtPatientDistrict.Text = "";
            txtPatientState.Text = "";
            txtPatientPin.Text = "";
            txtPatientAge.Text = "";
        }

        private void btnCampVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("CampDetails");
            VSearch.obEyeCampDetails = this;
            VSearch.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("PatientDetails");
            VSearch.obEyeCampDetails = this;
            VSearch.ShowDialog();
        }

        private void txtCampVillageSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtCampVillageSearch.Text.Length == 0)
                {
                    cbCampVillage.DataSource = null;
                    cbCampVillage.DataBindings.Clear();
                    cbCampVillage.Items.Clear();
                    //if (btnSave.Enabled == true)
                    ClearVillageDetails("Camp");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch("Camp") == false)
                    {
                        FillAddressData(txtCampVillageSearch.Text, "Camp");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ClearVillageDetails(string sAddressType)
        {
            if (sAddressType == "Camp")
            {
                txtCampVillage.Text = "";
                txtCampMandal.Text = "";
                txtCampDistrict.Text = "";
                txtCampState.Text = "";
                txtCampPin.Text = "";
            }
            else if (sAddressType == "Patient")
            {
                txtPatientVill.Text = "";
                txtPatientMandal.Text = "";
                txtPatientDistrict.Text = "";
                txtPatientState.Text = "";
                txtPatientPin.Text = "";
            }
        }
        private bool FindInputAddressSearch(string sAddressType)
        {
            bool blFind = false;
            try
            {
                if (sAddressType == "Camp")
                    for (int i = 0; i < this.cbCampVillage.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbCampVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtCampVillageSearch.Text) > -1)
                        {
                            blFind = true;
                            cbCampVillage.SelectedIndex = i;
                            txtCampVillage.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[i])).Row.ItemArray[1] + "";
                            txtCampMandal.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[i])).Row.ItemArray[2] + "";
                            txtCampDistrict.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[i])).Row.ItemArray[3] + "";
                            txtCampState.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[i])).Row.ItemArray[4] + "";
                            txtCampPin.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[i])).Row.ItemArray[5] + "";
                            //strStateCode = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[0] + "";
                            break;
                        }
                        break;
                    }
                else if (sAddressType == "Patient")
                    for (int i = 0; i < this.cbPatientVillage.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbPatientVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtPatientVilSearch.Text) > -1)
                        {
                            blFind = true;
                            cbPatientVillage.SelectedIndex = i;
                            txtPatientVill.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[i])).Row.ItemArray[1] + "";
                            txtPatientMandal.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[i])).Row.ItemArray[2] + "";
                            txtPatientDistrict.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[i])).Row.ItemArray[3] + "";
                            txtPatientState.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[i])).Row.ItemArray[4] + "";
                            txtPatientPin.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[i])).Row.ItemArray[5] + "";
                            //strStateCode = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[0] + "";
                            break;
                        }
                        break;
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
            return blFind;
        }
        private void FillAddressData(string sSearch, string sAddressType)
        {
            Hashtable htParam = null;
            objInvoiceData = new InvoiceDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (sSearch.Trim().Length >= 0)
                    htParam = new Hashtable();
                htParam.Add("sVillage", sSearch);
                htParam.Add("sDistrict", strDist);



                htParam.Add("sCDState", CommonData.StateCode);
                dsVillage = new DataSet();
                dsVillage = objInvoiceData.GetVillageDataSet(htParam);
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {
                    if (sAddressType == "Camp")
                    {
                        txtCampVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                        txtCampMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                        txtCampDistrict.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                        txtCampState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                        txtCampPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                        //strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";

                        txtPatientVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                        txtPatientMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                        txtPatientDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                        txtPatientState.Text = dtVillage.Rows[0]["State"].ToString();
                        txtPatientPin.Text = dtVillage.Rows[0]["PIN"].ToString();
                    }
                    else if (sAddressType == "Patient")
                    {
                        txtPatientVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                        txtPatientMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                        txtPatientDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                        txtPatientState.Text = dtVillage.Rows[0]["State"].ToString();
                        txtPatientPin.Text = dtVillage.Rows[0]["PIN"].ToString();
                        //strStateCode = dtVillage.Rows[0]["CDState"].ToString();
                    }
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    if (sAddressType == "Camp")
                    {
                        txtCampVillage.Text = "";
                        txtCampMandal.Text = "";
                        txtCampDistrict.Text = "";
                        txtCampState.Text = "";
                        txtCampPin.Text = "";
                        //strStateCode = "";
                        FillAddressComboBox(dtVillage, sAddressType);
                    }
                    else if (sAddressType == "Patient")
                    {
                        txtPatientVill.Text = "";
                        txtPatientMandal.Text = "";
                        txtPatientDistrict.Text = "";
                        txtPatientState.Text = "";
                        txtPatientPin.Text = "";
                        //strStateCode = "";
                        FillAddressComboBox(dtVillage, sAddressType);
                    }
                }

                else
                {
                    htParam = new Hashtable();
                    htParam.Add("sVillage", "%" + sSearch);
                    htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = objInvoiceData.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    FillAddressComboBox(dtVillage, sAddressType);
                    ClearVillageDetails(sAddressType);
                }
                Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //objData = null;
                Cursor.Current = Cursors.Default;

            }

        }

        private void txtPatientVilSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtPatientVilSearch.Text.Length == 0)
                {
                    cbPatientVillage.DataSource = null;
                    cbPatientVillage.DataBindings.Clear();
                    cbPatientVillage.Items.Clear();
                    //if (btnSave.Enabled == true)
                    ClearVillageDetails("Patient");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch("Patient") == false)
                    {
                        FillAddressData(txtPatientVilSearch.Text, "Patient");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void FillAddressComboBox(DataTable dt, string sAddressType)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));
            dataTable.Columns.Add("Display", typeof(String));


            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + 
                     "", dt.Rows[i]["Pin"] + 
                     "", dt.Rows[i]["Display"] + ""});

            if (sAddressType == "Camp")
            {
                cbCampVillage.DataBindings.Clear();
                cbCampVillage.DataSource = dataTable;
                cbCampVillage.DisplayMember = "Display";
                cbCampVillage.ValueMember = "StateID";
            }
            else if (sAddressType == "Patient")
            {
                cbPatientVillage.DataBindings.Clear();
                cbPatientVillage.DataSource = dataTable;
                cbPatientVillage.DisplayMember = "Display";
                cbPatientVillage.ValueMember = "StateID";
            }
        }

        private void cbCampVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCampVillage.SelectedIndex > -1)
            {
                if (this.cbCampVillage.Items[cbCampVillage.SelectedIndex].ToString() != "")
                {
                    txtCampVillage.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtCampMandal.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtCampDistrict.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtCampState.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtCampPin.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    //strStateCode = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[0] + "";

                    txtPatientVill.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtPatientMandal.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtPatientDistrict.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtPatientState.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPatientPin.Text = ((System.Data.DataRowView)(this.cbCampVillage.Items[cbCampVillage.SelectedIndex])).Row.ItemArray[5] + "";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                int iRes = 0;
                iRes = SaveData();
                if (iRes > 0)
                {
                    MessageBox.Show("Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btnCancel_Click(null,null);
                    txtPatientSlno.Focus();
                    txtPatientSlno.Text = "";
                    txtPatientName.Text = "";
                    txtPatientFatherName.Text = "";
                    txtPatientHNo.Text = "";
                    txtPatientLandmark.Text = "";
                    //txtPatientVill.Text = "";
                    //txtPatientVilSearch.Text = "";
                    //txtPatientMandal.Text = "";
                    //txtPatientDistrict.Text = "";
                    //txtPatientState.Text = "";
                    //txtPatientPin.Text = "";
                    txtPatientAge.Text = "";
                    updateFlag = false;
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private int SaveData()
        {
            string strSQL = "";
            int iRes=0;
            objDb = new SQLDB();
            try
            {
                if (!updateFlag)
                {
                    strSQL = " INSERT INTO FOUNDATION_EYE_CAMP_HEAD("+
                                                "FECH_TRN_TYPE,"+
                                                "FECH_CAMP_OP_NO,"+
                                                "FECH_CAMP_OP_SL_NO,"+
                                                "FECH_CAMP_DATE,"+
                                                "FECH_CAMP_VILLAGE,"+
                                                "FECH_CAMP_MANDAL,"+
                                                "FECH_CAMP_DIST,"+
                                                "FECH_CAMP_STATE,"+
                                                "FECH_CAMP_PIN,"+
                                                "FECH_OP_NAME,"+
                                                "FECH_FOH,"+
                                                "FECH_FOH_NAME,"+
                                                "FECH_HNO,"+
                                                "FECH_LANDMARK,"+
                                                "FECH_VILLAGE,"+
                                                "FECH_MANDAL,"+
                                                "FECH_DIST,"+
                                                "FECH_STATE,"+
                                                "FECH_PIN,"+
                                                "FECH_AGE,"+
                                                "FECH_SEX,"+
                                                "FECH_CREATED_BY,"+
                                                "FECH_CREATED_DATE) "+
                                                "VALUES('EYE CAMP',"+Convert.ToInt32(txtOpNo.Text)+
                                                ","+Convert.ToInt32(txtPatientSlno.Text)+
                                                ",'"+dtpCampDate.Value.ToString("dd/MMM/yyyy")+
                                                "','"+txtCampVillage.Text.ToUpper()+
                                                "','" + txtCampMandal.Text.ToUpper() +
                                                "','" + txtCampDistrict.Text.ToUpper() +
                                                "','" + txtCampState.Text.ToUpper() +
                                                "','" + txtCampPin.Text +
                                                "','" + txtPatientName.Text.ToUpper() +
                                                "','" + cbRelation.SelectedItem.ToString().ToUpper() +
                                                "','" + txtPatientFatherName.Text.ToUpper() +
                                                "','" + txtPatientHNo.Text.ToUpper() +
                                                "','" + txtPatientLandmark.Text.ToUpper() +
                                                "','" + txtPatientVill.Text.ToUpper() +
                                                "','" + txtPatientMandal.Text.ToUpper() +
                                                "','" + txtPatientDistrict.Text.ToUpper() +
                                                "','" + txtPatientState.Text.ToUpper() +
                                                "','" + txtPatientPin.Text+
                                                "'," +Convert.ToInt32(txtPatientAge.Text) +
                                                ",'" + cbMORF.SelectedItem.ToString().ToUpper() +
                                                "','" + CommonData.LogUserId+
                                                "',getdate())";
                }
                else
                {
                    strSQL = " UPDATE FOUNDATION_EYE_CAMP_HEAD SET FECH_CAMP_OP_NO=" + Convert.ToInt32(txtOpNo.Text) +
                                                ",FECH_CAMP_OP_SL_NO="+Convert.ToInt32(txtPatientSlno.Text)+
                                                ",FECH_CAMP_DATE='"+dtpCampDate.Value.ToString("dd/MMM/yyyy")+
                                                "',FECH_CAMP_VILLAGE='" + txtCampVillage.Text.ToUpper() +
                                                "',FECH_CAMP_MANDAL='" + txtCampMandal.Text.ToUpper() +
                                                "',FECH_CAMP_DIST='" + txtCampDistrict.Text.ToUpper() +
                                                "',FECH_CAMP_STATE='" + txtCampState.Text.ToUpper() +
                                                "',FECH_CAMP_PIN='" + txtCampPin.Text.ToUpper() +
                                                "',FECH_OP_NAME='" + txtPatientName.Text.ToUpper() +
                                                "',FECH_FOH='" + cbRelation.SelectedItem.ToString().ToUpper() +
                                                "',FECH_FOH_NAME='" + txtPatientFatherName.Text.ToUpper() +
                                                "',FECH_HNO='" + txtPatientHNo.Text.ToUpper() +
                                                "',FECH_LANDMARK='" + txtPatientLandmark.Text.ToUpper() +
                                                "',FECH_VILLAGE='" + txtPatientVill.Text.ToUpper() +
                                                "',FECH_MANDAL='" + txtPatientMandal.Text.ToUpper() +
                                                "',FECH_DIST='" + txtPatientDistrict.Text.ToUpper() +
                                                "',FECH_STATE='" + txtPatientState.Text.ToUpper() +
                                                "',FECH_PIN='"+txtPatientPin.Text +
                                                "',FECH_AGE="+Convert.ToInt32(txtPatientAge.Text) +
                                                ",FECH_SEX='" + cbMORF.SelectedItem.ToString().ToUpper() +
                                                "',FECH_MODIFIED_BY='" + CommonData.LogUserId +
                                                "',FECH_MODIFIED_DATE=getdate() WHERE FECH_TRN_ID=" + iTrnNo;
                }
                iRes = objDb.ExecuteSaveData(strSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
            return iRes;
        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtOpNo.Text.Length == 0)
            {
                MessageBox.Show("Enter OP No", "Eye Camp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtCampVillage.Text.Length == 0)
            {
                MessageBox.Show("Enter Camp Address", "Eye Camp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPatientSlno.Text.Length == 0)
            {
                MessageBox.Show("Enter Patient Sl No", "Eye Camp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPatientAge.Text.Length == 0)
            {
                MessageBox.Show("Enter Patient Age", "Eye Camp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPatientVill.Text.Length == 0)
            {
                MessageBox.Show("Enter Patient Address", "Eye Camp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return flag;
        }

        //private int GenerateTrnNo()
        //{
        //    objDb = new SQLDB();
        //    DataTable dt=null;
        //    int  iTrnNo=0;
        //    string strCommand = "";
        //    try
        //    {
        //        strCommand = "SELECT isNull(MAX(FECH_TRN_ID)+1,'1')  AS TRNNO FROM FOUNDATION_EYE_CAMP_HEAD ";
        //        dt = objDb.ExecuteDataSet(strCommand).Tables[0];
        //         iTrnNo = Convert.ToInt32( dt.Rows[0][0].ToString());
                    
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
              
        //        dt = null;
        //        objDb = null;
        //    }
        //    return iTrnNo;
        //}


        private DataSet GetCampPatientDetails(int opNo, int opSlNo)
        { DataSet ds=null;
            try
            {
                objDb = new SQLDB();
                SqlParameter[] param = new SqlParameter[2];
                ds = new DataSet();
                try
                {

                    param[0] = objDb.CreateParameter("@xopNo", DbType.Int32, opNo, ParameterDirection.Input);
                    param[1] = objDb.CreateParameter("@xopSlNo", DbType.Int32, opSlNo, ParameterDirection.Input);
                    ds = objDb.ExecuteDataSet("GetFoundationCampData", CommandType.StoredProcedure, param);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    param = null;
                    objDb = null;
                }
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
            return ds;
        }

        private void txtPatientSlno_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOpNo.Text.Length > 0 && txtPatientSlno.Text.Length > 0)
            {
                DataTable dt = new DataTable();
                dt = GetCampPatientDetails(Convert.ToInt32(txtOpNo.Text), Convert.ToInt32(txtPatientSlno.Text)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    updateFlag = true;
                    iTrnNo = Convert.ToInt32(dt.Rows[0]["trnId"]);
                    txtCampVillageSearch.Text = dt.Rows[0]["campVill"] + "";
                    txtCampVillage.Text = dt.Rows[0]["campVill"] + "";
                    txtCampMandal.Text = dt.Rows[0]["campMandal"] + "";
                    txtCampDistrict.Text = dt.Rows[0]["campDist"] + "";
                    txtCampState.Text = dt.Rows[0]["campState"] + "";
                    txtCampPin.Text = dt.Rows[0]["campPin"] + "";
                    dtpCampDate.Value = Convert.ToDateTime(dt.Rows[0]["campDate"] + "");
                    //txtCampLandMark.Text = dt.Rows[0][""] + "";
                    //txtPatientSlno.Text = dt.Rows[0][""] + "";
                    txtPatientName.Text = dt.Rows[0]["patientName"] + "";
                    cbRelation.SelectedItem = dt.Rows[0]["patientRelation"] + "";
                    txtPatientFatherName.Text = dt.Rows[0]["patientFoHName"] + "";
                    txtPatientAge.Text = dt.Rows[0]["patientAge"] + "";
                    cbMORF.SelectedItem = dt.Rows[0]["patientSex"] + "";
                    txtPatientHNo.Text = dt.Rows[0]["patientHno"] + "";
                    txtPatientLandmark.Text = dt.Rows[0]["patientLandmark"] + "";
                    txtPatientVill.Text = dt.Rows[0]["patientVillage"] + "";
                    txtPatientVilSearch.Text = dt.Rows[0]["patientVillage"] + "";
                    txtPatientMandal.Text = dt.Rows[0]["patientMandal"] + "";
                    txtPatientDistrict.Text = dt.Rows[0]["patientDist"] + "";
                    txtPatientState.Text = dt.Rows[0]["patientSate"] + "";
                    txtPatientPin.Text = dt.Rows[0]["patientPin"] + "";
                }
                else
                {
                    iTrnNo = 0;
                    updateFlag = false;
                    txtPatientName.Text = "";
                    txtPatientFatherName.Text = "";
                    txtPatientHNo.Text = "";
                    //txtPatientLandmark.Text = "";
                    //txtPatientVill.Text = "";
                    //txtPatientVilSearch.Text = "";
                    //txtPatientMandal.Text = "";
                    //txtPatientDistrict.Text = "";
                    //txtPatientState.Text = "";
                    //txtPatientPin.Text = "";
                    txtPatientAge.Text = "";
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(updateFlag==true)
            {
                string strSQL = "";
                int iRes = 0;
                objDb = new SQLDB();
                try
                {
                    strSQL = "DELETE FOUNDATION_EYE_CAMP_HEAD WHERE FECH_TRN_ID="+iTrnNo;
                    iRes = objDb.ExecuteSaveData(strSQL);
                    if(iRes>0)
                    {
                        updateFlag = false;
                        MessageBox.Show("Deleted Successfully", "Eye Camp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnCancel_Click(null,null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                }
            }
        }

        private void txtOpNo_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtOpNo.Text.Length>0)
            {
                DataTable dt = new DataTable();
                dt = GetCampPatientDetails(Convert.ToInt32(txtOpNo.Text), 0).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtCampVillageSearch.Text = dt.Rows[0]["campVill"] + "";
                    txtCampVillage.Text = dt.Rows[0]["campVill"] + "";
                    txtCampMandal.Text = dt.Rows[0]["campMandal"] + "";
                    txtCampDistrict.Text = dt.Rows[0]["campDist"] + "";
                    txtCampState.Text = dt.Rows[0]["campState"] + "";
                    txtCampPin.Text = dt.Rows[0]["campPin"] + "";
                    dtpCampDate.Value = Convert.ToDateTime(dt.Rows[0]["campDate"] + "");
                }
                else
                {
                    txtCampVillageSearch.Text =  "";
                    txtCampVillage.Text =  "";
                    txtCampMandal.Text =  "";
                    txtCampDistrict.Text = "";
                    txtCampState.Text = "";
                    txtCampPin.Text =  "";
                }
            }
        }

        private void cbPatientVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPatientVillage.SelectedIndex > -1)
            {
                if (this.cbPatientVillage.Items[cbPatientVillage.SelectedIndex].ToString() != "")
                {

                    txtPatientVill.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[cbPatientVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtPatientMandal.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[cbPatientVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtPatientDistrict.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[cbPatientVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtPatientState.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[cbPatientVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPatientPin.Text = ((System.Data.DataRowView)(this.cbPatientVillage.Items[cbPatientVillage.SelectedIndex])).Row.ItemArray[5] + "";
                }
            }
        }
    }
}
