using HarmfulContentDetection.FormApp.DataStructures;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Linq;

namespace HarmfulContentDetection.FormApp
{
    class PipelineModelScorer
    {
        private readonly string modelLocation;
        private readonly MLContext mlContext;

        private IList<BoundingBox> _boundingBoxes = new List<BoundingBox>();

        public PipelineModelScorer(string modelLocation, MLContext mlContext)
        {
            this.modelLocation = modelLocation;
            this.mlContext = mlContext;
        }

        public struct ImageNetSettings
        {
            public const int imageHeight = 640;
            public const int imageWidth = 640;
        }

        public struct TinyYoloModelSettings
        {
            public const string ModelInput = "images";
            public const string ModelOutput = "output";
        }

        public ITransformer LoadModel()
        {
            ImageNetData[] inMemoryCollection = new ImageNetData[]
            {
                new ImageNetData
                {
                    InputImage = null,
                    Label = ""
                }
            };
            var data = mlContext.Data.LoadFromEnumerable<ImageNetData>(inMemoryCollection);

            var pipeline = mlContext.Transforms.ResizeImages(outputColumnName: "images", imageWidth: ImageNetSettings.imageWidth, imageHeight: ImageNetSettings.imageHeight, inputColumnName: "InputImage")
                            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "images"))
                            .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelLocation, outputColumnNames: new[] { TinyYoloModelSettings.ModelOutput }, inputColumnNames: new[] { TinyYoloModelSettings.ModelInput }));

            var model = pipeline.Fit(data);

            return model;
        }

        private IEnumerable<float[]> PredictDataUsingModel(IDataView testData, ITransformer model)
        {
            IDataView scoredData = model.Transform(testData);

            IEnumerable<float[]> probabilities = scoredData.GetColumn<float[]>(TinyYoloModelSettings.ModelOutput);

            return probabilities;
        }

        public IEnumerable<float[]> Score(ITransformer model, IDataView data)
        {
            return PredictDataUsingModel(data, model);
        }
    }
}

