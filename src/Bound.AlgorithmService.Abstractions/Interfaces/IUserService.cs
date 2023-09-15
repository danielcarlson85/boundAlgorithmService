using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bound.AlgorithmService.Abstractions.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User user, string password, string role);
        Task DeleteUserAsync(string objectId);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(string mail);
        Task UpdateUserAsync(User user, string role);
    }
}