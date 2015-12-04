using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SDMS
{
    public partial class AgingIntervals : Form
    {
        SQLDB objSQLdb = null;
        bool flagUpdate = false;
        bool flag = true;
        int ToValue = 0;
        

        public AgingIntervals()
        {
            InitializeComponent();
        }

        private void AgingIntervals_Load(object sender, EventArgs e)
        {
            gvAgingIntervalDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                       System.Drawing.FontStyle.Regular);

            FillCompanyData();
            GenerateTrnNo();

            dtpValidFrom.Value = DateTime.Today;
            dtpValidTo.Value = DateTime.Today;

            gvAgingIntervalDetails.Rows.Add();
            gvAgingIntervalDetails.Rows[0].Cells["fromDay"].Value = "0";
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["CM_COMPANY_NAME"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

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

        private void GenerateTrnNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT ISNULL(MAX(FAIH_TRN_ID),0)+1 TrnNo FROM FA_AGEWISE_INPUT_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTranNo.Text = dt.Rows[0]["TrnNo"] + "";
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

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Aging Intervals", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
                flag = false;
            }
            else if (!(dtpValidTo.Value > dtpValidFrom.Value))
            {
                MessageBox.Show("Enter Valid Dates", "Aging Intervals ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
            }

           
            else if (gvAgingIntervalDetails.Rows.Count > 0)
            {
                for (int iVar1 = 0; iVar1 < gvAgingIntervalDetails.Rows.Count; iVar1++)
                {

                    if (Convert.ToString(gvAgingIntervalDetails.Rows[iVar1].Cells["toDay"].Value) == "")
                    {
                        flag = false;
                        MessageBox.Show("Please Enter To Days Value", "Aging Intervals", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }

                    else if (Convert.ToString(gvAgingIntervalDetails.Rows[iVar1].Cells["IntRate"].Value) == "")
                    {
                        flag = false;
                        MessageBox.Show("Please Enter InterestRate", "Aging Intervals", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }
                }
            }

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            if (CheckData() == true)
            {
                try
                {


                    if (flagUpdate == true)
                    {
                        strCommand = "UPDATE FA_AGEWISE_INPUT_HEAD " +
                                     " SET FAIH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "',FAIH_VALID_FROM='" + Convert.ToDateTime(dtpValidFrom.Value).ToString("dd/MMM/yyyy") +
                                        "',FAIH_VALID_TO='" + Convert.ToDateTime(dtpValidTo.Value).ToString("dd/MMM/yyyy") +
                                        "',FAIH_STATUS='A',FAIH_MODIFIED_BY='" + CommonData.LogUserId +
                                        "',FAIH_MODIFIED_DATE='" + CommonData.CurrentDate +
                                        "' WHERE FAIH_TRN_ID=" + Convert.ToInt32(txtTranNo.Text) + " ";
                        flagUpdate = false;
                    }
                    else
                    {
                        
                        strCommand = "INSERT INTO FA_AGEWISE_INPUT_HEAD(FAIH_TRN_ID " +
                                                                        ",FAIH_COMPANY_CODE " +
                                                                        ",FAIH_VALID_FROM " +
                                                                        ",FAIH_VALID_TO " +
                                                                        ",FAIH_STATUS " +
                                                                        ",FAIH_CREATED_BY " +
                                                                        ",FAIH_CREATED_DATE " +
                                                                        ")VALUES(" + Convert.ToInt32(txtTranNo.Text) +
                                                                        ",'" + cbCompany.SelectedValue.ToString() +
                                                                        "','" + Convert.ToDateTime(dtpValidFrom.Value).ToString("dd/MMM/yyyy") +
                                                                        "','" + Convert.ToDateTime(dtpValidTo.Value).ToString("dd/MMM/yyyy") +
                                                                        "','A','" + CommonData.LogUserId +
                                                                        "','" + CommonData.CurrentDate + "')";

                    }

                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    if (SaveAgingDetailDetails() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    GenerateTrnNo();
                    btnCancel_Click(null, null);
                }
                else
                {
                    strCommand = "DELETE FROM FA_AGEWISE_INPUT_HEAD WHERE FAIH_TRN_ID= " + Convert.ToInt32(txtTranNo.Text) + "";
                    iRes = objSQLdb.ExecuteSaveData(strCommand);

                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int SaveAgingDetailDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            try
            {
                if (gvAgingIntervalDetails.Rows.Count > 0)
                {
                    strCmd = "DELETE FROM FA_AGEWISE_INPUT_DETL WHERE FAID_TRN_ID=" + Convert.ToInt32(txtTranNo.Text) + "";
                    iRes = objSQLdb.ExecuteSaveData(strCmd);

                    strCmd = "";
                    for (int i = 0; i < gvAgingIntervalDetails.Rows.Count; i++)
                    {
                        if (gvAgingIntervalDetails.Rows[i].Cells["fromDay"].Value.ToString() != "" && gvAgingIntervalDetails.Rows[i].Cells["toDay"].Value.ToString() != "" && gvAgingIntervalDetails.Rows[i].Cells["IntRate"].Value.ToString() != "")
                        {
                            strCmd += "INSERT INTO FA_AGEWISE_INPUT_DETL(FAID_TRN_ID " +
                                                                        ",FAID_COMPANY_CODE " +
                                                                        ",FAID_SL_NO " +
                                                                        ",FAID_FROM_DAYS " +
                                                                        ",FAID_TO_DAYS " +
                                                                        ",FAID_INT_RATE " +
                                                                        ",FAID_VALID_FROM " +
                                                                        ",FAID_VALID_TO " +
                                                                        ")VALUES(" + Convert.ToInt32(txtTranNo.Text) +
                                                                        ",'" + cbCompany.SelectedValue.ToString() +
                                                                        "'," + gvAgingIntervalDetails.Rows[i].Cells["SLNO"].Value.ToString() +
                                                                        "," + gvAgingIntervalDetails.Rows[i].Cells["fromDay"].Value.ToString() +
                                                                        "," + gvAgingIntervalDetails.Rows[i].Cells["toDay"].Value.ToString() +
                                                                        "," + gvAgingIntervalDetails.Rows[i].Cells["IntRate"].Value.ToString() +
                                                                        ",'" + Convert.ToDateTime(dtpValidFrom.Value).ToString("dd/MMM/yyyy") +
                                                                        "','" + Convert.ToDateTime(dtpValidTo.Value).ToString("dd/MMM/yyyy") + "')";
                        }
                    }
                }
                if (strCmd.Length > 10)
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

      
       

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Dispose();
            this.Close();
        }

        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void gvAgingIntervalDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            dtpValidTo.Value = DateTime.Today;
            dtpValidFrom.Value = DateTime.Today;
            GenerateTrnNo();           
            gvAgingIntervalDetails.Rows.Clear();
            gvAgingIntervalDetails.Rows.Add();
            //gvAgingIntervalDetails.AllowUserToAddRows = false;
            gvAgingIntervalDetails.Rows[0].Cells["fromDay"].Value = "0";
        }

        private void gvAgingIntervalDetails_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                this.gvAgingIntervalDetails.Rows[e.RowIndex + i].Cells["SLNO"].Value = (e.RowIndex + (i + 1)).ToString();
            }
            
        }

        private void txtTranNo_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvAgingIntervalDetails.Rows.Clear();

                             
            if (txtTranNo.Text != "")
            {

                try
                {
                    string strCommand = "SELECT FAID_FROM_DAYS " +
                                             ", FAID_TO_DAYS " +
                                             ", FAID_INT_RATE " +
                                             ", FAIH_COMPANY_CODE " +
                                             ", FAIH_VALID_FROM " +
                                             ", FAIH_VALID_TO " +
                                             ", FAIH_STATUS " +
                                             " FROM FA_AGEWISE_INPUT_DETL " +
                                             " INNER JOIN FA_AGEWISE_INPUT_HEAD ON FAIH_TRN_ID=FAID_TRN_ID " +
                                             " WHERE FAID_TRN_ID=" + txtTranNo.Text + " ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        cbCompany.SelectedValue = dt.Rows[0]["FAIH_COMPANY_CODE"].ToString();
                        dtpValidFrom.Value = Convert.ToDateTime(dt.Rows[0]["FAIH_VALID_FROM"].ToString());
                        dtpValidTo.Value = Convert.ToDateTime(dt.Rows[0]["FAIH_VALID_TO"].ToString());
                        
                        for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                        {
                            gvAgingIntervalDetails.Rows.Add();

                            gvAgingIntervalDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                            gvAgingIntervalDetails.Rows[iVar].Cells["fromDay"].Value = dt.Rows[iVar]["FAID_FROM_DAYS"].ToString();
                            gvAgingIntervalDetails.Rows[iVar].Cells["toDay"].Value = dt.Rows[iVar]["FAID_TO_DAYS"].ToString();
                            gvAgingIntervalDetails.Rows[iVar].Cells["IntRate"].Value = dt.Rows[iVar]["FAID_INT_RATE"].ToString();
                        }
                    }
                    else
                    {
                        GenerateTrnNo();
                        gvAgingIntervalDetails.Rows.Add();
                        //gvAgingIntervalDetails.AllowUserToAddRows = false;
                        gvAgingIntervalDetails.Rows[0].Cells["fromDay"].Value = "0";
                        
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
            else
            {
              
                cbCompany.SelectedIndex = 0;
                dtpValidFrom.Value = DateTime.Today;
                dtpValidTo.Value = DateTime.Today;
                gvAgingIntervalDetails.Rows.Clear();
            }

        }

        private void gvAgingIntervalDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            for (int i = 0; i < gvAgingIntervalDetails.Rows.Count; i++)
            {

                if ((Convert.ToString(gvAgingIntervalDetails.Rows[i].Cells["toDay"].Value) == "0") || (Convert.ToString(gvAgingIntervalDetails.Rows[i].Cells["IntRate"].Value) == ""))
                {
                    flag = false;

                }
            }
            if ((Convert.ToString(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value) != "")&&(Convert.ToString(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value) != "0"))
            {
                //if (Convert.ToString(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value) != "0")
                //{
                    if (Convert.ToString(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["IntRate"].Value) != "")
                    {
                         ToValue = Convert.ToInt32(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value.ToString());
                        if (flag == false)
                        {
                            gvAgingIntervalDetails.AllowUserToAddRows = false;
                        }


                        //if (ToValue == Convert.ToInt32(gvAgingIntervalDetails.Rows[e.RowIndex-1].Cells["toDay"].Value.ToString()))
                        //{
                           
                        //    MessageBox.Show("To Days Value already Entered");
                        //    gvAgingIntervalDetails.CurrentCell.Value = null;
                        //    gvAgingIntervalDetails.Focus();

                        //}
                        else
                        {
                            flag = true;
                            gvAgingIntervalDetails.Rows.Add();
                            gvAgingIntervalDetails.Rows[e.RowIndex + 1].Cells["fromDay"].Value = ToValue + 1;
                        }

                    //}
                }
                    if (e.RowIndex >= 0 && e.ColumnIndex == gvAgingIntervalDetails.Columns["toDay"].Index)
                    {
                        if (gvAgingIntervalDetails.Rows[e.RowIndex].Cells["fromDay"].Value.ToString() != ""
                            && gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value.ToString() != "")
                            
                        {
                            if (Convert.ToInt32(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["fromDay"].Value) >
                                Convert.ToInt32(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value))
                            {
                                gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value = gvAgingIntervalDetails.Rows[e.RowIndex].Cells["fromDay"].Value;
                            }                            
                        }
                    }
                    if (e.RowIndex >= 0 && e.ColumnIndex == gvAgingIntervalDetails.Columns["IntRate"].Index)
                    {
                        if (gvAgingIntervalDetails.Rows[e.RowIndex].Cells["fromDay"].Value.ToString() != ""
                            && gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value.ToString() != ""
                            && gvAgingIntervalDetails.Rows[e.RowIndex].Cells["IntRate"].Value.ToString() != "" && e.RowIndex == gvAgingIntervalDetails.Rows.Count-1)
                        {
                            gvAgingIntervalDetails.Rows.Add();
                            gvAgingIntervalDetails.Rows[e.RowIndex + 1].Cells["fromDay"].Value = (Convert.ToInt32(gvAgingIntervalDetails.Rows[e.RowIndex].Cells["toDay"].Value) + 1).ToString();
                        }
                    }

            }




        }

        private void gvAgingIntervalDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvAgingIntervalDetails.Rows.Count > 1)
            {
                if (e.ColumnIndex == gvAgingIntervalDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {

                        DataGridViewRow dgvr = gvAgingIntervalDetails.Rows[e.RowIndex];
                        gvAgingIntervalDetails.Rows.Remove(dgvr);

                        for (int i = 0; i < gvAgingIntervalDetails.Rows.Count; i++)
                        {

                            gvAgingIntervalDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvAgingIntervalDetails.Rows[0].Cells["fromDay"].Value = "0";
                        }

                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "Aging Intervals", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }

        }

        private void gvAgingIntervalDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }
       
            
    }
}
