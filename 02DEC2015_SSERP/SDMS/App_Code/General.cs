using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SDMS
{
    public class General
    {
        public General()
        {
        }

        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;

            return DateTime.TryParse(txtDate, out tempDate) ? true : false;
        }

        public int GetComboBoxSelectedIndex(string strValue, ComboBox cbBox)
        {
            int iDx = -1;
            for (int i = 0; i < cbBox.Items.Count; i++)
            {
                if (((SDMS.ComboboxItem)(cbBox.Items[i])).Value.ToString() == strValue)
                {
                    iDx = i;
                    break;
                }
            }
            return iDx;
        }
        public void loadFlash(Form frmFlash)
        {
            if (frmFlash == null || frmFlash.Text == "")
            {
                frmFlash.Show();
            }
        }
        public void UnLoadFlash(Form frmFlash)
        {
           frmFlash.Close();
        }
    }
    public static class crReportParams
    {
        static string _strFromDate = string.Empty;
        static string _strToDate = string.Empty;

        static crReportParams()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public static string FromDate
        {
            get
            {
                return _strFromDate;
            }
            set
            {
                _strFromDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ToDate
        {
            get
            {
                return _strToDate;
            }
            set
            {
                _strToDate = value;
            }
        }
     
    }



    public sealed class WaitCursor : IDisposable
    {
        private Cursor _SavedCursor;
        public WaitCursor()
        {
            _SavedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }
        public void Dispose()
        {
            Cursor.Current = _SavedCursor;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
