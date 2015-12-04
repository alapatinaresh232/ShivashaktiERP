using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using System.IO;
using SSCRM.App_Code;
using SSTrans;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;





namespace SSCRM
{
    public partial class PFUANMaster : Form
    {
        PFUANMaster obj = null;
        private Master objMstr = null;
        private DataTable dtEmpInfo = null;
        private InvoiceDB objInvoiceData = null;
        private SQLDB objDb = null;
        private HRInfo objHRInfo = null;
        private Security objSecurity = null;
        bool flagUdate = false,flagUpdate1=false;
        DataTable dt = null;
        public PFUANMaster()
        {
            InitializeComponent();
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtEcodeSearch.Text.Length > 4)
            //{
            //    FillEmployeeMasterData();
            //    FillEmpPFUANDetailData1();
               
            //}
            //else
            //{
            //    txtMemberName.Text = "";
            //    txtFatherName.Text = "";
            //    txtHRISDesig.Text = "";
            //    txtDept.Text = "";
            //    txtBranch.Text = "";
            //    txtComp.Text = "";
            //    meHRISDataofBirth.Text = "";
            //    meHRISDateOfJoin.Text = "";
            //    picEmp.BackgroundImage = null;

            //    txtUAN.Text = "";
            //    txtEmpHNo.Text = "";
            //    txtEmpLandMark.Text = "";
            //    txtEmpVill.Text = "";
            //    txtEmpMandal.Text = "";
            //    txtEmpDistricit.Text = "";
            //    txtEmpState.Text = "";
            //    txtEmpPin.Text = "";
            //    txtEmpPh.Text = "";

            //    txtDocNumber.Text = "";
            //    txtNameAsPerDoc.Text = "";
            //    txtFatherAsDoc.Text = "";
            //    txtBankAccIFSC.Text = "";
            //    picDoc.BackgroundImage = null;
            //    picDoc.Image = null;

            //}
        }

