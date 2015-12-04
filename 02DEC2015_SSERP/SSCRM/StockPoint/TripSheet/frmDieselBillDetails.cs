using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSCRM
{
    public partial class frmDieselBillDetails : Form
    {
        public TripSheet objTripSheet;
        private bool flagUpdate = false;
        string strBillNo = "", strPerLtrCost = "", strTotLtrs = "", strTotAmt = "";
        DateTime dtpDieselBillDate;
        DataGridViewRow RowIndex;       


        public frmDieselBillDetails()
        {
            InitializeComponent();
            
        }

        public frmDieselBillDetails(DataGridViewRow rIndex,DateTime dtpDate,string sBillNo,string sLtrCost,string sTotLtrs,string sTotAmt)
        {
            InitializeComponent();

            flagUpdate = true;
            RowIndex = rIndex;
            dtpDieselBillDate = dtpDate;
            strBillNo = sBillNo;
            strPerLtrCost = sLtrCost;
            strTotLtrs = sTotLtrs;
            strTotAmt = sTotAmt;
        }

        private void frmDieselBillDetails_Load(object sender, EventArgs e)
        {
            dtpBillDate.Value = DateTime.Now;

            if (flagUpdate == true)
            {
                dtpBillDate.Value = dtpDieselBillDate;
                txtDieselBillNo.Text = strBillNo;
                txtPerLtrCost.Text = strPerLtrCost;
                txtTotLtrs.Text = strTotLtrs;
                txtTotBillAmt.Text = strTotAmt;
            }
        }

        private void CalculateTotBillAmount()
        {
            double nLtrCost, nTotLtrs,TotAmount;
            nLtrCost = nTotLtrs = TotAmount = 0;

            if (txtPerLtrCost.Text.Length > 0)
                nLtrCost = Convert.ToDouble(txtPerLtrCost.Text);
            if (txtTotLtrs.Text.Length > 0)
                nTotLtrs = Convert.ToDouble(txtTotLtrs.Text);

            TotAmount = Convert.ToDouble(nLtrCost * nTotLtrs);
            txtTotBillAmt.Text = (TotAmount).ToString("0.00");

        }

        private void txtPerLtrCost_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotBillAmount();
        }

        private void txtTotLtrs_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotBillAmount();
        }

        private void isDigitsCheck(object sender,KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != '.')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
        }

        private void txtPerLtrCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
            
        }

        private void txtTotLtrs_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtTotBillAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private bool CheckData()
        {
            bool flag = true;


            if (dtpBillDate.Value>DateTime.Now)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpBillDate.Focus();
                return flag;
            }
            if (txtDieselBillNo.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Bill No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDieselBillNo.Focus();
                return flag;
            }
            if (txtTotLtrs.Text.Length == 0 )
            {
                flag = false;
                MessageBox.Show("Please Enter Total Litres", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTotLtrs.Focus();
                return flag;
            }
            if (txtPerLtrCost.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Diesel Per Ltr Cost", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPerLtrCost.Focus();
                return flag;
            }
            if (txtDieselBillNo.Text.Length > 2)
            {
                if (txtTotLtrs.Text.Equals("0"))
                {
                    flag = false;
                    MessageBox.Show("Please Enter Correct Ltr Cost and Total Ltrs", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTotLtrs.Focus();
                    return flag;

                }
            }
           

            return flag;

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvBillDetails = null;

            if (CheckData() == true)
            {

                if (flagUpdate == true)
                {
                    ((TripSheet)objTripSheet).gvDieselDetails.Rows.Remove(RowIndex);
                }

                if (CheckDuplicateBills() == true)
                {

                    dgvBillDetails = ((TripSheet)objTripSheet).gvDieselDetails;
                    AddBillDetailsToGrid(dgvBillDetails);

                    ((TripSheet)objTripSheet).CalculateDieselTotalCost();
                    flagUpdate = false;
                    this.Close();
                }
            }

        }

        private bool CheckDuplicateBills()
        {
            bool flagCheck = true;
            string strDate;       
            
            strDate = Convert.ToDateTime(dtpBillDate.Value).ToString("dd/MMM/yyyy");

            if (((TripSheet)objTripSheet).gvDieselDetails.Rows.Count > 0)
            {
                for (int i = 0; i < ((TripSheet)objTripSheet).gvDieselDetails.Rows.Count; i++)
                {
                    if (strDate.Equals(((TripSheet)objTripSheet).gvDieselDetails.Rows[i].Cells["BillDate"].Value.ToString()))
                    {
                        if (txtDieselBillNo.Text.Equals(((TripSheet)objTripSheet).gvDieselDetails.Rows[i].Cells["BillNo"].Value.ToString()))
                        {
                            flagCheck = false;
                            MessageBox.Show("Entered Bill No Already Exists", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return flagCheck;
                        }
                    }
                }
            }



            return flagCheck;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpBillDate.Value = DateTime.Now;
            txtDieselBillNo.Text = "";
            txtPerLtrCost.Text = "";
            txtTotLtrs.Text = "";
            txtTotBillAmt.Text = "";
        }

        #region "GRIDVIEW DETAILS"

        public void AddBillDetailsToGrid(DataGridView dgvBillDetails)
        {
            int intRow = 0;
            intRow = dgvBillDetails.Rows.Count + 1;
           
            try
            {
               
                DataGridViewRow tempRow = new DataGridViewRow();

                DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                cellSlNo.Value = intRow;
                tempRow.Cells.Add(cellSlNo);

                DataGridViewCell cellDate = new DataGridViewTextBoxCell();
                cellDate.Value = Convert.ToDateTime(dtpBillDate.Value).ToString("dd/MMM/yyyy");
                tempRow.Cells.Add(cellDate);

                DataGridViewCell cellBillNo = new DataGridViewTextBoxCell();
                cellBillNo.Value = txtDieselBillNo.Text.ToString();
                tempRow.Cells.Add(cellBillNo);

                DataGridViewCell cellLtrCost = new DataGridViewTextBoxCell();
                cellLtrCost.Value = txtPerLtrCost.Text;
                tempRow.Cells.Add(cellLtrCost);

                DataGridViewCell cellTotLtrs = new DataGridViewTextBoxCell();
                cellTotLtrs.Value = txtTotLtrs.Text;
                tempRow.Cells.Add(cellTotLtrs);

                DataGridViewCell cellTotAmount = new DataGridViewTextBoxCell();
                cellTotAmount.Value = txtTotBillAmt.Text;
                tempRow.Cells.Add(cellTotAmount);
                               
                intRow = intRow + 1;
                dgvBillDetails.Rows.Add(tempRow);
               
                    //for (int i = 0; i < objTripSheet.gvDieselDetails.Rows.Count; i++)
                    //{
                    //    objTripSheet.gvDieselDetails.Rows[i].Cells["SlNo_Diesel"].Value = (i + 1).ToString();
                    //}
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDieselBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b')
            {
                if (!char.IsLetterOrDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        
    }
}
