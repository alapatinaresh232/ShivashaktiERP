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
    public partial class CombiProductsList : Form
    {
        SQLDB objSQLdb = null;
       

        public CombiProductsList()
        {
            InitializeComponent();
        }

        private void CombiProductsList_Load(object sender, EventArgs e)
        {
            gvCombiProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                      System.Drawing.FontStyle.Regular);
            gvSingleProducts.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                     System.Drawing.FontStyle.Regular);
            FillCombiProductsToGrid();
        }

        public void FillCombiProductsToGrid()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            int intRow = 1;
            gvCombiProductDetails.Rows.Clear();
            try
            {
                string strCommand = "SELECT PM_PRODUCT_ID CombiId " +
                                    ", PM_PRODUCT_NAME CombiName " +
                                    "FROM PRODUCT_MAS " +
                                    "WHERE PM_PRODUCT_TYPE='CMBPK'";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    intRow = intRow + 1;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellCombiId = new DataGridViewTextBoxCell();
                    cellCombiId.Value = dt.Rows[i]["CombiId"];
                    tempRow.Cells.Add(cellCombiId);

                    DataGridViewCell cellCombiName = new DataGridViewTextBoxCell();
                    cellCombiName.Value = dt.Rows[i]["CombiName"];
                    tempRow.Cells.Add(cellCombiName);

                    gvCombiProductDetails.Rows.Add(tempRow);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }

        private void FillSingleProductsToGrid(string sCombiID)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
                       
            string strCommand = "";
            int intRow = 1;
            gvSingleProducts.Rows.Clear();
            try
            {

                strCommand = "SELECT PML_SNGLPCK_PRODUCT_ID SingleProdID" +
                            ",PM.PM_PRODUCT_NAME SingleProdName" +
                            ",CM.CATEGORY_NAME SingleCategory" +
                            ",PML_QTY SingleQty " +
                            ",PML_RATE_FREE_FLAG " +
                            "FROM PRODUCT_MAS_LI " +
                            "INNER JOIN PRODUCT_MAS PM " +
                            "ON PM.PM_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID " +
                            "INNER JOIN CATEGORY_MASTER CM " +
                            "ON CM.CATEGORY_ID = PM.PM_CATEGORY_ID " +
                            "INNER JOIN PRODUCT_MAS PC " +
                            "ON PC.PM_PRODUCT_ID = PML_PRODUCT_ID " +
                            "WHERE PML_PRODUCT_ID = '" + sCombiID + "'";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];


                for (int ivar = 0; ivar < dt.Rows.Count; ivar++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO1 = new DataGridViewTextBoxCell();
                    cellSLNO1.Value = intRow;
                    intRow = intRow + 1;
                    tempRow.Cells.Add(cellSLNO1);




                    DataGridViewCell cellSProductId = new DataGridViewTextBoxCell();
                    cellSProductId.Value = dt.Rows[ivar]["SingleProdID"];
                    tempRow.Cells.Add(cellSProductId);

                    DataGridViewCell cellSProdName = new DataGridViewTextBoxCell();
                    cellSProdName.Value = dt.Rows[ivar]["SingleProdName"];
                    tempRow.Cells.Add(cellSProdName);

                    DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                    cellCategoryName.Value = dt.Rows[ivar]["SingleCategory"];
                    tempRow.Cells.Add(cellCategoryName);

                    DataGridViewCell cellProdFlag = new DataGridViewTextBoxCell();
                    cellProdFlag.Value = dt.Rows[ivar]["PML_RATE_FREE_FLAG"]; ;
                    tempRow.Cells.Add(cellProdFlag);

                    DataGridViewCell cellSProdQty = new DataGridViewTextBoxCell();
                    cellSProdQty.Value = dt.Rows[ivar]["SingleQty"];
                    tempRow.Cells.Add(cellSProdQty);


                    gvSingleProducts.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null; 
                dt = null;
            }
           



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       

        private void gvCombiProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string sCombiId = gvCombiProductDetails.Rows[e.RowIndex].Cells["CombiId"].Value.ToString();
                if (e.ColumnIndex == 3)
                {


                    CombiMaster combiMas = new CombiMaster(sCombiId);
                    combiMas.objCombiProdList = this;
                    combiMas.ShowDialog();


                }


                if (e.ColumnIndex == 4)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvCombiProductDetails.Rows[e.RowIndex];
                        gvCombiProductDetails.Rows.Remove(dgvr);
                        MessageBox.Show("Data Deleted Successfully", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (gvCombiProductDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvCombiProductDetails.Rows.Count; i++)
                            {
                                gvCombiProductDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }

        private void gvCombiProductDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string SProductId = gvCombiProductDetails.Rows[e.RowIndex].Cells["CombiId"].Value.ToString();

            FillSingleProductsToGrid(SProductId);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CombiMaster objCombiMaster = new CombiMaster();
            objCombiMaster.objCombiProdList = this;
            objCombiMaster.ShowDialog();
        }
    }
}
