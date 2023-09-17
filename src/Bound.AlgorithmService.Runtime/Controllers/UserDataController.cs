// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using WorkoutData.Abstractions.Interfaces;
using WorkoutData.Runtime;

namespace WorkoutData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : Controller
    {
        public IBlobManager BlobsManager { get; }

        public UserDataController(IBlobManager blobsManager)
        {
            this.BlobsManager = blobsManager;
        }

        /// <summary>
        /// Returns the data from a specific blob.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="date">The date when the user excersised.</param>
        /// <param name="machine">The name of the exersice machine.</param>
        /// <param name="time">The time of the pass.</param>
        /// <returns>The excersice information of the pass.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("userdata")]
        public async Task<IActionResult> GetAllUserData(string username, string date, string machine, string time)
        {
            var fullName = $"{username}/{date}/{machine}/{time}.txt";
            string blobData = await this.BlobsManager.GetAllDataFromBlob("users", fullName);
            return this.Ok(blobData);
        }
    }
}

/*
example:

https://localhost:5001/api/Blobs/userdata?username=testuser&date=2021-12-08&machine=rygg&time=15%3A30



or in swagger:

userdata: testuser
date: 2012-08-12
machine: rygg
time: 15:30

*/