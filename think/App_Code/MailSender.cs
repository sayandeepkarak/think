using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace think
{
    public class MailSender
    {
        private SmtpClient smtp;
        private MailMessage mail;
        private string serviceMail;
        private string appCode;

        public MailSender(string serviceMail, string appCode)
        {
            this.smtp = new SmtpClient("smtp.gmail.com", 587);
            this.serviceMail = serviceMail;
            this.appCode = appCode;
            this.smtp.Credentials = new NetworkCredential(this.serviceMail, this.appCode);
            this.smtp.EnableSsl = true;
        }

        public void loadAndDispatch(string address, string subject, string message, Action<bool> callback)
        {
            try
            {
                this.mail = new MailMessage(this.serviceMail, address, subject, message);
                this.mail.IsBodyHtml = true;
                this.smtp.Send(this.mail);
                callback(true);
            }
            catch (Exception)
            {
                callback(false);
            }
        }
    }
}