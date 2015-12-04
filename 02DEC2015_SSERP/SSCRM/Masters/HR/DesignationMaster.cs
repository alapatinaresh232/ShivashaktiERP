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
    public partial class DesignationMaster : Form
    {
        SQLDB objDb = null;
        bool flagUpdate = false;
        public DesignationMaster()
        {
            InitializeComponent();
        }
        private void AddingDesignation_Load(object sender, EventArgs e)
        {
            FillDepartment();
            GeneratingDesigId();
        }
        private void FillDepartment()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT dept_name,dept_code FROM Dept_Mas ";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "000000";

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
        private void GeneratingDesigId()
        {
            SQLDB objDb = new SQLDB();
            DataTable dt=null;
            try
            {   
               string strCommand = "SELECT MAX(desig_code)+1 FROM DESIG_MAS";                
                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                txtDesigId.Text = dt.Rows[0][0].ToString(); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                objDb = null;
            }            
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDept.SelectedIndex > 0)
            {
                FillDesigToDG();
            }
        }
        private void FillDesigToDG()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT dp.dept_desc DeptName,dm.desig_code DesigId,dp.dept_code Dept_code,dm.desig_name DesigName FROM DESIG_MAS dm  INNER JOIN Dept_Mas dp ON dp.dept_code=dm.dept_code WHERE dm.dept_code="+Convert.ToInt32( cbDept.SelectedValue.ToString())+" ORDER BY desig_name ASC";
                dt = objDb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvDesigDetails.Rows.Clear();
                    for (int iVar = 0; iVar < dt.Rows.Count;iVar++ )
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                        cellSNo.Value = (iVar + 1).ToString();
                        tempRow.Cells.Add(cellSNo);

                        DataGridViewCell cellDeptId = new DataGridViewTextBoxCell();
                        cellDeptId.Value = (dt.Rows[iVar]["Dept_code"].ToString());
                        tempRow.Cells.Add(cellDeptId);

                        DataGridViewCell cellDept = new DataGridViewTextBoxCell();
                        cellDept.Value = (dt.Rows[iVar]["DeptName"].ToString());
                        tempRow.Cells.Add(cellDept);

                        DataGridViewCell cellDesigId = new DataGridViewTextBoxCell();
                        cellDesigId.Value = (dt.Rows[iVar]["DesigId"].ToString());
                        tempRow.Cells.Add(cellDesigId);

                        DataGridViewCell cellDesigName = new DataGridViewTextBoxCell();
                        cellDesigName.Value = (dt.Rows[iVar]["DesigName"].ToString());
                        tempRow.Cells.Add(cellDesigName);

                        gvDesigDetails.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                objDb = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                string strCmd="";
                    objDb = new SQLDB();
                    try
                    {
                        if (flagUpdate == false)
                            strCmd = "INSERT into DESIG_MAS(dept_code,desig_code,desig_name,desig_desc,ndesig_name)VALUES(" + Convert.ToInt32(cbDept.SelectedValue.ToString()) + " ," + Convert.ToInt32(txtDesigId.Text) + " ,'" + txtDesigName.Text + "','" + txtDesigName.Text + "','" + txtDesigName.Text + "');";
                        else
                        {
                            strCmd = "update DESIG_MAS SET dept_code=" + Convert.ToInt32(cbDept.SelectedValue.ToString()) + ",desig_name='" + txtDesigName.Text + "',desig_desc='" + txtDesigName.Text + "',ndesig_name='" + txtDesigName.Text + "' WHERE desig_code=" + Convert.ToInt32(txtDesigId.Text);
                            flagUpdate = false;
                        }
                        int iRes = objDb.ExecuteSaveData(strCmd);
                        if (iRes > 0)
                        {
                            MessageBox.Show("Data Saved Successfully", "Designation Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillDesigToDG();
                            GeneratingDesigId();
                            txtDesigName.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Data Not Saved ", "Designation Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool CheckData()
        {
            bool  flag=true;
            if(cbDept.SelectedIndex == 0)
            {
                MessageBox.Show("Select Department", "Designation Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbDept.Focus();
                flag = false;
            }
            if (txtDesigName.Text.Length == 0)
            {
                MessageBox.Show("Enter Desingnation Name", "Designation Master", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbDept.Focus();
                flag = false;
            }
            
           
           
            return flag;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            cbDept.SelectedIndex = 0;
            txtDesigName.Text = "";
            gvDesigDetails.Rows.Clear();
            GeneratingDesigId();
        }

        private void gvDesigDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (Convert.ToBoolean(gvDesigDetails.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                {
                    flagUpdate = true;
                    cbDept.SelectedValue = gvDesigDetails.Rows[e.RowIndex].Cells["DeptCode"].Value.ToString();

                    txtDesigId.Text=gvDesigDetails.Rows[e.RowIndex].Cells["DesigId"].Value.ToString();
                    txtDesigName.Text=gvDesigDetails.Rows[e.RowIndex].Cells["Designation"].Value.ToString();
                }

                if (e.ColumnIndex == gvDesigDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        objDb = new SQLDB();
                        int iDesigCode = Convert.ToInt32(gvDesigDetails.Rows[e.RowIndex].Cells["DesigId"].Value);
                        try
                        {
                            string strCmd = "delete desig_mas where desig_code=" + iDesigCode;
                            int iRes = objDb.ExecuteSaveData(strCmd);
                            if (iRes > 0)
                            {
                                MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                flagUpdate = false;
                                FillDesigToDG();
                                GeneratingDesigId();
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
            }
        }

    }
}
