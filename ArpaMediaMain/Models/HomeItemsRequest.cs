using ArpaMedia.Web.Api.ErrorCodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class HomeItemsRequest
    {
        [Required(ErrorMessage = ValidationErrors.FieldRequired)]
        public int PostsLimit { get; set; }
        [Required(ErrorMessage = ValidationErrors.FieldRequired)]
        public string Language { get; set; }
    }
}
