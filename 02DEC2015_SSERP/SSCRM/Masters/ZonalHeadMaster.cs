using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;

namespace SSCRM
{
    public partial class ZonalHeadMaster : Form
    {
        SQLDB objSQLdb = null;
        Int32 intCurrentRow = 0, intCurrentCell = 0;
        bool flagText = true;

        public ZonalHeadMaster()
        {
            InitializeComponent();
        }

        private void ZonalHeadMaster_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillFinYearData();

            gvBranchHeadDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
        }


        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                " FROM USER_BRANCH " +
                                " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                "' ORDER BY CM_COMPANY_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
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

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbUserBranch.DataSource = null;

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' AND BRANCH_TYPE='BR' " +
                                        " ORDER BY BRANCH_NAME ";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbUserBranch.DataSource = dt;
                    cbUserBranch.DisplayMember = "BRANCH_NAME";
                    cbUserBranch.ValueMember = "BRANCH_CODE";
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

        private void FillFinYearData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                strCommand = "SELECT DISTINCT(FY_FIN_YEAR) FROM FIN_YEAR";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbFinYear.DataSource = dt;

                    cbFinYear.ValueMember = "FY_FIN_YEAR";
                    cbFinYear.DisplayMember = "FY_FIN_YEAR";
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();

            if (cbCompany.SelectedIndex > 0 && cbUserBranch.SelectedIndex > 0 && cbFinYear.SelectedIndex > 0)
            {
                FillBranchZonalHeadDetails(cbCompany.SelectedValue.ToString(), cbUserBranch.SelectedValue.ToString(), cbFinYear.Text.ToString());
            }
        }


        private DataSet GetBranchZonalHeadDetails(string CompCode,string BranchCode,string sFinYear)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {               
                param[0] = objSQLdb.CreateParameter("@xComp_Code", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranch_Code", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFinYear, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_BranchZonalHeadDetails", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        private void FillBranchZonalHeadDetails(string CompCode,string BranchCode,string FinYear)
        {

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvBranchHeadDetails.Rows.Clear();
            
            if (CompCode.Length>0 && BranchCode.Length>0 && FinYear.Length>0)
            {
                try
                {
                    dt = GetBranchZonalHeadDetails(CompCode,BranchCode,FinYear).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            gvBranchHeadDetails.Rows.Add();

                            if (dt.Rows[i]["StatusFlag"].ToString() == "APPROVED")
                            {
                                gvBranchHeadDetails.Rows[i].Cells["BranchHead"].ReadOnly = true;
                                gvBranchHeadDetails.Rows[i].Cells["DivHead"].ReadOnly = true;
                                gvBranchHeadDetails.Rows[i].Cells["RegHead"].ReadOnly = true;
                                gvBranchHeadDetails.Rows[i].Cells["ZonalHead"].ReadOnly = true;
                            }
                           

                            gvBranchHeadDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                            gvBranchHeadDetails.Rows[i].Cells["FinYear"].Value = dt.Rows[i]["FinYear"].ToString();
                            gvBranchHeadDetails.Rows[i].Cells["DocMonth"].Value = dt.Rows[i]["DocMonth"].ToString();
                            gvBranchHeadDetails.Rows[i].Cells["BranchHead"].Value = dt.Rows[i]["BranchHead"].ToString();
                            gvBranchHeadDetails.Rows[i].Cells["DivHead"].Value = dt.Rows[i]["DivHead"].ToString();
                            gvBranchHeadDetails.Rows[i].Cells["RegHead"].Value = dt.Rows[i]["RegHead"].ToString();
                            gvBranchHeadDetails.Rows[i].Cells["ZonalHead"].Value = dt.Rows[i]["ZonalHead"].ToString();
                            gvBranchHeadDetails.Rows[i].Cells["Status"].Value = dt.Rows[i]["StatusFlag"].ToString();
                        }

                    }
                    else
                    {
                        gvBranchHeadDetails.Rows.Clear();
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
          
        }

        private void cbFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0 && cbUserBranch.SelectedIndex>0 && cbFinYear.SelectedIndex>0)
            {
                FillBranchZonalHeadDetails(cbCompany.SelectedValue.ToString(), cbUserBranch.SelectedValue.ToString(), cbFinYear.Text.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvBranchHeadDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            Int32 EmpEcode = 0;

            if (Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["BranchHead"].Value).Length <= 7
                && Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["BranchHead"].Value) != "")
            {
                EmpEcode = Convert.ToInt32(gvBranchHeadDetails.Rows[e.RowIndex].Cells["BranchHead"].Value);

                strCmd = "SELECT cast(ECODE as varchar)+'--'+MEMBER_NAME EmpName FROM EORA_MASTER WHERE ECODE="+ EmpEcode +"";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["BranchHead"].Value = dt.Rows[0]["EmpName"].ToString();
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["DivHead"].Value = dt.Rows[0]["EmpName"].ToString();
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["RegHead"].Value = dt.Rows[0]["EmpName"].ToString();
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["ZonalHead"].Value = dt.Rows[0]["EmpName"].ToString();
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Ecode","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            if (Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["DivHead"].Value).Length <= 7
                && Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["DivHead"].Value)!="")
            {
                EmpEcode = Convert.ToInt32(gvBranchHeadDetails.Rows[e.RowIndex].Cells["DivHead"].Value);

                strCmd = "SELECT cast(ECODE as varchar)+'--'+MEMBER_NAME EmpName FROM EORA_MASTER WHERE ECODE=" + EmpEcode + "";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {                   
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["DivHead"].Value = dt.Rows[0]["EmpName"].ToString();
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["RegHead"].Value = dt.Rows[0]["EmpName"].ToString();
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["ZonalHead"].Value = dt.Rows[0]["EmpName"].ToString();
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["RegHead"].Value).Length <=7
                && Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["RegHead"].Value)!="")
            {
                EmpEcode = Convert.ToInt32(gvBranchHeadDetails.Rows[e.RowIndex].Cells["RegHead"].Value);

                strCmd = "SELECT cast(ECODE as varchar)+'--'+MEMBER_NAME EmpName FROM EORA_MASTER WHERE ECODE=" + EmpEcode + "";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {                   
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["RegHead"].Value = dt.Rows[0]["EmpName"].ToString();
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["ZonalHead"].Value = dt.Rows[0]["EmpName"].ToString();
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    return;
                }
            }
            if (Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["ZonalHead"].Value).Length <=7
                && Convert.ToString(gvBranchHeadDetails.Rows[e.RowIndex].Cells["ZonalHead"].Value)!="")
            {
                EmpEcode = Convert.ToInt32(gvBranchHeadDetails.Rows[e.RowIndex].Cells["ZonalHead"].Value);

                strCmd = "SELECT cast(ECODE as varchar)+'--'+MEMBER_NAME EmpName FROM EORA_MASTER WHERE ECODE=" + EmpEcode + "";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {                    
                    gvBranchHeadDetails.Rows[e.RowIndex].Cells["ZonalHead"].Value = dt.Rows[0]["EmpName"].ToString();
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }


        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void gvBranchHeadDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            flagText = true;

            if (this.gvBranchHeadDetails.CurrentCell.ColumnIndex == gvBranchHeadDetails.Columns["BranchHead"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 5;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if (this.gvBranchHeadDetails.CurrentCell.ColumnIndex == gvBranchHeadDetails.Columns["DivHead"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 5;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if (this.gvBranchHeadDetails.CurrentCell.ColumnIndex == gvBranchHeadDetails.Columns["RegHead"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 5;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if (this.gvBranchHeadDetails.CurrentCell.ColumnIndex == gvBranchHeadDetails.Columns["ZonalHead"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 5;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
         
        }


        private bool CheckData()
        {
            bool flag = true;

            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;
                
            }

            if (cbUserBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbUserBranch.Focus();
                return flag;
                
            }
            if (cbFinYear.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Financial Year", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbFinYear.Focus();
                return flag;
                
            }
            if (gvBranchHeadDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvBranchHeadDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvBranchHeadDetails.Rows[i].Cells["BranchHead"].Value) == "")
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Branch Head Ecode for the Month of "+gvBranchHeadDetails.Rows[i].Cells["DocMonth"].Value +"","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return flag;
                    }
                    if (Convert.ToString(gvBranchHeadDetails.Rows[i].Cells["DivHead"].Value) == "")
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Divisional Head Ecode for the Month of " + gvBranchHeadDetails.Rows[i].Cells["DocMonth"].Value + "", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return flag;
                    }
                    if (Convert.ToString(gvBranchHeadDetails.Rows[i].Cells["RegHead"].Value) == "")
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Regional Head Ecode for the Month of " + gvBranchHeadDetails.Rows[i].Cells["DocMonth"].Value + "", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return flag;

                    }
                    if (Convert.ToString(gvBranchHeadDetails.Rows[i].Cells["ZonalHead"].Value) == "")
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Zonal Head Ecode for the Month of " + gvBranchHeadDetails.Rows[i].Cells["DocMonth"].Value + "", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return flag;

                    }
                }
            }
            return flag;
                
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "", Status = "";

            if (CheckData() == true)
            {

                try
                {
                    if (gvBranchHeadDetails.Rows.Count > 0)
                    {
                        strCmd = "DELETE FROM BRANCH_ZONAL_HEAD_MASTER "+
                                 " WHERE BZHM_COMP_CODE='"+ cbCompany.SelectedValue.ToString() +
                                 "' and BZHM_BRANCH_CODE='"+ cbUserBranch.SelectedValue.ToString() +
                                 "' and BZHM_FIN_YEAR='"+ cbFinYear.Text.ToString() +"' ";

                        for (int i = 0; i < gvBranchHeadDetails.Rows.Count; i++)
                        {
                            Status = gvBranchHeadDetails.Rows[i].Cells["Status"].Value.ToString();
                            if (Status.Length == 0)
                            {
                                Status = "NOT APPROVED";
                            }
                            else
                            {
                                Status = "APPROVED";
                            }

                            strCmd += "INSERT INTO BRANCH_ZONAL_HEAD_MASTER(BZHM_COMP_CODE " +
                                                                        ", BZHM_BRANCH_CODE " +
                                                                        ", BZHM_FIN_YEAR " +
                                                                        ", BZHM_DOC_MONTH " +
                                                                        ", BZHM_ZONAL_HEAD_ECODE " +
                                                                        ", BZHM_REGION_HEAD_ECODE " +
                                                                        ", BZHM_DIV_HEAD_ECODE " +
                                                                        ", BZHM_BRANCH_HEAD_ECODE " +
                                                                        ", BZHM_STATUS " +
                                                                        ", BZHM_CREATED_BY " +
                                                                        ", BZHM_CREATED_DATE " +
                                                                        ")VALUES " +
                                                                        "('" + cbCompany.SelectedValue.ToString() +
                                                                        "','" + cbUserBranch.SelectedValue.ToString() +
                                                                        "','" + cbFinYear.Text.ToString() +
                                                                        "','" + gvBranchHeadDetails.Rows[i].Cells["DocMonth"].Value.ToString() +
                                                                        "','" + gvBranchHeadDetails.Rows[i].Cells["ZonalHead"].Value.ToString().Split('-')[0] +
                                                                        "','" + gvBranchHeadDetails.Rows[i].Cells["RegHead"].Value.ToString().Split('-')[0] +
                                                                        "','" + gvBranchHeadDetails.Rows[i].Cells["DivHead"].Value.ToString().Split('-')[0] +
                                                                        "','" + gvBranchHeadDetails.Rows[i].Cells["BranchHead"].Value.ToString().Split('-')[0] +
                                                                        "','" + Status +
                                                                        "','" + CommonData.LogUserId + "',getdate())";
                        }
                    }

                    if (strCmd.Length > 10)
                        iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbUserBranch.SelectedIndex = -1;
            cbFinYear.SelectedIndex = 0;
            gvBranchHeadDetails.Rows.Clear();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
             objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "";

            if (CheckData() == true)
            {
                try
                {
                    strCmd = "UPDATE BRANCH_ZONAL_HEAD_MASTER SET BZHM_STATUS='APPROVED',BZHM_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                            "',BZHM_LAST_MODIFIED_DATE=getdate() " +
                            "  WHERE BZHM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                            "' and BZHM_BRANCH_CODE='" + cbUserBranch.SelectedValue.ToString() +
                            "' and BZHM_FIN_YEAR='" + cbFinYear.Text.ToString() + "' ";

                    if (strCmd.Length > 10)
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
                    MessageBox.Show("Data Approved By " + CommonData.LogUserId + " ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0 && cbUserBranch.SelectedIndex > 0 && cbFinYear.SelectedIndex > 0)
            {
                FillBranchZonalHeadDetails(cbCompany.SelectedValue.ToString(), cbUserBranch.SelectedValue.ToString(), cbFinYear.Text.ToString());
            }
        }


    }
}
