using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Services.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    public class LanguageService: BaseEntityService, ILanguageService
    {
        /// <summary>
        /// Create New Language.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Saved LanguageResponse Object or Error object if any issues.</returns> 
        public ResponseBase CreateLanguage(LanguageRequest request)
        {
            var language = LanguageHelper.GetLanguageByCode(request.Code, DBArpaContext);
            if (language != null)
            {
                BadResponse badResponse = new BadResponse("Language with the code already exist.");
                return badResponse;
            }

            Data.Models.Language newlyCreatedLanguage = LanguageHelper.CreateLanguage(request, DBArpaContext);

            OkResponse<LanguageResponse> response = new OkResponse<LanguageResponse>();
            response.Response = new LanguageResponse(newlyCreatedLanguage);
            response.StatusCode = StatusCodes.Status201Created;
            return response;
        }

        /// <summary>
        /// Get all Language.
        /// </summary>
        /// <returns name="ResponseBase">Languages Object or Error object if any issues.</returns> 
        public ResponseBase GetLanguages()
        {
            List<Data.Models.Language> languages = LanguageHelper.GetLanguages(DBArpaContext);

            List<LanguageResponse> languageResponses = new List<LanguageResponse>();
            foreach (var lang in languages)
            {
                var languageResponse = new LanguageResponse(lang);
                languageResponses.Add(languageResponse);
            }

            OkResponse<List<LanguageResponse>> okResponse = new OkResponse<List<LanguageResponse>>();
            okResponse.Response = languageResponses;
            return okResponse;
        }

        public ResponseBase UpdateLanguage(LanguageRequest request)
        {
            Data.Models.Language language = LanguageHelper.GetLanguageById(request.Id, DBArpaContext);
            if (language == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError("Id", new string[] { "Wrong Language Id." });
                return badResponse;
            }
            Data.Models.Language updatedLanguage = LanguageHelper.UpdateLanguage(request, DBArpaContext);
            if (updatedLanguage == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(new string[] { "Language update is not successful." });
                return badResponse;
            }
            var user = LanguageHelper.UpdateLanguage(request, DBArpaContext);
            LanguageResponse languageResponse = new LanguageResponse(language);
            OkResponse<LanguageResponse> okResponse = new OkResponse<LanguageResponse>();
            okResponse.Response = languageResponse;
            return okResponse;


        }
    }
}
