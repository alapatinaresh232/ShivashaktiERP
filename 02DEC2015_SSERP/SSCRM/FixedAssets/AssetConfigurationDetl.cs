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
    public partial class AssetConfigurationDetl : Form
    {
        SQLDB objSQLdb = null;
        public ItInventory objItInventory;
        private string AssetConfigId = "";      
        DataRow[] drs;
        string AssetType = "";

        public AssetConfigurationDetl(string sAssetType)
        {
            InitializeComponent();
            AssetType = sAssetType;
        }


        public AssetConfigurationDetl(string strAssetType,DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
            AssetType = strAssetType;
        }

        private void AssetConfigurationDetl_Load(object sender, EventArgs e)
        {
            FillConfigType();

            if (drs != null)
            {
                AssetConfigId = drs[0]["AssetConfigId"].ToString();
                cbConfigType.Text = drs[0]["ConfigType"].ToString();
                cbConfigMake.Text = drs[0]["ConfigMake"].ToString();
                cbConfigModel.Text = drs[0]["ConfigModel"].ToString();
                txtCapacity.Text = drs[0]["ConfigCapacity"].ToString();
                txtQty.Text = drs[0]["ConfigQty"].ToString();
            }
        }

        private void FillConfigType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbConfigModel.DataSource = null;
            try
            {
                string strCmd = "SELECT DISTINCT(FACM_ASSET_CONF_TYPE) "+
                                " FROM FIXED_ASSETS_SYS_CONFID_MAS "+
                                " WHERE FACM_ASSET_TYPE='"+ AssetType +
                                "' ORDER BY FACM_ASSET_CONF_TYPE ASC";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbConfigType.DataSource = dt;
                    cbConfigType.DisplayMember = "FACM_ASSET_CONF_TYPE";
                    cbConfigType.ValueMember = "FACM_ASSET_CONF_TYPE";
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

        private void FillConfigMakeData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbConfigModel.DataSource = null;
            try
            {
                if (cbConfigType.SelectedIndex > 0)
                {
                    string strCmd = "SELECT DISTINCT(FACM_ASSET_CONF_MAKE) " +
                                    " FROM FIXED_ASSETS_SYS_CONFID_MAS " +
                                    " WHERE FACM_ASSET_CONF_TYPE='" + cbConfigType.SelectedValue.ToString() +
                                    "'ORDER BY FACM_ASSET_CONF_MAKE ASC";


                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbConfigMake.DataSource = dt;
                    cbConfigMake.DisplayMember = "FACM_ASSET_CONF_MAKE";
                    cbConfigMake.ValueMember = "FACM_ASSET_CONF_MAKE";
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

        private void FillConfigModels()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                if (cbConfigMake.SelectedIndex > 0)
                {
                    string strCmd = "SELECT DISTINCT(FACM_ASSET_CONF_MODEL) " +
                                    " FROM FIXED_ASSETS_SYS_CONFID_MAS " +
                                    " WHERE  FACM_ASSET_CONF_TYPE='"+ cbConfigType.SelectedValue.ToString() +
                                    "' AND FACM_ASSET_CONF_MAKE='" + cbConfigMake.SelectedValue.ToString() +
                                    "' ORDER BY FACM_ASSET_CONF_MODEL ASC ";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbConfigModel.DataSource = dt;
                    cbConfigModel.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbConfigModel.ValueMember = "FACM_ASSET_CONF_MODEL";
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

        private void GenerateAssetConfigSLNo()
        {
            if (cbConfigType.SelectedIndex > 0)
            {
                objSQLdb = new SQLDB();
                DataTable dt = new DataTable();
                string strNewNo = "";
                string strCmd = "";
                try
                {


                    if (AssetConfigId.Length == 0)
                    {
                        strNewNo = "SSGC/" + cbConfigType.SelectedValue.ToString() + '/';

                        strCmd = "SELECT ISNULL(MAX(CAST(SUBSTRING(ISNULL(FASC_CONFIG_ASSET_ID,'" + strNewNo +
                                       "')," + (strNewNo.Length + 1) + "," + (strNewNo.Length + 6) + ") AS NUMERIC)),0)+1 " +
                                       " FROM FIXED_ASSETS_SYSTEM_CONFIG " +
                                       " WHERE FASC_CONFIG_ASSET_ID LIKE '%" + strNewNo + "%'";
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            AssetConfigId = strNewNo + Convert.ToInt32(dt.Rows[0][0].ToString());
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
            else
            {
                
                AssetConfigId = "";
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbConfigType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Configuration Type","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbConfigType.Focus();
            }
            else if (cbConfigModel.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Configuration Model", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbConfigModel.Focus();
            }
            else if (cbConfigMake.SelectedIndex == 0)

            {
                flag = false;
                MessageBox.Show("Please Select Configuration Make", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbConfigMake.Focus();
            }
            else if (txtQty.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Quantity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Focus();

            }
            //else if (txtCapacity.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Capacity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtCapacity.Focus();

            //}

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool bFlag = false;

            if (drs != null)
            {
                ((ItInventory)objItInventory).dtConfigDetl.Rows.Remove(drs[0]);
            }

            if (CheckData() == true)
            {
                if (((ItInventory)objItInventory).dtConfigDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < ((ItInventory)objItInventory).dtConfigDetl.Rows.Count; i++)
                    {
                        if (cbConfigType.Text.Equals(((ItInventory)objItInventory).dtConfigDetl.Rows[i]["ConfigType"].ToString()))
                        {
                            bFlag = true;
                            break;

                        }

                    }
                    
                }
                if (bFlag == false)
                {

                    ((ItInventory)objItInventory).dtConfigDetl.Rows.Add(new object[] { "-1",AssetConfigId,txtCapacity.Text.ToString(),cbConfigType.Text.ToString(),
                                               cbConfigMake.Text.ToString(),cbConfigModel.Text.ToString(),txtQty.Text.ToString() });
                    ((ItInventory)objItInventory).GetAssetConfigDetails();

                    this.Close();

                }
                else
                {
                    this.Close();
                }
            }
            
            

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbConfigMake.SelectedIndex = -1;
            cbConfigType.SelectedIndex = 0;
            cbConfigModel.SelectedIndex = -1;
            txtCapacity.Text = "";
            txtQty.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbConfigType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConfigType.SelectedIndex > 0)
            {
                GenerateAssetConfigSLNo();
                FillConfigMakeData();
            }

        }

        private void cbConfigMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConfigMake.SelectedIndex > 0)
            {
                FillConfigModels();
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            
        }

        private void txtCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

       
     
    }
}