        private void FillEmpPFUANDetailData1()
        {
            objHRInfo = new HRInfo();
            DataSet ds = new DataSet();
            try
            {
                ds = objHRInfo.GetEmpPFUANDetailData1(txtEcodeSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objHRInfo = null;
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    flagUdate = true;

                    txtUAN.Text = ds.Tables[0].Rows[0]["HAUM_UAN_NO"].ToString();
                    if (ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_HNO"].ToString().Length != 0)
                    {
                        dt = ds.Tables[0];
                        txtEmpHNo.ReadOnly = true;
                        txtEmpLandMark.ReadOnly = true;
                        txtEmpVill.ReadOnly = true;
                        txtEmpMandal.ReadOnly = true;
                        txtEmpDistricit.ReadOnly = true;
                        txtEmpState.ReadOnly = true;
                        txtEmpPin.ReadOnly = true;
                        txtEmpPh.ReadOnly = true;


                        txtEmpHNo.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_HNO"].ToString();
                        txtEmpLandMark.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_LANDMARK"].ToString();
                        txtEmpVill.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_VILLAGE"].ToString();
                        txtEmpMandal.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_MANDAL"].ToString();
                        txtEmpDistricit.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_DISTRICT"].ToString();
                        txtEmpState.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_STATE"].ToString();
                        txtEmpPin.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_PIN"].ToString();
                        txtEmpPh.Text = ds.Tables[0].Rows[0]["HAUM_ADDR_PRES_PHNO"].ToString();
                    }
                    else
                    {
                        txtEmpHNo.ReadOnly = false;
                        txtEmpLandMark.ReadOnly = false;
                        txtEmpVill.ReadOnly = false;
                        txtEmpMandal.ReadOnly = false;
                        txtEmpDistricit.ReadOnly = false;
                        txtEmpState.ReadOnly = false;
                        txtEmpPin.ReadOnly = false;
                        txtEmpPh.ReadOnly = false;
                    }
                  




                   

                    if (ds.Tables[0].Rows[0]["HAUM_SIG1"].ToString() != "")
                    {
                        GetImage((byte[])ds.Tables[0].Rows[0]["HAUM_SIG1"], "SIG1");
                    }
                    if (ds.Tables[0].Rows[0]["HAUM_SIG2"].ToString() != "")
                    {
                        GetImage((byte[])ds.Tables[0].Rows[0]["HAUM_SIG2"], "SIG2");
                    }
                    if (ds.Tables[0].Rows[0]["HAUM_SIG3"].ToString() != "")
                    {
                        GetImage((byte[])ds.Tables[0].Rows[0]["HAUM_SIG3"], "SIG3");
                    }

                    FillEmpPFUANDetailData2("");
                }
                else
                {
                    //txtUAN.Text = "";
                    //txtEmpHNo.Text = "";
                    //txtEmpLandMark.Text = "";
                    //txtEmpVill.Text = "";
                    //txtEmpMandal.Text = "";
                    //txtEmpDistricit.Text = "";
                    //txtEmpState.Text = "";
                    //txtEmpPin.Text = "";
                    //txtEmpPh.Text = "";
                    txtEmpHNo.ReadOnly = false;
                    txtEmpLandMark.ReadOnly = false;
                    txtEmpVill.ReadOnly = false;
                    txtEmpMandal.ReadOnly = false;
                    txtEmpDistricit.ReadOnly = false;
                    txtEmpState.ReadOnly = false;
                    txtEmpPin.ReadOnly = false;
                    txtEmpPh.ReadOnly = false;
                }
            }
            ds = null;
            
        }
        private void FillEmpPFUANDetailData2(string sDoc)
        {
            if(txtEcodeSearch.Text.ToString().Length>0)
            {
            objHRInfo = new HRInfo();
            DataSet ds = null;
            try
            {
                ds = objHRInfo.GetEmpPFUANDetailData2(txtEcodeSearch.Text, sDoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objHRInfo = null;
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flagUpdate1 = true;

                    if (CommonData.LogUserId.ToUpper() != "ADMIN" && flagUpdate1 == true)
                        btnSave.Enabled = false;
                    else
                        btnSave.Enabled = true;

                    cbTypeOfDoc.Text = ds.Tables[0].Rows[0]["HAUMD_DOC_TYPE"].ToString();
                    txtDocNumber.Text = ds.Tables[0].Rows[0]["HAUMD_DOC_NO"].ToString();
                    txtNameAsPerDoc.Text = ds.Tables[0].Rows[0]["HAUMD_NAME_AS_DOC"].ToString();
                    txtFatherAsDoc.Text = ds.Tables[0].Rows[0]["HAUMD_FATHER_NAME"].ToString();
                    if (ds.Tables[0].Rows[0]["HAUMD_DOB"].ToString().Length>0)
                    dtpDOBDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["HAUMD_DOB"].ToString());
                    if (cbTypeOfDoc.Text == "BANK ACCOUNT NUMBER")
                        txtBankAccIFSC.Text = ds.Tables[0].Rows[0]["HAUMD_BANK_ACC_IFSC"].ToString();
                    if (cbTypeOfDoc.Text == "DRIVING LICENSE" || cbTypeOfDoc.Text == "PASSPORT")
                        dtpExpDate.Value = Convert.ToDateTime( ds.Tables[0].Rows[0]["HAUMD_LICENCE_EXP"].ToString());
                    if (ds.Tables[0].Rows[0]["HAUMD_DOC_IMG"].ToString() != "")
                    {
                        GetImage((byte[])ds.Tables[0].Rows[0]["HAUMD_DOC_IMG"], "DOC");
                    }
                }
                else
                {
                    flagUpdate1 = false;
                    btnSave.Enabled = true;

                    txtDocNumber.Text = "";
                    txtNameAsPerDoc.Text = "";
                    txtFatherAsDoc.Text = "";
                    txtBankAccIFSC.Text = "";
                    picDoc.BackgroundImage = null;
                    picDoc.Image = null;
                }
            }
            ds = null;
                }
        }

