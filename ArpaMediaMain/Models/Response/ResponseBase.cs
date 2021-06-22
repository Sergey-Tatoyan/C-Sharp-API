using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class ResponseBase
    {
        [JsonIgnore]
        public int StatusCode { get; set; }

        public static implicit operator Task<object>(ResponseBase v)
        {
            throw new NotImplementedException();
        }
    }
}
