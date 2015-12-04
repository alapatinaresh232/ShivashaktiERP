using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace SDMS.App_Code
{
    public class UtilityLibrary
    {
        public UtilityLibrary()
        {
        }
        #region "Utility functions"


        public static void PopulateControl(ListBox ListControl, DataView DataSource, string DisplayField, string ValueField)
        {
            ListControl.DataSource = DataSource;
            ListControl.ValueMember = DisplayField;
            ListControl.DisplayMember = ValueField;

        }

        public static void PopulateControl(ComboBox ComboControl, System.Data.DataView DataSource, int DisplayFieldOrdinal, int ValueFieldOrdinal)
        {
            ComboControl.DataSource = DataSource;
            ComboControl.DisplayMember = DataSource.Table.Columns[DisplayFieldOrdinal].ColumnName;
            ComboControl.ValueMember = DataSource.Table.Columns[ValueFieldOrdinal].ColumnName;
        }

        public static void PopulateControl(ComboBox ComboControl, System.Data.DataView DataSource, int DisplayFieldOrdinal, int ValueFieldOrdinal, string DefaultText, int DefaultValue)
        {
            System.Data.DataRow drNewRow = DataSource.Table.NewRow();
            drNewRow[DisplayFieldOrdinal] = DefaultText;
            drNewRow[ValueFieldOrdinal] = DefaultValue;
            DataSource.Table.Rows.InsertAt(drNewRow, 0);
            DataSource.Table.AcceptChanges();
            PopulateControl(ComboControl, DataSource.Table.DefaultView, DisplayFieldOrdinal, ValueFieldOrdinal);
        }

        public static void PopulateControl(ComboBox ComboControl, System.Data.DataView DataSource, int DisplayFieldOrdinal, int ValueFieldOrdinal, string DefaultText, string DefaultValue)
        {
            System.Data.DataRow drNewRow = DataSource.Table.NewRow();
            drNewRow[DisplayFieldOrdinal] = DefaultText;
            drNewRow[ValueFieldOrdinal] = DefaultValue;
            DataSource.Table.Rows.InsertAt(drNewRow, 0);
            DataSource.Table.AcceptChanges();
            System.Data.DataTable oTmpTable = DataSource.Table.Copy();
            DataSource.Table.Rows.Remove(drNewRow);
            DataSource.Table.AcceptChanges();

            PopulateControl(ComboControl, oTmpTable.DefaultView, DisplayFieldOrdinal, ValueFieldOrdinal);

        }


        #endregion


        public static bool ClearValidate(Control oFrm, ToolTip cTool)
        {
            return false;
        }

        public static bool CustomValidate(Control oFrm, ToolTip cTool)
        {
            System.Windows.Forms.DateTimePicker oCntDTPicker;
            string sType;
            Control oCntl = oFrm.GetNextControl(oFrm, true);
            bool bError = false;
            while (oCntl != null)
            {
                //foreach(Control oCntl in oFrm.Controls)

                sType = oCntl.GetType().ToString().ToLower();
                switch (sType)
                {

                    case "Forest.Forestlibrary.imphonetextbox":
                        {
                            if (oCntl.Name.ToLower().IndexOf("_optional", 0) >= 0)
                            {
                                if (!UtilityFunctions.IsValidPhoneNumber(oCntl.Text) && oCntl.Text != "")
                                {
                                    bError = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (!UtilityFunctions.IsValidPhoneNumber(oCntl.Text) || oCntl.Text.Trim() == "")
                                {
                                    bError = true;
                                    break;
                                }
                            }
                            break;
                        }
                    case "Forest.Forestlibrary.imdatetimepicker":
                        {
                            if (oCntl.Name.ToLower().IndexOf("_optional", 0) >= 0)
                            {
                                if (!UtilityFunctions.IsDate(oCntl.Text) && oCntl.Text != "")
                                {
                                    bError = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (!UtilityFunctions.IsDate(oCntl.Text) || oCntl.Text.Trim() == "")
                                {
                                    bError = true;
                                    break;
                                }
                            }
                            break;
                        }
                    case "system.windows.forms.textbox":
                        {
                            if (oCntl.Enabled && oCntl.Visible)
                                ///////////////////Text box checking/////////////////////////////////////
                                //optional entry field                                
                                //oCntl.Text = oCntl.Text.ToUpper();
                            if (oCntl.Name.ToLower().IndexOf("_optional", 0) >= 0)
                            {
                                //Nothing will be done for the optional fields
                                //if(the optional field is a Positive number field
                                if (oCntl.Name.ToLower().IndexOf("_pnum", 0) >= 0)
                                {
                                    if (oCntl.Text.Trim() != "" && !UtilityFunctions.IsPositiveNumber(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }

                                }//number field
                                else if (oCntl.Name.ToLower().IndexOf("_num", 0) >= 0)
                                {
                                    if (oCntl.Text.Trim() != "" && !UtilityFunctions.IsNumeric(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }
                                }//Percentage number field
                                else if (oCntl.Name.ToLower().IndexOf("_percent", 0) >= 0)
                                {
                                    if (oCntl.Text.Trim() != "" && (!UtilityFunctions.IsPositiveNumber(oCntl.Text) || UtilityFunctions.ValueOf(oCntl.Text) > 100))
                                    {
                                        bError = true;
                                        break;
                                    }
                                }
                                else if (oCntl.Name.ToLower().IndexOf("_wnum", 0) >= 0)
                                {
                                    if (oCntl.Text.Trim() != "" && !UtilityFunctions.IsWholeNumeric(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }
                                    //Percentage number field
                                }
                                else if (oCntl.Name.ToLower().IndexOf("_date", 0) >= 0)
                                {
                                    if (oCntl.Text.Trim() != "" && !UtilityFunctions.IsDate(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                if (oCntl.Name.IndexOf("SSN", 0) >= 0)
                                {
                                    if ((oCntl.Text.Length != 11))
                                        bError = true;
                                    //if((oCntl.Text.Length != 9 && oCntl.Text.Length != 8) || !UtilityFunctions.IsPositiveNumber(oCntl.Text))
                                    //Email field
                                }
                                else if (oCntl.Name.ToLower().IndexOf("email", 0) >= 0)
                                {
                                    oCntl.Text = oCntl.Text.ToLower();
                                    if (!UtilityFunctions.IsValidEmail(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }
                                    //Positive number field
                                }
                                else if (oCntl.Name.ToLower().IndexOf("_pnum", 0) >= 0)
                                {
                                    if (!UtilityFunctions.IsPositiveNumber(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }
                                    //number field
                                }
                                else if (oCntl.Name.ToLower().IndexOf("_num", 0) >= 0)
                                {
                                    if (!UtilityFunctions.IsNumeric(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }
                                    //Percentage number field
                                }
                                else if (oCntl.Name.ToLower().IndexOf("_wnum", 0) >= 0)
                                {
                                    if (!UtilityFunctions.IsWholeNumeric(oCntl.Text))
                                    {
                                        bError = true;
                                        break;
                                    }
                                    //Percentage number field
                                }
                                else if (oCntl.Name.ToLower().IndexOf("_percent", 0) >= 0)
                                {
                                    if (!UtilityFunctions.IsPositiveNumber(oCntl.Text) || UtilityFunctions.ValueOf(oCntl.Text) > 100)
                                    {
                                        bError = true;
                                        break;
                                    }
                                }
                                //Normal Field (if(nothing is specifed we assume it as required field)
                                else if (oCntl.Text.Trim() == "")
                                {
                                    bError = true;
                                    break;
                                }
                            }

                            /////////////////End of Text box checking////////////////////////////////


                            break;
                        }
                    case "system.windows.forms.combobox":
                        {
                            ComboBox oCombo;

                            oCombo = (ComboBox)oCntl;

                            if (oCntl.Enabled && oCntl.Visible)
                            {
                                if (oCntl.Name.ToLower().IndexOf("_optional", 0) >= 0)
                                {
                                    //Nothing will be done for the optional fields
                                    //if u want to enhance you code that part here
                                    //  Or (oCntl.Text = "--Please select--") 
                                }
                                else if (oCntl.Text == "" || Convert.ToInt32(oCombo.SelectedIndex) <= 0)
                                {
                                    bError = true;
                                    break;
                                }


                            }
                            break;
                        }
                    //case "system.windows.forms.checkbox":
                    //    {
                    //        CheckBox oCheck;

                    //        oCheck = (CheckBox)oCntl;

                    //        if (oCntl.Enabled && oCntl.Visible)
                    //        {
                                
                    //            if (oCheck.Checked)
                    //            {
                    //                //Nothing will be done for the optional fields
                    //                //if u want to enhance you code that part here
                    //                //  Or (oCntl.Text = "--Please select--") 
                    //            }
                    //            else if (!oCheck.Checked)
                    //            {
                    //                bError = true;
                    //                break;
                    //            }


                    //        }
                    //        break;
                    //    }
                    case "system.windows.forms.datetimepicker":
                        {
                            oCntDTPicker = (System.Windows.Forms.DateTimePicker)oCntl;
                            if (oCntDTPicker.Enabled && oCntDTPicker.Visible)
                            {

                                if (oCntDTPicker.Name.ToLower().IndexOf("_optional", 0) >= 0)
                                {
                                    //Nothing will be done for the optional fields
                                    //if u want to enhance you code that part here

                                    //Normal Date picker control(if nothing is specifed we assume it as required field)
                                    //UPGRADE_WARNING: Couldn't resolve default property of object oCntl.Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                }
                                else if (oCntDTPicker.ShowCheckBox && !oCntDTPicker.Checked)
                                {
                                    bError = true;
                                    break;
                                }

                            }
                            break;
                        }
                }
                if (bError)
                    break;
                else
                    oCntl = oFrm.GetNextControl(oCntl, true);
            }
            cTool.ShowAlways = false;
            if (bError && oCntl != null)
            {
                string sMsg = "Please enter a value.";
                if (oCntl.Tag != null && oCntl.Tag.ToString().Length > 0)
                    sMsg = oCntl.Tag.ToString();
                if (cTool != null)
                {
                    cTool.ToolTipTitle = "SSCRM Application";
                    cTool.UseFading = true;
                    cTool.UseAnimation = true;
                    cTool.IsBalloon = true;

                    cTool.ShowAlways = true;
                    cTool.AutoPopDelay = 5000;
                    cTool.InitialDelay = 1000;
                    cTool.ReshowDelay = 500;
                    cTool.SetToolTip(oCntl, "Required information missing");
                }
                if (cTool != null)
                    cTool.Show(sMsg, oCntl);
                else
                    MessageBox.Show(sMsg, "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);

                oCntl.Focus();
                return false;
            }
            else
                return true;
        }

        public static void HideControls(params Control[] Controls)
        {
            for (int iCtr = 0; iCtr < Controls.Length; iCtr++)
                Controls[iCtr].Visible = false;

        }

        public static void DisableControls(params Control[] Controls)
        {
            for (int iCtr = 0; iCtr < Controls.Length; iCtr++)
                Controls[iCtr].Enabled = false;
        }

        #region ErrorLog
        public static void ErrorLog(Form oForm, EventHandler eEventHandler, Boolean oBoolean)
        {
            ErrorProvider eError = new ErrorProvider();
            eError.GetError(oForm);
            System.Diagnostics.EventLog oEventLog = new System.Diagnostics.EventLog();
            string str = oEventLog.Entries.ToString();
        }
        #endregion

    }
}
