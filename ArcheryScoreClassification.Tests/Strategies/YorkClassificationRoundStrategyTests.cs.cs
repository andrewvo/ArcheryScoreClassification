using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Strategies;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Xunit;

namespace ArcheryScoreClassification.Tests.Strategies
{
    public class YorkClassificationRoundStrategyTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public YorkClassificationRoundStrategyTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenCanHandle()
        {
            //Arrange
            var subject = Mocker.CreateInstance<YorkRoundStrategy>();
            var roundName = "York";
            //Act
            var result = subject.CanHandle(roundName);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void WhenCanHandleAndRoundNameIsNotCorrect()
        {
            //Arrange
            var subject = Mocker.CreateInstance<YorkRoundStrategy>();
            var roundName = AutoFixture.Create<string>();

            //Act
            var result = subject.CanHandle(roundName);

            //Assert
            result.Should().BeFalse();
        }


        [Fact]
        public void WhenGetClassificationScores()
        {
            //Arrange
            var subject = Mocker.CreateInstance<YorkRoundStrategy>();
            var yorkClassificationScoresConfig = AutoFixture.Create<YorkClassificationScoresConfig>();

            Mocker.GetMock<IOptions<YorkClassificationScoresConfig>>().Setup(op => op.Value).Returns(yorkClassificationScoresConfig);
            //Act
            var result = subject.GetClassificationScores();
            //Assert
            result.Should().BeEquivalentTo(yorkClassificationScoresConfig.YorkClassificationScores);
        }
    }
}
