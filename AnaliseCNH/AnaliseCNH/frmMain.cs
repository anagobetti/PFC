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
using Emgu.CV.OCR;
using LeitorDocumento;
using Emgu.CV.Util;
using DetectorDocumento;
using System.Text.RegularExpressions;

namespace AnaliseCNH
{
    public partial class frmMain : Form
    {
        private Image<Bgr, Byte> imgOriginal;
        private Bgr drawColor = new Bgr(Color.Blue);
        private Leitor leitorDocumento;
        private FaceDetector faceDetector;
        private CNHController cnhController;
        private FindContour contourController;

        //private Construtor construtor;

        public frmMain()
        {
            InitializeComponent();
            leitorDocumento = new Leitor();
            faceDetector = new FaceDetector();
            cnhController = new CNHController();
            contourController = new FindContour();
            //construtor = new Construtor();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ObterEProcessarDocumento();
        }

        private void ObterEProcessarDocumento()
        {
            Mat processedImage = new Mat();

            try
            {
                DialogResult result = ofdOpenFile.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        imgOriginal = new Image<Bgr, byte>(ofdOpenFile.FileName)
                        .Resize(1000, 1400, Emgu.CV.CvEnum.Inter.Linear, true);
                        textBox1.Text = ofdOpenFile.FileName;
                    }
                    catch
                    {
                        MessageBox.Show(String.Format("Invalide File: {0}", ofdOpenFile.FileName));
                        return;
                    }
                }
                if (imgOriginal != null)
                {
                    processedImage = cnhController.ProcessImage(imgOriginal);
                    if (cnhController.faces.Count > 0)
                    {
                        contourController.ObterListaLinhas(imgOriginal);
                        if (contourController.listOfLines.Count > 0)
                        {
                            #region Nome

                            List<LineSegment2D> yMenor1 = contourController.listOfLines.Where(l => l.P1 != null && l.P1.Y < cnhController.faces[0].Y && l.P1.X < cnhController.faces[0].X).ToList<LineSegment2D>();
                            yMenor1 = yMenor1.Where(l => l.Direction.Y == 1).ToList<LineSegment2D>();
                            yMenor1 = yMenor1.Where(l => l.Length > 10).ToList<LineSegment2D>();
                            yMenor1 = yMenor1.Where(l => l.P1.X > 150).OrderByDescending(l1 => l1.P1.Y).ToList<LineSegment2D>();
                            LineSegment2D line = yMenor1.FirstOrDefault();
                            CvInvoke.Line(processedImage, line.P1, line.P2, new Bgr(Color.Green).MCvScalar, 2, LineType.FourConnected);
                            int xInferior = line.P1.X > line.P2.X ? line.P1.X : line.P2.X;
                            int YInferior = line.P1.Y > line.P2.Y ? line.P1.Y : line.P2.Y;

                            int tamanhoNomeY = 35;
                            int tamanhoNomeX = 800;

                            Image<Gray, byte> gray = imgOriginal.Convert<Gray, Byte>();
                            Image<Gray, byte> grayImage = gray.Clone();

                            gray = gray.ThresholdBinary(new Gray(121), new Gray(255));

                            Rectangle rec = new Rectangle(xInferior + 4, YInferior - tamanhoNomeY, 462, 30);//629, 180
                            Image<Gray, byte> roi = (new Mat(gray.Clone().Mat, rec)).ToImage<Gray, Byte>();
                            processedImage = roi.Mat;
                            label2.Text = cnhController.LerDocumento(roi, 0);

                            #endregion

                            #region CPF

                            List<LineSegment2D> yCPF = contourController.listOfLines.Where(l => l.P1 != null && l.P1.Y >= cnhController.faces[0].Y && l.P1.X > cnhController.faces[0].X && l.Length > 20).OrderBy(l1 => l1.P1.Y).ThenBy(l2 => l2.P1.X).ToList<LineSegment2D>();

                            List<LineSegment2D> yCPF2 = contourController.listOfLines.Where(l => l.P2 != null && l.P1.Y >= cnhController.faces[0].Y && l.P2.X > cnhController.faces[0].X && l.Length > 20).OrderBy(l1 => l1.P2.Y).ThenBy(l2 => l2.P2.X).ToList<LineSegment2D>();
                            yCPF.AddRange(yCPF2);
                            yCPF = yCPF.OrderBy(l => l.P1.Y).ThenBy(l => l.P2.Y).Take(5).ToList<LineSegment2D>();

                            yCPF = yCPF.OrderBy(l => l.P1.X).ToList<LineSegment2D>();
                            int num = 3;
                            for (int i = 0; i < num; i++)
                            {
                                CvInvoke.Line(imgOriginal, yCPF[i].P1, yCPF[i].P2, new Bgr(Color.Red).MCvScalar, 5, LineType.FourConnected);
                                // CvInvoke.Line(imgOriginal, yCPF2[i].P1, yCPF2[i].P2, new Bgr(Color.Blue).MCvScalar, 3, LineType.FourConnected);

                            }
                            int indice1 = 0;
                            int indice2 = indice1++;
                            for(indice2 = 1; indice2 < yCPF.Count; indice2++)
                            {
                                if(yCPF[indice2].P1.X > yCPF[indice1].P1.X)
                                {
                                    break;
                                }
                            }
                            int xEsquerdaCpf = yCPF[indice1].P1.X < yCPF[indice1].P2.X ? yCPF[indice1].P1.X : yCPF[indice1].P2.
                                X;
                            int xDireitaCpf = yCPF[indice2].P1.X < yCPF[indice2].P2.X ? yCPF[indice2].P1.X : yCPF[indice2].P2.X;
                            int ySuperiorCpf = yCPF[0].P1.Y < yCPF[0].P2.Y ? yCPF[0].P1.Y : yCPF[0].P2.Y;
                            int tamanho = xDireitaCpf - xEsquerdaCpf - 12;
                            if (tamanho <= 0)
                            {
                                tamanho = 30;
                            }
                            Rectangle rec2 = new Rectangle(xEsquerdaCpf + 4, (int)(ySuperiorCpf*1.02), tamanho, 30);//629, 180

                            Image<Gray, byte> roi2 = (new Mat(gray.Clone().Mat, rec2)).ToImage<Gray, Byte>();
                            processedImage = roi2.Mat;
                            string teste = cnhController.LerDocumento(roi2, 1);
                         ///   teste = mascaraCPF(teste);
                            label2.Text = teste;

                            #endregion

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                return;
            }
            ibOriginal.Image = imgOriginal;
            ibCinza.Image = processedImage;

        }

        private string mascaraCPF(string teste)
        {


            teste = teste.Trim();
            teste = Regex.Replace(teste, @"\s+", "");
            return String.Format("{0}.{1}.{2}-{3}", teste.Substring(0,3), teste.Substring(4,3), teste.Substring(8,3), teste.Substring(12,2));
        }

        private void ibOriginal_Click(object sender, EventArgs e)
        {

        }
    }
}
