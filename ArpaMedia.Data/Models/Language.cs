using System;
using System.Collections.Generic;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class Language
    {
        public Language()
        {
            BannerTypes = new HashSet<BannerType>();
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<BannerType> BannerTypes { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
