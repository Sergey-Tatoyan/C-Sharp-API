using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Entity.EntityHelpers;
using ArpaMedia.Web.Api.EntityProviders;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.UploadServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    public class BannerService : BaseEntityService, IBannerService
    {

        /// <summary>
        /// Create Banner object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="Category">Saved Banner Object.</returns> 
        public ResponseBase CreateBanner(BannerRequest request)
        {
            BadResponse response = new BadResponse();
            BannerType bannerType = BannerTypeHelper.GetBannerTypeById(request.BannerTypeId, DBArpaContext);
            if (bannerType == null)
            {
                response.AddResponseError("Id", new string[] { "Wrong Banner Type." });
                return response;
            }
            var oldBanner = BannerHelper.GetBannerByTitle(request.Title, DBArpaContext);
            if (oldBanner != null)
            {
                response.AddResponseError("Title", new string[] { "Duplicate title." });
            }

            if (response.Errors != null)
            {
                return response;
            }
            try
            {
                OkResponse<BannerResponse> okResponse = new OkResponse<BannerResponse>();
                var bannerEntity = BannerHelper.CreateBanner(request, DBArpaContext);
                BannerResponse bannerResponse = new BannerResponse(bannerEntity);
                okResponse.Response = bannerResponse;
                okResponse.StatusCode = StatusCodes.Status201Created;
                return okResponse;
            }
            catch (Exception e)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(new string[] { e.Message });
                return badResponse;
            }
        }

        /// <summary>
        /// Update Banner object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="Banner">Saved Banner Object.</returns> 
        public ResponseBase UpdateBanner(BannerRequest request)
        {
            BannerType bannerType = BannerTypeHelper.GetBannerTypeById(request.BannerTypeId, DBArpaContext);
            if (bannerType == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError("Id", new string[] { "Wrong BannerType Id." });
                return badResponse;
            }
            BadResponse response = new BadResponse();

            var currentBanner = BannerHelper.GetBannerById(request.Id, DBArpaContext);
            if (currentBanner == null)
            {
                response.AddResponseError(new string[] { "Banner was not found." });
                return response;
            }

            var oldBanner = BannerHelper.GetBannerByTitle(request.Title, DBArpaContext);

            if (oldBanner != null && oldBanner.Id != currentBanner.Id)
            {
                response.AddResponseError("Title", new string[] { "Duplicate title." });
            }

            if (response.Errors != null)
            {
                return response;
            }

            try
            {
                OkResponse<BannerResponse> okResponse = new OkResponse<BannerResponse>();
                var bannerEntity = BannerHelper.UpdateBanner(request, DBArpaContext);
                BannerResponse bannerResponse = new BannerResponse(bannerEntity);
                okResponse.Response = bannerResponse;
                okResponse.StatusCode = StatusCodes.Status200OK;
                return okResponse;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Delete Banner object in database.
        /// </summary>
        /// <param name="request">Banner to delete.</param> 
        /// <returns name="ResponseBase">Status code and message.</returns> 
        public ResponseBase DeleteBanner(DeleteRequest request)
        {
            BadResponse response = new BadResponse();
            response.StatusCode = 403;
            Banner banner = BannerHelper.GetBannerById(request.Id, DBArpaContext);
            if (banner == null)
            {
                response.AddResponseError(new string[] { "Banner was not found." });
                return response;
            }
            OkResponse<Object> okResponse = new OkResponse<Object>();
            BannerHelper.DeleteBanner(banner.Id, DBArpaContext);
            okResponse.StatusCode = StatusCodes.Status200OK;
            okResponse.Response = "Success";
            return okResponse;
        }

        /// <summary>
        ///  Get Banners by given banner type.
        /// </summary>
        /// <param name="type">BannerType to get banners.</param> 
        /// <returns name="ResponseBase">Status code and message.</returns> 
        public ResponseBase GetBannerForBannerType(string type)
        {
            var bannerType = BannerTypeHelper.GetBannerTypeByTitle(type, DBArpaContext);
            if (bannerType == null)
            {
                return new BadResponse("Wrong BannerType.");
            }

            var banners = BannerHelper.GetBannerByBannerTypeId(bannerType.Id, DBArpaContext);

            List<BannerResponse> bannerResponse = new List<BannerResponse>();
            foreach (var item in banners)
            {
                bannerResponse.Add(new BannerResponse(item));
            }

            OkResponse<List<BannerResponse>> okResponsde = new OkResponse<List<BannerResponse>>();
            okResponsde.Response = bannerResponse;
            return okResponsde;
        }

        /// <summary>
        ///  Get Banners by given banner type, replace imageURL.
        /// </summary>
        /// <param name="type">Banner type to get banners.</param> 
        /// <param name="domain">Domain of the site.</param> 
        /// <returns name="ResponseBase">Status code and message.</returns> 
        public ResponseBase GetBannerForBannerTypeWithDomain(string domain, string type)
        {
            ResponseBase searchResponse = GetBannerForBannerType(type);
            if (searchResponse.StatusCode == StatusCodes.Status200OK)
            {
                OkResponse<List<BannerResponse>> successRespnse = (OkResponse<List<BannerResponse>>)searchResponse;
                List<BannerResponse> bannerResponses = successRespnse.Response;
                foreach (var eacheBannerResponse in successRespnse.Response)
                {
                    eacheBannerResponse.ImageUrl = domain + eacheBannerResponse.ImageUrl;
                }
            }
            return searchResponse;
        }

        /// <summary>
        /// Upload image for the banner object.
        /// </summary>
        /// <param name="bannerId">Id of the Banner object to be modified.</param> 
        /// <param name="imageStream">Data of the image.</param> 
        /// <returns name="ResponseBase">Success message.</returns> 
        public ResponseBase UploadImage(int bannerId, Stream imageStream)
        {
            if (bannerId <= 0)
            {
                return new BadResponse("Incorrect Banner Id.");
            }

            Banner banner = BannerHelper.GetBannerById(bannerId, DBArpaContext);
            if (banner == null)
            {
                return new BadResponse("Incorrect Banner Id.");
            }

            ImageProvider uploadProvider = new ImageProvider();
            try
            {
                if (banner.ImageUrl != null)
                {
                    uploadProvider.DeleteFile(banner.ImageUrl);
                }

                string imagePath = uploadProvider.UploadImage(imageStream);
                banner.ImageUrl = imagePath;

                BannerHelper.SaveContext(DBArpaContext);

                OkResponse<string> okResponse = new OkResponse<string>();
                okResponse.Response = "Success";
                okResponse.StatusCode = StatusCodes.Status201Created;
                return okResponse;
            }
            catch (Exception e)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(e.Message);
                return badResponse;
            }
        }
    }
}
