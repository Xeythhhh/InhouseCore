
using Microsoft.AspNetCore.SignalR;

namespace Api;

public sealed class TestBackgroundService(
    ILogger<TestBackgroundService> logger,
    IHubContext<TestHub, ITestHub> hubContext) :
    BackgroundService
{
    private static readonly TimeSpan Period = TimeSpan.FromSeconds(7);
    private static int _count;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(Period);

        while (!stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            _count++;
            logger.LogDebug("Incrementing count in TestBackgroundService. Value: {@Count}", _count);
            await hubContext.Clients.All.ReceiveNotification(_count.ToString());
        }
    }
}
