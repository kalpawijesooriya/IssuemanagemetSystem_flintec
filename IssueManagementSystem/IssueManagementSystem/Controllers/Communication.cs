using IssueManagementSystem.Models;
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

                   

                    if (communicateData.getEmail() == 1 && emailAddress!= null)
                    {
                        sendMail(emailAddress, msg,communicateData.getsubject());
                        //using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                        //{
                        //    string query = "INSERT INTO tbl_Notifications ([Status],[Message],[Type],[EmployeeNumber],[Date]) VALUES( 1,'" + msg + "','email','" + communicateData.getEmployeeNumber() + "','" + date + "') ";
                        //    db.Database.ExecuteSqlCommand(query);
                        //}                                                 
                    }

                    if (communicateData.getMessage() == 1 && mobileNumber != null)                    
                        send_SMS(mobileNumber, msg);

                    if (communicateData.getCall() == 1 && mobileNumber != null)
                        take_Call(mobileNumber, communicateData.getcallNote(), communicateData.getrepetCount(),communicateData.getdelay());



                    gsm_status = true;
                    if (numberList.Count!= 0)
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

        public void take_Call(string number, string message ,string repetCount,string delay)
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

            if (cr[0] == "false") {
                if (Int32.Parse(repetCount) > callCount)
                {
                    int waitingTime = Int32.Parse(delay) * 60 * 1000 - 23000;
                    Thread.Sleep(waitingTime);
                    take_Call(number, message, repetCount, delay);
                }
                if (Int32.Parse(repetCount) <= callCount)
                {
                    gsm_status = true;
                    callCount = 0;
                    doCommunicate();
                }

            }
            sr.Close();
            stream.Close();
            callCount = 0;

        }

        



    }
}
