using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IMavTechWebSite.Models
{
    [ValidateAntiForgeryToken]
    public class Email
    {
        [Required]
        [Display(Name = "Name")]
        public string FromName { get; set; }

        public string ToName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string FromEmailAddress { get; set; }

        public string ToEmailAddress { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Body { get; set; }
        public int Port { get; set; }
        public string Smtp { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
