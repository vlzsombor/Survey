using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Survey.Server.Services
{
    public class EmailService
    {
        //Execute().Wait();
        public static async Task SendEmail(string toEmail, string accessGuid)
        {
            var apiKey = Environment.GetEnvironmentVariable("emailTest");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("vl.zsombor@hotmail.com", "Zsombor");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(toEmail, "xddxd");
            var plainTextContent = "what is this?";
            var htmlContent = $"<strong>and easy to do anywhere, even with C# ${accessGuid}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
    }
}
