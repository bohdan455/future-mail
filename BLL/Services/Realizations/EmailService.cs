using BLL.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Realizations
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IUnitOfWork unitOfWork,ILogger<EmailService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> VerifyAsync(Guid code)
        {
            var emailToVerify = _unitOfWork.Repository<EmailVerification>()
                .GetFirstByCondition(e => e.Id == code, includes: er => er.Include(e => e.Email));
            if (emailToVerify == null)
            {
                return false;
            }

            emailToVerify.Email.IsVerified = true;
            _unitOfWork.Repository<EmailVerification>().Delete(emailToVerify);
            await _unitOfWork.CommitAsync();
            return true;
        }
        
        public async Task SendVerificationLetter(string email,string verificationLink)
        {
            var client = GenerateSmtpClient();

            await client.SendMailAsync(EmailSettings.Email,email,"Verification letter", $"Future mail - Verification Url {verificationLink}");
            _logger.LogInformation("Verification email was sent to {0}", email);


        }
        public async Task SendFutureMailAsync(string email, string text)
        {
            var client = GenerateSmtpClient();
            await client.SendMailAsync(EmailSettings.Email, email, "Email from future", $"Future email: \n{text}");
            _logger.LogInformation("Email in future was sent to {0}", email);
        }
        private SmtpClient GenerateSmtpClient()
        {
            return new SmtpClient(EmailSettings.Host)
            {
                Port = EmailSettings.Port,
                Credentials = new NetworkCredential(EmailSettings.Username, EmailSettings.Password),
                EnableSsl = true
            };
        }

    }
}
