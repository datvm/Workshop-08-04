using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Models.Services;
using Workshop.Api.Models.ViewModels;

namespace Workshop.Api.Controllers
{

    [Route("tweet")]
    [ApiController]
    [Authorize]
    public class TweetController : ControllerBase
    {

        ITweetService tweetService;

        public TweetController(ITweetService tweetService)
        {
            this.tweetService = tweetService;
        }

        [HttpGet, Route("")]
        public async Task<IEnumerable<BasicTweetViewModel>> GetUserTweets()
        {
            var userId = int.Parse(this.User.Claims
                .First(q => q.Type == "id")
                .Value);

            return await this.tweetService.GetUserTweets(userId);
        }

    }

}