using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.EntityProviders;
using System;
using System.Collections.Generic;

namespace ArpaMedia.Web.Api.Models
{
    public class BannerResponse
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string SmallText { get; set; }
        public string ImageUrl { get; set; }
        public int BannerTypeId { get; set; }

        public BannerResponse(Banner banner)
        {
            this.Id = banner.Id;
            this.Title = banner.Title;
            this.SmallText = banner.SmallText;
            this.ImageUrl = banner.ImageUrl;
            this.BannerTypeId = banner.BannerTypeId;
        }

        public BannerResponse()
        {

        }
    }
}
