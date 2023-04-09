using System.ComponentModel.DataAnnotations;

namespace PostSomething_auth.Requests
{
    public class RegisterUserBody
    {
        public string? UserName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string ConfirmationPassword { get; set; }
    }
}
