using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class AttendedFarmerDetails : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        public FarmerMeetingForm objFarmerMeetingForm;
        string strfrmType = "";
        DataGridViewRow dgvr;
        string strFormerid = "";
        bool blCustomerSearch = false;
        string sVil = "";
        string sMandal = "";
        string sDistrict = "";
        string sState = "";
        string formID = "";
        bool bflag = false;

        public AttendedFarmerDetails(string sType,string strVil,string strMandal,string strDistrict,string strState,string FrmId)
        {
            InitializeComponent();
            strfrmType = sType;
            sVil = strVil;
            sMandal = strMandal;
            sDistrict = strDistrict;
            sState = strState;
            formID = FrmId;
        }
        public AttendedFarmerDetails(string sType,DataGridViewRow dgvrow)
        {
            InitializeComponent();
            bflag = true;
            strfrmType = sType;
            dgvr = dgvrow;
        }

        private void AttendedFarmerDetails_Load(object sender, EventArgs e)
        {
            cbSpotSaleBooking.SelectedIndex = 1;       
 
            if (formID.Equals("1"))
            {
                cbRelation.SelectedIndex = 0;
                txtVillage.Text = sVil;
                txtMandal.Text = sMandal;
                txtState.Text = sState;
                txtDistrict.Text = sDistrict;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private bool CheckData()
        {
            bool bFlag = true;

            if (txtFarmerName.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Farmer Name", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFarmerName.Focus();
                
            }
            //else if (txtHouseNo.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter HouseNo", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtHouseNo.Focus();

            //}
            //else if (txtAcrescnt.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter No Of Acres", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtAcrescnt.Focus();
            //}
            //else if (txtCropName.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter CropName", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtCropName.Focus();

            //}
            else if (txtVillage.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Village Name", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVillSearch.Focus();

            }
            else if(cbSpotSaleBooking.Text=="YES")
            {
                if (txtAmount.Text.Length == 0 || txtAmount.Text.Equals("0"))
                {
                    bFlag = false;
                    MessageBox.Show("Please Enter Spot Booking Sale Amount", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAmount.Focus();
                    return bFlag;
                }

            }
            //else if (txtMobileNo.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter Mobile Number", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMobileNo.Focus();
            //}
            //else if (txtRemarks.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter Crop Remarks", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //}

            return bFlag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvFarmerDetails = null;


            if (CheckData() == true)
            {
                if (bflag == true)
                {
                    ((FarmerMeetingForm)objFarmerMeetingForm).gvFarmerDetails.Rows.Remove(dgvr);
                    if (strfrmType == "FarmerMeetingForm")
                    {
                        dgvFarmerDetails = ((FarmerMeetingForm)objFarmerMeetingForm).gvFarmerDetails;
                        AddFarmerDetailsToGrid(dgvFarmerDetails);
                        //MessageBox.Show("Farmer Details Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnClear_Click(null, null);
                        this.Close();


                    }
                    bflag = false;
                }
                else
                {
                    if (strfrmType == "FarmerMeetingForm")
                    {
                        dgvFarmerDetails = ((FarmerMeetingForm)objFarmerMeetingForm).gvFarmerDetails;
                        AddFarmerDetailsToGrid(dgvFarmerDetails);
                        //MessageBox.Show("Farmer Details Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnClear_Click(null, null);
                        this.Close();


                    }
                   
                }
            }
        }

        private void AddFarmerDetailsToGrid(DataGridView dgvFarmerDetails)
        {
            int intRow = 0;
            intRow = dgvFarmerDetails.Rows.Count + 1;

            DataGridViewRow tempRow = new DataGridViewRow();
            double dAmt = 0;
            if (txtAmount.Text.Length > 0)
            {
                dAmt = Convert.ToDouble(txtAmount.Text);
            }
            else
            {
                dAmt = 0;
            }

            DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
            cellSlNo.Value = intRow;
            tempRow.Cells.Add(cellSlNo);                       

            DataGridViewCell cellFarmerName = new DataGridViewTextBoxCell();
            cellFarmerName.Value = txtFarmerName.Text.ToString().Replace("'"," ");
            tempRow.Cells.Add(cellFarmerName);
          
            DataGridViewCell cellForhRel = new DataGridViewTextBoxCell();
            cellForhRel.Value = cbRelation.Text.ToString();
            tempRow.Cells.Add(cellForhRel);

            DataGridViewCell cellForhRelName = new DataGridViewTextBoxCell();
            cellForhRelName.Value = txtRelationName.Text.ToString().Replace("'"," ");
            tempRow.Cells.Add(cellForhRelName);


            DataGridViewCell cellHouseNo = new DataGridViewTextBoxCell();
            cellHouseNo.Value = txtHouseNo.Text.ToString();
            tempRow.Cells.Add(cellHouseNo);

            DataGridViewCell cellLandMark = new DataGridViewTextBoxCell();
            cellLandMark.Value = txtLandMark.Text.ToString().Replace("'"," ");
            tempRow.Cells.Add(cellLandMark);

            DataGridViewCell cellVillage = new DataGridViewTextBoxCell();
            cellVillage.Value = txtVillage.Text.ToString();
            tempRow.Cells.Add(cellVillage);

            DataGridViewCell cellMandal = new DataGridViewTextBoxCell();
            cellMandal.Value = txtMandal.Text.ToString();
            tempRow.Cells.Add(cellMandal);

            DataGridViewCell cellDistrict = new DataGridViewTextBoxCell();
            cellDistrict.Value = txtDistrict.Text.ToString();
            tempRow.Cells.Add(cellDistrict);

            DataGridViewCell cellState = new DataGridViewTextBoxCell();
            cellState.Value = txtState.Text.ToString();
            tempRow.Cells.Add(cellState);

            DataGridViewCell cellPin = new DataGridViewTextBoxCell();
            cellPin.Value = txtPin.Text.ToString();
            tempRow.Cells.Add(cellPin);

            DataGridViewCell cellMobileNo = new DataGridViewTextBoxCell();
            cellMobileNo.Value = txtMobileNo.Text;
            tempRow.Cells.Add(cellMobileNo);

            DataGridViewCell cellAcres = new DataGridViewTextBoxCell();
            cellAcres.Value = txtAcrescnt.Text.ToString();
            tempRow.Cells.Add(cellAcres);

            DataGridViewCell cellCropName = new DataGridViewTextBoxCell();
            cellCropName.Value = txtCropName.Text.ToString().Replace("'"," ");
            tempRow.Cells.Add(cellCropName);

            DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
            cellRemarks.Value = txtRemarks.Text.ToString().Replace("'","");
            tempRow.Cells.Add(cellRemarks);

            DataGridViewCell cellSpotSaleFlag = new DataGridViewTextBoxCell();
            cellSpotSaleFlag.Value = cbSpotSaleBooking.Text.ToString();
            tempRow.Cells.Add(cellSpotSaleFlag);

            DataGridViewCell cellInvNo = new DataGridViewTextBoxCell();
            cellInvNo.Value = txtInvNo.Text.ToString();
            tempRow.Cells.Add(cellInvNo);

            DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
            cellAmount.Value = dAmt;
            tempRow.Cells.Add(cellAmount);
          

            intRow = intRow + 1;
            dgvFarmerDetails.Rows.Add(tempRow);
        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch vilsearch = new VillageSearch("AttendedFarmerDetails");
            vilsearch.objAttendedFarmerDetails = this;
            vilsearch.ShowDialog();
        }

        private void txtAcrescnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != '\b' && (e.KeyChar != '.'))
            //{
            //    if (!char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
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

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar!=',')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = Char.ToUpper(e.KeyChar);
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsLetter(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}

        }

      

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFarmerName.Text = "";
            txtDistrict.Text = "";
            txtCropName.Text = "";
            txtAcrescnt.Text = "";
            txtHouseNo.Text = "";
            txtLandMark.Text = "";
            txtMobileNo.Text = "";
            txtPin.Text = "";
            txtState.Text = "";
            txtVillage.Text = "";
            txtVillSearch.Text = "";
            //cbVillage.SelectedIndex = 0;
            txtMandal.Text = "";
            txtRemarks.Text = "";

        }

        private void txtCropName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsLetter(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void txtLandMark_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsLetter(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}

        }

        //private bool FindInputAddressSearch()
        //{
        //    bool blFind = false;
        //    try
        //    {
               
        //            for (int i = 0; i < this.cbVillage.Items.Count; i++)
        //            {
        //                string strItem = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
        //                if (strItem.IndexOf(txtVillSearch.Text) > -1)
        //                {
        //                    blFind = true;
        //                    cbVillage.SelectedIndex = i;
        //                    txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "";
        //                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[2] + "";
        //                    txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[3] + "";
        //                    txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[4] + "";
        //                    txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[5] + "";
                            
        //                    break;
        //                }
        //                break;
        //            }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //    }
        //    return blFind;
        //}

        private void FillAddressData(string svilsearch)
        {
            Hashtable htParam = null;
            objServicedb = new ServiceDeptDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            if (txtVillSearch.Text != "")
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (svilsearch.Trim().Length >= 0)

                        htParam = new Hashtable();
                    htParam.Add("sVillage", svilsearch);
                    htParam.Add("sDistrict", strDist);



                    htParam.Add("sCDState", CommonData.StateCode);
                    dsVillage = new DataSet();
                    dsVillage = objServicedb.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    if (dtVillage.Rows.Count == 1)
                    {
                        txtVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                        txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                        txtState.Text = dtVillage.Rows[0]["State"].ToString();
                        txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();

                    }
                    else if (dtVillage.Rows.Count > 1)
                    {
                        txtVillage.Text = "";
                        txtMandal.Text = "";
                        txtDistrict.Text = "";
                        txtState.Text = "";
                        txtPin.Text = "";

                        FillAddressComboBox(dtVillage);
                    }

                    else
                    {
                        htParam = new Hashtable();
                        htParam.Add("sVillage", "%" + svilsearch);
                        htParam.Add("sDistrict", strDist);
                        dsVillage = new DataSet();
                        dsVillage = objServicedb.GetVillageDataSet(htParam);
                        dtVillage = dsVillage.Tables[0];
                        FillAddressComboBox(dtVillage);


                    }


                    Cursor.Current = Cursors.Default;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objServicedb = null;
                    Cursor.Current = Cursors.Default;

                }
            }
            else
            {

                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
            }

        }
        private void FillAddressComboBox(DataTable dt)
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

            cbVillage.DataBindings.Clear();
            cbVillage.DataSource = dataTable;
            cbVillage.DisplayMember = "Panchayath";
            cbVillage.ValueMember = "StateID";

        }

        private void txtVillSearch_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                if (txtVillSearch.Text.Length == 0)
                {
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();
                    //txtVillage.Text = "";
                    //txtState.Text = "";
                    //txtDistrict.Text = "";
                    //txtMandal.Text = "";
                    //txtPin.Text = "";

                }

                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    FillAddressData(txtVillSearch.Text);
                }   
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
           
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > -1)
            {
                if (this.cbVillage.Items[cbVillage.SelectedIndex].ToString() != "")
                {
                    txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    
                }
            }
        }

        private void txtFarmerSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtFarmerSearch.Text.Length > 0)
                {
                    blCustomerSearch = true;
                    if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    {
                        FillCustomerFarmerData();

                    }
                }
            }
            catch (Exception ex)
            {

            }         
                //if (this.txtFarmerSearch.TextLength >= 2)
                //    FillCustomerFarmerData();
                //this.txtFarmerName.SelectionStart = this.txtFarmerSearch.TextLength;            
        }

      


        private void FillCustomerFarmerData()
        {
            DataSet ds = null;
            objServicedb = new ServiceDeptDB();
            try
            {
                if (txtVillage.Text!= "")
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ds = new DataSet();
                    ds = objServicedb.CustomersInVillage_Get(txtVillage.Text, txtMandal.Text, txtDistrict.Text, txtState.Text);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count == 1)
                    {

                        txtFarmerName.Text = dt.Rows[0]["CUSTOMER_NAME"] + "";
                        cbRelation.Text = dt.Rows[0]["SO_FO"] + "";
                        txtRelationName.Text = dt.Rows[0]["FORG_NAME"] + "";
                        txtMobileNo.Text = dt.Rows[0]["MOBILE"] + "";

                    }
                    else if (dt.Rows.Count > 1)
                    {
                        FillCustomerComboBox(dt);
                    }
                    else
                    {
                        cbFarmers.DataSource = null;
                        cbFarmers.DataBindings.Clear();
                        cbFarmers.Items.Clear();
                        txtFarmerName.Text = "";
                        txtRelationName.Text = "";
                        txtMobileNo.Text = "";


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objServicedb = null;
                ds.Dispose();
                Cursor.Current = Cursors.Default;
            }
        }

        private void FillCustomerComboBox(DataTable dt)
        {
            DataTable dataCustomer = new DataTable("Customer");
            dataCustomer.Columns.Add("farmer_ID", typeof(String));
            dataCustomer.Columns.Add("farmer_name", typeof(String));           
            dataCustomer.Columns.Add("forg_relation", typeof(String));
            dataCustomer.Columns.Add("forg_name", typeof(String));

            cbFarmers.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataCustomer.Rows.Add(new String[] { dt.Rows[i]["CUSTOMER_ID"] + 
                     "", dt.Rows[i]["CUSTOMER_NAME"] +  
                     "", dt.Rows[i]["SO_FO"] + "", dt.Rows[i]["FORG_NAME"] +""});


            cbFarmers.DataSource = dataCustomer;
            cbFarmers.DisplayMember = "farmer_name";
            cbFarmers.ValueMember = "farmer_ID";
        }

        private void cbFarmers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFarmers.SelectedIndex > -1)
            {
                if (this.cbFarmers.Items[cbFarmers.SelectedIndex].ToString() != "")
                {
                    txtFarmerName.Text = ((System.Data.DataRowView)(this.cbFarmers.Items[cbFarmers.SelectedIndex])).Row.ItemArray[1] + "";
                    cbRelation.Text = ((System.Data.DataRowView)(this.cbFarmers.Items[cbFarmers.SelectedIndex])).Row.ItemArray[2] + "";
                    txtRelationName.Text = ((System.Data.DataRowView)(this.cbFarmers.Items[cbFarmers.SelectedIndex])).Row.ItemArray[3] + "";                    

                }
            }

        }

        private void txtVillage_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtVillage.Text != "")
            {
                FillCustomerFarmerData();
            }
        }

        private void txtFarmerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar) && !(char.IsWhiteSpace(e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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
