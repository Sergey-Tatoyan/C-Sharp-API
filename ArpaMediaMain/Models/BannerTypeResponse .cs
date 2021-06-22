using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.EntityProviders;
using ArpaMedia.Web.Api.Services.Authorization;
using System;
using System.Collections.Generic;

namespace ArpaMedia.Web.Api.Models
{
    public class BannerTypeResponse
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public LanguageResponse Language { get; set; }

        public int LanguageId { get; set; }

        public BannerTypeResponse(BannerType bannerType)
        {
            this.Id = bannerType.Id;
            this.Title = bannerType.Title;
            this.LanguageId = bannerType.LanguageId;
            if (bannerType.Language != null)
            {
                Language = new LanguageResponse(bannerType.Language);
            }
        }

        public BannerTypeResponse()
        {

        }
    }
}
