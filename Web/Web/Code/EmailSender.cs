using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Web.Code
{
    public static class EmailSender
    {
        public static void Send(Configuration configuration, EmailMssg msg)
        {
            SmtpClient Client = new SmtpClient();
            MailMessage message = new MailMessage();
            message.IsBodyHtml = msg.IsHtml;
            message.Body = msg.GetMessageContent();
            message.Subject = msg.Subject;

            foreach (string receiver in msg.Receivers)
                message.To.Add(receiver);

            message.From = new MailAddress(msg.SenderAddress);


            Client.Port = configuration.ftpConfig.Port;
            Client.Host = configuration.ftpConfig.Server;
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential(configuration.ftpConfig.Login, configuration.ftpConfig.Password);

            Client.Send(message);
        }
    }
}