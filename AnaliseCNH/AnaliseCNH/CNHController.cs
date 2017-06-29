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
using DetectorDocumento;
using Emgu.CV.OCR;
using LeitorDocumento;
using System.Text;

namespace AnaliseCNH
{
    public class CNHController
    {
        private object drawColor;
        private FaceDetector faceDetector;
        private Leitor leitorDocumento;
        public List<Rectangle> faces;
        Mat imageretorno;

        public CNHController()
        {
            faceDetector = new FaceDetector();
            leitorDocumento = new Leitor();

        }


        public void EncontrarBrasao()
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



        public Mat ProcessImage(Image<Bgr, byte> imgOriginal)
        {
            Mat image = imgOriginal.Clone().Mat;
            imageretorno = image;
            faces = new List<Rectangle>();
            faceDetector.Detect(image, faces);

            if (faces.Count > 0)
            {
                // foreach (Rectangle face in faces)
                CvInvoke.Rectangle(image, faces[0], new Bgr(Color.Red).MCvScalar, 2);

                Image<Gray, byte> gray = imgOriginal.Convert<Gray, Byte>();
                Image<Gray, byte> grayImage = gray.Clone();

                gray = gray.ThresholdBinary(new Gray(121), new Gray(255));

                Rectangle rec = new Rectangle(167, 160, 462, 20);//629, 180
                Image<Gray, byte> roi = (gray.Clone().Mat).ToImage<Gray, Byte>();
                // Image<Gray, byte> roi = (new Mat(gray.Clone().Mat, rec)).ToImage<Gray, Byte>();

                //               LerDocumento(roi);

            }
            else
            {
                //identidade - trarar 
            }

            return imageretorno;
        }

