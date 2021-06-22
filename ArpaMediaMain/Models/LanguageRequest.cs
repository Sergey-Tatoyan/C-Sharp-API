using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class LanguageRequest
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }
    }
}
