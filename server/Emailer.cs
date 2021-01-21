using System;
using System.Net.Mail;

public class Emailer {
    public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        throw new NotImplementedException("Mailing service is not implemented yet");
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("data@techcollege.dk"); //From address from AppSettings.
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            SmtpClient smtp = new SmtpClient();
            //smtp.Host = ConfigurationManager.AppSettings["Host"]; //SMTP Server name or IP from AppSettings.
            smtp.Send(mailMessage);
        }
    }
}