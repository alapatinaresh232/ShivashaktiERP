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
using System.Net.Mail;
using System.Net;

namespace SSAdmin
{
    public class UtilityDB
    {

        private SQLDB objData = null;

        #region Constructor
        public UtilityDB()
        {

        }
        #endregion

        #region To check new tran number
        public void SetInvoiceDataExistedForPostMonth()
        {
            objData = new SQLDB();
            string strSQL = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                strSQL = " select count(SIBH_INVOICE_NUMBER) as RowCnt from SALES_INV_BULTIN_HEAD " +
                    " where SIBH_COMPANY_CODE='" + CommonData.CompanyCode +
                    "' AND SIBH_BRANCH_CODE = '" + CommonData.BranchCode +
                    "' AND SIBH_FIN_YEAR = '" + CommonData.FinancialYear +
                    "' AND SIBH_INVOICE_DATE > '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'";


                dt = objData.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows[0][0].ToString() == "0")
                {
                    strSQL = " select count(SIH_INVOICE_NUMBER) as RowCnt from SALES_INV_HEAD " +
                    " where SIH_COMPANY_CODE='" + CommonData.CompanyCode +
                    "' AND SIH_BRANCH_CODE = '" + CommonData.BranchCode +
                    "' AND SIH_FIN_YEAR = '" + CommonData.FinancialYear +
                    "' AND SIH_INVOICE_DATE > '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'";

                    dt = objData.ExecuteDataSet(strSQL).Tables[0];
                    if (dt.Rows[0][0].ToString() != "0")
                        CommonData.IsExistedPostMonthData = true;
                }
                else
                    CommonData.IsExistedPostMonthData = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {

                dt = null;
                objData = null;
            }

        }
        #endregion

        #region Branch Type Table
        public DataTable dtBranchType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("xx", "Select Banch Type");
            table.Rows.Add("PU", "PRODUCTION UNIT");
            table.Rows.Add("HO", "HEAD OFFICE");
            table.Rows.Add("BR", "BRANCH");
            table.Rows.Add("SP", "STOCK POINT");
            table.Rows.Add("TR", "TRANSPORT");
            table.Rows.Add("OL", "OUTLET");

