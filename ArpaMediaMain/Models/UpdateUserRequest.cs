using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models.Authorization
{
    public class UpdateUserRequest
    {
        [EmailAddress(ErrorMessage = "email_invalid")]
        public string NewEmail { get; set; }

        [MinLength(8)]
        [MaxLength(60)]

        public string NewPassword { get; set; }
        [MinLength(8)]
        [MaxLength(60)]

        public string ConfirmPassword { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int UserID { get; set; }
    }
}
