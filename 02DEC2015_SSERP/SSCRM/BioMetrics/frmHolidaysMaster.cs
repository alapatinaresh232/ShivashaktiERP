using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;

namespace SSCRM
{
    public partial class frmHolidaysMaster : Form
    {
        SQLDB objSQLdb = null;

        private string strState = "", strBrType = "", strBranch = "", strStates = "",strLocType="";

        public frmHolidaysMaster()
        {
            InitializeComponent();
        }

        private void frmHolidaysMaster_Load(object sender, EventArgs e)
        {
            FillStates();
            FillBranchTypes();
            nmYear.Value = DateTime.Now.Year;
            dtpHolidayDate.Value = DateTime.Today;            
            GenerateHolidayId();
        }

        private void GenerateHolidayId()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = " SELECT ISNULL(MAX(HLHM_HOLIDAY_ID),0)+1 AS HolidayId FROM HR_LOCATION_HOLIDAY_MASTER";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtHolId.Text = dt.Rows[0]["HolidayId"] + "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }

        private void FillStates()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbState.Items.Clear();
            try    
            {
                strCmd = "SELECT DISTINCT STATE_CODE StateCode,sm_state StateName FROM BRANCH_MAS "+
                         " INNER JOIN state_mas ON sm_state_code=STATE_CODE "+
                         " ORDER BY sm_state asc";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["StateCode"].ToString();
                        oclBox.Text = item["StateName"].ToString();
                        clbState.Items.Add(oclBox);
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
                objSQLdb = null;
                dt = null;
            }

        }

