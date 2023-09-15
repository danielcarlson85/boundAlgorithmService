using System.ComponentModel.DataAnnotations;

namespace AlgorithmService.Abstractions.Dtos.User
{
    public class UserBase : DtoBase
    {
        [Required]
        public string Role { get; set; }

    }
}