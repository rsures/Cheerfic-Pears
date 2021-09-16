using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Rahave Suresan
//Jan.29.2021
//
namespace Cheer_fic_Pears
{
    class Square : PictureBox
    {
        //fields
        private int mPoint; 

        //register clicks on the main form
        protected override void WndProc (ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)HTTRANSPARENT;
            else
                base.WndProc(ref m);
        }

        //properies
        public int Point
        {
            get { return mPoint; }
            set { mPoint = value; }
        }
    }
}
