using Microsoft.EntityFrameworkCore;
using Mini_Twitter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Tests.Data
{
    public class InMemoryDataFaker
    {
        //public TwitterContext GetContext()
        //{
        //    var options = new DbContextOptionsBuilder<TwitterContext>()
        //        .UseInMemoryDatabase(databaseName: "TwitterInMemoryDb")
        //        .Options;

        //    var context = new TwitterContext(options);
        //    context.Database.EnsureCreated();


        //    return context;
        //}
    }
}
