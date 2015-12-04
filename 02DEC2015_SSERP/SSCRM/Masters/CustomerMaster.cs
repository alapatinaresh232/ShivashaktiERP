using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class CustomerMaster : Form
    {
        //private InvoiceDB objInv = null;
        private SQLDB objSQLdb = null;
        public CustomerMaster()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("CustomerMaster");
            VSearch.objCustomerMaster = this;
            VSearch.ShowDialog();
        }

        private void txtVillage_TextChanged(object sender, EventArgs e)
        {
            //objData = new InvoiceDB();
            if (txtVillage.Text != "" && txtMandal.Text != "" && txtDistrict.Text != "" && txtState.Text != "")
            {
                FillAllCustomersToGrid(txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtDistrict.Text.ToString(), txtState.Text.ToString());
            }
        }

        private void FillAllCustomersToGrid(string sVillage, string sMandal, string sDistrict, string sState)
        {
            //objData = new InvoiceDB();
            DataTable dt = new DataTable();
            dt = CustomersInVillage_Get(sVillage, sMandal, sDistrict, sState);
        }

        private DataTable CustomersInVillage_Get(string sVillage, string sMandal, string sDistrict, string sState)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xVillage", DbType.String, sVillage, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xMandal", DbType.String, sMandal, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDistrict", DbType.String, sDistrict, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xState", DbType.String, sState, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("CustomersInVillage_Get", CommandType.StoredProcedure, param).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return dt;
        }
    }
}
