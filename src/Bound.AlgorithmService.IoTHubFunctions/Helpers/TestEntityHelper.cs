using System;
using WorkoutData.Abstractions.Models;

namespace AlgorithmService.IoTHubFunctions.Helpers
{
    public class TestEntityHelper
    {
        public static BlobPathValue CreateTestDataUser(UserData userData)
        {
            var containerName = "users";
            var blobPath = $"testUser/{DateTime.Now.ToShortDateString()}/rygg/";
            var blobName = $"{DateTime.Now.ToShortTimeString()}.txt";

            var fullBlobName = blobPath + blobName;

            var testDataUser = new BlobPathValue
            {
                ContainerName = containerName,
                BlobName = fullBlobName
            };

            return testDataUser;
        }
    }
}




/*{
  "Name": "ut elit erat. dolor nibh",
  "Reps": "tincidunt ut aliquam lorem.",
  "Id": "tincidunt sed.",
  "Date": "2009-12-25T00:00:00"
}*/