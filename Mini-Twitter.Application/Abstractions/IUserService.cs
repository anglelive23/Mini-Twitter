using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Application.Abstractions
{
    public interface IUserService
    {
        List<string> GetFollowersList(string userId);
        bool IsExistingUser(string userId);
    }
}
