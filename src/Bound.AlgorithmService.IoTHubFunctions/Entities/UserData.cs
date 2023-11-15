using System;
using System.Collections.Generic;

namespace Bound.AlgorithmService.IoTHubFunctions
{
    public partial class BoundDeviceFunctions
    {
        public class UserData
        {
            public string MachineName { get; set; }
            public string ObjectId { get; set; }
            public List<TrainingData> TrainingData { get; set; }

            public string Email { get; set; }
            public int TotalReps { get; set; }
            public int Weight { get; set; }
            public DateTime Date { get; set; }
        }
    }

}
