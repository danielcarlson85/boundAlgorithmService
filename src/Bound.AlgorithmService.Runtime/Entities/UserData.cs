// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Bound.AlgorithmService.Runtime.Entities;
using System;
using System.Collections.Generic;

namespace WorkoutData.Runtime
{
    public class UserData
    {
        public string MachineName { get; set; }
        public string ObjectId { get; set; }
        public List<TrainingData> TrainingData { get; set; }
    }
}
