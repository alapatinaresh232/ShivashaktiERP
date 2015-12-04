using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SDMS
{
    public partial class TaskMaster : Form
    {
        String strTreeLvl = null;
        bool flagCheck = false;
        TreeNode selectedNode = new TreeNode();
        SQLDB objSQLData = new SQLDB();
        public TaskMaster()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void TaskMaster_Load(object sender, EventArgs e)
        {
           
            FillTasks();
        }
        private void FillTasks()
        {
            TreeNode tNode1 = new TreeNode();
            tNode1.Text = "Main Menu";
            tNode1.Tag = "Main Menu";
            tvTasks.Nodes.Add(tNode1);
            DataTable dt = null;
            objSQLData = new SQLDB();
            try
            {
                string strCommand = "SELECT CAST(TM_TASK_LVL1 AS VARCHAR)+'^'+CAST(TM_TASK_LVL2 AS VARCHAR)+'^'+CAST(TM_TASK_LVL3 AS VARCHAR)+'^'+CAST(TM_TASK_LVL4 AS VARCHAR)+'^'+CAST(TM_TASK_LVL5 AS VARCHAR)+'^'+CAST(TM_TASK_LVL6 AS VARCHAR)+'^'+CAST(TM_TASK_LVL7 AS VARCHAR) AS VALUE_MEMBER,TM_LABEL,TM_PROGRAM_NAME FROM DMS_TASK_MASTER order by TM_TASK_LVL1,TM_TASK_LVL2,TM_TASK_LVL3,TM_TASK_LVL4,TM_TASK_LVL5,TM_TASK_LVL6,TM_TASK_LVL7";
                dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];

               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode tNode = new TreeNode();
                    tNode.Text = dt.Rows[i]["TM_LABEL"].ToString();
                    tNode.Tag = dt.Rows[i]["VALUE_MEMBER"].ToString();
                    string[] strLevel = dt.Rows[i]["VALUE_MEMBER"].ToString().Split('^');
                    int[] arrLevel = new int[7];
                    for (int j = 0; j < 7; j++)
                    {
                        arrLevel[j] = Convert.ToInt32(strLevel[j]);
                    }

                    if (arrLevel[0] != 0 && arrLevel[1] == 0 && arrLevel[2] == 0 && arrLevel[3] == 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                    {
                        tvTasks.Nodes[0].Nodes.Add(tNode);
                    }
                    else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] == 0 && arrLevel[3] == 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                    {
                        tvTasks.Nodes[0].Nodes[arrLevel[0] - 1].Nodes.Add(tNode);
                    }
                    else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] == 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                    {
                        tvTasks.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes.Add(tNode);
                    }
                    else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] == 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                    {
                        tvTasks.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes.Add(tNode);
                    }
                    else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] != 0 && arrLevel[5] == 0 && arrLevel[6] == 0)
                    {
                        tvTasks.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes[arrLevel[3] - 1].Nodes.Add(tNode);
                    }
                    else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] != 0 && arrLevel[5] != 0 && arrLevel[6] == 0)
                    {
                        tvTasks.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes[arrLevel[3] - 1].Nodes[arrLevel[4] - 1].Nodes.Add(tNode);
                    }
                    else if (arrLevel[0] != 0 && arrLevel[1] != 0 && arrLevel[2] != 0 && arrLevel[3] != 0 && arrLevel[4] != 0 && arrLevel[5] != 0 && arrLevel[6] != 0)
                    {
                        tvTasks.Nodes[0].Nodes[arrLevel[0] - 1].Nodes[arrLevel[1] - 1].Nodes[arrLevel[2] - 1].Nodes[arrLevel[3] - 1].Nodes[arrLevel[4] - 1].Nodes[arrLevel[5] - 1].Nodes.Add(tNode);
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
                objSQLData = null;
            }
            tvTasks.Nodes[0].Expand();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objSQLData = new SQLDB();
                try
                {
                    strTreeLvl = selectedNode.Tag + "";
                    String[] strArrLvl = strTreeLvl.Split('^');
                    int[] iArrLvl = new int[7];
                    if (strArrLvl.Length > 1)
                    {
                        int iFlag = 0;
                        for (int iVar = 0; iVar < strArrLvl.Length; iVar++)
                        {
                            iArrLvl[iVar] = Convert.ToInt32(strArrLvl[iVar]);
                            if (iArrLvl[iVar] == 0 && iFlag == 0)
                            {
                                int pos = (selectedNode.Nodes.Count + 1);
                                iArrLvl[iVar] = pos;
                                iFlag++;
                            }
                        }
                    }
                    else
                    {
                        iArrLvl[0] = selectedNode.Nodes.Count + 1;
                    }
                    String strCommand = "INSERT INTO DMS_TASK_MASTER(" +
                                        "TM_TASK_LVL1,TM_TASK_LVL2" +
                                        ",TM_TASK_LVL3,TM_TASK_LVL4" +
                                        ",TM_TASK_LVL5,TM_TASK_LVL6" +
                                        ",TM_TASK_LVL7,TM_LABEL" +
                                        ",TM_PROGRAM_NAME) " +
                                        "VALUES(" + iArrLvl[0] +
                                        "," + iArrLvl[1] +
                                        "," + iArrLvl[2] +
                                        "," + iArrLvl[3] +
                                        "," + iArrLvl[4] +
                                        "," + iArrLvl[5] +
                                        "," + iArrLvl[6] +
                                        ",'" + txtLable.Text +
                                        "','" + txtProgramName.Text + "');";
                    int iRes = objSQLData.ExecuteSaveData(strCommand);
                    if (iRes > 0)
                    {
                        tvTasks.Nodes.Clear();
                        FillTasks();
                        MessageBox.Show("Task is Saved");
                        txtLable.Text = "";
                        txtProgramName.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                }
            }

        }
        //private int getLvlPosition(String[] strTreeLvl)
        //{
        //    int position = 0;
        //    for (int iVar = 0; iVar < strTreeLvl.Length; iVar++)
        //    {
        //        if (strTreeLvl[i] == 0)
        //        {
        //            position = iVar;
        //            return (position+1);
        //        }
        //        return (position+1);
        //    }
        //}
        private bool CheckData()
        {
            bool flag = true;
            if (txtLable.Text == "")
            {
                flag = false;
                MessageBox.Show("Enter Task Lable", "Task Master",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (txtProgramName.Text == "")
            {
                flag = false;
                MessageBox.Show("Enter Program Name", "Task Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(CallNodeSelector() == false)
            {
                flag = false;
                MessageBox.Show("Select Task", "Task Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return flag;
        }
        private bool CallNodeSelector()
        {
            flagCheck = false;
            TreeNodeCollection nodes = this.tvTasks.Nodes;
            foreach (TreeNode n in nodes)
            {
                GetNodeRecursive(n);
            }
            return flagCheck;
        }
        private bool GetNodeRecursive(TreeNode treeNode)
        {

            bool flag = false;

            if (treeNode.Checked)
            {
                selectedNode = treeNode;
                flagCheck = true;
                return flagCheck;
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                flag = GetNodeRecursive(tn);
            }
            return flag;
        }
    }
}
