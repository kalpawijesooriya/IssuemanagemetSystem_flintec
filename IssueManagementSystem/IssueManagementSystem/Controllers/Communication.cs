using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace IssueManagementSystem.Controllers
{
    public class Communication
    {
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

        public void sendMail()
        {
            using (MailMessage mm = new MailMessage("flintec.ism.alerts@gmail.com", "kalpa.wijesooriya@outlook.com"))
            {
                mm.Subject = "machine breakedown Notification";
                mm.Body = "This is test message";

                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("flintec.ism.alerts@gmail.com", "ism@flintec");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                   
                }
            }

        }

        public void Send_SMS()
        {

            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = "p=94789091710&m=sjghs";
                byte[] data = encoding.GetBytes(postData);

                WebRequest request = WebRequest.Create("http://192.168.137.104/sms_call.php");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                WebResponse response = request.GetResponse();
                stream = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream);

                var cs = sr.ReadToEnd();

                sr.Close();
                stream.Close();

            }
            catch (Exception ex)
            {

            }



        }
    }
}