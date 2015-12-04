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
    public partial class LoanTypeMaster : Form
    {
        SQLDB objsqldb = null;
        DataTable dt = null;
        DataSet ds = null;
        bool flage = false;

        public LoanTypeMaster()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoanMaster_Load(object sender, EventArgs e)
        {
            dgvLoanMaster.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            chkLoanActive.Checked = true;
            FillLoanMasterDelToGrid();
        }

        private void btbClear_Click(object sender, EventArgs e)
        {
          
            txtLoanType.Text = "";
            txtLoanDescription.Text = "";
           
        }
        private void FillLoanMasterDelToGrid()
        {
            objsqldb = new SQLDB();
            ds = new DataSet();
            string StrCommand = "";
       

            try
            {
                StrCommand = "SELECT HPLTM_LOAN_TYPE ,HPLTM_LOAN_TYPE_DESC,HPLTM_RECORD_STATUS " +
                            " FROM HR_PARYOLL_LOAN_TYPE_MASTER ";
                ds = objsqldb.ExecuteDataSet(StrCommand);
                dt = ds.Tables[0];
                dgvLoanMaster.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow temprow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = (i + 1).ToString();
                    temprow.Cells.Add(cellSLNO);

                    DataGridViewCell cellLoanType = new DataGridViewTextBoxCell();
                    cellLoanType.Value = dt.Rows[i]["HPLTM_LOAN_TYPE"].ToString();
                    temprow.Cells.Add(cellLoanType);

                    DataGridViewCell cellLoanDescription = new DataGridViewTextBoxCell();
                    cellLoanDescription.Value = dt.Rows[i]["HPLTM_LOAN_TYPE_DESC"].ToString();
                    temprow.Cells.Add(cellLoanDescription);

                    //DataGridViewCell cellLoantatus = new DataGridViewTextBoxCell();
                    //cellLoantatus.Value = dt.Rows[i]["HPLTM_RECORD_STATUS"].ToString();
                    //temprow.Cells.Add(cellLoantatus);

                    DataGridViewCell cellLoantatus = new DataGridViewTextBoxCell();
                    if (dt.Rows[i]["HPLTM_RECORD_STATUS"].ToString() == "R")
                        cellLoantatus.Value = "ACTIVE";
                    else
                        cellLoantatus.Value = "DEACTIVE";
                    temprow.Cells.Add(cellLoantatus);

                    dgvLoanMaster.Rows.Add(temprow);




                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objsqldb = null;
                ds = null;
                dt = null;
            }
        }
        //private bool checkData()
        //{
        //    bool blValue = true;
        //    dt= new DataTable ();
        //    string strCommand = "";
            
        //    if(flage ==false)
        //    {
             
        //            strCommand ="SELECT * FROM HR_PARYOLL_LOAN_TYPE_MASTER WHERE HPLTM_LOAN_TYPE_DESC =' " + txtLoanDescription.Text +
        //                                                                            " '  AND HPLTM_RECORD_STATUS = 'A' " +
        //                                                                              "  AND  HPLTM_LOAN_TYPE ='" + txtLoanType.Text + "'";
        //            dt = objsqldb.ExecuteDataSet(strCommand).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                MessageBox.Show("LoneType Allready Exist!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //                txtLoanType.Text = "";
        //                txtLoanDescription.Text = "";
 
        //            }

                
        //    }
        //    return blValue;


        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            int ival = 0; 
            objsqldb = new SQLDB();
            string strCommand = "";
             dt = new DataTable();
             string sStatus = "";
            if (chkLoanActive.Checked == true)
                 sStatus = "R";
            else
                 sStatus = "C";
            if (txtLoanType.Text != "")
            {

                try
                {

                    strCommand = "SELECT * FROM HR_PARYOLL_LOAN_TYPE_MASTER WHERE  HPLTM_LOAN_TYPE ='"+txtLoanType.Text +"'";
                    dt = objsqldb.ExecuteDataSet(strCommand).Tables[0];

                    strCommand = "";

                    if (flage == true)
                    {
                        strCommand = "UPDATE HR_PARYOLL_LOAN_TYPE_MASTER SET   HPLTM_LOAN_TYPE_DESC ='" + txtLoanDescription.Text +
                                                                                       "',HPLTM_RECORD_STATUS = '" + sStatus + "' " +
                                                                                      "   WHERE HPLTM_LOAN_TYPE ='" + txtLoanType.Text + "'";
                    }
                    else if (flage == false)
                    {
                        if (dt.Rows.Count == 0)
                        {
                            strCommand = "INSERT INTO HR_PARYOLL_LOAN_TYPE_MASTER(HPLTM_LOAN_TYPE " +
                                                                                ",HPLTM_LOAN_TYPE_DESC " +
                                                                                ",HPLTM_RECORD_STATUS " +
                                                                                ")VALUES('" + txtLoanType.Text +
                                                                                       "','" + txtLoanDescription.Text + "','" + sStatus + "')";
                        }
                        else
                        {
                            MessageBox.Show("LoneType Allready Exist!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        }

                    }
                    if (strCommand.Length > 5)
                    {
                        ival = objsqldb.ExecuteSaveData(strCommand);
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
                if (ival > 0)
                {

                    MessageBox.Show("Data saved Succefully", "Loan Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flage = false;
                    FillLoanMasterDelToGrid();
                    txtLoanType.Clear();
                    txtLoanDescription.Clear();
                    chkLoanActive.Checked = true;


                }
                else
                {
                    MessageBox.Show("Data Not Saved", "Loan Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Lone Type","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }

        

        private void dgvLoanMaster_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dgvLoanMaster.Columns["Edit"].Index)
            {
                flage = true;

                txtLoanType.Text = dgvLoanMaster.Rows[e.RowIndex].Cells["HPLTM_LOAN_TYPE"].Value.ToString();
                txtLoanDescription.Text = dgvLoanMaster.Rows[e.RowIndex].Cells["HPLTM_LOAN_TYPE_DESC"].Value.ToString();
                chkLoanActive.Checked = true;

            }
        }

        private void btndelete_Click(object sender, EventArgs e)
            {
            int ival = 0;
            objsqldb = new SQLDB();
            dt = new DataTable();
            string strCommand = "";
            if (txtLoanType.Text != "")
            {
                try
                {
                    flage = true;
                    strCommand = "UPDATE HR_PARYOLL_LOAN_TYPE_MASTER SET HPLTM_LOAN_TYPE_DESC ='" +txtLoanDescription.Text +
                                                                                           "',HPLTM_RECORD_STATUS = 'c' " +
                                                                                           "  WHERE HPLTM_LOAN_TYPE ='"+txtLoanType.Text+"'";

                    if (strCommand.Length > 10)
                    {
                        ival = objsqldb.ExecuteSaveData(strCommand);
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
                if (ival > 0)
                {
                    
                    MessageBox.Show("Data Deleted Successfully", "LoanMaster", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillLoanMasterDelToGrid();
                    txtLoanType.Text = "";
                    txtLoanDescription.Text = "";
                }
                else
                {
                    MessageBox.Show("Data Not Deleted", "LoanMaster", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtLoanType_KeyPress(object sender, KeyPressEventArgs e)
         {

             e.Handled = (e.KeyChar == (char)Keys.Space);
            //if (char.IsLetter(e.KeyChar) == false)
            //    e.Handled = true;
            //e.KeyChar = Char.ToUpper(e.KeyChar);
             if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
             {
                 if (!char.IsLetter((e.KeyChar)))
                 {
                     e.KeyChar = Char.ToUpper(e.KeyChar);
                     e.Handled = true;
                 }
             }

        }

        private void txtLoanDescription_KeyPress(object sender, KeyPressEventArgs e)
        {

            //e.Handled = (e.KeyChar == (char)Keys.Space);

            e.KeyChar = Char.ToUpper(e.KeyChar);

        }

       

     
     }
}
