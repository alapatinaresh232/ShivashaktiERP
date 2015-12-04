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
    public partial class supplier : Form
    {
        SQLDB objSqlDB;
        public supplier()
        {
            InitializeComponent();
        }

        private void supplier_Load(object sender, EventArgs e)
        {
            GetMaxCode();
            lblChk.Text = "0";
            GetDataBind();
        }

        public void GetDataBind()
        {
            objSqlDB = new SQLDB();
            DataSet ds = objSqlDB.ExecuteDataSet("select *From supplier_master");
            objSqlDB = null;
            DataTable dt = ds.Tables[0];
            gvSupplier.Rows.Clear();
            int intRow = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellCCode = new DataGridViewTextBoxCell();
                cellCCode.Value = dt.Rows[i]["SM_SUPPLIER_CODE"];
                tempRow.Cells.Add(cellCCode);

                DataGridViewCell cellCName = new DataGridViewTextBoxCell();
                cellCName.Value = dt.Rows[i]["SM_SUPPLIER_NAME"];
                tempRow.Cells.Add(cellCName);

                DataGridViewCell cellMobile = new DataGridViewTextBoxCell();
                cellMobile.Value = dt.Rows[i]["SM_SUPPLIER_MOBILE_NO"];
                tempRow.Cells.Add(cellMobile);

                intRow = intRow + 1;
                gvSupplier.Rows.Add(tempRow);
            }
        }

        public void GetMaxCode()
        {
            objSqlDB = new SQLDB();
            DataSet dsMax = objSqlDB.ExecuteDataSet("SELECT MAX(SUBSTRING(SM_SUPPLIER_CODE,5,7)) FROM SUPPLIER_MASTER");
            int iMaxval = Convert.ToInt32(dsMax.Tables[0].Rows[0][0]) + 1;
            txtSupplierCode.Text = "SUP-" + iMaxval.ToString("000000");
            objSqlDB = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvSupplier.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    string sSuppCode = gvSupplier.Rows[e.RowIndex].Cells[gvSupplier.Columns["SM_SUPPLIER_CODE"].Index].Value.ToString();
                    objSqlDB = new SQLDB();
                    DataSet dsEdit = objSqlDB.ExecuteDataSet("SELECT *FROM SUPPLIER_MASTER WHERE SM_SUPPLIER_CODE='" + sSuppCode + "'");
                    if (dsEdit.Tables[0].Rows.Count > 0)
                    {
                        lblChk.Text = "1";
                        txtSupplierCode.Text = dsEdit.Tables[0].Rows[0]["SM_SUPPLIER_CODE"].ToString();
                        txtSupplierName.Text = dsEdit.Tables[0].Rows[0]["SM_SUPPLIER_NAME"].ToString();
                        txtAddress.Text = dsEdit.Tables[0].Rows[0]["SM_SUPPLIER_ADDRESS"].ToString();
                        txtContactNos.Text = dsEdit.Tables[0].Rows[0]["SM_SUPPLIER_CONTACT_NO"].ToString();
                        txtMobile.Text = dsEdit.Tables[0].Rows[0]["SM_SUPPLIER_MOBILE_NO"].ToString();
                        txtEmailID.Text = dsEdit.Tables[0].Rows[0]["SM_MAIL_ID"].ToString();
                    }
                    else
                    {
                        lblChk.Text = "0";
                        btnClear_Click(null, null);
                    }
                    objSqlDB = null;
                }
                if (e.ColumnIndex == gvSupplier.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        string sSuppCode = gvSupplier.Rows[e.RowIndex].Cells[gvSupplier.Columns["SM_SUPPLIER_CODE"].Index].Value.ToString();
                        objSqlDB = new SQLDB();
                        int iRetVal = objSqlDB.ExecuteSaveData("DELETE FROM SUPPLIER_MASTER WHERE SM_SUPPLIER_CODE='" + sSuppCode + "'");
                        objSqlDB = null;
                        if (iRetVal > 0)
                            MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No Deleted data", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                        GetDataBind();
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSupplierCode.Text = "";
            txtSupplierName.Text = "";
            txtAddress.Text = "";
            txtContactNos.Text = "";
            txtMobile.Text = "";
            txtEmailID.Text = "";
            lblChk.Text = "0";
            GetMaxCode();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlQry = "";
            if (lblChk.Text == "1")
            {
                sqlQry = "UPDATE SUPPLIER_MASTER SET SM_SUPPLIER_NAME='" + txtSupplierName.Text + "',SM_SUPPLIER_ADDRESS='" + txtAddress.Text + "',SM_SUPPLIER_CONTACT_NO='" + txtContactNos.Text + 
                    "',SM_SUPPLIER_MOBILE_NO='" + txtMobile.Text + "',SM_MAIL_ID='" + txtEmailID.Text + "' WHERE SM_SUPPLIER_CODE='" + txtSupplierCode.Text + "'";
            }
            else
            {
                sqlQry = "INSERT INTO SUPPLIER_MASTER (SM_SUPPLIER_CODE,SM_SUPPLIER_NAME,SM_SUPPLIER_ADDRESS,SM_SUPPLIER_CONTACT_NO,SM_SUPPLIER_MOBILE_NO,SM_MAIL_ID) VALUES " +
                    "('" + txtSupplierCode.Text + "','" + txtSupplierName.Text + "','" + txtAddress.Text + "','" + txtContactNos.Text + "','" + txtMobile.Text + "','" + txtEmailID.Text + "')";
            }
            objSqlDB = new SQLDB();
            int iRetVal = objSqlDB.ExecuteSaveData(sqlQry);
            objSqlDB = null;
            if (iRetVal > 0)
                MessageBox.Show("Inserted Data Successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data not Inserted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(null, null);
            GetDataBind();
        }
    }
}
