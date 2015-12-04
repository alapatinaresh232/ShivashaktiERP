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
    public partial class TrainingPrgTopicsDetl : Form
    {
        SQLDB objSQLdb = null;
        public TrainingProgramDetails objTrainingProgramDetails;

        DataRow[] drs;
        string StrName = "";
        string strDesig = "";
        int EmpEcode = 0;
        string sTrainerName = "";
        string strTrDetl = "";
        string sTrainerType = "";

        public TrainingPrgTopicsDetl()
        {
            InitializeComponent();
        }
        public TrainingPrgTopicsDetl(string stType,int iEcode,string SEname,string sDesig)
        {
            InitializeComponent();
            EmpEcode = iEcode;
            StrName = SEname;
            strDesig = sDesig;
            sTrainerType = stType;
        }

        public TrainingPrgTopicsDetl(string stType, string sTrName, string TrDetl)
        {
            InitializeComponent();
            sTrainerType = stType;
            sTrainerName = sTrName;
            strTrDetl = TrDetl;
        }
        public TrainingPrgTopicsDetl(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void TrainingPrgTopicsDetl_Load(object sender, EventArgs e)
        {
            FillTopicTypes();          
            cbMethodologyType.SelectedIndex = 0;

            cbTrainerType.Text = sTrainerType;
            txtEcodeSearch.Text = Convert.ToString(EmpEcode);
            txtEName.Text = StrName;
            txtEmpDesg.Text = strDesig;
            txtTrainerName.Text = sTrainerName;
            txtTrainerDetl.Text = strTrDetl;

            if (drs != null)
            {
                cbTopicType.Text = drs[0]["TopicType"].ToString();
                cbTopicName.Text = drs[0]["TopicName"].ToString();
                cbMethodologyType.Text = drs[0]["MethodType"].ToString();

                if (drs[0]["StartTime"].ToString().Length != 0)
                {
                    txtSTHours.Text = drs[0]["StartTime"].ToString().Split('.')[0];
                    txtSTMinutes.Text = drs[0]["StartTime"].ToString().Split('.')[1];
                }

                if (drs[0]["EndTime"].ToString().Length != 0)
                {
                    txtCLHours.Text = drs[0]["EndTime"].ToString().Split('.')[0];
                    txtCLMinutes.Text = drs[0]["EndTime"].ToString().Split('.')[1];
                }

                txtRemarks.Text = drs[0]["Remarks"].ToString();

                if (drs[0]["TrainerType"].ToString().Equals("INTERNAL"))
                {
                    cbTrainerType.SelectedIndex = 0;
                    txtEcodeSearch.Text = drs[0]["TrainerEcode"].ToString();
                    txtEName.Text = drs[0]["InternalTrName"].ToString();
                    txtEmpDesg.Text = drs[0]["EmpDesig"].ToString();
                }
                else
                {
                    cbTrainerType.SelectedIndex = 1;
                    txtTrainerName.Text = drs[0]["TrainerName"].ToString();
                    txtTrainerDetl.Text = drs[0]["TrainerDetails"].ToString();
                }


            }
        }

        private void FillTopicTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT HTTTM_TOPIC_TYPE FROM HR_TRAINING_TOPIC_TYPE_MASTER " +
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

                    dt.Rows.InsertAt(row, 0);

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

        private bool CheckData()
        {
            bool flag = true;

            if (cbTopicType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Topic Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            else if (cbTopicName.SelectedIndex == 0 || cbTopicName.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Topic Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            else if (cbMethodologyType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Method Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            if (txtSTHours.Text.Trim().Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Training Starting Time", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSTHours.Focus();
                return flag;
            }
            if (txtCLHours.Text.Trim().Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Training Starting Time", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCLHours.Focus();
                return flag;
            }

            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool bFlag = false;
            string StartTime = "", EndTime = "";

            if (CheckData())
            {
                if (drs != null)
                {
                    ((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows.Remove(drs[0]);
                }

                if (txtSTMinutes.Text.Trim().Length == 0)
                    txtSTMinutes.Text = "00";
                if (txtCLMinutes.Text.Trim().Length == 0)
                    txtCLMinutes.Text = "00";

                StartTime = (txtSTHours.Text) + '.' + (txtSTMinutes.Text);
                EndTime = (txtCLHours.Text) + '.' + (txtCLMinutes.Text);  

                if (cbTrainerType.Text.Equals("INTERNAL"))
                {

                    if (((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows.Count > 0)
                    {
                        for (int i = 0; i < ((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows.Count; i++)
                        {
                            if (cbTopicType.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["TopicType"].ToString()))
                            {
                                if (cbTopicName.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["TopicName"].ToString()))
                                {
                                    if (cbMethodologyType.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["MethodType"].ToString()))
                                    {
                                        if (txtEcodeSearch.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["TrainerEcode"].ToString()))
                                        {
                                            if (StartTime.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["StartTime"].ToString()))
                                            {
                                                bFlag = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                            }

                        }

                    }


                    if (bFlag == false)
                    {
                        ((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows.Add(new Object[] { "-1", cbTopicName.SelectedValue.ToString(),cbTopicType.Text.ToString(),
                            cbTopicName.Text.ToString(),cbMethodologyType.Text.ToString(),"INTERNAL",txtEcodeSearch.Text.ToString(),
                            "","",txtEName.Text.ToString(),txtEmpDesg.Text.ToString(),StartTime,EndTime,txtRemarks.Text.ToString().Replace("'","")});

                        ((TrainingProgramDetails)objTrainingProgramDetails).GetTrainingMethodDetails();
                    }
                }
                else if (cbTrainerType.Text.Equals("EXTERNAL"))
                {

                    if (((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows.Count > 0)
                    {
                        for (int i = 0; i < ((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows.Count; i++)
                        {
                            if (cbTopicType.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["TopicType"].ToString()))
                            {
                                if (cbTopicName.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["TopicName"].ToString()))
                                {
                                    if (cbMethodologyType.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["MethodType"].ToString()))
                                    {
                                        if (txtTrainerName.Text.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["TrainerName"].ToString()))
                                        {
                                            if (StartTime.Equals(((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows[i]["StartTime"].ToString()))
                                            {
                                                bFlag = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                            }


                        }

                    }


                    if (bFlag == false)
                    {
                        ((TrainingProgramDetails)objTrainingProgramDetails).dtTopicMethodDetl.Rows.Add(new Object[] { "-1", cbTopicName.SelectedValue.ToString(),cbTopicType.Text.ToString(),
                            cbTopicName.Text.ToString(),cbMethodologyType.Text.ToString(),"EXTERNAL","0",txtTrainerName.Text.ToString(),txtTrainerDetl.Text.ToString(),"","",
                        StartTime,EndTime,txtRemarks.Text.ToString().Replace("'","")});

                        ((TrainingProgramDetails)objTrainingProgramDetails).GetTrainingMethodDetails();
                    }
                }


                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbMethodologyType.SelectedIndex = 0;
            cbTopicType.SelectedIndex = 0;
            cbTopicName.SelectedIndex = -1;
            txtSTHours.Text = "";
            txtSTMinutes.Text = "";
            txtCLHours.Text = "";
            txtCLMinutes.Text = "";
            txtRemarks.Text = "";
        }

        private void cbTrainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrainerType.SelectedIndex == 0)
            {
                lblName.Visible = false;
                lblTrDetl.Visible = false;
                txtTrainerName.Visible = false;
                txtTrainerDetl.Visible = false;

                lblEcode.Visible = true;
                lblDesg.Visible = true;
                txtEcodeSearch.Visible = true;
                txtEName.Visible = true;
                txtEmpDesg.Visible = true;

            }
            else if (cbTrainerType.SelectedIndex == 1)
            {
                lblName.Visible = true;
                lblTrDetl.Visible = true;
                txtTrainerName.Visible = true;
                txtTrainerDetl.Visible = true;

                lblEcode.Visible = false;
                lblDesg.Visible = false;
                txtEcodeSearch.Visible = false;
                txtEName.Visible = false;
                txtEmpDesg.Visible = false;
            }
        }

        private void cbTopicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTopicType.SelectedIndex > 0)
            {
                FillTopicNames();
            }
        }

        private void txtTrainerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
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

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {

                try
                {
                    string strCommand = "SELECT MEMBER_NAME,desig_name FROM EORA_MASTER " +
                                        " INNER JOIN DESIG_MAS ON desig_code=DESG_ID " +
                                        " WHERE ECODE =" + Convert.ToInt32(txtEcodeSearch.Text) + " ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtEmpDesg.Text = dt.Rows[0]["desig_name"].ToString();

                    }
                    else
                    {
                        txtEName.Text = "";
                        txtEmpDesg.Text = "";
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
        }

        private void txtSTHours_Validated(object sender, EventArgs e)
        {
            if ((txtSTHours.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtSTHours.Text) >= 24)
                {
                    MessageBox.Show("please enter valid Starting Hours");
                    txtSTHours.Focus();

                }
            }
        }

        private void txtSTMinutes_Validated(object sender, EventArgs e)
        {
            if ((txtSTMinutes.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtSTMinutes.Text) >= 60)
                {
                    MessageBox.Show("please enter valid Starting Time in Minutes");
                    txtSTMinutes.Focus();
                }
            }
        }

        private void txtCLHours_Validated(object sender, EventArgs e)
        {
            if ((txtCLHours.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtCLHours.Text) >= 24)
                {
                    MessageBox.Show("please enter valid Closing Hours");
                    txtCLHours.Focus();

                }
            }
        }

        private void txtCLMinutes_Validated(object sender, EventArgs e)
        {
            if ((txtCLMinutes.Text.Length) > 0)
            {
                if (Convert.ToInt32(txtCLMinutes.Text) >= 60)
                {
                    MessageBox.Show("please enter valid Closing Time in Minutes");
                    txtCLMinutes.Focus();
                }
            }
        }

    
    
    }
}
