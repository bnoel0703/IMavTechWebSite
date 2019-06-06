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
        /// Server's name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Server's email address
        /// </summary>
        public string ServerEmailAddress { get; set; }

        /// <summary>
        /// Owner's name
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Owner's email address
        /// </summary>
        public string OwnerEmailAddress { get; set; }

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
        /// Makes up body of email. Taken from contact modal or overriden in method
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
