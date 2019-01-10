using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueManagementSystem.Models;
using Microsoft.AspNet.SignalR;

namespace IssueManagementSystem
{
    public class IsmHub : Hub
    {
        public void Announce()
        {

            Clients.All.Announce();

        }
        public static void Send()
        {

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<IsmHub>();
            context.Clients.All.displayStatus();
        }
    }
}