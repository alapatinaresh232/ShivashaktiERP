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
    public partial class MobileNoMonthlyBills : Form
    {
        SQLDB objSQLdb = null;
           

        public MobileNoMonthlyBills()
        {
            InitializeComponent();
        }

        private void MobileNoMonthlyBills_Load(object sender, EventArgs e)
        {
            gvMobileNoBillDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);

            dtpMonth.Value = DateTime.Today;

            FillCompanyData();
        }


        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
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
        
        private DataSet GetMobileNoBillDetails(string CompCode,string DocMonth,string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MobileBillDetails", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }



        private void FillMobileNoBillDetailsToGrid()
        {           
            objSQLdb = new SQLDB();            
            DataTable dt = new DataTable();
            gvMobileNoBillDetails.Rows.Clear();
            if (cbCompany.SelectedIndex > 0)
            {
                try
                {

                    dt = GetMobileNoBillDetails(cbCompany.SelectedValue.ToString(), Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper(), "").Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvMobileNoBillDetails.Rows.Add();

                            gvMobileNoBillDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["MobileId"].Value = dt.Rows[i]["ID"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["CompCode"].Value = dt.Rows[i]["CompCode"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["CompanyName"].Value = dt.Rows[i]["CompName"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["MobileNo"].Value = dt.Rows[i]["MobileNo"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["Name"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["BillAmount"].Value = dt.Rows[i]["BillAmt"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["CompanyLimit"].Value = dt.Rows[i]["LimitAmt"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["PersonalAmt"].Value = dt.Rows[i]["PersAmt"].ToString();
                            gvMobileNoBillDetails.Rows[i].Cells["PaidAmt"].Value = dt.Rows[i]["PaidAmt"].ToString();


                        }
                        TotalBillAmount();
                        TotalPaidAmt();
                        TotalPersonalAmt();
                        TotalCompanyLimitAmt();
                    }
                    else
                    {
                        txtTotalBillAmt.Text = "";
                        txtTotalPaidAmt.Text = "";
                        txtTotCompLimitAmt.Text = "";
                        txtTotPersonalAmt.Text = "";
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
      

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private bool CheckData()
        {         
            bool flag = true;

            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
            }

            else if (gvMobileNoBillDetails.Rows.Count > 0)
            {

                for (int i = 0; i < gvMobileNoBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvMobileNoBillDetails.Rows[i].Cells["BillAmount"].Value) == "")
                    {
                        gvMobileNoBillDetails.Rows[i].Cells["BillAmount"].Value = "0";
                    }
                    if (Convert.ToString(gvMobileNoBillDetails.Rows[i].Cells["CompanyLimit"].Value) == "")
                    {
                        gvMobileNoBillDetails.Rows[i].Cells["CompanyLimit"].Value = "0";
                    }
                    if (Convert.ToString(gvMobileNoBillDetails.Rows[i].Cells["PersonalAmt"].Value) == "")
                    {
                        gvMobileNoBillDetails.Rows[i].Cells["PersonalAmt"].Value = "0";
                    }
                    if (Convert.ToString(gvMobileNoBillDetails.Rows[i].Cells["PaidAmt"].Value) == "")
                    {
                        gvMobileNoBillDetails.Rows[i].Cells["PaidAmt"].Value = "0";
                    }
                }
            }
                  
               return flag;
        }

      

        private void btnSave_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            objSQLdb = new SQLDB();
            string strCommand = "";           
            if (CheckData())
            {
                try
                {
                   
                    if (gvMobileNoBillDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvMobileNoBillDetails.Rows.Count; i++)
                        {                           

                            string strDelete = "DELETE FROM MOBILE_MONTHLY_BILLS WHERE MB_MOBILE_NO=" + gvMobileNoBillDetails.Rows[i].Cells["MobileNo"].Value.ToString() +
                                        " AND MB_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper() + "'";
                            iRes = objSQLdb.ExecuteSaveData(strDelete);

                            strCommand += "INSERT INTO MOBILE_MONTHLY_BILLS(MB_MOBILE_NO" +
                                                                          ",MB_MONTH " +
                                                                          ",MB_BILL_AMOUNT " +
                                                                          ",MB_COMP_LIMIT " +
                                                                          ",MB_PERS_AMT " +
                                                                          ",MB_PAID_AMOUNT " +
                                                                          ",MB_CREATED_BY " +
                                                                          ",MB_CREATED_DATE)" +
                                                                          " VALUES " +
                                                                          "(" + gvMobileNoBillDetails.Rows[i].Cells["MobileNo"].Value.ToString() +
                                                                          ",'" + Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                          "'," + Convert.ToDouble(gvMobileNoBillDetails.Rows[i].Cells["BillAmount"].Value).ToString("0.00") +
                                                                          "," + Convert.ToDouble(gvMobileNoBillDetails.Rows[i].Cells["CompanyLimit"].Value).ToString("0.00") +
                                                                          "," + gvMobileNoBillDetails.Rows[i].Cells["PersonalAmt"].Value.ToString() +
                                                                          "," + gvMobileNoBillDetails.Rows[i].Cells["PaidAmt"].Value.ToString() +
                                                                          ",'" + CommonData.LogUserId +
                                                                          "',getdate())";


                        }
                    }

                    if (strCommand.Length > 10)
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

                    cbCompany.SelectedIndex = 0;
                    dtpMonth.Value = DateTime.Today;
                    gvMobileNoBillDetails.Rows.Clear();
                    txtTotalBillAmt.Text = "";
                    txtTotalPaidAmt.Text = "";
                    txtTotCompLimitAmt.Text = "";
                    txtTotPersonalAmt.Text = "";
                }
                else
                {
                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            FillMobileNoBillDetailsToGrid();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillMobileNoBillDetailsToGrid();
            }
            else
            {
                gvMobileNoBillDetails.Rows.Clear();
                txtTotalBillAmt.Text = "";
                txtTotalPaidAmt.Text = "";
                txtTotCompLimitAmt.Text = "";
                txtTotPersonalAmt.Text = "";
            }
        }
     


        #region "CALCULATE TOTALS"
        private void TotalBillAmount()
        {
            double totBillAmt = 0;
            for (int nRow = 0; nRow < gvMobileNoBillDetails.Rows.Count; nRow++)
            {

                if (gvMobileNoBillDetails.Rows[nRow].Cells["BillAmount"].Value.ToString() != "")
                {
                    totBillAmt += Convert.ToDouble(gvMobileNoBillDetails.Rows[nRow].Cells["BillAmount"].Value.ToString());
                }

            }
            txtTotalBillAmt.Text = totBillAmt.ToString("f");
        }

        private void TotalCompanyLimitAmt()
        {
            double totCompLimitAmt = 0;
            for (int nRow = 0; nRow < gvMobileNoBillDetails.Rows.Count; nRow++)
            {

                if (Convert.ToString(gvMobileNoBillDetails.Rows[nRow].Cells["CompanyLimit"].Value) != "")
                {
                    totCompLimitAmt += Convert.ToDouble(gvMobileNoBillDetails.Rows[nRow].Cells["CompanyLimit"].Value);
                }

            }
            txtTotCompLimitAmt.Text = totCompLimitAmt.ToString("f");

        }

        private void TotalPersonalAmt()
        {
            double totPersonalAmt = 0;

            for (int nRow = 0; nRow < gvMobileNoBillDetails.Rows.Count; nRow++)
            {

                if (Convert.ToString(gvMobileNoBillDetails.Rows[nRow].Cells["PersonalAmt"].Value) != "")
                {

                    totPersonalAmt += Convert.ToDouble(gvMobileNoBillDetails.Rows[nRow].Cells["PersonalAmt"].Value);
                }

            }

            txtTotPersonalAmt.Text = totPersonalAmt.ToString("f");
        }

        private void TotalPaidAmt()
        {
            double totPaidAmt = 0;
            for (int nRow = 0; nRow < gvMobileNoBillDetails.Rows.Count; nRow++)
            {

                if (Convert.ToString(gvMobileNoBillDetails.Rows[nRow].Cells["PaidAmt"].Value) != "")
                {
                    totPaidAmt += Convert.ToDouble(gvMobileNoBillDetails.Rows[nRow].Cells["PaidAmt"].Value);
                }

            }
            txtTotalPaidAmt.Text = totPaidAmt.ToString("f");
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            dtpMonth.Value = DateTime.Today;
            gvMobileNoBillDetails.Rows.Clear();
            txtTotalBillAmt.Text = "";
            txtTotalPaidAmt.Text = "";
            txtTotCompLimitAmt.Text = "";
            txtTotPersonalAmt.Text = "";

        }

     

        private void gvMobileNoBillDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double BillAmount = 0;
            double CompanyLimitAmt = 0;
            double PersonalAmt = 0;


            if ((Convert.ToString(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["BillAmount"].Value) != ""))
            {
                if (Convert.ToString(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["CompanyLimit"].Value) != "")
                {
                    BillAmount = Convert.ToDouble(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["BillAmount"].Value.ToString());
                    CompanyLimitAmt = Convert.ToDouble(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["CompanyLimit"].Value.ToString());

                    if (BillAmount > CompanyLimitAmt)
                    {
                        PersonalAmt = BillAmount - CompanyLimitAmt;
                        gvMobileNoBillDetails.Rows[e.RowIndex].Cells["PersonalAmt"].Value = Convert.ToString(PersonalAmt);
                        gvMobileNoBillDetails.Rows[e.RowIndex].Cells["PersonalAmt"].Value = Convert.ToDouble(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["PersonalAmt"].Value).ToString("f");
                    }
                    else
                    {
                        gvMobileNoBillDetails.Rows[e.RowIndex].Cells["PersonalAmt"].Value = "0";
                    }
                }
                else
                {
                    gvMobileNoBillDetails.Rows[e.RowIndex].Cells["CompanyLimit"].Value = "0";
                    //gvMobileNoBillDetails.Rows[e.RowIndex].Cells["PersonalAmt"].Value = "0";
                }

            }
            else
            {
                gvMobileNoBillDetails.Rows[e.RowIndex].Cells["BillAmount"].Value = "0";
                gvMobileNoBillDetails.Rows[e.RowIndex].Cells["PersonalAmt"].Value = "0";
            }

            if ((Convert.ToString(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["BillAmount"].Value)!= ""))
            {
                gvMobileNoBillDetails.Rows[e.RowIndex].Cells["PaidAmt"].Value = Convert.ToDouble(gvMobileNoBillDetails.Rows[e.RowIndex].Cells["BillAmount"].Value).ToString("f"); 
            }

            TotalBillAmount();
            TotalCompanyLimitAmt();
            TotalPersonalAmt();
            TotalPaidAmt();
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
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
        }

        private void gvMobileNoBillDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress); 
        }

      
       
        
        
    }

      
}
