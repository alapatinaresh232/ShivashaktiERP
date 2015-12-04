using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security;
using System.Data.Common;
//using SSCRM;

namespace SSCRMDB
{
    public class SQLDB
    {
        #region Local Variables
        public string strDBConn = string.Empty; 
        private SqlConnection objSQLConn = null;
        private SqlCommand objCommand = null;
        private SqlDataAdapter objSqlDataAdapter = null;
        private Security objSecure = null;
        private ProcessFrm frmProcess = null;
        #endregion

        #region Constructor
        public SQLDB()
        {          
            
        }
        #endregion

        #region Database connection
        private void SQLDBOpen()
        {
            objSecure = new Security();
            try
            {
                if (CommonData.ConnectionType.ToUpper() == "LOCAL")
                    strDBConn = ConfigurationManager.AppSettings["DBConLoc"];
                else
                    strDBConn = ConfigurationManager.AppSettings["DBCon"];

                objSQLConn = new SqlConnection(objSecure.GetDecodeString(strDBConn));

                if (objSQLConn.State == ConnectionState.Closed)
                    objSQLConn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        #endregion

        #region Save Data
        public int ExecuteSaveData(string strDMLString)
        {
            try
            {
            //if (frmProcess == null || frmProcess.Text == "")
            //{
            //    frmProcess = new ProcessFrm();
            //    frmProcess.Show();
            //}

                SQLDBOpen();
                objCommand = new SqlCommand(strDMLString, objSQLConn);
                objCommand.CommandType = CommandType.Text;
                objCommand.Transaction = objSQLConn.BeginTransaction();
                int recordsInserted = objCommand.ExecuteNonQuery();
                objCommand.Transaction.Commit();
                if (recordsInserted.ToString() == "0")
                    return 0;
                else
                    return recordsInserted;
                //frmProcess.Close();
            }
            catch (Exception ex)
            {
                //frmProcess.Close();
                objCommand.Transaction.Rollback();
                throw new Exception(ex.ToString());

            }
            finally
            {
                //frmProcess.Close();
                objCommand.Dispose();
                objSQLConn.Close();
                objSQLConn = null;
            }
        }

        public int ExecuteSaveData(string strDBProgram, params SqlParameter[] ParameterList)
        {
            try
            {

                SQLDBOpen();

                objCommand = new SqlCommand(strDBProgram, objSQLConn);
                objCommand.CommandType = CommandType.StoredProcedure;
                
                if (ParameterList != null)
                    AddParameter(ParameterList);

                objCommand.Transaction = objSQLConn.BeginTransaction();
                int recordsInserted = objCommand.ExecuteNonQuery();
                objCommand.Transaction.Commit();
                return recordsInserted;

            }
            catch (Exception ex)
            {
                objCommand.Transaction.Rollback();
                throw new Exception(ex.ToString());

            }
            finally
            {
                objCommand.Dispose();
                objSQLConn.Close();
                objSQLConn = null;
            }
        }

        public int ExecuteSaveData(CommandType sCommandType, string strDBProgram, params SqlParameter[] ParameterList)
        {
            try
            {

                SQLDBOpen();

                objCommand = new SqlCommand(strDBProgram, objSQLConn);
                objCommand.CommandType = sCommandType;

                if (ParameterList != null)
                    AddParameter(ParameterList);

                objCommand.Transaction = objSQLConn.BeginTransaction();
                int recordsInserted = objCommand.ExecuteNonQuery();
                objCommand.Transaction.Commit();
                return recordsInserted;

            }
            catch (Exception ex)
            {
                objCommand.Transaction.Rollback();
                throw new Exception(ex.ToString());

            }
            finally
            {
                objCommand.Dispose();
                objSQLConn.Close();
                objSQLConn = null;
            }
        }
        #endregion

        #region Creating new sql parameter
        public SqlParameter CreateParameter(string ParameterName, DbType ParmDbType, object ParameterValue, ParameterDirection ParmDir)
        {
            SqlParameter SQLparam = null;
            try
            {
                SQLparam = new SqlParameter();
                SQLparam.ParameterName = ParameterName;
                SQLparam.DbType = ParmDbType;
                SQLparam.Value = ParameterValue;
                SQLparam.Direction = ParmDir;
            }
            catch (Exception ex)
            {
                string sErrMsg = ReportErrorMsg(ex);
                throw new Exception(sErrMsg, ex);
            }
            return SQLparam;
        }
        #endregion

        #region Code for ExecuteDataSet function
        /// <summary>
        /// 
        /// </remarks>
        public DataSet ExecuteDataSet(string CommandText)
        {
            return ExecuteDataSet(CommandText, CommandType.Text);
        }

        /// <summary>
        ///
        /// </summary>
        public DataSet ExecuteDataSet(string CommandText, CommandType CmdType)
        {
            return ExecuteDataSet(CommandText, CmdType, (SqlParameter[])null);
        }
        /// <summary>
        /// Executes select query or Stored procedure It takes Parameters.
        /// </summary>
        public DataSet ExecuteDataSet(string CommandText, CommandType CmdType, params SqlParameter[] ParameterList)
        {
            DataSet ds = new DataSet();
            objCommand = new SqlCommand();
            try
            {
                objCommand.CommandText = CommandText;
                //if (frmProcess == null || frmProcess.Text == "")
                //{
                //    frmProcess = new ProcessFrm();
                //    frmProcess.Show();
                //}
                switch (CmdType)
                {
                    case CommandType.StoredProcedure:
                        objCommand.CommandType = CommandType.StoredProcedure;
                        break;
                    case CommandType.Text:
                        objCommand.CommandType = CommandType.Text;
                        break;
                    case CommandType.TableDirect:
                        objCommand.CommandType = CommandType.TableDirect;
                        break;
                }
                if (ParameterList != null)
                    AddParameter(ParameterList);
                SQLDBOpen();
                objCommand.Connection = objSQLConn;
                objSqlDataAdapter = new SqlDataAdapter(objCommand);
                objSqlDataAdapter.Fill(ds);
                objSQLConn.Close();
                objSqlDataAdapter = null;
            }
            catch (Exception ex)
            {
                //frmProcess.Close();
                throw new Exception(ex.ToString());
            }
            finally
            {
                //frmProcess.Close();
                objSQLConn.Close();
                objSQLConn.Dispose();
                objSQLConn = null;
                objCommand.Parameters.Clear();
            }
            return ds;
        }
        #endregion

        #region Code for AddParameter function
        private SqlParameter AddParameter(params SqlParameter[] param)
        {
            SqlParameter RetParam = null;
            foreach (SqlParameter parms in param)
            {
                objCommand.Parameters.Add(parms);
                if (parms.Direction == ParameterDirection.ReturnValue)
                    RetParam = parms;
            }
            return RetParam;
        }
        #endregion

        #region Report Error
        public static string ReportErrorMsg(Exception ex)
        {
            string errMsg = string.Empty;
            StackFrame s = new StackFrame(1, true);
            string strError = "[Line: " + s.GetFileLineNumber() + "]" + Environment.NewLine
                       + "Method: " + s.GetMethod() + Environment.NewLine
                       + "File Name: " + s.GetFileName() + Environment.NewLine
                       + "Error: " + ex.Message + Environment.NewLine;

            errMsg = strError;
            return errMsg;

        }
        #endregion

        
    }
}
