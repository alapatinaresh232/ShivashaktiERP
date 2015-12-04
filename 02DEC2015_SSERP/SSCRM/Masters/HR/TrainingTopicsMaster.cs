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
using SSAdmin;

namespace SSCRM
{
    public partial class TrainingTopicsMaster : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;     
        Int32 nTopicId = 0;
       
        bool flagUpdate = false;
        string sDepartments = "";

        public TrainingTopicsMaster()
        {
            InitializeComponent();
        }

        private void TrainingTopicsMaster_Load(object sender, EventArgs e)
        {
            FillTopicTypes();
            FillDepartmentData();         
            
            cbTopicType.Visible = true;
            cbTopicName.Visible = true;
            txtTopicName.Visible = false;
            txtTopicType.Visible = false;    

        }

        private void FillTopicTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();          
            try
            {
                string strCmd = "SELECT HTTTM_TOPIC_TYPE FROM HR_TRAINING_TOPIC_TYPE_MASTER "+
                                " ORDER BY HTTTM_TOPIC_TYPE ASC";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["HTTTM_TOPIC_TYPE"] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbTopicType.DataSource = dt;
                    cbTopicType.DisplayMember = "HTTTM_TOPIC_TYPE";
                    cbTopicType.ValueMember = "HTTTM_TOPIC_TYPE";
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

        private void FillTopicNames()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbTopicName.DataSource = null;
            try
            {
                if (cbTopicType.SelectedIndex > 0)
                {
                    string strCmd = "SELECT HTTMH_TOPIC_ID,HTTMH_TOPIC_NAME FROM HR_TRAINING_TOPIC_MASTER_HEAD " +
                                    " WHERE HTTMH_TOPIC_TYPE='" + cbTopicType.Text.ToString() +
                                    "' ORDER BY HTTMH_TOPIC_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["HTTMH_TOPIC_NAME"] = "--Select--";

                    dt.Rows.InsertAt(row,0);

                    cbTopicName.DataSource = dt;
                    cbTopicName.DisplayMember = "HTTMH_TOPIC_NAME";
                    cbTopicName.ValueMember = "HTTMH_TOPIC_ID";

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

      

        private void chkTopicName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTopicName.Checked == true)
            {
                txtTopicName.Visible = true;
                cbTopicName.Visible = false;
                cbTopicName.SelectedIndex = 0;
               
               
            }
            else
            {
                txtTopicName.Visible = false;
                cbTopicName.Visible = true;
            }

        }

        private void chkTopicType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTopicType.Checked == true)
            {
                cbTopicType.Visible = false;
                txtTopicType.Visible = true;
                chkTopicName.Checked = true;
                cbTopicName.Visible = false;
                txtTopicName.Visible = true;
                chkTopicName.Enabled = false;
                cbTopicType.SelectedIndex = 0;

            }
            else
            {
                chkTopicName.Checked = false;
                cbTopicName.Visible = true;
                txtTopicName.Visible = false;
                txtTopicType.Visible = false;
                cbTopicType.Visible = true;
                chkTopicName.Enabled = true;
            }
        }

      

      
        private void FillDepartmentData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            clbDesigs.Items.Clear();
            try
            {
                string strCommand = "SELECT DISTINCT dept_name,dept_code FROM Dept_Mas ORDER BY dept_name";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Text = dataRow["dept_name"].ToString();
                        oclBox.Tag = dataRow["dept_code"].ToString();

                        clbDepartments.Items.Add(oclBox);


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

        private void FillDesignationsData(string strDeptCode)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT(desig_name), desig_code FROM DESIG_MAS " +
                                " WHERE CAST(dept_code AS VARCHAR)IN(" + strDeptCode + ") ORDER BY desig_name";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Text = dataRow["desig_name"].ToString();
                        oclBox.Tag = dataRow["desig_code"].ToString();

                        clbDesigs.Items.Add(oclBox);
                        oclBox = null;


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

        private bool CheckData()
        {
            bool flag = true;

            if (chkTopicType.Checked == false)
            {
                if (cbTopicType.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Topic Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbTopicType.Focus();
                    return flag;
                }
            }
            else if (chkTopicType.Checked == true)
            {
                if (txtTopicType.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Topic Type ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTopicType.Focus();
                    return flag;
                }

            }

            if (chkTopicName.Checked == false)
            {
                if (cbTopicName.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Topic Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbTopicName.Focus();
                    return flag;
                }
                
            }
            else if (chkTopicName.Checked == true)
            {
                if (txtTopicName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Topic Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTopicName.Focus();
                    return flag;
                }

            }
            if (txtDuration.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Topic Duration ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDuration.Focus();
                return flag;
            }

            if (clbDepartments.CheckedItems.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Department", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;

            }
            if (clbDesigs.CheckedItems.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Designation", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
           
            return flag;
        }






        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (SaveTopicMasterHead() > 0)
                {
                    if (SaveTopicsMasterDetl() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);

                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private int SaveTopicMasterHead()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            DataTable dt = new DataTable();
            try
            {                       
                
                if (chkTopicType.Checked == true && chkTopicName.Checked == true)
                {


                    strCommand = "SELECT * FROM HR_TRAINING_TOPIC_TYPE_MASTER "+
                                 " WHERE HTTTM_TOPIC_TYPE='"+ txtTopicType.Text.ToString() + 
                                 "' AND HTTTM_TOPIC_TYPE_DESC='"+ txtTopicType.Text.ToString() +"'";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    strCommand = "";
                    if (dt.Rows.Count == 0)
                    {

                        strCommand = "INSERT INTO HR_TRAINING_TOPIC_TYPE_MASTER(HTTTM_TOPIC_TYPE "+
                                                                             ", HTTTM_TOPIC_TYPE_DESC "+
                                                                             ", HTTTM_AUTHORIZED_BY "+
                                                                             ", HTTTM_CREATED_BY "+
                                                                             ", HTTTM_CREATED_DATE "+
                                                                             ")VALUES('"+ txtTopicType.Text.ToString() +
                                                                             "','"+ txtTopicType.Text.ToString() +
                                                                             "','','"+ CommonData.LogUserId +
                                                                             "',getdate())";

                        if (strCommand.Length > 10)
                        {
                            int iRec = objSQLdb.ExecuteSaveData(strCommand);
                        }

                        strCommand = "";
                        objSQLdb = new SQLDB();
                       
                        strCommand = "INSERT INTO HR_TRAINING_TOPIC_MASTER_HEAD(HTTMH_TOPIC_NAME " +
                                                                             ", HTTMH_TOPIC_TYPE " +
                                                                             ", HTTMH_TOPIC_TIME " +
                                                                             ", HTTMH_AUTHORIZED_BY " +
                                                                             ", HTTMH_CREATED_BY " +
                                                                             ", HTTMH_CREATED_DATE " +
                                                                             ")VALUES " +
                                                                             "('" + txtTopicName.Text.ToString() +
                                                                             "','" + txtTopicType.Text.ToString() +
                                                                             "'," + Convert.ToInt32(txtDuration.Text) +
                                                                             ",'','" + CommonData.LogUserId +
                                                                             "',getdate())";
                    }
                    else
                    {
                        MessageBox.Show("Topic Type Already Exists!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                if (chkTopicType.Checked == false && chkTopicName.Checked == true)
                {
                    strCommand = "SELECT * FROM HR_TRAINING_TOPIC_MASTER_HEAD "+
                                " WHERE HTTMH_TOPIC_NAME='" + txtTopicName.Text.ToString() +
                                "' AND HTTMH_TOPIC_TYPE='" + cbTopicType.SelectedValue.ToString() +"'";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    strCommand = "";

                    if (dt.Rows.Count == 0)
                    {
                        strCommand = "INSERT INTO HR_TRAINING_TOPIC_MASTER_HEAD(HTTMH_TOPIC_NAME " +
                                                                           ", HTTMH_TOPIC_TYPE " +
                                                                           ", HTTMH_TOPIC_TIME " +
                                                                           ", HTTMH_AUTHORIZED_BY " +
                                                                           ", HTTMH_CREATED_BY " +
                                                                           ", HTTMH_CREATED_DATE " +
                                                                           ")VALUES " +
                                                                           "('" + txtTopicName.Text.ToString() +
                                                                           "','" + cbTopicType.SelectedValue.ToString() +
                                                                           "'," + Convert.ToInt32(txtDuration.Text) +
                                                                           ",'','" + CommonData.LogUserId +
                                                                           "',getdate())";
                    }
                    else
                    {
                        MessageBox.Show("Topic Name Already Exists!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                if (chkTopicType.Checked == false && chkTopicName.Checked == false)
                {
                    strCommand = "UPDATE HR_TRAINING_TOPIC_MASTER_HEAD SET HTTMH_TOPIC_NAME='" + cbTopicName.Text.ToString() +
                                "',HTTMH_TOPIC_TYPE='" + cbTopicType.SelectedValue.ToString() +
                                "',HTTMH_TOPIC_TIME=" + Convert.ToInt32(txtDuration.Text) +
                                ",HTTMH_LAST_MODIFIED_BY='"+ CommonData.LogUserId +
                                "',HTTMH_LAST_MODIFIED_DATE=getdate()"+
                                " WHERE HTTMH_TOPIC_ID= " + Convert.ToInt32(cbTopicName.SelectedValue) + "";
                }

                if (strCommand.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private int SaveTopicsMasterDetl()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            int iRes = 0;
            int nSlNo = 1;
            string strCommand = "";           
            try
            {
               
                    strCommand = "DELETE FROM HR_TRAINING_TOPIC_MASTER_DETL "+
                                " WHERE HTTMD_TOPIC_ID="+ Convert.ToInt32(cbTopicName.SelectedValue) +"";
                    iRes = objSQLdb.ExecuteSaveData(strCommand);

                    strCommand = "";

                    if (chkTopicName.Checked == false)
                    {
                        nTopicId = Convert.ToInt32(cbTopicName.SelectedValue.ToString());
                    }
                    else
                    {
                        string strCmd = "SELECT MAX(HTTMH_TOPIC_ID)+1 TopicId FROM HR_TRAINING_TOPIC_MASTER_HEAD";
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            nTopicId = Convert.ToInt32(dt.Rows[0]["TopicId"].ToString());
                        }
                    }
                    for (int i = 0; i < clbDepartments.Items.Count; i++)
                    {
                        if (clbDepartments.GetItemCheckState(i) == CheckState.Checked)
                        {
                            for (int j = 0; j < clbDesigs.Items.Count; j++)
                            {

                                if (clbDesigs.GetItemCheckState(j) == CheckState.Checked)
                                {                                  
                                    
                                    strCommand += "INSERT INTO HR_TRAINING_TOPIC_MASTER_DETL(HTTMD_TOPIC_ID " +
                                                                                          ", HTTMD_SL_NO " +
                                                                                          ", HTTMD_DEPT_ID " +
                                                                                          ", HTTMD_DESG_CODE " +
                                                                                          ", HTTMD_TOPIC_DURATION " +
                                                                                          ", HTTMD_TOPIC_DETAILS " +
                                                                                          ", HTTMD_METHOD_FLAG " +
                                                                                          ", HTTMD_METHOD_DETAILS " +
                                                                                          ", HTTMD_CREATED_BY " +
                                                                                          ", HTTMD_AUTHORIZED_BY " +
                                                                                          ", HTTMD_CREATED_DATE " +
                                                                                          ")VALUES(" + nTopicId +
                                                                                          "," + (nSlNo) +
                                                                                          ",'" + ((NewCheckboxListItem)(clbDepartments.Items[i])).Tag.ToString() +
                                                                                          "'," + ((NewCheckboxListItem)(clbDesigs.Items[j])).Tag.ToString() +
                                                                                          "," + Convert.ToDouble(txtDuration.Text) +
                                                                                          ",'',' ','','" + CommonData.LogUserId +
                                                                                          "','',getdate())";
                                    nSlNo++;
                                }

                            }
                        }
                    }
             

                if (strCommand.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;

        }

      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbTopicType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbTopicType.SelectedIndex > 0)
            {
                FillTopicNames();
            }
        }
  
        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            nTopicId = 0;          
           
            if (chkTopicName.Checked == true)
            {
                txtTopicName.Text = "";
            }
            else if (chkTopicName.Checked == false)
            {
                cbTopicName.SelectedIndex = -1;
            }

            if (chkTopicType.Checked == true)
            {
                txtTopicType.Text = "";
            }
            else if (chkTopicType.Checked == false)
            {
                cbTopicType.SelectedIndex = 0;
            }

            chkTopicName.Checked = false;
            chkTopicType.Checked = false;
            txtDuration.Text = "";

            for (int i = 0; i < clbDepartments.Items.Count; i++)
            {
                clbDepartments.SetItemCheckState(i, CheckState.Unchecked);

            }

            clbDesigs.Items.Clear();

            
        }
      

        private void FillTrainingTopicDetails()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            sDepartments = "";
            string DeptName = "";
            string Desig = "";

            if (cbTopicName.SelectedIndex > 0)
            {
                try
                {
                    dt = objHRdb.GetTrainingTopicDetails(Convert.ToInt32(cbTopicName.SelectedValue)).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;
                       
                        txtDuration.Text = dt.Rows[0]["TopicDuration"].ToString();
                        clbDesigs.Items.Clear();
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (sDepartments != dt.Rows[i]["DeptId"].ToString())
                            {
                                sDepartments = dt.Rows[i]["DeptId"].ToString();
                                FillDesignationsData(sDepartments);
                            }

                            if (DeptName != dt.Rows[i]["DeptName"].ToString())
                            {
                                DeptName = dt.Rows[i]["DeptName"].ToString();

                                for (int j = 0; j < clbDepartments.Items.Count; j++)
                                {
                                    if (((NewCheckboxListItem)(clbDepartments.Items[j])).Text.Equals(DeptName))
                                    {
                                        clbDepartments.SetSelected(j, true);
                                        clbDepartments.SetItemChecked(j, true);
                                    }
                                }
                            }
                        }
                        for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                        {

                            Desig = dt.Rows[iVar]["DesigId"].ToString();

                            for (int k = 0; k < clbDesigs.Items.Count; k++)
                            {
                                if (((NewCheckboxListItem)(clbDesigs.Items[k])).Tag.Equals(Desig))
                                {
                                    clbDesigs.SetSelected(k, true);
                                    clbDesigs.SetItemChecked(k, true);
                                }
                            }



                        }
                    }
                    else
                    {
                        flagUpdate = false;

                        for (int i = 0; i < clbDepartments.Items.Count; i++)
                        {
                            clbDepartments.SetItemCheckState(i, CheckState.Unchecked);                            
                        }
                        if (clbDesigs.Items.Count > 0)
                        {
                            clbDesigs.Items.Clear();
                        }

                       
                        

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRdb = null;
                    dt = null;
                }
            }
            else
            {
                flagUpdate = false;

                for (int i = 0; i < clbDepartments.Items.Count; i++)
                {
                    clbDepartments.SetItemCheckState(i, CheckState.Unchecked);

                }

                clbDesigs.Items.Clear();
                


            }

        }


        private void cbTopicName_SelectedIndexChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (cbTopicName.SelectedIndex > 0)
            {
                try
                {

                    FillTrainingTopicDetails();
                   

                    if (flagUpdate == false)
                    {
                        string strCmd = "SELECT HTTMH_TOPIC_ID,HTTMH_TOPIC_NAME,HTTMH_TOPIC_TIME " +
                                        " FROM HR_TRAINING_TOPIC_MASTER_HEAD " +
                                        " WHERE HTTMH_TOPIC_NAME='" + cbTopicName.Text.ToString() + "' ";
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            txtDuration.Text = dt.Rows[0]["HTTMH_TOPIC_TIME"].ToString();
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
                txtDuration.Text = "";
                for (int i = 0; i < clbDepartments.Items.Count; i++)
                {
                    clbDepartments.SetItemCheckState(i, CheckState.Unchecked);

                }

                clbDesigs.Items.Clear();

            }


        }


        private void txtTopicName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtTopicType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void clbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
          
           
                clbDesigs.Items.Clear();
                sDepartments = "";

                if (clbDepartments.SelectedItems != null)
                {
                    for (int i = 0; i < clbDepartments.Items.Count; i++)
                    {
                        if (clbDepartments.GetItemCheckState(i) == CheckState.Checked)
                        {
                            sDepartments += ((SSAdmin.NewCheckboxListItem)(clbDepartments.Items[i])).Tag.ToString() + ",";

                        }
                    }
                    sDepartments = sDepartments.TrimEnd(',');
                    if (sDepartments.Length > 2)
                    {
                        FillDesignationsData(sDepartments);
                    }
                }
            
        }

        private void chkAllDepartments_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDepartments.Checked == true)
            {
                for (int i = 0; i < clbDepartments.Items.Count; i++)
                {
                    clbDepartments.SetItemCheckState(i, CheckState.Checked);

                }

                for (int i = 0; i < clbDepartments.Items.Count; i++)
                {

                    if (clbDepartments.GetItemCheckState(i) == CheckState.Checked)
                    {

                        sDepartments += ((SSAdmin.NewCheckboxListItem)(clbDepartments.Items[i])).Tag.ToString() + ",";

                    }
                }
                sDepartments = sDepartments.TrimEnd(',');
                if (sDepartments.Length > 2)
                {
                    FillDesignationsData(sDepartments);
                }

            }
            else
            {
                clbDesigs.Items.Clear();

                for (int i = 0; i < clbDepartments.Items.Count; i++)
                {
                    clbDepartments.SetItemCheckState(i, CheckState.Unchecked);
                }

            }
        }

        private void chkAllDesignations_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDesignations.Checked == true)
            {
                for (int i = 0; i < clbDesigs.Items.Count; i++)
                {
                    clbDesigs.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                for (int i = 0; i < clbDesigs.Items.Count; i++)
                {
                    clbDesigs.SetItemCheckState(i, CheckState.Unchecked);
                }

            }

        }

     
    }
}
