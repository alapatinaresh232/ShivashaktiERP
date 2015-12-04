using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class VehicleMaster : Form
    {
        SQLDB objDb = null;
        public VehicleMaster()
        {
            InitializeComponent();
        }

        private void VehicleMaster_Load(object sender, EventArgs e)
        {
            txtDsearch.CharacterCasing = CharacterCasing.Upper;
            //txtDsearch.Enabled = false;
            FillVehicleType();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chkVehMake.Visible = true;
            }
            else
            {
                chkVehMake.Visible = false;
            }

        }
        private void FillVehicleType()
        {
            objDb= new SQLDB();
            try
            {
                DataTable dt = objDb.ExecuteDataSet("SELECT DISTINCT(VM_VEHICLE_TYPE) FROM VEHICLE_MASTER").Tables[0];

                if (dt.Rows.Count > 1)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "Select";
                    dt.Rows.InsertAt(row, 0);
                    cbVehType.DataSource = dt;
                    cbVehType.DisplayMember = "VM_VEHICLE_TYPE";
                    cbVehType.ValueMember = "VM_VEHICLE_TYPE";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objDb = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (chkVehMake.Checked == false)
            {
                if (txtDsearch.Text.Trim().Length > 0 && txtDsearch.Enabled == true)
                {
                    objDb = new SQLDB();
                    
                    int iRec = 0;
                    DataTable dt = new DataTable();
                    try
                    {
                        string sqlText = "SELECT  * FROM VEHICLE_MASTER WHERE VM_VEHICLE_TYPE='"+cbVehType.SelectedValue.ToString()+"' AND VM_VEHICLE_MAKE='"+cbVehMake.SelectedValue.ToString()+"'AND VM_VEHICLE_MODEL LIKE '%"+txtDsearch.Text+"%'";
                        dt = objDb.ExecuteDataSet(sqlText).Tables[0];
                        sqlText = "";
                        if (dt.Rows.Count == 0)
                        {
                            sqlText = "INSERT INTO VEHICLE_MASTER(VM_VEHICLE_TYPE,VM_VEHICLE_MAKE,VM_VEHICLE_MODEL) VALUES('"+cbVehType.SelectedValue.ToString()+"','"+cbVehMake.SelectedValue.ToString()+"','"+txtDsearch.Text.ToUpper()+"') ";

                            iRec = objDb.ExecuteSaveData(sqlText);
                                if (iRec > 0)
                                {
                                    if (CommonData.LogUserId.ToUpper() != "ADMIN")
                                    {
                                        MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    txtDsearch.Text = "";
                                    cbVehMake_SelectedIndexChanged(null,null);
                                }
                        }
                        else
                        {
                            MessageBox.Show("Vehicle Name Allready Exist!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        dt = null;
                        objDb = null;
                    }
                }
            }
            else
            {
                if (txtVehMake.Text.Trim().Length > 0 && txtDsearch.Text.Trim().Length > 0)
                {
                    objDb = new SQLDB();
                   
                    int iRec = 0;
                    DataTable dt = new DataTable();
                    DataTable dtKeys = new DataTable();
                    try
                    {
                        string sqlText = "SELECT  * FROM VEHICLE_MASTER WHERE VM_VEHICLE_TYPE='" + cbVehType.SelectedValue.ToString() + "' AND VM_VEHICLE_MAKE LIKE '%" + txtVehMake.Text + "%'";
                        dt = objDb.ExecuteDataSet(sqlText).Tables[0];
                        sqlText = "";
                        if (dt.Rows.Count == 0)
                        {
                            sqlText = "INSERT INTO VEHICLE_MASTER(VM_VEHICLE_TYPE,VM_VEHICLE_MAKE,VM_VEHICLE_MODEL) VALUES('" + cbVehType.SelectedValue.ToString() + "','" + txtVehMake.Text.ToUpper() + "','" + txtDsearch.Text.ToUpper() + "')";

                            iRec = objDb.ExecuteSaveData(sqlText);
                            if (iRec > 0)
                            {
                                MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDsearch.Text = "";
                                txtVehMake.Text = "";
                                FillDataToList();
                                //cbVehMake_SelectedIndexChanged(null, null);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Vehicle Make Allready Exist!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        dt = null;
                        objDb = null;
                    }
                }
            }
            
        }

        private void chkVehMake_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVehMake.Checked == true)
            {
                txtVehMake.Visible = true;
                cbVehMake.Visible = false;
                lstMappedBranches.DataSource = null;
                FillDataToList();
                //txtDsearch.Enabled = true;
                //txtDsearch.ReadOnly = false;
            }
            else
            {
                
                txtVehMake.Visible = false;
                cbVehMake.Visible = true;
                //chkVehMake.Visible = true;
                //txtDsearch.Enabled = false;
            }
            cbVehType_SelectedIndexChanged(null,null);
        }

        private void cbVehType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehType.SelectedIndex > 0)
            {
                cbVehMake.DataSource = null;
                cbVehMake.DataBindings.Clear();
                cbVehMake.Items.Clear();

                
                lstMappedBranches.DataSource = null;
                lstMappedBranches.DataBindings.Clear();
                lstMappedBranches.Items.Clear();

                objDb = new SQLDB();
                try
                {
                    DataTable dt = objDb.ExecuteDataSet("SELECT  DISTINCT(VM_VEHICLE_MAKE) FROM VEHICLE_MASTER WHERE VM_VEHICLE_TYPE='"+cbVehType.SelectedValue.ToString()+"'").Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[0] = "Select";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 1)
                    {
                        cbVehMake.DataSource = dt;
                        cbVehMake.DisplayMember = "VM_VEHICLE_MAKE";
                        cbVehMake.ValueMember = "VM_VEHICLE_MAKE";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objDb = null;
                }
            }

            if (chkVehMake.Checked == true)
            {
                FillDataToList();
            }
        }
        private void FillDataToList()
        {
            if (chkVehMake.Checked == true)
            {
                if (cbVehType.SelectedIndex > 0)
                {
                    cbVehMake.DataSource = null;
                    cbVehMake.DataBindings.Clear();
                    //cbDistrict.Items.Clear();



                    lstMappedBranches.DataSource = null;
                    lstMappedBranches.DataBindings.Clear();
                    lstMappedBranches.Items.Clear();
                }
                objDb = new SQLDB();
                try
                {
                    DataTable dt = objDb.ExecuteDataSet("SELECT  DISTINCT(VM_VEHICLE_MAKE) FROM VEHICLE_MASTER WHERE VM_VEHICLE_TYPE='"+cbVehType.SelectedValue.ToString()+"'").Tables[0];
                    //DataRow dr = dt.NewRow();
                    //dr[0] = "Select";
                    //dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 1)
                    {
                        lstMappedBranches.DataSource = dt;
                        lstMappedBranches.DisplayMember = "VM_VEHICLE_MAKE";
                        lstMappedBranches.ValueMember = "VM_VEHICLE_MAKE";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    objDb = null;
                }
            }
        }

        private void cbVehMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehMake.SelectedIndex > 0)
            {
                lstMappedBranches.DataSource = null;
                lstMappedBranches.DataBindings.Clear();
                lstMappedBranches.Items.Clear();

                objDb = new SQLDB();
                try
                {

                    DataTable dt = objDb.ExecuteDataSet("SELECT  DISTINCT(VM_VEHICLE_MODEL) FROM VEHICLE_MASTER WHERE VM_VEHICLE_TYPE='"+cbVehType.SelectedValue.ToString()+"' AND VM_VEHICLE_MAKE='"+cbVehMake.SelectedValue.ToString()+"'").Tables[0];
                    lstMappedBranches.Items.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        lstMappedBranches.DataSource = dt;
                        lstMappedBranches.DisplayMember = "VM_VEHICLE_MODEL";
                        lstMappedBranches.ValueMember = "VM_VEHICLE_MODEL";
                    }
                    //txtDsearch.Enabled = true;
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objDb = null;
                }
            }
            //else
            //    //txtDsearch.Enabled = false;
        }

       
       
    }
}






























