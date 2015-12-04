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
using SSTrans;

namespace SSCRM
{
    public partial class CombiMaster : Form
    {
        SQLDB objSQLdb = null;
        IndentDB objInv = null;
        bool flagUpdate = false;
        public CombiProductsList objCombiProdList = null;
        string sCombiId = "";
        bool flagUpdateCheck = false;
       
  
        public CombiMaster()
        {
            InitializeComponent();

        }
        public CombiMaster(string sCombiProdId)
        {
            InitializeComponent();
            sCombiId = sCombiProdId;
        }
        private void CombiMaster_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);

            FillProductsToGrid(sCombiId);
            if (flagUpdate == false)
            {
                GenerateCombiId();
            }
        }


        private void GenerateCombiId()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GenerateNewCombiID", CommandType.StoredProcedure, param);

                txtCombiId.Text = "COMBI" + Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString("0000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                ds = null;
                param = null;
            }
        }

        private DataTable checkCombiHavingProducts()
        {
            objInv = new IndentDB();
            DataTable dt=new DataTable();
            string sSingleProducts = "";
                      
            //bool bFlag = true;
            
            try
            {
                if (flagUpdate == false)
                {

                    for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                    {

                        sSingleProducts += gvProductDetails.Rows[iVar].Cells["ProductId"].Value.ToString() +
                                          "(" + gvProductDetails.Rows[iVar].Cells["ProdQty"].Value.ToString() + ")" + ",";

                    }
                    sSingleProducts = sSingleProducts.Remove(sSingleProducts.Length - 1);

                    dt = objInv.Get_CombiHavingProducts(sSingleProducts).Tables[0];
                    return dt;
                }
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                objInv = null;
               // dt = null;
            }
            return dt;


        }

      

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
            GenerateCombiName();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            //if (txtCombiName.Text!= "")
            //{
                ProductSearchAll PSearch = new ProductSearchAll("CombiMaster");
                PSearch.objCombiMaster = this;
                PSearch.ShowDialog();
                GenerateCombiName();
            //}
            //else
            //{
            //    MessageBox.Show("Please Enter Combi Name", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

        }

        private void GenerateCombiName()
        {
            string combiProdName = "";
            if (gvProductDetails.Rows.Count > 0)
            {
                for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                {

                    combiProdName += gvProductDetails.Rows[iVar].Cells["ProductName"].Value.ToString() +
                                      "(" + Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["ProdQty"].Value).ToString("0") + ")" + ", ";

                }
                combiProdName = combiProdName.TrimEnd(' ');
                combiProdName = combiProdName.TrimEnd(',');
                //combiProdName = combiProdName.Remove(combiProdName.Length - 1);
                txtCombiName.Text = combiProdName;
            }
            else
            {
                txtCombiName.Text = "";
            }

        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtCombiName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Combi Name", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCombiName.Focus();
                return flag;
            }
            else if (gvProductDetails.Rows.Count == 0)
            {
                 flag = false;
                MessageBox.Show("Please Select atleast One Product", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;

            }
            else if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString() == "0.00")
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Product Quantity", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }
                }
                DataTable dt = new DataTable();
                dt = checkCombiHavingProducts();
                if (dt.Rows.Count > 0)
                {
                    flag = false;
                    MessageBox.Show("Combi Already Having Products " + dt.Rows[0][0].ToString(), "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;

                }
            }
            return flag;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
           
            string strCommand = "";
            if (CheckData() == true)
            {

                try
                {
                   
                    if (flagUpdate == true)
                    {
                        strCommand = "UPDATE PRODUCT_MAS SET PM_PRODUCT_NAME ='" + txtCombiName.Text.ToUpper() +
                                     "',PM_UOM ='COMBO',PM_CATEGORY_ID='009',PM_PRODUCT_TYPE ='CMBPK' " +
                                     ",PM_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                     "',PM_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                                     "' WHERE PM_PRODUCT_ID='" + txtCombiId.Text.ToUpper() + "' ";
                        flagUpdate = false;
                    }
                    else
                    {

                        strCommand = "INSERT INTO PRODUCT_MAS(PM_PRODUCT_ID " +
                                                ",PM_PRODUCT_NAME " +
                                                ",PM_UOM " +
                                                ",PM_UNIT_POINTS " +
                                                ",PM_CATEGORY_ID " +
                                                ",PM_PRODUCT_TYPE " +
                                                ",PM_CREATED_BY " +
                                                ",PM_CREATED_DATE)VALUES('" + txtCombiId.Text +
                                                "','" + txtCombiName.Text.ToUpper() +
                                                "','COMBO',0,'009','CMBPK','" + CommonData.LogUserId +
                                                "','" + CommonData.CurrentDate + "')";
                    }
                  

                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                   
                    SaveSingleProducts();
                                     
                    MessageBox.Show("Combi Created Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    objCombiProdList.FillCombiProductsToGrid();
                   
                    //GenerateCombiId();
                    //btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int SaveSingleProducts()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "DELETE FROM PRODUCT_MAS_LI WHERE PML_PRODUCT_ID='" + txtCombiId.Text + "'";
            
            try
            {
                iRes = objSQLdb.ExecuteSaveData(strCommand);
                strCommand = "";
                //if (flagUpdateCheck == true)
                //{
                //    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                //    {
                //        strCommand += "UPDATE PRODUCT_MAS_LI " +
                //                     " SET PML_PRODUCT_SL_NO=" + Convert.ToInt32(gvProductDetails.Rows[i].Cells["SlNo"].Value.ToString()) +
                //                     ",PML_QTY=" + Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString()) +
                //                     ", PML_RATE_FREE_FLAG='" + gvProductDetails.Rows[i].Cells["ProdFlag"].Value.ToString() +
                //                     "', PML_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                //                     "', PML_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                //                     "'  WHERE PML_PRODUCT_ID='" + txtCombiId.Text + 
                //                     "' AND  PML_SNGLPCK_PRODUCT_ID='" + gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() +"' ";
                //    }
                //    flagUpdateCheck = false;
                //}
                //else
                //{
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    strCommand += "INSERT INTO PRODUCT_MAS_LI(PML_PRODUCT_ID " +
                                         ",PML_PRODUCT_SL_NO " +
                                         ",PML_SNGLPCK_PRODUCT_ID " +
                                         ",PML_QTY " +
                                         ",PML_RATE_FREE_FLAG " +
                                         ",PML_CREATED_BY " +
                                         ",PML_CREATED_DATE)VALUES('" + txtCombiId.Text +
                                         "'," + Convert.ToInt32(gvProductDetails.Rows[i].Cells["SlNo"].Value.ToString()) +
                                         ",'" + gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() +
                                         "'," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString()) +
                                         ",'" + gvProductDetails.Rows[i].Cells["ProdFlag"].Value.ToString() +
                                         "','" + CommonData.LogUserId +
                                         "','" + CommonData.CurrentDate + "')";
                }
                //}
                   
                iRes = objSQLdb.ExecuteSaveData(strCommand);
                if (iRes > 0)
                {
                    return iRes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            
            return iRes;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtCombiName.Text = "";
            gvProductDetails.Rows.Clear();
            GenerateCombiId();
           

        }

        private void FillProductsToGrid(string sCombiId)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvProductDetails.Rows.Clear();
                       
                try
                {

                    string strCommand = "SELECT PC.PM_PRODUCT_NAME CombiName " +
                                        ",PML_SNGLPCK_PRODUCT_ID SingleProdID " +
                                        ",PM.PM_PRODUCT_NAME SingleProdName " +
                                        ",CM.CATEGORY_NAME SingleCategory " +
                                        ",PML_QTY SingleQty " +
                                        ",PML_PRODUCT_SL_NO " +
                                        ",PML_RATE_FREE_FLAG " +
                                        " FROM PRODUCT_MAS_LI " +
                                        " INNER JOIN PRODUCT_MAS PM " +
                                        " ON PM.PM_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID " +
                                        " INNER JOIN CATEGORY_MASTER CM " +
                                        " ON CM.CATEGORY_ID = PM.PM_CATEGORY_ID " +
                                        " INNER JOIN PRODUCT_MAS PC " +
                                        " ON PC.PM_PRODUCT_ID = PML_PRODUCT_ID " +
                                        " WHERE PML_PRODUCT_ID ='" + sCombiId + "' ORDER BY  PML_PRODUCT_SL_NO ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        flagUpdateCheck = true;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            txtCombiId.Text = sCombiId;
                            txtCombiName.Text = dt.Rows[i]["CombiName"].ToString();
                            gvProductDetails.Rows.Add();

                            gvProductDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                            gvProductDetails.Rows[i].Cells["ProductId"].Value = dt.Rows[i]["SingleProdID"].ToString();
                            gvProductDetails.Rows[i].Cells["ProductName"].Value = dt.Rows[i]["SingleProdName"].ToString();
                            gvProductDetails.Rows[i].Cells["CategoryName"].Value = dt.Rows[i]["SingleCategory"].ToString();
                            gvProductDetails.Rows[i].Cells["ProdQty"].Value = dt.Rows[i]["SingleQty"].ToString();
                            gvProductDetails.Rows[i].Cells["ProdFlag"].Value = dt.Rows[i]["PML_RATE_FREE_FLAG"].ToString();



                        }
                    }
                    else
                    {
                        txtCombiName.Text = "";
                        gvProductDetails.Rows.Clear();
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

      

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                    gvProductDetails.Rows.Remove(dgvr);
                    MessageBox.Show("Data Deleted Successfully", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (gvProductDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {
                            gvProductDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Deleted", "Combi Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void gvProductDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["ProdFlag"].Index)
            {
                if ((gvProductDetails.Rows[e.RowIndex].Cells["ProdFlag"].Value.ToString()) == "F")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["ProdFlag"].Value = "R";
                }
                else if ((gvProductDetails.Rows[e.RowIndex].Cells["ProdFlag"].Value.ToString()) == "R")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["ProdFlag"].Value = "F";
                }


            }

        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["ProdQty"].Index)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value).ToString("0.00");
                }
            }
            GenerateCombiName();
        }

      
        
    }
}
