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
            bool success = true;
            SendMyselfEmail(email, success);
            if (success)
                SendThankYouEmail(email);
            else
            {
                RedirectToAction("Index", "Home");
            }
        }

        private void SendThankYouEmail(EmailModel email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings.Value.ToName, emailSettings.Value.ToEmailAddress));
            message.To.Add(new MailboxAddress(email.ClientName, email.ClientEmailAddress));
            message.Subject = "I got your email! Thank you!";
            message.Body = new TextPart("plain text")
            {
                Text = "Thanks for reaching out to me. I'll look over it as soon as I can and get back to you."
            };

            using (var client = new SmtpClient())
            {
                client.Connect(emailSettings.Value.Smtp, Convert.ToInt32(emailSettings.Value.Port));
                client.Authenticate(emailSettings.Value.UserName, emailSettings.Value.Password);
                client.Send(message);
                client.Disconnect(true);
            }

            RedirectToAction("Index", "Home");
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

        private bool SendMyselfEmail(EmailModel email, bool success)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(emailSettings.Value.FromName, emailSettings.Value.FromEmailAddress));
                message.To.Add(new MailboxAddress(emailSettings.Value.ToName, emailSettings.Value.ToEmailAddress));
                message.Subject = $"{email.ClientName} sent a message. '{email.Subject}'";
                message.Body = new TextPart("plain text")
                {
                    Text = $"{email.Body} | Respond to {email.ClientEmailAddress} if interested."
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(emailSettings.Value.Smtp, Convert.ToInt32(emailSettings.Value.Port));
                    client.Authenticate(emailSettings.Value.UserName, emailSettings.Value.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                success = true;
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong. {ex.Message}");
            }
        }
    }
}
