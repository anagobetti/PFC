using Emgu.CV;
using Emgu.CV.Structure;
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
    public partial class Form1 : Form   
    {
        public Form1()
        {
            InitializeComponent();
            string path = "Imagens/600.jpg";
            imgOriginal.Image = new Image<Bgr, byte>(path).Resize(1000, 1400, Emgu.CV.CvEnum.Inter.Linear, true);
            lblAlertas.Text = "Não foi possível validar o documento. Favor verificar se o documento é um CNH e tentar novamente.";
          

        }

         }
}
