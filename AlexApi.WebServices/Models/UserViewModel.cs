using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlexAPI.WebServices.Models
{
    public class UserViewModel
    {
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ClientId { get; set; }
    }
}
