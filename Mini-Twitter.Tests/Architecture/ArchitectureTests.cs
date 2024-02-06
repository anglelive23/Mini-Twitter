using FluentAssertions;
using NetArchTest.Rules;

namespace Mini_Twitter.Tests.Architecture
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Mini_Twitter.Domain";
        private const string ApplicationNamespace = "Min_Twitter.Application";
        private const string InfrastuctureNamespace = "Mini_Twitter.Infrastructure";
        private const string APINamespace = "Mini_Twitter.API";

        [Fact]
        public void Domain_Should_Not_HaveDependancyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var dependencies = new[]
            {
                ApplicationNamespace,
                InfrastuctureNamespace,
                APINamespace
            };

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(dependencies)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependancyOnAPIAndInfrastructure()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var dependencies = new[]
            {
                APINamespace,
                InfrastuctureNamespace
            };

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(dependencies)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependancyOnAPIAndDomain()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                APINamespace,
                DomainNamespace
            };

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void API_Should_Not_HaveDependancyOnDomain()
        {
            // Arrange
            var assembly = typeof(API.AssemblyReference).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void API_Controllers_Should_HaveDependancyOnMediatR()
        {
            // Arrange
            var assembly = typeof(API.AssemblyReference).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .Should()
                .HaveDependencyOn("MediatR")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
