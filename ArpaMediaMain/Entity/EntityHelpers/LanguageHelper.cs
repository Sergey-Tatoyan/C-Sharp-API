using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using WebApi.Models;

namespace ArpaMedia.Web.Api.Services.Authorization
{
    public class LanguageHelper
    {
        /// <summary>
        /// Get Language by code.
        /// </summary>
        /// <param name="code">The code of the language to be searched.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Language">Found language by code.</returns> 
        public static Language GetLanguageByCode(String code, ArpaMediaContext context)
        {
            var languageResult = context.Languages.Where(u => u.Code == code).FirstOrDefault();
            return languageResult;
        }

        /// <summary>
        /// Get Language by id.
        /// </summary>
        /// <param name="languageId">The id of the language to be searched.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Language">Found language by id.</returns> 
        public static Language GetLanguageById(int languageId, ArpaMediaContext context)
        {
            var languageResult = context.Languages.Where(u => u.Id == languageId).FirstOrDefault();
            return languageResult;
        }

        /// <summary>
        /// Create Language.
        /// </summary>
        /// <param name="request">The data to be saved.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Language">Newly created Language.</returns> 
        public static Language CreateLanguage(LanguageRequest request, ArpaMediaContext context)
        {
            Language language = new Language();
            language.Code = request.Code;
            language.DisplayName = request.DisplayName;
            language.LastModified = DateTime.Now;
            language.Created = DateTime.Now;

            var savedLanguage = context.Add(language);
            context.SaveChanges();
            return savedLanguage.Entity;
        }

        /// <summary>
        /// Get all Language.
        /// </summary>
        /// <returns name="ResponseBase">Languages Object or Error object if any issues.</returns> 
        public static List<Language> GetLanguages(ArpaMediaContext context)
        {
            return context.Languages.ToList();
        }

        public static Language UpdateLanguage(LanguageRequest request, ArpaMediaContext context)
        {
            Language language = LanguageHelper.GetLanguageById(request.Id, context);
            language.Code = request.Code;
            language.DisplayName = request.DisplayName;
           
            try
            {
                context.SaveChanges();
                return language;
            }
            catch
            {
                return null;
            }
        }

        internal static Language GetLanguageById(object languageId, ArpaMediaContext dBArpaContext)
        {
            throw new NotImplementedException();
        }
    }
}
