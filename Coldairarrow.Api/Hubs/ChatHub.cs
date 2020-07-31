using Coldairarrow.Api.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Hubs
{

    public class ChatHub : Hub
    {
        private readonly CountService _countService;
        public ChatHub(CountService countService)
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
            //for (int i = 0; i < 10; i++)
            //{
            //    data.Message = "后台自动推";
            //    data.UserName = i.ToString();
            //}
            ////服务端返回是调用方法
            //return Clients.All.SendAsync("ReceiveMessage", data);
            int count;
            do
            {

                count = _countService.GetLatestCount();
                for (int i = 0; i < count; i++)
                {
                    data.Message = "后台自动推";
                    data.UserName = i.ToString();
                }
                Thread.Sleep(1000);
                await Clients.All.SendAsync("ReceiveMessage", data);

            } while (count < 20);
            await Clients.All.SendAsync("结束");

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
