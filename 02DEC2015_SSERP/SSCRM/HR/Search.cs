using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.IO;
using System.Text.RegularExpressions;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class Search : Form
    {
        public SQLDB objSQLDB;

        public string sType = "";
        private bool blLoad = false;
        public Search()
        {
            InitializeComponent();
        }

        public Search(string Types)
        {
            InitializeComponent();
            sType = Types;
        }

        private void Search_Load(object sender, EventArgs e)
        {
            //btnSave.Visible = false;
            blLoad = true;
            DateTime dt = DateTime.Now;
            dt = dt.AddYears(-18);
            dtpDOB.Value = Convert.ToDateTime(dt.ToString("dd/MM/yyyy"));
            dtpDOB.Checked = false;
            //gvCheckRecords.Visible = false;
            //string strSQL1 = "";
            //objSQLDB = new SQLDB();

            //strSQL1 = "SELECT HAED_APPL_NUMBER,HAED_SSC_NUMBER,HAED_NAME,HAED_DATEOFBIRTH,HAED_FATHER_NAME,HAMH_EORA_TYPE," +
            //        " HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_MY_PHOTO FROM HR_APPL_CHECK AC LEFT OUTER JOIN HR_APPL_MASTER_HEAD MH ON " +
            //        " AC.HAED_APPL_NUMBER=MH.HAMH_APPL_NUMBER";
            //Ds = objSQLDB.ExecuteDataSet(strSQL1, CommandType.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strSQL = "", strSQL1 = "", strSQL2 = "";
            objSQLDB = new SQLDB();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            try
            {
                if (!blLoad)
                {
                    blLoad = false;
                    if ((txtName.Text == "") && (txtFName.Text == "") && (txtSSCNo.Text == ""))
                    {
                        MessageBox.Show("Please enter any criteria.", "HR DATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    strSQL = " SELECT TOP 10 HAED_APPL_NUMBER,HAED_SSC_NUMBER,HAED_NAME,HAED_DATEOFBIRTH,HAED_FATHER_NAME,HAMH_EORA_TYPE," +
                            " HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_MY_PHOTO FROM HR_APPL_CHECK AC LEFT OUTER JOIN HR_APPL_MASTER_HEAD MH ON " +
                            " AC.HAED_APPL_NUMBER=MH.HAMH_APPL_NUMBER WHERE 1 = 1 ";

                    if (dtpDOB.Checked == true)
                    {
                        strSQL1 += " AND HAED_DATEOFBIRTH = '" + Convert.ToDateTime(dtpDOB.Value).ToString("dd/MMM/yyyy") + "' ";
                    }
                    if (txtSSCNo.Text != "")
                    {
                        strSQL2 += " AND replace(HAED_SSC_NUMBER,' ','') LIKE '%" + txtSSCNo.Text.Replace(" ", "") + "%'";
                    }
                    if (txtName.Text != "")
                    {
                        strSQL1 += " AND replace(HAED_NAME,' ','') LIKE '%" + txtName.Text.Replace(" ","") + "%'";
                    }
                    if (txtName.Text != "")
                    {
                        strSQL1 += " AND replace(HAED_FATHER_NAME,' ','') LIKE '%" + txtFName.Text.Replace(" ", "") + "%'";
                    }

                    strSQL1 += "  ORDER BY HAED_NAME";
                    if (txtSSCNo.Text != "")
                    {
                        dt2 = objSQLDB.ExecuteDataSet(strSQL + strSQL2, CommandType.Text).Tables[0];
                    }
                    dt1 = objSQLDB.ExecuteDataSet(strSQL + strSQL1, CommandType.Text).Tables[0];

                    lblTotal.Text = "Total Records: 0";
                    gvCheckRecords.DataSource = null;
                    if (dt1.Rows.Count > 0)
                    {
                        GetData(dt1);
                        btnSave.Enabled = false;
                        // lblMsg.Visible = false;
                    }
                    else if (dt2.Rows.Count > 0)
                    {
                        GetData(dt2);
                        btnSave.Enabled = false;
                        // lblMsg.Visible = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        // lblMsg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HR DATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dt1 = null;
            objSQLDB = null;
        }

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    #region commented
        //    //char[] testarr = txtSSCNo.Text.ToCharArray();
        //    //for (int i = 0; i < testarr.Length; i++)
        //    //{
        //    //    if (!char.IsLetterOrDigit(testarr[i]))
        //    //    {
        //    //        this.txtSSCNo.Text = this.txtSSCNo.Text.Remove(testarr[i], 1);
        //    //    }                
        //    //}
        //    //char[] trim = { '=', '\\', ';', '.', ':', ',', '+', '*', ' ' };//, "\", \";", ".", ":", ",", "+", "*" };
        //    //int pos;
        //    //while ((pos = this.txtSSCNo.Text.IndexOfAny(trim)) >= 0)
        //    //{                
        //    //    this.txtSSCNo.Text = this.txtSSCNo.Text.Remove(pos, 1);
        //    //}
        //    #endregion

        //    if ((txtName.Text == "") && (txtFName.Text == "") && (txtSSCNo.Text == ""))
        //    {
        //        //MessageBox.Show("Please enter any fields.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //return;
        //    }
        //    string sDateVal1 = "", sFilter = "", sFilter1 = "";
        //    DataTable dt1 = new DataTable();
        //    DataTable dt2 = new DataTable();
        //    lblTotal.Text = "Total Records: 0";
        //    if (dtpDOB.Checked == true)
        //    {
        //        sDateVal1 = " AND HAED_DATEOFBIRTH = #" + Convert.ToDateTime(dtpDOB.Value).ToString("MM/dd/yyyy") + "#";
        //    }
        //    if (txtSSCNo.Text != "")
        //    {
        //        DataView dvFilter1 = Ds.Tables[0].DefaultView;
        //        sFilter1 = "HAED_SSC_NUMBER LIKE '%" + txtSSCNo.Text + "%'";
        //        sFilter1 += sDateVal1;
        //        dvFilter1.RowFilter = sFilter1;
        //        dt2 = dvFilter1.ToTable();                 
        //    }
        //    bool ival = dtpDOB.Checked;
        //    btnSave.Visible = false;
        //    lblMsg.Visible = false;
        //    gvCheckRecords.Visible = false;

        //    DataView dvFilter = Ds.Tables[0].DefaultView;
        //    sFilter = "HAED_NAME LIKE '%" + txtName.Text + "%' AND HAED_FATHER_NAME LIKE '%" + txtFName.Text + "%'";
        //    sFilter += sDateVal1;
        //    dvFilter.RowFilter = sFilter;
        //    dt1 = dvFilter.ToTable();            
        //    gvCheckRecords.DataSource = null;
        //    if (dt1.Rows.Count > 0)
        //    {
        //        GetData(dt1);
        //        btnSave.Visible = false;
        //        lblMsg.Visible = false;
        //        gvCheckRecords.Visible = true;
        //    }
        //    else
        //    {
        //        GetData(dt2);
        //        btnSave.Visible = false;
        //        lblMsg.Visible = false;
        //        gvCheckRecords.Visible = true;
        //    }
        //    if ((dt1.Rows.Count == 0) && (dt2.Rows.Count == 0))
        //    {
        //        btnSave.Visible = true;
        //        lblMsg.Visible = true;
        //        gvCheckRecords.Visible = false;
        //    }
        //    objSQLDB = null;
        //}

        public void GetData(DataTable dtCheck)
        {
            int intRow = 1;
            gvCheckRecords.Rows.Clear();
            for (int i = 0; i < dtCheck.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellEora = new DataGridViewTextBoxCell();
                cellEora.Value = dtCheck.Rows[i]["HAMH_EORA_TYPE"];
                tempRow.Cells.Add(cellEora);

                DataGridViewCell cellCompanycode = new DataGridViewTextBoxCell();
                cellCompanycode.Value = dtCheck.Rows[i]["HAMH_COMPANY_CODE"];
                tempRow.Cells.Add(cellCompanycode);

                DataGridViewCell cellsBCode = new DataGridViewTextBoxCell();
                cellsBCode.Value = dtCheck.Rows[i]["HAMH_BRANCH_CODE"];
                tempRow.Cells.Add(cellsBCode);

                DataGridViewCell cellsApplNo = new DataGridViewTextBoxCell();
                cellsApplNo.Value = dtCheck.Rows[i]["HAED_APPL_NUMBER"];
                tempRow.Cells.Add(cellsApplNo);

                DataGridViewCell cellSSC = new DataGridViewTextBoxCell();
                cellSSC.Value = dtCheck.Rows[i]["HAED_SSC_NUMBER"];
                tempRow.Cells.Add(cellSSC);

                DataGridViewCell cellHeadName = new DataGridViewTextBoxCell();
                cellHeadName.Value = dtCheck.Rows[i]["HAED_NAME"];
                tempRow.Cells.Add(cellHeadName);

                
                DataGridViewCell cellDOB = new DataGridViewTextBoxCell();
                if (dtCheck.Rows[i]["HAED_DATEOFBIRTH"].ToString() != "")
                    cellDOB.Value = Convert.ToDateTime(dtCheck.Rows[i]["HAED_DATEOFBIRTH"]).ToString("dd/MM/yyyy");
                tempRow.Cells.Add(cellDOB);

                DataGridViewCell cellFname = new DataGridViewTextBoxCell();
                cellFname.Value = dtCheck.Rows[i]["HAED_FATHER_NAME"];
                tempRow.Cells.Add(cellFname);
                DataGridViewCell cellPhoto1 = new DataGridViewTextBoxCell();
                DataGridViewCell cellPhoto = new DataGridViewImageCell();
                if (dtCheck.Rows[i]["HAMH_MY_PHOTO"].ToString() != "")
                {
                    cellPhoto.Value = dtCheck.Rows[i]["HAMH_MY_PHOTO"];
                    tempRow.Cells.Add(cellPhoto);
                }
                else
                    tempRow.Cells.Add(cellPhoto1);
                intRow = intRow + 1;
                gvCheckRecords.Rows.Add(tempRow);
            }
            lblTotal.Text = "Total Records: " + gvCheckRecords.Rows.Count;
        }

        public void GetImage(byte[] imageData)
        {
            try
            {
                //Initialize image variable
                Image newImage;
                //Read image data into a memory stream
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);

                    //Set image variable value using memory stream.
                    newImage = Image.FromStream(ms, true);
                }
                //set picture
                pictureBox1.BackgroundImage = newImage;
                pictureBox1.Visible = true;
                this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                pictureBox1.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
                return;

            int year = Convert.ToInt32(Convert.ToDateTime(CommonData.CurrentDate).Year) - Convert.ToInt32(Convert.ToDateTime(dtpDOB.Value).Year);
            if (year < 18)
            {
                MessageBox.Show("Dateof birth bellow 18 years not accepted.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gvCheckRecords.Rows.Count == 0)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want enter application?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    frmApplication frmchld = new frmApplication(txtName.Text, txtFName.Text, dtpDOB.Value, txtSSCNo.Text.Replace(" ", ""), sType);//, txtRecruited.Text, txtTrained.Text, dtFrmDt.Value, dtToDt.Value);
                    frmchld.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    frmchld.MdiParent = MdiParent;
                    frmchld.Show();
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("Data Matched with others", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSSCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && (e.KeyChar != ' '))
            {
                if (!char.IsLetterOrDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            blLoad = false;
            if (e.KeyChar != '\'')
            {
                if (char.IsLetter((e.KeyChar)) == false && e.KeyChar == '\b')
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    e.Handled = true;
                }
            }
        }

        private void txtFName_KeyPress(object sender, KeyPressEventArgs e)
        {
            blLoad = false;
            if ((e.KeyChar != '\b') && (e.KeyChar != '\''))
            {
                if (!char.IsLetter((e.KeyChar)))
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvCheckRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvCheckRecords.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvCheckRecords.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        string CompanyCode = gvCheckRecords.Rows[e.RowIndex].Cells[gvCheckRecords.Columns["HAMH_COMPANY_CODE"].Index].Value.ToString();
                        string BranchCode = gvCheckRecords.Rows[e.RowIndex].Cells[gvCheckRecords.Columns["HAMH_BRANCH_CODE"].Index].Value.ToString();
                        string ApplNo = gvCheckRecords.Rows[e.RowIndex].Cells[gvCheckRecords.Columns["HAMH_APPL_NUMBER"].Index].Value.ToString();
                        GetImage((byte[])gvCheckRecords.Rows[e.RowIndex].Cells[gvCheckRecords.Columns["HAMH_MY_PHOTO"].Index].Value);
                    }
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            //blLoad = false;
            ////if (txtName.Text != "")
            ////{
            //txtName.Text = System.Text.RegularExpressions.Regex.Replace(txtName.Text, @"\s+", " ");
            //txtName.Text = txtName.Text.TrimStart(' ');
            //gvCheckRecords.Rows.Clear();
            //btnSearch_Click(null, null);
            ////}
            ////else
            ////    gvCheckRecords.Rows.Clear();
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {
            //blLoad = false;
            ////if (txtFName.Text != "")
            ////{
            //txtFName.Text = System.Text.RegularExpressions.Regex.Replace(txtFName.Text, @"\s+", " ");
            //txtFName.Text = txtFName.Text.TrimStart(' ');
            //gvCheckRecords.Rows.Clear();
            //btnSearch_Click(null, null);
            ////}
            ////else
            ////    gvCheckRecords.Rows.Clear();
        }

        private void txtSSCNo_TextChanged(object sender, EventArgs e)
        {
           // blLoad = false;
           // btnSearch_Click(null, null);
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            //blLoad = false;
            ////if (txtName.Text != "")
            ////{
            //txtName.Text = System.Text.RegularExpressions.Regex.Replace(txtName.Text, @"\s+", " ");
            //txtName.Text = txtName.Text.TrimStart(' ');
            //gvCheckRecords.Rows.Clear();
            //if (txtName.Text.Trim().Length > 5)
            //{
            //    btnSearch_Click(null, null);
            //}
        }

        private void txtFName_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtSSCNo_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void txtName_Validated(object sender, EventArgs e)
        {

            blLoad = false;
            gvCheckRecords.Rows.Clear();
            if (txtSSCNo.Text.Trim().Length >= 3)
            {
                btnSearch_Click(null, null);
            }
        }

        private void txtFName_Validated(object sender, EventArgs e)
        {
            blLoad = false;
            //if (txtFName.Text != "")
            //{
            txtFName.Text = System.Text.RegularExpressions.Regex.Replace(txtFName.Text, @"\s+", " ");
            txtFName.Text = txtFName.Text.TrimStart(' ');
            gvCheckRecords.Rows.Clear();
            if (txtFName.Text.Trim().Length >= 3)
            {
                btnSearch_Click(null, null);
            }
            //}
            //else
            //    gvCheckRecords.Rows.Clear();
        }
    }
}
