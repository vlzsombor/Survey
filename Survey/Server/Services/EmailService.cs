using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace Survey.Server.Services
{
    public class EmailService
    {
        //Execute().Wait();
        public static async Task SendEmail(List<(string email, string pin, string accessguid)> toEmail)
        {


            var apiKey = Environment.GetEnvironmentVariable("emailTest");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("vl.zsombor@hotmail.com", "Zsombor");
            var subject = "Sending with SendGrid is Fun";
            var plainTextContent = "what is this?";

            List<string> htmlContents = new List<string>();
            List<EmailAddress> emails = new List<EmailAddress>();

            var a = new List<Dictionary<string, string>>();

            foreach (var x in toEmail)
            {
                htmlContents.Add($"<strong>and easy to do anywhere, even with C# your access guid: {x.accessguid} <br> your pin {x.pin}</strong>");
                emails.Add(new EmailAddress(x.email, "anyone"));
                a.Add(new Dictionary<string, string>()
                    {
                        { "pin",x.pin},
                        { "accessguid",x.accessguid}
                    });
            }




            //var msg2 = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var msg = MailHelper.CreateMultipleEmailsToMultipleRecipients(
                from,
                emails,
                htmlContents,
      "jojooj {pin} -pin- <br> -accessguid- accessguid",
      "jojooj {pin} -pin- <br> -accessguid- accessguid",
                a
                );


            var response = await client.SendEmailAsync(msg);
        }
    }
}
