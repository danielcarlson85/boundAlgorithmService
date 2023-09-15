using AlgorithmService.IoTHubFunctions.Entities;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using WorkoutData.Abstractions.Models;

namespace AlgorithmService.IoTHubFunctions.Helpers
{
    public class DeviceHelpers
    {
        public static Device ExtractDeviceData(EventData message)
        {
            var device = new Device
            {
                DeviceName = message.SystemProperties["iothub-connection-device-id"].ToString()
            };

            return device;
        }

        public static DeviceData ExtractUserDataFromDevice(EventData message)
        {
            var messageText = Encoding.UTF8.GetString(message.Body.Array);
            var userData = JsonConvert.DeserializeObject<DeviceData>(messageText);
            userData.TrainingData = messageText;

            return userData;
        }
    }

    public class DeviceData
    {
        public string MachineName { get; set; }
        public string ObjectId { get; set; }
        public string TrainingData { get; set; }
    }

    public class TrainingData
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
    }
}
