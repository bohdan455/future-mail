using BLL.Services.Interfaces;
using BLL.Services.Realizations;

namespace Api.BackgroundServices
{
    public class DateChecker : BackgroundService
    {
        private readonly ILogger<DateChecker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public DateChecker(ILogger<DateChecker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await using (var scope = _scopeFactory.CreateAsyncScope())
                {
                    var futureMailService = scope.ServiceProvider.GetService<IFutureMailService>();
                    await futureMailService!.CheckMailsDateAsync();
                }
                    
                _logger.LogInformation("Checks was completed");
                await Task.Delay(1000,stoppingToken);
            }
        }
    }
}
