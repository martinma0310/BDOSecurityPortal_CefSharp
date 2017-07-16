using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDOSecurityPortal
{
    public partial class ShowQRCode : Form
    {
        public ShowQRCode()
        {
            InitializeComponent();
        }

        private void pbBtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
