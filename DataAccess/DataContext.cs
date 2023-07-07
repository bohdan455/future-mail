using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<FutureMail> FutureMails { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }

    }
}
