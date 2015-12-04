using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Security;
using System.Collections;
using System.Net;
using System.Xml;
using System.IO;
using SSTrans;
using SSCRMDB;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace SSCRM
{
    public partial class SendSMS : Form
    {
        private SQLDB objDB = null;
        public SendSMS()
        {
            InitializeComponent();
        }

        private void SendSMS_Load(object sender, EventArgs e)
        {
            cbSenderID.SelectedIndex = 0;
            GetSMSCredits();
            //GetSentSMSList();
        }

        private void GetSentSMSList()
        {
            objDB = new SQLDB();
            string sRes = "";
            string[] sCredits = null;
            try
            {
                DateTime dtDate = DateTime.Now;
                //while (dtDate < DateTime.Now)
                //{
                dtDate = dtDate.AddDays(-5);
                    DateTime dtTodate = dtDate.AddDays(6);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org//getDLR.jsp?userid=SBTLAP&password=admin@66&fromdate=" + dtDate.ToString("yyyy-MM-dd hh:mm:ss") +
                                                "&todate=" + dtTodate.ToString("yyyy-MM-dd hh:mm:ss") + "&redownload=yes&responce%20type=xml");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    sRes = reader.ReadToEnd();
                    sRes = sRes.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
                    sRes = sRes.Replace("responsecode", "").Replace("dlristcount", "").Replace("resposedescription", "").Replace("pendingdrcount", "");
                    sRes = sRes.Replace("response", "").Replace("drlist", "").Replace("undeliveredreason", "").Replace("messageid", "").Replace("externalid", "").Replace("senderid", "");
                    sRes = sRes.Replace("mobileno", "").Replace("message", "").Replace("submittime", "").Replace("senttime", "").Replace("deliverytime", "");
                    sRes = sRes.Replace("status", "").Replace("details", "").Replace("<![CDATA[", "").Replace("]]>", "");
                    sRes = sRes.Replace("</dr>", "").Replace("<dr>", "^");
                    sRes = sRes.Replace("<>", "").Replace("</> ", "~").Replace("</>", "~");
                    sCredits = sRes.Split('^');

                    foreach (string sMessage in sCredits)
                    {
                        if (sMessage.Length > 30)
                        {
                            string[] smsMessage = sMessage.Split('~');
                            try
                            {
                                string sqlText = " INSERT INTO SEND_SMS_INDIA (SSI_MESSAGE_ID, SSI_EXTARNAL_ID, SSI_SENDER_ID," +
                                                    "SSI_MOBILE_NO, SSI_MESSAGE_TEXT, SSI_SUBMIT_TIME, SSI_SENT_TIME, SSI_DELIVERY_TIME," +
                                                    "SSI_STATUS, SSI_UNDELIVERED_REASON, SSI_DETAILS)" +
                                                    "SELECT " + smsMessage[0] + "," + smsMessage[1] + ",'" + smsMessage[2] + "'," + smsMessage[3].Replace("+91", "0") +
                                                    ",'" + smsMessage[4].Replace("'", "") + "','" + smsMessage[5] + "','" + smsMessage[6] + "','" + smsMessage[7] +
                                                    "','" + smsMessage[8] + "','" + smsMessage[9] + "','" + smsMessage[10] + "' WHERE NOT EXISTS " +
                                                    "(SELECT SSI_MESSAGE_ID FROM SEND_SMS_INDIA WHERE SSI_MESSAGE_ID=" + smsMessage[0] + ")";
                                sqlText += " UPDATE SEND_SMS_INDIA SET SSI_DELIVERY_TIME='" + smsMessage[7] +
                                                    "',SSI_STATUS='" + smsMessage[8] +
                                                    "',SSI_UNDELIVERED_REASON='" + smsMessage[9] +
                                                    "',SSI_DETAILS='" + smsMessage[10] +
                                                    "' WHERE SSI_MESSAGE_ID=" + smsMessage[0];
                                objDB.ExecuteSaveData(sqlText);

                            }
                            catch
                            {

                            }

                        }
                    }
                    //dtDate = dtDate.AddDays(5);
                //}
                //sRes = sRes.Replace("", "");
                //sRes = sRes.Replace(" ", "");
            }
            catch
            {
                
            }
        }

        private void GetSMSCredits()
        {
            string sRes = "";
            string[] sCredits = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/smscredit.jsp?user=SBTLAP&password=admin@66");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                sRes = reader.ReadToEnd();
                sRes = sRes.Replace("sms", "").Replace("accountexpdate", "").Replace("balanceexpdate", "").Replace("credit", "");
                sRes = sRes.Replace("<>", "").Replace("</>", ",");
                sRes = sRes.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
                sRes = sRes.Replace(" ","");
                sCredits = sRes.Split(',');
                string sCredit = "", sValidFrom = "", sExpTo = "";
                sCredit = sCredits[2]; sValidFrom = sCredits[0]; sExpTo = sCredits[1];
                txtCredits.Text = sCredit;
                txtValidFrom.Text = Convert.ToDateTime(sValidFrom).ToString("dd-MMM-yyyy");
                txtValidTo.Text = Convert.ToDateTime(sExpTo).ToString("dd-MMM-yyyy");

                
            }
            catch
            {
                txtCredits.Text = "0";
                txtValidFrom.Text = "";
                txtValidTo.Text = "";
            }
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (CheckData())
            {
                DataSet dataSet = new DataSet();
                bool bFlag = true;
                string result = "";
                try
                {
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://shiva.bulksmshyderabad.co.in/api/smsapi.aspx?" +
                    //    "username=shivashakti&password=shiva123&to=" + txtMobileNo.Text +
                    //    "&from=" + cbSenderID.SelectedItem.ToString() + "&message=" + txtMessage.Text + "");

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.smsjust.com/blank/sms/user/urlsms.php?username=shivashakti&pass=1234567&senderid=" + cbSenderID.SelectedItem.ToString() + "&dest_mobileno="
                                            + txtMobileNo.Text + "&msgtype=UNI&message=" + txtMessage.Text + "&response=Y");

                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/getundeliveredreasonanddescription.jsp?userid=SBTLAP&password=admin@66");



                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();                    
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    DataTable dataTable = new DataTable();
                    //dataSet.ReadXml(response.GetResponseStream());
                    //dataTable.ReadXml(reader);
                    result = reader.ReadToEnd();
                    //dataSet.ReadXml(response.GetResponseStream());
                    //dataSet.ReadXmlSchema(reader);                    
                    //reader.Close();
                    
                    bFlag = result.Contains("error");
                    
                    //MessageBox.Show(result);
                    //System.IO.StreamReader xmlStream = new System.IO.StreamReader("schema.xsd");
                    //DataSet dataSet = new DataSet();

                    result = result.Replace("\r", "");
                    result = result.Replace("\t", "");
                    result = result.Replace("\n", "");
                    //result = result.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>", "");
                    result = result.Replace(" ", "");
                    //System.IO.StringReader xmlSR = new System.IO.StringReader(result);
                    ////DataSet dataSet = new DataSet();
                    ////DataTable dataTable = new DataTable();
                    //result = result.Replace("\r\n", "");
                    //result = result.Replace("\t", "");
                    //result = result.Replace("\n", "");
                    //dataTable.Columns.Add("col1", typeof(string));
                    //dataTable.Columns.Add("col2", typeof(string));
                    //dataSet.Tables.Add(dataTable);
                    //dataTable.ReadXml(result);
                    
                    //dataSet.ReadXml(result);
                    
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
            GetSMSCredits();
        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (txtMobileNo.Text.Length < 10)
            {
                MessageBox.Show("Invalid Mobile No", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMessage.Text.Length == 0)
            {
                MessageBox.Show("Enter Message", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cbSenderID.SelectedIndex == 0)
            {
                MessageBox.Show("Select Sender ID", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return bFlag;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbSenderID.SelectedIndex = 0;
            txtMessage.Text = "";
            txtMobileNo.Text = "";
            GetSMSCredits();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folder = @"C:\\Program Files\\RS Solution\\Realsoft 8.8\\McDataFiles";
            var files = Directory.GetFiles(folder);
            NameValueCollection nvc = new NameValueCollection();
            string sqlText = "";
            objDB = new SQLDB();
            if (files != null)
            {
                foreach (string file in files)
                {
                    string fname = Path.GetFileNameWithoutExtension(file);
                    if (fname.Length < 9)
                    {
                        string temp = File.ReadAllText(file);
                        string[] words = Regex.Split(temp, @"\r\n");
                        try
                        {
                            string sDate = fname.Substring(0, 2) + "-" + fname.Substring(2, 2) + "-" + fname.Substring(4, 2);
                            DateTime dtPunchDate = Convert.ToDateTime(sDate);
                            foreach (string word in words)
                            {

                                if (word.Length > 5)
                                    sqlText += "INSERT INTO HR_BR_PUNCHES_DUMP(HBPD_DUMP_STRING, HBPD_PUN_DATE, HBPD_PUN_TIME" +
                                                ", HBPD_PUN_SOURCE, HBPD_ECODE, HBPD_COMP_CODE, HBPD_BRANCH_CODE) " +
                                                "SELECT '" + word + "', '" + Convert.ToDateTime(dtPunchDate).ToString("dd-MMM-yyyy") +
                                                "', " + word.Substring(4, 4) + ", 'S', " + Convert.ToInt32(word.Substring(10, 6)) +
                                                ", " + CommonData.CompanyCode + ", " + CommonData.BranchCode + 
                                                " WHERE NOT EXISTS (SELECT HBPD_DUMP_STRING " +
                                                "FROM HR_BR_PUNCHES_DUMP WHERE HBPD_DUMP_STRING = '" + word + "') ";
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            int iRes = 0;
            if (sqlText.Length > 10)
                iRes = objDB.ExecuteSaveData(sqlText);

            MessageBox.Show(iRes.ToString() + " Records Updated", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
