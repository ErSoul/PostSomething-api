using PostSomething_api.Models;

namespace PostSomething_api.DTO
{
    public class User(ApiUser userEntity)
    {
        public Guid Id { get; set; } = new Guid(userEntity.Id);
        public string? UserName { get; set; } = userEntity.UserName;
        public string Email { get; set; } = userEntity.Email!;
        public string? Address { get; set; } = userEntity.Address;
    }
}