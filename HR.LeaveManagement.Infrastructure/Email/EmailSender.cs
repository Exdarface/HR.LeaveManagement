using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.Email
{
    public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
    {
        private EmailSettings _emailSettings = emailSettings.Value;
        public async Task<bool> SendEmailAsync(Application.Models.Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridEmail = MailHelper.CreateSingleEmail(from, to, subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(sendGridEmail);

            return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
