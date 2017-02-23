using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Emgu.CV;                  
using Emgu.CV.CvEnum;           
using Emgu.CV.Structure;        
using Emgu.CV.UI;               


namespace AnaliseCNH
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Mat imgOriginal;

            try
            {
                string path = "Imagens/1200.jpg";
                imgOriginal = new Mat(path, LoadImageType.AnyColor);
            }
            catch (Exception ex)
            {
                throw;
                return;
            }

            if (imgOriginal == null)
            {
                throw new Exception("imagem nula");
                return;
            }

           
           ibOriginal.Image = imgOriginal;
           
        }
    }
}
