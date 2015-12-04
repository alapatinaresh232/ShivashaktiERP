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
using SSCRM.App_Code;
using CrystalDecisions.CrystalReports.Engine;
namespace SSCRM
{
    public partial class ActivityServiceSearch : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRInfo = null;
        ServiceDB objServiceDB = null;
        DataSet ds, dsAll;
        Reports.Invoice.DSActivityService oDSActivity = new SSCRM.Reports.Invoice.DSActivityService();
        string strChkDoc = "", strChkVill = "", strChkProd = "", strQty = "";
        public ActivityServiceSearch()
        {
            InitializeComponent();
        }

        private void SalesService_Load(object sender, EventArgs e)
        {
            GetPopupContrpls();
            cmbQty.SelectedIndex = 0;
            cmbQty.SelectedIndex = 0;
        }

        public void GetPopupContrpls()
        {
            objHRInfo = new HRInfo();
            DataTable dtBranch = objHRInfo.GetAllBranchList(CommonData.CompanyCode, "BR", "").Tables[0];
            UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objHRInfo = null;
        }

        public void btnDisplay_Click(object sender, EventArgs e)
        {
            //GetALLData();
            oDSActivity.Tables[0].Rows.Clear();
            objServiceDB = new ServiceDB();
            DataSet ds = objServiceDB.GetSearchServiceData(CommonData.CompanyCode, CommonData.BranchCode);
            objServiceDB = null;
            string strMain = "";
            if (strChkDoc != "")
                strMain += " SIH_DOCUMENT_MONTH in (" + strChkDoc.TrimEnd(',') + ")";
            if (strChkVill != "")
            {
                if (strMain != "")
                    strMain += " AND ";
                strMain += " cm_village in (" + strChkVill.TrimEnd(',') + ")";
            }
            if (strChkProd != "")
            {
                if (strMain != "")
                    strMain += " AND ";
                strMain += " pm_product_name in (" + strChkProd.TrimEnd(',') + ")";
            }
            if (cmbQty.SelectedIndex == 4)
                txtTo_Validated(null, null);
            else
                txtFrm_Validated(null, null);

            if (strQty != "")
                strMain += strQty;

            DataRow[] dr = ds.Tables[0].Select(strMain, "SIH_INVOICE_NUMBER");
            string sInvoiceid = "";
            int ival = 0;
            string sam_Name = "", ACTUAL_DATE = "", ATTEND_BY_ECODE = "", TNA_FARMER_REMARKS = "", TNA_PRODUCT_ID = "", pm_product_name = "", TNA_TARGET_DATE = "", SHORT_NAME = "";
            string sam_Name1 = "", ACTUAL_DATE1 = "", ATTEND_BY_ECODE1 = "", TNA_FARMER_REMARKS1 = "", TNA_PRODUCT_ID1 = "", pm_product_name1 = "", TNA_TARGET_DATE1 = "", SHORT_NAME1 = "";
            string sam_Name2 = "", ACTUAL_DATE2 = "", ATTEND_BY_ECODE2 = "", TNA_FARMER_REMARKS2 = "", TNA_PRODUCT_ID2 = "", pm_product_name2 = "", TNA_TARGET_DATE2 = "", SHORT_NAME2 = "";
            string sam_Name3 = "", ACTUAL_DATE3 = "", ATTEND_BY_ECODE3 = "", TNA_FARMER_REMARKS3 = "", TNA_PRODUCT_ID3 = "", pm_product_name3 = "", TNA_TARGET_DATE3 = "", SHORT_NAME3 = "";
            for (int i = 0; i < dr.Length-1; i++)
            {
                if (sInvoiceid != dr[i][4].ToString())
                {
                    ival = 0;
                    sam_Name = dr[i][20].ToString(); ACTUAL_DATE = dr[i][21].ToString(); ATTEND_BY_ECODE = dr[i][22].ToString(); TNA_FARMER_REMARKS = dr[i][23].ToString(); TNA_PRODUCT_ID = dr[i][24].ToString(); pm_product_name = dr[i][25].ToString(); TNA_TARGET_DATE = dr[i][26].ToString(); SHORT_NAME = dr[i][28].ToString();
                }
                else
                {
                    ival++;
                    if (ival == 1)
                    {
                        sam_Name1 = dr[i][20].ToString(); ACTUAL_DATE1 = dr[i][21].ToString(); ATTEND_BY_ECODE1 = dr[i][22].ToString(); TNA_FARMER_REMARKS1 = dr[i][23].ToString(); TNA_PRODUCT_ID1 = dr[i][24].ToString(); pm_product_name1 = dr[i][25].ToString(); TNA_TARGET_DATE1 = dr[i][26].ToString(); SHORT_NAME1 = dr[i][28].ToString();
                    }
                    else if (ival == 2)
                    {
                        sam_Name2 = dr[i][20].ToString(); ACTUAL_DATE2 = dr[i][21].ToString(); ATTEND_BY_ECODE2 = dr[i][22].ToString(); TNA_FARMER_REMARKS2 = dr[i][23].ToString(); TNA_PRODUCT_ID2 = dr[i][24].ToString(); pm_product_name2 = dr[i][25].ToString(); TNA_TARGET_DATE2 = dr[i][26].ToString(); SHORT_NAME2 = dr[i][28].ToString();
                    }
                    else if (ival == 3)
                    {
                        sam_Name3 = dr[i][20].ToString(); ACTUAL_DATE3 = dr[i][21].ToString(); ATTEND_BY_ECODE3 = dr[i][22].ToString(); TNA_FARMER_REMARKS3 = dr[i][23].ToString(); TNA_PRODUCT_ID3 = dr[i][24].ToString(); pm_product_name3 = dr[i][25].ToString(); TNA_TARGET_DATE3 = dr[i][26].ToString(); SHORT_NAME3 = dr[i][28].ToString();
                    }
                }
                if (dr[i][4].ToString() != dr[i + 1][4].ToString())
                {
                    oDSActivity.Tables[0].Rows.Add(dr[i][0].ToString(), dr[i][1].ToString(), dr[i][2].ToString(), dr[i][3].ToString(), dr[i][4].ToString()
                        , dr[i][5].ToString(), dr[i][6].ToString(), dr[i][7].ToString(), dr[i][8].ToString(), dr[i][9].ToString(), dr[i][10].ToString()
                        , dr[i][11].ToString(), dr[i][12].ToString(), dr[i][13].ToString(), dr[i][14].ToString(), dr[i][15].ToString(), dr[i][16].ToString()
                        , dr[i][17].ToString(), dr[i][18].ToString(), dr[i][19].ToString(), sam_Name, ACTUAL_DATE, ATTEND_BY_ECODE, TNA_FARMER_REMARKS,
                        TNA_PRODUCT_ID, pm_product_name, TNA_TARGET_DATE, SHORT_NAME, sam_Name1, ACTUAL_DATE1, ATTEND_BY_ECODE1, TNA_FARMER_REMARKS1,
                        TNA_PRODUCT_ID1, pm_product_name1, TNA_TARGET_DATE1, SHORT_NAME1, sam_Name2, ACTUAL_DATE2, ATTEND_BY_ECODE2, TNA_FARMER_REMARKS2,
                        TNA_PRODUCT_ID2, pm_product_name2, TNA_TARGET_DATE2, SHORT_NAME2, sam_Name3, ACTUAL_DATE3, ATTEND_BY_ECODE3, TNA_FARMER_REMARKS3,
                        TNA_PRODUCT_ID3, pm_product_name3, TNA_TARGET_DATE3, SHORT_NAME3);

                    sam_Name = ""; ACTUAL_DATE = ""; ATTEND_BY_ECODE = ""; TNA_FARMER_REMARKS = ""; TNA_PRODUCT_ID = ""; pm_product_name = ""; TNA_TARGET_DATE = ""; SHORT_NAME = "";
                    sam_Name1 = ""; ACTUAL_DATE1 = ""; ATTEND_BY_ECODE1 = ""; TNA_FARMER_REMARKS1 = ""; TNA_PRODUCT_ID1 = ""; pm_product_name1 = ""; TNA_TARGET_DATE1 = ""; SHORT_NAME1 = "";
                    sam_Name2 = ""; ACTUAL_DATE2 = ""; ATTEND_BY_ECODE2 = ""; TNA_FARMER_REMARKS2 = ""; TNA_PRODUCT_ID2 = ""; pm_product_name2 = ""; TNA_TARGET_DATE2 = ""; SHORT_NAME2 = "";
                    sam_Name3 = ""; ACTUAL_DATE3 = ""; ATTEND_BY_ECODE3 = ""; TNA_FARMER_REMARKS3 = ""; TNA_PRODUCT_ID3 = ""; pm_product_name3 = ""; TNA_TARGET_DATE3 = ""; SHORT_NAME3 = "";
                }
                sInvoiceid = dr[i][4].ToString();
            }
            CommonData.ViewReport = "SERVICE_ACTIVITY_DETAILS";
            ReportViewer chldRV = new ReportViewer(oDSActivity);
            chldRV.Show();
        }
        public void GetALLData()
        {
            string strMain = "";
            if (strChkDoc != "")
                strMain += " AND sr_document_month in (" + strChkDoc.TrimEnd(',') + ") ";

            if (strChkVill != "")
                strMain += " AND sr_cnv_village in (" + strChkVill.TrimEnd(',') + ")";

            if (strChkProd != "")
                strMain += " AND sr_prod_name in (" + strChkProd.TrimEnd(',') + ")";

            if (cmbQty.SelectedIndex == 4)
                txtTo_Validated(null, null);
            else
                txtFrm_Validated(null, null);

            if (strQty != "")
                strMain += strQty;
            //CommonData.ViewReport = "SERVICE_ACTIVITY_DETAILS";
            //ReportViewer chldRV = new ReportViewer(cmbBranch.SelectedValue.ToString(), strMain);
            //chldRV.Show();
        }

