using BLL.Services.Interfaces;
using BLL.Services.Realizations;

namespace Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IFutureMailService, FutureMailService>();
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
