﻿using IssueManagementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
  
    public class Communication
    {
        static Queue numberList = new Queue();
        static bool gsm_status = true;
        int callCount = 0;
        public Communication()
        {
          
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void setCD(Queue CommunicationList)
        {

            numberList= new Queue (CommunicationList.ToArray());          
            doCommunicate();
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        private  void doCommunicate()
        {

            if (numberList.Count != 0)
            {
                if (gsm_status)
                {
                    CommunicationData communicateData = (CommunicationData)numberList.Dequeue();
                    try
                    {
                        var time = DateTime.Now;
                        string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");
                        var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        gsm_status = false;
                        var msg = communicateData.getMsg();
                        var emailAddress = communicateData.getEmailAddress();
                        var mobileNumber = communicateData.getNumber();
                        int empNo = communicateData.getEmployeeNumber();
                        int issue_occor_id = communicateData.getissue_occour_id();
                        int emailStatus = communicateData.getEmail();
                        int callStatus = communicateData.getCall();
                        int msgStatus = communicateData.getMessage();

                        using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                        {

                            string query = "INSERT INTO tbl_issue_feedback (issue_occurrence_id,EmployeeNumber, call_send, call_answered, sms_send,email_send)VALUES(" + issue_occor_id + "," + empNo + "," + callStatus + ", 0, " + msgStatus + ", " + emailStatus + ");";
                            db.Database.ExecuteSqlCommand(query);
                        }

                        if (emailStatus == 1 && emailAddress != null)
                        {
                            sendMail(emailAddress, msg, communicateData.getsubject());
                            //using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                            //{
                            //    string query = "INSERT INTO tbl_Notifications ([Status],[Message],[Type],[EmployeeNumber],[Date]) VALUES( 1,'" + msg + "','email','" + communicateData.getEmployeeNumber() + "','" + date + "') ";
                            //    db.Database.ExecuteSqlCommand(query);
                            //}                                                 
                        }

                        if (msgStatus == 1 && mobileNumber != null)
                            send_SMS(mobileNumber, msg);

                        if (callStatus == 1 && mobileNumber != null)
                            take_Call(mobileNumber, communicateData.getcallNote(), communicateData.getrepetCount(), communicateData.getdelay(), empNo, issue_occor_id);



                        gsm_status = true;
                        if (numberList.Count != 0)
                        {
                            doCommunicate();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        gsm_status = true;
                    }
                }
                else
                {
                    int milliseconds = 2000;
                    Thread.Sleep(milliseconds);
                    doCommunicate();
                }
            }
        }

        public void lightON(string light, string ip)
        {
            string url = "http://"+ip+"/led.php?on=" + light;
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }

        public void lightOFF(string light, string ip)
        {
            string url = "http://"+ip+"/led.php?off=" + light;
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }
        public void lightONMachineshop(string light, string ip,string group)
        {
            string url = "http://" + ip + "/ledmachineshop.php?on=" + light+"&group="+ group;
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }
        public void lightOFFMachineshop(string light, string ip, string group)
        {
            string url = "http://" + ip + "/ledmachineshop.php?off=" + light + "&group=" + group;
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }
        public void storesbuzzerOn()
        {
            //string url = "http://192.168.40.246/led.php?on=1";
            string url = "http://192.168.137.238/led.php?on=1";
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }
        public void storesbuzzerOff()
        {
            //   string url = "http://192.168.40.246/led.php?off=1";
            string url = "http://192.168.137.238/led.php?off=1";
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }

        public void maintenancesbuzzerOn()
        {
            //  string url1 = "http://192.168.20.240/led.php?on=1";
            // string url2 = "http://192.168.20.240/led.php?on=2";
            string url1 = "http://192.168.137.238/led.php?on=1";
            string url2 = "http://192.168.137.238/led.php?on=2";
            HttpWebRequest webReq1 = (HttpWebRequest)WebRequest.Create(string.Format(url1));
            HttpWebRequest webReq2 = (HttpWebRequest)WebRequest.Create(string.Format(url2));
            webReq1.Method = "GET";
            webReq2.Method = "GET";
            HttpWebResponse webResponse1 = (HttpWebResponse)webReq1.GetResponse();
            HttpWebResponse webResponse2 = (HttpWebResponse)webReq2.GetResponse();
        }

        public void maintenancesbuzzerOff()
        {
            //  string url1 = "http://192.168.20.240/led.php?off=1";
            //  string url2 = "http://192.168.20.240/led.php?off=2";
            string url1 = "http://192.168.137.238/led.php?off=1";
            string url2 = "http://192.168.137.238/led.php?off=2";
            HttpWebRequest webReq1 = (HttpWebRequest)WebRequest.Create(string.Format(url1));
            HttpWebRequest webReq2 = (HttpWebRequest)WebRequest.Create(string.Format(url2));
            webReq1.Method = "GET";
            webReq2.Method = "GET";
            HttpWebResponse webResponse1 = (HttpWebResponse)webReq1.GetResponse();
            HttpWebResponse webResponse2 = (HttpWebResponse)webReq2.GetResponse();
        }

        public void sendMail(string emailAddress, string msg,string subject)
        {
            using (MailMessage mm = new MailMessage("ppts@flintec.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = msg;

                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("ppts@flintec.com", "user1082@#");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    // Debug.WriteLine("This is count : " + count.ToString());

                    
                }
            }
        }

        public  void send_SMS(string number,string message)
        {

            ASCIIEncoding encoding = new ASCIIEncoding();
                   
                    
            string postData = "p=" + number + "&m="+ message;
            byte[] data = encoding.GetBytes(postData);

            WebRequest request = WebRequest.Create("http://192.168.40.241/sms_call.php");

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);


            var cs = sr.ReadToEnd().Trim();
            Debug.WriteLine("SMS Respond: " + cs);

            sr.Close();
            stream.Close();

        }

        public void take_Call(string number, string message ,string repetCount,string delay,int empNo, int issue_occor_id)
        {

            callCount++;
            ASCIIEncoding encoding = new ASCIIEncoding();


            string postData = "p=" + number + "&c=" + message;
            byte[] data = encoding.GetBytes(postData);

            WebRequest request = WebRequest.Create("http://192.168.40.241/sms_call.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            var cs = sr.ReadToEnd().Trim();
            
            var cr=cs.Split(',');
            Debug.WriteLine("Call Respond: " + cr[0]);
            Debug.WriteLine("Call Respond: " + cr[1]);
            int call_answered = 0;
            if (cr[0] == "true")
            {
                call_answered = 1;
            }
                using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {

                string query = "UPDATE tbl_issue_feedback SET call_send=" + callCount + ",call_answered ="+ call_answered + "WHERE issue_occurrence_id=" + issue_occor_id + "AND EmployeeNumber=" + empNo;
                db.Database.ExecuteSqlCommand(query);
            }
            if (cr[0] == "false") {
                if (Int32.Parse(repetCount) > callCount)
                {
                    double delaycall = Int32.Parse(delay);
                    if (Int32.Parse(delay) < 0.39) { delaycall = 0.39; }
                  
                        double waitingTime = delaycall* 60 * 1000 - 23000;
                    Thread.Sleep(Convert.ToInt32(waitingTime));
                   
                    take_Call(number, message, repetCount, delay, empNo, issue_occor_id);
                }
                if (Int32.Parse(repetCount) <= callCount)
                {
                    gsm_status = true;
                    callCount = 0; if (numberList.Count != 0)
                    {
                        doCommunicate();
                    }
                }

            }
            sr.Close();
            stream.Close();
            callCount = 0;

        }

        



    }
}
