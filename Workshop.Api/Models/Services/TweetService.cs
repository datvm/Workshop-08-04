using Microsoft.EntityFrameworkCore;
using ServiceSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Api.Models.Entities;
using Workshop.Api.Models.ViewModels;

namespace Workshop.Api.Models.Services
{

    public interface ITweetService : IService
    {

        Task<IEnumerable<BasicTweetViewModel>> GetUserTweets(int userId);

    }

    public class TweetService : ITweetService, IService<ITweetService>
    {

        WorkshopContext workshopContext;

        public TweetService(WorkshopContext workshopContext)
        {
            this.workshopContext = workshopContext;
        }

        public async Task<IEnumerable<BasicTweetViewModel>> GetUserTweets(int userId)
        {
            return await this.workshopContext.Tweet
                .AsNoTracking()
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedTime)
                .Select(q => new BasicTweetViewModel()
                {
                    Id = q.Id,
                    CreatedTime = q.CreatedTime,
                    Content = q.Content,
                    Title = q.Title,
                    Slug = q.Slug,
                    UserId = q.UserId,
                })
                .ToListAsync();
        }

    }
}
