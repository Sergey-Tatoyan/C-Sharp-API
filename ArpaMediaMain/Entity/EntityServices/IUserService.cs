using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using System.Collections.Generic;

namespace ArpaMedia.Web.Api.EntityServices
{
    interface IUserService
    {
        ResponseBase GetAll();
        void CreateUser();
        ResponseBase UpdateUser(UpdateUserRequest model);
    }
}
