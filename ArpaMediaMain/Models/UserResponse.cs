using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.ValidateModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArpaMedia.Web.Api.Models.Authorization
{
    public class UserResponse
    {
        public int UserId { get; set; } 
        public string Email { get; set; }
        public DateTime LastModified { get; set; }

        public UserResponse(User user)
        {
            this.UserId = user.UserId;
            this.Email = user.Email;
            this.LastModified = user.LastModified;
        }
    }
}
