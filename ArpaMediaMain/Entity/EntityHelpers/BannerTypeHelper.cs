using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Entity.EntityHelpers;
using ArpaMedia.Web.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ArpaMedia.Web.Api.EntityProviders
{
    public class BannerTypeHelper
    {
        /// <summary>
		/// Create BannerTypeHelper object in database.
		/// </summary>
		/// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="BannerType">Saved BannerTypeHelper Object.</returns> 
        public static BannerType CreateBannerType(BannerTypeRequest request, ArpaMediaContext context)
        {
            BannerType bannerType = new BannerType();
            bannerType.Title = request.Title;
            bannerType.LanguageId = request.LangaugeId;
            try
            {
                var savedBannerType = context.Add(bannerType);
                context.SaveChanges();
                return savedBannerType.Entity;
            }
            catch
            {
                context.Remove(bannerType);
                return null;
            }
        }

        /// <summary>
		/// Update BannerType object in database.
		/// </summary>
		/// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="BannerType">Updated bannerType Object.</returns> 
        public static BannerType UpdateBannerType(BannerTypeRequest request, ArpaMediaContext context)
        {
            BannerType bannerType = GetBannerTypeById(request.Id, context);
            bannerType.Title = request.Title;
            bannerType.LanguageId = bannerType.LanguageId;
            context.SaveChanges();
            return bannerType;
        }

        /// <summary>
		/// Delete BannerType object from database by Id.
		/// </summary>
		/// <param name="bannerTypeId">Id of the bannerType to be deleted.</param>  
        /// <param name="context">Context of the Database.</param> 
        public static bool DeleteBannerType(int bannerTypeId, ArpaMediaContext context)
        {
            ICollection<Banner> banners = context.Banners.Where(b => b.BannerTypeId == bannerTypeId).ToList();

            foreach (var eachBanner in banners)
            {
                Banner banner = BannerHelper.GetBannerById(eachBanner.Id, context);
                context.Banners.Remove(banner);
            }
            BannerType bannerType = GetBannerTypeById(bannerTypeId, context);
            if (bannerType == null)
            {
                return false;
            }
            context.BannerTypes.Remove(bannerType);
            context.SaveChanges();
            return true;
        }

        /// <summary>
		/// Get BannerType object from database by Id.
		/// </summary>
		/// <param name="bannerTypeId">Id of the bannerType to be retrieved.</param>  
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="BannerType">Find BannerType by bannerTypeId.</returns> 
        public static BannerType GetBannerTypeById(int bannerTypeId, ArpaMediaContext context)
        {
            var bannerType = context.BannerTypes.Where(u => u.Id == bannerTypeId).Include(u => u.Language).FirstOrDefault();
            return bannerType;
        }

        /// <summary>
        /// Get BannerType object from database by Title.
        /// </summary>
        /// <param name="bannerTypeTitle">Title of the bannerType to be retrieved.</param>  
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="BannerType">Find BannerType by bannerType Title.</returns> 
        public static BannerType GetBannerTypeByTitle(string bannerTypeTitle, ArpaMediaContext context)
        {
            var bannerType = context.BannerTypes.Where(u => u.Title.Equals(bannerTypeTitle)).Include(u => u.Language).FirstOrDefault();
            return bannerType;
        }

        /// <summary>
        /// Get bannerTypes by language code.
        /// </summary>
        /// <param name="LanguageCode">Code of the language.</param>  
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="BannerType">All BannerTypes found for language Code.</returns> 
        public static List<BannerType> GetbannerTypeByLanguageCode(string LanguageCode, ArpaMediaContext context)
        {
            List<BannerType> bannerTypes = null;
            var language = context.Languages.Where(l => l.Code == LanguageCode).FirstOrDefault();
            if (language != null)
            {
                bannerTypes = context.BannerTypes.Where(bt => bt.LanguageId == language.Id).Include(bt => bt.Language).ToList();
            }
            return bannerTypes;
        }

        /// <summary>
        /// Get all bannerTypes.
        /// </summary>
        /// <param name="context">Context of the Database.</param>
        /// <returns name="BannerType">All BannerTypes.</returns> 
        public static List<BannerType> GetAll(ArpaMediaContext context)
        {
            List<BannerType> bannerTypes = context.BannerTypes.Include(u => u.Language).ToList();
            return bannerTypes;
        }

    }
}
