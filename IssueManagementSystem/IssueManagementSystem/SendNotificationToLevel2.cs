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

                }

            }

        }
    }
}