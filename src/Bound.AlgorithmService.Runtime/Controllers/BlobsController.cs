//// -------------------------------------------------------------------------------------------------
//// Copyright (c) Bound Technologies AB. All rights reserved.
//// -------------------------------------------------------------------------------------------------

//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.WindowsAzure.Storage.Blob;
//using WorkoutData.Abstractions.Interfaces;
//using WorkoutData.Abstractions.Models;

//namespace WorkoutData.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BlobsController : Controller
//    {
//        public IBlobManager BlobsManager { get; }

//        public BlobsController(IBlobManager blobsManager)
//        {
//            this.BlobsManager = blobsManager;
//        }

//        /// <summary>
//        /// Appends data to a specific blob file.
//        /// </summary>
//        /// <param name="pathValue">The path to where the blob is located.</param>
//        /// <param name="textToAppend">The text to be appended in the blob.</param>
//        /// <returns>The status of the return.</returns>
//        [ProducesResponseType((int)HttpStatusCode.NoContent)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpPost("file/{textToAppend}")]
//        public async Task<IActionResult> AppendDataInBlob([FromBody] PathValue pathValue, string textToAppend)
//        {
//            await this.BlobsManager.AppendDataInBlob(pathValue, textToAppend);
//            return this.Ok();
//        }

//        /// <summary>
//        /// Gets one specific blob file in container.
//        /// </summary>
//        /// <param name="containerName">The container name.</param>
//        /// <param name="blobName">The blob name.</param>
//        /// <returns>The information of the blob.</returns>
//        [ProducesResponseType((int)HttpStatusCode.NoContent)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpGet("{containerName}/{blobName}")]
//        public async Task<IActionResult> GetOneBlobFileInContainer(string containerName, string blobName)
//        {
//            blobName = blobName.Replace("%2F", "/");
//            return this.Ok(await this.BlobsManager.GetOneBlobFileInContainer(containerName, blobName));
//        }

//        /// <summary>
//        /// Returns the data from a specific blob.
//        /// </summary>
//        /// <param name="containerName">The container name.</param>
//        /// <param name="blobName">The blob name.</param>
//        /// <returns>The information in the blob.</returns>
//        [ProducesResponseType((int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpGet("file/{containerName}/{blobName}")]
//        public async Task<IActionResult> GetAllDataFromBlob(string containerName, string blobName)
//        {
//            blobName = blobName.Replace("%2F", "/");

//            string blobData = await this.BlobsManager.GetAllDataFromBlob(containerName, blobName);
//            return this.Ok(blobData);
//        }

//        /// <summary>
//        /// Returns all blobs in one container.
//        /// </summary>
//        /// <param name="containerName">The container name.</param>
//        /// <returns>All blobs found in container.</returns>
//        [ProducesResponseType((int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpGet("{containerName}")]
//        public async Task<IActionResult> GetAllBlobsInContainer(string containerName)
//        {
//            BlobResultSegment blobData = await this.BlobsManager.GetAllBlobsInContainer(containerName);
//            return this.Ok(blobData);
//        }

//        /// <summary>
//        /// Creates a new Blob in a container.
//        /// </summary>
//        /// <param name="pathValue">The path to where the blob is located.</param>
//        /// <returns>The status of the creation of the blob.</returns>
//        [ProducesResponseType((int)HttpStatusCode.Created)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpPost]
//        public async Task<IActionResult> CreateNewBlobInContainer([FromBody] PathValue pathValue)
//        {
//            await this.BlobsManager.CreateNewBlobInContainer(pathValue);
//            return this.Ok();
//        }
//    }
//}