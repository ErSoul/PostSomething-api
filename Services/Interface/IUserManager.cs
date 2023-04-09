using Microsoft.AspNetCore.Identity;
using PostSomething_auth.Models;

namespace PostSomething_auth.Services.Interface
{
    public interface IUserManager<TUser> where TUser : class
    {
        Task<IdentityResult> CreateAsync(TUser user, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(TUser user);
        Task<IdentityResult> ConfirmEmailAsync(ApiUser user, string token);
        Task<TUser?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(TUser user, string password);
        Task<ApiUser?> FindByIdAsync(string id);
    }
}
