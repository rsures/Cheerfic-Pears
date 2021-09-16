using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cheer_fic_Pears
{
    public partial class frmTimeOptions : Form
    {
        public frmTimeOptions()
        {
            InitializeComponent();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            frmMain NewWindow;
            if (rad3Min.Checked == true)
                NewWindow = new frmMain(3);
            else if (rad5Min.Checked == true)
                NewWindow = new frmMain(5);
            else
                NewWindow = new frmMain(3);

            //make this from invisible
            this.Visible = false;
            NewWindow.ShowDialog();
            NewWindow.Dispose();
            this.Visible = true;
        }
    }
}
