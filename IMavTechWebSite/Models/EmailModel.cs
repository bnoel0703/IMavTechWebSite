using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IMavTechWebSite.Models
{
    [ValidateAntiForgeryToken]
    public class EmailModel
    {
        public string FromName { get; set; }
        public string FromEmailAddress { get; set; }
        public string ToName { get; set; }
        public string ToEmailAddress { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string ClientName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Your Email Address")]
        public string ClientEmailAddress { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
        public string Smtp { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
