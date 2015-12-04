using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

//using SSCRM;

namespace ZkFingerDemo
{
    public partial class FingerDemo : Form
    {
        //public EmployeeFingerEnroll obj = null;
        public byte[] Emptemplate;
        private int FMatchType, fpcHandle;        
        private bool FAutoIdentify;
        bool step2;
        public bool fpstatus;
        public int sEcode;
        public string Branch,Message;
        public byte[] oldEmptemplate;
        public FingerDemo()
        {
            InitializeComponent();
        }
        //public FingerDemo(EmployeeFingerEnroll objs)
        //{
        //    //obj = objs;
        //    InitializeComponent();
        //}



        //Show hint image
        private void ShowHintImage(int iType)
        {

            this.Refresh();
        }

        //Show fingerprint image
        private void ZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            ShowHintImage(0);
            Graphics g = PictureBox1.CreateGraphics();
            Bitmap bmp = new Bitmap(PictureBox1.Width, PictureBox1.Height);
            g = Graphics.FromImage(bmp);
            int dc = g.GetHdc().ToInt32();
            ZKFPEngX1.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            PictureBox1.Image = bmp;
            //obj.picFigerPrint.Image = bmp;
        }

        //
        private void ZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (e.actionResult)
            {
                try
                {
                    e.aTemplate = ZKFPEngX1.GetTemplate();
                    if (lblMessage.Text == "To Confirm Again Put finger on the Device")
                    {

                        if (ZKFPEngX1.VerFinger(ref e.aTemplate, oldEmptemplate, true, ref step2))
                        {

                            fpstatus = true;

                            this.Close();
                            return;
                        }
                        else
                        {
                            fpstatus = false;
                            MessageBox.Show("Fingerprint Not Matched ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.Close();
                            return;
                        }
                        //Emptemplate = (byte[])e.aTemplate;
                        ZKFPEngX1.EndEngine();
                        MessageBox.Show("Fingerprint Added Successfully ");
                        this.Close();
                    }

                    else
                    {
                        string database = "Database=SIVASHAKTHI;Server=202.63.115.34\\SBPL;User ID=sbpl;Password=sbpl123";
                        SqlConnection con = new SqlConnection(database);
                        con.Open();
                        string Query = "";
                        Query = "select HAFP_FINGER_FP1,HAFP_FINGER_FP4 from HR_APPL_FINGER_PRINTS ";

                        //SqlCommand cmd = new SqlCommand(Query, con);

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "GetEmpFingerPrintsFromBranch";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter param = new SqlParameter();

                        param.ParameterName = "@xBranchCode";
                        param.DbType = DbType.String;
                        param.Value = Branch;
                        param.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        //DataSet ds = null;

                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            DataTable dt = ds.Tables[0];
                            foreach (DataRow row in dt.Rows)
                            {
                                byte[] fptemp2 = (byte[])row["HAFP_FINGER_FP1"];


                                if (ZKFPEngX1.VerFinger(ref e.aTemplate, fptemp2, true, ref step2))
                                {

                                    fpstatus = true;
                                   // sEcode  = (int)row["ECODE"];
                                    sEcode = Convert.ToInt32(row["ECODE"].ToString());
                                    this.Close();
                                    return;
                                }
                                else
                                {
                                    fpstatus = false;
                                    sEcode = Convert.ToInt32(row["ECODE"].ToString());
                                    this.Close();
                                }

                                if (!DBNull.Value.Equals(row["HAFP_FINGER_FP4"]))
                                {
                                    fptemp2 = (byte[])row["HAFP_FINGER_FP4"];


                                    if (ZKFPEngX1.VerFinger(ref e.aTemplate, fptemp2, true, ref step2))
                                    {

                                        fpstatus = true;
                                        //sEcode = (int)row["ECODE"];
                                        sEcode = Convert.ToInt32(row["ECODE"].ToString());
                                        this.Close();
                                        return;
                                    }
                                    else
                                    {
                                        fpstatus = false;
                                        //sEcode = (int)row["ECODE"];
                                        sEcode = Convert.ToInt32(row["ECODE"].ToString());
                                        this.Close();
                                    }
                                }
                            }
                        }
                        Emptemplate = (byte[])e.aTemplate;
                        ZKFPEngX1.EndEngine();
                       

                        //MessageBox.Show("Updated");
                        //PictureBox1.Image = null;
                      
                    }
                 
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    PictureBox1.Image = null;
                }

            }
            else
            {
                //ShowHintInfo("Fingerprint register failed");
                MessageBox.Show("Fingerprint register failed ", "提示! ", MessageBoxButtons.YesNo,MessageBoxIcon.Stop);
                this.Close();
            }



        }










        







        //窗口初始化
        private void FingerDemo_Load(object sender, EventArgs e)
        {

            if (ZKFPEngX1.InitEngine() == 0)
            {

                ZKFPEngX1.FPEngineVersion = "9";

                ZKFPEngX1.EnrollCount = 1;

                ZKFPEngX1.CancelEnroll();
                ZKFPEngX1.EnrollCount = 1;
                ZKFPEngX1.BeginEnroll();
            }
            else
            {
                MessageBox.Show("Failed to connect Device \n"
                                + "Please Connect properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }

        }









    }
}
