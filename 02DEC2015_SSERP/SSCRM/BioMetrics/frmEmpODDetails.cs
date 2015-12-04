using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SSCRMDB;
using SSAdmin;

namespace SSCRM
{
    public partial class frmEmpODDetails : Form
    {
        SQLDB objSQLdb = null;
        public frmEmpODBranchList objEmpODBranchList = null;
        bool flagUpdate= false;
        string strBranchCode = "";
      public  string strBranchName = "";
        string[] arrBranch;  

        public frmEmpODDetails()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

              string strCmd = "SELECT DISTINCT CM.CM_COMPANY_NAME,CM.CM_COMPANY_CODE  " +
                                  " FROM ICLOCK_DEVICES_LIST IK " +
                                  " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = IK.BRANCH_CODE " +
                                  " INNER JOIN COMPANY_MAS CM ON CM.CM_COMPANY_CODE = BM.COMPANY_CODE  ";


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

        private void FillLoctionTypeData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "select DISTINCT BRANCH_TYPE ,CASE WHEN BRANCH_TYPE='BR' THEN 'BRANCH' " +
                                  " WHEN BRANCH_TYPE='SP' THEN 'STOCK POINT' " +
                                  " WHEN BRANCH_TYPE='PU' THEN 'PRODUCTION UNIT' " +
                                  " WHEN BRANCH_TYPE='HO' THEN 'HEAD OFFICE' " +
                                  " WHEN BRANCH_TYPE='TR' THEN 'TRANSPORT UNIT' END AS DisplayMember from BRANCH_MAS BM  " +
                                  " INNER JOIN ICLOCK_DEVICES_LIST IK  ON IK.BRANCH_CODE = BM.BRANCH_CODE where BM.COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbLocationType.DataSource = dt;
                    cbLocationType.DisplayMember = "DisplayMember";
                    cbLocationType.ValueMember = "BRANCH_TYPE";
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

        private void FillLocationData()
        {

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
             
            try
            {
                string strCmd = "select DISTINCT BM.BRANCH_CODE+'@'+ICLOCK_DEVICE_SLNO as strBranchCode ,BRANCH_NAME as strBranchName from BRANCH_MAS BM " +
                                  " INNER JOIN ICLOCK_DEVICES_LIST IK ON IK.BRANCH_CODE = BM.BRANCH_CODE "+
                                  " where BM.BRANCH_TYPE = '" + cbLocationType.SelectedValue.ToString() +
                                  "' AND BM.COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' ORDER BY BM.BRANCH_NAME ";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                clbLocation.Items.Clear();

                foreach (DataRow dataRow in dt.Rows)
                {
                   
                        if (dataRow["strBranchCode"] + "" != "")
                        {
                            NewCheckboxListItem ClbBox = new NewCheckboxListItem();
                            ClbBox.Tag = dataRow["strBranchCode"].ToString();
                            ClbBox.Text = dataRow["strBranchName"].ToString();
                            clbLocation.Items.Add(ClbBox);
                            ClbBox = null;
                        }
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

        private void frmEmpODDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();
           
        }

        private void cbLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocationType.SelectedIndex > 0)
            {
                FillLocationData();
            }
           
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillLoctionTypeData();
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();


                 if (CheckData() == true)
                 {

                     if (SaveHead() > 0)
                     {
                         MessageBox.Show("Data Saved Successfully", "frmEmpODDetails", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         this.Close();
                         objEmpODBranchList.FillEmpODGridDetails();
                     }
                     //else
                     //{
                     //    MessageBox.Show("Data Not Saved ", "frmEmpODDetails", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     //    cbCompany.SelectedText = "";
                     //}
                 }
             
        }
        private int SaveHead()
        {
            objSQLdb = new SQLDB();
            int iresult = 0;
            string strcmd = "";

            try
            {

                for (int i = 0; i < clbLocation.Items.Count; i++)
                {
                    if (clbLocation.GetItemCheckState(i) == CheckState.Checked)
                    {
                        arrBranch = null;
                        arrBranch = ((SSAdmin.NewCheckboxListItem)(clbLocation.Items[i])).Tag.ToString().Split('@');
                        strBranchCode = arrBranch[0];
                        strBranchName = arrBranch[1];

                        strcmd += " INSERT INTO HR_EMP_OD_BRANCH_LIST(HOBL_APPL_NO " +
                                                               ", HOBL_EORA_CODE " +
                                                               ", HOBL_OD_COMP_CODE " +
                                                               ", HOBL_OD_BRANCH_CODE " +
                                                               ", HOBL_ICLOCK_SLNO " +
                                                               ", HOBL_CREATED_BY " +
                                                               ", HOBL_CREATED_DATE" +
                                                               ")SELECT * from(SELECT'" + ((frmEmpODBranchList)objEmpODBranchList).txtName.Tag.ToString() +
                                                               "' appl,'" + ((frmEmpODBranchList)objEmpODBranchList).txtEcode.Text.ToString() +
                                                               "'ecode,'" + cbCompany.SelectedValue.ToString() +
                                                               "'comp,'" + strBranchCode +
                                                               "'branchcode,'" + strBranchName +
                                                               "'devsino,'" + CommonData.LogUserId + "' users,getDate() date)a  where appl not in " +
                                                               " (select HOBL_APPL_NO from HR_EMP_OD_BRANCH_LIST WHERE HOBL_APPL_NO='" + ((frmEmpODBranchList)objEmpODBranchList).txtName.Tag.ToString() +
                                                               "' and HOBL_OD_BRANCH_CODE='" + strBranchCode +"' )";
                        strcmd += " EXEC Amsbd_BioTransfer_InsDel_OD " + ((frmEmpODBranchList)objEmpODBranchList).txtEcode.Text.ToString() +
                                              ",'" + strBranchName + "' ,'INSERT'";
                    }
                }

                if (strcmd.Length > 10)
                {
                    iresult = objSQLdb.ExecuteSaveData(strcmd);
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iresult;


        }


        private bool CheckData()
        {
            bool bFlag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "frmEmpODDetails", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return bFlag;
            }

            if (cbLocationType.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select LocationType", "frmEmpODDetails", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLocationType.Focus();
                return bFlag;
            }
            bool blSource = false;
            for (int i = 0; i < clbLocation.Items.Count; i++)
            {
                if (clbLocation.GetItemCheckState(i) == CheckState.Checked)
                {
                    blSource = true;
                }
            }

            if (clbLocation.Items.Count == 0 || blSource== false)
            {
                bFlag = false;
                MessageBox.Show("Please Select Location", "frmEmpODDetails", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clbLocation.Focus();
                return bFlag;
            }

          return bFlag;
        }

        private void chkLocAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocAll.Checked == true)
            {
                for (int i = 0; i < clbLocation.Items.Count;i++ )
                {
                    clbLocation.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                for (int i = 0; i < clbLocation.Items.Count;i++ )
                {
                    clbLocation.SetItemCheckState(i,CheckState.Unchecked);
                }
            }
        }
        
       
    }
}
