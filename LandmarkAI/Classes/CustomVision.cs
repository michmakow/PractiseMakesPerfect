﻿using System;
using System.Collections.Generic;

namespace LandmarkAI.Classes
{
    public class Prediction
        {
            public double Probability { get; set; }
            public string TagId { get; set; }
            public string TagName { get; set; }
        }

        public class CustomVision
        {
            public string Id { get; set; }
            public string Project { get; set; }
            public string Iteration { get; set; }
            public DateTime Created { get; set; }
            public IList<Prediction> Predictions { get; set; }
        }
}
