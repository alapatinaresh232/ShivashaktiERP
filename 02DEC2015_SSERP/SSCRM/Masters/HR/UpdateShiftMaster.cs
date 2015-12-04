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
    public partial class UpdateShiftMaster : Form
    {
        SQLDB objDb = null;
        bool flagUpdate = false;
        public UpdateShiftMaster()
        {
            InitializeComponent();
        }
            
        private void btnSave_Click(object sender, EventArgs e)
        {
            objDb = new SQLDB();
            int iRes = 0;
            string strQuery;
            txtShiftName.Text = txtShiftName.Text.Replace(" ", "");
            txtDescription.Text = txtDescription.Text.Replace(" ", "");
 
            if (CheckData())
            {
                if (flagUpdate==false)
                {

                    strQuery = "insert into HR_SHIFT_MASTER(HSM_SHIFT"+
                                            ",HSM_SHIFT_desc"+
                                            ",HSM_SHIFT_START"+
                                            ",HSM_SHIFT_END"+
                                            ",HSM_SHIFT_CHAR" +
                                            ",HSM_ACTIVE" +
                                            ",HSM_CREATED_BY"+
                                            ",HSM_CREATED_DATE) " +
                                            "values('" + txtShiftName.Text + 
                                            "','" + txtDescription.Text + 
                                            "'," + Convert.ToDouble(dtpFromTime.Value.ToString("HH:mm")) +
                                            "," + Convert.ToDouble(dtpToTime.Value.ToString("HH:mm")) +
                                            ",'" + txtShiftID.Text +
                                            "','" + CommonData.LogUserId +
                                            "','" + cmbStatus.Text.ToString()+
                                            "',GETDATE())";

                }
                else
                {
                    strQuery = "update HR_SHIFT_MASTER "+
                                    "set HSM_SHIFT_desc='"+txtDescription.Text +
                                    "', HSM_SHIFT_START=" + Convert.ToDouble(dtpFromTime.Value.ToString("HH:mm")) +
                                    ", HSM_SHIFT_END=" + Convert.ToDouble(dtpToTime.Value.ToString("HH:mm")) +
                                    ", HSM_MODIFIED_BY='" + CommonData.LogUserId +
                                    "', HSM_ACTIVE='" + cmbStatus.Text.ToString() +
                                    "', HSM_SHIFT_CHAR='" + txtShiftID.Text +
                                    "', HSM_MODIFIED_DATE=GETDATE()" + 
                                    " where HSM_SHIFT='" + txtShiftName.Text + "'";
                }
                try
                {
                    iRes = objDb.ExecuteSaveData(strQuery);
                    
                }
                catch(Exception ex){
                     MessageBox.Show(ex.ToString());
                
                }
                if (iRes > 0)
                {
                    shiftMasterInformation();
                    MessageBox.Show("Data Saved Succesfully", "SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagUpdate = false;
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }    
         
        }
        private void shiftMasterInformation()
        {
            gvShiftMaster.Rows.Clear();
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "select HSM_SHIFT, HSM_SHIFT_desc "+
                                        ", HSM_SHIFT_START, HSM_SHIFT_END"+
                                        ", HSM_SHIFT_CHAR, HSM_ACTIVE " +
                                        "from HR_SHIFT_MASTER "+
                                        "where HSM_SHIFT ='" + txtShiftName.Text + "'";
                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSlNO = new DataGridViewTextBoxCell();
                        cellSlNO.Value = (i + 1).ToString();
                        tempRow.Cells.Add(cellSlNO);

                        DataGridViewCell cellShiftID = new DataGridViewTextBoxCell();
                        cellShiftID.Value = dt.Rows[i]["HSM_SHIFT_CHAR"];
                        tempRow.Cells.Add(cellShiftID);

                        DataGridViewCell cellShift = new DataGridViewTextBoxCell();
                        cellShift.Value = dt.Rows[i]["HSM_SHIFT"];
                        tempRow.Cells.Add(cellShift);

                        DataGridViewCell cellDesc = new DataGridViewTextBoxCell();
                        cellDesc.Value = dt.Rows[i]["HSM_SHIFT_desc"];
                        tempRow.Cells.Add(cellDesc);

                        DataGridViewCell cellFrom = new DataGridViewTextBoxCell();
                        cellFrom.Value = dt.Rows[i]["HSM_SHIFT_START"];
                        tempRow.Cells.Add(cellFrom);

                        DataGridViewCell cellTo = new DataGridViewTextBoxCell();
                        cellTo.Value = dt.Rows[i]["HSM_SHIFT_END"];
                        tempRow.Cells.Add(cellTo);

                        DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                        cellStatus.Value = dt.Rows[i]["HSM_ACTIVE"];
                        tempRow.Cells.Add(cellStatus);

                        gvShiftMaster.Rows.Add(tempRow);
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


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtShiftName.Text = "";
            txtDescription.Text = "";
            txtShiftID.Text = "";
            cmbStatus.Text = "ACTIVE";
            dtpFromTime.Value = Convert.ToDateTime("10:00");
            dtpToTime.Value = Convert.ToDateTime("18:00");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private bool CheckData()
        {
            bool flag = true;
            if (txtShiftName.Text == string.Empty)
            {
                MessageBox.Show("Enter ShiftName","SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtShiftName.Focus();
                flag = false;
            }
            else if (txtDescription.Text == string.Empty)
            {
                MessageBox.Show("Enter Description", "SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDescription.Focus();
                flag = false;
            } 
            //else if (txtFromTime.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter FromTime","SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    txtFromTime.Focus();
            //    flag = false;
            //}
            //else if (txtToTime.Text == string.Empty)
            //{
            //    MessageBox.Show("Enter ToTime", "SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    txtToTime.Focus();
            //    flag = false;
            //}

            return flag;
        }
       

        private void gvShiftMaster_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            objDb = new SQLDB();
            if (e.ColumnIndex == gvShiftMaster.Columns["Edit"].Index)
            {
                flagUpdate = true;

                txtShiftName.Text = gvShiftMaster.Rows[e.RowIndex].Cells["Shift"].Value.ToString();
                txtDescription.Text = gvShiftMaster.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                txtShiftID.Text = gvShiftMaster.Rows[e.RowIndex].Cells["ShiftID"].Value.ToString();
                cmbStatus.Text = gvShiftMaster.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                //txtFromTime.Text = gvShiftMaster.Rows[e.RowIndex].Cells["From"].Value.ToString();
                //txtToTime.Text = gvShiftMaster.Rows[e.RowIndex].Cells["TO"].Value.ToString();
                txtShiftName.Enabled = false;

            }
            else if (e.ColumnIndex == gvShiftMaster.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    int iRes = 0;
                    try
                    {
                        string strCommand = "DELETE FROM HR_SHIFT_MASTER WHERE HSM_SHIFT='" + gvShiftMaster.Rows[e.RowIndex].Cells["Shift"].Value + "';";
                        iRes = objDb.ExecuteSaveData(strCommand);
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objDb = null;
                        btnClear_Click(null, null);
                        txtShiftName.Enabled = true;
                    }
                    if (iRes > 0)
                    {
                        shiftMasterInformation();
                        MessageBox.Show("Data Deleted Succesfully", "SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;

                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "SHIFT MASTER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void txtFromTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

        private void txtToTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }


        
       
    }
}
