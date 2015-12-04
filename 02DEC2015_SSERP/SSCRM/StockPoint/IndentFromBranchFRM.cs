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
using SSTrans;
using SSAdmin;
using SSCRMDB;
namespace SSCRM
{
    public partial class IndentFromBranchFRM : Form
    {
        private IndentDB objData = null;
      
        public IndentFromBranchFRM()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void StockIndentFRM_Load(object sender, EventArgs e)
        {
            FillBranchData();
            FillBranchGroupData();
            cbGroup.SelectedIndex = 0;
            cbBranches.SelectedIndex = 0;
        }

        private void FillIndentList()
        {
            objData = new IndentDB();
            string strGlCode="0";
            string strStockPointCode=string.Empty;
            try
            {

                gvIndentList.Rows.Clear();
                if (cbGroup.SelectedIndex > -1)
                {
                    string[] strArrGlCode = ((NewCheckboxListItem)(cbGroup.SelectedItem)).Tag.ToString().Split('~');
                    strGlCode = strArrGlCode[0];
                }
                if (cbBranches.SelectedIndex > -1)
                    strStockPointCode = ((NewCheckboxListItem)(cbBranches.SelectedItem)).Tag.ToString();
                if (strGlCode != "" && strStockPointCode.Length >= 1)
                {
                    DataTable dt = objData.IndentFromBrnchList_Get(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToInt32(strGlCode), strStockPointCode).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                        {

                            gvIndentList.Rows.Add();
                            gvIndentList.Rows[intRow].Cells["SLNOList"].Value = intRow + 1;
                            gvIndentList.Rows[intRow].Cells["IndentNo"].Value = dt.Rows[intRow]["indent_number"].ToString();
                            gvIndentList.Rows[intRow].Cells["IndentDate"].Value = Convert.ToDateTime(dt.Rows[intRow]["indent_date"]).ToString("dd/MM/yyyy");
                            gvIndentList.Rows[intRow].Cells["GLName"].Value = dt.Rows[intRow]["GLECODE"].ToString();
                            gvIndentList.Rows[intRow].Cells["IndentFrom"].Value = dt.Rows[intRow]["IndentFrom"].ToString();
                            gvIndentList.Rows[intRow].Cells["TotalProducts"].Value = dt.Rows[intRow]["TotalProducts"].ToString();
                            gvIndentList.Rows[intRow].Cells["TotalReqQty"].Value = dt.Rows[intRow]["TotalReqQty"].ToString();
                            gvIndentList.Rows[intRow].Cells["IndentFromCode"].Value = dt.Rows[intRow]["IndentFromCode"].ToString();
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGroup.SelectedIndex > -1)
            {
                string[] strArrGlCode = ((NewCheckboxListItem)(cbGroup.SelectedItem)).Tag.ToString().Split('~');
                if(cbGroup.SelectedIndex >0)
                    lblGroupEcode.Text = strArrGlCode[1].ToString();
                FillIndentList();
            }
        }
        private void FillBranchGroupData()
        {
            IndentDB objIndent = new IndentDB();
            string strStockPointCode = string.Empty;
            try
            {
                if (cbBranches.SelectedIndex > -1)
                    strStockPointCode = ((NewCheckboxListItem)(cbBranches.SelectedItem)).Tag.ToString();

                cbGroup.DataSource = null;
                cbGroup.Items.Clear();
                DataTable dtGroup = objIndent.IndentGroupList_Get(CommonData.CompanyCode.ToString(), strStockPointCode, CommonData.DocMonth).Tables[0];
                DataRow dr = dtGroup.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtGroup.Rows.InsertAt(dr, 0);
                dr = null;
                if (dtGroup.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dtGroup.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ENAME"].ToString();
                        oclBox.Text = dataRow["GroupName"].ToString();
                        cbGroup.Items.Add(oclBox);
                        oclBox = null;
                    }

                }
                dtGroup = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objIndent = null;
            }

        }

        private void FillBranchData()
        {
            IndentDB objIndent = new IndentDB();
            try
            {
                DataTable dtBranch = objIndent.IndentStockPointList_Get(CommonData.CompanyCode.ToString()).Tables[0];
                DataRow dr = dtBranch.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtBranch.Rows.InsertAt(dr, 0);
                dr = null;
                if (dtBranch.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtBranch.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["branch_code"].ToString();
                        oclBox.Text = dataRow["branch_name"].ToString();
                        cbBranches.Items.Add(oclBox);
                        oclBox = null;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objIndent = null;
            }

        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > -1)
            {
                FillBranchGroupData();
                FillIndentList();
            }
        }
   }
}
