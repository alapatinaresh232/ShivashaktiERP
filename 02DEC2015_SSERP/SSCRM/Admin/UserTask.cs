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
using SSTrans;
namespace SSCRM
{
    public partial class UserTask : Form
    {
        private Master objMaster = null;
        private UserTaskDB objTask = null;
        private SQLDB objDA = null;
        public UserTask()
        {
            InitializeComponent();
        }

        private void ProductSearch_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 120, Screen.PrimaryScreen.WorkingArea.Y + 20);
            //this.StartPosition = FormStartPosition.CenterScreen;
            FillUsers();
        }

        private void FillUsers()
        {
            objMaster = new Master();
            DataTable dt=null;
            try
            {

                dt = objMaster.UserMasterList_Get(CommonData.CompanyCode, CommonData.BranchCode).Tables[0];

                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["UM_USER_ID"] + "" != "")
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["UM_USER_ID"].ToString();
                        oclBox.Text = dataRow["UM_USER_NAME"].ToString() + " ( " + dataRow["UM_USER_ID"].ToString() + " )";
                        clbUsers.Items.Add(oclBox);
                        oclBox = null;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objMaster = null;
            }
        }

        private void FillTasks()
        {
            objTask = new UserTaskDB();
            DataTable dt = null;
            
            try
            {
                if (clbUsers.SelectedIndex > -1)
                {
                    dt = objTask.UserTasks_Get(((NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString()).Tables[0];

                    TreeNode tNode1 = new TreeNode();
                    tNode1.Text = "Main Menu";
                    tNode1.Tag = "Main Menu";
                    treeView1.Nodes.Add(tNode1);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeNode tNode = new TreeNode();
                        tNode.Text = dt.Rows[i]["Caption"].ToString();
                        tNode.Tag = dt.Rows[i]["VALUE_MEMBER"].ToString();
                        string[] strLevel = dt.Rows[i]["VALUE_MEMBER"].ToString().Split('^');
                        int[] arrLevel = new int[7];
                        for (int j = 0; j < 7; j++)
                        {
                            arrLevel[j] = Convert.ToInt32(strLevel[j]);
                        }

                        if (arrLevel[0] != 0 && arrLevel[1] == 0 && arrLevel[2] == 0 && arrLevel[3] == 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                        {
                            treeView1.Nodes[0].Nodes.Add(tNode);
                        }
                        else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] == 0 && arrLevel[3] == 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                        {
                            treeView1.Nodes[0].Nodes[arrLevel[0] - 1].Nodes.Add(tNode);
                        }
                        else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] == 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                        {
                            treeView1.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes.Add(tNode);
                        }
                        else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                        {
                            treeView1.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes.Add(tNode);
                        }
                        else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] != 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                        {
                            treeView1.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes[arrLevel[3] - 1].Nodes.Add(tNode);
                        }
                        else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] != 0 && arrLevel[5] != 0 && arrLevel[6] == 0)
                        {
                            treeView1.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes[arrLevel[3] - 1].Nodes[arrLevel[4] - 1].Nodes.Add(tNode);
                        }
                        else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] != 0 && arrLevel[5] != 0 && arrLevel[6] != 0)
                        {
                            treeView1.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes[arrLevel[3] - 1].Nodes[arrLevel[4] - 1].Nodes[arrLevel[5] - 1].Nodes.Add(tNode);
                        }
                        if (dt.Rows[i]["STATUS"].ToString() == "E")
                        {
                            tNode.Checked = true;
                        }
                        else
                        {
                            tNode.Checked = false;
                        }
                    }

                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            treeView1.Nodes[0].Expand();
            treeView1.Nodes[0].Nodes[0].Expand();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            for (int i = 0; i < clbUsers.Items.Count; i++)
            {
                clbUsers.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private bool CheckData()
        {
            bool blVil = false;
          TreeNodeCollection  nodes = this.treeView1.Nodes;

            foreach (TreeNode n in nodes)
            {
              blVil =  GetNodeRecursive(n);
            }
            return blVil;
        }
        private bool GetNodeRecursive(TreeNode treeNode)
        {
            bool flag=false;
            if (treeNode.StateImageIndex == 1 || treeNode.StateImageIndex == 2)
            {
                flag = true;
            }
            if (flag == false)
            {
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    flag = GetNodeRecursive(tn);
                }
            }
            return flag;
        }
        private void GetNodeRecursive(TreeNode treeNode,StringBuilder sbSQL)
        {
            if (treeNode.StateImageIndex == 1 || treeNode.StateImageIndex == 2)
            {
                string[] strArrTask = null;
                String strTask = treeNode.Tag + "";
                strArrTask = strTask.Split('^');
                if (strArrTask.Length > 1)
                {
                    sbSQL.Append(" INSERT INTO USER_TASKS(UT_USER_ID, UT_COMPANY_ID, UT_TASK_LVL1, UT_TASK_LVL2, UT_TASK_LVL3 " +
                                ", UT_TASK_LVL4, UT_TASK_LVL5, UT_TASK_LVL6, UT_TASK_LVL7 " +
                                ", UT_TASK_STATUS, UT_CREATED_BY, UT_CREATED_DATE) " +
                                " VALUES ('" + ((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString() + "', 'NFL'" +
                                ", " + strArrTask[0].ToString() + ", " + strArrTask[1].ToString() +
                                ", " + strArrTask[2].ToString() + ", " + strArrTask[3].ToString() +
                                ", " + strArrTask[4].ToString() + ", " + strArrTask[5].ToString() +
                                ", " + strArrTask[6].ToString() + ", 'E','" + CommonData.LogUserId +
                                "', getdate()); ");
                }
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                GetNodeRecursive(tn,sbSQL);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void clbUsers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //clbTasks.Items.Clear();
            treeView1.Nodes.Clear();
            for (int i = 0; i < clbUsers.Items.Count; i++)
            {
                if (e.Index != i)
                    clbUsers.SetItemCheckState(i, CheckState.Unchecked);
            }
            if (e.NewValue == CheckState.Checked)
                FillTasks();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string[] strArrTask = null;
            StringBuilder sbSQL = new StringBuilder();
            string strSQL = string.Empty;
            try
            {
                if (CheckData())
                {
                    objDA = new SQLDB();
                    strSQL = "DELETE FROM USER_TASKS WHERE UT_USER_ID='" + ((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString() + "'";
                    int del = objDA.ExecuteSaveData(strSQL);
                   
                    TreeNodeCollection nodes = this.treeView1.Nodes;

                    foreach (TreeNode n in nodes)
                    {
                        GetNodeRecursive(n,sbSQL);
                    }

                    strSQL = sbSQL.ToString().Substring(0, sbSQL.ToString().Length - 1);
                    int Rec = objDA.ExecuteSaveData(strSQL);
                    if (Rec > 0)
                    {
                        MessageBox.Show("User Tasks Saved");
                        treeView1.Nodes.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Select atleast one task!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                objDA = null;

            }
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void SearchEcode(string searchString, ListBox cbEcodes)
        {
            if (searchString.Trim().Length > 0)
            {
                for (int i = 0; i < cbEcodes.Items.Count; i++)
                {
                    if (cbEcodes.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
                    {
                        if (((System.Data.DataRowView)(cbEcodes.Items[i])).Row.ItemArray[1].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }
                    else  // for checkbox list search
                    {
                        if (cbEcodes.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }

                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //treeView1.Update();
            TriStateTreeView.getStatus(e);
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbUsers);
        }
        /*private void FillTasks()
        {
            objTask = new UserTaskDB();
            DataTable dt = null;
            tvTasks.Nodes.Clear();
            int intNode = 0;
            try
            {
                if (clbUsers.SelectedIndex > -1)
                {
                    dt = objTask.UserTasks_Get(((NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString()).Tables[0];

                    //tvTasks.Nodes.Add("Tasks");

                    foreach (DataRow dataRow in dt.Rows)
                    {

                        if (Convert.ToInt16(dataRow["Level1"]) != intNode)
                        {
                            intNode = Convert.ToInt16(dataRow["Level1"]);
                            TreeNode rootNode = new TreeNode();
                            rootNode.Tag = dataRow["Level1"].ToString();
                            rootNode.Text = dataRow["Caption"].ToString();
                            tvTasks.Nodes.Add(rootNode); //dataRow["Level1"].ToString(), dataRow["Caption"].ToString());
                            RecurseFolders(dataRow["Level1"].ToString(), rootNode);
                            //if (Convert.ToString(dataRow["USERID"] + "") != "")
                            //    tvTasks.Nodes[0].Checked = true;

                        }
                        else
                        {

                        }
                              
                    }





                   /* if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0 && Convert.ToInt16(dataRow["Level5"]) > 0 && Convert.ToInt16(dataRow["Level6"]) > 0 && Convert.ToInt16(dataRow["Level7"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level7"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level7"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    else if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0 && Convert.ToInt16(dataRow["Level5"]) > 0 && Convert.ToInt16(dataRow["Level6"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level6"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level6"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    else if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0 && Convert.ToInt16(dataRow["Level5"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level5"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level5"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    else if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level4"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level4"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    else if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level3"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes.Add(dataRow["Level3"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    else if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level2"]);
                        tvTasks.Nodes[0].Nodes.Add(dataRow["Level2"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    else if (Convert.ToInt16(dataRow["Level1"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level1"]);
                        tvTasks.Nodes.Add(dataRow["Level1"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    }*/

        /*foreach (DataRow dataRow in dt.Rows)
                {

                    if (Convert.ToInt16(dataRow["Level1"]) != intNode)
                    {
                        intNode = Convert.ToInt16(dataRow["Level1"]);
                        tvTasks.Nodes.Add(dataRow["Level1"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                }
                intNode = 0;
                foreach (DataRow dataRow in dt.Rows)
                {

                    if (Convert.ToInt16(dataRow["Level2"]) != intNode && Convert.ToInt16(dataRow["Level2"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level2"]);
                        tvTasks.Nodes[0].Nodes.Add(dataRow["Level2"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                    else
                    {
                    }
                }

                intNode = 0;
                foreach (DataRow dataRow in dt.Rows)
                {

                    if (Convert.ToInt16(dataRow["Level3"]) != intNode && Convert.ToInt16(dataRow["Level3"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level3"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes.Add(dataRow["Level3"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                }
                intNode = 0;
                foreach (DataRow dataRow in dt.Rows)
                {

                    if (Convert.ToInt16(dataRow["Level4"]) != intNode && Convert.ToInt16(dataRow["Level4"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level4"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level4"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                }
                intNode = 0;
                foreach (DataRow dataRow in dt.Rows)
                {

                    if (Convert.ToInt16(dataRow["Level5"]) != intNode && Convert.ToInt16(dataRow["Level5"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level5"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level5"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                }
                intNode = 0;
                foreach (DataRow dataRow in dt.Rows)
                {

                    if (Convert.ToInt16(dataRow["Level6"]) != intNode && Convert.ToInt16(dataRow["Level6"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level6"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level6"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                }
                intNode = 0;
                foreach (DataRow dataRow in dt.Rows)
                {

                    if (Convert.ToInt16(dataRow["Level7"]) != intNode && Convert.ToInt16(dataRow["Level7"]) > 0)
                    {
                        intNode = Convert.ToInt16(dataRow["Level7"]);
                        tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level7"].ToString(), dataRow["Caption"].ToString());
                        if (Convert.ToString(dataRow["USERID"] + "") != "")
                            tvTasks.Nodes[0].Checked = true;

                    }
                }
                  */

        /*         if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) == 0 && Convert.ToInt16(dataRow["Level3"]) == 0 && Convert.ToInt16(dataRow["Level4"]) == 0 && Convert.ToInt16(dataRow["Level5"]) == 0 && Convert.ToInt16(dataRow["Level6"]) == 0 && Convert.ToInt16(dataRow["Level7"]) == 0)
                        {
                            tvTasks.Nodes[0].Nodes.Add(dataRow["Level1"].ToString(), dataRow["Caption"].ToString());
                            if (Convert.ToString(dataRow["USERID"] + "") != "")
                                tvTasks.Nodes[0].Checked = true;

                        }
                        if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) == 0 && Convert.ToInt16(dataRow["Level4"]) == 0 && Convert.ToInt16(dataRow["Level5"]) == 0 && Convert.ToInt16(dataRow["Level6"]) == 0 && Convert.ToInt16(dataRow["Level7"]) == 0)
                        {
                            intNode = Convert.ToInt16(dataRow["Level1"]);
                            tvTasks.Nodes[0].Nodes[0].Nodes.Add(dataRow["Level2"].ToString(), dataRow["Caption"].ToString());
                            if (Convert.ToString(dataRow["USERID"] + "") != "")
                                tvTasks.Nodes[0].Checked = true;

                        }
                        if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) == 0 && Convert.ToInt16(dataRow["Level5"]) == 0 && Convert.ToInt16(dataRow["Level6"]) == 0 && Convert.ToInt16(dataRow["Level7"]) == 0)
                        {
                            intNode = Convert.ToInt16(dataRow["Level2"]);
                            tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level3"].ToString(), dataRow["Caption"].ToString());
                            if (Convert.ToString(dataRow["USERID"] + "") != "")
                                tvTasks.Nodes[0].Checked = true;

                        }
                        if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0 && Convert.ToInt16(dataRow["Level5"]) == 0 && Convert.ToInt16(dataRow["Level6"]) == 0 && Convert.ToInt16(dataRow["Level7"]) == 0)
                        {
                            intNode = Convert.ToInt16(dataRow["Level3"]);
                            tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level4"].ToString(), dataRow["Caption"].ToString());
                            if (Convert.ToString(dataRow["USERID"] + "") != "")
                                tvTasks.Nodes[0].Checked = true;


                        }
                        if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0 && Convert.ToInt16(dataRow["Level5"]) > 0 && Convert.ToInt16(dataRow["Level6"]) == 0 && Convert.ToInt16(dataRow["Level7"]) == 0)
                        {
                            intNode = Convert.ToInt16(dataRow["Level4"]);
                            tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level5"].ToString(), dataRow["Caption"].ToString());
                            if (Convert.ToString(dataRow["USERID"] + "") != "")
                                tvTasks.Nodes[0].Checked = true;


                        }
                        if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0 && Convert.ToInt16(dataRow["Level5"]) > 0 && Convert.ToInt16(dataRow["Level6"]) > 0 && Convert.ToInt16(dataRow["Level7"]) == 0)
                        {
                            intNode = Convert.ToInt16(dataRow["Level5"]);
                            tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level6"].ToString(), dataRow["Caption"].ToString());
                            if (Convert.ToString(dataRow["USERID"] + "") != "")
                                tvTasks.Nodes[0].Checked = true;


                        }
                        if (Convert.ToInt16(dataRow["Level1"]) > 0 && Convert.ToInt16(dataRow["Level2"]) > 0 && Convert.ToInt16(dataRow["Level3"]) > 0 && Convert.ToInt16(dataRow["Level4"]) > 0 && Convert.ToInt16(dataRow["Level5"]) > 0 && Convert.ToInt16(dataRow["Level6"]) > 0 && Convert.ToInt16(dataRow["Level7"]) > 0)
                        {
                            intNode = Convert.ToInt16(dataRow["Level6"]);
                            tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dataRow["Level7"].ToString(), dataRow["Caption"].ToString());
                            if (Convert.ToString(dataRow["USERID"] + "") != "")
                                tvTasks.Nodes[0].Checked = true;

                        }


                        for (int i = 0; i < dataRow.ItemArray.Length - 5; i++)
                        {
                            if (Convert.ToUInt16(dataRow.ItemArray[i]) == intNode)
                            {
                                if (intNode == 1)
                                    tvTasks.Nodes[0].Nodes[0].Nodes.Add(intNode.ToString(), dataRow.ItemArray[7].ToString());
                                if (intNode == 2)
                                    tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(intNode.ToString(), dataRow.ItemArray[7].ToString());
                                if (intNode == 3)
                                    tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(intNode.ToString(), dataRow.ItemArray[7].ToString());
                                if (intNode == 4)
                                    tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(intNode.ToString(), dataRow.ItemArray[7].ToString());
                                if (intNode == 5)
                                    tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(intNode.ToString(), dataRow.ItemArray[7].ToString());
                                if (intNode == 6)
                                    tvTasks.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes.Add(intNode.ToString(), dataRow.ItemArray[7].ToString());
                            }
                        }
                        this.tvTasks.SelectedNode.Expand();
                    }
                
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objMaster = null;
            }
        }


        //public void RecurseFolders(string path, TreeNode node)
        //{
        //    var dir = new DirectoryInfo(path);
        //    node.Text = dir.Name;
        //    node.Tag = dir.Name;
        //    try
        //    {
        //        foreach (var subdir in dir.GetDirectories())
        //        {
        //            var childnode = new TreeNode();
        //            node.Nodes.Add(childnode);

        //            RecurseFolders(subdir.FullName, childnode);
        //        }
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        // TODO:  write some handler to log and/or deal with 
        //        // unauthorized exception cases
        //    }

        //    foreach (var fi in dir.GetFiles().OrderBy(c => c.Name))
        //    {
        //        var fileNode = new TreeNode(fi.Name);
        //        node.Nodes.Add(fileNode);
        //    }
        //}
                */

     
    }
}
