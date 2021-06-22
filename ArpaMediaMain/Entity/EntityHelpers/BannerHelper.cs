using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityHelpers
{
    public class BannerHelper : BaseEntityHelper
    {

        /// <summary>
        /// Create Banner object in database.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Banner">Saved Banner Object.</returns> 
        public static Banner CreateBanner(BannerRequest request, ArpaMediaContext context)
        {

            Banner banner = new Banner();
            banner.Title = request.Title;
            banner.BannerTypeId = request.BannerTypeId;
            banner.SmallText = request.SmallText;
            try
            {
                var savedBanner = context.Add(banner);
                context.SaveChanges();
                return savedBanner.Entity;
            }
            catch
            {
                context.Remove(banner);
                return null;
            }
        }

        /// <summary>
        /// Update Post object in database.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Post">Updated Post Object.</returns> 
        public static Banner UpdateBanner(BannerRequest request, ArpaMediaContext context)
        {
            Banner banner = BannerHelper.GetBannerById(request.Id, context);
            banner.Title = request.Title;
            banner.SmallText = request.SmallText;
            banner.BannerTypeId = request.BannerTypeId;
            try
            {
                context.SaveChanges();
                return banner;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get Banner object from database by bannerId.
        /// </summary>
        /// <param name="bannerId">Banner id.</param> 
        /// <param name="context">Banner of the Database.</param> 
        /// <returns name="Banner">Retrived Banner Object by Id.</returns> 
        public static Banner GetBannerById(int bannerId, ArpaMediaContext context)
        {
            Banner banner = context.Banners.Where(u => u.Id == bannerId).FirstOrDefault();
            return banner;
        }

        /// <summary>
        /// Delete Banner object from database.
        /// </summary>
        /// <param name="bannerId">Necessary banner id to Delete from database.</param> 
        /// <param name="context">Context from the Database.</param> 
        /// <returns name="bool">True if deleted, false if error.</returns> 
        public static bool DeleteBanner(int bannerId, ArpaMediaContext context)
        {
            Banner banner = GetBannerById(bannerId, context);
            context.Banners.Remove(banner);
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get Banner object.
        /// </summary>
        /// <param name="context">Context of the Database.</param>
        /// <param name="bannerTypeId">bannerTypeId of the banner.</param> 
        /// <returns name="Banner">Search Banners by BannerTypeId.</returns> 
        public static ICollection<Banner> GetBannerByBannerTypeId(int bannerTypeId, ArpaMediaContext context)
        {
            ICollection<Banner> banners = context.Banners.Where(u => u.BannerTypeId == bannerTypeId).ToList();
            return banners;
        }

        /// <summary>
        /// Get Banner object from database by Title.
        /// </summary>
        /// <param name="bannerTitle">Title of the banner to be retrieved.</param>  
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Banner"> Banners by given Title.</returns> 
        public static Banner GetBannerByTitle(string bannerTitle, ArpaMediaContext context)
        {
            var banner = context.Banners.Where(u => u.Title == bannerTitle).FirstOrDefault();
            return banner;
        }
    }
}
