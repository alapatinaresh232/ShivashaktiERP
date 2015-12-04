using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Security;
using System.Collections;
using System.Net;
using System.Xml;
using System.IO;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using DataBoundTreeView;
namespace SSCRM
{
    public partial class BranchAboveLevelMapping : Form
    {
        private SQLDB objDB = null;
        private StaffLevel objStaffLevel = null;
        private UtilityDB objUtil = null;
        DataTable dtEmpList = new DataTable();
        public BranchAboveLevelMapping()
        {
            InitializeComponent();
        }

        private void BranchAboveLevelMapping_Load(object sender, EventArgs e)
        {
            FillDocMonth();
            FillCompList();
            clblinkDest.Enabled = false;
            clblinkSource.Enabled = false;
            //clbdlinkDest.Enabled = false;
            //clbdlinkSource.Enabled = false;
            tbLinkDelink.SelectedIndex = 0;
        }

        private void FillDocMonth()
        {
            objUtil = new UtilityDB();
            DataTable dt = new DataTable();
            try
            {
                cbDocmonth.DataSource = null;
                dt = objUtil.dtCompanyDocumentMonth(CommonData.CompanyCode);
                if (dt.Rows.Count > 0)
                {
                    cbDocmonth.DisplayMember = "DocMonth";
                    //cbDocmonth.ValueMember = "FinYear";
                    cbDocmonth.ValueMember = "DocMonth";
                    cbDocmonth.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                objUtil = null;
            }
        }

        private void FillCompList()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string sqlText = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE CM_COMPANY_CODE IN ('NFL','VNF','NKBPL','SSBPL','SBTLNPL')";
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDB = null;
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tvComp.Nodes.Add(dt.Rows[i]["CM_COMPANY_CODE"].ToString(), dt.Rows[i]["CM_COMPANY_NAME"].ToString());
                }
            }
            dt = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objDB = new SQLDB();
                int iRes=0;
                string sDestCode = "", sSourceCodes = "";
                if (tbLinkDelink.SelectedIndex == 0)
                {
                    sDestCode = ((NewCheckboxListItem)(clblinkDest.Items[0])).Tag.ToString();
                    for (int i = 0; i < clblinkSource.Items.Count; i++)
                    {
                        sSourceCodes += ((NewCheckboxListItem)(clblinkSource.Items[i])).Tag.ToString() + ",";
                    }
                    sSourceCodes = sSourceCodes.TrimEnd(',');
                    try
                    {
                        iRes = objDB.ExecuteSaveData("exec AboveBranchLeaders_Ins '" + CommonData.LogUserId + "','" + cbDocmonth.SelectedValue.ToString() + "', " + sDestCode + ", '" + sSourceCodes + "'");
                        if (iRes > 0)
                        {
                            MessageBox.Show("Data Saved Successfully", "Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clblinkDest.Items.Clear();
                            clblinkSource.Items.Clear();
                            tvComp_AfterCheck(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Data Not Saved", "Mapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objDB = null;
                    }
                }
                else if (tbLinkDelink.SelectedIndex == 1)
                {
                    sDestCode = ((NewCheckboxListItem)(clbdlinkEmplist.SelectedItem)).Tag.ToString();
                    for (int i = 0; i < clbdlinkSource.Items.Count; i++)
                    {
                        if (clbdlinkSource.GetItemCheckState(i) == CheckState.Checked)
                        {
                            sSourceCodes += ((NewCheckboxListItem)(clbdlinkSource.Items[i])).Tag.ToString() + ",";
                        }
                    }
                    sSourceCodes = sSourceCodes.TrimEnd(',');
                    
                    try
                    {
                        iRes = objDB.ExecuteSaveData("exec AboveBranchLeaders_Del '" + cbDocmonth.SelectedValue.ToString() + "', " + sDestCode + ", '" + sSourceCodes + "'");
                        if (iRes > 0)
                        {
                            MessageBox.Show("Dlink Completed Successfully", "Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clbdlinkEmplist.Items.Clear();
                            clbdlinkSource.Items.Clear();
                            tvComp_AfterCheck(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Data Not Saved", "Mapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objDB = null;
                    }
                }
            }
        }

        private bool CheckData()
        {
            if (tbLinkDelink.SelectedIndex == 0)
            {
                if (clblinkDest.Items.Count == 0)
                {
                    MessageBox.Show("Select Destination Person / Leader", "AboveMapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (clblinkSource.Items.Count == 0)
                {
                    MessageBox.Show("Select Source Persons", "AboveMapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                if (clbdlinkEmplist.Items.Count == 0)
                {
                    MessageBox.Show("Select Destination Person / Leader", "AboveMapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (clbdlinkSource.Items.Count == 0)
                {
                    MessageBox.Show("Select Source Persons", "AboveMapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void clbComp_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
            //if (clbComp.SelectedItems.Count != 0)
            //{
           
                //string strTask = "";
                //for (int i = 0; i < clbComp.Items.Count; i++)
                //{
                //    if (clbComp.GetItemCheckState(i) == CheckState.Checked)
                //    {
                //        strTask += ((NewCheckboxListItem)(clbComp.Items[i])).Tag + ",";
                //    }
                //}
                //strTask = strTask.TrimEnd(',');
                //clblinkEmplist.Items.Clear();
                //if (strTask.Length > 0)
                //    FillLinkEmplist(strTask);
            //}
        }

        private void FillLinkEmplist(string sComp)
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            clblinkEmplist.Items.Clear();
            clbdlinkEmplist.Items.Clear();
            try
            {
                string sqlText = "exec AboveBranchLeaders_Qry '" + cbDocmonth.SelectedValue.ToString() + "','" + sComp + "'";
                dtEmpList = objDB.ExecuteDataSet(sqlText).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDB = null;
            }
            if (tbLinkDelink.SelectedIndex == 0)
            {
                if (dtEmpList.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtEmpList.Rows)
                    {
                        if (dataRow["xDest_ecode"] + "" != "")
                        {
                            bool bExist = false;
                            if (clblinkDest.Items.Count > 0)
                            {
                                if (((NewCheckboxListItem)(clblinkDest.Items[0])).Tag.ToString() == dataRow["xDest_ecode"].ToString())
                                {
                                    bExist = true;
                                }
                            }
                            if (bExist != true)
                            {
                                for (int i = 0; i < clblinkSource.Items.Count; i++)
                                {
                                    if (((NewCheckboxListItem)(clblinkSource.Items[i])).Tag.ToString() == dataRow["xDest_ecode"].ToString())
                                    {
                                        bExist = true;
                                    }
                                }
                            }
                            if (bExist == false)
                            {
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["xDest_ecode"].ToString();
                                oclBox.Text = dataRow["xDest_ecode"].ToString() + "-" + dataRow["xDest_eName"].ToString() + "-" + dataRow["xDest_eDesig"].ToString() + "-(" + dataRow["xDest_Nog"].ToString() + ")";
                                clblinkEmplist.Items.Add(oclBox);
                                oclBox = null;
                            }

                        }
                    }
                }
            }
            else if (tbLinkDelink.SelectedIndex == 1)
            {
                foreach (DataRow dataRow in dtEmpList.Rows)
                {
                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = dataRow["xDest_ecode"].ToString();
                    oclBox.Text = dataRow["xDest_ecode"].ToString() + "-" + dataRow["xDest_eName"].ToString() + "-(" + dataRow["xDest_Nog"].ToString() + ")";
                    clbdlinkEmplist.Items.Add(oclBox);
                    oclBox = null;
                }
            }            
            dt = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void clbComp_Click(object sender, EventArgs e)
        {
            
        }

        private void clbComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string strTask = "";
            //for (int i = 0; i < clbComp.Items.Count; i++)
            //{
            //    if (clbComp.GetItemCheckState(i) == CheckState.Checked)
            //    {
            //        strTask += ((NewCheckboxListItem)(clbComp.Items[i])).Tag + ",";
            //    }
            //}
            //strTask = strTask.TrimEnd(',');
            //clblinkEmplist.Items.Clear();
            //if (strTask.Length > 0)
            //    FillLinkEmplist(strTask);
        }

        private void tvComp_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string strTask = "";
            for (int i = 0; i < tvComp.Nodes.Count; i++)
            {
                if (tvComp.Nodes[i].Checked == true)
                {
                    strTask += tvComp.Nodes[i].Name.ToString() + ",";
                }
            }
            strTask = strTask.TrimEnd(',');
            clblinkEmplist.Items.Clear();
            clbdlinkEmplist.Items.Clear();
            if (strTask.Length > 0)
                FillLinkEmplist(strTask);
            
            //clbdlinkSource.Items.Clear();
            //clblinkDest.Items.Clear();
            //clbdlinkSource.Items.Clear();
        }

        private void cbDocmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            clblinkSource.Items.Clear();
            clblinkDest.Items.Clear();
            tvComp_AfterCheck(null, null);            
        }

        private void txtclblinkEmplistSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtclblinkEmplistSearch.Text.ToString(), clblinkEmplist);
        }
        private void SearchEcode(string searchString, System.Windows.Forms.ListBox cbEcodes)
        {
            if (searchString.Trim().Length > 0)
            {
                for (int i = 0; i < cbEcodes.Items.Count; i++)
                {
                    if (cbEcodes.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
                    {
                        if (((System.Data.DataRowView)(cbEcodes.Items[i])).Row.ItemArray[1].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }
                    else  // for checkbox list search
                    {
                        if (cbEcodes.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }

                }
            }
        }

        private void txtclblinkSourceSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtclblinkSourceSearch.Text.ToString(), clblinkSource);
        }

        private void btnAddlinkDest_Click(object sender, EventArgs e)
        {
            clblinkDest.Items.Clear();
            if (clblinkEmplist.SelectedItems.Count == 1)
            {
                clblinkDest.Items.Add(clblinkEmplist.SelectedItem);
                clblinkDest.SetItemCheckState(0, CheckState.Checked);
                tvComp_AfterCheck(null, null);
            }            
            else
                MessageBox.Show("Select One Employee Only to Add", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void clblinkDest_ItemCheck(object sender, ItemCheckEventArgs e)
        {            
            //if (clblinkDest.GetItemCheckState(0) == CheckState.Unchecked)
            //{
            //    clblinkDest.SetItemCheckState(0, CheckState.Checked);
            //}
        }

        private void btnRemlinkDest_Click(object sender, EventArgs e)
        {
            clblinkDest.Items.Clear();
            tvComp_AfterCheck(null, null);
        }

        private void btnAddlinkSrc_Click(object sender, EventArgs e)
        {
            clblinkSource.Items.Clear();
            for (int i = 0; i < clblinkEmplist.Items.Count; i++)
            {
                if (clblinkEmplist.GetItemCheckState(i) == CheckState.Checked)
                {
                    clblinkSource.Items.Add((NewCheckboxListItem)(clblinkEmplist.Items[i]));
                    clblinkSource.SetItemCheckState(clblinkSource.Items.Count-1, CheckState.Checked);
                }
            }
            tvComp_AfterCheck(null, null);
        }

        private void btnRemlinkSrc_Click(object sender, EventArgs e)
        {
            clblinkSource.Items.Clear();
            tvComp_AfterCheck(null, null);
        }

        private void tbLinkDelink_TabIndexChanged(object sender, EventArgs e)
        {
            tvComp_AfterCheck(null, null);
        }

        private void clbdlinkEmplist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < clbdlinkEmplist.Items.Count; i++)
            {
                if (e.Index != i)
                    clbdlinkEmplist.SetItemCheckState(i, CheckState.Unchecked);
            }
            if (e.NewValue == CheckState.Checked)
                FillMappedSourceECodes();
            else
                clbdlinkSource.Items.Clear();
        }

        private void FillMappedSourceECodes()
        {
            DataTable dt = null;
            objStaffLevel = new StaffLevel();
            clbdlinkSource.Items.Clear();
            try
            {
                dt = objStaffLevel.AboveBranchLeaders_InQ_Get(cbDocmonth.SelectedValue.ToString(), ((NewCheckboxListItem)(clbdlinkEmplist.SelectedItem)).Tag.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["xDest_ecode"].ToString();
                        oclBox.Text = dataRow["xDest_ecode"].ToString() + "-" + dataRow["xDest_eName"].ToString() + "-(" + dataRow["xDest_NoG"].ToString() + ")";
                        clbdlinkSource.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objStaffLevel = null;
                dt = null;
            }
        }

        private void tbLinkDelink_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clbComp_A(null, null);
        }

    }
}
