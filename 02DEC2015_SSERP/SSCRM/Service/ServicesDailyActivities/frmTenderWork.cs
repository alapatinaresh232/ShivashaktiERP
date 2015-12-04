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
    public partial class frmTenderWork : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities;
        DataRow[] drs;

        public frmTenderWork()
        {
            InitializeComponent();
        }
        public frmTenderWork(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void frmTenderWork_Load(object sender, EventArgs e)
        {
            FillProductCategories();
            FillDistricts();

            if (drs != null)
            {
                cbDistricts.Text = drs[0]["BranchName"].ToString();
                txtAdhOrJdhName.Text = drs[0]["Purpose"].ToString();              
                cbProdCategory.Text = drs[0]["RelatedWork"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
                
            }
        }

        private void FillDistricts()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            try
            {
                strCmd = "SELECT DISTINCT District  DistrictName FROM VILLAGEMASTERUKEY ORDER BY District asc";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbDistricts.DataSource = dt;
                    cbDistricts.ValueMember = "DistrictName";
                    cbDistricts.DisplayMember = "DistrictName";
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

        private void FillProductCategories()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            try
            {
                strCmd = "SELECT DISTINCT CATEGORY_ID CategoryId, CATEGORY_NAME CategoryName "+
                         " FROM CATEGORY_MASTER ORDER BY CATEGORY_NAME ASC";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[1] = "--Select--";
                    dt.Rows.InsertAt(row,0);

                    cbProdCategory.DataSource = dt;
                    cbProdCategory.ValueMember = "CategoryName";
                    cbProdCategory.DisplayMember = "CategoryName";
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

        private bool CheckData()
        {
            bool flag = true;

            if (cbDistricts.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select District", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDistricts.Focus();
                return flag;
            }

            if (cbProdCategory.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Product Category", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbProdCategory.Focus();
                return flag;
            }

            if (txtAdhOrJdhName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Name Of ADH/JDH", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdhOrJdhName.Focus();
                return flag;
            }


            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (drs != null)
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }

                ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","TENDER WORK",cbDistricts.SelectedValue.ToString(),
                                cbDistricts.Text.ToString(),txtAdhOrJdhName.Text.ToString().Replace("'",""),"","","","","","","TENDER WORK", 
                               "Tender Work Of"+'-'+cbProdCategory.Text+'('+"Tenders Name"+'-'+ txtAdhOrJdhName.Text+')', txtRemarks.Text.ToString().Replace("'",""),
                                0,0,cbProdCategory.Text.ToString() });
                ((ServiceActivities)objServiceActivities).GetActivityDetails();


                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbDistricts.SelectedIndex = 0;
            cbProdCategory.SelectedIndex = 0;
            txtAdhOrJdhName.Text = "";
            txtRemarks.Text = "";
        }

       
        
       
    }
}
