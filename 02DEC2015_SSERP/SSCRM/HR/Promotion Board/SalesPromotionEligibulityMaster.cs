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
    public partial class SalesPromotionEligibulityMaster : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRinfo = null;
        bool upDateFlage = false;

        public SalesPromotionEligibulityMaster()
        {
            InitializeComponent();
        }

        private void SalesPromotionEligibulityMaster_Load(object sender, EventArgs e)
        {
            gvSalePromotionDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
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

        private void FillPromotionType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT(HPEM_TYPE )FROM HR_PROM_ELIG_MASTER  ORDER BY HPEM_TYPE";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbPromotionType.DataSource = dt;
                    cbPromotionType.DisplayMember = "HPEM_TYPE";
                    cbPromotionType.ValueMember = "HPEM_TYPE";
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
        private void FillSalesPromotionDeatailsTogrid()
        {
            objHRinfo = new HRInfo();
            DataTable dt = new DataTable();


            gvSalePromotionDetails.Rows.Clear();

            if (cbCompany.SelectedIndex > 0)
            {
                try
                {
                    dt = objHRinfo.GetSalesPromotionDetails(cbCompany.SelectedValue.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvSalePromotionDetails.Rows.Add();

                            gvSalePromotionDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            //gvSalePromotionDetails.Rows[i].Cells["PromotionId"].Value = dt.Rows[i]["ID"].ToString();
                            gvSalePromotionDetails.Rows[i].Cells["DeptId"].Value = dt.Rows[i]["DeptId"].ToString();
                            gvSalePromotionDetails.Rows[i].Cells["PromotionType"].Value = dt.Rows[i]["PromotionType"].ToString();


                            gvSalePromotionDetails.Rows[i].Cells["ExcellentMonths"].Value = dt.Rows[i]["ExeMonths"].ToString();

                            gvSalePromotionDetails.Rows[i].Cells["ExcellentPoints"].Value = dt.Rows[i]["ExePoints"].ToString();
                            gvSalePromotionDetails.Rows[i].Cells["GoodMonths"].Value = dt.Rows[i]["GoodMonths"].ToString();
                            gvSalePromotionDetails.Rows[i].Cells["GoodPoints"].Value = dt.Rows[i]["GoodPoints"].ToString();
                            gvSalePromotionDetails.Rows[i].Cells["AverageMonths"].Value = dt.Rows[i]["AvgMonths"].ToString();
                            gvSalePromotionDetails.Rows[i].Cells["AveragePoints"].Value = dt.Rows[i]["AvgPoints"].ToString();

                        }

                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRinfo = null;
                    dt = null;
                }
            }

        }
        private void FillPointsCreteria()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                strCommand = "SELECT HPEM_EXC_MONTHS" +
                                   ",HPEM_EXC_PTS" +
                                   ",HPEM_GOOD_MONTHS" +
                                   ",HPEM_GOOD_PTS" +
                                   ",HPEM_AVG_MONTHS" +
                                   ",HPEM_AVG_PTS " +
                                   ",HPEM_EXTORD_MONTHS " +
                                   ",HPEM_EXTORD_PTS " +
                                   " FROM HR_PROM_ELIG_MASTER WHERE HPEM_TYPE='" + cbPromotionType.SelectedValue.ToString() +
                                   "' AND HPEM_COMP_CODE='" + cbCompany.SelectedValue.ToString() + "'";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    upDateFlage = true;
                    txtExeMonts.Text = dt.Rows[0]["HPEM_EXC_MONTHS"].ToString();
                    txtExePoints.Text = dt.Rows[0]["HPEM_EXC_PTS"].ToString();
                    txtGoodMonths.Text = dt.Rows[0]["HPEM_GOOD_MONTHS"].ToString();
                    txtGoodPoints.Text = dt.Rows[0]["HPEM_GOOD_PTS"].ToString();
                    txtAvgMonths.Text = dt.Rows[0]["HPEM_AVG_MONTHS"].ToString();
                    txtAvgPoints.Text = dt.Rows[0]["HPEM_AVG_PTS"].ToString();
                    txtExtOrdMnths.Text = dt.Rows[0]["HPEM_EXTORD_MONTHS"].ToString();
                    txtExtOrdPts.Text = dt.Rows[0]["HPEM_EXTORD_PTS"].ToString();

                }
                else
                {
                    txtExeMonts.Text = "";
                    txtExePoints.Text = "";
                    txtGoodMonths.Text = "";
                    txtGoodPoints.Text = "";
                    txtAvgMonths.Text = "";
                    txtAvgPoints.Text = "";
                    txtExtOrdMnths.Text = "";
                    txtExtOrdPts.Text = "";
                    upDateFlage = false;
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
            bool Chkflag = true;


            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                cbCompany.Focus();
                return Chkflag;
            }


            else if (cbPromotionType.SelectedIndex == 0)
            {
                MessageBox.Show("Select Promotion Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                cbPromotionType.Focus();
                return Chkflag;
            }

            return Chkflag;
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            int iVal = 0;
            objSQLdb = new SQLDB();

            string strCommand = "";
            if (CheckData())
            {
                try
                {                   
                    try { Convert.ToDouble(txtExeMonts.Text.ToString()); }
                    catch { txtExeMonts.Text = "0"; }
                    try { Convert.ToDouble(txtExePoints.Text.ToString()); }
                    catch { txtExePoints.Text = "0"; }
                    try { Convert.ToDouble(txtGoodMonths.Text.ToString()); }
                    catch { txtGoodMonths.Text = "0"; }
                    try { Convert.ToDouble(txtGoodPoints.Text.ToString()); }
                    catch { txtGoodPoints.Text = "0"; }
                    try { Convert.ToDouble(txtAvgMonths.Text.ToString()); }
                    catch { txtAvgMonths.Text = "0"; }
                    try { Convert.ToDouble(txtAvgPoints.Text.ToString()); }
                    catch { txtAvgPoints.Text = "0"; }
                    try { Convert.ToDouble(txtExtOrdMnths.Text.ToString()); }
                    catch { txtExtOrdMnths.Text = "0"; }
                    try { Convert.ToDouble(txtExtOrdPts.Text.ToString()); }
                    catch { txtExtOrdPts.Text = "0"; }

                    if (upDateFlage == true)
                    {
                        strCommand = "UPDATE HR_PROM_ELIG_MASTER SET HPEM_TYPE='" + cbPromotionType.SelectedValue.ToString() +
                                                                 "',HPEM_EXC_MONTHS=" + txtExeMonts.Text.ToString() +
                                                                    ",HPEM_EXC_PTS=" + txtExePoints.Text.ToString() +
                                                                    ",HPEM_GOOD_MONTHS=" + txtGoodMonths.Text.ToString() +
                                                                    ",HPEM_GOOD_PTS=" + txtGoodPoints.Text.ToString() +
                                                                    ",HPEM_AVG_MONTHS=" + txtAvgMonths.Text.ToString() +
                                                                    ",HPEM_AVG_PTS=" + txtAvgPoints.Text.ToString() +
                                                                    ",HPEM_EXTORD_MONTHS=" + txtExtOrdMnths.Text.ToString() +
                                                                    ",HPEM_EXTORD_PTS=" + txtExtOrdPts.Text.ToString() +
                                                                " WHERE HPEM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                                                "' AND HPEM_TYPE='" + cbPromotionType.SelectedValue.ToString() + "'";

                    }
                    else if (upDateFlage == false)
                    {

                        strCommand = "INSERT INTO HR_PROM_ELIG_MASTER(HPEM_DEPT_ID" +
                                                                    ",HPEM_TYPE " +
                                                                    ",HPEM_CRITERIA " +
                                                                    ",HPEM_EXC_MONTHS" +
                                                                    ",HPEM_EXC_PTS" +
                                                                    ",HPEM_GOOD_MONTHS" +
                                                                    ",HPEM_GOOD_PTS" +
                                                                    ",HPEM_AVG_MONTHS" +
                                                                    ",HPEM_AVG_PTS" +
                                                                    ",HPEM_EXTORD_MONTHS" +
                                                                    ",HPEM_EXTORD_PTS" +
                                                                    ",HPEM_COMP_CODE)VALUES(1200000,'" + cbPromotionType.SelectedValue.ToString() +
                                                                     "','MONTH_AVG'," + txtExeMonts.Text.ToString() +
                                                                    "," + txtExePoints.Text.ToString() +
                                                                    "," + txtGoodMonths.Text.ToString() +
                                                                    "," + txtGoodPoints.Text.ToString() +
                                                                    "," + txtAvgMonths.Text.ToString() +
                                                                    "," + txtAvgPoints.Text.ToString() +
                                                                    "," + txtExtOrdMnths.Text.ToString() +
                                                                    "," + txtExtOrdPts.Text.ToString() +
                                                                    ",'" + cbCompany.SelectedValue.ToString() + "')";
                    }
                    if (strCommand.Length > 5)
                    {
                        iVal = objSQLdb.ExecuteSaveData(strCommand);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
                if (iVal > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillSalesPromotionDeatailsTogrid();
                    //cbCompany.SelectedIndex = 0;
                    cbPromotionType.SelectedIndex = 0;
                    txtExeMonts.Text = "";
                    txtExePoints.Text = "";
                    txtGoodMonths.Text = "";
                    txtGoodPoints.Text = "";
                    txtAvgMonths.Text = "";
                    txtAvgPoints.Text = "";
                    //btnClear_Click(null, null);
                    upDateFlage = false;


                }
                else
                {
                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbCompany.SelectedIndex == 0)
            {
                cbPromotionType.DataSource = null;
                txtExeMonts.Text = "";
                txtExePoints.Text = "";
                txtGoodMonths.Text = "";
                txtGoodPoints.Text = "";
                txtAvgMonths.Text = "";
                txtAvgPoints.Text = "";
                gvSalePromotionDetails.Rows.Clear();
            }
            else if (cbCompany.SelectedIndex > 0)
            {
                FillPromotionType();
                FillSalesPromotionDeatailsTogrid();
            }

        }

        private void gvSalePromotionDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == gvSalePromotionDetails.Columns["Edit"].Index)
                {
                    upDateFlage = true;
                    cbPromotionType.SelectedValue = gvSalePromotionDetails.Rows[e.RowIndex].Cells["PromotionType"].Value.ToString();
                    txtExeMonts.Text = gvSalePromotionDetails.Rows[e.RowIndex].Cells["ExcellentMonths"].Value.ToString();
                    txtExePoints.Text = gvSalePromotionDetails.Rows[e.RowIndex].Cells["ExcellentPoints"].Value.ToString();
                    txtGoodMonths.Text = gvSalePromotionDetails.Rows[e.RowIndex].Cells["GoodMonths"].Value.ToString();
                    txtGoodPoints.Text = gvSalePromotionDetails.Rows[e.RowIndex].Cells["GoodPoints"].Value.ToString();
                    txtAvgMonths.Text = gvSalePromotionDetails.Rows[e.RowIndex].Cells["AverageMonths"].Value.ToString();
                    txtAvgPoints.Text = gvSalePromotionDetails.Rows[e.RowIndex].Cells["AveragePoints"].Value.ToString();
                }
            }
        }

        private void cbPromotionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPromotionType.SelectedIndex > 0)
            {
                FillPointsCreteria();


            }

            else
            {
                txtExeMonts.Text = "";
                txtExePoints.Text = "";
                txtGoodMonths.Text = "";
                txtGoodPoints.Text = "";
                txtAvgMonths.Text = "";
                txtAvgPoints.Text = "";
                //gvSalePromotionDetails.Rows.Clear();

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            cbCompany.SelectedIndex = 0;
            cbPromotionType.SelectedIndex = -1;
            txtExeMonts.Text = "";
            txtExePoints.Text = "";
            txtGoodMonths.Text = "";
            txtGoodPoints.Text = "";
            txtAvgMonths.Text = "";
            txtAvgPoints.Text = "";
            gvSalePromotionDetails.Rows.Clear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int ival = 0;

            if (cbCompany.SelectedIndex > 0)
            {
                if (cbPromotionType.SelectedIndex > 0)
                {
                    if (upDateFlage == true)
                    {
                        DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.Yes)
                        {

                            try
                            {
                                strCommand = "DELETE FROM HR_PROM_ELIG_MASTER WHERE HPEM_TYPE='" + cbPromotionType.SelectedValue.ToString() +
                                    "' AND HPEM_COMP_CODE='" + cbCompany.SelectedValue.ToString() + "'";
                                ival = objSQLdb.ExecuteSaveData(strCommand);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }


                            if (ival > 0)
                            {
                                MessageBox.Show("Data Deleted successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FillSalesPromotionDeatailsTogrid();
                                cbPromotionType.SelectedIndex = 0;
                                txtExeMonts.Text = "";
                                txtExePoints.Text = "";
                                txtGoodMonths.Text = "";
                                txtGoodPoints.Text = "";
                                txtAvgMonths.Text = "";
                                txtAvgPoints.Text = "";
                                upDateFlage = false;
                                //btnClear_Click(null, null);
                            }
                        }
                    }              
                 
                }
                else
                {
                    MessageBox.Show("Please Select Promotion Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


            else
            {
                MessageBox.Show("Please Select Company ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void txtExeMonts_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtGoodMonths_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAvgMonths_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtExePoints_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtGoodPoints_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAvgPoints_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
 }

