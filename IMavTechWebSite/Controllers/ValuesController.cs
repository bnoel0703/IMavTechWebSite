using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using IMavTechWebSite.Models;

namespace IMavTechWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Email Placeholder" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        private void Send(Email email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(email.FromName, email.FromEmailAddress));
            message.To.Add(new MailboxAddress(email.ToName, email.ToEmailAddress));
            message.Subject = email.Subject;
            message.Body = new TextPart("plain text")
            {
                Text = email.Body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(email.Smtp, email.Port);
                client.Authenticate(email.UserName, email.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

