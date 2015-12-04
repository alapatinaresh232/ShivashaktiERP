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
namespace SSCRM
{
    public partial class VillageSearch : Form
    {
        private StaffLevel objState = null;
        private SQLDB objData = null;
        public ComplainantMaster objComplainantMaster = null;
        public LawyerMaster objLawyerMaster = null;
        public LeagalCaseDetails objLegalCase = null;
        public AddressCtrl objFrmAddress = null;
        public Invoice objFrmInvoice;
        public PrevInvoice objFrmPrevInvoice;
        public DoorKnocks objDoorKnocks = null;
        public SalseORder objSalseORder = null;
        public CustomerMaster objCustomerMaster = null;
        public CampMasAdd objFrmCampMas = null;
        private InvoiceDB objInv = null;
        public AdvancedRefund objAdvanced = null;
        public AttendedFarmerDetails objAttendedFarmerDetails = null;
        public FarmerMeetingForm objFarmerMeetingForm = null;
        public EyeCampPatientDetails obEyeCampDetails;
        public frmDemoPlots objfrmDemoPlots = null;
        public frmAttendedFarmerDetails objfrmAttendedFarmerDetails = null;
        public EyeSurgeryPatientDetails objEyeSurgeryPatientDetails = null;
        public frmSchoolVisits objfrmSchoolVisits = null;
        public SVAttendedStudentDetails objSVAttendedStudentDetails = null;
        public SVGiftDetails objSVGiftDetails = null;
        public frmProductPromotion objfrmProductPromotion = null;
        public PMAttendedOthersDetails objPMAttendedOthersDetails = null;
        public MemberEnrollment objMemberEnrollment = null;
        public PFUANMaster objPFUANMaster = null;
        public SPRentalAgriments objSPRentalAgriments = null;
        public frmBranchMasterUpdate objfrmBranchMasterUpdate = null;
        public CustomerAddressUpdate objCustomerAddressUpdate = null;
        public frmWrongCommitmentOrFinancialFrauds objWCFF = null;
        public SPInvoice objFrmSPInvoice;


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
            if (strFrom == "CampDetails")
            {
                cbStates.SelectedValue = "AP";
                cbDistrict.SelectedValue = "GUNTUR";
                //cbMandal.SelectedValue = "VINUKONDA";
                cbStates.Enabled = false;
                cbDistrict.Enabled = false;
            }
            if (strFrom == "PatientDetails" || strFrom == "EyeSurgeryPatientDetails" || strFrom == "MemberEntrollMent")
            {
                cbStates.SelectedValue = "AP";
                cbDistrict.SelectedValue = "GUNTUR";
                //cbMandal.SelectedValue = "VINUKONDA";
                cbStates.Enabled = false;
                //cbDistrict.Enabled = false;
            }

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
                if (strFrom == "CampDetails")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MANDAL");
                    dt.Columns.Add("MANDAL1");
                    dt.Rows.Add("--Please Select--", "--Please Select--");
                    dt.Rows.Add("BOLLAPALLE", "BOLLAPALLE");
                    dt.Rows.Add("IPUR", "IPUR");
                    dt.Rows.Add("NUZENDLA", "NUZENDLA");
                    dt.Rows.Add("SAVALYAPURAM", "SAVALYAPURAM");
                    dt.Rows.Add("VINUKONDA", "VINUKONDA");
                    cbMandal.DataSource = null;
                    cbMandal.DataBindings.Clear();
                    cbMandal.Items.Clear();
                    cbMandal.DataSource = dt;
                    cbMandal.DisplayMember = "MANDAL";
                    cbMandal.ValueMember = "MANDAL";
                }
                if (strFrom == "MemberEntrollMent")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MANDAL");
                    dt.Columns.Add("MANDAL1");
                    dt.Rows.Add("--Please Select--", "--Please Select--");
                    dt.Rows.Add("BOLLAPALLE", "BOLLAPALLE");
                    dt.Rows.Add("IPUR", "IPUR");
                    dt.Rows.Add("NUZENDLA", "NUZENDLA");
                    dt.Rows.Add("SAVALYAPURAM", "SAVALYAPURAM");
                    dt.Rows.Add("VINUKONDA", "VINUKONDA");
                    cbMandal.DataSource = null;
                    cbMandal.DataBindings.Clear();
                    cbMandal.Items.Clear();
                    cbMandal.DataSource = dt;
                    cbMandal.DisplayMember = "MANDAL";
                    cbMandal.ValueMember = "MANDAL";
                }
                else
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
                        DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(), cbDistrict.SelectedValue.ToString(), string.Empty).Tables[0];
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
                    ((Invoice)objFrmInvoice).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((Invoice)objFrmInvoice).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((Invoice)objFrmInvoice).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((Invoice)objFrmInvoice).txtState.Text = cbStates.Text.ToString();
                    ((Invoice)objFrmInvoice).strStateCode = cbStates.SelectedValue.ToString();
                    ((Invoice)objFrmInvoice).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "SPInvoice")
                {
                    ((SPInvoice)objFrmSPInvoice).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((SPInvoice)objFrmSPInvoice).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((SPInvoice)objFrmSPInvoice).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((SPInvoice)objFrmSPInvoice).txtState.Text = cbStates.Text.ToString();
                    ((SPInvoice)objFrmSPInvoice).strStateCode = cbStates.SelectedValue.ToString();
                    ((SPInvoice)objFrmSPInvoice).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "SalesOrders")
                {
                    ((SalseORder)objSalseORder).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((SalseORder)objSalseORder).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((SalseORder)objSalseORder).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((SalseORder)objSalseORder).txtState.Text = cbStates.Text.ToString();
                    ((SalseORder)objSalseORder).strStateCode = cbStates.SelectedValue.ToString();
                    ((SalseORder)objSalseORder).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "CampMas")
                {
                    ((CampMasAdd)objFrmCampMas).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((CampMasAdd)objFrmCampMas).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((CampMasAdd)objFrmCampMas).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((CampMasAdd)objFrmCampMas).txtState.Text = cbStates.Text.ToString();
                    //((SalseORder)objFrmCampMas).strStateCode = cbStates.SelectedValue.ToString();
                    //((SalseORder)objFrmCampMas).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "DoorKnocks")
                {
                    ((DoorKnocks)objDoorKnocks).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((DoorKnocks)objDoorKnocks).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((DoorKnocks)objDoorKnocks).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((DoorKnocks)objDoorKnocks).txtState.Text = cbStates.Text.ToString();
                    ((DoorKnocks)objDoorKnocks).strStateCode = cbStates.SelectedValue.ToString();
                    ((DoorKnocks)objDoorKnocks).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "PreInvoice")
                {
                    ((PrevInvoice)objFrmPrevInvoice).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((PrevInvoice)objFrmPrevInvoice).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((PrevInvoice)objFrmPrevInvoice).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((PrevInvoice)objFrmPrevInvoice).txtState.Text = cbStates.Text.ToString();
                    ((PrevInvoice)objFrmPrevInvoice).strStateCode = cbStates.SelectedValue.ToString();
                    ((PrevInvoice)objFrmPrevInvoice).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "HRAddress")
                {
                    ((AddressCtrl)objFrmAddress).Village = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((AddressCtrl)objFrmAddress).Mondal = cbMandal.SelectedValue.ToString();
                    ((AddressCtrl)objFrmAddress).District = cbDistrict.SelectedValue.ToString();
                    ((AddressCtrl)objFrmAddress).State = cbStates.Text.ToString();
                    ((AddressCtrl)objFrmAddress).Pin = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "CustomerMaster")
                {
                    ((CustomerMaster)objCustomerMaster).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((CustomerMaster)objCustomerMaster).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((CustomerMaster)objCustomerMaster).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((CustomerMaster)objCustomerMaster).txtState.Text = cbStates.Text.ToString();
                    ((CustomerMaster)objCustomerMaster).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "AdvancedRefund")
                {
                    ((AdvancedRefund)objAdvanced).txtrefVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((AdvancedRefund)objAdvanced).txtrefMandal.Text = cbMandal.SelectedValue.ToString();
                    ((AdvancedRefund)objAdvanced).txtrefDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((AdvancedRefund)objAdvanced).txtrefState.Text = cbStates.Text.ToString();                    
                    ((AdvancedRefund)objAdvanced).txtrefPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "AttendedFarmerDetails")
                {
                    ((AttendedFarmerDetails)objAttendedFarmerDetails).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((AttendedFarmerDetails)objAttendedFarmerDetails).txtVillSearch.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((AttendedFarmerDetails)objAttendedFarmerDetails).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((AttendedFarmerDetails)objAttendedFarmerDetails).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((AttendedFarmerDetails)objAttendedFarmerDetails).txtState.Text = cbStates.Text.ToString();
                    ((AttendedFarmerDetails)objAttendedFarmerDetails).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "FarmerMeetingForm")
                {

                    ((FarmerMeetingForm)objFarmerMeetingForm).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((FarmerMeetingForm)objFarmerMeetingForm).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((FarmerMeetingForm)objFarmerMeetingForm).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((FarmerMeetingForm)objFarmerMeetingForm).txtState.Text = cbStates.Text.ToString();
                    ((FarmerMeetingForm)objFarmerMeetingForm).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "frmDemoPlots")
                {

                    ((frmDemoPlots)objfrmDemoPlots).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((frmDemoPlots)objfrmDemoPlots).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((frmDemoPlots)objfrmDemoPlots).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((frmDemoPlots)objfrmDemoPlots).txtState.Text = cbStates.Text.ToString();

                }
                else if (strFrom == "frmAttendedFarmerDetails")
                {

                    ((frmAttendedFarmerDetails)objfrmAttendedFarmerDetails).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((frmAttendedFarmerDetails)objfrmAttendedFarmerDetails).txtVillSearch.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((frmAttendedFarmerDetails)objfrmAttendedFarmerDetails).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((frmAttendedFarmerDetails)objfrmAttendedFarmerDetails).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((frmAttendedFarmerDetails)objfrmAttendedFarmerDetails).txtState.Text = cbStates.Text.ToString();
                    ((frmAttendedFarmerDetails)objfrmAttendedFarmerDetails).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "PatientDetails")
                {
                    ((EyeCampPatientDetails)obEyeCampDetails).txtPatientVill.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtPatientVilSearch.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtPatientMandal.Text = cbMandal.SelectedValue.ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtPatientDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtPatientState.Text = cbStates.Text.ToString();
                    //((EyeCampPatientDetails)obEyeCampDetails).strStateCode = cbStates.SelectedValue.ToString();
                }
                else if (strFrom == "CampDetails")
                {
                    ((EyeCampPatientDetails)obEyeCampDetails).txtCampVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtCampVillageSearch.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtCampMandal.Text = cbMandal.SelectedValue.ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtCampDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((EyeCampPatientDetails)obEyeCampDetails).txtCampState.Text = cbStates.Text.ToString();
                    //((EyeCampPatientDetails)obEyeCampDetails).strStateCode = cbStates.SelectedValue.ToString();
                }
                else if (strFrom == "EyeSurgeryPatientDetails")
                {
                    ((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtState.Text = cbStates.Text.ToString();
                    ((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtPin.Text = clbVillage.SelectedValue.ToString();


                }

                else if (strFrom == "frmSchoolVisits")
                {
                    ((frmSchoolVisits)objfrmSchoolVisits).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((frmSchoolVisits)objfrmSchoolVisits).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((frmSchoolVisits)objfrmSchoolVisits).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((frmSchoolVisits)objfrmSchoolVisits).txtState.Text = cbStates.Text.ToString();
                    ((frmSchoolVisits)objfrmSchoolVisits).txtPin.Text = clbVillage.SelectedValue.ToString();

                }
                else if (strFrom == "SVAttendedStudentDetails")
                {
                    ((SVAttendedStudentDetails)objSVAttendedStudentDetails).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((SVAttendedStudentDetails)objSVAttendedStudentDetails).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((SVAttendedStudentDetails)objSVAttendedStudentDetails).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((SVAttendedStudentDetails)objSVAttendedStudentDetails).txtState.Text = cbStates.Text.ToString();
                    ((SVAttendedStudentDetails)objSVAttendedStudentDetails).txtPin.Text = clbVillage.SelectedValue.ToString();

                }
                else if (strFrom == "SVGiftDetails")
                {
                    ((SVGiftDetails)objSVGiftDetails).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((SVGiftDetails)objSVGiftDetails).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((SVGiftDetails)objSVGiftDetails).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((SVGiftDetails)objSVGiftDetails).txtState.Text = cbStates.Text.ToString();
                    ((SVGiftDetails)objSVGiftDetails).txtPin.Text = clbVillage.SelectedValue.ToString();

                }
                else if (strFrom == "frmProductPromotion")
                {
                    ((frmProductPromotion)objfrmProductPromotion).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((frmProductPromotion)objfrmProductPromotion).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((frmProductPromotion)objfrmProductPromotion).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((frmProductPromotion)objfrmProductPromotion).txtState.Text = cbStates.Text.ToString();
                    ((frmProductPromotion)objfrmProductPromotion).txtPin.Text = clbVillage.SelectedValue.ToString();

                }
                else if (strFrom == "PMAttendedOthersDetails")
                {
                    ((PMAttendedOthersDetails)objPMAttendedOthersDetails).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((PMAttendedOthersDetails)objPMAttendedOthersDetails).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((PMAttendedOthersDetails)objPMAttendedOthersDetails).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((PMAttendedOthersDetails)objPMAttendedOthersDetails).txtState.Text = cbStates.Text.ToString();
                    ((PMAttendedOthersDetails)objPMAttendedOthersDetails).txtPin.Text = clbVillage.SelectedValue.ToString();

                }
                else if (strFrom == "MemberEntrollMent")
                {
                    ((MemberEnrollment)objMemberEnrollment).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((MemberEnrollment)objMemberEnrollment).txtPanchayat.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((MemberEnrollment)objMemberEnrollment).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    //((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    //((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtState.Text = cbStates.Text.ToString();
                    //((EyeSurgeryPatientDetails)objEyeSurgeryPatientDetails).txtPin.Text = clbVillage.SelectedValue.ToString();


                }
                else if (strFrom == "LegalCase")
                {
                   ((LeagalCaseDetails)objLegalCase).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((LeagalCaseDetails)objLegalCase).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((LeagalCaseDetails)objLegalCase).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((LeagalCaseDetails)objLegalCase).txtState.Text = cbStates.Text.ToString();
                    //((LeagalCaseDetails)objLegalCase)..Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "ComplainantMaster")
                {
                    ((ComplainantMaster)objComplainantMaster).txtVill.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((ComplainantMaster)objComplainantMaster).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((ComplainantMaster)objComplainantMaster).txtDistricit.Text = cbDistrict.SelectedValue.ToString();
                    ((ComplainantMaster)objComplainantMaster).txtState.Text = cbStates.Text.ToString();
                    ((ComplainantMaster)objComplainantMaster).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "PresentLawyer")
                {
                    ((LawyerMaster)objLawyerMaster).txtPresentVill.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((LawyerMaster)objLawyerMaster).txtPresentMandal.Text = cbMandal.SelectedValue.ToString();
                    ((LawyerMaster)objLawyerMaster).txtPresentDistricit.Text = cbDistrict.SelectedValue.ToString();
                    ((LawyerMaster)objLawyerMaster).txtPresentState.Text = cbStates.Text.ToString();
                    ((LawyerMaster)objLawyerMaster).txtPresentPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "PermantLawyer")
                {
                    ((LawyerMaster)objLawyerMaster).txtPermantVill.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((LawyerMaster)objLawyerMaster).txtPermantMandal.Text = cbMandal.SelectedValue.ToString();
                    ((LawyerMaster)objLawyerMaster).txtPermantDistricit.Text = cbDistrict.SelectedValue.ToString();
                    ((LawyerMaster)objLawyerMaster).txtPermantState.Text = cbStates.Text.ToString();
                    ((LawyerMaster)objLawyerMaster).txtPermantPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "PFUANMaster")
                {
                    ((PFUANMaster)objPFUANMaster).txtEmpVill.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((PFUANMaster)objPFUANMaster).txtEmpMandal.Text = cbMandal.SelectedValue.ToString();
                    ((PFUANMaster)objPFUANMaster).txtEmpDistricit.Text = cbDistrict.SelectedValue.ToString();
                    ((PFUANMaster)objPFUANMaster).txtEmpState.Text = cbStates.Text.ToString();
                    ((PFUANMaster)objPFUANMaster).txtEmpPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "SPRentalAgriments")
                {
                    ((SPRentalAgriments)objSPRentalAgriments).txtLocation.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((SPRentalAgriments)objSPRentalAgriments).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((SPRentalAgriments)objSPRentalAgriments).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((SPRentalAgriments)objSPRentalAgriments).txtState.Text = cbStates.Text.ToString();
                    ((SPRentalAgriments)objSPRentalAgriments).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "BranchMasterUpdate")
                {
                    ((frmBranchMasterUpdate)objfrmBranchMasterUpdate).txtLocation.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((frmBranchMasterUpdate)objfrmBranchMasterUpdate).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((frmBranchMasterUpdate)objfrmBranchMasterUpdate).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((frmBranchMasterUpdate)objfrmBranchMasterUpdate).txtState.Text = cbStates.Text.ToString();
                    ((frmBranchMasterUpdate)objfrmBranchMasterUpdate).txtState.Tag = cbStates.SelectedValue.ToString();
                    ((frmBranchMasterUpdate)objfrmBranchMasterUpdate).txtPin.Text = clbVillage.SelectedValue.ToString();

                }
                else if (strFrom == "CustomerAddressUpdate")
                {
                    ((CustomerAddressUpdate)objCustomerAddressUpdate).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((CustomerAddressUpdate)objCustomerAddressUpdate).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((CustomerAddressUpdate)objCustomerAddressUpdate).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((CustomerAddressUpdate)objCustomerAddressUpdate).txtState.Text = cbStates.Text.ToString();
                    ((CustomerAddressUpdate)objCustomerAddressUpdate).strStateCode = cbStates.SelectedValue.ToString();
                    ((CustomerAddressUpdate)objCustomerAddressUpdate).txtPin.Text = clbVillage.SelectedValue.ToString();
                }
                else if (strFrom == "SERVICE_WC_FF")
                {
                    ((frmWrongCommitmentOrFinancialFrauds)objWCFF).txtVillage.Text = ((System.Data.DataRowView)(((System.Windows.Forms.ListBox)(clbVillage)).SelectedItem)).Row.ItemArray[0].ToString();
                    ((frmWrongCommitmentOrFinancialFrauds)objWCFF).txtMandal.Text = cbMandal.SelectedValue.ToString();
                    ((frmWrongCommitmentOrFinancialFrauds)objWCFF).txtDistrict.Text = cbDistrict.SelectedValue.ToString();
                    ((frmWrongCommitmentOrFinancialFrauds)objWCFF).txtState.Text = cbStates.Text.ToString();
                    ((frmWrongCommitmentOrFinancialFrauds)objWCFF).txtPin.Text = clbVillage.SelectedValue.ToString();
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
