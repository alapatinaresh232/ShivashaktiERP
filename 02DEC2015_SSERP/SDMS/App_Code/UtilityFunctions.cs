using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace SDMS.App_Code
{
    public class UtilityFunctions
    {
        public UtilityFunctions()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string GetguiID()
        {
            string delimStr = "-";
            char[] delimiter = delimStr.ToCharArray();
            string gid = Guid.NewGuid().ToString();
            gid.Replace("-", "");
            return (gid);
        }
        public static string GetMonthName(int iMonth, bool bFullMonthName)
        {
            string m_strMonthName = "";

            if (iMonth > 12 || iMonth <= 0)
            {
                return m_strMonthName = "Invaild Month";
            }

            string[] aFullMonthNames = { "January", "February", "March", "April", "May", "June", "July", "Augest", "September", "October", "November", "December" };
            string[] aShortMonthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            if (bFullMonthName)
            {
                for (int iCount = 1; iCount <= aFullMonthNames.Length; iCount++)
                {
                    if (iCount == iMonth)
                    {
                        m_strMonthName = aFullMonthNames[iMonth - 1];
                        break;
                    }
                }
            }
            if (!bFullMonthName)
            {
                for (int iCount = 1; iCount <= aFullMonthNames.Length; iCount++)
                {
                    if (iCount == iMonth)
                    {
                        m_strMonthName = aShortMonthNames[iMonth - 1];
                        break;
                    }
                }
            }
            return m_strMonthName;
        }
        
        public static bool ValidateDates(DateTime dtStDate, DateTime dtEndDate)
        {
            if (dtStDate > dtEndDate)
                return false;
            else
                return true;
        }

        public static bool IsNumeric(string sValue)
        {

            if (sValue.Trim() == "")
                return false;
            else
                return Regex.Match(sValue, "(-{0,1}[0-9]+){0,1}(\\.{0,1}[0-9]+)").Value == sValue;
        }

        public static bool IsWholeNumeric(string sValue)
        {

            if (sValue.Trim() == "")
                return false;
            else
                return Regex.Match(sValue, "(-{0,1}[0-9]+)").Value == sValue;
        }
        public static bool IsDate(string sValue)
        {
            if (sValue.Trim() == "")
                return false;
            else
                return Information.IsDate(sValue);
        }
        public static bool IsPositiveNumber(string sValue)
        {

            if (IsNumeric(sValue))
                return Convert.ToDouble(sValue) > 0;
            else
                return false;

        }
        public static double ValueOf(string sValue)
        {
            if (IsNumeric(sValue))
                return Convert.ToDouble(sValue);
            else
                return 0;

        }
        public static bool IsValidSSN(string sValue)
        {
            if (sValue.Trim().Length == 0)
                return false;
            else
                return (Regex.Match(sValue, @"^\(\d{3}\)-\d{2}-\d{4}$").Success || Regex.Match(sValue, @"^\d{3}-\d{2}-\d{4}$").Success);
        }
        //---------------------------for decimal-------------------

        public static bool IsDecimalNumeric(string sValue)
        {

            if (sValue.Trim() == "")
                return false;
            else
                return Regex.Match(sValue, "(-{0,1}[0-9]+){0,1}(.{0,1}[0-9]+)").Value == sValue;
        }
        public static bool IsValidEmail(string sValue)
        {
            // Return true if strIn is in valid e-mail format.
            if (sValue.Trim().Length == 0)
                return false;
            else
                return Regex.Match(sValue, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Value == sValue;
        }
       
        public static bool IsValidPhoneNumber(string sValue)
        {
            if (sValue.Trim().Length == 0)
                return false;
            else
                return (Regex.Match(sValue, @"^\(\d{3}\)-\d{3}-\d{4}$").Success || Regex.Match(sValue, @"^\d{3}-\d{3}-\d{4}$").Success);
        }        
        
        //---------------------------------------------------------
        public static string getFinancialYear(DateTime DtSelect)
        {
            int iMonth = DtSelect.Month;
            int presentyear = DtSelect.Year;
            IFormatProvider frmt = new System.Globalization.CultureInfo("en-GB", true);

            if (iMonth <= 4 && DtSelect.Date <= Convert.ToDateTime(("05/04/" + DtSelect.Year), frmt).Date)
            {
                presentyear = presentyear - 1;
            }
            string strCurrentFinancialyear = (presentyear).ToString().Substring(2, 2) + "-" + Convert.ToString(presentyear + 1).Substring(2, 2);
            return strCurrentFinancialyear;
        }
    }
}
