using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMavTechWebSite.Models
{
    public class Email
    {
        public string SenderName { get; set; }
        public string SenderEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailDescription { get; set; }
    }
}
