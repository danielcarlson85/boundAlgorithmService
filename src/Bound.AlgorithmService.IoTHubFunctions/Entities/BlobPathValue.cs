﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using System;
using static Bound.AlgorithmService.IoTHubFunctions.BoundDeviceFunctions;

namespace WorkoutData.Abstractions.Models
{
    public class BlobPathValue
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }

        public UserData UserData { get; set; }
    }
}
