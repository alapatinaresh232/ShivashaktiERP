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
    public class AuditDB
    {
        private SQLDB objSQLdb = null;

        public DataTable dtBranchType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("xx", "Select Banch Type");
            table.Rows.Add("PU", "PRODUCTION UNIT");           
            table.Rows.Add("BR", "BRANCH");
            table.Rows.Add("SP", "STOCK POINT");
            table.Rows.Add("TR", "TRANSPORT");
            table.Rows.Add("JN", "JOURNEY");
            table.Rows.Add("LV", "LEAVE");
            table.Rows.Add("CO", "CORPORATE OFFICE");
            table.Rows.Add("OL", "OUTLET");
            table.Rows.Add("SD", "SUNDAY");
            table.Rows.Add("HD", "HOLIDAY");


            return table;
        }




        public DataSet GetAuditEcodes(Int32 nDesgId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@nDesgLvlId", DbType.Int32, nDesgId, ParameterDirection.Input);
               
                ds = objSQLdb.ExecuteDataSet("AuditEcodesForMapping_Get", CommandType.StoredProcedure, param);

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


        public DataSet LevelGroupAuditEcodes_Get(Int32 nDestEcode,string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {                
                param[0] = objSQLdb.CreateParameter("@nDestEcode", DbType.Int32, nDestEcode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("LevelGroupAuditEcodes_Get", CommandType.StoredProcedure, param);

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

        public DataSet GetAuditMaporUnmappedEcodes(string DocMonth,char cMapped)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@cMapped", DbType.String, cMapped, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_AuditMaporUnmappedEcodes", CommandType.StoredProcedure, param);
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

        public DataSet GetAuditBranchRegions(string CompCode,string BrType,string Zone,string Region,string sUserId,string sType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranch", DbType.String, BrType, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sZone", DbType.String, Zone, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sRegion", DbType.String, Region, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sUserId", DbType.String, sUserId, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@sType", DbType.String, sType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_AuditBranchRegions", CommandType.StoredProcedure, param);
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
