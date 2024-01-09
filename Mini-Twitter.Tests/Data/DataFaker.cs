using Microsoft.OData.ModelBuilder;
using Mini_Twitter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Tests.Data
{
    public static class DataFaker
    {
        public static List<ApplicationUser> GetUsers()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "randomId",
                    UserName = "Ahmed.Allam",
                    IsDeleted = false,
                },
                new ApplicationUser
                {
                    Id = "anotherRandomId",
                    UserName = "Ahmed.Allam",
                    IsDeleted = false,
                },
                new ApplicationUser
                {
                    Id = "thirdRandomId",
                    UserName = "Ahmed.Allam",
                    IsDeleted = false,
                },
            };
        }

        public static List<UserFollowers> GetFollowersList()
        {
            return new List<UserFollowers>
            {
                new UserFollowers { FollowerId = "randomId", FolloweeId = "anotherRandomId" },
                new UserFollowers { FollowerId = "randomId", FolloweeId = "thirdRandomId" },
            };
        }

        public static List<Tweet> GetTweets()
        {
            return new List<Tweet>
            {
                new Tweet
                {
                    Id = Random.Shared.Next(1,100),
                    Context = Guid.NewGuid().ToString(),
                    IsDeleted = false,
                    UserId = "randomId"
                },
                new Tweet
                {
                    Id = Random.Shared.Next(1,100),
                    Context = Guid.NewGuid().ToString(),
                    IsDeleted = false,
                    UserId = "randomId"
                },
                new Tweet
                {
                    Id = Random.Shared.Next(1,100),
                    Context = Guid.NewGuid().ToString(),
                    IsDeleted = false,
                    UserId = "anotherRandomId"
                },
                new Tweet
                {
                    Id = Random.Shared.Next(1,100),
                    Context = Guid.NewGuid().ToString(),
                    IsDeleted = false,
                    UserId = "thirdRandomId"
                }
            };
        }

        public static List<string> GetFollowersIdList(string userId)
        {
            return GetFollowersList()
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId)
                .ToList();
        }
    }
}
