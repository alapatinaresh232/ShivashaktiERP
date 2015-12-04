using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class AuditDRPlanning : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;
        bool flagUpdate = false;
       
        int EmpEcode = 0;
        int EmpApplNo = 0;
      
             
        public AuditDRPlanning()
        {
            InitializeComponent();
        }

        private void AuditDRPlanning_Load(object sender, EventArgs e)
        {
           

            dtpFromdate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;

            FillAuditEmployeeDetails();

            gvAuditEmpPlanningDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);


            if (CommonData.LogUserId == "admin" || CommonData.LogUserId == "ADMIN")
            {
                txtEcodeSearch.Text = "";
            }
            else
            {
                txtEcodeSearch.Text = CommonData.LogUserId;
            }           
                                           
           
        }


        public DataSet GetAuditEmployeeEcodes(string EmpName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sECodeName", DbType.String, EmpName, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_AuditEmployeeEcodes", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

       


        private void FillAuditEmployeeDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbAuditEmployees.DataBindings.Clear();

            try
            {

                dt = GetAuditEmployeeEcodes(txtEcodeSearch.Text.ToString()).Tables[0];

                if (dt.Rows.Count > 0)
                {                 
                    cbAuditEmployees.DataSource = dt;
                    cbAuditEmployees.DisplayMember = "EmpName";
                    cbAuditEmployees.ValueMember = "EmpDetl";
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
            

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                FillAuditEmployeeDetails();
            }

        }


        private void btnAddRecoveryDetl_Click(object sender, EventArgs e)
        {                           
            AddAuditDRPlanningDetl AuditDRPlan = new AddAuditDRPlanningDetl();
            AuditDRPlan.objAuditDRPlanning = this;
            AuditDRPlan.ShowDialog();

        }

        private void cbAuditEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbAuditEmployees.SelectedIndex > -1)
            {
                txtEmpDesg.Text = ((System.Data.DataRowView)(cbAuditEmployees.SelectedItem)).Row.ItemArray[1].ToString().Split('@')[2];

            }
          
            txtDept.Text = "INTERNAL AUDIT";
            FillEmpAuditPlanningDetl();


        }
        

        public void FillEmpAuditPlanningDetl()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            gvAuditEmpPlanningDetl.Rows.Clear();
                       
            Int32 iEmpEcode = 0;

            if (cbAuditEmployees.SelectedIndex > -1)
            {

                try
                {
                    if (cbAuditEmployees.SelectedIndex > -1)
                    {
                        iEmpEcode = Convert.ToInt32(((System.Data.DataRowView)(cbAuditEmployees.SelectedItem)).Row.ItemArray[1].ToString().Split('@')[0]);
                    }
                    
                    dt = objHRdb.GetEmpAuditPlanningDetl(iEmpEcode, Convert.ToDateTime(dtpFromdate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy")).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvAuditEmpPlanningDetl.Rows.Add();

                            gvAuditEmpPlanningDetl.Rows[i].Cells["SlNO"].Value = (i + 1).ToString();
                            gvAuditEmpPlanningDetl.Rows[i].Cells["CompCode"].Value = dt.Rows[i]["CompCode"].ToString();
                            gvAuditEmpPlanningDetl.Rows[i].Cells["LocationCode"].Value = dt.Rows[i]["BranchCode"].ToString();
                            gvAuditEmpPlanningDetl.Rows[i].Cells["BranchType"].Value = dt.Rows[i]["TrnType"].ToString();
                            gvAuditEmpPlanningDetl.Rows[i].Cells["Date"].Value = Convert.ToDateTime(dt.Rows[i]["PlanningDate"]).ToString("dd/MMM/yyyy");
                            gvAuditEmpPlanningDetl.Rows[i].Cells["Location"].Value = dt.Rows[i]["LocationName"].ToString();
                            gvAuditEmpPlanningDetl.Rows[i].Cells["Remarks"].Value = dt.Rows[i]["Remarks"].ToString();
                           
                          
                        }

                       
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRdb = null;
                    
                }
            }
            
        }

        private void dtpFromdate_ValueChanged(object sender, EventArgs e)
        {
            FillEmpAuditPlanningDetl();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            FillEmpAuditPlanningDetl();
        }

        
        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            FillAuditEmployeeDetails();

        }

        private bool CheckData()
        {
            bool flag = true;

            if (cbAuditEmployees.SelectedIndex < -1)
            {
                flag = false;
                MessageBox.Show("Please Select Employee Name","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbAuditEmployees.Focus();
                return flag;
               
            }

            if (gvAuditEmpPlanningDetl.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Employee DR Planning Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAddEmpDRDetails.Focus();
                return flag;
               
            }

            return flag;

        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            EmpEcode = 0;
            EmpApplNo = 0;

            if (CheckData() == true)
            {
                try
                {
                    EmpEcode = Convert.ToInt32(cbAuditEmployees.Text.ToString().Split('-')[0]);
                    EmpApplNo = Convert.ToInt32(cbAuditEmployees.SelectedValue.ToString().Split('@')[1]);

                    if (gvAuditEmpPlanningDetl.Rows.Count > 0)
                    {
                        for (int j = 0; j < gvAuditEmpPlanningDetl.Rows.Count; j++)
                        {
                            strCommand += "DELETE FROM AUDIT_DR_PLANNING WHERE ADRP_EORA_CODE=" + EmpEcode +
                                       " AND ADRP_DATE='" + Convert.ToDateTime(gvAuditEmpPlanningDetl.Rows[j].Cells["Date"].Value).ToString("dd/MMM/yyyy") + "'";                                                                 
                        }

                        iRes = objSQLdb.ExecuteSaveData(strCommand);

                        strCommand = "";
                        iRes = 0;
                        objSQLdb = new SQLDB();

                        for (int i = 0; i < gvAuditEmpPlanningDetl.Rows.Count; i++)
                        {

                            strCommand += "INSERT INTO AUDIT_DR_PLANNING(ADRP_APPL_NO " +
                                                                     ", ADRP_EORA_CODE " +
                                                                     ", ADRP_TRN_TYPE " +
                                                                     ", ADRP_LOCATION " +
                                                                     ", ADRP_DATE " +
                                                                     ", ADRP_REMARKS " +
                                                                     ", ADRP_CREATED_BY " +
                                                                     ", ADRP_CREATED_DATE " +
                                                                     ")VALUES " +
                                                                     "(" + EmpApplNo +
                                                                     "," + EmpEcode +
                                                                     ",'" + gvAuditEmpPlanningDetl.Rows[i].Cells["BranchType"].Value.ToString() +
                                                                     "','" + gvAuditEmpPlanningDetl.Rows[i].Cells["LocationCode"].Value.ToString() +
                                                                     "','" + Convert.ToDateTime(gvAuditEmpPlanningDetl.Rows[i].Cells["Date"].Value).ToString("dd/MMM/yyyy") +
                                                                     "','" + gvAuditEmpPlanningDetl.Rows[i].Cells["Remarks"].Value.ToString() +
                                                                     "','" + CommonData.LogUserId +
                                                                     "',getdate())";
                        }
                    }

                    if (strCommand.Length > 5)
                    {
                        iRes = objSQLdb.ExecuteSaveData(strCommand);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                    gvAuditEmpPlanningDetl.Rows.Clear();
                    dtpFromdate.Value = DateTime.Today;
                    dtpToDate.Value = DateTime.Today;
                    txtEcodeSearch.Text = "";
                    FillEmpAuditPlanningDetl();

                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }                        
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            txtEcodeSearch.Text = "";
            gvAuditEmpPlanningDetl.Rows.Clear();
            dtpFromdate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;
            //txtDept.Text = "";
            //txtEmpDesg.Text = "";
            cbAuditEmployees.SelectedIndex = 0;

        }     

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void gvAuditEmpPlanningDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            objSQLdb = new SQLDB();           
            string strCmd = "";
            string strComp = "";
            string strBranchCode = "";
            string strBranType = "";
            string strBranName = "";
            string strRmarks = "";
            DateTime dtpPlanDate;
            DataGridViewRow rowIndex;                           

            if (e.RowIndex >= 0)
            {             
               
                if (e.ColumnIndex == gvAuditEmpPlanningDetl.Columns["Edit"].Index)
                {
                    if (Convert.ToBoolean(gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {                       
                        rowIndex = gvAuditEmpPlanningDetl.Rows[e.RowIndex];

                        strComp = gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["CompCode"].Value.ToString();
                        strBranchCode = gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["LocationCode"].Value.ToString();
                        strBranType = gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["BranchType"].Value.ToString();
                        dtpPlanDate = Convert.ToDateTime(gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["Date"].Value.ToString());
                        strRmarks = gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                        AddAuditDRPlanningDetl AuditDRPlan = new AddAuditDRPlanningDetl(dtpPlanDate,strComp,strBranchCode,strBranType,strRmarks,rowIndex);
                        AuditDRPlan.objAuditDRPlanning = this;
                        AuditDRPlan.ShowDialog();

                        OrderSlNo();
                    }
                }

                if (e.ColumnIndex == gvAuditEmpPlanningDetl.Columns["Del"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {

                                               
                        strCmd = "DELETE FROM AUDIT_DR_PLANNING WHERE ADRP_EORA_CODE=" + Convert.ToInt32(cbAuditEmployees.Text.ToString().Split('-')[0]) +
                                        " AND ADRP_DATE='" + Convert.ToDateTime(gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["Date"].Value).ToString("dd/MMM/yyyy") +
                                        "'AND ADRP_TRN_TYPE='" + gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["BranchType"].Value.ToString() +
                                        "'AND ADRP_LOCATION='" + gvAuditEmpPlanningDetl.Rows[e.RowIndex].Cells["LocationCode"].Value.ToString() + "' ";

                        int iRes = objSQLdb.ExecuteSaveData(strCmd);

                        DataGridViewRow dgvr = gvAuditEmpPlanningDetl.Rows[e.RowIndex];
                        gvAuditEmpPlanningDetl.Rows.Remove(dgvr);

                    
                            MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OrderSlNo();
                            //FillEmpAuditPlanningDetl();

                        
                    }
                }

            }
        }

        private void OrderSlNo()
        {
            if (gvAuditEmpPlanningDetl.Rows.Count > 0)
            {
                for (int i = 0; i < gvAuditEmpPlanningDetl.Rows.Count; i++)
                {
                    gvAuditEmpPlanningDetl.Rows[i].Cells["SlNO"].Value = (i + 1).ToString();
                }
            }
        }
    
      
    }
}
