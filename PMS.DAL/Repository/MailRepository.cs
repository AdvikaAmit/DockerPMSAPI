using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PMS.Domain.DTO;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly MailSettings _mailSettings;
        public MailRepository(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmail(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);

            if (string.IsNullOrEmpty(mailRequest.ToEmail))
            {
                InternetAddressList list = new InternetAddressList();
                foreach (string address in mailRequest.recipients)
                {
                    list.Add(new MailboxAddress(address));
                }
                email.To.AddRange(list);
            }
            else
            {
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            }
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
