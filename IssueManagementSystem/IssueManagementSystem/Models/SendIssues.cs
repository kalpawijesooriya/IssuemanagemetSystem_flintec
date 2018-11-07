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
    public static class IssueService
    {
        static readonly string connString = @"data source=192.168.1.110;initial catalog=issue_management_system;user id=admin;password=1234;";

        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;

        public static string GetIssue()
        {
            try
            {

                var messages = new List<issue_occurrence>();
                using (var connection = new SqlConnection(connString))
                {
                   
                    connection.Open();
                    //// Sanjay : Alwasys use "dbo" prefix of database to trigger change event
                    using (command = new SqlCommand(@"SELECT [issue_occurrence_id],[date_time],[description],[machine_machine_id],[material_id],[line_line_id],[issue_issue_ID],[responsible_person_emp_id],[issue_satus],[location],[responsible_person_confirm_status],[responsible_person_confirm_feedback] FROM [dbo].[issue_occurrence]", connection))
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
                           // System.Diagnostics.Debug.WriteLine("Date    ##############"+reader["date_time"].ToString());
                            messages.Add(item: new issue_occurrence
                            {
                                issue_occurrence_id= (int)reader["issue_occurrence_id"],
                                date = reader["date_time"].ToString(),
                                material_id= reader["material_id"] != DBNull.Value ? (string)reader["material_id"] : "",
                                description = reader["description"] != DBNull.Value ? (string)reader["description"] : "",
                                machine_machine_id = reader["machine_machine_id"] != DBNull.Value ? (string)reader["machine_machine_id"] : "",
                                line_line_id = (int)reader["line_line_id"],
                                issue_issue_ID = (int)reader["issue_issue_ID"],
                                responsible_person_emp_id = (int)reader["responsible_person_emp_id"],
                                responsible_person_confirm_status = (int)reader["responsible_person_confirm_status"],
                                responsible_person_confirm_feedback = reader["responsible_person_confirm_feedback"] != DBNull.Value ? (string)reader["responsible_person_confirm_feedback"] : "",
                                location = reader["location"] != DBNull.Value ? (string)reader["location"] : "",
                                issue_satus = reader["issue_satus"] != DBNull.Value ? (string)reader["issue_satus"] : ""




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