using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;
using SSCRMDB;
using SSAdmin;

namespace SSCRM
{
    public partial class AboveGrpSourceToDestMapping : Form
    {
        private UtilityDB objUtil = null;
        private SQLDB objDB = null;
        private StaffLevel objStaffLevel = null;
        //private UtilityLibrary 
        public AboveGrpSourceToDestMapping()
        {
            InitializeComponent();
        }

        private void AboveGrpSourceToDestMapping_Load(object sender, EventArgs e)
        {
            FillDocMonth();
            FillDropDownControls();
        }

        private void FillDropDownControls()
        {
            objDB = new SQLDB();
            try
            {
                DataTable dt = new DataTable();
                dt = objDB.ExecuteDataSet("SELECT DISTINCT cast(DESIG_CODE as varchar)+'^'+cast(isnull(ldm_elevel_id,0) as varchar) " +
                                                        "DESIG_CODE,Desig_Name From DESIG_MAS left JOIN LevelsDesig_mas ON LDM_DESIG_ID " +
                                                        "= DESIG_CODE ORDER BY Desig_Name", CommandType.Text).Tables[0];
                DataView dvDestDesg = dt.DefaultView;
                DataView dvSourceDesg = objDB.ExecuteDataSet("SELECT DISTINCT cast(DESIG_CODE as varchar)+'^'+cast(isnull(ldm_elevel_id,0) as varchar) " +
                                                        "DESIG_CODE,Desig_Name From DESIG_MAS left JOIN LevelsDesig_mas ON LDM_DESIG_ID " +
                                                        "= DESIG_CODE ORDER BY Desig_Name", CommandType.Text).Tables[0].DefaultView;
                UtilityLibrary.PopulateControl(cmbDestDesig, dvDestDesg, 1, 0, "--PLEASE SELECT--", 0);
                UtilityLibrary.PopulateControl(cmbSourceDesig, dvSourceDesg, 1, 0, "--PLEASE SELECT--", 0);
                dt = null;
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

        private void cbDocmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillLinkEmplist();
        }

        //private void FillLinkEmplist()
        //{
        //    objDB = new SQLDB();
        //    DataTable dt = new DataTable();
        //    clbEmpListQry.Items.Clear();
        //    //clbdlinkEmplist.Items.Clear();
        //    try
        //    {
        //        string sqlText = "exec AboveBranchLeaders_Qry '" + cbDocmonth.SelectedValue.ToString() + "','NFL,VNF,NKBPL,SSBPL,SBTLNPL'";
        //        dt = objDB.ExecuteDataSet(sqlText).Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objDB = null;
        //    }

        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dataRow in dt.Rows)
        //        {
        //            NewCheckboxListItem oclBox = new NewCheckboxListItem();
        //            oclBox.Tag = dataRow["xDest_ecode"].ToString();
        //            oclBox.Text = dataRow["xDest_ecode"].ToString() + "-" + dataRow["xDest_eName"].ToString() + "-(" + dataRow["xDest_Nog"].ToString() + ")";
        //            clbEmpListQry.Items.Add(oclBox);
        //            oclBox = null;
        //        }
        //    }
        //    dt = null;
        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDestEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtSorceEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDestEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDestEcode.Text.ToString().Trim().Length > 4)
            {
                GetEmpData();
                GetMappedData();
            }
            else
            {
                txtDestEName.Text = "";
            }
        }
        private void FillMappedSourceECodes()
        {
            DataTable dt = null;
            objStaffLevel = new StaffLevel();
            clbSource.Items.Clear();
            try
            {
                dt = objStaffLevel.AboveBranchLeaders_InQ_Get(cbDocmonth.SelectedValue.ToString(), txtDestEcode.Text.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["xDest_ecode"].ToString();
                        oclBox.Text = dataRow["xDest_ecode"].ToString() + "-" + dataRow["xDest_eName"].ToString() + "-(" + dataRow["xDest_NoG"].ToString() + ")";
                        clbSource.Items.Add(oclBox);
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
        private void GetMappedData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            clbSource.Items.Clear();
            try
            {
                string sqlText = "SELECT " +
                                "lsd_doc_month," +
                                "lsd_source_desig_id," +
                                "lsd_source_elevel_id," +
                                "lsd_source_ecode," +
                                "CAST(ES.ECODE AS VARCHAR)+'-'+ES.MEMBER_NAME+'-('+ES.DESIG+')' lsd_source_ename," +
                                "lsd_dest_desig_id," +
                                "lsd_dest_elevel_id," +
                                "lsd_dest_ecode," +
                                "CAST(ED.ECODE AS VARCHAR)+'-'+ED.MEMBER_NAME+'-('+ED.DESIG+')' lsd_dest_ename " +
                                "from LevelSource_Dest_mapping " +
                                "INNER JOIN EORA_MASTER ES ON ES.ECODE = lsd_source_ecode " +
                                "INNER JOIN EORA_MASTER ED ON ED.ECODE = lsd_dest_ecode " +
                                "WHERE lsd_dest_ecode = " + txtDestEcode.Text + " AND lsd_doc_month = '" + cbDocmonth.SelectedValue.ToString() + "'";
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    cmbDestDesig.SelectedValue = dt.Rows[0]["lsd_dest_desig_id"].ToString() + "^" + dt.Rows[0]["lsd_dest_elevel_id"].ToString();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["lsd_source_ecode"].ToString() + "^" + dataRow["lsd_source_desig_id"].ToString() + "^" + dataRow["lsd_source_elevel_id"].ToString();
                        oclBox.Text = dataRow["lsd_source_ename"].ToString();
                        clbSource.Items.Add(oclBox);
                        oclBox = null;
                        clbSource.SetItemCheckState(clbSource.Items.Count - 1, CheckState.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                objDB = null;
            }
        }

        private void GetEmpData()
        {
            objDB = new SQLDB();
            if (txtDestEcode.Text.ToString().Trim().Length > 4)
            {
                DataTable dt = objDB.ExecuteDataSet("SELECT ISNULL(HAAM_EMP_CODE,ECODE) ECODE,CAST(DESG_ID as varchar)+'^'+cast(isnull(ldm_elevel_id,0) as varchar) DESG_ID" +
                                                       ",CAST(ISNULL(HAAM_EMP_CODE,ECODE) AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' AS ENAME " +
                                                       "FROM EORA_MASTER LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = ECODE " +
                                                       "LEFT JOIN LevelsDesig_mas ON ldm_company_code = company_code AND LDM_DESIG_ID = DESG_ID " +
                                                       "WHERE ECODE=" + txtDestEcode.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtDestEName.Text = dt.Rows[0]["ENAME"].ToString();
                    cmbDestDesig.SelectedValue = dt.Rows[0]["DESG_ID"].ToString();
                }
                else
                {
                    txtDestEName.Text = "";
                    cmbSourceDesig.SelectedIndex = 0;
                }
                dt = null;
            }
            else
                txtDestEName.Text = "";
            objDB = null;
        }

        private void txtSorceEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSorceEcode.Text.ToString().Trim().Length > 4)
            {
                GetSourceEmpData();
            }
            else
            {
                txtSourceEname.Text = "";
            }
        }
        private void GetSourceEmpData()
        {
            objDB = new SQLDB();
            if (txtSorceEcode.Text.ToString().Trim().Length > 4)
            {
                DataTable dt = objDB.ExecuteDataSet("SELECT ISNULL(HAAM_EMP_CODE,ECODE) ECODE,CAST(DESG_ID as varchar)+'^'+cast(isnull(ldm_elevel_id,0) as varchar) DESG_ID" +
                                                       ",CAST(ISNULL(HAAM_EMP_CODE,ECODE) AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' AS ENAME " +
                                                       "FROM EORA_MASTER LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = ECODE " +
                                                       "LEFT JOIN LevelsDesig_mas ON ldm_company_code = company_code AND LDM_DESIG_ID = DESG_ID " +
                                                       "WHERE ECODE=" + txtSorceEcode.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //txtSorceEcode.Text = dt.Rows[0]["ECODE"].ToString();
                    txtSourceEname.Text = dt.Rows[0]["ENAME"].ToString();
                    cmbSourceDesig.SelectedValue = dt.Rows[0]["DESG_ID"].ToString();
                }
                else
                {
                    txtSourceEname.Text = "";
                    cmbSourceDesig.SelectedIndex = 0;
                }
                dt = null;
            }
            else
                txtSourceEname.Text = "";
            objDB = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDestEName.Text.Trim().Length > 0)
            {
                if (txtSourceEname.Text.Trim().Length > 0)
                {
                    objDB = new SQLDB();
                    DataTable dt = new DataTable();
                    string sqlText = "select * from LevelSource_Dest_mapping where lsd_source_ecode = " + txtSorceEcode.Text + " and lsd_doc_month = '" + cbDocmonth.Text + "'";
                    try
                    {
                        dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            for (int i = 0; i < clbSource.Items.Count; i++)
                            {
                                if (((SSAdmin.NewCheckboxListItem)(clbSource.Items[i])).Tag.ToString() == txtSorceEcode.Text + "^" + cmbSourceDesig.SelectedValue.ToString())
                                {
                                    MessageBox.Show("Allready Added to list", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = txtSorceEcode.Text + "^" + cmbSourceDesig.SelectedValue.ToString();
                            oclBox.Text = txtSourceEname.Text;
                            clbSource.Items.Add(oclBox);
                            oclBox = null;
                            clbSource.SetItemCheckState(clbSource.Items.Count - 1, CheckState.Checked);
                        }
                        else
                        {
                            MessageBox.Show("Allready Mapped With " + dt.Rows[0]["lsd_dest_ecode"].ToString(), "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
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
                else
                {
                    MessageBox.Show("Enter Source Employee Ecode", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter Destination Employee Ecode", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbDocmonth.SelectedIndex = 0;
            cmbDestDesig.SelectedIndex = 0;
            cmbSourceDesig.SelectedIndex = 0;
            txtDestEcode.Text = "";
            txtDestEName.Text = "";
            txtSorceEcode.Text = "";
            txtSourceEname.Text = "";
            clbSource.Items.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objDB = new SQLDB();
                string sqlText = "";
                int iRes = 0;
                try
                {
                    string sSorceECodes = "";
                    sqlText = "DELETE FROM LevelSource_Dest_mapping WHERE lsd_dest_ecode = " + txtDestEcode.Text +
                        " AND lsd_doc_month='" + cbDocmonth.SelectedValue.ToString() + "'";
                    objDB.ExecuteSaveData(sqlText);
                    for (int i = 0; i < clbSource.Items.Count; i++)
                    {
                        if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                        {
                            string[] sSourceEcode = ((SSAdmin.NewCheckboxListItem)(clbSource.Items[i])).Tag.ToString().Split('^');
                            sSorceECodes += sSourceEcode[0] + ",";
                            string[] sDestEcode = cmbDestDesig.SelectedValue.ToString().Split('^');
                            sqlText += " INSERT INTO LevelSource_Dest_mapping" +
                                        "(lsd_doc_month," +
                                        "lsd_source_ecode," +
                                        "lsd_source_elevel_id," +
                                        "lsd_source_desig_id," +
                                        "lsd_dest_ecode," +
                                        "lsd_dest_elevel_id," +
                                        "lsd_dest_desig_id," +
                                        "lsd_created_by," +
                                        "lsd_created_date)" +
                                        "VALUES('" + cbDocmonth.SelectedValue.ToString() +
                                        "'," + sSourceEcode[0] +
                                        "," + sSourceEcode[2] +
                                        "," + sSourceEcode[1] +
                                        "," + txtDestEcode.Text +
                                        "," + sDestEcode[1] +
                                        "," + sDestEcode[0] +
                                        ",'" + CommonData.LogUserId +
                                        "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                        "');";
                        }
                        sqlText += "exec AboveBranchLeaders_Ins '" + CommonData.LogUserId + 
                                    "','" + cbDocmonth.SelectedValue.ToString() +
                                    "', " + txtDestEcode.Text + ", '" + sSorceECodes + "'";
                    }
                    iRes = objDB.ExecuteSaveData(sqlText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool CheckData()
        {
            throw new NotImplementedException();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

    }
}
