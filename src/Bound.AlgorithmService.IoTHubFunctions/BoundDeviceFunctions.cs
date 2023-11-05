using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutData.Abstractions.Models;
using WorkoutData.Managers;

namespace Bound.AlgorithmService.IoTHubFunctions
{
    public partial class BoundDeviceFunctions
    {
        //{"MachineName": "ChestMachine", "ObjectId": "6bc00f44-9a73-47da-a542-ee87d1e3981d", "Device": "Empty", "TrainingData": []}

        static string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=bound2023iothub;AccountKey=KkV6TfhwGF24XAm60fyD3/FGsLQtI+0bkYvqbAN7dBoY7FEkRfrbFAh+ZqQEbbL4fBgCVSHtNboh+ASt1Fpi8g==;EndpointSuffix=core.windows.net";

        static BlobContainerManager blobContainerManager = new BlobContainerManager(blobConnectionString);
        static BlobsManager blobsManager = new BlobsManager(blobConnectionString);

        [FunctionName("IoTHubDeviceData")]
        public static async Task RunAsync([EventHubTrigger("samples-workitems", Connection = "IoTHubConnectionString")] string myEventHubMessage, ILogger log)
        {
            log.LogInformation($"C# function triggered to process a message: {myEventHubMessage}");

            var userData = JsonConvert.DeserializeObject<UserData>(myEventHubMessage);
            var cleanedTrainingData = CleanList(userData.TrainingData);
            var userTrainingData = JsonConvert.SerializeObject(cleanedTrainingData);

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

        static List<TrainingData> CleanList(List<TrainingData> dataList)
        {
            var firstOccurrences = new Dictionary<string, TrainingData>();
            var lastOccurrences = new Dictionary<string, TrainingData>();

            foreach (var data in dataList)
            {
                if (!firstOccurrences.ContainsKey(data.X.ToString())) 
                    firstOccurrences[data.X.ToString()] = data;
                lastOccurrences[data.X.ToString()] = data;
            }

            var cleanedData = firstOccurrences.Values.ToList();
            cleanedData.AddRange(lastOccurrences.Values);
            cleanedData = cleanedData.OrderBy(data => data.X).ToList();
            return cleanedData;
        }

        // Need this because when published to azure azure using other date format
        private static string GetBlobFullFileName(UserData userData)
        {
            var year = DateTime.UtcNow.Year.ToString();
            var month = DateTime.UtcNow.Month.ToString();
            var day = DateTime.UtcNow.Day.ToString();

            var hour = DateTime.UtcNow.Hour;
            var hourSweden = (hour + 2).ToString();

            var minute = DateTime.UtcNow.Minute.ToString();

            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            if (hourSweden.Length == 1) hourSweden = "0" + hourSweden;
            if (minute.Length == 1) minute = "0" + minute;

            var fullSwedishDate = $"{year}-{month}-{day} {hourSweden}:{minute}.json";
            var machineName = userData.MachineName.ToLower();
            var fullBlobFileName = machineName + "/" + fullSwedishDate;
            return fullBlobFileName;
        }
    }
}
