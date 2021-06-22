using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Entity.EntityHelpers;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.ErrorCodes;
using ArpaMedia.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    public class SubscribeService: BaseEntityService, ISubscribeService
    {

        /// <summary>
        /// Create New Subscribe.
        /// </summary>
        /// <param name="trimrequest">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Saved SubscribeResponse Object or Error object if any issues.</returns> 
        public ResponseBase CreateSubscribe(SubscribeRequest trimrequest)
        {
            var subscribe = SubscribeHelper.GetSubscribeByEmail(trimrequest.Email, DBArpaContext);
            if (subscribe == null)
            {
                SubscribeHelper.CreateSubscribe(trimrequest, DBArpaContext);
                OkResponse<String> response = new OkResponse<String>();
                response.Response = "successful";
                response.StatusCode = StatusCodes.Status201Created;
                return response;
            }
            BadResponse badResponse = new BadResponse();
            badResponse.AddResponseError("Email", new string[] { SubscribeErrors.EmailExists });
            return badResponse;
        }

        /// <summary>
        /// Get all subsribers.
        /// </summary>
        /// <returns name="Subscribed">All subsribers</returns> 
        public List<Subscribed> GetSubscribers()
        {
            var result = SubscribeHelper.GetSubscribers(DBArpaContext);
            return result;
        }
    }
}
