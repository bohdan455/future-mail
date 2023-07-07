using Azure.Core;
using BLL.Dto;
using BLL.Mappers.Extensions;
using BLL.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BLL.Services.Realizations
{
    public class FutureMailService : IFutureMailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly HttpContext _context;
        private string GenerateVerificationLink(Guid code)
        {
            var request = _context.Request;
            var location = new Uri($"{request.Scheme}://{request.Host}/api/Email/{code}");

            var url = location.AbsoluteUri;
            return url;
        }
        public FutureMailService(IUnitOfWork unitOfWork,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _context = httpContextAccessor.HttpContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if letter was planed and false if email wasn't verified</returns>
        public async Task<bool> SendAsync(EmailLetterDto letterDto)
        {
            var email = _unitOfWork.Repository<Email>().GetFirstByCondition(e => e.EmailAddress == letterDto.EmailAddress);
            if (email == null)
            {
                var newEmail = new Email
                {
                    EmailAddress = letterDto.EmailAddress,
                };

                var newEmailVerification = new EmailVerification
                {
                    Email = newEmail
                };

                _unitOfWork.Repository<Email>().Insert(newEmail);
                _unitOfWork.Repository<EmailVerification>().Insert(newEmailVerification);

                var link = GenerateVerificationLink(newEmailVerification.Id);
                await _emailService.SendVerificationLetter(letterDto.EmailAddress, link);
                await _unitOfWork.CommitAsync();

                return false;
            }

            if (!email.IsVerified)
            {
                return false;
            }

            _unitOfWork.Repository<FutureMail>().Insert(letterDto.ToFutureMail(email));
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task CheckMailsDateAsync()
        {
            var letters = _unitOfWork.Repository<FutureMail>().GetAll(includes: fm => fm.Include(e => e.Email));
            foreach(var letter in letters)
            {
                if(letter.DateOfReceiving < DateTime.UtcNow)
                {
                    await _emailService.SendFutureMailAsync(letter.Email.EmailAddress, letter.Text);
                    _unitOfWork.Repository<FutureMail>().Delete(letter);
                    await _unitOfWork.CommitAsync();
                }
            }
        }
    }
}
