using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OutputCaching;
using Mini_Twitter.Application.Features.Tweets.Commands.CreateTweet;
using Mini_Twitter.Application.Features.Tweets.Commands.CreateTweetReply;
using Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweet;
using Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweetReply;
using Mini_Twitter.Application.Features.Tweets.Queries.GetTweetDetails;
using Mini_Twitter.Application.Features.Tweets.Queries.GetTweetsList;
using Mini_Twitter.Application.Models.Dtos;
using Mini_Twitter.API.Controllers;
using Mini_Twitter.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Tests.Controllers
{
    public class TweetsControllerTests
    {
        [Fact]
        public async Task TweetsController_GetAllTweets_Should_Return_OkResult()
        {
            // Arrange
            var tweetsList = new Mock<List<Tweet>>();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTweetsListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(tweetsList.Object.AsQueryable());

            var controller = new TweetsController(mediatorMock.Object);

            // Act
            var result = await controller.GetAllTweets();

            // Assert
            ((OkObjectResult)result).Value.Should().BeAssignableTo<IQueryable<Tweet>>();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task TweetsController_GetTweetById_Should_Return_OkResult()
        {
            // Arrange
            var key = 0;
            var tweetsList = new Mock<List<Tweet>>();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTweetDetailsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(tweetsList.Object.AsQueryable());

            var controller = new TweetsController(mediatorMock.Object);

            // Act
            var result = await controller.GetTweetById(key);

            // Assert
            ((OkObjectResult)result).Value.Should().BeOfType<SingleResult<Tweet>>();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task TweetsController_AddTweet_Should_Return_CreatedResult()
        {
            // Arrange
            var tweetDto = new CreateTweetDto
            {
                Context = "Random context",
                UserId = "Random guid"
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.Is<CreateTweetCommand>(cmd => cmd.TweetDto == tweetDto),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Tweet
                {
                    Context = tweetDto.Context,
                    UserId = tweetDto.UserId
                });

            var cacheMock = new Mock<IOutputCacheStore>();
            var controller = new TweetsController(mediatorMock.Object);

            // Act
            var result = await controller.AddTweet(tweetDto, cacheMock.Object, CancellationToken.None);
            cacheMock.Verify(c => c.EvictByTagAsync("Tweets", It.IsAny<CancellationToken>()), Times.Once);

            // Assert
            result.Should().BeOfType<CreatedODataResult<Tweet>>();
            ((CreatedODataResult<Tweet>)result).Value.Should().BeEquivalentTo(tweetDto);
        }

        [Theory]
        [InlineData(1, "Reply content", "UserId")]
        public async Task TweetsController_AddReplyForTweet_Should_Return_CreatedResult(int tweetKey, string replyContext, string userId)
        {
            // Arrange
            var replyDto = new CreateReplyDto
            {
                Context = replyContext,
                UserId = userId
            };
            var mediatorMock = new Mock<IMediator>();

            mediatorMock
                .Setup(m => m.Send(
                It.Is<CreateTweetReplyCommand>(cmd => cmd.Id == tweetKey && cmd.ReplyDto == replyDto),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Reply());

            var controller = new TweetsController(mediatorMock.Object);
            var cacheMock = new Mock<IOutputCacheStore>();

            // Act
            var result = await controller.AddReplyForTweet(tweetKey, replyDto, cacheMock.Object, CancellationToken.None);
            cacheMock.Verify(c => c.EvictByTagAsync("Tweets", It.IsAny<CancellationToken>()), Times.Once());

            // Assert
            result.Should().BeOfType<CreatedODataResult<Reply>>();
            ((CreatedODataResult<Reply>)result).Value.Should().BeOfType(typeof(Reply));
        }

        [Theory]
        [InlineData(1, "New tweet Context")]
        public async Task TweetsController_UpdateTweet_Should_Return_NoContentResult(int tweetKey, string newContext)
        {
            // Arrange
            var updatedTweetDto = new UpdateTweetDto
            {
                Context = newContext
            };

            var mediatorMock = new Mock<IMediator>();
            var cacheMock = new Mock<IOutputCacheStore>();

            mediatorMock.Setup(m => m.Send(It.Is<UpdateTweetCommand>(cmd => cmd.UpdateTweetDto == updatedTweetDto & cmd.Id == tweetKey), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Tweet());

            var controller = new TweetsController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateTweet(tweetKey, updatedTweetDto, cacheMock.Object, CancellationToken.None);
            cacheMock.Verify(c => c.EvictByTagAsync("Tweets", It.IsAny<CancellationToken>()), Times.Once());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            ((NoContentResult)result).StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Theory]
        [InlineData(1, 1, "reply update context")]
        public async Task TweetsController_UpdateReplyForTweet_Should_Return_NoContentResult(int tweetKey, int replyKey, string replyUpdatedContext)
        {
            // Arrange
            var replyDto = new UpdateTweetReplyDto
            {
                Context = replyUpdatedContext
            };

            var command = new UpdateTweetReplyCommand
            {
                TweetId = tweetKey,
                ReplyId = replyKey,
                UpdateTweetReplyDto = replyDto
            };

            var mediatorMock = new Mock<IMediator>();
            var cacheMock = new Mock<IOutputCacheStore>();

            mediatorMock
                .Setup(m => m.Send(It.Is<UpdateTweetReplyCommand>(cmd => cmd.TweetId == command.TweetId && cmd.ReplyId == command.ReplyId && cmd.UpdateTweetReplyDto == command.UpdateTweetReplyDto),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Reply());

            var controller = new TweetsController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateReplyForTweet(tweetKey, replyKey, replyDto, cacheMock.Object, CancellationToken.None);
            cacheMock.Verify(c => c.EvictByTagAsync("Tweets", It.IsAny<CancellationToken>()), Times.Once());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            ((NoContentResult)result).StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