        private void GetAllInvoicdeData(string InvoicdeNo)
        {
            DataView dvFil = ds.Tables[0].DefaultView;
            dvFil.RowFilter = "TNA_INVOICE_NUMBER=" + InvoicdeNo;
            DataTable dt;
            dt = dvFil.ToTable();
            if (dt.Rows.Count > 0)
            {

            }
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                chkDocMonth.Items.Clear();
                chkVillage.Items.Clear();
                chkProducts.Items.Clear();
                strChkDoc = "";
                strChkProd = "";
                strChkVill = "";
                objServiceDB = new ServiceDB();
                dsAll = objServiceDB.GetInvocieInfoforService(cmbBranch.SelectedValue.ToString(),"", "", "", 102);
                foreach (DataRow dr in dsAll.Tables[0].Rows)
                {
                    chkDocMonth.Items.Add(dr["TNA_DOC_MONTH"].ToString());
                }
                foreach (DataRow drV in dsAll.Tables[1].Rows)
                {
                    chkVillage.Items.Add(drV["CM_VILLAGE"].ToString());
                }
                foreach (DataRow drP in dsAll.Tables[2].Rows)
                {
                    chkProducts.Items.Add(drP["pm_product_name"].ToString());
                }
                objServiceDB = null;
            }
        }

        private void chkDocMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkDoc = "";
            for (int i = 0; i < chkDocMonth.CheckedItems.Count; i++)
            {
                strChkDoc += "'" + chkDocMonth.CheckedItems[i].ToString() + "',";
            }
        }

        private void chkVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkVill = "";
            for (int i = 0; i < chkVillage.CheckedItems.Count; i++)
            {
                strChkVill += "'" + chkVillage.CheckedItems[i].ToString() + "',";
            }
        }

        private void chkProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkProd = "";
            for (int i = 0; i < chkProducts.CheckedItems.Count; i++)
            {
                strChkProd += "'" + chkProducts.CheckedItems[i].ToString() + "',";
            }
        }

        private void txtFrm_Validated(object sender, EventArgs e)
        {
            strQty = "";
            if (cmbQty.SelectedIndex == 0)
                strQty = "";
            else if (cmbQty.SelectedIndex == 1)
                strQty += " AND TNA_QTY <= " + txtFrm.Text;
            else if (cmbQty.SelectedIndex == 2)
                strQty += " AND TNA_QTY >= " + txtFrm.Text;
            else if (cmbQty.SelectedIndex == 3)
                strQty += " AND TNA_QTY = " + txtFrm.Text;
        }

        private void txtTo_Validated(object sender, EventArgs e)
        {
            strQty = "";
            if (cmbQty.SelectedIndex == 4)
                strQty += " AND TNA_QTY BETWEEN " + txtFrm.Text + " AND " + txtTo.Text;
        }

        private void cmbQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbQty.SelectedIndex != 4)
            {
                lblTo.Visible = false;
                txtTo.Visible = false;
                lblFrm.Text = "No.";
            }
            else
            {
                lblTo.Visible = true;
                txtTo.Visible = true;
                lblFrm.Text = "From";
            }
        }

        private void txtFrm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void chkDocM_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocM.Checked == true)
            {
                for (int i = 0; i < chkDocMonth.Items.Count; i++)
                {
                    chkDocMonth.SetItemCheckState(i, CheckState.Checked);
                }
                strChkDoc = "";
                for (int i = 0; i < chkDocMonth.CheckedItems.Count; i++)
                {
                    strChkDoc += "'" + chkDocMonth.CheckedItems[i].ToString() + "',";
                }
            }
            else
            {
                for (int i = 0; i < chkDocMonth.Items.Count; i++)
                {
                    chkDocMonth.SetItemCheckState(i, CheckState.Unchecked);
                }
                strChkDoc = "";
            }
        }

        private void chkVill_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVill.Checked == true)
            {
                for (int i = 0; i < chkVillage.Items.Count; i++)
                {
                    chkVillage.SetItemCheckState(i, CheckState.Checked);
                }
                strChkVill = "";
                for (int i = 0; i < chkVillage.CheckedItems.Count; i++)
                {
                    strChkVill += "'" + chkVillage.CheckedItems[i].ToString() + "',";
                }
            }
            else
            {
                for (int i = 0; i < chkVillage.Items.Count; i++)
                {
                    chkVillage.SetItemCheckState(i, CheckState.Unchecked);
                }
                strChkVill = "";
            }
        }

        private void chkProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProduct.Checked == true)
            {
                for (int i = 0; i < chkProducts.Items.Count; i++)
                {
                    chkProducts.SetItemCheckState(i, CheckState.Checked);
                }
                strChkProd = "";
                for (int i = 0; i < chkProducts.CheckedItems.Count; i++)
                {
                    strChkProd += "'" + chkProducts.CheckedItems[i].ToString() + "',";
                }
            }
            else
            {
                for (int i = 0; i < chkProducts.Items.Count; i++)
                {
                    chkProducts.SetItemCheckState(i, CheckState.Unchecked);
                }
                strChkProd = "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
