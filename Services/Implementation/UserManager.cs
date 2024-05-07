using Microsoft.AspNetCore.Identity;
using PostSomething_api.Models;
using PostSomething_api.Services.Interface;
using System.Diagnostics.CodeAnalysis;

namespace PostSomething_api.Services.Implementation
{
    [ExcludeFromCodeCoverage]
    public class UserManager : IUserManager<ApiUser>
    {
        private readonly UserManager<ApiUser> _userManager;

        public UserManager(UserManager<ApiUser> userManager)
        {
            _userManager = userManager;
        }
        public Task<IdentityResult> CreateAsync(ApiUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }
        public Task<string> GenerateEmailConfirmationTokenAsync(ApiUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public Task<IdentityResult> ConfirmEmailAsync(ApiUser user, string token)
        {
            return _userManager.ConfirmEmailAsync(user, token);
        }
        public Task<ApiUser?> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }
        public Task<bool> CheckPasswordAsync(ApiUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }
        public Task<ApiUser?> FindByIdAsync(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

    }
}
