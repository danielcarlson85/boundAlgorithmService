using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AlgorithmService.Abstractions.Dtos.User
{
    public class UserRequest : UserBase
    {
        [JsonIgnore]
        public string ObjectId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}