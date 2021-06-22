using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityHelpers
{
    public class SubscribeHelper
    {
        /// <summary>
        /// Get Subscribe by Email.
        /// </summary>
        /// <param name="Email">The code of the Subscribe to be searched.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Subscribe">Found Subscribe by Email.</returns> 
        public static Subscribed GetSubscribeByEmail(String Email, ArpaMediaContext context)
        {
            var subscribeResult = context.Subscribeds.Where(u => u.Email == Email).FirstOrDefault();
            return subscribeResult;
        }

        /// <summary>
        /// Create Subscribe.
        /// </summary>
        /// <param name="request">The data to be saved.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Subscribe">Newly created Subscribe.</returns> 
        public static Subscribed CreateSubscribe(SubscribeRequest request, ArpaMediaContext context)
        {
            Subscribed subscribe = new Subscribed();
            subscribe.Email = request.Email;
            var savedsubscribe = context.Add(subscribe);
            context.SaveChanges();
            return savedsubscribe.Entity;
        }

        /// <summary>
        /// Get all subsribers.
        /// </summary>
        /// <returns name="ResponseBase">Saved SubscribeResponse Object or Error object if any issues.</returns> 
        public static List<Subscribed> GetSubscribers(ArpaMediaContext context)
        {
            return context.Subscribeds.ToList();
        }
    }
}
