namespace BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendFutureMailAsync(string email, string text);
        Task SendVerificationLetter(string email, string verificationLink);
        Task<bool> VerifyAsync(Guid code);
    }
}