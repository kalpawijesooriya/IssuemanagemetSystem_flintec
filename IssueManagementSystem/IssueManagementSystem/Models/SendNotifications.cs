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
    public static class NotificaionService
    {
        static readonly string connString = ConfigurationManager.ConnectionStrings["issue_management_systemEntities2"].ToString();

        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;


        /// <summary>
        /// Gets the notifications.
        /// </summary>
        /// <returns></returns>
        public static string GetNotification()
        {
            try
            {

                var messages = new List<tbl_Notifications>();
                using (var connection = new SqlConnection(connString))
                {

                    connection.Open();
                    //// Sanjay : Alwasys use "dbo" prefix of database to trigger change event SELECT TOP 200 * FROM [dbo].[tbl_Maintenance_MachineBreakdown] ORDER BY [Date] DESC
                    using (command = new SqlCommand(@"SELECT TOP 200 * FROM [dbo].[tbl_Notifications] ORDER BY [Date] DESC", connection))
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
                           //System.Diagnostics.Debug.WriteLine("Date    ##############"+(string)reader["Date"]);
                            messages.Add(item: new tbl_Notifications
                            {
                                NotificationId = (int)reader["NotificationId"],
                                Status =(int)reader["Status"],
                                Message = reader["Message"] != DBNull.Value ? (string)reader["Message"] : "",
                                Type = reader["Type"] != DBNull.Value ? (string)reader["Type"] : "",
                                EmployeeNumber = (int)reader["EmployeeNumber"],
                                Dates = reader["Date"].ToString()


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
                MyHub.Send();
            }
        }

    }
}