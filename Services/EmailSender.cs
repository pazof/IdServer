
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Hosting;
using MailKit.Net.Smtp;
using MimeKit;

using System;
using IdServer.Interfaces;
using IdServer.Entities;

namespace IdServer.Services
{
    public class EmailSender : IEmailSender, IMailer
    {
        public EmailSender(IOptions<SmtpSettings> smtpSettings,
         IWebHostEnvironment env,
         IDataProtectionProvider provider)
        {
            Options = smtpSettings.Value;
            Env = env;
            DataProtector = provider.CreateProtector(Options.ProtectionTitle);
        }
        public IDataProtector DataProtector { get; } 
        public SmtpSettings Options { get; } //set only via Secret Manager
        public IWebHostEnvironment Env { get; }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SenderName, subject, message, email);
        }

        public async Task Execute(string name, string subject, string message, string email)
        {
            await SendMailAsync(name, email, subject, message);
        }

        public async Task SendMailAsync(string name, string email, string subjet, string body)
        {
            try {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(Options.SenderName, Options.SenderEMail));
                message.To.Add(new MailboxAddress(name??email, email));
                message.Body = new TextPart("html") { Text = body };
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s,c,h,e)=>true;
                    if (Env.IsDevelopment())
                    {
                        await client.ConnectAsync(Options.Server, Options.Port, MailKit.Security.SecureSocketOptions.None);
                    }
                    else 
                    {
                        await client.ConnectAsync(Options.Server);
                    }
                    await client.AuthenticateAsync(Options.UserName, Options.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}