using Microsoft.ML.Data;

namespace HarmfulContentDetection.FormApp.DataStructures
{
    public class ImageNetPrediction
    {
        [ColumnName("grid")]
        public float[] PredictedLabels;
    }
}
