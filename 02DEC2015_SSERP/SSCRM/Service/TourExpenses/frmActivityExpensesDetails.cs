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
    public partial class frmActivityExpensesDetails : Form
    {
       
        public ServicesTourExpenses objServicesTourExpenses;

        DataGridViewRow gridRow;
       
        public frmActivityExpensesDetails()
        {
            InitializeComponent();
        
          
        }
        public frmActivityExpensesDetails(DataGridViewRow rowIndex)
        {
            InitializeComponent();
            gridRow = rowIndex;
        }


        private void frmActivityExpensesDetails_Load(object sender, EventArgs e)
        {

            dtpActivityDate.Enabled = false;
        }

        private bool CheckData()
        {
            bool flag = true;


            if (txtFrmLocation.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter From Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFrmLocation.Focus();
                return flag;
            }
            if (txtToLocation.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter To Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtToLocation.Focus();
                return flag;
            }
            

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvExpDetails = null;
            if (CheckData() == true)
            {
                ((ServicesTourExpenses)objServicesTourExpenses).gvEmpTourBillDetails.Rows.Remove(gridRow);

                dgvExpDetails = ((ServicesTourExpenses)objServicesTourExpenses).gvEmpTourBillDetails;
                AddExpensesDetailsToGrid(dgvExpDetails);

               

                this.Dispose();
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
    
        private void AddExpensesDetailsToGrid(DataGridView dgvExpDetl)
        {
            int intRow = 0;
            intRow = dgvExpDetl.Rows.Count + 1;

            if (txtHours.Text.Length == 0)
            {
                txtHours.Text = "0";
            }
            if (txtMinutes.Text.Length == 0)
            {
                txtMinutes.Text = "0";
            }
            if (txtNoOfKM.Text == "")
            {
                txtNoOfKM.Text = "0";
            }
            if (txtPhoneBillAmt.Text.Length == 0)            
                txtPhoneBillAmt.Text = "0";            
            if (txtDaAmt.Text.Length == 0)
                txtDaAmt.Text = "0";
            if (txtFairAmt.Text.Length == 0)
                txtFairAmt.Text = "0";
            if (txtLodgeAmt.Text.Length == 0)
                txtLodgeAmt.Text = "0";
            if (txtLocalConv.Text.Length == 0)
                txtLocalConv.Text = "0";
            if (txtTotAmount.Text.Length == 0)
                txtTotAmount.Text = "0";
            

            string strDeptTime = Convert.ToInt32(txtHours.Text).ToString("00") + '.' + Convert.ToInt32(txtMinutes.Text).ToString("00");

            DataGridViewRow tempRow = new DataGridViewRow();
            DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
            cellSlNo.Value = intRow;
            tempRow.Cells.Add(cellSlNo);

            DataGridViewCell cellActivityDate = new DataGridViewTextBoxCell();
            cellActivityDate.Value = Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MMM/yyyy");
            tempRow.Cells.Add(cellActivityDate);

            DataGridViewCell cellDeptTime = new DataGridViewTextBoxCell();
            cellDeptTime.Value = strDeptTime;
            tempRow.Cells.Add(cellDeptTime);

            DataGridViewCell cellfrmLocation = new DataGridViewTextBoxCell();
            cellfrmLocation.Value = txtFrmLocation.Text.ToString().Replace("'", " ");
            tempRow.Cells.Add(cellfrmLocation);

            DataGridViewCell cellToLocation = new DataGridViewTextBoxCell();
            cellToLocation.Value = txtToLocation.Text.ToString().Replace("'", " ");
            tempRow.Cells.Add(cellToLocation);

            DataGridViewCell cellNoOfKm = new DataGridViewTextBoxCell();
            cellNoOfKm.Value = Convert.ToDouble(txtNoOfKM.Text);
            tempRow.Cells.Add(cellNoOfKm);
                       
            DataGridViewCell cellModeOfJour = new DataGridViewTextBoxCell();
            cellModeOfJour.Value = txtModeOfJourney.Text.ToString().Replace("'", "");
            tempRow.Cells.Add(cellModeOfJour);

            DataGridViewCell cellFairAmt = new DataGridViewTextBoxCell();
            cellFairAmt.Value = Convert.ToDouble(txtFairAmt.Text);
            tempRow.Cells.Add(cellFairAmt);

            DataGridViewCell cellLodgeAmt = new DataGridViewTextBoxCell();
            cellLodgeAmt.Value = Convert.ToDouble(txtLodgeAmt.Text);
            tempRow.Cells.Add(cellLodgeAmt);

            DataGridViewCell cellDaAmt = new DataGridViewTextBoxCell();
            cellDaAmt.Value = Convert.ToDouble(txtDaAmt.Text);
            tempRow.Cells.Add(cellDaAmt);

            DataGridViewCell cellLocalConvAmt = new DataGridViewTextBoxCell();
            cellLocalConvAmt.Value = Convert.ToDouble(txtLocalConv.Text);
            tempRow.Cells.Add(cellLocalConvAmt);

            DataGridViewCell cellPhoneBillAmt = new DataGridViewTextBoxCell();
            cellPhoneBillAmt.Value = Convert.ToDouble(txtPhoneBillAmt.Text);
            tempRow.Cells.Add(cellPhoneBillAmt);

            DataGridViewCell cellTotalAmt = new DataGridViewTextBoxCell();
            cellTotalAmt.Value = Convert.ToDouble(txtTotAmount.Text);
            tempRow.Cells.Add(cellTotalAmt);

            DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
            cellRemarks.Value = txtRemarks.Text.ToString().Replace("'", "");
            tempRow.Cells.Add(cellRemarks);

            intRow = intRow + 1;
            dgvExpDetl.Rows.Add(tempRow);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
                
             

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            txtFrmLocation.Text = "";
            txtToLocation.Text = "";
            txtHours.Text = "";
            txtMinutes.Text = "";
            txtModeOfJourney.Text = "";
            txtNoOfKM.Text = "";
            txtRemarks.Text = "";
            txtTotAmount.Text = "";
            txtDaAmt.Text = "";
            txtFairAmt.Text = "";
            txtLocalConv.Text = "";
            txtLodgeAmt.Text = "";
            txtNoOfKM.Text = "";
            txtPhoneBillAmt.Text = "";          

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

        private void txtNoOfKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

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
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
            

        }

        private void txtLodgeAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
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
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
           
        }

        private void txtDaAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
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
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtFairAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
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
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtLocalConv_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
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
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtPhoneBillAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }
            
     
    }
}
