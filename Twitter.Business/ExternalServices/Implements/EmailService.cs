using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.ExternalServices.Interfaces;

namespace Twitter.Business.ExternalServices.Implements
{
    public class EmailService :IEmailService
    {
        IConfiguration _configuration { get; }
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string toMail, string header, string body)
        {
            if (!string.IsNullOrEmpty(toMail))
            {
                SmtpClient smtpClient = new SmtpClient(_configuration["Email:Host"],
                    Convert.ToInt32(_configuration["Email:Port"]));
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_configuration["Email:Username"],
                    _configuration["Email:Password"]);

                MailAddress from = new MailAddress(_configuration["Email:Username"], "Hello");
                MailAddress to = new MailAddress(toMail);
                MailMessage message = new MailMessage(from, to);
                message.Body = body;
                message.Subject = header;
                smtpClient.Send(message);
            }
            else
            {

                throw new ArgumentException("Email address cannot be null or empty.", nameof(toMail));
            }
        }

    }
}
