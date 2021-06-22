using ArpaMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityHelpers
{
    public class BaseEntityHelper
    {
        /// <summary>
        /// Save changes in Database.
        /// </summary>
        /// <param name="context">Context from the Database.</param> 
        public static void SaveContext(ArpaMediaContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
