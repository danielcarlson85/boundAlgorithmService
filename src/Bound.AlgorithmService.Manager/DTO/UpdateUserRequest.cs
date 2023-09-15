using Microsoft.Graph;

namespace Bound.AlgorithmService.Managers.DTO
{
    public class UpdateUserRequest : UserDTOBase
    {
        public PasswordProfile PasswordProfile { get; set; }
    }
}