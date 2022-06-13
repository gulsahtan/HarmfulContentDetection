using Emgu.CV;
using HarmfulContentDetection.Scorer;
using HarmfulContentDetection.Scorer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarmfulContentDetection.FormApp
{
    public partial class RealTimeForm : Form
    {

        double totalFrame;
        double fps;
        int frameNo;
        bool isReadingFrame;

        VideoCapture capture;
        List<Bitmap> images = new List<Bitmap>();

        public RealTimeForm()
        {
            InitializeComponent();
        }                
 
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frameNo = 0;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                capture = new VideoCapture(ofd.FileName);
                Mat m = new Mat();
                capture.Read(m);
                using var image = (Image)m.ToBitmap();

                List<Prediction> predictions = new List<Prediction>();

                if (Alcohol.Checked == true || Cigarette.Checked == true || Violence.Checked == true)
                {
                    if (Cigarette.Checked == true && Alcohol.Checked == false && Violence.Checked == false)
                    {
                        using var cigaretteScorer = new Scorer<CigaretteModel>("Assets/cigaretteweight/best.onnx");
                        List<Prediction> cigarettePred = cigaretteScorer.Predict(image);
                        predictions.AddRange(cigarettePred);
                    }
                    if (Cigarette.Checked == false && Alcohol.Checked == true && Violence.Checked == false)
                    {
                        using var alcoholScorer = new Scorer<AlcoholModel>("Assets/alcoholweight/best.onnx");
                        List<Prediction> alcoholPred = alcoholScorer.Predict(image);
                        predictions.AddRange(alcoholPred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }

                    if (Cigarette.Checked == false && Alcohol.Checked == false && Violence.Checked == true)
                    {
                        using var violenceScorer = new Scorer<ViolenceModel>("Assets/violenceweight/best.onnx");
                        List<Prediction> violencePred = violenceScorer.Predict(image);
                        predictions.AddRange(violencePred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {
                            if (pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }
                    if (Cigarette.Checked == false && Alcohol.Checked == true && Violence.Checked == true)
                    {
                        using var alcoholviolenceScorer = new Scorer<AlcoholViolenceModel>("Assets/alcoholviolenceweight/best.onnx");
                        List<Prediction> alcoholviolencePred = alcoholviolenceScorer.Predict(image);
                        predictions.AddRange(alcoholviolencePred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");

                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41 || pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });

                    }
                    if (Cigarette.Checked == true && Alcohol.Checked == false && Violence.Checked == true)
                    {
                        using var cigaretteviolenceScorer = new Scorer<CigaretteViolenceModel>("Assets/cigaretteviolenceweight/best.onnx");
                        List<Prediction> cigaretteviolencePred = cigaretteviolenceScorer.Predict(image);
                        predictions.AddRange(cigaretteviolencePred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");

                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {
                            if (pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }
                    if (Cigarette.Checked == true && Alcohol.Checked == true && Violence.Checked == false)
                    {
                        using var alcoholcigaretteScorer = new Scorer<AlcoholCigaretteModel>("Assets/alcoholcigaretteweight/best.onnx");
                        List<Prediction> alcoholcigarette = alcoholcigaretteScorer.Predict(image);
                        predictions.AddRange(alcoholcigarette);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");

                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41)
                            {
                                predictions.Add(pred);
                            }
                        });
                    
                    }
                    if (Cigarette.Checked == true && Alcohol.Checked == true && Violence.Checked == true)
                    {
                        using var allScorer = new Scorer<AlcoholCigaretteViolenceModel>("Assets/alcoholcigaretteviolenceweight/best.onnx");
                        List<Prediction> allPred = allScorer.Predict(image);
                        predictions.AddRange(allPred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");

                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41 || pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }
                }

                using var graphics = Graphics.FromImage(image);
                Bitmap img1 = new Bitmap(image);

                foreach (var prediction in predictions) 
                {
                    if (prediction != null)
                    {
                        double score = Math.Round(prediction.Score, 2);


                        for (Int32 xx = (int)prediction.Rectangle.Left; xx < prediction.Rectangle.Right; xx += 12)
                        {
                            for (Int32 yy = (int)prediction.Rectangle.Top; yy < prediction.Rectangle.Bottom; yy += 12)
                            {
                                Int32 avgR = 0, avgG = 0, avgB = 0;
                                Int32 blurPixelCount = 0;
                                Rectangle currentRect = new Rectangle(xx, yy, 12, 12);

                                for (Int32 a = currentRect.Left; (a < currentRect.Right && a < image.Width); a++)
                                {
                                    for (Int32 b = currentRect.Top; (b < currentRect.Bottom && b < image.Height); b++)
                                    {
                                        Color pixel = img1.GetPixel(a, b);

                                        avgR += pixel.R;
                                        avgG += pixel.G;
                                        avgB += pixel.B;

                                        blurPixelCount++;
                                    }
                                }

                                avgR = avgR / blurPixelCount;
                                avgG = avgG / blurPixelCount;
                                avgB = avgB / blurPixelCount;

                                graphics.FillRectangle(new SolidBrush(Color.FromArgb(avgR, avgG, avgB)), currentRect);
                            }
                        }
                    }
                }      

                Bitmap bitmapImage = new Bitmap(image);
                pictureBox1.Image = bitmapImage;
                totalFrame = capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount);
                fps = capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
                label1.Text = frameNo.ToString() + "/" + totalFrame.ToString();

            }

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                return;
            }
            isReadingFrame = true;

            ObjectDetection(Alcohol.Checked,Violence.Checked, Cigarette.Checked);

        }

        private async void ObjectDetection(bool alcohol, bool violence, bool cigarette)
        {
            Stopwatch stopwatch = new Stopwatch();
            DateTime startTime =DateTime.Now;
            TimeSpan timeElapsed = DateTime.Now - startTime;
            var milisec = timeElapsed.TotalMilliseconds;

            Mat m = new Mat();
            while (isReadingFrame == true && (frameNo +(Convert.ToInt16(numericUpDown1.Value)) < totalFrame))
            {               
                stopwatch.Start();

                frameNo += Convert.ToInt16(numericUpDown1.Value);
                capture.Set(Emgu.CV.CvEnum.CapProp.PosFrames, frameNo);
                capture.Read(m);
                using var image = (Image)m.ToBitmap();

                List<Prediction> predictions = new List<Prediction>();
                if (alcohol == true || cigarette == true || violence == true)
                {
                    if (cigarette == true && alcohol == false && violence == false)
                    {
                        using var cigaretteScorer = new Scorer<CigaretteModel>("Assets/cigaretteweight/best.onnx");
                        List<Prediction> cigarettePred = cigaretteScorer.Predict(image);
                        predictions.AddRange(cigarettePred);
                    }
                    if (cigarette == false && alcohol == true && violence == false)
                    {
                        using var alcoholScorer = new Scorer<AlcoholModel>("Assets/alcoholweight/best.onnx");
                        List<Prediction> alcoholPred = alcoholScorer.Predict(image);
                        predictions.AddRange(alcoholPred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }

                    if (cigarette == false && alcohol == false && violence == true)
                    {
                        using var violenceScorer = new Scorer<ViolenceModel>("Assets/violenceweight/best.onnx");
                        List<Prediction> violencePred = violenceScorer.Predict(image);
                        predictions.AddRange(violencePred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {
                            if (pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }
                    if (cigarette == false && alcohol == true && violence == true)
                    {
                        using var alcoholviolenceScorer = new Scorer<AlcoholViolenceModel>("Assets/alcoholviolenceweight/best.onnx");
                        List<Prediction> alcoholviolencePred = alcoholviolenceScorer.Predict(image);
                        predictions.AddRange(alcoholviolencePred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41 || pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });

                    }
                    if (cigarette == true && alcohol == false && violence == true)
                    {
                        using var cigaretteviolenceScorer = new Scorer<CigaretteViolenceModel>("Assets/cigaretteviolenceweight/best.onnx");
                        List<Prediction> cigaretteviolencePred = cigaretteviolenceScorer.Predict(image);
                        predictions.AddRange(cigaretteviolencePred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {
                            if (pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }
                    if (cigarette == true && alcohol == true && violence == false)
                    {
                        using var alcoholcigaretteScorer = new Scorer<AlcoholCigaretteModel>("Assets/alcoholcigaretteweight/best.onnx");
                        List<Prediction> alcoholcigarette = alcoholcigaretteScorer.Predict(image);
                        predictions.AddRange(alcoholcigarette);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41)
                            {
                                predictions.Add(pred);
                            }
                        });
                    }
                    if (cigarette == true && alcohol == true && violence == true)
                    {
                        using var allScorer = new Scorer<AlcoholCigaretteViolenceModel>("Assets/alcoholcigaretteviolenceweight/best.onnx");
                        List<Prediction> allPred = allScorer.Predict(image);
                        predictions.AddRange(allPred);
                        using var yolosScorer = new Scorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");
                        List<Prediction> yolosPred = yolosScorer.Predict(image);
                        Parallel.ForEach(yolosPred, pred =>
                        {

                            if (pred.Label.Id == 40 || pred.Label.Id == 41 || pred.Label.Id == 44)
                            {
                                predictions.Add(pred);
                            }
                        });
                       
                    }
                }

                using var graphics = Graphics.FromImage(image);
                Bitmap img1 = new Bitmap(image);
                foreach (var prediction in predictions)
                {
                    if (prediction != null)
                    {
                        double score = Math.Round(prediction.Score, 2);


                        for (Int32 xx = (int)prediction.Rectangle.Left; xx < prediction.Rectangle.Right; xx += 12)
                        {
                            for (Int32 yy = (int)prediction.Rectangle.Top; yy < prediction.Rectangle.Bottom; yy += 12)
                            {
                                Int32 avgR = 0, avgG = 0, avgB = 0;
                                Int32 blurPixelCount = 0;
                                Rectangle currentRect = new Rectangle(xx, yy, 12, 12);

                                for (Int32 a = currentRect.Left; (a < currentRect.Right && a < image.Width); a++)
                                {
                                    for (Int32 b = currentRect.Top; (b < currentRect.Bottom && b < image.Height); b++)
                                    {
                                        Color pixel = img1.GetPixel(a, b);

                                        avgR += pixel.R;
                                        avgG += pixel.G;
                                        avgB += pixel.B;

                                        blurPixelCount++;
                                    }
                                }

                                avgR = avgR / blurPixelCount;
                                avgG = avgG / blurPixelCount;
                                avgB = avgB / blurPixelCount;

                                graphics.FillRectangle(new SolidBrush(Color.FromArgb(avgR, avgG, avgB)), currentRect);
                            }
                        }
                    }
                }

                Bitmap bitmapImage = new Bitmap(image);

                stopwatch.Stop();

                var timeExecute = stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
                pictureBox1.Image = bitmapImage.ToMat().ToBitmap();

                await Task.Delay((int)Math.Round(1000.0 / (double)timeExecute));
                label1.Text = frameNo.ToString() + "/" + totalFrame.ToString();
            }
        }   
        private void btnPause_Click(object sender, EventArgs e)
        {
            isReadingFrame = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            MenuForms to = new MenuForms();
            to.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MenuForms to = new MenuForms();
                    to.Show();
                    RealTimeForm to1 = new RealTimeForm();                   
                    to1.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
