using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace IssueManagementSystem.Hubs
{
    public class IssueHub : Hub
    {
        public static void SendIssue()
        {
           
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<IssueHub>();
            context.Clients.All.displayStatus();
        }
    }
}