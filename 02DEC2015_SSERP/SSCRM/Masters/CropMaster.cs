using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
namespace SSCRM
{
    public partial class CropMaster : Form
    {
        SQLDB objsqldb = null;
        DataSet ds = null;
        DataTable dt = null;
        bool flage = false;
        public int CROPID =0;
        
        //string StrCommand = "";
        public CropMaster()
        {
            InitializeComponent();
        }

        private void CropMaster_Load(object sender, EventArgs e)
        {
            dgvCropMaster.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
            FillCropMasterDelToGrid();
        }
        

        private void btbClear_Click(object sender, EventArgs e)
        {
            flage = false;
            txtCropName.Text = "";
            txtDescription.Text = "";
        }
        private void FillCropMasterDelToGrid()
        {
            objsqldb = new SQLDB();
            ds = new DataSet();
            string StrCommand = "";

            try
            {
                StrCommand = "SELECT  * FROM CROP_MASTER ";
                ds = objsqldb.ExecuteDataSet(StrCommand);
                dt = ds.Tables[0];
                dgvCropMaster.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow temprow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = (i + 1).ToString();
                    temprow.Cells.Add(cellSLNO);

                    DataGridViewCell cellCropId = new DataGridViewTextBoxCell();
                    cellCropId.Value = dt.Rows[i]["CROP_ID"].ToString();
                    temprow.Cells.Add(cellCropId);

                    DataGridViewCell cellCropName = new DataGridViewTextBoxCell();
                    cellCropName.Value = dt.Rows[i]["CROP_NAME"].ToString();
                    temprow.Cells.Add(cellCropName);

                    DataGridViewCell cellCropDescription = new DataGridViewTextBoxCell();
                    cellCropDescription.Value = dt.Rows[i]["CROP_DESC"].ToString();
                    temprow.Cells.Add(cellCropDescription);
                    dgvCropMaster.Rows.Add(temprow);

                    

 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objsqldb = null;
                ds = null;
                dt = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {   
            int ival =0;

        if (txtCropName.Text != "")
        {
            objsqldb = new SQLDB();
            string StrCommad = "";

            try
            {


                if (flage)
                {
                    StrCommad = "UPDATE CROP_MASTER SET CROP_NAME = '" + txtCropName.Text +
                                                      "', CROP_DESC  = '" + txtDescription.Text + "' WHERE CROP_ID = " + CROPID + " ";
                    
                }
                else
                {

                    StrCommad = "INSERT INTO  CROP_MASTER (CROP_NAME,CROP_DESC)VALUES('" + txtCropName.Text.ToUpper() +
                        "','" + txtDescription.Text.ToUpper() + "')";
                   
                }


                ival = objsqldb.ExecuteSaveData(StrCommad);
                txtCropName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objsqldb = null;
            }
            if (ival > 0)
            {
                flage = false;
                FillCropMasterDelToGrid();
             
                MessageBox.Show("Data saved Succefully", "CropMaster", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data NotSaved Succefully", "CropMaster", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Please enter the CropName Name");
        }
          txtCropName.Clear();
          txtDescription.Clear(); 
            

        }
    

        private void dgvCropMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ival = 0;
            if (e.ColumnIndex == 4)
            {
                
                CROPID =Convert.ToInt32( dgvCropMaster.Rows[e.RowIndex].Cells["CROP_ID"].Value);
               txtCropName.Text = dgvCropMaster.Rows[e.RowIndex].Cells["CROP_NAME"].Value.ToString();
               txtDescription.Text = dgvCropMaster.Rows[e.RowIndex].Cells["CROP_DESC"].Value.ToString();
               flage = true;
            }
            if (e.ColumnIndex == 5)
            {
                objsqldb = new SQLDB();
                string StrCommand = "";
                try
                {
                    StrCommand  =" DELETE FROM CROP_MASTER WHERE CROP_ID = ' " + dgvCropMaster.Rows[e.RowIndex].Cells["CROP_ID"].Value.ToString() + " '";
                    ival = objsqldb.ExecuteSaveData(StrCommand);
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (ival > 0)
                {
                    FillCropMasterDelToGrid();
                    flage = false;
                    txtCropName.Clear();
                    txtDescription.Clear();
                    MessageBox.Show("Data Deleted Successfully", "Crop Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data not Deleted,Crop", "Master",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
