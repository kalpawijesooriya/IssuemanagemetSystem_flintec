﻿using IssueManagementSystem.Controllers;
using IssueManagementSystem.Models;
using Quartz;
using System;
using System.Collections;
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
                Queue numberList = new Queue();
                Communication com = new Communication();
                foreach (var items in current_issue_list)
                {
                    DateTime issueDate = Convert.ToDateTime(items.issue_date);
                    TimeSpan span = current.Subtract(issueDate);
                    var DD = span.Days;
                    var MM = span.Minutes;
                    var HH = span.Hours;
                    double timeInHours = (DD * 24) + HH + (DD / 60.0);

                    var communicationInfo = db.issue_line_person.Where(x => x.line_id == items.line_line_id && x.issue_id == items.issue_issue_ID && x.person_level==2).OrderBy(x => x.levelOfResponsibility).ToList();
                    foreach (var i in communicationInfo)
                    {
                       int responceTime = Int32.Parse(i.sendAlertAfter) ;
                        if (timeInHours >= responceTime)
                        {
                           
                            var lineInfo=db.lines.Where(x=>x.line_id== items.line_line_id).FirstOrDefault();
                           var issueInfo=db.issues.Where(x=>x.issue_id== items.issue_issue_ID).FirstOrDefault();
                           
                            using (BigRedEntities BR = new BigRedEntities())
                            {
                                var responsiblePersonInfo = BR.tbl_PPA_User.Where(y => y.EmployeeNumber == items.responsible_person_emp_id).FirstOrDefault();

                                string msg = "The " + issueInfo.issue1 + " issue occurred on " + issueDate + " in " + lineInfo.line_name + " Line not solved yet. Responsible person is "+ responsiblePersonInfo.Name+" Thank You.";
                                string callNote = lineInfo.line_name + " Line "+ issueInfo.issue1 + " not solved yet";
                                var personInfo = BR.tbl_PPA_User.Where(y => y.EmployeeNumber == i.EmployeeNumber).FirstOrDefault();
                                CommunicationData cd = new CommunicationData(personInfo.Phone, msg, personInfo.EMail, i.email, i.call, i.message, personInfo.EmployeeNumber, "Unsolved Issue", callNote, "0", "0");
                                numberList.Enqueue(cd);

                            }
                        }
                    }
                   
                    System.Diagnostics.Debug.WriteLine(DD);
                }
                if (numberList != null) { com.setCD(numberList); }
              
            }

        }
    }
}