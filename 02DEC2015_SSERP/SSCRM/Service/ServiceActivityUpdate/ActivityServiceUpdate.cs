using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class ActivityServiceUpdate : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRInfo = null;
        ServiceDB objServiceDB = null;
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;
        DataSet dsEmployee;
        private string strOrderNo = "";
        private string stProdName = "";
        private string sQty = "", sActivityName = "";
        private Int32 nActivityId = 0;
        private Int32 nCntActId = 0;
        private double NoOfReplaces = 0;
        private string sEcode = "", sCompany = "", sBranCode = "", strDate = "", strTrnNo = "", strRefNo = "", sFinYear = "";
        DateTime dtpActivityDate;
        
        public ActivityServiceUpdate()
        {
            InitializeComponent();
            btnAddCountingDetails.Visible = false;
            btnAddReplDetails.Visible = false;
        }
        public ActivityServiceUpdate(string CompCode,string BranCode,string strEcode,string sDate)
        {
            InitializeComponent();
            btnAddCountingDetails.Visible = false;
            btnAddReplDetails.Visible = false;
            sEcode = strEcode;
            sCompany = CompCode;
            sBranCode = BranCode;
            strDate = sDate;
        }
        public ActivityServiceUpdate(string CompCode, string BranCode, string strEcode, string FinYear,string sTrnNo,string sRefNo)
        {
            InitializeComponent();
            btnAddCountingDetails.Visible = false;
            btnAddReplDetails.Visible = false;
            sEcode = strEcode;
            sCompany = CompCode;
            sBranCode = BranCode;           
            strTrnNo = sTrnNo;
            strRefNo = sRefNo;
            sFinYear = FinYear;
        }

        private void ActivityServiceUpdate_Load(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            DataSet dsFinYear = new DataSet();
            DataTable dtfinYear = new DataTable();
            DataTable dtBranch = new DataTable();
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            

            objHRInfo = new HRInfo();
            if (sCompany.Length > 0)
            {
                 dtBranch = objHRInfo.GetAllBranchList(sCompany, "BR", "").Tables[0];
            }
            else
            {
                 dtBranch = objHRInfo.GetAllBranchList(CommonData.CompanyCode, "BR", "").Tables[0];
            }
            UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objHRInfo = null;
            objServiceDB = new ServiceDB();
            dsEmployee = objServiceDB.GetInvocieInfoforService("", "", "","", 103);
            objServiceDB = null;
            objSQLdb = new SQLDB();
            dsFinYear = objSQLdb.ExecuteDataSet("SELECT DISTINCT FY_FIN_YEAR FROM FIN_YEAR");
            dtfinYear = dsFinYear.Tables[0];
            cmbFinYear.DataSource = null;
            if (dtfinYear.Rows.Count > 0)
            {
                cmbFinYear.DataSource = dtfinYear;
                cmbFinYear.DisplayMember = "FY_FIN_YEAR";
                cmbFinYear.ValueMember = "FY_FIN_YEAR";
                
                cmbFinYear.SelectedIndex = 0;
                cmbFinYear.SelectedValue = CommonData.FinancialYear;

                if (sFinYear.Length > 0)
                    cmbFinYear.SelectedValue = sFinYear;

            }
            btnUpdate.Enabled = false;
            btnClear.Enabled = false;
            btnAddReplDetails.Visible = false;
            btnAddCountingDetails.Visible = false;

            if (sBranCode.Length > 0)
            {
                cmbBranch.SelectedValue = sBranCode.Split('@')[0];
            }
            else
            {
                if (CommonData.BranchType == "BR")
                    cmbBranch.SelectedValue = CommonData.BranchCode;
                else
                    cmbBranch.SelectedIndex = 0;
            }

            if (strTrnNo.Length > 0)
            {
                GetAllInvoicdeData(strTrnNo,"");
                txtOrderNo_Validated(null,null);
            }
        }

       
        public void GetAllInvoicdeData(string OrderNo,string InvNo)
        {
           
            objServiceDB = new ServiceDB();
            DataSet dvFil = new DataSet();
            DataSet ds=new DataSet();
           
            try
            {
                dvFil = objServiceDB.GetInvocieInfoforService(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, OrderNo, InvNo, 101);

                if (dvFil.Tables[0].Rows.Count > 0)
                {
                    btnAddReplDetails.Visible = false;
                    btnUpdate.Enabled = true;
                    btnClear.Enabled = true;
                    btnAddCountingDetails.Visible = false;
                   
                    txtInvoiceNo.Text = dvFil.Tables[0].Rows[0]["InvNo"].ToString();
                    txtOrderNo.Text = dvFil.Tables[0].Rows[0]["OrderNo"].ToString();
                    txtDocMonth.Text = dvFil.Tables[0].Rows[0]["DocMonth"].ToString();
                    txtCampName.Text = dvFil.Tables[0].Rows[0]["CampName"].ToString();
                    txtVillage.Text = dvFil.Tables[0].Rows[0]["Village"].ToString();                   
                    txtSrEname.Text = dvFil.Tables[0].Rows[0]["SRName"].ToString();
                    txtMandal.Text = dvFil.Tables[0].Rows[0]["Mandal"].ToString();
                    txtDistrict.Text = dvFil.Tables[0].Rows[0]["District"].ToString();
                    txtState.Text = dvFil.Tables[0].Rows[0]["State"].ToString();
                    txtPin.Text = dvFil.Tables[0].Rows[0]["Pin"].ToString();
                    txtHouseNo.Text = dvFil.Tables[0].Rows[0]["HouseNo"].ToString();
                    txtLandMark.Text = dvFil.Tables[0].Rows[0]["LandMark"].ToString();
                    txtMobileNo.Text = dvFil.Tables[0].Rows[0]["MobileNO"].ToString();
                    txtLanLineNo.Text = dvFil.Tables[0].Rows[0]["LandLineNo"].ToString();
                    txtCustomerName.Text = dvFil.Tables[0].Rows[0]["FarmerName"].ToString();
                    txtRelationName.Text = dvFil.Tables[0].Rows[0]["ForgName"].ToString();
                    cbRelation.Text = dvFil.Tables[0].Rows[0]["fatherOrHus"].ToString();
                    FillActivityDetail(OrderNo, InvNo);
                    //FillProductDetails(OrderNo, InvNo);
                    CheckingForNewReplacement();
                    CheckingForAddAnotherCountingDetails();

                }
                else
                {
                    txtVillage.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";
                    txtHouseNo.Text = "";
                    txtLandMark.Text = "";
                    txtMobileNo.Text = "";
                    txtLanLineNo.Text = "";
                    txtCustomerName.Text = "";
                    txtRelationName.Text = "";
                    cbRelation.SelectedIndex = 0;
                    dgvActivity.Rows.Clear();
                    gvProductDetails.Rows.Clear();
                    btnUpdate.Enabled = false;
                    btnClear.Enabled = false;
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dvFil = null;
                objServiceDB = null;
                
            }
        }           


        public void Clear()
        {            
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            txtHouseNo.Text = "";
            txtLandMark.Text = "";
            txtMobileNo.Text = "";
            txtLanLineNo.Text = "";
            txtCustomerName.Text = "";
            txtRelationName.Text = "";
            cbRelation.SelectedIndex = 0;
            dgvActivity.Rows.Clear();
            gvProductDetails.Rows.Clear();
            btnUpdate.Enabled = false;
            btnClear.Enabled = false;
            txtInvoiceNo.Text = "";
            txtOrderNo.Text = "";
            txtSrEname.Text = "";
        }

        private void FillActivityDetail(string OrderNo,string InvNo)
        {
            objServiceDB = new ServiceDB();
            int intRow = 1;
            dgvActivity.Rows.Clear();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (txtInvoiceNo.Text != "")
            {
                try
                {
                    ds = objServiceDB.GetInvocieInfoforService(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, OrderNo, InvNo, 104);
                    dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {                       
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["ActivityId"].ToString().Length > 0)
                            {
                        
                                DataGridViewRow tempRow = new DataGridViewRow();

                                DataGridViewCell cellActSlNo = new DataGridViewTextBoxCell();
                                cellActSlNo.Value = (i+1).ToString();
                                tempRow.Cells.Add(cellActSlNo);

                                DataGridViewCell cellActivityID = new DataGridViewTextBoxCell();
                                cellActivityID.Value = dt.Rows[i]["ActivityId"];
                                tempRow.Cells.Add(cellActivityID);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                cellMainProductID.Value = dt.Rows[i]["ProdId"];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellProdName = new DataGridViewTextBoxCell();
                                cellProdName.Value = dt.Rows[i]["ProductName"].ToString();
                                tempRow.Cells.Add(cellProdName);

                                DataGridViewCell cellInvQty = new DataGridViewTextBoxCell();
                                cellInvQty.Value = dt.Rows[i]["Qty"].ToString();
                                tempRow.Cells.Add(cellInvQty);

                                DataGridViewCell cellTargetDate = new DataGridViewTextBoxCell();
                                if (dt.Rows[i]["TargetDate"].ToString() != "")
                                    cellTargetDate.Value = Convert.ToDateTime(dt.Rows[i]["TargetDate"].ToString()).ToString("dd/MMM/yyyy");
                                else
                                    cellTargetDate.Value = "";
                                tempRow.Cells.Add(cellTargetDate);
                                
                                DataGridViewCell cellCreatedDate = new DataGridViewTextBoxCell();
                                if (dt.Rows[i]["CreatedDate"].ToString() != "")
                                    cellCreatedDate.Value = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("dd/MMM/yyyy");
                                else
                                    cellCreatedDate.Value = "";
                                tempRow.Cells.Add(cellCreatedDate);

                                DataGridViewCell cellApprovedBy = new DataGridViewTextBoxCell();
                                cellApprovedBy.Value = dt.Rows[i]["ApprovedBy"].ToString();
                                tempRow.Cells.Add(cellApprovedBy);

                                DataGridViewCell cellApprovedName = new DataGridViewTextBoxCell();
                                cellApprovedName.Value = dt.Rows[i]["ApprovedName"].ToString();
                                tempRow.Cells.Add(cellApprovedName);

                                DataGridViewCell cellActivityName = new DataGridViewTextBoxCell();
                                cellActivityName.Value = dt.Rows[i]["ActivityName"].ToString();
                                tempRow.Cells.Add(cellActivityName);

                                DataGridViewCell cellActualQty = new DataGridViewTextBoxCell();
                                cellActualQty.Value = dt.Rows[i]["ActualQty"].ToString();
                                tempRow.Cells.Add(cellActualQty);

                                DataGridViewCell cellProductID = new DataGridViewTextBoxCell();
                                cellProductID.Value = dt.Rows[i]["InvoiceNumber"];
                                tempRow.Cells.Add(cellProductID);                                                             

                                DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                                cellEcode.Value = dt.Rows[i]["EmpName"].ToString();
                                tempRow.Cells.Add(cellEcode);


                                DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                if (dt.Rows[i]["ActualDate"].ToString() != "")
                                {
                                    cellDessc.Value = Convert.ToDateTime(dt.Rows[i]["ActualDate"]).ToString("dd/MMM/yyyy");
                                }
                                else
                                    cellDessc.Value = "";
                                tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellActQty = new DataGridViewTextBoxCell();
                                cellActQty.Value = dt.Rows[i]["ActivityQty"].ToString();
                                tempRow.Cells.Add(cellActQty);

                                DataGridViewCell cellActivityMode = new DataGridViewTextBoxCell();
                                cellActivityMode.Value = dt.Rows[i]["ActivityMode"].ToString();
                                tempRow.Cells.Add(cellActivityMode);
                                
                                DataGridViewCell cellCropName = new DataGridViewTextBoxCell();
                                cellCropName.Value = dt.Rows[i]["CropName"].ToString();
                                tempRow.Cells.Add(cellCropName);

                                DataGridViewCell cellCropAcres = new DataGridViewTextBoxCell();
                                cellCropAcres.Value = dt.Rows[i]["CropAcres"].ToString();
                                tempRow.Cells.Add(cellCropAcres);

                                DataGridViewCell cellFarmerOpinion = new DataGridViewTextBoxCell();
                                cellFarmerOpinion.Value = dt.Rows[i]["FarmerOpinion"].ToString();
                                tempRow.Cells.Add(cellFarmerOpinion);

                                DataGridViewCell cellAoSuggestion = new DataGridViewTextBoxCell();
                                cellAoSuggestion.Value = dt.Rows[i]["AoSuggestion"].ToString();
                                tempRow.Cells.Add(cellAoSuggestion);

                                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                                cellRemarks.Value = dt.Rows[i]["Remarks"].ToString();
                                tempRow.Cells.Add(cellRemarks);

                                intRow = intRow + 1;
                                dgvActivity.Rows.Add(tempRow);
                            }
                        }
                    }
                    else
                    {
                        dgvActivity.Rows.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            }
        }

        private void FillProductDetails(string OrderNo,string InvNo)
        {
            objServiceDB = new ServiceDB();
            int intRow = 1;
            gvProductDetails.Rows.Clear();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
          
                try
                {
                    ds = objServiceDB.GetInvocieInfoforService(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, OrderNo, InvNo, 104);
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["ProductId"].ToString().Length > 0)
                            {

                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                cellMainProductID.Value = dt.Rows[i]["ProductId"];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                cellMainProduct.Value = dt.Rows[i]["ProductName"];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                cellDessc.Value = dt.Rows[i]["CategoryName"];
                                tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                if (dt.Rows[i]["Qty"].ToString() != "")
                                {
                                    cellPoints.Value = Convert.ToDouble(dt.Rows[i]["Qty"]).ToString("f");
                                }
                                else
                                {
                                    cellPoints.Value = "0.00";
                                }
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                gvProductDetails.Rows.Add(tempRow);



                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objServiceDB = null;
                    dt = null;
                }
           
        }

        private void dgvActivity_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex > 0)
            //{
            //    try
            //    {
            //        //if (UtilityFunctions.IsNumeric(dgvActivity.Rows[e.RowIndex].Cells["Ecode"].Value.ToString()) == false)
            //        //    dgvActivity.Rows[e.RowIndex].Cells["Ecode"].Value = "";
            //        if (Convert.ToString(dgvActivity.Rows[e.RowIndex].Cells["Ecode"].Value) != "")
            //        {
            //            DataRow[] drow = dsEmployee.Tables[0].Select("ECode=" + dgvActivity.Rows[e.RowIndex].Cells["Ecode"].Value);
            //            if (drow.Length > 0)
            //                dgvActivity.Rows[e.RowIndex].Cells["Ecode"].Value = drow[0].ItemArray[1].ToString();
            //            else
            //                dgvActivity.Rows[e.RowIndex].Cells["Ecode"].Value = "";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}

            //if (dgvActivity.Rows[e.RowIndex].Cells["Qty"].Value.ToString() == "")
            //{
            //    dgvActivity.Rows[e.RowIndex].Cells["Qty"].Value= "0.00";
            //}

        }

        private void dgvActivity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            stProdName = "";
            sQty = "";
                                       
            if (e.RowIndex >= 0)
            {
                if (dgvActivity.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(dgvActivity.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        string ActID = dgvActivity.Rows[e.RowIndex].Cells[dgvActivity.Columns["ActivityID"].Index].Value.ToString();
                        string InvoiceID = dgvActivity.Rows[e.RowIndex].Cells[dgvActivity.Columns["InvNo"].Index].Value.ToString();
                       
                        string strOrderNo = "";
                        string sProdId = dgvActivity.Rows[e.RowIndex].Cells[dgvActivity.Columns["ProductId"].Index].Value.ToString();
                        strOrderNo = txtOrderNo.Text.ToString();
                        if (dgvActivity.Rows[e.RowIndex].Cells[dgvActivity.Columns["CreatedDate"].Index].Value.ToString() != "")
                        {

                            string sDays = (DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dgvActivity.Rows[e.RowIndex].Cells[dgvActivity.Columns["CreatedDate"].Index].Value).ToString("dd/MM/yyyy"))).TotalDays.ToString();
                            if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dgvActivity.Rows[e.RowIndex].Cells[dgvActivity.Columns["CreatedDate"].Index].Value).ToString("dd/MM/yyyy"))).TotalDays > 15)
                            {
                                if (CommonData.LogUserId.ToUpper() != "ADMIN")
                                {
                                    MessageBox.Show("This Data Can't Modify", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                    
                                }
                                else
                                {
                                    if (ActID == "14" || ActID == "15" || ActID == "16" || ActID == "17")
                                    {
                                        frmTechnicalServiceDetails TechService = new frmTechnicalServiceDetails(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, ActID, InvoiceID, strOrderNo,sEcode,strDate,sProdId);
                                        TechService.objActivityServiceUpdate = this;
                                        TechService.ShowDialog();
                                    }
                                    else
                                    {
                                        ECodesSearch oECodesSearch = new ECodesSearch(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, ActID, InvoiceID, strOrderNo,sEcode,strDate,sProdId);
                                        oECodesSearch.objActivityServiceUpdate = this;
                                        oECodesSearch.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                if (ActID == "14" || ActID == "15" || ActID == "16" || ActID == "17")
                                {
                                    frmTechnicalServiceDetails TechService = new frmTechnicalServiceDetails(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, ActID, InvoiceID, strOrderNo, sEcode, strDate,sProdId);
                                    TechService.objActivityServiceUpdate = this;
                                    TechService.ShowDialog();
                                }
                                else
                                {
                                    ECodesSearch oECodesSearch = new ECodesSearch(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, ActID, InvoiceID, strOrderNo, sEcode, strDate,sProdId);
                                    oECodesSearch.objActivityServiceUpdate = this;
                                    oECodesSearch.ShowDialog();
                                }
                            }                          
                        }
                        else
                        {
                            if (ActID == "14" || ActID == "15" || ActID == "16" || ActID == "17")
                            {
                                frmTechnicalServiceDetails TechService = new frmTechnicalServiceDetails(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, ActID, InvoiceID, strOrderNo, sEcode, strDate,sProdId);
                                TechService.objActivityServiceUpdate = this;
                                TechService.ShowDialog();
                            }
                            else
                            {
                                ECodesSearch oECodesSearch = new ECodesSearch(cmbBranch.SelectedValue.ToString(), cmbFinYear.Text, ActID, InvoiceID, strOrderNo, sEcode, strDate,sProdId);
                                oECodesSearch.objActivityServiceUpdate = this;
                                oECodesSearch.ShowDialog();
                            }
                        }
                                               
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            if (sEcode.Length > 0)
            {
                this.Close();
                this.Dispose();
                objEmployeeDARWithTourBills.FillEmployeeActivityDetails(Convert.ToInt32(sEcode), objEmployeeDARWithTourBills.dtpTrnDate.Value.ToString("dd/MMM/yyyy"));
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInvoiceNo.Text = "";
            Clear();
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int intRec = 0;
                bool iChk = true;
                for (int i = 0; i < dgvActivity.Rows.Count; i++)
                {
                    if (Convert.ToString(dgvActivity.Rows[i].Cells["Ecode"].Value) == "")
                    {
                        iChk = false;
                    }
                    if (Convert.ToString(dgvActivity.Rows[i].Cells["ActivityDt"].Value) != "")
                    {
                        if ((dgvActivity.Rows[i].Cells["ActivityDt"].Value.ToString().IndexOf(" ") >= 0) || (dgvActivity.Rows[i].Cells["ActivityDt"].Value.ToString().Length < 10))
                        {
                            MessageBox.Show("Enter Invoice date!");
                            iChk = false;
                        }
                        else
                        {
                            if (Convert.ToInt32(Convert.ToDateTime(dgvActivity.Rows[i].Cells["ActivityDt"].Value.ToString()).ToString("yyyy")) < 1950)
                            {
                                MessageBox.Show("Enter valid  Date !");
                                iChk = false;
                            }
                        }
                    }
                    else
                        iChk = false;
                    if (Convert.ToString(dgvActivity.Rows[i].Cells["Remarks"].Value) == "")
                    {
                        iChk = false;
                    }
                    if (Convert.ToString(dgvActivity.Rows[i].Cells["Qty"].Value) == "")
                    {
                        dgvActivity.Rows[i].Cells["Qty"].Value = "0";
                    }
                }
                string sqlUpdate = "";
                if (iChk == false)
                {
                    MessageBox.Show("Please enter valid infomration", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    for (int i = 0; i < dgvActivity.Rows.Count; i++)
                    {
                        string[] sqlEcode = dgvActivity.Rows[i].Cells["Ecode"].Value.ToString().Split('-');
                        sqlUpdate += "UPDATE SERVICES_TNA SET TNA_ACTUAL_DATE='" + Convert.ToDateTime(dgvActivity.Rows[i].Cells["ActivityDt"].Value).ToString("dd/MMM/yyyy") +
                                    "',TNA_ATTEND_BY_ECODE=" + sqlEcode[0].ToString() +
                                    ",TNA_FARMER_REMARKS='" + dgvActivity.Rows[i].Cells["Remarks"].Value.ToString() +
                                    "',TNA_ACTIVITY_QTY='" + Convert.ToDouble(dgvActivity.Rows[i].Cells["Qty"].Value).ToString("0.00") +
                                   "', TNA_CREATED_BY='" + CommonData.LogUserId + "', TNA_CREATED_DATE=getdate() "+
                                   " WHERE TNA_BRANCH_CODE='" + cmbBranch.SelectedValue + "' AND TNA_FIN_YEAR='" + cmbFinYear.Text +
                                    "' AND TNA_ACTIVITY_ID=" + dgvActivity.Rows[i].Cells["Activityid"].Value + " AND TNA_INVOICE_NUMBER=" + txtInvoiceNo.Text;

                    }
                }
                objSQLdb = new SQLDB();
                if (sqlUpdate.Length > 10)
                {
                    intRec = objSQLdb.ExecuteSaveData(sqlUpdate);
                }
                objSQLdb = null;
                if(intRec>0)
                MessageBox.Show("Data saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOrderNo_Validated(object sender, EventArgs e)
        {
            strOrderNo = "";
            
           
            if (Convert.ToString(txtOrderNo.Text).Trim().Length > 0)
            {
                strOrderNo = Convert.ToInt32(txtOrderNo.Text).ToString("00000");
                strOrderNo = Convert.ToString(strOrderNo);
                
                GetAllInvoicdeData(strOrderNo,"");
                FillProductDetails(strOrderNo, "");
                FillActivityDetail(strOrderNo, "");                
            }
            else
            {
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtMobileNo.Text = "";
                txtLanLineNo.Text = "";
                txtCustomerName.Text = "";
                txtRelationName.Text = "";
                cbRelation.SelectedIndex = 0;
                dgvActivity.Rows.Clear();
                gvProductDetails.Rows.Clear();               
                btnUpdate.Enabled = false;
                btnClear.Enabled = false;
                txtDocMonth.Text = "";
                txtCampName.Text = "";
                txtSrEname.Text = "";
               
            }
        }           

     
        private void txtInvoiceNo_Validated(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
                GetAllInvoicdeData("", txtInvoiceNo.Text);
                FillProductDetails("", txtInvoiceNo.Text);
                FillActivityDetail("", txtInvoiceNo.Text);
            }
            else
            {
               
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtMobileNo.Text = "";
                txtLanLineNo.Text = "";
                txtCustomerName.Text = "";
                txtRelationName.Text = "";
                cbRelation.SelectedIndex = 0;
                dgvActivity.Rows.Clear();
                gvProductDetails.Rows.Clear();                
                btnUpdate.Enabled = false;
                btnClear.Enabled = false;
                txtCampName.Text = "";
                txtDocMonth.Text = "";
                txtSrEname.Text = "";

            }
        
        }

        private void txtInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAddReplDetails_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

                DialogResult dlgResult = MessageBox.Show("Do you want Add Another Replacement Details?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        strCmd = "SELECT distinct TNA_COMPANY_CODE CompCode " +
                                ",TNA_STATE_CODE StateCode " +
                                ",TNA_BRANCH_CODE BranCode " +
                                ",TNA_FIN_YEAR FinYear " +
                                ",DATEADD(DAY,SAM_LEAD_TIME,SIH_INVOICE_DATE) TargetDate " +
                                ", TNA_DOC_MONTH DocMonth " +
                                ",TNA_ORDER_NUMBER OrderNo " +
                                ",TNA_INVOICE_NUMBER InvNo " +
                                ",TNA_INVOICE_SL_NO InvSlNo " +
                                ",TNA_PRODUCT_ID ProdId " +
                                ",TNA_QTY Qty " +
                                ",pm_product_name ProdName " +
                                " FROM SERVICES_TNA " +
                                " INNER JOIN PRODUCT_MAS ON pm_product_id=TNA_PRODUCT_ID " +
                                " INNER JOIN SALES_INV_HEAD ON SIH_COMPANY_CODE=TNA_COMPANY_CODE " +
                                " and SIH_BRANCH_CODE=TNA_BRANCH_CODE and SIH_FIN_YEAR=TNA_FIN_YEAR " +
                                " and SIH_ORDER_NUMBER=TNA_ORDER_NUMBER " +
                                " and SIH_INVOICE_NUMBER=TNA_INVOICE_NUMBER " +
                                " INNER JOIN SERVICES_ACTIVITIES_MAS on SAM_ACTIVITY_ID=" + nActivityId + " " +
                                "  WHERE TNA_COMPANY_CODE='" + CommonData.CompanyCode +
                                "' and TNA_BRANCH_CODE='" + cmbBranch.SelectedValue.ToString() +
                                "' and TNA_FIN_YEAR='" + cmbFinYear.Text.ToString() +
                                "' and TNA_ORDER_NUMBER='" + txtOrderNo.Text.ToString() +
                                "' and TNA_PRODUCT_ID='TKPRYL0000' and TNA_INVOICE_NUMBER='" + txtInvoiceNo.Text.ToString() + "'";

                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //if (Convert.ToDateTime(dt.Rows[0]["TargetDate"].ToString()) < DateTime.Today)
                            //{
                            AddReplacementDetails ReplDetl = new AddReplacementDetails(dt, sActivityName, nActivityId, sEcode, strDate);
                                ReplDetl.objActivityServiceUpdate = this;
                                ReplDetl.ShowDialog();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Replacement-2 Target Date is Greater than today Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
        }
        private void CheckingForNewReplacement()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            nActivityId = 0;
            NoOfReplaces=0;
            try
            {

                strCmd = "SELECT max(TNA_ACTIVITY_ID) as ActivityId,COUNT(TNA_ACTIVITY_ID) as NoOfRepl " +
                         " FROM SERVICES_TNA " +
                         " INNER JOIN SERVICES_ACTIVITIES_MAS ON SAM_ACTIVITY_ID=TNA_ACTIVITY_ID and SAM_ACTIVITY_NAME LIKE 'REPLACE%' " +
                         " WHERE TNA_COMPANY_CODE='" + CommonData.CompanyCode +
                         "' and TNA_BRANCH_CODE='" + cmbBranch.SelectedValue.ToString() +
                         "' and TNA_FIN_YEAR='" + cmbFinYear.Text.ToString() +
                         "' and TNA_ORDER_NUMBER='" + txtOrderNo.Text.ToString() +
                         "' and TNA_INVOICE_NUMBER='" + txtInvoiceNo.Text.ToString() +
                         "' and TNA_PRODUCT_ID='TKPRYL0000' and TNA_ATTEND_BY_ECODE is not null";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    NoOfReplaces = Convert.ToDouble(dt.Rows[0]["NoOfRepl"].ToString());
                    if (dt.Rows[0]["ActivityId"].ToString() != "" && NoOfReplaces < 3)
                    {
                        btnAddReplDetails.Visible = true;
                        if (dt.Rows[0]["ActivityId"].ToString().Equals("13"))
                        {
                            nActivityId = 18;
                            sActivityName = "REPLACEMENT 2";
                        }
                        else
                        {
                            //NoOfReplaces = Convert.ToInt32(dt.Rows[0]["NoOfRepl"].ToString());                           
                            //nActivityId = Convert.ToInt32(dt.Rows[0]["ActivityId"].ToString());
                            nActivityId = 19;
                            sActivityName = "REPLACEMENT 3";
                        }
                    }
                    else
                    {
                        btnAddReplDetails.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void CheckingForAddAnotherCountingDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            nCntActId = 0;            
            try
            {

                strCmd = "SELECT Top 1 TNA_ACTIVITY_ID ActivityId,isnull(TNA_ACTIVITY_MODE,'') ActivityMode  " +
                         " FROM SERVICES_TNA " +
                         " INNER JOIN SERVICES_ACTIVITIES_MAS ON SAM_ACTIVITY_ID=TNA_ACTIVITY_ID and SAM_ACTIVITY_NAME LIKE 'COUNTING%' " +
                         " WHERE TNA_COMPANY_CODE='" + CommonData.CompanyCode +
                         "'and TNA_BRANCH_CODE='" + cmbBranch.SelectedValue.ToString() +
                         "'and TNA_FIN_YEAR='" + cmbFinYear.Text.ToString() +
                         "'and TNA_ORDER_NUMBER='" + txtOrderNo.Text.ToString() +
                         "'and TNA_INVOICE_NUMBER='" + txtInvoiceNo.Text.ToString() +
                         "'and TNA_PRODUCT_ID='TKPRYL0000' and TNA_ATTEND_BY_ECODE is not null " +
                         " ORDER BY TNA_ACTIVITY_ID DESC ";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ActivityId"].ToString() == "12" && dt.Rows[0]["ActivityMode"].ToString() != "")
                    {
                        btnAddCountingDetails.Visible = true;
                        nCntActId = 20;
                        sActivityName = "COUNTING-2";
                    }
                    else
                    {
                        btnAddCountingDetails.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cmbBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cmbBranch.Focus();
                return flag;
            }
            if (cmbFinYear.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Financial Year", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbFinYear.Focus();
                return flag;
            }
            if (txtOrderNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Order No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOrderNo.Focus();
                return flag;
            }
            if (txtInvoiceNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Invoice No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInvoiceNo.Focus();
                return flag;
            }
            return flag;
        }

        private void btnAddressUpdate_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            if (CheckData() == true)
            {
                try
                {
                    strCmd = "exec SSCRM_REP_CRO_SALES_INV_PRINT  '" + cmbFinYear.Text + "','" + CommonData.CompanyCode + "','" + cmbBranch.SelectedValue.ToString() + "'," + Convert.ToInt32(txtInvoiceNo.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        CustomerAddressUpdate CustomerAddress = new CustomerAddressUpdate(dt);
                        CustomerAddress.objActivityServiceUpdate = this;
                        CustomerAddress.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Order Number/Invoice Number","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void txtOrderNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOrderNo.Text.Length == 0)
            {
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtMobileNo.Text = "";
                txtLanLineNo.Text = "";
                txtCustomerName.Text = "";
                txtRelationName.Text = "";
                cbRelation.SelectedIndex = 0;
                dgvActivity.Rows.Clear();
                gvProductDetails.Rows.Clear();
                txtInvoiceNo.Text = "";
                btnUpdate.Enabled = false;
                btnClear.Enabled = false;
                txtDocMonth.Text = "";
                txtCampName.Text = "";
            }
        }

        private void txtInvoiceNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtInvoiceNo.Text.Length == 0)
            {
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtMobileNo.Text = "";
                txtLanLineNo.Text = "";
                txtCustomerName.Text = "";
                txtRelationName.Text = "";
                cbRelation.SelectedIndex = 0;
                dgvActivity.Rows.Clear();
                gvProductDetails.Rows.Clear();
                txtOrderNo.Text = "";
                btnUpdate.Enabled = false;
                btnClear.Enabled = false;
                txtDocMonth.Text = "";
                txtCampName.Text = "";
            }
        }

        private void btnAddCountingDetails_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            DialogResult dlgResult = MessageBox.Show("Do you want Add Another COUNTING Details?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                try
                {
                    strCmd = "SELECT distinct TNA_COMPANY_CODE CompCode " +
                            ",TNA_STATE_CODE StateCode " +
                            ",TNA_BRANCH_CODE BranCode " +
                            ",TNA_FIN_YEAR FinYear " +
                            ",DATEADD(DAY,SAM_LEAD_TIME,SIH_INVOICE_DATE) TargetDate " +
                            ", TNA_DOC_MONTH DocMonth " +
                            ",TNA_ORDER_NUMBER OrderNo " +
                            ",TNA_INVOICE_NUMBER InvNo " +
                            ",TNA_INVOICE_SL_NO InvSlNo " +
                            ",TNA_PRODUCT_ID ProdId " +
                            ",TNA_QTY Qty " +
                            ",pm_product_name ProdName " +
                            " FROM SERVICES_TNA " +
                            " INNER JOIN PRODUCT_MAS ON pm_product_id=TNA_PRODUCT_ID " +
                            " INNER JOIN SALES_INV_HEAD ON SIH_COMPANY_CODE=TNA_COMPANY_CODE " +
                            " and SIH_BRANCH_CODE=TNA_BRANCH_CODE and SIH_FIN_YEAR=TNA_FIN_YEAR " +
                            " and SIH_ORDER_NUMBER=TNA_ORDER_NUMBER " +
                            " and SIH_INVOICE_NUMBER=TNA_INVOICE_NUMBER " +
                            " INNER JOIN SERVICES_ACTIVITIES_MAS on SAM_ACTIVITY_ID=" + nCntActId + " " +
                            "  WHERE TNA_COMPANY_CODE='" + CommonData.CompanyCode +
                            "' and TNA_BRANCH_CODE='" + cmbBranch.SelectedValue.ToString() +
                            "' and TNA_FIN_YEAR='" + cmbFinYear.Text.ToString() +
                            "' and TNA_ORDER_NUMBER='" + txtOrderNo.Text.ToString() +
                            "' and TNA_PRODUCT_ID='TKPRYL0000' and TNA_INVOICE_NUMBER='" + txtInvoiceNo.Text.ToString() + "'";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //if (Convert.ToDateTime(dt.Rows[0]["TargetDate"].ToString()) < DateTime.Today)
                        //{
                        AddReplacementDetails ReplDetl = new AddReplacementDetails(dt, sActivityName, nCntActId,sEcode,strDate);
                        ReplDetl.objActivityServiceUpdate = this;
                        ReplDetl.ShowDialog();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Replacement-2 Target Date is Greater than today Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

      

      
    }
}
