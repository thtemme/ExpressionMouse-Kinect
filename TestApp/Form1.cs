using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.FaceTracking;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ExpressionMouse;

namespace TestApp
{
    public partial class DebugForm : Form
    {
        
        private const double sigma = 4;
        private KinectSensor Kinect = null;
        private FaceTracker faceTracker = null;
        private byte[] colorImage;
        private ColorImageFormat colorImageFormat = ColorImageFormat.Undefined;
        private short[] depthImage;
        private DepthImageFormat depthImageFormat = DepthImageFormat.Undefined;
        public Skeleton[] SkeletonData { get; set; }
        private List<Vector3DF> headRotationHistory = new List<Vector3DF>();
        //via Pascalsches Dreieck
        //int[] gaussFilter = { 92378, 75582, 50388, 27132, 11628, 3876, 969, 171, 19, 1 };
        //int[] gaussFilter = { 155117520, 145422675, 119759850, 86493225, 54627300, 30045015, 14307150, 5852925, 2035800, 593775, 142506, 27405, 4060, 435, 30, 1 };
        int[] gaussFilter = { 64, 55, 45, 35, 25, 20, 15, 10, 8, 7, 6, 5, 4, 3, 2, 1 };
        List<string> relevantDiffKeys = new List<string>();
        private const int filterLength = 16;
        double gaussFactor = 0;
        int rightCounter = 0;
        int leftCounter = 0;


        public void StartKinectST()
        {
            //Berechne initial Gauß-Filter für Glättung:
            //for (int j = 0; j < filterLength; j++)
            //{
            //    //Gauss-Berechnung
            //gaussFilter[j] = (1 / (Math.Sqrt(2 * Math.PI) * sigma)) * Math.Pow(Math.E, -(Math.Pow(j, 2) / (2 * Math.Pow(sigma, 2))));
            //}
            foreach (double filterValue in gaussFilter)
                gaussFactor += filterValue;

            Kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected); // Get first Kinect Sensor
            Kinect.SkeletonStream.Enable(); // Enable skeletal tracking
            Kinect.ColorStream.Enable();
            Kinect.DepthStream.Enable();
            SkeletonData = new Skeleton[Kinect.SkeletonStream.FrameSkeletonArrayLength]; // Allocate ST data
            Kinect.Start(); // Start Kinect sensor
            faceTracker = new FaceTracker(Kinect);
            Kinect.AllFramesReady += this.OnAllFramesReady;

            //Set Near and Seated Mode
            Kinect.SkeletonStream.EnableTrackingInNearRange = true;
            Kinect.DepthStream.Range = DepthRange.Near;
            Kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;

        }

        private Bitmap ConvertGrey(Bitmap source)
        {
            Bitmap bm = new Bitmap(source.Width,source.Height);
            for(int y=0;y<bm.Height;y++)
            {
                for(int x=0;x<bm.Width;x++)
                {
                    Color c=source.GetPixel(x,y);
                    int luma = (int)(c.R*0.3 + c.G*0.59+ c.B*0.11);
                    bm.SetPixel(x,y,Color.FromArgb(luma,luma,luma));                    
                }
            }
            return bm;
        }


