using ArpaMedia.Web.Api.ErrorCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models.Response
{
    public class PostSearchRequest
    {
        public string Query { get; set; }
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = ValidationErrors.FieldRequired)]
        public int Offset { get; set; }
        [Required(ErrorMessage = ValidationErrors.FieldRequired)]
        public int Limit { get; set; }
    }
}