        private void TratarImagem(Mat image)
        {

            List<String> licenses = new List<String>();
            List<IInputOutputArray> roisImagesList = new List<IInputOutputArray>();
            List<IInputOutputArray> filteredROIsImagesList = new List<IInputOutputArray>();
            List<RotatedRect> detectedROIsRegionList = new List<RotatedRect>();

            using (Mat gray = new Mat())
            using (Mat canny = new Mat())
            //using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);
                CvInvoke.Canny(gray, canny, 100, 50, 3, false);
                double cannyThresholdLinking = 120.0;
                double cannyThreshold = 180.0;



                UMat cannyEdges = new UMat();
                CvInvoke.Canny(gray, cannyEdges, cannyThreshold, cannyThresholdLinking);

                LineSegment2D[] lines = CvInvoke.HoughLinesP(
                cannyEdges,
                1, //Distance resolution in pixel-related units
                Math.PI / 45.0, //Angle resolution measured in radians.
                20, //threshold
                30, //min Line width
                10); //gap between lines

                imageretorno = image;

                List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle

                using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                {
                    CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                    int count = contours.Size;
                    for (int i = 0; i < count; i++)
                    {
                        using (VectorOfPoint contour = contours[i])
                        using (VectorOfPoint approxContour = new VectorOfPoint())
                        {
                            CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                            if (CvInvoke.ContourArea(approxContour, false) > 15) //only consider contours with area greater than 250
                            {
                                CvInvoke.DrawContours(imageretorno, contours, i, new MCvScalar(0, 0, 255));

                                if (approxContour.Size == 4) //The contour has 4 vertices.
                                {
                                    bool isRectangle = true;
                                    Point[] pts = approxContour.ToArray();
                                    LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                    for (int j = 0; j < edges.Length; j++)
                                    {
                                        double angle = Math.Abs(
                                           edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                        if (angle < 80 || angle > 100)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }


                        //imageretorno = FilterPlate(canny.ToUMat(AccessType.Read)).ToMat(AccessType.ReadWrite);
                        // int[,] hierachy = CvInvoke.FindContourTree(canny, contours, ChainApproxMethod.ChainApproxSimple);

                        //FindROIs(contours, hierachy, 0, gray, canny, roisImagesList, filteredROIsImagesList, detectedROIsRegionList);
                    }
                }
            }
        }

        private void FindROIs(VectorOfVectorOfPoint contours, int[,] hierachy, int idx, Mat gray, Mat canny, List<IInputOutputArray> roisImagesList,
            List<IInputOutputArray> filteredROIsImagesList, List<RotatedRect> detectedROIsRegionList)
        {

            for (; idx >= 0; idx = hierachy[idx, 0])
            {
                int numberOfChildren = GetNumberOfChildren(hierachy, idx);
                if (numberOfChildren == 0) continue;

                using (VectorOfPoint contour = contours[idx])
                {
                    if (CvInvoke.ContourArea(contour) > 400)
                    {
                        CvInvoke.DrawContours(imageretorno, contours, idx, new MCvScalar(0, 0, 255));
                        if (numberOfChildren < 3)
                        {
                            FindROIs(contours, hierachy, hierachy[idx, 2], gray, canny, roisImagesList,
                               filteredROIsImagesList, detectedROIsRegionList);
                            continue;
                        }

                        RotatedRect box = CvInvoke.MinAreaRect(contour);
                        if (box.Angle < -45.0)
                        {
                            float tmp = box.Size.Width;
                            box.Size.Width = box.Size.Height;
                            box.Size.Height = tmp;
                            box.Angle += 90.0f;
                        }
                        else if (box.Angle > 45.0)
                        {
                            float tmp = box.Size.Width;
                            box.Size.Width = box.Size.Height;
                            box.Size.Height = tmp;
                            box.Angle -= 90.0f;
                        }

                        using (UMat tmp1 = new UMat())
                        using (UMat tmp2 = new UMat())
                        {
                            PointF[] srcCorners = box.GetVertices();

                            PointF[] destCorners = new PointF[]
                            {
                                new PointF(0, box.Size.Height - 1),
                                new PointF(0, 0),
                                new PointF(box.Size.Width - 1, 0),
                                new PointF(box.Size.Width - 1, box.Size.Height - 1)
                            };

                            using (Mat rot = CameraCalibration.GetAffineTransform(srcCorners, destCorners))
                            {
                                CvInvoke.WarpAffine(gray, tmp1, rot, Size.Round(box.Size));
                            }

                            //resize the license plate such that the front is ~ 10-12. This size of front results in better accuracy from tesseract
                            Size approxSize = new Size(240, 180);
                            double scale = Math.Min(approxSize.Width / box.Size.Width, approxSize.Height / box.Size.Height);
                            Size newSize = new Size((int)Math.Round(box.Size.Width), (int)Math.Round(box.Size.Height));
                            CvInvoke.Resize(tmp1, tmp2, newSize, 0, 0, Inter.Cubic);

                            //removes some pixels from the edge
                            int edgePixelSize = 2;
                            Rectangle newRoi = new Rectangle(new Point(edgePixelSize, edgePixelSize),
                               tmp2.Size - new Size(2 * edgePixelSize, 2 * edgePixelSize));
                            UMat plate = new UMat(tmp2, newRoi);

                            UMat filteredPlate = FilterPlate(plate);

                            Tesseract.Character[] words;
                            StringBuilder strBuilder = new StringBuilder();
                            using (UMat tmp = filteredPlate.Clone())
                            {

                                // _ocr.Recognize(tmp);
                                string word = LerDocumento(tmp.ToImage<Gray, byte>(), 0);

                                strBuilder.Append(word);
                            }

                            //licenses.Add(strBuilder.ToString());
                            roisImagesList.Add(plate);
                            filteredROIsImagesList.Add(filteredPlate);
                            detectedROIsRegionList.Add(box);
                        }
                    }
                }
            }
        }

        public string LerDocumento(Image<Gray, byte> image, int tipo)
        {
            Tesseract.Character[] characters = leitorDocumento.Reconhecer(image, tipo);
            String text = leitorDocumento.ObterTexto(tipo);
            return text;
            //Tesseract.Character[] characters = leitorDocumento.GetCharacters();

        }




        private static int GetNumberOfChildren(int[,] hierachy, int idx)
        {
            //first child
            idx = hierachy[idx, 2];
            if (idx < 0)
                return 0;

            int count = 1;
            while (hierachy[idx, 0] > 0)
            {
                count++;
                idx = hierachy[idx, 0];
            }
            return count;
        }

        private static UMat FilterPlate(UMat plate)
        {
            UMat thresh = new UMat();
            CvInvoke.Threshold(plate, thresh, 120, 255, ThresholdType.BinaryInv);

            //Image<Gray, Byte> thresh = plate.ThresholdBinaryInv(new Gray(120), new Gray(255));

            Size plateSize = plate.Size;
            using (Mat plateMask = new Mat(plateSize.Height, plateSize.Width, DepthType.Cv8U, 1))
            using (Mat plateCanny = new Mat())
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                plateMask.SetTo(new MCvScalar(255.0));
                CvInvoke.Canny(plate, plateCanny, 100, 50);
                CvInvoke.FindContours(plateCanny, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                int count = contours.Size;
                for (int i = 1; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    {
                        Rectangle rect = CvInvoke.BoundingRectangle(contour);
                        if (rect.Height > (plateSize.Height >> 1))
                        {
                            rect.X -= 1; rect.Y -= 1; rect.Width += 2; rect.Height += 2;
                            Rectangle roi = new Rectangle(Point.Empty, plate.Size);
                            rect.Intersect(roi);
                            CvInvoke.Rectangle(plateMask, rect, new MCvScalar(), -1);
                            //plateMask.Draw(rect, new Gray(0.0), -1);
                        }
                    }

                }

                thresh.SetTo(new MCvScalar(), plateMask);
            }

            CvInvoke.Erode(thresh, thresh, null, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.Dilate(thresh, thresh, null, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            return thresh;
        }
    }
}
