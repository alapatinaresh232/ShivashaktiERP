using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
using System.IO;



namespace SSCRM
{
    public partial class PaymentMode : Form
    {
        SQLDB objSQLdb = null;
        public PaymentMode()
        {
            InitializeComponent();
        }

     

        private void PaymentMode_Load(object sender, EventArgs e)
        {
            cbPaymentMode.SelectedIndex =-1;
        }
        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                picEmpPhoto.BackgroundImage = newImage;
                this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void GetEmployeePayMentDetails()
        {
         
            objSQLdb= new SQLDB();
            DataTable dt = new DataTable();
            DataSet dsPhoto = new DataSet();

            if (txtEcodeSearch.Text != "")
            {

                try
                {
                    string strcmd = "SELECT  MEMBER_NAME EmpName " +
                                            ",CM_COMPANY_NAME CompName " +
                                            ",CM_COMPANY_CODE CompCode" +
                                            ",BRANCH_NAME BranchName" +
                                            ",BM.BRANCH_CODE BranchCode" +
                                            ",dept_name DeptName" +
                        //",DT.dept_code" +
                                            ",DESIG EmpDesig" +
                        //", DS.desig_code" +   
                                            ",HPCM_WAGEMONTH WageMonth" +
                                            ",HPCM_PS_NET_PAY NetPaySal" +
                                            ",HPCM_PAY_MODE PayMode" +
                                            ",HPCM_PS_BANK_NAME BankName" +
                                            ",HPCM_PS_BANK_ACCOUNT_NO BankAccNo" +
                                            ",HAPS_APPL_NUMBER ApplNo" +                                          
                                            " FROM EORA_MASTER EM " +
                                            " INNER JOIN BRANCH_MAS BM ON EM.BRANCH_CODE= BM.BRANCH_CODE " +
                                            " INNER JOIN COMPANY_MAS CM ON CM.CM_COMPANY_CODE=BM.COMPANY_CODE " +
                                            " INNER JOIN Dept_Mas DT ON  DT.dept_code= EM.DEPT_ID" +
                                            " INNER JOIN DESIG_MAS DS ON DS.desig_code=EM.DESG_ID  " +
                                            " INNER JOIN HR_APPL_PHOTO_SIG ON EM.ECODE=HAPS_EORA_CODE "+
                                            " LEFT JOIN HR_PAYROLL_CALC_MONYY ON HPCM_EORA_CODE=EM.ECODE  " +
                                            " AND HPCM_WAGEMONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper() +
                                            "' WHERE  ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "";

                    dt = objSQLdb.ExecuteDataSet(strcmd).Tables[0];
                    dsPhoto = objSQLdb.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + txtEcodeSearch.Text);

                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                        txtComp.Text = dt.Rows[0]["CompName"].ToString();
                        txtComp.Tag = dt.Rows[0]["CompCode"].ToString();
                        txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
                        txtBranch.Tag = dt.Rows[0]["BranchCode"].ToString();
                        txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                        txtHRISDesig.Text = dt.Rows[0]["EmpDesig"].ToString();
                        txtNetSal.Text = dt.Rows[0]["NetPaySal"].ToString();
                        cbPaymentMode.SelectedItem = dt.Rows[0]["PayMode"].ToString();
                        txtBankName.Text = dt.Rows[0]["BankName"].ToString();
                        txtAccNo.Text = dt.Rows[0]["BankAccNo"].ToString();
                        txtApplNo.Text = dt.Rows[0]["ApplNo"].ToString();
                        if (dsPhoto.Tables[0].Rows.Count > 0)
                            GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                        else
                            picEmpPhoto.BackgroundImage = null;
            


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
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

      
        private bool CheckData()
        {
            bool flag = true;
            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
          
            return flag;
        }
        //private void GetPaymentDetails()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    string strCommand = "";
        //    try
        //    {
        //        strCommand = "SELECT HPCM_PS_NET_PAY" +
        //                            ",HPCM_PAY_MODE" +
        //                            ",HPCM_PS_BANK_NAME" +
        //                            ",HPCM_PS_BANK_ACCOUNT_NO" +
        //                            " FROM HR_PAYROLL_CALC_MONYY " +
        //                            " WHERE HPCM_WAGEMONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString(" MMM/yyyy") +
        //                            "' AND HPCM_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text.ToString()) + "";
        //        dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            txtNetSal.Text = dt.Rows[0]["HPCM_PS_NET_PAY"].ToString();
        //            txtPayMode.Text = dt.Rows[0]["HPCM_PAY_MODE"].ToString();
        //            txtBankName.Text = dt.Rows[0]["HPCM_PS_BANK_NAME"].ToString();
        //            txtAccNo.Text = dt.Rows[0]["HPCM_PS_BANK_ACCOUNT_NO"].ToString();

        //        }
        //        else
        //        {
        //            txtNetSal.Text = "";
        //            txtPayMode.Text = "";
        //            txtBankName.Text = "";
        //            dtpMonth.Value = DateTime.Today;
        //            txtEcodeSearch.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            int iVal = 0;
            objSQLdb = new SQLDB();
         
            string StrCommand = "";
            if (CheckData())
            {
                try
                {

                    StrCommand = "UPDATE  HR_PAYROLL_CALC_MONYY SET  HPCM_PAY_MODE='" + cbPaymentMode.SelectedItem.ToString() +
                                                               "', HPCM_MODIFIED_BY='" + CommonData.LogUserId +
                                                               "',HPCM_MODIFIED_DATE=getdate()" +
                                                               "  WHERE HPCM_WAGEMONTH ='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                               "' AND HPCM_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text.ToString()) + "";
                    iVal = objSQLdb.ExecuteSaveData(StrCommand);



                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                if (iVal > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }

                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        


        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {

            if (txtEcodeSearch.Text != "")
            {
                GetEmployeePayMentDetails();
            }
                
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {   txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtDept.Text = "";
            txtComp.Text = "";
            txtHRISDesig.Text = "";
            txtBranch.Text = "";
            txtNetSal.Text = "";
            cbPaymentMode.SelectedIndex =-1;
            txtBankName.Text = "";
            dtpMonth.Value = DateTime.Today;
            txtApplNo.Text = "";
            picEmpPhoto.BackgroundImage = null;
           
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {

            if (txtEcodeSearch.Text.Length > 4)
            {
                GetEmployeePayMentDetails();
            }
            else
            {
              
                txtEName.Text = "";
                txtDept.Text = "";
                txtComp.Text = "";
                txtHRISDesig.Text = "";
                txtBranch.Text = "";
                txtNetSal.Text = "";
                cbPaymentMode.SelectedIndex = -1;
                txtBankName.Text = "";
                dtpMonth.Value = DateTime.Today;
                txtApplNo.Text = "";
                picEmpPhoto.BackgroundImage = null;
            }
        }

        private void txtNetSal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

       
    }
}
