using ArpaMedia.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    interface ILanguageService
    {
        /// <summary>
        /// Create New Language.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Saved LanguageResponse Object or Error object if any issues.</returns> 
        public ResponseBase CreateLanguage(LanguageRequest request);

        /// <summary>
        /// Get all Language.
        /// </summary>
        /// <returns name="ResponseBase">Languages Object or Error object if any issues.</returns> 
        public ResponseBase GetLanguages();

        /// <summary>
        /// Update Language object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Updated RequestLanguage Object or Error object if any issues.</returns> 
        public ResponseBase UpdateLanguage(LanguageRequest request);
    }
}
