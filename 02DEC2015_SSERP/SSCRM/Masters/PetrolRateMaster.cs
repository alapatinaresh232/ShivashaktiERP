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
using SSCRM;
namespace SSCRM
{
    public partial class PetrolRateMaster : Form
    {
        private SQLDB objSQLData = null;
        bool flag = false;
        public PetrolRateMaster()
        {
            InitializeComponent();
        }

        private void PetrolRateMaster_Load(object sender, EventArgs e)
        {
            FillCompanyData();           
            //FillStateData();
         
        }
        private void FillCompanyData()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE ACTIVE ='T'";
                dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLData = null;
                dt = null;
            }

        }
        private void FillStateData()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT sm_state_code,sm_state FROM state_mas ";
                dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    CbState.DataSource = dt;
                    CbState.DisplayMember = "sm_state";
                    CbState.ValueMember = "sm_state_code";
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLData = null;
                dt = null;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();


        }
        private DataTable Get_PetrolRateDetails(string sCompCode, string sStateCode, string sEffDate)
        {
            objSQLData = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataTable dt = new DataTable();
            DataTable dtDtl = new DataTable();

            try
            {
                param[0] = objSQLData.CreateParameter("@xCMPNY", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLData.CreateParameter("@xState", DbType.String, sStateCode, ParameterDirection.Input);
                param[2] = objSQLData.CreateParameter("@xEffDate", DbType.String, sEffDate, ParameterDirection.Input);
                dt = objSQLData.ExecuteDataSet("Get_Petrol_Rate_details", CommandType.StoredProcedure, param).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLData = null;
            }
            return dt;
        }
        private void CbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0 && CbState.SelectedIndex > 0)
            { 
                FillPetrolDataToGrid();
            }
            else
            {
                gvPetrolDetails.Rows.Clear();
            }
        }

        private void FillPetrolDataToGrid()
        {
            objSQLData = new SQLDB();
            DataTable dtPetrolDetails = new DataTable();
            gvPetrolDetails.Rows.Clear();
            int intRow = 1;
            
            try
            {
                dtPetrolDetails =  Get_PetrolRateDetails(cbCompany.SelectedValue.ToString(),CbState.SelectedValue.ToString(),Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy"));



                if (dtPetrolDetails.Rows.Count > 0)
                {

                    for (int i = 0; i < dtPetrolDetails.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellCompanyCode = new DataGridViewTextBoxCell();
                        cellCompanyCode.Value = dtPetrolDetails.Rows[i]["CompCode"];
                        tempRow.Cells.Add(cellCompanyCode);

                        DataGridViewCell companyName = new DataGridViewTextBoxCell();
                        companyName.Value = dtPetrolDetails.Rows[i]["CompName"];
                        tempRow.Cells.Add(companyName);

                        DataGridViewCell cellStateCode = new DataGridViewTextBoxCell();
                        cellStateCode.Value = dtPetrolDetails.Rows[i]["StateCode"];
                        tempRow.Cells.Add(cellStateCode);

                        DataGridViewCell cellStateName = new DataGridViewTextBoxCell();
                        cellStateName.Value = dtPetrolDetails.Rows[i]["StateName"];
                        tempRow.Cells.Add(cellStateName);

                        DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
                        cellBranchCode.Value = dtPetrolDetails.Rows[i]["BranchCode"];
                        tempRow.Cells.Add(cellBranchCode);

                        DataGridViewCell cellBranchName = new DataGridViewTextBoxCell();
                        cellBranchName.Value = dtPetrolDetails.Rows[i]["BranchName"];
                        tempRow.Cells.Add(cellBranchName);

                        DataGridViewCell cellEffDate = new DataGridViewTextBoxCell();
                        cellEffDate.Value = dtPetrolDetails.Rows[i]["EffDate"];
                        tempRow.Cells.Add(cellEffDate);

                        DataGridViewCell cellLitre = new DataGridViewTextBoxCell();
                        cellLitre.Value = '1';
                        tempRow.Cells.Add(cellLitre);
                        if( Convert.ToString(dtPetrolDetails.Rows[i]["ApprovalFlag"].ToString()) =="")
                        {
                            DataGridViewCell cellPrice = new DataGridViewTextBoxCell();
                            cellPrice.Value = '0';
                            tempRow.Cells.Add(cellPrice);
                           
                        }


                      else  if (Convert.ToInt32(dtPetrolDetails.Rows[i]["ApprovalFlag"].ToString()) == 0)
                        {
                            DataGridViewCell cellPrice = new DataGridViewTextBoxCell();
                            cellPrice.Value = dtPetrolDetails.Rows[i]["PetrolPrice"];
                            tempRow.Cells.Add(cellPrice);
                            cellPrice.ReadOnly = true;
                            flag = true;


                        }

                        else
                        {
                            DataGridViewCell cellPrice = new DataGridViewTextBoxCell();
                            cellPrice.Value = dtPetrolDetails.Rows[i]["PetrolPrice"];
                            tempRow.Cells.Add(cellPrice);
                            cellPrice.ReadOnly = true;
                        }
                        if (dtPetrolDetails.Rows[i]["ApprovalFlag"].ToString() == "")
                        {
                            DataGridViewCell cellApprovalFlg = new DataGridViewTextBoxCell();
                            cellApprovalFlg.Value = '0';
                            tempRow.Cells.Add(cellApprovalFlg);
                            btnSave.Enabled = true;
                        }
                        else if (Convert.ToInt32(dtPetrolDetails.Rows[i]["ApprovalFlag"].ToString()) == 0)
                        {
                            DataGridViewCell cellApprovalFlg = new DataGridViewTextBoxCell();
                            cellApprovalFlg.Value = dtPetrolDetails.Rows[i]["ApprovalFlag"];
                            tempRow.Cells.Add(cellApprovalFlg);
                            btnSave.Enabled = true;
                        }
                        else
                        {
                            DataGridViewCell cellApprovalFlg = new DataGridViewTextBoxCell();
                            cellApprovalFlg.Value = dtPetrolDetails.Rows[i]["ApprovalFlag"];
                            tempRow.Cells.Add(cellApprovalFlg);                          
                            btnSave.Enabled = false;
                        }

                        gvPetrolDetails.Rows.Add(tempRow);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLData = null;
                dtPetrolDetails = null;
            }
        }

          private bool CheckData()
          {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
                flag = false;
            }
            if (CbState.SelectedIndex == 0 || CbState.SelectedIndex == -1)
            {
                MessageBox.Show("Select State", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
                flag = false;
            }         
            if (gvPetrolDetails.Rows.Count > 0)
            {
                for (int iVar = 0; iVar < gvPetrolDetails.Rows.Count; iVar++)
                {

                    if (Convert.ToDouble(gvPetrolDetails.Rows[iVar].Cells["Price"].Value.ToString())== 0.00)
                    {
                        flag = false;
                        MessageBox.Show("Please enter Petrol Price", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }                 
                }
            }     
            

            return flag;

        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            string strCommand = "";
            int iRes = 0;
         
            if (CheckData() == true)
            {
                try
                {
                 
                    if (gvPetrolDetails.Rows.Count > 0)
                    {

                       
                            for (int i = 0; i < gvPetrolDetails.Rows.Count; i++)
                            {
                                if (flag == false)
                                {
                                    strCommand += "INSERT INTO PETROL_RATE_MASTER (PRM_COMP_CODE " +
                                                                                     ",PRM_STATE_CODE " +
                                                                                     ",PRM_BRANCH_CODE " +
                                                                                     ",PRM_WEF_DATE" +
                                                                                     ",PRM_PETROL_QTY" +
                                                                                     ",PRM_PRICE" +
                                                                                     ",PRM_APPROVAL_FLAG" +
                                                                                     ",PRM_CREATED_BY" +
                                                                                     ",PRM_CREATED_DATE" +
                                                                                     ")VALUES" +
                                                                                     "('" + cbCompany.SelectedValue.ToString() +
                                                                                     "','" + CbState.SelectedValue.ToString() +
                                                                                     "','" + gvPetrolDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                                                                                     "','" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                                                                     "'," + Convert.ToDouble(gvPetrolDetails.Rows[i].Cells["PetrolQty"].Value.ToString()) +
                                                                                     "," + Convert.ToDouble(gvPetrolDetails.Rows[i].Cells["Price"].Value.ToString()) +
                                                                                     "," + gvPetrolDetails.Rows[i].Cells["ApprovalFlg"].Value.ToString() +
                                                                                      ",'" + CommonData.LogUserId +
                                                                                     "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "')";

                                }
                                else
                                {
                                    strCommand += "UPDATE PETROL_RATE_MASTER SET   PRM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                                                                 "',PRM_STATE_CODE='" + CbState.SelectedValue.ToString() +
                                                                                 "',PRM_BRANCH_CODE='" + gvPetrolDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                                                                                 "',PRM_WEF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                                                                                 "',PRM_PETROL_QTY='" + Convert.ToDouble(gvPetrolDetails.Rows[i].Cells["PetrolQty"].Value.ToString()) +
                                                                                 "',PRM_PRICE='" + Convert.ToDouble(gvPetrolDetails.Rows[i].Cells["Price"].Value.ToString()) +
                                                                                 "',PRM_MODIFIED_BY='" + CommonData.LogUserId +
                                                                                 "',PRM_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                                                 "' WHERE PRM_COMP_CODE ='" + cbCompany.SelectedValue.ToString() +
                                                                                 "' AND PRM_STATE_CODE ='" + CbState.SelectedValue.ToString() +
                                                                                 "' AND PRM_BRANCH_CODE= '" + gvPetrolDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                                                                                 "' AND PRM_WEF_DATE= '" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "'";


                                }
                               
                        }



                        

                        iRes = objSQLData.ExecuteSaveData(strCommand);

                    }

                }
                catch (Exception ex)
                {


                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                  
                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }


                else
                {
                    MessageBox.Show("Data not saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

       
        private void btnClear_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            CbState.SelectedIndex = -1;
            gvPetrolDetails.Rows.Clear();
            btnSave.Enabled = true;
            dtpEffDate.Value = DateTime.Now;
            flag = false;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            string strCommand = "";
            int iRes = 0;

            if (CheckData() == true)
            {
                try
                {
                    for (int i = 0; i < gvPetrolDetails.Rows.Count; i++)
                    {
                        strCommand += "UPDATE PETROL_RATE_MASTER SET   PRM_APPROVAL_FLAG='1'" +
                                                                                 " WHERE PRM_COMP_CODE ='" + cbCompany.SelectedValue.ToString() +
                                                                                 "' AND PRM_STATE_CODE ='" + CbState.SelectedValue.ToString() +
                                                                                 "' AND PRM_BRANCH_CODE= '" + gvPetrolDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                                                                                 "' AND PRM_WEF_DATE= '" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +"'";



                        //strCommand += "UPDATE PETROL_RATE_MASTER SET   PRM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                        //                                                        "',PRM_STATE_CODE='" + CbState.SelectedValue.ToString() +
                        //                                                        "',PRM_BRANCH_CODE='" + gvPetrolDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                        //                                                        "',PRM_WEF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") +
                        //                                                        "',PRM_PETROL_QTY='" + Convert.ToDouble(gvPetrolDetails.Rows[i].Cells["PetrolQty"].Value.ToString()) +
                        //                                                        "',PRM_PRICE='" + Convert.ToDouble(gvPetrolDetails.Rows[i].Cells["Price"].Value.ToString()) +
                        //                                                        "',PRM_MODIFIED_BY='" + CommonData.LogUserId +
                        //                                                        "',PRM_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                        //                                                        "',PRM_APPROVAL_FLAG='1" +
                        //                                                        "' WHERE PRM_COMP_CODE ='" + cbCompany.SelectedValue.ToString() +
                        //                                                        "' AND PRM_STATE_CODE ='" + CbState.SelectedValue.ToString() +
                        //                                                        "' AND PRM_BRANCH_CODE= '" + gvPetrolDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                        //                                                        "' AND PRM_WEF_DATE= '" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "'";

                    } 

                        

                        iRes = objSQLData.ExecuteSaveData(strCommand);

                    

                }
                catch (Exception ex)
                {


                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {

                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                    btnClear_Click(null, null);
                }


                else
                {
                    MessageBox.Show("Data not saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtpEffDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex>0 && CbState.SelectedIndex>0)
            {
                FillPetrolDataToGrid();
            }
        }

        private void gvPetrolDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 11)
                {
                    if (Convert.ToInt32(gvPetrolDetails.Rows[e.RowIndex].Cells["ApprovalFlg"].Value.ToString()).Equals(0))
                    {
                        if (gvPetrolDetails.Rows[e.RowIndex].Cells["EffDate"].Value.ToString() != "")
                        {

                            cbCompany.SelectedValue = gvPetrolDetails.Rows[e.RowIndex].Cells["CompanyCode"].Value.ToString();
                            CbState.SelectedValue = gvPetrolDetails.Rows[e.RowIndex].Cells["StateCode"].Value.ToString();
                            dtpEffDate.Value = Convert.ToDateTime(gvPetrolDetails.Rows[e.RowIndex].Cells["EffDate"].Value.ToString());
                            gvPetrolDetails.Rows[e.RowIndex].Cells["Price"].ReadOnly = false;
                            flag = true;
                        }
                    }

                    else
                    {
                        //gvPetrolDetails.Rows[e.RowIndex].Cells["Price"].ReadOnly = teue;

                        MessageBox.Show(" This Data not Editable", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }

        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillStateData();
            }
            else
            {
                CbState.DataSource = null;
                gvPetrolDetails.Rows.Clear();
            }
        }
    }
}
