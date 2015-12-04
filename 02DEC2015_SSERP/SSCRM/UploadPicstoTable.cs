using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.IO;
using SSTrans;
using SSAdmin;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class UploadPicstoTable : Form
    {
        private SQLDB objData = null;
        private Security objSecurity = null;
        public UploadPicstoTable()
        {
            InitializeComponent();
        }

        private void UploadPicstoTable_Load(object sender, EventArgs e)
        {
            //UploadAllPicstoTable();
        }

        private void UploadAllPicstoTable()
        {
            for (int i = 0; i < 282; i++)
            {
                byte[] imageData = { 0 };
                imageData = ReadFile("C:\\Users\\NareshKumar\\Desktop\\PHOTOS\\"+(i+1).ToString()+".jpg");
                SaveCompany((i+1).ToString(), imageData);
            }
        }
        byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }
        public int SaveCompany(string CompanyCode, byte[] sLogo)
        {
            int iretuval = 0;
            try
            {
                string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                objSecurity = new Security();
                SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                string qry = "";
                objData = new SQLDB();                
                if (sLogo.Length > 1)
                    qry = "UPDATE TDP_MEMBERS SET TDP_MEMBER_PHOTO=@Logo WHERE TDP_CODE=" + CompanyCode + "";
                   
                SqlCommand SqlCom = new SqlCommand(qry, CN);
                if (sLogo.Length > 1)
                    SqlCom.Parameters.Add(new SqlParameter("@Logo", (object)sLogo));
                CN.Open();
                iretuval = SqlCom.ExecuteNonQuery();
                CN.Close();
                return iretuval;
            }
            catch (Exception ex)
            {
                objSecurity = null;
                return iretuval = 0;
            }
            finally
            {
                objSecurity = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //string strSQL = " INSERT INTO SkuDescriptions(SkuCode,SkuDetails,CreatedBy,CreatedDate)" +
            //                       " SELECT '" + txtSKU.Text + "','" + rtbSkuNotes.Text + "','" + CommonData.LogUserId + "' ,GETDATE() " +
            //                       " WHERE NOT EXISTS (SELECT *  from SkuDescriptions WHERE SkuCode = '" + txtSKU.Text + "')" +
            //                       " UPDATE SkuDescriptions SET SkuDetails ='" + rtbSkuNotes.Text + "',ModifiedBy='" + CommonData.LogUserId +
            //                       "', ModifiedDate=getdate() WHERE SkuCode='" + txtSKU.Text + "'" +
            //                       " UPDATE SKUMaster set SkuDesc='" + txtSKUDesc.Text + "'  where SkuCode='" + txtSKU.Text + "' ";

            //  objSQLDb = new SQLDB();

            string database = "Database=POS1516;Server=202.63.115.37\\SBPL;User ID=sa;Password=sa123";
            SqlConnection objSQLConn = new SqlConnection(database);
            objSQLConn.Open();


            //SqlCommand objCommand = new SqlCommand(strSQL, objSQLConn);
            //objCommand.CommandType = CommandType.Text;
            //objCommand.Transaction = objSQLConn.BeginTransaction();
            //int recordsInserted = objCommand.ExecuteNonQuery();
            //objCommand.Transaction.Commit();



            string[] files = Directory.GetFiles("D:\\Daman-ECommerceFiles\\daman-skuImages.war");

            string strSQLs = "";
            for (int iFile = 0; iFile < files.Length; iFile++)
            {
                string str = new FileInfo(files[iFile]).Name;
               // string [] str1 = str.Split('.');
                if(str.Length==10)
                strSQLs += "insert into temp_daman_pics(picskucode)values('"+str.Substring(0,6)+"')";

            }
            SqlCommand objCommand = new SqlCommand(strSQLs, objSQLConn);
            objCommand.CommandType = CommandType.Text;
            objCommand.Transaction = objSQLConn.BeginTransaction();
            int recordsInserted = objCommand.ExecuteNonQuery();
            objCommand.Transaction.Commit();
        }
    }
}
