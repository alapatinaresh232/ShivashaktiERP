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
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class OrderFrmCanclFineCircular : Form
    {
        bool flagUpdate = false;
        SQLDB objDB = null;
        string strCmpData = "";
        string strBranchData = "";
        public OrderFrmCanclFineCircular()
        {
            InitializeComponent();
        }

        private void OrderFrmCanclFineCircular_Load(object sender, EventArgs e)
        {
            FillRoles();
            FillCompanyData();
            FillDefaultGridData();
        }

        private void FillDefaultGridData()
        {
            gvDiscountDetails.Rows.Add();
            gvDiscountDetails.Rows[0].Cells["SLNO"].Value = 1;
            gvDiscountDetails.Rows[0].Cells["perFrom"].Value = 0;
            gvDiscountDetails.Rows[0].Cells["perTo"].Value = 100;
            gvDiscountDetails.Rows[0].Cells["FineAmt"].Value = 0;
        }
        private void FillRoles()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("SR", "SALES REPRESENTATIVE");
            table.Rows.Add("GL", "GROUP LEADER");
            table.Rows.Add("AB", "TEAM LEADER & ABOVE");
            cbRole.DataSource = table;
            cbRole.DisplayMember = "name";
            cbRole.ValueMember = "type";
        }
        private void FillCompanyData()
        {
            try
            {
               Security objSecurity = new Security();
                DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
             
                for (int i = 0; i < dtCpy.Rows.Count;i++ )
                {
                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = dtCpy.Rows[i]["CM_Company_Code"].ToString();
                    oclBox.Text = dtCpy.Rows[i]["CM_Company_Name"].ToString();
                    clbCompany.Items.Add(oclBox);
                    oclBox = null;
                }
                objSecurity = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillBranchData()
        {
            try
            {
                tvBranches.Nodes.Clear();
                string strCmd = "";
                if (strCmpData.Length == 0)
                {
                    //((ListBox)clbBranch).DataSource = null;
                    tvBranches.Nodes.Clear();
                    chkBranchAll.Checked = false;
                }
                else
                {
                    string str = strCmpData.TrimEnd(',');
                   
                    string [] strArr = str.Split(',');

                    for (int l = 0; l < strArr.Length;l++ )
                    {

                        DataSet dsState = new DataSet();
                        string strrr = strArr[l].TrimEnd();
                        dsState = Get_UserBranchStateFilterCursor(strArr[l], "", CommonData.LogUserId, "", "STATE");

                        if (dsState.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes.Add(dsState.Tables[0].Rows[j]["StateCode"].ToString(), dsState.Tables[0].Rows[j]["StateName"].ToString());

                                DataSet dschild = new DataSet();
                                dschild = Get_UserBranchStateFilterCursor(strArr[l], dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "BR", "CHILD");

                                if (dschild.Tables[0].Rows.Count > 0)
                                {
                                    for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                    {
                                        tvBranches.Nodes[j].Nodes.Add(dschild.Tables[0].Rows[k]["BranCode"].ToString() + "@" + dschild.Tables[0].Rows[k]["COMPANY_CODE"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                    }
                                }


                            }
                        }
                    }



                    //((ListBox)clbBranch).DataSource = dt;
                    //((ListBox)clbBranch).DisplayMember = "BRANCH_NAME";
                    //((ListBox)clbBranch).ValueMember = "BRANCH_CODE";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chkCompAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompAll.Checked == true)
            {
                strCmpData="";
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Checked);
                    strCmpData += "'" + ((NewCheckboxListItem)clbCompany.Items[i]).Tag + "',";
                }
                //strCmpData = "NFL,NKBPL,SATL,SBTLNPL,SHS,SLAF,SSBPL,VNF";
                clbCompany.Enabled = false;
                FillBranchData();
                chkBranchAll.Checked = true;
                
            }
            else
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Unchecked);
                }
                strCmpData = "";
                clbCompany.Enabled = true;
                FillBranchData();
                chkBranchAll.Checked = false;
               
            }
        }

        private void chkBranchAll_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkBranchAll.Checked == true)
                CheckAllNodes(tvBranches.Nodes);
            else
                UncheckAllNodes(tvBranches.Nodes);
        }
        public void CheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true;
                CheckChildren(node, true);
            }
        }
        public void UncheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                CheckChildren(node, false);
            }
        }

        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
            }
        }
        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            strCmpData = "";
            try
            {
                for (int i = 0; i < clbCompany.CheckedItems.Count;i++ )
                {
                    strCmpData += ""+((NewCheckboxListItem)clbCompany.CheckedItems[i]).Tag + ",";
                    //strCmpData += "" + (view[clbCompany.ValueMember].ToString()) + "";
                }
                if (strCmpData.Length > 0)
                {
                    strCmpData = strCmpData.Substring(0, strCmpData.Length - 1);

                }
            }
            catch
            {

            }
            FillBranchData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private DataSet Get_UserBranchStateFilterCursor(string sCompCode, string sStateCode, string sLogUserId, string sBranchtType, string sGetType)
        {
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objDB.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[2] = objDB.CreateParameter("@sUser", DbType.String, sLogUserId, ParameterDirection.Input);
                param[3] = objDB.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[4] = objDB.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("Get_UserBranchStateFilterCursor", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objDB = null;
            }
            return ds;
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvDiscountDetails.Rows.Clear();
            FillDefaultGridData();
        }

        private void gvDiscountDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Int32 nRowCnt = 0;

            nRowCnt = gvDiscountDetails.Rows.Count - 1;
            if (gvDiscountDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                gvDiscountDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == gvDiscountDetails.Columns["perTo"].Index && Convert.ToString(gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value) != "")
            {
                if (Convert.ToDouble(gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value) > 100)
                {
                    MessageBox.Show("To Percenage Should not Exceed than 100","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value = 100;
                }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == gvDiscountDetails.Columns["perTo"].Index || e.ColumnIndex == gvDiscountDetails.Columns["FineAmt"].Index)
            {


                if (Convert.ToString(gvDiscountDetails.Rows[e.RowIndex].Cells["perFrom"].Value) != ""
                    && Convert.ToString(gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value) != ""
                    && Convert.ToString(gvDiscountDetails.Rows[e.RowIndex].Cells["FineAmt"].Value) != "" && e.RowIndex == gvDiscountDetails.Rows.Count - 1)
                {
                    if (Convert.ToDouble(gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value) > Convert.ToDouble(gvDiscountDetails.Rows[e.RowIndex].Cells["perFrom"].Value) 
                        && Convert.ToDouble(gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value) != 100 )
                    {
                        gvDiscountDetails.Rows.Add();
                        gvDiscountDetails.Rows[e.RowIndex + 1].Cells["SLNO"].Value = gvDiscountDetails.Rows.Count;
                        gvDiscountDetails.Rows[e.RowIndex + 1].Cells["perFrom"].Value = (Convert.ToDouble(gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value)+0.1).ToString();
                        gvDiscountDetails.Rows[e.RowIndex + 1].Cells["perTo"].Value = 100;
                        gvDiscountDetails.Rows[e.RowIndex + 1].Cells["FineAmt"].Value = 0;
                        gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].ReadOnly = true;
                    }
                    if (Convert.ToDouble(gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value) < Convert.ToDouble(gvDiscountDetails.Rows[e.RowIndex].Cells["perFrom"].Value))
                    {
                        //flag = false;
                        MessageBox.Show("'To Percentage (" + gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value
                            + ")' Must Be Greater Than or Equal 'From Sales Volume(" + gvDiscountDetails.Rows[e.RowIndex].Cells["perFrom"].Value + ")' ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gvDiscountDetails.Rows[e.RowIndex].Cells["perTo"].Value = 100;
                    }
                }

            }
            if (e.ColumnIndex == gvDiscountDetails.Columns["FineAmt"].Index && Convert.ToString(gvDiscountDetails.Rows[e.RowIndex].Cells["FineAmt"].Value) != "")
            {

            }
        }

        private void gvDiscountDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 nRowCnt = 0;

            if (gvDiscountDetails.Rows.Count > 1)
            {
                nRowCnt = gvDiscountDetails.Rows.Count - 1;

                if (e.ColumnIndex == gvDiscountDetails.Columns["Delete"].Index)
                {
                    if (e.RowIndex == nRowCnt)
                    {
                        DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.Yes)
                        {
                            gvDiscountDetails.Rows[e.RowIndex - 1].Cells["perTo"].ReadOnly = false;

                            DataGridViewRow dgvr = gvDiscountDetails.Rows[e.RowIndex];
                            gvDiscountDetails.Rows.Remove(dgvr);

                            if (gvDiscountDetails.Rows.Count == 0)
                            {
                                //AddFirstRowToGrid();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("You Can Delete Only Last Record", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
        }

        private void gvDiscountDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }
        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != '.')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            
        }

        private void txtMaxGrps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMinOrderForms_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOtherFineAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(checkData())
            {
                try
                {
                    if (SaveCircularData() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void GetSelectedCompAndBranches()
        {
           
            strBranchData = "";
           

            bool iscomp = false;
            bool iSstate = false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                   

                        if (tvBranches.Nodes[i].Nodes[j].Checked == true)
                        {
                            if (strBranchData != string.Empty)
                                strBranchData += ",";
                            strBranchData += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                            iscomp = true;
                            iSstate = true;
                        }

                   
                    //if (iSstate == true)
                    //{
                    //    if (State != string.Empty)
                    //        State += ",";
                    //    State += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                    //}
                    iSstate = false;
                }

                //if (iscomp == true)
                //{
                //    if (Company != string.Empty)
                //        Company += ",";
                //    Company += tvBranches.Nodes[i].Name.ToString();
                //}
                //iscomp = false;
            }
        }

        private bool checkData()
        {
            bool flag = true;
            GetSelectedCompAndBranches();
            if(strCmpData.Length==0)
            {
                MessageBox.Show("Please Select Company ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                return flag;
            }
            if(strBranchData.Length==0)
            {
                MessageBox.Show("Please Select Branch", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                return flag;
            }
            if(gvDiscountDetails.Rows.Count == 1)
            {
                MessageBox.Show("Please Enter Percentages & Fine Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                return flag;
            }
            if (cbRole.SelectedValue.ToString() == "AB" && txtMaxGrps.Text.Length==0)
            {
                MessageBox.Show("Please Enter Groups", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                return flag;
            }
            //if(txtOtherFineDesc.Text.Length<1)
            //{
            //    MessageBox.Show("Please Enter Other Fine Amount Discription", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    flag = false;
            //    return flag;
            //}
            //if (txtOtherFineAmt.Text.Length == 0)
            //{
            //    MessageBox.Show("Please Enter Other Fine Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    flag = false;
            //    return flag;
            //}
            return flag;
        }

        private int SaveCircularData()
        {
            int iRes = 0;
            string [] str = strBranchData.Split(',');
            string strCMD = "";
            //if (txtMinOrderForms.Text.Length == 0)
            //    txtMinOrderForms.Text = "0";
            if (txtMaxGrps.Text.Length == 0)
                txtMaxGrps.Text = "0";
            //if (txtOtherFineAmt.Text.Length == 0)
            //    txtOtherFineAmt.Text = "0";
            for (int i = 0; i < str.Length;i++ )
            {
                for (int j = 0; j < gvDiscountDetails.Rows.Count;j++ )
                {
                    strCMD += " insert into ORDER_FORMS_FINE_CIRCULARS(OFFC_COMP_CODE,"+
                                                                        "OFFC_BRANCH_CODE,"+
                                                                        "OFFC_DOC_TYPE,"+
                                                                        "OFFC_EFF_DATE," +
                                                                        "OFFC_ROLE,"+
                                                                        "OFFC_SINO,"+
                                                                        "OFFC_FROM_PERC,"+
                                                                        "OFFC_TO_PERC,"+
                                                                        "OFFC_FINE_AMT,"+
                                                                        "OFFC_VALIDTO_GROUPS,"+
                                                                        "OFFC_OTHER_DESC,"+
                                                                        "OFFC_OTHER_FINE,"+
                                                                        "OFFC_MIN_ORDER_FORMS,"+
                                                                        "OFFC_CREATED_BY,"+
                                                                        "OFFC_CREATED_DATE"+                                                                        
                                                                        ") SELECT "+
                                                                        "'" + str[i].Split('@')[1] + "'," +
                                                                        "'" + str[i].Split('@')[0] + "'," +
                                                                        "'CANCELLING',"+
                                                                        "'"+dtpFDate.Value.ToString("dd/MMM/yyyy")+"',"+
                                                                        "'"+cbRole.SelectedValue+"',"+
                                                                        "" + gvDiscountDetails.Rows[j].Cells["SLNO"].Value + "," +
                                                                        "" + gvDiscountDetails.Rows[j].Cells["perFrom"].Value + "," +
                                                                        "" + gvDiscountDetails.Rows[j].Cells["perTo"].Value + "," +
                                                                        "" + gvDiscountDetails.Rows[j].Cells["FineAmt"].Value + "," +
                                                                        ""+txtMaxGrps.Text+","+
                                                                        "'',"+
                                                                        "0,"+
                                                                        "0,"+
                                                                        "'"+CommonData.LogUserId+"',"+
                                                                        "GETDATE() " +
                                                                        " WHERE NOT EXISTS (SELECT * FROM ORDER_FORMS_FINE_CIRCULARS WHERE OFFC_COMP_CODE='"+str[i].Split('@')[1]+
                                                                        "' AND OFFC_BRANCH_CODE='" + str[i].Split('@')[0] + "' AND OFFC_DOC_TYPE='CANCELLING' AND OFFC_EFF_DATE='"
                                                                        + dtpFDate.Value.ToString("dd/MMM/yyyy") + "' and OFFC_ROLE='"+cbRole.SelectedValue+"' AND OFFC_SINO=" + gvDiscountDetails.Rows[j].Cells["SLNO"].Value + ")";
                }
            }
            if(strCMD.Length>0)
            {
                objDB = new SQLDB();
                iRes = objDB.ExecuteSaveData(strCMD);
            }

            return iRes;
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.SelectedValue.ToString() == "AB")
            {
                txtMaxGrps.Enabled = true;
            }
            else
            {
                txtMaxGrps.Enabled = false;
            }
            FillData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            flagUpdate = false;
            for (int i = 0; i < clbCompany.Items.Count; i++)
            {
                clbCompany.SetItemCheckState(i, CheckState.Unchecked);
            }
            strCmpData = "";
            clbCompany.Enabled = true;
            FillBranchData();
            chkBranchAll.Checked = false;

            btnClearProd_Click(null,null);
            txtMaxGrps.Text = "";
            //txtMinOrderForms.Text = "";
            //txtOtherFineAmt.Text = "";
            //txtOtherFineDesc.Text = "";
        }

        private void dtpFDate_ValueChanged(object sender, EventArgs e)
        {
            FillData();
        }
        private void FillData()
        {
            if (strCmpData.Length > 0 && strBranchData.Length > 0)
            {
                objDB = new SQLDB();
                SqlParameter[] param = new SqlParameter[5];
                DataSet ds = new DataSet();
                string[] strBranch = strBranchData.Split(',');

                try
                {
                    param[0] = objDB.CreateParameter("@xCompanyCode", DbType.String, strBranch[0].Split('@')[1], ParameterDirection.Input);
                    param[1] = objDB.CreateParameter("@xBranchCode", DbType.String, strBranch[0].Split('@')[0], ParameterDirection.Input);
                    param[2] = objDB.CreateParameter("@xEffDate", DbType.String, dtpFDate.Value.ToString("dd/MMM/yyyy"), ParameterDirection.Input);
                    param[3] = objDB.CreateParameter("@xEmpRole", DbType.String, cbRole.SelectedValue, ParameterDirection.Input);
                    param[4] = objDB.CreateParameter("@xDocType", DbType.String, "CANCELLING", ParameterDirection.Input);
                    ds = objDB.ExecuteDataSet("GetCancelFineCircularDetails", CommandType.StoredProcedure, param);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {

                        flagUpdate = true;
                        btnDelete.Enabled = true;
                        txtMaxGrps.Text = dt.Rows[0]["OFFC_VALIDTO_GROUPS"].ToString();
                        //txtOtherFineAmt.Text = dt.Rows[0]["OFFC_OTHER_FINE"].ToString();
                        //txtOtherFineDesc.Text = dt.Rows[0]["OFFC_OTHER_DESC"].ToString();
                        //txtMinOrderForms.Text = dt.Rows[0]["OFFC_MIN_ORDER_FORMS"].ToString();
                        gvDiscountDetails.Rows.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvDiscountDetails.Rows.Add();
                            gvDiscountDetails.Rows[i].Cells["SLNO"].Value = dt.Rows[i]["OFFC_SINO"];
                            gvDiscountDetails.Rows[i].Cells["perFrom"].Value = dt.Rows[i]["OFFC_FROM_PERC"];
                            gvDiscountDetails.Rows[i].Cells["perTo"].Value = dt.Rows[i]["OFFC_TO_PERC"];
                            gvDiscountDetails.Rows[i].Cells["FineAmt"].Value = dt.Rows[i]["OFFC_FINE_AMT"];
                        }

                    }
                    else
                    {
                        txtMaxGrps.Text = "";
                        //txtOtherFineAmt.Text = "";
                        //txtOtherFineDesc.Text = "";
                        //txtMinOrderForms.Text = "";
                        gvDiscountDetails.Rows.Clear();
                        FillDefaultGridData();
                        btnDelete.Enabled = false;
                        flagUpdate = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
        }
        private void tvBranches_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //GetSelectedCompAndBranches();
        }

        private void tvBranches_Validated(object sender, EventArgs e)
        {
            GetSelectedCompAndBranches();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(flagUpdate==true)
            {
                  DialogResult result = MessageBox.Show("Do you want to Delete ",
                                               "CRM", MessageBoxButtons.YesNo);
                  if (result == DialogResult.Yes)
                  {
                      GetSelectedCompAndBranches();
                      string[] str = strBranchData.Split(',');
                      int iRes = 0;
                      string strCMD = "";
                      try
                      {
                          for (int i = 0; i < str.Length; i++)
                          {

                              strCMD += " DELETE FROM ORDER_FORMS_FINE_CIRCULARS WHERE OFFC_COMP_CODE='" + str[i].Split('@')[1] +
                                                                                  "' AND OFFC_BRANCH_CODE='" + str[i].Split('@')[0] +
                                                                                  "' and OFFC_ROLE='" + cbRole.SelectedValue + "' AND OFFC_DOC_TYPE='CANCELLING' AND OFFC_EFF_DATE='" + dtpFDate.Value.ToString("dd/MMM/yyyy") + "' ";

                          }
                          if (strCMD.Length > 0)
                          {
                              objDB = new SQLDB();
                              iRes = objDB.ExecuteSaveData(strCMD);
                              if (iRes > 0)
                              {
                                  MessageBox.Show("Data Deleted Succussfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                  btnCancel_Click(null, null);
                              }
                          }
                      }
                      catch(Exception ex)
                      {
                          MessageBox.Show(ex.ToString());
                      }

                  }
            }
        }

        
    }
}
