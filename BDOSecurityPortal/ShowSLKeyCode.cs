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
    public partial class ShowSLKeyCode : Form
    {
        public ShowSLKeyCode(List<string> slCodeList)
        {
            InitializeComponent();

            labelKeyCode.Text = ResourceCulture.GetString("SLKeyCode");
            foreach (var slCode in slCodeList)
            {
                int iCode = slCodeList.IndexOf(slCode);
                if (iCode <= 4)
                {
                    TextBox txtSLCode = this.Controls.Find("txtSLCode" + iCode.ToString(), true)[0] as TextBox;
                    txtSLCode.Text = slCodeList[iCode];
                }
            }
        }

        private void pbBtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void pbBtnClose_MouseLeave(object sender, EventArgs e)
        {
            pbBtnClose.Image = Properties.Resources.btn_close2;
        }

        private void pbBtnClose_MouseEnter(object sender, EventArgs e)
        {
            pbBtnClose.Image = Properties.Resources.btn_close_hover;
        }

        #region 窗体可拖动

        private Point mPoint = new Point();

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        #endregion

    }
}
