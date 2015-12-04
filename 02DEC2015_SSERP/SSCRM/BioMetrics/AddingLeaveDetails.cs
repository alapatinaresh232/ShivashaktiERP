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
    public partial class AddingLeaveDetails : Form
    {
        int iIndex = 0;
        LeaveEntryForm leavForm=null;
        string sEcode = "";
        string strDesc = "";
        SQLDB objDb = null;
        public AddingLeaveDetails()
        {
            InitializeComponent();
        }
        public AddingLeaveDetails(int idex,LeaveEntryForm lForm,string Ecode)
        {
            iIndex = idex;
            leavForm = lForm;
            sEcode = Ecode;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void AddingLeaveDetails_Load(object sender, EventArgs e)
        {
            cbType.SelectedIndex = 0;      
            //txtApplNo.Text = leavForm.gvLeaveDetl.Rows[iIndex].Cells["SLNO"].Value.ToString();
            txtLeavDate.Text = leavForm.dtFromDate.Value.ToString("dd/MMM/yyyy");
            txtToDate.Text = leavForm.dtpToDate.Value.ToString("dd/MMM/yyyy");
            //txtDays.Text = leavForm.gvLeaveDetl.Rows[iIndex].Cells["LeaveDays"].Value.ToString();

            txtDays.Text = leavForm.txtNoOfDays.Text;
            if (leavForm.gvLeaveDetl.Rows[iIndex].Cells["LeaveType"].Value != null)
            {
                cbType.SelectedItem = leavForm.gvLeaveDetl.Rows[iIndex].Cells["LeaveType"].Value.ToString();
            }
            if (leavForm.gvLeaveDetl.Rows[iIndex].Cells["Remarks"].Value != null)
            {
                txtRemarks.Text = leavForm.gvLeaveDetl.Rows[iIndex].Cells["Remarks"].Value.ToString();
            }
                 
        }

        private void FillCoffDays()
        {
            try
            {
                string strCmd = "SELECT CONVERT( nVARCHAR(15),HECT_COFF_FROM_DATE,103) date1 "+
                                ",CONVERT(nvarchar(15),HECT_COFF_FROM_DATE,103) date2 "+
                                ",HECT_COFF_NOON,hect_noof_coff_days "+
                                " FROM HR_EMPLOYEE_COFF_TRN WHERE HECT_EORA_CODE=" + sEcode + " AND HECT_COFF_VALID_FLAG='O'";
               
                objDb = new SQLDB();
                DataTable dt = objDb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    cbAgnstDate.DataSource = dt;
                    cbAgnstDate.DisplayMember = "date1";
                    cbAgnstDate.ValueMember = "date2";
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                   
                }
                else
                {
                    MessageBox.Show("Employee Not Eligible For COFF", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                for (int iVar = 0; iVar < leavForm.gvLeaveDetl.Rows.Count;iVar++ )
                {
                    leavForm.gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = cbType.SelectedItem.ToString();
                    leavForm.gvLeaveDetl.Rows[iVar].Cells["Desc"].Value = strDesc;
                    if (cbType.SelectedItem.ToString() == "COFF")
                    {
                        leavForm.gvLeaveDetl.Rows[iVar].Cells["CoffAgntDate"].Value = cbAgnstDate.SelectedValue.ToString();
                    }
                    else
                    {
                        leavForm.gvLeaveDetl.Rows[iVar].Cells["CoffAgntDate"].Value = "";
                    }
                    leavForm.gvLeaveDetl.Rows[iVar].Cells["Remarks"].Value = txtRemarks.Text;
                  
                }
                leavForm.txtReason.Text = txtRemarks.Text;
                this.Close();
            }
        }

        private bool CheckData()
        {
            bool flag=true;
            if (cbType.SelectedIndex <= 0)
            {
                flag = false;
                MessageBox.Show("Please Select Type", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbType.Focus();
                return flag;
            }
            if (cbAgnstDate.SelectedIndex <= 0 && cbType.SelectedItem.ToString()=="COFF")
            {
                flag = false;
                MessageBox.Show("Please Select Against Date", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbAgnstDate.Focus();
                return flag;
            }
            if(txtRemarks.Text=="")
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRemarks.Focus();
                return flag;
            }
            if (cbType.SelectedItem.ToString() == "COFF")
            {
                try
                {
                    string strCmd = "SELECT HECT_NOOF_COFF_DAYS FROM HR_EMPLOYEE_COFF_TRN WHERE HECT_EORA_CODE=" + leavForm.txtEcodeSearch.Text + " and HECT_COFF_FROM_DATE='" + Convert.ToDateTime(cbAgnstDate.SelectedValue.ToString()).ToString("dd/MMM/yyyy") + "'";
                    objDb = new SQLDB();
                    DataTable dtCoff = objDb.ExecuteDataSet(strCmd).Tables[0];
                    string strCmd1 = "SELECT SUM(HELTL_LEAVE_DAYS) FROM HR_EMPLOYEE_LEAVE_TRN_LNITEM WHERE HELTL_EORA_CODE=" + leavForm.txtEcodeSearch.Text + " AND HELTL_COFF_AGNST_DATE='" + Convert.ToDateTime(cbAgnstDate.SelectedValue.ToString()).ToString("dd/MMM/yyyy") + "'";
                    DataTable dtLeave = objDb.ExecuteDataSet(strCmd1).Tables[0];

                    if (dtCoff.Rows.Count > 0 && dtLeave.Rows[0][0].ToString().Length == 0)
                    {
                        return true;
                    }
                    if (dtCoff.Rows.Count > 0 && dtLeave.Rows[0][0].ToString().Length > 0)
                    {
                        if (Convert.ToDouble(dtCoff.Rows[0][0].ToString()) < Convert.ToDouble(dtLeave.Rows[0][0].ToString()) + Convert.ToDouble(txtDays.Text))
                        {
                            MessageBox.Show("Employee Not Eligible For COFF", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return flag;
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedItem.ToString() == "CL")
            {
                strDesc = "CASUAL LEAVE";
            }
            if (cbType.SelectedItem.ToString() == "SL")
            {
                strDesc = "SICK LEAVE";
            }
            if (cbType.SelectedItem.ToString() == "EL")
            {
                strDesc = "EARNED LEAVE";
            }
            if (cbType.SelectedItem.ToString() == "LOP")
            {
                strDesc = "LOSS OF PAY";
            }

            if (cbType.SelectedItem.ToString() == "COFF")
            {
                cbAgnstDate.Visible = true;
                lblCoffDate.Visible = true;
                FillCoffDays();
                strDesc = "COMPENSATORY OFF";
            }
            else
            {
                cbAgnstDate.Visible = false;
                lblCoffDate.Visible = false;
            }
        }

      
    }
}
