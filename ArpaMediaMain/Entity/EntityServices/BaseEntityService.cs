using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using ArpaMedia.Web.Api.Services.Authorization;
using System;

namespace ArpaMedia.Web.Api.EntityServices
{
    public class BaseEntityService
    {
        protected ArpaMediaContext DBArpaContext { get; set; }

        public BaseEntityService()
        {
            DBArpaContext = new ArpaMediaContext();
        }
    }
}
