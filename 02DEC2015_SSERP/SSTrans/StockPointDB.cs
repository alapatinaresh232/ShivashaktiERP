using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SSCRMDB;
using System.Data.SqlClient;
using System.Collections;

namespace SSTrans
{
    public class StockPointDB
    {
        private SQLDB objSQLdb = null;

        #region Constructor
        public StockPointDB()
        {          
            
        }
        #endregion

        public DataSet SPGRNHeadDetails_Get(string strCompCode, string strBranchCode, string strDocMonth, string grnRefno)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sGrnRefNo", DbType.String, grnRefno, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SPGRNHeadDetails_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public DataSet SPPKMGRNHeadDetails_Get(string strCompCode, string strBranchCode, string strDocMonth, string grnRefno)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sGrnRefNo", DbType.String, grnRefno, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SPPKMGRNHeadDetails_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public DataSet SPGRNDetlInfo_Get(string strCompCode, string strBranchCode, string strDocMonth, string grnno)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sGrnNo", DbType.String, grnno, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SPGRNDetlInfo_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet SPPKMGRNDetlInfo_Get(string strCompCode, string strBranchCode, string strDocMonth, string grnno)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sGrnNo", DbType.String, grnno, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SPPKMGRNDetlInfo_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }        

        public DataSet SPDCHeadDetails_Get(string strCompCode, string strBranchCode, string strDocMonth, string grnno,string sTrnType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sDCRefNo", DbType.String, grnno, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@sTrnType", DbType.String, sTrnType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SPDCHeadDetails_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet SPRentalAgrimentDetl_Get(string strCompCode, string strBranchCode, Int32 strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sTrnNo", DbType.Int32, strTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SPRentalAgrimentDetl_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet Get_TripSheetHeadDetails(string sTrnNo, string sRefNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRefNo", DbType.String, sRefNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_TripSheetHead", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public Hashtable Get_TripSheetDetails(string sTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {


                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_TripSheetDeliveryDetails", CommandType.StoredProcedure, param);
                ht.Add("TripDeliveryDetails", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[3];
                ds = new DataSet();

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_TripSheetProductDetl", CommandType.StoredProcedure, param);
                ht.Add("TripProductDetails", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[3];
                ds = new DataSet();

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_TripSheetDieselDetl", CommandType.StoredProcedure, param);
                ht.Add("TripDieselDetl", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[3];
                ds = new DataSet();

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_TripSheetDCDetl", CommandType.StoredProcedure, param);
                ht.Add("TripDCorDCSTDetl", ds.Tables[0]);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }

        public DataSet Get_UserBranchesWithStateFilter(string sCompCode, string sStateCode, string sLogUserId, string sBranchtType, string sGetType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sUser", DbType.String, sLogUserId, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_UserBranches_With_StateFilter", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public Int64 GenerateInvNoForSPInvoice(string sBranchCode)
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            Int64 InvNo = 0;
            try
            {
                string sqlSel = " SELECT isNull(MAX(SISH_SP_SP_INVOICE_NO),0)+1  AS TrnNo FROM SALES_INV_SP_HEAD " +
                                " WHERE SISH_SP_BRANCH_CODE ='" + sBranchCode +
                                "' AND SISH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                ds = objSQLdb.ExecuteDataSet(sqlSel, CommandType.Text);
                InvNo = Convert.ToInt64(ds.Tables[0].Rows[0][0]);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return InvNo;
        }

        public Hashtable GetInvoiceBulitin_For_SPInvoice(string strCompCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvoiceBulitinHead_Get", CommandType.StoredProcedure, param);
                ht.Add("InvHead", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    nInvoiceNo = Convert.ToInt32(ds.Tables[0].Rows[0]["invoice_number"]);
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvoiceBultinDetail_Get", CommandType.StoredProcedure, param);
                ht.Add("InvDetail", ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }

        public DataSet SalesOrderForSPInvoice_Get(string sCompCode, string sBranCode,string sFinYear, string sOrdNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, sFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrdNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SalesOrderForBuiltInAndInvoice_Get", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public Hashtable GetInvoiceDataForSPInvoice(string strCompCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetInvoiceHead_Proc", CommandType.StoredProcedure, param);
                ht.Add("InvHead", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    nInvoiceNo = Convert.ToInt32(ds.Tables[0].Rows[0]["invoice_number"]);
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetInvoiceDetail_Proc", CommandType.StoredProcedure, param);
                ht.Add("InvDetail", ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }

        public DataTable Get_InvoiceDetailFreeItemsForSPInvoice(string sCompCode, string sBranCode, Int32 intInvNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, intInvNo, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("InvoiceDetailFreeItems_Get", CommandType.StoredProcedure, param).Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return dt;
        }

        public DataTable InvoiceBltnDetailFreeItemsForSPInvoice_Get(string sCompCode, string sBranCode, Int32 intInvNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, intInvNo, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("InvoiceBltnDetailFreeItems_Get", CommandType.StoredProcedure, param).Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return dt;
        }

        public DataSet Get_SPInvoiceHeadDetails(string sCompCode, string sBranCode, string sSPCode, Int32 nSPInvNo, Int32 nInvNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sSPBranCode", DbType.String, sSPCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nSPInvNo", DbType.Int32, nSPInvNo, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvNo, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("StockPointInvoiceHead_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public string getProductRatePoints_ForSPInv(string sCompCode, string sBranCode, string sStateCode, string sProdCode, double nProdQty, DateTime dInvDate)
        {
            objSQLdb = new SQLDB();
            string strRatePoints = string.Empty;
            string sqlSel = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sqlSel = " SELECT PRRM_PRODUCT_ID, PRRM_VALID_TABLE_NUMBER, PRRM_VALID_STATUS, PRRM_FROM_DATE, PRRM_TO_DATE" +
                                ", PRRM_FROM_QTY_RANGE, PRRM_TO_QTY_RANGE, PRRM_RATE, PRRM_PRODUCT_POINTS FROM  PRODUCT_RATE_RANGE_MAS" +
                                " WHERE  PRRM_COMPANY_CODE='" + sCompCode +
                                "' AND PRRM_STATE_CODE='" + sStateCode +
                                "' AND PRRM_BRANCH_CODE='" + sBranCode +
                    //"' AND  PRRM_FIN_YEAR='" + CommonData.FinancialYear +
                                "' AND  PRRM_PRODUCT_ID='" + sProdCode + "'";

                dt = objSQLdb.ExecuteDataSet(sqlSel, CommandType.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToDateTime(dInvDate) >= Convert.ToDateTime(Convert.ToDateTime(dr["PRRM_FROM_DATE"] + "").ToString("dd/MM/yyyy")) && Convert.ToDateTime(Convert.ToDateTime(dr["PRRM_TO_DATE"] + "").ToString("dd/MM/yyyy")) >= Convert.ToDateTime(dInvDate))
                        {
                            if (nProdQty >= Convert.ToDouble(dr["PRRM_FROM_QTY_RANGE"] + "") && Convert.ToDouble(dr["PRRM_TO_QTY_RANGE"] + "") >= nProdQty)
                            {
                                strRatePoints = dr["PRRM_RATE"] + "";
                                strRatePoints = strRatePoints + "@" + dr["PRRM_PRODUCT_POINTS"] + "";
                                break;
                            }
                            else if (nProdQty >= Convert.ToDouble(dr["PRRM_FROM_QTY_RANGE"] + "") && Convert.ToDouble(dr["PRRM_TO_QTY_RANGE"] + "") == 0)
                            {
                                strRatePoints = dr["PRRM_RATE"] + "";
                                strRatePoints = strRatePoints + "@" + dr["PRRM_PRODUCT_POINTS"] + "";
                                break;
                            }

                        }

                    }
                }
                //else
                //{
                //    sqlSel = " SELECT  TIP_Rate, TIP_Product_Points FROM TEMPLATE_INV_PRODUCT " +
                //                    " WHERE TIP_Company_Code='" + CommonData.CompanyCode +
                //                    "' AND TIP_Fin_Year='" + CommonData.FinancialYear +
                //                    "' AND TIP_BRANCH_CODE='" + CommonData.BranchCode +
                //                    "' AND TIP_Product_code='" + sProdCode + "'";
                //    dt = objSQLdb.ExecuteDataSet(sqlSel, CommandType.Text).Tables[0];
                //    if (dt.Rows.Count > 0)
                //    {
                //        strRatePoints = dt.Rows[0]["TIP_Rate"] + "";
                //        strRatePoints = strRatePoints + "@" + dt.Rows[0]["TIP_Product_Points"] + "";
                //    }
                //}

                dt = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return strRatePoints;
        }

        public DataTable InvFreeProductForSPInv_Get(string sCompCode, string sBranCode, string StateCode, string sProdCode, double nQty)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataTable dt = new DataTable();
            try
            {
                if (dt.Rows.Count == 0)
                {
                    dt.Clear();
                    param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                    param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                    param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
                    param[3] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                    param[4] = objSQLdb.CreateParameter("@sProductCode", DbType.String, sProdCode, ParameterDirection.Input);
                    param[5] = objSQLdb.CreateParameter("@nSoldUnit", DbType.Double, nQty, ParameterDirection.Input);
                    dt = objSQLdb.ExecuteDataSet("InvFreeProduct_Get", CommandType.StoredProcedure, param).Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return dt;
        }

        public DataTable Get_GCandAboveList(string sCompCode, string sBranCode, string DocMonth, string EcodeSearch)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, EcodeSearch, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("Get_GCandAboveList", CommandType.StoredProcedure, param).Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return dt;
        }
        public DataTable Get_DcDcSTNo_BY_LRNO_StockDumping(string sBranCode, string sToBranCode, string sDocMonth, string sDCDate, string sLRNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataTable dt = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xBranCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xToBranCode", DbType.String, sToBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDCDate", DbType.String, sDCDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xLRNo", DbType.String, sLRNo, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("Get_DCDCSTrnsFor_StockDumping", CommandType.StoredProcedure, param).Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return dt;
        }

        public Hashtable Get_StockDumpingDetails(string sCompode, string sBrancode, string sFinYear, string sDocMonth, string sTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBrancode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_StockDumpingHead", CommandType.StoredProcedure, param);
                ht.Add("StockDumpHead", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[5];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBrancode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_StockDumpingDetl", CommandType.StoredProcedure, param);
                ht.Add("StockDumpDetl", ds.Tables[0]);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }

    }
}
