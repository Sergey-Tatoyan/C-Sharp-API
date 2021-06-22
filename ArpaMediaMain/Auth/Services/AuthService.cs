using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using ArpaMedia.Web.Api.Services.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.EntityServices;

namespace ArpaMedia.Web.Api.Services
{
    public class AuthService : BaseEntityService, IAuthService
    {
        private readonly AppSettings _appSettings;


        public AuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public ResponseBase Authenticate(AuthenticateRequest model, IConfiguration configuration)
        {
            BadResponse response = new BadResponse();
            response.StatusCode = 403;
            var user = UserHelper.FindUser(model, DBArpaContext);
            // return null if user not found
            if (user == null)
            {
                response.AddResponseError(new string[] { "Wrong credentials." });
                return response;
            }

            OkResponse<AuthenticateResponse> okResponse = new OkResponse<AuthenticateResponse>();
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user, configuration);
            var expiration = int.Parse(configuration["Auth:Expiration_In_Seconds"]);
            AuthenticateResponse authenticateResponse = new AuthenticateResponse(token, expiration);
            okResponse.Response = authenticateResponse;
            okResponse.StatusCode = 201;
            return okResponse;
        }

        // helper methods

        private string GenerateJwtToken(User user, IConfiguration configuration)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddSeconds(int.Parse(configuration["Auth:Expiration_In_Seconds"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
