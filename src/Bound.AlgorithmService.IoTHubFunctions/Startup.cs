using AlgorithmService.IoTHubFunctions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkoutData.Abstractions.Interfaces;
using WorkoutData.Managers;

[assembly: WebJobsStartup(typeof(Startup))]

namespace AlgorithmService.IoTHubFunctions
{
    public class Startup : IWebJobsStartup
    {
        public static int Counter;

        public void Configure(IWebJobsBuilder builder)
        {
            var blobStorageConnectionString = Environment.GetEnvironmentVariable("StorageAccountStorageConnectionString");

            builder.Services.AddScoped<IBlobContainerManager>(_ => new BlobContainerManager(blobStorageConnectionString));
            builder.Services.AddScoped<IBlobManager>(_ => new BlobsManager(blobStorageConnectionString));

        }
    }
}
