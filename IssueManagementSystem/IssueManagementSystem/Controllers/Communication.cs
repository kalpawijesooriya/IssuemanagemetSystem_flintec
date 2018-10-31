using IssueManagementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        dbController db = dbController.getInstance();
        public Communication()
        {
          
        }

        public void setCD(CommunicationData cd) {

            numberList.Enqueue(cd);          
            doCommunicate();
        }




        private void doCommunicate()
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

                    if (communicateData.getEmail() == 1)
                    {
                        sendMail(communicateData.getEmailAddress());

                        string query = "INSERT INTO [dbo].tbl_Notification(Ststus,Message,Type,EmployeeNumber,Date) VALUES('1','" + communicateData.getMsg() + "','email','"+ communicateData.getEmployeeNumber()+ "','"+ date + "') ";
                        db.runQuery_update_or_delete(query);

                    }

                    if (communicateData.getMessage() == 1)
                    
                        send_SMS(communicateData.getNumber(), communicateData.getMsg());

                    

                    if (communicateData.getCall() ==1)
                        take_Call(communicateData.getNumber(), communicateData.getMsg());

                    gsm_status = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
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

        public void sendMail(string emailAddress)
        {
            using (MailMessage mm = new MailMessage("ppts@flintec.com", emailAddress))
            {
                mm.Subject = "machine breakedown Notification";
                mm.Body = "This is test message";

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

                    WebRequest request = WebRequest.Create("http://192.168.137.238/sms_call.php");
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
                    Debug.WriteLine("SMS Respond: "+ cs);

                    sr.Close();
                    stream.Close();

        }

        public void take_Call(string number, string message)
        {



            ASCIIEncoding encoding = new ASCIIEncoding();


            string postData = "p=" + number + "&c=" + message;
            byte[] data = encoding.GetBytes(postData);

            WebRequest request = WebRequest.Create("http://192.168.137.238/sms_call.php");
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
            Debug.WriteLine("Call Respond: " + cs);
            sr.Close();
            stream.Close();


        }

        



    }
}
