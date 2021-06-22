using System;
using System.Collections.Generic;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class CategoryPost
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PostId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Post Post { get; set; }
    }
}
