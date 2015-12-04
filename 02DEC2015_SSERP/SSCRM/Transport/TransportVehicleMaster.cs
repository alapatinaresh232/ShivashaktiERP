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

namespace SSCRM
{
    public partial class TransportVehicleMaster : Form
    {
        SQLDB objDb = null;
        bool flagUpdate = false;
        public TransportVehicleMaster()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objDb = new SQLDB();
            int iRes = 0;
            txtClassNo.Text = txtClassNo.Text.Replace(" ", "");
            txtEngineNo.Text = txtEngineNo.Text.Replace(" ", "");
            txtVehicleNo.Text = txtVehicleNo.Text.Replace(" ", "");
            if (CheckData())
            {
                if (flagUpdate)
                {
                    try
                    {
                        string[] strLocation = cbLocation.SelectedValue.ToString().Split('@');
                        string strCommand = " UPDATE TR_VEHICLE_MAS set TVM_VEHICLE_TYPE='" + cbVehType.SelectedValue.ToString() + 
                            "',TVM_ENGINE_NUMBER='" + txtEngineNo.Text.ToUpper() + "',TVM_CHASSIS_NUMBER='" + txtClassNo.Text.ToUpper() + 
                            "',TVM_DATE_OF_REG='" + Convert.ToDateTime(dtpReg.Value).ToString("yyyy/MM/dd") + 
                            "',TVM_PERMIT_FLAG='" + cbPermit.SelectedItem.ToString() + "',TVM_HPA_FLAG='" + txtHPA.Text.ToUpper() + 
                            "',TVM_VEHICLE_STATUS='" + cbStatus.SelectedItem.ToString() + "',TVM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + 
                            "',TVM_BRANCH_CODE='" + strLocation[0] + "',TVM_STATE_CODE='" + strLocation[1] + 
                            "',TVM_LAST_MODIFIED_BY='"+CommonData.LogUserId+"',TVM_LAST_MODIFIED_DATE=getdate()" +
                            ",TVM_VEHICLE_NAME='"+cbVehicleName.SelectedValue.ToString()+"'" +
                            " WHERE TVM_VEHICLE_NUMBER='" + txtVehicleNo.Text.ToUpper() + "'";
                        iRes = objDb.ExecuteSaveData(strCommand);
                        if (iRes > 0)
                        {
                            FillVehicleInformation();
                            MessageBox.Show("Data Saved Succesfully", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            txtVehicleNo.Enabled = true;
                            btnClear_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Data Not Saved", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                else
                {
                    objDb = new SQLDB();
                    DataTable dt = new DataTable();
                    int iCount = 0;
                    try
                    {
                        
                        string strCommand = "SELECT COUNT(*) FROM TR_VEHICLE_MAS where TVM_VEHICLE_NUMBER='" + txtVehicleNo.Text + "'";
                        dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                        iCount = Convert.ToInt32(dt.Rows[0][0]);
                        if (iCount == 0)
                        {
                            string[] strLocation = cbLocation.SelectedValue.ToString().Split('@');
                            string strInsertCommand = "INSERT INTO TR_VEHICLE_MAS (TVM_VEHICLE_NUMBER,TVM_VEHICLE_TYPE,TVM_ENGINE_NUMBER"+
                                ",TVM_CHASSIS_NUMBER,TVM_DATE_OF_REG,TVM_PERMIT_FLAG,TVM_HPA_FLAG,TVM_VEHICLE_STATUS,TVM_CREATED_BY"+
                                ",TVM_CREATED_DATE,TVM_COMPANY_CODE,TVM_BRANCH_CODE,TVM_STATE_CODE,TVM_VEHICLE_NAME) " +
                                "VALUES('" + txtVehicleNo.Text.ToUpper().Trim() + "','" + cbVehType.SelectedValue.ToString() +
                                "','" + txtEngineNo.Text.ToUpper().Trim() + "','" + txtClassNo.Text.ToUpper().Trim() + "','" + Convert.ToDateTime(dtpReg.Value).ToString("dd/MMM/yyyy") + 
                                "','" + cbPermit.SelectedItem.ToString() + "','" + txtHPA.Text.ToUpper() + "','" + cbStatus.SelectedItem.ToString() + 
                                "','"+CommonData.LogUserId+"',getdate(),'" + cbCompany.SelectedValue.ToString() + "','" + strLocation[0] + 
                                "','" + strLocation[1] + "','"+cbVehicleName.SelectedValue.ToString()+"')";
                            iRes = objDb.ExecuteSaveData(strInsertCommand);
                            if (iRes > 0)
                            {
                                FillVehicleInformation();
                                MessageBox.Show("Data Saved Succesfully", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnClear_Click(null, null);
                            }
                            else
                            {
                                MessageBox.Show("Data Not Saved", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vehicle No Already Registered");

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
                    try
                    {
                        
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

            
            //MessageBox.Show(cbPermit.SelectedItem.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtVehicleNo.Text = "";
            txtVehicleNo.Enabled = true;
            txtEngineNo.Text = "";
            cbPermit.SelectedIndex = 0;
            txtHPA.Text = "";
            txtClassNo.Text = "";
            cbStatus.SelectedIndex = 0;
            cbVehType.SelectedIndex = 0;
            dtpReg.Value = DateTime.Today;
            flagUpdate = false;
            cbCompany.SelectedIndex = 0;
            gvVehicleDetails.ClearSelection();
            
        }
        private void TransportVehicleMaster_Load(object sender, EventArgs e)
        {
            gvVehicleDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            FillVehicleType();
            FillCompanyData();
            //FillLocationData();
            FillVehicleInformation();
            cbPermit.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            cbVehType.SelectedIndex = 0;
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
        private void FillLocationData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            cbLocation.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE+'@'+STATE_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' ORDER BY BRANCH_NAME ASC";
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
                    cbLocation.ValueMember = "branchCode";
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
        private bool CheckData()
        {
            bool flag = true;
            if(cbCompany.SelectedIndex==0)
            {
                MessageBox.Show("Select Company","Transport Vehicle Master",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                cbCompany.Focus();
                flag = false;
            }
            else if (txtVehicleNo.Text == string.Empty)
            {
                MessageBox.Show("Enter Vehicle No", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtVehicleNo.Focus();
                flag = false;
            }
            else if (cbLocation.SelectedIndex == 0)
            {
                MessageBox.Show("Select Location", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbLocation.Focus();
                flag = false;
            }
            else if (cbVehType.SelectedIndex == 0)
            {
                MessageBox.Show("Select Vehicle Type", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbVehType.Focus();
                flag = false;
            }
            else if (cbVehicleName.SelectedIndex == 0)
            {
                MessageBox.Show("Select Vehicle Name", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbVehicleName.Focus();
                flag = false;
            }
            else if (txtEngineNo.Text == string.Empty)
            {
                MessageBox.Show("Enter Engine No", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtEngineNo.Focus();
                flag = false;
            }
            else if (cbPermit.SelectedIndex == 0)
            {
                MessageBox.Show("Selct Permit Type", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbPermit.Focus();
                flag = false;
            }
            else if (txtHPA.Text == string.Empty)
            {
                MessageBox.Show("Enter HPA", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtHPA.Focus();
                flag = false;
            }
            else if (txtClassNo.Text == string.Empty)
            {
                MessageBox.Show("Enter Chassis No", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtClassNo.Focus();
                flag = false;
            }
            else if (cbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Select Status Type", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbStatus.Focus();
                flag = false;
            }
            

            return flag;
        }
        private void FillVehicleInformation()
        {
            gvVehicleDetails.Rows.Clear();
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT TVM_VEHICLE_NUMBER,TVM_VEHICLE_TYPE,TVM_VEHICLE_NAME,TVM_ENGINE_NUMBER,TVM_BRANCH_CODE,TVM_DATE_OF_REG,TVM_CHASSIS_NUMBER,TVM_PERMIT_FLAG,TVM_HPA_FLAG,TVM_VEHICLE_STATUS,TVM_STATE_CODE,TVM_COMPANY_CODE FROM TR_VEHICLE_MAS INNER JOIN BRANCH_MAS ON BRANCH_CODE = TVM_BRANCH_CODE";
                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                      
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                        cellSNo.Value = (i + 1).ToString();
                        tempRow.Cells.Add(cellSNo);

                       
                        
                        DataGridViewCell cellVNo = new DataGridViewTextBoxCell();
                        cellVNo.Value = dt.Rows[i]["TVM_VEHICLE_NUMBER"];
                        tempRow.Cells.Add(cellVNo);

                        DataGridViewCell cellVType = new DataGridViewTextBoxCell();
                        cellVType.Value = dt.Rows[i]["TVM_VEHICLE_TYPE"];
                        tempRow.Cells.Add(cellVType);

                        DataGridViewCell cellVName = new DataGridViewTextBoxCell();
                        cellVName.Value = dt.Rows[i]["TVM_VEHICLE_NAME"];
                        tempRow.Cells.Add(cellVName);
                        
                        DataGridViewCell cellEngineNo = new DataGridViewTextBoxCell();
                        cellEngineNo.Value = dt.Rows[i]["TVM_ENGINE_NUMBER"];
                        tempRow.Cells.Add(cellEngineNo);

                        DataGridViewCell cellLocation = new DataGridViewTextBoxCell();
                        cellLocation.Value = dt.Rows[i]["TVM_BRANCH_CODE"];
                        tempRow.Cells.Add(cellLocation);

                        DataGridViewCell cellLocationName = new DataGridViewTextBoxCell();
                        cellLocationName.Value = dt.Rows[i]["TVM_BRANCH_CODE"];
                        tempRow.Cells.Add(cellLocationName);

                        DataGridViewCell cellDOR = new DataGridViewTextBoxCell();
                        cellDOR.Value = Convert.ToDateTime(dt.Rows[i]["TVM_DATE_OF_REG"]).ToShortDateString();
                        tempRow.Cells.Add(cellDOR);

                        DataGridViewCell cellClassNo = new DataGridViewTextBoxCell();
                        cellClassNo.Value = dt.Rows[i]["TVM_CHASSIS_NUMBER"];
                        tempRow.Cells.Add(cellClassNo);

                        DataGridViewCell cellPermit = new DataGridViewTextBoxCell();
                        cellPermit.Value = dt.Rows[i]["TVM_PERMIT_FLAG"];
                        tempRow.Cells.Add(cellPermit);

                        DataGridViewCell cellHPA = new DataGridViewTextBoxCell();
                        cellHPA.Value = dt.Rows[i]["TVM_HPA_FLAG"];
                        tempRow.Cells.Add(cellHPA);

                        DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                        cellStatus.Value = dt.Rows[i]["TVM_VEHICLE_STATUS"];
                        tempRow.Cells.Add(cellStatus);

                        DataGridViewCell cellState = new DataGridViewTextBoxCell();
                        cellState.Value = dt.Rows[i]["TVM_STATE_CODE"];
                        tempRow.Cells.Add(cellState);

                        DataGridViewCell cellCompCode = new DataGridViewTextBoxCell();
                        cellCompCode.Value = dt.Rows[i]["TVM_COMPANY_CODE"];
                        tempRow.Cells.Add(cellCompCode);

                        gvVehicleDetails.Rows.Add(tempRow);
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

        private void txtVehicleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }
        private void txtEngineNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void txtClassNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void gvVehicleDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            objDb = new SQLDB();
            if (e.ColumnIndex == gvVehicleDetails.Columns["edit"].Index)
            {
                flagUpdate = true;

                txtVehicleNo.Text = gvVehicleDetails.Rows[e.RowIndex].Cells["vehicleNo"].Value.ToString();
                txtEngineNo.Text = gvVehicleDetails.Rows[e.RowIndex].Cells["engineNo"].Value.ToString();
                txtClassNo.Text = gvVehicleDetails.Rows[e.RowIndex].Cells["classisNo"].Value.ToString();
                dtpReg.Value = Convert.ToDateTime( gvVehicleDetails.Rows[e.RowIndex].Cells["dor"].Value.ToString());
                cbVehType.SelectedItem = gvVehicleDetails.Rows[e.RowIndex].Cells["vehicleType"].Value.ToString();
                cbPermit.SelectedItem = gvVehicleDetails.Rows[e.RowIndex].Cells["permit"].Value.ToString();
                //cbHpa.SelectedItem = gvVehicleDetails.Rows[e.RowIndex].Cells["hpa"].Value.ToString();
                txtHPA.Text = gvVehicleDetails.Rows[e.RowIndex].Cells["hpa"].Value.ToString();
                cbStatus.SelectedItem = gvVehicleDetails.Rows[e.RowIndex].Cells["status"].Value.ToString();
                cbCompany.SelectedValue = gvVehicleDetails.Rows[e.RowIndex].Cells["companyCode"].Value.ToString();
                cbLocation.SelectedValue = gvVehicleDetails.Rows[e.RowIndex].Cells["BranchCode"].Value.ToString() + "@" + gvVehicleDetails.Rows[e.RowIndex].Cells["state"].Value.ToString();
                txtVehicleNo.Enabled = false;

            }
            else if (e.ColumnIndex == gvVehicleDetails.Columns["del"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        string strCommand = "DELETE FROM TR_VEHICLE_MAS WHERE TVM_VEHICLE_NUMBER='" + gvVehicleDetails.Rows[e.RowIndex].Cells["vehicleNo"].Value + "';";
                        int iRes = objDb.ExecuteSaveData(strCommand);
                        if (iRes > 0)
                        {
                            FillVehicleInformation();
                            MessageBox.Show("Data Deleted Succesfully", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;

                        }
                        else
                        {
                            MessageBox.Show("Data Not Deleted", "Transport Vehicle Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objDb = null;
                        btnClear_Click(null, null);
                        txtVehicleNo.Enabled = true;
                    }
                }
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocationData();
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillVehicleType();
        }
        

       
        private void FillVehicleType()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            //cbVehType.DataSource = null;
            try
            {
               
                    string strCommand = "SELECT DISTINCT(VM_VEHICLE_TYPE) FROM VEHICLE_MASTER;";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
               
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    

                    dt.Rows.InsertAt(dr, 0);
                    cbVehType.DataSource = dt;
                    cbVehType.DisplayMember = "VM_VEHICLE_TYPE";
                    cbVehType.ValueMember = "VM_VEHICLE_TYPE";
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
        private void FillVehicleName()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            cbVehicleName.DataSource = null;
            try
            {
                if (cbVehType.SelectedIndex > 0)
                {
                    string strCommand = "SELECT VM_VEHICLE_MODEL FROM VEHICLE_MASTER where VM_VEHICLE_TYPE='" + cbVehType.SelectedValue.ToString() + "'";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                   // dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbVehicleName.DataSource = dt;
                    cbVehicleName.DisplayMember = "VM_VEHICLE_MODEL";
                    cbVehicleName.ValueMember = "VM_VEHICLE_MODEL";
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

        private void cbVehType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVehicleName();
        }

        private void txtHPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        
                              
    }
}
