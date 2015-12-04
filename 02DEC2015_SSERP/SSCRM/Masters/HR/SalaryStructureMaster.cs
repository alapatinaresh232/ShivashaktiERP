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
    public partial class SalaryStructureMaster : Form
    {
        SQLDB objDb = null;
        HRInfo objHRinfo = null;

        bool flage = false;

        public SalaryStructureMaster()
        {
            InitializeComponent();
        }

        private void SaleryStructerMaster_Load(object sender, EventArgs e)
        {
            dgvSaltructDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            dtpSalStructFrom.Value = DateTime.Today;
            dtpSalStructTo.Value = DateTime.Today;

            cbSalStructType.SelectedIndex = 0;
            lblDept.Visible = false;
            lblDesig.Visible = false;
            cbDept.Visible = false;
            CbDesig.Visible = false;

            //FillSalStructureDeatailsTogrid();
        }


        private void FillDepartment()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT dept_name,dept_code FROM Dept_Mas Order By dept_name";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "0";

                    dt.Rows.InsertAt(dr, 0);
                    cbDept.DataSource = dt;
                    cbDept.DisplayMember = "dept_name";
                    cbDept.ValueMember = "dept_code";
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
        private void FillDesignation()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT  desig_name,desig_code FROM DESIG_MAS Order BY desig_name";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "0";

                    dt.Rows.InsertAt(dr, 0);
                    CbDesig.DataSource = dt;
                    CbDesig.DisplayMember = "desig_name";
                    CbDesig.ValueMember = "desig_code";
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



        private void FillDesignationData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                if (cbDept.SelectedIndex > 0)
                {
                    strCommand = "SELECT desig_code,desig_name FROM DESIG_MAS " +
                                        " WHERE dept_code= " + Convert.ToInt32(cbDept.SelectedValue) + " Order BY desig_name";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    CbDesig.DataSource = dt;
                    CbDesig.DisplayMember = "desig_name";
                    CbDesig.ValueMember = "desig_code";
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
        private void GetSalStructDetails()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            if (txtSalStructCode.Text != "")
            {
                try
                {
                    strCommand = "SELECT  HSSM_SALSTRU_TYPE" +                       
                                       ", desig_name" +
                                       ",HSSM_EFFECTED_FROM" +
                                       ",HSSM_SALSTRU_STATUS" +
                                       ",HSSM_BASIC" +
                                       ",HSSM_HRA" +
                                       ",HSSM_CCA" +
                                       ",HSSM_CONV_ALW" +
                                       ",HSSM_LTA_ALW" +
                                       ",HSSM_SPL_ALW" +
                                       ",HSSM_UNF_ALW" +
                                       ",HSSM_VEH_ALW" +
                                       ",HSSM_CH_ED_ALW" +
                                       ",HSSM_BNP_ALW" +
                                       ",HSSM_MED_REIMB" +
                                       ",HSSM_PET_ALW" +
                                       ",HSSM_GROSS" +
                                       ",HSSM_DESG_CODE "+
                                       " FROM HR_SAL_STRU_MASTER" +
                                       " LEFT JOIN DESIG_MAS ON desig_code = HSSM_DESG_CODE" +                                      
                                       " WHERE HSSM_SALSTRU_CODE='" + txtSalStructCode.Text.ToString() + "'";


                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        flage = true;

                        if (dt.Rows[0]["HSSM_SALSTRU_TYPE"].ToString().Equals("S"))
                        {
                            cbSalStructType.SelectedIndex = 1;
                        }
                        else
                        {
                            cbSalStructType.SelectedIndex = 2;
                            CbDesig.Text = dt.Rows[0]["desig_name"].ToString();
                            //CbDesig.SelectedValue = dt.Rows[0]["HSSM_DESG_CODE"].ToString();
                        }
                        cbSalStructType.Tag = dt.Rows[0]["HSSM_SALSTRU_TYPE"].ToString();   
                     

                        dtpSalStructFrom.Value = Convert.ToDateTime(dt.Rows[0]["HSSM_EFFECTED_FROM"].ToString());

                        txtSalBasic.Text = Convert.ToInt32(dt.Rows[0]["HSSM_BASIC"]).ToString(); ;
                        txtHRA.Text = Convert.ToInt32(dt.Rows[0]["HSSM_HRA"]).ToString();
                        txtCCA.Text = Convert.ToInt32(dt.Rows[0]["HSSM_CCA"]).ToString();
                        txtConve.Text = Convert.ToInt32(dt.Rows[0]["HSSM_CONV_ALW"]).ToString();
                        txtLTA.Text = Convert.ToInt32(dt.Rows[0]["HSSM_LTA_ALW"]).ToString();
                        txtSpecial.Text = Convert.ToInt32(dt.Rows[0]["HSSM_SPL_ALW"]).ToString();
                        txtUniform.Text = Convert.ToInt32(dt.Rows[0]["HSSM_UNF_ALW"]).ToString();
                        txtVehicle.Text = Convert.ToInt32(dt.Rows[0]["HSSM_VEH_ALW"]).ToString();
                        txtChild.Text = Convert.ToInt32(dt.Rows[0]["HSSM_CH_ED_ALW"]).ToString();
                        txtBNP.Text = Convert.ToInt32(dt.Rows[0]["HSSM_BNP_ALW"]).ToString();
                        txtMED.Text = Convert.ToInt32(dt.Rows[0]["HSSM_MED_REIMB"]).ToString();
                        txtPET.Text = Convert.ToInt32(dt.Rows[0]["HSSM_PET_ALW"]).ToString();
                        txtGrosSal.Text = Convert.ToInt32(dt.Rows[0]["HSSM_GROSS"]).ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                }
            }         
            
                
        }


        private void txtSalBasic_TextChanged(object sender, EventArgs e)
        {
            
            CalculateGrossTotals();
        }

        private void FillSalStructureDeatailsTogrid()
        {
            objHRinfo = new HRInfo();
            DataTable dt = new DataTable();
            dgvSaltructDetails.Rows.Clear();

            if (cbSalStructType.SelectedIndex > 0)
            {
                try
                {

                    dt = objHRinfo.Get_SalaryStructureDetails(cbSalStructType.Tag.ToString()).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dgvSaltructDetails.Rows.Add();

                            dgvSaltructDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalStruType"].Value = dt.Rows[i]["SalStruType"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalStruCode"].Value = dt.Rows[i]["SalStruCode"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["DesigCode"].Value = dt.Rows[i]["DesigCode"].ToString();

                            dgvSaltructDetails.Rows[i].Cells["DesignName"].Value = dt.Rows[i]["DesignName"].ToString();

                            dgvSaltructDetails.Rows[i].Cells["EffectFrom"].Value = Convert.ToDateTime(dt.Rows[i]["EffectFrom"].ToString()).ToShortDateString();
                            dgvSaltructDetails.Rows[i].Cells["EffectTo"].Value = dt.Rows[i]["EffectTo"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalStatus"].Value = dt.Rows[i]["SalStatus"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalBasic"].Value = dt.Rows[i]["SalBasic"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalHra"].Value = dt.Rows[i]["SalHra"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalCca"].Value = dt.Rows[i]["SalCca"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalConvAlw"].Value = dt.Rows[i]["SalConvAlw"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalLTAAlw"].Value = dt.Rows[i]["SalLTAAlw"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalSpecialAlw"].Value = dt.Rows[i]["SalSpecialAlw"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalUnfAlw"].Value = dt.Rows[i]["SalUnfAlw"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalvehicleAlw"].Value = dt.Rows[i]["SalvehicleAlw"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalChildAlw"].Value = dt.Rows[i]["SalChildAlw"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalBnpAlw"].Value = dt.Rows[i]["SalBnpAlw"].ToString();

                            dgvSaltructDetails.Rows[i].Cells["SalMedReimb"].Value = dt.Rows[i]["SalMedReimb"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["SalpET"].Value = dt.Rows[i]["SalpET"].ToString();
                            dgvSaltructDetails.Rows[i].Cells["Gross"].Value = dt.Rows[i]["SalGross"].ToString();



                        }

                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRinfo = null;
                    dt = null;
                }
            }

        }


        private void CalculateGrossTotals()
        {
            Int32 SalBasic, SalHra, SalCca, SalConv, SalLta, SalSpec, SalUnif, SalVeh, SalChild, SalBnp, SalMed, SalPet, SalGross;
            SalBasic = SalHra = SalCca = SalConv = SalLta = SalSpec = SalUnif = SalVeh = SalChild = SalBnp = SalMed = SalPet = SalGross = 0;
            if (txtSalBasic.Text.Length > 0)
                SalBasic = Convert.ToInt32(txtSalBasic.Text);
            if (txtHRA.Text.Length > 0)
                SalHra = Convert.ToInt32(txtHRA.Text);
            if (txtCCA.Text.Length > 0)
                SalCca = Convert.ToInt32(txtCCA.Text);
            if (txtConve.Text.Length > 0)
                SalConv = Convert.ToInt32(txtConve.Text);
            if (txtLTA.Text.Length > 0)
                SalLta = Convert.ToInt32(txtLTA.Text);
            if (txtSpecial.Text.Length > 0)
                SalSpec = Convert.ToInt32(txtSpecial.Text);
            if (txtUniform.Text.Length > 0)
                SalUnif = Convert.ToInt32(txtUniform.Text);
            if (txtVehicle. Text.Length > 0)
                SalVeh = Convert.ToInt32(txtVehicle.Text);
            if (txtChild.Text.Length > 0)
                SalChild = Convert.ToInt32(txtChild.Text);
            if (txtBNP.Text.Length > 0)
                SalBnp = Convert.ToInt32(txtBNP.Text);
            if (txtMED.Text.Length > 0)
                SalMed = Convert.ToInt32(txtMED.Text);
            if (txtPET. Text.Length > 0)
                SalPet = Convert.ToInt32(txtPET.Text);
            SalGross = Convert.ToInt32(SalBasic + SalHra + SalCca + SalConv + SalLta + SalSpec + SalUnif + SalVeh + SalChild + SalBnp + SalMed + SalPet);
            txtGrosSal.Text = SalGross.ToString("0");


        }
            

        private void txtSalBasic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtHRA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCCA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtConve_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b'))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLTA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtSpecial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtUniform_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtChild_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtBNP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMED_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPET_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void cbSalStructType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSalStructType.SelectedIndex > 0)
            {
                if (cbSalStructType.SelectedIndex == 1)
                {
                    cbSalStructType.Tag = "S";
                    dgvSaltructDetails.Columns["DesignName"].Visible = false;
                    dgvSaltructDetails.Columns["EffectFrom"].Visible = true;
                    txtSalStructCode.Text = "";
                    FillSalStructureDeatailsTogrid();

                    lblDept.Visible = false;
                    lblDesig.Visible = false;
                    cbDept.Visible = false;
                    CbDesig.Visible = false;
                }
                else if (cbSalStructType.SelectedIndex == 2)
                {
                    cbSalStructType.Tag = "D";
                    dgvSaltructDetails.Columns["DesignName"].Visible = true;
                    dgvSaltructDetails.Columns["EffectFrom"].Visible = false;
                    txtSalStructCode.Text = "";
                       
                    FillSalStructureDeatailsTogrid();

                    FillDepartment();

                    lblDept.Visible = true;
                    lblDesig.Visible = true;
                    cbDept.Visible = true;
                    CbDesig.Visible = true;
                }
            }
        }
        private bool CheckData()
        {
            bool Chkflag = true;
           

            if (cbSalStructType.SelectedIndex == 0)
            {
                MessageBox.Show("Select SalaryType", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                cbSalStructType.Focus();
                return Chkflag;
            }        
         
            
            else if (txtSalStructCode.Text.Length == 0)
            {
                MessageBox.Show("Enter Struct Code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Chkflag = false;
                txtSalStructCode.Focus();
                return Chkflag;
            }
            else if(cbSalStructType.SelectedIndex==2)
            {

                if (CbDesig.SelectedIndex == 0 || CbDesig.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Design", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Chkflag = false;
                    CbDesig.Focus();
                    return Chkflag;
                }
              //else if (cbDept.SelectedIndex == 0)
              //{
              //    MessageBox.Show("Select Dept", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //    Chkflag = false;
              //    cbDept.Focus();
              //    return Chkflag;
              //}
            }
               
          
           

            return Chkflag;
        }

        private void txtSalBasic_Validated(object sender, EventArgs e)
        {
            //txtSalBasic, txtHRA, txtCCA, txtConve, txtLTA, txtSpecial, txtUniform, txtVehicle, txtChild, txtBNP, txtMED, txtPET, txtGrosSal
        }

        private void txtSalBasic_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtHRA_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtCCA_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtConve_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtLTA_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtSpecial_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtUniform_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtChild_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtBNP_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtMED_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtPET_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtVehicle_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateGrossTotals();
        }

        private void txtVehicle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

      
        private void txtSalStructCode_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {

            int ival = 0;
            objDb = new SQLDB();
            string strCommand = "";
            if (CheckData())
            {

                Int32 SalBasic, SalHra, SalCca, SalConv, SalLta, SalSpec, SalUnif, SalVeh, SalChild, SalBnp, SalMed, SalPet, SalGross;
                SalBasic = SalHra = SalCca = SalConv = SalLta = SalSpec = SalUnif = SalVeh = SalChild = SalBnp = SalMed = SalPet = SalGross = 0;
                if (txtSalBasic.Text.Length > 0)
                    SalBasic = Convert.ToInt32(txtSalBasic.Text);
                if (txtHRA.Text.Length > 0)
                    SalHra = Convert.ToInt32(txtHRA.Text);
                if (txtCCA.Text.Length > 0)
                    SalCca = Convert.ToInt32(txtCCA.Text);
                if (txtConve.Text.Length > 0)
                    SalConv = Convert.ToInt32(txtConve.Text);
                if (txtLTA.Text.Length > 0)
                    SalLta = Convert.ToInt32(txtLTA.Text);
                if (txtSpecial.Text.Length > 0)
                    SalSpec = Convert.ToInt32(txtSpecial.Text);
                if (txtUniform.Text.Length > 0)
                    SalUnif = Convert.ToInt32(txtUniform.Text);
                if (txtVehicle.Text.Length > 0)
                    SalVeh = Convert.ToInt32(txtVehicle.Text);
                if (txtChild.Text.Length > 0)
                    SalChild = Convert.ToInt32(txtChild.Text);
                if (txtBNP.Text.Length > 0)
                    SalBnp = Convert.ToInt32(txtBNP.Text);
                if (txtMED.Text.Length > 0)
                    SalMed = Convert.ToInt32(txtMED.Text);
                if (txtPET.Text.Length > 0)
                    SalPet = Convert.ToInt32(txtPET.Text);
                SalGross = Convert.ToInt32(SalBasic + SalHra + SalCca + SalConv + SalLta + SalSpec + SalUnif + SalVeh + SalChild + SalBnp + SalMed + SalPet);
                txtGrosSal.Text = SalGross.ToString("0");

                if (txtGrosSal.Text != "0")
                {

                    try
                    {
                       
                        if (flage == true)
                        {

                            strCommand = "UPDATE HR_SAL_STRU_MASTER SET HSSM_SALSTRU_TYPE='" + cbSalStructType.Tag.ToString() +
                                                                        "',HSSM_SALSTRU_CODE='" + txtSalStructCode.Text +
                                                                        "',HSSM_EFFECTED_FROM ='" + Convert.ToDateTime(dtpSalStructFrom.Value).ToString("dd/MMM/yyyy") +
                                                                        //"',HSSM_EFFECTED_TO='" + Convert.ToDateTime(dtpSalStructTo.Value).ToString("dd/MMM/yyyy") +
                                                                        "',HSSM_SALSTRU_STATUS='y',HSSM_BASIC=" + SalBasic +
                                                                        ",HSSM_HRA=" + SalHra +
                                                                        ",HSSM_CCA=" + SalCca +
                                                                        ",HSSM_CONV_ALW=" + SalConv +
                                                                        ",HSSM_LTA_ALW=" + SalLta +
                                                                        ",HSSM_SPL_ALW=" + SalSpec +
                                                                        ",HSSM_UNF_ALW=" + SalUnif +
                                                                        ",HSSM_VEH_ALW=" + SalVeh +
                                                                        ",HSSM_CH_ED_ALW=" + SalChild +
                                                                        ",HSSM_BNP_ALW=" + SalBnp +
                                                                        ",HSSM_MED_REIMB=" + SalMed +
                                                                        ",HSSM_PET_ALW=" + SalPet +
                                                                        ",HSSM_GROSS=" + Convert.ToInt32(txtGrosSal.Text) +
                                                                        ",HSSM_MODIFIED_BY='" + CommonData.LogUserId +
                                                                        "',HSSM_MODIFIED_DATE=getdate()" +
                                                                         " WHERE HSSM_SALSTRU_CODE='" + txtSalStructCode.Text + "'";

                            if (cbSalStructType.SelectedIndex == 2)
                            {
                                strCommand += "UPDATE HR_SAL_STRU_MASTER SET HSSM_DESG_CODE=" + Convert.ToInt32(CbDesig.SelectedValue) +
                                                " WHERE HSSM_SALSTRU_CODE='" + txtSalStructCode.Text + "'";
                            }


                        }
                        else if (flage == false)
                        {


                            strCommand = "INSERT INTO HR_SAL_STRU_MASTER(HSSM_SALSTRU_TYPE" +
                                                             ",HSSM_SALSTRU_CODE" +
                                                           ",HSSM_EFFECTED_FROM" +
                                                           ",HSSM_SALSTRU_STATUS" +
                                                           ",HSSM_BASIC" +
                                                           ",HSSM_HRA" +
                                                           ",HSSM_CCA" +
                                                           ",HSSM_CONV_ALW" +
                                                           ",HSSM_LTA_ALW" +
                                                           ",HSSM_SPL_ALW" +
                                                           ",HSSM_UNF_ALW" +
                                                           ",HSSM_VEH_ALW" +
                                                           ",HSSM_CH_ED_ALW" +
                                                           ",HSSM_BNP_ALW" +
                                                           ",HSSM_MED_REIMB" +
                                                           ",HSSM_PET_ALW" +
                                                           ",HSSM_GROSS ";
                            if (cbSalStructType.Text.Equals("DESIGNATION"))
                            {
                                strCommand += ",HSSM_DESG_CODE";
                            }

                            strCommand += ",HSSM_CREATED_BY,HSSM_CREATED_DATE)VALUES('" + cbSalStructType.Tag.ToString() +
                                                            "','" + txtSalStructCode.Text +
                                                            "','" + Convert.ToDateTime(dtpSalStructFrom.Value).ToString("dd/MMM/yyyy") +
                                                            "','Y'," + SalBasic +
                                                            "," + SalHra +
                                                            "," + SalCca +
                                                            "," + SalConv +
                                                            "," + SalLta +
                                                            "," + SalSpec +
                                                            "," + SalUnif +
                                                            "," + SalVeh +
                                                            "," + SalChild +
                                                            "," + SalBnp +
                                                            "," + SalMed +
                                                            "," + SalPet +
                                                            "," + Convert.ToInt32(txtGrosSal.Text) + " ";
                            if (cbSalStructType.Text.Equals("DESIGNATION"))
                            {
                                strCommand += "," + Convert.ToInt32(CbDesig.SelectedValue) + " ";
                            }
                            strCommand += ",'" + CommonData.LogUserId + "',getdate())";

                        }

                        if (strCommand.Length > 10)
                        {
                            ival = objDb.ExecuteSaveData(strCommand);
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (ival > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnClear_Click(null, null);                        
                        flage = false;


                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSalStructCode.ReadOnly = false;
            txtSalStructCode.Text = "";
            cbDept.SelectedIndex = 0;
            CbDesig.SelectedIndex = 0;
            cbSalStructType.SelectedIndex = 0;
            dtpSalStructFrom.Value = DateTime.Today;
            dtpSalStructTo.Value = DateTime.Today;
            txtSalBasic.Text = "";
            txtHRA.Text = "";
            txtCCA.Text = "";
            txtConve.Text = "";
            txtLTA.Text = "";
            txtSpecial.Text = "";
            txtUniform.Text = "";
            txtVehicle.Text = "";
            txtChild.Text = "";
            txtBNP.Text = "";
            txtMED.Text = "";
            txtPET.Text = "";
            txtGrosSal.Text = "";
            dgvSaltructDetails.Rows.Clear();
            flage = false;
        }

        private void dgvSaltructDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == dgvSaltructDetails.Columns["Edit"].Index)
                {
                    flage = true;

                    FillDesignation();
                    txtSalStructCode.Text = dgvSaltructDetails.Rows[e.RowIndex].Cells["SalStruCode"].Value.ToString();
                    txtSalStructCode.ReadOnly = true;

                    if (dgvSaltructDetails.Rows[e.RowIndex].Cells["SalStruType"].Value.ToString().Equals("D"))
                    {
                        CbDesig.SelectedValue = dgvSaltructDetails.Rows[e.RowIndex].Cells["DesigCode"].Value.ToString();
                    }
                    dtpSalStructFrom.Value = Convert.ToDateTime(dgvSaltructDetails.Rows[e.RowIndex].Cells["EffectFrom"].Value.ToString());
                    //dtpSalStructTo.Value = Convert.ToDateTime(dgvSaltructDetails.Rows[e.RowIndex].Cells["EffectTo"].Value.ToString());
                    txtSalBasic.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalBasic"].Value).ToString();
                    txtHRA.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalHra"].Value).ToString();
                    txtCCA.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalCca"].Value).ToString();
                    txtConve.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalConvAlw"].Value).ToString();
                    txtLTA.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalLTAAlw"].Value).ToString();
                    txtSpecial.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalSpecialAlw"].Value).ToString();
                    txtUniform.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalUnfAlw"].Value).ToString();
                    txtVehicle.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalvehicleAlw"].Value).ToString();
                    txtChild.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalChildAlw"].Value).ToString();
                    txtBNP.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalBnpAlw"].Value).ToString();
                    txtMED.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalMedReimb"].Value).ToString();
                    txtPET.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["SalpET"].Value).ToString();
                    txtGrosSal.Text = Convert.ToInt32(dgvSaltructDetails.Rows[e.RowIndex].Cells["Gross"].Value).ToString();


                }
            }
               
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            objDb = new SQLDB();
            string strCommand = "";
            int ival = 0;

            if (txtSalStructCode.Text != "")
            {                
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {

                    try
                    {
                        strCommand = "DELETE FROM HR_SAL_STRU_MASTER WHERE HSSM_SALSTRU_CODE='" + txtSalStructCode.Text + "'";
                        ival = objDb.ExecuteSaveData(strCommand);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                    if (ival > 0)
                    {
                        MessageBox.Show("Data deleted successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnClear_Click(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter Valid Salary Structure Code","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
           

           
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDept.SelectedIndex > 0)
            {
                FillDesignationData();
            }
        }

        private void txtSalStructCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSalStructCode.Text.Length > 0)
            {
                GetSalStructDetails();
            }
            else
            {
                dtpSalStructFrom.Value = DateTime.Today;
                CbDesig.SelectedIndex = -1;
                txtSalBasic.Text = "";
                txtHRA.Text = "";
                txtCCA.Text = "";
                txtConve.Text = "";
                txtLTA.Text = "";
                txtSpecial.Text = "";
                txtUniform.Text = "";
                txtVehicle.Text = "";
                txtChild.Text = "";
                txtBNP.Text = "";
                txtMED.Text = "";
                txtPET.Text = "";
                txtGrosSal.Text = "";
            }
        }

    }
}

    