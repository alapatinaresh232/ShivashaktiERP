using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SSCRMDB;
using SSCRM;
using SSAdmin;

namespace SSCRM
{
    public partial class PunchMissing : Form
    {
        public PunchMissing()
        {
            InitializeComponent();
        }

        private SQLDB objSQLData = null;
        bool flagUpdate = false;           

        private void PunchMissing_Load(object sender, EventArgs e)
        {
            GenerateTransactionNo();

            cmbInOut.SelectedIndex = 0;
            dtpAppDate.Value = DateTime.Today;
            dtpPunchMissDate.Value = DateTime.Today;
            
        }

        private void GenerateTransactionNo()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT ISNULL(MAX(HEPM_APPL_NUMBER),0)+1 TranNo FROM HR_EMPLOYEE_PUNCH_MISSING";
                dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTranNo.Text = dt.Rows[0]["TranNo"] + "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLData = null;
            }

        }

        private bool CheckEmployeeIsOnLeave()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;

            try
            {
                string strCmd = "SELECT HELT_EORA_CODE,HELT_LEAVE_FROM_DATE,HELT_LEAVE_TO_DATE FROM HR_EMPLOYEE_LEAVE_TRN WHERE HELT_EORA_CODE="+ txtEcodeSearch.Text +" ";
                dt = objSQLData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime fromDate = Convert.ToDateTime(dt.Rows[i]["HELT_LEAVE_FROM_DATE"].ToString());
                        DateTime toDate = Convert.ToDateTime(dt.Rows[i]["HELT_LEAVE_TO_DATE"].ToString());

                      
                            if ((dtpPunchMissDate.Value <= fromDate && dtpPunchMissDate.Value >= toDate) || (dtpPunchMissDate.Value >= (fromDate) && dtpPunchMissDate.Value <= (toDate)))
                            {
                                flag = false;
                                MessageBox.Show("Employee applied for Leave on these Dates","Punch Missing",MessageBoxButtons.OK,MessageBoxIcon.Information );
                                return flag;
                            }
                            //else if ((dtpPunchMissDate.Value >= (fromDate) && dtpPunchMissDate.Value <= (toDate)) || (dtpPunchMissDate.Value >= (fromDate) && dtpPunchMissDate.Value <= (fromDate)))
                            //{
                            //    flag = false;
                            //    MessageBox.Show("Employee applied for Leave on these Dates");
                            //    return flag;
                            //}
                       
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                objSQLData = null;
                dt = null;
            }
            return flag;
        }

        private bool CheckEmployeeIsOnDuty()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;

            try
            {
                string strCmd = "SELECT HEOT_EORA_CODE,HEOT_OD_FROM_DATE,HEOT_OD_TO_DATE FROM HR_EMPLOYEE_OD_TRN WHERE HEOT_EORA_CODE="+ txtEcodeSearch.Text +" ";
                dt = objSQLData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime fromDate = Convert.ToDateTime(dt.Rows[i]["HEOT_OD_FROM_DATE"].ToString());
                        DateTime toDate = Convert.ToDateTime(dt.Rows[i]["HEOT_OD_TO_DATE"].ToString());
                       
                            if ((dtpPunchMissDate.Value <= fromDate && dtpPunchMissDate.Value >= toDate) || (dtpPunchMissDate.Value >= fromDate && dtpPunchMissDate.Value <= toDate))
                            {
                                flag = false;
                                MessageBox.Show("Employee is OnDuty on these days ","Punch Missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return flag;
                            }
                            //else if ((dtFromDate.Value >= (fromDate) && dtpToDate.Value <= (toDate)) || (dtFromDate.Value >= (fromDate) && dtpToDate.Value <= (fromDate)))
                            //{
                            //    flag = false;
                            //    MessageBox.Show("Employee is on  OnDuty  these days");
                            //    return flag;
                            //}
                       
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                objSQLData = null;
                dt = null;
            }
            return flag;
        }




        private bool CheckData()
        {
            bool flag = true;
            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "Punch Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeSearch.Focus();
                
            }
            else if (txtHours.Text.Length < 2)
            {
                flag = false;
                MessageBox.Show("Please Enter Punch Miss Time in Hours Format", "Punch Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHours.Focus();
               
            }
            else if (txtMinutes.Text.Length < 2)
            {
                flag = false;
                MessageBox.Show("Please Enter Punch Miss Time in Minutes", "Punch Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMinutes.Focus();
                
            }
            else if (cmbInOut.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select InTime or OutTime ", "Punch Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbInOut.Focus();
               
            }
            else if (txtReason.Text == string.Empty)
            {
                flag = false;
                MessageBox.Show("Please Enter Reason", "Punch Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReason.Focus();
               
            }
           
            else if (txtNameApprovBy.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Approved Ecode", "Punch Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeApprovBy.Focus();
               
            }
            else if (txtEcodeApprovBy.Text.Equals(txtEcodeSearch.Text))
            {
                flag = false;
                MessageBox.Show("Ecode and Approved Ecode Should not be Same!", "Punch Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeApprovBy.Text = "";
                txtNameApprovBy.Text = "";
                txtEcodeApprovBy.Focus();
            }


            else if (gvPunchDetails.Rows.Count > 0 || gvPunchDetails.Rows.Count == 0)
            {
                for (int i = 0; i < gvPunchDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(txtHours.Text + txtMinutes.Text).Equals(gvPunchDetails.Rows[i].Cells["Time"].Value))
                    {
                        flag = false;
                        MessageBox.Show("Punch Time already Existed", "Punch Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }
                }
            }
            else
            {

                btnSave_Click(null, null);

            }
                  
           return flag;

        }

       
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strCommand = "";
            int iRes = 0;

            if (CheckData() == true)
            {
                if (CheckEmployeeIsOnLeave())
                {
                    if (CheckEmployeeIsOnDuty())
                    {
                        objSQLData = new SQLDB();
                        string[] strData = txtEcodeSearch.Tag.ToString().Split('@');

                        try
                        {
                            string strTime = (txtHours.Text + txtMinutes.Text).ToString();
                            if (flagUpdate == true)
                            {
                                if (cmbInOut.SelectedIndex == 1)
                                {
                                    strCommand = "UPDATE HR_EMPLOYEE_PUNCH_MISSING SET HEPM_COMPANY_CODE='" + strData[0] + "',HEPM_BRANCH_CODE='" + strData[1] + "',HEPM_EORA_CODE=" + txtEcodeSearch.Text +
                                        ",HEPM_APPL_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                        "',HEPM_PUNCHMISS_DATE='" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") +
                                        "',HEPM_PUNCHMISS_TIME='" + strTime + "',HEPM_DAY_INTIME='" + strTime + "',HEPM_DAY_OUTTIME='',HEPM_REASON='" + txtReason.Text + "',HEPM_APPROVED_BY_ECODE=" + txtEcodeApprovBy.Text +
                                       ",HEPM_MODIFIED_BY='" + CommonData.LogUserId + "',HEPM_MODIFIED_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                       "' WHERE HEPM_APPL_NUMBER=" + txtTranNo.Text + " ";

                                    strCommand += " UPDATE HR_HO_LOG_TRN SET HHLT_IN=" + (txtHours.Text + "." + txtMinutes.Text).ToString() +
                                                    " WHERE EXISTS (SELECT * FROM HR_HO_LOG_TRN HHLT WHERE HHLT.HHLT_ECODE=HR_HO_LOG_TRN.HHLT_ECODE" +
                                                    " and HHLT.HHLT_DATE=HR_HO_LOG_TRN.HHLT_DATE) AND HHLT_ECODE = " + txtEcodeSearch.Text +
                                                    " AND HHLT_DATE='" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "'";

                                    flagUpdate = false;
                                }
                                else if (cmbInOut.SelectedIndex == 2)
                                {
                                    strCommand = " UPDATE HR_EMPLOYEE_PUNCH_MISSING SET HEPM_COMPANY_CODE='" + strData[0] + "',HEPM_BRANCH_CODE='" + strData[1] +
                                        "',HEPM_EORA_CODE=" + txtEcodeSearch.Text + ",HEPM_APPL_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                        "',HEPM_PUNCHMISS_DATE='" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") +
                                        "',HEPM_PUNCHMISS_TIME='" + strTime + "',HEPM_DAY_INTIME='', HEPM_DAY_OUTTIME='" + strTime + "',HEPM_REASON='" + txtReason.Text + "',HEPM_APPROVED_BY_ECODE=" + txtEcodeApprovBy.Text +
                                        ",HEPM_MODIFIED_BY='" + CommonData.LogUserId + "',HEPM_MODIFIED_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                        "' WHERE HEPM_APPL_NUMBER=" + txtTranNo.Text + " ";

                                    strCommand += " UPDATE HR_HO_LOG_TRN SET HHLT_OUT=" + (txtHours.Text + "." + txtMinutes.Text).ToString() +
                                                    " WHERE EXISTS (SELECT * FROM HR_HO_LOG_TRN HHLT WHERE HHLT.HHLT_ECODE=HR_HO_LOG_TRN.HHLT_ECODE" +
                                                    " and HHLT.HHLT_DATE=HR_HO_LOG_TRN.HHLT_DATE) AND HHLT_ECODE = " + txtEcodeSearch.Text + 
                                                    " AND HHLT_DATE='" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "'";
                                    flagUpdate = false;
                                }
                            }
                            else
                            {
                                if (cmbInOut.SelectedIndex == 1)
                                {

                                    strCommand = "INSERT INTO HR_EMPLOYEE_PUNCH_MISSING(HEPM_COMPANY_CODE" +
                                        ",HEPM_BRANCH_CODE , HEPM_EORA_CODE" +
                                       ",HEPM_APPL_DATE , HEPM_APPL_NUMBER" +
                                       ", HEPM_PUNCHMISS_DATE" +
                                       ",HEPM_PUNCHMISS_TIME" +
                                       ",HEPM_DAY_INTIME" +
                                       ", HEPM_REASON" +
                                       ",HEPM_APPROVED_BY_ECODE" +
                                       ",HEPM_CREATED_BY" +
                                       ",HEPM_CREATED_DATE)VALUES('" + strData[0] + "','" + strData[1] + "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                       ",'" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") + "'," + Convert.ToInt32(txtTranNo.Text) +
                                       ",'" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "','" + strTime + "','" + strTime + "','" + txtReason.Text +
                                       "'," + Convert.ToInt32(txtEcodeApprovBy.Text) + ",'" + CommonData.LogUserId + "','" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                       "'); INSERT INTO HR_HO_PUNCHES(HHP_ECODE,HHP_PUN_DATE,HHP_PUN_TIME,HHP_PUN_SOURCE)VALUES(" + Convert.ToInt32(txtEcodeSearch.Text) +
                                       ",'" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "','" + strTime + "','M')";

                                    strCommand += " UPDATE HR_HO_LOG_TRN SET HHLT_IN=" + (txtHours.Text + "." + txtMinutes.Text).ToString() +
                                                    " WHERE EXISTS (SELECT * FROM HR_HO_LOG_TRN HHLT WHERE HHLT.HHLT_ECODE=HR_HO_LOG_TRN.HHLT_ECODE" +
                                                    " and HHLT.HHLT_DATE=HR_HO_LOG_TRN.HHLT_DATE) AND HHLT_ECODE = " + txtEcodeSearch.Text +
                                                    " AND HHLT_DATE='" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "'";
                                }
                                else if (cmbInOut.SelectedIndex == 2)
                                {
                                    strCommand = "INSERT INTO HR_EMPLOYEE_PUNCH_MISSING(HEPM_COMPANY_CODE" +
                                        ",HEPM_BRANCH_CODE , HEPM_EORA_CODE" +
                                       ",HEPM_APPL_DATE" +
                                       ",HEPM_APPL_NUMBER" +
                                       ", HEPM_PUNCHMISS_DATE" +
                                       ",HEPM_PUNCHMISS_TIME" +
                                       ",HEPM_DAY_OUTTIME " +
                                       ",HEPM_REASON," +
                                       "HEPM_APPROVED_BY_ECODE" +
                                       ",HEPM_CREATED_BY" +
                                       ",HEPM_CREATED_DATE)VALUES('" + strData[0] + "','" + strData[1] +
                                       "'," + Convert.ToInt32(txtEcodeSearch.Text) + ",'" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") + "'," + Convert.ToInt32(txtTranNo.Text) +
                                       ",'" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "','" + strTime + "','" + strTime + "','" + txtReason.Text +
                                       "'," + Convert.ToInt32(txtEcodeApprovBy.Text) + ",'" + CommonData.LogUserId + "','" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                       "'); INSERT INTO HR_HO_PUNCHES(HHP_ECODE,HHP_PUN_DATE,HHP_PUN_TIME,HHP_PUN_SOURCE) VALUES(" + Convert.ToInt32(txtEcodeSearch.Text) +
                                       ",'" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "','" + strTime + "','M') ";


                                    strCommand += " UPDATE HR_HO_LOG_TRN SET HHLT_OUT=" + (txtHours.Text + "." + txtMinutes.Text).ToString() +
                                                    "WHERE EXISTS (SELECT * FROM HR_HO_LOG_TRN HHLT WHERE HHLT.HHLT_ECODE=HR_HO_LOG_TRN.HHLT_ECODE" +
                                                    " and HHLT.HHLT_DATE=HR_HO_LOG_TRN.HHLT_DATE) AND HHLT_ECODE = " + txtEcodeSearch.Text +
                                                    " AND HHLT_DATE='" + Convert.ToDateTime(dtpPunchMissDate.Value).ToString("dd/MMM/yyyy") + "'";
                                }
                            }

                            iRes = objSQLData.ExecuteSaveData(strCommand);
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            objSQLData = null;

                        }

                        if (iRes > 0)
                        {

                            MessageBox.Show("Data Saved Sucessfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCancel_Click(null, null);
                            GenerateTransactionNo();
                        }

                        else
                        {
                            MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

        }
            
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtEmpDesg.Text = "";
            txtEcodeApprovBy.Text = "";
            txtNameApprovBy.Text = "";
            txtReason.Text = "";
            txtHours.Text = "";
            txtMinutes.Text = "";
            cmbInOut.SelectedIndex = 0;
            dtpAppDate.Value = DateTime.Today;
            dtpPunchMissDate.Value = DateTime.Today;
            GenerateTransactionNo();
            gvPunchDetails.Rows.Clear();
            
        }
                                    
        private void txtHours_Validating(object sender, CancelEventArgs e)
        {
            if ((txtHours.Text.Length)>0)
            {
                if (Convert.ToInt32(txtHours.Text) >= 24)
                {
                    MessageBox.Show("please enter valid Time in Hours");
                    txtHours.Focus();

                }
            }
           
        }

        private void txtMinutes_Validating(object sender, CancelEventArgs e)
        {

            if ((txtMinutes.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtMinutes.Text) >= 60)
                {
                    MessageBox.Show("please enter valid Time in Minutes");
                    txtMinutes.Focus();
                }
            }
        }
         
       
        private void txtEcodeApprovBy_TextChanged(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeApprovBy.Text !="")
            {

                try
                {
                    string strCommand = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE =" + Convert.ToInt32(txtEcodeApprovBy.Text) + " ";
                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtNameApprovBy.Text = dt.Rows[0]["MEMBER_NAME"] + "";

                    }
                    else
                    {
                        txtNameApprovBy.Text = "";
                    }
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                    dt = null;

                }

            }
           
        }
     
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtTranNo_Validated(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();

            if (txtTranNo.Text != "")
            {

                try
                {
                    string strCommand = "SELECT HEPM_EORA_CODE,HEPM_APPL_DATE, HEPM_PUNCHMISS_DATE,HEPM_DAY_INTIME,HEPM_DAY_OUTTIME, HEPM_PUNCHMISS_TIME,HEPM_REASON,HEPM_APPROVED_BY_ECODE FROM HR_EMPLOYEE_PUNCH_MISSING WHERE HEPM_APPL_NUMBER=" + Convert.ToInt32(txtTranNo.Text) + " ";
                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string strInTime = dt.Rows[0]["HEPM_DAY_INTIME"].ToString();
                        string strOutTime = dt.Rows[0]["HEPM_DAY_OUTTIME"].ToString();
                        if (strInTime.Equals(""))
                        {
                            flagUpdate = true;

                            string strHr = dt.Rows[0]["HEPM_PUNCHMISS_TIME"].ToString().Substring(0, 2);
                            string strMin = dt.Rows[0]["HEPM_PUNCHMISS_TIME"].ToString().Substring(2, 2);

                            txtEcodeSearch.Text = dt.Rows[0]["HEPM_EORA_CODE"].ToString();
                            dtpAppDate.Value = Convert.ToDateTime(dt.Rows[0]["HEPM_APPL_DATE"].ToString());
                            dtpPunchMissDate.Value = Convert.ToDateTime(dt.Rows[0]["HEPM_PUNCHMISS_DATE"].ToString());
                            txtHours.Text = strHr;
                            txtMinutes.Text = strMin;
                            cmbInOut.SelectedIndex = 2;

                            txtReason.Text = dt.Rows[0]["HEPM_REASON"].ToString();
                            txtEcodeApprovBy.Text = dt.Rows[0]["HEPM_APPROVED_BY_ECODE"].ToString();
                        }
                        else if (strOutTime.Equals(""))
                        {
                            flagUpdate = true;

                            string strHr = dt.Rows[0]["HEPM_PUNCHMISS_TIME"].ToString().Substring(0, 2);
                            string strMin = dt.Rows[0]["HEPM_PUNCHMISS_TIME"].ToString().Substring(2, 2);

                            txtEcodeSearch.Text = dt.Rows[0]["HEPM_EORA_CODE"].ToString();
                            dtpAppDate.Value = Convert.ToDateTime(dt.Rows[0]["HEPM_APPL_DATE"].ToString());
                            dtpPunchMissDate.Value = Convert.ToDateTime(dt.Rows[0]["HEPM_PUNCHMISS_DATE"].ToString());
                            txtHours.Text = strHr;
                            txtMinutes.Text = strMin;
                            cmbInOut.SelectedIndex = 1;

                            txtReason.Text = dt.Rows[0]["HEPM_REASON"].ToString();
                            txtEcodeApprovBy.Text = dt.Rows[0]["HEPM_APPROVED_BY_ECODE"].ToString();
                        }


                    }
                    else
                    {
                        btnCancel_Click(null,null);
                        GenerateTransactionNo();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                    dt = null;
                }
            }
                       
        }

        private void GetPunchDetailsToGrid()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();

            if (txtEcodeSearch.Text !="")
            {
                try
                {

                    string strCommand = "SELECT HHP_PUN_DATE, HHP_PUN_TIME ,HHP_PUN_SOURCE,HHP_PUNCHTIMEDETAILS_ID  FROM HR_HO_PUNCHES WHERE HHP_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " AND HHP_PUN_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("yyyy/MM/dd") + "' ";
                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                        cellSNo.Value = (i + 1).ToString();
                        tempRow.Cells.Add(cellSNo);

                        DataGridViewCell cellDate = new DataGridViewTextBoxCell();
                        cellDate.Value = dtpAppDate.Value.ToShortDateString();
                        tempRow.Cells.Add(cellDate);

                        DataGridViewCell cellTime = new DataGridViewTextBoxCell();
                        cellTime.Value = dt.Rows[i]["HHP_PUN_TIME"].ToString();
                        tempRow.Cells.Add(cellTime);

                        DataGridViewCell cellPunSource = new DataGridViewTextBoxCell();
                        cellPunSource.Value = dt.Rows[i]["HHP_PUN_SOURCE"].ToString();
                        tempRow.Cells.Add(cellPunSource);

                        DataGridViewCell cellPunTimeId = new DataGridViewTextBoxCell();
                        cellPunTimeId.Value = dt.Rows[i]["HHP_PUNCHTIMEDETAILS_ID"].ToString();
                        tempRow.Cells.Add(cellPunTimeId);


                        gvPunchDetails.Rows.Add(tempRow);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                    dt = null;
                }
            }
            else
            {
                gvPunchDetails.Rows.Clear();
            }
        }

       
        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            gvPunchDetails.Rows.Clear();
            
            GetPunchDetailsToGrid();

        }

        private void dtpAppDate_ValueChanged(object sender, EventArgs e)
        {
            gvPunchDetails.Rows.Clear();
            //btnCancel_Click(null,null);
            
            GetPunchDetailsToGrid();
        }

        private void txtEcodeApprovBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {
                try
                {

                    string strCommand = "SELECT MEMBER_NAME,DESIG,company_code+'@'+BRANCH_CODE AS Val FROM EORA_MASTER WHERE ECODE= " + Convert.ToInt32(txtEcodeSearch.Text) + "  ";

                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                        txtEmpDesg.Text = dt.Rows[0]["DESIG"] + "";
                        txtEcodeSearch.Tag = dt.Rows[0]["Val"].ToString();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                    dt = null;

                }
            }
            else
            {
                txtEName.Text = "";
                txtEmpDesg.Text = "";
            }

        }
        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
  
    }
}
