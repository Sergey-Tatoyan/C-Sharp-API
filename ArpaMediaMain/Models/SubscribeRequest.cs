using ArpaMedia.Web.Api.ErrorCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class SubscribeRequest
    {
        [EmailAddress(ErrorMessage = ValidationErrors.EmailValidation)]
        [Required(ErrorMessage = ValidationErrors.FieldRequired)]
        [MaxLength(100, ErrorMessage = ValidationErrors.FieldMaxLength)]
        public string Email { get; set; }
    }
}
