using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IssueManagementSystem.Startup))]

namespace IssueManagementSystem
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            app.MapSignalR();
        }
    }
}
