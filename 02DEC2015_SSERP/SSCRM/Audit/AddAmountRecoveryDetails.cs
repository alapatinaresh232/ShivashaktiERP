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

namespace SSCRM
{
    public partial class AddAmountRecoveryDetails : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;

        public MisconductForm objMisconductForm;
        string strFormType = "";
        string stCompCode = "", stBranCode = "", stDocMonth = "";
        DataRow[] drs;


        public AddAmountRecoveryDetails(string sfrmType,string CompCode,string BranCode,string DocMonth)
        {
            InitializeComponent();
            strFormType = sfrmType;
            stCompCode = CompCode;
            stBranCode = BranCode;
            stDocMonth = DocMonth;
        }
        public AddAmountRecoveryDetails(string sfrmType,DataRow[] dr)
        {
            InitializeComponent();
            strFormType = sfrmType;
            drs = dr;
        }

        private void AddAmountRecoveryDetails_Load(object sender, EventArgs e)
        {
            FillEmployeeData();
            dtpReceiptDate.Value = DateTime.Today;
            cbRecType.SelectedIndex = 0;

            if (strFormType.Equals("AmountRecovery"))
            {
                if (drs != null)
                {
                    cbRecType.Text = drs[0]["ReceiptType"].ToString();

                    if (drs[0]["ReceiptDate"].ToString() != "")
                    dtpReceiptDate.Value = Convert.ToDateTime(drs[0]["ReceiptDate"].ToString());

                    cbEmployees.Text = drs[0]["RecEcode"].ToString() + '-' + drs[0]["RecEmpName"].ToString();                   
                    txtReceiptNo.Text = drs[0]["ReceiptNo"].ToString();
                    txtRemarks.Text = drs[0]["RecRemarks"].ToString();
                    txtAmount.Text = drs[0]["RecAmount"].ToString();
                }
            }
            else if (strFormType.Equals("ActualAmountRecovery"))
            {
                if (drs != null)
                {
                    cbRecType.Text = drs[0]["ActualRcptType"].ToString();

                    if (drs[0]["ActualRcptDate"].ToString() != "")
                        dtpReceiptDate.Value = Convert.ToDateTime(drs[0]["ActualRcptDate"].ToString());

                    cbEmployees.Text = drs[0]["ActualRecEcode"].ToString() + '-' + drs[0]["ActualRecName"].ToString();
                    
                    txtReceiptNo.Text = drs[0]["ActualRcptNo"].ToString();
                    txtRemarks.Text = drs[0]["ActualRecRemarks"].ToString();
                    txtAmount.Text = drs[0]["ActualRecAmount"].ToString();
                }

            }
        }

        private void FillEmployeeData()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            cbEmployees.DataBindings.Clear();

           
                try
                {

                    dt = objHRdb.GetEmployeesForMisconduct(stCompCode,stBranCode,stDocMonth,txtEnameSearch.Text.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        //dr[1] = "--Select--";
                        //dr[3] = "--Select--";

                        //dt.Rows.InsertAt(dr, 0);

                        cbEmployees.DataSource = dt;
                        cbEmployees.DisplayMember = "ENAME";
                        cbEmployees.ValueMember = "EmpDetl";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRdb = null;
                    dt = null;
                }            
        }



        //private void GetEmpName()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    if (txtEcodeSearch.Text != "")
        //    {
        //        try
        //        {
        //            string strCmd = "SELECT MEMBER_NAME EmpName,DEPT_ID DeptId,DESG_ID DesigCode " +
        //                            " FROM EORA_MASTER " +
        //                            " WHERE ECODE= " + Convert.ToInt32(txtEcodeSearch.Text) + "";
        //            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

        //            if (dt.Rows.Count > 0)
        //            {
        //                txtEname.Text = dt.Rows[0]["EmpName"].ToString();                        
        //            }
        //            else
        //            {
        //                txtEname.Text = "";
        //                //MessageBox.Show("Emp Data Not Found On This Ecode","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
        //                //return;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //            objSQLdb = null;
        //            dt = null;
        //        }
        //    }
        //    else
        //    {
        //        txtEname.Text = "";
        //    }

        //}

        private bool CheckData()
        {
            bool bFlag = true;

           
            if (cbRecType.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Receipt Type", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbRecType.Focus();
                return bFlag;
            }

            if (txtReceiptNo.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Receipt No", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtReceiptNo.Focus();
                return bFlag;
            }
            if (txtReceiptNo.Text.Equals("0"))
            {
                bFlag = false;
                MessageBox.Show("Please Enter Valid Receipt No", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtReceiptNo.Focus();
                return bFlag;
            }
            if (cbEmployees.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Employee", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbEmployees.Focus();
                return bFlag;
            }
            //if (txtEname.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter Valid Ecode", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    txtEcodeSearch.Focus();
            //    return bFlag;
            //}
            if (txtAmount.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Amount", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAmount.Focus();
                return bFlag;
            }

            return bFlag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {               

                if (strFormType.Equals("AmountRecovery"))
                {
                    if (drs != null)
                    {
                        ((MisconductForm)objMisconductForm).dtModeOfRecDetl.Rows.Remove(drs[0]);
                    }


                    ((MisconductForm)objMisconductForm).dtModeOfRecDetl.Rows.Add(new object[] { "-1",cbEmployees.Text.ToString().Split('-')[0],
                        cbEmployees.Text.ToString().Split('-')[1],cbRecType.Text,Convert.ToDateTime(dtpReceiptDate.Value).ToString("dd/MMM/yyyy"),
                        txtReceiptNo.Text,txtAmount.Text,txtRemarks.Text.Replace("\'", "") });
                    ((MisconductForm)objMisconductForm).GetModeOfRecoveryDetails();


                }
                else if (strFormType.Equals("ActualAmountRecovery"))
                {
                    if (drs != null)
                    {
                        ((MisconductForm)objMisconductForm).dtActualRecDetl.Rows.Remove(drs[0]);
                    }


                    ((MisconductForm)objMisconductForm).dtActualRecDetl.Rows.Add(new object[] { "-1",cbEmployees.Text.ToString().Split('-')[0],
                        cbEmployees.Text.ToString().Split('-')[1],cbRecType.Text,
                            Convert.ToDateTime(dtpReceiptDate.Value).ToString("dd/MMM/yyyy"),txtReceiptNo.Text,txtAmount.Text,txtRemarks.Text.Replace("\'", "") });
                    ((MisconductForm)objMisconductForm).GetActualRecoveryDetails();
                }

                this.Close();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            txtReceiptNo.Text = "";
            txtRemarks.Text = "";
            cbEmployees.SelectedIndex = 0;
            cbRecType.SelectedIndex = 0;
            dtpReceiptDate.Value = DateTime.Today;

        }
                   

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtReceiptNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEnameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEnameSearch.Text != "")
            {
                FillEmployeeData();
            }
            else
            {
                FillEmployeeData();
            }
        }

     
    }
}
