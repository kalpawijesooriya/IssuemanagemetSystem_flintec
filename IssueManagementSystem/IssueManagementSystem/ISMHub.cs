using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace IssueManagementSystem
{
	public class ISMHUB:Hub
	{
        public void Announce(String message)
        {
            Clients.All.Announce(message);  
        }
	}
}