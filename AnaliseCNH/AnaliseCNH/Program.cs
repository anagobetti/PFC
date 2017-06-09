using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AnaliseCNH
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
            /*Image<Bgr, Byte> imgOriginal;
            Image<Gray, byte> imgGray;

            string path = "Imagens/1200.jpg";
            imgOriginal = new Image<Bgr, byte>(path);
            imgGray = imgOriginal.Convert<Gray, Byte>();

            Form frm = new Form();

            HistogramBox histo = new HistogramBox();

            histo.ClearHistogram();
            histo.GenerateHistograms(imgGray, 256);
            histo.Dock = DockStyle.Fill;
            histo.Refresh();

            frm.Controls.Add(histo);

            frm.ShowDialog();

            // CNHController.EncontrarBrasao();*/

        }
    }
}
