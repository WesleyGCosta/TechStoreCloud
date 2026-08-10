using Api.Models.Response;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AccessResponse?> AuthenticateAsync(string username, string password);
    }
}
