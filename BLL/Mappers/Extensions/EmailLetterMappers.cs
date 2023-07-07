using BLL.Dto;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.Extensions
{
    public static class EmailLetterMappers
    {
        public static FutureMail ToFutureMail(this EmailLetterDto emailLetter,Email email)
        {
            return new FutureMail
            {
                Email = email,
                DateOfReceiving = emailLetter.DateOfReceiving,
                Text = emailLetter.Text
            };
        }
    }
}
