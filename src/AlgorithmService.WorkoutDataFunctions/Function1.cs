using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;

namespace AlgorithmService.WorkoutDataFunctions
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([BlobTrigger("{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            (string objectId, string machineName, string fileName) = GetDeviceData(name);

            Console.WriteLine(objectId);
            Console.WriteLine(machineName);
            Console.WriteLine(fileName);

            StreamReader reader = new StreamReader(myBlob);
            string text = reader.ReadToEnd();

            //Console.WriteLine(text);

            Console.WriteLine(stopwatch.ElapsedMilliseconds + " Millisekunder tog det att hämta datan");


            //log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
        private (string objectId, string machineName, string fileName) GetDeviceData(string name)
        {
            var objectId = name.Split('/')[0];
            var machineName = name.Split('/')[1];
            var fileName = name.Split('/')[2];

            return (objectId, machineName, fileName);
        }
    }
}
