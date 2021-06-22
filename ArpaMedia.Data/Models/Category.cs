using System;
using System.Collections.Generic;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryPosts = new HashSet<CategoryPost>();
            InverseParentCategory = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMenuItem { get; set; }
        public bool IsCalendarItem { get; set; }
        public bool IsHomeItem { get; set; }
        public int LanguageId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual Language Language { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<CategoryPost> CategoryPosts { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
    }
}
