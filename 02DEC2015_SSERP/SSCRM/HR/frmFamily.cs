using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;
using SSCRMDB;

namespace SSCRM
{
    public partial class frmFamily : Form
    {
        public EmployeeContactDetails objEmpContactDetails = null;
        public frmApplication objApplication;
        DataGridViewRow Fdgvr;
        
        private SQLDB objSQLDB = null;
        DataRow[] drs;
        string strmemberName="";
        string strRelation="";
        DateTime dateDob;
        string strResiding="";
        string strDepending="";
        string strOccupation="";
        bool flagContactDetails = false;
        string strInvType = "";
        public frmFamily()
        {
            InitializeComponent();
        }
        public frmFamily(string strFormType)
        {
            InitializeComponent();
            strInvType = strFormType;
        }
        public frmFamily(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        public frmFamily(string strFType, string memberName,string  relation,DateTime dob, string residing,string depending,string occupation,DataGridViewRow dgvr)
        {
            InitializeComponent();
            flagContactDetails = true;
            strInvType = strFType;
                strmemberName = memberName;
                strRelation = relation;
                dateDob  = dob;
                strResiding = residing;
                strDepending = depending;
                strOccupation = occupation;
                Fdgvr = dgvr;
             
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFamily_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 300, Screen.PrimaryScreen.WorkingArea.Y + 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            objSQLDB = new SQLDB();
            string strSQL1 = "SELECT OM_OCCUPATION,OM_OCCUPATION FROM OCCUPATION_MASTER";
            DataTable dt1 = objSQLDB.ExecuteDataSet(strSQL1, CommandType.Text).Tables[0];
            UtilityLibrary.PopulateControl(cmbOccupation_optional, dt1.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            cmbRelationShip.SelectedIndex = 1;
            cmbDepending.SelectedIndex = 1;
            cmbResiding.SelectedIndex = 1;
            if (drs != null)
            {
                cmbRelationShip.SelectedIndex = cmbRelationShip.Items.IndexOf(drs[0][1].ToString());
                txtName.Text = drs[0][2].ToString();
                dtpDOB.Value = Convert.ToDateTime(drs[0][3]);
                cmbResiding.SelectedIndex = cmbResiding.Items.IndexOf(drs[0][4].ToString());
                cmbDepending.SelectedIndex = cmbDepending.Items.IndexOf(drs[0][5].ToString());
                //txtOccupation.Text = drs[0][6].ToString();
                cmbOccupation_optional.Text = drs[0][6].ToString();

            }
            else
            {
                if (strmemberName != "")
                {
                    cmbRelationShip.Text = strRelation;
                    txtName.Text = strmemberName;
                    dtpDOB.Value = Convert.ToDateTime(dateDob);
                    cmbRelationShip.Text = strResiding;
                    cmbDepending.Text = strDepending;
                    cmbOccupation_optional.Text = strOccupation;
                }
            }
            dtpDOB_ValueChanged(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (strInvType != "EmployeeContactDetails")
            {
                //if (flagContactDetails == false)
                //{
                    UtilityLibrary oUtility = new UtilityLibrary();
                    if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
                        return;

                    //bellow line for delete the row in dtFamily table

                    if (drs != null)
                    {

                        ((frmApplication)objApplication).dtFamily.Rows.Remove(drs[0]);
                        //till here
                        DataView dv = ((frmApplication)objApplication).dtFamily.DefaultView;
                        if (txtName.Text == "FATHER" || txtName.Text == "MOTHER" || txtName.Text == "WIFE")
                        {
                            if (dv.Table.Rows.Count > 0)
                            {
                                dv.RowFilter = "sName='" + txtName.Text + "'";
                                DataTable dt;
                                dt = dv.ToTable();
                                if (dt.Rows.Count > 0)
                                {
                                    MessageBox.Show("This Relation Ship is already exists", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                    ((frmApplication)objApplication).dtFamily.Rows.Add(new Object[] { "-1", cmbRelationShip.Text, txtName.Text, dtpDOB.Value, cmbResiding.Text, cmbDepending.Text, cmbOccupation_optional.Text.ToString().ToUpper() });
                    ((frmApplication)objApplication).GetDGVFamily();
                    this.Close();
                //}
            }


            else if (strInvType.Equals("EmployeeContactDetails"))
            {
                DataGridView dgvFamily = null;
                if (txtName.Text != "")
                {
                    if (flagContactDetails == false)
                    {
                        dgvFamily = ((EmployeeContactDetails)objEmpContactDetails).dgvFamilyDetails;
                        AddFamilyDetailsToGrid(dgvFamily);

                        OrderSlNo();

                        this.Close();

                    }


                    else if (flagContactDetails == true)
                    {
                            ((EmployeeContactDetails)objEmpContactDetails).dgvFamilyDetails.Rows.Remove(Fdgvr);

                            dgvFamily = ((EmployeeContactDetails)objEmpContactDetails).dgvFamilyDetails;
                            AddFamilyDetailsToGrid(dgvFamily);

                            OrderSlNo();

                            this.Close();
                        

                    }
                }
            }
                

            
        }
        private void AddFamilyDetailsToGrid(DataGridView dgvFamilyDetails)
        {         
            int intRow =1;
            intRow = dgvFamilyDetails.Rows.Count+1;

            DataGridViewRow tempRow = new DataGridViewRow();


            DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
            cellSlNo.Value = (intRow+1);
            tempRow.Cells.Add(cellSlNo);

            DataGridViewCell cellMembername = new DataGridViewTextBoxCell();
            cellMembername.Value = (txtName.Text);
            tempRow.Cells.Add(cellMembername);

            DataGridViewCell cellRelation = new DataGridViewTextBoxCell();
            cellRelation.Value = cmbRelationShip.SelectedItem.ToString();
            tempRow.Cells.Add(cellRelation);

            DataGridViewCell cellDob = new DataGridViewTextBoxCell();
            cellDob.Value = Convert.ToDateTime(dtpDOB.Value).ToShortDateString();
            tempRow.Cells.Add(cellDob);

            DataGridViewCell cellResiding = new DataGridViewTextBoxCell();
            cellResiding.Value = cmbResiding.SelectedItem.ToString();
            tempRow.Cells.Add(cellResiding);

            DataGridViewCell celldepending = new DataGridViewTextBoxCell();
            celldepending.Value = cmbDepending.SelectedItem.ToString();
            tempRow.Cells.Add(celldepending);

            DataGridViewCell cellOccupation = new DataGridViewTextBoxCell();
            cellOccupation.Value = cmbOccupation_optional.Text.ToString();
            tempRow.Cells.Add(cellOccupation);


            intRow = intRow + 1;
            dgvFamilyDetails.Rows.Add(tempRow);
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime a = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy,MM,dd"));
            DateTime b = Convert.ToDateTime(dtpDOB.Value.ToString("yyyy,MM,dd"));
            TimeSpan ival = a - b;
            int years = (zeroTime + ival).Year - 1;
            txtAge.Text = years.ToString();
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            if (txtAge.Text.Length > 0)
            {
                dtpDOB.Value = Convert.ToDateTime(System.DateTime.Now).AddYears(-Convert.ToInt32(txtAge.Text));
            }
        }
        private void OrderSlNo()
        {
            if (((EmployeeContactDetails)objEmpContactDetails).dgvFamilyDetails.Rows.Count > 0)
            {
                for (int i = 0; i < ((EmployeeContactDetails)objEmpContactDetails).dgvFamilyDetails.Rows.Count; i++)
                {
                    ((EmployeeContactDetails)objEmpContactDetails).dgvFamilyDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                }
            }
        } 

       
      
    }
}
