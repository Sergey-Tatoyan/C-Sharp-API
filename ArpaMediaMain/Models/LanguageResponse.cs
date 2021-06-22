using ArpaMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class LanguageResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }

        public LanguageResponse(Language language)
        {
            this.Code = language.Code;
            this.DisplayName = language.DisplayName;
            this.Id = language.Id;
        }
    }
}
