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
using SSAdmin;
using System.IO;

namespace SSCRM
{
    public partial class VehicleLoanRecovery : Form
    {
        private SQLDB objDB = null;
        private double payments = 0.00;
        private double loanAmt = 0.00;
        private double toPay = 0.00;
        private bool bIsUpdate = false;
        private int iTrnID = 0;
        public VehicleLoanRecovery()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void VehicleLoanRecovery_Load(object sender, EventArgs e)
        {
            dgvLoanRecovery.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            //dtpDocmentation.Value = Convert.ToDateTime(CommonData.DocMonth);
            dtpPaymentDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            FillReceiptType();
            txtDocMonth.Text = CommonData.DocMonth;
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Trim().Length > 4)
            {
                GetRecruiterName();
                GetVehicleData();
            }
            else
            {
                txtEmpName.Text = "";
                lblDataNotFound.Visible = false;
                dgvLoanRecovery.Rows.Clear();
            }
        }

        private void GetVehicleData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            dgvLoanRecovery.Rows.Clear();
            if (txtEcodeSearch.Text.Length > 0)
            {
                try
                {
                    string sqlText = "SELECT HVLH_COMPANY_CODE,HVLH_BRANCH_CODE,HVLH_EORA_CODE,HVLH_VEHICLE_REG_NUMBER" +
                                        ",HVLH_VEHICLE_COST,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE,HVLH_EMI" +
                                        " FROM HR_VEHICLE_LOAN_HEAD WHERE HVLH_EORA_CODE = '" + txtEcodeSearch.Text + "';";
                    dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lblDataNotFound.Visible = false;
                        txtEMI.Text = Convert.ToDouble(dt.Rows[0]["HVLH_EMI"]).ToString("f");
                        txtPayment.Text = "";
                        txtVehicleNo.Text = dt.Rows[0]["HVLH_VEHICLE_REG_NUMBER"] + "";
                        loanAmt = Convert.ToDouble(dt.Rows[0]["HVLH_LOAN_AMT"]);
                        FillVehicleRecoveryToGrid(dt);
                    }
                    else
                    {
                        lblDataNotFound.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
                finally
                {
                    objDB = null;
                    dt = null;
                }
            }
        }

