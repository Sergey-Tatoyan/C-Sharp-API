using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class OkResponse<T>: ResponseBase
    {
        public T Response { get; set; }

        public OkResponse()
        {
            this.StatusCode = StatusCodes.Status200OK;
        }
    }
}
