using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using ArpaMedia.Web.Api.Services.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ArpaMedia.Web.Api.EntityServices
{
    public class UserService: BaseEntityService, IUserService 
    {

        public User GetById(int id) {
            var user = UserHelper.GetUserById(id,DBArpaContext);
            return user;
        }


        /// <summary>
		/// Get User list from database.
		/// </summary> 
        /// <returns name="users">Saved Category Object.</returns>
        public ResponseBase GetAll()
        {
            OkResponse <ICollection<User>> response = new OkResponse<ICollection<User>>();
            var users = UserHelper.GetAll(DBArpaContext);
            response.Response = users;
            response.StatusCode = 200;
            return response;
        }

        /// <summary>
		/// Create User.
		/// </summary>
        public void CreateUser()
        {
            UserHelper.CreateUser(DBArpaContext);
        }

        /// <summary>
        /// Update User  in database.
        /// </summary>
        /// <param name="model">Necessary data to save in database.</param> 
        /// <returns name="user">Updated User.</returns> 
        public ResponseBase UpdateUser(UpdateUserRequest model)
        {
            BadResponse response = new BadResponse();
            var userEntity = UserHelper.GetUserById(model.UserID, DBArpaContext);
            if (userEntity == null)
            {
                response.AddResponseError(new string[] { "Wrong UserId." });
                return response;
            }

            if (model.NewPassword != null )
            { 
                if (!model.NewPassword.Equals(model.ConfirmPassword))
                {
                    response.AddResponseError("ConfirmPassword", new string[] { "Confirm Password and Password are not Equals." });
                }
            }

            AMResponseProvider provider = new AMResponseProvider();
            if (!provider.VerifyPassword(model.Password, userEntity.PasswordSalt, userEntity.PasswordKey))
            {
                response.AddResponseError("Password", new string[] { "Invalid Old Password." });
            }

            if (response.Errors != null)
            {
                return response;
            }

            var user = UserHelper.UpdateUser(model, DBArpaContext);
            var userResponse = new UserResponse(user);
            var okResponse = new OkResponse<UserResponse>();
            okResponse.Response = userResponse;
            okResponse.StatusCode = StatusCodes.Status200OK;
            return okResponse;
        }
    }
}