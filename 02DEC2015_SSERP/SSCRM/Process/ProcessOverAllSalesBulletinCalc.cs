using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Threading;

namespace SSCRM
{
    public partial class ProcessOverAllSalesBulletinCalc : Form
    {
        SQLDB objDB = new SQLDB();
        public ProcessOverAllSalesBulletinCalc()
        {
            InitializeComponent();
        }

        

        private void ProcessOverAllSalesBulletinCalc_Load(object sender, EventArgs e)
        {
            //picProcess.Visible = false;
            lblProcess.Visible = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            SSCRM.ProcessFrm pBar = new SSCRM.ProcessFrm();
            pBar.Visible = true;
            pBar.backgroundWorker1.WorkerReportsProgress = true;
            pBar.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(pBar.backgroundWorker1_ProgressChanged);
            pBar.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(pBar.backgroundWorker1_DoWork);
            pBar.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(pBar.backgroundWorker1_RunWorkerCompleted);
            pBar.backgroundWorker1.RunWorkerAsync();

            FUNCTIONTORUN();

            pBar.Visible = false;


            
        }

        private void FUNCTIONTORUN()
        {
            lblProcess.Visible = false;
            //picProcess.Visible = true;
            objDB = new SQLDB();
            int iRes = 0;
            string sqlText = "";
            sqlText = "EXEC RUN_BULLETIN_PROCESS '','','',''";
            //sqlText = "DELETE FROM BPS_SALES_BULLETINS_HEAD --WHERE SIBH_DOCUMENT_MONTH = @xDoc_month"+
            //                " GO"+
            //                " INSERT INTO BPS_SALES_BULLETINS_HEAD"+
            //                " SELECT SIBH_COMPANY_CODE,SIBH_STATE_CODE,SIBH_BRANCH_CODE,SIBH_FIN_YEAR,SIBH_DOCUMENT_MONTH,ISNULL(HAAM_EMP_CODE,SIBH_EORA_CODE) SIBH_EORA_CODE"+
            //                ",COUNT(SIBH_INVOICE_NUMBER) SIBH_INVOICE_COUNT,ISNULL(SUM(SIBH_INVOICE_AMOUNT),0) SIBH_INVOICE_AMOUNT,ISNULL(SUM(SIBH_ADVANCE_AMOUNT),0) SIBH_ADVANCE_AMOUNT"+
            //                ",ISNULL(SUM(SIBH_RECEIVED_AMOUNT),0) SIBH_RECEIVED_AMOUNT FROM SALES_INV_BULTIN_HEAD LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = SIBH_EORA_CODE"+                            
            //                " GROUP BY SIBH_COMPANY_CODE,SIBH_STATE_CODE,SIBH_BRANCH_CODE,SIBH_FIN_YEAR,SIBH_DOCUMENT_MONTH,HAAM_EMP_CODE,SIBH_EORA_CODE"+
            //                " GO"+
            //                " DELETE FROM BPS_SALES_INV_BULLETIN_DETL"+
            //                " GO"+
            //                " INSERT INTO BPS_SALES_INV_BULLETIN_DETL"+
            //                " SELECT SIBD_COMPANY_CODE,SIBD_STATE_CODE,SIBD_BRANCH_CODE,SIBD_FIN_YEAR,SIBH_DOCUMENT_MONTH SIBD_DOCUMENT_MONTH,ISNULL(HAAM_EMP_CODE,SIBH_EORA_CODE) SIBD_EORA_CODE"+
            //                ",SIBD_PRODUCT_ID,ISNULL(AVG(SIBD_PRICE),0) SIBD_PRICE,ISNULL(SUM(SIBD_QTY),0) SIBD_QTY,ISNULL(SUM(SIBD_AMOUNT),0) SIBD_AMOUNT,ISNULL(SUM(SIBD_PRODUCT_POINTS),0) SIBD_PRODUCT_POINTS"+
            //                " FROM SALES_INV_BULTIN_HEAD INNER JOIN SALES_INV_BULTIN_DETL ON SIBD_BRANCH_CODE = SIBH_BRANCH_CODE AND SIBD_FIN_YEAR = SIBH_FIN_YEAR AND SIBD_INVOICE_NUMBER = SIBH_INVOICE_NUMBER"+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = SIBH_EORA_CODE "+
            //                " GROUP BY SIBD_COMPANY_CODE,SIBD_STATE_CODE,SIBD_BRANCH_CODE,SIBD_FIN_YEAR,SIBH_DOCUMENT_MONTH,HAAM_EMP_CODE,SIBH_EORA_CODE,SIBD_PRODUCT_ID"+
            //                " GO"+
            //                " DELETE FROM BPS_LEVELGROUP_MAP_SOURCE_DEST"+
            //                " GO"+
            //                " INSERT INTO BPS_LEVELGROUP_MAP_SOURCE_DEST(lgsd_dest_code,lgsd_doc_month,lgsd_source_ecode)"+
            //                " SELECT DISTINCT * FROM (SELECT DISTINCT ISNULL(DC.HAAM_EMP_CODE,lgmd_dest_ecode) lgmd_dest_ecode,lgmd_doc_month,ISNULL(SC.HAAM_EMP_CODE,lgm_source_ecode) lgm_source_ecode"+
            //                " FROM LevelGroup_map_Detl INNER JOIN LevelGroup_map ON lgm_group_ecode = lgmd_group_ecode AND lgm_doc_month = lgmd_doc_month "+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION DC ON DC.HAAM_AGENT_CODE = lgmd_dest_ecode"+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION SC ON SC.HAAM_AGENT_CODE = lgm_source_ecode"+
            //                " UNION"+
            //                " SELECT DISTINCT ISNULL(DC.HAAM_EMP_CODE,lgmd_dest_ecode) lgmd_dest_ecode,lgmd_doc_month,ISNULL(SC.HAAM_EMP_CODE,lgmd_group_ecode) lgmd_group_ecode  FROM LevelGroup_map_Detl "+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION DC ON DC.HAAM_AGENT_CODE = lgmd_dest_ecode LEFT JOIN HR_APPL_A2E_MIGRATION SC ON SC.HAAM_AGENT_CODE = lgmd_group_ecode"+
            //                " UNION"+
            //                " SELECT DISTINCT ISNULL(DC.HAAM_EMP_CODE,lgmd_dest_ecode) lgmd_dest_ecode,lgmd_doc_month,ISNULL(DC.HAAM_EMP_CODE,lgmd_dest_ecode) lgmd_dest_ecode FROM LevelGroup_map_Detl "+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION DC ON DC.HAAM_AGENT_CODE = lgmd_dest_ecode) AS TABLE1"+
            //                " GO"+
            //                " INSERT INTO BPS_LEVELGROUP_MAP_SOURCE_DEST(lgsd_dest_code,lgsd_doc_month,lgsd_source_ecode)"+
            //                " SELECT ISNULL(GC.HAAM_EMP_CODE,lgm_group_ecode),lgm_doc_month,ISNULL(SR.HAAM_EMP_CODE,lgm_source_ecode) FROM LevelGroup_map "+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION GC ON GC.HAAM_AGENT_CODE = lgm_group_ecode"+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION SR ON SR.HAAM_AGENT_CODE = lgm_source_ecode"+
            //                " WHERE lgm_group_ecode NOT IN (select distinct lgmd_dest_ecode from LevelGroup_map_Detl where lgmd_doc_month = lgm_doc_month and lgmd_branch_code = lgm_branch_code)"+
            //                " UNION"+
            //                " SELECT DISTINCT ISNULL(GC.HAAM_EMP_CODE,lgm_group_ecode),lgm_doc_month,ISNULL(GC.HAAM_EMP_CODE,lgm_group_ecode) FROM LevelGroup_map"+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION GC ON GC.HAAM_AGENT_CODE = lgm_group_ecode"+
            //                " WHERE lgm_group_ecode NOT IN (select distinct lgmd_dest_ecode from LevelGroup_map_Detl where lgmd_doc_month = lgm_doc_month and lgmd_branch_code = lgm_branch_code)"+
            //                " GO"+
            //                " DELETE FROM BPS_GROUP_COUNT "+
            //                " GO"+
            //                " INSERT INTO BPS_GROUP_COUNT(lgmd_dest_ecode,lgmd_doc_month,lgmd_groups,lgmd_SRs)"+
            //                " select distinct ISNULL(HAAM_EMP_CODE,lgmd_dest_ecode) lgmd_dest_ecode,lgmd_doc_month,count(DISTINCT(lgmd_group_ecode)) lgmd_groups,count(DISTINCT(lgmd_group_ecode))+count(DISTINCT(lgm_source_ecode)) rs_srs"+
            //                " from LevelGroup_map_Detl"+
            //                " INNER JOIN LevelGroup_map ON lgm_group_ecode = lgmd_group_ecode and lgm_doc_month = lgmd_doc_month"+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = lgmd_dest_ecode"+
            //                " where lgm_group_name != 'OFFICE SALES'"+
            //                " GROUP BY HAAM_EMP_CODE,lgmd_dest_ecode,lgmd_doc_month"+
            //                " GO"+
            //                " INSERT INTO BPS_GROUP_COUNT(lgmd_dest_ecode,lgmd_doc_month,lgmd_groups,lgmd_SRs)"+
            //                " select distinct ISNULL(HAAM_EMP_CODE,lgm_group_ecode) lgm_group_ecode,lgm_doc_month,'1' lgmd_groups,COUNT(lgm_source_ecode) lgmd_srs"+
            //                " from LevelGroup_map"+
            //                " LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = lgm_group_ecode"+
            //                " where lgm_group_name != 'OFFICE SALES'"+
            //                " AND lgm_group_ecode not in (select distinct lgmd_dest_ecode from LevelGroup_map_Detl where lgmd_doc_month = lgm_doc_month and lgmd_branch_code = lgm_branch_code)"+
            //                " GROUP BY HAAM_EMP_CODE,lgm_group_ecode,lgm_doc_month";
            try
            {
                iRes = objDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDB = null;
            }
            if (iRes > 0)
            {
                MessageBox.Show("Processed " + iRes + " Records", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblProcess.Visible = true;
                //picProcess.Visible = false;
            }
            else
            {
                lblProcess.Visible = true;
                //picProcess.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);

                //run in back thread
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //progressBar1.Value = progressBar1.Maximum;
        }
    }
}
