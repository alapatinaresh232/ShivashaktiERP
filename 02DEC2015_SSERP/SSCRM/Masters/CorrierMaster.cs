using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using SSCRMDB;

namespace SSCRM
{
    public partial class CorrierMaster : Form
    {
        string strConn = null, strCommand = null;
        SQLDB objDb = null;
        DataSet ds = null;
        DataTable dt = null;
        bool flag = false;       
        //int rowIndex = 0;        
        public CorrierMaster()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gvCorrierDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
            generateId();
            FillCorrierDataToGrid();
            if (CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                gvCorrierDetl.Columns["Delete"].Visible = false;
            }
        }
        private void FillCorrierDataToGrid()
        {
            objDb = new SQLDB();
            ds = new DataSet();
            try
            {                
                strCommand = "SELECT * FROM CORRIER_MASTER";
                ds = objDb.ExecuteDataSet(strCommand);
                dt = ds.Tables[0];
                gvCorrierDetl.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)gvCorrierDetl.RowTemplate.Clone();
                    row.CreateCells(gvCorrierDetl, i+1, dt.Rows[i]["CM_ID"], dt.Rows[i]["CM_CORRIER_NAME"]);
                    gvCorrierDetl.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                ds = null;
                dt = null;
            }          
        }

        private void gvCorrierDetl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRes = 0;
            if (e.ColumnIndex == 3)
            {
                txtId.Text = gvCorrierDetl.Rows[e.RowIndex].Cells["CM_ID"].Value.ToString();
                txtName.Text = gvCorrierDetl.Rows[e.RowIndex].Cells["CM_CORRIER_NAME"].Value.ToString();
                txtName.Focus();
                flag = true;
            }
            if (e.ColumnIndex == 4)
            {                
                objDb = new SQLDB();
                try
                {
                    strCommand = "DELETE FROM CORRIER_MASTER WHERE CM_ID = '" + gvCorrierDetl.Rows[e.RowIndex].Cells["CM_ID"].Value + "'";
                    iRes = objDb.ExecuteSaveData(strCommand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                }
                if (iRes > 0)
                {
                    FillCorrierDataToGrid();
                    flag = false;
                    generateId();
                    txtName.Clear();
                    MessageBox.Show("Data Deleted Successfully", "Courrier Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data not Deleted", "Courrier Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            if (txtName.Text != "")
            {
                objDb = new SQLDB();
                try
                {   
                    if (flag)
                    {
                        strCommand = "UPDATE CORRIER_MASTER SET CM_CORRIER_NAME='" + txtName.Text.ToUpper() + "' WHERE CM_ID='" + txtId.Text + "'";                        
                    }
                    else
                    {
                        generateId();                        
                        strCommand = "INSERT INTO CORRIER_MASTER(CM_ID,CM_CORRIER_NAME) VALUES ('" + txtId.Text.ToUpper() + "','" + txtName.Text.ToUpper() + "')";                        
                    }
                    iRes = objDb.ExecuteSaveData(strCommand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                }
                if (iRes > 0)
                {
                    flag = false;
                    FillCorrierDataToGrid();
                    MessageBox.Show("Data Saved Successfully", "Courrier Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data not Saved", "Courrier Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter the Corrier Name");
            }            
            generateId();
            txtName.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flag = false;
            generateId();
            txtName.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void generateId()
        {
           SQLDB objDb = new SQLDB();
            DataSet ds = new DataSet();
            DataTable dt;
            try
            {   
                strCommand = "SELECT MAX(CAST(SUBSTRING(CM_ID, 3, 3) AS NUMERIC))+1 FROM CORRIER_MASTER";                
                ds = objDb.ExecuteDataSet(strCommand);
                dt = ds.Tables[0];
                txtId.Text = "CS" + Convert.ToInt32(dt.Rows[0][0]).ToString("000"); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ds = null;
                dt = null;
                objDb = null;
            }            
        }
    }
}