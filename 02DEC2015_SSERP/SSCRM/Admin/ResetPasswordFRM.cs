using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class ResetPasswordFRM : Form
    {
        private SQLDB objData = null;
        private Security objSec = null;
        public ResetPasswordFRM()
        {
            InitializeComponent();
        }

        private void ResetPasswordFRM_Load(object sender, EventArgs e)
        {
           // this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 120, Screen.PrimaryScreen.WorkingArea.Y + 130);
            //this.StartPosition = FormStartPosition.CenterScreen;
            lblUserId.Text = CommonData.LogUserId;
        }
        
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objSec = new Security();
            try
            {
                if (CheckData())
                {
                    strSQl = "UPDATE USER_MASTER SET UM_PASSWORD='" + objSec.GetEncodeString(txtPassword.Text) +
                             "' WHERE UM_USER_ID = '" + CommonData.LogUserId +
                             "' ";

                    int RecCnt = objData.ExecuteSaveData(strSQl);

                    if (RecCnt > 0)
                    {
                        MessageBox.Show("Password changed", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            objData = null;
            objSec = null;
        }
        private bool CheckData()
        {
            bool blValue = true;
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Enter Password.", "Reset password", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtPassword.Focus();
                return blValue;

            }
            if (txtConfirmPWD.Text == "")
            {
                MessageBox.Show("Enter Confirm Password.", "Reset password", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtConfirmPWD.Focus();
                return blValue;

            }
            if (txtPassword.Text!= txtConfirmPWD.Text)
            {
                MessageBox.Show("Password not matched.", "Reset password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                blValue = false;
                txtPassword.Focus();
                return blValue;

            }
            return blValue;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;
          
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtConfirmPWD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }
    }
}
