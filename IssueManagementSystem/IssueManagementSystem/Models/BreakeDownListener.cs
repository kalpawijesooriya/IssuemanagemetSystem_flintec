using IssueManagementSystem.Models;
using IssueManagementSystem.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace IssueManagementSystem.Models
{

    namespace IssueManagementSystem.Models
    {
        public static class BreakeDownService
        {

            static readonly string connString = ConfigurationManager.ConnectionStrings["BigRedEntities2"].ToString();


            // SqlDependency.Start(@connectionString.ToString());
            internal static SqlCommand command = null;
            internal static SqlDependency dependency = null;

            public static FLINTEC_Item_dbContext FLINTEC_Item_dbContext { get; private set; }

            public static string GetBreakedown()
            {
                try
                {

                    var messages = new List<tbl_Maintenance_MachineBreakdown>();
                    using (var connection = new SqlConnection(connString))
                    {

                        connection.Open();
                        //// Sanjay : Alwasys use "dbo" prefix of database to trigger change event
                        ///SELECT TOP 5 * FROM tbl_Maintenance_MachineBreakdown ORDER BY [Date] DESC
                        using (command = new SqlCommand(@"SELECT [NotificationId],[Status],[Message],[Type],[EmployeeNumber],[Date] FROM [dbo].[tbl_Notifications]", connection))
                        {
                            command.Notification = null;

                            if (dependency == null)
                            {
                                dependency = new SqlDependency(command);
                                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                            }

                            if (connection.State == ConnectionState.Closed)
                                connection.Open();

                            var reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                messages.Add(item: new tbl_Maintenance_MachineBreakdown
                                {
                                    BId = (int)reader["BId"],
                                    Area = reader["Area"].ToString(),
                                    MachineID = reader["MachineID"].ToString(),
                                    Urgency = reader["Urgency"] != DBNull.Value ? (string)reader["Urgency"] : "",
                                    Description = reader["Description"].ToString(),
                                    Dates = reader["Date"].ToString(),

                                });

                            }
                        }

                    }
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(messages);
                    return json;

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error :" + ex);
                    return null;
                }


            }

            private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
            {
                if (dependency != null)
                {
                    dependency.OnChange -= dependency_OnChange;
                    dependency = null;
                }
                if (e.Type == SqlNotificationType.Change)
                {
                    IssueHub.SendIssue();
                    System.Diagnostics.Debug.WriteLine("Add new item");

                }
            }

        }
    }
}