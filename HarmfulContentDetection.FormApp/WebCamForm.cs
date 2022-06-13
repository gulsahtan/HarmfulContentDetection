using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HarmfulContentDetection.Scorer;
using HarmfulContentDetection.Scorer.Models;

namespace HarmfulContentDetection.FormApp
{
    public partial class WebCamForm : Form
    {
        public WebCamForm()
        {
            InitializeComponent();
        }

        private void WebCamForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (device.IsRunning == true)
            {
                device.SignalToStop();
                device.WaitForStop();
            }
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Dialog Title", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MenuForms to = new MenuForms();
                    to.Show();
                    WebCamForm to1 = new WebCamForm();
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
        FilterInfoCollection filter;
        VideoCaptureDevice device;
        private void btnStart_Click(object sender, EventArgs e)
        {
            device= new VideoCaptureDevice(filter[cboCamera.SelectedIndex].MonikerString);
            device.NewFrame += Device_NewFrame;
            device.Start();
        }
        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            using var image = (Bitmap)eventArgs.Frame.Clone();

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
                }
                if (Cigarette.Checked == true && Alcohol.Checked == false && Violence.Checked == true)
                {
                    using var cigaretteviolenceScorer = new Scorer<CigaretteViolenceModel>("Assets/cigaretteviolenceweight/best.onnx");
                    List<Prediction> cigaretteviolencePred = cigaretteviolenceScorer.Predict(image);
                    predictions.AddRange(cigaretteviolencePred);
                }
                if (Cigarette.Checked == true && Alcohol.Checked == true && Violence.Checked == false)
                {
                    using var alcoholcigaretteScorer = new Scorer<AlcoholCigaretteModel>("Assets/alcoholcigaretteweight/best.onnx");
                    List<Prediction> alcoholcigarette = alcoholcigaretteScorer.Predict(image);
                    predictions.AddRange(alcoholcigarette);
                }
                if (Cigarette.Checked == true && Alcohol.Checked == true && Violence.Checked == true)
                {
                    using var allScorer = new Scorer<AlcoholCigaretteViolenceModel>("Assets/alcoholcigaretteviolenceweight/best.onnx");
                    List<Prediction> allPred = allScorer.Predict(image);
                    predictions.AddRange(allPred);
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

            pic.Image = bitmapImage;
        }
        private void WebCamForm_Load(object sender, EventArgs e)
        {
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filter)           
                cboCamera.Items.Add(device.Name);
            //cboCamera.SelectedIndex = 0;
            device = new VideoCaptureDevice();
            
        }
    }
}
