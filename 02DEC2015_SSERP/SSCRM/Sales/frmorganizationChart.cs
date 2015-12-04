using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;


namespace SSCRM
{
    public partial class frmorganizationChart : Form
    {
        private string DocMonth;
        public frmorganizationChart()
        {
            InitializeComponent();
        }

        public frmorganizationChart(string sDocMonth)
        {
            InitializeComponent();
            DocMonth = sDocMonth;
        }

        private void frmorganizationChart_Load(object sender, EventArgs e)
        {
            //webBrowser1.Url =new Uri("http://www.shivashakthigroup.net/sty/default.aspx?Company=" + CommonData.CompanyCode + "&&Branch=" + CommonData.BranchCode + "&&DocMonth=" + DocMonth + "");
            //webBrowser1.Refresh();
            webBrowser1.Navigate("http://www.shivashakthigroup.net/sty/default.aspx?Company=" + CommonData.CompanyCode + "&&Branch=" + CommonData.BranchCode + "&&DocMonth=" + DocMonth,true);
        }
    }
}
