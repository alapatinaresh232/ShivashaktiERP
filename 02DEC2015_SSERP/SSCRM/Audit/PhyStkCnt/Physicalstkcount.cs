using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Collections;
using SSCRMDB;

namespace SSCRM
{
    public partial class Physicalstkcount : Form
    {
        DataGridViewRow dgvr;
        SQLDB ObjSqlDb = null;
        private Security objSecurity = null;
        private bool GoodQtyCell = true;
        bool flagUpdate = false;
        byte[] ImgArr;
        delegate void SetComboBoxCellType(int iRowIndex);



        public Physicalstkcount()
        {
            InitializeComponent();
        }

        private void Physicalstkcount_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillZonesList();
            FillRegionsList();
            dtpDate.Value = DateTime.Today;
            GenerateTransactionNo();
        }


        private void FillCompanyData()
        {
            ObjSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId + "' " +
                                   " ORDER BY CM_COMPANY_NAME";

                dt = ObjSqlDb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

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
                ObjSqlDb = null;
                dt = null;
            }
        }
        public void GenerateTransactionNo()
        {
            ObjSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            if (cbLocation.SelectedIndex > 0)
            {
                try
                {
                    string strcmd = "select IsNull(Max(cast(APSCH_TRN_NUMBER as numeric)),0)+1 " +
                                    " from AUDIT_PHY_STK_COUNT_HEAD " +
                                    " WHERE APSCH_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] +
                                    "' and APSCH_FIN_YEAR='" + CommonData.FinancialYear + "' ";
                    dt = ObjSqlDb.ExecuteDataSet(strcmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTrnNo.Text = Convert.ToInt32(dt.Rows[0][0]).ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    ObjSqlDb = null;
                    dt = null;
                }
            }
        }

        private void FillZonesList()
        {
            ObjSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
           

            if (cbCompany.SelectedIndex > 0)
            {
                try
                {
                    strCommand = "SELECT DISTINCT ABM_STATE FROM AUDIT_BRANCH_MAS " +
                                 " WHERE ABM_COMP_CODE='" + cbCompany.SelectedValue.ToString() + "'";

                    dt = ObjSqlDb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

                        cbZones.DataSource = dt;
                        cbZones.DisplayMember = "sm_state";
                        cbZones.ValueMember = "ABM_STATE";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    ObjSqlDb = null;
                    dt = null;
                }
            }
            else
            {
                cbZones.DataSource = null;
            }
        }

        private void FillRegionsList()
        {
            ObjSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
         

            if (cbZones.SelectedIndex > 0)
            {
                try
                {
                    strCommand = "SELECT DISTINCT ABM_REGION FROM AUDIT_BRANCH_MAS " +
                                " WHERE ABM_STATE='" + cbZones.SelectedValue.ToString() + "' " +
                                " AND ABM_COMP_CODE='" + cbCompany.SelectedValue.ToString() + "'";

                    dt = ObjSqlDb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

                        cbRegion.DataSource = dt;
                        cbRegion.DisplayMember = "ABM_REGION";
                        cbRegion.ValueMember = "ABM_REGION";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    ObjSqlDb = null;
                    dt = null;
                }
            }
            else
            {
                cbRegion.DataSource = null;
            }
        }

        public void FilllocationData()
        {

            ObjSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            cbLocation.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0 && cbZones.SelectedIndex > 0 && cbRegion.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as branchCode " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " INNER JOIN AUDIT_BRANCH_MAS ON ABM_BRANCH_CODE=UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId + "' " +
                                        " AND ABM_STATE='" + cbZones.SelectedValue.ToString() +
                                        "'AND ABM_REGION='" + cbRegion.SelectedValue.ToString() +
                                        "' ORDER BY BRANCH_NAME ASC";
                    dt = ObjSqlDb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "branchCode";

                }

                //string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                //cbLocation.SelectedValue = BranCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ObjSqlDb = null;
                dt = null;
            }
        }


        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                cbRegion.DataSource = null;
                cbLocation.DataSource = null;
                FillZonesList();
                // FilllocationData();

            }
            else
            {
                cbZones.DataSource = null;
                cbRegion.DataSource = null;
                cbLocation.DataSource = null;
            }
        }


        private void cbZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbZones.SelectedIndex > 0)
            {
                FillRegionsList();
            }
            else
            {
                cbRegion.DataSource = null;
                cbLocation.DataSource = null;
            }
        }

        private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRegion.SelectedIndex > 0)
            {
                FilllocationData();
            }
            else
            {
                cbLocation.DataSource = null;
            }
        }



        private void btnAddAuditordetails_Click(object sender, EventArgs e)
        {
            Auditors adtfrm = new Auditors();
            adtfrm.objPhystkfrm = this;
            adtfrm.ShowDialog();
        }
               
        private void btnAddStkdetails_Click(object sender, EventArgs e)
        {
            if (cbLocation.SelectedIndex > 0 )
            {
                StockPersonDetails Stkfrm = new StockPersonDetails(cbLocation.SelectedValue.ToString().Split('@')[0]);
                Stkfrm.objPhystkfrm = this;
                Stkfrm.ShowDialog();
            }
        }       

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FillOpeningStockProdDetails()
        {
            ObjSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            if (flagUpdate == false)
            {
                try
                {
                    strCmd = " SELECT DISTINCT OS_PRODUCT_ID ProdId,PM_PRODUCT_NAME ProdName,CATEGORY_NAME CategoryName " +
                             " FROM OPENING_STOCK " +
                             " INNER JOIN PRODUCT_MAS ON PM_PRODUCT_ID=OS_PRODUCT_ID " +
                             " INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM_CATEGORY_ID " +
                             " WHERE OS_FIN_YEAR='" + CommonData.FinancialYear +
                             "' and OS_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "'";
                    dt = ObjSqlDb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        gvProductDetails.Rows.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvProductDetails.Rows.Add();
                            gvProductDetails.Rows[i].Cells["SlNo3"].Value = (i + 1).ToString();
                            gvProductDetails.Rows[i].Cells["ProductId"].Value = dt.Rows[i]["ProdId"].ToString();
                            gvProductDetails.Rows[i].Cells["ProdName"].Value = dt.Rows[i]["ProdName"].ToString();
                            gvProductDetails.Rows[i].Cells["Catg"].Value = dt.Rows[i]["CategoryName"].ToString();
                            gvProductDetails.Rows[i].Cells["GoodERP"].Value = "0";
                            gvProductDetails.Rows[i].Cells["DmgERP"].Value = "0";
                            gvProductDetails.Rows[i].Cells["GoodBook"].Value = "0";
                            gvProductDetails.Rows[i].Cells["DmgBook"].Value = "0";
                            gvProductDetails.Rows[i].Cells["GoodPhyCnt"].Value = "0";
                            gvProductDetails.Rows[i].Cells["DmgPhyCnt"].Value = "0";
                            gvProductDetails.Rows[i].Cells["GoodDiff"].Value = "0";
                            gvProductDetails.Rows[i].Cells["DmgDiff"].Value = "0";
                            gvProductDetails.Rows[i].Cells["TotDiff"].Value = "0";
                            gvProductDetails.Rows[i].Cells["DmgStkRemoved"].Value = "0";
                            gvProductDetails.Rows[i].Cells["PhyCntFlag"].Value = "0";
                            gvProductDetails.Rows[i].Cells["Letter"].Value = "";
                            gvProductDetails.Rows[i].Cells["Reason"].Value = "";
                            gvProductDetails.Rows[i].Cells["LetterImage"].Value = "";
                            gvProductDetails.Rows[i].Cells["Remarks"].Value = "";
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    ObjSqlDb = null;
                    dt = null;
                }
            }
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocation.SelectedIndex > 0)
            {
                GenerateTransactionNo();
                if (flagUpdate == false)
                {
                    FillOpeningStockProdDetails();
                }
            }

        }

        private int SaveHeadDetails()
        {
            ObjSqlDb = new SQLDB();
            int iRes = 0;
            string strCmd = "";
           
            try
            {
                string[] strCode = cbLocation.SelectedValue.ToString().Split('@');

                strCmd = "Delete from AUDIT_PHY_STK_COUNT_AUDITORS_DETL where APSCAD_TRN_NUMBER='" + txtTrnNo.Text +
                    "' and APSCAD_FIN_YEAR='" + CommonData.FinancialYear + "' and APSCAD_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "' ";

                strCmd += " Delete from AUDIT_PHY_STK_COUNT_SP_EMP_DETL where APSCSED_TRN_NUMBER='" + txtTrnNo.Text +
                    "' and APSCSED_FIN_YEAR='" + CommonData.FinancialYear + "' and APSCSED_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "' ";

                strCmd += " Delete from AUDIT_PHY_STK_COUNT_PRODUCT_DETL where APSCPD_TRN_NUMBER='" + txtTrnNo.Text +
                   "' and APSCPD_FIN_YEAR='" + CommonData.FinancialYear + "' and APSCPD_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "' ";


                if (flagUpdate == true)
                {
                    strCmd += " Update AUDIT_PHY_STK_COUNT_HEAD set APSCH_DOC_MONTH= '" + Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy").ToUpper() + "' " +
                                            ", APSCH_TRN_DATE='" + Convert.ToDateTime(dtpDate.Value).ToString("dd/MMM/yyyy") +
                                            "',APSCH_MODIFIED_BY='" + CommonData.LogUserId + "' , APSCH_MODIFIED_DATE= getdate() " +
                                             ", APSCH_REMARKS='" + txtRemarks.Text.ToString().Replace("'","") +
                                             "' where APSCH_TRN_NUMBER = '" + txtTrnNo.Text +
                                             "' and APSCH_BRANCH_CODE='" + strCode[0] +
                                             "' and APSCH_FIN_YEAR='" + CommonData.FinancialYear + "' ";

                }
                else 
                {

                    GenerateTransactionNo();
                    ObjSqlDb = new SQLDB();

                    strCmd = "INSERT INTO AUDIT_PHY_STK_COUNT_HEAD(APSCH_COMPANY_CODE " +
                                                                 ", APSCH_STATE_CODE " +
                                                                 ", APSCH_BRANCH_CODE " +
                                                                 ", APSCH_FIN_YEAR " +
                                                                 ", APSCH_DOC_MONTH " +
                                                                 ", APSCH_CREATED_BY " +
                                                                 ", APSCH_CREATED_DATE " +
                                                                 ", APSCH_TRN_NUMBER " +
                                                                 ", APSCH_TRN_DATE " +
                                                                 ", APSCH_REMARKS " +
                                                                 ", APSCH_REGION " +
                                                                 ", APSCH_ZONE " +
                                                                 ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                 "','" + strCode[1] +
                                                                 "','" + strCode[0] +
                                                                 "','" + CommonData.FinancialYear +
                                                                 "','" + Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy").ToUpper() + 
                                                                 "','" + CommonData.LogUserId + 
                                                                 "',getdate(),'" + txtTrnNo.Text.ToString() +
                                                                 "','" + Convert.ToDateTime(dtpDate.Value).ToString("dd/MMM/yyyy") +
                                                                 "','" + txtRemarks.Text.ToString().Replace("'","") +
                                                                 "','" + cbRegion.SelectedValue.ToString() +
                                                                 "','" + cbZones.SelectedValue.ToString() + "')";
                }




                if (strCmd.Length > 10)
                {                   
                    iRes = ObjSqlDb.ExecuteSaveData(strCmd);                   
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;

        }

        private int AuditorGridDetails()
        {
            ObjSqlDb = new SQLDB();
            int iResult = 0;
            string strCommand = "";
            try
            {
                string[] strArr = cbLocation.SelectedValue.ToString().Split('@');

                for (int i = 0; i < gvAuditorDetails.Rows.Count; i++)
                {
                    strCommand += "INSERT INTO AUDIT_PHY_STK_COUNT_AUDITORS_DETL(APSCAD_COMPANY_CODE " +
                                                                 ", APSCAD_STATE_CODE " +
                                                                 ", APSCAD_BRANCH_CODE " +
                                                                 ", APSCAD_FIN_YEAR " +
                                                                 ", APSCAD_TRN_NUMBER " +
                                                                 ", APSCAD_SL_NO " +
                                                                 ", APSCAD_EORA_CODE " +
                                                                 ", APSCAD_CREATED_BY " +
                                                                 ", APSCAD_CREATED_DATE " +
                                                                 ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                 "','" + strArr[1] +
                                                                 "','" + strArr[0] +
                                                                 "','" + CommonData.FinancialYear + "','" + txtTrnNo.Text.ToString() +
                                                                 "','" + gvAuditorDetails.Rows[i].Cells["SlNo1"].Value.ToString() +
                                                                 "','" + gvAuditorDetails.Rows[i].Cells["Ecode"].Value.ToString() +
                                                                 "','" + CommonData.LogUserId + "',getdate())";

                }
                if (strCommand.Length > 10)
                {
                    iResult = ObjSqlDb.ExecuteSaveData(strCommand);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return iResult;
        }

        private int StockGridDetails()
        {
            ObjSqlDb = new SQLDB();
            int iResult = 0;
            string strCommand = "";
            try
            {
                string[] strArr = cbLocation.SelectedValue.ToString().Split('@');

                for (int i = 0; i < gvStockDetails.Rows.Count; i++)
                {
                    strCommand += "INSERT INTO AUDIT_PHY_STK_COUNT_SP_EMP_DETL(APSCSED_COMPANY_CODE " +
                                                                 ", APSCSED_STATE_CODE " +
                                                                 ", APSCSED_BRANCH_CODE " +
                                                                 ", APSCSED_FIN_YEAR " +
                                                                 ", APSCSED_TRN_NUMBER " +
                                                                 ", APSCSED_SL_NO " +
                                                                 ", APSCSED_EORA_CODE  " +
                                                                 ", APSCSED_CREATED_BY " +
                                                                 ", APSCSED_CREATED_DATE " +
                                                                 ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                 "','" + strArr[1] +
                                                                 "','" + strArr[0] +
                                                                 "','" + CommonData.FinancialYear + "','" + txtTrnNo.Text.ToString() +
                                                                 "','" + (i+1) +
                                                                 "','" + gvStockDetails.Rows[i].Cells["Ecode1"].Value.ToString() +
                                                                 "','" + CommonData.LogUserId + "'," +
                                                                 "getdate())";

                }
                if (strCommand.Length > 10)
                {
                    iResult = ObjSqlDb.ExecuteSaveData(strCommand);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return iResult;
        }

        private int ProductionGridDetails()
        {
            ObjSqlDb = new SQLDB();
            int iResult = 0;
            string strCommand = "";
            byte[] arr;
            arr = null;
            ImageConverter converter = new ImageConverter();
            try
            {
                string[] strArr = cbLocation.SelectedValue.ToString().Split('@');

                if(gvProductDetails.Rows.Count>0)
                {

                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvProductDetails.Rows[i].Cells["LetterImage"].Value) !="")
                         arr = (byte[])gvProductDetails.Rows[i].Cells["LetterImage"].Value;



                        strCommand = "INSERT INTO AUDIT_PHY_STK_COUNT_PRODUCT_DETL(APSCPD_COMPANY_CODE " +
                                                                      ", APSCPD_STATE_CODE " +
                                                                      ", APSCPD_BRANCH_CODE " +
                                                                      ", APSCPD_FIN_YEAR " +
                                                                      ", APSCPD_TRN_NUMBER " +
                                                                      ", APSCPD_SL_NO " +
                                                                      ", APSCPD_PRODUCT_ID" +
                                                                      ", APSCPD_GOOD_QTY_PER__ERP  " +
                                                                      ", APSCPD_BAD_QTY_PER__ERP " +
                                                                      ", APSCPD_GOOD_QTY_PER__BOOK  " +
                                                                      ", APSCPD_BAD_QTY_PER__BOOK " +
                                                                      ", APSCPD_PHY_GOOD_QTY  " +
                                                                      ", APSCPD_PHY_BAD_QTY " +
                                                                      ", APSCPD_DMG_PLANTS_REMOVED" +
                                                                      ", APSCPD_PHYCNT_FLAG " +
                                                                      ", APSCPD_LETTER_COLLECTED_NOT " +
                                                                      ", APSCPD_REASON " +
                                                                      ", APSCPD_REMARKS " +
                                                                      ", APSCPD_CREATED_BY" +
                                                                      ", APSCPD_CREATED_DATE ";

                        if (arr != null )
                        {
                            strCommand += ", APSCPD_LETTER_IMAGE" ;
                        }

                      
                        strCommand += ")VALUES" +
                                              "('" + cbCompany.SelectedValue.ToString() +
                                                                     "','" + strArr[1] +
                                                                     "','" + strArr[0] +
                                                                     "','" + CommonData.FinancialYear + "','" + txtTrnNo.Text.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["SlNo3"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["GoodERP"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["DmgERP"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["GoodBook"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["DmgBook"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["GoodPhyCnt"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["DmgPhyCnt"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["DmgStkRemoved"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["PhyCntFlag"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["Letter"].Value.ToString() +
                                                                     "','" + gvProductDetails.Rows[i].Cells["Reason"].Value.ToString().Replace("'","") +
                                                                     "','" + gvProductDetails.Rows[i].Cells["Remarks"].Value.ToString().Replace("'","") +
                                                                     "','" + CommonData.LogUserId + "' , getdate() ";
                                                                   

                        if (arr != null )
                        {
                            strCommand +=", @Image";
                        }


                        strCommand += ")";

                        string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                        objSecurity = new Security();
                        SqlConnection Con = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                        SqlCommand SqlCom = new SqlCommand(strCommand, Con);
                        if (arr != null)
                        {
                            SqlCom.Parameters.Add(new SqlParameter("@Image", (object)arr));
                        }
                                              
                        Con.Open();
                        iResult = SqlCom.ExecuteNonQuery();
                        Con.Close();
                        arr = null;
                        strCommand = "";

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return iResult;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjSqlDb = new SQLDB();

            if (CheckData() == true)
            {
                try
                {

                    if (SaveHeadDetails() > 0)
                    {
                        if (AuditorGridDetails() > 0)
                        {
                            if (StockGridDetails() > 0)
                            {
                                if (ProductionGridDetails() > 0)
                                {
                                    MessageBox.Show("Data Saved SuccessFully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtRemarks.Text = "";
                                    dtpDate.Value = DateTime.Today;
                                    gvAuditorDetails.Rows.Clear();
                                    gvStockDetails.Rows.Clear();
                                    gvProductDetails.Rows.Clear();
                                    GenerateTransactionNo();
                                    flagUpdate = false;
                                    FillOpeningStockProdDetails();
                                }
                                else
                                {
                                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    flagUpdate = true;

                                }

                            }
                            else
                            {
                                flagUpdate = true;
                                MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            flagUpdate = true;
                            MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public Hashtable GetDetails(string strTrnNo, string strBCode, string strFinYear)
        {
            ObjSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = ObjSqlDb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);
                param[1] = ObjSqlDb.CreateParameter("@sFinYear", DbType.String, strFinYear.ToString(), ParameterDirection.Input);
                param[2] = ObjSqlDb.CreateParameter("@sBranCode", DbType.String, strBCode, ParameterDirection.Input);

                ds = ObjSqlDb.ExecuteDataSet("PHY_STK_CNT_HEAD_DETAILS", CommandType.StoredProcedure, param);
                ht.Add("Physicalstkcount", ds.Tables[0]);
                ds = null;
                param = null;
                ObjSqlDb = null;

                ObjSqlDb = new SQLDB();
                param = new SqlParameter[3];
                ds = new DataSet();
                param[0] = ObjSqlDb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);
                param[1] = ObjSqlDb.CreateParameter("@sFinYear", DbType.String, strFinYear.ToString(), ParameterDirection.Input);
                param[2] = ObjSqlDb.CreateParameter("@sBranCode", DbType.String, strBCode, ParameterDirection.Input);


                ds = ObjSqlDb.ExecuteDataSet("PHY_STK_CNT_AUDIT_EMP_DETAILS", CommandType.StoredProcedure, param);
                ht.Add("AuditDetails", ds.Tables[0]);



                ObjSqlDb = new SQLDB();
                param = new SqlParameter[3];
                ds = new DataSet();
                param[0] = ObjSqlDb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);
                param[1] = ObjSqlDb.CreateParameter("@sFinYear", DbType.String, strFinYear.ToString(), ParameterDirection.Input);
                param[2] = ObjSqlDb.CreateParameter("@sBranCode", DbType.String, strBCode, ParameterDirection.Input);


                ds = ObjSqlDb.ExecuteDataSet("PHY_STK_CNT_STOCK_EMP_DETAILS", CommandType.StoredProcedure, param);
                ht.Add("StockDetails", ds.Tables[0]);


                ObjSqlDb = new SQLDB();
                param = new SqlParameter[3];
                ds = new DataSet();
                param[0] = ObjSqlDb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);
                param[1] = ObjSqlDb.CreateParameter("@sFinYear", DbType.String, strFinYear.ToString(), ParameterDirection.Input);
                param[2] = ObjSqlDb.CreateParameter("@sBranCode", DbType.String, strBCode, ParameterDirection.Input);


                ds = ObjSqlDb.ExecuteDataSet("PHY_STK_CNT_PRODUCT_DETAILS", CommandType.StoredProcedure, param);
                ht.Add("ProductionDetails", ds.Tables[0]);




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ht;
        }

        public void FillPhyCntDetails()
        {
            Hashtable ht;
            DataTable dtPhyCnt;
            DataTable dtAudit;
            DataTable dtStock;
            DataTable dtProduction;
            gvAuditorDetails.Rows.Clear();
            gvStockDetails.Rows.Clear();
            gvProductDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    ht = GetDetails(txtTrnNo.Text.ToString(), cbLocation.SelectedValue.ToString().Split('@')[0], CommonData.FinancialYear);

                    dtPhyCnt = (DataTable)ht["Physicalstkcount"];
                    dtAudit = (DataTable)ht["AuditDetails"];
                    dtStock = (DataTable)ht["StockDetails"];
                    dtProduction = (DataTable)ht["ProductionDetails"];

                    if (dtPhyCnt.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        dtpDate.Value = Convert.ToDateTime(dtPhyCnt.Rows[0]["APSCH_TRN_DATE"]);
                        txtRemarks.Text = dtPhyCnt.Rows[0]["APSCH_REMARKS"].ToString();

                        if (Convert.ToInt32(dtPhyCnt.Rows[0]["BackDays"]) > 15)
                        {
                            if (CommonData.LogUserId.ToUpper() != "ADMIN" && CommonData.LogUserRole != "MANAGEMENT")
                            {
                                btnSave.Enabled = false;
                                btnDelete.Enabled = false;
                               
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                btnDelete.Enabled = true;
                               
                            }
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnDelete.Enabled = true;
                        }


                        if (dtAudit.Rows.Count > 0)
                        {
                            
                            for (int i = 0; i < dtAudit.Rows.Count; i++)
                            {
                                gvAuditorDetails.Rows.Add();
                                gvAuditorDetails.Rows[i].Cells["SlNo1"].Value = (i + 1).ToString();
                                gvAuditorDetails.Rows[i].Cells["Ecode"].Value = dtAudit.Rows[i]["APSCAD_EORA_CODE"].ToString();
                                gvAuditorDetails.Rows[i].Cells["EmpName"].Value = dtAudit.Rows[i]["HRIS_EMP_NAME"].ToString();
                                gvAuditorDetails.Rows[i].Cells["Desig"].Value = dtAudit.Rows[i]["desig_name"].ToString();
                                gvAuditorDetails.Rows[i].Cells["DOJ"].Value = Convert.ToDateTime(dtAudit.Rows[i]["EMP_DOJ"].ToString()).ToShortDateString();

                            }
                        }
                        if (dtStock.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtStock.Rows.Count; i++)
                            {
                                gvStockDetails.Rows.Add();
                                gvStockDetails.Rows[i].Cells["Slno2"].Value = (i + 1).ToString();
                                gvStockDetails.Rows[i].Cells["Ecode1"].Value = dtStock.Rows[i]["APSCSED_EORA_CODE"].ToString();
                                gvStockDetails.Rows[i].Cells["Ename"].Value = dtStock.Rows[i]["HRIS_EMP_NAME"].ToString();
                                gvStockDetails.Rows[i].Cells["Designation"].Value = dtStock.Rows[i]["desig_name"].ToString();
                                gvStockDetails.Rows[i].Cells["Doj2"].Value = Convert.ToDateTime(dtStock.Rows[i]["EMP_DOJ"].ToString()).ToShortDateString();

                            }
                        }
                        if (dtProduction.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtProduction.Rows.Count; i++)
                            {
                                gvProductDetails.Rows.Add();
                                gvProductDetails.Rows[i].Cells["SlNo3"].Value = (i + 1).ToString();
                                gvProductDetails.Rows[i].Cells["ProductId"].Value = dtProduction.Rows[i]["APSCPD_PRODUCT_ID"].ToString();
                                gvProductDetails.Rows[i].Cells["ProdName"].Value = dtProduction.Rows[i]["PM_PRODUCT_NAME"].ToString();
                                gvProductDetails.Rows[i].Cells["Catg"].Value = dtProduction.Rows[i]["CATEGORY_NAME"].ToString();
                                gvProductDetails.Rows[i].Cells["GoodERP"].Value = dtProduction.Rows[i]["APSCPD_GOOD_QTY_PER__ERP"].ToString();
                                gvProductDetails.Rows[i].Cells["DmgERP"].Value = dtProduction.Rows[i]["APSCPD_BAD_QTY_PER__ERP"].ToString();
                                gvProductDetails.Rows[i].Cells["GoodBook"].Value = dtProduction.Rows[i]["APSCPD_GOOD_QTY_PER__BOOK"].ToString();
                                gvProductDetails.Rows[i].Cells["DmgBook"].Value = dtProduction.Rows[i]["APSCPD_BAD_QTY_PER__BOOK"].ToString();
                                gvProductDetails.Rows[i].Cells["GoodPhyCnt"].Value = dtProduction.Rows[i]["APSCPD_PHY_GOOD_QTY"].ToString();
                                gvProductDetails.Rows[i].Cells["DmgPhyCnt"].Value = dtProduction.Rows[i]["APSCPD_PHY_BAD_QTY"].ToString();
                                gvProductDetails.Rows[i].Cells["GoodDiff"].Value = Convert.ToDouble(dtProduction.Rows[i]["APSCPD_GOOD_QTY_PER__BOOK"]) - (Convert.ToDouble(dtProduction.Rows[i]["APSCPD_PHY_GOOD_QTY"]));
                                gvProductDetails.Rows[i].Cells["DmgDiff"].Value = Convert.ToDouble(dtProduction.Rows[i]["APSCPD_BAD_QTY_PER__BOOK"]) - (Convert.ToDouble(dtProduction.Rows[i]["APSCPD_PHY_BAD_QTY"]));
                                gvProductDetails.Rows[i].Cells["TotDiff"].Value = Convert.ToDouble(gvProductDetails.Rows[i].Cells["GoodDiff"].Value) + (Convert.ToDouble(gvProductDetails.Rows[i].Cells["DmgDiff"].Value));
                                gvProductDetails.Rows[i].Cells["DmgStkRemoved"].Value = dtProduction.Rows[i]["APSCPD_DMG_PLANTS_REMOVED"].ToString();
                                gvProductDetails.Rows[i].Cells["PhyCntFlag"].Value = dtProduction.Rows[i]["APSCPD_PHYCNT_FLAG"].ToString();
                                gvProductDetails.Rows[i].Cells["Letter"].Value = dtProduction.Rows[i]["APSCPD_LETTER_COLLECTED_NOT"];
                                gvProductDetails.Rows[i].Cells["Reason"].Value = dtProduction.Rows[i]["APSCPD_REASON"].ToString();

                                if (dtProduction.Rows[i]["APSCPD_LETTER_IMAGE"].ToString() != null)
                                {
                                    gvProductDetails.Rows[i].Cells["LetterImage"].Value = dtProduction.Rows[i]["APSCPD_LETTER_IMAGE"];
                                }
                                else
                                {
                                    gvProductDetails.Rows[i].Cells["LetterImage"].Value = "";
                                }
                                gvProductDetails.Rows[i].Cells["Remarks"].Value = dtProduction.Rows[i]["APSCPD_REMARKS"].ToString();

                            }
                            CalculateTotals();
                        }
                    }
                    else
                    {
                        flagUpdate = false;
                        //cbCompany.SelectedIndex = 0;
                        //cbZones.SelectedIndex = -1;
                        // cbRegion.SelectedIndex = -1;
                        // cbLocation.SelectedIndex = -1;
                        gvAuditorDetails.Rows.Clear();
                        gvStockDetails.Rows.Clear();
                        gvProductDetails.Rows.Clear();
                        txtRemarks.Text = "";
                        dtpDate.Value = DateTime.Today;
                        GenerateTransactionNo();
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
           
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtTrnNo.Text.Length > 0)
                {
                    FillPhyCntDetails();
                }
                else
                {
                    flagUpdate = false;
                    //cbCompany.SelectedIndex = 0;
                    //cbZones.SelectedIndex = -1;
                    // cbRegion.SelectedIndex = -1;
                    // cbLocation.SelectedIndex = -1;
                    gvAuditorDetails.Rows.Clear();
                    gvStockDetails.Rows.Clear();
                    gvProductDetails.Rows.Clear();
                    txtRemarks.Text = "";
                    dtpDate.Value = DateTime.Today;
                    GenerateTransactionNo();
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtTrnNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            //cbCompany.SelectedIndex = 0;
            //cbZones.SelectedIndex = -1;
            //cbRegion.SelectedIndex = -1;
            //cbLocation.SelectedIndex = -1;
            dtpDate.Value = DateTime.Today;
            gvAuditorDetails.Rows.Clear();
            gvStockDetails.Rows.Clear();
            gvProductDetails.Rows.Clear();
            txtRemarks.Text = "";
            txtTrnNo.Text = "";
            GenerateTransactionNo();
            txtBookDmgQty.Text = "";
            txtBookGoodQty.Text = "";
            txtCntDmgQty.Text = "";
            txtCntGoodQty.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = true;

        }
       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            ObjSqlDb = new SQLDB();
            string strcmd = "";
            if (txtTrnNo.Text.Length>0 && flagUpdate == true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        strcmd = "DELETE FROM AUDIT_PHY_STK_COUNT_AUDITORS_DETL where APSCAD_TRN_NUMBER='" + txtTrnNo.Text +
                        "' and APSCAD_FIN_YEAR='" + CommonData.FinancialYear + "' and APSCAD_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "' ";

                        strcmd += " DELETE FROM AUDIT_PHY_STK_COUNT_SP_EMP_DETL where APSCSED_TRN_NUMBER='" + txtTrnNo.Text +
                        "' and APSCSED_FIN_YEAR='" + CommonData.FinancialYear + "' and APSCSED_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "' ";

                        strcmd += " DELETE FROM AUDIT_PHY_STK_COUNT_PRODUCT_DETL where APSCPD_TRN_NUMBER='" + txtTrnNo.Text +
                        "' and APSCPD_FIN_YEAR='" + CommonData.FinancialYear + "' and APSCPD_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "' ";

                        strcmd += " DELETE FROM AUDIT_PHY_STK_COUNT_HEAD where APSCH_TRN_NUMBER='" + txtTrnNo.Text +
                        "' and APSCH_FIN_YEAR='" + CommonData.FinancialYear + "' and APSCH_BRANCH_CODE='" + cbLocation.SelectedValue.ToString().Split('@')[0] + "' ";

                        if (strcmd.Length > 10)
                        {
                            iRes = ObjSqlDb.ExecuteSaveData(strcmd);
                        }
                        if (iRes > 0)
                        {
                            MessageBox.Show("Data Deleted Sucessfully", "Confirm?", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            btnCancel_Click(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }


        }

     

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            GoodQtyCell = true;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["GoodERP"].Index)
            {
                TextBox txtGoodQty = e.Control as TextBox;
                if(txtGoodQty != null)
                {
                    txtGoodQty.MaxLength = 6;
                    txtGoodQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["PhyCntFlag"].Index)
            {
               GoodQtyCell = false;
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["Letter"].Index)
            {
                GoodQtyCell = false;
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["Reason"].Index)
            {
                GoodQtyCell = false;
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["Remarks"].Index)
            {
                GoodQtyCell = false;
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
           
            
                if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) &&(GoodQtyCell == true))
                {
                    e.Handled = true;
                    return;
                }

                // checks to make sure only 1 decimal is allowed
                if (e.KeyChar == 46)
                {
                    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }
            
        }

        private void CalculateTotals()
        {

            double totGoodQty = 0;
            double totDmgQty = 0;
            double totCntGoodQty = 0;
            double totCntDmgQty = 0;
            for (int nRow = 0; nRow < gvProductDetails.Rows.Count; nRow++)
            {

                if (Convert.ToString(gvProductDetails.Rows[nRow].Cells["GoodBook"].Value) != "")
                {
                    totGoodQty += Convert.ToDouble(gvProductDetails.Rows[nRow].Cells["GoodBook"].Value.ToString());
                }
                if (Convert.ToString(gvProductDetails.Rows[nRow].Cells["DmgBook"].Value) != "")
                {
                    totDmgQty += Convert.ToDouble(gvProductDetails.Rows[nRow].Cells["DmgBook"].Value.ToString());
                }
                if (Convert.ToString(gvProductDetails.Rows[nRow].Cells["GoodPhyCnt"].Value) != "")
                {
                    totCntGoodQty += Convert.ToDouble(gvProductDetails.Rows[nRow].Cells["GoodPhyCnt"].Value.ToString());
                }
                if (Convert.ToString(gvProductDetails.Rows[nRow].Cells["DmgPhyCnt"].Value) != "")
                {
                    totCntDmgQty += Convert.ToDouble(gvProductDetails.Rows[nRow].Cells["DmgPhyCnt"].Value.ToString());
                }

            }
            txtBookGoodQty.Text = totGoodQty.ToString("f");
            txtBookDmgQty.Text = totDmgQty.ToString("f");
            txtCntGoodQty.Text = totCntGoodQty.ToString("f");
            txtCntDmgQty.Text = totCntDmgQty.ToString("f");

            
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == gvProductDetails.Columns["GoodBook"].Index) || (e.ColumnIndex == gvProductDetails.Columns["GoodPhyCnt"].Index))
            {
                gvProductDetails.Rows[e.RowIndex].Cells["GoodDiff"].Value =Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["GoodBook"].Value) -(Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["GoodPhyCnt"].Value));
            }
            if ((e.ColumnIndex == gvProductDetails.Columns["DmgBook"].Index) || (e.ColumnIndex == gvProductDetails.Columns["DmgPhyCnt"].Index))
            {
                gvProductDetails.Rows[e.RowIndex].Cells["DmgDiff"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DmgBook"].Value) - (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DmgPhyCnt"].Value));
            }

            if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["GoodDiff"].Value) != "" || Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["DmgDiff"].Value) != "")
            {
                gvProductDetails.Rows[e.RowIndex].Cells["TotDiff"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["GoodDiff"].Value) + Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DmgDiff"].Value);
            }

            CalculateTotals();
        }

        private void gvProductDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["PhyCntFlag"].Index)
            {
                if ((gvProductDetails.Rows[e.RowIndex].Cells["PhyCntFlag"].Value.ToString()) == "N")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["PhyCntFlag"].Value = "Y";
                }
                else if ((gvProductDetails.Rows[e.RowIndex].Cells["PhyCntFlag"].Value.ToString()) == "Y")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["PhyCntFlag"].Value = "N";
                }


            }
            if (e.ColumnIndex == gvProductDetails.Columns["Letter"].Index)
            {
                if ((gvProductDetails.Rows[e.RowIndex].Cells["Letter"].Value.ToString()) == "N")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Letter"].Value = "Y";
                }
                else if ((gvProductDetails.Rows[e.RowIndex].Cells["Letter"].Value.ToString()) == "Y")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Letter"].Value = "N";
                }


            }

        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] Arr;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvProductDetails.Columns["Del"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {

                        dgvr = gvProductDetails.Rows[e.RowIndex];
                        gvProductDetails.Rows.Remove(dgvr);

                        if (gvStockDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                            {
                                gvProductDetails.Rows[i].Cells["SlNo3"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }


                if (e.ColumnIndex == gvProductDetails.Columns["Image1"].Index)
                {
                    gvProductDetails.BeginEdit(true);
                    ComboBox combo = (ComboBox)gvProductDetails.EditingControl;
                    if (combo.SelectedIndex==0)
                    {
                        ImageBrowser img = new ImageBrowser(this, e.RowIndex, "PHY_STK_CNT");
                        img.objPhyStkCnt = this;
                        img.ShowDialog();

                    }
                    else if (combo.SelectedIndex == 1)
                    {
                        if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["LetterImage"].Value) != "")
                        {
                            DialogResult dlgResult = MessageBox.Show("Do you want delete Image", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dlgResult == DialogResult.Yes)
                            {
                                if (gvProductDetails.Rows.Count > 0)
                                {
                                    gvProductDetails.Rows[e.RowIndex].Cells["LetterImage"].Value = "";
                                    MessageBox.Show("Image Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    combo.SelectedIndex = 0;

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Image", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            combo.SelectedIndex = 0;
                        }
                        
                    }
                    else if (combo.SelectedIndex == 2)
                    {
                        if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["LetterImage"].Value.ToString()) != "")
                        {

                            Arr = null;
                            Arr = (byte[])gvProductDetails.Rows[e.RowIndex].Cells["LetterImage"].Value;
                            frmDisplayImage ImgView = new frmDisplayImage(Arr);
                            ImgView.objPhyStkCnt = this;
                            ImgView.ShowDialog();
                        }                       
                        
                       
                    }
                }


                
            }

        }
        private bool CheckData()
        {
            bool bFlag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return bFlag;
            }
            if (cbZones.SelectedIndex == 0 || cbZones.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Zone", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbZones.Focus();
                return bFlag;
            }
            if (cbRegion.SelectedIndex == 0 || cbRegion.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Region", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbRegion.Focus();
                return bFlag;
            }
            if (cbLocation.SelectedIndex == 0 || cbLocation.SelectedIndex==-1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLocation.Focus();
                return bFlag;
            }
            if (Convert.ToDateTime(dtpDate.Value) > DateTime.Today)
            {
                bFlag = false;
                MessageBox.Show("Please Select Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpDate.Focus();
                return bFlag;
            }

            if (gvAuditorDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Auditor Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddAuditordetails.Focus();
                return bFlag;

            }
            if (gvStockDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add StockDetails", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddStkdetails.Focus();
                return bFlag;

            }
            if (gvProductDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add ProductDetails", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnProductSearch.Focus();
                return bFlag;
            }


            return bFlag;

        }

        private void gvAuditorDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvAuditorDetails.Columns["Del_EmpDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {

                        dgvr = gvAuditorDetails.Rows[e.RowIndex];
                        gvAuditorDetails.Rows.Remove(dgvr);

                        if (gvAuditorDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvAuditorDetails.Rows.Count; i++)
                            {
                                gvAuditorDetails.Rows[i].Cells["SlNo1"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }
            }
        }

        private void gvStockDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvStockDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        dgvr = gvStockDetails.Rows[e.RowIndex];
                        gvStockDetails.Rows.Remove(dgvr);

                        if (gvStockDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvStockDetails.Rows.Count; i++)
                            {
                                gvStockDetails.Rows[i].Cells["Slno2"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }
            }
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                ProductSearchAll Pdtfrm = new ProductSearchAll("Physicalstkcount", cbCompany.SelectedValue.ToString());
                Pdtfrm.objPhystkfrm = this;
                Pdtfrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

       
        
    }
}
