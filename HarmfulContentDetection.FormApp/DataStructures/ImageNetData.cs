using System.Drawing;
using Microsoft.ML.Transforms.Image;

namespace HarmfulContentDetection.FormApp.DataStructures
{
    public class ImageNetData
    {
        // Dimensions provided here seem not to play an important role
        [ImageType(640, 640)]
        public Bitmap InputImage { get; set; }

        public string Label { get; set; }

        public ImageNetData() 
        {
            InputImage = null;
            Label = "";
        }
    }
}
