using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Domain.Entities
{
    public class UserFollowers
    {
        public string FolloweeId { get; set; }
        public ApplicationUser Followee { get; set; }

        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }
    }
}
