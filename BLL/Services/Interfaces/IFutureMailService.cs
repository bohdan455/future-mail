using BLL.Dto;

namespace BLL.Services.Interfaces
{
    public interface IFutureMailService
    {
        Task CheckMailsDateAsync();
        Task<bool> SendAsync(EmailLetterDto letterDto);
    }
}