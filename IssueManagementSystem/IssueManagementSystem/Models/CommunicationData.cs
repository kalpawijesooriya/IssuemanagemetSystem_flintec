﻿using System;
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



        public CommunicationData(string number, string msg, string emailAddress, int email, int call, int message,int EmployeeNumber,string subject)
        {
            this.number = number;
            this.msg = msg;
            this.emailAddress = emailAddress;
            this.email = email;
            this.message = message;
            this.call = call;
            this.EmployeeNumber = EmployeeNumber;
            this.subject = subject;
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

    }
}