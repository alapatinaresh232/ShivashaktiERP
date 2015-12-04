using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient; 
using SSTrans;

namespace SSCRM
{
    public partial class MobileNoBillDtails : Form
    {
        SQLDB objSqlDb = null;
        bool flagUpdate = false;
        
        public MobileNoBillDtails()
        {
            InitializeComponent();
        }

        private void MobileNoBillDtails_Load(object sender, EventArgs e)
        {
            gvMobileNoBillDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                   System.Drawing.FontStyle.Regular);
            dtpMonth.Value = DateTime.Today;
        }
        private DataSet MobileNumberBillDetails(string MobileNo)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSqlDb.CreateParameter("@xMobileNo", DbType.String, MobileNo, ParameterDirection.Input);

                ds = objSqlDb.ExecuteDataSet("Get_MobileNoBillDetails", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSqlDb = null;
            }
            return ds;
        }

        private void GetMobileNoBillDetails()
        {
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            gvMobileNoBillDetails.Rows.Clear();
         

            try
            {

                dt = GetMobileMonthlyBillDetails(txtMobileNo.Text.ToString(),"").Tables[0];


                if (dt.Rows.Count > 0)
                {

                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtCompany.Text = dt.Rows[0]["CompName"].ToString();
                    txtPlace.Text = dt.Rows[0]["Place"].ToString();
                    txtDeptorDesign.Text = dt.Rows[0]["DeptDesg"].ToString();
                    txtCompLimit.Text = dt.Rows[0]["LimitAmt"].ToString();
                    if (dt.Rows[0]["IssuedDate"].ToString() == "")
                    {
                        txtIssuedDate.Text = "";
                    }
                    else
                    {
                        txtIssuedDate.Text = Convert.ToDateTime(dt.Rows[0]["IssuedDate"]).ToShortDateString();
                    }
                    txtStatus.Text = dt.Rows[0]["Status"].ToString();

                }
                else
                {
                    txtName.Text = "";
                    txtCompany.Text = "";
                    txtDeptorDesign.Text = "";
                    txtIssuedDate.Text = "";
                    txtStatus.Text = "";
                  
                    txtPlace.Text = "";
                    gvMobileNoBillDetails.Rows.Clear();


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlDb = null;
                dt = null;
            }
 
        }
        private void FillMobileNoBillDetailsToGrid()
        {
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            gvMobileNoBillDetails.Rows.Clear();
            try
            {
                  dt =MobileNumberBillDetails(txtMobileNo.Text.ToString()).Tables[0];


                  if (dt.Rows.Count > 0)
                  {
                      for (int i = 0; i < dt.Rows.Count; i++)
                      {

                          gvMobileNoBillDetails.Rows.Add();
                          gvMobileNoBillDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                          gvMobileNoBillDetails.Rows[i].Cells["ID"].Value = dt.Rows[i]["ID"].ToString();
                          gvMobileNoBillDetails.Rows[i].Cells["MonthDate"].Value = Convert.ToDateTime(dt.Rows[i]["Month"]).ToString("MMMyyyy").ToUpper();
                          gvMobileNoBillDetails.Rows[i].Cells["BillAmount"].Value = dt.Rows[i]["BillAmt"].ToString();
                          gvMobileNoBillDetails.Rows[i].Cells["CompanyLimit"].Value = dt.Rows[i]["LimitAmt"].ToString();
                          gvMobileNoBillDetails.Rows[i].Cells["PersonalAmt"].Value = dt.Rows[i]["PersAmt"].ToString();
                          gvMobileNoBillDetails.Rows[i].Cells["PaidAmt"].Value = dt.Rows[i]["PaidAmt"].ToString();
                      }
                  }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlDb = null;
                dt = null;
            }
        }
        private DataSet GetMobileMonthlyBillDetails(string MobileNo, string DocMonth)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSqlDb.CreateParameter("@xMobileNo", DbType.String, MobileNo, ParameterDirection.Input);
                param[1] = objSqlDb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);

                ds = objSqlDb.ExecuteDataSet("Get_MobileNoMonthDeatils", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSqlDb = null;
            }
            return ds;
        }
        private void GetMobileMonthlyBillDetails()
        {
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
          
            try
            {
                dt = GetMobileMonthlyBillDetails(txtMobileNo.Text.ToString(), Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper()).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["BillAmt"].ToString() != "")
                    {
                        flagUpdate = true;
                    }

                    txtBillAmt.Text = dt.Rows[0]["BillAmt"].ToString();
                    txtCompLimit.Text = dt.Rows[0]["LimitAmt"].ToString();
                    txtPersonal.Text = dt.Rows[0]["PersAmt"].ToString();
                    txtPaidAmt.Text = dt.Rows[0]["PaidAmt"].ToString();

                }
                else
                {
                    txtBillAmt.Text = "";
                    //txtCompLimit.Text = "";
                    txtPersonal.Text = "";
                    txtPaidAmt.Text = "";
                    flagUpdate = false;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlDb = null;
                dt = null;
            }
 
        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
       {
             if(txtMobileNo.Text != "")
           {
               GetMobileMonthlyBillDetails();
               //flagUpdate = true;
           }
         
        }
      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtMobileNo.Text = "";
            txtMobileNo.Focus();
            txtName.Text = "";
            txtCompany.Text = "";
            txtDeptorDesign.Text = "";
            txtIssuedDate.Text = "";
            txtStatus.Text = "";
            dtpMonth.Value = DateTime.Today;
            txtPlace.Text = "";
            gvMobileNoBillDetails.Rows.Clear();
           flagUpdate = false;

        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') 
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMobileNo_Validated(object sender, EventArgs e)
        {
            if (txtMobileNo.Text != "")
            {
                GetMobileNoBillDetails();
                FillMobileNoBillDetailsToGrid();
                //txtMobileNo.Focus();
            }
            //else
            //{
            //    txtName.Text = "";
            //    txtCompany.Text = "";
            //    txtDeptorDesign.Text = "";
            //    txtMonthDate.Text = "";
            //    txtStatus.Text = "";

            //    txtPlace.Text = "";
            //    gvMobileNoBillDetails.Rows.Clear();

            //}
            dtpMonth_ValueChanged(null, null);
        }

        private void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtCompany.Text = "";
            txtDeptorDesign.Text = "";
            txtIssuedDate.Text = "";
            txtStatus.Text = ""; 
          
            txtPlace.Text = "";
            txtPaidAmt.Text = "";
            txtPersonal.Text = "";
            txtCompLimit.Text = "";
            txtBillAmt.Text = "";
          
            gvMobileNoBillDetails.Rows.Clear();
            if (txtMobileNo.Text.Length >= 9)
            {
                txtMobileNo_Validated(null, null);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int ival = 0;
            objSqlDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                if (flagUpdate == true)
                {
                    strCommand = "UPDATE  MOBILE_MONTHLY_BILLS SET MB_MOBILE_NO=" + txtMobileNo.Text.ToString() +
                                                                 ",MB_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                 "',MB_BILL_AMOUNT='" + txtBillAmt.Text.ToString() +
                                                                 "',MB_PAID_AMOUNT='" + txtPaidAmt.Text.ToString() +
                                                                 "',MB_COMP_LIMIT='" + txtCompLimit.Text.ToString() +
                                                                 "',MB_PERS_AMT='" + txtPersonal.Text.ToString() +
                                                                 "',MB_MODIFIED_BY='" + CommonData.LogUserId +
                                                                 "',MB_MODIFIED_DATE=getdate()" +
                                                                 " WHERE  MB_MOBILE_NO=" + txtMobileNo.Text.ToString() +
                                                                  " AND MB_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper() + "'";
                }
                if (flagUpdate == false)
                {
                    strCommand = "INSERT INTO MOBILE_MONTHLY_BILLS (MB_MOBILE_NO" +
                                                                   ",MB_MONTH" +
                                                                   ",MB_BILL_AMOUNT" +
                                                                   ",MB_PAID_AMOUNT" +
                                                                   ",MB_COMP_LIMIT" +
                                                                   ",MB_PERS_AMT" +
                                                                   ",MB_CREATED_BY" +
                                                                   ",MB_CREATED_DATE)" +
                                                                   " VALUES" +
                                                                   "(" + txtMobileNo.Text.ToString() +
                                                                   ",'" +  Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper()+
                                                                   "','" + txtBillAmt.Text.ToString() +
                                                                   "','" + txtPaidAmt.Text.ToString() +
                                                                   "','" + txtCompLimit.Text.ToString() +
                                                                   "','" + txtPersonal.Text.ToString() +
                                                                   "','" + CommonData.LogUserId +
                                                                              "',getdate())";
                }
                if (strCommand.Length > 5)
                {
                    ival = objSqlDb.ExecuteSaveData(strCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (ival>0)
            {
                MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flagUpdate = false;
                //dtpMonth.Value = DateTime.Today;
                txtBillAmt.Text = "";
                txtCompLimit.Text = "";
                txtPaidAmt.Text = "";
                txtPersonal.Text = "";
                FillMobileNoBillDetailsToGrid();
             
              
              
            }
            else
            {
                MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

         

        }

        private void txtMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
          
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    
        }

        private void txtCompLimit_TextChanged(object sender, EventArgs e)
        {
            Int32 Billamount = 0;
            Int32 CompanyLimitAmount = 0;
            Int32 PersonalAmt = 0;
            if (txtBillAmt.Text != "")
            {
                if (txtCompLimit.Text != "")
                {
                    Billamount = Convert.ToInt32(txtBillAmt.Text.ToString());
                    CompanyLimitAmount = Convert.ToInt32(txtCompLimit.Text.ToString());
                    if (Billamount > CompanyLimitAmount)
                    {
                        PersonalAmt = Billamount - CompanyLimitAmount;
                        txtPersonal.Text = Convert.ToString(PersonalAmt);
                       
                    }
                    else
                    {
                        txtPersonal.Text = "0";

                    }

                }
                else
                {
                    txtCompLimit.Text = "0";

                }
            }
            else
            {
                txtPersonal.Text = "0";
                txtPaidAmt.Text = "0";
            }
            if (txtBillAmt.Text != "")
            {
                txtPaidAmt.Text = txtBillAmt.Text.ToString();
            }
            else
            {
                txtPaidAmt.Text = "";
                txtPersonal.Text = "";
                //txtCompLimit.Text = "";
                txtBillAmt.Text = "";
               

            }
        }

        private void gvMobileNoBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ival = 0;
            if (e.ColumnIndex == gvMobileNoBillDetails.Columns["Delete"].Index)
            {
                if (e.RowIndex >= 0)
                {
                    DialogResult dlgresult;

                    dlgresult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgresult == DialogResult.Yes)
                    {
                        string  objDocMonth =Convert.ToDateTime(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["MonthDate"].Value).ToString("MMMyyyy").ToUpper();
                        objSqlDb = new SQLDB();
                        try
                        {
                            string Strcommand = "DELETE FROM MOBILE_MONTHLY_BILLS WHERE MB_ID=" + gvMobileNoBillDetails.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                             ival = objSqlDb.ExecuteSaveData(Strcommand);
                            if (ival > 0)
                            {
                                MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                FillMobileNoBillDetailsToGrid();

                            }
                            else
                            {
                                MessageBox.Show("Data not Deleted ", "Invoice Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
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
                }
            }
        }

        private void txtBillAmt_KeyUp(object sender, KeyEventArgs e)
        {
            txtCompLimit_TextChanged(null, null);
        }

       
      
       
    }
}
