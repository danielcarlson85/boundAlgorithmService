// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkoutData.Abstractions.Interfaces;
using WorkoutData.Abstractions.Models;

namespace WorkoutData.Managers
{
    public class BlobContainerManager : IBlobContainerManager
    {
        private static CloudStorageAccount storageAccount = null;
        private static CloudBlobContainer cloudBlobContainer = null;
        private static CloudBlobClient cloudBlobClient = null;

        public BlobContainerManager(string connectionString)
        {
            var blobStorageConnectionString = connectionString;

            try
            {
                storageAccount = CloudStorageAccount.Parse(blobStorageConnectionString);
                cloudBlobClient = storageAccount.CreateCloudBlobClient();
            }
            catch (FormatException formatException)
            {
                throw new Exception(formatException.Message + " Please check your connectionstring");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DeleteContainer(string containerName)
        {
            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            return await cloudBlobContainer.DeleteIfExistsAsync();
        }

        public async Task<bool> CreateBlobContainer(string blobContainerName)
        {
            Debug.WriteLine("Creates a reference to the container that will be used, must use small charachters on name");

            cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainerName);
            await cloudBlobContainer.CreateIfNotExistsAsync();

            Debug.WriteLine(cloudBlobContainer.Name + " is created");

            return true;
        }

        public async Task UploadFileToContainer()
        {
            Debug.WriteLine("Creates a reference to the Blobfile that the file should be uploaded to");

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("UploadedFile.txt");
            await cloudBlockBlob.UploadFromFileAsync("UploadFile.txt");

            Debug.WriteLine(cloudBlobContainer.Name + " is uploaded");
        }

        public async Task DownloadFileFromContainer()
        {
            Debug.WriteLine("Creates a reference to the Blobfile that should be downloaded");

            string destinationFile = "DOWNLOADED.txt";
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("DownloadedFile.txt");
            await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);
            Debug.WriteLine(cloudBlockBlob.Uri + " is downloaded");
        }

        public async Task<bool> DeleteAllContainers()
        {
            Debug.WriteLine("If container is empty it is deleted");
            return await cloudBlobContainer.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllBlobContainers()
        {
            List<string> blobContainers = new List<string>();

            var containers = await cloudBlobClient.ListContainersSegmentedAsync(null);
            blobContainers.AddRange(containers.Results.Select(u => u.Name));

            return blobContainers;
        }

        public CloudBlobContainer GetOneBlobContainer(string blobContainerName)
        {
            var container = cloudBlobClient.GetContainerReference(blobContainerName);
            return container;
        }

        private async Task<bool> CheckIfBlobExistsOnCloud(CloudBlobClient client, BlobPathValue pathValue)
        {
            return await client.GetContainerReference(pathValue.ContainerName)
                .GetBlobReference(pathValue.BlobName)
                .ExistsAsync();
        }
    }
}