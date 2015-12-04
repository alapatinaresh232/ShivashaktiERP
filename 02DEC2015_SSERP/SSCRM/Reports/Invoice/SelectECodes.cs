using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class SelectECodes : Form
    {
        private SQLDB objSQLDB = null;
        private ReportViewer childReportViewer = null;
        public int iForm = 0;
        //private string strEcodes = "";
        public SelectECodes()
        {
            InitializeComponent();
        }
        public SelectECodes(int iform)
        {
            InitializeComponent();
            iForm = iform;
        }

        private void SelectECodes_Load(object sender, EventArgs e)
        {
            if (iForm == 1)
            {
                this.Text = "Digital ID Cards";
            }
            else
            {
                this.Text = "Normal/Branch ID Cards";
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
            {
                GetEmpData();
            }
            else
            {
                txtEname.Text = "";
            }
        }

        private void GetEmpData()
        {
            objSQLDB = new SQLDB();
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
            {
                DataTable dt = objSQLDB.ExecuteDataSet("SELECT ISNULL(HAAM_EMP_CODE,ECODE) ECODE,CAST(ISNULL(HAAM_EMP_CODE,ECODE) AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' AS ENAME " +
                                                       "FROM EORA_MASTER LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = ECODE " +
                                                       "WHERE ECODE=" + txtEcodeSearch.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtEname.Text = dt.Rows[0]["ENAME"].ToString();
                else
                    txtEname.Text = "";
            }
            else
                txtEname.Text = "";
            objSQLDB = null;
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnEcodeAdd_Click(object sender, EventArgs e)
        {
            if (txtEname.Text != "")
            {
                string[] strName = txtEname.Text.ToString().Split('-');
                for (int i = 0; i < clbGLList.Items.Count; i++)
                {
                    if (((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString() == strName[0])
                    {
                        MessageBox.Show(strName[0] + "/" + txtEcodeSearch.Text + " Already Exist", "SSCRM-IDPrint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEcodeSearch.Text = "";
                        txtEname.Text = "";
                        return;
                    }
                }
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = strName[0];
                oclBox.Text = txtEname.Text;
                clbGLList.Items.Add(oclBox);
                for (int i = 0; i < clbGLList.Items.Count; i++)
                {
                    if (((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString() == strName[0])
                    {
                        clbGLList.SetItemChecked(i, true);
                    }
                }
                //clbGLList.SetItemChecked(clbGLList.Items.Count - 1, true);
                oclBox = null;
                txtEcodeSearch.Text = "";
                txtEname.Text = "";
            }
            else
            {
                MessageBox.Show("Enter Valid Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clbGLList.Items.Clear();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (iForm == 1)
            {
                if (clbGLList.Items.Count > 0)
                {
                    string strEcodes = "";
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            if (strEcodes != "")
                                strEcodes += ",";
                            strEcodes += ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString();
                        }
                    }
                    if (strEcodes.Length > 0)
                    {
                        childReportViewer = new ReportViewer(strEcodes);
                        CommonData.ViewReport = "EMPLOYEE ID-CARDS_DIGITAL";
                        childReportViewer.Show();
                    }
                }
                else
                    MessageBox.Show("Please Select Atleast One Ecode", "SSCRM-IDPrint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (clbGLList.Items.Count > 0)
                {
                    string strEcodes = "";
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            if (strEcodes != "")
                                strEcodes += ",";
                            strEcodes += ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString();
                        }
                    }
                    if (strEcodes.Length > 0)
                    {
                        childReportViewer = new ReportViewer(strEcodes);
                        CommonData.ViewReport = "EMPLOYEE ID-CARDS";
                        childReportViewer.Show();
                    }
                }
                else
                    MessageBox.Show("Please Select Atleast One Ecode", "SSCRM-IDPrint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
