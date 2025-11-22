using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class SupportChatHub : Hub
{
    public async Task SendMessageToSupport(string message)
    {
        var userId = Context.UserIdentifier;
        var userName = Context.User.Identity.Name;

        await Clients.GroupExcept("SupportTeam", Context.ConnectionId).SendAsync("ReceiveMessage", userId, userName, message);

        await Clients.Caller.SendAsync("ReceiveMessage", userId, userName, message);
    }

    [Authorize(Roles = "Support")]
    public async Task ReplyToCustomer(string customerUserId, string message)
    {
        var supportAgentName = Context.User.Identity.Name;

        string customerGroup = $"user-{customerUserId}";

        await Clients.Group(customerGroup).SendAsync("ReceiveMessage", Context.UserIdentifier, supportAgentName, message);

        await Clients.Caller.SendAsync("ReceiveMessage", Context.UserIdentifier, supportAgentName, message);     
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        string userGroup = $"user-{userId}";

        await Groups.AddToGroupAsync(Context.ConnectionId, userGroup);

        if (Context.User.IsInRole("Support"))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SupportTeam");
        }

        await base.OnConnectedAsync();
    }
}