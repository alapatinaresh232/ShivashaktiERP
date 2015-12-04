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
    public class ExcelDB
    {
        private Security objSecurity = null;
        private SQLDB objSQLdb = null;        
        public DataSet GetBranchWiseBulletins(string strComp, string strBranch, string strDocMonth, string strRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, strDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, strRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_REVENUE_SUMMERY", CommandType.StoredProcedure, param);

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

        public DataSet GetSalesRegisterDetails(string strComp, string strBranch, string strDocMonth, string strFromDate, string strToDate, string strRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, strDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFRDT", DbType.String, strFromDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTODT", DbType.String, strToDate, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, strRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_SALES_REG_DETL", CommandType.StoredProcedure, param);

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

        public DataSet GetSalesRegisterDetails1(string strComp, string strBranch, string strLogBranch, string strDocMonth, string strFromDate, string strToDate, string strRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogBranch", DbType.String, strLogBranch, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, strDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xFRDT", DbType.String, strFromDate, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xTODT", DbType.String, strToDate, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, strRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_SALES_REG_DETL1", CommandType.StoredProcedure, param);

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

        public DataSet GetSalesRegisterWithoutCustDetails(string strComp, string strBranch, string strDocMonth, string strFromDate, string strToDate, string strRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, strDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFRDT", DbType.String, strFromDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTODT", DbType.String, strToDate, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, strRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_SALEBULTINS_REG_DETLX", CommandType.StoredProcedure, param);

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

        public DataSet GetSRWiseSalesBulletins(string sDocMonth, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = objSQLdb.CreateParameter("@xCompanyCOde", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
            param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
            param[2] = objSQLdb.CreateParameter("@xdocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
            param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);
            DataSet dtExcel = objSQLdb.ExecuteDataSet("SSCRM_YTD_SALE_BULTIN_PROCESS_NEW", CommandType.StoredProcedure, param);
            param = null;
            objSQLdb = null;
            return dtExcel;
        }

        public DataSet GetSaleBuiltinReport(string sDocMonths)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = objSQLdb.CreateParameter("@xCompanyCOde", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
            param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
            param[2] = objSQLdb.CreateParameter("@xdocMonth", DbType.String, sDocMonths, ParameterDirection.Input);
            DataSet dtExcel = objSQLdb.ExecuteDataSet("SSCRM_YTD_SALE_BULTIN_PROCESS", CommandType.StoredProcedure, param);
            param = null;
            objSQLdb = null;
            return dtExcel;
        }

        public DataSet GetMonthWisePerformance(string sCompany, string sBranch, string DocMonth, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = objSQLdb.CreateParameter("@cmp_cd", DbType.String, sCompany, ParameterDirection.Input);
            param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranch, ParameterDirection.Input);
            param[2] = objSQLdb.CreateParameter("@xDoc_month", DbType.String, DocMonth, ParameterDirection.Input);
            param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);
            DataSet dtExcel = objSQLdb.ExecuteDataSet("SSCRM_REP_SALES_SR_MULTI_DOCMM", CommandType.StoredProcedure, param);
            param = null;
            objSQLdb = null;
            return dtExcel;
        }

        public DataSet GetSalesOrderRegister(string strComp, string strBranch, string strDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@CompanyCode", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@DocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSaleOrderInfo", CommandType.StoredProcedure, param);

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

        public DataSet GetAdvanceRefundRegister(string strComp, string strBranch, string strFrom, string strTo, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom", DbType.String, strFrom, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo", DbType.String, strTo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, strRep, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_ADVANCE_REFUND", CommandType.StoredProcedure, param);

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

        public DataSet GetAttendanceChecklistData(string strWagePeriod, string strCompany)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xWagePeriod", DbType.String, strWagePeriod, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, strCompany, ParameterDirection.Input);                
                ds = objSQLdb.ExecuteDataSet("HR_PAYROLL_MANUAL_ATTD_MTOD_CHECKLIST", CommandType.StoredProcedure, param);

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

        public DataSet GetEmpPerfAgainstAsset(string sComp, string sBranch, string sFYear, string sDocm, 
            string sAType, string sAModel, string sEcode, string sRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[8];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocm, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xAssetType", DbType.String, sAType, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xAssetModel", DbType.String, sAModel, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xEoraCode", DbType.String, sEcode, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRep, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_EMP_PERF_AGNST_ASSET", CommandType.StoredProcedure, param);

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

        public DataSet GetPayRollReportsData(string strWagePeriod, string strCompany, string strEcode, string strReport)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xWagePeriod", DbType.String, strWagePeriod, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xECode", DbType.String, strEcode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xREPORT_TYPE", DbType.String, strReport, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_PAYROLL_PRINT_MONYY", CommandType.StoredProcedure, param);

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

        public DataSet GetPayRollCheckListReportsData(string strWagePeriod, string strCompany, string strEcode, string strReport)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xWagePeriod", DbType.String, strWagePeriod, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xECode", DbType.String, strEcode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xPROCTYPE", DbType.String, strReport, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_PAYROLL_CALC_CHECKLIST", CommandType.StoredProcedure, param);

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

        public DataSet GetLoanRecoveryStatement(string strCompany, string strBranch, string strWagePeriod, string strReport)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xComp", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranch", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xWageMonth", DbType.String, strWagePeriod, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, strReport, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSERP_REP_MONTHLY_LOAN_DEDUCTION_STATEMENT", CommandType.StoredProcedure, param);

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

        public DataSet GetSRWiseCumulativeBulletins(string company, string branches, string documentMonth, string finyear, string sRepType, string sEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, finyear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, documentMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xEcode", DbType.String, sEcode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SRWISE_CUMULATIVE_BLTNS", CommandType.StoredProcedure, param);

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

        public DataSet GetStockPointStockSummary(string sCompany, string sBranch, string sFrom, string sTo, string sRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, sCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRep, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@FromDocMM", DbType.String, sFrom, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@ToDocMM", DbType.String, sTo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("CSP_sscrm_spstkLdgr", CommandType.StoredProcedure, param);

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

        public DataSet GetProductionStockSummary(string sCompany, string sBranch, string sFrom, string sTo, string sRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, sCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRep, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@FromDocMM", DbType.String, sFrom, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@ToDocMM", DbType.String, sTo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("sscrm_pu_stksumm", CommandType.StoredProcedure, param);

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

        public DataSet GetAuditQueryReg(string company, string branches, string finyear, string documentMonth, string strEcodes, string DevTypes, string Dept, string MisCond, string MgntPnt, string PptPnt, string status, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[12];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@cmp_cd", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, finyear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDoc_month", DbType.String, documentMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xAuditEcode", DbType.String, strEcodes, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xDevType", DbType.String, DevTypes, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xDept", DbType.String, Dept, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xIsMisCon", DbType.String, MisCond, ParameterDirection.Input);
                param[8] = objSQLdb.CreateParameter("@xMgntPoint", DbType.String, MgntPnt, ParameterDirection.Input);
                param[9] = objSQLdb.CreateParameter("@xPptPoint", DbType.String, PptPnt, ParameterDirection.Input);
                param[10] = objSQLdb.CreateParameter("@xStatus", DbType.String, status, ParameterDirection.Input);
                param[11] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_AUDIT_QUERY_REG", CommandType.StoredProcedure, param);

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

        public DataSet GetBrPayRollCheckListReportsData(string strWagePeriod, string strCompany, string strBranchCode, string strEcode, string strPayRollType, string strReport)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xWagePeriod", DbType.String, strWagePeriod, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xBranch_cd", DbType.String, strBranchCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xECode", DbType.String, strEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xPayRollType", DbType.String, strPayRollType, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xPROCTYPE", DbType.String, strReport, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_BR_PAYROLL_CALC_CHECKLIST", CommandType.StoredProcedure, param);

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

        public DataSet GetAuditRecDetails(string company, string branches, string finyear, string documentMonth, string Region, string Zone, string sEcode, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[8];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, finyear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, documentMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRegion", DbType.String, Region, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xZone", DbType.String, Zone, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xEcode", DbType.String, sEcode, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_AUDIT_RECOVERY_DETLS", CommandType.StoredProcedure, param);

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

        public DataSet GetAuditDeviationDetails(string company, string branches, string Region, string documentMonth, string DevType, string RepType, string strEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@cmp_cd", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xRegion", DbType.String, Region, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, documentMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xDevType", DbType.String, DevType, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xEcode", DbType.String, strEcode, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_AUDIT_DEVTYPE_CRTAB", CommandType.StoredProcedure, param);

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

        public DataSet GetAllIndiaTopSrDownloadExcel(string strCompany, string strBranchCode, string NoOfRecords, string FromDoc, string ToDoc, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xNoofRecords", DbType.String, NoOfRecords, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFromDocMonth", DbType.String, FromDoc, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xToDocMonth", DbType.String, ToDoc, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_ALL_INDIA_TOP_SRS", CommandType.StoredProcedure, param);

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

        public DataSet GetAllIndiaTopGLSDownloadExcel(string strCompany, string strBranchCode, string NoOfRecords, string FromDoc, string ToDoc, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xNoofRecords", DbType.String, NoOfRecords, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFromDocMonth", DbType.String, FromDoc, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xToDocMonth", DbType.String, ToDoc, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_ALL_INDIA_TOP_GL", CommandType.StoredProcedure, param);

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

        public DataSet GetAllIndiaToppersForAwards(string company, string branches, string Zone, string Region, string Division, Int32 iFGPM, Int32 iTGPM, Int32 iFrmGrps, Int32 iToGrps, Int32 NoOfRec, string FrmDocMonth, string ToDocMonth, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[13];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xZone", DbType.String, Zone, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRegion", DbType.String, Region, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xDivision", DbType.String, Division, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xFromGrp_PerMonth", DbType.Int32, iFGPM, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xToGrp_PerMonth", DbType.Int32, iTGPM, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xFrom_Grps", DbType.Int32, iFrmGrps, ParameterDirection.Input);
                param[8] = objSQLdb.CreateParameter("@xTo_Grps", DbType.Int32, iToGrps, ParameterDirection.Input);
                param[9] = objSQLdb.CreateParameter("@xNoofRecords", DbType.Int32, NoOfRec, ParameterDirection.Input);
                param[10] = objSQLdb.CreateParameter("@xFromDocMonth", DbType.String, FrmDocMonth, ParameterDirection.Input);
                param[11] = objSQLdb.CreateParameter("@xToDocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[12] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSERP_REP_ALL_INDIA_TOPERS_FOR_AWARDS", CommandType.StoredProcedure, param);

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

        public DataSet Get_ServiceReplacementDetails(string strCompany, string strBranchCode, string DocMonth, string sProductId, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xProductId", DbType.String, sProductId, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SERVICES_ACTIVITY_SUMMARY", CommandType.StoredProcedure, param);

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

        public DataSet Get_FieldSupportDetails(string strCompany, string strBranchCode, string FrmDocMonth, string ToDocMonth, string Empcode, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, FrmDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xEcode", DbType.String, Empcode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_FIELD_SUPPORT_DETAILS", CommandType.StoredProcedure, param);

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

        public DataSet Get_SRWiseTopToBottom(string strCompany, string strBranchCode, string DocMonth, string sLogBranCode, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xLog_BranCode", DbType.String, sLogBranCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SR_WISE_TOP_TO_BOTTOM", CommandType.StoredProcedure, param);

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
        public DataSet GetSRWiseCumulativeBulletins2(string company, string branches, string logBranches, string finyear, string documentMonth, string sRepType, string sEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogBranch", DbType.String, logBranches, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFinYear", DbType.String, finyear, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, documentMonth, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xEcode", DbType.String, sEcode, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SRWISE_CUMULATIVE_BLTNS_2", CommandType.StoredProcedure, param);

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

        public DataSet GetTransportCostSummary(string strComp, string strBranch, string strLogBranch, string strFinYear, string strDocMonth, string strRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, strComp, ParameterDirection.Input);
            param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranch, ParameterDirection.Input);
            param[2] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String, strLogBranch, ParameterDirection.Input);
            param[3] = objSQLdb.CreateParameter("@sFinYear", DbType.String, strFinYear, ParameterDirection.Input);
            param[4] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
            param[5] = objSQLdb.CreateParameter("@sRepType", DbType.String, strRepType, ParameterDirection.Input);
            DataSet dtExcel = objSQLdb.ExecuteDataSet("SSCRM_REP_TRANSPORT_COST_SUMMARY", CommandType.StoredProcedure, param);
            param = null;
            objSQLdb = null;
            return dtExcel;
        }

        public DataSet Get_GroupWiseStockReconciliation(string strCompany, string strBranchCode, string DocMonth, Int32 EmpEcode, string sLogBranCode, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_Month", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xECode", DbType.String, EmpEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xLog_Branch", DbType.String, sLogBranCode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_SALE_BULTIN_GCSR_STOCK_2", CommandType.StoredProcedure, param);

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

        public DataSet Get_AuditTourScheduleSummary(string strCompany, string strBranchCode, string DocMonth, string EmpCode, string sFrmDate, string sToDate, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xdocMM", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xAuditorsEcode", DbType.String, EmpCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xFRDT", DbType.String, sFrmDate, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xTODT", DbType.String, sToDate, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY", CommandType.StoredProcedure, param);

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

        public DataSet Get_AuditDRPlanning(string strCompany, string strBranchCode, string DocMonth, string EmpCode, string sFrmDate, string sToDate, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xdocMM", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xAuditorsEcode", DbType.String, EmpCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xFRDT", DbType.String, sFrmDate, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xTODT", DbType.String, sToDate, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_AUDIT_REPORT_PLAN_VS_ACTU", CommandType.StoredProcedure, param);

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

        public DataSet Get_AuditSolvationSummary(string strCompany, string strBranchCode, string Region, string DocMonth, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@cmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xRegion", DbType.String, Region, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_AUDIT_QUERY_SUMMARY", CommandType.StoredProcedure, param);

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

        public DataSet GetBranchOrderSheetReconsilation2(string company, string branches, string logBranches, string finyear, string documentMonth, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@COMPANY_CODE", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BRANCH_CODE", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@LogBranchCode", DbType.String, logBranches, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@FIN_YEAR", DbType.String, finyear, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@DOCMONTH", DbType.String, documentMonth, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@REPORT_TYPE", DbType.String, sRepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("GetBranchOrderSheetReconsilation2", CommandType.StoredProcedure, param);

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

        public DataSet GetGlWiseBranchPerfStatment(string company, string branches, string logBranches, string documentMonth, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogBranch", DbType.String, logBranches, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, documentMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("NFL_GL_PERFORMANCE_STATEMENT", CommandType.StoredProcedure, param);

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

        public DataSet Get_GCGLWiseSaleBulletins(string strCompany, string strBranchCode, string strLogBranch, string DocMonth, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogBranch", DbType.String, strLogBranch, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("NFL_GL_PERFORMANCE_STATEMENT", CommandType.StoredProcedure, param);

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

        public DataSet GetTMWiseSalesBulletins(string company, string branches, string documentMonth, string sTmCodes, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, documentMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTMECode", DbType.String, sTmCodes, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("NFL_TM_WISE_PERFORMANCE_STATEMENT", CommandType.StoredProcedure, param);

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

        public DataSet GetSalesOrderRegisterNew(string strCompany, string strBranchCode, string logBranch, string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@CompanyCode", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogBranch", DbType.String, logBranch, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@DocMonth", DbType.String, DocMonth, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("GetSaleOrderInfo2", CommandType.StoredProcedure, param);

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

        public DataSet Get_AOWiseReconciliation(string strCompany, string strBranchCode, string frmDocMonth, string ToDocMonth, string strEcode, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, frmDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xEcode", DbType.String, strEcode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_AO_WISE_RECONCILIATION_STMT", CommandType.StoredProcedure, param);

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

        public DataSet Get_DocClearenceCertificate(string strCompany, string strBranchCode, string DocMonth, string strEcode, string sLogBranCode, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_Month", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xECode", DbType.String, strEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xLog_Branch", DbType.String, sLogBranCode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_SALE_BULTIN_GCSR_ACCT_1", CommandType.StoredProcedure, param);

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

        public DataSet GetGlWiseDocSheetDetails(string company, string branches, string logBranches, string documentMonth, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogBranch", DbType.String, logBranches, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, documentMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_GL_WISE_DOC_SHEET", CommandType.StoredProcedure, param);

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

        public DataSet GetAdvanceRefundRegisterLOgBranchWise(string strComp, string strBranch, string strLogBranch, string strFrom, string strTo, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String, strLogBranch, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFrom", DbType.String, strFrom, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTo", DbType.String, strTo, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, strRep, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_ADVANCE_REFUND_LOGICALBRANCH", CommandType.StoredProcedure, param);

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
        public DataSet Get_NewSRJoinings(string strComp, string strBranch, string sFromDate, string sToDate, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, sFromDate, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xToDOC_MONTH", DbType.String, sToDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, strRep, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_NEW_JOINS", CommandType.StoredProcedure, param);

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

        public DataSet Get_WorkingSalesEmployees(string strComp, string strBranch, string sLogBranch, string sDocMonth, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogicalBranchCode", DbType.String, sLogBranch, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, sDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, strRep, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_STAFF_DETAILS1", CommandType.StoredProcedure, param);

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

        public DataSet StaffRecruitmentAnalasis(string company, string branches, string finyear, string documentMonth, string sRepType, string sSuccpts)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, finyear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, documentMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, sRepType, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xSuccPTs", DbType.String, sSuccpts, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSERP_REP_RECRUITMTENT_ANALYSIS", CommandType.StoredProcedure, param);

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
        public DataSet BRWiseStockReconsilation(string company, string branches, string documentMonth, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, documentMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@RepType", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_SP_STOCK_RECONSILATION", CommandType.StoredProcedure, param);

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
        public DataSet StateWiseStockReconsilation(string company, string branches, string documentMonth, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDOC_MONTH", DbType.String, documentMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("MIS_SP_STOCK_PROCESS", CommandType.StoredProcedure, param);

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

        public DataSet Get_LowPerformersList(string strComp, string strBranch, int Ecode, string FrmDocMonth, string ToDocMonth, string MnthFlag,
               int frmMnths, int ToMnths, string GrpsFlag, int FrmGrps, int ToGrps, string PointsFlag, int FrmPnts, int ToPnts, string PntsPerGrp,
               int FrmPntsPerGrp, int ToPntsPerGrp, string PntsPerHead, int FrmPntsPerHead, int ToPntsPerHead, string sortBy, string strRep,
               string sPntsGHFlag, string sLosDate)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[24];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, FrmDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xMonths_Flag", DbType.String, MnthFlag, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xFrom_Mnths", DbType.Int32, frmMnths, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xTo_Mnths", DbType.Int32, ToMnths, ParameterDirection.Input);
                param[8] = objSQLdb.CreateParameter("@xGroups_Flag", DbType.String, GrpsFlag, ParameterDirection.Input);
                param[9] = objSQLdb.CreateParameter("@xFrom_Grps", DbType.Int32, FrmGrps, ParameterDirection.Input);
                param[10] = objSQLdb.CreateParameter("@xTo_Grps", DbType.Int32, ToGrps, ParameterDirection.Input);
                param[11] = objSQLdb.CreateParameter("@xPers_Pnts_Flag", DbType.String, PointsFlag, ParameterDirection.Input);
                param[12] = objSQLdb.CreateParameter("@xFrm_PersPnts", DbType.Int32, FrmPnts, ParameterDirection.Input);
                param[13] = objSQLdb.CreateParameter("@xTo_PersPnts", DbType.Int32, ToPnts, ParameterDirection.Input);
                param[14] = objSQLdb.CreateParameter("@xPnts_PerGrp_Flag", DbType.String, PntsPerGrp, ParameterDirection.Input);
                param[15] = objSQLdb.CreateParameter("@xFrom_Pnts_PerGrp", DbType.Int32, FrmPntsPerGrp, ParameterDirection.Input);
                param[16] = objSQLdb.CreateParameter("@xTo_Pnts_PerGrp", DbType.Int32, ToPntsPerGrp, ParameterDirection.Input);
                param[17] = objSQLdb.CreateParameter("@xPnts_PerHead_Flag", DbType.String, PntsPerHead, ParameterDirection.Input);
                param[18] = objSQLdb.CreateParameter("@xFrom_Pnts_PerHead", DbType.Int32, FrmPntsPerHead, ParameterDirection.Input);
                param[19] = objSQLdb.CreateParameter("@xTo_Pnts_PerHead", DbType.Int32, ToPntsPerHead, ParameterDirection.Input);
                param[20] = objSQLdb.CreateParameter("@xSort_By", DbType.String, sortBy, ParameterDirection.Input);
                param[21] = objSQLdb.CreateParameter("@xRepType", DbType.String, strRep, ParameterDirection.Input);
                param[22] = objSQLdb.CreateParameter("@xPnts_PerHeadGrp_Flag", DbType.String, sPntsGHFlag, ParameterDirection.Input);
                param[23] = objSQLdb.CreateParameter("@xLosAsOnDate", DbType.String, sLosDate, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_LOW_PERFORMERS_LIST", CommandType.StoredProcedure, param);

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


        public DataSet Get_Sp_TransportCost_Summary(string strComp, string strBranch, string FromDocMonth, string ToDocMonth, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, FromDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, strRep, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SP_TRANSPORT_COST_MONTH_WISE", CommandType.StoredProcedure, param);

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

        public DataSet Get_SP_Stock_Transactions_Sum(string strComp, string strBranch, string FromDocMonth, string ToDocMonth, string sTrnTypes, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();

            try
            {
                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, FromDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTran_Type", DbType.String, sTrnTypes, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, strRep, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SP_STOCK_TRANSACTIONS_SUMMARY", CommandType.StoredProcedure, param);

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

        public DataSet Get_Sp_Trip_Wise_TransportCost(string strComp, string strBranch, string FromDocMonth, string ToDocMonth, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, FromDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, strRep, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SP_TRANSPORT_COST_TRIP_WISE", CommandType.StoredProcedure, param);

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
        public DataSet GetStmtofFieldSupportAndDeviations(string company, string branches, string FrmDocMonth, string ToDocMonth, Int32 iFGPM, Int32 iTGPM, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranch", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFromDocMonth", DbType.String, FrmDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xToDocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xGroupFrom", DbType.Int32, iFGPM, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xGroupTo", DbType.Int32, iTGPM, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xReportType", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSERP_REP_FieldSupport_Deviations", CommandType.StoredProcedure, param);

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

        public DataSet Get_Recruitment_vs_RetainedSRs(string strComp, string strBranch, string FromDocMonth, string ToDocMonth, Int32 frmGrps, Int32 ToGrps, Int32 nPoints, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[8];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xComp_Code", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, FromDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xFrom_Grps", DbType.Int32, frmGrps, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xTo_Grps", DbType.Int32, ToGrps, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xPoints", DbType.Int32, nPoints, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, strRep, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_RECRUITMENT_VS_RETAINED_SRs", CommandType.StoredProcedure, param);

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

        public DataSet Get_GroupsSalesAnalysation(string strComp, string strBranch, string FromDocMonth, string ToDocMonth, string sGrpRange, Int32 frmGrps, Int32 ToGrps, string strRep)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[8];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xComp_Code", DbType.String, strComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFrom_DocMonth", DbType.String, FromDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xTo_DocMonth", DbType.String, ToDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xGrp_Range", DbType.String, sGrpRange, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xFrom_Grps", DbType.Int32, frmGrps, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xTo_Grps", DbType.Int32, ToGrps, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, strRep, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_GROUPS_SALES_ANALYSATION", CommandType.StoredProcedure, param);

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
        public DataSet GetBranchMaster(string company, string branches, string branchType, string sStatus)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, company, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, branches, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xBranchType", DbType.String, branchType, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xREP_TYPE", DbType.String, sStatus, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSERP_REP_BRANCH_MASTER", CommandType.StoredProcedure, param);

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
        public DataSet Get_SalesInvoiceDetails(string strCompany, string strBranchCode, string strLogBranch, string FRDocMonth, string TODocMonth, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xLogBranch", DbType.String, strLogBranch, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xfrmDocMonth", DbType.String, FRDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xToDocMonth", DbType.String, FRDocMonth, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_INVOICE_WISE_DOC_SHEET", CommandType.StoredProcedure, param);

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

        public DataSet Get_EmpDailyAttendance(string strCompany, string strBranchCode, string sEcode, string FromDate, string ToDate, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[8];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDeptCode", DbType.String, 0, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xECode", DbType.String, sEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xWagePeriod", DbType.String, "", ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xFrom", DbType.String, FromDate, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xToday", DbType.String, ToDate, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xPROCTYPE", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("HR_BR_PAYROLL_ATTD_MTOD_PROCESS", CommandType.StoredProcedure, param);

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
        public DataSet Get_EmpLeaveReconciliationDetl(string strCompany, string strBranchCode, Int32 nYear, string strDept, string strEcode, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xYear", DbType.Int32, nYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDepartment", DbType.String, strDept, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xEcode", DbType.String, strEcode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("SSERP_REP_LEAVE_RECONSILATION", CommandType.StoredProcedure, param);

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
        public DataSet GetDlCollReg(string strComp, string strBranch, string strDocMonth, string FromDate, string ToDate, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                //param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strComp, ParameterDirection.Input);
                //param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
                param[0] = objSQLdb.CreateParameter("@xFromDate", DbType.String, strDocMonth, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xToDate", DbType.String, FromDate, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xRegType", DbType.String, ToDate, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SATL_CASHBANK_REG", CommandType.StoredProcedure, param);

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

        public DataSet Get_AuditPhyStkCntDetl(string strCompany, string strBranchCode, string sFrmDate, string sToDate, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xComp_Code", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranch_Code", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFromDate", DbType.String, sFrmDate, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xToDate", DbType.String, sToDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_AUDIT_PHY_STK_CNT_DETAILS", CommandType.StoredProcedure, param);

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

        public DataSet Get_SalesEmpList(string strCompany, string strBranchCode, string DocMonth, Int32 FrmGrps, Int32 ToGrps, Int32 FrmDesgId, Int32 ToDesgId, string sLosDate, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[9];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, strCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDoc_Month", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFrom_Grps", DbType.Int32, FrmGrps, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTo_Grps", DbType.Int32, ToGrps, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xFrom_DesgId", DbType.Int32, FrmDesgId, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xTo_DesgId", DbType.Int32, ToDesgId, ParameterDirection.Input);
                param[7] = objSQLdb.CreateParameter("@xLOsAsOnDate", DbType.String, sLosDate, ParameterDirection.Input);
                param[8] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSERP_REP_SALES_EMP_LIST_MONTH_WISE", CommandType.StoredProcedure, param);

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
