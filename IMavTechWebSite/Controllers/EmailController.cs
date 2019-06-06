using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMavTechWebSite.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IMavTechWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private const string _followUpMessage = "Thanks for reaching out to me. I'll look over your message as soon as I can and get back to you.";
        private readonly IOptions<EmailModel> emailSettings;

        public EmailController(IOptions<EmailModel> email)
        {
            emailSettings = email;
        }

        // GET: api/Email
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Email/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Email
        [HttpPost]
        public void Post([FromForm] EmailModel email)
        {
            SendContactRequestEmail(email);

            //This redirection doesn't work at all
            //TODO: Finish AJAX so redirection attempt isn't needed
            RedirectToAction(controllerName:"Home", actionName:"Index");
        }

        /// <summary>
        /// Follow up email that goes out to the user
        /// </summary>
        /// <param name="email">The Email Object</param>
        private void SendFollowUpEmail(EmailModel email)
        {
            email.Body = _followUpMessage;
            SendEmail(email.ClientName,
                       email.ClientEmailAddress,
                       emailSettings.Value.OwnerName,
                       emailSettings.Value.OwnerEmailAddress,
                       email);
        }

        // PUT: api/Email/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Sends first email that will go to the owner through the web server. If successful, second email will be sent.
        /// </summary>
        /// <param name="email">The Email Object</param>
        private void SendContactRequestEmail(EmailModel email)
        {
            email.Body = $"{email.Body} | Respond to {email.ClientEmailAddress} if interested.";
            SendEmail(emailSettings.Value.ServerName,
                       emailSettings.Value.ServerEmailAddress,
                       emailSettings.Value.OwnerName,
                       emailSettings.Value.OwnerEmailAddress,
                       email);

            SendFollowUpEmail(email);
        }

        /// <summary>
        /// First half of Mailkit API. Prepares the email for sending.
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderEmailAddress"></param>
        /// <param name="recipientName"></param>
        /// <param name="recipientEmailAddress"></param>
        /// <param name="email"></param>
        private void SendEmail(string senderName, string senderEmailAddress, string recipientName, string recipientEmailAddress, EmailModel email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmailAddress));
            message.To.Add(new MailboxAddress(recipientName, recipientEmailAddress));
            message.Subject = $"{email.ClientName} sent a message. '{email.Subject}'";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<div>{email.Body}</div>";
            bodyBuilder.TextBody = $"{email.Body}";

            message.Body = bodyBuilder.ToMessageBody();

            UseMailKitSmtp(message);
        }

        /// <summary>
        /// Second half of Mailkit API. Sends the email over SMTP.
        /// </summary>
        /// <param name="message"></param>
        private void UseMailKitSmtp(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(emailSettings.Value.Smtp, Convert.ToInt32(emailSettings.Value.Port));
                client.Authenticate(emailSettings.Value.UserName, emailSettings.Value.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
