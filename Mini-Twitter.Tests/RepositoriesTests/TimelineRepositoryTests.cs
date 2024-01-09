using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Mini_Twitter.Application.Abstractions;
using Mini_Twitter.Domain.Entities;
using Mini_Twitter.Infrastructure;
using Mini_Twitter.Infrastructure.Repositories;
using Mini_Twitter.Tests.Data;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mini_Twitter.Tests.RepositoriesTests
{
    public class TimelineRepositoryTests
    {
        [Theory]
        [InlineData("randomId")]
        public void TimelineRepository_GetTimeLineForAUser_ReturnsTimeLine_ForExistingUser(string userId)
        {
            // Arrange
            var contextMock = new Mock<TwitterContext>();
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(
                s => s.IsExistingUser(userId))
                .Returns(true);
            userServiceMock.Setup(s => s.GetFollowersList(userId))
                .Returns(DataFaker.GetFollowersIdList(userId));

            contextMock.Setup(c => c.Tweets)
                .ReturnsDbSet(DataFaker.GetTweets());

            var repo = new TimelineRepository(contextMock.Object, userServiceMock.Object);

            // Act
            var result = repo.GetTimeLineForAUser(userId, 1, 10);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(4); // 4 based on the fake data i made, rather comment this assert for better testability with different page/page size
        }

        [Theory]
        [InlineData("fakeId")]
        public void TimelineRepository_GetTimeLineForAUser_ReturnsNull_ForNoneExistingUser(string userId)
        {
            // Arrange
            var contextMock = new Mock<TwitterContext>();
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(
                s => s.IsExistingUser(userId))
                .Returns(false);
            var repo = new TimelineRepository(contextMock.Object, userServiceMock.Object);
            // Act
            var result = repo.GetTimeLineForAUser(userId, 1, 10);

            // Assert
            result.Should().BeNull();
        }
    }
}
