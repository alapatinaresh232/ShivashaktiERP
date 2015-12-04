using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM;
using SSTrans;

namespace SSCRM
{
    public partial class CompensatoryOFF : Form
    {
        private SQLDB objSQLData = null;
        private IndentDB objIndentData = null;
        bool flagUpdate = false;
        private string sFormType = "";


        public CompensatoryOFF()
        {
            InitializeComponent();
        }

        public CompensatoryOFF(string sfrmType)
        {
            InitializeComponent();
            sFormType = sfrmType;
        }
              

        private void CompensatoryOFF_Load(object sender, EventArgs e)
        {
            GenerateTransactionNo();

            dtpAppDate.Value = DateTime.Today;
            dtpFromdate.Value = DateTime.Today;

            lblChecking.Visible = false;
            lblChecking.Text = "";
            cmbDayType.SelectedIndex = 0;

            //txtToMinutes.Text = "00";
            //txtToHours.Text = "00";
          
            //txtFrmMinutes.Text = "00";
            //txtFrmHours.Text = "00";
            txtToMinutes.Text = txtToHours.Text = txtFrmMinutes.Text = txtFrmHours.Text = "00";

            if (CommonData.LogUserId.ToUpper() == "ADMIN" || sFormType=="HO")
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            if (txtEcodeSearch.Text !="")
            {
                try
                {
                    if (sFormType == "HO")
                    {
                        strCommand = "SELECT MEMBER_NAME,DESIG,company_code+'@'+BRANCH_CODE AS Val " +
                                            " FROM EORA_MASTER WHERE ECODE= " + Convert.ToInt32(txtEcodeSearch.Text) + "  ";
                    }
                    else
                    {
                        strCommand = "SELECT MEMBER_NAME,DESIG,company_code+'@'+BRANCH_CODE AS Val " +
                                           " FROM EORA_MASTER WHERE ECODE= " + Convert.ToInt32(txtEcodeSearch.Text) +
                                           " AND BRANCH_CODE='"+ CommonData.BranchCode +"' ";
                    }

                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtEmpDesg.Text = dt.Rows[0]["DESIG"].ToString();
                        txtEcodeSearch.Tag = dt.Rows[0]["Val"].ToString();

                    }
                    else
                    {
                        txtEName.Text = "";
                        txtEmpDesg.Text = "";
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

        private void txtEcodeApprovBy_TextChanged(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeApprovBy.Text !="")
            {

                try
                {
                    string strCommand = "SELECT MEMBER_NAME,DESIG FROM EORA_MASTER WHERE ECODE =" + Convert.ToInt32(txtEcodeApprovBy.Text) + " ";
                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtNameApprovBy.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtAppEmpDesg.Text = dt.Rows[0]["DESIG"].ToString();

                    }
                    else
                    {
                        txtNameApprovBy.Text = "";
                        txtAppEmpDesg.Text = "";
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

        private void GenerateTransactionNo()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            String strCommand = "";
            try
            {
                if (sFormType == "HO")
                {
                   strCommand = "SELECT ISNULL(MAX(HECT_APPL_NUMBER),0)+1 TranNo "+
                                  " FROM HR_EMPLOYEE_COFF_TRN where isnull(HECT_LOC_TYPE,'HO')='"+ sFormType +"'";
                }
                else
                {
                    strCommand = "SELECT ISNULL(MAX(HECT_APPL_NUMBER),0)+1 TranNo " +
                                     " FROM HR_EMPLOYEE_COFF_TRN "+
                                     " where isnull(HECT_LOC_TYPE,'HO')='" + sFormType +
                                     "' and HECT_BRANCH_CODE='"+ CommonData.BranchCode +"' ";
                }

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
                dt = null;
            }

        }

        private bool CheckData()
        {
            bool flag = true;
            
            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "Compensatory OFF ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeSearch.Focus();
                
            }
           //else if (txtReason.Text == string.Empty)
           // {
           //     flag = false;
           //     MessageBox.Show("Please Enter Reason", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
           //     txtReason.Focus();
               
           // }
            //else if (cmbDayType.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select DayType ", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cmbDayType.Focus();
                
            //}
          
            else if (dtpFromdate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("C-Off Working date is less than or Equal to Today's Date", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            else if (gvCoffDetl.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show(" Please Add C-Off Details ", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            else if (txtNameApprovBy.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Approved Ecode", "Compensatory OFF ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeApprovBy.Focus();

            }
            else if (txtEcodeApprovBy.Text.Equals(txtEcodeSearch.Text))
            {
                flag = false;
                MessageBox.Show("Ecode and Approved Ecode Should not be same", "Compensatory OFF ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeApprovBy.Text = "";
                txtNameApprovBy.Text = "";
                txtAppEmpDesg.Text = "";
                txtEcodeApprovBy.Focus();
                
            }
          

            //else if (isDateCheck() == true)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select valid Working date", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return flag;
            //}
            //else if (CheckingRecord() == false)
            //{
            //    flag = false;
            //    return flag;
            //}
                               
            return flag;

        }

        private bool CheckingRecord()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;
            try
            {
                
                string strCmd = "SELECT HECT_EORA_CODE, COUNT(HECT_NOOF_COFF_DAYS) AS days FROM HR_EMPLOYEE_COFF_TRN "+
                                " WHERE HECT_EORA_CODE ="+ txtEcodeSearch.Text +
                                " AND DATEPART(MONTH,HECT_COFF_FROM_DATE) <= DATEADD(MONTH, 2, GETDATE()) GROUP BY HECT_EORA_CODE";
                dt=objSQLData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //strCmd = "";
                        

                        //int days = Convert.ToInt32(dt.Rows[i]["days"].ToString());

                        //if (days > 4)
                        //{
                        //    flag = false;
                        //    MessageBox.Show("Employee already 4 Working  COFFs");
                        //    return flag;
                        //}
                        //else
                        //{


                            strCmd = "SELECT HECT_EORA_CODE FROM HR_EMPLOYEE_COFF_TRN WHERE HECT_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                    " AND HECT_COFF_FROM_DATE='" + dtpFromdate.Value.ToString("yyyy/MM/dd") + 
                                    "' AND HECT_COFF_NOON='" + cmbDayType.SelectedItem.ToString() + "'";
                            if (objSQLData.ExecuteDataSet(strCmd).Tables[0].Rows.Count > 0)
                            {
                                flag = false;
                                MessageBox.Show(" This Record already Exists","Compensatory OFF",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return flag;
                            }
                        //}
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
                dt = null;
            }
            return flag;
        }

       

        private bool CheckingEmployeeIsonLeave()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            bool flagCheckLeave = true;

            try
            {
                string strCmd = "SELECT HELT_EORA_CODE,HELT_LEAVE_FROM_DATE,HELT_LEAVE_TO_DATE "+
                                " FROM HR_EMPLOYEE_LEAVE_TRN WHERE HELT_EORA_CODE= "+ txtEcodeSearch.Text +" ";
                dt = objSQLData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime fromDate = Convert.ToDateTime(dt.Rows[i]["HELT_LEAVE_FROM_DATE"].ToString());
                        DateTime toDate = Convert.ToDateTime(dt.Rows[i]["HELT_LEAVE_TO_DATE"].ToString());

                        if (dtpFromdate.Value >= (fromDate) && dtpFromdate.Value <= (toDate))
                        {
                            flagCheckLeave = false;
                            MessageBox.Show("Employee applied for Leave on these Dates", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return flagCheckLeave;
                        }
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
            return flagCheckLeave;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;

            if (CheckData() == true)
            {
                if (CheckingEmployeeIsonLeave() == true)
                {
                    objSQLData = new SQLDB();
                    string[] strData = txtEcodeSearch.Tag.ToString().Split('@');

                    try
                    {
                        string strCommand = "";
                        string FromTime = txtFrmHours.Text+'.'+txtFrmMinutes.Text;
                        string ToTime= txtToHours.Text + '.' + txtToMinutes.Text;

                        //if (flagUpdate == true)
                        //{
                        //    if (sFormType == "HO")
                        //    {
                        //        for (int i = 0; i < gvCoffDetl.Rows.Count; i++)
                        //        {

                        //            strCommand += "UPDATE HR_EMPLOYEE_COFF_TRN SET HECT_COMPANY_CODE='" + strData[0] +
                        //                          "', HECT_BRANCH_CODE='" + strData[1] +
                        //                          "',HECT_APPL_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                        //                          "',HECT_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                        //                          ",HECT_COFF_FROM_DATE='" + Convert.ToDateTime(gvCoffDetl.Rows[i].Cells["CoffDate"].Value).ToString("dd/MMM/yyyy") +
                        //                          "',HECT_COFF_NOON='',HECT_NOOF_COFF_DAYS=" + Convert.ToDouble(txtNoOfDays.Text) +
                        //                          ",HECT_COFF_REASON='" + gvCoffDetl.Rows[i].Cells["Reason"].Value.ToString() +
                        //                          "',HECT_COFF_APPROVED_BY_ECODE=" + Convert.ToInt32(txtEcodeApprovBy.Text) +
                        //                          ",HECT_COFF_VALID_FLAG='O'" +
                        //                          ",HECT_MODIFIED_BY='" + CommonData.LogUserId +
                        //                          "',HECT_MODIFIED_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                        //                          "',HECT_COFF_FROM_TIME=" + gvCoffDetl.Rows[i].Cells["FrmTime"].Value.ToString() +
                        //                          ",HECT_COFF_TO_TIME=" + gvCoffDetl.Rows[i].Cells["ToTime"].Value.ToString() +
                        //                          " WHERE HECT_APPL_NUMBER=" + txtTranNo.Text +
                        //                          " and isnull(HECT_LOC_TYPE,'HO')='" + sFormType + "'";
                        //        }
                        //        //if (strCommand.Length > 5)
                        //        //{
                        //        //    result = objSQLData.ExecuteSaveData(strCommand);
                        //        //}
                        //    }
                        //    else
                        //    {
                        //        for (int i = 0; i < gvCoffDetl.Rows.Count; i++)
                        //        {

                        //            strCommand += "UPDATE HR_EMPLOYEE_COFF_TRN SET HECT_COMPANY_CODE='" + strData[0] +
                        //                          "', HECT_BRANCH_CODE='" + strData[1] +
                        //                          "',HECT_APPL_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                        //                          "',HECT_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                        //                          ",HECT_COFF_FROM_DATE='" + Convert.ToDateTime(gvCoffDetl.Rows[i].Cells["CoffDate"].Value).ToString("dd/MMM/yyyy") +
                        //                          "',HECT_COFF_NOON='',HECT_NOOF_COFF_DAYS=" + Convert.ToDouble(txtNoOfDays.Text) +
                        //                          ",HECT_COFF_REASON='" + gvCoffDetl.Rows[i].Cells["Reason"].Value.ToString() +
                        //                          "',HECT_COFF_APPROVED_BY_ECODE=" + Convert.ToInt32(txtEcodeApprovBy.Text) +
                        //                          ",HECT_COFF_VALID_FLAG='O'" +
                        //                          ",HECT_MODIFIED_BY='" + CommonData.LogUserId +
                        //                          "',HECT_MODIFIED_DATE='" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                        //                          "',HECT_COFF_FROM_TIME=" + gvCoffDetl.Rows[i].Cells["FrmTime"].Value.ToString() +
                        //                          ",HECT_COFF_TO_TIME=" + gvCoffDetl.Rows[i].Cells["ToTime"].Value.ToString() +
                        //                          "  WHERE HECT_BRANCH_CODE='" + CommonData.BranchCode +
                        //                          "' AND HECT_APPL_NUMBER=" + txtTranNo.Text +
                        //                          " and HECT_LOC_TYPE='" + sFormType + "'";
                        //        }
                        //    }                          
                        //}
                        //if (strCommand.Length > 5)
                        //{
                        //    result = objSQLData.ExecuteSaveData(strCommand);
                        //}
                        //else
                        //{
                          
                          
                            objSQLData = new SQLDB();
                            if (sFormType == "HO")
                            {
                                strCommand += "DELETE from HR_EMPLOYEE_COFF_TRN " +
                                         " WHERE HECT_APPL_NUMBER =" + Convert.ToInt32(txtTranNo.Text) +
                                         " AND isnull(HECT_LOC_TYPE,'HO')='" + sFormType + "'";
                            }
                            else
                            {
                                strCommand += "DELETE from HR_EMPLOYEE_COFF_TRN " +
                                            " WHERE HECT_APPL_NUMBER =" + Convert.ToInt32(txtTranNo.Text) +
                                            " AND HECT_LOC_TYPE='" + sFormType +
                                            "'and HECT_BRANCH_CODE='" + CommonData.BranchCode + "'";

                            }
                            for (int i = 0; i <gvCoffDetl.Rows.Count; i++)
                            {

                                strCommand += "INSERT INTO HR_EMPLOYEE_COFF_TRN(HECT_COMPANY_CODE " +
                                                                            ", HECT_BRANCH_CODE " +
                                                                            ", HECT_APPL_NUMBER" +
                                                                            ", HECT_APPL_DATE" +
                                                                            ", HECT_EORA_CODE" +
                                                                            ", HECT_COFF_FROM_DATE" +
                                                                            ", HECT_COFF_NOON" +
                                                                            ", HECT_NOOF_COFF_DAYS" +
                                                                            ", HECT_COFF_REASON" +
                                                                            ", HECT_COFF_APPROVED_BY_ECODE" +
                                                                            ", HECT_CREATED_BY" +
                                                                            ", HECT_CREATED_DATE " +
                                                                            ", HECT_COFF_VALID_FLAG " +
                                                                            ", HECT_COFF_FROM_TIME " +
                                                                            ", HECT_COFF_TO_TIME " +
                                                                            ",HECT_SI_NO" +
                                                                            ", HECT_LOC_TYPE " +                                                                           
                                                                            ")VALUES " +
                                                                            "('" + strData[0] +
                                                                            "','" + strData[1] +
                                                                            "'," + Convert.ToInt32(txtTranNo.Text) +
                                                                            ",'" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                                                            "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                                            ",'" + Convert.ToDateTime(gvCoffDetl.Rows[i].Cells["CoffDate"].Value).ToString("dd/MMM/yyyy") +
                                                                            "','" + cmbDayType.SelectedItem.ToString() +
                                                                            "'," + Convert.ToDouble(gvCoffDetl.Rows[i].Cells["NoOfDays"].Value).ToString() +
                                                                            ",'" +gvCoffDetl.Rows[i].Cells["Reason"].Value.ToString() +
                                                                            "'," + Convert.ToInt32(txtEcodeApprovBy.Text) +
                                                                            ",'" + CommonData.LogUserId +
                                                                            "','" + Convert.ToDateTime(dtpAppDate.Value).ToString("dd/MMM/yyyy") +
                                                                            "','O'" +
                                                                            "," + gvCoffDetl.Rows[i].Cells["FrmTime"].Value.ToString() +
                                                                            "," + gvCoffDetl.Rows[i].Cells["ToTime"].Value.ToString() +
                                                                            "," + gvCoffDetl.Rows[i].Cells["SLNO"].Value.ToString() +
                                                                            ",'" + sFormType + "')";
                            }

                            if (strCommand.Length > 5)
                            {
                                result = objSQLData.ExecuteSaveData(strCommand);
                            }
                        }
                     

                    //}
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (result > 0)
                    {

                        MessageBox.Show("Data Saved Sucessfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        GenerateTransactionNo();
                        flagUpdate = false;
                    }

                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtEmpDesg.Text = "";
            txtEcodeApprovBy.Text = "";
            txtNameApprovBy.Text = "";
            txtAppEmpDesg.Text = "";
            cmbDayType.SelectedIndex = 0;
            gvCoffDetl.Rows.Clear();
            txtReason.Text = "";
            dtpAppDate.Value = DateTime.Today;
            dtpFromdate.Value = DateTime.Today;
            txtToMinutes.Text = txtToHours.Text = txtFrmMinutes.Text = txtFrmHours.Text = "00";
            txtNoOfDays.Text = "0";            
            GenerateTransactionNo();
            btnSave.Enabled = true;
            lblChecking.Visible = false;
            lblChecking.Text = "";
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

        private bool isDateCheck()
        {
            objIndentData = new IndentDB();
            objSQLData = new SQLDB();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string strYear = DateTime.Now.Year.ToString();
            Int32 intYear = Convert.ToInt32(strYear);
            bool flagCheck = true;


            try
            {
                string strCommand = "SELECT HSM_HOLIDAY_DATE FROM HR_HOLIDAY_MASTER ";
                dt=objSQLData.ExecuteDataSet(strCommand).Tables[0];
                ds = objIndentData.get_HolidaysList(intYear);
                if (ds.Tables[0].Rows.Count > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (((Convert.ToDateTime(ds.Tables[0].Rows[i]["hsm_holiday_date"].ToString())) == dtpFromdate.Value) || ((Convert.ToDateTime(dt.Rows[j]["HSM_HOLIDAY_DATE"].ToString())) == dtpFromdate.Value))
                            {
                                flagCheck = false;
                                //MessageBox.Show("Selected date is  holiday", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return flagCheck;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objIndentData = null;
                objSQLData = null;
                dt = null;
                ds = null;

            }
            return flagCheck;
        }

            
        private void dtpFromdate_ValueChanged(object sender, EventArgs e)
        {
            isDateCheck();
            
        }

        private void txtTranNo_Validated(object sender, EventArgs e)
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            gvCoffDetl.Rows.Clear();
            if (txtTranNo.Text != "")
            {
                try
                {
                    if (sFormType == "HO")
                    {
                         strCommand = "SELECT HECT_APPL_DATE,HECT_EORA_CODE,HECT_COFF_FROM_DATE " +
                                            ",HECT_COFF_NOON,HECT_NOOF_COFF_DAYS,HECT_COFF_REASON " +
                                            ",HECT_COFF_APPROVED_BY_ECODE,HECT_COFF_FROM_TIME " +
                                            ",HECT_COFF_TO_TIME,HECT_LOC_TYPE " +
                                            ",DATEDIFF(DAY, HECT_CREATED_DATE, GETDATE()) BackDays " +
                                             ",HECT_SI_NO " +
                                            " FROM HR_EMPLOYEE_COFF_TRN " +                                           
                                            " WHERE HECT_APPL_NUMBER= " + txtTranNo.Text + 
                                            " AND isnull(HECT_LOC_TYPE,'HO')='"+ sFormType +"' ";
                    }
                    else
                    {
                        strCommand = "SELECT HECT_APPL_DATE,HECT_EORA_CODE,HECT_COFF_FROM_DATE " +
                                               ",HECT_COFF_NOON,HECT_NOOF_COFF_DAYS,HECT_COFF_REASON " +
                                               ",HECT_COFF_APPROVED_BY_ECODE,HECT_COFF_FROM_TIME " +
                                               ",HECT_COFF_TO_TIME,HECT_LOC_TYPE " +
                                               ",DATEDIFF(DAY, HECT_CREATED_DATE, GETDATE()) BackDays " +
                                                ",HECT_SI_NO " +
                                               " FROM HR_EMPLOYEE_COFF_TRN " +
                                               " WHERE HECT_BRANCH_CODE='"+ CommonData.BranchCode +
                                               "' and HECT_APPL_NUMBER= " + txtTranNo.Text +
                                               " AND HECT_LOC_TYPE='" + sFormType + "' ";

                    }

                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dt.Rows[0]["BackDays"].ToString()) > 3)
                        {
                            btnSave.Enabled = false;
                            lblChecking.Visible = true;
                            lblChecking.Text = "This Data Can Not Modify";
                           // MessageBox.Show("This Data Can Not Modify!","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            lblChecking.Visible = false;
                            lblChecking.Text = "";
                        }

                        flagUpdate = true;

                        dtpAppDate.Value = Convert.ToDateTime(dt.Rows[0]["HECT_APPL_DATE"].ToString());
                        txtEcodeSearch.Text = dt.Rows[0]["HECT_EORA_CODE"].ToString();
                        dtpFromdate.Value = Convert.ToDateTime(dt.Rows[0]["HECT_COFF_FROM_DATE"].ToString());
                       // cmbDayType.SelectedItem = dt.Rows[0]["HECT_COFF_NOON"].ToString();
                        txtNoOfDays.Text = dt.Rows[0]["HECT_NOOF_COFF_DAYS"].ToString();
                        txtReason.Text = dt.Rows[0]["HECT_COFF_REASON"].ToString();
                        txtEcodeApprovBy.Text = dt.Rows[0]["HECT_COFF_APPROVED_BY_ECODE"].ToString();
                        if (dt.Rows[0]["HECT_COFF_FROM_TIME"].ToString().Length != 0)
                        {
                            txtFrmHours.Text = dt.Rows[0]["HECT_COFF_FROM_TIME"].ToString().Split('.')[0];
                            txtFrmMinutes.Text = dt.Rows[0]["HECT_COFF_FROM_TIME"].ToString().Split('.')[1];
                        }
                        if (dt.Rows[0]["HECT_COFF_TO_TIME"].ToString().Length != 0)
                        {
                            txtToHours.Text = dt.Rows[0]["HECT_COFF_TO_TIME"].ToString().Split('.')[0];
                            txtToMinutes.Text = dt.Rows[0]["HECT_COFF_TO_TIME"].ToString().Split('.')[1];
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvCoffDetl.Rows.Add();
                            gvCoffDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvCoffDetl.Rows[i].Cells["CoffDate"].Value = Convert.ToDateTime(dt.Rows[i]["HECT_COFF_FROM_DATE"]).ToString("dd/MMM/yyyy").ToUpper();
                            gvCoffDetl.Rows[i].Cells["FrmTime"].Value = dt.Rows[i]["HECT_COFF_FROM_TIME"].ToString();
                            gvCoffDetl.Rows[i].Cells["ToTime"].Value = dt.Rows[i]["HECT_COFF_TO_TIME"].ToString();
                            gvCoffDetl.Rows[i].Cells["Reason"].Value = dt.Rows[i]["HECT_COFF_REASON"].ToString();
                            gvCoffDetl.Rows[i].Cells["NoOfDays"].Value = dt.Rows[i]["HECT_NOOF_COFF_DAYS"].ToString();

                        }
                    }
                    else
                    {
                        GenerateTransactionNo();
                        btnCancel_Click(null,null);
                        flagUpdate = false;


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

        private void cmbDayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDayType.SelectedIndex == 0)
            {
               txtNoOfDays.Text = "0";
            }
            else if (cmbDayType.SelectedIndex == 1 || cmbDayType.SelectedIndex == 4)
            {
                txtNoOfDays.Text = "1";
            }
            else if (cmbDayType.SelectedIndex == 2 || cmbDayType.SelectedIndex == 3)
            {
                txtNoOfDays.Text = "0.5";
            }
           
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete",
                                              "CRM", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (DialogResult.Yes==result && flagUpdate == true)
            {
                try
                {
                    string strSQL = "";
                    int res = 0;
                    if (sFormType == "HO")
                    {
                        strSQL = "DELETE from HR_EMPLOYEE_COFF_TRN "+
                                 " WHERE HECT_APPL_NUMBER =" + Convert.ToInt32(txtTranNo.Text)+
                                 " AND isnull(HECT_LOC_TYPE,'HO')='"+ sFormType +"'";
                    }
                    else
                    {
                        strSQL = "DELETE from HR_EMPLOYEE_COFF_TRN " +
                                    " WHERE HECT_APPL_NUMBER =" + Convert.ToInt32(txtTranNo.Text) +
                                    " AND HECT_LOC_TYPE='" + sFormType +
                                    "'and HECT_BRANCH_CODE='"+ CommonData.BranchCode +"'";

                    }

                    objSQLData = new SQLDB();
                    if (strSQL.Length > 5)
                    {
                        res = objSQLData.ExecuteSaveData(strSQL);
                    }
                    if(res > 0)
                    {
                        MessageBox.Show("Deleted Successfully",
                                              "CRM", MessageBoxButtons.OK,MessageBoxIcon.Information);

                        txtEcodeSearch.Text = "";
                        txtEName.Text = "";
                        txtEmpDesg.Text = "";
                        txtEcodeApprovBy.Text = "";
                        txtNameApprovBy.Text = "";
                        txtAppEmpDesg.Text = "";
                        cmbDayType.SelectedIndex = 0;
                        txtReason.Text = "";
                        dtpAppDate.Value = DateTime.Today;
                        dtpFromdate.Value = DateTime.Today;
                        flagUpdate = false;
                        lblChecking.Text = "";
                        lblChecking.Visible = false;
                        btnSave.Enabled = true;
                         txtToMinutes.Text = txtToHours.Text = txtFrmMinutes.Text = txtFrmHours.Text = "00";
                         txtNoOfDays.Text = "0";       
                        GenerateTransactionNo();
                        gvCoffDetl.Rows.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void txtFrmHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFrmMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

       
        private void hoursWorked()
        {
             int ToTime = Convert.ToInt32(txtToHours.Text) * 60 + Convert.ToInt32(txtToMinutes.Text);
            int FromTime= Convert.ToInt32(txtFrmHours.Text) * 60 + Convert.ToInt32(txtFrmMinutes.Text);

            int TotalHours = ToTime - FromTime;

            if (TotalHours >= 240 && TotalHours < 480)
            {
                txtNoOfDays.Text = "0.5";
            }
            else if (TotalHours >= 480 && TotalHours < 720)
            {
                txtNoOfDays.Text = "1.0";
            }
            else if (TotalHours >= 720 && TotalHours < 960)
            {
                txtNoOfDays.Text = "1.5";
            }
            else if (TotalHours >= 960)
            {
                txtNoOfDays.Text = "2.0";
            }
            else
            {
                txtNoOfDays.Text = "0";
            }
        }

       

        private void txtFrmHours_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFrmHours.Text == "")
            {
                txtFrmHours.Text = "00";
            }
            else
            {
                //txtToHours.Text = (Convert.ToInt32(txtFrmHours.Text) + 1).ToString();
                //hoursWorked();
            }
        }

        private void txtFrmMinutes_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFrmMinutes.Text == "")
            {
                txtFrmMinutes.Text = "00";
            }
            else
            {
                hoursWorked();
            }
        }

        private void txtToHours_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtToHours.Text == "")
            {
                txtToHours.Text = "00";
            }
            else
            {
                hoursWorked();
            }
        }

        private void txtToMinutes_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtToMinutes.Text == "")
            {
                txtToMinutes.Text = "00";
            }
            else
            {
                hoursWorked();
            }
        }

        private void txtTranNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar!='\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        public void AddCoffDeatilsGrid()
        {
            try
            {
                
                bool isItemExisted = false;
               

                    for (int i = 0; i < gvCoffDetl.Rows.Count; i++)
                    {
                        if (gvCoffDetl.Rows[i].Cells["CoffDate"].Value.ToString().Equals(dtpFromdate.Value.ToString("dd/MMM/yyyy")))
                        {
                            isItemExisted = true;
                            return;
                        }
                    }

                    if (isItemExisted == false)
                    {


                        DataGridViewRow temprow = new DataGridViewRow();

                        DataGridViewCell Cellsno = new DataGridViewTextBoxCell();
                        Cellsno.Value = gvCoffDetl.Rows.Count + 1;
                        temprow.Cells.Add(Cellsno);

                        DataGridViewCell CellCoffDate = new DataGridViewTextBoxCell();
                        CellCoffDate.Value = dtpFromdate.Value.ToString("dd/MMM/yyyy");
                        temprow.Cells.Add(CellCoffDate);

                        DataGridViewCell CellFrmTime = new DataGridViewTextBoxCell();
                        CellFrmTime.Value = txtFrmHours.Text + '.' + txtFrmMinutes.Text;
                        temprow.Cells.Add(CellFrmTime);

                        DataGridViewCell CellToTime = new DataGridViewTextBoxCell();
                        CellToTime.Value = txtToHours.Text + '.' + txtToMinutes.Text;
                        temprow.Cells.Add(CellToTime);

                        DataGridViewCell CellNoofdays = new DataGridViewTextBoxCell();
                        CellNoofdays.Value = txtNoOfDays.Text;
                        temprow.Cells.Add(CellNoofdays);

                        DataGridViewCell CellReason = new DataGridViewTextBoxCell();
                        CellReason.Value = txtReason.Text;
                        temprow.Cells.Add(CellReason);


                        gvCoffDetl.Rows.Add(temprow);


                        //if (dgvAuditorDetails.Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < dgvAuditorDetails.Rows.Count; i++)
                        //    {
                        //        dgvAuditorDetails.Rows[i].Cells["SlNo1"].Value = i + 1;
                        //    }
                        //}

                    }                  
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CheckCoffData()
        {
            bool flag = true;

            //if (txtEName.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Valid Ecode", "Compensatory OFF ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtEcodeSearch.Focus();

            //}
            if (txtNoOfDays.Text == "0")
            {
                flag = false;
                MessageBox.Show("Please Enter From & To Time  ", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbDayType.Focus();

            }
            else if (txtReason.Text == string.Empty)
            {
                flag = false;
                MessageBox.Show("Please Enter Reason", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReason.Focus();

            }
         
            //else if (txtNameApprovBy.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Valid Approved Ecode", "Compensatory OFF ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtEcodeApprovBy.Focus();

            //}
            //else if (txtEcodeApprovBy.Text.Equals(txtEcodeSearch.Text))
            //{
            //    flag = false;
            //    MessageBox.Show("Ecode and Approved Ecode Should not be same", "Compensatory OFF ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtEcodeApprovBy.Text = "";
            //    txtNameApprovBy.Text = "";
            //    txtAppEmpDesg.Text = "";
            //    txtEcodeApprovBy.Focus();

            //}
            else if (dtpFromdate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("C-Off Working date is less than or Equal to Today's Date", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }

            //else if (isDateCheck() == true)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select valid Working date", "Compensatory OFF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return flag;
            //}
            //else if (CheckingRecord() == false)
            //{
            //    flag = false;
            //    return flag;
            //}

            return flag;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckCoffData())
            {
                AddCoffDeatilsGrid();
                txtReason.Text = "";               
                dtpFromdate.Value = DateTime.Today;
                txtToMinutes.Text = txtToHours.Text = txtFrmMinutes.Text = txtFrmHours.Text = "00";
                txtNoOfDays.Text = "0";           

            }
        }

        private void gvCoffDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvCoffDetl.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {

                        DataGridViewRow dgvr = gvCoffDetl.Rows[e.RowIndex];
                        gvCoffDetl.Rows.Remove(dgvr);

                        if (gvCoffDetl.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvCoffDetl.Rows.Count; i++)
                            {
                                gvCoffDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }
            }
        }
      

       
    }
}
