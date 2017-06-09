using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;

namespace AnaliseCNH
{
    public class FindContour
    {
        public List<LineSegment2D> listOfLines;

        #region Metodos Internos
        internal void ObterImagemCinza(Image<Bgr, byte> imgOriginal, out Image<Gray, byte> imgCinza1)
        {
            Image<Gray, byte> grayImage = imgOriginal.Convert<Gray, Byte>().Clone().PyrDown().PyrUp();
            UMat uimage = new UMat();
            CvInvoke.CvtColor(imgOriginal, uimage, ColorConversion.Bgr2Gray);

            UMat pyrDown = new UMat();
            CvInvoke.PyrDown(uimage, pyrDown);
            CvInvoke.PyrUp(pyrDown, uimage);

            imgCinza1 = uimage.ToImage<Gray, byte>();//.Clone().ThresholdToZero(new Gray(17)).PyrDown().PyrUp(); ;

        }

        internal void ObterContornos(Image<Gray, byte> imgCinza, out Image<Gray, byte> imgCanny)
        {
            Image<Gray, Byte> cannyGray = imgCinza.Clone().Canny(145, 255);
            imgCanny = cannyGray;
        }

        internal void ObterLinhas(Image<Gray, byte> imgCinza, out Image<Bgr, byte> imgLinha)
        {
            imgLinha = imgCinza.Convert<Bgr, byte>();

            double rhoResolution = (double)1 / 20;//IrhoResolution > 0 ? 1/(double)IrhoResolution : 1;
            double thetaResolution = (double)Math.PI / 100;// ( DthetaResolution > 0 ? DthetaResolution: 1) ;

            double minLineWidth = (double)5 / 10;
            double gapBetweenLines = (double)20 / 10;
            int threshold = 0;
            LineSegment2D[] lines =
                 imgCinza.Copy()
                 .HoughLinesBinary(rhoResolution, thetaResolution, 0, minLineWidth, gapBetweenLines)[0];
            //listOfLines = lines;
            listOfLines = new List<LineSegment2D>();
            int leng = 0;
            foreach (LineSegment2D line in lines)
            {
                if (Math.Abs(line.Length) >= leng)
                {
                    CvInvoke.Line(imgLinha, line.P1, line.P2, new Bgr(Color.Green).MCvScalar, 3, LineType.FourConnected);
                    listOfLines.Add(line);
                }
            }

        }


        #endregion

        #region Metodos Publicos
        public void ObterListaLinhas(Image<Bgr, byte> imgOriginal)
        {
            Image<Gray, byte> gray;
            ObterImagemCinza(imgOriginal, out gray) ;
            ObterContornos(gray, out gray);
            ObterLinhas(gray, out imgOriginal);
           // lines = listOfLines;
        }
        #endregion

        #region testes
        internal void IdentifyContours(Image<Bgr, byte> imgOriginal, int thresholdValue, int thresholdValue2, out Image<Gray, byte> gray, out Image<Bgr, byte> color)
        {
            color = imgOriginal;
            gray = imgOriginal.Convert<Gray, Byte>();
            Image<Gray, byte> grayImage = gray.Clone();

            Image<Gray, byte> tempcolor = gray.ThresholdToZero(new Gray(thresholdValue)).PyrDown().PyrUp();
            gray = grayImage.ThresholdBinary(new Gray(thresholdValue), new Gray(thresholdValue2)).PyrDown().PyrUp();
            color = tempcolor.Convert<Bgr, byte>();

            double thresh = thresholdValue;
            double threshLinking = thresholdValue2;

            Image<Gray, Byte> cannyGray = tempcolor.Canny(thresh, threshLinking);
            Image<Gray, Byte> cannyGray1 = gray.Canny(thresh, threshLinking);

        }
        #endregion
    }
}
