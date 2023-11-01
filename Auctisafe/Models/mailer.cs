using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI;

namespace Auctisafe.Models
{
    public class mailer
    {
        public void Email_sender(string email, int code)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("Auctisafe@gmail.com", "Auctisafe", System.Text.Encoding.UTF8);
                mail.Subject = "Signup Verification";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "This is your verification code: " + code;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("Auctisafe@gmail.com", "dongmqkgbgzlblve");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(mail);
            }
            catch(Exception x)
            {

            }
                
            
        }
        public void Emailer(string email, string subject, string body)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("Auctisafe@gmail.com", "Auctisafe", System.Text.Encoding.UTF8);
                mail.Subject = subject.ToString();
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = body.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("Auctisafe@gmail.com", "dongmqkgbgzlblve");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(mail);
            }
            catch (Exception x)
            {

            }
        }
    }
}