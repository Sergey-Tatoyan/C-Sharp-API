using ArpaMedia.Web.Api.ErrorCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models.Response
{
    public class CalendarItemsRequest
    {
        [Required(ErrorMessage = ValidationErrors.FieldRequired)]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = ValidationErrors.FieldRequired)]
        [DataType(DataType.Date, ErrorMessage = ValidationErrors.FieldDate)]
        public DateTime End { get; set; }
        public string Language { get; set; }
    }
}
