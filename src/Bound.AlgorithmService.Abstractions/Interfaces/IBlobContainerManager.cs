// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkoutData.Abstractions.Interfaces
{
    public interface IBlobContainerManager
    {
        Task<bool> CreateBlobContainer(string blobContainerName);

        Task<bool> DeleteAllContainers();

        Task<bool> DeleteContainer(string id);

        Task DownloadFileFromContainer();

        Task<List<string>> GetAllBlobContainers();

        CloudBlobContainer GetOneBlobContainer(string blobContainerName);

        Task UploadFileToContainer();
    }
}