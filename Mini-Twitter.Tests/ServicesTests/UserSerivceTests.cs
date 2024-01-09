using Mini_Twitter.Infrastructure.Repositories;
using Mini_Twitter.Infrastructure;
using Mini_Twitter.Tests.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using Mini_Twitter.Infrastructure.Services;
using FluentAssertions;

namespace Mini_Twitter.Tests.ServicesTests
{
    public class UserSerivceTests
    {
        [Theory]
        [InlineData("randomId")]
        public void UserService_GetFollowersList_ReturnsList_ForExistingUser(string userId)
        {
            // Arrange
            var contextMock = new Mock<TwitterContext>();
            contextMock.Setup(c => c.UserFollowers)
                .ReturnsDbSet(DataFaker.GetFollowersList());

            var service = new UserService(contextMock.Object);

            // Act
            var result = service.GetFollowersList(userId);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }

        [Theory]
        [InlineData("noneExistUserId")]
        public void UserService_GetFollowersList_ReturnsEmptyList_ForNoneEsitingUser(string userId)
        {
            // Arrange
            var contextMock = new Mock<TwitterContext>();
            contextMock.Setup(c => c.UserFollowers)
                .ReturnsDbSet(DataFaker.GetFollowersList());

            var service = new UserService(contextMock.Object);

            // Act
            var result = service.GetFollowersList(userId);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("randomId")]
        public void UserService_IsExistingUser_ReturnsTrue_ForExistingUser(string userId)
        {
            // Arrange
            var contextMock = new Mock<TwitterContext>();
            contextMock.Setup(c => c.Users)
                .ReturnsDbSet(DataFaker.GetUsers());
            var service = new UserService(contextMock.Object);

            // Act
            var result = service.IsExistingUser(userId);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("noneExistUserId")]
        public void UserService_IsExistingUser_ReturnsFalse_ForNonexistentUser(string userId)
        {
            // Arrange
            var contextMock = new Mock<TwitterContext>();
            contextMock.Setup(c => c.Users)
                .ReturnsDbSet(DataFaker.GetUsers());
            var serivce = new UserService(contextMock.Object);

            // Act
            var result = serivce.IsExistingUser(userId);

            // Assert
            result.Should().BeFalse();
        }
    }
}
