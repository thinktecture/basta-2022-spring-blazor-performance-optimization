using BlazorPerformance.Api.Hubs;
using BlazorPerformance.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace BlazorPerformance.Api.BackgroundServices
{
    public class RealTimeSyncService : BackgroundService
    {
        private readonly IHubContext<RealtimeHub> _hubContext;
        private readonly IServiceProvider _serviceProvider;

        public RealTimeSyncService(IHubContext<RealtimeHub> hubContext, IServiceProvider serviceProvider)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var random = new Random();            
            using var scope = _serviceProvider.CreateScope();
            var conferencesService = scope.ServiceProvider.GetService<ConferencesService>();
            if (conferencesService != null)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var conf = await conferencesService.GetRandomConference(stoppingToken);
                    await _hubContext.Clients.All.SendAsync("UpdateCount", conf?.Id ?? 1, random.Next(10, 100));
                    await Task.Delay(2500);
                }
            }
        }
    }
}
