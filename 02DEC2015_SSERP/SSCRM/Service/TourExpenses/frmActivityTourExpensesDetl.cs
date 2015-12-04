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
    public partial class frmActivityTourExpensesDetl : Form
    {
        ServiceDeptDB objServicedb = null;
        public frmActivityExpensesDetails objfrmActivityExpensesDetails;


        private  DateTime dtActivityDate;
        private string sCompCode = "";
        private string sBranchCode = "";
        private string sDocMonth = "";
        private string strEcode = "";
        DataRow[] drs;

        public frmActivityTourExpensesDetl(DataRow[] dr, string CompCode, string BranCode, string DocMonth, string SEcode)
        {
            InitializeComponent();
            drs = dr;

            sCompCode = CompCode;
            sBranchCode = BranCode;
            sDocMonth = DocMonth;
            strEcode = SEcode;
        }
        public frmActivityTourExpensesDetl(DateTime ActivityDate, string CompCode, string BranCode, string DocMonth, string SEcode)
        {
            InitializeComponent();

            dtActivityDate = ActivityDate;
            sCompCode = CompCode;
            sBranchCode = BranCode;
            sDocMonth = DocMonth;
            strEcode = SEcode;
        }

        private void frmActivityTourExpensesDetl_Load(object sender, EventArgs e)
        {
            dtpActivityDate.Enabled = false;

            if (drs == null)
            {
                dtpActivityDate.Value = dtActivityDate;
                GetServiceEmpActivities();
            }


            if (drs != null)
            {
                dtActivityDate = Convert.ToDateTime(drs[0]["ActivityDate"].ToString());  
                dtpActivityDate.Value = Convert.ToDateTime(drs[0]["ActivityDate"].ToString());              
                GetServiceEmpActivities();

                cbActivityTypes.Text = drs[0]["Activity"].ToString();
                txtFrmLocation.Text = drs[0]["fromLocation"].ToString();
                txtToLocation.Text = drs[0]["ToLocation"].ToString();
                txtFairAmt.Text = drs[0]["FairAmt"].ToString();
                txtDaAmt.Text = drs[0]["DAAmt"].ToString();
                txtLodgeAmt.Text = drs[0]["LodgeAmt"].ToString();
                txtLocalConv.Text = drs[0]["LocalConvAmt"].ToString();
                txtNoOfKM.Text = drs[0]["NoOfKm"].ToString();
                txtPhoneBillAmt.Text = drs[0]["PhoneBillAmt"].ToString();
                txtTotAmount.Text = drs[0]["TotalAmt"].ToString();
                txtModeOfJourney.Text = drs[0]["ModeOfJourney"].ToString();

            }

        }


        private bool CheckData()
        {
            bool flag = true;

            if (cbActivityTypes.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Activity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbActivityTypes.Focus();
            }

            else if (txtFrmLocation.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter From Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFrmLocation.Focus();
            }
            else if (txtToLocation.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter To Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtToLocation.Focus();
            }

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            bool bFlag=false;
            if (CheckData() == true)
            {

                if (drs != null)
                {
                    //((frmActivityExpensesDetails)objfrmActivityExpensesDetails).dtExpenses.Rows.Remove(drs[0]);
                }
              

                if (txtFairAmt.Text == "")
                {
                    txtFairAmt.Text = "0";
                }

                if (txtDaAmt.Text == "")
                {
                    txtDaAmt.Text = "0";
                }

                if (txtLocalConv.Text == "")
                {
                    txtLocalConv.Text = "0";
                }
                if (txtLodgeAmt.Text == "")
                {
                    txtLodgeAmt.Text = "0";
                }

                if (txtNoOfKM.Text.Length == 0)
                {
                    txtNoOfKM.Text = "0";
                }

                if (txtPhoneBillAmt.Text == "")
                {
                    txtPhoneBillAmt.Text = "0";
                }

                CalculateTotalAmount();

                //if (((frmActivityExpensesDetails)objfrmActivityExpensesDetails).dtExpenses.Rows.Count > 0)
                //{
                //    for (int i = 0; i < ((frmActivityExpensesDetails)objfrmActivityExpensesDetails).dtExpenses.Rows.Count; i++)
                //    {
                //        if (cbActivityTypes.Text.Equals(((frmActivityExpensesDetails)objfrmActivityExpensesDetails).dtExpenses.Rows[i]["Activity"].ToString()))
                //        {
                //            if (dtActivityDate.ToShortDateString().Equals(((frmActivityExpensesDetails)objfrmActivityExpensesDetails).dtExpenses.Rows[i]["ActivityDate"].ToString()))
                //            {
                //                bFlag = true;
                //                break;
                //            }

                //        }

                //    }

                //}
                //if (bFlag == false)
                //{

                //    ((frmActivityExpensesDetails)objfrmActivityExpensesDetails).dtExpenses.Rows.Add(new Object[] { "-1",cbActivityTypes.SelectedValue.ToString().Split('@')[1],
                //    cbActivityTypes.Text.ToString(),Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MM/yyyy"),
                //    txtFrmLocation.Text.ToString(),txtToLocation.Text.ToString(),
                //    txtNoOfKM.Text.ToString(),txtModeOfJourney.Text.ToString(),txtFairAmt.Text,txtLodgeAmt.Text,
                //    txtDaAmt.Text,txtLocalConv.Text,txtPhoneBillAmt.Text,txtTotAmount.Text});


                //    //((frmActivityExpensesDetails)objfrmActivityExpensesDetails).GetEmpTourExpensesDetails();
                //}


                this.Close();
            }
        }


        private void CalculateTotalAmount()
        {
            double FairAmt, LodgeAmt, PhoneBillAmt, DaAmt, LocalConv, TotAmount;
            FairAmt = LodgeAmt = PhoneBillAmt = DaAmt = LocalConv = TotAmount = 0;

            if (txtFairAmt.Text.Length > 0)
            {
                FairAmt = Convert.ToDouble(txtFairAmt.Text);
            }
            if (txtLodgeAmt.Text.Length > 0)
            {
                LodgeAmt = Convert.ToDouble(txtLodgeAmt.Text);
            }

            if (txtPhoneBillAmt.Text.Length > 0)
            {
                PhoneBillAmt = Convert.ToDouble(txtPhoneBillAmt.Text);
            }

            if (txtDaAmt.Text.Length > 0)
            {
                DaAmt = Convert.ToDouble(txtDaAmt.Text);
            }

            if (txtLocalConv.Text.Length > 0)
            {
                LocalConv = Convert.ToDouble(txtLocalConv.Text);
            }

            TotAmount = Convert.ToDouble(FairAmt + LodgeAmt + PhoneBillAmt + DaAmt + LocalConv);

            txtTotAmount.Text = TotAmount.ToString("0");
        }

        private void txtLodgeAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }
              

        private void txtDaAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFairAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLocalConv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPhoneBillAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPhoneBillAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtLocalConv_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtFairAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtDaAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }
        private void txtLodgeAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtNoOfKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }



        public void GetServiceEmpActivities()
        {
            objServicedb = new ServiceDeptDB();
            DataTable dt = new DataTable();
            try
            {
                dt = objServicedb.GetServiceEmpActivities(sCompCode, sBranchCode, sDocMonth, strEcode,Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MMM/yyyy")).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["ActivityName"] = "--Select--";
                    row["TranNo"] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbActivityTypes.DataSource = dt;
                    cbActivityTypes.DisplayMember = "ActivityName";
                    cbActivityTypes.ValueMember = "TranNo";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServicedb = null;
                dt = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtFrmLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b' && (e.KeyChar != ' '))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b' && (e.KeyChar != ' '))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbActivityTypes.SelectedIndex = 0;
            txtFairAmt.Text = "";
            txtDaAmt.Text = "";
            txtFrmLocation.Text = "";
            txtToLocation.Text = "";
            txtTotAmount.Text = "";
            txtLocalConv.Text = "";
            txtLodgeAmt.Text = "";
            txtModeOfJourney.Text = "";
            txtNoOfKM.Text = "";
            txtPhoneBillAmt.Text = "";
            
        }

        private void txtModeOfJourney_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

       
    
    }
}
