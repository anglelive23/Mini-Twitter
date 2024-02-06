using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Application.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tweet, TweetDto>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<Reply, ReplyDto>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<Retweet, RetweetDto>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForAllMembers(o => o.ExplicitExpansion());
        }
    }
}