        private void FillEmployeeMasterData()
        {
            objMstr = new Master();
            DataSet ds = new DataSet();
            try
            {
                ds = objMstr.GetEmployeeMasterDetl(txtEcodeSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objMstr = null;
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtEmpInfo = ds.Tables[0];
                    txtMemberName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    txtFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    txtHRISDesig.Text = ds.Tables[0].Rows[0]["DesigName"].ToString();
                    txtDept.Text = ds.Tables[0].Rows[0]["DeptName"].ToString();
                    txtComp.Text = ds.Tables[0].Rows[0]["CompName"].ToString();
                    txtBranch.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    meHRISDataofBirth.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EmpDob"]).ToString("dd/MM/yyyy");
                    meHRISDateOfJoin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EmpDoj"]).ToString("dd/MM/yyyy");
                   

                    if (ds.Tables[0].Rows[0]["PhotoSig"].ToString() != "")
                    {
                        GetImage((byte[])ds.Tables[0].Rows[0]["PhotoSig"],"EMP");
                    }


                }
                else
                {
                    dtEmpInfo = null;
                }
            }
            ds = null;
        }

        private void GetImage(byte[] imageData,string str )
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                if (str == "EMP")
                {
                    picEmp.BackgroundImage = newImage;
                    this.picEmp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
                if (str == "DOC")
                {
                    //gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);
                    picDoc.Height = newImage.Height;
                    picDoc.Width = newImage.Width;
                    picDoc.Image = newImage;
                    //this.picDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    //double fileByte = (((picDoc.Image.Width) * (picDoc.Image.Height)) / 1024);
                    //lblFileSize.Text = "Image Size: " + fileByte.ToString() + " KB";
                }
                if(str=="SIG1")
                {
                    pbSig1.Height = newImage.Height;
                    pbSig1.Width = newImage.Width;
                    pbSig1.Image = newImage;
                }
                if (str == "SIG2")
                {
                    pbSig2.Height = newImage.Height;
                    pbSig2.Width = newImage.Width;
                    pbSig2.Image = newImage;
                }
                if (str == "SIG3")
                {
                    pbSig3.Height = newImage.Height;
                    pbSig3.Width = newImage.Width;
                    pbSig3.Image = newImage;
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbTypeOfDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTypeOfDoc.SelectedIndex > 0)
            {
                if (cbTypeOfDoc.SelectedIndex == 6)
                {
                    lblBankAcc.Visible = true;
                    txtBankAccIFSC.Visible = true;
                    dtpExpDate.Visible = false;
                    lblBankAcc.Text = "Bank Acc IFSC Code";



                }
                else if (cbTypeOfDoc.SelectedIndex == 5)
                {
                    dtpExpDate.Visible = true;
                    lblBankAcc.Visible = true;
                    txtBankAccIFSC.Visible = false;
                    lblBankAcc.Text = "Driving Lic(Exp Date)";
                }
                else if (cbTypeOfDoc.SelectedIndex == 4)
                {
                    dtpExpDate.Visible = true;
                    lblBankAcc.Visible = true;
                    txtBankAccIFSC.Visible = false;
                    lblBankAcc.Text = "Passport (Exp Date)";
                }
                else
                {
                    dtpExpDate.Visible = false;
                    lblBankAcc.Visible = false;
                    txtBankAccIFSC.Visible = false;
                }

                FillEmpPFUANDetailData2(cbTypeOfDoc.SelectedValue.ToString());
            }
        }

        private void PFUANMaster_Load(object sender, EventArgs e)
        {
            gpFirst.Visible = true;
            gpSecond.Visible = false;
            FillDocType();
          
            obj = this;
        }

