using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Collections;
using SSTrans;
using SSAdmin;

namespace SSCRM
{
    public partial class LawyerMaster : Form
    {
        Master objMstr = null;
        private InvoiceDB objInvoiceData = null;
        LeagalCaseDetails lcd=null;
        string strName="";
        bool flagUpdate = false;
        public LawyerMaster()
        {
            InitializeComponent();
        }
        public LawyerMaster(LeagalCaseDetails lc,string sName)
        {
            InitializeComponent();
            strName = sName;
            lcd = lc;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LawyerMaster_Load(object sender, EventArgs e)
        {
            GenerateNewLawyerId();
            txtLawyerName.Text = strName;
        }

        private void GenerateNewLawyerId()
        {
            SQLDB objDb = new SQLDB();
            DataSet ds = new DataSet();
            DataTable dt;
            try
            {
                string strCommand = "SELECT ISNULL(MAX(LLM_LAWYER_ID), 0)+1 from LEGAL_LAWYER_MASTER";
                ds = objDb.ExecuteDataSet(strCommand);
                dt = ds.Tables[0];
                txtLawyerId.Text = Convert.ToInt32(dt.Rows[0][0]).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ds = null;
                dt = null;
                objDb = null;
            }        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            GenerateNewLawyerId();
            txtLawyerName.Text = "";
            txtPresentHNo.Text = "";
            txtPresentLandMark.Text = "";
            txtPresentVill.Text = "";
            txtPresentVillSearch.Text = "";
            cbPresentVill.DataSource = null;
            txtPresentMandal.Text = "";
            txtPresentDistricit.Text = "";
            txtPresentState.Text = "";
            txtPresentPin.Text = "";
            txtPresentPhone.Text = "";

            chkPermanentAddress.CheckState = CheckState.Unchecked;
            txtPermantHNo.Text = "";
            txtPermantLandMark.Text = "";
            txtPermantVill.Text = "";
            txtPermantVillSearch.Text = "";
            cbPermantVill.DataSource = null;
            txtPermantMandal.Text = "";
            txtPermantDistricit.Text = "";
            txtPermantState.Text = "";
            txtPermantPin.Text = "";
            txtPermantPhone.Text = "";



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if (SaveLawyerMaster() > 0)
                {
                    MessageBox.Show("Data Added Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(lcd != null)
                    {
                    lcd.FillLawyerData("0");
                    lcd.cbLawyerName.SelectedValue = txtLawyerId.Text;
                    }
                    flagUpdate = false;
                    btnClear_Click(null, null);
                    this.Close();

                }
            }
        }

        private int SaveLawyerMaster()
        {
            int iSave = 0;
            if (txtPermantPin.Text == "")
            {
                txtPermantPin.Text = "0";
            }
            if(txtPresentPin.Text=="")
            {
                txtPresentPin.Text = "0";
            }
            string strSQL = "";
            try
            {
                if (flagUpdate == false)
                {
                    strSQL = " INSERT INTO LEGAL_LAWYER_MASTER(LLM_LAWYER_ID" +
                                                                    ",LLM_LAWYER_NAME" +
                                                                    ",LLM_ADD_PRES_ADDR_HNO" +
                                                                    ",LLM_ADD_PRES_ADDR_LANDMARK" +
                                                                    ",LLM_ADD_PRES_ADDR_VILL_OR_TOWN" +
                                                                    ",LLM_ADD_PRES_ADDR_MANDAL" +
                                                                    ",LLM_ADD_PRES_ADDR_DISTRICT" +
                                                                    ",LLM_ADD_PRES_ADDR_STATE" +
                                                                    ",LLM_ADD_PRES_ADDR_PIN" +
                                                                    ",LLM_ADD_PRES_ADDR_PHONE" +
                                                                    ",LLM_ADD_PERM_ADDR_HNO" +
                                                                    ",LLM_ADD_PERM_ADDR_LANDMARK" +
                                                                    ",LLM_ADD_PERM_ADDR_VILL_OR_TOWN" +
                                                                    ",LLM_ADD_PERM_ADDR_MANDAL" +
                                                                    ",LLM_ADD_PERM_ADDR_DISTRICT" +
                                                                    ",LLM_ADD_PERM_ADDR_STATE" +
                                                                    ",LLM_ADD_PERM_ADDR_PIN" +
                                                                    ",LLM_ADD_PERM_ADDR_PHONE" +
                                                                    ",LLM_CREATED_BY" +
                                                                    ",LLM_CREATED_DATE)" +
                                                                    " values(" + txtLawyerId.Text.Trim() +
                                                                    ",'" + txtLawyerName.Text.Trim() +
                                                                    "','" + txtPresentHNo.Text +
                                                                    "','" + txtPresentLandMark.Text +
                                                                    "','" + txtPresentVill.Text +
                                                                    "','" + txtPresentMandal.Text +
                                                                    "','" + txtPresentDistricit.Text +
                                                                    "','" + txtPresentState.Text +
                                                                    "','" + txtPresentPin.Text +
                                                                    "','" + txtPresentPhone.Text +
                                                                    "','" + txtPermantHNo.Text +
                                                                    "','" + txtPermantLandMark.Text +
                                                                    "','" + txtPermantVill.Text +
                                                                    "','" + txtPermantMandal.Text +
                                                                    "','" + txtPermantDistricit.Text +
                                                                    "','" + txtPermantState.Text +
                                                                    "','" + txtPermantPin.Text +
                                                                    "','" + txtPermantPhone.Text +
                                                                    "','" + CommonData.LogUserId +
                                                                    "',getdate())";
                }
                else
                {
                    strSQL = " update LEGAL_LAWYER_MASTER set LLM_LAWYER_NAME = '" + txtLawyerName.Text.Trim() +
                                                              "',LLM_ADD_PRES_ADDR_HNO ='" + txtPresentHNo.Text +
                                                                    "',LLM_ADD_PRES_ADDR_LANDMARK ='" + txtPresentLandMark.Text +
                                                                    "',LLM_ADD_PRES_ADDR_VILL_OR_TOWN ='" + txtPresentVill.Text +
                                                                    "',LLM_ADD_PRES_ADDR_MANDAL ='" + txtPresentMandal.Text +
                                                                    "',LLM_ADD_PRES_ADDR_DISTRICT='" + txtPresentDistricit.Text +
                                                                    "',LLM_ADD_PRES_ADDR_STATE='" + txtPresentState.Text +
                                                                    "',LLM_ADD_PRES_ADDR_PIN='" + txtPresentPin.Text +
                                                                    "',LLM_ADD_PRES_ADDR_PHONE='" + txtPresentPhone.Text +
                                                                    "',LLM_ADD_PERM_ADDR_HNO='" + txtPermantHNo.Text +
                                                                    "',LLM_ADD_PERM_ADDR_LANDMARK='" + txtPermantLandMark.Text +
                                                                    "',LLM_ADD_PERM_ADDR_VILL_OR_TOWN='" + txtPermantVill.Text +
                                                                    "',LLM_ADD_PERM_ADDR_MANDAL='" + txtPermantMandal.Text +
                                                                    "',LLM_ADD_PERM_ADDR_DISTRICT='" + txtPermantDistricit.Text +
                                                                    "',LLM_ADD_PERM_ADDR_STATE='" + txtPermantState.Text +
                                                                    "',LLM_ADD_PERM_ADDR_PIN='" + txtPermantPin.Text +
                                                                    "',LLM_ADD_PERM_ADDR_PHONE='" + txtPermantPhone.Text +
                                                                    "',LLM_MODIFIED_BY='" + CommonData.LogUserId +
                                                                    "',LLM_MODIFIED_DATE=getdate() where LLM_LAWYER_ID=" + txtLawyerId.Text.Trim();
                }
                SQLDB objSqlDb = new SQLDB();
                iSave = objSqlDb.ExecuteSaveData(strSQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iSave;
        }

        private bool CheckData()
        {
            bool flag = true;
            if(txtLawyerName.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Name", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLawyerName.Focus();
                return flag;
            }
            //if (txtPresentHNo.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Present HNO.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtPresentHNo.Focus();
            //    return flag;
            //}
            //if (txtPresentMandal.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Present Village", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtPresentVillSearch.Focus();
            //    return flag;
            //}
            //if (txtPresentPhone.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Present Phone Number", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtPresentPhone.Focus();
            //    return flag;
            //}
            //if (txtPermantHNo.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Permenant HNO.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtPermantHNo.Focus();
            //    return flag;
            //}
            //if (txtPermantMandal.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Permenant Village", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtPermantVillSearch.Focus();
            //    return flag;
            //}
            if (txtPermantPhone.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Permenant Phone Number", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPermantPhone.Focus();
                return flag;
            }
            return flag;
        }

        private void chkPermanentAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPermanentAddress.CheckState == CheckState.Checked)
            {
                txtPermantHNo.Text = txtPresentHNo.Text;
                txtPermantLandMark.Text = txtPresentLandMark.Text;
                txtPermantVill.Text = txtPresentVill.Text;
                txtPermantMandal.Text = txtPresentMandal.Text;
                txtPermantDistricit.Text = txtPresentDistricit.Text;
                txtPermantState.Text = txtPresentState.Text;
                txtPermantPin.Text = txtPresentPin.Text;
                txtPermantPhone.Text = txtPresentPhone.Text;
            }
            else
            {
                txtPermantHNo.Text = "";
                txtPermantLandMark.Text = "";
                txtPermantVill.Text = "";
                txtPermantMandal.Text = "";
                txtPermantDistricit.Text = "";
                txtPermantState.Text = "";
                txtPermantPin.Text = "";
                txtPermantPhone.Text = "";
            }
        }

        private void txtPresentVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtPresentVillSearch.Text.Length == 0)
                {
                    cbPresentVill.DataSource = null;
                    cbPresentVill.DataBindings.Clear();
                    cbPresentVill.Items.Clear();
                    //if (btnSave.Enabled == true)
                    ClearVillageDetails("Present");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch("Present") == false)
                    {
                        FillAddressData("Present",txtPresentVillSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FillAddressData(string sAddressType, string sSearch)
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
                    if (sAddressType == "Present")
                    {
                        txtPresentVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                        txtPresentMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                        txtPresentDistricit.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                        txtPresentState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                        txtPresentPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    }
                    else 
                    {
                        txtPermantVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                        txtPermantMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                        txtPermantDistricit.Text = dtVillage.Rows[0]["District"].ToString();
                        txtPermantState.Text = dtVillage.Rows[0]["State"].ToString();
                        txtPermantPin.Text = dtVillage.Rows[0]["PIN"].ToString();
                    }
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    if (sAddressType == "Present")
                    {
                        txtPresentVill.Text = "";
                        txtPresentMandal.Text = "";
                        txtPresentDistricit.Text = "";
                        txtPresentState.Text = "";
                        txtPresentPin.Text = "";
                        FillAddressComboBox(dtVillage, sAddressType);
                    }
                    else
                    {
                        txtPermantVill.Text = "";
                        txtPermantMandal.Text = "";
                        txtPermantDistricit.Text = "";
                        txtPermantState.Text = "";
                        txtPermantPin.Text = "";
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
                Cursor.Current = Cursors.Default;
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


            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});

            if (sAddressType == "Present")
            {
                cbPresentVill.DataBindings.Clear();
                cbPresentVill.DataSource = dataTable;
                cbPresentVill.DisplayMember = "Panchayath";
                cbPresentVill.ValueMember = "StateID";
            }
            else 
            {
                cbPermantVill.DataBindings.Clear();
                cbPermantVill.DataSource = dataTable;
                cbPermantVill.DisplayMember = "Panchayath";
                cbPermantVill.ValueMember = "StateID";
            }
        }

