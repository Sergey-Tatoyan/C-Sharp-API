using ArpaMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMenuItem { get; set; }
        public bool IsCalendarItem { get; set; }
        public bool IsHomeItem { get; set; }
        public int? ParentCategoryId { get; set; }

        public ICollection<CategoryResponse> ChildCategories { get; set; }
        public LanguageResponse Language { get; set; }

        public CategoryResponse(Category category)
        {
            this.Name = category.Name;
            this.DisplayOrder = category.DisplayOrder;
            this.Id = category.Id;
            this.IsMenuItem = category.IsMenuItem;
            this.ParentCategoryId = category.ParentCategoryId;
            this.IsCalendarItem = category.IsCalendarItem;
            this.IsHomeItem = category.IsHomeItem;
            if (category.Language != null)
            {
                Language = new LanguageResponse(category.Language);
            }
        }

        public CategoryResponse()
        {

        }
    }
}
