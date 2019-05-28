using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMavTechWebSite.Models
{
    public class EmailModel
    {
        public string FromName { get; set; }
        public string FromEmailAddress { get; set; }
        public string ToName { get; set; }
        public string ToEmailAddress { get; set; }
        public string ClientName { get; set; }
        public string ClientEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Smtp { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
