using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bound.AlgorithmService.IoTHubFunctions
{
    public class BoundDeviceFunctions
    {

        [FunctionName("IoTHubDeviceData")]
        public static void Run([EventHubTrigger("samples-workitems", Connection = "IoTHubConnectionString")] string myEventHubMessage, ILogger log)
        {
            log.LogInformation($"C# function triggered to process a message: {myEventHubMessage}");

            var UserData = JsonConvert.DeserializeObject<UserData>(myEventHubMessage);
        }


        public class UserData
        {
            public string MachineName { get; set; }
            public string ObjectId { get; set; }
            public List<TrainingData> TrainingData { get; set; }
        }

        public class TrainingData
        {
            public long X { get; set; }
            public long Y { get; set; }
            public long Z { get; set; }
        }
    }

}
