﻿using System.Collections.Generic;

namespace HarmfulContentDetection.Scorer.Models.Abstract
{
    /// <summary>
    /// Model descriptor.
    /// </summary>
    public abstract class Model
    {
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public abstract int Depth { get; set; }

        public abstract int Dimensions { get; set; }

        public abstract float[] Strides { get; set; }
        public abstract float[][][] Anchors { get; set; }
        public abstract int[] Shapes { get; set; }

        public abstract float Confidence { get; set; }
        public abstract float MulConfidence { get; set; }
        public abstract float Overlap { get; set; }

        public abstract string[] Outputs { get; set; }
        public abstract List<Label> Labels { get; set; }
        public abstract bool UseDetect { get; set; }
    }
}
