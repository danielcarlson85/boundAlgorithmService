using System;
using static Bound.AlgorithmService.IoTHubFunctions.BoundDeviceFunctions;

namespace AlgorithmService.IoTHubFunctions.Helpers
{
    public class BlobHelpers
    {
        private static string CreateBlobName(UserData dataValue)
        {
            return $"{DateTime.Now.ToShortTimeString()}.txt";
        }

        public static string CreateBlobPath(string deviceName, UserData userData)
        {
            var blobName = CreateBlobName(userData);

            //var fullBlobPath = $"{userData.Name}/{userData.Date}/{deviceName}/{blobName}";
            return null;//fullBlobPath;
        }
    }
}
