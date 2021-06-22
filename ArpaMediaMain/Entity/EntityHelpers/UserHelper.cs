using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using WebApi.Models;

namespace ArpaMedia.Web.Api.Services.Authorization
{
    public class UserHelper
    {
        /// <summary>
        /// Find User  from database.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param>
        /// <returns name="UserResult">Found User.</returns> 
        /// <param name="context">Context of the Database.</param> 

        public static User FindUser(AuthenticateRequest request, ArpaMediaContext context)
        {
            AMResponseProvider provider = new AMResponseProvider();
            
            var UserResult = context.Users.Where(u => u.Email.Equals(request.Email)).FirstOrDefault();
            if (UserResult != null) {
                bool passwodIsVerified = provider.VerifyPassword(request.Password, UserResult.PasswordSalt, UserResult.PasswordKey);
                if (passwodIsVerified) {
                    return UserResult;
                }
                return null;
            }
            return UserResult;
        }

        /// <summary>
		/// Get User  from database by Id.
		/// </summary>
		/// <param name="userId">Id of the User to be retrieved.</param> 
		/// <param name="context">Context of the Database.</param> 
        public static User GetUserById(int userId, ArpaMediaContext context)
        {
            var UserResult = context.Users.Where(u => u.UserId == userId).FirstOrDefault();
            return UserResult;
        }

        /// <summary>
        /// Get User List  from database.
        /// </summary>
        /// <param name="context">Context of the Database.</param> 
        public static ICollection<User> GetAll(ArpaMediaContext context)
        {
            var UserResult = context.Users.ToList();
            return UserResult;
        }

        /// <summary>
        /// Create User in database.
        /// </summary> 
        /// <returns name="User">Saved User.</returns> 
        /// <param name="context">Context of the Database.</param> 
        public static void CreateUser(ArpaMediaContext context)
        {
            var user = new User();
            user.Email = "test@test.com";
            user.Created = DateTime.Now;
            user.LastModified = DateTime.Now;

            AMResponseProvider provider = new AMResponseProvider();
            byte[] newSalt;
            byte[] newKey;

            provider.CreatePasswordComponents("11111111", out newSalt, out newKey);

            user.PasswordSalt = newSalt;
            user.PasswordKey = newKey;
            context.Users.Add(user);
            context.SaveChanges();
        }

        /// <summary>
        /// Update User  in database.
        /// </summary>
        /// <param name="context">Post of the Database.</param> 
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="User">Updated User.</returns> 
        public static User UpdateUser(UpdateUserRequest request, ArpaMediaContext context)
        {
            var user = GetUserById(request.UserID, context);

            if (request.NewEmail != null)
            {
                user.Email = request.NewEmail;
            } 

            if (request.NewPassword != null)
            {
                AMResponseProvider provider = new AMResponseProvider();
                byte[] newSalt;
                byte[] newKey;

                provider.CreatePasswordComponents(request.NewPassword, out newSalt, out newKey);
                user.PasswordSalt = newSalt;
                user.PasswordKey = newKey;
            }
            user.LastModified = DateTime.Now;
            context.SaveChanges();
            return user;
        }
    }
}
