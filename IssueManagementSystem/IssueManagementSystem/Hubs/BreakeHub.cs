using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IssueManagementSystem.Hubs
{
    public class BreakeHub : Hub
    {
        public static void SendBreakeDown()
        {

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<BreakeHub>();
            context.Clients.All.displayStatus();
        }
    }
}