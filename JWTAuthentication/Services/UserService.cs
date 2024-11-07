using JWTAuthentication.Data;
using JWTAuthentication.Entities;
using JWTAuthentication.Models.Request;
using JWTAuthentication.Models.Response;
using JWTAuthentication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservationServiceDAL.Exceptions;
using System.IdentityModel.Tokens.Jwt;

namespace JWTAuthentication.Services
{
    public class UserService : IUserService
    {
        protected readonly JWTAuthenticationContext _dbContext;
        protected readonly IJwtService _jwtSevice;
        protected readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(JWTAuthenticationContext dbContext, UserManager<User> userManager, IJwtService jwtSevice, RoleManager<IdentityRole> roleManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._jwtSevice = jwtSevice;
            this._roleManager = roleManager;
        }

        public async Task<string> RegistrateAsync(RegistrationRequest request)
        {
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                    _dbContext.SaveChanges();
                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email} is already registered.";
            }
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username)
                        ?? throw new EntityNotFoundException(
                            $"{nameof(User)} with user name {request.Username} not found.");

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new EntityNotFoundException("Incorrect username or password.");
            }

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(_jwtSevice.GenerateJwtToken(user));
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var refreshToken = new RefreshToken();

            if (user.RefreshTokens.Any(a => a.IsActive))
            {
                refreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
            }
            else
            {
                refreshToken = _jwtSevice.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                _dbContext.Update(user);
                _dbContext.SaveChanges();
            }

            return new AuthenticationResponse(user, rolesList.ToList(), jwtToken, refreshToken);
        }

        public async Task<string> AddRoleAsync(AddRoleRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {request.Email}.";
            }
            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var roleExists = Enum.GetNames(typeof(Roles)).Any(x => x.ToLower() == request.Role.ToLower());
                if (roleExists)
                {
                    var validRole = Enum.GetValues(typeof(Roles)).Cast<Roles>().Where(x => x.ToString().ToLower() == request.Role.ToLower()).FirstOrDefault();
                    await _userManager.AddToRoleAsync(user, validRole.ToString());
                    return $"Added {request.Role} to user {request.Email}.";
                }
                return $"Role {request.Role} not found.";
            }
            return $"Incorrect Credentials for user {user.Email}.";

        }


        public async Task<IEnumerable<User>> GetAllAsync() {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return _dbContext.Users.Find(id)
                ?? throw new EntityNotFoundException(
                    GetEntityNotFoundErrorMessage(id));
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync(string token)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token)) 
                        ?? throw new EntityNotFoundException(
                            $"{nameof(User)} with token {token} not found.");

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            var newRefreshToken = new RefreshToken();

            if (refreshToken.IsActive)
            {
                refreshToken.Revoked = DateTime.UtcNow;

                newRefreshToken = _jwtSevice.GenerateRefreshToken();
                user.RefreshTokens.Add(newRefreshToken);
                _dbContext.Update(user);
                _dbContext.SaveChanges();
            }

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(_jwtSevice.GenerateJwtToken(user));
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            return new AuthenticationResponse(user, rolesList.ToList(), jwtToken, newRefreshToken);
        }

        public bool RevokeTokenAsync(string token)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (user == null || !refreshToken.IsActive)
            {
                return false;
            }
            refreshToken.Revoked = DateTime.UtcNow;
            _dbContext.Update(user);
            _dbContext.SaveChanges();
            return true;
        }

        protected static string GetEntityNotFoundErrorMessage(string id) =>
           $"{typeof(User).Name} with id {id} not found.";
    }
}