        private void FillBranchTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbBrType.Items.Clear();
            try
            {
                strCmd = "SELECT Distinct(BRANCH_TYPE) BranchType " +
                           ", CASE  WHEN BRANCH_TYPE='BR' THEN 'BRANCH' " +
                           "  WHEN BRANCH_TYPE='SP' THEN 'STOCK POINT' " +
                           "  WHEN BRANCH_TYPE='PU' THEN 'PRODUCTION UNIT' " +
                           "  WHEN BRANCH_TYPE='TR' THEN 'TRANSPORT UNIT' " +
                           "  WHEN BRANCH_TYPE='HO' THEN 'HEAD OFFICE' " +
                           "  WHEN BRANCH_TYPE='ST' THEN 'STORE' " +
                           "  WHEN BRANCH_TYPE='OL' THEN 'OUTLETS' END BranchTypeName " +
                           "  FROM BRANCH_MAS  WHERE BRANCH_TYPE NOT IN('PO') ORDER BY BranchTypeName";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dr["BranchType"].ToString();
                        oclBox.Text = dr["BranchTypeName"].ToString();
                        clbBrType.Items.Add(oclBox);
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
                objSQLdb = null;
                dt = null;
            }
        }

        private void FillBranches(string strStates,string strType)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbbranch.Items.Clear();
            if (strStates.Length > 0)
            {
                try
                {

                    if (chkBrTypeAll.Checked == true)
                    {
                        strCmd = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as BranchCode FROM USER_BRANCH  " +
                                 " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                 " WHERE  UB_USER_ID ='" + CommonData.LogUserId +
                                 "' and STATE_CODE in (" + strStates + ") and ACTIVE='T' " +
                                 "  ORDER BY BRANCH_NAME ASC";
                    }
                    else
                    {
                        if (strType.Length > 0)
                        {
                            strCmd = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as BranchCode FROM USER_BRANCH  " +
                                     " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                     " WHERE  UB_USER_ID ='" + CommonData.LogUserId +
                                     "' and STATE_CODE in (" + strStates + ") and ACTIVE='T' " +
                                     " and BRANCH_TYPE IN (" + strBrType + ") ORDER BY BRANCH_NAME ASC";
                        }
                    }
                    if (strCmd.Length > 0)
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dr["BranchCode"].ToString();
                            oclBox.Text = dr["BRANCH_NAME"].ToString();
                            clbbranch.Items.Add(oclBox);
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
                    dt = null;
                    objSQLdb = null;
                }
            }
        }
        private bool CheckData()
        {
            bool flag = true;

            GetSelectedValues();

            if (strStates.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast one State" ,"SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return flag;
            }
            if (strLocType.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast one Branch Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (strBranch.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast one Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (txtHolName.Text.Length < 3)
            {
                flag = false;
                MessageBox.Show("Please Enter Holiday Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;
            if (CheckData() == true)
            {
                try
                {

                    if (clbbranch.CheckedItems.Count > 0)
                    {
                        for (int i = 0; i < clbbranch.Items.Count; i++)
                        {
                            if (clbbranch.GetItemCheckState(i) == CheckState.Checked)
                            {
                                strCmd += "INSERT INTO HR_LOCATION_HOLIDAY_MASTER(HLHM_HOLIDAY_ID " +
                                                                               ", HLHM_STATE_CODE " +
                                                                               ", HLHM_BRANCH_CODE " +
                                                                               ", HLHM_HOLIDAY_DATE " +
                                                                               ", HLHM_HOLIDAY_NAME " +
                                                                               ", HLHM_HOLIDAY_DESCRIPTION " +
                                                                               ", HLHM_CREATED_BY " +
                                                                               ", HLHM_CREATED_DATE " +
                                                                               ")VALUES(" + Convert.ToInt32(txtHolId.Text) +
                                                                               ",'" + ((NewCheckboxListItem)clbbranch.Items[i]).Tag.ToString().Split('@')[1] +
                                                                               "','" + ((NewCheckboxListItem)clbbranch.Items[i]).Tag.ToString().Split('@')[0] +
                                                                               "','" + Convert.ToDateTime(dtpHolidayDate.Value).ToString("dd/MMM/yyyy") +
                                                                               "','" + txtHolName.Text.ToString().Replace("'", "") +
                                                                               "','" + txtHolDesc.Text.ToString().Replace("'", "") +
                                                                               "','" + CommonData.LogUserId +
                                                                               "',getdate())";
                            }
                        }
                    }

                    if (strCmd.Length > 0)
                    {
                        iRes = objSQLdb.ExecuteSaveData(strCmd);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GenerateHolidayId();
                    txtHolName.Text = "";
                    txtHolDesc.Text = "";
                    txtHolName.Focus();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void clbState_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //strState = "";

            //for (int i = 0; i < clbState.Items.Count; i++)
            //{
            //    if (e.Index != i)
            //        clbState.SetItemCheckState(i, CheckState.Unchecked);

            //    if (clbState.GetItemCheckState(i) == CheckState.Checked)
            //    {
            //        strState += "'" + ((NewCheckboxListItem)clbState.Items[i]).Tag + "',";
            //    }
            //}

            //if (strState.Length > 0)
            //{
            //    strState = strState.TrimEnd(',');
            //    FillBranches(strState, strBrType);
            //}
        }

        private void clbBrType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //strBrType = "";

            //for (int i = 0; i < clbBrType.Items.Count; i++)
            //{
            //    if (clbBrType.GetItemCheckState(i) == CheckState.Checked)
            //    {
            //        strBrType += "'" + ((NewCheckboxListItem)clbBrType.Items[i]).Tag + "',";
            //    }
            //}

            //if (strBrType.Length > 0)
            //{
            //    strBrType = strBrType.TrimEnd(',');

            //    FillBranches(strState,strBrType);
            //}
        }

        private void chkBrTypeAll_CheckedChanged(object sender, EventArgs e)
        {
            strBrType = "";
            if (chkBrTypeAll.Checked == true)
            {
                clbBrType.Enabled = false;

                for (int i = 0; i < clbBrType.Items.Count; i++)
                {
                    clbBrType.SetItemCheckState(i, CheckState.Checked);
                }
                strBrType = "ALL";
            }
            else
            {
                clbBrType.Enabled = true;

                for (int i = 0; i < clbBrType.Items.Count; i++)
                {
                    clbBrType.SetItemCheckState(i, CheckState.Unchecked);
                }

            }

            FillBranches(strState,strBrType);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clbbranch.Items.Clear();
            FillBranchTypes();
            FillStates();            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void clbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            strState = "";

            for (int i = 0; i < clbState.Items.Count; i++)
            {
                if (clbState.GetItemCheckState(i) == CheckState.Checked)
                {
                    strState += "'" + ((NewCheckboxListItem)clbState.Items[i]).Tag + "',";
                }
            }

            if (strState.Length > 0)
            {
                strState = strState.TrimEnd(',');
                FillBranches(strState, strBrType);
            }
        }

        private void clbBrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            strBrType = "";

            for (int i = 0; i < clbBrType.Items.Count; i++)
            {
                if (clbBrType.GetItemCheckState(i) == CheckState.Checked)
                {
                    strBrType += "'" + ((NewCheckboxListItem)clbBrType.Items[i]).Tag + "',";
                }
            }

            if (strBrType.Length > 0)
            {
                strBrType = strBrType.TrimEnd(',');

                FillBranches(strState, strBrType);
            }
        }

        private void chkBrAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBrAll.Checked == true)
            {
                clbbranch.Enabled = false;
                for (int i = 0; i < clbbranch.Items.Count; i++)
                {
                    clbbranch.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                clbbranch.Enabled = true;
                for (int i = 0; i < clbbranch.Items.Count; i++)
                {
                    clbbranch.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void GetSelectedValues()
        {
            strBranch = "";
            strLocType = "";
            strStates = "";

            for (int i = 0; i < clbbranch.Items.Count; i++)
            {
                if (clbbranch.GetItemCheckState(i) == CheckState.Checked)
                {
                    strBranch += ((NewCheckboxListItem)clbbranch.Items[i]).Tag.ToString().Split('@')[0] + ',';
                }
            }

            if (strBrType.Length > 0)
            {
                strBranch = strBranch.TrimEnd(',');                
            }


            for (int i = 0; i < clbState.Items.Count; i++)
            {
                if (clbState.GetItemCheckState(i) == CheckState.Checked)
                {
                    strStates += "'" + ((NewCheckboxListItem)clbState.Items[i]).Tag + "',";
                }
            }

            if (strStates.Length > 0)
            {
                strStates = strState.TrimEnd(',');
               
            }

            for (int i = 0; i < clbBrType.Items.Count; i++)
            {
                if (clbBrType.GetItemCheckState(i) == CheckState.Checked)
                {
                    strLocType += "'" + ((NewCheckboxListItem)clbBrType.Items[i]).Tag + "',";
                }
            }

            if (strLocType.Length > 0)
            {
                strLocType = strLocType.TrimEnd(',');

            }

        }

        private bool CheckDatails()
        {
            bool flag = true;

            GetSelectedValues();

            if (strStates.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast one State", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (strLocType.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast one Branch Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (strBranch.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast one Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }          


            return flag;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckDatails() == true)
            {
                ReportViewer childReportViewer = new ReportViewer("",strBranch,nmYear.Value.ToString());
                CommonData.ViewReport = "SSCRM_HOLIDAY_MASTER_BRANCH_WISE";
                childReportViewer.Show();
            }
        }

        private void txtHolId_KeyUp(object sender, KeyEventArgs e)
        {
            //objSQLdb = new SQLDB();
            //DataTable dt = new DataTable();
            //string strCmd = "";

            //try
            //{
            //    strCmd = "SELECT HLHM_HOLIDAY_ID,HLHM_BRANCH_CODE,HLHM_HOLIDAY_DATE,HLHM_HOLIDAY_NAME "+
            //             ",HLHM_HOLIDAY_DESCRIPTION,HLHM_STATE_CODE FROM HR_LOCATION_HOLIDAY_MASTER "+
            //             " WHERE HLHM_HOLIDAY_ID= "+ txtHolId.Text +"";
            //    dt = objSQLdb.ExecuteDataSet(strCmd);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

      

       

    }
}
