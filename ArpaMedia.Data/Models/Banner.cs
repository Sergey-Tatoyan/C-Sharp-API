using System;
using System.Collections.Generic;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class Banner
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SmallText { get; set; }
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
        public int BannerTypeId { get; set; }

        public virtual BannerType BannerType { get; set; }
    }
}
