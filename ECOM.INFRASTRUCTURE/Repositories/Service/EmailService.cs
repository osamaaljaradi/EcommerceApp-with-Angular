using ECOM.CORE.DTO;
using ECOM.CORE.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.INFRASTRUCTURE.Repositories.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task sendEmail(EmailDTO emailDTO)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("My",_configuration["EmailSetting:From"]));
            message.Subject = emailDTO.Subject;
            message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDTO.Content
            };
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(_configuration["EmailSetting:Smtp"],
                       int.Parse(_configuration["EmailSetting:Port"]), true);
                    await smtp.AuthenticateAsync(_configuration["EmailSetting:Username"],
                        _configuration["EmailSetting:Password"]);
                    await smtp.SendAsync(message);
                }
                catch (Exception ex)
                {

                }
                finally {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
        }
    }
}
