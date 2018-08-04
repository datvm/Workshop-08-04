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

    public interface IUserService : IService
    {

        Task<User> GetByGoogleUserAsync(GoogleUserInfoViewModel userInfo);

    }

    public class UserService : IUserService, IService<IUserService>
    {

        WorkshopContext dbContext;

        public UserService(WorkshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetByGoogleUserAsync(GoogleUserInfoViewModel userInfo)
        {
            if (userInfo.Email.IsNullOrEmpty())
            {
                throw new Exception("NoEmail");
            }

            var user = await this.dbContext.User
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.GoogleId == userInfo.GoogleId);

            if (user == null)
            {
                user = new User()
                {
                    Email = userInfo.Email,
                    GoogleId = userInfo.GoogleId,
                };

                this.dbContext.User.Add(user);
                await this.dbContext.SaveChangesAsync();
            }

            return user;
        }

    }

}
