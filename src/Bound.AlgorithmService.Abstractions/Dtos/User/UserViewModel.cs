using Newtonsoft.Json;

namespace AlgorithmService.Abstractions.Dtos.User
{
    public class UserViewModel : DtoBase
    {
        [JsonProperty("is_logged_in")]
        public bool? IsLoggedIn { get; set; }

        [JsonProperty("ios_device_token")]
        public string IosDeviceToken { get; set; }

        [JsonProperty("apn_token")]
        public string ApnToken { get; set; }
    }
}
