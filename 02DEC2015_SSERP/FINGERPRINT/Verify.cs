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

namespace ZkFingerDemo
{
    public partial class Verify : Form
    {
        int match_count;
        public string emp_appl_number;
        public bool fpstatus;
        public DataTable dtVerif;
        bool step;
        bool step2;
        public Verify()
        {
            InitializeComponent();
        }
       

        private void Verify_Load(object sender, EventArgs e)
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
                                 + "Please Connect properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }
        }

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

        }

        //
        private void ZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (e.actionResult)
            {
                try
                {
                    e.aTemplate = ZKFPEngX1.GetTemplate();

                    //string database = "Database=SIVASHAKTHI;Server=202.63.115.34\\SBPL;User ID=sbpl;Password=sbpl123";
                    //SqlConnection con = new SqlConnection(database);
                    //con.Open();
                    //string Query = "";
                    //Query = "select HAFP_FINGER_FP1,HAFP_FINGER_FP4 from HR_APPL_FINGER_PRINTS  where HAFP_APPL_NUMBER='" + emp_appl_number + "' ";
                    //SqlCommand cmd = new SqlCommand(Query, con);
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds);
                    //if (ds.Tables[0].Rows.Count > 0)
                    if (dtVerif.Rows.Count > 0)
                    {
                        try
                        {
                            //DataTable dt = ds.Tables[0];
                            DataTable dt = dtVerif;
                            foreach (DataRow row in dt.Rows)
                            {
                                byte[] fptemp2 = (byte[])row["HAFP_FINGER_FP1"];


                                if (ZKFPEngX1.VerFinger(ref e.aTemplate, fptemp2, true, ref step2))
                                {

                                    fpstatus = true;
                                   
                                    this.Close();
                                    return;
                                }
                                else
                                {
                                    fpstatus = false;

                                    this.Close();
                                }


                                if (!DBNull.Value.Equals(row["HAFP_FINGER_FP4"]))
                                {
                                    fptemp2 = (byte[])row["HAFP_FINGER_FP4"];


                                    if (ZKFPEngX1.VerFinger(ref e.aTemplate, fptemp2, true, ref step2))
                                    {

                                        fpstatus = true;
                                        this.Close();
                                        return;
                                    }
                                    else
                                    {
                                        fpstatus = false;

                                        this.Close();
                                    }
                                }
                            }
                            ZKFPEngX1.EndEngine();
                        }
                        catch (Exception EX)
                        {
                            MessageBox.Show(EX.Message);
                            PictureBox1.Image = null;
                        }

                    }



                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //ShowHintInfo("Fingerprint register failed");
                MessageBox.Show("Fingerprint register failed ", "提示! ", MessageBoxButtons.YesNo,MessageBoxIcon.Stop);
            }



        }

    }
}
