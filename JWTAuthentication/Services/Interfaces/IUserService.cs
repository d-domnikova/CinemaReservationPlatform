using JWTAuthentication.Entities;
using JWTAuthentication.Models.Request;
using JWTAuthentication.Models.Response;

namespace JWTAuthentication.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> RegistrateAsync(RegistrationRequest request);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> AddRoleAsync(AddRoleRequest request);

        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<AuthenticationResponse> RefreshTokenAsync(string token);
        bool RevokeTokenAsync(string token);
    }
}
