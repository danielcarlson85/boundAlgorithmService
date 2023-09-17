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

            public string Name { get; set; }
            public string Reps { get; set; }
            public string Data { get; set; }
            public DateTime Date { get; set; }
        }
    }

}
