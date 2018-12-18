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
        
        static readonly string connString = ConfigurationManager.ConnectionStrings["issue_management_systemEntities2"].ToString();

  
        // SqlDependency.Start(@connectionString.ToString());
        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;

        public static FLINTEC_Item_dbContext FLINTEC_Item_dbContext { get; private set; }

        public static string GetIssue()
        {
            try
            {

                var messages = new List<issue_occurrence>();
                using (var connection = new SqlConnection(connString))
                {
                   
                    connection.Open();
                    //// Sanjay : Alwasys use "dbo" prefix of database to trigger change event
                    using (command = new SqlCommand(@"SELECT [issue_occurrence_id],[issue_date],[description],[machine_machine_id],[material_id],[line_line_id],[issue_issue_ID],[responsible_person_emp_id],[issue_satus],[location],[responsible_person_confirm_status],[responsible_person_confirm_feedback],[solved_date],[commented_date] FROM [dbo].[issue_occurrence]", connection))
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

                            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                            {
                                int enpId = (int)reader["responsible_person_emp_id"];

                                using (BigRedEntities BE = new BigRedEntities())
                                {
                                  var  userInfo = BE.tbl_PPA_User.Where(x => x.EmployeeNumber == enpId).FirstOrDefault();


                                    var material_id = reader["material_id"] != DBNull.Value ? (string)reader["material_id"] : "";
                                    string matirialName = null;
                                    if (material_id != "")
                                    {
                                        using (FLINTEC_Item_dbContext context = new FLINTEC_Item_dbContext())
                                        {
                                            var matirialInfo = context.FLINTEC_Items.Where(x => x.No_ == material_id).FirstOrDefault();
                                            matirialName = matirialInfo.Search_Description;

                                        }
                                    }

                                    messages.Add(item: new issue_occurrence
                                    {
                                        issue_occurrence_id = (int)reader["issue_occurrence_id"],
                                        issueDate = reader["issue_date"].ToString(),
                                        description = reader["description"] != DBNull.Value ? (string)reader["description"] : "",
                                        matirial = material_id+" - "+ matirialName,
                                        machine_machine_id = reader["machine_machine_id"] != DBNull.Value ? (string)reader["machine_machine_id"] : "",
                                        line_line_id = (int)reader["line_line_id"],
                                        issue_issue_ID = (int)reader["issue_issue_ID"],
                                        responsible_person_emp_id = (int)reader["responsible_person_emp_id"],
                                        responsible_person_confirm_status = (int)reader["responsible_person_confirm_status"],
                                        responsible_person_confirm_feedback = reader["responsible_person_confirm_feedback"] != DBNull.Value ? (string)reader["responsible_person_confirm_feedback"] : "",
                                        location = reader["location"] != DBNull.Value ? (string)reader["location"] : "",
                                        issue_satus = reader["issue_satus"] != DBNull.Value ? (string)reader["issue_satus"] : "",
                                        solvedDate = reader["solved_date"].ToString(),
                                        commentedDate = reader["commented_date"].ToString(),
                                        responciblepersonName = userInfo.Name
                                    });
                                }
                            }
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