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
    public partial class AddOdDetail : Form
    {
        string strNo, strDate, strBranch, strRemarks;
        string sVisitedBranchCode, sVisitedBranch,strtobeVisitedBranch;
        OnDutySlip obj;
        public AddOdDetail()
        {
            InitializeComponent();
        }
        public AddOdDetail(string sNo,string sDate,string sBranch,string stobeVisitedBranch,string sRemarks,OnDutySlip o)
        {
            
            
            InitializeComponent();
            FillVisitingBranch();
            if (stobeVisitedBranch.Length > 0)
                cmbVisitingBranch.SelectedValue = stobeVisitedBranch;

            txtNo.Text = sNo;
            txtDate.Text = sDate;

            strBranch = sBranch;

            txtReason.Text = sRemarks;
            obj = o;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbVisitingBranch.Text == "--NO BRANCH--")
            {
                sVisitedBranch="";
                sVisitedBranchCode = "";
            }
            else
            {
                sVisitedBranchCode = cmbVisitingBranch.SelectedValue.ToString();
                sVisitedBranch = cmbVisitingBranch.Text;
            }
            obj.gvODDetl.Rows[Convert.ToInt32(txtNo.Text)-1].Cells["VisitingBranchCode"].Value = sVisitedBranchCode;
            obj.gvODDetl.Rows[Convert.ToInt32(txtNo.Text)-1].Cells["VisitingBranch"].Value = sVisitedBranch;
            obj.gvODDetl.Rows[Convert.ToInt32(txtNo.Text)-1].Cells["Remarks"].Value = txtReason.Text.ToString().Replace("'","");

            this.Close();

        }
        private void FillVisitingBranch()
        {
            try
            {
                string strSQL = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE  ACTIVE='T' and BRANCH_CODE <> '" + strBranch + "'";
               SQLDB  objData = new SQLDB();
                DataTable dt = objData.ExecuteDataSet(strSQL).Tables[0];
                cmbVisitingBranch.DataSource = null;
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--NO BRANCH--";
                    row[1] = "--NO BRANCH--";
                    dt.Rows.InsertAt(row, 0);
                    cmbVisitingBranch.DataSource = dt;


                    cmbVisitingBranch.DisplayMember = "BRANCH_NAME";
                    cmbVisitingBranch.ValueMember = "BRANCH_CODE";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
