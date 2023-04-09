namespace PostSomething_auth.Services.Interface
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
