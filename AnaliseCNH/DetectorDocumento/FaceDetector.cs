using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DetectorDocumento
{
    public class FaceDetector
    {
        public void Detect(Mat image, List<Rectangle> faces)
        {
            string faceFileName = "haarcascade_frontalface_default.xml";

            try
            {
                using (CascadeClassifier face = new CascadeClassifier(faceFileName))
                {
                    using (UMat ugray = new UMat())
                    {
                        CvInvoke.CvtColor(image, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                        CvInvoke.EqualizeHist(ugray, ugray);

                        Rectangle[] facesDetected = face.DetectMultiScale(
                           ugray,
                           1.1,
                           10,
                           new Size(20, 20));

                        faces.AddRange(facesDetected);
                    }
                }
            }
            catch(Exception e)
            {
                int i = 0;
            }
        }
    }
}
