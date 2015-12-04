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
using SSTrans;

namespace SSCRM 
{
    public partial class AddAuditDRPlanningDetl : Form
    {
       
        private Master objMaster = null;

        AuditDB objAuditdb = null;
        HRInfo objHRdb = null;
        private SQLDB objSQLdb = null;
        private UtilityDB objUtility = null;
        string CompCode = "";
        string BranCode = "";
        string BranType = "";
        string strRemarks = "";
        DateTime planDate;
        DataGridViewRow RowIndex;
        double dTotDays = 0;
      
        bool flagUpdate = false;
        
        public AuditDRPlanning objAuditDRPlanning;

                    
        public AddAuditDRPlanningDetl()
        {
            InitializeComponent();           
        }

        public AddAuditDRPlanningDetl(DateTime dtpPlanDate,string sCompCode,string sBranCode,string sBranType,string sRemarks,DataGridViewRow dgvr)
        {
            InitializeComponent();
            CompCode = sCompCode;
            BranCode = sBranCode;
            BranType = sBranType;
            strRemarks = sRemarks;
            RowIndex = dgvr;
            planDate = dtpPlanDate;
            flagUpdate = true;
          
        }

        private void AddAuditDRPlanningDetl_Load(object sender, EventArgs e)
        {
            FillBranchTypes();
            FillCompanyData();
            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;
            

            if (cbBranchType.SelectedIndex > 0)
            {
                FillBranchData();
            }
                      
            if (flagUpdate==true)
            {
                
                if (CompCode != "")
                {
                    cbCompany.SelectedValue = CompCode;
                }

                cbBranchType.Text = BranType;


                if (BranCode != "")
                {
                    cbBranch.SelectedValue = BranCode;
                }

                dtpFromDate.Value = Convert.ToDateTime(planDate);
                dtpToDate.Value = Convert.ToDateTime(planDate);

                txtRemarks.Text = strRemarks;
            }

          
        }

        private void FillBranchTypes()
        {
            objAuditdb = new AuditDB();
          
            try
            {
                cbBranchType.DataSource = objAuditdb.dtBranchType();                
                cbBranchType.DisplayMember = "name";
                cbBranchType.ValueMember = "type";

                cbBranchType.SelectedValue = CommonData.BranchType;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objAuditdb = null;
            }
        }
        private void FillCompanyData()
        {
            DataSet ds = null;
            Security objComp = new Security();
            try
            {
                ds = new DataSet();
                ds = objComp.GetCompanyDataSet();
                DataTable dtCompany = ds.Tables[0];
                if (dtCompany.Rows.Count > 0)
                {
                    cbCompany.DisplayMember = "CM_Company_Name";
                    cbCompany.ValueMember = "CM_Company_Code";
                    cbCompany.DataSource = dtCompany;
                }
                cbCompany.SelectedValue = CommonData.CompanyCode;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objComp = null;
                
            }

        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > -1 && cbBranchType.SelectedIndex>-1)
                {

                    string strCommand = "SELECT BRANCH_NAME,BRANCH_CODE  FROM BRANCH_MAS " +
                                        " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "'and BRANCH_TYPE='"+ cbBranchType.SelectedValue.ToString()  +
                                        "' Order by BRANCH_NAME ";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BRANCH_CODE";
                }

                cbBranch.SelectedValue = CommonData.BranchCode;

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



        //private bool CheckDuplicatePlanning()
        //{
        //    bool flagCheck = true;
        //    string strDate;
        //    string strType = "";

        //    strDate = Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy");
        //    if (cbBranchType.SelectedIndex == 6 || cbBranchType.SelectedIndex == 7 || cbBranchType.SelectedIndex == 8)
        //    {
        //        strType = cbBranchType.Text.ToString();
        //    }
        //    else
        //    {
        //        strType = cbBranch.Text.ToString();
        //    }


        //    if (((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows.Count; i++)
        //        {
        //            if (strDate.Equals(((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows[i].Cells["Date"].Value.ToString()))
        //            {
        //                if (strType.Equals(((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows[i].Cells["Location"].Value.ToString()))
        //                {
        //                    flagCheck = false;
        //                    MessageBox.Show("Already Exists", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return flagCheck;
        //                }
        //            }
        //        }
        //    }



        //    return flagCheck;
        //}

             

