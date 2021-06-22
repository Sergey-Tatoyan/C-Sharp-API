using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    interface ISubscribeService
    {
        /// <summary>
        /// Create New Subscribe.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Saved SubscribeResponse Object or Error object if any issues.</returns> 
        public ResponseBase CreateSubscribe(SubscribeRequest request);

        /// <summary>
        /// Get all subsribers.
        /// </summary>
        /// <returns name="Subscribed">All subsribers</returns> 
        public List<Subscribed> GetSubscribers();
    }
}
