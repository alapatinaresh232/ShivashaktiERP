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
    public partial class MisconductCategoryMaster : Form
    {
        SQLDB objSQLDb = null;
        bool isModify = false;

        public MisconductCategoryMaster()
        {
            InitializeComponent();
        }


        private void MisconductCategoryMaster_Load(object sender, EventArgs e)
        {
            FillMisconductHeadDetails();
        }


        private void FillMisconductHeadDetails()
        {
            objSQLDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT HRMC_MISCONDUCT_CODE,HRMC_MISCONDUCT_HEAD FROM HR_MISCONDUCT_MAS ";
                dt = objSQLDb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["HRMC_MISCONDUCT_HEAD"] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbSourceName.DataSource = dt;
                    cbSourceName.DisplayMember = "HRMC_MISCONDUCT_HEAD";
                    cbSourceName.ValueMember = "HRMC_MISCONDUCT_CODE";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDb = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void chkMisConHeadName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMisConHeadName.Checked == true)
            {
                txtMisConHeadName.Visible = true;
                cbSourceName.Visible = false;
                lstMappedMisconductDetails.DataSource = null;
                lstMappedMisconductDetails.Items.Clear();
                txtMisConDetailName.Text = string.Empty;
                txtMisConHeadName.Text = string.Empty;
            }
            else
            {
                txtMisConHeadName.Visible = false;
                cbSourceName.Visible = true;
                lstMappedMisconductDetails.DataSource = null;
                lstMappedMisconductDetails.Items.Clear();
                cbSourceName.SelectedIndex = 0;
                txtMisConDetailName.Text = string.Empty;
                txtMisConHeadName.Text = string.Empty;
            }

        }

       

        private void cbSourceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSourceName.SelectedIndex > 0)
            {
                FillMappedList(cbSourceName.SelectedValue.ToString());
            }
            else
            {
                lstMappedMisconductDetails.DataSource = null;
            }
        }

        private void FillMappedList(string strSelectedValue)
        {
            objSQLDb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT HMD_MISCONDUCT_DETL_ID,HMD_MISCONDUCT_DETL_DESC "+
                                " FROM HR_MISCONDUCT_MAS_DETL "+
                                " WHERE HMD_MISCONDUCT_ID='"+ Convert.ToInt32(strSelectedValue) +
                                "' ORDER BY HMD_MISCONDUCT_DETL_DESC ASC";
                dt = objSQLDb.ExecuteDataSet(strCmd).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    lstMappedMisconductDetails.DataSource = dt;
                    lstMappedMisconductDetails.DisplayMember = "HMD_MISCONDUCT_DETL_DESC";
                    lstMappedMisconductDetails.ValueMember = "HMD_MISCONDUCT_DETL_ID";
                }

                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDb = null;
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (chkMisConHeadName.Checked == true)
            {

                if (txtMisConHeadName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Misconduct Head Name", "Misconduct Category Master ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMisConHeadName.Focus();

                }
                else if (txtMisConDetailName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Misconduct Detail Name", "Misconduct Category Master ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMisConDetailName.Focus();

                }
            }
            else
            {
                if (cbSourceName.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Misconduct Head Name", "Misconduct Category Master ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }

            return flag;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            objSQLDb = new SQLDB();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strCommand = "";
            string sHRMisconductCode = null;

            int iResSource = 0;
            if (CheckData() == true)
            {
                try
                {
                    if (chkMisConHeadName.Checked == true)
                    {
                        if (txtMisConDetailName.Text != "" && txtMisConHeadName.Text != "")
                        {
                            strCommand = "SELECT HRMC_MISCONDUCT_HEAD " +



                                               " FROM HR_MISCONDUCT_MAS " +
                                               " WHERE HRMC_MISCONDUCT_HEAD=' " + txtMisConHeadName.Text.ToUpper() + " '";
                            dt = objSQLDb.ExecuteDataSet(strCommand).Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                strCommand = "SELECT ISNULL(MAX(HRMC_MISCONDUCT_CODE),0)+1  FROM HR_MISCONDUCT_MAS";
                                ds = objSQLDb.ExecuteDataSet(strCommand);
                                sHRMisconductCode = Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString();

                                strCommand = "INSERT INTO HR_MISCONDUCT_MAS(HRMC_MISCONDUCT_HEAD)VALUES('" + txtMisConHeadName.Text.ToUpper() + "')";
                                iResSource = objSQLDb.ExecuteSaveData(strCommand);

                                InsertHRDetailName(sHRMisconductCode);
                                FillMisconductHeadDetails();
                            }
                            else
                            {

                                sHRMisconductCode = objSQLDb.ExecuteDataSet("SELECT HRMC_MISCONDUCT_CODE FROM HR_MISCONDUCT_MAS " +
                                                                         " WHERE HRMC_MISCONDUCT_HEAD='" + txtMisConHeadName.Text.ToUpper() +
                                                                         "'").Tables[0].Rows[0][0].ToString();
                                InsertHRDetailName(sHRMisconductCode);
                            }
                            FillMappedList(sHRMisconductCode);
                        }
                    }
                    if (chkMisConHeadName.Checked == false && cbSourceName.SelectedIndex > 0 && isModify == false)
                    {
                        if (txtMisConDetailName.Text != "")
                        {
                            InsertHRDetailName(cbSourceName.SelectedValue.ToString());
                        }
                    }
                    if (isModify == true && chkMisConHeadName.Checked == false)
                    {
                        string strUpdate = "UPDATE HR_MISCONDUCT_MAS_DETL " +
                                           "SET HMD_MISCONDUCT_DETL_DESC='" + txtMisConDetailName.Text.ToUpper() +
                                           "' WHERE HMD_MISCONDUCT_DETL_ID=" + lstMappedMisconductDetails.SelectedValue.ToString() + " ";

                        int iRes = objSQLDb.ExecuteSaveData(strUpdate);
                        isModify = false;
                        FillMappedList(cbSourceName.SelectedValue.ToString());
                        txtMisConDetailName.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLDb = null;
                }
            }

        }

        private void InsertHRDetailName(string sMisconductCode)
        {

            string strCommand = " SELECT ISNULL(MAX(HMD_MISCONDUCT_DETL_ID),0)+1  FROM HR_MISCONDUCT_MAS_DETL ";
                
            string sHRMisconDetailId = Convert.ToInt32(objSQLDb.ExecuteDataSet(strCommand).Tables[0].Rows[0][0]).ToString();
           
            strCommand = "INSERT INTO HR_MISCONDUCT_MAS_DETL(HMD_MISCONDUCT_ID "+
                          ",HMD_MISCONDUCT_DETL_DESC "+
                          ")VALUES(" + sMisconductCode + 
                          ",' " + txtMisConDetailName.Text.ToUpper() + " ')";
              
            int iRes = objSQLDb.ExecuteSaveData(strCommand);
            if (iRes > 0)
            {
                FillMappedList(sMisconductCode);
                txtMisConDetailName.Text = string.Empty;
            }
        }

        private void lstMappedMisconductDetails_DoubleClick(object sender, EventArgs e)
        {
            Object obj = lstMappedMisconductDetails.GetItemText(lstMappedMisconductDetails.SelectedItem);
            txtMisConDetailName.Text = obj.ToString();
            isModify = true;
        }

        private void txtMisConHeadName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

        private void txtMisConDetailName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

       
       
    }
}
