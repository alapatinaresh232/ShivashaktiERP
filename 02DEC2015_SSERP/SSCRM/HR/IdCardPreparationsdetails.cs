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
    public partial class IdCardPreparationsdetails : Form
    {
        SQLDB objDb = null;
        DataSet dsEmpData = null,ds=null;
        string sqlText="",strDateType="";

        public IdCardPreparationsdetails()
        {
            InitializeComponent();
        }
        public IdCardPreparationsdetails(string str)
        {
            strDateType = str;
            InitializeComponent();
        }
        private void Idcarddetails_Load(object sender, EventArgs e)
        {
            gvEmpDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            cmbTrnType.SelectedIndex = 0;
            //if (strDateType == "R")
            //{
            //    gvEmpDetl.Columns["RecDate"].Visible = true;
            //    gvEmpDetl.Columns["PrepDate"].Visible = false;
            //    gvEmpDetl.Columns["PrintDate"].Visible = false;
            //    gvEmpDetl.Columns["DispatchDate"].Visible = false;
            //    gvEmpDetl.Columns["select"].Visible = false;

            //    lblEcode.Visible = true;
            //    txtEcode.Visible = true;
            //    txtName.Visible = true;
            //    btnAdd.Visible = true;
                
            //    showEmpDetails();
            //}
            //else if (strDateType == "P")
            //{
            //    gvEmpDetl.Columns["RecDate"].Visible = true;
            //    gvEmpDetl.Columns["PrepDate"].Visible = false;
            //    gvEmpDetl.Columns["PrintDate"].Visible = false;
            //    gvEmpDetl.Columns["DispatchDate"].Visible = false;
            //    gvEmpDetl.Columns["select"].Visible = true;

            //    lblEcode.Visible = false;
            //    txtEcode.Visible = false;
            //    txtName.Visible = false;
            //    btnAdd.Visible = false;

            //    showEmpDetails();

            //}
            //else if (strDateType == "T")
            //{
            //    gvEmpDetl.Columns["RecDate"].Visible = false;
            //    gvEmpDetl.Columns["PrepDate"].Visible = true;
            //    gvEmpDetl.Columns["PrintDate"].Visible = false;
            //    gvEmpDetl.Columns["DispatchDate"].Visible = false;
            //    gvEmpDetl.Columns["select"].Visible = true;
            //    lblEcode.Visible = false;
            //    txtEcode.Visible = false;
            //    txtName.Visible = false;
            //    btnAdd.Visible = false;

            //    showEmpDetails();
                
            //}
            //else if (strDateType == "D")
            //{
            //    gvEmpDetl.Columns["RecDate"].Visible = false;
            //    gvEmpDetl.Columns["PrepDate"].Visible = false;
            //    gvEmpDetl.Columns["PrintDate"].Visible = true;
            //    gvEmpDetl.Columns["DispatchDate"].Visible = false;
            //    gvEmpDetl.Columns["select"].Visible = true;

            //    lblEcode.Visible = false;
            //    txtEcode.Visible = false;
            //    txtName.Visible = false;
            //    btnAdd.Visible = false;

            //    showEmpDetails();
            //}
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (strDateType == "R" && txtName.Text != "")
            {
                objDb = new SQLDB();
                ds = new DataSet();
                int iCount = 0;
                try
                {
                    sqlText = "SELECT COUNT(*) FROM ID_CARD_DISPATCH_HISTORY WHERE icdh_eora_code="+txtEcode.Text+" AND ICDH_CARD_STATUS IN ('R','P','T')";
                    ds=objDb.ExecuteDataSet(sqlText);
                    iCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iCount == 0)
                {
                    try
                    {
                        iCount = 0;
                        sqlText = "SELECT ISNULL(MAX(ICDH_TRN_NO),0)+1 FROM ID_CARD_DISPATCH_HISTORY";
                        ds = objDb.ExecuteDataSet(sqlText);
                        sqlText = "INSERT INTO ID_CARD_DISPATCH_HISTORY(ICDH_APPL_NO,ICDH_EORA_CODE,ICDH_BRANCH_CODE,ICDH_DESIG_ID" +
                            ",ICDH_TRN_NO,ICDH_CARD_STATUS,ICDH_APPL_RECVD_DATE,ICDH_CREATED_BY,ICDH_CREATED_DATE) VALUES(" + Convert.ToInt32(dsEmpData.Tables[0].Rows[0]["HAMH_APPL_NUMBER"]) +
                            "," + Convert.ToInt32(dsEmpData.Tables[0].Rows[0]["ECODE"]) + ",'" + dsEmpData.Tables[0].Rows[0]["BRANCH_CODE"] +
                            "'," + dsEmpData.Tables[0].Rows[0]["DESG_ID"] + "," + Convert.ToInt32(ds.Tables[0].Rows[0][0]) + ",'R'," +
                            "'" + Convert.ToDateTime(dtpDate.Value).ToString("yyyy/MM/dd") + "','"+CommonData.LogUserId+"',getdate())";
                        iCount = objDb.ExecuteSaveData(sqlText);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objDb = null;
                        ds = null;
                        dsEmpData = null;
                    }
                    if (iCount > 0)
                    {
                        MessageBox.Show("Data Saved Successfully","IDCards",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        showEmpDetails();
                    }
                    else
                    {
                        MessageBox.Show("Data not Saved", "IDCards", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Record already in printing process");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            objDb = new SQLDB();
            sqlText = "";
            for (int i = 0; i < gvEmpDetl.Rows.Count; i++)
            {
                bool bflag = (bool)gvEmpDetl.Rows[i].Cells["select"].EditedFormattedValue;
                if (bflag)
                {                   
                    if (strDateType == "P")
                    {
                        sqlText += " update ID_CARD_DISPATCH_HISTORY set ICDH_ID_CARD_PREP_DATE= " +
                                    "'" + Convert.ToDateTime(dtpDate.Value).ToString("yyyy/MM/dd") +
                                    "',ICDH_CARD_STATUS='P'   where ICDH_TRN_NO=" + gvEmpDetl.Rows[i].Cells["transNo"].Value;
                    }
                    else if (strDateType == "T")
                    {
                        sqlText += " update ID_CARD_DISPATCH_HISTORY set ICDH_PRINT_SEND_DATE= '" + Convert.ToDateTime(dtpDate.Value).ToString("yyyy/MM/dd") + "',ICDH_CARD_STATUS='T'   where ICDH_TRN_NO=" + gvEmpDetl.Rows[i].Cells["transNo"].Value;
                    }
                    else if (strDateType == "D")
                    {
                        sqlText += " update ID_CARD_DISPATCH_HISTORY set ICDH_DISPATCH_DATE= '" + Convert.ToDateTime(dtpDate.Value).ToString("yyyy/MM/dd") + "',ICDH_CARD_STATUS='D'   where ICDH_TRN_NO=" + gvEmpDetl.Rows[i].Cells["transNo"].Value;
                    }
                }
            }
            if (sqlText.Length > 10)
            {
                try
                {
                    iRes = objDb.ExecuteSaveData(sqlText);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                }

                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSCRM-IDCards", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showEmpDetails();
                }
                else
                {
                    MessageBox.Show("Data not Saved", "SSCRM-IDCards", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcode_TextChanged(object sender, EventArgs e)
        {

        }
        private void showEmpDetails()
        {
            string sStatus = "";
            if (strDateType == "R" || strDateType == "P")
                sStatus = "R";
            else if (strDateType == "T")
                sStatus = "P";
            else if (strDateType == "D")
                sStatus = "T";
            else
                sStatus = "";
            objDb = new SQLDB();
            ds = new DataSet();
            gvEmpDetl.Rows.Clear();
            try
            {
                sqlText = "SELECT ICDH_APPL_NO,ICDH_EORA_CODE,EM.MEMBER_NAME,DM.desig_name,DESG_ID," +
                          "ICDH_BRANCH_CODE,BRANCH_NAME,case when ICDH_CARD_STATUS='R' THEN 'RECIEVED' " +
                          "WHEN ICDH_CARD_STATUS='P' THEN 'PREPARED' WHEN ICDH_CARD_STATUS='T' THEN 'PRINTING' " +
                          "WHEN ICDH_CARD_STATUS='D' THEN 'DISPATCHED' ELSE '' END AS ICDH_CARD_STATUS,ICDH_APPL_RECVD_DATE,ICDH_TRN_NO" +
                          ",ICDH_DISPATCH_DATE,ICDH_ID_CARD_PREP_DATE,ICDH_PRINT_SEND_DATE " +
                          "FROM ID_CARD_DISPATCH_HISTORY " +
                          "INNER JOIN EORA_MASTER EM ON  ICDH_EORA_CODE = EM.ECODE " +
                          "INNER JOIN DESIG_MAS DM ON ICDH_DESIG_ID = DM.desig_code " +
                          "INNER JOIN BRANCH_MAS BM ON  ICDH_BRANCH_CODE = BM.BRANCH_CODE " +
                          "WHERE ICDH_CARD_STATUS='" + sStatus + "'";

                ds = objDb.ExecuteDataSet(sqlText);                
                
                if(ds.Tables[0] != null)
                {
                    for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSiNO = new DataGridViewTextBoxCell();
                        cellSiNO.Value = (i + 1).ToString();
                        tempRow.Cells.Add(cellSiNO);

                        DataGridViewCell cellTransNo = new DataGridViewTextBoxCell();
                        cellTransNo.Value = ds.Tables[0].Rows[i]["ICDH_TRN_NO"];
                        tempRow.Cells.Add(cellTransNo);

                        DataGridViewCell cellAppNo = new DataGridViewTextBoxCell();
                        cellAppNo.Value = ds.Tables[0].Rows[i]["ICDH_APPL_NO"];
                        tempRow.Cells.Add(cellAppNo);

                        DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                        cellEcode.Value = ds.Tables[0].Rows[i]["ICDH_EORA_CODE"];
                        tempRow.Cells.Add(cellEcode);

                        DataGridViewCell cellName = new DataGridViewTextBoxCell();
                        cellName.Value = ds.Tables[0].Rows[i]["MEMBER_NAME"];
                        tempRow.Cells.Add(cellName);

                        DataGridViewCell cellDesigId = new DataGridViewTextBoxCell();
                        cellDesigId.Value = ds.Tables[0].Rows[i]["DESG_ID"];
                        tempRow.Cells.Add(cellDesigId);

                        DataGridViewCell cellDesig = new DataGridViewTextBoxCell();
                        cellDesig.Value = ds.Tables[0].Rows[i]["desig_name"];
                        tempRow.Cells.Add(cellDesig);

                        DataGridViewCell cellBrCode = new DataGridViewTextBoxCell();
                        cellBrCode.Value = ds.Tables[0].Rows[i]["ICDH_BRANCH_CODE"];
                        tempRow.Cells.Add(cellBrCode);

                        DataGridViewCell cellBrName = new DataGridViewTextBoxCell();
                        cellBrName.Value = ds.Tables[0].Rows[i]["BRANCH_NAME"];
                        tempRow.Cells.Add(cellBrName);

                        DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                        cellStatus.Value = ds.Tables[0].Rows[i]["ICDH_CARD_STATUS"];
                        tempRow.Cells.Add(cellStatus);

                        DataGridViewCell cellRecDate = new DataGridViewTextBoxCell();
                        cellRecDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[i]["ICDH_APPL_RECVD_DATE"].ToString()).ToShortDateString();
                        tempRow.Cells.Add(cellRecDate);

                        DataGridViewCell cellPrepDate = new DataGridViewTextBoxCell();
                        cellPrepDate.Value = ds.Tables[0].Rows[i]["ICDH_ID_CARD_PREP_DATE"];
                        tempRow.Cells.Add(cellPrepDate);

                        DataGridViewCell cellPrintDate = new DataGridViewTextBoxCell();
                        cellPrintDate.Value = ds.Tables[0].Rows[i]["ICDH_PRINT_SEND_DATE"];
                        tempRow.Cells.Add(cellPrintDate);

                        DataGridViewCell cellDipDate = new DataGridViewTextBoxCell();
                        cellDipDate.Value = ds.Tables[0].Rows[i]["ICDH_DISPATCH_DATE"];
                        tempRow.Cells.Add(cellDipDate);

                        gvEmpDetl.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ds = null;
                objDb = null;
            }
        }

        private void gvEmpDetl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTrnType.SelectedIndex > 0)
            {
                if (cmbTrnType.SelectedIndex == 1)
                {
                    strDateType = "R";
                    gvEmpDetl.Columns["RecDate"].Visible = true;
                    gvEmpDetl.Columns["PrepDate"].Visible = false;
                    gvEmpDetl.Columns["PrintDate"].Visible = false;
                    gvEmpDetl.Columns["DispatchDate"].Visible = false;
                    gvEmpDetl.Columns["select"].Visible = false;

                    lblEcode.Text = "Ecode";
                    
                    txtName.Visible = true;
                    btnAdd.Visible = true;

                    dtpDate.Value = Convert.ToDateTime(CommonData.CurrentDate);

                    showEmpDetails();
                }
                else if (cmbTrnType.SelectedIndex == 2)
                {
                    strDateType = "P";
                    gvEmpDetl.Columns["RecDate"].Visible = true;
                    gvEmpDetl.Columns["PrepDate"].Visible = false;
                    gvEmpDetl.Columns["PrintDate"].Visible = false;
                    gvEmpDetl.Columns["DispatchDate"].Visible = false;
                    gvEmpDetl.Columns["select"].Visible = true;

                    lblEcode.Text = "Search";
                    
                    txtName.Visible = false;
                    btnAdd.Visible = false;
                    dtpDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                    showEmpDetails();
                }
                else if (cmbTrnType.SelectedIndex == 3)
                {
                    strDateType = "T";
                    gvEmpDetl.Columns["RecDate"].Visible = false;
                    gvEmpDetl.Columns["PrepDate"].Visible = true;
                    gvEmpDetl.Columns["PrintDate"].Visible = false;
                    gvEmpDetl.Columns["DispatchDate"].Visible = false;
                    gvEmpDetl.Columns["select"].Visible = true;
                    lblEcode.Text = "Search";
                    
                    txtName.Visible = false;
                    btnAdd.Visible = false;
                    dtpDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                    showEmpDetails();
                }
                else if (cmbTrnType.SelectedIndex == 4)
                {
                    strDateType = "D";
                    gvEmpDetl.Columns["RecDate"].Visible = false;
                    gvEmpDetl.Columns["PrepDate"].Visible = false;
                    gvEmpDetl.Columns["PrintDate"].Visible = true;
                    gvEmpDetl.Columns["DispatchDate"].Visible = false;
                    gvEmpDetl.Columns["select"].Visible = true;
                    dtpDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                    lblEcode.Text = "Search";
                    
                    txtName.Visible = false;
                    btnAdd.Visible = false;

                    showEmpDetails();
                }
                dtpDate.Visible = true;
                lblDate.Visible = true;
            }
            else
            {
                
                dtpDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                lblEcode.Text = "Search";
                
                txtName.Visible = false;
                btnAdd.Visible = false;
                dtpDate.Visible = false;
                lblDate.Visible = false;

                showEmpDetails();
            }
            txtEcode.Text = "";
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbTrnType.SelectedIndex > 1)
            {
                gvEmpDetl.ClearSelection();
                int rowIndex = 0;
                foreach (DataGridViewRow row in gvEmpDetl.Rows)
                {
                    if (row.Cells[3].Value.ToString() == txtEcode.Text)
                    {
                        rowIndex = row.Index;
                        gvEmpDetl.CurrentCell = gvEmpDetl.Rows[rowIndex].Cells[3];
                        break;
                    }
                }
            }
            else
            {
                if (txtEcode.Text.ToString().Trim().Length > 4)
                {
                    if (txtEcode.Text != "")
                    {
                        objDb = new SQLDB();
                        dsEmpData = new DataSet();
                        try
                        {
                            sqlText = "SELECT ECODE,MEMBER_NAME,desg_id,EM.BRANCH_CODE,BRANCH_NAME,HAMH_APPL_NUMBER,desig_name FROM EORA_MASTER EM " +
                                      "INNER JOIN DESIG_MAS DM ON DM.desig_code=EM.DESG_ID " +
                                      "INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE=EM.BRANCH_CODE " +
                                      "INNER JOIN HR_APPL_MASTER_HEAD HM ON HM.HAMH_EORA_CODE=EM.ECODE WHERE ECODE=" + txtEcode.Text;
                            dsEmpData = objDb.ExecuteDataSet(sqlText);
                            if (dsEmpData.Tables[0].Rows.Count != 0)
                            {
                                txtName.Text = dsEmpData.Tables[0].Rows[0]["MEMBER_NAME"].ToString();
                            }
                            else
                                txtName.Text = "";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            objDb = null;
                        }
                    }
                    //GetEmpData();
                }
                else
                {
                    txtName.Text = "";
                }
            }
            
        }
        private void GetEmpData()
        {
            objDb = new SQLDB();
            if (txtEcode.Text.ToString().Trim().Length > 4)
            {
                DataTable dt = objDb.ExecuteDataSet("SELECT ISNULL(HAAM_EMP_CODE,ECODE) ECODE,CAST(ISNULL(HAAM_EMP_CODE,ECODE) AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' AS ENAME " +
                                                       "FROM EORA_MASTER LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = ECODE " +
                                                       "WHERE ECODE=" + txtEcode.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtName.Text = dt.Rows[0]["ENAME"].ToString();
                else
                    txtName.Text = "";
            }
            else
                txtName.Text = "";
            objDb = null;
        }
        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

       
    }
}