        private Bitmap Convolution(Bitmap original, bool right, out double dx, out double dy)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);
            Dictionary<string, int> angleCount = new Dictionary<string, int>();
            angleCount.Add("-90--75", 0);
            angleCount.Add("-75--50", 0);
            angleCount.Add("-50--35", 0);
            angleCount.Add("-35--20", 0);
            angleCount.Add("-20-0", 0);
            angleCount.Add("0-20", 0);
            angleCount.Add("20-30", 0);
            angleCount.Add("30-45", 0);
            angleCount.Add("45-60", 0);
            angleCount.Add("60-75", 0);
            angleCount.Add("75-90", 0);
            angleCount.Add("else", 0);
            int[,] sobelMatrixX = {{-1,0,1},{-2,0,2},{-1,0,1}};
            int[,] sobelMatrixY = {{-1,-2,-1},{0,0,0},{1,2,1}};
            int divisor = 8;
            double dxGesamt = 0;
            double dyGesamt = 0;
            double direction = 0;
            for (int i = 1; i < original.Height-1; i++)
            {                
                for (int j = 1; j < original.Width-1; j++)
                {
                    double sumX = 0;
                    double sumY = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            double x = sobelMatrixX[k+1,l+1] * original.GetPixel(j+l,i+k).B;
                            double y = sobelMatrixY[k + 1, l + 1] * original.GetPixel(j + l, i + k).B;
                            sumX+=x;
                            sumY += y;
                        }
                    }
                    sumX = sumX / divisor;
                    sumY = sumY / divisor;

                    direction = (Math.Atan(sumX / sumY)) * 180 / Math.PI;
                    if (direction >= -90 && direction <= -75)
                        angleCount["-90--75"]++;
                    else if (direction > -75 && direction <= -50)
                        angleCount["-75--50"]++;
                    else if (direction > -50 && direction <= -35)
                        angleCount["-50--35"]++;
                    else if (direction > -35 && direction <= -20)
                        angleCount["-35--20"]++;
                    else if (direction > -20 && direction <= 0)
                        angleCount["-20-0"]++;
                    else if (direction > 0 && direction <= 20)
                        angleCount["0-20"]++;
                    else if (direction > 20 && direction <= 30)
                        angleCount["20-30"]++;
                    else if (direction > 30 && direction <= 45)
                        angleCount["30-45"]++;
                    else if (direction > 45 && direction <= 60)
                        angleCount["45-60"]++;
                    else if (direction > 60 && direction <= 75)
                        angleCount["60-75"]++;
                    else if (direction > 75 && direction <= 90)
                        angleCount["75-90"]++;
                    else
                        angleCount["else"]++;

                 
                    int gesamt = (int)Math.Sqrt(Math.Pow(sumX, 2) + Math.Pow(sumY, 2));
                    result.SetPixel(j, i, Color.FromArgb(gesamt, gesamt, gesamt));

                    dxGesamt += sumX;
                    dyGesamt += sumY;
                }
            }
            dxGesamt = dxGesamt / (original.Width * original.Height);
            dyGesamt = dyGesamt / (original.Width * original.Height);
            dx = dxGesamt;
            dy = dyGesamt;

            if (IsEyeClosed(angleCount))
            {
                if (right)
                    cbLeftEyeClosed.Checked = true;
                else
                    cbRightEyeClosed.Checked = true;
            }
            else
            {
                if (right)
                    cbLeftEyeClosed.Checked = false;
                else                
                    cbRightEyeClosed.Checked = false;
            }
            return result;
        }

        private bool IsEyeClosed(Dictionary<string, int> angleDistribution)
        {
            //Eye is closed, if 75% of all Pixels have a direction between -35° and +30°
            int valueSum = 0;
            int inAreaSum = 0;
            foreach(KeyValuePair<string,int> kvp in angleDistribution)
            {
                valueSum += kvp.Value;
                if ( kvp.Key == "-20-0" || kvp.Key == "0-20")
                {
                    inAreaSum += kvp.Value;
                }
            }
            if (valueSum == 0)
                return false;
            double result = (100 * inAreaSum) / valueSum;
            if (result >= 65)
                return true;
            else return false;

        }

        private void OnAllFramesReady(object sender, Microsoft.Kinect.AllFramesReadyEventArgs allFramesReadyEventArgs)
        {
            ColorImageFrame colorImageFrame = null;
            DepthImageFrame depthImageFrame = null;
            SkeletonFrame skeletonFrame = null;

            try
            {
                colorImageFrame = allFramesReadyEventArgs.OpenColorImageFrame();
                depthImageFrame = allFramesReadyEventArgs.OpenDepthImageFrame();
                skeletonFrame = allFramesReadyEventArgs.OpenSkeletonFrame();

                if (colorImageFrame == null || depthImageFrame == null || skeletonFrame == null)
                {
                    return;
                }

                // Check for image format changes.  The FaceTracker doesn't
                // deal with that so we need to reset.
                if (this.depthImageFormat != depthImageFrame.Format)
                {
                    this.depthImage = null;
                    this.depthImageFormat = depthImageFrame.Format;
                }

                if (this.colorImageFormat != colorImageFrame.Format)
                {
                    this.colorImage = null;
                    this.colorImageFormat = colorImageFrame.Format;
                }

                // Create any buffers to store copies of the data we work with
                if (this.depthImage == null)
                {
                    this.depthImage = new short[depthImageFrame.PixelDataLength];
                }

                if (this.colorImage == null)
                {
                    this.colorImage = new byte[colorImageFrame.PixelDataLength];
                }

                // Get the skeleton information
                if (this.SkeletonData == null || this.SkeletonData.Length != skeletonFrame.SkeletonArrayLength)
                {
                    this.SkeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                }

                colorImageFrame.CopyPixelDataTo(this.colorImage);
                depthImageFrame.CopyPixelDataTo(this.depthImage);
                skeletonFrame.CopySkeletonDataTo(this.SkeletonData);
                Skeleton activeSkeleton = null;
                activeSkeleton = (from skel in this.SkeletonData where skel.TrackingState == SkeletonTrackingState.Tracked select skel).FirstOrDefault();


                //Idea: Separate Eye-Parts of Color Image
                //Use learning Algorithm for right and left eye
                //Detect blink on separated parts of color Image

                //colorImage is one dimensional array with 640 x 480 x 4 (RGBA) values


                if (activeSkeleton != null)
                {
                    FaceTrackFrame currentFaceFrame = faceTracker.Track(ColorImageFormat.RgbResolution640x480Fps30, colorImage, depthImageFormat, depthImage, activeSkeleton);
                    float browRaiserValue = currentFaceFrame.GetAnimationUnitCoefficients()[AnimationUnit.BrowRaiser];
                    float browLowererValue = currentFaceFrame.GetAnimationUnitCoefficients()[AnimationUnit.BrowLower];
                    tbBrowLowerer.Text = browLowererValue.ToString();
                    tbBrowRaiser.Text = browRaiserValue.ToString();
                    //Get relevant Points for blink detection
                    //Left eye
                    int minX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveOneFourthLeftEyelid].X);
                    int minY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveOneFourthLeftEyelid].Y);
                    int maxX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.BelowThreeFourthLeftEyelid].X);
                    int maxY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.BelowThreeFourthLeftEyelid].Y);
                    Bitmap leftEye = EyeExtract(colorImageFrame, currentFaceFrame, minX, minY, maxX, maxY, false);
                    pbLeftEye.Image = leftEye;

                    //Right eye
                    minX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveThreeFourthRightEyelid].X);
                    minY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveThreeFourthRightEyelid].Y);
                    maxX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.OneFourthBottomRightEyelid].X);
                    maxY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.OneFourthBottomRightEyelid].Y);

                    Bitmap rightEye = EyeExtract(colorImageFrame, currentFaceFrame, minX, minY, maxX, maxY, true);
                    pbRightEye.Image = rightEye;

                    //Wende Kantenfilter auf die beiden Augen an.
                    double dxRight;
                    double dyRight;
                    double dxLeft;
                    double dyLeft;
                    if (rightEye != null && leftEye != null)
                    {
                        Bitmap edgePicRight = Convolution(ConvertGrey(rightEye), true, out dxRight, out dyRight);
                        Bitmap edgePicLeft = Convolution(ConvertGrey(leftEye), false, out dxLeft, out dyLeft);
                        


                        //If Face is rotated, move Mouse
                        if (headRotationHistory.Count > filterLength && currentFaceFrame.TrackSuccessful)
                        {
                            int x = 0;
                            int y = 0;

                            //Method 1: Ohne Glättung                            
                            //ScaleXY(currentFaceFrame.Rotation, out x, out y);                            
                            //MouseControl.Move(x, y);

                            ////Method 2: Glättung über die letzten x Bilder:                           
                            //int i = 0;
                            //Vector3DF rotationMedium = new Vector3DF();
                            //while (i < 10 && headRotationHistory.Count - 1 > i)
                            //{
                            //    i++;
                            //    rotationMedium.X += headRotationHistory[headRotationHistory.Count - 1 - i].X;
                            //    rotationMedium.Y += headRotationHistory[headRotationHistory.Count - 1 - i].Y;
                            //}
                            //rotationMedium.X = rotationMedium.X / i;
                            //rotationMedium.Y = rotationMedium.Y / i;
                            //ScaleXY(rotationMedium, out x, out y);
                            //MouseControl.Move(x, y);

                            //Method 3: Gauß-Filter: Gewichte die letzten Bilder stärker.



                            Vector3DF rotationMedium = new Vector3DF();
                            rotationMedium.X = currentFaceFrame.Rotation.X * gaussFilter[0];
                            rotationMedium.Y = currentFaceFrame.Rotation.Y * gaussFilter[0];
                            int i = 0;
                            while (i < filterLength - 1)
                            {
                                i++;
                                rotationMedium.X += (headRotationHistory[headRotationHistory.Count - 1 - i].X * gaussFilter[i]);
                                rotationMedium.Y += (headRotationHistory[headRotationHistory.Count - 1 - i].Y * gaussFilter[i]);
                            }
                            rotationMedium.X = (float)(rotationMedium.X / gaussFactor);
                            rotationMedium.Y = (float)(rotationMedium.Y / gaussFactor);
                            ScaleXY(rotationMedium, out x, out y);
                            
                            MouseControl.Move(x, y);
                            //Method 4: Quadratische Glättung
                            //double deltaX = ((-currentFaceFrame.Rotation.Y) - (-headRotationHistory.Last().Y));
                            //double deltaY = ((-currentFaceFrame.Rotation.X) - (-headRotationHistory.Last().X));
                            //if (deltaX < 0)
                            //    deltaX = -Math.Pow(deltaX, 2) * 4;
                            //else
                            //    deltaX = Math.Pow(deltaX, 2) * 4;
                            //if (deltaY < 0)
                            //    deltaY = -Math.Pow(deltaY, 2) * 5;
                            //else
                            //    deltaY = Math.Pow(deltaY, 2) * 5;
                            //MouseControl.DeltaMove((int)Math.Round(deltaX, 0), (int)Math.Round(deltaY));
                        }

                        headRotationHistory.Add(currentFaceFrame.Rotation);
                        if (headRotationHistory.Count >= 100)
                            headRotationHistory.RemoveAt(0);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (colorImageFrame != null)
                {
                    colorImageFrame.Dispose();
                }

                if (depthImageFrame != null)
                {
                    depthImageFrame.Dispose();
                }

                if (skeletonFrame != null)
                {
                    skeletonFrame.Dispose();
                }
            }
        }

        private Bitmap EyeExtract(ColorImageFrame colorImageFrame, FaceTrackFrame currentFaceFrame, int minX, int minY, int maxX, int maxY, bool right)
        { 
            if (maxX < minX)
            {
                int temp = maxX;
                maxX = minX;
                minX = temp;
            }
            if (maxY < minY)
            {
                int temp = maxY;
                maxY = minY;
                minY = temp;
            }

            //Try to extract picture from area made by the marked points 
            if (maxX > minX && maxY > minY && minY > 0 && minX > 0)
            {
                int minPosOneDim = ConvertXYToOneDimensionalCoordinate((int)minX, (int)minY, colorImageFrame.Width);
                int maxPosOneDim = ConvertXYToOneDimensionalCoordinate((int)maxX, (int)maxY, colorImageFrame.Width);

                int xLength = maxX - minX + 1;
                int yLength = maxY - minY + 1;
                //ARGB
                object[, ,] eyePic = new object[xLength, yLength, 4];
                for (int xi = minX; xi <= maxX; xi++)
                {
                    for (int yi = minY; yi <= maxY; yi++)
                    {
                        int oneDimensional = ConvertXYToOneDimensionalCoordinate(xi, yi, colorImageFrame.Width);
                        eyePic[xi - minX, yi - minY, 0] = colorImage.GetValue(oneDimensional);
                        eyePic[xi - minX, yi - minY, 1] = colorImage.GetValue(oneDimensional + 1);
                        eyePic[xi - minX, yi - minY, 2] = colorImage.GetValue(oneDimensional + 2);
                        eyePic[xi - minX, yi - minY, 3] = colorImage.GetValue(oneDimensional + 3);
                    }
                }
                //Write eyePic to PictureBox
                Bitmap eyeBitmap = new Bitmap(xLength, yLength);
                for (int i = 0; i < xLength; i++)
                {
                    for (int j = 0; j < yLength; j++)
                    {
                        eyeBitmap.SetPixel(i, j, Color.FromArgb(255, Int32.Parse(eyePic[i, j, 0].ToString()), Int32.Parse(eyePic[i, j, 1].ToString()), Int32.Parse(eyePic[i, j, 2].ToString())));
                    }
                }
                
                
                if (rightCounter % 100 == 0)
                    rightCounter = 0;
                if (leftCounter % 100 == 0)
                    leftCounter = 0;
                return eyeBitmap;
            }
            return null;
        }

        public void ScaleXY(Vector3DF rotation, out int xResult, out int yResult)
        {

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double y = ((SystemParameters.PrimaryScreenHeight / 30) * -rotation.X) +
                       (SystemParameters.PrimaryScreenHeight / 2);
            double x = ((SystemParameters.PrimaryScreenWidth / 40) * -rotation.Y) +
                       (SystemParameters.PrimaryScreenWidth / 2);

            if (x < 0)
            {
                x = 0;
            }
            else if (x > screenWidth - 1)
            {
                x = screenWidth - 1;
            }

            if (y < 0)
            {
                y = 0;
            }
            else if (y > SystemParameters.PrimaryScreenHeight - 1)
                y = SystemParameters.PrimaryScreenHeight;
            xResult = (int)x;
            yResult = (int)y;
        }

        private int ConvertXYToOneDimensionalCoordinate(int x, int y, int width)
        {
            return ((y-1) * width * 4 + x * 4);
        }

        private void ConvertOneDimensionalCoordinateToXY(int value, int width, out int x, out int y)
        {
            x = value % width;
            y = value / width;
        }

        public DebugForm()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            StartKinectST();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
