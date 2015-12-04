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
    public partial class frmProductPriceCircular : Form
    {
        SQLDB objSQLData = null;


        public frmProductPriceCircular()
        {
            InitializeComponent();
        }

        private void frmProductPriceCircular_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            dtpEffDate.Value = DateTime.Today;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            gvSubProdDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);


        }




        private void FillCompanyData()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE ACTIVE='T'";
                dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLData = null;
                dt = null;
            }

        }

        private void FillBranchData()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
          
            clbBranchesList.Items.Clear();

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT BRANCH_NAME,BRANCH_CODE+'@'+ STATE_CODE AS val  FROM BRANCH_MAS "+
                                        " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND BRANCH_TYPE='BR' and active = 'T' Order by BRANCH_NAME";
                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["val"].ToString();
                        oclBox.Text = dataRow["BRANCH_NAME"].ToString();

                        clbBranchesList.Items.Add(oclBox);

                    }
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLData = null;
                dt = null;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("frmProductPriceCircular");
            PSearch.objfrmProductPriceCircular = this;
            PSearch.ShowDialog();
            SetSubProdSlNo();
        }

        private void FillProductsToGrid()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            gvProductDetails.Rows.Clear();

            int intRow = 1;
            if (clbBranchesList.CheckedItems.Count > 0)
            {
                try
                {
                    string[] strArr = { "" };
                    foreach (var checkedItems in clbBranchesList.CheckedItems)
                    {
                        strArr = ((NewCheckboxListItem)(checkedItems)).Tag.ToString().Split('@');
                    }

                    string strCmd = "SELECT CATEGORY_NAME, PM_PRODUCT_NAME, PPM_PRODUCT_ID, PPM_MRP," +
                                    "PPM_OFFER_PRICE, PPM_PRODUCT_POINTS, isnull(PPM_SALE_PRICE,0) PPM_SALE_PRICE," +
                                    " isnull(PPM_VAT_AMT,0) PPM_VAT_AMT, isnull(PPM_VAT_PER,0) PPM_VAT_PER FROM PRODUCT_PRICE_MASTER " +
                                    "INNER JOIN PRODUCT_MAS ON PM_PRODUCT_ID =PPM_PRODUCT_ID " +
                                    "INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM_CATEGORY_ID " +
                                    "WHERE PPM_WEF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                    "' AND PPM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                    "' AND PPM_BRANCH_CODE='" + strArr[0] + "'";
                    dt = objSQLData.ExecuteDataSet(strCmd).Tables[0];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                        cellCategoryName.Value = dt.Rows[i]["CATEGORY_NAME"];
                        tempRow.Cells.Add(cellCategoryName);

                        DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                        cellProductId.Value = dt.Rows[i]["PPM_PRODUCT_ID"];
                        tempRow.Cells.Add(cellProductId);

                        DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                        cellProductName.Value = dt.Rows[i]["PM_PRODUCT_NAME"];
                        tempRow.Cells.Add(cellProductName);


                        DataGridViewCell cellProdMrp = new DataGridViewTextBoxCell();
                        cellProdMrp.Value = dt.Rows[i]["PPM_MRP"];
                        tempRow.Cells.Add(cellProdMrp);

                        DataGridViewCell cellOfferPrice = new DataGridViewTextBoxCell();
                        cellOfferPrice.Value = dt.Rows[i]["PPM_OFFER_PRICE"];
                        tempRow.Cells.Add(cellOfferPrice);

                        DataGridViewCell cellProdPoints = new DataGridViewTextBoxCell();
                        cellProdPoints.Value = dt.Rows[i]["PPM_PRODUCT_POINTS"];
                        tempRow.Cells.Add(cellProdPoints);

                        DataGridViewCell cellProdSP = new DataGridViewTextBoxCell();
                        cellProdSP.Value = dt.Rows[i]["PPM_SALE_PRICE"];
                        tempRow.Cells.Add(cellProdSP);

                        DataGridViewCell cellProdVP = new DataGridViewTextBoxCell();
                        cellProdVP.Value = dt.Rows[i]["PPM_VAT_PER"];
                        tempRow.Cells.Add(cellProdVP);

                        DataGridViewCell cellProdVat = new DataGridViewTextBoxCell();
                        cellProdVat.Value = dt.Rows[i]["PPM_VAT_AMT"];
                        tempRow.Cells.Add(cellProdVat);

                        gvProductDetails.Rows.Add(tempRow);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                    dt = null;
                }
            }
        }




        private void FillSubProducts()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            gvSubProdDetails.Rows.Clear();
            int intRow = 1;
            if (clbBranchesList.CheckedItems.Count > 0)
            {
                try
                {
                    string[] strArr = { "" };
                    foreach (var checkedItems in clbBranchesList.CheckedItems)
                    {
                        strArr = ((NewCheckboxListItem)(checkedItems)).Tag.ToString().Split('@');
                    }
                    string strCommand = "SELECT PM.PM_PRODUCT_NAME,PC.PM_PRODUCT_NAME combiname ,CATEGORY_NAME,PML_QTY,PML_PRODUCT_ID " +
                                            ",PML_SNGLPCK_PRODUCT_ID ,PPML_SL_NO ,ISNULL(ISNULL(PPML_MRP,PPM_MRP),0) PPML_MRP, " +
                                            "ISNULL(ISNULL(PPML_OFFER_PRICE,PPM_OFFER_PRICE),0) PPML_OFFER_PRICE ," +
                                            "ISNULL(ISNULL(PPML_PRODUCT_POINTS,PPM_PRODUCT_POINTS),0) PPML_PRODUCT_POINTS, " +
                                            "ISNULL(ISNULL(PPML_SALE_PRICE,PPM_SALE_PRICE),0) PPML_SALE_PRICE, " +
                                            "ISNULL(ISNULL(PPML_VAT_AMT,PPM_VAT_AMT),0) PPML_VAT_AMT, " +
                                            "ISNULL(ISNULL(PPML_VAT_PER,PPM_VAT_PER),0) PPML_VAT_PER " +
                                            "FROM PRODUCT_MAS_LI LEFT JOIN PRODUCT_PRICE_MASTER_LI ON PPML_PRODUCT_ID = PML_PRODUCT_ID " +
                                            "AND PPML_SINGLE_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID " +
                                            "AND PPML_BRANCH_CODE = '" + strArr[0] + "' AND PPML_WEF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "' " +
                                            "LEFT JOIN PRODUCT_PRICE_MASTER ON PPM_BRANCH_CODE = '" + strArr[0] + "' " +
                                            "AND PPM_WEF_DATE = '" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "' AND PPM_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID " +
                                            "INNER JOIN PRODUCT_MAS PM ON PM.PM_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID  " +
                                            "INNER JOIN PRODUCT_MAS PC ON PC.PM_PRODUCT_ID=PML_PRODUCT_ID  " +
                                            "INNER JOIN CATEGORY_MASTER CM ON CM.CATEGORY_ID = PM.PM_CATEGORY_ID"+
                                            " ORDER BY PC.PM_PRODUCT_NAME,PM.PM_PRODUCT_NAME  ";
                    //"WHERE PML_PRODUCT_ID='" + sProdID + "'";
                    //"SELECT PM.PM_PRODUCT_NAME,PC.PM_PRODUCT_NAME combiname " +
                    //        ",CATEGORY_NAME,PML_QTY,PML_PRODUCT_ID " +
                    //        ",PML_SNGLPCK_PRODUCT_ID ,PPML_SL_NO " +
                    //        ",PPML_MRP, PPML_OFFER_PRICE " +
                    //       ",PPML_PRODUCT_POINTS FROM PRODUCT_MAS_LI " +
                    //       " LEFT JOIN PRODUCT_PRICE_MASTER_LI ON PPML_PRODUCT_ID = PML_PRODUCT_ID AND" +
                    //       " PPML_SINGLE_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID AND PPML_COMP_CODE = '" + cbCompany.SelectedValue.ToString() +
                    //       "' AND PPML_BRANCH_CODE = '" + strArr[0] + "' AND PPML_WEF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                    //       "' INNER JOIN PRODUCT_MAS PM ON PM.PM_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID " +
                    //       " INNER JOIN PRODUCT_MAS PC ON PC.PM_PRODUCT_ID=PML_PRODUCT_ID " +
                    //       " INNER JOIN CATEGORY_MASTER CM ON CM.CATEGORY_ID = PM.PM_CATEGORY_ID " +                           
                    //      " WHERE PML_PRODUCT_ID='" + sProdID + "'";


                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSLNO);


                        DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                        cellProductId.Value = dt.Rows[i]["PML_PRODUCT_ID"];
                        tempRow.Cells.Add(cellProductId);

                        DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                        //cellProductName.Value = dt.Rows[i]["ProductName"];
                        cellProductName.Value = dt.Rows[i]["combiname"];
                        tempRow.Cells.Add(cellProductName);


                        DataGridViewCell cellsubCategoryName = new DataGridViewTextBoxCell();
                        cellsubCategoryName.Value = dt.Rows[i]["CATEGORY_NAME"];
                        tempRow.Cells.Add(cellsubCategoryName);

                        DataGridViewCell cellSingleProdId = new DataGridViewTextBoxCell();
                        cellSingleProdId.Value = dt.Rows[i]["PML_SNGLPCK_PRODUCT_ID"];
                        tempRow.Cells.Add(cellSingleProdId);

                        DataGridViewCell cellSingleProdName = new DataGridViewTextBoxCell();
                        cellSingleProdName.Value = dt.Rows[i]["PM_PRODUCT_NAME"];
                        tempRow.Cells.Add(cellSingleProdName);

                        DataGridViewCell cellProdQty = new DataGridViewTextBoxCell();
                        cellProdQty.Value = dt.Rows[i]["PML_QTY"];
                        tempRow.Cells.Add(cellProdQty);

                        DataGridViewCell cellProdMrp = new DataGridViewTextBoxCell();
                        cellProdMrp.Value = dt.Rows[i]["PPML_MRP"];
                        tempRow.Cells.Add(cellProdMrp);

                        DataGridViewCell cellProdOfferPrice = new DataGridViewTextBoxCell();
                        cellProdOfferPrice.Value = dt.Rows[i]["PPML_OFFER_PRICE"];
                        tempRow.Cells.Add(cellProdOfferPrice);

                        DataGridViewCell cellProdPoints = new DataGridViewTextBoxCell();
                        cellProdPoints.Value = dt.Rows[i]["PPML_PRODUCT_POINTS"];
                        tempRow.Cells.Add(cellProdPoints);

                        DataGridViewCell cellprodSlNo = new DataGridViewTextBoxCell();
                        cellprodSlNo.Value = dt.Rows[i]["PPML_SL_NO"];
                        tempRow.Cells.Add(cellprodSlNo);

                        DataGridViewCell cellprodSale = new DataGridViewTextBoxCell();
                        cellprodSale.Value = dt.Rows[i]["PPML_SALE_PRICE"];
                        tempRow.Cells.Add(cellprodSale);

                        DataGridViewCell cellProdVatPer = new DataGridViewTextBoxCell();
                        cellProdVatPer.Value = dt.Rows[i]["PPML_VAT_PER"];
                        tempRow.Cells.Add(cellProdVatPer);

                        DataGridViewCell cellprodVat = new DataGridViewTextBoxCell();
                        cellprodVat.Value = dt.Rows[i]["PPML_VAT_AMT"];
                        tempRow.Cells.Add(cellprodVat);

                        gvSubProdDetails.Rows.Add(tempRow);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                    dt = null;
                }
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            
            string strCommand = "";

            int iRes = 0;
            int iResult1 = 0;
            if (CheckData() == true)
            {
              

                try
                {
                    if (gvProductDetails.Rows.Count > 0)
                    {
                        foreach (var checkedItems in clbBranchesList.CheckedItems)
                        {
                            string[] strArr = ((NewCheckboxListItem)(checkedItems)).Tag.ToString().Split('@');



                            strCommand = " DELETE FROM PRODUCT_PRICE_MASTER_LI WHERE PPML_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND PPML_BRANCH_CODE='" + strArr[0] +
                                        "' AND PPML_WEF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "' ";
                            iResult1 = objSQLData.ExecuteSaveData(strCommand);


                            strCommand = " DELETE FROM PRODUCT_PRICE_MASTER WHERE  PPM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND PPM_BRANCH_CODE='" + strArr[0] +
                                        "' AND PPM_WEF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "'";
                            iResult1 = objSQLData.ExecuteSaveData(strCommand);
                        }

                        foreach (var checkedItems in clbBranchesList.CheckedItems)
                        {
                            string[] strArr = ((NewCheckboxListItem)(checkedItems)).Tag.ToString().Split('@');

                            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                            {
                                try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["prodMrp"].Value.ToString()); }
                                catch { gvProductDetails.Rows[i].Cells["prodMrp"].Value = 0; }
                                try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["offerPrice"].Value.ToString()); }
                                catch { gvProductDetails.Rows[i].Cells["offerPrice"].Value = 0; }
                                try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["prodPoints"].Value.ToString()); }
                                catch { gvProductDetails.Rows[i].Cells["prodPoints"].Value = 0; }
                                try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["SalePrice"].Value.ToString()); }
                                catch { gvProductDetails.Rows[i].Cells["SalePrice"].Value = 0; }
                                try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["Vat"].Value.ToString()); }
                                catch { gvProductDetails.Rows[i].Cells["Vat"].Value = 0; }
                                try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["VatPer"].Value.ToString()); }
                                catch { gvProductDetails.Rows[i].Cells["VatPer"].Value = 0; }

                                strCommand += " INSERT INTO PRODUCT_PRICE_MASTER(PPM_COMP_CODE,PPM_STATE_CODE " +
                                              ",PPM_BRANCH_CODE,PPM_WEF_DATE" +
                                              ",PPM_PRODUCT_ID,PPM_MRP,PPM_OFFER_PRICE " +
                                              ",PPM_PRODUCT_POINTS, PPM_SALE_PRICE, PPM_VAT_AMT, PPM_VAT_PER)" +
                                              " VALUES('" + cbCompany.SelectedValue.ToString() +
                                              "','" + strArr[1] + "','" + strArr[0] +
                                              "','" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                              "','" + gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() +
                                              "'," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["prodMrp"].Value.ToString()) +
                                              "," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["offerPrice"].Value.ToString()) +
                                              "," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["prodPoints"].Value.ToString()) +
                                              "," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["SalePrice"].Value.ToString()) +
                                              "," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["Vat"].Value.ToString()) +
                                              "," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["VatPer"].Value.ToString()) +
                                              ")";


                                //strCommand += "INSERT INTO PRODUCT_PRICE_MASTER (PPM_COMP_CODE,PPM_STATE_CODE " +
                                //              ",PPM_BRANCH_CODE,PPM_WEF_DATE" +
                                //              ",PPM_PRODUCT_ID,PPM_MRP,PPM_OFFER_PRICE " +
                                //              ",PPM_PRODUCT_POINTS)VALUES('" + cbCompany.SelectedValue.ToString() + "','" + strArr[1] + "','" + strArr[0] +
                                //              "','" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                //              "','" + gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() +
                                //              "'," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["prodMrp"].Value.ToString()) +
                                //              "," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["offerPrice"].Value.ToString()) +
                                //              "," + Convert.ToDouble(gvProductDetails.Rows[i].Cells["prodPoints"].Value.ToString()) + ")";

                            }
                        }
                       
                                                
                    }

                  
                    iRes = objSQLData.ExecuteSaveData(strCommand);



                }
                catch (Exception ex)
                {


                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    SaveSubProducts();
                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btnCancel_Click(null, null);
                }


                else
                {
                    MessageBox.Show("Data not saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private int SaveSubProducts()
        {
            objSQLData = new SQLDB();
            string[] strArr;

            string strInsert = "";
            int iRes = 0;

            try
            {
                if (gvSubProdDetails.Rows.Count > 0)
                {

               
                   foreach(var checkedItems in clbBranchesList.CheckedItems)
                    {
                        strArr = ((NewCheckboxListItem)(checkedItems)).Tag.ToString().Split('@');


                        for (int j = 0; j < gvSubProdDetails.Rows.Count; j++)
                        {

                            //strInsert += "INSERT INTO PRODUCT_PRICE_MASTER_LI(PPML_COMP_CODE,PPML_STATE_CODE " +
                            //    ", PPML_BRANCH_CODE,PPML_WEF_DATE,PPML_PRODUCT_ID,PPML_SL_NO,PPML_SINGLE_PRODUCT_ID " +
                            //    " ,PPML_MRP,PPML_OFFER_PRICE,PPML_PRODUCT_POINTS) VALUES('" + cbCompany.SelectedValue.ToString() +
                            //    "','" + strArr[1] + "','" + strArr[0] +
                            //    "','" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                            //    "','" + gvSubProdDetails.Rows[j].Cells["SProductId"].Value.ToString() +
                            //    "'," + Convert.ToInt32(gvSubProdDetails.Rows[j].Cells["prodSlNo"].Value.ToString()) +
                            //    ",'" + gvSubProdDetails.Rows[j].Cells["SingleProdId"].Value.ToString() +
                            //    "'," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdMrp"].Value.ToString()) +
                            //    "," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdOffPrice"].Value.ToString()) +
                            //    "," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdPoints"].Value.ToString()) + ") ";

                            try { Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdMrp"].Value.ToString()); }
                            catch { gvSubProdDetails.Rows[j].Cells["subProdMrp"].Value = 0; }
                            try { Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdOffPrice"].Value.ToString()); }
                            catch { gvSubProdDetails.Rows[j].Cells["subProdOffPrice"].Value = 0; }
                            try { Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdPoints"].Value.ToString()); }
                            catch { gvSubProdDetails.Rows[j].Cells["subProdPoints"].Value = 0; }
                            try { Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["singleSalePrice"].Value.ToString()); }
                            catch { gvSubProdDetails.Rows[j].Cells["singleSalePrice"].Value = 0; }
                            try { Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["singleVat"].Value.ToString()); }
                            catch { gvSubProdDetails.Rows[j].Cells["singleVat"].Value = 0; }
                            try { Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["singleVatPer"].Value.ToString()); }
                            catch { gvSubProdDetails.Rows[j].Cells["singleVatPer"].Value = 0; }

                            strInsert += " INSERT INTO PRODUCT_PRICE_MASTER_LI(PPML_COMP_CODE, PPML_STATE_CODE" +
                                            ", PPML_BRANCH_CODE, PPML_WEF_DATE, PPML_PRODUCT_ID, PPML_SL_NO, PPML_SINGLE_PRODUCT_ID" +
                                            ",PPML_MRP, PPML_OFFER_PRICE, PPML_PRODUCT_POINTS, PPML_SALE_PRICE, PPML_VAT_AMT, PPML_VAT_PER)" +
                                            " VALUES('" + cbCompany.SelectedValue.ToString() +
                                            "','" + strArr[1] + "','" + strArr[0] +
                                            "','" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                            "','" + gvSubProdDetails.Rows[j].Cells["SProductId"].Value.ToString() +
                                            "'," + j+1 +
                                            ",'" + gvSubProdDetails.Rows[j].Cells["SingleProdId"].Value.ToString() +
                                            "'," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdMrp"].Value.ToString()) +
                                            "," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdOffPrice"].Value.ToString()) +
                                            "," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdPoints"].Value.ToString()) +
                                            "," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["singleSalePrice"].Value.ToString()) +
                                            "," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["singleVat"].Value.ToString()) +
                                            "," + Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["singleVatPer"].Value.ToString()) +
                                            ") ";
                        }



                    }
                   
                }
               
                    iRes = objSQLData.ExecuteSaveData(strInsert);
                    
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        
            return iRes;


        }

        private void dtpEffDate_ValueChanged(object sender, EventArgs e)
        {
            if (clbBranchesList.SelectedIndex > 0)
            {
                FillProductsToGrid();
                gvSubProdDetails.Rows.Clear();
                if (gvProductDetails.Rows.Count > 0)
                {
                    FillSubProducts();
                }
                SetSubProdSlNo();
            }

        }
        private void SetSubProdSlNo()
        {
            for (int i = 0; i < gvSubProdDetails.Rows.Count; i++)
            {
                gvSubProdDetails.Rows[i].Cells["slNo1"].Value = GetCombiSlNo(gvSubProdDetails.Rows[i].Cells["SProductId"].Value.ToString());
                
            }
            bool bflag=false;
            do
            {
                bflag = false;
                for (int i = 0; i < gvSubProdDetails.Rows.Count; i++)
                {
                    if (gvSubProdDetails.Rows[i].Cells["slNo1"].Value.ToString() == "0")
                    {
                        DataGridViewRow dgvr = gvSubProdDetails.Rows[gvSubProdDetails.Rows[i].Index];
                        gvSubProdDetails.Rows.Remove(dgvr);
                        bflag = true;
                    }
                    
                }
            } while (bflag);

        }

        private string GetCombiSlNo(string sCombiID)
        {
            string sCombSlNo = "0";
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() == sCombiID)
                {
                    sCombSlNo = gvProductDetails.Rows[i].Cells["slNo"].Value.ToString();
                }
            }
            return sCombSlNo;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
            gvSubProdDetails.Rows.Clear();
            cbCompany.SelectedIndex = 0;

            dtpEffDate.Value = DateTime.Today;
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
                flag = false;
            }


            else if (gvProductDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select One product", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;

            }
            else if (gvProductDetails.Rows.Count > 0)
            {
                for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                {

                    if (gvProductDetails.Rows[iVar].Cells["prodMrp"].Value.ToString().Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please enter product Mrp", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }

                    else if (gvProductDetails.Rows[iVar].Cells["offerPrice"].Value.ToString().Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please enter product OfferPrice", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }
                    else if (gvProductDetails.Rows[iVar].Cells["prodPoints"].Value.ToString().Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please enter product Points", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }

                }
            }

            if (gvSubProdDetails.Rows.Count > 0)
            {
                for (int iVar1 = 0; iVar1 < gvSubProdDetails.Rows.Count; iVar1++)
                {

                    if (gvSubProdDetails.Rows[iVar1].Cells["subProdMrp"].Value.ToString().Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please enter SubProduct Mrp", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }

                    else if (gvSubProdDetails.Rows[iVar1].Cells["subProdOffPrice"].Value.ToString().Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please enter SubProduct  OfferPrice", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }
                    else if (gvSubProdDetails.Rows[iVar1].Cells["subProdPoints"].Value.ToString().Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please enter SubProduct  Points", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }

                }
            }

            return flag;

        }




        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

        private void gvSubProdDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == gvProductDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {

                        string productId = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["ProductId"].Index].Value.ToString();

                        DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                        gvProductDetails.Rows.Remove(dgvr);
                        try
                        {
                        Loop:
                            for (int i = 0; i < gvSubProdDetails.Rows.Count; i++)
                            {
                                if (productId.Equals(gvSubProdDetails.Rows[i].Cells["SProductId"].Value.ToString()))
                                {
                                    DataGridViewRow dgvr1 = gvSubProdDetails.Rows[i];
                                    gvSubProdDetails.Rows.Remove(dgvr1);
                                    goto Loop;

                                }
                            }




                            MessageBox.Show("Data Deleted Successfully", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "Product Price Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            SetSubProdSlNo();

        }

        private void clbBranchesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (e.NewValue == CheckState.Checked)
            //{
            //    //string[] strBranchCode = clbBranchesList.Items[e.Index];
            //}
            //else
            //    clbBranchesList.SetItemCheckState(e.Index, CheckState.Unchecked);
            if (clbBranchesList.CheckedItems.Count > 0)
            {
                FillProductsToGrid();
                gvSubProdDetails.Rows.Clear();
                if (gvProductDetails.Rows.Count > 0)
                {
                    FillSubProducts();
                }
                SetSubProdSlNo();
            }

        }

        private void clbBranchesList_Validated(object sender, EventArgs e)
        {
            FillProductsToGrid();
            gvSubProdDetails.Rows.Clear();
            if (gvProductDetails.Rows.Count > 0)
            {
                FillSubProducts();
            }
            SetSubProdSlNo();
        }

        private void gvSubProdDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //double dQty = 0, dMrp = 0, dOffer = 0, dPoints = 0;
            //string sCombiID = gvSubProdDetails.Rows[e.RowIndex].Cells["SProductId"].Value.ToString();
            //string sProdID = gvSubProdDetails.Rows[e.RowIndex].Cells["SingleProdId"].Value.ToString();
            //for (int i = 0; i < gvSubProdDetails.RowCount; i++)
            //{
            //    if (gvSubProdDetails.Rows[i].Cells["SProductId"].Value.ToString() == sCombiID)
            //    {
            //        dQty += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString());
            //        dMrp += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString())*Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdMrp"].Value.ToString());
            //        dOffer += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString())*Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdOffPrice"].Value.ToString());
            //        dPoints += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString())*Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdPoints"].Value.ToString());
            //    }
            //    //if (gvSubProdDetails.Rows[i].Cells["SingleProdId"].Value.ToString() == sProdID)
            //    //{
            //    //    //gvSubProdDetails.Rows[i].Cells["prodQty"].Value = "";
            //    //    gvSubProdDetails.Rows[i].Cells["subProdMrp"].Value = gvSubProdDetails.Rows[e.RowIndex].Cells["subProdMrp"].Value.ToString();
            //    //    gvSubProdDetails.Rows[i].Cells["subProdOffPrice"].Value = gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString();
            //    //    gvSubProdDetails.Rows[i].Cells["subProdPoints"].Value = gvSubProdDetails.Rows[e.RowIndex].Cells["subProdPoints"].Value.ToString();
            //    //}
            //}
            //for (int i = 0; i < gvProductDetails.RowCount; i++)
            //{
            //    if (gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() == sCombiID)
            //    {
            //        gvProductDetails.Rows[i].Cells["prodMrp"].Value = dMrp.ToString("f");
            //        gvProductDetails.Rows[i].Cells["offerPrice"].Value = dOffer.ToString("f");
            //        gvProductDetails.Rows[i].Cells["prodPoints"].Value = dPoints.ToString("f");
            //    }
            //    //if (gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() == sProdID)
            //    //{
            //    //    gvProductDetails.Rows[i].Cells["prodMrp"].Value = gvSubProdDetails.Rows[e.RowIndex].Cells["subProdMrp"].Value.ToString();
            //    //    gvProductDetails.Rows[i].Cells["offerPrice"].Value = gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString();
            //    //    gvProductDetails.Rows[i].Cells["prodPoints"].Value = gvSubProdDetails.Rows[e.RowIndex].Cells["subProdPoints"].Value.ToString();
            //    //}

            //}
            //dQty = dMrp = dOffer = dPoints = 0;
            //for (int i = 0; i < gvSubProdDetails.RowCount; i++)
            //{
            //    string sPCombiID = gvSubProdDetails.Rows[i].Cells["SProductId"].Value.ToString();
            //    //string sPProdID = gvSubProdDetails.Rows[i].Cells["SingleProdId"].Value.ToString();
            //    dQty = dMrp = dOffer = dPoints = 0;
            //    for (int j = 0; j < gvSubProdDetails.RowCount; j++)
            //    {
            //        if (gvSubProdDetails.Rows[j].Cells["SProductId"].Value.ToString() == sCombiID)
            //        {
            //            dQty += Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["prodQty"].Value.ToString());
            //            dMrp += Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["prodQty"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdMrp"].Value.ToString());
            //            dOffer += Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["prodQty"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdOffPrice"].Value.ToString());
            //            dPoints += Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["prodQty"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[j].Cells["subProdPoints"].Value.ToString());
            //        }
            //    }
            //    for (int j = 0; j < gvProductDetails.RowCount; j++)
            //    {
            //        if (gvProductDetails.Rows[j].Cells["ProductId"].Value.ToString() == sCombiID)
            //        {
            //            gvProductDetails.Rows[j].Cells["prodMrp"].Value = dMrp.ToString("f");
            //            gvProductDetails.Rows[j].Cells["offerPrice"].Value = dOffer.ToString("f");
            //            gvProductDetails.Rows[j].Cells["prodPoints"].Value = dPoints.ToString("f");
            //        }
            //    }
                
            //}

            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == gvSubProdDetails.Columns["singleSalePrice"].Index)
                {
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleSalePrice"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["singleSalePrice"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value = 0; }
                    gvSubProdDetails.Rows[e.RowIndex].Cells["singleVat"].Value = (Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString()) - Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleSalePrice"].Value.ToString())).ToString("f");
                }
                if (e.ColumnIndex == gvSubProdDetails.Columns["singleVat"].Index)
                {
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVat"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["singleVat"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value = 0; }
                    gvSubProdDetails.Rows[e.RowIndex].Cells["singleSalePrice"].Value = (Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString()) - Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVat"].Value.ToString())).ToString("f");
                }
                if (e.ColumnIndex == gvSubProdDetails.Columns["singleVatPer"].Index)
                {
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVat"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["singleVat"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value = 0; }
                    gvSubProdDetails.Rows[e.RowIndex].Cells["singleSalePrice"].Value = (Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString()) * (100 + Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()) - Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString())) / (100 + Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()))).ToString("f");
                    gvSubProdDetails.Rows[e.RowIndex].Cells["singleVat"].Value = (Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["subProdOffPrice"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()) / (100 + Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()))).ToString("f");
                }
                double dQty = 0, dMrp = 0, dOffer = 0, dPoints = 0, dSalePrice = 0, dVat = 0;
                string sCombiID = gvSubProdDetails.Rows[e.RowIndex].Cells["SProductId"].Value.ToString();
                for (int i = 0; i < gvSubProdDetails.RowCount; i++)
                {
                    try { Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdMrp"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[i].Cells["subProdMrp"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdOffPrice"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[i].Cells["subProdOffPrice"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdPoints"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[i].Cells["subProdPoints"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["singleSalePrice"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[i].Cells["singleSalePrice"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["singleVat"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[i].Cells["singleVat"].Value = 0; }
                    try { Convert.ToDouble(gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value.ToString()); }
                    catch { gvSubProdDetails.Rows[e.RowIndex].Cells["singleVatPer"].Value = 0; }
                    if (gvSubProdDetails.Rows[i].Cells["SProductId"].Value.ToString() == sCombiID)
                    {
                        dQty += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString());
                        dMrp += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdMrp"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString());
                        dOffer += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdOffPrice"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString());
                        dPoints += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["subProdPoints"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString());
                        dSalePrice += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["singleSalePrice"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString());
                        dVat += Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["singleVat"].Value.ToString()) * Convert.ToDouble(gvSubProdDetails.Rows[i].Cells["prodQty"].Value.ToString());
                    }
                }
                //dQty = dQty / gvSubProdDetails.RowCount;
                //dMrp = dMrp / gvSubProdDetails.RowCount;
                //dOffer = dOffer / gvSubProdDetails.RowCount;
                //dPoints = dPoints / gvSubProdDetails.RowCount;
                for (int i = 0; i < gvProductDetails.RowCount; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() == sCombiID)
                    {
                        gvProductDetails.Rows[i].Cells["prodMrp"].Value = dMrp.ToString("f");
                        gvProductDetails.Rows[i].Cells["offerPrice"].Value = dOffer.ToString("f");
                        gvProductDetails.Rows[i].Cells["prodPoints"].Value = dPoints.ToString("f");
                        gvProductDetails.Rows[i].Cells["SalePrice"].Value = dSalePrice.ToString("f");
                        gvProductDetails.Rows[i].Cells["Vat"].Value = dVat.ToString("f");
                        if (dOffer > 0)
                            gvProductDetails.Rows[i].Cells["VatPer"].Value = ((dVat * 100) / dOffer).ToString("f");
                        else
                            gvProductDetails.Rows[i].Cells["VatPer"].Value = 0.00;
                    }
                }
            }
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value = 0; }
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value = 0; }
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value = 0; }
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value = 0; }
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["prodMrp"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["prodMrp"].Value = 0; }
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["prodPoints"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["prodPoints"].Value = 0; }

                if (e.ColumnIndex == gvProductDetails.Columns["SalePrice"].Index)
                {
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value = 0; }
                    gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value =
                        (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString())
                        - Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value.ToString())).ToString("f");
                }
                if (e.ColumnIndex == gvProductDetails.Columns["Vat"].Index)
                {
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value = 0; }
                    gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value =
                        (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString())
                        - Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value.ToString())).ToString("f");
                }
                if (e.ColumnIndex == gvProductDetails.Columns["VatPer"].Index
                    || e.ColumnIndex == gvProductDetails.Columns["offerPrice"].Index
                    || e.ColumnIndex == gvProductDetails.Columns["SalePrice"].Index)
                {
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()); }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value = 0; }
                    try
                    {

                        gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value =
                            ((Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString()) * (100 + Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()) - Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()))) / (100 + Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()))).ToString("f");
                        gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value =
                            ((Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["offerPrice"].Value.ToString()) * Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString())) / (100 + Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["VatPer"].Value.ToString()))).ToString("f");
                    }
                    catch { gvProductDetails.Rows[e.RowIndex].Cells["SalePrice"].Value = "0";
                    gvProductDetails.Rows[e.RowIndex].Cells["Vat"].Value = "0";
                    }
                }
            }
        }

  
    }
}
