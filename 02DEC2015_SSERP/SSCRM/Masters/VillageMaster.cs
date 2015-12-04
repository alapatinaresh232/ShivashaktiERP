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
    public partial class VillageMaster : Form
    {
        private StaffLevel objState = null;
        private InvoiceDB objInv = null;
        private SQLDB objDB = null;
        public VillageMaster()
        {
            InitializeComponent();
        }

        private void VillageMaster_Load(object sender, EventArgs e)
        {
            txtDsearch.CharacterCasing = CharacterCasing.Upper;
            txtDsearch.Enabled = false;
            FillStateComboBox();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chkDistr.Visible = true;
                chkMand.Visible = true;
            }
            else
            {
                chkDistr.Visible = false;
                chkMand.Visible = false;
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
            
                //if (cbStates.SelectedValue.ToString() == "NP")
                //{
                //    lblDistrict.Text = "Zone";
                //    lblMandal.Text = "District";
                //}
                //else
                //{
                //    lblDistrict.Text = "District";
                //    lblMandal.Text = "Mandal";
                //}

                if (cbStates.SelectedIndex > 0)
                {
                    cbDistrict.DataSource = null;
                    cbDistrict.DataBindings.Clear();
                    cbDistrict.Items.Clear();

                    cbMandal.DataSource = null;
                    cbMandal.DataBindings.Clear();
                    cbMandal.Items.Clear();

                    lstMappedBranches.DataSource = null;
                    lstMappedBranches.DataBindings.Clear();
                    lstMappedBranches.Items.Clear();

                    objInv = new InvoiceDB();
                    try
                    {
                        DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(), string.Empty, string.Empty).Tables[0];
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
            
            if(chkDistr.Checked == true || chkMand.Checked == true)
            {
                FillDataToList();
            }
            
        }

        private void cbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkDistr.Checked == false && chkMand.Checked == false)
            {
                if (cbDistrict.SelectedIndex > 0)
                {
                    cbMandal.DataSource = null;
                    cbMandal.DataBindings.Clear();
                    cbMandal.Items.Clear();

                    lstMappedBranches.DataSource = null;
                    lstMappedBranches.DataBindings.Clear();
                    lstMappedBranches.Items.Clear();

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
            else
            {
                FillDataToList();
            }
        }

        private void cbMandal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMandal.SelectedIndex > 0)
            {
                lstMappedBranches.DataSource = null;
                lstMappedBranches.DataBindings.Clear();
                lstMappedBranches.Items.Clear();

                objInv = new InvoiceDB();
                try
                {
                    DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(), cbDistrict.SelectedValue.ToString(), cbMandal.SelectedValue.ToString()).Tables[0];
                    lstMappedBranches.Items.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        lstMappedBranches.DataSource = dt;
                        lstMappedBranches.DisplayMember = "PANCHAYAT";
                        lstMappedBranches.ValueMember = "PIN";
                    }
                    txtDsearch.Enabled = true;
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
            else
                txtDsearch.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (chkDistr.Checked == false && chkMand.Checked == false)
            {
                if (txtDsearch.Text.Trim().Length > 0 && txtDsearch.Enabled == true)
                {
                    objDB = new SQLDB();
                    int imax = 0;
                    int iRec = 0;
                    DataTable dt = new DataTable();
                    DataTable dtKeys = new DataTable();
                    try
                    {
                        string sqlText = "SELECT * FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District = '" + cbDistrict.Text.ToString() + "' AND Mandal = '" + cbMandal.Text.ToString() + "' AND Panchayat LIKE '%" + txtDsearch.Text + "%';";
                        dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                        sqlText = "SELECT DISTINCT CDSTATE,CDDISTRICT,CDMANDAL FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District = '" + cbDistrict.Text.ToString() + "' AND Mandal = '" + cbMandal.Text.ToString() + "';";
                        dtKeys = objDB.ExecuteDataSet(sqlText).Tables[0];
                        sqlText = "";
                        if (dt.Rows.Count == 0 && dtKeys.Rows.Count > 0)
                        {
                            sqlText = "SELECT ISNULL(MAX(Ukey),0)+1 FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District = '" + cbDistrict.Text.ToString() + "' AND Mandal = '" + cbMandal.Text.ToString() + "';";
                            imax = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString());
                            if (imax != 0)
                            {
                                sqlText = "INSERT INTO VILLAGEMASTERUKEY(State,District,Mandal,Panchayat,CDState,CDDistrict,CDMandal,Ukey,CREATED_BY,CREATED_DATE) VALUES('" + cbStates.Text.ToString() + "'" +
                                            ",'" + cbDistrict.SelectedValue.ToString() + "','" + cbMandal.SelectedValue.ToString() + "','" + txtDsearch.Text + "','" + cbStates.SelectedValue.ToString() + "'" +
                                            ",'" + dtKeys.Rows[0]["CDDISTRICT"].ToString() + "','" + dtKeys.Rows[0]["CDMANDAL"].ToString() + "','" + imax + "','" + CommonData.LogUserId + "'" +
                                            ",'" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'); ";

                                iRec = objDB.ExecuteSaveData(sqlText);
                                if (iRec > 0)
                                {
                                    if (CommonData.LogUserId.ToUpper() != "ADMIN")
                                    {
                                        MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    txtDsearch.Text = "";
                                    cbMandal_SelectedIndexChanged(null, null);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Village Allready Exist!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        dt = null;
                        objDB = null;
                    }
                }
            }
            else if (chkDistr.Checked == true && chkMand.Checked == false)
            {
                if (txtDistrict.Text.Trim().Length > 0 && txtMandal.Text.Trim().Length > 0 && txtDsearch.Text.Trim().Length > 0)
                {
                    objDB = new SQLDB();
                    int imax = 0;
                    int iRec = 0;
                    DataTable dt = new DataTable();
                    DataTable dtKeys = new DataTable();
                    try
                    {
                        string sqlText = "SELECT * FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District LIKE '%" + txtDistrict.Text + "%';";
                        dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                        //sqlText = "SELECT DISTINCT CDSTATE FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District = '" + cbDistrict.Text.ToString() + "' AND Mandal = '" + cbMandal.Text.ToString() + "';";
                        //dtKeys = objDB.ExecuteDataSet(sqlText).Tables[0];
                        sqlText = "";
                        if (dt.Rows.Count == 0)
                        {
                            sqlText = "SELECT ISNULL(MAX(CDDistrict),0)+1 FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "';";
                            imax = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString());
                            if (imax != 0)
                            {
                                sqlText = "INSERT INTO VILLAGEMASTERUKEY(State,District,Mandal,Panchayat,CDState,CDDistrict,CDMandal,Ukey,CREATED_BY,CREATED_DATE) VALUES('" + cbStates.Text.ToString() + "'" +
                                            ",'" + txtDistrict.Text.ToString() + "','" + txtMandal.Text.ToString() + "','" + txtDsearch.Text + "','" + cbStates.SelectedValue.ToString() + "'" +
                                            ",'" + imax + "','1','1','" + CommonData.LogUserId + "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'); ";

                                iRec = objDB.ExecuteSaveData(sqlText);
                                if (iRec > 0)
                                {
                                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDsearch.Text = "";
                                    txtDistrict.Text = "";
                                    txtMandal.Text = "";
                                    FillDataToList();
                                    //cbMandal_SelectedIndexChanged(null, null);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("District Allready Exist!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        dt = null;
                        objDB = null;
                    }
                }
            }
            else if (chkDistr.Checked == false && chkMand.Checked == true)
            {
                if (txtMandal.Text.Trim().Length > 0 && txtDsearch.Text.Trim().Length > 0)
                {
                    objDB = new SQLDB();
                    int imax = 0;
                    int iRec = 0;
                    DataTable dt = new DataTable();
                    DataTable dtKeys = new DataTable();
                    try
                    {
                        string sqlText = "SELECT * FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District = '" + cbDistrict.Text.ToString() + "' AND Mandal LIKE '%" + txtMandal.Text + "%';";
                        dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                        sqlText = "SELECT DISTINCT CDState,CDDistrict FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District = '" + cbDistrict.Text.ToString() + "';";
                        dtKeys = objDB.ExecuteDataSet(sqlText).Tables[0];
                        sqlText = "";
                        if (dt.Rows.Count == 0 && dtKeys.Rows.Count > 0)
                        {
                            sqlText = "SELECT ISNULL(MAX(CDMandal),0)+1 FROM VILLAGEMASTERUKEY WHERE State = '" + cbStates.Text.ToString() + "' AND District = '" + cbDistrict.Text.ToString() + "';";
                            imax = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString());
                            if (imax != 0)
                            {
                                sqlText = "INSERT INTO VILLAGEMASTERUKEY(State,District,Mandal,Panchayat,CDState,CDDistrict,CDMandal,Ukey,CREATED_BY,CREATED_DATE) VALUES('" + cbStates.Text.ToString() + "'" +
                                            ",'" + cbDistrict.Text.ToString() + "','" + txtMandal.Text.ToString() + "','" + txtDsearch.Text + "','" + cbStates.SelectedValue.ToString() + "'" +
                                            ",'" + dtKeys.Rows[0]["CDDistrict"].ToString() + "','" + imax + "','1','" + CommonData.LogUserId + "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'); ";

                                iRec = objDB.ExecuteSaveData(sqlText);
                                if (iRec > 0)
                                {
                                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDsearch.Text = "";
                                    txtMandal.Text = "";
                                    FillDataToList();
                                    //cbMandal_SelectedIndexChanged(null, null);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mandal Allready Exist!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        dt = null;
                        objDB = null;
                    }
                }
            }
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), lstMappedBranches);
        }


        private void SearchEcode(string searchString, System.Windows.Forms.ListBox cbEcodes)
        {
            if (searchString.Trim().Length > 0)
            {
                for (int i = 0; i < cbEcodes.Items.Count; i++)
                {
                    //ListBox nItem = new ListBox();
                    //nItem = cbEcodes.Items[i];
                    //if (cbEcodes.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
                    //{
                    //    //if (nItem.Text.Contains == searchString)
                    //    //{
                    //    //    cbEcodes.SetSelected(i, true);
                    //    //    break;
                    //    //}
                    //    //else
                    //    //    cbEcodes.SetSelected(i, false);

                    //}
                    //else  // for checkbox list search
                    //{
                    //    if (cbEcodes.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                    //    {
                    //        cbEcodes.SetSelected(i, true);
                    //        break;
                    //    }
                    //    else
                    //        cbEcodes.SetSelected(i, false);

                    //}

                }
            }
        }

        private void chkDistr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDistr.Checked == true)
            {
                chkMand.Checked = false;
                cbDistrict.Visible = false;
                cbMandal.Visible = false;
                txtDsearch.Enabled = true;
                cbStates_SelectedIndexChanged(null, null);
                lstMappedBranches.DataSource = null;
                FillDataToList();
            }
            else
            {
                cbDistrict.Visible = true;
                cbMandal.Visible = true;
                txtDsearch.Enabled = false;
            }

        }
        private void chkMand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMand.Checked == true)
            {
                chkDistr.Checked = false;
                cbMandal.Visible = false;
                lstMappedBranches.DataSource = null;
                FillDataToList();
                txtDsearch.Enabled = true;
                txtDsearch.ReadOnly = false;
            }
            else
            {
                cbMandal.Visible = true;
                txtDsearch.Enabled = false;
            }
        }

        private void FillDataToList()
        {
            if (chkDistr.Checked == true && chkMand.Checked == false)
            {
                if (cbStates.SelectedValue.ToString() == "NP")
                {
                    lblDistrict.Text = "Zone";
                    lblMandal.Text = "District";
                }
                else
                {
                    lblDistrict.Text = "District";
                    lblMandal.Text = "Mandal";
                }

                if (cbStates.SelectedIndex > 0)
                {
                    cbDistrict.DataSource = null;
                    cbDistrict.DataBindings.Clear();
                    //cbDistrict.Items.Clear();

                    cbMandal.DataSource = null;
                    cbMandal.DataBindings.Clear();
                    cbMandal.Items.Clear();

                    lstMappedBranches.DataSource = null;
                    lstMappedBranches.DataBindings.Clear();
                    lstMappedBranches.Items.Clear();

                    objInv = new InvoiceDB();
                    try
                    {
                        DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(), string.Empty, string.Empty).Tables[0];
                        //DataRow dr = dt.NewRow();
                        //dr[0] = "Select";
                        //dt.Rows.InsertAt(dr, 0);

                        if (dt.Rows.Count > 1)
                        {
                            lstMappedBranches.DataSource = dt;
                            lstMappedBranches.DisplayMember = "DISTRICT";
                            lstMappedBranches.ValueMember = "DISTRICT";
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
            else if (chkDistr.Checked == false && chkMand.Checked == true)
            {
                if (cbDistrict.SelectedIndex > 0)
                {
                    cbMandal.DataSource = null;
                    cbMandal.DataBindings.Clear();
                    cbMandal.Items.Clear();

                    lstMappedBranches.DataSource = null;
                    lstMappedBranches.DataBindings.Clear();
                    lstMappedBranches.Items.Clear();

                    objInv = new InvoiceDB();
                    try
                    {
                        //cbMandal.Items.Insert(0, new ListItem("Select", "0"));
                        DataTable dt = objInv.VillageMasterFilter_Get(cbStates.SelectedValue.ToString(), cbDistrict.SelectedValue.ToString(), string.Empty).Tables[0];
                        //DataRow dr = dt.NewRow();
                        //dr[0] = "Select";
                        //dt.Rows.InsertAt(dr, 0);

                        if (dt.Rows.Count > 1)
                        {
                            lstMappedBranches.DataSource = dt;
                            lstMappedBranches.DisplayMember = "MANDAL";
                            lstMappedBranches.ValueMember = "MANDAL";
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

       
    }
}
