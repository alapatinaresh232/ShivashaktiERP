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
    public partial class TransportMaster : Form
    {
        string strConn = null, strCommand = null;        
        DataSet ds = null;
        DataTable dt = null;
        SQLDB objDb = null;
        bool flag = false;
        public TransportMaster()
        {
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            gvTransportDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
            generateId();
            FillTransportDataToGrid();
        }
        private void FillTransportDataToGrid()
        {
            objDb = new SQLDB();
            ds = new DataSet();
            try
            {
                
                strCommand = "SELECT * FROM TRANSPORT_MASTER";
                ds = objDb.ExecuteDataSet(strCommand);
                dt = ds.Tables[0];
                gvTransportDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)gvTransportDetails.RowTemplate.Clone();
                    row.CreateCells(gvTransportDetails, i + 1, dt.Rows[i]["TM_ID"], dt.Rows[i]["TM_TRANSPORT_NAME"],dt.Rows[i]["TM_TRANSPORT_ADDRESS"]);
                    gvTransportDetails.Rows.Add(row);
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

        private void gvTransportDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            if (txtTransName.Text != "")
            {
                objDb = new SQLDB();
                try
                {
                    
                    if (flag)
                    {
                        strCommand = "UPDATE TRANSPORT_MASTER SET TM_TRANSPORT_NAME='" + txtTransName.Text.ToUpper() + "',TM_TRANSPORT_ADDRESS='" + txtTransAddress.Text.ToUpper() + "' WHERE TM_ID='" + txtTransportId.Text + "'";
                    }
                    else
                    {
                        generateId();
                        strCommand = "INSERT INTO TRANSPORT_MASTER(TM_ID,TM_TRANSPORT_NAME,TM_TRANSPORT_ADDRESS) VALUES ('" + txtTransportId.Text.ToUpper() + "','" + txtTransName.Text.ToUpper() + "','" + txtTransAddress.Text.ToUpper() + "')";
                    }
                    objDb = new SQLDB();
                    iRes = objDb.ExecuteSaveData(strCommand);
                    txtTransName.Focus();
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
                    FillTransportDataToGrid();
                    MessageBox.Show("Data Saved Successfully", "Tranport Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data not Saved", "Tranport Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter the Transport Name");
                
            }
            generateId();
            txtTransName.Clear();
            txtTransAddress.Clear();
        }
         private void btnClear_Click(object sender, EventArgs e)
         {
             flag = false;
             generateId();
             txtTransName.Text = "";
             txtTransAddress.Text = "";
         }

         private void btnClose_Click(object sender, EventArgs e)
         {
             this.Dispose();
         }
         public void generateId()
         {
             objDb = new SQLDB();
             DataSet ds = new DataSet();
             try
             {
                 
                 strCommand = "SELECT MAX(CAST(SUBSTRING(TM_ID, 4, 3) AS NUMERIC))+1 FROM TRANSPORT_MASTER";
                 ds = objDb.ExecuteDataSet(strCommand);
                 txtTransportId.Text = "TP" + Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString("0000");
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
             finally
             {
                 ds = null;
                 objDb = null;
             }
         }
         private void gvTransportDetails_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
         {
             int iRes = 0;
             if (e.ColumnIndex == 4)
             {
                 txtTransportId.Text = gvTransportDetails.Rows[e.RowIndex].Cells["TM_ID"].Value.ToString();
                 txtTransName.Text = gvTransportDetails.Rows[e.RowIndex].Cells["TM_TRANSPORT_NAME"].Value.ToString();
                 txtTransAddress.Text = gvTransportDetails.Rows[e.RowIndex].Cells["TM_TRANSPORT_ADDRESS"].Value.ToString();
                 txtTransName.Focus();
                 flag = true;
             }
             if (e.ColumnIndex == 5)
             {
                 
                 
                 objDb = new SQLDB();
                 try
                 {
                     strCommand = "DELETE FROM TRANSPORT_MASTER WHERE TM_ID = '" + gvTransportDetails.Rows[e.RowIndex].Cells["TM_ID"].Value + "'";
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
                     FillTransportDataToGrid();
                     flag = false;
                     generateId();
                     txtTransName.Clear();
                     txtTransAddress.Clear();
                     MessageBox.Show("Data Deleted Successfully", "Tansport Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
                 else
                 {
                     MessageBox.Show("Data not Deleted", "Transport Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }
         }
       
    }
}
 