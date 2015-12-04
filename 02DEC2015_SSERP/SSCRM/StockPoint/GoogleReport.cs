using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSCRM
{
    public partial class GoogleReport : Form
    {
        public string sStart = "", sEnd = "";
        public GoogleReport()
        {
            InitializeComponent();
        }        
        public GoogleReport(string St, string Ed)
        {
            InitializeComponent();
            sStart = St;
            sEnd = Ed;
        }
        private void GoogleReport_Load(object sender, EventArgs e)
        {
            string url = @"http://www.shivashakthigroup.net/sty/GoogMap.aspx?start=" + sStart + "&end=" + sEnd + "&sensor=false";
            webBrowser1.Navigate(new Uri(url));
        }
    }
}
