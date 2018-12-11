using IssueManagementSystem.Models;
using Quartz;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace IssueManagementSystem
{
    public class SendNotificationToLevel2 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
               DateTime current =DateTime.Now;
               var current_issue_list = db.issue_occurrence.Where(x => x.issue_satus == "1").ToList();
                foreach (var items in current_issue_list)
       
                {
                    DateTime issueDate = Convert.ToDateTime(items.issue_date);

                    //int result = DateTime.Compare(current , issueDate);
                    //string relationship;
                    //if (result < 0)
                    //    relationship = "is earlier than";
                    //else if (result == 0)
                    //    relationship = "is the same time as";
                    //else
                    //    relationship = "is later than";
                    TimeSpan span = current.Subtract(issueDate);
                    var DD = span.Days;
                    var MM = span.Minutes;
                    System.Diagnostics.Debug.WriteLine(DD);
                }

            }

        }
    }
}