using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SSCRMDB;

namespace SSCRM
{
    public partial class RecruitmentSourceMaster : Form
    {
        SQLDB objDb = null;
        
        public RecruitmentSourceMaster()
        {
            InitializeComponent();
        }
        
        private void HRRecruitmentMaster_Load(object sender, EventArgs e)
        {
            FillRecruitmentSourceNames();
        }
        private void FillRecruitmentSourceNames()
        {
            objDb = new SQLDB();
            try
            {
                DataTable dt = objDb.ExecuteDataSet("SELECT HRSM_RECRUITMENT_SOURCE_CODE,HRSM_RECRUITMENT_SOURCE_NAME FROM HR_RECRUITMENT_SOURCE_MASTER").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";
                    dt.Rows.InsertAt(row, 0);
                    cbSourceName.DataSource = dt;
                    cbSourceName.DisplayMember = "HRSM_RECRUITMENT_SOURCE_NAME";
                    cbSourceName.ValueMember = "HRSM_RECRUITMENT_SOURCE_CODE";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objDb = null;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void chkSourceName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSourceName.Checked == true)
            {
                txtSourceName.Visible = true;
                cbSourceName.Visible = false;
                lstMappedSourceNames.DataSource = null;
                lstMappedSourceNames.Items.Clear();
            }
            else
            {
                txtSourceName.Visible = false;
                cbSourceName.Visible = true;
                lstMappedSourceNames.DataSource = null;
                lstMappedSourceNames.Items.Clear();
                cbSourceName.SelectedIndex = 0;
            }

        }

        private void cbSourceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSourceName.SelectedIndex > 0)
                FillMappedList(cbSourceName.SelectedValue.ToString());
            else
                lstMappedSourceNames.DataSource = null;

        }
        private void FillMappedList(string strSelectedValue)
        {
            objDb = new SQLDB();
           
                try
                {
                    DataTable dt = objDb.ExecuteDataSet("SELECT HRSMD_RECRUITMENT_SOURCE_DETL_CODE,HRSMD_RECRUITMENT_SOURCE_DETL_NAME FROM HR_RECRUITMENT_SOURCE_MASTER_DETL WHERE HRSMD_RECRUITMENT_SOURCE_CODE='" + strSelectedValue + "' ORDER BY HRSMD_RECRUITMENT_SOURCE_DETL_NAME ASC").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lstMappedSourceNames.DataSource = dt;
                        lstMappedSourceNames.DisplayMember = "HRSMD_RECRUITMENT_SOURCE_DETL_NAME";
                        lstMappedSourceNames.ValueMember = "HRSMD_RECRUITMENT_SOURCE_DETL_CODE";
                    }

                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objDb = null;
                }            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            objDb = new SQLDB();
           
            string sHRSourceCode = null;
           
            int iResSource = 0;
            try
            {
                if (chkSourceName.Checked == true)
                {
                    if (txtDetialName.Text != "" && txtSourceName.Text != "")
                    {
                        DataTable dt = objDb.ExecuteDataSet("SELECT HRSM_RECRUITMENT_SOURCE_NAME FROM HR_RECRUITMENT_SOURCE_MASTER WHERE HRSM_RECRUITMENT_SOURCE_NAME='" + txtSourceName.Text + "'").Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            sHRSourceCode = Convert.ToInt32(objDb.ExecuteDataSet("SELECT MAX(CAST(SUBSTRING(HRSM_RECRUITMENT_SOURCE_CODE, 4, 6) AS NUMERIC))+1 FROM HR_RECRUITMENT_SOURCE_MASTER").Tables[0].Rows[0][0]).ToString("000000");
                            iResSource = objDb.ExecuteSaveData("INSERT INTO HR_RECRUITMENT_SOURCE_MASTER(HRSM_RECRUITMENT_SOURCE_CODE,HRSM_RECRUITMENT_SOURCE_NAME) VALUES('SRC" + sHRSourceCode + "','" + txtSourceName.Text + "')");
                            InsertHRDetailName("SRC" + sHRSourceCode);
                            FillRecruitmentSourceNames();
                        }
                        else
                        {
                            sHRSourceCode = objDb.ExecuteDataSet("SELECT HRSM_RECRUITMENT_SOURCE_CODE FROM HR_RECRUITMENT_SOURCE_MASTER WHERE HRSM_RECRUITMENT_SOURCE_NAME='" + txtSourceName.Text + "'").Tables[0].Rows[0][0].ToString();
                            InsertHRDetailName(sHRSourceCode);
                        }
                        FillMappedList("SRC" + sHRSourceCode);
                    }
                }
                else if(chkSourceName.Checked == false && cbSourceName.SelectedIndex > 0)
                {
                    if (txtDetialName.Text != "")
                    {
                        InsertHRDetailName(cbSourceName.SelectedValue.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
            }
        }
        private void InsertHRDetailName(string sSrcCode)
        {
            string strCommand = "SELECT MAX(CAST(SUBSTRING(HRSMD_RECRUITMENT_SOURCE_DETL_CODE, 4, 6) AS NUMERIC))+1 FROM HR_RECRUITMENT_SOURCE_MASTER_DETL";
            string sHRDetailCode = Convert.ToInt32((objDb.ExecuteDataSet(strCommand)).Tables[0].Rows[0][0]).ToString("000000");
            strCommand = "INSERT INTO HR_RECRUITMENT_SOURCE_MASTER_DETL(HRSMD_RECRUITMENT_SOURCE_CODE,HRSMD_RECRUITMENT_SOURCE_DETL_CODE,HRSMD_RECRUITMENT_SOURCE_DETL_NAME) VALUES('" + sSrcCode + "','RSD" + sHRDetailCode + "','" + txtDetialName.Text.ToUpper().Replace(" ", "") + "')";
            int iRes = objDb.ExecuteSaveData(strCommand);
            if (iRes > 0)
            {
                FillMappedList(sSrcCode);
                txtDetialName.Text = string.Empty;
            }
        }
    }
}
