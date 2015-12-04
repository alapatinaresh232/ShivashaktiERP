using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using SSCRMDB;

using System.Windows.Forms;

namespace SSTrans
{
    public class DealerInfo
    {
        private SQLDB objSQLdb = null;
        private Security objSecurity;
        string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
        SqlConnection CN;
        int iRes;
        public string DealerHeadSave( bool updateFlag,object[] MainHead, DataTable dtPartners, DataTable dtPrevTurnOver, DataTable dtBussinessDetails, DataTable dtOtherDealerShips, DataTable dtBussnessVehicles, DataTable dtTerritoryDetl, DataTable dtFixedAssets, DataTable dtSecurityCheqs, DataTable dtBankerDetails)
        {

            try
            {
                //objSecurity = new Security();
                objSQLdb = new SQLDB();
                //CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                //SqlTransaction transaction;
                //CN.Open();
                string sqlText ="";
                if (updateFlag == false)
                {
                    sqlText = "INSERT INTO DL_APPL_MASTER_HEAD(DAMH_COMPANY_CODE,DAMH_BRANCH_CODE,DAMH_APPL_NUMBER,DAMH_APPL_DATE,DAMH_FIRM_NAME,DAMH_FIRM_ADDR_HNO,DAMH_FIRM_ADDR_LANDMARK" +
                        ",DAMH_FIRM_ADDR_VILL_OR_TOWN,DAMH_FIRM_ADDR_MANDAL,DAMH_FIRM_ADDR_DISTRICT,DAMH_FIRM_ADDR_STATE,DAMH_FIRM_ADDR_PIN,DAMH_FIRM_ADDR_PHONE,DAMH_FIRM_ADDR_OFF_PHONE,DAMH_FIRM_ADDR_RES_PHONE" +
                        ",DAMH_FIRM_ADDR_FAX,DAMH_FIRM_ADDR_MOBILE,DAMH_FIRM_ADDR_EMAIL,DAMH_DEALER_NAME,DAMH_SEX,DAMH_DOB,DAMH_FORH,DAMH_FORH_NAME,DAMH_FIRM_HEAD_HNO,DAMH_FIRM_HEAD_LANDMARK,DAMH_FIRM_HEAD_VILL_OR_TOWN" +
                        ",DAMH_FIRM_HEAD_MANDAL,DAMH_FIRM_HEAD_DISTRICT,DAMH_FIRM_HEAD_STATE,DAMH_FIRM_HEAD_PIN,DAMH_FIRM_HEAD_PHONE,DAMH_FIRM_HEAD_OFF_PHONE,DAMH_FIRM_HEAD_RES_PHONE,DAMH_FIRM_HEAD_FAX,DAMH_FIRM_HEAD_MOBILE" +
                        ",DAMH_FIRM_HEAD_EMAIL,DAMH_DOJ,DAMH_FIRM_TYPE,DAMH_VAT_NUMBER,DAMH_CST_NUMBER,DAMH_IT_PAN_NUMBER,DAMH_PESTICIDE_LICENSE_NUMBER,DAMH_PESTICIDE_LICENSE_DATE,DAMH_PESTICIDE_LICENSE_VALID_UPTO" +
                        ",DAMH_PESTICIDE_LICENSE_ISSUED_BY,DAMH_FERTILIZR_LICENSE_NUMBER,DAMH_FERTILIZR_LICENSE_DATE,DAMH_FERTILIZR_LICENSE_VALID_UPTO,DAMH_FERTILIZR_LICENSE_ISSUED_BY,DAMH_PRESENT_BUSINESS,DAMH_BUSINESS_TYPE" +
                        ",DAMH_PREV_YR_PEST_TURNOVER_WHSALE,DAMH_PREV_YR_PEST_TURNOVER_RETAIL,DAMH_NOOF_DEALERS,DAMH_PRESENT_AREA_VILL_OR_TOWN,DAMH_PRESENT_AREA_MANDAL,DAMH_PRESENT_AREA_DISTRICT,DAMH_PRESENT_AREA_STATE" +
                        ",DAMH_GODOWN_SPACE_SFT,DAMH_NOOF_EMPLOYEES,DAMH_EXPECTED_TURNOVER_OUR_PROD,DAMH_EXPECTED_CAPITAL_OUR_PROD,DAMH_FINANCE_ARRANGE_TYPE,DAMH_REPRESENT_CODE,DAMH_ENCL_SECU_CHQS,DAMH_ENCL_LTR_HEADS" +
                        ",DAMH_ENCL_PD_OR_ARTCL,DAMH_ENCL_VAT_REG_COPY,DAMH_ENCL_PEST_LICE_COPY,DAMH_ENCL_IT_PAN_CARD_COPY,DAMH_ENCL_INDBOND_COPY,DAMH_APPOINTMENT_APPPROVED,DAMH_CREDIT_LIMIT_AMOUNT,DAMH_CREDIT_LIMIT_APPROVED_BY,DAMH_CREATED_BY,DAMH_CREATED_DATE,DAMH_DEALER_CODE,DAMH_ENCL_FERT_LICE_COPY,DAMH_ANY_OTHER_AGREEMENTS,DAMH_APPOINTMENT_RECOMMENDED_BY,DAMH_REMARKS) " +
                        "VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + Convert.ToInt32(MainHead[2]) + ",'" + MainHead[3] + "','" + MainHead[4] + "','" + MainHead[5] + "','" + MainHead[6] + "','" + MainHead[7] + "','" + MainHead[8]
                        + "','" + MainHead[9] + "','" + MainHead[10] + "'," + Convert.ToInt32(MainHead[11]) + ",'" + MainHead[12] + "','" + MainHead[13] + "','" + MainHead[14] + "','" + MainHead[15] + "','" + MainHead[16] + "','" + MainHead[17] + "','" + MainHead[18]
                        + "','" + MainHead[19] + "','" + MainHead[20] + "','" + MainHead[21] + "','" + MainHead[22] + "','" + MainHead[23] + "','" + MainHead[24] + "','" + MainHead[25] + "','" + MainHead[26] + "','" + MainHead[27] + "','" + MainHead[28]
                        + "'," + Convert.ToInt32(MainHead[29]) + ",'" + MainHead[30] + "','" + MainHead[31] + "','" + MainHead[32] + "','" + MainHead[33] + "','" + MainHead[34] + "','" + MainHead[35] + "','" + MainHead[36] + "','" + MainHead[37] + "','" + MainHead[38]
                        + "','" + MainHead[39] + "','" + MainHead[40] + "','" + MainHead[41] + "','" + MainHead[42] + "','" + MainHead[43] + "','" + MainHead[44] + "','" + MainHead[45] + "','" + MainHead[46] + "','" + MainHead[47] + "','" + MainHead[48]
                        + "','" + MainHead[49] + "','" + MainHead[50] + "'," + Convert.ToInt32(MainHead[51]) + "," + Convert.ToInt32(MainHead[52]) + "," + Convert.ToInt32(MainHead[53]) + ",'" + MainHead[54] + "','" + MainHead[55] + "','" + MainHead[56] + "','" + MainHead[57] + "'," + Convert.ToInt32(MainHead[58])
                        + "," + Convert.ToInt32(MainHead[59]) + "," + Convert.ToInt32(MainHead[60]) + "," + Convert.ToInt32(MainHead[61]) + ",'" + MainHead[62] + "'," + Convert.ToInt32(MainHead[63]) + ",'" + MainHead[64] + "','" + MainHead[65] + "','" + MainHead[66] + "','" + MainHead[67] + "','" + MainHead[68]
                        + "','" + MainHead[69] + "','" + MainHead[70] + "'," + Convert.ToInt32(MainHead[71]) + "," + Convert.ToInt32(MainHead[72]) + "," + Convert.ToInt32(MainHead[73]) + ",'" + MainHead[74] + "','" + MainHead[75] + "'," + Convert.ToInt32(MainHead[76]) + ",'" + MainHead[77] + "','" + MainHead[78] + "'," + Convert.ToInt32( MainHead[79]) + ",'" + MainHead[80] + "');";


                }
                if (updateFlag == true)
                {
                    sqlText = "UPDATE DL_APPL_MASTER_HEAD SET DAMH_COMPANY_CODE='" + MainHead[0] + "',DAMH_BRANCH_CODE='" + MainHead[1] + "',DAMH_APPL_DATE='" + MainHead[3] + "',DAMH_FIRM_NAME='" + MainHead[4] + "',DAMH_FIRM_ADDR_HNO='" + MainHead[5] + "',DAMH_FIRM_ADDR_LANDMARK='" + MainHead[6] + "'" +
                        ",DAMH_FIRM_ADDR_VILL_OR_TOWN='" + MainHead[7] + "',DAMH_FIRM_ADDR_MANDAL='" + MainHead[8] + "',DAMH_FIRM_ADDR_DISTRICT='" + MainHead[9] + "',DAMH_FIRM_ADDR_STATE='" + MainHead[10] + "',DAMH_FIRM_ADDR_PIN=" + Convert.ToInt32(MainHead[11]) + ",DAMH_FIRM_ADDR_PHONE='" + MainHead[12] + "',DAMH_FIRM_ADDR_OFF_PHONE='" + MainHead[13] + "'" +
                        ",DAMH_FIRM_ADDR_RES_PHONE='" + MainHead[14] + "',DAMH_FIRM_ADDR_FAX='" + MainHead[15] + "',DAMH_FIRM_ADDR_MOBILE='" + MainHead[16] + "',DAMH_FIRM_ADDR_EMAIL='" + MainHead[17] + "',DAMH_DEALER_NAME='" + MainHead[18] + "',DAMH_SEX='" + MainHead[19] + "',DAMH_DOB='" + MainHead[20] + "',DAMH_FORH='" + MainHead[21] + "',DAMH_FORH_NAME='" + MainHead[22] + "',DAMH_FIRM_HEAD_HNO='" + MainHead[23] + "'" +
                        ",DAMH_FIRM_HEAD_LANDMARK='" + MainHead[24] + "',DAMH_FIRM_HEAD_VILL_OR_TOWN='" + MainHead[25] + "',DAMH_FIRM_HEAD_MANDAL='" + MainHead[26] + "',DAMH_FIRM_HEAD_DISTRICT='" + MainHead[27] + "',DAMH_FIRM_HEAD_STATE='" + MainHead[28] + "',DAMH_FIRM_HEAD_PIN=" + Convert.ToInt32(MainHead[29]) + ",DAMH_FIRM_HEAD_PHONE='" + MainHead[30] + "'" +
                        ",DAMH_FIRM_HEAD_OFF_PHONE='" + MainHead[31] + "',DAMH_FIRM_HEAD_RES_PHONE='" + MainHead[32] + "',DAMH_FIRM_HEAD_FAX='" + MainHead[33] + "',DAMH_FIRM_HEAD_MOBILE='" + MainHead[34] + "',DAMH_FIRM_HEAD_EMAIL='" + MainHead[35] + "',DAMH_DOJ='" + MainHead[36] + "',DAMH_FIRM_TYPE='" + MainHead[37] + "',DAMH_VAT_NUMBER='" + MainHead[38] + "',DAMH_CST_NUMBER='" + MainHead[39] + "'" +
                        ",DAMH_IT_PAN_NUMBER='" + MainHead[40] + "',DAMH_PESTICIDE_LICENSE_NUMBER='" + MainHead[41] + "',DAMH_PESTICIDE_LICENSE_DATE='" + MainHead[42] + "',DAMH_PESTICIDE_LICENSE_VALID_UPTO='" + MainHead[43] + "',DAMH_PESTICIDE_LICENSE_ISSUED_BY='" + MainHead[44] + "',DAMH_FERTILIZR_LICENSE_NUMBER='" + MainHead[45] + "'" +
                        ",DAMH_FERTILIZR_LICENSE_DATE='" + MainHead[46] + "',DAMH_FERTILIZR_LICENSE_VALID_UPTO='" + MainHead[47] + "',DAMH_FERTILIZR_LICENSE_ISSUED_BY='" + MainHead[48] + "',DAMH_PRESENT_BUSINESS='" + MainHead[49] + "',DAMH_BUSINESS_TYPE='" + MainHead[50] + "',DAMH_PREV_YR_PEST_TURNOVER_WHSALE=" + Convert.ToDouble(MainHead[51]) + "" +
                        ",DAMH_PREV_YR_PEST_TURNOVER_RETAIL=" + Convert.ToDouble(MainHead[52]) + ",DAMH_NOOF_DEALERS=" + Convert.ToInt32(MainHead[53]) + ",DAMH_PRESENT_AREA_VILL_OR_TOWN='" + MainHead[54] + "',DAMH_PRESENT_AREA_MANDAL='" + MainHead[55] + "',DAMH_PRESENT_AREA_DISTRICT='" + MainHead[56] + "',DAMH_PRESENT_AREA_STATE='" + MainHead[57] + "',DAMH_GODOWN_SPACE_SFT=" + Convert.ToInt32(MainHead[58])+""+
                        ",DAMH_NOOF_EMPLOYEES=" + Convert.ToInt32(MainHead[59]) + ",DAMH_EXPECTED_TURNOVER_OUR_PROD=" + Convert.ToDouble(MainHead[60]) + ",DAMH_EXPECTED_CAPITAL_OUR_PROD=" + Convert.ToDouble(MainHead[61]) + ",DAMH_FINANCE_ARRANGE_TYPE='" + MainHead[62] + "',DAMH_REPRESENT_CODE=" + Convert.ToInt32(MainHead[63]) + ",DAMH_ENCL_SECU_CHQS='" + MainHead[64] + "',DAMH_ENCL_LTR_HEADS='" + MainHead[65] + "'" +
                        ",DAMH_ENCL_PD_OR_ARTCL='" + MainHead[66] + "',DAMH_ENCL_VAT_REG_COPY='" + MainHead[67] + "',DAMH_ENCL_PEST_LICE_COPY='" + MainHead[68] + "',DAMH_ENCL_IT_PAN_CARD_COPY='" + MainHead[69] + "',DAMH_ENCL_INDBOND_COPY='" + MainHead[70] + "',DAMH_APPOINTMENT_APPPROVED=" + Convert.ToInt32(MainHead[71]) + ",DAMH_CREDIT_LIMIT_AMOUNT=" + Convert.ToDouble(MainHead[72]) + ",DAMH_CREDIT_LIMIT_APPROVED_BY=" + Convert.ToInt32(MainHead[73]) + "" +
                        ",DAMH_MODIFIED_BY='" + MainHead[74] + "',DAMH_MODIFIED_DATE='" + MainHead[75] + "',DAMH_ENCL_FERT_LICE_COPY='" + MainHead[77] + "',DAMH_ANY_OTHER_AGREEMENTS='" + MainHead[78] + "',DAMH_APPOINTMENT_RECOMMENDED_BY=" + Convert.ToInt32(MainHead[79]) + ",DAMH_REMARKS='" + MainHead[80] + "' where DAMH_APPL_NUMBER=" + MainHead[2];
                }



                int iApplNo = Convert.ToInt32(MainHead[2]);


                if (dtPartners.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_PARTNERS_DETL WHERE DAPD_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtPartners.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_PARTNERS_DETL(DAPD_COMPANY_CODE,DAPD_BRANCH_CODE,DAPD_APPL_NUMBER,DAPD_SLNO,DAPD_PRTNR_NAME,DAPD_PRTNR_AGE,DAPD_PRTNR_ADDR_HNO,DAPD_PRTNR_ADDR_LANDMARK,DAPD_PRTNR_ADDR_VILL_OR_TOWN,DAPD_PRTNR_ADDR_MANDAL,DAPD_PRTNR_ADDR_DISTRICT,DAPD_PRTNR_ADDR_STATE,DAPD_PRTNR_ADDR_PIN,DAPD_PRTNR_ADDR_PHONE) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtPartners.Rows[i]["SLNO"])) + ",'" + dtPartners.Rows[i]["PartnerName"] + "'," + Convert.ToInt32(dtPartners.Rows[i]["Age"]) + ",'" + dtPartners.Rows[i]["Hno"] + "','" + dtPartners.Rows[i]["LandMark"] + "','" + dtPartners.Rows[i]["Vill"] + "','" + dtPartners.Rows[i]["Mandal"] + "','" + dtPartners.Rows[i]["District"] + "','" + dtPartners.Rows[i]["State"] + "'," + Convert.ToInt32(dtPartners.Rows[i]["Pin"]) + ",'" + dtPartners.Rows[i]["Phone"] + "');";
                    }
                }

