using System.Threading.Tasks;

namespace Bound.AlgorithmService.EventBus
{
    public interface IUserEventBusHandler
    {
        Task SendMessageAsync(string payload);
        Task StartRecieveMessageAsync();
    }
}