using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mini_Twitter.Application.Features.Timelines.Queries.GetTimeline;
using Mini_Twitter.Controllers;
using Mini_Twitter.Domain.Entities;
using Moq;

namespace Mini_Twitter.Tests.ControllersTests
{
    public class TimeLinesControllerTests
    {
        [Fact]
        public async Task TimeLinesController_GetTimeLineForAUser_Returns_OkResult()
        {
            // Arrange
            var userId = "valid-guid";
            var pageNumber = 1;
            var pageSize = 10;
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTimeLineQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Tweet>());
            var controller = new TimeLinesController(mediatorMock.Object);
            var expectedTweets = new Mock<List<Tweet>>();

            // Act
            var result = await controller.GetTimeLineForAUser(userId, pageNumber, pageSize);

            // Assert
            result.Should().NotBeOfType(typeof(NotFoundResult));
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedTweets.Object);
        }

        [Fact]
        public async Task TimeLinesController_GetTimeLineForAUser_Returns_NotFound_If_UserId_Is_Invalid()
        {
            // Arrange
            var userId = "invalid_guid";
            var pageNumber = 1;
            var pageSize = 10;
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTimeLineQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Tweet>)null);
            var controller = new TimeLinesController(mediatorMock.Object);

            // Act
            var result = await controller.GetTimeLineForAUser(userId, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}
