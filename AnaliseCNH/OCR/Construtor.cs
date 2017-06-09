using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeitorDocumento
{
    public class Construtor
    {
        double hessianThresh = 300;

        public Construtor()
        {
            CvInvoke.CheckLibraryLoaded();
        }

        Mat descriptor;
        Mat featuresUnclustered;
        VectorOfKeyPoint observedKeyPoints;
        Mat result;

        public void ConstrutorXML(Mat modelImage)
        {
           
            observedKeyPoints = new VectorOfKeyPoint();
            descriptor = new Mat();
            featuresUnclustered = new Mat();
            result = new Mat();
            Mat observedImage = CvInvoke.Imread("Imagens/1200.jpg", LoadImageType.Grayscale);

            try
            {
                using (UMat uObservedImage = observedImage.ToUMat(AccessType.Read))
                {
                    SURF surfCPU = new SURF(hessianThresh);
                    UMat modelDescriptors = new UMat();

                    UMat observedDescriptors = new UMat();
                    surfCPU.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);

                    int dictionarySize = 200;

                    MCvTermCriteria tc = new MCvTermCriteria(100, 0.001);
                    int retries = 1;

                    BOWKMeansTrainer bowTrainer = new BOWKMeansTrainer(dictionarySize, tc, retries, KMeansInitType.PPCenters);
                    try
                    {
                        bowTrainer.Add(observedDescriptors.ToMat(AccessType.Read));
                        bowTrainer.Cluster(result);

                        FileStorage fs = new FileStorage("C:/Ana/PFC/AnaliseCNH/dictionary.yml", FileStorage.Mode.Write);
                        fs.Write(result, "vocabulario");
                        fs.Dispose();
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        public void LeitorXML()
        {
            Mat leitura = new Mat();
            FileStorage fs = new FileStorage("C:/Ana/PFC/AnaliseCNH/dictionary.yml", FileStorage.Mode.Read);
            FileNode fn = fs.GetNode("vocabulario");
            fn.ReadMat(leitura);
            fs.Dispose();

            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();
            BFMatcher matcher = new BFMatcher(DistanceType.L2);
           
            //matcher.Add(leitura);

            SURF surfCPU = new SURF(hessianThresh);

            BOWImgDescriptorExtractor bowDE = new BOWImgDescriptorExtractor(surfCPU, matcher);
            string fileName = "Imagens/1200.jpg";
            string imageTag ="teste1";

            FileStorage fs1 = new FileStorage("C:/Ana/PFC/AnaliseCNH/descriptor.yml", FileStorage.Mode.Write);
            Mat img = CvInvoke.Imread("Imagens/1200.jpg", LoadImageType.Grayscale);


            bowDE.SetVocabulary(leitura);
            VectorOfKeyPoint keyPoints = new VectorOfKeyPoint();
            UMat uObservedImage = img.ToUMat(AccessType.Read);
            UMat observedDescriptors = new UMat();
            surfCPU.DetectAndCompute(uObservedImage, null, keyPoints, observedDescriptors, false);

            Mat bowDescriptor = new Mat();

            bowDE.Compute(img, keyPoints, bowDescriptor);
            fs1.Write(bowDescriptor, imageTag);

            fs1.Dispose();



        }
    }
}
