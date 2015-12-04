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
    public partial class FixedAssetMaster : Form
    {
        SQLDB objSQLdb = null;
        bool flagUpdate = false;

        public FixedAssetMaster()
        {
            InitializeComponent();
        }

        private void FixedAssetMaster_Load(object sender, EventArgs e)
        {
            txtDsearch.CharacterCasing = CharacterCasing.Upper;           

            FillAssetType();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chkAssetMake.Visible = true;
                chkAssetType.Visible = true;
            }
            else
            {
                chkAssetMake.Visible = false;
                chkAssetType.Visible = false;
            }

        }

        private void FillAssetType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                string strCmd = "SELECT DISTINCT(FAM_ASSET_TYPE) FROM FIXED_ASSETS_MAS ORDER BY FAM_ASSET_TYPE ASC";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                DataRow row = dt.NewRow();
                row[0] = "--Select--";
                dt.Rows.InsertAt(row, 0);

                if (dt.Rows.Count > 1)
                {                    
                    cbAssetType.DataSource = dt;
                    cbAssetType.DisplayMember = "FAM_ASSET_TYPE";
                    cbAssetType.ValueMember = "FAM_ASSET_TYPE";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLdb = null;
            }
        }

        //private void GenerateTrnNo()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string strCmd = "SELECT ISNULL(MAX(FAM_ID),0)+1  FROM FIXED_ASSETS_MAS";
        //        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            //txtTrnNo.Text=dt.Rows[0][0].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }
        //}
        private bool CheckData()
        {
            bool flag = true;
            if (chkAssetType.Checked == false)
            {
                if (cbAssetType.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Asset Type", "Fixed Asset Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbAssetType.Focus();
                }
            }
            else if (chkAssetMake.Checked == false)
            {
                if (cbAssetMake.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Asset Maker", "Fixed Asset Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbAssetMake.Focus();

                }
            }

            else if (chkAssetType.Checked == true && chkAssetMake.Checked == false)
            {
                if (txtAssetType.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Asset Type", "Fixed Asset Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAssetType.Focus();
                }
            }
            else if (chkAssetMake.Checked == true)
            {
                if (txtAssetMake.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Asset Maker", "Fixed Asset Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAssetMake.Focus();
                    return flag;
                }
            }
            else if (txtDsearch.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Asset Model", "Fixed Asset Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDsearch.Focus();
            }
            return flag;


        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";

            int iRec = 0;
            DataTable dt = new DataTable();
            if (chkAssetType.Checked == true && chkAssetMake.Checked == true)
            {

                if (txtAssetType.Text != "" && txtAssetMake.Text != "" && txtDsearch.Text != "")
                {
                    strCommand = "SELECT * FROM FIXED_ASSETS_MAS " +
                                " WHERE  FAM_ASSET_TYPE ='" + txtAssetType.Text +
                                 "' AND FAM_ASSET_MAKE='" + txtAssetMake.Text +
                                 "' AND FAM_ASSET_MODEL='" + txtDsearch.Text + "' ";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    strCommand = "";
                    if (dt.Rows.Count == 0)
                    {
                        try
                        {
                            strCommand = "INSERT INTO FIXED_ASSETS_MAS(FAM_ASSET_TYPE " +
                                             ", FAM_ASSET_MAKE " +
                                             ", FAM_ASSET_MODEL " +
                                             ", FAM_CREATED_BY " +
                                             ", FAM_CREATED_DATE " +
                                             ")VALUES " +
                                             "('" + txtAssetType.Text.ToString() +
                                             "','" + txtAssetMake.Text.ToString() +
                                             "','" + txtDsearch.Text.ToString() +
                                             "','" + CommonData.LogUserId +
                                             "','" + CommonData.CurrentDate + "')";

                            iRec = objSQLdb.ExecuteSaveData(strCommand);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        if (iRec > 0)
                        {
                            MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);                          
                            txtAssetMake.Text = "";
                            txtAssetType.Text = "";
                            txtDsearch.Text = "";
                            chkAssetType.Checked = false;
                            cbAssetType.Visible = true;
                            txtAssetType.Visible = false;
                            FillAssetType();
                            lstMappedAssets.DataSource = null;

                        }
                    }

                    else
                    {
                        MessageBox.Show("Asset Type Already Exists!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }

                }
            }
            else if (txtAssetMake.Text.Length > 0 && txtDsearch.Text.Length > 0)
            {
                strCommand = "SELECT * FROM FIXED_ASSETS_MAS " +
                           " WHERE  FAM_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() +
                           "' AND FAM_ASSET_MAKE ='" + txtAssetMake.Text + "' ";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                strCommand = "";


                if (dt.Rows.Count == 0)
                {
                    try
                    {
                        strCommand = "INSERT INTO FIXED_ASSETS_MAS(FAM_ASSET_TYPE " +
                                         ", FAM_ASSET_MAKE " +
                                         ", FAM_ASSET_MODEL " +
                                         ", FAM_CREATED_BY " +
                                         ", FAM_CREATED_DATE " +
                                         ")VALUES " +
                                         "('" + cbAssetType.SelectedValue.ToString() +
                                         "','" + txtAssetMake.Text.ToString() +
                                         "','" + txtDsearch.Text.ToString() +
                                         "','" + CommonData.LogUserId +
                                         "','" + CommonData.CurrentDate + "')";

                        iRec = objSQLdb.ExecuteSaveData(strCommand);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (iRec > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbAssetType.SelectedIndex = 0;
                        txtAssetMake.Text = "";
                        txtDsearch.Text = "";
                        FillDataToList();
                    }
                }
                else
                {

                    MessageBox.Show("Asset Make Already Exists!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }

            else if (chkAssetType.Checked == false && chkAssetMake.Checked == false)
            {
                if (txtDsearch.Text.Length > 0)
                {
                    strCommand = "SELECT * FROM FIXED_ASSETS_MAS " +
                              " WHERE  FAM_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() +
                              "' AND FAM_ASSET_MAKE ='" + cbAssetMake.SelectedValue.ToString() +
                              "' AND FAM_ASSET_MODEL LIKE '%" + txtDsearch.Text + "%'; ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    strCommand = "";

                    if (dt.Rows.Count == 0)
                    {

                        try
                        {

                            strCommand = "INSERT INTO FIXED_ASSETS_MAS(FAM_ASSET_TYPE " +
                                             ", FAM_ASSET_MAKE " +
                                             ", FAM_ASSET_MODEL " +
                                             ", FAM_CREATED_BY " +
                                             ", FAM_CREATED_DATE " +
                                             ")VALUES " +
                                             "('" + cbAssetType.SelectedValue.ToString() +
                                             "','" + cbAssetMake.SelectedValue.ToString() +
                                             "','" + txtDsearch.Text.ToString() +
                                             "','" + CommonData.LogUserId +
                                             "','" + CommonData.CurrentDate + "')";

                            iRec = objSQLdb.ExecuteSaveData(strCommand);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        if (iRec > 0)
                        {
                            MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //cbAssetType.SelectedIndex = 0;
                            //cbAssetMake.SelectedIndex = -1;
                            txtDsearch.Text = "";
                            cbAssetMake_SelectedIndexChanged(null, null);

                        }

                    }

                    else
                    {
                        MessageBox.Show("Asset Model Already Exists!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            
        }
           
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }



        private void FillAssetTypeToList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            if (chkAssetType.Checked == true)
            {
                lstMappedAssets.DataSource = null;
                lstMappedAssets.DataBindings.Clear();

                try
                {
                    string strCmd = "SELECT DISTINCT(FAM_ASSET_TYPE) FROM FIXED_ASSETS_MAS ";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        lstMappedAssets.DataSource = dt;
                        lstMappedAssets.DisplayMember = "FAM_ASSET_TYPE";
                        lstMappedAssets.ValueMember = "FAM_ASSET_TYPE";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    objSQLdb = null;
                }
            }


        }

        private void FillDataToList()
        {
            if (chkAssetMake.Checked == true)
            {
                if (cbAssetType.SelectedIndex > 0)
                {
                    cbAssetMake.DataSource = null;
                    cbAssetMake.DataBindings.Clear();                 
                    

                    lstMappedAssets.DataSource = null;
                    lstMappedAssets.DataBindings.Clear();
                    lstMappedAssets.Items.Clear();
                }
                objSQLdb = new SQLDB();
                string strCmd = "";
                try
                {
                    if (cbAssetType.SelectedIndex > 0)
                    {
                        strCmd = "SELECT DISTINCT(FAM_ASSET_MAKE) FROM FIXED_ASSETS_MAS " +
                                  " WHERE FAM_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() + "'";
                    }
                    else
                    {
                        strCmd = "SELECT DISTINCT(FAM_ASSET_MAKE) FROM FIXED_ASSETS_MAS ";
                    }

                    DataTable dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                   

                    if (dt.Rows.Count > 0)
                    {
                        lstMappedAssets.DataSource = dt;
                        lstMappedAssets.DisplayMember = "FAM_ASSET_MAKE";
                        lstMappedAssets.ValueMember = "FAM_ASSET_MAKE";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objSQLdb = null;
                }
            }
        }

        private void cbAssetMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssetMake.SelectedIndex > 0)
            {
                lstMappedAssets.DataSource = null;
                lstMappedAssets.DataBindings.Clear();
                lstMappedAssets.Items.Clear();

                objSQLdb = new SQLDB();
                try
                {
                    string strCmd = "SELECT DISTINCT(FAM_ASSET_MODEL) FROM FIXED_ASSETS_MAS " +
                                    " WHERE FAM_ASSET_TYPE='"+ cbAssetType.SelectedValue.ToString() +
                                    "' AND FAM_ASSET_MAKE='"+ cbAssetMake.SelectedValue.ToString() +"'";

                    DataTable dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    lstMappedAssets.Items.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        lstMappedAssets.DataSource = dt;
                        lstMappedAssets.DisplayMember = "FAM_ASSET_MODEL";
                        lstMappedAssets.ValueMember = "FAM_ASSET_MODEL";
                    }
                    //txtDsearch.Enabled = true;
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objSQLdb = null;
                }
            }
           
        }

        private void chkAssetType_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkAssetType.Checked == true)
            {
                chkAssetMake.Checked = true;
                txtAssetType.Visible = true;
                cbAssetType.Visible = false;
                txtAssetMake.Visible = true;
                
                lstMappedAssets.DataSource = null;
                FillAssetTypeToList();
                txtDsearch.Enabled = true;
                //txtDsearch.ReadOnly = false;
            }
            else
            {
                cbAssetType.Visible = true;
                cbAssetMake.Visible = true;                
                txtAssetType.Visible = false;
                chkAssetMake.Checked = false;                
                FillAssetType();
                cbAssetMake.DataSource = null;
                lstMappedAssets.DataSource = null;

                //chkAssetType.Visible = true;
                txtDsearch.Text = "";
            }

        }


        private void chkAssetMake_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAssetMake.Checked == true)
            {
                txtAssetMake.Visible = true;
                cbAssetMake .Visible = false;
                lstMappedAssets.DataSource = null;
                FillDataToList();
                //txtDsearch.Enabled = true;
                //txtDsearch.ReadOnly = false;
            }
            else
            {

                txtAssetMake.Visible = false;
                cbAssetMake.Visible = true;
                lstMappedAssets.DataSource = null;
                //chkAssetMake.Visible = true;
                //txtDsearch.Enabled = false;
            }
            cbAssetType_SelectedIndexChanged(null,null);
        }

        private void txtAssetType_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbAssetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssetType.SelectedIndex > 0)
            {
                cbAssetMake.DataSource = null;
                cbAssetMake.DataBindings.Clear();
                cbAssetMake.Items.Clear();


                lstMappedAssets.DataSource = null;
                lstMappedAssets.DataBindings.Clear();
                lstMappedAssets.Items.Clear();

                objSQLdb = new SQLDB();
                try
                {
                    string strCmd = "SELECT DISTINCT(FAM_ASSET_MAKE) FROM FIXED_ASSETS_MAS " +
                                    " WHERE FAM_ASSET_TYPE='" + cbAssetType.Text.ToString() + "'";

                    DataTable dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

                        cbAssetMake.DataSource = dt;
                        cbAssetMake.DisplayMember = "FAM_ASSET_MAKE";
                        cbAssetMake.ValueMember = "FAM_ASSET_MAKE";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objSQLdb = null;
                }
            }

            if (chkAssetMake.Checked == true)
            {
                FillDataToList();
            }
        }

        private void txtAssetType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtAssetMake_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
              e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void lstMappedAssets_DoubleClick(object sender, EventArgs e)
        {
            Object obj = lstMappedAssets.GetItemText(lstMappedAssets.SelectedItem);
            //flagUpdate = true;

            if (chkAssetType.Checked == false && chkAssetMake.Checked == false)
            {
                txtDsearch.Text = obj.ToString();
            }

            else if (chkAssetType.Checked == true && chkAssetMake.Checked == false)
            {
                txtAssetType.Text = obj.ToString();
            }
            else if (chkAssetMake.Checked == true)
            {
                txtAssetMake.Text = obj.ToString();
            }

            
        }

      

       
       
    }
}
