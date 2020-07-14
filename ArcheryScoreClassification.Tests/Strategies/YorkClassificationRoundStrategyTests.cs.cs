using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
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
            var score = AutoFixture.Create<int>();
            var classification = AutoFixture.Create<string>();
            Mocker.GetMock<IOptions<YorkClassificationScoresConfig>>().Setup(op => op.Value).Returns(yorkClassificationScoresConfig);
            Mocker.GetMock<IGetClosestClassification>().Setup(ccs => ccs.Get(score, yorkClassificationScoresConfig.YorkClassificationScores)).Returns(classification);
            //Act
            var result = subject.GetClassification(score);
            //Assert
            result.Body.Should().Be(classification);
            result.StatusCode.Should().Be(200);
        }
    }
}
