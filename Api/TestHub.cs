using Microsoft.AspNetCore.SignalR;

namespace Api;

public class TestHub : Hub<ITestHub>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId)
            .ReceiveNotification("TestNotificationSignalR.Connected");

        await base.OnConnectedAsync();
    }
}

public interface ITestHub
{
    Task ReceiveNotification(string message);
}
