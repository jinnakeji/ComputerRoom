using Coldairarrow.Api.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Hubs
{
    public class RemoteHub : Hub
    {
        private readonly CountService _countService;

        public RemoteHub(CountService countService)
        {
            _countService = countService;
        }

        public async Task GetLatestCount(string random)
        {
            int count;
            do
            {
                count = _countService.GetLatestCount();
                Thread.Sleep(1000);
                await Clients.All.SendAsync("ReceiveUpdate", count);
            } while (count < 20);
            await Clients.All.SendAsync("结束");
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("someFunc", new { });
            await Clients.AllExcept(connectionId).SendAsync("someFunc");
        }

        /// <summary>
        /// 客户端调用方法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SendMessage(ChatMessageInfo data)
        {
            await Clients.All.SendAsync("DeviceData", data);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToGroup(string message)
        {
            return Clients.Group("SignalR Users").SendAsync("ReceiveMessage", message);
        }
    }
}