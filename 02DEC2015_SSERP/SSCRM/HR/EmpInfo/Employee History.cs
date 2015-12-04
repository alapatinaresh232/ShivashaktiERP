using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;
using System.Data.Sql;
using SSAdmin;
using System.IO;
using SSTrans;

namespace SSCRM
{
    public partial class Employee_History : Form
    {
        SQLDB objData = null;
        HRInfo objHRdb = null;
        DataTable DTEmpData = null;
        string strComp = "", strBranch = "";
        
        public Employee_History()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Employee_History_Load(object sender, EventArgs e)
        {
           // FillCompany();
            objData = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();           
           // || CommonData.LogUserRole == "MANAGEMENT"
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
            {
                //FillEmployeeData();
                txtEnameSearch.Focus();
                txtEcodeSearch.ReadOnly = false;
                txtEcodeSearch.BackColor = Color.White;
            }
            else
            {
                strCmd = "SELECT DISTINCT COMPANY_CODE FROM USER_BRANCH "+
                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE "+
                         " WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
                dt = objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (strComp != "")
                            strComp += ",";
                        strComp += dt.Rows[i]["COMPANY_CODE"].ToString();
                    }
                }
                else
                {
                    strComp += CommonData.CompanyCode.ToString();
                }
                strCmd = "SELECT distinct UB_BRANCH_CODE FROM USER_BRANCH WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
                dt2 = objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (strBranch != "")
                            strBranch += ",";
                        strBranch += dt2.Rows[i]["UB_BRANCH_CODE"].ToString();
                    }
                }
                else
                {
                    strBranch += CommonData.BranchCode.ToString();
                }

               
            }
            FillEmployeesList(strComp, strBranch);
        }

        private void FillEmployeesList(string sCompcode,string sBranCode)
        {
            cbEmployees.DataBindings.Clear();

            //if (txtEnameSearch.Text.Length > 2)
            //{
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                objData = new SQLDB();
                SqlParameter[] param = new SqlParameter[4];
                param[0] = objData.CreateParameter("@xcmp_cd", DbType.String,sCompcode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@xECodeName", DbType.String, txtEnameSearch.Text, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@xUserRole", DbType.String, CommonData.LogUserRole, ParameterDirection.Input);

                ds = objData.ExecuteDataSet("BranchLvlEcodeSearch_For_EmpHistory", CommandType.StoredProcedure, param);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    cbEmployees.DataSource = dt;
                    cbEmployees.DisplayMember = "ENAME";
                    cbEmployees.ValueMember = "EmpDetl";
                }
           // }
        }


        //private void FillCompany()
        //{
        //    try
        //    {
        //        DataTable DT = new DataTable();
        //        objData = new SQLDB();
        //        if (CommonData.LogUserId.ToUpper() != "ADMIN")
        //        {
        //            SqlParameter[] param = new SqlParameter[4];
        //            param[0] = objData.CreateParameter("@sCompany", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
        //            param[1] = objData.CreateParameter("@sUser", DbType.String, CommonData.LogUserEcode, ParameterDirection.Input);
        //            param[2] = objData.CreateParameter("@sBranchType", DbType.String, "", ParameterDirection.Input);
        //            param[3] = objData.CreateParameter("@sType", DbType.String, "PARENT", ParameterDirection.Input);
        //            DT = objData.ExecuteDataSet("UserBranchCursor_Get", CommandType.StoredProcedure, param).Tables[0];
        //        }
        //        else
        //        {
        //            SqlParameter[] param = new SqlParameter[3];
        //            param[0] = objData.CreateParameter("@sCompany", DbType.String,"", ParameterDirection.Input);
        //            param[1] = objData.CreateParameter("@sBranch", DbType.String, "", ParameterDirection.Input);
        //            param[2] = objData.CreateParameter("@sType", DbType.String, "PARENT", ParameterDirection.Input);
        //            DT = objData.ExecuteDataSet("AdminBranchCursor_Get", CommandType.StoredProcedure, param).Tables[0];
        //        }

        //        if(DT.Rows.Count>0)
        //        {
        //            DataRow  dr = DT.NewRow();
        //            dr[0] = "-----Select-----";
        //            dr[1] = "-----Select-----";
        //            DT.Rows.InsertAt(dr, 0);
        //            cmbCompany.DisplayMember = "COMPANY_NAME";
        //            cmbCompany.ValueMember = "COMPANY_CODE";
        //            cmbCompany.DataSource = DT;
                    
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(cmbCompany.SelectedIndex>0)
        //    FillBranchData();
        //}

        //private void FillBranchData()
        //{
        //    try
        //    {
        //        if (cmbCompany.SelectedItem != null)
        //        {
        //            DataTable DT = new DataTable();
        //            objData = new SQLDB();
        //            if (CommonData.LogUserId.ToUpper() != "ADMIN")
        //            {
        //                SqlParameter[] param = new SqlParameter[4];
        //                param[0] = objData.CreateParameter("@sCompany", DbType.String, cmbCompany.SelectedValue, ParameterDirection.Input);
        //                param[1] = objData.CreateParameter("@sUser", DbType.String, CommonData.LogUserEcode, ParameterDirection.Input);
        //                param[2] = objData.CreateParameter("@sBranchType", DbType.String, "", ParameterDirection.Input);
        //                param[3] = objData.CreateParameter("@sType", DbType.String, "CHILD", ParameterDirection.Input);

        //                DT = objData.ExecuteDataSet("UserBranchCursor_Get", CommandType.StoredProcedure, param).Tables[0];
        //            }
        //            else
        //            {
        //                SqlParameter[] param = new SqlParameter[3];
        //                param[0] = objData.CreateParameter("@sCompany", DbType.String, cmbCompany.SelectedValue, ParameterDirection.Input);
        //                param[1] = objData.CreateParameter("@sBranch", DbType.String, "", ParameterDirection.Input);
        //                param[2] = objData.CreateParameter("@sType", DbType.String, "CHILD", ParameterDirection.Input);
                       

        //                DT = objData.ExecuteDataSet("AdminBranchCursor_Get", CommandType.StoredProcedure, param).Tables[0];
        //            }
        //            if (DT.Rows.Count > 0)
        //            {
        //                DataRow dr = DT.NewRow();
        //                dr[0] = "-----Select-----";
        //                dr[1] = "-----Select-----";
        //                DT.Rows.InsertAt(dr, 0);

        //                cmbBranch.DisplayMember = "BRANCH_NAME";
        //                cmbBranch.ValueMember = "BRANCH_CODE";
        //                cmbBranch.DataSource = DT;
                        
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(cmbBranch.SelectedIndex>0)
        //    FillDeptData(cmbBranch.SelectedValue.ToString(),0,0,"DEPT");
        //}

        //private void FillDeptData(string strBranch,int strDept,int strDesig,string stype)
        //{
        //    try
        //    {
        //        DTEmpData = new DataTable();
        //        objData = new SQLDB();
        //        SqlParameter[] param = new SqlParameter[4];
        //        param[0] = objData.CreateParameter("@xBranchCode", DbType.String, strBranch, ParameterDirection.Input);
        //        param[1] = objData.CreateParameter("@xDeptCode", DbType.String, strDept, ParameterDirection.Input);
        //        param[2] = objData.CreateParameter("@xDesig", DbType.String, strDesig, ParameterDirection.Input);
        //        param[3] = objData.CreateParameter("@xType", DbType.String, stype, ParameterDirection.Input);
        //        DTEmpData = objData.ExecuteDataSet("GetuEmployeesListByUserBranch", CommandType.StoredProcedure, param).Tables[0];
        //        if (DTEmpData.Rows.Count > 0)
        //        {
                   

        //            if(stype=="DEPT")
        //            {
        //                DataRow dr = DTEmpData.NewRow();
        //                dr[0] = "-----Select-----";
        //                dr[1] = "-----Select-----";
        //                DTEmpData.Rows.InsertAt(dr, 0);
        //                cmbDept.DisplayMember = "dept_desc";
        //                cmbDept.ValueMember = "DeptCode";
                       
        //                cmbDept.DataSource = DTEmpData;
        //            }
        //            if (stype == "DESIG")
        //            {
        //                DataRow dr = DTEmpData.NewRow();
        //                dr[0] = "-----Select-----";
        //                dr[1] = "-----Select-----";
        //                DTEmpData.Rows.InsertAt(dr, 0);
        //                cmbDesig.DisplayMember = "desig_desc";
        //                cmbDesig.ValueMember = "DesigCode";
                       
        //                cmbDesig.DataSource = DTEmpData;
        //            }
        //            if (stype == "EMP")
        //            {
        //                clbUsers.Items.Clear();
        //                foreach (DataRow dataRow in DTEmpData.Rows)
        //                {
        //                    if (dataRow["ECODE"] + "" != "")
        //                    {
        //                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
        //                        oclBox.Tag = dataRow["ECODE"].ToString();
        //                        oclBox.Text = dataRow["MemmberName"].ToString() ;
        //                        clbUsers.Items.Add(oclBox);
        //                        oclBox = null;
        //                    }
        //                }
        //            }
                    
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbDept.SelectedIndex > 0)
        //        FillDeptData(cmbBranch.SelectedValue.ToString(),Convert.ToInt32( cmbDept.SelectedValue.ToString()),0,"DESIG");
        //}

        //private void cmbDesig_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbDesig.SelectedIndex > 0)
        //        FillDeptData(cmbBranch.SelectedValue.ToString(),Convert.ToInt32(cmbDept.SelectedValue.ToString()),Convert.ToInt32(cmbDesig.SelectedValue.ToString()),"EMP");
        //}

        //private void clbUsers_ItemCheck(object sender, ItemCheckEventArgs e)
        //{
        //    for (int i = 0; i < clbUsers.Items.Count; i++)
        //    {
        //        if (e.Index != i)
        //            clbUsers.SetItemCheckState(i, CheckState.Unchecked);
        //    }
        //    if (e.NewValue == CheckState.Checked)
        //    {
        //        txtEcodeSearch.Text = ((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString();
        //        txtEcodeSearch_Validated(null, null);
        //    }
        //    else
        //    {
        //        txtEcodeSearch.Text = "";
        //        txtEcodeSearch_Validated(null, null);
        //    }
        //}

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            gvEmpSalDetl.Rows.Clear();
            gvPerformance.Rows.Clear();
            gvExperience.Rows.Clear();
            gvEducation.Rows.Clear();
            gvFamily.Rows.Clear();
            gvEmpRemarks.Rows.Clear(); 
            DataTable dtAddress = new DataTable();          
            try
            {
                if (txtEcodeSearch.Text.Length > 4)
                {                                     

                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    objData = new SQLDB();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = objData.CreateParameter("@xEcode", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);
                    
                     ds = objData.ExecuteDataSet("Get_EmployeeMaster_Detlx", CommandType.StoredProcedure, param);
                     dt = ds.Tables[0]; 
                    DataTable dtSalary = ds.Tables[1];

                    if (dt.Rows.Count > 0)
                    {

                        txtName.Text = dt.Rows[0]["EmpName"].ToString();
                        txtName.Tag = dt.Rows[0]["ApplNo"].ToString();
                        txtCompany.Text = dt.Rows[0]["CompName"].ToString();
                        txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
                        txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                        txtDesig.Text = dt.Rows[0]["DesigName"].ToString();
                        txtFather.Text = dt.Rows[0]["FatherName"].ToString();
                        meHRISDataofBirth.Text = Convert.ToDateTime(dt.Rows[0]["EmpDob"].ToString()).ToString("dd/MM/yyyy");
                        txtAge.Text = dt.Rows[0]["Age"].ToString();
                        meHRISDateOfJoin.Text = Convert.ToDateTime(dt.Rows[0]["EmpDoj"].ToString()).ToString("dd/MM/yyyy");
                        if (dt.Rows[0]["PhotoSig"].ToString().Length != 0)
                            GetImage((byte[])dt.Rows[0]["PhotoSig"], "PHOTO");
                        else
                            picEmpPhoto.BackgroundImage = null;
                        int Dayss = ((DateTime.Now - Convert.ToDateTime(dt.Rows[0]["EmpDoj"].ToString())).Days);



                        txtTotServYer.Text = dt.Rows[0]["TotalServYears"].ToString();
                        txtTotServMonths.Text = dt.Rows[0]["TotalServMonths"].ToString();
                        //txtPresCompSerYear.Text = dt.Rows[0]["compServYear"].ToString();
                        //txtPresCompSerMonths.Text = dt.Rows[0]["compServMonths"].ToString();
                        txtPresDesigYear.Text = dt.Rows[0]["desigServYear"].ToString();
                        txtPresDesigMonth.Text = dt.Rows[0]["desigServMonths"].ToString();


                        objData = new SQLDB();
                        string strCmd = "exec Get_EmployeeEoraInfo_ERP " + Convert.ToInt32(txtEcodeSearch.Text) + "";
                        dtAddress = objData.ExecuteDataSet(strCmd).Tables[0];

                        txtApplNo.Text = dtAddress.Rows[0]["ApplNo"].ToString();

                        txtWorkStatus.Text = dtAddress.Rows[0]["WorkingStatus"].ToString();
                        txtMailId.Text = dtAddress.Rows[0]["EmailId"].ToString();
                        txtContactNo.Text = dtAddress.Rows[0]["MobileNo"].ToString();
                        txtBloodGroup.Text = dtAddress.Rows[0]["BloodGroup"].ToString();

                        txtPresHouseNo.Text = dtAddress.Rows[0]["PreHNo"].ToString();
                        txtPresLandMark.Text = dtAddress.Rows[0]["Pres_LandMark"].ToString();
                        txtPresMandal.Text = dtAddress.Rows[0]["Pres_Mandal"].ToString();
                        txtPersVillage.Text = dtAddress.Rows[0]["Pres_Vill"].ToString();
                        txtPresDistrict.Text = dtAddress.Rows[0]["Pres_District"].ToString();
                        txtPresState.Text = dtAddress.Rows[0]["Pres_State"].ToString();
                        txtPersPin.Text = dtAddress.Rows[0]["Pres_Pin"].ToString();
                        PresentPhone_num.Text = dtAddress.Rows[0]["Pres_Phone"].ToString();

                        txtPermentPhNo_num.Text = dtAddress.Rows[0]["Perm_Phone"].ToString();
                        txtPermi_HNo.Text = dtAddress.Rows[0]["Perm_HNo"].ToString();
                        txtPermi_LandMark.Text = dtAddress.Rows[0]["Perm_LandMark"].ToString();
                        txtPermin_District.Text = dtAddress.Rows[0]["Perm_District"].ToString();
                        txtPermin_Village.Text = dtAddress.Rows[0]["Perm_Vill"].ToString();
                        txtPermin_Mandal.Text = dtAddress.Rows[0]["Perm_Mandal"].ToString();
                        txtPermin_State.Text = dtAddress.Rows[0]["Perm_State"].ToString();
                        txtPermin_Pin.Text = dtAddress.Rows[0]["Perm_Pin"].ToString();

                        txtEmer_HNo.Text = dtAddress.Rows[0]["Cont_Pers_HNo"].ToString();
                        txtEmer_LandMark.Text = dtAddress.Rows[0]["Cont_Pers_LandMark"].ToString();
                        txtEmer_Mandal.Text = dtAddress.Rows[0]["Cont_Pers_Mandal"].ToString();
                        txtEmer_Village.Text = dtAddress.Rows[0]["Cont_Pers_Village"].ToString();
                        txtEmer_District.Text = dtAddress.Rows[0]["Cont_Pers_District"].ToString();
                        txtEmer_State.Text = dtAddress.Rows[0]["Cont_Pers_State"].ToString();
                        txtEmer_Pin.Text = dtAddress.Rows[0]["Cont_Pers_Pin"].ToString();

                        txtContPhNo_num.Text = dtAddress.Rows[0]["Cont_Pers_Phone_Res"].ToString();
                        txtContPhNo1_optional.Text = dtAddress.Rows[0]["Cont_Pers_Phone_Office"].ToString();
                        txtContactName.Text = dtAddress.Rows[0]["Cont_Pers_Name"].ToString();
                       
                        if (dtSalary.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtSalary.Rows.Count; i++)
                            {
                                gvEmpSalDetl.Rows.Add();
                                gvEmpSalDetl.Rows[i].Cells["SlNo"].Value = (i + 1);
                                gvEmpSalDetl.Rows[i].Cells["TranType"].Value = dtSalary.Rows[i]["PROMOTION_NAME"];
                                gvEmpSalDetl.Rows[i].Cells["EffDate"].Value = Convert.ToDateTime(dtSalary.Rows[i]["hess_eff_date"].ToString()).ToString("dd/MMM/yyyy");
                                gvEmpSalDetl.Rows[i].Cells["ToCompany"].Value = dtSalary.Rows[i]["HESS_TO_COMPANY_CODE"];
                                gvEmpSalDetl.Rows[i].Cells["ToBranch"].Value = dtSalary.Rows[i]["BRANCH_NAME"];
                                gvEmpSalDetl.Rows[i].Cells["ToDept"].Value = dtSalary.Rows[i]["dept_desc"];
                                gvEmpSalDetl.Rows[i].Cells["ToDesig"].Value = dtSalary.Rows[i]["desig_desc"];
                                gvEmpSalDetl.Rows[i].Cells["GrossSalary"].Value = dtSalary.Rows[i]["GrossSal"];

                            }
                        }

                        else
                        {                          

                            gvEmpSalDetl.Rows.Clear();
                        }

                        GetEmployeePersonalDetails();
                        GetEmployeePerformance();
                        GetEmployeeAuditRemarks();
                        GetEmpAwardDetails();

                        tabPages.SelectTab(EmpTracking);


                    }
                    else
                    {
                        ClearControls();
                    }
                   
                    
                }
                else
                {
                    ClearControls();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetEmpAwardDetails()
        {
            objData = new SQLDB();
            DataTable dtRemarks = new DataTable();
            gvAwardDetail.Rows.Clear();
            string strPerf = "", strGift = "";
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[2];

                param[0] = objData.CreateParameter("@xFinYear", DbType.String, "", ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xEcode", DbType.Int32, Convert.ToInt32(txtEcodeSearch.Text), ParameterDirection.Input);

                ds = objData.ExecuteDataSet("Get_EmpAwardDetails", CommandType.StoredProcedure, param);
                dtRemarks = ds.Tables[0];

                if (dtRemarks.Rows.Count > 0)
                {

                    for (int i = 0; i < dtRemarks.Rows.Count; i++)
                    {
                        if (dtRemarks.Rows[i]["PerfPoints"].ToString().Equals("0.00"))
                        {
                            strPerf = dtRemarks.Rows[i]["PerfDetails"].ToString();
                        }
                        else
                        {
                            strPerf = dtRemarks.Rows[i]["PerfDetails"].ToString() + "" + dtRemarks.Rows[i]["PerfPoints"].ToString();
                        }
                        if (dtRemarks.Rows[i]["GiftCash"].ToString().Equals("0.00"))
                        {
                            strGift = dtRemarks.Rows[i]["TripName"].ToString();
                        }
                        else
                        {
                            strGift = dtRemarks.Rows[i]["TripName"].ToString() + "" + dtRemarks.Rows[i]["GiftCash"].ToString();
                        }

                        gvAwardDetail.Rows.Add();
                        gvAwardDetail.Rows[i].Cells["SlNo_Award"].Value = (i + 1).ToString();
                        gvAwardDetail.Rows[i].Cells["AwardDate"].Value = Convert.ToDateTime(dtRemarks.Rows[i]["AwardDate"].ToString()).ToString("dd/MMM/yyyy");
                        gvAwardDetail.Rows[i].Cells["EventName"].Value = dtRemarks.Rows[i]["EventName"].ToString();
                        gvAwardDetail.Rows[i].Cells["AwardName"].Value = dtRemarks.Rows[i]["AwardName"].ToString();
                        gvAwardDetail.Rows[i].Cells["Performance"].Value = strPerf;
                        gvAwardDetail.Rows[i].Cells["GiftOrCheque"].Value = strGift;
                        gvAwardDetail.Rows[i].Cells["Memento"].Value = dtRemarks.Rows[i]["MementoType"].ToString();                        
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetEmployeeAuditRemarks()
        {
            objData = new SQLDB();
            DataTable dtRemarks = new DataTable();
            gvEmpRemarks.Rows.Clear();           
            try
            {
                DataSet ds = new DataSet();               
                SqlParameter[] param = new SqlParameter[1];               

                param[0] = objData.CreateParameter("@xEcode", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);               

                ds = objData.ExecuteDataSet("Get_EmployeeRemarksForEmpHistory", CommandType.StoredProcedure, param);
                dtRemarks = ds.Tables[0];

                if (dtRemarks.Rows.Count > 0)
                {

                    for (int i = 0; i < dtRemarks.Rows.Count; i++)
                    {
                        gvEmpRemarks.Rows.Add();
                        gvEmpRemarks.Rows[i].Cells["SlNo_Remarks"].Value = (i+1).ToString();
                        gvEmpRemarks.Rows[i].Cells["Audit_Month"].Value = dtRemarks.Rows[i]["DocMonth"].ToString();
                        gvEmpRemarks.Rows[i].Cells["AuditPoint"].Value = dtRemarks.Rows[i]["AuditPoint"].ToString();
                        gvEmpRemarks.Rows[i].Cells["DevType"].Value = dtRemarks.Rows[i]["Dev_Type"].ToString();
                        gvEmpRemarks.Rows[i].Cells["SubDevType"].Value = dtRemarks.Rows[i]["SubDev_Type"].ToString();
                        gvEmpRemarks.Rows[i].Cells["DevAmt"].Value = dtRemarks.Rows[i]["DevAmt"].ToString();
                        gvEmpRemarks.Rows[i].Cells["RecAmt"].Value = dtRemarks.Rows[i]["RecAmt"].ToString();
                        gvEmpRemarks.Rows[i].Cells["Status"].Value = dtRemarks.Rows[i]["Status"].ToString();
                        gvEmpRemarks.Rows[i].Cells["UnsolvedReason"].Value = dtRemarks.Rows[i]["UnsolvedReason"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetEmployeePerformance()
        {
            gvPerformance.Rows.Clear();
           
            try
            {
                DataTable dtSalePerform = new DataTable();
              
                DataSet ds = new DataSet();
                objData = new SQLDB();
                SqlParameter[] param = new SqlParameter[5];
                //DateTime dtFrom = Convert.ToDateTime(meHRISDateOfJoin.Text);
                //if ((Convert.ToDateTime("01/APR/2013")-Convert.ToDateTime(meHRISDateOfJoin.Text)).Days>0)
                //    dtFrom = Convert.ToDateTime("01/APR/2013");
                //else
                //    dtFrom = Convert.ToDateTime(meHRISDateOfJoin.Text);

                param[0] = objData.CreateParameter("@xEcode", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xDOC_MONTH", DbType.String, "", ParameterDirection.Input);
                param[2] = objData.CreateParameter("@xFrom", DbType.String, "" , ParameterDirection.Input);
                param[3] = objData.CreateParameter("@xTo", DbType.String, "", ParameterDirection.Input);
                param[4] = objData.CreateParameter("@xREP_TYPE", DbType.String, "", ParameterDirection.Input);

                ds = objData.ExecuteDataSet("SSCRM_REP_EMP_SALE_PERFORMANCE1", CommandType.StoredProcedure, param);
                dtSalePerform = ds.Tables[0];
            
                if(dtSalePerform.Rows.Count>0)
                {                   
                    for (int i = 0; i < dtSalePerform.Rows.Count;i++ )
                    {
                        gvPerformance.Rows.Add();
                        gvPerformance.Rows[i].Cells["SiNO_Sales"].Value = (i + 1);
                        gvPerformance.Rows[i].Cells["Month"].Value = dtSalePerform.Rows[i]["rs_document_month"];

                        gvPerformance.Rows[i].Cells["groups"].Value = (dtSalePerform.Rows[i]["rs_groups"].ToString());
                        gvPerformance.Rows[i].Cells["Points_Sales"].Value = dtSalePerform.Rows[i]["rs_points"];
                        gvPerformance.Rows[i].Cells["revenue_Sales"].Value = dtSalePerform.Rows[i]["rs_revenue_amt"];
                        gvPerformance.Rows[i].Cells["SalesPPoints"].Value = dtSalePerform.Rows[i]["rs_avg_sales_price"];
                        gvPerformance.Rows[i].Cells["avgPmdPGroups"].Value = dtSalePerform.Rows[i]["rs_avg_pmd_per_group"];
                        gvPerformance.Rows[i].Cells["PointsPGroup"].Value = dtSalePerform.Rows[i]["rs_points_per_group"];
                        gvPerformance.Rows[i].Cells["PlantspGroups"].Value = dtSalePerform.Rows[i]["rs_plants_per_group"];
                        gvPerformance.Rows[i].Cells["GrominpGroup"].Value = dtSalePerform.Rows[i]["rs_gromin_per_group"].ToString();
                        gvPerformance.Rows[i].Cells["FocusedpGroup"].Value = dtSalePerform.Rows[i]["rs_fp_pg"];
                        gvPerformance.Rows[i].Cells["DC"].Value = dtSalePerform.Rows[i]["rs_ratios_dc"];
                        gvPerformance.Rows[i].Cells["pc"].Value = dtSalePerform.Rows[i]["rs_ratios_pc"];
                        gvPerformance.Rows[i].Cells["demons"].Value = dtSalePerform.Rows[i]["rs_pph_demos"];
                        gvPerformance.Rows[i].Cells["points"].Value = dtSalePerform.Rows[i]["rs_pph_points"];
                        gvPerformance.Rows[i].Cells["cust"].Value = dtSalePerform.Rows[i]["rs_pph_customers"];
                        gvPerformance.Rows[i].Cells["revenue"].Value = dtSalePerform.Rows[i]["rs_pph_revenue"];
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //private void GetNoOfYearsMonths(string Doj,TextBox txtYears,TextBox txtMonths)
        //{
        //    int days = (DateTime.Now - Convert.ToDateTime(Doj)).Days;
        //    if(days<365)
        //        txt
        // double years = ((DateTime.Now - Convert.ToDateTime(Doj)).TotalDays)/365;
        //}
        public void GetImage(byte[] imageData, string TYPE)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                if (TYPE == "PHOTO")
                {
                    picEmpPhoto.BackgroundImage = newImage;
                    this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtName.Tag = "";
            txtCompany.Text = "";
            txtBranch.Text = "";
            txtDept.Text = "";
            txtDesig.Text = "";
            txtFather.Text = "";
            meHRISDataofBirth.Text = "";
            txtAge.Text = "";
            meHRISDateOfJoin.Text = "";
            picEmpPhoto.BackgroundImage = null;
            //int Dayss = ((DateTime.Now - Convert.ToDateTime(dt.Rows[0]["EmpDoj"].ToString())).Days);
            txtTotServYer.Text = "";
            txtTotServMonths.Text = "";
            //txtPresCompSerYear.Text = "";
            //txtPresCompSerMonths.Text = "";
            txtPresDesigYear.Text = "";
            txtPresDesigMonth.Text = "";
            txtMailId.Text = "";
            txtContactNo.Text = "";
            txtBloodGroup.Text = "";
            txtWorkStatus.Text = "";
            txtApplNo.Text = "";

            ClearControls();
            gvEmpSalDetl.Rows.Clear();
            gvEducation.Rows.Clear();
            gvExperience.Rows.Clear();
            gvFamily.Rows.Clear();
            gvAwardDetail.Rows.Clear();
            gvPerformance.Rows.Clear();
            gvExperience.Rows.Clear();
            txtEcodeSearch.Focus();

        }

        private void ClearControls()
        {
           // txtEcodeSearch.Focus();
            txtEcodeSearch.Text = "";
            txtName.Text = "";
            txtName.Tag = "";
            txtCompany.Text = "";
            txtBranch.Text = "";
            txtDept.Text = "";
            txtDesig.Text = "";
            txtFather.Text = "";
            meHRISDataofBirth.Text = "";
            txtAge.Text = "";
            meHRISDateOfJoin.Text = "";
            picEmpPhoto.BackgroundImage = null;
            //int Dayss = ((DateTime.Now - Convert.ToDateTime(dt.Rows[0]["EmpDoj"].ToString())).Days);
            txtTotServYer.Text = "";
            txtTotServMonths.Text = "";
            //txtPresCompSerYear.Text = "";
            //txtPresCompSerMonths.Text = "";
            txtPresDesigYear.Text = "";
            txtPresDesigMonth.Text = "";
            txtMailId.Text = "";
            txtContactNo.Text = "";
            txtBloodGroup.Text = "";
            txtWorkStatus.Text = "";
            txtApplNo.Text = "";

            gvEmpSalDetl.Rows.Clear();
            gvEducation.Rows.Clear();
            gvExperience.Rows.Clear();
            gvFamily.Rows.Clear();
            gvAwardDetail.Rows.Clear();
            gvPerformance.Rows.Clear();
            gvExperience.Rows.Clear();

            txtPresHouseNo.Text = "";
            txtPresLandMark.Text = "";
            txtPresMandal.Text = "";
            txtPersVillage.Text = "";
            txtPresDistrict.Text = "";
            txtPresState.Text = "";
            txtPersPin.Text = "";
            PresentPhone_num.Text = "";

            txtPermentPhNo_num.Text = "";
            txtPermi_HNo.Text = "";
            txtPermi_LandMark.Text = "";
            txtPermin_District.Text = "";
            txtPermin_Village.Text = "";
            txtPermin_Mandal.Text = "";
            txtPermin_State.Text = "";
            txtPermin_Pin.Text = "";

            txtEmer_HNo.Text = "";
            txtEmer_LandMark.Text = "";
            txtEmer_Mandal.Text = "";
            txtEmer_Village.Text = "";
            txtEmer_District.Text = "";
            txtEmer_State.Text = "";
            txtEmer_Pin.Text = "";
            txtContPhNo_num.Text = "";
            txtContPhNo1_optional.Text = "";
            txtContactName.Text = "";
        }

        //public void EnableTab(TabPage page, bool enable)
        //{
        //    EnableControls(page.Controls, enable);
        //}

        //private void EnableControls(Control.ControlCollection ctls, bool enable)
        //{
        //    foreach (Control ctl in ctls)
        //    {
        //        ctl.Enabled = enable;
        //        EnableControls(ctl.Controls, enable);
        //    }
        //}


        private void GetEmployeePersonalDetails()
        {          
           
            try
            {
                DataTable dtFamily = new DataTable();
                DataTable dtExperience = new DataTable();
                DataTable dtEducation = new DataTable();
                DataSet ds = new DataSet();
                objData = new SQLDB();
                SqlParameter[] param = new SqlParameter[3];
               
                param[0] = objData.CreateParameter("@CompanyCode", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@BranchCode", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@ApplicatioNo", DbType.String, txtName.Tag, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetEmp_PersonalDetails", CommandType.StoredProcedure, param);
                dtFamily = ds.Tables[0];
                dtEducation = ds.Tables[1];
                dtExperience = ds.Tables[2];
                
                gvFamily.Rows.Clear();
                if(dtFamily.Rows.Count>0)
                {                   

                    int intRow = 1;
                    for (int i = 0; i < dtFamily.Rows.Count; i++)
                    {
                        
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtFamily.Rows[i]["SLNO"] = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellRelationShip = new DataGridViewTextBoxCell();
                        cellRelationShip.Value = dtFamily.Rows[i]["RelationShip"];
                        tempRow.Cells.Add(cellRelationShip);

                        DataGridViewCell cellsName = new DataGridViewTextBoxCell();
                        cellsName.Value = dtFamily.Rows[i]["sName"];
                        tempRow.Cells.Add(cellsName);

                        DataGridViewCell cellDateofBirth = new DataGridViewTextBoxCell();
                        cellDateofBirth.Value = Convert.ToDateTime(dtFamily.Rows[i]["DateofBirth"]).ToString("dd/MM/yyyy");
                        tempRow.Cells.Add(cellDateofBirth);

                        DataGridViewCell cellResiding = new DataGridViewTextBoxCell();
                        cellResiding.Value = dtFamily.Rows[i]["Residing"];
                        tempRow.Cells.Add(cellResiding);

                        DataGridViewCell cellDepending = new DataGridViewTextBoxCell();
                        cellDepending.Value = dtFamily.Rows[i]["Depending"];
                        tempRow.Cells.Add(cellDepending);

                        DataGridViewCell cellOccupation = new DataGridViewTextBoxCell();
                        cellOccupation.Value = dtFamily.Rows[i]["Occupation"];
                        tempRow.Cells.Add(cellOccupation);
                        intRow = intRow + 1;
                        gvFamily.Rows.Add(tempRow);
                    }
                }
                if (dtEducation.Rows.Count > 0)
                {
                    int intRow = 1;
                    gvEducation.Rows.Clear();
                    for (int i = 0; i < dtEducation.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtEducation.Rows[i]["SLNO_edu"] = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellExamPass = new DataGridViewTextBoxCell();
                        cellExamPass.Value = dtEducation.Rows[i]["ExamPass"];
                        tempRow.Cells.Add(cellExamPass);

                        DataGridViewCell cellYearPass = new DataGridViewTextBoxCell();
                        cellYearPass.Value = dtEducation.Rows[i]["YearPass"];
                        tempRow.Cells.Add(cellYearPass);

                        DataGridViewCell cellInstName = new DataGridViewTextBoxCell();
                        cellInstName.Value = dtEducation.Rows[i]["InstName"];
                        tempRow.Cells.Add(cellInstName);

                        DataGridViewCell cellInstLocation = new DataGridViewTextBoxCell();
                        cellInstLocation.Value = dtEducation.Rows[i]["InstLocation"];
                        tempRow.Cells.Add(cellInstLocation);

                        DataGridViewCell cellSubject = new DataGridViewTextBoxCell();
                        cellSubject.Value = dtEducation.Rows[i]["Subject"];
                        tempRow.Cells.Add(cellSubject);

                        DataGridViewCell cellUniversity = new DataGridViewTextBoxCell();
                        cellUniversity.Value = dtEducation.Rows[i]["University"];
                        tempRow.Cells.Add(cellUniversity);

                        DataGridViewCell cellPerofPass = new DataGridViewTextBoxCell();
                        cellPerofPass.Value = dtEducation.Rows[i]["PerofPass"];
                        tempRow.Cells.Add(cellPerofPass);

                        intRow = intRow + 1;
                        gvEducation.Rows.Add(tempRow);
                    }
                }
                if (dtExperience.Rows.Count > 0)
                {
                    int intRow = 1;
                    gvExperience.Rows.Clear();
                    for (int i = 0; i < dtExperience.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtExperience.Rows[i]["SlNo_ex"] = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellOrg = new DataGridViewTextBoxCell();
                        cellOrg.Value = dtExperience.Rows[i]["Organisation"];
                        tempRow.Cells.Add(cellOrg);

                        DataGridViewCell cellFromDate = new DataGridViewTextBoxCell();
                        cellFromDate.Value = Convert.ToDateTime(dtExperience.Rows[i]["FromDate"].ToString()).ToString("dd/MMM/yyyy");
                        tempRow.Cells.Add(cellFromDate);

                        DataGridViewCell cellToDate = new DataGridViewTextBoxCell();
                        cellToDate.Value = Convert.ToDateTime(dtExperience.Rows[i]["ToDate"].ToString()).ToString("dd/MMM/yyyy");
                        tempRow.Cells.Add(cellToDate);

                        DataGridViewCell cellSalary = new DataGridViewTextBoxCell();
                        cellSalary.Value = dtExperience.Rows[i]["Salary"];
                        tempRow.Cells.Add(cellSalary);

                        DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                        cellRemarks.Value = dtExperience.Rows[i]["Remarks_ex"];
                        tempRow.Cells.Add(cellRemarks);

                        intRow = intRow + 1;
                        gvExperience.Rows.Add(tempRow);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillEmployeeData()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            cbEmployees.DataBindings.Clear();
            //if (txtEnameSearch.Text.Length > 3)
            //{
                try
                {

                    dt = objHRdb.GetEmployeesForMisconduct("", "", "", txtEnameSearch.Text.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        //dr[1] = "--Select--";
                        //dr[3] = "--Select--";

                        //dt.Rows.InsertAt(dr, 0);

                        cbEmployees.DataSource = dt;
                        cbEmployees.DisplayMember = "ENAME";
                        cbEmployees.ValueMember = "EmpDetl";
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
           // }
        }



        private void tabPages_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //if (txtName.Text.Length > 2)
            //{
            //    if (gvEmpSalDetl.Rows.Count == 0)
            //    {
            //        if (e.TabPage == tabPages.TabPages[0])
            //        {
            //            e.Cancel = true;
            //            EmpTracking.Hide();
            //        }
            //    }
            //    else
            //    {
            //        if (e.TabPage == tabPages.TabPages[0])
            //        {
            //            e.Cancel = false;
            //            EmpTracking.Show();
            //        }
            //    }
            //    if (gvPerformance.Rows.Count == 0)
            //    {
            //        if (e.TabPage == tabPages.TabPages[1])
            //        {
            //            e.Cancel = true;
            //            SalesPerf.Hide();
            //            MessageBox.Show("No Sales","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //        }
            //    }
            //    else
            //    {
            //        if (e.TabPage == tabPages.TabPages[1])
            //        {
            //            e.Cancel = false;
            //            SalesPerf.Show();
            //        }
            //    }
            //    if (gvEmpRemarks.Rows.Count == 0)
            //    {
            //        if (e.TabPage == tabPages.TabPages[4])
            //        {
            //            e.Cancel = true;
            //            EmpRemarks.Hide();
            //            MessageBox.Show("No Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    else
            //    {
            //        if (e.TabPage == tabPages.TabPages[4])
            //        {
            //            e.Cancel = false;
            //            EmpRemarks.Show();
            //        }
            //    }

               
            //    if (gvExperience.Rows.Count == 0 && gvFamily.Rows.Count==0 && gvEducation.Rows.Count==0)
            //    {
            //        if (e.TabPage == tabPages.TabPages[5])
            //        {
            //            e.Cancel = true;
            //            EmpDetl.Hide();
            //        }
            //    }
            //    else
            //    {
            //        if (e.TabPage == tabPages.TabPages[5])
            //        {
            //            e.Cancel = false;
            //            EmpDetl.Show();
            //        }
            //    }

            //    if (gvAwardDetail.Rows.Count == 0)
            //    {
            //        if (e.TabPage == tabPages.TabPages[2])
            //        {
            //            e.Cancel = true;
            //            Awards.Hide();
            //            MessageBox.Show("No Awards", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    else
            //    {
            //        if (e.TabPage == tabPages.TabPages[2])
            //        {
            //            e.Cancel = false;
            //            Awards.Show();
            //        }
            //    }

            //    if (e.TabPage == tabPages.TabPages[3])
            //    {
            //        e.Cancel = true;
            //        Stocks.Hide();
            //       // MessageBox.Show("No Awards", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }

        private void cbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmployees.Items.Count != 0)
            {
                if (cbEmployees.SelectedIndex >= -1)
                {
                    //if (Convert.ToString(cbEmployees.SelectedValue) != "System.Data.DataRowView")
                    //{

                    txtEcodeSearch.Text = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[1].ToString().Split('-')[0];
                    //txtEnameSearch.Text = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[1].ToString().Split('-')[0];
                    if (txtEcodeSearch.Text.Length > 3)
                    {
                        txtEcodeSearch_Validated(null, null);
                    }
                    // }
                }
            }
        }

        private void txtEnameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEnameSearch_Validated(object sender, EventArgs e)
        {
            if (txtEnameSearch.Text.Length > 3)
            {
                //if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
                //    FillEmployeeData();
                //else
                    FillEmployeesList(strComp, strBranch);
            }
            else
            {
                //if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
                //    FillEmployeeData();
                //else
                    FillEmployeesList(strComp, strBranch);
            }
            cbEmployees_SelectedIndexChanged(null,null);
        }

     
     
    }
}
