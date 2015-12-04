using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSTrans;
using SSCRMDB;
namespace SSCRM
{
    public partial class MultipleEntry : Form
    {
        HRInfo objHRInfo = new HRInfo();
        SQLDB objSQLDB = new SQLDB();
        public MultipleEntry()
        {
            InitializeComponent();
        }

        private void MultipleEntry_Load(object sender, EventArgs e)
        {
            GetDatatoGrid();
        }
        public void GetDatatoGrid()
        {
            objHRInfo = new HRInfo();
            DataSet ds = objHRInfo.GetDuplicatRecords();
            objHRInfo = null;
            DataTable dt = ds.Tables[0];
            int intRow = 1;
            gvMultiple.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellAppNo = new DataGridViewTextBoxCell();
                cellAppNo.Value = dt.Rows[i]["HAMH_APPL_NUMBER"];
                tempRow.Cells.Add(cellAppNo);

                DataGridViewCell celleCode = new DataGridViewTextBoxCell();
                celleCode.Value = dt.Rows[i]["HAMH_EORA_CODE"];
                tempRow.Cells.Add(celleCode);

                DataGridViewCell cellename = new DataGridViewTextBoxCell();
                cellename.Value = dt.Rows[i]["HAMH_NAME"];
                tempRow.Cells.Add(cellename);

                DataGridViewCell cellFname = new DataGridViewTextBoxCell();
                cellFname.Value = dt.Rows[i]["HAMH_FORH_NAME"];
                tempRow.Cells.Add(cellFname);

                DataGridViewCell cellDOB = new DataGridViewTextBoxCell();
                cellDOB.Value = Convert.ToDateTime(dt.Rows[i]["HAMH_DOB"]).ToString("dd/MMM/yyyy"); ;
                tempRow.Cells.Add(cellDOB);

                DataGridViewCell cellDOJ = new DataGridViewTextBoxCell();
                cellDOJ.Value = Convert.ToDateTime(dt.Rows[i]["HAMH_DOJ"]).ToString("dd/MMM/yyyy");
                tempRow.Cells.Add(cellDOJ);

                DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                cellStatus.Value = dt.Rows[i]["STATUS"];
                tempRow.Cells.Add(cellStatus);

                intRow = intRow + 1;
                gvMultiple.Rows.Add(tempRow);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvMultiple_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvMultiple.Columns["Delete"].Index)
                {
                    string Status = gvMultiple.Rows[e.RowIndex].Cells[gvMultiple.Columns["STATUS"].Index].Value.ToString();
                    if (Status == "WORKING" && CommonData.LogUserId.ToUpper()!="ADMIN")
                        MessageBox.Show("This record cannot deleted.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        //DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (dlgResult == DialogResult.Yes)
                        {
                            string sEcode = gvMultiple.Rows[e.RowIndex].Cells[gvMultiple.Columns["HAMH_EORA_CODE"].Index].Value.ToString();
                            string sApplNo = gvMultiple.Rows[e.RowIndex].Cells[gvMultiple.Columns["HAMH_APPL_NUMBER"].Index].Value.ToString();
                            objHRInfo = new HRInfo();
                            int isqlqry = objHRInfo.DelDuplicatRecords(Convert.ToInt32(sApplNo), Convert.ToInt32(sEcode), 102);
                            objHRInfo = null;
                            if (isqlqry > 0)
                            {
                                //MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            }
                            else
                                MessageBox.Show("Selected information not Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            GetDatatoGrid();
                        }
                    }
                }
            }
        }
    }
}
