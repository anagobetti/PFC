using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnaliseCNH
{
    public partial class frmCalibracao : Form
    {
        FindContour processor = new FindContour();
        Image<Bgr, Byte> imgOriginal;
        Image<Gray, Byte> gray;
        Image<Bgr, Byte> color;

        Image<Gray, Byte> gray1;
        Image<Gray, Byte> gray2;


        Image<Gray, Byte> canny1;
        Image<Gray, Byte> canny2;

        public frmCalibracao()
        {
            InitializeComponent();
            string path = "Imagens/300.jpg";
            imgOriginal = new Image<Bgr, byte>(path).Resize(1000, 1400, Emgu.CV.CvEnum.Inter.Linear, true);
        }

        private void th1_Scroll(object sender, EventArgs e)
        {
            ImagemCinza();
        }
        private void th2_Scroll(object sender, EventArgs e)
        {
            ImagemCinza();
        }
        private void th3_Scroll(object sender, EventArgs e)
        {
            ImagemCinza();
        }
        private void th4_Scroll(object sender, EventArgs e)
        {
            ImagemCinza();
        }

        private void ImagemCinza()
        {
            processor.ObterImagemCinza(imgOriginal, out gray1);
            ObterContornos();
            ObterLinha();
            ObterHistograma();
            imgCinza0.Image = imgOriginal;
            imgCanny1.Image = gray1;
        }

        private void ObterContornos()
        {
            processor.ObterContornos(gray1, out canny1);
            imgCanny0.Image = canny1;
        }

        private void ObterLinha()
        {
            processor.ObterLinhas(canny1, out color);
            imgCinza1.Image = color;

        }

        private void ObterHistograma()
        {
            Form frm = new Form();

            HistogramBox histo = new HistogramBox();

            histo.ClearHistogram();
            histo.GenerateHistograms(color.Convert<Gray, byte>(), 255);
            histo.Dock = DockStyle.Fill;
            histo.Refresh();

            frm.Controls.Add(histo);

            frm.ShowDialog();
        }

    }
}
