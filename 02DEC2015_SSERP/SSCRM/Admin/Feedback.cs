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

namespace SSCRM
{
    public partial class FeedBack : Form
    {
        private UserTaskDB objUserTask = null;
        private SQLDB objData = null;
        private UtilityDB objUtil = null;
        string strBCode = string.Empty;
        bool blFormLoad = true;
        public FeedBack()
        {
            InitializeComponent();
        }

        private void FeedBack_Load(object sender, EventArgs e)
        {
            cbFeedBackType.SelectedIndex = 0;
            txtFeedBack.CharacterCasing = CharacterCasing.Upper;
        }
        private void FillFeedBackList()
        {
            objUserTask = new UserTaskDB();
            DataTable dt=null;
            try
            {
        
                    this.gvFeedBack.Rows.Clear();


                    dt = objUserTask.UserFeedBack_Get(cbFeedBackType.Text).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                        {
                            gvFeedBack.Rows.Add();
                            gvFeedBack.Rows[intRow].Cells["SLNO"].Value = intRow+1;
                            gvFeedBack.Rows[intRow].Cells["FeedBackNo"].Value = dt.Rows[intRow]["FBNO"].ToString();
                            gvFeedBack.Rows[intRow].Cells["FeedBackType"].Value = dt.Rows[intRow]["FBTYPE"].ToString();
                            gvFeedBack.Rows[intRow].Cells["EnterDate"].Value = dt.Rows[intRow]["EntryDate"].ToString();
                            gvFeedBack.Rows[intRow].Cells["FeedBackGV"].Value = dt.Rows[intRow]["FeedBack"].ToString();
                            
                        }
                    }
                    

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Feed Back", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objUserTask = null;
            }
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvFeedBack_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                txtFeedBack.Text = gvFeedBack.Rows[gvFeedBack.CurrentCell.RowIndex].Cells["FeedBackGV"].Value.ToString();
                
            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objUtil = new UtilityDB();
            string strFBNo = string.Empty;
            try
            {
                if (CheckData())
                {

                    objUserTask = new UserTaskDB();
                    strFBNo = objUserTask.GenerateFeedBackTranNo().ToString();
                    strSQl = " INSERT into CRM_FEEDBACK (CF_COMPANY_CODE , CF_STATE_CODE, CF_BRANCH_CODE, CF_FIN_YEAR,CF_TRN_TYPE,CF_TRN_NUMBER,CF_TRN_DATE,CF_TRN_REM,CF_CREATED_BY,CF_CREATED_DATE)" +
                                 " VALUES('" + CommonData.CompanyCode +
                                 "', '" + CommonData.StateCode +
                                 "', '" + CommonData.BranchCode +
                                 "', '" + CommonData.FinancialYear +
                                 "', '" + cbFeedBackType.Text +
                                 "', '" + strFBNo +
                                 "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                 "', '" + txtFeedBack.Text +
                                 "', '" + CommonData.LogUserId +
                                 "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                 "' )";
                    int rec = objData.ExecuteSaveData(strSQl);
                    if(rec>0)
                        objUtil.SendMail(CommonData.LogUserEcode+"-"+CommonData.LogUserName, CommonData.BranchName, txtFeedBack.Text);
                    txtFeedBack.Text = "";
                    FillFeedBackList();
                  }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Feed back", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            
       
            if (txtFeedBack.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter feed back ", "Feed back", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtFeedBack.Focus();
                return blValue;
            }

            DialogResult result = MessageBox.Show("You cannot modify once data is saved , Do you want to save ?",
                                           "Feed Back", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                blValue = false;

            return blValue;
        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtFeedBack.Text = string.Empty;
            FillFeedBackList();
        }

        private void cbFeedBackType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFeedBackList();
        }

        private void txtFeedBack_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
