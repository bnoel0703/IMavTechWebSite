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
        /// <summary>
        /// Sender's name
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Sender's email address
        /// </summary>
        public string FromEmailAddress { get; set; }

        /// <summary>
        /// Recipient's name
        /// </summary>
        public string ToName { get; set; }

        /// <summary>
        /// Recipient's email address
        /// </summary>
        public string ToEmailAddress { get; set; }

        /// <summary>
        /// Name taken from contact modal
        /// </summary>
        [Required]
        [Display(Name = "Your Name")]
        public string ClientName { get; set; }

        /// <summary>
        /// Email address taken from contact modal
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Your Email Address")]
        public string ClientEmailAddress { get; set; }

        /// <summary>
        /// Email subject taken from contact modal
        /// </summary>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Makes up body of email. Taken from contact modal
        /// </summary>
        [Required]
        [Display(Name = "Description")]
        public string Body { get; set; }

        public string Smtp { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
