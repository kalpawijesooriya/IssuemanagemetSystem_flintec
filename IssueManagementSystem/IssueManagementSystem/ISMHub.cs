using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueManagementSystem.Models;
using Microsoft.AspNet.SignalR;

namespace IssueManagementSystem
{
    public class ISMHUB : Hub
    {
        public void Announce()
        {

            Clients.All.Announce();

        }
    }
}