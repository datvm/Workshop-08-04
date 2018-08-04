using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Models;
using Workshop.Api.Models.Services;
using Workshop.Api.Models.ViewModels;

namespace Workshop.Api.Controllers
{

    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IGoogleService googleService;
        IUserService userService;
        JwtIssuer jwtIssuer;

        public AuthController(
            IGoogleService googleService,
            IUserService userService,
            JwtIssuer jwtIssuer)
        {
            this.googleService = googleService;
            this.userService = userService;
            this.jwtIssuer = jwtIssuer;
        }

        [Route("token")]
        public IActionResult Token(string email, string password)
        {
            throw new NotImplementedException();
        }

        [Route("google-auth")]
        public ActionResult<GoogleAuthResponseViewModel> GoogleAuth(string state)
        {
            if (state.IsNullOrEmpty())
            {
                return this.BadRequest();
            }

            var result = this.googleService.GetOAuth2Url(state);
            return new GoogleAuthResponseViewModel()
            {
                Url = result.AbsoluteUri,
            };
        }

        [Route("google")]
        public async Task<ActionResult<GoogleLoginResponseViewModel>> GoogleLogin(string code)
        {
            var googleUserInfo = await this.googleService.GetUserFromCode(code);
            var user = await this.userService.GetByGoogleUserAsync(googleUserInfo);

            var token = this.jwtIssuer.IssueToken(
                "id", user.Id.ToString(),
                "email", user.Email,
                // WARNING: NOT FOR PRODUCTION
                "isAdmin", (user.Email == "datvm2@gmail.com" ? "1" : "0"));

            return new GoogleLoginResponseViewModel()
            {
                Token = token,
            };
        }

    }

}