        private void FillDocType()
        {
            cbTypeOfDoc.DataSource = null;
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Rows.Add("----SELECT-----", "-----SELECT-----");
            table.Rows.Add("NATIONAL POPULATION REGISTER", "N");
            table.Rows.Add("AADHAAR", "A");
            table.Rows.Add("PAN", "P");
            table.Rows.Add("PASSPORT", "T");
            table.Rows.Add("DRIVING LICENSE", "D");
            table.Rows.Add("BANK ACCOUNT NUMBER", "B");
            table.Rows.Add("ELECTION CARD", "E");
            table.Rows.Add("RATION CARD", "R");

            cbTypeOfDoc.DataSource = table;
            cbTypeOfDoc.DisplayMember = "type";
            cbTypeOfDoc.ValueMember = "name";

            //return table;
            table = null;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                gpFirst.Visible = false;
                gpSecond.Visible = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            gpFirst.Visible = true;
            gpSecond.Visible = false;
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //ImageBrowser childImage = new ImageBrowser(this);

            //childImage.ShowDialog();

            ImageBrowser img = new ImageBrowser(this,picDoc,"DOC");
            img.ShowDialog();
        }

        private void txtEmpVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtEmpVillSearch.Text.Length == 0)
                {
                    cbEmpVill.DataSource = null;
                    cbEmpVill.DataBindings.Clear();
                    cbEmpVill.Items.Clear();
                    //if (btnSave.Enabled == true)
                    ClearVillageDetails();
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtEmpVillSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                for (int i = 0; i < this.cbEmpVill.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbEmpVill.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtEmpVillSearch.Text) > -1)
                    {
                        blFind = true;
                        cbEmpVill.SelectedIndex = i;
                        txtEmpVill.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[i])).Row.ItemArray[1] + "";
                        txtEmpMandal.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[i])).Row.ItemArray[2] + "";
                        txtEmpDistricit.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[i])).Row.ItemArray[3] + "";
                        txtEmpState.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[i])).Row.ItemArray[4] + "";
                        txtEmpPin.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[i])).Row.ItemArray[5] + "";
                        break;
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
            return blFind;
        }

        private void FillAddressData(string sSearch)
        {
            Hashtable htParam = null;
            objInvoiceData = new InvoiceDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (sSearch.Trim().Length >= 0)
                    htParam = new Hashtable();
                htParam.Add("sVillage", sSearch);
                htParam.Add("sDistrict", strDist);



                htParam.Add("sCDState", CommonData.StateCode);
                dsVillage = new DataSet();
                dsVillage = objInvoiceData.GetVillageDataSet(htParam);
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {
                    txtEmpVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtEmpMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                    txtEmpDistricit.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtEmpState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtEmpPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    ClearVillageDetails();
                    FillAddressComboBox(dtVillage);
                }

                else
                {
                    htParam = new Hashtable();
                    htParam.Add("sVillage", "%" + sSearch);
                    htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = objInvoiceData.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    FillAddressComboBox(dtVillage);
                    ClearVillageDetails();
                }
                Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                Cursor.Current = Cursors.Default;

            }

        }

        private void FillAddressComboBox(DataTable dt)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));


            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});


            cbEmpVill.DataBindings.Clear();
            cbEmpVill.DataSource = dataTable;
            cbEmpVill.DisplayMember = "Panchayath";
            cbEmpVill.ValueMember = "StateID";
        }

        private void ClearVillageDetails()
        {
            txtEmpVill.Text = "";
            txtEmpMandal.Text = "";
            txtEmpDistricit.Text = "";
            txtEmpState.Text = "";
            txtEmpPin.Text = "";
        }

        private void cbEmpVill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmpVill.SelectedIndex > -1)
            {
                if (this.cbEmpVill.Items[cbEmpVill.SelectedIndex].ToString() != "")
                {
                    txtEmpVill.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[cbEmpVill.SelectedIndex])).Row.ItemArray[1] + "";
                    txtEmpMandal.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[cbEmpVill.SelectedIndex])).Row.ItemArray[2] + "";
                    txtEmpDistricit.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[cbEmpVill.SelectedIndex])).Row.ItemArray[3] + "";
                    txtEmpState.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[cbEmpVill.SelectedIndex])).Row.ItemArray[4] + "";
                    txtEmpPin.Text = ((System.Data.DataRowView)(this.cbEmpVill.Items[cbEmpVill.SelectedIndex])).Row.ItemArray[5] + "";
                }
            }
        }

        private void btnDealerVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("PFUANMaster");
            VSearch.objPFUANMaster = this;
            VSearch.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picDoc.Image != null && CheckData() && (pbSig1.Image!=null || pbSig2.Image!=null || pbSig3.Image!=null) )
            {
                if(SaveHeadData()>0)
                {
                    if(SaveDetailData()>0)
                    {
                        MessageBox.Show("Data Saved Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUdate = false;
                        flagUpdate1 = false;
                        btnCancel_Click(null, null);
                        gpFirst.Visible = true;
                        gpSecond.Visible = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Attach Document", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int SaveDetailData()
        {
            int iRes = 0, iretuval=0;
            try
            {
                string strCmd = " DELETE FROM HR_APPL_UAN_ID_PROOF WHERE HAUMD_APPL_NO='" + dtEmpInfo.Rows[0]["ApplNumber"] + 
                    "' and HAUMD_EORA_CODE=" + txtEcodeSearch.Text + " and HAUMD_DOC_TYPE='" + cbTypeOfDoc.Text + "'";
                objDb = new SQLDB();
                objDb.ExecuteSaveData(strCmd);

                Image img = picDoc.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

                strCmd = " INSERT INTO HR_APPL_UAN_ID_PROOF(HAUMD_APPL_NO,HAUMD_EORA_CODE,HAUMD_DOC_TYPE,HAUMD_DOC_NO,HAUMD_NAME_AS_DOC,HAUMD_FATHER_NAME,HAUMD_DOB";
                            if(cbTypeOfDoc.Text=="BANK ACCOUNT NUMBER")
                                strCmd += ",HAUMD_BANK_ACC_IFSC";
                            if (cbTypeOfDoc.Text == "DRIVING LICENSE")
                                strCmd += ",HAUMD_LICENCE_EXP";
                            if (cbTypeOfDoc.Text == "PASSPORT")
                                strCmd += ",HAUMD_PASSPORT_EXP";
                            strCmd += ",HAUMD_PF_DOC_NO,HAUMD_UAN_NO) VALUES(" + dtEmpInfo.Rows[0]["ApplNumber"] + "," + dtEmpInfo.Rows[0]["Ecode"] + ",'" + cbTypeOfDoc.Text +
                                "','"+txtDocNumber.Text.Replace(" ","")+"','"+txtNameAsPerDoc.Text+"','"+txtFatherAsDoc.Text+"','"+dtpDOBDate.Value.ToString("dd/MMM/yyyy")+"',";
                            if (cbTypeOfDoc.Text == "BANK ACCOUNT NUMBER")
                                strCmd+= "'"+txtBankAccIFSC.Text.Replace(" ","")+"',";
                            if (cbTypeOfDoc.Text == "DRIVING LICENSE" || cbTypeOfDoc.Text == "PASSPORT")
                                strCmd += "'" + dtpExpDate.Value.ToString("dd/MMM/yyyy") + "',)";
                            strCmd += "'" + cbTypeOfDoc.SelectedValue.ToString() + "'"+
                                    ",'" + txtUAN.Text.Replace(" ", "") + "')";




                   iRes = objDb.ExecuteSaveData(strCmd);


                   strCmd = "SELECT HAUMD_ID FROM HR_APPL_UAN_ID_PROOF WHERE HAUMD_ID=(SELECT max(HAUMD_ID) FROM HR_APPL_UAN_ID_PROOF)";
                   objDb = new SQLDB();
                   DataTable dt = objDb.ExecuteDataSet(strCmd).Tables[0];
                   int iId = Convert.ToInt32(dt.Rows[0]["HAUMD_ID"].ToString());
                  
                   string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                   objSecurity = new Security();
                   SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                   string qry = " update HR_APPL_UAN_ID_PROOF set HAUMD_DOC_IMG=@Logo where HAUMD_EORA_CODE=" + dtEmpInfo.Rows[0]["Ecode"] +
                       " and HAUMD_APPL_NO=" + dtEmpInfo.Rows[0]["ApplNumber"] + " and HAUMD_PF_DOC_NO='" + cbTypeOfDoc.SelectedValue.ToString() + "'";
                   SqlCommand SqlCom = new SqlCommand(qry, CN);
                   if (arr.Length > 1)
                       SqlCom.Parameters.Add(new SqlParameter("@Logo", (object)arr));
                   CN.Open();
                   iretuval = SqlCom.ExecuteNonQuery();
                   CN.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iretuval;
        }

        private int SaveHeadData()
        {
            int iRes = 0, iretuval = 0; byte[] arr1,arr2,arr3;

            try
            {
                string strCMD = "";
                if (flagUdate == false)
                {
                    strCMD = " INSERT INTO HR_APPL_UAN_MAS(HAUM_APPL_NO,HAUM_EORA_CODE,HAUM_UAN_NO,HAUM_ADDR_PRES_HNO,HAUM_ADDR_PRES_LANDMARK,HAUM_ADDR_PRES_VILLAGE" +
                        ",HAUM_ADDR_PRES_MANDAL,HAUM_ADDR_PRES_DISTRICT,HAUM_ADDR_PRES_STATE,HAUM_ADDR_PRES_PIN,HAUM_ADDR_PRES_PHNO,HAUM_CREATED_BY,HAUM_CREATED_DATE)" +
                        " VALUES('" + dtEmpInfo.Rows[0]["ApplNumber"] + "'," + dtEmpInfo.Rows[0]["Ecode"] + ",'" + txtUAN.Text.Replace(" ","") + "','" + txtEmpHNo.Text + "','" + txtEmpLandMark.Text +
                        "','" + txtEmpVill.Text + "','" + txtEmpMandal.Text + "','" + txtEmpDistricit.Text + "','" + txtEmpState.Text + "','" + txtEmpPin.Text +
                        "','"+txtEmpPh.Text+"','" + CommonData.LogUserId + "',GETDATE())";
                }
                else
                {
                    strCMD = " UPDATE HR_APPL_UAN_MAS set HAUM_UAN_NO='" + txtUAN.Text.Replace(" ", "") + "',HAUM_ADDR_PRES_HNO='" + txtEmpHNo.Text
                        + "',HAUM_ADDR_PRES_LANDMARK='" + txtEmpLandMark.Text + "',HAUM_ADDR_PRES_VILLAGE='" + txtEmpVill.Text + "',HAUM_ADDR_PRES_MANDAL='" + txtEmpMandal.Text +
                        "',HAUM_ADDR_PRES_DISTRICT='" + txtEmpDistricit.Text + "',HAUM_ADDR_PRES_STATE='" + txtEmpState.Text + "',HAUM_ADDR_PRES_PIN='"+txtEmpPin.Text+
                        "',HAUM_ADDR_PRES_PHNO='" + txtEmpPh.Text + "',HAUM_MODIFIED_BY='" + CommonData.LogUserId + "',HAUM_MODIFIED_DATE=GETDATE() "
                        + " WHERE HAUM_EORA_CODE='" + txtEcodeSearch.Text + "' AND HAUM_APPL_NO='" + dtEmpInfo.Rows[0]["ApplNumber"] + "' ";
                }


                objDb = new SQLDB();
                iRes = objDb.ExecuteSaveData(strCMD);



                Image img1 = null;
                img1 = pbSig1.Image;
               
                ImageConverter converter1 = new ImageConverter();
                if (dt != null)
                {
                    if (dt.Rows[0]["HAUM_SIG1"].ToString() == "")
                    {

                        arr1 = (byte[])converter1.ConvertTo(img1, typeof(byte[]));
                    }
                    else
                    {
                        arr1 = new byte[0];
                    }
                }
                else
                {
                    arr1 = (byte[])converter1.ConvertTo(img1, typeof(byte[]));
                }
                Image img2 = null;
                img2 = pbSig2.Image;
               
                ImageConverter converter2 = new ImageConverter();
                if (dt != null)
                {
                    if (dt.Rows[0]["HAUM_SIG2"].ToString() == "")
                    {

                        arr2 = (byte[])converter2.ConvertTo(img2, typeof(byte[]));
                    }
                    else
                    {
                        arr2 = new byte[0];
                    }
                }
                else
                {
                    arr2 = (byte[])converter1.ConvertTo(img2, typeof(byte[]));
                }
                Image img3 = null;
                img3 = pbSig3.Image;
               
                ImageConverter converter3 = new ImageConverter();
                if (dt != null)
                {
                    if (dt.Rows[0]["HAUM_SIG3"].ToString() == "")
                    {

                        arr3 = (byte[])converter3.ConvertTo(img3, typeof(byte[]));
                    }
                    else
                    {
                        arr3 = new byte[0];
                    }
                }
                else
                {
                    arr3 = (byte[])converter1.ConvertTo(img3, typeof(byte[]));
                }


                string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                objSecurity = new Security();
                SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                string qry = " update HR_APPL_UAN_MAS set HAUM_ADDR_PRES_STATE='" + txtEmpState.Text+"'";
                if (arr1.Length > 1)
                            qry += ", HAUM_SIG1=@Logo1 ";
                        if (arr2.Length > 1)
                            qry += " ,HAUM_SIG2=@Logo2 ";
                        if (arr3.Length > 1)
                            qry += " ,HAUM_SIG3=@Logo3 ";
                        qry += " where HAUM_EORA_CODE=" + dtEmpInfo.Rows[0]["Ecode"] +
                    " and HAUM_APPL_NO=" + dtEmpInfo.Rows[0]["ApplNumber"];
                SqlCommand SqlCom = new SqlCommand(qry, CN);
                if (arr1.Length > 1)
                    SqlCom.Parameters.Add(new SqlParameter("@Logo1", (object)arr1));
                if (arr2.Length > 1)
                    SqlCom.Parameters.Add(new SqlParameter("@Logo2", (object)arr2));
                if (arr3.Length > 1)
                    SqlCom.Parameters.Add(new SqlParameter("@Logo3", (object)arr3));
                CN.Open();
                iretuval = SqlCom.ExecuteNonQuery();
                CN.Close();










            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtMemberName.Text == "")
            {
                MessageBox.Show("Enter Valid Ecode", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEcodeSearch.Focus();
                return flag;
            }
            if(txtEmpHNo.Text.Length==0)
            {
                MessageBox.Show("Enter HNo", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpHNo.Focus();
                return flag;
            }
            if (txtEmpLandMark.Text.Length == 0)
            {
                MessageBox.Show("Enter LandMark", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpLandMark.Focus();
                return flag;
            }
            if (txtEmpVill.Text.Length == 0)
            {
                MessageBox.Show("Enter Village", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpVill.Focus();
                return flag;
            }
            if (txtEmpMandal.Text.Length == 0)
            {
                MessageBox.Show("Enter Mandal", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpMandal.Focus();
                return flag;
            }
            if (txtEmpDistricit.Text.Length == 0)
            {
                MessageBox.Show("Enter District", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpDistricit.Focus();
                return flag;
            }
            if (txtEmpState.Text.Length == 0)
            {
                MessageBox.Show("Enter State", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpState.Focus();
                return flag;
            }
            if (txtEmpPin.Text.Length == 0)
            {
                MessageBox.Show("Enter PIN No", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpPin.Focus();
                return flag;
            }

            if (txtEmpPh.Text.Length == 0)
            {
                MessageBox.Show("Enter PHNo", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtEmpPh.Focus();
                return flag;
            }

            //if(txtUAN.Text.Length==0)
            //{
            //    MessageBox.Show("Enter UAN Number", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtUAN.Focus();
            //    flag = false;
            //    return flag;
            //}
            if(cbTypeOfDoc.SelectedIndex==0)
            {
                MessageBox.Show("Select Type of Document", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                cbTypeOfDoc.Focus();
                return flag;
            }
            if (txtDocNumber.Text.Length == 0)
            {
                MessageBox.Show("Enter Document Number", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtDocNumber.Focus();
                return flag;
            }
            if (txtNameAsPerDoc.Text.Length==0)
            {
                MessageBox.Show("Enter Name As per Document", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtNameAsPerDoc.Focus();
                return flag;
            }
            if (txtFatherAsDoc.Text.Length == 0)
            {
                MessageBox.Show("Enter Father Name As per Document", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtFatherAsDoc.Focus();
                return flag;
            }
            if (txtBankAccIFSC.Text.Length == 0 && cbTypeOfDoc.SelectedIndex==6)
            {
                MessageBox.Show("Enter Bank Acc IFSC Code", "PF UAN Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
                txtBankAccIFSC.Focus();
                return flag;
            }
            
            return flag;
        }

        private void txtEmpPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUdate = false;
            flagUpdate1 = false;
            txtEcodeSearch.Text = "";
            txtMemberName.Text = "";
            txtFatherName.Text = "";
            txtHRISDesig.Text = "";
            txtDept.Text = "";
            txtBranch.Text = "";
            txtComp.Text = "";
            meHRISDataofBirth.Text = "";
            meHRISDateOfJoin.Text = "";
            picEmp.BackgroundImage = null;

            txtUAN.Text = "";
            txtEmpHNo.Text = "";
            txtEmpLandMark.Text = "";
            txtEmpVill.Text = "";
            txtEmpMandal.Text = "";
            txtEmpDistricit.Text = "";
            txtEmpState.Text = "";
            txtEmpPin.Text = "";
            txtEmpPh.Text = "";

            txtDocNumber.Text = "";
            txtNameAsPerDoc.Text = "";
            txtFatherAsDoc.Text = "";
            txtBankAccIFSC.Text = "";
            picDoc.BackgroundImage = null;
            picDoc.Image = null;
            pbSig1.Image = null;
            pbSig2.Image = null;
            pbSig3.Image = null;

        }

        private void btnSig1Browse_Click(object sender, EventArgs e)
        {
            ImageBrowser img = new ImageBrowser(this, pbSig1,"SIG");
            img.ShowDialog();
        }

        private void btnSig2Browse_Click(object sender, EventArgs e)
        {
            ImageBrowser img = new ImageBrowser(this, pbSig2,"SIG");
            img.ShowDialog();
        }

        private void btnSig3Browse_Click(object sender, EventArgs e)
        {
            ImageBrowser img = new ImageBrowser(this, pbSig3,"SIG");
            img.ShowDialog();
        }

        private void txtEmpPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtNameAsPerDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txtFatherAsDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 4)
            {
                FillEmployeeMasterData();
                FillEmpPFUANDetailData1();

            }
            else
            {
                txtMemberName.Text = "";
                txtFatherName.Text = "";
                txtHRISDesig.Text = "";
                txtDept.Text = "";
                txtBranch.Text = "";
                txtComp.Text = "";
                meHRISDataofBirth.Text = "";
                meHRISDateOfJoin.Text = "";
                picEmp.BackgroundImage = null;

                txtUAN.Text = "";
                txtEmpHNo.Text = "";
                txtEmpLandMark.Text = "";
                txtEmpVill.Text = "";
                txtEmpMandal.Text = "";
                txtEmpDistricit.Text = "";
                txtEmpState.Text = "";
                txtEmpPin.Text = "";
                txtEmpPh.Text = "";

                txtDocNumber.Text = "";
                txtNameAsPerDoc.Text = "";
                txtFatherAsDoc.Text = "";
                txtBankAccIFSC.Text = "";
                picDoc.BackgroundImage = null;
                picDoc.Image = null;

            }
        }

      





     
      


     

   
      

     
       

        

      

       

       

        

      
      

      
       

    }
}
