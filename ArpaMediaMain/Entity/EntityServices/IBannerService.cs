using ArpaMedia.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    interface IBannerService
    {
        /// <summary>
        /// Create Banner object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Saved RequestBanner Object or Error object if any issues.</returns> 
        public ResponseBase CreateBanner(BannerRequest request);

        /// <summary>
        /// Update Banner object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Updated RequestBanner Object or Error object if any issues.</returns> 
        public ResponseBase UpdateBanner(BannerRequest request);

        /// <summary>
        /// Delete Banner object from databese by Id.
        /// </summary>
        /// <param name="request">Necessary data to delete banner object from database.</param> 
        /// <returns name="ResponseBase">Successes message or Error object if any issues.</returns> 
        public ResponseBase DeleteBanner(DeleteRequest request);

        /// <summary>
        /// Get Banner object.
        /// </summary>
        /// <param name="type">Get Banners by given bannerType.</param> 
        /// <returns name="ResponseBase"> Banners or Error object if any issues.</returns> 
        public ResponseBase GetBannerForBannerType(string type);

        /// <summary>
        /// Get Banner object replace imageURL.
        /// </summary>
        /// <param name="type">Get Banners by given BannerType.</param> 
        /// <param name="domain">Doamin of the site.</param> 
        /// <returns name="ResponseBase"> Banners or Error object if any issues.</returns> 
        public ResponseBase GetBannerForBannerTypeWithDomain(string domain, string type);

        /// <summary>
        /// Upload image for the post object.
        /// </summary>
        /// <param name="bannerId">Id of the Post object to be modified.</param> 
        /// <param name="imageStream">Data of the image.</param> 
        /// <returns name="ResponseBase">Success message.</returns> 
        public ResponseBase UploadImage(int bannerId, Stream imageStream);
    }
}