                if (dtPrevTurnOver.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_PREV_TURNOVER WHERE DAPT_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtPrevTurnOver.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_PREV_TURNOVER(DAPT_COMPANY_CODE,DAPT_BRANCH_CODE,DAPT_APPL_NUMBER,DAPT_SLNO,DAPT_YEAR,DAPT_PRODUCT,DAPT_TURNOVER) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtPrevTurnOver.Rows[i]["SLNO"])) + "," + Convert.ToInt32(dtPrevTurnOver.Rows[i]["Yearpt"]) + ",'" + dtPrevTurnOver.Rows[i]["Product"] + "'," + Convert.ToInt32(dtPrevTurnOver.Rows[i]["TurnOver"]) + ");";
                    }
                }

                if (dtBussinessDetails.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_BUSISNESS_DETL WHERE DABD_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtBussinessDetails.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_BUSISNESS_DETL(DABD_COMPANY_CODE,DABD_BRANCH_CODE,DABD_APPL_NUMBER,DABD_SLNO,DABD_COMPANY_NAME,DABD_YEAR,DABD_PRODUCT,DABD_TURNOVER_LAST2YEARS,DABD_TURNOVER,DABD_TOTAL_TURNOVER) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtBussinessDetails.Rows[i]["SLNO"])) + ",'" + dtBussinessDetails.Rows[i]["CompanyName"] + "'," + Convert.ToInt32(dtBussinessDetails.Rows[i]["YearBD"]) + ",'" + dtBussinessDetails.Rows[i]["Product"] + "'," + Convert.ToInt32(dtBussinessDetails.Rows[i]["Last2YearTurnOver"]) + "," + Convert.ToInt32(dtBussinessDetails.Rows[i]["TurnOver"]) + "," + Convert.ToInt32(dtBussinessDetails.Rows[i]["TotalTurnOver"]) + ");";
                    }
                }
                if (dtOtherDealerShips.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_OTHER_DEALERSHIPS WHERE DAOD_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtOtherDealerShips.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_OTHER_DEALERSHIPS(DAOD_COMPANY_CODE,DAOD_BRANCH_CODE,DAOD_APPL_NUMBER,DAOD_SLNO,DAOD_COMPANY_NAME,DAOD_FROM_YEAR,DAOD_REMARKS) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtOtherDealerShips.Rows[i]["SLNO"])) + ",'" + dtOtherDealerShips.Rows[i]["CompanyName"] + "'," + Convert.ToInt32(dtOtherDealerShips.Rows[i]["FromYear"]) + ",'" + dtOtherDealerShips.Rows[i]["Remarks"] + "');";
                    }
                }
                if (dtBussnessVehicles.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_BUSINESS_VEHICLES WHERE DABV_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtBussnessVehicles.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_BUSINESS_VEHICLES(DABV_COMPANY_CODE,DABV_BRANCH_CODE,DABV_APPL_NUMBER,DABV_SLNO,DABV_VEHICLE_TYPE,DABV_NOOF_VEHICLES) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtBussnessVehicles.Rows[i]["SLNO"])) + ",'" + dtBussnessVehicles.Rows[i]["VehType"] + "'," + Convert.ToInt32(dtBussnessVehicles.Rows[i]["NoOfVeh"]) + ");";
                    }
                }
                if (dtTerritoryDetl.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_TERRITORY_DETL WHERE DATD_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtTerritoryDetl.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_TERRITORY_DETL(DATD_COMPANY_CODE,DATD_BRANCH_CODE,DATD_APPL_NUMBER,DATD_SLNO,DATD_TYPE,DATD_NAME) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtTerritoryDetl.Rows[i]["SLNO"])) + ",'" + dtTerritoryDetl.Rows[i]["TerritoryType"] + "','" + dtTerritoryDetl.Rows[i]["TerritoryName"] + "');";
                    }
                }
                if (dtFixedAssets.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_FIXEDASSETS_DETL WHERE DAFXD_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtFixedAssets.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_FIXEDASSETS_DETL(DAFXD_COMPANY_CODE,DAFXD_BRANCH_CODE,DAFXD_APPL_NUMBER,DAFXD_SLNO,DAFXD_DETAILS1,DAFXD_DETAILS2,DAFXD_DETAILS3) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtFixedAssets.Rows[i]["SLNO"])) + ",'" + dtFixedAssets.Rows[i]["Details1"] + "','" + dtFixedAssets.Rows[i]["Details2"] + "','" + dtFixedAssets.Rows[i]["Details3"] + "');";
                    }
                }
                if (dtSecurityCheqs.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_SECUCHQS_DETL WHERE DASD_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtSecurityCheqs.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_SECUCHQS_DETL(DASD_COMPANY_CODE,DASD_BRANCH_CODE,DASD_APPL_NUMBER,DASD_SLNO,DASD_CHEQUE_NUMBER,DASD_BANK_NAME,DASD_BRANCH_NAME) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtSecurityCheqs.Rows[i]["SLNO"])) + ",'" + dtSecurityCheqs.Rows[i]["CheqNo"] + "','" + dtSecurityCheqs.Rows[i]["BankName"] + "','" + dtSecurityCheqs.Rows[i]["BranchName"] + "');";
                    }
                }
                if (dtBankerDetails.Rows.Count > 0)
                {
                    sqlText += "DELETE FROM DL_APPL_BANK_DETL WHERE DABANK_APPL_NUMBER=" + iApplNo + "; ";
                    for (int i = 0; i < dtBankerDetails.Rows.Count; i++)
                    {
                        sqlText += "INSERT INTO DL_APPL_BANK_DETL(DABANK_COMPANY_CODE,DABANK_BRANCH_CODE,DABANK_APPL_NUMBER,DABANK_SLNO,DABANK_BANK_NAME,DABANK_ADDR_HNO,DABANK_ADDR_LANDMARK,DABANK_ADDR_VILL_OR_TOWN,DABANK_ADDR_MANDAL,DABANK_ADDR_DISTRICT,DABANK_ADDR_STATE,DABANK_ADDR_PIN,DABANK_ADDR_PHONE) " +
                            " VALUES('" + MainHead[0] + "','" + MainHead[1] + "'," + iApplNo + "," + (Convert.ToInt32(dtBankerDetails.Rows[i]["SLNO"])) + ",'" + dtBankerDetails.Rows[i]["BankerName"] + "','" + dtBankerDetails.Rows[i]["Hno"] + "','" + dtBankerDetails.Rows[i]["LandMark"] + "','" + dtBankerDetails.Rows[i]["Vill"] + "','" + dtBankerDetails.Rows[i]["Mandal"] + "','" + dtBankerDetails.Rows[i]["District"] + "','" + dtBankerDetails.Rows[i]["State"] + "'," + Convert.ToInt32(dtBankerDetails.Rows[i]["Pin"]) + "," + dtBankerDetails.Rows[i]["Phone"] + ");";
                    }
                }
                if (sqlText.Length > 10)
                {
                    //SqlCommand sqlCmd = new SqlCommand(sqlText, CN);
                    //sqlCmd.CommandType = CommandType.Text;
                    //sqlCmd.ExecuteNonQuery();
                    iRes = objSQLdb.ExecuteSaveData(sqlText);
                    if (iRes > 0)
                    {
                        
                       
                        return "saved";
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ex.ToString();
            }
            finally
            {
                //CN.Close();
                objSQLdb = null;
                //objSecurity = null;
            }

            return "";
        }
        #region "PHOTO UPDATED"
        public void UpdatePhoto(int ApplicationID, byte[] buffer)
        {
            try
            {
                objSecurity = new Security();
                SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                SqlCommand cmdPhoto = new SqlCommand("DL_imageStore", CN);
                cmdPhoto.CommandType = CommandType.StoredProcedure;
                cmdPhoto.Parameters.AddWithValue("@imode", 111);
                cmdPhoto.Parameters.AddWithValue("@ApplicatioNo", ApplicationID);
                cmdPhoto.Parameters.AddWithValue("@ivHAMH_MY_PHOTO", buffer);
                CN.Open();
                cmdPhoto.ExecuteNonQuery();
                CN.Close();
                objSecurity = null;
            }
            //objSQLdb = new SQLDB();
            //SqlParameter[] param = new SqlParameter[3];
            //DataSet ds = new DataSet();
            //try
            //{
            //    param[0] = objSQLdb.CreateParameter("@imode", DbType.Int32, 111, ParameterDirection.Input);


            //    param[1] = objSQLdb.CreateParameter("@ApplicatioNo", DbType.Int32, ApplicationID, ParameterDirection.Input);
            //    param[2] = objSQLdb.CreateParameter("@ivHAMH_MY_PHOTO", DbType.Byte[], buffer, ParameterDirection.Input);
            //    ds = objSQLdb.ExecuteDataSet("DL_imageStore", CommandType.StoredProcedure, param);
            //}
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                
                objSQLdb = null;
            }

        }
        #endregion

        public DataSet GetDealerInformation(int iMode, string sCompanyCode, string sBranchCode, int ApplNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@imode", DbType.Int32, iMode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@CompanyCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@BranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@ApplicatioNo", DbType.Int32, ApplNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DL_GetAllInformation", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet DL_DealerSearch_Get(string sCompanyCode, string sDealerSearch, string sFirmSearch)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xDealerName", DbType.String, sDealerSearch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFirmName", DbType.String, sFirmSearch, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DL_GetDealersListSearch", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }


        public DataSet GetDlVillages(string sStateCode, string sDistrict, string sMandal)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCDState", DbType.String, sStateCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDistrict", DbType.String, sDistrict, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sMandal", DbType.String, sMandal, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetDlVillages_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public DataSet GetDlStates_Proc()
        {
            DataSet ds = new DataSet();
            objSQLdb = new SQLDB();
            try
            {

                ds = objSQLdb.ExecuteDataSet("GetDlStates_Proc", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;

            }
            return ds;
        }

        public Hashtable GetDlSalesCreditNoteDetails(string sRefNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            string sTranNo = "";
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nRefNo", DbType.String, sRefNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SalesCreditNoteHead_Get", CommandType.StoredProcedure, param);
                ht.Add("Head", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    sTranNo = ds.Tables[0].Rows[0]["TrnNumber"].ToString();
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SalesCreditNoteDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail", ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }
        public DataTable SOEcodeSearchByDesig_Get(string cCode,string bCode,string docMonth,string desig,string soName )
        {
             objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataTable dt = new DataTable();
            
            string sTranNo = "";
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDesigId", DbType.String, desig, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xEmpName", DbType.String, soName, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("SOEcodeSearchByDesig_Get", CommandType.StoredProcedure, param).Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }
        public DataTable DesigInShanm()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataTable dt = new DataTable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("DesigInShanm", CommandType.StoredProcedure, param).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }
    }
}
