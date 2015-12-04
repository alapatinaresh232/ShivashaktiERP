using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using SSCRMDB;

namespace SSTrans
{
    public class InvoiceDB
    {
        private SQLDB objSQLdb = null;
        
        #region Constructor
        public InvoiceDB()
        {          
            
        }
        #endregion
        
        public DataSet GetVillageSearch(string sType, string sSearch)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sType", DbType.String, sType, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sSearch", DbType.String, sSearch, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("VillageSearch_Proc", CommandType.StoredProcedure, param);

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
        public DataSet InvLevelEmpNameSearch(string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevel_EMPName_Search", CommandType.StoredProcedure, param);
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
        public DataSet VillageMasterFilter_Get(string sStateCode, string sDistrict, string sMandal)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCDState", DbType.String, sStateCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDistrict", DbType.String, sDistrict, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sMandal", DbType.String, sMandal, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("VillageMasterFilter_Get", CommandType.StoredProcedure, param);

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
                
        public DataSet GetVillageDataSet(Hashtable htParams)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sVillage", DbType.String, Convert.ToString(htParams["sVillage"]), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDistrict", DbType.String, Convert.ToString(htParams["sDistrict"]), ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sCDState", DbType.String, Convert.ToString(htParams["sCDState"]), ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("VillageMaster_Proc", CommandType.StoredProcedure, param);

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

        public DataSet GetProductDataSet(string sCompCode, string sBranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
           
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("ProductMaster_Proc", CommandType.StoredProcedure, param);
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

        public Hashtable GetInvoiceData(string strSCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
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
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
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

        public Int64 CheckInvoiceNoForBuiltIn(string strSCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Int64 intInvNo = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetInvoiceHead_Proc", CommandType.StoredProcedure, param);
                if (ds.Tables[0].Rows.Count > 0)
                    intInvNo = Convert.ToInt64(ds.Tables[0].Rows[0]["invoice_number"]);
                ds = null;

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
            return intInvNo;
        }
        public Hashtable GetDLOrderBookingData(string sCompanyCode, string sBrCode, int iOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            int nTrnNo = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBrCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sOrderNo", DbType.Int32, iOrderNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DL_GetOrderBookingHeadData", CommandType.StoredProcedure, param);
                ht.Add("OrderHead", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    nTrnNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DLOH_TRN_NO"]);
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@nTrnNo", DbType.Int32, nTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DL_GetOrderBookingDetailData", CommandType.StoredProcedure, param);
                ht.Add("OrderDetail", ds.Tables[0]);

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
        public DataSet IndentStationaryList_Get(string strCCode, string strBCode, string sItamName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            //Int64 intInvNo = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, strCCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sItemName", DbType.String, sItamName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("StationaryItemsSearch_Get_search", CommandType.StoredProcedure, param);
                
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
        public DataSet BrIndentStationaryList_Get(string strCCode, string strBCode, string sItamName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            //Int64 intInvNo = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, strCCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sItemName", DbType.String, sItamName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("StationaryItemsSearch_Get", CommandType.StoredProcedure, param);

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

        public Int64 CheckBuiltInNoForInvoice(string strSCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Int64 intInvNo = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);
                
                ds = objSQLdb.ExecuteDataSet("InvoiceBulitinHead_Get", CommandType.StoredProcedure, param);
                if (ds.Tables[0].Rows.Count > 0)
                    intInvNo = Convert.ToInt64(ds.Tables[0].Rows[0]["invoice_number"]);
                ds = null;

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
            return intInvNo;
        }

        public Int64 CheckPreviousInvoiceNo(string strSCode, string strBCode, string strFinYear, int nInvoiceNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Int64 intInvNo = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, strFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvoiceHeadPrev_Get", CommandType.StoredProcedure, param);
                if (ds.Tables[0].Rows.Count > 0)
                    intInvNo = Convert.ToInt64(ds.Tables[0].Rows[0]["invoice_number"]);
                ds = null;

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
            return intInvNo;
        }

        public Hashtable InvoiceBulitin_Get(string strSCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
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

        public Hashtable GetPrevInvoiceData(string strSCode, string strBCode, string strFinYear, int nInvoiceNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, strFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvoiceHeadPrev_Get", CommandType.StoredProcedure, param);
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
                ds = objSQLdb.ExecuteDataSet("InvoiceDetailPrev_Get", CommandType.StoredProcedure, param);
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
        
        public DataSet InvoiceBuiltinList_Get(int nECODE, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nECODE", DbType.Int32, nECODE, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvoiceBuiltinList_Get", CommandType.StoredProcedure, param);
                
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

        public DataSet StkAdjustTrnList_Get(int nECODE, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nECODE", DbType.Int32, nECODE, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("StkAdjustTrnList_Get", CommandType.StoredProcedure, param);

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

        public DataSet InvoiceBulitinListToCustomer_Get()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvoiceBulitinListToCustomer_Get", CommandType.StoredProcedure, param);

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
        
        public string GetCustomerFarmerId(string sState, string sDist, string sMandal, string sPanchayat)
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            string NewFarmerId = string.Empty;
            SqlParameter[] param = new SqlParameter[4];
            try
            {
                param[0] = objSQLdb.CreateParameter("@sState", DbType.String, sState, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDistrict", DbType.String, sDist, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sMandal", DbType.String, sMandal, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sPanchayat", DbType.String, sPanchayat, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("GenerateFormerId_Proc", CommandType.StoredProcedure, param);
                NewFarmerId = Convert.ToString(ds.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                param = null;
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                ds = null;
                objSQLdb = null;
            }
            return NewFarmerId;
        }

        public DataSet GetXLSCustomerDetls(int intXLSInvNo)
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            try
            {
                ds = objSQLdb.ExecuteDataSet("select * from SalesXLS_Customer_Data where cm_invoice_number=" + intXLSInvNo, CommandType.Text);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            finally
            {

                objSQLdb = null;

            }
            return ds;
        }

        public DataSet InvCustomerSearch_Get(string sCustName, string sVillage, string sMandal, string sState)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sSearch", DbType.String, sCustName, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sVillage", DbType.String, sVillage, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sMandal", DbType.String, sMandal, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sState", DbType.String, sState, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvCustomerSearch_Get", CommandType.StoredProcedure, param);
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

        public DataSet GetEcodeList(string sCompCode, string sBranchCode, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelEcode_Proc", CommandType.StoredProcedure, param);

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

        public DataSet GetGCListForStkContra(string sCompCode, string sBranchCode, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("StkAdjustEcodeList_Get", CommandType.StoredProcedure, param);

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

        public DataSet GetAllEcodeList(string sCompCode, string sBranchCode, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelAllEcode_Proc", CommandType.StoredProcedure, param);

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

        public DataSet GetGLEcodeList(string sCompCode, string sBranchCode, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelGLEcode_Proc", CommandType.StoredProcedure, param);

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

        public DataSet InvLevelEcodeSearch_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelEcodeSearch_Get", CommandType.StoredProcedure, param);

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

        public DataSet StkAdjustEcodeSearch_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelEcodeSearch_Get", CommandType.StoredProcedure, param);

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

        public DataSet OrdShtReturnEcodeSearch_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("OrdShtReturnEcodeSearch_Get", CommandType.StoredProcedure, param);

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

         public DataSet GetTranportCostDetails(string sBranchCode, string sDocMonth, int nECODE)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {              
                param[0] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sECode", DbType.Int32, nECODE, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_Tranport_Cost_Details", CommandType.StoredProcedure, param);

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



         public DataSet InvLevel_Transport_Cost_GLEcode_Proc(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName)
         {
             objSQLdb = new SQLDB();
             SqlParameter[] param = new SqlParameter[4];
             DataSet ds = new DataSet();
             try
             {

                 param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                 param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                 param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                 param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                 ds = objSQLdb.ExecuteDataSet("InvLevel_Transport_Cost_GLEcode_Proc", CommandType.StoredProcedure, param);

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


  public DataSet GetTransportCostEcodeList(string sCompCode, string sBranchCode, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {



                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevel_Transport_Cost_EcodeSearch_Proc", CommandType.StoredProcedure, param);

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
    

        public DataSet InvLevelFSEcodeSearch_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName, Int32 strSREcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sSRCode", DbType.Int32, strSREcode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelFSEcodeSearch_Get", CommandType.StoredProcedure, param);

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

        public DataSet OrderSheetIssueEcodeSearch_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("OrderSheetIssueEcodeSearch_Get", CommandType.StoredProcedure, param);

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
        public DataSet InvLevelAllEcodeSearch_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelAllEcodeSearch_Get", CommandType.StoredProcedure, param);

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

        public DataSet InvLevelGLEcodeSearch_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelGLEcodeSearch_Get", CommandType.StoredProcedure, param);

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

        public DataSet InvLevelSREcodeSearchByGL_Get(string sCompCode, string sBranchCode, string sDocMonth, Int32 iGLEcode, string sEcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sGLCode", DbType.Int32, iGLEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvLevelSREcodeSearchByGL_Get", CommandType.StoredProcedure, param);

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

        public DataSet AdminBranchCursor_Get(string sCompCode, string sBranchtType, string sGetType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranch", DbType.String, sBranchtType, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("AdminBranchCursor_Get", CommandType.StoredProcedure, param);

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
   
        public DataSet GetProductMasterSearch(string sCompanyCode, string sCategoryCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sCategoryCode", DbType.String, sCategoryCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("ProductMasterSearh_Get", CommandType.StoredProcedure, param);

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



        public DataSet InvProductSearchCursor_Get(string sCompanyCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, CommonData.StateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvProductSearchCursor_Get_ByDocMonth", CommandType.StoredProcedure, param);

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

        public string getProductRatePoints(string sProdCode, double nProdQty, DateTime dInvDate)
        {
            objSQLdb = new SQLDB();
            string strRatePoints = string.Empty;
            string sqlSel = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                 sqlSel = " SELECT PRRM_PRODUCT_ID, PRRM_VALID_TABLE_NUMBER, PRRM_VALID_STATUS, PRRM_FROM_DATE, PRRM_TO_DATE" +
                                 ", PRRM_FROM_QTY_RANGE, PRRM_TO_QTY_RANGE, PRRM_RATE, PRRM_PRODUCT_POINTS FROM  PRODUCT_RATE_RANGE_MAS" +
                                 " WHERE  PRRM_COMPANY_CODE='" + CommonData.CompanyCode +
                                 "' AND PRRM_STATE_CODE='" + CommonData.StateCode +
                                 "' AND PRRM_BRANCH_CODE='" + CommonData.BranchCode +
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

        public DataTable InvFreeProduct_Get(string sProdCode, double nQty)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataTable dt = new DataTable();
            try
            {

                //param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                //param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                //param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, CommonData.StateCode, ParameterDirection.Input);
                //param[3] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                //param[4] = objSQLdb.CreateParameter("@sProductCode", DbType.String, sProdCode, ParameterDirection.Input);
                //param[5] = objSQLdb.CreateParameter("@nSoldUnit", DbType.Int16, nQty, ParameterDirection.Input);
                //dt = objSQLdb.ExecuteDataSet("InvFreeProduct_Absolute_Get", CommandType.StoredProcedure, param).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dt.Clear();
                    param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                    param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                    param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, CommonData.StateCode, ParameterDirection.Input);
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

        public DataTable InvoiceDetailFreeItems_Get(Int32 intInvNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
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

        public DataTable InvoiceBltnDetailFreeItems_Get(Int32 intInvNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
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
        
        public Int64 GenerateInvoiceNo(string sCompCode, string sBranchCode)
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            Int64 InvNo = 0;
            try
            {
                string sqlSel = " SELECT isNull(MAX(INVNO),'1')  AS INVNO FROM InvoiceNumberGenerate_View " +
                                " WHERE COMPANY_CODE ='" + sCompCode +
                                "' AND BRANCH_CODE='" + sBranchCode +
                                "' AND FIN_YEAR='" + CommonData.FinancialYear + "'";
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

        public Int64 GenerateTrnNoForStkContra(string sCompCode, string sBranchCode)
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            Int64 InvNo = 0;
            try
            {
                string sqlSel = " SELECT isNull(MAX(GSSC_TRN_NO),'1')+1  AS TRNNO FROM GC_STK_SALES_CONTRA " +
                                " WHERE GSSC_COMP_CODE ='" + sCompCode +
                                "' AND GSSC_BRANCH_CODE='" + sBranchCode +
                                "' AND GSSC_FIN_YEAR='" + CommonData.FinancialYear + "'";
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

        public DataSet SalesOrderForBuiltInAndInvoice_Get(string sOrdNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
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

        /// <summary>
        /// This is Used for Get the Sales Document SR Data based on bellow parameters 
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <param name="BranchCode"></param>
        /// <param name="DocMonth"></param>
        /// <returns></returns>
        public Hashtable GetSalesDmSRData(string CompanyCode, string BranchCode, string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@cmp_cd", DbType.String, CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_month", DbType.String, DocMonth.ToString(), ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SALES_DM_SR_DATA_GET", CommandType.StoredProcedure, param);
                ht.Add("SalseDM", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;
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

        /// <summary>
        /// This is used for Check the order sheets numbers
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <param name="BranchCode"></param>
        /// <param name="DocMonth"></param>
        /// <param name="Ecode"></param>
        /// <returns></returns>
        public DataSet GetDNKEcodeInfo(string CompanyCode, string BranchCode, string DocMonth, int Ecode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@cmp_cd", DbType.String, CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_month", DbType.String, DocMonth.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetOrderIssueECodeInfo", CommandType.StoredProcedure, param);
                ht.Add("ORD", ds.Tables[0]);
                ht = null;
                param = null;
                objSQLdb = null;
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
        public DataSet GetInvocieInfoforService(string BranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetInvoiceInfoforService", CommandType.StoredProcedure, param);
                param = null;
                objSQLdb = null;
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

        //public DataSet InvLevelSREcodeSearchByGL_Get(string sCompCode, string sBranchCode, string sDocMonth, Int32 sGCEcode, string sEcodeName)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[5];
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@sGLCode", DbType.Int32, sGCEcode, ParameterDirection.Input);
        //        param[4] = objSQLdb.CreateParameter("@sECodeName", DbType.String, sEcodeName, ParameterDirection.Input);
        //        ds = objSQLdb.ExecuteDataSet("InvLevelSREcodeSearchByGL_Get", CommandType.StoredProcedure, param);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        param = null;
        //        objSQLdb = null;
        //    }
        //    return ds;
        //}

        //public Hashtable SRSummaryBulletin_Get(string sCompCode, string sBranchCode, string sDocMonth, string sSREcode)
        //{


        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[4];
        //    DataSet ds = new DataSet();
        //    Hashtable ht = new Hashtable();
        //    try
        //    {
        //        param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@xEcode", DbType.String, sSREcode, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);                
        //        ds = objSQLdb.ExecuteDataSet("GetSRSummaryBulletinHeadData", CommandType.StoredProcedure, param);
        //        ht.Add("SRSumHead", ds.Tables[0]);                
        //        ds = null;

        //        param = null;
        //        objSQLdb = null;
        //        objSQLdb = new SQLDB();
        //        param = new SqlParameter[4];
        //        ds = new DataSet();
        //        param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@xEcode", DbType.String, sSREcode, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);    
        //        ds = objSQLdb.ExecuteDataSet("GetSRSummaryBulletinDetlData", CommandType.StoredProcedure, param);
        //        ht.Add("SRSumDetl", ds.Tables[0]);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        param = null;
        //        objSQLdb = null;
        //    }
        //    return ht;
        //}


        public DataSet SRPMDDADemos_Get(string sCompCode, string sBranchCode, string sDocMonth, string sEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);                
                param[3] = objSQLdb.CreateParameter("@sECode", DbType.String, sEcode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSRPMDDADemos", CommandType.StoredProcedure, param);

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

        public DataSet GetVillagesDdlForCustList(string sCompCode, string sBranchCode, string sState
                                                , string sDist, string sVill, string sProdType
                                                , string sProdID, string sFrom, string sTo
                                                , Int32 sFQty, Int32 sTQty, string sType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[12];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xState", DbType.String, sState, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDistrict", DbType.String, sDist, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xVillage", DbType.String, sVill, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xProdType", DbType.String, sProdType, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xProductID", DbType.String, sProdID, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xFrom", DbType.String, sFrom, ParameterDirection.Input);
                param[8] = objSQLdb.CreateParameter("@xTo", DbType.String, sTo, ParameterDirection.Input);
                param[9] = objSQLdb.CreateParameter("@xFromQty", DbType.Int32, sFQty, ParameterDirection.Input);
                param[10] = objSQLdb.CreateParameter("@xToQty", DbType.Int32, sTQty, ParameterDirection.Input);
                param[11] = objSQLdb.CreateParameter("@xType", DbType.String, sType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetVillagesDdlForCustList", CommandType.StoredProcedure, param);

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

        public DataSet GetAdjustingAmount(string sAcountId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xAccountId", DbType.String, sAcountId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetAdjustingAmount", CommandType.StoredProcedure, param);
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


        public DataSet UpdatingOutStandingAmt(string strCode, string strBCode, string strFinYear, string sDocType, int iVcrId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, strCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYr", DbType.String, strFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocType", DbType.String, sDocType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xVouID", DbType.Int32, iVcrId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("FA_OU_VCB_UPDT", CommandType.StoredProcedure, param);
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

        public DataSet BeforeUpdatingOutStandingAmt(string strCode, string strBCode, string strFinYear, string sDocType, int iVcrId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, strCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYr", DbType.String, strFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocType", DbType.String, sDocType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xVouID", DbType.Int32, iVcrId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("OU_VCB_BEFORE_UPDT", CommandType.StoredProcedure, param);
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

        public DataSet GetVoucherListData(string sAcountId, string sFromDate, string sToDate, string sDealer)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sAcountId, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xFromDate", DbType.String, sFromDate, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xToDate", DbType.String, sToDate, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDealerCode", DbType.String, sDealer, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetVoucherData", CommandType.StoredProcedure, param);
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



        public Hashtable GetRecieptVoucherData(string sCompCode, string sBranchCode, string sFinYear, string sDocType, string sVoucherId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocType", DbType.String, sDocType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sVoucherId", DbType.String, sVoucherId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DLReceiptVoucherHead_Get", CommandType.StoredProcedure, param);
                ht.Add("Head", ds.Tables[0]);
                //if (ds.Tables[0].Rows.Count > 0)
                //    sTranNo = ds.Tables[0].Rows[0]["TrnNumber"].ToString();
                //ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[5];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocType", DbType.String, sDocType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sVoucherId", DbType.String, sVoucherId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DLReceiptVoucherBillDetails1_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail1", ds.Tables[0]);

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[5];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocType", DbType.String, sDocType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sVoucherId", DbType.String, sVoucherId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DLReceiptVoucherBillDetails2_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail2", ds.Tables[0]);
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
        public DataSet UserBranchCursor_Get(string sCompCode, string sBranchtType, string sGetType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sUser", DbType.String, CommonData.LogUserId, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("UserBranchCursor_Get", CommandType.StoredProcedure, param);

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
        public DataSet GetFixedAssetsData(string AssetSLNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@AssetSLNo", DbType.String, AssetSLNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("FixedAssetsDetails_Get", CommandType.StoredProcedure, param);

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

        public DataTable GetSalesReceiptDataOfEmp(string sCompCode, string sBCode, string sDocMonth, string Ecode)
        {
            DataTable dt = new DataTable();
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Convert.ToInt32(Ecode), ParameterDirection.Input);


                dt = objSQLdb.ExecuteDataSet("GetSalesReceiptDataOfEmp", CommandType.StoredProcedure, param).Tables[0];

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

        public DataSet GetFixedAssetsConfigurationDetails(string AssetSLNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@AssetSLNo", DbType.String, AssetSLNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_FixedAssetsConfigDetails", CommandType.StoredProcedure, param);

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

        public Hashtable SRSummaryBulletin_Get(string sCompCode, string sBranchCode, string sDocMonth, string sSREcode)
        {


            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xEcode", DbType.String, sSREcode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSRSummaryBulletinHeadData", CommandType.StoredProcedure, param);
                ht.Add("SRSumHead", ds.Tables[0]);
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xEcode", DbType.String, sSREcode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSRSummaryBulletinDetlData", CommandType.StoredProcedure, param);
                ht.Add("SRSumDetl", ds.Tables[0]);

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xEcode", DbType.String, sSREcode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSRSummaryBulletinFreeProducts", CommandType.StoredProcedure, param);
                ht.Add("SRFreeProdDetl", ds.Tables[0]);
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

        public DataSet Get_SalesInvBulletDetails(string OrderNo, string MobileNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xOrderNo", DbType.String, OrderNo, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xMobileNo", DbType.String, MobileNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_SalesInvBulletDetails", CommandType.StoredProcedure, param);

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

        public DataSet Get_SalesDifferenceChecklictForGCInBranches(string sCompany, string sBranch, string sDocMonth,string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, sCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_month", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_DOCMM_SUMM_CHECKLIST", CommandType.StoredProcedure, param);

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


        public DataSet Get_OrderSheetIssueDetails_ByOrderNo(string sCompany, string sBranch, string sDocMonth, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, sCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_month", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_OrderSheetIssueDetails_ByOrderNo", CommandType.StoredProcedure, param);

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

        public Hashtable Get_SpecialApprovalDetails(string strSCode, string strBCode, int nTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xTrnNo", DbType.Int32, nTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_SpecialApprovalHead", CommandType.StoredProcedure, param);
                ht.Add("SPApprovalHead", ds.Tables[0]);

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[3];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xTrnNo", DbType.Int32, nTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_SpecialApprovalDetail", CommandType.StoredProcedure, param);
                ht.Add("SPApprovalDetail", ds.Tables[0]);
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


        public DataSet Get_SalesReportingDevByEcode(Int32 sEcode, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, sEcode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_RepDevforSalesEmp", CommandType.StoredProcedure, param);

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
        public Hashtable GetSalesDmTMAndAboveData(string CompanyCode, string BranchCode, string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@cmp_cd", DbType.String, CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_month", DbType.String, DocMonth.ToString(), ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SALES_TM_AND_ABOVE_DATA_GET", CommandType.StoredProcedure, param);
                ht.Add("SalseDM", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;
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

        public DataSet InvProductSearchCursorForSPInv_Get(string sCompanyCode, string sBranCode, string sStateCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvProductSearchCursor_Get_ByDocMonth", CommandType.StoredProcedure, param);

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

    }
}
