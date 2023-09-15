using AlgorithmService.IoTHubFunctions;
using AlgorithmService.IoTHubFunctions.Constants;
using AlgorithmService.IoTHubFunctions.Entities;
using AlgorithmService.IoTHubFunctions.Helpers;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutData.Abstractions.Interfaces;
using WorkoutData.Abstractions.Models;
using WorkoutData.Managers;
using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

namespace Bound.AlgorithmService.IoTHubFunctions
{
    public class BoundDeviceFunctions
    {

        static string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=bound2023iothub;AccountKey=KkV6TfhwGF24XAm60fyD3/FGsLQtI+0bkYvqbAN7dBoY7FEkRfrbFAh+ZqQEbbL4fBgCVSHtNboh+ASt1Fpi8g==;EndpointSuffix=core.windows.net";

        static BlobContainerManager blobContainerManager = new BlobContainerManager(blobConnectionString);
        static BlobsManager blobsManager = new BlobsManager(blobConnectionString);

        [FunctionName("IoTHubDeviceData")]
        public static async Task RunAsync([EventHubTrigger("samples-workitems", Connection = "IoTHubConnectionString")] string myEventHubMessage, ILogger log)
        {
            log.LogInformation($"C# function triggered to process a message: {myEventHubMessage}");

            var userData = JsonConvert.DeserializeObject<UserData>(myEventHubMessage);
            var userTrainingData = JsonConvert.SerializeObject(userData.TrainingData);


            var dataToSave = new BlobPathValue();

            dataToSave.ContainerName = userData.ObjectId;
            dataToSave.BlobName = userData.MachineName.ToLower() + "/" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            await blobsManager.AppendDataInBlob(dataToSave, userTrainingData);
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