        private void FillVehicleRecoveryToGrid(DataTable dt)
        {
            objDB = new SQLDB();
            DataTable dtRecv = new DataTable();
            payments = Convert.ToDouble(dt.Rows[0]["HVLH_LOAN_RECOVERY_CUTOFFDATE"]);
            try
            {
                string sqlText = "EXEC GetEmpVehicleloanRecoveryDetls '" + CommonData.CompanyCode +
                                    "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear +
                                    "','" + CommonData.DocMonth + "','" + txtEcodeSearch.Text +
                                    "','" + dt.Rows[0]["HVLH_VEHICLE_REG_NUMBER"].ToString() + "',''";
                dtRecv = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dtRecv.Rows.Count > 0)
                {
                    for (int i = 0; i < dtRecv.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = i+1;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellFinYear = new DataGridViewTextBoxCell();
                        cellFinYear.Value = dtRecv.Rows[i]["HVLR_FIN_YEAR"];
                        tempRow.Cells.Add(cellFinYear);

                        DataGridViewCell cellDocMonth = new DataGridViewTextBoxCell();
                        cellDocMonth.Value = dtRecv.Rows[i]["HVLR_DOC_MONTH"];
                        tempRow.Cells.Add(cellDocMonth);

                        DataGridViewCell cellEMI = new DataGridViewTextBoxCell();
                        cellEMI.Value = Convert.ToDouble(dt.Rows[0]["HVLH_EMI"]).ToString("f");
                        tempRow.Cells.Add(cellEMI);
                        
                        DataGridViewCell cellRecptType = new DataGridViewTextBoxCell();
                        cellRecptType.Value = dtRecv.Rows[i]["HVLR_RECPT_TYPE"];
                        tempRow.Cells.Add(cellRecptType);

                        DataGridViewCell cellRecptNo = new DataGridViewTextBoxCell();
                        cellRecptNo.Value = dtRecv.Rows[i]["HVLR_RECPT_NO"];
                        tempRow.Cells.Add(cellRecptNo);

                        DataGridViewCell cellRecAmt = new DataGridViewTextBoxCell();
                        cellRecAmt.Value = dtRecv.Rows[i]["HVLR_RECD_AMT"];
                        payments += Convert.ToDouble(dtRecv.Rows[i]["HVLR_RECD_AMT"]);
                        tempRow.Cells.Add(cellRecAmt);

                        DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                        cellRemarks.Value = dtRecv.Rows[i]["HVLR_REMARKS"]+"";
                        tempRow.Cells.Add(cellRemarks);

                        DataGridViewCell cellTrnID = new DataGridViewTextBoxCell();
                        cellTrnID.Value = dtRecv.Rows[i]["HVLR_ID"] + "";
                        tempRow.Cells.Add(cellTrnID);

                        dgvLoanRecovery.Rows.Add(tempRow);
                    }
                    
                }
                else
                {
                    
                }
                CalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                objDB = null;
                dt = null;
            }
        }

        private void CalculateTotals()
        {
            txtLoanAmt.Text = loanAmt.ToString("f");
            txtPayments.Text = payments.ToString("f");
            toPay = loanAmt - payments;
            txtToPay.Text = toPay.ToString("f");
        }

        private void GetRecruiterName()
        {
            objDB = new SQLDB();
            string recruiterName = "";
            try
            {
                string sqlText = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE LIKE '%" + txtEcodeSearch.Text + "%';";
                recruiterName = objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                if (recruiterName.Length > 1)
                {
                    txtEmpName.Text = recruiterName;
                }
                else
                {
                    txtEmpName.Text = "";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            string sqlText = "";
            int iRes = 0;
            string sFinYear = "";
            if (CheckData())
            {
                //sqlText = "SELECT COUNT(*) FROM DOCUMENT_MONTH WHERE FIN_YEAR = '" + CommonData.FinancialYear + "' AND DOCUMENT_MONTH = '" + Convert.ToDateTime(dtpPaymentDate.Value).ToString("MMMyyyy").ToUpper() + "';";
                sqlText = "SELECT distinct FIN_YEAR FROM DOCUMENT_MONTH WHERE DOCUMENT_MONTH = '" + txtDocMonth.Text.ToString().Replace("'","").ToUpper() + "';";
                sFinYear = objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                if (sFinYear == "")
                {
                    MessageBox.Show("Document Month Not Belongs to This Financial Year", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                    iRes = 0;
                try
                {
                    if (bIsUpdate == false)
                        sqlText = " INSERT INTO HR_VEHICLE_LOAN_RECOVERY(HVLR_COMPANY_CODE, " +
                            "HVLR_BRANCH_CODE, HVLR_EORA_CODE, HVLR_VEHICLE_REG_NUMBER, HVLR_FIN_YEAR" +
                            ",HVLR_DOC_MONTH, HVLR_RECD_DATE, HVLR_RECD_AMT, HVLR_REMARKS, " +
                            "HVLR_CREATED_BY, HVLR_CREATED_DATE, HVLR_RECPT_TYPE, HVLR_RECPT_NO, " +
                            "HVLR_RECPT_DDCHECK_NO) VALUES('" + CommonData.CompanyCode + "'" +
                            ",'" + CommonData.BranchCode + "','" + txtEcodeSearch.Text + "','" + txtVehicleNo.Text +
                            "','" + sFinYear + "'" + ",'" + txtDocMonth.Text.ToString().ToUpper() +
                            "','" + Convert.ToDateTime(dtpPaymentDate.Value).ToString("dd/MMM/yyyy") + "'" +
                            ",'" + txtPayment.Text + "','" + txtRemarks.Text.Replace("'", "") +
                            "','" + CommonData.LogUserId + "',getdate(),'" + cbRefType.SelectedValue.ToString() + 
                            "'," + txtReceiptNo.Text + ",'" + txtRefNo.Text.ToString().Replace("'", "") + "');";
                    else
                        sqlText = " UPDATE HR_VEHICLE_LOAN_RECOVERY " +
                            "SET HVLR_RECD_DATE='" + Convert.ToDateTime(dtpPaymentDate.Value).ToString("dd/MMM/yyyy") + "'," +
                            " HVLR_RECD_AMT=" + txtPayment.Text +
                            ", HVLR_MODIFIED_BY='" + CommonData.LogUserId +
                            "', HVLR_MODIFIED_DATE=GETDATE()" +
                            ", HVLR_REMARKS='" + txtRemarks.Text.Replace("'", "") +
                            "', HVLR_RECPT_TYPE='" + cbRefType.SelectedValue.ToString() +
                            "', HVLR_RECPT_NO=" + txtReceiptNo.Text +
                            ", HVLR_RECPT_DDCHECK_NO='" + txtRefNo.Text.ToString().Replace("'", "") +
                            "' WHERE HVLR_ID = " + iTrnID;
                        iRes = objDB.ExecuteSaveData(sqlText);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                    }
                    else
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtPayment.Text = "";
                    txtRemarks.Text = "";
                    GetVehicleData();
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

        private bool CheckData()
        {
            bool bFlag = true;
            if(txtEmpName.Text == "")
            {
                bFlag = false;
                MessageBox.Show("Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            try { Convert.ToDouble(txtPayment.Text); }
            catch { txtPayment.Text = "0"; }
            try { Convert.ToDouble(txtReceiptNo.Text); }
            catch { txtReceiptNo.Text = "0"; }
            if (txtPayment.Text == "0")
            {
                bFlag = false;
                MessageBox.Show("Enter Payed Amount", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            if (cbRefType.SelectedIndex < 0)
            {
                bFlag = false;
                MessageBox.Show("Select Reciept Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            if (txtReceiptNo.Text == "0")
            {
                bFlag = false;
                MessageBox.Show("Enter Reciept No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            
            return bFlag;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bIsUpdate = false;
            iTrnID = 0;
            txtEcodeSearch.Text = "";
            txtEmpName.Text = "";
            txtPayment.Text = "";
            txtEMI.Text = "";
            txtRemarks.Text = "";
            txtPayments.Text = "";
            txtLoanAmt.Text = "";
            txtToPay.Text = "";
            txtVehicleNo.Text = "";
            dgvLoanRecovery.Rows.Clear();
            cbRefType.SelectedIndex = 0;
            txtReceiptNo.Text = "";
            txtRefNo.Text = "";
            dtpPaymentDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            //dtpDocmentation.Value = Convert.ToDateTime(CommonData.DocMonth.ToUpper());
        }

        private void dgvLoanRecovery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == dgvLoanRecovery.Columns["Edit"].Index)
                {
                    objDB = new SQLDB();
                    DataTable dt = new DataTable();
                    try
                    {
                        string sqlText = "SELECT HVLR_COMPANY_CODE," +
                            "HVLR_BRANCH_CODE," +
                            "HVLR_EORA_CODE," +
                            "HVLR_VEHICLE_REG_NUMBER," +
                            "HVLR_FIN_YEAR," +
                            "HVLR_DOC_MONTH," +
                            "HVLR_RECD_DATE," +
                            "HVLR_RECD_AMT," +
                            "HVLR_REMARKS," +
                            "ISNULL(HVLR_MODIFIED_BY,HVLR_CREATED_BY) LastEntryBy," +
                            "ISNULL(HVLR_MODIFIED_DATE,HVLR_CREATED_DATE) LastEntryDate," +
                            "HVLR_RECPT_TYPE," +
                            "HVLR_RECPT_NO," +
                            "HVLR_RECPT_DDCHECK_NO," +
                            "HVLR_ID" +
                            " FROM HR_VEHICLE_LOAN_RECOVERY where HVLR_ID = " + dgvLoanRecovery.Rows[e.RowIndex].Cells["TrnID"].Value.ToString();
                        dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            txtEcodeSearch.Text = dt.Rows[0]["HVLR_EORA_CODE"].ToString();
                            dtpPaymentDate.Value = Convert.ToDateTime(dt.Rows[0]["HVLR_RECD_DATE"].ToString());
                            txtPayment.Text = dt.Rows[0]["HVLR_RECD_AMT"].ToString();
                            txtVehicleNo.Text = dt.Rows[0]["HVLR_VEHICLE_REG_NUMBER"].ToString();
                            if (dt.Rows[0]["HVLR_RECPT_TYPE"].ToString() != "")
                                cbRefType.SelectedValue = dt.Rows[0]["HVLR_RECPT_TYPE"].ToString();
                            else
                                cbRefType.SelectedIndex = 0;
                            txtReceiptNo.Text = dt.Rows[0]["HVLR_RECPT_NO"].ToString();
                            txtRefNo.Text = dt.Rows[0]["HVLR_RECPT_DDCHECK_NO"].ToString();
                            txtRemarks.Text = dt.Rows[0]["HVLR_REMARKS"].ToString();
                            bIsUpdate = true;
                            iTrnID = Convert.ToInt32(dt.Rows[0]["HVLR_ID"].ToString());
                        }
                        else
                        {
                            btnCancel_Click(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        btnCancel_Click(null, null);

                    }
                    finally
                    {
                        objDB = null;
                    }
                }
            }
        }

        public DataTable dtDCTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("CASH", "CASH");
            table.Rows.Add("BANK", "BANK");
            table.Rows.Add("JV", "JV");
            table.Rows.Add("DD", "DD");
            table.Rows.Add("CHEQUE", "CHEQUE");


            return table;
        }

        private void FillReceiptType()
        {

            try
            {
                DataTable dt = dtDCTranType();
                if (dt.Rows.Count > 0)
                {

                    cbRefType.DataSource = dt;
                    cbRefType.DisplayMember = "type";
                    cbRefType.ValueMember = "name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                Cursor.Current = Cursors.Default;
            }

        }
    }
}
