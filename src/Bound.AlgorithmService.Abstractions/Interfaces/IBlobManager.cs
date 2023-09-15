// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using WorkoutData.Abstractions.Models;

namespace WorkoutData.Abstractions.Interfaces
{
    public interface IBlobManager
    {
        Task<bool> AppendDataInBlob(PathValue pathValue, string textToAppend);

        Task<bool> CreateNewBlobInContainer(PathValue pathValue);

        Task<bool> DeleteBlobInContainer(string containerName, string blobName);

        Task<BlobResultSegment> GetAllBlobsInContainer(string containerName);

        Task<string> GetAllDataFromBlob(string containerName, string blobName);

        Task<string> GetOneBlobFileInContainer(string container, string blobName);
    }
}