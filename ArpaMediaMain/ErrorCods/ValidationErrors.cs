using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.ErrorCodes
{
    public class ValidationErrors
    {
        public const string FieldRequired = "api.validation.fieldRequired";

        public const string FieldDate = "api.validation.fieldDate";

        public const string FieldMaxLength = "api.validation.fieldMaxLength";

        public const string EmailValidation = "api.validation.fieldEmail";
    }
}
