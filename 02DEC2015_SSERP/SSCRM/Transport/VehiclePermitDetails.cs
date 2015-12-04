using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class VehiclePermitDetails : Form
    {
        SQLDB objDb = null;
        bool flagUpdate = false;
        public VehiclePermitDetails()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillCompanyData();

        }
        private void FillCompanyData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }
        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocationData();
        }
        private void FillLocationData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            //cbLocation.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' ORDER BY BRANCH_NAME ASC";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "BRANCH_CODE";
                    //cbLocation.ValueMember = "LOCATION";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }
        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVehicleNo();
        }
        private void FillVehicleNo()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            cbVehNo.DataSource = null;
            try
            {
                if (cbLocation.SelectedIndex > 0)
                {
                    string strCommand = "SELECT TVM_VEHICLE_NAME+' - ('+TVM_VEHICLE_NUMBER+')' AS VEHICLE,TVM_VEHICLE_NUMBER FROM TR_VEHICLE_MAS WHERE TVM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND TVM_BRANCH_CODE='" + cbLocation.SelectedValue.ToString() + "' ORDER BY TVM_VEHICLE_NAME ASC";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbVehNo.DataSource = dt;
                    cbVehNo.DisplayMember = "VEHICLE";
                    cbVehNo.ValueMember = "TVM_VEHICLE_NUMBER";
                    //cbLocation.ValueMember = "LOCATION";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }

        private void gvPermitDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            objDb = new SQLDB();

            if(e.ColumnIndex==gvPermitDetails.Columns["edit"].Index)
            {
                flagUpdate = true;
                txtPermitNo.Enabled = false;
                cbVehNo.SelectedValue = gvPermitDetails.Rows[e.RowIndex].Cells["vehicleno"].Value.ToString();
                cbPermit.SelectedItem = gvPermitDetails.Rows[e.RowIndex].Cells["permittype"].Value.ToString();
                txtPermitNo.Text = gvPermitDetails.Rows[e.RowIndex].Cells["permitno"].Value.ToString();
                dtpValidFrom.Value = Convert.ToDateTime(gvPermitDetails.Rows[e.RowIndex].Cells["from"].Value.ToString());
                dtpValidTo.Value = Convert.ToDateTime(gvPermitDetails.Rows[e.RowIndex].Cells["to"].Value.ToString());
                //string strvehno = gvPermitDetails.Rows[e.RowIndex].Cells["vehicleno"].Value.ToString();
                //string strCommand = "SELECT  TVM_COMPANY_CODE,TVM_BRANCH_CODE FROM TR_VEHICLE_MAS  WHERE TVM_VEHICLE_NUMBER ='"+strvehno+"'";
                //cbCompany.SelectedValue = objDb.ExecuteDataSet(strCommand).Tables[0].Rows[0][0];
                
                //cbLocation.SelectedValue = objDb.ExecuteDataSet(strCommand).Tables[0].Rows[0][1];
            }
            else if (e.ColumnIndex == gvPermitDetails.Columns["del"].Index)
            {
               
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        string strCommand = "DELETE FROM TR_VEHICLE_PERMIT_PC_DETL WHERE VPPD_VEHICLE_REG_NO='" + gvPermitDetails.Rows[e.RowIndex].Cells["vehicleno"].Value + "' AND VPPD_DOC_TYPE='PERMIT' AND VPPD_DOC_NO='" + gvPermitDetails.Rows[e.RowIndex].Cells["permitno"].Value + "'";
                        int iRes = objDb.ExecuteSaveData(strCommand);
                        if (iRes > 0)
                        {
                            FillVehiclePermitDetails();
                            MessageBox.Show("Data Deleted Succesfully", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            txtPermitNo.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Data Not Deleted", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objDb = null;
                        //btnClear_Click(null, null);
                        txtPermitNo.Text = "";
                        cbPermit.SelectedIndex = 0;
                        dtpValidFrom.Value = DateTime.Today;
                        dtpValidTo.Value = DateTime.Today;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void FillVehiclePermitDetails()
        {
            gvPermitDetails.Rows.Clear();
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            if (cbVehNo.SelectedIndex > 0)
            {
                try
                {
                    string strCommand = "SELECT TVM_VEHICLE_NAME,VPPD_VEHICLE_REG_NO,VPPD_SL_NO,VPPD_DOC_NO,VPPD_DOC_CLASS,VPPD_VALID_FROM,VPPD_VALID_TO,VPPD_CREATED_BY FROM TR_VEHICLE_PERMIT_PC_DETL INNER JOIN TR_VEHICLE_MAS VM ON TVM_VEHICLE_NUMBER=VPPD_VEHICLE_REG_NO WHERE VPPD_VEHICLE_REG_NO='" + cbVehNo.SelectedValue.ToString() + "' AND VPPD_DOC_TYPE ='PERMIT'";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                            cellSNo.Value = (i + 1).ToString();
                            tempRow.Cells.Add(cellSNo);

                            DataGridViewCell cellPermitSNo = new DataGridViewTextBoxCell();
                            cellPermitSNo.Value = dt.Rows[i]["VPPD_SL_NO"];
                            tempRow.Cells.Add(cellPermitSNo);


                            DataGridViewCell cellVNo = new DataGridViewTextBoxCell();
                            cellVNo.Value = dt.Rows[i]["VPPD_VEHICLE_REG_NO"];
                            tempRow.Cells.Add(cellVNo);

                            DataGridViewCell cellVName = new DataGridViewTextBoxCell();
                            cellVName.Value = dt.Rows[i]["TVM_VEHICLE_NAME"];
                            tempRow.Cells.Add(cellVName);

                            DataGridViewCell cellPType = new DataGridViewTextBoxCell();
                            cellPType.Value = dt.Rows[i]["VPPD_DOC_NO"];
                            tempRow.Cells.Add(cellPType);

                            DataGridViewCell cellPNo = new DataGridViewTextBoxCell();
                            cellPNo.Value = dt.Rows[i]["VPPD_DOC_CLASS"];
                            tempRow.Cells.Add(cellPNo);

                            DataGridViewCell cellValidFrom = new DataGridViewTextBoxCell();
                            cellValidFrom.Value = Convert.ToDateTime(dt.Rows[i]["VPPD_VALID_FROM"]).ToShortDateString();
                            tempRow.Cells.Add(cellValidFrom);

                            DataGridViewCell cellValidTo = new DataGridViewTextBoxCell();
                            cellValidTo.Value = Convert.ToDateTime( dt.Rows[i]["VPPD_VALID_TO"]).ToShortDateString();
                            tempRow.Cells.Add(cellValidTo);


                            gvPermitDetails.Rows.Add(tempRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                    dt = null;
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            txtPermitNo.Enabled = true;
            cbCompany.SelectedIndex = 0;
            cbLocation.DataSource = null;
            cbVehNo.DataSource = null;
            txtPermitNo.Text = "";
            cbPermit.SelectedIndex = 0;
            dtpValidFrom.Value = DateTime.Today;
            dtpValidTo.Value = DateTime.Today;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strCommand = "";
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                if (checkData())
                {
                    if (flagUpdate)
                    {
                        strCommand = " UPDATE TR_VEHICLE_PERMIT_PC_DETL SET VPPD_DOC_CLASS='" + cbPermit.SelectedItem.ToString() +
                            "',VPPD_VALID_FROM='" + Convert.ToDateTime(dtpValidFrom.Value).ToString("dd/MMM/yyyy") + "',VPPD_VALID_TO='" + Convert.ToDateTime(dtpValidTo.Value).ToString("dd/MMM/yyyy") + 
                            "',VPPD_LAST_MODIFIED_BY='',VPPD_LAST_MODIFIED_DATE=getDate() WHERE VPPD_VEHICLE_REG_NO='" + cbVehNo.SelectedValue.ToString() + "' AND VPPD_DOC_TYPE='PERMIT' AND VPPD_DOC_NO='"+txtPermitNo.Text+"'";
                        flagUpdate = false;
                        txtPermitNo.Enabled = true;
                    }
                    else
                    {
                        string[] strVehicle = cbVehNo.SelectedValue.ToString().Split('(');
                        strCommand = "SELECT VPPD_DOC_NO FROM TR_VEHICLE_PERMIT_PC_DETL WHERE VPPD_DOC_NO='" + txtPermitNo.Text + "'";
                        dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            strCommand = "SELECT ISNULL(MAX(VPPD_SL_NO), 0)+1 FROM TR_VEHICLE_PERMIT_PC_DETL";
                             dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                            
                            strCommand = "INSERT INTO TR_VEHICLE_PERMIT_PC_DETL(VPPD_SL_NO,VPPD_VEHICLE_REG_NO,VPPD_DOC_CLASS,VPPD_DOC_NO,VPPD_VALID_FROM,"+
                            "VPPD_VALID_TO,VPPD_CREATED_BY,VPPD_DOC_TYPE,VPPD_CREATED_DATE) VALUES(" + Convert.ToInt32(dt.Rows[0][0]) + ",'" + cbVehNo.SelectedValue.ToString() + 
                            "','" + cbPermit.SelectedItem.ToString() + "','" + txtPermitNo.Text.ToUpper().Replace(" ", "") + 
                            "','" + Convert.ToDateTime(dtpValidFrom.Value).ToString("dd/MMM/yyyy") + 
                            "','" + Convert.ToDateTime(dtpValidTo.Value).ToString("dd/MMM/yyyy") + "','','PERMIT',GETDATE())";
                        }
                        else
                            MessageBox.Show("Permit No already Exists");
                    }
                    int iRes = objDb.ExecuteSaveData(strCommand);
                    strCommand = "";
                    dt = null;
                    if (iRes > 0)
                    {
                        FillVehiclePermitDetails();
                        MessageBox.Show("Data Saved Succesfully", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                strCommand = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                //btnClear_Click(null,null);
                txtPermitNo.Text = "";
                cbPermit.SelectedIndex = 0;
                dtpValidFrom.Value = DateTime.Today;
                dtpValidTo.Value = DateTime.Today;

            }
        }

        private bool checkData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbCompany.Focus();
                flag = false;
            }
            else if (cbLocation.SelectedIndex == 0)
            {
                MessageBox.Show("Select Location", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbLocation.Focus();
                flag = false;
            }
            else if (cbVehNo.SelectedIndex == 0)
            {
                MessageBox.Show("Select Vehicle No", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbVehNo.Focus();
                flag = false;
            }
            else if (cbPermit.SelectedIndex == 0)
            {
                MessageBox.Show("Selct Permit Type", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbPermit.Focus();
                flag = false;
            }
            else if (txtPermitNo.Text == string.Empty)
            {
                MessageBox.Show("Enter Permit No", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPermitNo.Focus();
                flag = false;
            }
            else if (!(dtpValidTo.Value > dtpValidFrom.Value))
            {
                MessageBox.Show("Enter Valid Dates", "Vehicle Permit Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
            }
            return flag;
        }

        private void cbVehNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVehiclePermitDetails();
            cbPermit.SelectedIndex = 0;
        }


        

    }
}
