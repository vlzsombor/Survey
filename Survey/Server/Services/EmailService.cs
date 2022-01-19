using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Survey.Shared;

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

            List<string> htmlContents = new List<string>();
            List<EmailAddress> emails = new List<EmailAddress>();

            var a = new List<Dictionary<string, string>>();

            foreach (var x in toEmail)
            {
                htmlContents.Add("Your credentials for voting on the surWee board");
                emails.Add(new EmailAddress(x.email, "anyone"));
                a.Add(new Dictionary<string, string>()
                    {
                        { "pin",x.pin},
                        { "accessguid",x.accessguid}
                    });
            }


            //$"This is your access datas your link: <a href=\"{Constants.FRONTEND_URL.BOARD_LOGIN + "\\" + x.accessguid  }\"></a> <br> your pin {x.pin}"

            //var msg2 = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var msg = MailHelper.CreateMultipleEmailsToMultipleRecipients(
                from,
                emails,
                htmlContents,
      $"Your link to the board: {Constants.FRONTEND_URL.BOARD_LOGIN}/accessguid \n your password: pin",
      $"Your link to the board: {Constants.FRONTEND_URL.BOARD_LOGIN}/accessguid <br> your password:  pin",
                a
                );


            var response = await client.SendEmailAsync(msg);
        }
    }
}
