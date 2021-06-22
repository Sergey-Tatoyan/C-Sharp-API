using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.EntityProviders;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.ErrorCodes;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Services.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    public class BannerTypeService : BaseEntityService, IBannerTypeService
    {
        /// <summary>
        /// Create BannerType object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Saved RequestBannerType Object or Error object if any issues.</returns> 
        public ResponseBase CreateBannerType(BannerTypeRequest request)
        {
            BadResponse response = new BadResponse();
            Language language = LanguageHelper.GetLanguageById(request.LangaugeId, DBArpaContext);
            if (language == null)
            {

                response.AddResponseError("Id", new string[] { "Wrong Language Id." });
                return response;
            }
            var oldBannerType = BannerTypeHelper.GetBannerTypeByTitle(request.Title, DBArpaContext);
            if (oldBannerType != null)
            {
                response.AddResponseError("Name", new string[] { "Duplicate title." });
            }

            if (response.Errors != null)
            {
                return response;
            }

            try
            {
                OkResponse<BannerTypeResponse> okResponse = new OkResponse<BannerTypeResponse>();
                var bannerTypeEntity = BannerTypeHelper.CreateBannerType(request, DBArpaContext);
                BannerTypeResponse bannerTypeResponse = new BannerTypeResponse(bannerTypeEntity);
                okResponse.Response = bannerTypeResponse;
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
        /// Update BannerType object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Updated RequestBannerType Object or Error object if any issues.</returns> 
        public ResponseBase UpdateBannerType(BannerTypeRequest request)
        {
            Language language = LanguageHelper.GetLanguageById(request.LangaugeId, DBArpaContext);
            if (language == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError("Id", new string[] { "Wrong Language Id." });
                return badResponse;
            }
            BadResponse response = new BadResponse();

            var currentBannerType = BannerTypeHelper.GetBannerTypeById(request.Id, DBArpaContext);
            if (currentBannerType == null)
            {
                response.AddResponseError(new string[] { "BannerType was not found." });
                return response;
            }

            var oldBannerType = BannerTypeHelper.GetBannerTypeByTitle(request.Title, DBArpaContext);

            if (oldBannerType != null && oldBannerType.Id != currentBannerType.Id)
            {
                response.AddResponseError("Title", new string[] { "Duplicate title." });
            }

            if (response.Errors != null)
            {
                return response;
            }

            try
            {
                OkResponse<BannerTypeResponse> okResponse = new OkResponse<BannerTypeResponse>();
                var bannerTypeEntity = BannerTypeHelper.UpdateBannerType(request, DBArpaContext);
                BannerTypeResponse bannerTypeResponse = new BannerTypeResponse(bannerTypeEntity);
                okResponse.Response = bannerTypeResponse;
                okResponse.StatusCode = StatusCodes.Status200OK;
                return okResponse;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// Delete BannerType object from databese by Id.
        /// </summary>
        /// <param name="request">Necessary data to delete BannerType object from database.</param> 
        /// <returns name="ResponseBase">Successes message or Error object if any issues.</returns> 
        public ResponseBase DeleteBannerType(DeleteRequest request)
        {
            BannerType bannerType = BannerTypeHelper.GetBannerTypeById(request.Id, DBArpaContext);
            if (bannerType == null)
            {
                return new BadResponse("BannerType was not found.");
            }

            if (bannerType.Banners.Count != 0)
            {
                return new BadResponse("You can't delete. Please consider removeing banners connected with this type.");
            }

            OkResponse<Object> okResponse = new OkResponse<Object>();
            bool isDeleted = BannerTypeHelper.DeleteBannerType(bannerType.Id, DBArpaContext);
            okResponse.StatusCode = StatusCodes.Status200OK;
            okResponse.Response = "Success";
            return okResponse;
        }
        /// <summary>
        /// Get BannerType object by Id.
        /// </summary>
        /// <returns name="ResponseBase"> BannerType or Error object if any issues.</returns> 
        public ResponseBase GetBannerTypeById(int BannerTypeId)
        {
            BannerType bannerType = BannerTypeHelper.GetBannerTypeById(BannerTypeId, DBArpaContext);
            if (bannerType == null)
            {
                return new BadResponse("BannerType was not found.");
            }
            OkResponse<BannerTypeResponse> okResponse = new OkResponse<BannerTypeResponse>();
            okResponse.StatusCode = StatusCodes.Status200OK;
            okResponse.Response = new BannerTypeResponse(bannerType);
            return okResponse;
        }

        /// <summary>
        /// Get BannerType object by languageCode.
        /// </summary>
        /// <returns name="ResponseBase"> BannerType or Error object if any issues.</returns> 
        public ResponseBase GetbannerTypeByLanguageCode(string LanguageCode)
        {
            Language language = LanguageHelper.GetLanguageByCode(LanguageCode, DBArpaContext);
            if (language == null)
            {
                return new BadResponse( "Wrong Language Code." );
            }
            List<BannerType> bannerTypes = BannerTypeHelper.GetbannerTypeByLanguageCode(LanguageCode, DBArpaContext);
            List<BannerTypeResponse> bannerTypeResponses = new List<BannerTypeResponse>();
            foreach (var item in bannerTypes)
            {
                bannerTypeResponses.Add(new BannerTypeResponse(item));
            }
            OkResponse<List<BannerTypeResponse>> okResponse = new OkResponse<List<BannerTypeResponse>>();
            okResponse.StatusCode = StatusCodes.Status200OK;
            okResponse.Response = bannerTypeResponses;
            return okResponse;
        }

        /// <summary>
        /// Get BannerType object by Title.
        /// </summary>
        /// <returns name="ResponseBase"> BannerType or Error object if any issues.</returns> 
        public ResponseBase GetBannerTypeByTitle(string bannerTypeTitle)
        {
            BannerType bannerType = BannerTypeHelper.GetBannerTypeByTitle(bannerTypeTitle, DBArpaContext);
            if (bannerType == null)
            {
                return new BadResponse(BannerErrors.BannerNotFound);
            }
            OkResponse<BannerTypeResponse> okResponse = new OkResponse<BannerTypeResponse>();
            okResponse.StatusCode = StatusCodes.Status200OK;
            okResponse.Response = new BannerTypeResponse(bannerType);
            return okResponse;
        }

    }
}
