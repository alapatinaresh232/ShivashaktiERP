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
    public partial class HolidaysMaster : Form
    {
        private SQLDB objSQLDb;
        bool bFlag = false;


        public HolidaysMaster()
        {
            InitializeComponent();
        }

       

        private void HolidaysMaster_Load(object sender, EventArgs e)
        {
            nmYear.Value = DateTime.Now.Year;
            dtpHolidayDate.Value = DateTime.Today;
            FillHolidaysToGrid();
            GenerateHolidayId();

        }

        private void GenerateHolidayId()
        {
            objSQLDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT ISNULL(MAX(HHM_HOLIDAY_ID),0)+1 AS HolidayId FROM HR_HOLIDAY_MASTER";
                dt = objSQLDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtHolId.Text = dt.Rows[0]["HolidayId"] + "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDb = null;
                dt = null;
            }
        }
        private void FillHolidaysToGrid()
        {
            objSQLDb = new SQLDB();
            DataTable dt = new DataTable();
            int intRow = 1;
            gvHolidaysDetails.Rows.Clear();
            try
            {
                string strCommand = "SELECT HHM_HOLIDAY_ID,HHM_HOLIDAY_NAME "+
                                    ",HSM_HOLIDAY_DATE,HSM_HOLIDAY_DESCRIPTION "+
                                    " FROM HR_HOLIDAY_MASTER "+
                                    " WHERE DATEPART(yyyy,HSM_HOLIDAY_DATE)='"+ nmYear.Value +"'";
                dt=objSQLDb.ExecuteDataSet(strCommand).Tables[0];
               
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSLNO);


                        DataGridViewCell cellHolidayId = new DataGridViewTextBoxCell();
                        cellHolidayId.Value = dt.Rows[i]["HHM_HOLIDAY_ID"];
                        tempRow.Cells.Add(cellHolidayId);

                        
                        DataGridViewCell cellHolidayDate = new DataGridViewTextBoxCell();
                        cellHolidayDate.Value = Convert.ToDateTime(dt.Rows[i]["HSM_HOLIDAY_DATE"]).ToShortDateString();
                        tempRow.Cells.Add(cellHolidayDate);

                        DataGridViewCell cellHolidayName = new DataGridViewTextBoxCell();
                        cellHolidayName.Value = dt.Rows[i]["HHM_HOLIDAY_NAME"];
                        tempRow.Cells.Add(cellHolidayName);

                        DataGridViewCell cellHolidayDesc = new DataGridViewTextBoxCell();
                        cellHolidayDesc.Value = dt.Rows[i]["HSM_HOLIDAY_DESCRIPTION"];
                        tempRow.Cells.Add(cellHolidayDesc);


                       
                        gvHolidaysDetails.Rows.Add(tempRow);
                    }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDb = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            string strCmd = "";
            if (txtHolName.Text != "")
            {
                try
                {
                    objSQLDb = new SQLDB();
                    if (bFlag)
                    {
                        strCmd = "UPDATE HR_HOLIDAY_MASTER SET HHM_HOLIDAY_NAME='" + txtHolName.Text +
                                 "',HSM_HOLIDAY_DATE='" + Convert.ToDateTime(dtpHolidayDate.Value).ToString("dd/MMM/yyyy") +
                                 "',HSM_HOLIDAY_DESCRIPTION='" + txtHolDesc.Text + 
                                 "',HSM_MODIFIED_BY='" + CommonData.LogUserId +
                                 "', HSM_MODIFIED_DATE='" + CommonData.CurrentDate + 
                                 "' WHERE HHM_HOLIDAY_ID= " + txtHolId.Text + " ";
                    }
                    else
                    {

                        strCmd = "INSERT INTO HR_HOLIDAY_MASTER(HHM_HOLIDAY_ID " +
                                 ",HHM_HOLIDAY_NAME " +
                                 ",HSM_HOLIDAY_DESCRIPTION " +
                                 ",HSM_HOLIDAY_DATE " +
                                 ",HSM_CREATED_BY,HSM_CREATED_DATE)VALUES(" + Convert.ToInt32(txtHolId.Text) +
                                 ",'" + txtHolName.Text +
                                 "','" + txtHolDesc.Text +
                                 "','" + Convert.ToDateTime(dtpHolidayDate.Value).ToString("dd/MMM/yyyy") +
                                 "','" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "') ";
                    }

                    iRes = objSQLDb.ExecuteSaveData(strCmd);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    bFlag = false;
                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillHolidaysToGrid();
                    btnCancel_Click(null, null);
                    
                }
                else
                {
                    MessageBox.Show("Data not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Holiday Name","Holidays Master",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }


            

        }

        private void gvHolidaysDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iResult =0;
            if (e.ColumnIndex == 5)
            {
                txtHolId.Text = gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayId"].Value.ToString();
                txtHolName.Text = gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayName"].Value.ToString();
                txtHolDesc.Text = gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayDesc"].Value.ToString();
                dtpHolidayDate.Value = Convert.ToDateTime(gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayDate"].Value.ToString());
                bFlag = true;
                txtHolName.Focus();

            }

            if (e.ColumnIndex == 6)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {


                    objSQLDb = new SQLDB();
                    try
                    {
                        string strCommand = "DELETE FROM HR_HOLIDAY_MASTER WHERE HHM_HOLIDAY_ID =" + gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayId"].Value + " ";
                        iResult = objSQLDb.ExecuteSaveData(strCommand);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objSQLDb = null;
                    }

                    if (iResult > 0)
                    {

                        bFlag = false;
                        MessageBox.Show("Data Deleted Successfully", "Holidays Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GenerateHolidayId();
                        FillHolidaysToGrid();
                        btnCancel_Click(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("Data not Deleted", "Holidays Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void nmYear_ValueChanged(object sender, EventArgs e)
        {
            FillHolidaysToGrid();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtHolName.Text = "";
            dtpHolidayDate.Value = DateTime.Today;
            txtHolDesc.Text = "";
            GenerateHolidayId();

        }

        private void txtHolName_KeyPress(object sender, KeyPressEventArgs e)
        {
             e.KeyChar = (e.KeyChar.ToString()).ToUpper().ToCharArray()[0];

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //if (gvHolidaysDetails.Rows.Count > 0)
            //{
                ReportViewer childReportViewer = new ReportViewer(Convert.ToInt32(nmYear.Value.ToString()));
                CommonData.ViewReport = "SSCRM_HOLIDAYMASTER";
                childReportViewer.Show();
            //}
        }

 
       
    }
}
