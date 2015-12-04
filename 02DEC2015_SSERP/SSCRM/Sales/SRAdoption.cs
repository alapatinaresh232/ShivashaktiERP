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
using SSCRMDB;
using SSAdmin;
using SSTrans;


namespace SSCRM
{
    public partial class SRAdoption : Form
    {
     
        private SQLDB objsqldb = null;
        private string strECode = string.Empty;
        DataGridViewRow dgvr;
        
        public SRAdoption()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void SRAdoption_Load(object sender, EventArgs e)
        {
            dtpDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
            gvAdoptionDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            gvAdoptionDetails.DefaultCellStyle.ForeColor = Color.Black;
            FillEmployeeDetails();
            
        }

        private DataSet GetDetails(string Ecode, string Company, string BranchCode, string DocMonth)
        {
            //DataSet ds = new DataSet();
            //objsqldb = new SQLDB();
            //SqlParameter[] param = new SqlParameter[4];
            //try
            //{
               
            //    param[0] = objsqldb.CreateParameter("@xCompany ",DbType.String, Company, ParameterDirection.Input);
            //    param[1] = objsqldb.CreateParameter("@xBranchCode",DbType.String,BranchCode, ParameterDirection.Input);
            //    param[2] = objsqldb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
            //    param[3] = objsqldb.CreateParameter("@xDocMonth",DbType.String, DocMonth, ParameterDirection.Input);
                 
            //    ds = objsqldb.ExecuteDataSet("GetMappedGCSRList_TMAbove",CommandType.StoredProcedure,param);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    param = null;
            //    objsqldb = null;
            //}

            //return ds;
            DataSet ds = new DataSet();
            objsqldb = new SQLDB();
            //SqlParameter[] param = new SqlParameter[4];
            //try
            //{
            //    param[0] = objsqldb.CreateParameter("@xCompany", DbType.String, "VNF", ParameterDirection.Input);
            //    param[1] = objsqldb.CreateParameter("@xBranchCode   ", DbType.String, "VNFAPBVAN", ParameterDirection.Input);
            //    param[2] = objsqldb.CreateParameter("@xEcode", DbType.String, Ecode, ParameterDirection.Input);
            //    param[3] = objsqldb.CreateParameter("@xDocMonth", DbType.String, "NOV2015", ParameterDirection.Input);

            //    ds = objsqldb.ExecuteDataSet("GetMappedGCSRList_TMAbove", CommandType.StoredProcedure, param);
            //}
            //catch (Exception EX)
            //{
            //    MessageBox.Show(EX.ToString());
            //}
            //finally
            //{
            //    param = null;
            //    objsqldb = null;
            //}
            string strSQL = "exec GetMappedGCSRList_TMAbove '" + Company + "','" + BranchCode + "','" + Ecode + "','" + DocMonth + "'";
            ds = objsqldb.ExecuteDataSet(strSQL); 
            return ds;
        }

