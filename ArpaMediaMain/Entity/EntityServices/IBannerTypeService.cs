using ArpaMedia.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    interface IBannerTypeService
    {

        /// <summary>
        /// Create BannerType object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Saved RequestBannerType Object or Error object if any issues.</returns> 
        public ResponseBase CreateBannerType(BannerTypeRequest request);

        /// <summary>
        /// Update BannerType object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Updated RequestBannerType Object or Error object if any issues.</returns> 
        public ResponseBase UpdateBannerType(BannerTypeRequest request);

        /// <summary>
        /// Delete BannerType object from databese by Id.
        /// </summary>
        /// <param name="request">Necessary data to delete BannerType object from database.</param> 
        /// <returns name="ResponseBase">Successes message or Error object if any issues.</returns> 
        public ResponseBase DeleteBannerType(DeleteRequest request);


        /// <summary>
        /// Get BannerType object.
        /// </summary>
        /// <param name="id">Get BannerType by given id.</param> 
        /// <returns name="ResponseBase"> BannerType or Error object if any issues.</returns> 
        public ResponseBase GetBannerTypeById(int id);

        /// <summary>
        /// Get BannerType objects.
        /// </summary>
        /// <param name="languageCode">Get BannerType by given languageCode.</param> 
        /// <returns name="ResponseBase"> BannerTypes or Error object if any issues.</returns> 
        public ResponseBase GetbannerTypeByLanguageCode(string languageCode);

        /// <summary>
        /// Get BannerType objects.
        /// </summary>
        /// <param name="bannerTypeTitle">Get BannerType by given bannerTypeTitle.</param> 
        /// <returns name="ResponseBase"> BannerTypes or Error object if any issues.</returns> 
        public ResponseBase GetBannerTypeByTitle(string bannerTypeTitle);

    }
}
