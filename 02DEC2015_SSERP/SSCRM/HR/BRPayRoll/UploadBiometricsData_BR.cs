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
using SSCRMDB;
using SSAdmin;
using SSTrans;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace SSCRM
{
    public partial class UploadBiometricsData_BR : Form
    {
        private SQLDB objDB = null;
        public UploadBiometricsData_BR()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
            if (!txtFolderPath.Text.ToLower().Contains("mcdatafiles"))
            {                
                MessageBox.Show("Invalid Path");
                string sPath = Get_SearchFolder("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\VirtualStore\\", "Program Files");
                sPath = Get_SearchFolder(sPath, "RS Solution");
                sPath = Get_SearchFolder(sPath, "Realsoft");
                sPath = Get_SearchFolder(sPath, "McDataFiles");
                txtFolderPath.Text = sPath;
                if (sPath.Length == 0)
                {
                    sPath = Get_SearchFolder("C:\\", "Program Files");
                    sPath = Get_SearchFolder(sPath, "RS Solution");
                    sPath = Get_SearchFolder(sPath, "Realsoft");
                    sPath = Get_SearchFolder(sPath, "McDataFiles");
                    txtFolderPath.Text = sPath;
                }
                
            }
            GetFilesInFolder(txtFolderPath.Text.ToString());
        }

        private void UploadBiometricsData_BR_Load(object sender, EventArgs e)
        {
            //marqueeProgressBarControl1.Visible = false;
            //string folder = @"C:\\Program Files\\RS Solution\\Realsoft 8.8\\McDataFiles";
            //var files = Directory.GetFiles(folder);
            //if (files != null)
            //{
            //    foreach (string file in files)
            //    {
            //        if (file.Contains("Program Files"))
            //        {
            //            folder = file;
            //        }
            //    }
            //}
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;
            string sUser = Environment.UserName;
            string sPath = Get_SearchFolder("C:\\Users\\" + sUser + "\\AppData\\Local\\VirtualStore\\", "Program Files");
            sPath = Get_SearchFolder(sPath, "RS Solution");
            sPath = Get_SearchFolder(sPath, "Realsoft");
            sPath = Get_SearchFolder(sPath, "McDataFiles");
            txtFolderPath.Text = sPath;
            if (sPath.Length == 0)
            {
                sPath = Get_SearchFolder("C:\\", "Program Files");
                sPath = Get_SearchFolder(sPath, "RS Solution");
                sPath = Get_SearchFolder(sPath, "Realsoft");
                sPath = Get_SearchFolder(sPath, "McDataFiles");
                txtFolderPath.Text = sPath;
            }
            GetFilesInFolder(sPath);
        }

        private void GetFilesInFolder(string sPath)
        {
            clbFiles.Items.Clear();
            string folder = sPath;
            if (sPath.Length > 1)
            {
                //@"C:\\Program Files\\RS Solution\\Realsoft 8.8\\McDataFiles";
                var files = Directory.GetFiles(folder, "*.dat",SearchOption.AllDirectories);
                if (files != null)
                {
                    foreach (string file in files)
                    {
                        string fname = Path.GetFileNameWithoutExtension(file);
                        if (fname.Length < 9)
                        {
                            //string sDate = fname.Substring(0, 2) + "-" + fname.Substring(2, 2) + "-" + fname.Substring(4, 2);
                            //DateTime dtPunchDate = Convert.ToDateTime(sDate);
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = Path.GetFileName(file);
                            oclBox.Text = Path.GetFileName(file);
                            clbFiles.Items.Add(oclBox);
                            oclBox = null;
                        }
                    }
                }
            }
        }

        private string Get_SearchFolder(string sPath, string sFolder)
        {
            string folder = sPath;
            try
            {
                var files = Directory.GetDirectories(folder);
                if (files != null)
                {
                    foreach (string file in files)
                    {
                        string sfile = file.ToLower();
                        if (sfile.Contains(sFolder.ToLower()))
                        {
                            folder = file;
                            return folder;
                        }
                    }
                }
            }
            catch { }
            return "";
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //marqueeProgressBarControl1.Visible = true;
            string folder = txtFolderPath.Text;
            //@"C:\\Program Files\\RS Solution\\Realsoft 8.8\\McDataFiles";
            if (folder.Length > 1)
            {
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
                                //DateTime dtPunchDate = Convert.ToDateTime(sDate);
                                //if (dtpFromDate.Value <= dtPunchDate && dtPunchDate <= dtpToDate.Value)
                                //{
                                    foreach (string word in words)
                                    {

                                        if (word.Length > 5)
                                            sqlText += " INSERT INTO HR_BR_PUNCHES_DUMP(HBPD_DUMP_STRING, HBPD_PUN_DATE, HBPD_PUN_TIME" +
                                                        ", HBPD_PUN_SOURCE, HBPD_ECODE, HBPD_COMP_CODE, HBPD_BRANCH_CODE, HBPD_CREATED_DATE) " +
                                                        "SELECT '" + word + "', '" + Convert.ToDateTime(word.Substring(0, 2) + 
                                                        "-" + word.Substring(2, 2) + "-20" + fname.Substring(4, 2)).ToString("dd-MMM-yyyy") +
                                                        "'," + Convert.ToInt32(word.Substring(4, 4)) + ", 'S', " + Convert.ToInt32(word.Substring(10, 6)) +
                                                        ", '" + CommonData.CompanyCode + "', '" + CommonData.BranchCode +
                                                        "', GETDATE() WHERE NOT EXISTS (SELECT HBPD_DUMP_STRING " +
                                                        "FROM HR_BR_PUNCHES_DUMP WHERE HBPD_DUMP_STRING = '" + word + "') ";
                                    }
                                //}
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                return;
                            }
                        }
                    }
                }
                int iRes = 0;
                try
                {
                    if (sqlText.Length > 10)
                        iRes = objDB.ExecuteSaveData(sqlText);

                    if (iRes > 0)
                    {
                        foreach (string file in files)
                        {
                            if (File.Exists(file))
                            {
                                try
                                {
                                    File.Delete(file);
                                }
                                catch { }
                            }
                        }
                        //if (File.Exists(@"C:\test.txt"))
                        //{
                        //    File.Delete(@"C:\test.txt");
                        //}
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                MessageBox.Show(iRes.ToString() + " Records Updated", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please Select Biometrics Installation Path");
            }
            //marqueeProgressBarControl1.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear,CommonData.DocMonth,dtpFromDate.Value.ToString("dd-MMM-yyyy"),dtpToDate.Value.ToString("dd-MMM-yyyy"), "WAGEATTD");
            CommonData.ViewReport = "SSERP_REP_HR_BR_ATTD_REG";
            childReportViewer.Show();
        }
    }
}