        public void FillSRAdoptionDetails()
        {
            objsqldb = new SQLDB();
            DataTable dt = new DataTable();
            gvAdoptionDetails.Rows.Clear();

            if (strECode.Length > 0 && txtBranch.Text.Length > 0)
            {
                try
                {
                    dt = GetDetails(strECode, txtCompany.Tag.ToString(), txtBranch.Tag.ToString(), Convert.ToDateTime(dtpDocMonth.Text.ToString()).ToString("MMMyyyy")).Tables[0];
                    if(dt.Rows.Count >0)
                    gvAdoptionDetails.Columns["PreviousMP"].HeaderText = dt.Rows[0]["PrevDocMonth"].ToString()+" " + "Points";
                    
                  for (int i = 0; i < dt.Rows.Count; i++)
                  {
                      DataGridViewRow temprow = new DataGridViewRow();
                      DataGridViewCell cellGno= new DataGridViewTextBoxCell();
                      cellGno.Value = dt.Rows[i]["GroupSlNo"];
                      temprow.Cells.Add(cellGno);

                      DataGridViewCell cellSRSlno = new DataGridViewTextBoxCell();
                      cellSRSlno.Value = dt.Rows[i]["SRSlNo"];
                      temprow.Cells.Add(cellSRSlno);
                      
                      DataGridViewCell Cellsno = new DataGridViewTextBoxCell();
                      Cellsno.Value = dt.Rows[i]["GroupSlNo"] + "." + dt.Rows[i]["SRSlNo"];
                      temprow.Cells.Add(Cellsno);

                      DataGridViewCell cellSRCamp = new DataGridViewTextBoxCell();
                      cellSRCamp.Value = dt.Rows[i]["SRCamp"];
                      temprow.Cells.Add(cellSRCamp);

                      DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                      cellEcode.Value = dt.Rows[i]["SREcode"];
                      temprow.Cells.Add(cellEcode);

                      DataGridViewCell cellGEcode = new DataGridViewTextBoxCell();
                      cellGEcode.Value = dt.Rows[i]["GCCode"];
                      temprow.Cells.Add(cellGEcode);

                      DataGridViewCell cellName = new DataGridViewTextBoxCell();
                      cellName.Value = dt.Rows[i]["SRName"]; 
                      temprow.Cells.Add(cellName);

                      DataGridViewCell cellCompanyCode = new DataGridViewTextBoxCell();
                      cellCompanyCode.Value = txtCompany.Tag;
                      temprow.Cells.Add(cellCompanyCode);

                      DataGridViewCell cellBranCode = new DataGridViewTextBoxCell();
                      cellBranCode.Value = txtBranch.Tag;
                      temprow.Cells.Add(cellBranCode);

                      DataGridViewCell cellDesig = new DataGridViewTextBoxCell();
                      cellDesig.Value = dt.Rows[i]["SRDesig"]; 
                      temprow.Cells.Add(cellDesig);
                     
                      DataGridViewCell cellDoj = new DataGridViewTextBoxCell();
                      cellDoj.Value = Convert.ToDateTime(dt.Rows[i]["SRDoj"]).ToShortDateString(); 
                      temprow.Cells.Add(cellDoj);

                      DataGridViewCell cellLos= new DataGridViewTextBoxCell();
                      cellLos.Value = dt.Rows[i]["LOSChar"]; 
                      temprow.Cells.Add(cellLos);

                      DataGridViewCell cellPMPoints = new DataGridViewTextBoxCell();
                      cellPMPoints.Value = dt.Rows[i]["PrevDocPoints"]; 
                      temprow.Cells.Add(cellPMPoints);

                      DataGridViewCell cellAdoptionStatus = new DataGridViewTextBoxCell();
                      cellAdoptionStatus.Value = dt.Rows[i]["AdoptionStatus"];
                      temprow.Cells.Add(cellAdoptionStatus);

                      gvAdoptionDetails.Rows.Add(temprow);

                      if (gvAdoptionDetails.Rows[i].Cells["ADPStatus"].Value.ToString() == "Adopted")
                      {
                          DataGridViewCell chkbox = (DataGridViewCell)gvAdoptionDetails.Rows[i].Cells["Adoptionchk"];
                          chkbox.Value = true;
                          chkbox.ReadOnly = true;

                          DataGridViewCell cellDelete = (DataGridViewCell)gvAdoptionDetails.Rows[i].Cells["Delete"];
                          DataGridViewImageCell DelImgCell = cellDelete as DataGridViewImageCell;
                          DelImgCell.Value = Properties.Resources.actions_delete;
                          DelImgCell.ReadOnly = false;

                      }
                      if (gvAdoptionDetails.Rows[i].Cells["ADPStatus"].Value.ToString() == "Disable")
                      {
                          DataGridViewCell chkbox = (DataGridViewCell)gvAdoptionDetails.Rows[i].Cells["Adoptionchk"];
                          DataGridViewCheckBoxCell chkboxCell = chkbox as DataGridViewCheckBoxCell;
                          chkboxCell.Value = false;
                          chkboxCell.FlatStyle = FlatStyle.Flat;
                          chkboxCell.Style.ForeColor = Color.DarkGray;
                          chkboxCell.ReadOnly = true;
                                
                      }
                      if (gvAdoptionDetails.Rows[i].Cells["SREcode"].Value.ToString() == (gvAdoptionDetails.Rows[i].Cells["GCEcode"].Value.ToString()))
                      {
                          gvAdoptionDetails.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Navy;
                          gvAdoptionDetails.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Bold);
                         
                      }                    
                     
                  }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objsqldb = null;
                    dt = null;
                }
            }
            
        }

        
        private int SaveAdoptionDetails()
        {
            objsqldb = new SQLDB();
            int iresult = 0;
            string strcmd = "";
            try
            {
                strcmd += "Delete from SALES_SR_ADOPTIONS Where SSA_EORA_CODE='" + cbEmployees.Text.ToString().Split('-')[0] +
                         "' and SSA_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Text.ToString()).ToString("MMMyyyy") +
                         "' and SSA_BRANCH_CODE='" + txtBranch.Tag.ToString() + "' ";

                for (int i = 0; i < gvAdoptionDetails.Rows.Count; i++)
                {
                    if ((Convert.ToBoolean(gvAdoptionDetails.Rows[i].Cells["Adoptionchk"].Value) == true) && ((gvAdoptionDetails.Rows[i].Cells["ADPStatus"].Value.ToString() == "Enable")||(gvAdoptionDetails.Rows[i].Cells["ADPStatus"].Value.ToString() == "Adopted")))
                      
                    {
                        strcmd += "INSERT INTO SALES_SR_ADOPTIONS (SSA_COMPANY_CODE, " +
                                                                " SSA_BRANCH_CODE, " +
                                                                " SSA_FIN_YEAR, " +
                                                                " SSA_DOC_MONTH, " +
                                                                " SSA_EORA_CODE ," +
                                                                " SSA_SR_EORA_CODE, " +
                                                                " SSA_SR_ECODE ," +
                                                                " SSA_CREATED_BY ," +
                                                                " SSA_CREATED_DATE " +
                                                                ")VALUES('" + txtCompany.Tag.ToString() +
                                                                "','" + txtBranch.Tag.ToString() +
                                                                "','" + CommonData.FinancialYear +
                                                                "','" + Convert.ToDateTime(dtpDocMonth.Text.ToString()).ToString("MMMyyyy") +
                                                                "','" + cbEmployees.Text.ToString().Split('-')[0] +
                                                                "','" + gvAdoptionDetails.Rows[i].Cells["SREcode"].Value.ToString() +
                                                                "','" + gvAdoptionDetails.Rows[i].Cells["SREcode"].Value.ToString() +
                                                                "','" + CommonData.LogUserId + "',  getdate() )";
                    }


                }


                if (strcmd.Length > 0)
                {
                    iresult = objsqldb.ExecuteSaveData(strcmd);

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return iresult;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (SaveAdoptionDetails() > 0)
                {
                    SaveAdoptionDetails();
                    MessageBox.Show("Data Saved Successfully", "SRAdoption", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEcode.Text = "";
                    cbEmployees.Text = "";
                    txtCompany.Text = "";
                    txtCompany.Text = "";
                    txtBranch.Text = "";
                    txtSearch.Text = "";
                    gvAdoptionDetails.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SRAdoption", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }          
        } 

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcode.Text = "";
            cbEmployees.Text = "";
            txtCompany.Text = "";
            txtCompany.Text = "";
            txtBranch.Text = "";
            txtSearch.Text = "";
            gvAdoptionDetails.Rows.Clear();
        }
        private bool CheckData()
        {
            bool bFlag = true;
            if (cbEmployees.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Valid Ecode", "SRAdoption", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcode.Focus();
                return bFlag;
            }
           return bFlag;
        }

        private void gvAdoptionDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ires = 0;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvAdoptionDetails.Columns["Delete"].Index)
                {
                    if((gvAdoptionDetails.Rows[e.RowIndex].Cells["ADPStatus"].Value.ToString() == "Adopted")) 
                    {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        dgvr = gvAdoptionDetails.Rows[e.RowIndex];

                        objsqldb = new SQLDB();
                        string strcmd = "";
                        try
                        {
                            strcmd = "Delete from SALES_SR_ADOPTIONS Where SSA_EORA_CODE='" +cbEmployees.Text.ToString().Split('-')[0] +
                                     "' and SSA_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Text.ToString()).ToString("MMMyyyy") +
                                     "' and SSA_BRANCH_CODE='" + txtBranch.Tag.ToString() +
                                     "' and SSA_SR_EORA_CODE='" + gvAdoptionDetails.Rows[e.RowIndex].Cells["SREcode"].Value.ToString() +"' ";
                            ires = objsqldb.ExecuteSaveData(strcmd);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (ires > 0)
                        {
                            MessageBox.Show("Data Deleted Successfully", "SRAdoption", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillSRAdoptionDetails();

                        }
                        else
                        {
                            MessageBox.Show("Data not Deleted", "SRAdoption", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                   }
                }
            }
        }

        private void dtpDocMonth_ValueChanged(object sender, EventArgs e)
        {
            FillEmployeeDetails();
            //FillSRAdoptionDetails();
        }
        private void SearchSREcode(string searchString)
        {
            if (searchString.Trim().Length > 0)
            {
                string searchValue = txtSearch.Text;

                gvAdoptionDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    foreach (DataGridViewRow row in gvAdoptionDetails.Rows)
                    {
                        if (row.Cells["SREcode"].Value.ToString().Equals(searchValue))
                        {
                            
                            row.Selected = true;
                            gvAdoptionDetails.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                        else
                        {
                            row.Selected = false;
                        }
                            
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

      
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                SearchSREcode(txtSearch.Text.ToString());
            }  
            
        }

        private void SRAdoption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && Control.ModifierKeys == Keys.Control)
            {
                lblSearch.Visible = true;
                txtSearch.Visible = true;
                txtSearch.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                lblSearch.Visible = false;
                txtSearch.Visible = false;
                txtSearch.Text = "";
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }
        }

        public DataSet GetEmployeeEcodes()
        {
            objsqldb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objsqldb.CreateParameter("@CompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objsqldb.CreateParameter("@xBranCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objsqldb.CreateParameter("@xDoc_month", DbType.String, Convert.ToDateTime(dtpDocMonth.Text.ToString()).ToString("MMMyyyy"), ParameterDirection.Input);
                param[3] = objsqldb.CreateParameter("@xECODE", DbType.String, txtEcode.Text, ParameterDirection.Input);


                ds = objsqldb.ExecuteDataSet("GetTMAndAboveDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objsqldb = null;
            }
            return ds;
        }
        private void FillEmployeeDetails()
        {
            objsqldb = new SQLDB();
            DataTable dt = new DataTable();
            cbEmployees.DataBindings.Clear();

            try
            {

                dt = GetEmployeeEcodes().Tables[0];

                if (dt.Rows.Count > 0)
                {
                    cbEmployees.DataSource = dt;
                    cbEmployees.DisplayMember = "MEMBER_NAME";
                    cbEmployees.ValueMember = "ValMem";
             
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objsqldb = null;
                dt = null;
            }

        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcode.Text.Length > 4)
            {
               
                    FillEmployeeDetails();
                
            }
            else
            {
                gvAdoptionDetails.Rows.Clear();
                //cbEmployees.Text = "";
                txtCompany.Text = "";
                txtBranch.Text = "";
            }
        }

        private void cbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmployees.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[0];
                txtCompany.Text = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[1];
                txtCompany.Tag = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[2];
                txtBranch.Tag = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[3];
                txtBranch.Text = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[4];

            }
            else
            {
                strECode = "0";
            }
            try { Convert.ToInt32(strECode); }
            catch { strECode = "0"; }
            if (Convert.ToInt32(strECode) != 0)
            {
                FillSRAdoptionDetails();
            }
            else
            {
                gvAdoptionDetails.Rows.Clear();
                //cbEmployees.Text = "";
                txtCompany.Text = "";
                txtBranch.Text = "";
            }
        }

        private void btnPrintAdoptionList_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SalesSRAdoptionDetails";
            ReportViewer childReportViewer = new ReportViewer(txtCompany.Tag.ToString(), txtBranch.Tag.ToString(), strECode, Convert.ToDateTime(dtpDocMonth.Text.ToString()).ToString("MMMyyyy"));
            childReportViewer.Show();
        }     

    }
}
