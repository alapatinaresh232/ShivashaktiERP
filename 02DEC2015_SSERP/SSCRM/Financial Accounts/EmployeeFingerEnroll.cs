using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSAdmin;
using System.IO;
using System.Data.SqlClient;
using ZkFingerDemo;
using System.Configuration;
using SSCRMDB;

namespace SSCRM
{
    public partial class EmployeeFingerEnroll : Form
    {
        Master objMstr = null;
        bool flagUpdate = false;
        public byte[] template;
        SQLDB objDB = null;
        public EmployeeFingerEnroll()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void FillEmployeeMasterData()
        {
            objMstr = new Master();
            DataSet ds = new DataSet();
            try
            {
                ds = objMstr.GetEmployeeMasterDetl(txtEcodeSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objMstr = null;
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //dtEmpInfo = ds.Tables[0];
                    txtName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    txtFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    txtDesig.Text = ds.Tables[0].Rows[0]["DesigName"].ToString();
                    txtDept.Text = ds.Tables[0].Rows[0]["DeptName"].ToString();
                    txtCompany.Text = ds.Tables[0].Rows[0]["CompName"].ToString();
                    txtBranch.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    txtBranch.Tag = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                    txtApplNo.Text = ds.Tables[0].Rows[0]["ApplNo"].ToString();
                    meHRISDataofBirth.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EmpDob"]).ToString("dd/MM/yyyy");
                    meHRISDateOfJoin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EmpDoj"]).ToString("dd/MM/yyyy");


                    if (ds.Tables[0].Rows[0]["PhotoSig"].ToString() != "")
                    {
                        GetImage(((byte[])ds.Tables[0].Rows[0]["PhotoSig"]),"EMP");
                    }
                    if (ds.Tables[0].Rows[0]["FingerPrint"].ToString() != "")
                    {
                        //picFigerPrint.BackgroundImage = null;
                        GetImage(((byte[])ds.Tables[0].Rows[0]["FingerPrint"]), "FINGER1");
                        //lblMessage.Visible = false;
                    }
                    else
                    {
                        btnEnroll2.Enabled = false;
                        picFigerPrint.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                        return;
                    }
                    
                    if (ds.Tables[0].Rows[0]["FingerPrint2"].ToString() != "")
                    {
                        //picFigerPrint.BackgroundImage = null;
                        GetImage(((byte[])ds.Tables[0].Rows[0]["FingerPrint2"]), "FINGER2");
                        //lblMessage.Visible = false;
                    }
                    else
                    {
                        picFigerPrint2.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                    }
                   

                }
                else
                {
                    //lblMessage.Visible = true;
                    picFigerPrint.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                    picFigerPrint2.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                    btnSave.Enabled = false;
                    btnEnroll.Enabled = true;
                    btnDelete.Enabled = true;
                    flagUpdate = false;
                    txtName.Text = "";
                    txtFatherName.Text = "";
                    txtDesig.Text = "";
                    txtDept.Text = "";
                    txtCompany.Text = "";
                    txtBranch.Text = "";
                    txtApplNo.Text = "";
                    meHRISDataofBirth.Text = "";
                    meHRISDateOfJoin.Text = "";
                    picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;
                    //picFigerPrint.BackgroundImage = null;

                }
            }
            ds = null;
        }
        public void GetImage(byte[] imageData,string sPhotoType)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                if(sPhotoType=="EMP")
                {
                    picEmpPhoto.BackgroundImage = newImage;
                    this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
                if (sPhotoType == "FINGER1" && imageData.Length>0)
                {
                    flagUpdate = true;
                    picFigerPrint.BackgroundImage = newImage;
                    this.picFigerPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnSave.Enabled = false;
                    btnEnroll.Enabled = false;
                    if ( CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserId=="40154")
                    {
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }
                }
                if (sPhotoType == "FINGER2" && imageData.Length > 0)
                {
                    flagUpdate = true;
                    picFigerPrint2.BackgroundImage = newImage;
                    this.picFigerPrint2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnSave.Enabled = false;
                    btnEnroll2.Enabled = false;
                    if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserId == "40154")
                    {
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            flagUpdate = false;
            btnSave.Enabled = false;
            btnEnroll.Enabled = true;
            btnEnroll2.Enabled = true;
            btnDelete.Enabled = true;
            txtName.Text = "";
            txtFatherName.Text = "";
            txtDesig.Text = "";
            txtDept.Text = "";
            txtCompany.Text = "";
            txtBranch.Text = "";
            txtApplNo.Text = "";
            meHRISDataofBirth.Text = "";
            meHRISDateOfJoin.Text = "";
            picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;
            picFigerPrint.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
            picFigerPrint2.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
            txtEcodeSearch.Text = "";
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            if(txtApplNo.Text.Length>0)
            {
                try
                {
                    //DialogResult result = MessageBox.Show("Please put your Finger on the Device",
                    //                             "CRM", MessageBoxButtons.YesNo);
                    //if (result == DialogResult.Yes)
                    //{
                        FingerDemo obj = new FingerDemo();
                        obj.Branch = txtBranch.Tag.ToString();
                        obj.lblMessage.Text = "Please Put finger on the Device";
                        obj.ShowDialog();
                        if (obj.fpstatus == true)
                        {
                            MessageBox.Show("FingerPrint Enrolled With Other's Ecode - " + obj.sEcode.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            btnSave.Enabled = false;
                        }
                        else
                        {

                            template = obj.Emptemplate;

                            FingerDemo obj1 = new FingerDemo();
                            obj1.Branch = txtBranch.Tag.ToString();
                            obj1.oldEmptemplate = template;
                            obj1.lblMessage.Text = "To Confirm Again Put finger on the Device";
                            obj1.ShowDialog();


                            if (obj1.fpstatus == true)
                            {
                                picFigerPrint.BackgroundImage = obj.PictureBox1.Image;
                                if (obj.Emptemplate != null)
                                {
                                    btnSave.Enabled = true;
                                }
                            
                            }
                            else
                            {
                                picFigerPrint.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                                btnSave.Enabled = false;
                            }
                            //btnSave.Enabled = true;
                            //lblMessage.Visible = false;
                            //picFigerPrint.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                        }
                       
                        //string database = "Database=SIVASHAKTHI;Server=192.168.1.4\\sbpl;User ID=sbpl;Password=sbpl123";
                        //SqlConnection con = new SqlConnection(database);
                        //con.Open();
                        //string Query = "insert into HR_APPL_FINGER_PRINTS(HAFP_ID,HAFP_APPL_NUMBER,HAFP_FINGER_FP1)values('" + txtEcodeSearch.Text + "','" + txtApplNo.Text + "',@PH)";
                        //SqlCommand cmd = new SqlCommand(Query, con);
                        //SqlParameter sqp = new SqlParameter("PH", template);
                        //cmd.Parameters.Add(sqp);
                        //cmd.ExecuteNonQuery();
                        //MessageBox.Show("Sucess");
                    //}

                }
                catch
                {
                    MessageBox.Show("Failed");
                    btnSave.Enabled = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int iretuval = 0;
               
                if (flagUpdate == false)
                {
                    string database = "Database=SIVASHAKTHI;Server=202.63.115.34\\sbpl;User ID=sbpl;Password=sbpl123";
                    SqlConnection con = new SqlConnection(database);

                    con.Open();
                    string Query = "insert into HR_APPL_FINGER_PRINTS(HAFP_ID,HAFP_APPL_NUMBER,HAFP_FINGER_FP1,HAFP_CREATED_DATE,HAFP_CREATED_BY)values('" + txtEcodeSearch.Text + "','" + txtApplNo.Text + "',@PH,getdate(),'" + CommonData.LogUserId + "')";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlParameter sqp = new SqlParameter("PH", template);
                    cmd.Parameters.Add(sqp);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (btnEnroll.Enabled)
                {
                    string database = "Database=SIVASHAKTHI;Server=202.63.115.34\\sbpl;User ID=sbpl;Password=sbpl123";
                    SqlConnection con = new SqlConnection(database);

                    con.Open();
                    Image img = picFigerPrint.BackgroundImage;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));



                    string qry = " update HR_APPL_FINGER_PRINTS set HAFP_FINGER_FP2=@Logo where " +
                        " HAFP_APPL_NUMBER=" + txtApplNo.Text + "";
                    SqlCommand SqlCom = new SqlCommand(qry, con);
                    if (arr.Length > 1)
                        SqlCom.Parameters.Add(new SqlParameter("@Logo", (object)arr));
                    //con.Open();
                    iretuval = SqlCom.ExecuteNonQuery();
                    con.Close();
                }

                if (btnEnroll2.Enabled)
                {
                    string database = "Database=SIVASHAKTHI;Server=202.63.115.34\\sbpl;User ID=sbpl;Password=sbpl123";
                    SqlConnection con = new SqlConnection(database);

                    con.Open();

                   


                    Image img = picFigerPrint2.BackgroundImage;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));



                    string qry = " update HR_APPL_FINGER_PRINTS set HAFP_FINGER_FP3=@Logo,HAFP_FINGER_FP4=@Logo2 where " +
                        " HAFP_APPL_NUMBER=" + txtApplNo.Text + "";
                    SqlCommand SqlCom = new SqlCommand(qry, con);
                    if (arr.Length > 1)
                        SqlCom.Parameters.Add(new SqlParameter("@Logo", (object)arr));
                    SqlCom.Parameters.Add(new SqlParameter("@Logo2",template));
                    //con.Open();
                    iretuval = SqlCom.ExecuteNonQuery();
                    con.Close();
                }

                if (iretuval > 0)
                {
                    MessageBox.Show("Employee Enrollment Done Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null,null);
                }
                else
                {
                    MessageBox.Show("Employee Enrollment Not Done", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(flagUpdate==true )
            {
                try
                {
                    string strCMD = " delete from HR_APPL_FINGER_PRINTS where HAFP_APPL_NUMBER="+txtApplNo.Text+" ";
                    objDB = new SQLDB();
                    int i = objDB.ExecuteSaveData(strCMD);
                    if(i>0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnClear_Click(null,null);
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void EmployeeFingerEnroll_Load(object sender, EventArgs e)
        {

        }

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtEcodeSearch.Text.Length > 4)
                {
                    FillEmployeeMasterData();
                }
                else
                {
                    //lblMessage.Visible = true;
                    picFigerPrint.BackgroundImage = global::SSCRM.Properties.Resources.enroll;

                    flagUpdate = false;

                    btnSave.Enabled = false;
                    btnEnroll.Enabled = true;
                    btnDelete.Enabled = true;
                    txtName.Text = "";
                    txtFatherName.Text = "";
                    txtDesig.Text = "";
                    txtDept.Text = "";
                    txtCompany.Text = "";
                    txtBranch.Text = "";
                    txtApplNo.Text = "";
                    meHRISDataofBirth.Text = "";
                    meHRISDateOfJoin.Text = "";
                    picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;
                    picFigerPrint.BackgroundImage = null;
                    picFigerPrint2.BackgroundImage = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEnroll2_Click(object sender, EventArgs e)
        {
            if (txtApplNo.Text.Length > 0)
            {
                try
                {
                    //DialogResult result = MessageBox.Show("Please put your Finger on the Device",
                    //                             "CRM", MessageBoxButtons.YesNo);
                    //if (result == DialogResult.Yes)
                    //{
                    FingerDemo obj = new FingerDemo();
                    obj.Branch = txtBranch.Tag.ToString();
                    obj.lblMessage.Text = "Please Put finger on the Device";
                    obj.ShowDialog();
                    if (obj.fpstatus == true)
                    {
                        MessageBox.Show("FingerPrint Enrolled With Other's Ecode - "+obj.sEcode.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        //template = obj.Emptemplate;
                        //picFigerPrint2.BackgroundImage = obj.PictureBox1.Image;
                        //if (obj.Emptemplate != null)
                        //{
                        //    btnSave.Enabled = true;
                        //}
                        //else
                        //{
                        //    picFigerPrint2.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                        //}
                        ////btnSave.Enabled = true;
                        ////lblMessage.Visible = false;
                        ////picFigerPrint.BackgroundImage = global::SSCRM.Properties.Resources.enroll;

                        




                        template = obj.Emptemplate;

                        FingerDemo obj1 = new FingerDemo();
                        obj1.Branch = txtBranch.Tag.ToString();
                        obj1.oldEmptemplate = template;
                        obj1.lblMessage.Text = "To Confirm Again Put finger on the Device";
                        obj1.ShowDialog();


                        if (obj1.fpstatus == true)
                        {
                            picFigerPrint2.BackgroundImage = obj.PictureBox1.Image;
                            if (obj.Emptemplate != null)
                            {
                                btnSave.Enabled = true;
                            }

                        }
                        else
                        {
                            picFigerPrint2.BackgroundImage = global::SSCRM.Properties.Resources.enroll;
                            btnSave.Enabled = false;
                        }
















                    }

                    //string database = "Database=SIVASHAKTHI;Server=192.168.1.4\\sbpl;User ID=sbpl;Password=sbpl123";
                    //SqlConnection con = new SqlConnection(database);
                    //con.Open();
                    //string Query = "insert into HR_APPL_FINGER_PRINTS(HAFP_ID,HAFP_APPL_NUMBER,HAFP_FINGER_FP1)values('" + txtEcodeSearch.Text + "','" + txtApplNo.Text + "',@PH)";
                    //SqlCommand cmd = new SqlCommand(Query, con);
                    //SqlParameter sqp = new SqlParameter("PH", template);
                    //cmd.Parameters.Add(sqp);
                    //cmd.ExecuteNonQuery();
                    //MessageBox.Show("Sucess");
                    //}

                }
                catch
                {
                    MessageBox.Show("Failed");
                    btnSave.Enabled = false;
                }
            }
        }

      

      
    }
}
