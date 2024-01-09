using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly TwitterContext _context;

        public UserService(TwitterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<string> GetFollowersList(string userId)
        {
            return _context
                .UserFollowers
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId)
                .ToList();
        }

        public bool IsExistingUser(string userId)
        {
            return _context
                .Users
                .Any(u => u.Id == userId && u.IsDeleted == false);
        }
    }
}
