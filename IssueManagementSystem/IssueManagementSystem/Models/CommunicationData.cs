using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueManagementSystem.Models
{
    public class CommunicationData
    {
        private string number;
        private string msg,subject;
        private string emailAddress;
        private int call, email, message;
        private int EmployeeNumber;
        private string callNote;
        private string repetCount;
        private string delay;
        private int issue_occour_id;


        public CommunicationData(string number, string msg, string emailAddress, int email, int call, int message,int EmployeeNumber,string subject, string callNote,string repetCount,string delay,int issue_occour_id)
        {
            this.number = number;
            this.msg = msg;
            this.emailAddress = emailAddress;
            this.email = email;
            this.message = message;
            this.call = call;
            this.EmployeeNumber = EmployeeNumber;
            this.subject = subject;
            this.callNote = callNote;
            this.repetCount = repetCount;
            this.delay = delay;
        }


        public string getNumber() {
            return number;
        }

        public string getMsg() {
            return msg;
        }
        public string getsubject()
        {
            return subject;
        }
        public string getEmailAddress()
        {
            return emailAddress;
        }
        public int getCall()
        {
        return call;
        }
        public int getEmail()
        {
            return email;
        }
        public int getMessage()
        {
            return message;
        }
        public int getEmployeeNumber()
        {
            return EmployeeNumber;
        }
        public int getissue_occour_id() { return issue_occour_id; }
        public string getcallNote(){ return callNote; }
        public string getrepetCount() { return repetCount; }
        public string getdelay() { return delay; }

    }
}