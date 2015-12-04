using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using SSCRMDB;
namespace SSTrans
{
    public class FixedAssetsDB
    {
        private SQLDB objSQLdb = null;


        public DataSet GetFixedAssetsServiceDetails(string sAssetSLNO)
        {
            DataSet ds = new DataSet();
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            try
            {
                //param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                //param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBCode, ParameterDirection.Input);
                //param[2] = objSQLdb.CreateParameter("@sAssetType", DbType.String, sAssetType, ParameterDirection.Input);
                param[0] = objSQLdb.CreateParameter("@sAssetSLNO", DbType.String, sAssetSLNO, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("Get_FixedAssetsServiceDetails", CommandType.StoredProcedure, param);

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

          public DataSet GetAssetSlNos(string BranchCode, string AssetSlNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xAssetSlNo", DbType.String, AssetSlNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_AssetSLNOs", CommandType.StoredProcedure, param);

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
        public DataSet GetPrinterConfigurationID(string BranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_PrinterConfigurationIDs", CommandType.StoredProcedure, param);

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
        public DataSet GetSystemConfigDtails(string AssetSlNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xAssetSlNo", DbType.String, AssetSlNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_SystemConfigDetails", CommandType.StoredProcedure, param);

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
        public DataSet GetScannerConfigurationID(string BranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ScannerIDs", CommandType.StoredProcedure, param);

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
        public DataSet GetBiometricsConfigurationID(string BranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_BiometricsIDs", CommandType.StoredProcedure, param);

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
        public DataSet GetWebcamConfigurationID(string BranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_WebCamConfigIDs", CommandType.StoredProcedure, param);

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
