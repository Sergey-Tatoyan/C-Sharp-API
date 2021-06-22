using ArpaMedia.Web.Api.Controllers;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using ArpaMedia.Web.Api.Services;
using ArpaMedia.Web.Api.ValidateModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using WebApi.Models;
using WebApi.EntityServices;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IAuthService authService;
        private IUserService userService;
        private IConfiguration configuration;
        private AMResponseProvider responseProvider;

        public UsersController(AuthService authService, IConfiguration configuration)
        {
            this.authService = authService;
            this.configuration = configuration;
            userService = new UserService();
            this.responseProvider = new AMResponseProvider();
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <remarks>
        /// This request is for change user email and password.
        /// 
        ///     Here you must give the user model and get updated user. The user with the id entered in the model will be changed․
        ///     
        /// </remarks>
        /// <param name="model">The user model must contain UserId, Email, Password, NewPassword, ConfirmPassword․</param>
        /// <response code="200">If user model is correct.</response>
        /// <response code="400">If user model is incorret.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult Update(UpdateUserRequest model)
        {
            var response = this.userService.UpdateUser(model);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /**
         * ============================================
         *              PUBLIC APIS
         * ============================================
         **/

        /// <summary>
        /// Authenticate public API
        /// </summary>
        /// <remarks>
        /// This request is for authentication.
        /// 
        ///     Here you must give the user model and get token. The token expiration is 15 min.
        ///     
        /// </remarks>
        /// <param name="model">The user model must contain email and password.</param>
        /// <response code="201">If username and password are correct.</response>
        /// <response code="400">If username or password are incorret.</response>
        [HttpPost("auth")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OkResponse<AuthenticateResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = this.authService.Authenticate(model, this.configuration);
            return this.responseProvider.VerifyResponse(response, this);
        }

        //remove this method
        [Authorize]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var users = this.userService.GetAll();
            return Ok(users);
        }

        //remove this method 
        [HttpPost("add")]
        public IActionResult add()
        {
            this.userService.CreateUser();
            return Ok("yeees");
        }
    }
}
