using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SDMS.App_Code;
using SSCRMDB;
using System.Windows.Forms;

namespace SDMS
{
    public partial class SecurityCheq : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] dr;
        bool updateFlag = false;
        public SecurityCheq()
        {
            InitializeComponent();
        }
        private void SecurityCheq_Load(object sender, EventArgs e)
        {
            SQLDB objectDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                dt = objectDB.ExecuteDataSet("SELECT DBM_BANK_NAME FROM DL_BANK_MASTER").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";


                    dt.Rows.InsertAt(dr, 0);
                    cbBankName.DataSource = dt;
                    cbBankName.DisplayMember = "DBM_BANK_NAME";
                    cbBankName.ValueMember = "DBM_BANK_NAME";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objectDB = null;
                dt = null;
            }
            GetSecuityCheques();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //UtilityLibrary oUtility = new UtilityLibrary();
            //if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
            //    return;

            //bellow line for delete the row in dtEducation table
            if (updateFlag)
            {
                ((DealarApplicationForm)dealerApplication).dtSecurityCheqs.Rows.Remove(dr[0]);
                updateFlag = false;
            }
            //till here
            if (txtCheqNo.Text.Length > 0 && cbBankName.SelectedIndex > 0 )
            {
                ((DealarApplicationForm)dealerApplication).dtSecurityCheqs.Rows.Add(new Object[] { "-1", txtCheqNo.Text, cbBankName.SelectedValue.ToString().ToUpper(), cbBranchName.Text.ToUpper() });
                GetSecuityCheques();
                txtCheqNo.Text = string.Empty;
                cbBankName.SelectedIndex = 0;
                //cbBranchName.SelectedIndex = 0;
                cbBranchName.Text = "";
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetSecuityCheques()
        {
            int intRow = 1;
            DataTable dt=((DealarApplicationForm)dealerApplication).dtSecurityCheqs;
            gvSecurityCheq.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dt.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellCheqNO = new DataGridViewTextBoxCell();
                cellCheqNO.Value = dt.Rows[i]["CheqNo"];
                tempRow.Cells.Add(cellCheqNO);

                DataGridViewCell cellBankName = new DataGridViewTextBoxCell();
                cellBankName.Value = dt.Rows[i]["BankName"];
                tempRow.Cells.Add(cellBankName);

                DataGridViewCell cellBranchName = new DataGridViewTextBoxCell();
                cellBranchName.Value = dt.Rows[i]["BranchName"];
                tempRow.Cells.Add(cellBranchName);

                intRow = intRow + 1;
                gvSecurityCheq.Rows.Add(tempRow);
            }
            dt = null;
        }

        private void gvSecurityCheq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = ((DealarApplicationForm)dealerApplication).dtSecurityCheqs;
            if (e.RowIndex >= 0)
            {
                if (gvSecurityCheq.Rows[e.RowIndex].Cells["Edit_SecCheq"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvSecurityCheq.Rows[e.RowIndex].Cells["Edit_SecCheq"].Selected) == true)
                    {
                        updateFlag = true;
                        int SlNo = Convert.ToInt32(gvSecurityCheq.Rows[e.RowIndex].Cells[gvSecurityCheq.Columns["SLNO_SecCheq"].Index].Value);
                         dr = dt.Select("SlNo=" + SlNo);
                        //Partners objPartners = new Partners(dr);
                        //objPartners.dealerApplication = this;
                        //objPartners.ShowDialog();
                        txtCheqNo.Text = dr[0][1].ToString();
                        cbBankName.SelectedValue = dr[0][2].ToString();
                        cbBranchName.Text = dr[0][3].ToString();
                    }
                }
                if (e.ColumnIndex == gvSecurityCheq.Columns["Delete_SecCheq"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvSecurityCheq.Rows[e.RowIndex].Cells[gvSecurityCheq.Columns["SLNO_SecCheq"].Index].Value);
                        DataRow[] dr = dt.Select("SlNo=" + SlNo);
                        ((DealarApplicationForm)dealerApplication).dtSecurityCheqs.Rows.Remove(dr[0]);
                        GetSecuityCheques();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void cbBranchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        
    }
}
