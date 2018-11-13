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
      
        public Communication()
        {
          
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void setCD(CommunicationData cd) {

            numberList.Enqueue(cd);          
            doCommunicate();
        }



      
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

                    if (communicateData.getCall() == 1 && mobileNumber != null)
                        take_Call(mobileNumber, msg);

                    if (communicateData.getEmail() == 1 && emailAddress!= null)
                    {
                        sendMail(emailAddress, msg,communicateData.getsubject());
                        using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                        {
                            string query = "INSERT INTO tbl_Notifications ([Status],[Message],[Type],[EmployeeNumber],[Date]) VALUES( 1,'" + msg + "','email','" + communicateData.getEmployeeNumber() + "','" + date + "') ";
                            db.Database.ExecuteSqlCommand(query);
                        }                                                 
                    }

                    if (communicateData.getMessage() == 1 && mobileNumber != null)                    
                        send_SMS(mobileNumber, msg);

                    

                   

                       gsm_status = true;
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

                    WebRequest request = WebRequest.Create("http://192.168.40.86/sms_call.php");

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
            System.Diagnostics.Debug.WriteLine("SMS Respond: " + cs);

                    sr.Close();
                    stream.Close();

        }

        public void take_Call(string number, string message)
        {



            ASCIIEncoding encoding = new ASCIIEncoding();


            string postData = "p=" + number + "&c=" + message;
            byte[] data = encoding.GetBytes(postData);

            WebRequest request = WebRequest.Create("http://192.168.40.86/sms_call.php");
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
            System.Diagnostics.Debug.WriteLine("Call Respond: " + cs);
            sr.Close();
            stream.Close();


        }

        



    }
}
