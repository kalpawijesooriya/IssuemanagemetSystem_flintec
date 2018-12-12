using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueManagementSystem.Hubs
{
    /// <summary>
    /// /Create hub from whrere all clients will be communicated
    /// </summary>
    //[HubName("myHub")]
    public class MyHub : Hub
    {
        public static void Send()
        {

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            context.Clients.All.displayStatus();
        }

    }
}