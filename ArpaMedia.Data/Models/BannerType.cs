using System;
using System.Collections.Generic;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class BannerType
    {
        public BannerType()
        {
            Banners = new HashSet<Banner>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
    }
}
