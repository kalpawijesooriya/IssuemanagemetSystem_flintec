using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IssueManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
      
        

        protected void Application_Start()


        {

            Database.SetInitializer<IssueManagementSystem.Models.FLINTEC_Item_dbContext>(null);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SqlDependency.Start(@"data source=192.168.1.110;initial catalog=issue_management_system;user id=admin;password=1234;");
           

        }
    }
}
