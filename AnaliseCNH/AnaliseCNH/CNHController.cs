using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.Cuda;


namespace AnaliseCNH
{
    public static class CNHController// : form
    {
        public static void EncontrarBrasao()
        {
            long matchTime;
       //     for (int i = 9; i < 21; i++)
         //   {

                using (Mat modelImage = CvInvoke.Imread("Imagens/brasao.jpg", LoadImageType.Grayscale))
                using (Mat observedImage = CvInvoke.Imread("Imagens/1200.jpg", LoadImageType.Grayscale))
                {
                  //  SURFController.uniquenessThreshold= 0.8;
                //    SURFController.hessianThresh = 50*i; 
                    Mat result = SURFController.Draw(modelImage, observedImage, out matchTime);
                    ImageViewer.Show(result, String.Format("Matched using {0} in {1} milliseconds", CudaInvoke.HasCuda ? "GPU" : "CPU", matchTime));
                }
            }
       // }

    }
}