        private void cbBranchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranchType.SelectedIndex > 0)
            {
                FillBranchData();
                if (cbBranch.Items.Count != 0)
                {
                    cbBranch.SelectedIndex = 0;
                }
            }
            else
            {
                cbBranch.DataSource = null;
            }
            
        }

       

        private bool CheckData()
        {
            bool flag = true;

           
            if (cbCompany.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;
            }
            if (cbBranchType.SelectedIndex == -1 || cbBranchType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Location Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranchType.Focus();
                return flag;
            }
            if (cbBranchType.SelectedIndex == 1 || cbBranchType.SelectedIndex == 2 || cbBranchType.SelectedIndex == 3 || cbBranchType.SelectedIndex == 4 || cbBranchType.SelectedIndex == 8)
            {
                if (cbBranch.SelectedIndex == 0 || cbBranch.SelectedIndex == -1)
                {
                    flag = false;
                    MessageBox.Show("Please Select Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbBranch.Focus();
                    return flag;
                }
            }
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Dates", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFromDate.Focus();
                return flag;
            }

            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvDRPlanDetl = null;

            if (CheckData() == true)
            {
                dgvDRPlanDetl = ((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl;
                AddEmpDRPlanDetailsToGrid(dgvDRPlanDetl);

                flagUpdate = false;
                this.Close();
            }

        }
        private void CalculateTotDays()
        {

            if ((dtpFromDate.Value > dtpToDate.Value))
            {
                dtpFromDate.Value = dtpToDate.Value;
            }
            else
            {
                double TotDays = (dtpToDate.Value - dtpFromDate.Value).TotalDays;
                dTotDays = TotDays;
            }
        }

        #region "GRIDVIEW DETAILS"

        public void AddEmpDRPlanDetailsToGrid(DataGridView dgvDRPlanDetl)
        {
            int intRow = 0;
            bool IsItemExisted = false;
            intRow = dgvDRPlanDetl.Rows.Count + 1;

            string sCompCode = "";
            string sBranchCode = "";
            string sBranchName = "";
            string sDate = "";

            try
            {
                CalculateTotDays();
                if (cbBranchType.SelectedIndex == 5 || cbBranchType.SelectedIndex == 6 || cbBranchType.SelectedIndex == 7 || cbBranchType.SelectedIndex == 9 || cbBranchType.SelectedIndex == 10)
                {
                    
                        sCompCode = "";
                        sBranchName = cbBranchType.Text.ToString();
                        sBranchCode = "";
                    
                }
                else
                {
                    sCompCode = cbCompany.SelectedValue.ToString();
                    sBranchCode = cbBranch.SelectedValue.ToString();
                    sBranchName = cbBranch.Text.ToString();
                }
                if (flagUpdate == true)
                {
                    ((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows.Remove(RowIndex);
                }           

                for (int i = 0; i <= dTotDays; i++)
                {
                    IsItemExisted = false;                          

                    sDate = Convert.ToDateTime(dtpFromDate.Value.AddDays(i)).ToString("dd/MMM/yyyy");
                   
                    if (((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows.Count > 0)
                    {
                        for (int j = 0; j < ((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows.Count; j++)
                        {
                            if (sDate.Equals(((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows[j].Cells["Date"].Value.ToString()))
                            {
                                if (sBranchName.Equals(((AuditDRPlanning)objAuditDRPlanning).gvAuditEmpPlanningDetl.Rows[j].Cells["Location"].Value.ToString()))
                                {
                                    IsItemExisted = true;
                                }
                            }
                        }
                    }
                    if (IsItemExisted == false)
                    {

                        DataGridViewRow tempRow = new DataGridViewRow();


                        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                        cellSlNo.Value = intRow;
                        tempRow.Cells.Add(cellSlNo);


                        DataGridViewCell cellCompCode = new DataGridViewTextBoxCell();
                        cellCompCode.Value = sCompCode;
                        tempRow.Cells.Add(cellCompCode);

                        DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
                        cellBranchCode.Value = sBranchCode;
                        tempRow.Cells.Add(cellBranchCode);

                        DataGridViewCell cellTrnType = new DataGridViewTextBoxCell();
                        cellTrnType.Value = cbBranchType.Text.ToString();
                        tempRow.Cells.Add(cellTrnType);

                        DataGridViewCell cellPlanDate = new DataGridViewTextBoxCell();
                        cellPlanDate.Value = Convert.ToDateTime(dtpFromDate.Value.AddDays(i)).ToString("dd/MMM/yyyy");
                        tempRow.Cells.Add(cellPlanDate);

                        DataGridViewCell cellLocationName = new DataGridViewTextBoxCell();
                        cellLocationName.Value = sBranchName;
                        tempRow.Cells.Add(cellLocationName);

                        DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                        cellRemarks.Value = txtRemarks.Text.ToString().Replace("\'", "");
                        tempRow.Cells.Add(cellRemarks);


                        intRow = intRow + 1;
                        dgvDRPlanDetl.Rows.Add(tempRow);
                        sDate = "";

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
                   
              

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtRemarks.Text = "";
            dtpFromDate.Value = DateTime.Today;
            cbCompany.SelectedIndex = 0;
            cbBranchType.SelectedIndex = 0;
            cbBranch.SelectedIndex = -1;
            
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value < dtpToDate.Value)
            {
                double TotDays = (dtpToDate.Value - dtpFromDate.Value).TotalDays;
                dTotDays = TotDays;
            }
            else
            {
                dTotDays = 0;
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value < dtpToDate.Value)
            {
                double TotDays = (dtpToDate.Value - dtpFromDate.Value).TotalDays;
                dTotDays = TotDays;
            }
            else
            {
                dTotDays = 0;
            }
        }
           
       

       
    }
}
