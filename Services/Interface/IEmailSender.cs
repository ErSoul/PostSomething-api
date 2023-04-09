namespace PostSomething_api.Services.Interface
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
