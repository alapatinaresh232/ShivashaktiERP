using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using SSCRMDB;
using SSAdmin;
using SSTrans;
namespace SDMS
{
    public partial class VillageSearch : Form
    {
        private StaffLevel objState = null;
        private SQLDB objData = null;
        public AddressCtrl objFrmAddress = null;
        //public Invoice objFrmInvoice;
        public DealarApplicationForm objDealarApplicationForm;
        //public PrevInvoice objFrmPrevInvoice;
        //public DoorKnocks objDoorKnocks = null;
        //public SalseORder objSalseORder = null;
        //public CustomerMaster objCustomerMaster = null;
        //public CampMasAdd objFrmCampMas = null;
        private InvoiceDB objInv = null;
        //public AdvancedRefund objAdvanced = null;
        public string strFrom;
        public VillageSearch()
        {
            InitializeComponent();
        }
        public VillageSearch(string sFrom)
        {
            InitializeComponent();
            strFrom = sFrom;
        }
        private void VillageSearch_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 120, Screen.PrimaryScreen.WorkingArea.Y + 100);
            //this.StartPosition = FormStartPosition.CenterScreen;
            FillStateComboBox();

        }

        private void FillStateComboBox()
        {
            objState = new StaffLevel();
            try
            {
                DataTable dt = objState.GetStatesDS().Tables[0];
                if (dt.Rows.Count > 1)
                {
                    cbStates.DataSource = dt;
                    cbStates.DisplayMember = "State";
                    cbStates.ValueMember = "CDState";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objState = null;
            }
        }

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbStates.SelectedIndex > 0)
            {
                cbDistrict.DataSource = null;
                cbDistrict.DataBindings.Clear();
                cbDistrict.Items.Clear();

                cbMandal.DataSource = null;
                cbMandal.DataBindings.Clear();
                cbMandal.Items.Clear();

                clbVillage.DataSource = null;
                clbVillage.DataBindings.Clear();
                clbVillage.Items.Clear();

                objInv = new InvoiceDB();
                try
                {
                    DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(),string.Empty,string.Empty).Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[0] = "Select";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 1)
                    {
                        cbDistrict.DataSource = dt;
                        cbDistrict.DisplayMember = "DISTRICT";
                        cbDistrict.ValueMember = "DISTRICT";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    objInv = null;
                }
            }
        }

        private void cbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDistrict.SelectedIndex > 0)
            {
                cbMandal.DataSource = null;
                cbMandal.DataBindings.Clear();
                cbMandal.Items.Clear();

                clbVillage.DataSource = null;
                clbVillage.DataBindings.Clear();
                clbVillage.Items.Clear();

                objInv = new InvoiceDB();
                try
                {
                    cbMandal.Items.Insert(0, new ListItem("Select", "0"));
                    DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(),cbDistrict.SelectedValue.ToString(),string.Empty).Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[0] = "Select";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 1)
                    {
                        cbMandal.DataSource = dt;
                        cbMandal.DisplayMember = "MANDAL";
                        cbMandal.ValueMember = "MANDAL";
                    }
                    dt = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    objInv = null;
                }
            }
        }

        private void cbMandal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMandal.SelectedIndex > 0)
            {
                clbVillage.DataSource = null;
                clbVillage.DataBindings.Clear();
                clbVillage.Items.Clear();

                objInv = new InvoiceDB();
                try
                {
                    DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(), cbDistrict.SelectedValue.ToString(), cbMandal.SelectedValue.ToString()).Tables[0];
                    if (dt.Rows.Count > 1)
                    {
                        clbVillage.DataSource = dt;
                        clbVillage.DisplayMember = "PANCHAYAT";
                        clbVillage.ValueMember = "PIN";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    objInv = null;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (strFrom == "Invoice")
                {
                //    ((Invoice)objFrmInvoice).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                //    ((Invoice)objFrmInvoice).txtMandal.Text = cbMandal.SelectedValue.ToString();
                //    ((Invoice)objFrmInvoice).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                //    ((Invoice)objFrmInvoice).txtState.Text = cbStates.Text.ToString();
                //    ((Invoice)objFrmInvoice).strStateCode = cbStates.SelectedValue.ToString();
                //    ((Invoice)objFrmInvoice).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "Firm")
                {
                    ((DealarApplicationForm)objDealarApplicationForm).txtFirmVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtFirmVillageSearch.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtFirmMandal.Text = cbMandal.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtFirmDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtFirmState.Text = cbStates.Text.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).strStateCode = cbStates.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtFirmPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "Dealer")
                {
                    ((DealarApplicationForm)objDealarApplicationForm).txtDealerVill.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDealerVillSearch.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDealerMandal.Text = cbMandal.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDealerDistricit.Text = cbDistrict.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDealerState.Text = cbStates.Text.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).strStateCode = cbStates.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDealerPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "DealerOperation")
                {
                    ((DealarApplicationForm)objDealarApplicationForm).txtDelOperTown.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDealerVillSearch.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDelOperMandal.Text = cbMandal.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDelOperDist.Text = cbDistrict.SelectedValue.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).txtDelOperState.Text = cbStates.Text.ToString();
                    ((DealarApplicationForm)objDealarApplicationForm).strStateCode = cbStates.SelectedValue.ToString();
                    
                }
                else if (strFrom == "SalesOrders")
                {
                    //((SalseORder)objSalseORder).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    //((SalseORder)objSalseORder).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    //((SalseORder)objSalseORder).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    //((SalseORder)objSalseORder).txtState.Text = cbStates.Text.ToString();
                    //((SalseORder)objSalseORder).strStateCode = cbStates.SelectedValue.ToString();
                    //((SalseORder)objSalseORder).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                //else if (strFrom == "CampMas")
                //{
                //    ((CampMasAdd)objFrmCampMas).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                //    ((CampMasAdd)objFrmCampMas).txtMandal.Text = cbMandal.SelectedValue.ToString();
                //    ((CampMasAdd)objFrmCampMas).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                //    ((CampMasAdd)objFrmCampMas).txtState.Text = cbStates.Text.ToString();
                //    //((SalseORder)objFrmCampMas).strStateCode = cbStates.SelectedValue.ToString();
                //    //((SalseORder)objFrmCampMas).txtPin.Text = clbVillage.SelectedValue.ToString();
                //}
                else if (strFrom == "DoorKnocks")
                {
                    //((DoorKnocks)objDoorKnocks).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    //((DoorKnocks)objDoorKnocks).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    //((DoorKnocks)objDoorKnocks).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    //((DoorKnocks)objDoorKnocks).txtState.Text = cbStates.Text.ToString();
                    //((DoorKnocks)objDoorKnocks).strStateCode = cbStates.SelectedValue.ToString();
                    //((DoorKnocks)objDoorKnocks).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "PreInvoice")
                {
                    //((PrevInvoice)objFrmPrevInvoice).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    //((PrevInvoice)objFrmPrevInvoice).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    //((PrevInvoice)objFrmPrevInvoice).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    //((PrevInvoice)objFrmPrevInvoice).txtState.Text = cbStates.Text.ToString();
                    //((PrevInvoice)objFrmPrevInvoice).strStateCode = cbStates.SelectedValue.ToString();
                    //((PrevInvoice)objFrmPrevInvoice).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "HRAddress")
                {
                    ((AddressCtrl)objFrmAddress).Village = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((AddressCtrl)objFrmAddress).Mondal = cbMandal.SelectedValue.ToString();
                    ((AddressCtrl)objFrmAddress).District = cbDistrict.SelectedValue.ToString();
                    ((AddressCtrl)objFrmAddress).State = cbStates.Text.ToString();
                    ((AddressCtrl)objFrmAddress).Pin = clbVillage.SelectedValue.ToString();
                }
                //else if (strFrom == "CustomerMaster")
                //{
                //    ((CustomerMaster)objCustomerMaster).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                //    ((CustomerMaster)objCustomerMaster).txtMandal.Text = cbMandal.SelectedValue.ToString();
                //    ((CustomerMaster)objCustomerMaster).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                //    ((CustomerMaster)objCustomerMaster).txtState.Text = cbStates.Text.ToString();
                //    ((CustomerMaster)objCustomerMaster).txtPin.Text = clbVillage.SelectedValue.ToString();
                //}
                else if (strFrom == "AdvancedRefund")
                {
                    //((AdvancedRefund)objAdvanced).txtrefVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    //((AdvancedRefund)objAdvanced).txtrefMandal.Text = cbMandal.SelectedValue.ToString();
                    //((AdvancedRefund)objAdvanced).txtrefDistrict.Text = cbDistrict.SelectedValue.ToString();
                    //((AdvancedRefund)objAdvanced).txtrefState.Text = cbStates.Text.ToString();                    
                    //((AdvancedRefund)objAdvanced).txtrefPin.Text = clbVillage.SelectedValue.ToString();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select village!", "Village Search");
            }
        }

        private void clbVillage_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (cbStates.SelectedIndex > 0 && e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < clbVillage.Items.Count; i++)
                {
                    if (e.Index != i)
                        clbVillage.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private bool CheckData()
        {
            bool blVil = false;
            for (int i = 0; i < clbVillage.Items.Count; i++)
            {
                if (clbVillage.GetItemCheckState(i) == CheckState.Checked)
                {
                    blVil = true;
                }
            }
            return blVil;
        }
    }
}
