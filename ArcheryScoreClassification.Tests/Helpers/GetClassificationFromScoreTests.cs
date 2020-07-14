using Amazon.Lambda.APIGatewayEvents;
using ArcheryScoreClassification.Helpers;
using ArcheryScoreClassification.Strategies;
using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace ArcheryScoreClassification.Tests.Helpers
{
    public class GetClassificationFromScoreTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public GetClassificationFromScoreTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenGetClassificationFromScore()
        {
            //Arrange
            var subject = Mocker.CreateInstance<GetClassificationFromScore>();
            var roundName = AutoFixture.Create<string>();
            var proxyResponse = AutoFixture.Create<APIGatewayProxyResponse>();
            var score = AutoFixture.Create<int>();

            Mocker.GetMock<IClassificationForParticularRoundStrategyFactory>()
                .Setup(factory => factory.GetStrategy(roundName))
                .Returns(Mocker.GetMock<IClassificationForParticularRoundStrategy>().Object);
            Mocker.GetMock<IClassificationForParticularRoundStrategy>().Setup(strat => strat.GetClassification(score)).Returns(proxyResponse);

            //Act
            var result = subject.GetClassification(score, roundName);
            //Assert
            result.Should().Be(proxyResponse);
        }

        [Fact]
        public void WhenGetClassificationButStrategyFactoryReturnsNull()
        {
            //Arrange
            var subject = Mocker.CreateInstance<GetClassificationFromScore>();
            var roundName = AutoFixture.Create<string>();
            var score = AutoFixture.Create<int>();

            Mocker.GetMock<IClassificationForParticularRoundStrategyFactory>()
                .Setup(factory => factory.GetStrategy(roundName))
                .Returns((IClassificationForParticularRoundStrategy)null);

            //Act
            var result = subject.GetClassification(score, roundName);

            //Assert
            result.Body.Should().Be("Round does not exist, or has not been implemented yet");
            result.StatusCode.Should().Be(404);
        }
    }
}