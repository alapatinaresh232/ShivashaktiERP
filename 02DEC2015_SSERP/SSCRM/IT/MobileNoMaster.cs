using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class MobileNoMaster : Form
    {
        SQLDB objDb = null;
        bool flage = false;
        string sStatus = "";
        public MobileNoMaster()
        {
            InitializeComponent();
        }

        private void MobileNoMaster_Load(object sender, EventArgs e)
        {
            gvMobileNoDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            FillCompanyData();
            FillMobileDetailsToGrid();
            DtpIssuedDate.Value = Convert.ToDateTime("01-01-1900");
            cbStatus.SelectedIndex = 0;

        }
        private void FillCompanyData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' AND CM_COMPANY_CODE IN ('NFL','VNF','SSBPL','SATL','SHS') ORDER BY CM_COMPANY_NAME";
                dt = objDb.ExecuteDataSet(strCmd).Tables[0];

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
                objDb = null;
                dt = null;
            }
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = (e.KeyChar == (char)Keys.Space);
            ////if (char.IsLetter(e.KeyChar) == false)
            ////    e.Handled = true;
            ////e.KeyChar = Char.ToUpper(e.KeyChar);
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetter((e.KeyChar)))
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    e.Handled = true;
                }
            }

        }


        private void txtDeptorDesign_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.KeyChar = Char.ToUpper(e.KeyChar);


        }
        private bool CheckData()
        {
            
            bool Chkflag = true;
            if (txtMobileNo.Text.Length == 0 || txtMobileNo.Text.Length != 10)
            {
                MessageBox.Show("  Enter Valid Mobile No", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                txtMobileNo.Focus();
                return Chkflag;
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                txtName.Focus();
                return Chkflag;
            }
            if (txtPlace.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Location", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                txtPlace.Focus();
                return Chkflag;
            }

            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                cbCompany.Focus();
                return Chkflag;
            }


            if (txtDeptorDesign.Text.Length == 0)
            {
                MessageBox.Show("Select Dept/Desig", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                txtDeptorDesign.Focus();
                return Chkflag;
            }
           

            if (cbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Select Status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                cbCompany.Focus();
                return Chkflag;
            }
            //if (txtCompanyLimit.Text.Length == 0)
            //{
            //    MessageBox.Show(" Please Enter Limit Amt", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Chkflag = false;
            //    cbCompany.Focus();
            //    return Chkflag;
            //}
            return Chkflag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (SaveMobileDetails() > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMobileNo.ReadOnly = false;
                    FillMobileDetailsToGrid();
                    flage = false;
                    txtMobileNo.Text = "";
                    txtName.Text = "";
                    txtPlace.Text = "";
                    cbCompany.SelectedIndex = 0;
                    txtDeptorDesign.Text = "";
                    cbStatus.SelectedIndex = 0;
                    DtpIssuedDate.Value = Convert.ToDateTime("01-01-1900");
                    txtCompanyLimit.Text = "";
                }
                else
                {
                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private int SaveMobileDetails()
        {
            int ival = 0;
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            //string sStatus="";
            string strCommand = "";
            try
            {
                if (txtCompanyLimit.Text.Length == 0)
                    txtCompanyLimit.Text = "0";

                if (flage == true)
                {

                    strCommand = "UPDATE MOBILE_NO_MASTER SET MBM_NAME='" + txtName.Text.ToString() +
                                                            "',MBM_COMPANY='" + cbCompany.SelectedValue.ToString() +
                                                            "',MBM_LOCATION ='" + txtPlace.Text.ToString() +
                                                            "',MBM_DEPT_DESIG='" + txtDeptorDesign.Text.ToString() +
                                                            "',MBM_LIMIT_FLAG='L',MBM_LIMIT_AMT=" + Convert.ToInt32(txtCompanyLimit.Text) + "";

                    if (Convert.ToDateTime(DtpIssuedDate.Value) > Convert.ToDateTime("01/JAN/1900"))
                    {
                        strCommand += ",MBM_ISSUED_DATE='" + DtpIssuedDate.Value.ToString("dd/MMM/yyyy") + "'";
                    }
                    strCommand += ",MBM_STATUS='" + sStatus +
                       "',MBM_MODIFIED_BY='" + CommonData.LogUserId +
                       "',MBM_MODIFIED_DATE=getdate()" +
                       " WHERE MBM_MOBILE_NO=" + txtMobileNo.Text.ToString() + " ";

                }
                else if (flage == false)
                {
                    strCommand = "INSERT INTO MOBILE_NO_MASTER(MBM_MOBILE_NO" +
                                                              ",MBM_NAME" +
                                                              ",MBM_COMPANY" +
                                                              ",MBM_LOCATION" +
                                                              ",MBM_DEPT_DESIG " +
                                                              ",MBM_LIMIT_FLAG" +
                                                              ",MBM_LIMIT_AMT ";

                    if (Convert.ToDateTime(DtpIssuedDate.Value) > Convert.ToDateTime("01/JAN/1900"))
                    {
                        strCommand += ",MBM_ISSUED_DATE";
                    }
                    strCommand += ",MBM_STATUS" +
                                                              ",MBM_CREATED_BY" +
                                                              ",MBM_CREATED_DATE" +
                                                              ")VALUES" +
                                                              "('" + txtMobileNo.Text.ToString() +
                                                              "','" + txtName.Text.ToString() +
                                                              "','" + cbCompany.SelectedValue.ToString() +
                                                              "','" + txtPlace.Text.ToString() +
                                                              "','" + txtDeptorDesign.Text.ToString() +
                                                              "','L'," + Convert.ToInt32(txtCompanyLimit.Text) + "";

                    if (Convert.ToDateTime(DtpIssuedDate.Value) > Convert.ToDateTime("01/JAN/1900"))
                    {
                        strCommand += ",'" + DtpIssuedDate.Value.ToString("dd/MMM/yyyy") + "'";
                    }
                    strCommand += ",'" + sStatus +
                       "','" + CommonData.LogUserId +
                       "',getdate())";
                }


                if (strCommand.Length > 10)
                {

                    ival = objDb.ExecuteSaveData(strCommand);
                }
                if (ival > 0)
                {
                    return ival;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ival;
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbStatus.SelectedIndex == 1)
            {
                sStatus = "ACTIVE";
            }
            else if (cbStatus.SelectedIndex == 2)
            {
                sStatus = "DEACTIVE";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtCompanyLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
        private void FillMobileDetailsToGrid()
        {
           
            int Row = 0;
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
             gvMobileNoDetails.Rows.Clear();
             try
             {

                 strCommand = "SELECT MBM_MOBILE_NO" +
                                  ",MBM_NAME" +
                                  ",MBM_COMPANY" +
                                  ",CM_COMPANY_NAME" +
                                  ",MBM_LOCATION" +
                                  ",MBM_DEPT_DESIG" +
                                  ",MBM_LIMIT_FLAG" +
                                  ",MBM_LIMIT_AMT" +
                                  ",MBM_ISSUED_DATE" +
                                  ",MBM_STATUS" +
                                  " FROM MOBILE_NO_MASTER " +
                                  "INNER JOIN COMPANY_MAS ON MBM_COMPANY=CM_COMPANY_CODE ";
                                     
                     dt = objDb.ExecuteDataSet(strCommand).Tables[0];

                     if (dt.Rows.Count > 0)
                     {
                         for (int i = 0; i < dt.Rows.Count; i++)
                         {
                             DataGridViewRow tempRow = new DataGridViewRow();

                             DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                             cellSlNo.Value = (i + 1).ToString();
                             tempRow.Cells.Add(cellSlNo);

                             DataGridViewCell cellMobileNo = new DataGridViewTextBoxCell();
                             cellMobileNo.Value = dt.Rows[i]["MBM_MOBILE_NO"].ToString();
                             tempRow.Cells.Add(cellMobileNo);

                             DataGridViewCell cellName = new DataGridViewTextBoxCell();
                             cellName.Value = dt.Rows[i]["MBM_NAME"].ToString();
                             tempRow.Cells.Add(cellName);



                             DataGridViewCell cellCompany = new DataGridViewTextBoxCell();
                             cellCompany.Value = dt.Rows[i]["CM_COMPANY_NAME"].ToString();
                             tempRow.Cells.Add(cellCompany);

                             DataGridViewCell cellCompanyCode = new DataGridViewTextBoxCell();
                             cellCompanyCode.Value = dt.Rows[i]["MBM_COMPANY"].ToString();
                             tempRow.Cells.Add(cellCompanyCode);

                             DataGridViewCell cellPlace = new DataGridViewTextBoxCell();
                             cellPlace.Value = dt.Rows[i]["MBM_LOCATION"].ToString();
                             tempRow.Cells.Add(cellPlace);



                             DataGridViewCell cellDeptorDesign = new DataGridViewTextBoxCell();
                             cellDeptorDesign.Value = dt.Rows[i]["MBM_DEPT_DESIG"].ToString();
                             tempRow.Cells.Add(cellDeptorDesign);                           

                            

                             DataGridViewCell cellLimitFlage = new DataGridViewTextBoxCell();
                             cellLimitFlage.Value = dt.Rows[i]["MBM_LIMIT_FLAG"].ToString();
                             tempRow.Cells.Add(cellLimitFlage);


                             DataGridViewCell cellLimitAmt = new DataGridViewTextBoxCell();
                             cellLimitAmt.Value = dt.Rows[i]["MBM_LIMIT_AMT"].ToString();
                             tempRow.Cells.Add(cellLimitAmt);

                             DataGridViewCell IssuedDate = new DataGridViewTextBoxCell();
                             IssuedDate.Value = dt.Rows[i]["MBM_ISSUED_DATE"].ToString();
                             tempRow.Cells.Add(IssuedDate);



                             DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                             cellStatus.Value = dt.Rows[i]["MBM_STATUS"].ToString();
                             tempRow.Cells.Add(cellStatus);




                             Row = Row + 1;
                             gvMobileNoDetails.Rows.Add(tempRow);




                         }

                     }
                     else
                     {
                         MessageBox.Show("No Mobile NO Details Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     }
                 

             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
            finally
            {
                objDb= null;
                dt = null;
            }



        }

        private void gvMobileNoDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvMobileNoDetails.Columns["Edit"].Index)
                {
                    flage = true;

                    txtMobileNo.ReadOnly = true;

                    txtMobileNo.Text = gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_MOBILE_NO"].Value.ToString();
                    txtName.Text = gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_NAME"].Value.ToString();
                    txtPlace.Text = gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_LOCATION"].Value.ToString();
                    cbCompany.Text = gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_COMPANY"].Value.ToString();
                    txtDeptorDesign.Text = gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_DEPT_DESIG"].Value.ToString();
                    txtCompanyLimit.Text = gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_LIMIT_AMT"].Value.ToString();
                    cbStatus.Text = gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_STATUS"].Value.ToString();
                    if (gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_ISSUED_DATE"].Value != "")
                    {
                        DtpIssuedDate.Value = Convert.ToDateTime(gvMobileNoDetails.Rows[e.RowIndex].Cells["MBM_ISSUED_DATE"].Value.ToString());
                    }

                }
            }         
                         
        }

        private void txtMobileNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtMobileNo.Text != "")
            {
                GetMobileNoDetails();
            }

        }
        private void GetMobileNoDetails()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            if (txtMobileNo.Text != "")
            {
                try
                {
                    strCommand = "SELECT MBM_MOBILE_NO" +
                                  ",MBM_NAME"+
                                  ",MBM_COMPANY" +
                                  ",MBM_LOCATION" +
                                  ",MBM_DEPT_DESIG" +
                                  ",MBM_LIMIT_AMT" +
                                  ",MBM_ISSUED_DATE" +
                                  ",MBM_STATUS" +
                                  //",MBM_CREATED_BY" +
                                  //",MBM_CREATED_DATE" +
                                  " FROM MOBILE_NO_MASTER"+
                                  " WHERE MBM_MOBILE_NO="+txtMobileNo.Text.ToString()+"";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        flage = true;

                        txtMobileNo.Text = dt.Rows[0]["MBM_MOBILE_NO"].ToString();
                        txtName.Text = dt.Rows[0]["MBM_NAME"].ToString();
                        cbCompany.SelectedValue = dt.Rows[0]["MBM_COMPANY"].ToString();
                        txtPlace.Text = dt.Rows[0]["MBM_LOCATION"].ToString();
                        txtDeptorDesign.Text = dt.Rows[0]["MBM_DEPT_DESIG"].ToString();
                        txtCompanyLimit.Text = dt.Rows[0]["MBM_LIMIT_AMT"].ToString();
                        if (dt.Rows[0]["MBM_ISSUED_DATE"].ToString() != "")
                        {
                            DtpIssuedDate.Value = Convert.ToDateTime(dt.Rows[0]["MBM_ISSUED_DATE"].ToString());
                        }
                        else
                        {
                            DtpIssuedDate.Value = Convert.ToDateTime("01/JAN/1900");
                        }
                        cbStatus.Text = dt.Rows[0]["MBM_STATUS"].ToString();

                      
                      
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                    dt = null;
                }
            }         
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objDb = new SQLDB();
            int iResult = 0;
            if (txtMobileNo.Text != "") 
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        string strDelete = "DELETE FROM MOBILE_NO_MASTER WHERE MBM_MOBILE_NO='" + txtMobileNo.Text.ToString() + "'";
                        iResult = objDb.ExecuteSaveData(strDelete);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (iResult > 0)
                    {
                        flage = false;

                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillMobileDetailsToGrid();
                        btnCancel_Click(null, null);


                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Please Enter Mobile No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flage = false;

            txtMobileNo.ReadOnly = false;
            txtMobileNo.Text = "";
            txtName.Text = "";
            txtPlace.Text = "";
            cbCompany.SelectedIndex = 0;
            txtDeptorDesign.Text = "";
            cbStatus.SelectedIndex = 0;
            DtpIssuedDate.Value = Convert.ToDateTime("01-01-1900");
            txtCompanyLimit.Text = "";
        }

     
    }
}
