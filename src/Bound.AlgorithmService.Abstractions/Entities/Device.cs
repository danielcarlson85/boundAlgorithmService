using AlgorithmService.Abstractions.Entities;

namespace AlgorithmService.IoTHubFunctions.Entities
{
    public class Device:BaseEntity
    {
        public string DeviceName { get; set; }
    }
}
