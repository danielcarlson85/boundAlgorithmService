using Bound.AlgorithmService.Abstractions.Models;
using Bound.AlgorithmService.Abstractions.Models.AzureADB2C.User;
using System.Threading.Tasks;

namespace Bound.AlgorithmService.Abstractions.Interfaces
{
    public interface IADTokenService
    {
        Task<ADUserResponse> LoginAsync(LoginCredentials loginCredentials);
        Task<string> GetRefreshTokenAsync(string refreshToken);
    }
}