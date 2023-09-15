// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using WorkoutData.Abstractions.Interfaces;
using WorkoutData.Abstractions.Models;

namespace WorkoutData.Managers
{
    public class BlobsManager : IBlobManager
    {
        private static CloudStorageAccount storageAccount = null;
        private static CloudBlobContainer cloudBlobContainer = null;
        private static CloudBlobClient cloudBlobClient = null;

        public BlobsManager(string connectionString)
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

        public async Task<BlobResultSegment> GetAllBlobsInContainer(string containerName)
        {
            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, null);
            return results;
        }

        public async Task<string> GetOneBlobFileInContainer(string containerName, string blobName)
        {
            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            CloudAppendBlob cloudBlockBlob = cloudBlobContainer.GetAppendBlobReference(blobName);
            string result = await cloudBlockBlob.DownloadTextAsync();

            return result;
        }

        public async Task<bool> DeleteBlobInContainer(string containerName, string blobName)
        {
            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
            bool result = await cloudBlockBlob.DeleteIfExistsAsync();

            return result;
        }

        public async Task<string> GetAllDataFromBlob(string containerName, string blobName)
        {
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            CloudBlob cloudBlob = cloudBlobContainer.GetBlobReference(blobName);

            MemoryStream memoryStream = new MemoryStream();
            await cloudBlob.DownloadToStreamAsync(memoryStream);
            memoryStream.Position = 0;

            using (StreamReader streamReader = new StreamReader(memoryStream))
            {
                string allmeasurements = streamReader.ReadToEnd();

                return allmeasurements;
            }
        }

        public async Task<bool> AppendDataInBlob(PathValue pathValue, string textToAppend)
        {
            try
            {
                CloudBlobContainer container = cloudBlobClient.GetContainerReference(pathValue.ContainerName);
                await container.CreateIfNotExistsAsync();

                CloudAppendBlob appBlob = container.GetAppendBlobReference(pathValue.BlobName);

                if (!await appBlob.ExistsAsync())
                {
                    await appBlob.CreateOrReplaceAsync();
                }

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob,
                };

                await container.SetPermissionsAsync(permissions);
                await appBlob.AppendTextAsync(textToAppend);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreateNewBlobInContainer(PathValue pathValue)
        {
            CloudBlobContainer container = cloudBlobClient.GetContainerReference(pathValue.ContainerName);
            await container.CreateIfNotExistsAsync();

            CloudAppendBlob appBlob = container.GetAppendBlobReference(pathValue.BlobName);

            if (!await appBlob.ExistsAsync())
            {
                await appBlob.CreateOrReplaceAsync();
            }

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob,
            };

            await container.SetPermissionsAsync(permissions);

            return await appBlob.ExistsAsync();
        }
    }
}