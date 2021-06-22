using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class CategoryRequest
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public bool IsMenuItem { get; set; }
        [Required]
        public bool IsCalendarItem { get; set; }
        [Required]
        public bool IsHomeItem { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
