using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace VideoSite
{
    public class MyHub : Hub
    {
        public void SendStream(Stream stream)
        {
            //推送
            this.Clients.Client("").SendStream(stream);
        }
    }
}