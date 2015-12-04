using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using SSCRMDB;
using SSTrans;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class ECodesSearch : Form
    {
        ServiceDB objServiceDB;
        DataSet ds;
        public ActivityServiceUpdate objActivityServiceUpdate;
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;
        SQLDB objSQLdb = null;
        private string BranchCode = "", sActivityID = "", InvoiceNo = "", sCheckVal = "", FinYear = "",sOrderNo = "", strCmd = "",sEcode="",sActDate="",sProductId="";
        private string sMobileNo = "", SenderId = "";
        DateTime dtpTarDate;
       
        DataTable dt = new DataTable();
      
        string DeptCode = "800000";

        public ECodesSearch()
        {
            InitializeComponent();
        }
        public ECodesSearch(string BCode,string FYear, string ActID, string InvNo, string OrderNo,string strEcode,string dtpDate,string sProdId)
        {
            InitializeComponent();
            BranchCode = BCode;
            sActivityID = ActID;
            InvoiceNo = InvNo;
            FinYear = FYear;
            sOrderNo = OrderNo; 
            sCheckVal = "1";
            sEcode = strEcode;
            sActDate = dtpDate;
            sProductId = sProdId;
        }

        private void ECodeSearch_Load(object sender, EventArgs e)
        {
            objServiceDB = new ServiceDB();            
            ds = objServiceDB.GetECodesforService(BranchCode,DeptCode);

            txtMessage.Visible = false;
            lblMsg.Visible = false;
            lblLangType.Visible = false;
            cbLanguages.Visible = false;
            txtMessage.Text = "";

            FillCropDetails();
            txtProdName.ReadOnly = true;
            txtOrderNo.ReadOnly = true;
            txtActivityName.ReadOnly = true;
            txtInvQty.ReadOnly = true;
            txtInvNo.ReadOnly = true;
            dtpActivityDate.Value = DateTime.Today;        
                        
            objServiceDB = null;

            cbActivityMode.SelectedIndex = 0;
            txtServiceSearch_Validated(null,null);

            dtpActivityDate.Value = DateTime.Today;
            txtEcodeSearch.Visible = false;
            txtEName.Visible = false;
            lblApprBy.Visible = false;
          
            if (sCheckVal == "1")
            {
                try
                {
                    FillEmployeeData();
                    EcodeSearch();
                    
                    objSQLdb = new SQLDB();

                    strCmd = "exec Get_ServiceActivityDetails '','" + BranchCode + "','" + FinYear + "','" + InvoiceNo + "','" + sOrderNo + "'," + sActivityID + ",'"+ sProductId +"'";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    sMobileNo = dt.Rows[0]["PhoneNo"].ToString();

                    if (dt.Rows[0]["AttendedEcode"].ToString() != "")
                    {
                        sEcode = dt.Rows[0]["AttendedEcode"].ToString();
                    }
                    txtServiceSearch.Text = sEcode;
                    if (sEcode.Length > 1)
                        txtServiceSearch_KeyUp(null, null);
                    if (sEcode != "")
                    {
                        cmbEmployee.SelectedValue = sEcode;
                    }
                    if (dt.Rows[0]["ActivityMode"].ToString() != "")
                    {
                        cbActivityMode.Text = dt.Rows[0]["ActivityMode"].ToString();
                    }
                    else
                    {
                        cbActivityMode.SelectedIndex = 0;
                    }

                    if (dt.Rows[0]["CropName"].ToString() != "")
                    {
                        cbCrops.Text = dt.Rows[0]["CropName"].ToString();
                    }
                    else
                    {
                        cbCrops.SelectedIndex = 0;
                    }
                  
                    txtRemarks.Text = dt.Rows[0]["FarmerRemarks"].ToString();
                    txtOrderNo.Text = sOrderNo;
                    txtInvNo.Text = InvoiceNo;
                    txtProdName.Text = dt.Rows[0]["ProductName"].ToString();
                    txtProdName.Tag = dt.Rows[0]["ProductId"].ToString();
                    txtActivityName.Text = dt.Rows[0]["ActivityName"].ToString();
                    txtInvQty.Text = dt.Rows[0]["Qty"].ToString();
                    txtCropAcres.Text = dt.Rows[0]["CropAcres"].ToString();
                    txtFarmerOpinion.Text = dt.Rows[0]["FarmerOpinion"].ToString();
                    txtAOSuggestion.Text = dt.Rows[0]["AoSuggestion"].ToString();
                    if (dt.Rows[0]["ActualQty"].ToString().Equals("0.00"))
                        txtActualQty.ReadOnly = false;
                    else
                        txtActualQty.ReadOnly = true;
                    txtActualQty.Text = dt.Rows[0]["ActualQty"].ToString();
                    txtQty.Text = dt.Rows[0]["ActivityQty"].ToString();

                    if (sActDate.Length != 0)
                    {
                        txtServiceSearch.ReadOnly = true;
                        cmbEmployee.Enabled = false;
                        dtpActivityDate.Enabled = false;
                        dtpActivityDate.Value = Convert.ToDateTime(sActDate);
                    }
                    else
                    {
                        txtServiceSearch.ReadOnly = false;
                        cmbEmployee.Enabled = true;
                        dtpActivityDate.Enabled = true;
                    }
                    if (dt.Rows[0]["ActualDate"].ToString() != "")
                    {
                        dtpActivityDate.Value = Convert.ToDateTime(dt.Rows[0]["ActualDate"].ToString());
                    }


                    if (dt.Rows[0]["ReplApprovedName"].ToString() != "")
                    {
                        lblApprBy.Visible = true;
                        txtEcodeSearch.Visible = true;
                        txtEName.Visible = true;
                        txtEcodeSearch.Text = dt.Rows[0]["ReplApprBy"].ToString();
                        txtEName.Text = dt.Rows[0]["ReplApprovedName"].ToString();
                    }
                    if (dt.Rows[0]["ActivityName"].ToString() == "COUNTING")
                    {
                        lblQty.Text = "Recommended Qty";
                    }
                    else if (dt.Rows[0]["ActivityName"].ToString().Substring(0,5) == "REPLA")
                    {
                        lblQty.Text = "Replaced Qty";
                    }
                    else if (dt.Rows[0]["ActivityName"].ToString() == "PRE PLANTING")
                    {
                        lblQty.Text = "Actual Qty";
                    }
                    else
                    {
                        lblQty.Text = "Actual Qty";
                    }
                    //if (txtActivityName.Text.Substring(0, 3) == "REP")
                    //{
                    //    txtMessage.Visible = true;
                    //    lblMsg.Visible = true;
                    //    lblLangType.Visible = true;
                    //    cbLanguages.Visible = true;
                    //    cbLanguages.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    txtMessage.Visible = false;
                    //    lblMsg.Visible = false;
                    //    lblLangType.Visible = false;
                    //    cbLanguages.Visible = false;
                    //}

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
        }
        private void SearchItems(ComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            sb.Append(acComboBox.Text.Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Substring(selectionEnd));

            index = acComboBox.FindString(sb.ToString());

            if (index == -1)
                e.Handled = false;
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                e.Handled = true;
            }
        }
        private void FillCropDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbCrops.DataSource = null;
            try
            {
                string strCmd = "SELECT CROP_ID,CROP_NAME FROM CROP_MASTER ORDER BY CROP_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCrops.DataSource = dt;
                    cbCrops.DisplayMember = "CROP_NAME";
                    cbCrops.ValueMember = "CROP_ID";

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

           
        public void GetECodeData(string BranchCode)
        {
            objServiceDB = new ServiceDB();
           
            DataSet dsData = objServiceDB.GetECodesforService(BranchCode,DeptCode);
            objServiceDB = null;
            UtilityLibrary.PopulateControl(cmbEmployee, dsData.Tables[0].DefaultView, 1, 0, "-- Please select --", 0);
            if (sCheckVal == "1")
            {
                if (sEcode != "")
                    cmbEmployee.SelectedValue = sEcode.Split('-')[0].ToString();
            }            
        }

        private bool CheckData()
        {
            bool flag = true;
            double dProdQty = 0, dActQty = 0;

            if (cmbEmployee.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Employee","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                cmbEmployee.Focus();
                return flag;

            }
            if (dtpActivityDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Activity Date Less than toDay's Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpActivityDate.Focus();
                return flag;
            }
            if (txtQty.Text != "")
                dActQty = Convert.ToDouble(txtQty.Text);
            if (txtInvQty.Text != "")
                dProdQty = Convert.ToDouble(txtInvQty.Text);

            //if (dActQty > dProdQty)
            //{
            //    flag = false;
            //    MessageBox.Show("Replaced Quantity less than or equal to Purchased Quantity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtQty.Focus();
            //    return flag;
            //}
            if (dt.Rows[0]["ReplApprovedName"].ToString() != "")
            {
                if (txtEName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Approved Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtEcodeSearch.Focus();
                    return flag;
                }
            }
            return flag;

        }           
   
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRetVal = 0;
            string CropName = "", sActivityMode = "";

            if (CheckData() == true)
            {
                try
                {
                    if (txtQty.Text.Length == 0)
                    {
                        txtQty.Text = "0";
                    }
                    if (txtEName.Text.Length == 0)
                    {
                        txtEcodeSearch.Text = "0";
                    }
                    if (txtActualQty.Text.Length == 0)
                    {
                        txtActualQty.Text = "0";
                    }
                    if (cbCrops.SelectedIndex > 0)
                    {
                        CropName = cbCrops.Text.ToString();
                    }
                    if (cbActivityMode.SelectedIndex > 0)
                    {
                        sActivityMode = cbActivityMode.Text.ToString();
                    }
                    else
                    {
                        sActivityMode = "";
                    }

                    string sqlEcode = cmbEmployee.Text.ToString().Split('-')[0];

                    string sqlUpdate = "UPDATE SERVICES_TNA SET TNA_ACTUAL_DATE='" + Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MMM/yyyy") + 
                                        "',TNA_ATTEND_BY_ECODE=" + cmbEmployee.SelectedValue +
                                        ",TNA_FARMER_REMARKS='" + txtRemarks.Text.ToString().Replace("'"," ") + 
                                        "',TNA_ACTIVITY_QTY='" + Convert.ToDouble(txtQty.Text).ToString("0.00") +
                                       "',TNA_CREATED_BY='" + CommonData.LogUserId + "', TNA_CREATED_DATE=getdate() "+
                                       ", TNA_REPL_APPROVED_BY="+ Convert.ToInt32(txtEcodeSearch.Text) +" " + 
                                       ", TNA_ACTUAL_QTY="+ Convert.ToDouble(txtActualQty.Text) +
                                       ", TNA_ACTIVITY_MODE='"+ sActivityMode +
                                       "',TNA_CROP_NAME='"+ CropName +
                                       "',TNA_CROP_ACRES='"+ txtCropAcres.Text.ToString().Replace("'","") +
                                       "',TNA_FARMER_OPINION='"+ txtFarmerOpinion.Text.ToString().Replace("'","") +
                                       "',TNA_AO_SUGGESTION='"+ txtAOSuggestion.Text.ToString().Replace("'","") +
                                       "' WHERE TNA_BRANCH_CODE='" + BranchCode + "' AND TNA_FIN_YEAR='" + FinYear + 
                                       "' AND TNA_ACTIVITY_ID=" + sActivityID +
                                       " and TNA_PRODUCT_ID='"+ txtProdName.Tag.ToString() +"' AND TNA_INVOICE_NUMBER=" + InvoiceNo;

                  
                    if (sqlUpdate.Length > 5)
                    {
                        iRetVal = objSQLdb.ExecuteSaveData(sqlUpdate);
                    }
                    //if (iRetVal > 0)
                    //{
                    //    if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MM/yyyy"))).TotalDays <= 3)
                    //    {

                    //        if (txtActivityName.Text.Substring(0, 5) == "REPLA" && txtMessage.Text.Length > 10 && cbActivityMode.SelectedIndex == 0)
                    //        {
                    //            if (sMobileNo.Length >= 10)
                    //            {
                    //                DialogResult result = MessageBox.Show("Do you want to Send SMS to Customer?",
                    //                                "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //                if (result == DialogResult.Yes)
                    //                {
                    //                    SendSMSToCustomers();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    ((ActivityServiceUpdate)objActivityServiceUpdate).GetAllInvoicdeData(sOrderNo, InvoiceNo);

                    //if (sEcode.Length > 0 && sActDate.Length > 0)
                    //{
                        
                    //    ((EmployeeDARWithTourBills)objEmployeeDARWithTourBills).FillEmployeeActivityDetails(Convert.ToInt32(sEcode), sActDate);
                    //}

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQty.Text = "";
            dtpActivityDate.Value = DateTime.Today;
            txtRemarks.Text = "";
            cmbEmployee.SelectedIndex = 0;
            txtAOSuggestion.Text = "";
            txtFarmerOpinion.Text = "";
            cbCrops.SelectedIndex = 0;
            cbActivityMode.SelectedIndex = 0;
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtCropAcres.Text = "";
            txtMessage.Text = "";
            cbLanguages.SelectedIndex = 0;
            
            
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER " +
                                    " WHERE ECODE=" + txtEcodeSearch.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EName"].ToString();
                    }
                    else
                    {
                        txtEName.Text = "";
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
                txtEName.Text = "";
            }
        }

        private void cmbEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)8))
            {
                SearchItems(cmbEmployee, ref e);
            }
            else
                e.Handled = false;
        }

        private void cmbEmployee_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtServiceSearch.Text.ToString().Trim().Length > 4)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void EcodeSearch()
        {
            SQLDB objData = new SQLDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cmbEmployee.DataSource = null;
                cmbEmployee.Items.Clear();
                dsEmp = objData.ExecuteDataSet("SELECT top 10 * FROM(SELECT ECODE,CAST(ECODE AS VARCHAR)+' - '+MEMBER_NAME+' ('+DESIG+')' ENAME,DESIG  "+
                                "FROM EORA_MASTER WHERE DEPT_ID='800000' AND MEMBER_NAME IS NOT NULL AND EORA = 'E') EMP WHERE ENAME LIKE '%" + txtServiceSearch.Text.ToString().Trim() + "%'");
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cmbEmployee.DataSource = dtEmp;
                    cmbEmployee.DisplayMember = "ENAME";
                    cmbEmployee.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cmbEmployee.SelectedIndex > -1)
                {
                    cmbEmployee.SelectedIndex = 0;
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }


        private void FillEmployeeData()
        {
            objServiceDB = new ServiceDB();
            ds = objServiceDB.GetECodesforService(BranchCode, DeptCode);
            UtilityLibrary.PopulateControl(cmbEmployee, ds.Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            objServiceDB = null;

        }

        private void txtServiceSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtServiceSearch.Text.ToString().Trim().Length > 1)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void txtServiceSearch_Validated(object sender, EventArgs e)
        {
            if (txtServiceSearch.Text.ToString().Trim().Length > 1)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void txtActualQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cbActivityMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbActivityMode.SelectedIndex == 0)
            //{
            //    txtMessage.Visible = true;
            //    lblMsg.Visible = true;
            //    lblLangType.Visible = true;
            //    cbLanguages.Visible = true;
            //    GetMessageFormat();
            //}
            //else
            //{
            //    txtMessage.Visible = false;
            //    lblMsg.Visible = false;
            //    lblLangType.Visible = false;
            //    cbLanguages.Visible = false;
            //    txtMessage.Text = "";
            //}
        }

        private void cbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetMessageFormat();
        }   



        private void SendSMSToCustomers()
        {

            DataSet dataSet = new DataSet();
            bool bFlag = true;
            string result = "";
            if (txtMessage.Text.Trim().Length > 10 && cbActivityMode.SelectedIndex==0)
            {
                try
                {

                   // MessageBox.Show(Convert.ToString(txtMessage.Text.Length));
                    if (SenderId.Length == 0)
                    {
                        SenderId = "SSGCHO";
                    }

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.smsjust.com/blank/sms/user/urlsms.php?username=shivashakti&pass=1234567&senderid="+ SenderId 
                        +" &dest_mobileno="+ sMobileNo + "&msgtype=UNI&message=" + txtMessage.Text + "&response=Y");

                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/getundeliveredreasonanddescription.jsp?userid=SBTLAP&password=admin@66");


                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                   
                    result = reader.ReadToEnd();

                    bFlag = result.Contains("error");

                    result = result.Replace("\r", "");
                    result = result.Replace("\t", "");
                    result = result.Replace("\n", "");                  
                    result = result.Replace(" ", "");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dataSet = null;
                }
                if (bFlag == false)
                {
                    MessageBox.Show("SMS Sent Successfully \n\n" + result, "SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("SMS Sending Error \n" + result, "SMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        
        private void GetMessageFormat()
        {
            string strMsg = "", strDate = "", sEmpName = "", sCompany = "";
            SenderId = "";

            if (cbActivityMode.SelectedIndex == 0)
            {

                if (dtpActivityDate.Value <= DateTime.Today)
                {
                    if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MM/yyyy"))).TotalDays <= 3)
                    {
                        if (cbLanguages.SelectedIndex == 0)
                        {
                            if (cmbEmployee.SelectedIndex >= -1 && txtQty.Text.Length > 0 && txtQty.Text!=".")
                            {
                                if (Convert.ToDouble(txtQty.Text) != 0)
                                {
                                    if (CommonData.CompanyCode == "NKBPL")
                                    {
                                        sCompany = "आपकी नवकिसान";
                                        SenderId = "NKBPPL";
                                    }
                                    else if (CommonData.CompanyCode == "SSBPL")
                                    {
                                        sCompany = "आपकी शिवशक्ति";
                                        SenderId = "SHVSKT";

                                    }
                                    else if (CommonData.CompanyCode == "NFL")
                                    {
                                        sCompany = "आपकी नवभारत";
                                        SenderId = "NFLVNF";
                                    }
                                    else if (CommonData.CompanyCode == "VNF")
                                    {
                                        sCompany = "आपकी विनूतना";
                                        SenderId = "NFLVNF";
                                    }
                                    else
                                    {
                                        sCompany = "आपकी शिवशक्ति";
                                        SenderId = "SSGCHO";
                                    }

                                    strDate = dtpActivityDate.Value.ToString("dd/MMM/yyyy");
                                    sEmpName = cmbEmployee.Text.Split('-', '(')[1];

                                    strMsg = "प्रिय ग्राहक नमस्कार,\r\nआपको  " + sEmpName + "  ए. ओ. द्धारा दिनांक  " + strDate + "  को   " + Convert.ToDouble(txtQty.Text).ToString("0.0") + "  यूनिट निशुल्क पौधा प्राप्त किये । जिनका पौधा रोपण एक सप्ताह के अंदर करते हुए मिटटी की नमी पर विशेष ध्यान दें । अन्य किसी जानकारी के लिए टोल फ्री नं 180030002766 पर संपर्क करें । " +
                                             "\r\n" + sCompany + "";
                                    txtMessage.Text = strMsg;
                                }
                            }
                            else
                            {
                                txtMessage.Text = "";
                            }
                        }
                        else
                        {
                            txtMessage.Text = "";
                        }
                    }
                    else
                    {
                        txtMessage.Text = "";
                    }
                }
                else
                {
                    txtMessage.Text = "";
                    MessageBox.Show("Please Select Activity Date less than or equal to Today's Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpActivityDate.Focus();
                }
            }
            else
            {
                txtMessage.Text = "";
                txtMessage.Visible = false;
                lblMsg.Visible = false;
                lblLangType.Visible = false;
                cbLanguages.Visible = false;

            }
        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetMessageFormat();
        }

        private void txtQty_KeyUp(object sender, KeyEventArgs e)
        {
                //GetMessageFormat();
            
        }

        private void dtpActivityDate_ValueChanged(object sender, EventArgs e)
        {
            //txtMessage.Visible = true;
            //lblMsg.Visible = true;
            //lblLangType.Visible = true;
            //cbLanguages.Visible = true;

            //if (dtpActivityDate.Value <= DateTime.Today)
            //{
            //    if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dtpActivityDate.Value).ToString("dd/MM/yyyy"))).TotalDays <= 3)
            //    {
            //        GetMessageFormat();
            //    }
            //    else
            //    {
            //        txtMessage.Visible = false;
            //        lblMsg.Visible = false;
            //        lblLangType.Visible = false;
            //        cbLanguages.Visible = false;
            //    }
            //}
            //else
            //{
            //    txtMessage.Visible = false;
            //    lblMsg.Visible = false;
            //    lblLangType.Visible = false;
            //    cbLanguages.Visible = false;
            //}
        }
                     
    }
}
