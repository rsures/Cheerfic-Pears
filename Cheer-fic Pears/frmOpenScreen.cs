using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Rahave Suresan
//Jan.29.2020
//Game's Main Menu Screen

namespace Cheer_fic_Pears
{
    public partial class frmOpenScreen : Form
    {
        public frmOpenScreen()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            frmMain NewWindow;
            NewWindow = new frmMain(3);

            //invisible
            this.Visible = false;
            NewWindow.ShowDialog();
            NewWindow.Dispose();
            this.Visible = true;
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How to Play: \nThe goal is to find a match of 3 or more fruit. When possible match is found, click on the fruit and the adjacent fruit to swap them. If no match is found, they will swap back. \n" +
                              "\nPoint System: \nPoints correspond with how many fruit are in a match. A match of three is 30 points. \n" +
                              "\nControls: \n Press R to restart the game.\n Press the escape key to return to main menu");
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            frmTimeOptions Options = new frmTimeOptions();
            this.Visible = false;
            Options.ShowDialog();
            Options.Dispose();
            this.Visible = true;
        }
    }
}
