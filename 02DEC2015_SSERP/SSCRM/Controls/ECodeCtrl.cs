using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class ECodeCtrl : UserControl
    {
        SQLDB objSQLDB;
        public ECodeCtrl()
        {
            InitializeComponent();
        }

        private void txtECode_TextChanged(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            if (txtECode.Text.Length > 4)
            {
                DataTable dt = objSQLDB.ExecuteDataSet("SELECT MEMBER_NAME+' ('+DESIG+')' AS DATA FROM EORA_MASTER WHERE ECODE=" + txtECode.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtEName.Text = dt.Rows[0][0].ToString();
                else
                    txtEName.Text = "";
            }
            else
                txtEName.Text = "";
            objSQLDB = null;
        }
        public string ECode
        {
            get { return txtECode.Text; }
            set { txtECode.Text = value; }
        }
        public string EName
        {
            get { return txtEName.Text; }
            set { txtEName.Text = value; }
        }

        private void txtECode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtECode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLDB = new SQLDB();
            if (txtECode.Text.Length > 4)
            {
                DataTable dt = objSQLDB.ExecuteDataSet("SELECT MEMBER_NAME+' ('+DESIG+')' AS DATA FROM EORA_MASTER WHERE ECODE=" + txtECode.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtEName.Text = dt.Rows[0][0].ToString();
                else
                    txtEName.Text = "";
            }
            else
                txtEName.Text = "";
            objSQLDB = null;
        }
    }
}