        private bool FindInputAddressSearch(string sAddressType)
        {
            bool blFind = false;
            try
            {
                if (sAddressType == "Present")
                    for (int i = 0; i < this.cbPresentVill.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbPresentVill.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtPresentVillSearch.Text) > -1)
                        {
                            blFind = true;
                            cbPresentVill.SelectedIndex = i;
                            txtPresentVill.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[i])).Row.ItemArray[1] + "";
                            txtPresentMandal.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[i])).Row.ItemArray[2] + "";
                            txtPresentDistricit.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[i])).Row.ItemArray[3] + "";
                            txtPresentState.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[i])).Row.ItemArray[4] + "";
                            txtPresentPin.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[i])).Row.ItemArray[5] + "";
                            break;
                        }
                        break;
                    }
                else 
                    for (int i = 0; i < this.cbPermantVill.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbPermantVill.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtPermantVillSearch.Text) > -1)
                        {
                            blFind = true;
                            cbPermantVill.SelectedIndex = i;
                            txtPermantVill.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[i])).Row.ItemArray[1] + "";
                            txtPermantMandal.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[i])).Row.ItemArray[2] + "";
                            txtPermantDistricit.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[i])).Row.ItemArray[3] + "";
                            txtPermantState.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[i])).Row.ItemArray[4] + "";
                            txtPermantPin.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[i])).Row.ItemArray[5] + "";
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

        private void ClearVillageDetails(string sAddressType)
        {
            if (sAddressType == "Firm")
            {
                txtPresentVill.Text = "";
                txtPresentMandal.Text = "";
                txtPresentDistricit.Text = "";
                txtPresentState.Text = "";
                txtPresentPin.Text = "";
            }
            else
            {
                txtPermantVill.Text = "";
                txtPermantMandal.Text = "";
                txtPermantDistricit.Text = "";
                txtPermantState.Text = "";
                txtPermantPin.Text = "";
            }
           
        }

        private void cbPresentVill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPresentVill.SelectedIndex > -1)
            {
                if (this.cbPresentVill.Items[cbPresentVill.SelectedIndex].ToString() != "")
                {
                    txtPresentVill.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[cbPresentVill.SelectedIndex])).Row.ItemArray[1] + "";
                    txtPresentMandal.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[cbPresentVill.SelectedIndex])).Row.ItemArray[2] + "";
                    txtPresentDistricit.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[cbPresentVill.SelectedIndex])).Row.ItemArray[3] + "";
                    txtPresentState.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[cbPresentVill.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPresentPin.Text = ((System.Data.DataRowView)(this.cbPresentVill.Items[cbPresentVill.SelectedIndex])).Row.ItemArray[5] + "";
                }
            }
        }

        private void cbPermantVill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPermantVill.SelectedIndex > -1)
            {
                if (this.cbPermantVill.Items[cbPermantVill.SelectedIndex].ToString() != "")
                {
                    txtPermantVill.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[cbPermantVill.SelectedIndex])).Row.ItemArray[1] + "";
                    txtPermantMandal.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[cbPermantVill.SelectedIndex])).Row.ItemArray[2] + "";
                    txtPermantDistricit.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[cbPermantVill.SelectedIndex])).Row.ItemArray[3] + "";
                    txtPermantState.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[cbPermantVill.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPermantPin.Text = ((System.Data.DataRowView)(this.cbPermantVill.Items[cbPermantVill.SelectedIndex])).Row.ItemArray[5] + "";
                }
            }
        }

        private void btnPresentVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("PresentLawyer");
            VSearch.objLawyerMaster = this;
            VSearch.ShowDialog();
        }

        private void btnPermantVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("PermantLawyer");
            VSearch.objLawyerMaster = this;
            VSearch.ShowDialog();
        }

        private void txtPermantVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtPermantVillSearch.Text.Length == 0)
                {
                    cbPermantVill.DataSource = null;
                    cbPermantVill.DataBindings.Clear();
                    cbPermantVill.Items.Clear();
                    //if (btnSave.Enabled == true)
                    ClearVillageDetails("Permant");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch("Permant") == false)
                    {
                        FillAddressData("Permant", txtPermantVillSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtLawyerName_TextChanged(object sender, EventArgs e)
        {
            if (txtLawyerName.Text.Length > 0)
            {
                objMstr = new Master();
                DataSet ds = new DataSet();
                try
                {
                    ds = objMstr.GetLawyerMasterDetl(txtLawyerName.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objMstr = null;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flagUpdate = true;
                    txtLawyerId.Text = ds.Tables[0].Rows[0]["LawyerId"].ToString();
                    txtPresentHNo.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_HNO"].ToString();
                    txtPresentLandMark.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_LANDMARK"].ToString();
                    txtPresentVill.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_VILL_OR_TOWN"].ToString();
                    txtPresentMandal.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_MANDAL"].ToString();
                    txtPresentDistricit.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_DISTRICT"].ToString();
                    txtPresentState.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_STATE"].ToString();
                    txtPresentPin.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_PIN"].ToString();
                    txtPresentPhone.Text = ds.Tables[0].Rows[0]["LLM_ADD_PRES_ADDR_PHONE"].ToString();
                    txtPermantHNo.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_HNO"].ToString();
                    txtPermantLandMark.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_LANDMARK"].ToString();
                    txtPermantVill.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_VILL_OR_TOWN"].ToString();
                    txtPermantMandal.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_MANDAL"].ToString();
                    txtPermantDistricit.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_DISTRICT"].ToString();
                    txtPermantState.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_STATE"].ToString();
                    txtPermantPin.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_PIN"].ToString();
                    txtPermantPhone.Text = ds.Tables[0].Rows[0]["LLM_ADD_PERM_ADDR_PHONE"].ToString();


                }
                else
                {
                    flagUpdate = false;
                    GenerateNewLawyerId();
                    txtPresentHNo.Text = "";
                    txtPresentLandMark.Text = "";
                    txtPresentVill.Text = "";
                    txtPresentMandal.Text = "";
                    txtPresentDistricit.Text = "";
                    txtPresentState.Text = "";
                    txtPresentPin.Text = "";
                    txtPresentPhone.Text = "";
                    txtPermantHNo.Text = "";
                    txtPermantLandMark.Text = "";
                    txtPermantVill.Text = "";
                    txtPermantMandal.Text = "";
                    txtPermantDistricit.Text = "";
                    txtPermantState.Text = "";
                    txtPermantState.Text = "";
                    txtPermantPin.Text = "";
                    txtPermantPhone.Text = "";
                }
                ds = null;
            }
            else
            {
                flagUpdate = false;
                GenerateNewLawyerId();
                txtPresentHNo.Text = "";
                txtPresentLandMark.Text = "";
                txtPresentVill.Text = "";
                txtPresentMandal.Text = "";
                txtPresentDistricit.Text = "";
                txtPresentState.Text = "";
                txtPresentPin.Text = "";
                txtPresentPhone.Text = "";
                txtPermantHNo.Text = "";
                txtPermantLandMark.Text = "";
                txtPermantVill.Text = "";
                txtPermantMandal.Text = "";
                txtPermantDistricit.Text = "";
                txtPermantState.Text = "";
                txtPermantState.Text = "";
                txtPermantPin.Text = "";
                txtPermantPhone.Text = "";
            }
        }
    }
}
