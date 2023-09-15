//// -------------------------------------------------------------------------------------------------
//// Copyright (c) Bound Technologies AB. All rights reserved.
//// -------------------------------------------------------------------------------------------------

//using Bound.AlgorithmService.EventBus;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.Threading.Tasks;
//using WorkoutData.Abstractions.Interfaces;

//namespace WorkoutData.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BlobContainerController : Controller
//    {
//        private readonly IUserEventBusHandler userEventBusHandler;

//        public IBlobContainerManager AzureBlobStorageService { get; }

//        public BlobContainerController(IBlobContainerManager azureBlobStorageService, IUserEventBusHandler userEventBusHandler)
//        {
//            this.AzureBlobStorageService = azureBlobStorageService;
//            this.userEventBusHandler = userEventBusHandler;
//        }

//        /// <summary>
//        /// Deletes a specified container.
//        /// </summary>
//        /// <param name="containerName">The container name of the Container to delete.</param>
//        /// <returns>Return the status of delete.</returns>
//        [ProducesResponseType((int)HttpStatusCode.NoContent)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpDelete("{containerName}")]
//        public async Task<IActionResult> DeleteAsync(string containerName)
//        {
//            return this.Ok(await this.AzureBlobStorageService.DeleteContainer(containerName));
//        }

//        /// <summary>
//        /// Returns a All BlobContainers.
//        /// </summary>
//        /// <returns>Return all blob containers.</returns>
//        [ProducesResponseType((int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpGet]
//        public async Task<IActionResult> GetAllAsync()
//        {
//            await userEventBusHandler.SendMessageAsync("Put a message on servicebus");

//            return this.Ok(await this.AzureBlobStorageService.GetAllBlobContainers());
//        }

//        /// <summary>
//        /// Creates a new new BlobContainer.
//        /// </summary>
//        /// <returns>The status of creation of the blobcontainer.</returns>
//        [ProducesResponseType((int)HttpStatusCode.Created)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] string blobContainerName)
//        {
//            return this.Ok(await this.AzureBlobStorageService.CreateBlobContainer(blobContainerName));
//        }
//    }
//}