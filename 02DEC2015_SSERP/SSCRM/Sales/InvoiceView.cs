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
    public partial class InvoiceView : Form
    {
        public InvoiceView()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void InvoiceView_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 30, Screen.PrimaryScreen.WorkingArea.Y + 30);
            this.StartPosition = FormStartPosition.CenterScreen;
            cbVillage.SelectedIndex = 0;
        }
    }
}
