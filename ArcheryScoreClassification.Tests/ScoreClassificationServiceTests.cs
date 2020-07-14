using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using ArcheryScoreClassification.Helpers;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using ArcheryScoreClassification.Models;

namespace ArcheryScoreClassification.Tests
{
    public class ScoreClassificationServiceTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }


        public ScoreClassificationServiceTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }
        [Fact]
        public void WhenGet()
        {
            //Arrange
            var serviceCollections = new ServiceCollection();
            var request = AutoFixture.Create<Request>();
            var classification = AutoFixture.Create<string>();
            var getClassificationFromScoreMock = new Mock<IGetClassificationFromScore>();
            getClassificationFromScoreMock.Setup(sp => sp.GetClassification(request.Score, request.RoundName)).Returns(classification);
            serviceCollections.AddScoped(provider => getClassificationFromScoreMock.Object);
            var serviceProvider = serviceCollections.BuildServiceProvider();

            var subject = new ScoreClassificationService(serviceProvider);
            //Act
            var result = subject.GetClassification(request);
            //Assert
            result.Message.Should().Be(classification);
            result.Request.Should().BeEquivalentTo(request);
        }
    }
}
