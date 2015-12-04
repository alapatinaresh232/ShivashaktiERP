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
    public partial class PolutionCheckDetails : Form
    {
        SQLDB objDb = null;
        bool flagUpdate = false;
        public PolutionCheckDetails()
        {
            InitializeComponent();
        }

        

        private void PolutionCheckDetails_Load(object sender, EventArgs e)
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
                    string strCommand = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";
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
                    string strCommand = "SELECT TVM_VEHICLE_NAME+' - ('+TVM_VEHICLE_NUMBER+')' AS VEHICLE,TVM_VEHICLE_NUMBER FROM TR_VEHICLE_MAS WHERE TVM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND TVM_BRANCH_CODE='" + cbLocation.SelectedValue.ToString() + "'";
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void cbVehNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVehiclePolCheckDetails();
        }
        private void FillVehiclePolCheckDetails()
        {
            gvPolCheckDetails.Rows.Clear();
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            if (cbVehNo.SelectedIndex > 0)
            {
                try
                {
                    string strCommand = "SELECT TVM_VEHICLE_NAME,VPPD_VEHICLE_REG_NO,VPPD_SL_NO,VPPD_DOC_NO,VPPD_VALID_FROM,VPPD_VALID_TO,VPPD_CREATED_BY FROM TR_VEHICLE_PERMIT_PC_DETL INNER JOIN TR_VEHICLE_MAS VM ON TVM_VEHICLE_NUMBER=VPPD_VEHICLE_REG_NO WHERE VPPD_VEHICLE_REG_NO='" + cbVehNo.SelectedValue.ToString() + "' AND VPPD_DOC_TYPE ='POLCHK'";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                            cellSNo.Value = (i + 1).ToString();
                            tempRow.Cells.Add(cellSNo);

                            DataGridViewCell cellPolChekSNo = new DataGridViewTextBoxCell();
                            cellPolChekSNo.Value = dt.Rows[i]["VPPD_SL_NO"];
                            tempRow.Cells.Add(cellPolChekSNo);


                            DataGridViewCell cellVNo = new DataGridViewTextBoxCell();
                            cellVNo.Value = dt.Rows[i]["VPPD_VEHICLE_REG_NO"];
                            tempRow.Cells.Add(cellVNo);


                            DataGridViewCell cellVName = new DataGridViewTextBoxCell();
                            cellVName.Value = dt.Rows[i]["TVM_VEHICLE_NAME"];
                            tempRow.Cells.Add(cellVName);


                            DataGridViewCell cellPNo = new DataGridViewTextBoxCell();
                            cellPNo.Value = dt.Rows[i]["VPPD_DOC_NO"];
                            tempRow.Cells.Add(cellPNo);

                            DataGridViewCell cellValidFrom = new DataGridViewTextBoxCell();
                            cellValidFrom.Value = Convert.ToDateTime(dt.Rows[i]["VPPD_VALID_FROM"]).ToShortDateString();
                            tempRow.Cells.Add(cellValidFrom);

                            DataGridViewCell cellValidTo = new DataGridViewTextBoxCell();
                            cellValidTo.Value = Convert.ToDateTime(dt.Rows[i]["VPPD_VALID_TO"]).ToShortDateString();
                            tempRow.Cells.Add(cellValidTo);


                            gvPolCheckDetails.Rows.Add(tempRow);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strCommand = "";
            DataTable dt = new DataTable();
            objDb = new SQLDB();
            try
            {
                if (checkData())
                {
                    //strCommand = "SELECT ISNULL(MAX(VPPD_SL_NO), 0)+1 FROM TR_VEHICLE_PERMIT_PC_DETL";
                    //DataTable dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                    //strCommand = "";
                    //dt = null;
                    if (flagUpdate)
                    {
                        strCommand = " UPDATE TR_VEHICLE_PERMIT_PC_DETL SET VPPD_VEHICLE_REG_NO='" + cbVehNo.SelectedValue.ToString() + "',VPPD_DOC_NO='" + txtPolCheckNo.Text.ToUpper().Replace(" ", "") + "',VPPD_VALID_FROM='" + Convert.ToDateTime(dtpValidFrom.Value).ToString("dd/MMM/yyyy") + "',VPPD_VALID_TO='" + Convert.ToDateTime(dtpValidTo.Value).ToString("dd/MMM/yyyy") + "',VPPD_DOC_TYPE='POLCHK',VPPD_LAST_MODIFIED_BY='',VPPD_LAST_MODIFIED_DATE=getDate() WHERE VPPD_VEHICLE_REG_NO='" + cbVehNo.SelectedValue.ToString() + "' AND VPPD_DOC_TYPE='POLCHK' AND VPPD_DOC_NO='" + txtPolCheckNo.Text + "'";
                        flagUpdate = false;
                        txtPolCheckNo.Enabled = true;
                    }
                    else
                    {
                        string[] strVehicle = cbVehNo.SelectedValue.ToString().Split('(');
                        strCommand = "SELECT VPPD_DOC_NO FROM TR_VEHICLE_PERMIT_PC_DETL WHERE VPPD_DOC_NO='"+txtPolCheckNo.Text+"'";
                        
                        dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                         if (dt.Rows.Count == 0)
                        {
                            strCommand = "SELECT ISNULL(MAX(VPPD_SL_NO), 0)+1 FROM TR_VEHICLE_PERMIT_PC_DETL";
                            dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                            
                            strCommand = "INSERT INTO TR_VEHICLE_PERMIT_PC_DETL(VPPD_SL_NO,VPPD_VEHICLE_REG_NO,VPPD_DOC_NO,VPPD_VALID_FROM,VPPD_VALID_TO,VPPD_CREATED_BY,VPPD_DOC_TYPE,VPPD_CREATED_DATE) VALUES(" + Convert.ToInt32(dt.Rows[0][0]) + 
                                ",'" + strVehicle[1] + "','" + txtPolCheckNo.Text.ToUpper().Replace(" ", "") + 
                                "','" + Convert.ToDateTime(dtpValidFrom.Value).ToString("dd/MMM/yyyy") + 
                                "','" + Convert.ToDateTime(dtpValidTo.Value).ToString("dd/MMM/yyyy") + "','','POLCHK',GETDATE())";
                        }
                        else
                            MessageBox.Show("Polution Check No already Exists");
                    }
                    int iRes = objDb.ExecuteSaveData(strCommand);
                    strCommand = "";
                    dt = null;
                    if (iRes > 0)
                    {
                        FillVehiclePolCheckDetails();
                        MessageBox.Show("Data Saved Succesfully", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtPolCheckNo.Text = "";
                dtpValidFrom.Value = DateTime.Today;
                dtpValidTo.Value = DateTime.Today;

            }
        }
        private bool checkData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbCompany.Focus();
                flag = false;
            }
            else if (cbLocation.SelectedIndex == 0)
            {
                MessageBox.Show("Select Location", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbLocation.Focus();
                flag = false;
            }
            else if (cbVehNo.SelectedIndex == 0)
            {
                MessageBox.Show("Select Vehicle No", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbVehNo.Focus();
                flag = false;
            }
            else if (txtPolCheckNo.Text == string.Empty)
            {
                MessageBox.Show("Enter PolutionCheck No", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPolCheckNo.Focus();
                flag = false;
            }
            else if (!(dtpValidTo.Value > dtpValidFrom.Value))
            {
                MessageBox.Show("Enter Valid Dates", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
            }
            return flag;
        }

        private void gvPolCheckDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            objDb = new SQLDB();

            if (e.ColumnIndex == gvPolCheckDetails.Columns["edit"].Index)
            {
                flagUpdate = true;
                txtPolCheckNo.Enabled = false;
                cbVehNo.SelectedValue = gvPolCheckDetails.Rows[e.RowIndex].Cells["vehicleno"].Value.ToString();
                txtPolCheckNo.Text = gvPolCheckDetails.Rows[e.RowIndex].Cells["polCheckNo"].Value.ToString();
                dtpValidFrom.Value = Convert.ToDateTime(gvPolCheckDetails.Rows[e.RowIndex].Cells["from"].Value.ToString());
                dtpValidTo.Value = Convert.ToDateTime(gvPolCheckDetails.Rows[e.RowIndex].Cells["to"].Value.ToString());
                string strvehno = gvPolCheckDetails.Rows[e.RowIndex].Cells["vehicleno"].Value.ToString();
                //string strCommand = "SELECT  TVM_COMPANY_CODE,TVM_BRANCH_CODE FROM TR_VEHICLE_MAS  WHERE TVM_VEHICLE_NUMBER ='" + strvehno + "'";
                //cbCompany.SelectedValue = objDb.ExecuteDataSet(strCommand).Tables[0].Rows[0][0];

                //cbLocation.SelectedValue = objDb.ExecuteDataSet(strCommand).Tables[0].Rows[0][1];
            }
            else if (e.ColumnIndex == gvPolCheckDetails.Columns["del"].Index)
            {

                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {

                        string strCommand = "DELETE FROM TR_VEHICLE_PERMIT_PC_DETL WHERE VPPD_VEHICLE_REG_NO='" + gvPolCheckDetails.Rows[e.RowIndex].Cells["vehicleno"].Value + "' AND VPPD_DOC_TYPE='POLCHK' AND VPPD_DOC_NO='" + gvPolCheckDetails.Rows[e.RowIndex].Cells["polCheckNo"].Value + "'";
                        int iRes = objDb.ExecuteSaveData(strCommand);
                        if (iRes > 0)
                        {
                            FillVehiclePolCheckDetails();
                            MessageBox.Show("Data Deleted Succesfully", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            txtPolCheckNo.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Data Not Deleted", "Vehicle PolutionCheck Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        txtPolCheckNo.Text = "";
                        dtpValidFrom.Value = DateTime.Today;
                        dtpValidTo.Value = DateTime.Today;

                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            txtPolCheckNo.Enabled = true;
            cbCompany.SelectedIndex = 0;
            cbLocation.DataSource = null;
            cbVehNo.DataSource = null;
            txtPolCheckNo.Text = "";
            dtpValidFrom.Value = DateTime.Today;
            dtpValidTo.Value = DateTime.Today;
        }

       

    }
}
