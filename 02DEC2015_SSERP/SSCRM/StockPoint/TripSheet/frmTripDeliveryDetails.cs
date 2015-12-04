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
    public partial class frmTripDeliveryDetails : Form
    {
        SQLDB objSQLdb = null;

        public TripSheet objTripSheet;
        private bool flagUpdate = false;
        string sTrnNo = "", sTripType = "";
        double dTotDays = 0;
        DataTable dtTripDetails = new DataTable();
        DataRow[] drs;

       
        public frmTripDeliveryDetails()
        {
            InitializeComponent();
            
        }

        public frmTripDeliveryDetails(DataTable dtTripDetl,DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
            dtTripDetails = dtTripDetl;
        }
      
      

        private void frmTripDeliveryDetails_Load(object sender, EventArgs e)
        {
            dtpClosingDate.Value = DateTime.Now;
           
            dtpStartingDate.Value = DateTime.Now;          


            gvProductDetails.Rows.Clear();

            if (drs != null)
            {
                dtpStartingDate.Value = Convert.ToDateTime(drs[0]["StartDate"].ToString());
                txtStartMetRead.Text = drs[0]["StartReading"].ToString();
                txtStartingPlace.Text = drs[0]["StartPlace"].ToString();

                dtpClosingDate.Value = Convert.ToDateTime(drs[0]["EndDate"].ToString());
            
                txtClosingMetRead.Text = drs[0]["EndReading"].ToString();
                txtClosingPlace.Text = drs[0]["EndPlace"].ToString();
                txtCleanerDA.Text = drs[0]["CleanerDA"].ToString();
                txtDriverDA.Text = drs[0]["DriverDA"].ToString();
                txtTotKm.Text = drs[0]["StartReading"].ToString();
                txtTotUnits.Text = drs[0]["NoOfUnitsDel"].ToString();
                txtTotDays.Text = drs[0]["TotDays"].ToString();
                txtCustCov.Text = drs[0]["NoOfCustCov"].ToString();

                txtSTHours.Text = drs[0]["StartTime"].ToString().Split('.')[0];
                txtSTMinutes.Text = drs[0]["StartTime"].ToString().Split('.')[1];
                txtCLHours.Text = drs[0]["EndTime"].ToString().Split('.')[0];
                txtCLMinutes.Text = drs[0]["EndTime"].ToString().Split('.')[1];

                txtEcodeSearch.Text = drs[0]["GLEcode"].ToString();
                txtName.Text = drs[0]["GLName"].ToString();

                if (dtTripDetails.Rows.Count > 0)
                {
                    int intRow = 0;

                    for (int i = 0; i < dtTripDetails.Rows.Count; i++)
                    {                       
                        if (dtTripDetails.Rows[i][2].Equals(txtStartMetRead.Text))
                        {
                            gvProductDetails.Rows.Add();                            
                            gvProductDetails.Rows[intRow].Cells["SLNO"].Value = intRow+1;
                            gvProductDetails.Rows[intRow].Cells["ProductId"].Value = dtTripDetails.Rows[i]["ProductId"].ToString();
                            gvProductDetails.Rows[intRow].Cells["ProductName"].Value = dtTripDetails.Rows[i]["ProdName"].ToString();
                            gvProductDetails.Rows[intRow].Cells["CategoryName"].Value = dtTripDetails.Rows[i]["CategoryName"].ToString();
                            gvProductDetails.Rows[intRow].Cells["ProdQty"].Value = dtTripDetails.Rows[i]["DespatchQty"].ToString();

                            intRow++;
                        }
                    }
                }
            }
            CalculateTotKM();
            CalculateTotUnits();
                       
        }



        private bool CheckData()
        {
            bool flag = true;

            double startReading, EndReading;
            startReading = EndReading = 0;

            if (dtpStartingDate.Value > DateTime.Now)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Starting Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpStartingDate.Focus();
                return flag;
            }
            if (dtpClosingDate.Value > DateTime.Now)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Closing Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpClosingDate.Focus();
                return flag;
            }

                       
            if (txtStartingPlace.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Starting Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartingPlace.Focus();
                return flag;
            }
            if (txtStartMetRead.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Starting Metre Reading", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartMetRead.Focus();
                return flag;
            }
            if (txtClosingPlace.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Closing Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtClosingPlace.Focus();
                return flag;
            }
            if (txtClosingMetRead.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Closing Metre Reading", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtClosingMetRead.Focus();
                return flag;
            }
            startReading = Convert.ToDouble(txtStartMetRead.Text);
            EndReading = Convert.ToDouble(txtClosingMetRead.Text);

            if (startReading > EndReading)
            {
                flag = false;
                MessageBox.Show("Start Metre Reading Must Less Than To Be Ending Metre Reading", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartMetRead.Focus();
                return flag;
            }

            //if (txtEcodeSearch.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Received Person Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtEcodeSearch.Focus();
            //    return flag;
            //}
            //if (gvProductDetails.Rows.Count == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Add Delivered Product Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    gvProductDetails.Focus();
            //    return flag;
            //}
            if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString()== "")
                    {
                        gvProductDetails.Rows[i].Cells["ProdQty"].Value = "0";
                    }
                }

            }

            return flag;

        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string StartTime, EndTime;
            StartTime = EndTime = "";
                     
            CalculateTotKM();
            CalculateTotUnits();

            if (CheckData() == true)
            {
                if (drs != null)
                {
                    ((TripSheet)objTripSheet).dtDeliveryDetl.Rows.Remove(drs[0]);

                  
                    for (int i = (objTripSheet).dtProductDetl.Rows.Count - 1; i >= 0; i--)
                    {
                        string stMetreRead = "";
                        stMetreRead = (objTripSheet).dtProductDetl.Rows[i][2].ToString();
                        if (stMetreRead.Equals(txtStartMetRead.Text.ToString()))
                        {
                            objTripSheet.dtProductDetl.Rows[i].Delete();
                        }

                    }
                }
               

                if (txtName.Text.Length == 0)
                    txtEcodeSearch.Text = "0";
                if (gvProductDetails.Rows.Count == 0)
                    txtTotUnits.Text = "0";
                if (txtCustCov.Text.Length == 0)
                    txtCustCov.Text = "0";
                if (txtDriverDA.Text.Length == 0)
                    txtDriverDA.Text = "0";
                if (txtCleanerDA.Text.Length == 0)
                    txtCleanerDA.Text = "0";

                if (txtSTMinutes.Text.Length == 0)
                    txtSTMinutes.Text = "00";
                if (txtCLMinutes.Text.Length == 0)
                    txtCLMinutes.Text = "00";

                StartTime = (txtSTHours.Text) + '.' + (txtSTMinutes.Text);
                EndTime = (txtCLHours.Text) + '.' + (txtCLMinutes.Text);

                if (CheckDuplicateReading() == true)
                {

                    ((TripSheet)objTripSheet).dtDeliveryDetl.Rows.Add(new object[] { "-1",txtEcodeSearch.Text.ToString(),dtpStartingDate.Value.ToString("dd/MMM/yyyy hh:MM:ss"),dtpClosingDate.Value.ToString("dd/MMM/yyyy hh:MM:ss"),
                         Convert.ToDateTime(dtpStartingDate.Value).ToString("dd/MMM/yyyy"),StartTime,txtStartingPlace.Text.ToString(),
                         txtStartMetRead.Text.ToString(),Convert.ToDateTime(dtpClosingDate.Value).ToString("dd/MMM/yyyy"),EndTime,
                         txtClosingPlace.Text.ToString(),txtClosingMetRead.Text.ToString(),txtTotKm.Text,txtTotUnits.Text,txtCustCov.Text,
                         txtTotDays.Text,txtDriverDA.Text,txtCleanerDA.Text,txtName.Text.ToString() });

                    ((TripSheet)objTripSheet).GetTripDeliveryDetails();


                    if (gvProductDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {

                            ((TripSheet)objTripSheet).dtProductDetl.Rows.Add(new object[] { "-1",gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString(),
                              txtStartMetRead.Text.ToString(),gvProductDetails.Rows[i].Cells["ProductName"].Value.ToString(),
                             gvProductDetails.Rows[i].Cells["CategoryName"].Value.ToString(),
                             gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString() });

                            ((TripSheet)objTripSheet).GetDeliverProductDetails();
                        }
                    }

                    ((TripSheet)objTripSheet).CalulateTotalTripDays();
                    ((TripSheet)objTripSheet).CalculateTotalKMs();

                    this.Close();
                }

            }

        }

        private bool CheckDuplicateReading()
        {
            bool flagCheck = true;
            string strReading;

            strReading = txtStartMetRead.Text.ToString();

            if (((TripSheet)objTripSheet).dtDeliveryDetl.Rows.Count > 0)
            {
                for (int i = 0; i < ((TripSheet)objTripSheet).dtDeliveryDetl.Rows.Count; i++)
                {
                    if (strReading.Equals(((TripSheet)objTripSheet).dtDeliveryDetl.Rows[i]["StartReading"].ToString()))
                    {
                        flagCheck = false;
                        MessageBox.Show("Metre Starting Reading Already Exists", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return flagCheck;
                    }
                }
            }



            return flagCheck;
        }
   


        //#region "GRIDVIEW DETAILS"

        //public void AddTripDeliveryDetailsToGrid(DataGridView dgvDeliverDetails)
        //{
        //    int intRow = 0;
        //    intRow = dgvDeliverDetails.Rows.Count + 1;
        //    string StartTime, EndTime;
        //    StartTime = EndTime = "";

        //    try
        //    {

        //        if (txtName.Text.Length == 0)
        //            txtEcodeSearch.Text = "0";
        //        if (gvProductDetails.Rows.Count == 0)
        //            txtTotUnits.Text = "0";
        //        if (txtCustCov.Text.Length == 0)
        //            txtCustCov.Text = "0";
        //        if (txtDriverDA.Text.Length == 0)
        //            txtDriverDA.Text = "0";
        //        if (txtCleanerDA.Text.Length == 0)
        //            txtCleanerDA.Text = "0";
        //        if (txtSTMinutes.Text.Length == 0)
        //            txtSTMinutes.Text = "00";
        //        if (txtCLMinutes.Text.Length == 0)
        //            txtCLMinutes.Text = "00";

        //        StartTime = (txtSTHours.Text) + '.' + (txtSTMinutes.Text);
        //        EndTime = (txtCLHours.Text) + '.' + (txtCLMinutes.Text);

        //        DataGridViewRow tempRow = new DataGridViewRow();

        //        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
        //        cellSlNo.Value = intRow;
        //        tempRow.Cells.Add(cellSlNo);

        //        DataGridViewCell cellGlEcode = new DataGridViewTextBoxCell();
        //        cellGlEcode.Value = txtEcodeSearch.Text.ToString();
        //        tempRow.Cells.Add(cellGlEcode);

        //        DataGridViewCell cellStartingDate = new DataGridViewTextBoxCell();
        //        cellStartingDate.Value = Convert.ToDateTime(dtpStartingDate.Value).ToString("dd/MMM/yyyy");
        //        tempRow.Cells.Add(cellStartingDate);

        //        DataGridViewCell cellStartTime = new DataGridViewTextBoxCell();
        //        cellStartTime.Value = StartTime;
        //        tempRow.Cells.Add(cellStartTime);

        //        DataGridViewCell cellStartPlace = new DataGridViewTextBoxCell();
        //        cellStartPlace.Value = txtStartingPlace.Text;
        //        tempRow.Cells.Add(cellStartPlace);

               
        //        DataGridViewCell cellStartReading = new DataGridViewTextBoxCell();
        //        cellStartReading.Value = txtStartMetRead.Text;
        //        tempRow.Cells.Add(cellStartReading);

        //        DataGridViewCell cellClosingDate = new DataGridViewTextBoxCell();
        //        cellClosingDate.Value = Convert.ToDateTime(dtpClosingDate.Value).ToString("dd/MMM/yyyy");
        //        tempRow.Cells.Add(cellClosingDate);

        //        DataGridViewCell cellEndTime = new DataGridViewTextBoxCell();
        //        cellEndTime.Value = EndTime;
        //        tempRow.Cells.Add(cellEndTime);

        //        DataGridViewCell cellClosingPlace = new DataGridViewTextBoxCell();
        //        cellClosingPlace.Value = txtClosingPlace.Text.ToString();
        //        tempRow.Cells.Add(cellClosingPlace);

        //        DataGridViewCell cellEndReading = new DataGridViewTextBoxCell();
        //        cellEndReading.Value = txtClosingMetRead.Text.ToString();
        //        tempRow.Cells.Add(cellEndReading);

        //        DataGridViewCell cellNoOfKm = new DataGridViewTextBoxCell();
        //        cellNoOfKm.Value = txtTotKm.Text;
        //        tempRow.Cells.Add(cellNoOfKm);

        //        DataGridViewCell cellTotUnits = new DataGridViewTextBoxCell();
        //        cellTotUnits.Value = txtTotUnits.Text;
        //        tempRow.Cells.Add(cellTotUnits);

        //        DataGridViewCell cellTotCust = new DataGridViewTextBoxCell();
        //        cellTotCust.Value = txtCustCov.Text;
        //        tempRow.Cells.Add(cellTotCust);

        //        DataGridViewCell cellTotDays = new DataGridViewTextBoxCell();
        //        cellTotDays.Value = txtTotDays.Text;
        //        tempRow.Cells.Add(cellTotDays);

        //        DataGridViewCell cellDriverDA = new DataGridViewTextBoxCell();
        //        cellDriverDA.Value = txtDriverDA.Text;
        //        tempRow.Cells.Add(cellDriverDA);

        //        DataGridViewCell cellCleanerDA = new DataGridViewTextBoxCell();
        //        cellCleanerDA.Value = txtCleanerDA.Text;
        //        tempRow.Cells.Add(cellCleanerDA);

        //        DataGridViewCell cellGLName = new DataGridViewTextBoxCell();
        //        cellGLName.Value = txtName.Text;
        //        tempRow.Cells.Add(cellGLName);

        //        intRow = intRow + 1;
        //        dgvDeliverDetails.Rows.Add(tempRow);            

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

     

        //#endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpStartingDate.Value = DateTime.Today;
            dtpClosingDate.Value = DateTime.Today;
            txtStartingPlace.Text = "";
            txtClosingPlace.Text = "";
            txtStartMetRead.Text = "";
            txtClosingMetRead.Text = "";
            txtCustCov.Text = "";
            txtEcodeSearch.Text = "";
            
            txtName.Text = "";
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll ProductsAdd = new ProductSearchAll("TripSheet");
            ProductsAdd.objTripDeliveryDetails = this;
            ProductsAdd.ShowDialog();

        }

        private void isDigitsCheck(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
                       
        }       


        private void txtCustCov_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);

        }
            

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    strCmd = "SELECT MEMBER_NAME EName,DESIG EmpDesig  FROM EORA_MASTER " +
                                       " WHERE ECODE= " + Convert.ToInt32(txtEcodeSearch.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["EName"].ToString();
                       
                    }
                    else
                    {
                        txtName.Text = "";
                       
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

        }

        private void CalculateTotKM()
        {
            double dStartReading, dEndReading,TotKM;
            dStartReading = dEndReading =TotKM= 0;

            if (txtStartMetRead.Text.Length > 0)
                dStartReading = Convert.ToDouble(txtStartMetRead.Text);
            if (txtClosingMetRead.Text.Length > 0)
                dEndReading = Convert.ToDouble(txtClosingMetRead.Text);

            if (dEndReading > dStartReading)
                TotKM = dEndReading - dStartReading;

            txtTotKm.Text = Convert.ToDouble(TotKM).ToString("0"); ;

        }


     

        private void txtStartMetRead_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtClosingMetRead_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

      

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                    gvProductDetails.Rows.Remove(dgvr);
                }
                if (gvProductDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                    }
                }
            }
            CalculateTotUnits();
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

    
        private void CalculateTotUnits()
        {
           double TotUnits = 0;

            if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString() != "")
                    {
                        TotUnits += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString());
                    }
                }
            }

            txtTotUnits.Text = TotUnits.ToString("f");
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value) != "")
            {                              

            }
            else
            {
                gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value = 0;
            }
            CalculateTotUnits();
        }

        private void txtClosingMetRead_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotKM();
        }

        private void txtStartMetRead_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotKM();
        }

        private void txtDriverDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtCleanerDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtSTHours_KeyUp(object sender, KeyEventArgs e)
        {

        }

       

        private void txtStartingPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtClosingPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void dtpStartingDate_ValueChanged(object sender, EventArgs e)
        {
            //if ((dtpStartingDate.Value > dtpClosingDate.Value))
            //{
            //    dtpStartingDate.Value = dtpClosingDate.Value;
            //}
            //else
            //{
            double TotDays = (dtpClosingDate.Value - dtpStartingDate.Value).TotalDays;
            dTotDays = TotDays;
            //}

            txtTotDays.Text = Convert.ToDouble(dTotDays).ToString("0");
        }

        private void dtpClosingDate_ValueChanged(object sender, EventArgs e)
        {
            //if ((dtpStartingDate.Value > dtpClosingDate.Value))
            //{
            //    dtpStartingDate.Value = dtpClosingDate.Value;
            //}
            //else
            //{
            double TotDays = (dtpClosingDate.Value - dtpStartingDate.Value).TotalDays;
            dTotDays = TotDays;
            //}

            txtTotDays.Text = Convert.ToDouble(dTotDays).ToString("0");
        }

        private void txtSTHours_Validated(object sender, EventArgs e)
        {
            if ((txtSTHours.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtSTHours.Text) >= 24)
                {
                    MessageBox.Show("please enter valid Hours");
                    txtSTHours.Focus();

                }
            }
        }

        private void txtSTMinutes_Validated(object sender, EventArgs e)
        {
            if ((txtSTMinutes.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtSTMinutes.Text) >= 60)
                {
                    MessageBox.Show("please enter valid Time in Minutes");
                    txtSTMinutes.Focus();
                }
            }

        }

        private void txtCLHours_Validated(object sender, EventArgs e)
        {
            if ((txtCLHours.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtCLHours.Text) >= 24)
                {
                    MessageBox.Show("please enter valid Hours");
                    txtCLHours.Focus();

                }
            }
        }

        private void txtCLMinutes_Validated(object sender, EventArgs e)
        {
            if ((txtCLMinutes.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtCLMinutes.Text) >= 60)
                {
                    MessageBox.Show("please enter valid Time in Minutes");
                    txtCLMinutes.Focus();
                }
            }
        }

     
 
       
    }
}
