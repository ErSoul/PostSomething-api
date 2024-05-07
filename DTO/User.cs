using PostSomething_api.Models;

namespace PostSomething_api.DTO
{
    public class User
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public User(ApiUser userEntity)
        {
            Id = new Guid(userEntity.Id);
            UserName = userEntity.UserName;
            Email = userEntity.Email!;
            Address = userEntity.Address;
        }
    }
}
