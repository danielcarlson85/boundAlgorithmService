using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WorkoutData.Abstractions.Models;
using WorkoutData.Managers;

namespace Bound.AlgorithmService.IoTHubFunctions
{
    public partial class BoundDeviceFunctions
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
            string fullBlobFileName = GetBlobFullFileName(userData);

            dataToSave.ContainerName = userData.ObjectId;
            dataToSave.BlobName = fullBlobFileName;

            var isSavedToBlob = await blobsManager.AppendDataInBlob(dataToSave, userTrainingData);
            if (isSavedToBlob)
            {
                Console.WriteLine();
                Console.WriteLine($"All training data is saved to blob: {userData.ObjectId}/{dataToSave.BlobName}");
            }
        }


        // Need this because when published to azure azure using other date format
        private static string GetBlobFullFileName(UserData userData)
        {
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.Month.ToString();
            var day = DateTime.Now.Day.ToString();

            var hour = DateTime.Now.Hour.ToString();
            var minute = DateTime.Now.Minute.ToString();

            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            if (hour.Length == 1) hour = "0" + hour;
            if (minute.Length == 1) minute = "0" + minute;

            var fullSwedishDate = $"{year}-{month}-{day} {hour}:{minute}.json";
            var machineName = userData.MachineName.ToLower();
            var fullBlobFileName = machineName + "/" + fullSwedishDate;
            return fullBlobFileName;
        }
    }
}
