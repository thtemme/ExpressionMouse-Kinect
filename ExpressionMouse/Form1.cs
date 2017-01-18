using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Threading;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.FaceTracking;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace ExpressionMouse
{
    public partial class ExpressionMouse : Form
    {
        #region Properties

        private Thread Workerthread;
        private KinectSensor Kinect = null;
        private FaceTracker faceTracker = null;
        private bool pointNClickActive = false;
        private List<Microsoft.Kinect.Toolkit.FaceTracking.Point> mousePositionHistory = new List<Microsoft.Kinect.Toolkit.FaceTracking.Point>();
        private List<float> mouthOpenHistory = new List<float>();
        private List<float> browLowererHistory = new List<float>();
        private List<float> browRaiserHistory = new List<float>();
        private byte[] colorImage;
        private ColorImageFormat colorImageFormat = ColorImageFormat.Undefined;
        private short[] depthImage;
        List<int> gaussFilter = new List<int>();
        private DepthImageFormat depthImageFormat = DepthImageFormat.Undefined;
        public Skeleton[] SkeletonData { get; set; }
        private List<bool> rightEyeClosedHistory = new List<bool>();
        private List<bool> leftEyeClosedHistory = new List<bool>();
        private List<Vector3DF> headRotationHistory = new List<Vector3DF>();
        double gaussFactor = 0;
        int clickDelay = 100;
        #endregion


        public ExpressionMouse()
        {
            InitializeComponent();

            //Try to read the file
            TextReader textReader = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
                XmlSerializer deserializer = new XmlSerializer(typeof(Settings));
                textReader = new StreamReader(@"settings.xml");
                Settings setting;
                setting = (Settings)deserializer.Deserialize(textReader);
                this.nudBrowLowererStartThreshold.Value = setting.BrowLowererStartthreshold;
                this.nudBrowRaiserStartThreshold.Value = setting.BrowRaiserStartThreshold;
                this.nudClickDelay.Value = setting.ClickDelay;
                this.nudConvolutionFilterLength.Value = setting.UsedFramesForClosedEyeDetection;
                this.nudDoubleClickSecondEyeThreshold.Value = setting.DoubleClickSecondEyeThreshold;
                this.nudEyeClosedFilterThreshold.Value = setting.EyeClosedFilterThreshold;
                this.nudHeadToScreenRelationXWidth.Value = setting.HeadToScreenRelationX;
                this.nudHeadToScreenRelationYHeight.Value = setting.HeadToScreenRelationY;
                this.nudMouthOpenConfirmation.Value = setting.MouthOpenConfirmation;
                this.nudMouthOpenEndThreshold.Value = setting.MouthOpenEndThreshold;
                this.nudMouthOpenStartThreshold.Value = setting.MouthOpenStartThreshold;
                this.nudPercentageHorizontalEdgePixels.Value = setting.PercentageHorizontalEdgePixels;
                this.nudScrollMultiplierDown.Value = setting.ScrollMultiplierDown;
                this.nudScrollMultiplierUp.Value = setting.ScrollMultiplierUp;
                this.tbSmoothingFilter.Text = setting.HeadrotationSmoothingFilterValues;
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
        }

        private void Init()
        {

            try
            {
                string[] splitString = { ", " };
                gaussFactor = 0;
                gaussFilter.Clear();
                string[] filterValues = tbSmoothingFilter.Text.Split(splitString, StringSplitOptions.None);
                foreach (var filterValue in filterValues)
                {
                    int value = Int32.Parse(filterValue);
                    gaussFilter.Add(value);
                    gaussFactor += value;
                }
                Kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected); // Get first Kinect Sensor
                Kinect.SkeletonStream.Enable(); // Enable skeletal tracking
                Kinect.ColorStream.Enable();
                Kinect.DepthStream.Enable();
                SkeletonData = new Skeleton[Kinect.SkeletonStream.FrameSkeletonArrayLength]; // Allocate ST data
                Kinect.Start(); // Start Kinect sensor
                faceTracker = new FaceTracker(Kinect);
                Kinect.AllFramesReady += this.OnAllFramesReady;

                //Set Near and Seated Mode
                //Not available for Xbox Sensor!
                //Kinect.SkeletonStream.EnableTrackingInNearRange = true;
                //Kinect.DepthStream.Range = DepthRange.Near;
                Kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                File.AppendAllText("init.txt",DateTime.Now+" - Kinect sensor initialized successfully.\n");
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error during Kinect Initialization. Ensure that Kinect sensor is connected correctly.\n\nError Message:\n" + e.ToString());
                File.AppendAllText("init.txt", DateTime.Now + " - Error during Kinect initialization.\n");
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            Workerthread = new Thread(this.Init);
            Workerthread.Start();
            btStart.Enabled = false;
            btStop.Enabled = true;
        }

        private void SetPictures(Bitmap leftEye, Bitmap rightEye)
        {
            pbLeft.Image = rightEye;
            pbRight.Image = leftEye;
        }

        private void OnAllFramesReady(object sender, AllFramesReadyEventArgs allFramesReadyEventArgs)
        {
            ColorImageFrame colorImageFrame = null;
            DepthImageFrame depthImageFrame = null;
            SkeletonFrame skeletonFrame = null;
            File.AppendAllText("mouseLog.txt", DateTime.Now + " - All Kinect frames ready.\n");
            try
            {
                colorImageFrame = allFramesReadyEventArgs.OpenColorImageFrame();
                depthImageFrame = allFramesReadyEventArgs.OpenDepthImageFrame();
                skeletonFrame = allFramesReadyEventArgs.OpenSkeletonFrame();

                if (colorImageFrame == null || depthImageFrame == null || skeletonFrame == null)
                {
                    File.AppendAllText("mouseLog.txt", DateTime.Now + " - Color- depth or Skeletonframe is null. Aborting Frame.\n");
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
                    File.AppendAllText("mouseLog.txt", DateTime.Now + " - Skeleton is there. Trying to find face.\n");
                    FaceTrackFrame currentFaceFrame = faceTracker.Track(ColorImageFormat.RgbResolution640x480Fps30, colorImage, depthImageFormat, depthImage, activeSkeleton);
                    if (currentFaceFrame.TrackSuccessful)
                    {
                        File.AppendAllText("mouseLog.txt", DateTime.Now + " - Recognized face successfully.\n");
                    }
                    else
                    {
                        File.AppendAllText("mouseLog.txt", DateTime.Now + " - Couldn't find face in frame.\n");
                    }

                    //Get relevant Points for blink detection
                    //Left eye
                    int minX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveOneFourthLeftEyelid].X);
                    int minY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveOneFourthLeftEyelid].Y);
                    int maxX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.BelowThreeFourthLeftEyelid].X);
                    int maxY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.BelowThreeFourthLeftEyelid].Y);
                    Bitmap leftEye = EyeExtract(colorImageFrame, currentFaceFrame, minX, minY, maxX, maxY, false);
                    //this.pbRight.BeginInvoke((MethodInvoker)(() => this.pbRight.Image = leftEye));
                    //

                    //Right eye
                    minX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveThreeFourthRightEyelid].X);
                    minY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.AboveThreeFourthRightEyelid].Y);
                    maxX = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.OneFourthBottomRightEyelid].X);
                    maxY = (int)Math.Round(currentFaceFrame.GetProjected3DShape()[FeaturePoint.OneFourthBottomRightEyelid].Y);
                    
                    Bitmap rightEye = EyeExtract(colorImageFrame, currentFaceFrame, minX, minY, maxX, maxY, true);
                    Bitmap leftEye2 = null;
                    Bitmap rightEye2 = null;
                    if(leftEye != null)
                        leftEye2 = new Bitmap(leftEye);
                    if (rightEye != null)
                        rightEye2 = new Bitmap(rightEye);
                   // System.Delegate d = new MethodInvoker(SetPictures));
                 //   this.Invoke(SetPictures, leftEye);
                    //pbRight.Image = rightEye;
                    this.pbLeft.BeginInvoke((MethodInvoker)(() => this.pbLeft.Image = rightEye2));
                    this.pbLeft.BeginInvoke((MethodInvoker)(() => this.pbRight.Image = leftEye2));
                   // this.Invoke(new MethodInvoker(SetPictures));
                    //Wende Kantenfilter auf die beiden Augen an.

                    if (rightEye != null && leftEye != null)
                    {
                        Dictionary<string, int> angleCount;
                        Bitmap edgePicRight = Convolution(ConvertGrey(rightEye), true, out angleCount);
                        bool rightEyeClosed = IsEyeClosed(angleCount);
                        Bitmap edgePicLeft = Convolution(ConvertGrey(leftEye), false, out angleCount);
                        bool leftEyeClosed = IsEyeClosed(angleCount);
                        //   pbLeftFaltung.Image = edgePicLeft;
                        //   pbRightFaltung.Image = edgePicRight;



                        if (rightEyeClosedHistory.Count > 100)
                            rightEyeClosedHistory.RemoveAt(0);
                        if (leftEyeClosedHistory.Count > 100)
                            leftEyeClosedHistory.RemoveAt(0);
                        leftEyeClosedHistory.Add(leftEyeClosed);
                        rightEyeClosedHistory.Add(rightEyeClosed);

                        //If Face is rotated, move Mouse
                        if (headRotationHistory.Count > gaussFilter.Count - 1 && leftEyeClosedHistory.Count > nudConvolutionFilterLength.Value && currentFaceFrame.TrackSuccessful)
                        {
                            int x = 0;
                            int y = 0;
                            float browRaiserValue = currentFaceFrame.GetAnimationUnitCoefficients()[AnimationUnit.BrowRaiser];
                            float browLowererValue = currentFaceFrame.GetAnimationUnitCoefficients()[AnimationUnit.BrowLower];
                            float mouthOpenValue = currentFaceFrame.GetAnimationUnitCoefficients()[AnimationUnit.JawLower];
                            if (browRaiserHistory.Count >= 100)
                            {
                                browRaiserHistory.RemoveAt(0);
                                browLowererHistory.RemoveAt(0);
                                mouthOpenHistory.RemoveAt(0);
                            }
                            browLowererHistory.Add(browLowererValue);
                            browRaiserHistory.Add(browRaiserValue);
                            mouthOpenHistory.Add(mouthOpenValue);

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
                            while (i < gaussFilter.Count - 1)
                            {
                                rotationMedium.X += (headRotationHistory[headRotationHistory.Count - 1 - i].X * gaussFilter[i]);
                                rotationMedium.Y += (headRotationHistory[headRotationHistory.Count - 1 - i].Y * gaussFilter[i]);
                                i++;
                            }
                            rotationMedium.X = (float)(rotationMedium.X / gaussFactor);
                            rotationMedium.Y = (float)(rotationMedium.Y / gaussFactor);
                            ScaleXY(rotationMedium, out x, out y);


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


                            //Check for right, left or Double Click
                            //1. Check if there was already a click 20 Frames ago, or if Drag & Drop is active
                            if (clickDelay > nudClickDelay.Value && !pointNClickActive)
                            {
                                //2. If not, calculate mean values of dy's last 16 Frames
                                if (CalculateMeanConvolutionValues())
                                    clickDelay = 0;
                                else
                                {
                                    //Else check for open Mouth
                                    if (mouthOpenValue > (float)nudMouthOpenStartThreshold.Value && mouthOpenHistory[mouthOpenHistory.Count - 2] > (float)nudMouthOpenConfirmation.Value && mouthOpenHistory[mouthOpenHistory.Count - 3] > (float)nudMouthOpenConfirmation.Value && mouthOpenHistory[mouthOpenHistory.Count - 4] > (float)nudMouthOpenConfirmation.Value)
                                    {
                                        MouseControl.Move(mousePositionHistory[mousePositionHistory.Count - 4].X, mousePositionHistory[mousePositionHistory.Count - 4].Y);
                                        this.lbAction.Invoke((MethodInvoker)(() => this.lbAction.Items.Add("Left Mouse Down on X: " + mousePositionHistory[mousePositionHistory.Count - 4].X + " Y: " + mousePositionHistory[mousePositionHistory.Count - 4].Y)));
                                        //lbAction.Items.Add("Left Mouse Down on X: " + mousePositionHistory[mousePositionHistory.Count - 4].X + " Y: " + mousePositionHistory[mousePositionHistory.Count - 4].Y);
                                        MouseControl.MouseDownLeft();
                                        pointNClickActive = true;
                                        clickDelay = 0;
                                    }
                                }
                            }
                            else if (pointNClickActive)
                            {
                                if (mouthOpenValue < (float)nudMouthOpenEndThreshold.Value)
                                {
                                    this.lbAction.Invoke((MethodInvoker)(() => this.lbAction.Items.Add("Left Mouse Up on X: " + x + " Y: " + y)));
                                    MouseControl.MouseUpLeft();
                                    pointNClickActive = false;
                                    clickDelay = 0;
                                }
                            }
                            MouseControl.Move(x, y);
                            if (browLowererValue > (float)nudBrowLowererStartThreshold.Value)
                                MouseControl.ScrollDown((int)(-browLowererValue * (int)nudScrollMultiplierDown.Value));
                            if (browRaiserValue > (float)nudBrowRaiserStartThreshold.Value)
                                MouseControl.ScrollDown((int)(browRaiserValue * (int)nudScrollMultiplierUp.Value));
                            if (mousePositionHistory.Count > 100)
                                mousePositionHistory.RemoveAt(0);
                            mousePositionHistory.Add(new Microsoft.Kinect.Toolkit.FaceTracking.Point(x, y));
                            File.AppendAllText("mouseLog.txt", DateTime.Now + " - Face and eyes successfully tracked.\n");
                        }
                    }
                    else
                    {
                        File.AppendAllText("mouseLog.txt", DateTime.Now + " - Face recognized but couldn't find eye in face.\n");
                    }
                    clickDelay++;

                    headRotationHistory.Add(currentFaceFrame.Rotation);
                    if (headRotationHistory.Count >= 100)
                        headRotationHistory.RemoveAt(0);
                }
                else
                {
                    File.AppendAllText("mouseLog.txt", DateTime.Now + " - Active Skeleton is null. Couldn't analyze frame.\n");
                }
            }
            catch (Exception e)
            {
                File.AppendAllText("mouseLog.txt", DateTime.Now + " - Error during frame analyzation.\n"+e.ToString());
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

        private Bitmap Convolution(Bitmap original, bool right, out Dictionary<string, int> angleCount)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);
            angleCount = new Dictionary<string, int>();
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
            int[,] sobelMatrixX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelMatrixY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            int divisor = 8;
            double dxGesamt = 0;
            double dyGesamt = 0;
            double direction = 0;
            for (int i = 1; i < original.Height - 1; i++)
            {
                for (int j = 1; j < original.Width - 1; j++)
                {
                    double sumX = 0;
                    double sumY = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            double x = sobelMatrixX[k + 1, l + 1] * original.GetPixel(j + l, i + k).B;
                            double y = sobelMatrixY[k + 1, l + 1] * original.GetPixel(j + l, i + k).B;
                            sumX += x;
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
            return result;
        }

        private bool IsEyeClosed(Dictionary<string, int> angleDistribution)
        {
            //Eye is closed, if 68% of all Conturpixels have a direction between -20° and +20°
            int valueSum = 0;
            int inAreaSum = 0;
            foreach (KeyValuePair<string, int> kvp in angleDistribution)
            {
                valueSum += kvp.Value;
                if (kvp.Key == "-20-0" || kvp.Key == "0-20")
                {
                    inAreaSum += kvp.Value;
                }
            }
            if (valueSum == 0)
                return false;
            double result = (100 * inAreaSum) / valueSum;
            if (result >= (int)nudPercentageHorizontalEdgePixels.Value)
                return true;
            else return false;

        }

        private Bitmap ConvertGrey(Bitmap source)
        {
            Bitmap bm = new Bitmap(source.Width, source.Height);
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = source.GetPixel(x, y);
                    int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    bm.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
                }
            }
            return bm;
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
            if (maxY > 480 || maxX > 640)
                return null;

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
                return eyeBitmap;
            }
            return null;
        }

        private int ConvertXYToOneDimensionalCoordinate(int x, int y, int width)
        {
            return ((y - 1) * width * 4 + x * 4);
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void FaceMouseConfig_Load(object sender, EventArgs e)
        {
        }

        public void ScaleXY(Vector3DF rotation, out int xResult, out int yResult)
        {
            
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double y = ((SystemParameters.PrimaryScreenHeight / (int)nudHeadToScreenRelationYHeight.Value) * -rotation.X) +
                       (SystemParameters.PrimaryScreenHeight / 2);
            double x = ((SystemParameters.PrimaryScreenWidth / (int)nudHeadToScreenRelationXWidth.Value) * -rotation.Y) +
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

        private bool CalculateMeanConvolutionValues()
        {
            float meanRightClosed = 0;
            float meanLeftClosed = 0;

            for (int i = leftEyeClosedHistory.Count - 1; i >= leftEyeClosedHistory.Count - 1 - nudConvolutionFilterLength.Value; i--)
            {
                if (leftEyeClosedHistory[i])
                    meanLeftClosed++;
                if (rightEyeClosedHistory[i])
                    meanRightClosed++;
            }
            int divisor = (leftEyeClosedHistory.Count) - (leftEyeClosedHistory.Count - 1 - (int)nudConvolutionFilterLength.Value);
            meanRightClosed = meanRightClosed / divisor;
            meanLeftClosed = meanLeftClosed / divisor;
            if (mousePositionHistory.Count - 1 - nudConvolutionFilterLength.Value <= 0)
                return false;
            if ((meanLeftClosed > (float)nudEyeClosedFilterThreshold.Value && meanRightClosed > (float)nudDoubleClickSecondEyeThreshold.Value) || (meanLeftClosed > (float)nudDoubleClickSecondEyeThreshold.Value && meanRightClosed > (float)nudEyeClosedFilterThreshold.Value))
            {
                MouseControl.Move(mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].X, mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].Y);
                MouseControl.Click();
                MouseControl.Click();
                this.lbAction.Invoke((MethodInvoker)(() => this.lbAction.Items.Add("Double Click on X: "+mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].X+" Y: "+mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].Y)));
                return true;
            }
            else if (meanLeftClosed > (float)nudEyeClosedFilterThreshold.Value)
            {
                MouseControl.Move(mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].X, mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].Y);
                MouseControl.Click();
                this.lbAction.Invoke((MethodInvoker)(() => this.lbAction.Items.Add("Left Click on X: " + mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].X + " Y: " + mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].Y)));
                return true;
            }
            else if (meanRightClosed > (float)nudEyeClosedFilterThreshold.Value)
            {
                MouseControl.Move(mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].X, mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].Y);
                MouseControl.RightClick();
                this.lbAction.Invoke((MethodInvoker)(() => this.lbAction.Items.Add("Right Click on X: " + mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].X + " Y: " + mousePositionHistory[mousePositionHistory.Count - 1 - (int)nudConvolutionFilterLength.Value].Y)));
                return true;
            }
            return false;

        }

        private void btStop_Click(object sender, EventArgs e)
        {            
            if (Kinect != null)
            {
                Kinect.AllFramesReady -= this.OnAllFramesReady;
                Kinect.DepthStream.Disable();
                Kinect.ColorStream.Disable();
                Kinect.SkeletonStream.Disable();
                btStop.Enabled = false;
                btStart.Enabled = true;                
                Kinect.Stop();
            }    
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.futuretechblog.com");
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            nudClickDelay.Value = 20;
            tbSmoothingFilter.Text = "64, 55, 45, 35, 25, 20, 15, 10, 8, 7, 6, 5, 4, 3, 2, 1";
            nudPercentageHorizontalEdgePixels.Value = 68;
            nudConvolutionFilterLength.Value = 10;
            nudEyeClosedFilterThreshold.Value = 0.5m;
            nudDoubleClickSecondEyeThreshold.Value = 0.18m;
            nudBrowRaiserStartThreshold.Value = 0.5m;
            nudBrowLowererStartThreshold.Value = 0.16m;
            nudMouthOpenStartThreshold.Value = 0.75m;
            nudMouthOpenConfirmation.Value = 0.5m;
            nudMouthOpenEndThreshold.Value = 0.25m;
            nudScrollMultiplierUp.Value = 150;
            nudScrollMultiplierDown.Value = 250;
            nudHeadToScreenRelationXWidth.Value = 35;
            nudHeadToScreenRelationYHeight.Value = 25;
        }

        private void FaceMouseConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Save Values to Settings Object
                Settings settingObject = new Settings();
                settingObject.BrowLowererStartthreshold = nudBrowLowererStartThreshold.Value;
                settingObject.BrowRaiserStartThreshold = nudBrowRaiserStartThreshold.Value;
                settingObject.ClickDelay = nudClickDelay.Value;
                settingObject.DoubleClickSecondEyeThreshold = nudDoubleClickSecondEyeThreshold.Value;
                settingObject.EyeClosedFilterThreshold = nudEyeClosedFilterThreshold.Value;
                settingObject.HeadrotationSmoothingFilterValues = tbSmoothingFilter.Text;
                settingObject.HeadToScreenRelationX = nudHeadToScreenRelationXWidth.Value;
                settingObject.HeadToScreenRelationY = nudHeadToScreenRelationYHeight.Value;
                settingObject.MouthOpenConfirmation = nudMouthOpenConfirmation.Value;
                settingObject.MouthOpenEndThreshold = nudMouthOpenEndThreshold.Value;
                settingObject.MouthOpenStartThreshold = nudMouthOpenStartThreshold.Value;
                settingObject.PercentageHorizontalEdgePixels = nudPercentageHorizontalEdgePixels.Value;
                settingObject.ScrollMultiplierDown = nudScrollMultiplierDown.Value;
                settingObject.ScrollMultiplierUp = nudScrollMultiplierUp.Value; settingObject.UsedFramesForClosedEyeDetection = nudConvolutionFilterLength.Value;


                //Try to serialize Object
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "Settings";
                xRoot.IsNullable = true;
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                TextWriter textWriter = new StreamWriter(@"settings.xml");
                serializer.Serialize(textWriter, settingObject);
                textWriter.Close();
            }
            catch (Exception)
            {
            }            
        }

        private void ttClickDelay_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
