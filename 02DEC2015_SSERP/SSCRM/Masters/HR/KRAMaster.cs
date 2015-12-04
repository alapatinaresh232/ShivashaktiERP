using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using SSCRMDB;
using System.IO;

namespace SSCRM
{
    public partial class KRAMaster : Form
    {
        SQLDB objSqlDb = null;
        DataGridViewRow dgvr;
        bool flag = false;

        public KRAMaster()
        {
            InitializeComponent();
        }

        private void KRAMaster_Load(object sender, EventArgs e)
        {
            FillDepartmentData();
            FillDesignationData();
            ParameterAutoComplete();
        }

        private void FillDepartmentData()
        {
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strcmd = "select distinct dept_code,dept_name from Dept_Mas ";
                dt = objSqlDb.ExecuteDataSet(strcmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    //dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbDepartment.DataSource = dt;
                    cbDepartment.DisplayMember = "dept_name";
                    cbDepartment.ValueMember = "dept_code";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void FillDesignationData()
        {
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            if (cbDepartment.SelectedIndex > 0)
            {
                try
                {
                    string strcmd = "select distinct desig_code,desig_name from DESIG_MAS where dept_code='" + cbDepartment.SelectedValue.ToString() + "' ";
                    dt = objSqlDb.ExecuteDataSet(strcmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = 0;
                        dr[1] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);
                        cbDesig.DataSource = dt;
                        cbDesig.DisplayMember = "desig_name";
                        cbDesig.ValueMember = "desig_code";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objSqlDb = null;
                    dt = null;
                }

            }
            else
            {
                cbDesig.DataSource = null;
            }

        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDepartment.SelectedIndex > 0)
            {
                FillDesignationData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTarget_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }


        private void txtparameter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSqlDb = new SQLDB();

            if (CheckData() == true)
            {
                if (KRAInsertUpdateDetails() > 0)
                {
                    FillKRAGrid();
                    MessageBox.Show("Data Saved Successfully", "KRAMaster", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtparameter.Enabled = true;
                    cbDepartment.Enabled = true;
                    cbDesig.Enabled = true;
                    txtparameter.Text = "";
                    txtTarget.Text = "";

                }
                else
                {
                    MessageBox.Show("Data not saved", "KRAMaster", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void gvKeyRoleActivities_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ires = 0;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvKeyRoleActivities.Columns["edit"].Index)
                {
                    flag = true;

                    //dgvr = gvKeyRoleActivities.Rows[e.RowIndex];
                    cbDepartment.Enabled = false;
                    cbDesig.Enabled = false;
                    txtparameter.Text = gvKeyRoleActivities.Rows[e.RowIndex].Cells["parameter"].Value.ToString();
                    txtparameter.Enabled = false;
                    txtTarget.Text = gvKeyRoleActivities.Rows[e.RowIndex].Cells["target"].Value.ToString();
                }


                if (e.ColumnIndex == gvKeyRoleActivities.Columns["Del"].Index)
                {

                    DialogResult dlgResult = MessageBox.Show("Do you want to Delete this Record ", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        dgvr = gvKeyRoleActivities.Rows[e.RowIndex];

                        objSqlDb = new SQLDB();
                        string strcmd = "";

                        try
                        {
                            strcmd = "Delete from HR_PMS_KRA_MASTER where HPKM_DEPT_CODE='" + cbDepartment.SelectedValue.ToString() +
                                                  "' and HPKM_DESIG_ID='" + cbDesig.SelectedValue.ToString() +
                                                  "' and HPKM_PERFORMANCE_PMTR_DESC = '" + gvKeyRoleActivities.Rows[e.RowIndex].Cells["parameter"].Value.ToString() + "' ";
                            objSqlDb = new SQLDB();
                            ires = objSqlDb.ExecuteSaveData(strcmd);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (ires > 0)
                        {
                            MessageBox.Show("Data Deleted Successfully");
                            FillKRAGrid();

                        }
                        else
                        {
                            MessageBox.Show("Data not Deleted");
                        }

                    }
                }

            }
        }

        private int KRAInsertUpdateDetails()
        {
            objSqlDb = new SQLDB();
            int iresult = 0;
            string strcmd = "";
            try
            {
                if (flag == true)
                {

                    strcmd = "Update HR_PMS_KRA_MASTER set HPKM_PERFORMANCE_PMTR_TARGET='" + txtTarget.Text.ToString() +
                                              "' , HPKM_MODIFIED_BY= '" + CommonData.LogUserId +
                                              "', HPKM_MODIFIED_DATE=getdate() " +
                                              " where HPKM_DEPT_CODE='" + cbDepartment.SelectedValue.ToString() +
                                              "' and HPKM_DESIG_ID='" + cbDesig.SelectedValue.ToString() +
                                              "' and HPKM_PERFORMANCE_PMTR_DESC = '" + txtparameter.Text.ToString() + "' ";

                }
                else if (flag == false)
                {

                    strcmd = "INSERT INTO HR_PMS_KRA_MASTER(HPKM_DEPT_CODE " +
                                                            ", HPKM_DESIG_ID " +
                                                            ", HPKM_ROLE_DESC " +
                                                            ", HPKM_PERFORMANCE_PMTR_DESC " +
                                                            ", HPKM_PERFORMANCE_PMTR_TARGET " +
                                                            ", HPKM_CREATED_BY" +
                                                            ", HPKM_CREATED_DATE" +
                                                            ")SELECT * from(SELECT'" + cbDepartment.SelectedValue.ToString() +
                                                            "' deptcode,'" + cbDesig.SelectedValue.ToString().Split('@')[0] +
                                                            "' desigid,'" + cbDesig.Text.ToString() +
                                                            "' designame,'" + txtparameter.Text.ToString() +
                                                            "' parameters,'" + txtTarget.Text.ToString() +
                                                            "'target,'" + CommonData.LogUserId + "'users,getdate() date)a  where parameters not in "+
                                                            "(select HPKM_PERFORMANCE_PMTR_DESC from HR_PMS_KRA_MASTER WHERE HPKM_PERFORMANCE_PMTR_DESC='"+ txtparameter.Text.ToString()+
                                                             "' AND HPKM_DEPT_CODE='" + cbDepartment.SelectedValue.ToString() +
                                                             "' AND HPKM_ROLE_DESC='"+ cbDesig.Text.ToString() +"' ) ";
                }

                if (strcmd.Length > 10)
                {
                    objSqlDb = new SQLDB();
                    iresult = objSqlDb.ExecuteSaveData(strcmd);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return iresult;
        }

        public Hashtable GetDetails(string strDeptCode, string strDesigCode)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSqlDb.CreateParameter("@sDeptCode", DbType.String, strDeptCode, ParameterDirection.Input);
                param[1] = objSqlDb.CreateParameter("@sDesigCode", DbType.String, strDesigCode, ParameterDirection.Input);

                ds = objSqlDb.ExecuteDataSet("HR_PMS_KRA_DETAILS", CommandType.StoredProcedure, param);
                ht.Add("KRAMasterDetails", ds.Tables[0]);
                ds = null;
                param = null;
                objSqlDb = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ht;

        }

        public void FillKRAGrid()
        {
            Hashtable ht;
            DataTable dtKRAMaster;
            gvKeyRoleActivities.Rows.Clear();

            try
            {
                ht = GetDetails(cbDepartment.SelectedValue.ToString(), cbDesig.SelectedValue.ToString());
                dtKRAMaster = (DataTable)ht["KRAMasterDetails"];
                if (dtKRAMaster.Rows.Count > 0)
                {
                    for (int i = 0; i < dtKRAMaster.Rows.Count; i++)
                    {
                        gvKeyRoleActivities.Rows.Add();
                        gvKeyRoleActivities.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                        gvKeyRoleActivities.Rows[i].Cells["parameter"].Value = dtKRAMaster.Rows[i]["HPKM_PERFORMANCE_PMTR_DESC"].ToString();
                        gvKeyRoleActivities.Rows[i].Cells["target"].Value = dtKRAMaster.Rows[i]["HPKM_PERFORMANCE_PMTR_TARGET"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbDepartment.SelectedIndex = 0;
            cbDesig.SelectedIndex = -1;
            txtparameter.Text = "";
            txtTarget.Text = "";
            gvKeyRoleActivities.Rows.Clear();
            txtparameter.Enabled = true;
            cbDepartment.Enabled = true;
            cbDesig.Enabled = true;
        }
        private bool CheckData()
        {
            bool bFlag = true;
            if (cbDepartment.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Department", "KRAMaster", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDepartment.Focus();
                return bFlag;
            }
            else if (cbDesig.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Designation", "KRAMaster", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDesig.Focus();
                return bFlag;
            }
            else if (txtparameter.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Parameter", "KRAMaster", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtparameter.Focus();
                return bFlag;
            }
            else if (txtTarget.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Target", "KRAMaster", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTarget.Focus();
                return bFlag;
            }

            return bFlag;
        }

        private void cbDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDesig.SelectedIndex > 0)
            {
                FillKRAGrid();
            }
        }
        private void ParameterAutoComplete()
        {
            try
            {
                objSqlDb = new SQLDB();
                //  AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();

                DataTable dt = new DataTable();
                string strcmd = "";
                strcmd = "select HPKM_PERFORMANCE_PMTR_DESC from HR_PMS_KRA_MASTER";
                dt = objSqlDb.ExecuteDataSet(strcmd).Tables[0];
                AutoCompleteTextBox(txtparameter, dt, "", "HPKM_PERFORMANCE_PMTR_DESC");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSqlDb = null;

            }
        }

        private void txtparameter_TextChanged(object sender, EventArgs e)
        {
           //  ParameterAutoComplete();
        }
        public static void AutoCompleteTextBox(TextBox TextBoxControl, DataTable dtDataTable, string strSqlText, string FieldName)
        {
            AutoCompleteStringCollection daColl = new AutoCompleteStringCollection();
            string[] postSource = dtDataTable
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>(FieldName))
                    .ToArray();
            var source = new AutoCompleteStringCollection();
            source.AddRange(postSource);
            TextBoxControl.AutoCompleteCustomSource = source;
            TextBoxControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TextBoxControl.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

    }
}