            return table;
        }
        #endregion

        #region SP DC transaction type
        public DataTable dtDCTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("SP2BR", "SP2BR");
            table.Rows.Add("SP2SP", "SP2SP");
            table.Rows.Add("SP2PU", "SP2PU");
            table.Rows.Add("SP2OL", "SP2OL");
           
           
            return table;
        }
        #endregion

        #region SP Internal Conversion Transaction Type
        public DataTable dtSPIntConvTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("GOOD TO DAMAGE", "G2D");
            table.Rows.Add("DAMAGE TO GOOD", "D2G");

            return table;
        }
        #endregion

        #region SPGRN transaction type
        public DataTable dtSPGRNTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

             table.Rows.Add("BR2SP", "BR2SP");
             table.Rows.Add("PU2SP", "PU2SP");
             table.Rows.Add("SP2SP", "SP2SP");
             //table.Rows.Add("GL2SP", "PU2BR");
             table.Rows.Add("OL2SP", "OL2SP");

            return table;
        }
        #endregion

        #region SPPKMGRN transaction type
        public DataTable dtSPPKMGRNTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            //table.Rows.Add("BR2SP", "BR2SP");
            table.Rows.Add("PU2SP", "PU2SP");
            table.Rows.Add("SP2SP", "SP2SP");
            //table.Rows.Add("GL2SP", "PU2BR");
            //table.Rows.Add("OL2SP", "OL2SP");

            return table;
        }
        #endregion

        #region PU DC transaction type
        public DataTable dtDCPUTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

             table.Rows.Add("PU2SP", "PU2SP");
             table.Rows.Add("PU2PU", "PU2PU");
             table.Rows.Add("PU2TU", "PU2TU");
             table.Rows.Add("PU2BR", "PU2BR");
             //table.Rows.Add("PU2BR", "PU2CU");
             table.Rows.Add("PU2OL", "PU2OL");

            return table;
        }
        #endregion

        #region PU DC transaction type
        public DataTable dtGRNPUTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("SP2PU", "SP2PU");
            table.Rows.Add("PU2PU", "PU2PU");
            table.Rows.Add("TU2PU", "TU2PU");
            table.Rows.Add("BR2PU", "BR2PU");
            //table.Rows.Add("PU2BR", "PU2CU");
            table.Rows.Add("OL2PU", "OL2PU");

            return table;
        }
        #endregion

        #region User role Table
        public DataTable dtUserRole()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("ENTRY", "ENTRY DATA");
            table.Rows.Add("VIEWD", "VIEW DATA");
            table.Rows.Add("MANAG", "MANAGEMENT");
            table.Rows.Add("ALLPM", "ALL PERMISIONS");
            table.Rows.Add("HRDEO", "HR DATAENTRY OPERATOR");
            table.Rows.Add("HRMGR", "HR MANAGER");
            table.Rows.Add("CORGM", "HR GM");
            table.Rows.Add("BRDEO", "BRANCH DATA ENTRY");
            table.Rows.Add("BRMGR", "BRANCH MANAGER");
            table.Rows.Add("HOMGR", "CORPORATE MANAGER");
            table.Rows.Add("HOMIS", "CORPORATE MIS");
            table.Rows.Add("CORDS", "DIRECTOR SALES");
            table.Rows.Add("CORMD", "MANAGING DIRECTOR");
            table.Rows.Add("HOCMD", "CHAIRMAN");
            table.Rows.Add("SPDEO", "STOCKPOINT DATA ENTRY");
            table.Rows.Add("SPMGR", "STOCKPOINT MANAGER");
            table.Rows.Add("SPMIS", "STOCKPOINT MIS");
            table.Rows.Add("PUDEO", "PRODUCTION DATA ENTRY");
            table.Rows.Add("PUMGR", "PRODUCTION MANAGER");
            table.Rows.Add("PUMIS", "PRODUCTION MIS");
            table.Rows.Add("TRDEO", "TRANSPORT DATA ENTRY");
            table.Rows.Add("TRMGR", "TRANSPORT MANAGER");
            table.Rows.Add("TRMIS", "TRANSPORT MIS");
            return table;
        }
        #endregion

        #region Vehicle types
        public DataTable dtVehicleType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("OWN", "OWN");
            table.Rows.Add("HIRE", "HIRE");
            table.Rows.Add("BYHAND", "BYHAND");
            table.Rows.Add("OTHER", "OTHER");

            return table;
        }
        #endregion

        public DataTable dtUserBranch(string strUserId)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                dt = objData.ExecuteDataSet("Select ub_branch_code+'@'+cm_company_code+'@'+branch_name+'@'+cm_company_name+'@'+state_code+'@'+branch_type branch_code"+
                                                ", branch_name  from user_branch UB inner join branch_mas bm on ub_branch_code=branch_code " +
				                                " inner join COMPANY_MAS on cm_company_code = company_code " +
                                                " where upper(ub_user_id)='" + strUserId + "' Order By branch_name ", CommandType.Text).Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;

            }
            return dt;
        }

        public DataTable UserBranch(string strUserId,string branchtype)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            string sqlText = "";
            if (branchtype == "")
            {
                sqlText = "Select ub_branch_code+'@'+cm_company_code+'@'+branch_name+'@'+cm_company_name+'@'+state_code+'@'+branch_type branch_code, branch_name  from user_branch UB " +
                            "inner join branch_mas bm on ub_branch_code=branch_code " +
                            "inner join COMPANY_MAS on cm_company_code = company_code " +
                            "where upper(ub_user_id)='" + strUserId + "' Order By branch_name";
            }
            else
            {
                sqlText = "Select ub_branch_code+'@'+cm_company_code+'@'+branch_name+'@'+cm_company_name+'@'+state_code+'@'+branch_type branch_code, branch_name  from user_branch UB " +
                            "inner join branch_mas bm on ub_branch_code=branch_code " +
                            "inner join COMPANY_MAS on cm_company_code = company_code " +
                            "where upper(ub_user_id)='" + strUserId + "' and bm.BRANCH_TYPE = '" + branchtype + "' Order By branch_name";
            }
            try
            {

                dt = objData.ExecuteDataSet(sqlText).Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;

            }
            return dt;
        }

        public DataTable dtCompanyDocumentMonth(string strCompCode)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                dt = objData.ExecuteDataSet("SELECT fin_year+'@'+cast(start_date as varchar)+'@'+ cast(end_date as varchar) AS FinYear, document_month AS DocMonth FROM document_month where company_code='" + strCompCode + "' And Status='R' ORDER BY start_date desc", CommandType.Text).Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;

            }
            return dt;
        }

        public DataSet UserBranchCursor_Get(string sUserId,string sCompCode, string sBranchtType, string sGetType)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objData.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sUser", DbType.String, sUserId, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("UserBranchCursor_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;
            }
            return ds;
        }

        #region Get Excel ColumnName With ColumnNo
        public string GetColumnName(int columnNumber)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string columnName = "";

            while (columnNumber > 0)
            {
                columnName = letters[(columnNumber - 1) % 26] + columnName;
                columnNumber = (columnNumber - 1) / 26;
            }

            return columnName;
        }
        #endregion

        //public DataSet GetCompanyDataSet()
        //{
        //    objSQLdb = new SQLDB();
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        ds = objSQLdb.ExecuteDataSet("CompanyMaster_Proc", CommandType.StoredProcedure);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;

        //    }
        //    return ds;
        //}

        //public DataSet UserBranchList(string strCompanCode)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[1];
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, strCompanCode, ParameterDirection.Input);
        //        ds = objSQLdb.ExecuteDataSet("UserBranchMaster_Get", CommandType.StoredProcedure, param);

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

        
        //public bool CanModifyDataUserAsPerBackDays(DateTime dtDate)
        //{
        //    bool blModf = false;
        //    if (Convert.ToInt16(CommonData.LogUserBackDays) >= 0)
        //    {
        //        dtDate = dtDate.AddDays(Convert.ToInt16(CommonData.LogUserBackDays));
        //        if (dtDate >= Convert.ToDateTime(Convert.ToDateTime(CommonData.DocTDate).ToString("dd/MM/yyyy")))
        //            blModf = true;
        //    }
        //    else
        //        blModf = true;

        //    return blModf;
        //}

        //public DataTable dtCompanyDocumentMonth()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    try
        //    {

        //        dt = objSQLdb.ExecuteDataSet("SELECT fin_year+'@'+cast(start_date as varchar)+'@'+ cast(end_date as varchar) AS FinYear, document_month AS DocMonth FROM document_month where company_code='" + CommonData.CompanyCode + "' And Status='R' ORDER BY start_date desc", CommandType.Text).Tables[0];

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;

        //    }
        //    return dt;
        //}

        //public Hashtable GetCompanyDocumentMonth()
        //{
        //    objSQLdb = new SQLDB();
        //    DataSet ds = new DataSet();
        //    Hashtable ht = new Hashtable();
        //    try
        //    {

        //        ds = objSQLdb.ExecuteDataSet("select *, getdate() as CurDate from document_month where company_code='" + CommonData.CompanyCode.ToString() + "' And Status='R'", CommandType.Text);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            ht.Add("DocMonth", ds.Tables[0].Rows[0]["document_month"]);
        //            ht.Add("DocFDate", ds.Tables[0].Rows[0]["start_date"]);
        //            ht.Add("DocTDate", ds.Tables[0].Rows[0]["end_date"]);
        //            ht.Add("FYear", ds.Tables[0].Rows[0]["fin_year"]);
        //            ht.Add("CurDate", ds.Tables[0].Rows[0]["CurDate"]);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        ds = null;

        //    }
        //    return ht;
        //}

        //public DataTable UserLogin(string sUserId, string sPwd)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[2];
        //    string str = string.Empty;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string sEcode = GetEncodeString(sPwd);
        //        //string sDcode = GetDecodeString(sEcode);
        //        param[0] = objSQLdb.CreateParameter("@sUSERID", DbType.String, sUserId, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@sPASSWORD", DbType.String, sEcode, ParameterDirection.Input);
        //        dt = objSQLdb.ExecuteDataSet("GetUserLogin_Proc", CommandType.StoredProcedure, param).Tables[0];

        //        if (dt.Rows.Count > 0)
        //        {
        //            CommonData.CompanyCode = dt.Rows[0]["CompanyCode"].ToString();
        //            CommonData.CompanyName = dt.Rows[0]["CompanyName"].ToString();
        //            CommonData.BranchCode = dt.Rows[0]["BranchCode"].ToString();
        //            CommonData.BranchName = dt.Rows[0]["BranchName"].ToString();
        //            CommonData.StateCode = dt.Rows[0]["StateCode"].ToString();
        //            CommonData.StateName = dt.Rows[0]["StateName"].ToString();
        //            CommonData.BranchType = dt.Rows[0]["BranchType"].ToString();
        //            CommonData.BranchLevelId = Convert.ToInt32(dt.Rows[0]["BranchLevelId"] + "");
        //            CommonData.LogUserId = sUserId;
        //            CommonData.LogUserEcode = dt.Rows[0]["UserEcode"] + "";
        //            CommonData.LogUserRole = dt.Rows[0]["UserRole"] + "";
        //            CommonData.LogUserBackDays = Convert.ToInt16(dt.Rows[0]["UserBackDays"] + "");
        //        }
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
        //    return dt;

        //}

        #region MAIL SENDING
        public string SendMail(string UserID, string Branch, string body)
        {
            String[] addrCC = { "satyaprasad@sivashakthi.net" };
            var fromAddress = new MailAddress("ssbplitho@gmail.com", "CRM FeedBack");
            var toAddress = new MailAddress("nareshit@sivashakthi.net");//nareshit@sivashakthi.net
            const string fromPassword = "ssbplitho5566";
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "CRM Feedback " + UserID + ", Branch Name: " + Branch,
                    Body = "<table width='80%'><tr><td>" + body + "</td></tr><tr><td style='height:50px'></td></tr><tr><td>From </td></tr><tr><td> User ID : " + UserID + "</td></tr><tr><td> Branch Name: " + Branch + "</td></tr></table>",
                    IsBodyHtml = true
                })
                {
                    for (int i = 0; i < addrCC.Length; i++)
                        message.CC.Add(addrCC[i]);
                    smtp.Send(message);
                    return "Yes";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

    }
}
