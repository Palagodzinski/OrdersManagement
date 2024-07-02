
namespace OrdersManagement.Application.Interfaces
{
    public interface IAuthorizationService
    {
        Task<string> GetAuthorizationTokenAsync();
    }
}
