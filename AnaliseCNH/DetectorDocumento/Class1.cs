using Emgu.CV;
//using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;
using System;
using System.Drawing;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV.Util;

namespace DetectorDocumento
{
    public class Class1
    {
        public void Classify(Image<Bgr, Byte> testImg, string folder)
        {
            int class_num = 3;  //number of clusters/classes
            int input_num = 0;  //number of train images
            int j = 0;

            SURF detector = new SURF(500, 4, 2, false);
            BFMatcher matcher = new BFMatcher(DistanceType.L2);

            BOWKMeansTrainer bowTrainer = new BOWKMeansTrainer(class_num, new MCvTermCriteria(10, 0.01), 3, Emgu.CV.CvEnum.KMeansInitType.PPCenters);
            BOWImgDescriptorExtractor bowDE = new BOWImgDescriptorExtractor(detector, matcher);

            FileInfo[] files = new DirectoryInfo(folder).GetFiles();
            foreach (FileInfo file in files)
            {
                Image<Bgr, byte> model = new Image<Bgr, byte>(file.FullName);
                Image<Gray, Byte> modelGray = model.Convert<Gray, Byte>();
                //Detect SURF key points from images
                MKeyPoint[] modelKeyPoints = detector.Detect(modelGray, null);

               /* using (Matrix<float> modelDescriptors = detector.DetectRaw(modelGray, null, modelKeyPoints))
                {
                    //Add the extracted BoW modelDescriptors into BOW trainer
                    bowTrainer.Add(modelDescriptors);
                }
                input_num++;
                */
                //Compute detected SURF key points & extract modelDescriptors
                /*
                    /*using (Matrix<float> modelDescriptors = detector.ComputeDescriptorsRaw(modelGray, null, modelKeyPoints))
                    {
                        //Add the extracted BoW modelDescriptors into BOW trainer
                        bowTrainer.Add(modelDescriptors);
                    }
                    input_num++;
                }

                //Cluster the feature vectors
                Matrix<float> dictionary = bowTrainer.Cluster();
                //Store the vocabulary
                bowDE.SetVocabulary(dictionary);
                //To store all modelBOWDescriptor in a single trainingDescriptors
                Matrix<float> trainingDescriptors = new Matrix<float>(input_num, class_num);
                //To label each modelBOWDescriptor, in this case all train images are labelled with different integer 
                //hence all images are considered as a unique class, i.e class_num = input_num
                Matrix<float> labels = new Matrix<float>(input_num, 1);
                //Use labels of type <int> instead of <float> for NormalBayesClassifier
                //Matrix<int> labels = new Matrix<int>(input_num, 1);

                foreach (FileInfo file in files)
                {
                    using (Image<Bgr, Byte> model = new Image<Bgr, byte>(file.FullName))
                    using (Image<Gray, Byte> modelGray = model.Convert<Gray, Byte>())
                    using (VectorOfKeyPoint modelKeyPoints = detector.DetectKeyPointsRaw(modelGray, null))
                    using (Matrix<float> modelBOWDescriptor = bowDE.Compute(modelGray, modelKeyPoints))
                    {
                        //To merge all modelBOWDescriptor into single trainingDescriptors
                        for (int i = 0; i < trainingDescriptors.Cols; i++)
                        {
                            trainingDescriptors.Data[j, i] = modelBOWDescriptor.Data[0, i];
                        }
                        labels.Data[j, 0] = (j + 1);
                        j++;
                    }
                }

                //Declaration for Support Vector Machine & parameters
                SVM my_SVM = new SVM();
                SVMParams p = new SVMParams();
                p.KernelType = Emgu.CV.ML.MlEnum.SVM_KERNEL_TYPE.LINEAR;
                p.SVMType = Emgu.CV.ML.MlEnum.SVM_TYPE.C_SVC;
                p.C = 1;
                p.TermCrit = new MCvTermCriteria(100, 0.00001);
                bool trained = my_SVM.Train(trainingDescriptors, labels, null, null, p);

                //NormalBayesClassifier classifier = new NormalBayesClassifier();
                //classifier.Train(trainingDescriptors, labels, null, null, false);

                using (Image<Gray, Byte> testImgGray = testImg.Convert<Gray, Byte>())
                using (VectorOfKeyPoint testKeyPoints = detector.DetectKeyPointsRaw(testImgGray, null))
                using (Matrix<float> testBOWDescriptor = bowDE.Compute(testImgGray, testKeyPoints))
                {
                    float result = my_SVM.Predict(testBOWDescriptor);
                    //float result = classifier.Predict(testBOWDescriptor, null);
                    //result will indicate whether test image belongs to trainDescriptor label 1, 2 or 3  
                    return result;
                }
             */
            }
        }
    }
}
