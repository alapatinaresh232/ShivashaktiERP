using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SSCRMDB
{
    public static class CommonData
    {
        static string _strCurrentDate = string.Empty;
        static string _strCompanyName = string.Empty;
        static string _strCompanyCode = string.Empty;
        static string _strBranchName = string.Empty;
        static string _strBranchCode = string.Empty;
        static string _strBranchType = string.Empty;
        static string _strStateName = string.Empty;
        static string _strStateCode = string.Empty;
        static string _strFinYear = string.Empty;
        static string _strDocMonth = string.Empty;
        static string _strDocFDate = string.Empty;
        static string _strDocTDate = string.Empty;
        static string _strViewReport = string.Empty;
        static string _strUserId = string.Empty;
        static string _strUserEcode = string.Empty;
        static string _strUserRole = string.Empty;
        static string _strUserName = string.Empty;
        static string _strConnectiontype = string.Empty;
        static Int16 _intUserBackDays = 0;
        static int _intBranchLevelId = 55;

        static bool _blIsExistedPostMonthData = false;

        static CommonData()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsExistedPostMonthData
        {
            get
            {
                return _blIsExistedPostMonthData;
            }
            set
            {
                _blIsExistedPostMonthData = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int BranchLevelId
        {
            get
            {
                return _intBranchLevelId;
            }
            set
            {
                _intBranchLevelId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LogUserId
        {
            get
            {
                return _strUserId;
            }
            set
            {
                _strUserId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LogUserEcode
        {
            get
            {
                return _strUserEcode;
            }
            set
            {
                _strUserEcode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LogUserRole
        {
            get
            {
                return _strUserRole;
            }
            set
            {
                _strUserRole = value;
            }
        }

        public static string LogUserName
        {
            get
            {
                return _strUserName;
            }
            set
            {
                _strUserName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Int16 LogUserBackDays
        {
            get
            {
                return _intUserBackDays;
            }
            set
            {
                _intUserBackDays = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string CurrentDate
        {
            get
            {
                return _strCurrentDate;
            }
            set
            {
                _strCurrentDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string CompanyName
        {
            get
            {
                return _strCompanyName;
            }
            set
            {
                _strCompanyName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string CompanyCode
        {
            get
            {
                return _strCompanyCode;
            }
            set
            {
                _strCompanyCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public static string BranchName
        {
            get
            {
                return _strBranchName;
            }
            set
            {
                _strBranchName = value;
            }
        }

        public static string ConnectionType
        {
            get
            {
                return _strConnectiontype;
            }
            set
            {
                _strConnectiontype = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string BranchCode
        {
            get
            {
                return _strBranchCode;
            }
            set
            {
                _strBranchCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string BranchType
        {
            get
            {
                return _strBranchType;
            }
            set
            {
                _strBranchType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string StateName
        {
            get
            {
                return _strStateName;
            }
            set
            {
                _strStateName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string StateCode
        {
            get
            {
                return _strStateCode;
            }
            set
            {
                _strStateCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string FinancialYear
        {
            get
            {
                return _strFinYear;
            }
            set
            {
                _strFinYear = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string DocMonth
        {
            get
            {
                return _strDocMonth;
            }
            set
            {
                _strDocMonth = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string DocFDate
        {
            get
            {
                return _strDocFDate;
            }
            set
            {
                _strDocFDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string DocTDate
        {
            get
            {
                return _strDocTDate;
            }
            set
            {
                _strDocTDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ViewReport
        {
            get
            {
                return _strViewReport;
            }
            set
            {
                _strViewReport = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SetCompanyDetails(string strCCode, string strCName, string sUserId)
        {
            _strCompanyCode = strCCode;
            _strCompanyName = strCName;
            _strUserId = sUserId;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SetCompanyDocMonthDetails(string sDocMonth
                            , string sDocFDate
                            , string sDocTDate
                            , string sFinYear
                            , string sCurDate)
        {
            _strDocMonth = sDocMonth;
            _strDocFDate = sDocFDate;
            _strDocTDate = sDocTDate;
            _strFinYear = sFinYear;
            _strCurrentDate = sCurDate;
        }
    }
}
