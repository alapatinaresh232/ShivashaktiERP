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
using System.Collections;
using SSTrans;
using System.Net.Mail;
using System.Net;
using SSCRM.App_Code;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class LeagalCaseDetails : Form
    {
        ComplainantMaster childComplainantMaster = null;
        LawyerMaster childLawyerMaster = null;

        SQLDB objDb=new SQLDB();
        Master objMstr = new Master();
        InvoiceDB objData = new InvoiceDB();
        bool flagUpdate = false;
        string sApproval = "";
        string strMailBody="";
        public LeagalCaseDetails()
        {
            InitializeComponent();
        }

        private void LeagalCaseDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            //FillComplainantData("0");
            //FillLawyerData("0");
            FillForumData();
            FillCaseType();
            FillStatus();
            FillCaseNos();
           
        }

        private void FillCaseNos()
        {
            objDb = new SQLDB();
            DataTable dtAccMas = new DataTable();
            if(cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0)
            {
            dtAccMas = objDb.ExecuteDataSet(" SELECT LCM_CASE_NUMBER,LCM_CASE_NUMBER FROM LEGAL_CASE_MASTER WHERE LCM_COMPANY_CODE='"+cbCompany.SelectedValue.ToString()+
                            "' and LCM_BRANCH_CODE='"+cbBranch.SelectedValue.ToString().Split('@')[0]+"'").Tables[0];
            UtilityLibrary.AutoCompleteTextBox(txtCaseNo, dtAccMas, "LCM_CASE_NUMBER", "LCM_CASE_NUMBER");
            }

        }

        private void FillForumData()
        {
            if (dtForum().Rows.Count > 0)
            {
                cbForum.DataSource = dtForum();
                cbForum.DisplayMember = "forum";
                cbForum.ValueMember = "forum";
            }
        }

        private DataTable dtForum()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSql = "select distinct(LCM_FORUM) forum,LCM_FORUM from legal_case_master";
                objDb = new SQLDB();
                dt = objDb.ExecuteDataSet(strSql).Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "--Select--";               
                dt.Rows.InsertAt(dr, 0);

                DataRow dr1 = dt.NewRow();
                dr1[0] = "OTHER";
                dr1[1] = "OTHER";

                dt.Rows.InsertAt(dr1,dt.Rows.Count );
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }
        private void FillCaseType()
        {
            try
            {
                cbCaseType.DataSource=dtCaseTypes();
                cbCaseType.DisplayMember = "CaseType";
                cbCaseType.ValueMember = "CaseType";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private DataTable dtCaseTypes()
        {
            DataTable table = new DataTable();

            string strSql = "select distinct(LCM_CASE_TYPE) CaseType,LCM_CASE_TYPE from legal_case_master";
            objDb = new SQLDB();
            table = objDb.ExecuteDataSet(strSql).Tables[0];

            DataRow dr = table.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            table.Rows.InsertAt(dr, 0);

            //DataRow dr1 = table.NewRow();
            //dr1[0] = "CRIMINAL";
            //dr1[1] = "CRIMINAL";
            //table.Rows.InsertAt(dr1, 1);

            //DataRow dr2 = table.NewRow();
            //dr2[0] = "CIVIL";
            //dr2[1] = "CIVIL";
            //table.Rows.InsertAt(dr2, 2);

            //DataRow dr3 = table.NewRow();
            //dr3[0] = "CONSUMER";
            //dr3[1] = "CONSUMER";
            //table.Rows.InsertAt(dr3, 3);


            DataRow dr4 = table.NewRow();
            dr4[0] = "OTHER";
            dr4[1] = "OTHER";
            table.Rows.InsertAt(dr4, table.Rows.Count);
            //table.Columns.Add("type", typeof(string));
            //table.Columns.Add("name", typeof(string));
        
            //table.Rows.Add("CRIMINAL", "CRIMINAL");
            //table.Rows.Add("CIVIL", "CIVIL");
            //table.Rows.Add("CONSUMER", "CONSUMER");

            return table;
        }
        private void FillStatus()
        {
            try
            {
                cbStatus.DataSource = dtStatus();
                cbStatus.DisplayMember = "name";
                cbStatus.ValueMember = "type";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillCompanyData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS where active='t'";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

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
                objDb = null;
                dt = null;
            }
        }
         private void FillLocationData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE+'@'+STATE_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' and active='t' ORDER BY BRANCH_NAME ASC";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "branchCode";
                    //cbLocation.ValueMember = "LOCATION";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocationData();
        }
        
        private DataTable dtStatus()
        {
            DataTable table=new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Rows.Add("--SELECT--", "--SELECT--");
            table.Rows.Add("CLOSED", "CLOSED");
            table.Rows.Add("TRIAL", "TRIAL");
            table.Rows.Add("PENDING", "PENDING");
            table.Rows.Add("APPEAL PENDING", "APPEAL PENDING");
 	        return table;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        

        private void txtHandledByEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtVillageSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillageSearch.Text.Length == 0)
                {
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();
                    if (btnSave.Enabled == true)
                        ClearVillageDetails();
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtVillageSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FillAddressData(string sSearch)
        {
            Hashtable htParam = null;
            objData = new InvoiceDB();
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
                dsVillage = objData.GetVillageDataSet(htParam);
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {
                    txtVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                    txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    //strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    txtVillage.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtState.Text = "";
                    //strStateCode = "";
                    FillAddressComboBox(dtVillage);
                }

                else
                {
                    htParam = new Hashtable();
                    htParam.Add("sVillage", "%" + sSearch);
                    htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = objData.GetVillageDataSet(htParam);
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
                objData = null;
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
            cbVillage.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});


            cbVillage.DataSource = dataTable;
            cbVillage.DisplayMember = "Panchayath";
            cbVillage.ValueMember = "StateID";
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                for (int i = 0; i < this.cbVillage.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtVillageSearch.Text) > -1)
                    {
                        blFind = true;
                        cbVillage.SelectedIndex = i;
                        txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "";
                        txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[2] + "";
                        txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[3] + "";
                        txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[4] + "";
                        //strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[0] + "";
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

        private void ClearVillageDetails()
        {
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > -1)
            {
                if (this.cbVillage.Items[cbVillage.SelectedIndex].ToString() != "")
                {
                    txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    //strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("LegalCase");
            VSearch.objLegalCase = this;
            VSearch.ShowDialog();
        }

        private void txtAmtInvolved_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtComplainantSearch_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
        public void FillComplainantData(string sSearch)
        {
            if (sSearch.Length > 0)
            {
                objMstr = new Master();
                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                try
                {
                    dt = objMstr.GetComplainantMasterDetl(sSearch).Tables[0];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objMstr = null;
                }
                if (dt.Rows.Count > 0)
                {
                    // txtComplainantDetails.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    //DataTable datatable = new DataTable();
                    //datatable.Columns.Add("ComplainantId", typeof(String));
                    //datatable.Columns.Add("ComplainantName", typeof(String));
                    //datatable.Columns.Add("Address", typeof(String));
                    //cbComplainantName.DataBindings.Clear();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //    datatable.Rows.Add(new String[] { dt.Rows[i]["ComplainantId"] +"", dt.Rows[i]["ComplainantName"] + "", dt.Rows[i]["Address"] + ""});

                    //DataRow dr = dt.NewRow();
                    //dr[0] = "--Select--";
                    ////dr[1] = "--Select--";
                    //dt.Rows.InsertAt(dr, 0);


                    cbComplainantName.DataSource = dt;
                    cbComplainantName.DisplayMember = "ComplainantName";
                    cbComplainantName.ValueMember = "ComplainantId";

                    //txtComplnAddress.Text = datatable.Rows[0]["Address"]+"";

                }
                else
                {
                    //dt.Columns.Add("type", typeof(string));
                    //dt.Columns.Add("name", typeof(string));
                    //DataRow dr1 = dt.NewRow();
                    //dr1[0] = "OTHER";
                   

                    //dt.Rows.InsertAt(dr1, dt.Rows.Count);
                    //cbComplainantName.DataSource = dt;
                    txtCompName.Text = "";
                    txtComplnAddress.Text = "";
                    cbComplainantName.DataSource = null;

                }
                dt = null;
            }
        }
        private void txtLawyerSearch_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
        public void FillLawyerData(string sSearch)
        {
            if (sSearch.Length > 0)
            {
                objMstr = new Master();
                DataSet ds = new DataSet();
                try
                {
                    ds = objMstr.GetLawyerMasterDetl(sSearch);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objMstr = null;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtComplainantDetails.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    cbLawyerName.DataSource = ds.Tables[0];
                    cbLawyerName.DisplayMember = "LawyerName";
                    cbLawyerName.ValueMember = "LawyerId";
                }
                else
                {
                    txtLawyerName.Text = "";
                    txtLawyerAddress.Text = "";
                    cbLawyerName.DataSource = null;
                }
                ds = null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            sApproval = "";
            cbCompany.SelectedIndex = 0;
            txtCaseNo.Text = "";
            txtCaseDetails.Text = "";
            txtAmtInvolved.Text = "";
            txtHandledByEcode.Text = "";
            txtHandledByName.Text = "";
            txtLawyerSearch.Text = "";
            txtComplainantSearch.Text = "";
            txtVillageSearch.Text = "";
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            cbCaseType.SelectedIndex = 0;
            cbForum.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            gvProductDetails.Rows.Clear();
            btnSave.Enabled = true;
            FillForumData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                try
                {
                    string strSql = "";
                    int iSave = 0;
                    if (flagUpdate == false)
                    {
                        strSql = " insert into LEGAL_CASE_MASTER (LCM_COMPANY_CODE" +
                                                                ",LCM_STATE_CODE" +
                                                                ",LCM_BRANCH_CODE" +
                                                                ",LCM_START_DATE" +
                                                                ",LCM_CASE_TYPE" +
                                                                ",LCM_CASE_NUMBER" +
                                                                ",LCM_CASE_COMPLAINANT_ID" +
                                                                ",LCM_LAWYER_ID" +
                                                                ",LCM_FORUM" +
                                                                ",LCM_LOCATION" +
                                                                ",LCM_MANDAL" +
                                                                ",LCM_DISTRICT" +
                                                                ",LCM_STATE" +
                                                                ",LCM_AMOUNT_INVOLVED" +
                                                                ",LCM_CASE_DETAILS" +
                                                                ",LCM_CREATED_BY" +
                                                                ",LCM_CASE_HANDLEDBY" +
                                                                ",LCM_APPROVAL_FLAG" +
                                                                ",LCM_CREATED_DATE)" +
                                                                " VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                                                                "','" + cbBranch.SelectedValue.ToString().Split('@')[0] +
                                                                "','" + dtpCaseDate.Value.ToString("dd/MMM/yyyy");

                                                                if (cbCaseType.SelectedValue.ToString() == "OTHER")
                                                                   strSql += "','" + txtOtherCaseType.Text;
                                                                else
                                                                   strSql+="','" + cbCaseType.SelectedValue.ToString();


                                                                strSql += "','" + txtCaseNo.Text.Trim().Replace(" ","") +
                                                                "'," + cbComplainantName.SelectedValue.ToString() +
                                                                "," + cbLawyerName.SelectedValue.ToString();
                                                                
                                                                if(cbForum.SelectedValue.ToString()=="OTHER")
                                                                  strSql+= ",'"+txtOtherForum.Text;
                                                                else
                                                                  strSql+= ",'"+cbForum.Text.ToString();

                                                      strSql+=  "','"+txtVillage.Text+
                                                                "','"+txtMandal.Text+
                                                                "','"+txtDistrict.Text+
                                                                "','"+txtState.Text+
                                                                "','"+txtAmtInvolved.Text+
                                                                "','"+txtCaseDetails.Text+
                                                                "','"+CommonData.LogUserId+
                                                                "',"+txtHandledByEcode.Text+
                                                                ",'N'"+
                                                                ",getdate())";
                    }
                    else if (flagUpdate==true && sApproval=="N")
                    {
                        strSql = "UPDATE LEGAL_CASE_MASTER SET LCM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                               "',LCM_STATE_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                                                               "',LCM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[0];
                        if (cbCaseType.SelectedValue.ToString() == "OTHER")
                            strSql += "',LCM_CASE_TYPE='" + txtOtherCaseType.Text;
                        else
                            strSql += "',LCM_CASE_TYPE='" + cbCaseType.SelectedValue.ToString();
                                                              
                                                   strSql+=   "',LCM_CASE_COMPLAINANT_ID=" + cbComplainantName.SelectedValue.ToString() +
                                                               ",LCM_LAWYER_ID=" + cbLawyerName.SelectedValue.ToString();
                        if (cbForum.SelectedValue.ToString() == "OTHER")
                            strSql += ",LCM_FORUM='" + txtOtherForum.Text;
                        else
                            strSql += ",LCM_FORUM='" + cbForum.SelectedValue.ToString();
                                                             strSql+=  "',LCM_LOCATION='"+txtVillage.Text+
                                                               "',LCM_MANDAL='"+txtMandal.Text+
                                                               "',LCM_DISTRICT='"+txtDistrict.Text+
                                                               "',LCM_STATE='"+txtState.Text+
                                                               "',LCM_AMOUNT_INVOLVED='"+txtAmtInvolved.Text+
                                                               "',LCM_CASE_DETAILS='"+ txtCaseDetails.Text+
                                                               "',LCM_CASE_HANDLEDBY=" + txtHandledByEcode.Text +
                                                               ",LCM_LAST_MODIFIED_BY='"+CommonData.LogUserId+
                                                               "',LCM_LAST_MODIFIED_DATE=GETDATE()"+
                                                               " WHERE LCM_CASE_NUMBER ='"+txtCaseNo.Text+
                                                               "' AND LCM_CASE_TYPE='"+cbCaseType.SelectedValue.ToString()+
                                                               "' AND LCM_COMPANY_CODE='"+cbCompany.SelectedValue.ToString()+
                                                               "' AND LCM_BRANCH_CODE='"+cbBranch.SelectedValue.ToString().Split('@')[0]+"'";  
                    }
                    else if (flagUpdate == true && sApproval == "A")
                    {
                        MessageBox.Show("Data Cannot be Changed ", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    objDb = new SQLDB();
                    iSave = objDb.ExecuteSaveData(strSql);
                    if(iSave>0)
                    {
                       
                        MessageBox.Show("Data Saved Successfully","SSCRM",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        FillCaseNos();
                        string strSQL = "SELECT LCM_COMPANY_CODE,cm_company_name,BRANCH_NAME,LCM_CASE_TYPE,LCM_CASE_NUMBER,LCM_START_DATE,LCM_FORUM,LCM_COMPLAINANT_NAME,LLM_LAWYER_NAME,LCM_CASE_DETAILS,LCM_AMOUNT_INVOLVED" +
                                        ",LCM_LOCATION+','+LCM_MANDAL+','+LCM_DISTRICT+','+LCM_STATE AS ADDRESS" +
                                        ",LCM_CASE_HANDLEDBY" +
                                        ",em1.MEMBER_NAME CreatedBy,lm.LCM_LAST_MODIFIED_BY ModifiedBy" +
                                        ",em.MEMBER_NAME HandledBy " +
                                         " FROM LEGAL_CASE_MASTER lm" +
                                         " INNER JOIN EORA_MASTER as em1 ON em1.ECODE=lm.LCM_CREATED_BY "+
                                         " INNER JOIN COMPANY_MAS ON cm_company_code=LCM_COMPANY_CODE" +
                                         " INNER JOIN BRANCH_MAS bm ON bm.BRANCH_CODE=LCM_BRANCH_CODE" +
                                         " INNER JOIN LEGAL_COMPLAINANT_MASTER ON lcm_complainant_id=lcm_case_complainant_id" +
                                         " INNER JOIN LEGAL_LAWYER_MASTER ON llm_lawyer_id=lcm_lawyer_id" +
                                         " INNER JOIN EORA_MASTER em ON em.ECODE = lm.lcm_case_handledby" +
                                         " WHERE LCM_CASE_NUMBER='" + txtCaseNo.Text;
                        if (cbCaseType.SelectedValue.ToString() == "OTHER")
                            strSQL += "' AND LCM_CASE_TYPE='" + txtOtherCaseType.Text;
                        else
                            strSQL += "' AND LCM_CASE_TYPE='" + cbCaseType.SelectedValue.ToString();
                        strSQL += "' AND LCM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                         "' AND LCM_BRANCH_CODE='"+cbBranch.SelectedValue.ToString().Split('@')[0]+"'";
                        objDb = new SQLDB();
                        DataTable dt = objDb.ExecuteDataSet(strSQL).Tables[0];
                        strMailBody = BuildingMailToLegal(dt);
                       string str =  SendMail(dt);

                        FillForumData();
                        FillCaseType();
                        FillComplainantData("0");
                        FillLawyerData("0");
                        btnClear_Click(null,null);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSCRM",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                cbCompany.Focus();
                return flag;
            }
            if (cbBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbBranch.Focus();
                return flag;
            }
            if (cbCaseType.SelectedValue.ToString() == "OTHER")
            {
                flag = false;
                MessageBox.Show("Please Select CaseType", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCaseType.Focus();
                return flag;
            }
            else if (cbCaseType.SelectedValue.ToString() == "--Select--")
            {
                flag = false;
                MessageBox.Show("Please Select CaseType", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return flag;
            }
            //else
            //{

            //}
            if (cbComplainantName.Items.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Correct Complainant Name", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtComplainantSearch.Focus();
                return flag;
            }
            if(cbLawyerName.Items.Count==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Correct Lawyer Name", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLawyerSearch.Focus();
                return flag;
            }
            if(txtCaseNo.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter CaseNo", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCaseNo.Focus();
                return flag;
            }
            if(txtVillage.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Village", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVillageSearch.Focus();
                return flag;
            }
            if(txtAmtInvolved.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Amount Involved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmtInvolved.Focus();
                return flag;
            }
            if (txtCaseDetails.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter CaseDetails", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCaseDetails.Focus();
                return flag;
            }
            if (txtHandledByName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Handled/Responsible By Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHandledByEcode.Focus();
                return flag;
            }
            if (cbForum.SelectedValue.ToString() == "OTHER")
            {
                if (txtOtherForum.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter FORUM ", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOtherForum.Focus();
                    return flag;
                }
            }
            else if (cbForum.SelectedValue.ToString() == "--Select--")
            {
                flag = false;
                MessageBox.Show("Please Select Forum", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return flag;
            }
            return flag;
        }

        private void cbForum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbForum.SelectedValue.ToString() == "OTHER")
            {
                txtOtherForum.Visible = true;
            }
            else
            {
                txtOtherForum.Visible = false;
            }
        }

        public void txtCaseNo_KeyUp(object sender, KeyEventArgs e)
        {
           if(txtCaseNo.Text.Length>0)
           {
               LegalInfoDB objLegal = new LegalInfoDB();              
               DataTable dt = new DataTable();
               DataTable dtDtl = new DataTable();
               if(cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0 && cbCaseType.SelectedIndex>0)
               {
               try
               {
                   dt = objLegal.getLegalCaseDetails(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0], cbCaseType.SelectedValue.ToString(), txtCaseNo.Text.Trim());
                   dtDtl = objLegal.getCaseHearingDetailsMaster(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0], cbCaseType.SelectedValue.ToString(), "01-JAN-1900", txtCaseNo.Text.Trim());


                   FillCaseDetails(dt,dtDtl);
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
               finally
               {
                   dt = null;
               }
               }
           }
        }

        private void FillCaseDetails( DataTable dtHead,DataTable dtDtl)
        {
            try
            {
                DataTable dt = dtHead;
                DataTable dtHearingDtl = dtDtl;
                if (dt.Rows.Count > 0)
                {
                    //FillComplainantData("0");
                    flagUpdate = true;
                    cbCaseType.SelectedValue = dt.Rows[0]["LCM_CASE_TYPE"] + "";
                    cbCompany.SelectedValue = dt.Rows[0]["LCM_COMPANY_CODE"] + "";
                    cbBranch.SelectedValue = dt.Rows[0]["LCM_BRANCH_CODE"] + "@" + dt.Rows[0]["LCM_STATE_CODE"];
                    dtpCaseDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["LCM_START_DATE"]).ToString("dd/MM/yyyy"));
                    cbComplainantName.SelectedValue = dt.Rows[0]["LCM_CASE_COMPLAINANT_ID"] + "";
                    cbLawyerName.SelectedValue = dt.Rows[0]["LCM_LAWYER_ID"] + "";
                    txtComplainantSearch.Text = dt.Rows[0]["LCM_COMPLAINANT_NAME"] + "";
                    txtLawyerSearch.Text = dt.Rows[0]["LLM_LAWYER_NAME"] + "";
                    txtCaseDetails.Text = dt.Rows[0]["LCM_CASE_DETAILS"] + "";
                    cbStatus.SelectedValue = dt.Rows[0]["LCM_STATUS"] + "";
                    cbForum.SelectedValue = dt.Rows[0]["LCM_FORUM"] + "";
                    txtAmtInvolved.Text = dt.Rows[0]["LCM_AMOUNT_INVOLVED"] + "";
                    txtHandledByEcode.Text = dt.Rows[0]["LCM_CASE_HANDLEDBY"] + "";
                    txtVillage.Text = dt.Rows[0]["LCM_LOCATION"] + "";
                    txtMandal.Text = dt.Rows[0]["LCM_MANDAL"] + "";
                    txtDistrict.Text = dt.Rows[0]["LCM_DISTRICT"] + "";
                    txtState.Text = dt.Rows[0]["LCM_STATE"] + "";
                    sApproval = dt.Rows[0]["LCM_APPROVAL_FLAG"] + "";
                    btnSave.Enabled = false;
                }
                else
                {
                    flagUpdate = false;
                    txtCaseDetails.Text = "";
                    txtAmtInvolved.Text = "";
                    txtHandledByEcode.Text = "";
                    txtHandledByName.Text = "";
                    txtLawyerSearch.Text = "";
                    txtComplainantSearch.Text = "";
                    txtVillageSearch.Text = "";
                    txtVillage.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtState.Text = "";
                    btnSave.Enabled = true;
                }
                FillHearingDetail(dtHearingDtl);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillHearingDetail(DataTable dtHearingDtl)
        {
            if (dtHearingDtl.Rows.Count > 0)
            {
                gvProductDetails.Rows.Clear();
                int count = 0;
                string strTempDate = "",strAttendName="";
                strTempDate = dtHearingDtl.Rows[0]["LCH_DATE"].ToString();
                for (int iRow = 0; iRow < dtHearingDtl.Rows.Count;iRow++ )
                {
                    if(dtHearingDtl.Rows[iRow]["LCH_ATTENDED_SL_NO"].ToString()!="0")
                    {
                        if (dtHearingDtl.Rows.Count != (iRow + 1))
                        {
                            if (strTempDate != dtHearingDtl.Rows[iRow + 1]["LCH_DATE"].ToString())
                            {
                                strTempDate = dtHearingDtl.Rows[iRow + 1]["LCH_DATE"].ToString();
                                strAttendName += getEmpLawyerName(dtHearingDtl.Rows[iRow]["LCH_ATTENDED_BY"].ToString(), dtHearingDtl.Rows[iRow]["LCH_ATTENDED_TYPE"].ToString()) + ",";

                                gvProductDetails.Rows.Add();
                                gvProductDetails.Rows[count].Cells["SLNO"].Value = (count + 1);
                                gvProductDetails.Rows[count].Cells["Date"].Value = Convert.ToDateTime(dtHearingDtl.Rows[iRow]["LCH_DATE"].ToString()).ToShortDateString();
                                gvProductDetails.Rows[count].Cells["Type"].Value = dtHearingDtl.Rows[iRow]["LCH_CASE_TYPE"].ToString();
                                gvProductDetails.Rows[count].Cells["AttendedBy"].Value = strAttendName;
                                gvProductDetails.Rows[count].Cells["Remarks"].Value = dtHearingDtl.Rows[iRow]["LCH_REMARKS"].ToString();
                                gvProductDetails.Rows[count].Cells["NextHDate"].Value = Convert.ToDateTime(dtHearingDtl.Rows[iRow]["LCH_NEXT_DATE"].ToString()).ToShortDateString();
                                gvProductDetails.Rows[count].Cells["Status"].Value = dtHearingDtl.Rows[iRow]["LCH_STATUS"].ToString();
                                gvProductDetails.Rows[count].Cells["TotalAmount"].Value = (Convert.ToDouble(dtHearingDtl.Rows[iRow]["LCH_LAWYER_FEES"])+Convert.ToDouble(dtHearingDtl.Rows[iRow]["LCH_STAMP_FEES"])+Convert.ToDouble(dtHearingDtl.Rows[iRow]["LCH_MISCELLANEOUS_AMT"])).ToString();

                                count++;
                                strAttendName = "";
                            }
                            else
                            {
                                //strTempDate = dtHearingDtl.Rows[iRow]["LCH_DATE"].ToString();
                                strAttendName += getEmpLawyerName(dtHearingDtl.Rows[iRow]["LCH_ATTENDED_BY"].ToString(), dtHearingDtl.Rows[iRow]["LCH_ATTENDED_TYPE"].ToString()) + ",";
                            }
                        }
                        else
                        {
                            strAttendName += getEmpLawyerName(dtHearingDtl.Rows[iRow]["LCH_ATTENDED_BY"].ToString(), dtHearingDtl.Rows[iRow]["LCH_ATTENDED_TYPE"].ToString()) + ",";
                            gvProductDetails.Rows.Add();
                            gvProductDetails.Rows[count].Cells["SLNO"].Value = (count + 1);
                            gvProductDetails.Rows[count].Cells["Date"].Value = Convert.ToDateTime(dtHearingDtl.Rows[iRow]["LCH_DATE"].ToString()).ToShortDateString();
                            gvProductDetails.Rows[count].Cells["Type"].Value = dtHearingDtl.Rows[iRow]["LCH_CASE_TYPE"].ToString();
                            gvProductDetails.Rows[count].Cells["AttendedBy"].Value = strAttendName;
                            gvProductDetails.Rows[count].Cells["Remarks"].Value = dtHearingDtl.Rows[iRow]["LCH_REMARKS"].ToString();
                            gvProductDetails.Rows[count].Cells["NextHDate"].Value = Convert.ToDateTime(dtHearingDtl.Rows[iRow]["LCH_NEXT_DATE"].ToString()).ToShortDateString();
                            gvProductDetails.Rows[count].Cells["Status"].Value = dtHearingDtl.Rows[iRow]["LCH_STATUS"].ToString();
                            gvProductDetails.Rows[count].Cells["TotalAmount"].Value = (Convert.ToDouble(dtHearingDtl.Rows[iRow]["LCH_LAWYER_FEES"]) + Convert.ToDouble(dtHearingDtl.Rows[iRow]["LCH_STAMP_FEES"]) + Convert.ToDouble(dtHearingDtl.Rows[iRow]["LCH_MISCELLANEOUS_AMT"])).ToString();
                        }
                        
                    }
                }
                cbStatus.SelectedValue = dtHearingDtl.Rows[0]["LCH_STATUS"].ToString();
                
            }
            else
            {
                gvProductDetails.Rows.Clear();
            }
        }

      

        private void txtHandledByEcode_TextChanged(object sender, EventArgs e)
        {
            if (txtHandledByEcode.Text.Length > 4)
            {
                txtHandledByName.Text = getEmpName(txtHandledByEcode.Text);
            }
            else
            {
                txtHandledByName.Text = "";
            }
        }

        private void dtpCaseDate_ValueChanged(object sender, EventArgs e)
        {
           dtpDocMonth.Value =  dtpCaseDate.Value;
        }
        private string getEmpName(string Ecode)
        {
            objMstr = new Master();
            DataSet ds = new DataSet();
            string Name="";
            try
            {
                ds = objMstr.GetEmployeeMasterDetl(Ecode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objMstr = null;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                Name= ds.Tables[0].Rows[0]["EmpName"].ToString();
            }
            else
            {
                Name= "";
            }
            ds = null;

            return Name;
        }
        private string getEmpLawyerName(string Ecode,string type)
        {
             string Name = "";
            if (type == "EMPLOYEE")
            {
                objMstr = new Master();
                DataSet ds = new DataSet();
               
                try
                {
                    ds = objMstr.GetEmployeeMasterDetl(Ecode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objMstr = null;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Name = ds.Tables[0].Rows[0]["EmpName"].ToString();
                }
                else
                {
                    Name = "";
                }
                ds = null;
                return Name;
            }
            else
            {
                string strSQL = "SELECT LLM_LAWYER_NAME FROM LEGAL_LAWYER_MASTER WHERE LLM_LAWYER_ID="+Ecode;
                objDb =new SQLDB();
                 Name= objDb.ExecuteDataSet(strSQL).Tables[0].Rows[0]["LLM_LAWYER_NAME"].ToString();
                 return Name;
            }
           
        }
        public string SendMail(DataTable DT)
        {
            try
            {
                string strSQL = "SELECT ECODE,HECD_EMP_EMAIL_ID FROM EORA_MASTER INNER JOIN HR_EMP_CONTACT_DETL ON ECODE=HECD_EORA_CODE WHERE DEPT_ID=1300000 AND DESG_ID=314";
                objDb = new SQLDB();
                DataTable dtContact = objDb.ExecuteDataSet(strSQL).Tables[0];
                String[] addrCC = { "legal@sivashakthi.net" };
                //String[] addrCC = { dtContact.Rows[0]["HECD_EMP_EMAIL_ID"].ToString() };
            //String[] addrCC = { "bharath_yrpt@yahoo.com" };
            MailAddress fromAddress = new MailAddress("ssbplitho@gmail.com", "SSERP-Legal :: CaseType-" + DT.Rows[0]["LCM_CASE_TYPE"] + " CaseNumber-" + DT.Rows[0]["LCM_CASE_NUMBER"]);
            MailAddress toAddress = new MailAddress("bharath_yrpt@yahoo.com");
            const string fromPassword = "ssbplitho5566";
            
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("ssbplitho@gmail.com", "ssbplitho5566")
                };
                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "SSERP-Legal :: CaseType-" + DT.Rows[0]["LCM_CASE_TYPE"] + " CaseNumber-" + DT.Rows[0]["LCM_CASE_NUMBER"] + ":",
                    Body = "</br></br></br>" + strMailBody + "</br></br> This is server generated mail please do not replay to this mail.",
                    IsBodyHtml = true
                }) 
                {
                    for (int i = 0; i < addrCC.Length; i++)
                        message.CC.Add(addrCC[i]);
                message.To.Add("bharath_yrpt@yahoo.com");
                message.Bcc.Add("nareshit@sivashakthi.net");
              
                    smtp.Send(message);
                    return "Yes";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        private string BuildingMailToLegal(DataTable dt)
        {
            string Mailbody = "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";
            Mailbody += "<tr><td colspan=\"8\"><a href=\"www.shivashakthigroup.com\">" +
                "<img src=\"http://shivashakthigroup.com/wp-content/uploads/2013/01/logo.png\" alt=\"Shivashakthi Group of Companies\"/></a></td></tr>";
            Mailbody += "<tr style =\"background-color:#6FA1D2; color:#ffffff;\"><td>Sl.No</td><td>Details</td><td>Description</td></tr>";
            int j = 1;
            Mailbody += "<tr><td>1</td><td><b>Company</b></td><td>" + dt.Rows[0]["cm_company_name"] + "</td></tr>" +
                        "<tr><td>2</td><td><b>Branch</b></td><td>" + dt.Rows[0]["BRANCH_NAME"] + "</td></tr>" +
                        "<tr><td>3</td><td><b>CaseType</b></td><td>" + dt.Rows[0]["LCM_CASE_TYPE"] + "</td></tr>" +
                        "<tr><td>4</td><td><b>Case No</b></td><td>" + dt.Rows[0]["LCM_CASE_NUMBER"] + "</td></tr>" +
                        "<tr><td>5</td><td><b>StartDate</b></td><td>" + Convert.ToDateTime(dt.Rows[0]["LCM_START_DATE"].ToString()).ToShortDateString() + "</td></tr>" +
                        "<tr><td>6</td><td><b>Forum</b></td><td>" + dt.Rows[0]["LCM_FORUM"] + "</td></tr>" +
                        "<tr><td>7</td><td><b>ComplainantName</b></td><td>" + dt.Rows[0]["LCM_COMPLAINANT_NAME"] + "</td></tr>" +
                        "<tr><td>8</td><td><b>LawyerName</b></td><td>" + dt.Rows[0]["LLM_LAWYER_NAME"] + "</td></tr>" +
                        "<tr><td>9</td><td><b>CaseDetails</b></td><td>" + dt.Rows[0]["LCM_CASE_DETAILS"] + "</td></tr>" +
                        "<tr><td>10</td><td><b>Address</b></td><td>" + dt.Rows[0]["ADDRESS"] + "</td></tr>" +
                        "<tr><td>11</td><td><b>AmountInvolved</b></td><td>" + dt.Rows[0]["LCM_AMOUNT_INVOLVED"] + "</td></tr>" +
                        "<tr><td>12</td><td><b>HandledBy</b></td><td>" + dt.Rows[0]["HandledBy"] + "</td></tr>";
            if (dt.Rows[0]["ModifiedBy"].ToString().Length == 0)
                Mailbody += "<tr><td>13</td><td><b>CreatedBy</b></td><td>" + dt.Rows[0]["CreatedBy"] + "</td></tr>";
            else
                Mailbody += "<tr><td>13</td><td><b>ModifiedBy</b></td><td>" + dt.Rows[0]["ModifiedBy"] + "</td></tr>";
            Mailbody += "</table>";
            return Mailbody;
        }
        

        private void cbCaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCaseType.SelectedValue.ToString() == "OTHER")
            {
                txtOtherCaseType.Visible = true;
            }
            else
            {
                txtOtherCaseType.Visible = false;
            }
        }

        private void cbComplainantName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComplainantName.SelectedIndex > -1)
            {
                if (this.cbComplainantName.Items[cbComplainantName.SelectedIndex].ToString() != "")
                {
                    //txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    //txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    //txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    //txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    //txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    //strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                    txtComplnAddress.Text = ((System.Data.DataRowView)(this.cbComplainantName.Items[cbComplainantName.SelectedIndex])).Row.ItemArray[2] + "";
                    txtComplnAddress.Tag = cbComplainantName.SelectedValue.ToString();
                    txtCompName.Text = ((System.Data.DataRowView)(this.cbComplainantName.Items[cbComplainantName.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }
        }

        private void btnAddComplainant_Click(object sender, EventArgs e)
        {
            childComplainantMaster = new ComplainantMaster(this,txtComplainantSearch.Text);
           
            childComplainantMaster.Show();
        }

        private void btnAddLawyer_Click(object sender, EventArgs e)
        {
            childLawyerMaster = new LawyerMaster(this,txtLawyerSearch.Text);
            
            childLawyerMaster.Show();
        }

        private void cbLawyerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLawyerName.SelectedIndex > -1)
            {
                if (this.cbLawyerName.Items[cbLawyerName.SelectedIndex].ToString() != "")
                {
                    //txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    //txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    //txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    //txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    //txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    //strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                    txtLawyerAddress.Text = ((System.Data.DataRowView)(this.cbLawyerName.Items[cbLawyerName.SelectedIndex])).Row.ItemArray[2] + "";
                    txtLawyerAddress.Tag = cbLawyerName.SelectedValue.ToString();
                    txtLawyerName.Text = ((System.Data.DataRowView)(this.cbLawyerName.Items[cbLawyerName.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }
            else
            {
                txtLawyerAddress.Text = "";
                txtLawyerName.Text = "";
            }
        }

        private void btnHearingDetls_Click(object sender, EventArgs e)
        {

            CaseHearings childCaseHearings = new CaseHearings(cbCompany.SelectedValue.ToString(),cbBranch.SelectedValue.ToString().Split('@')[0],cbCaseType.SelectedValue.ToString()
                                , txtCaseNo.Text.Trim(), cbCompany.Text, cbBranch.Text, cbBranch.SelectedValue.ToString().Split('@')[1],this);
            childCaseHearings.Show();
           
        }

        private void txtComplainantSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtComplainantSearch.Text.Length > 0)
                FillComplainantData(txtComplainantSearch.Text);
            //else
            //    FillComplainantData("0");
            else
            {
                txtCompName.Text = "";
                txtComplnAddress.Text = "";
                cbComplainantName.DataSource = null;
            }
        }

        private void txtLawyerSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtLawyerSearch.Text.Length > 0)
                FillLawyerData(txtLawyerSearch.Text);
            else
            {
                txtLawyerName.Text = "";
                txtLawyerAddress.Text = "";
                cbLawyerName.DataSource = null;
            }
            //else
            //    FillLawyerData("0");
        }

        private void txtCaseNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar!='/')
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendingMailToLegal();
            //string strSQL = "SELECT LCM_COMPANY_CODE,cm_company_name,BRANCH_NAME,LCM_CASE_TYPE,LCM_CASE_NUMBER,LCM_START_DATE,LCM_FORUM,LCM_COMPLAINANT_NAME,LLM_LAWYER_NAME,LCM_CASE_DETAILS,LCM_AMOUNT_INVOLVED" +
            //                           ",LCM_LOCATION+','+LCM_MANDAL+','+LCM_DISTRICT+','+LCM_STATE AS ADDRESS" +
            //                           ",LCM_CASE_HANDLEDBY" +
            //                           ",em1.MEMBER_NAME CreatedBy,lm.LCM_LAST_MODIFIED_BY ModifiedBy" +
            //                           ",em.MEMBER_NAME HandledBy " +
            //                            " FROM LEGAL_CASE_MASTER lm" +
            //                            " INNER JOIN EORA_MASTER as em1 ON em1.ECODE=lm.LCM_CREATED_BY " +
            //                            " INNER JOIN COMPANY_MAS ON cm_company_code=LCM_COMPANY_CODE" +
            //                            " INNER JOIN BRANCH_MAS bm ON bm.BRANCH_CODE=LCM_BRANCH_CODE" +
            //                            " INNER JOIN LEGAL_COMPLAINANT_MASTER ON lcm_complainant_id=lcm_case_complainant_id" +
            //                            " INNER JOIN LEGAL_LAWYER_MASTER ON llm_lawyer_id=lcm_lawyer_id" +
            //                            " INNER JOIN EORA_MASTER em ON em.ECODE = lm.lcm_case_handledby" +
            //                            " WHERE LCM_CASE_NUMBER='" + txtCaseNo.Text;
            //if (cbCaseType.SelectedValue.ToString() == "OTHER")
            //    strSQL += "' AND LCM_CASE_TYPE='" + txtOtherCaseType.Text;
            //else
            //    strSQL += "' AND LCM_CASE_TYPE='" + cbCaseType.SelectedValue.ToString();
            //strSQL += "' AND LCM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
            //                 "' AND LCM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString().Split('@')[0] + "'";
            //objDb = new SQLDB();
            //DataTable dt = objDb.ExecuteDataSet(strSQL).Tables[0];
            //strMailBody = BuildingMailToLegal(dt);
            //string str = SendMail(dt);
        }

        private void SendingMailToLegal()
        {





            //string sql = " SELECT LCH_COMPANY_CODE,LCH_STATE_CODE,LCH_BRANCH_CODE,LCH_CASE_TYPE,LCH_CASE_NUMBER" +
            //                  " ,MAX(LCH_NEXT_DATE) NEXT_DATE_OF_HEARING,LCM_CASE_DETAILS,CAST (LCM_CASE_HANDLEDBY AS NVARCHAR)+'-'+MEMBER_NAME Handled_By" +
            //                  " ,CM_COMPANY_NAME,BRANCH_NAME,LCM_COMPLAINANT_NAME,LLM_LAWYER_NAME,LCM_FORUM,sm_state" +
            //                  " FROM LEGAL_CASE_HEARINGS" +
            //                  " INNER JOIN LEGAL_CASE_MASTER ON LEGAL_CASE_MASTER.LCM_COMPANY_CODE=LCH_COMPANY_CODE AND LEGAL_CASE_MASTER.LCM_STATE_CODE=LCH_STATE_CODE" +
            //                  " AND LEGAL_CASE_MASTER.LCM_BRANCH_CODE=LCH_BRANCH_CODE AND LEGAL_CASE_MASTER.LCM_CASE_TYPE=LCH_CASE_TYPE AND" +
            //                  " LEGAL_CASE_MASTER.LCM_CASE_NUMBER=LCH_CASE_NUMBER" +

            //                  " INNER JOIN LEGAL_COMPLAINANT_MASTER ON LEGAL_COMPLAINANT_MASTER.LCM_COMPLAINANT_ID=LEGAL_CASE_MASTER.LCM_CASE_COMPLAINANT_ID "+
            //                  " INNER JOIN LEGAL_LAWYER_MASTER ON LEGAL_LAWYER_MASTER.LLM_LAWYER_ID=LEGAL_CASE_MASTER.LCM_LAWYER_ID "+
            //                  " INNER JOIN state_mas ON LCH_STATE_CODE=state_mas.sm_state_code"+
            //                  " INNER JOIN COMPANY_MAS ON LEGAL_CASE_HEARINGS.LCH_COMPANY_CODE=COMPANY_MAS.CM_COMPANY_CODE " +
            //                  " INNER JOIN BRANCH_MAS ON LEGAL_CASE_HEARINGS.LCH_BRANCH_CODE=BRANCH_MAS.BRANCH_CODE" +
            //                  " INNER JOIN EORA_MASTER ON ECODE=LCM_CASE_HANDLEDBY" +
            //                   " WHERE  DATEDIFF(dd, GETDATE(), LCH_NEXT_DATE)<=30 " +
            //                  " AND DATEDIFF(dd, GETDATE(), LCH_NEXT_DATE)> 0" +
            //                   " AND LCH_ATTENDED_FLAG='y'" +
            //                  " AND LCH_STATUS <> 'CLOSED' " +
            //                  " GROUP BY LCH_COMPANY_CODE,LCH_STATE_CODE,LCH_BRANCH_CODE,LCH_CASE_TYPE,LCH_CASE_NUMBER,"+
            //                    "LCM_CASE_DETAILS,CM_COMPANY_NAME,BRANCH_NAME,LCM_CASE_HANDLEDBY,MEMBER_NAME,LCM_COMPLAINANT_NAME,LLM_LAWYER_NAME,LCM_FORUM,sm_state ";
            //objDb = new SQLDB();
            //DataTable dt = objDb.ExecuteDataSet(sql).Tables[0];
            objDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objDb.CreateParameter("@xCompanyCode", DbType.String,CommonData.CompanyCode , ParameterDirection.Input);
                param[1] = objDb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objDb.CreateParameter("@xStateCode", DbType.String, "", ParameterDirection.Input);
                param[3] = objDb.CreateParameter("@HandledBy", DbType.String, "", ParameterDirection.Input);
                ds = objDb.ExecuteDataSet("SSERP_LEGAL_CASE_HEARINGS_REMAINDER_MAIL", CommandType.StoredProcedure, param);

                DataTable dt = ds.Tables[0];
            string Mailbody = "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";
            Mailbody += "<tr><td colspan=\"12\"><a href=\"www.shivashakthigroup.com\">" +
                "<img src=\"http://shivashakthigroup.com/wp-content/uploads/2013/01/logo.png\" alt=\"Shivashakthi Group of Companies\"/></a></td></tr>";
            Mailbody += "<tr style =\"background-color:#6FA1D2; color:#ffffff;\"><td>Sl.No</td><td>Company</td><td>State</td><td>Branch</td><td>CaseType</td>" +
                            " <td>CaseNo</td><td>Forum</td><td>ComplainantName</td><td>LawyerName</td><td>CaseDetails</td><td>NextDateOfHearing</td>"+
                            " <td>HandledBy</td></tr>";
            int j = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Mailbody += "<tr><td>"+(i+1)+"</td><td>" + dt.Rows[i]["cm_company_name"] + "</td>" +
                            "<td>" + dt.Rows[i]["sm_state"] + "</td>" +
                            "<td>" + dt.Rows[i]["BRANCH_NAME"] + "</td>" +
                            "<td>" + dt.Rows[i]["LCH_CASE_TYPE"] + "</td>" +
                            "<td>" + dt.Rows[i]["LCH_CASE_NUMBER"] + "</td>" +
                            "<td>" + dt.Rows[i]["LCM_FORUM"] + "</td>" +
                            "<td>" + dt.Rows[i]["LCM_COMPLAINANT_NAME"] + "</td>" +
                            "<td>" + dt.Rows[i]["LLM_LAWYER_NAME"] + "</td>" +
                            "<td>" + dt.Rows[i]["LCM_CASE_DETAILS"] + "</td>" +
                            "<td>" + Convert.ToDateTime( dt.Rows[i]["NEXT_DATE_OF_HEARING"].ToString()).ToString("dd/MMM/yyyy") + "</td>" +
                            "<td>" + dt.Rows[i]["Handled_By"] + "</td></tr>";
            }          
            
            Mailbody += "</table>";


            String[] addrCC = { "nageshg@sivashakthi.net"};
                //String[] addrCC = { dtContact.Rows[0]["HECD_EMP_EMAIL_ID"].ToString() };
            //String[] addrCC = { "bharath_yrpt@yahoo.com" };
            MailAddress fromAddress = new MailAddress("ssbplitho@gmail.com", "SSERP-LEGAL ");
            MailAddress toAddress = new MailAddress("legal@sivashakthi.net");
            const string fromPassword = "ssbplitho5566";
            
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("ssbplitho@gmail.com", "ssbplitho5566")
                };
                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "SSERP Remainder :: "+DateTime.Now.ToString("dd/MMM/yyyy"),
                    Body = "</br></br></br>" + Mailbody + "</br></br> This is server generated mail please do not replay to this mail.",
                    IsBodyHtml = true
                }) 
                {
                    for (int i = 0; i < addrCC.Length; i++)
                        message.CC.Add(addrCC[i]);
              //  message.To.Add("legal@sivashakthi.net");
                message.CC.Add("md@sivashakthi.net");
                message.CC.Add("vijayssgc@gmail.com");
                message.Bcc.Add("nareshit@sivashakthi.net");
                message.Bcc.Add("satyaprasad@sivashakthi.net");
                message.Bcc.Add("bharathit@sivashakthi.net");
                if (dt.Rows.Count > 0)
                {
                    smtp.Send(message);
                }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.ToString());
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCaseNos();
        }
    }
}
