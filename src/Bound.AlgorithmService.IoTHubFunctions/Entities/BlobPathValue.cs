// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using System;

namespace WorkoutData.Abstractions.Models
{
    public class BlobPathValue
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }

        public UserData UserData { get; set; }
    }
}
