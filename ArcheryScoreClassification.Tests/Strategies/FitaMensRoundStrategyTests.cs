using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
using ArcheryScoreClassification.Strategies;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ArcheryScoreClassification.Tests.Strategies
{
    public class FitaMensRoundStrategyTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public FitaMensRoundStrategyTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenCanHandle()
        {
            //Arrange
            var subject = Mocker.CreateInstance<FitaMensRoundStrategy>();
            var roundName = "FITA Mens";
            //Act
            var result = subject.CanHandle(roundName);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void WhenCanHandleAndRoundNameIsNotCorrect()
        {
            //Arrange
            var subject = Mocker.CreateInstance<FitaMensRoundStrategy>();
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
            var subject = Mocker.CreateInstance<FitaMensRoundStrategy>();
            var fitaMensClassificationScoresConfig = AutoFixture.Create<FitaMensClassificationScoresConfig>();
            var score = AutoFixture.Create<int>();
            var classification = AutoFixture.Create<string>();
            Mocker.GetMock<IOptions<FitaMensClassificationScoresConfig>>().Setup(op => op.Value).Returns(fitaMensClassificationScoresConfig);
            Mocker.GetMock<IGetClosestClassification>().Setup(ccs => ccs.Get(score, fitaMensClassificationScoresConfig.FitaMensClassificationScores)).Returns(classification);

            //Act
            var result = subject.GetClassification(score);
            //Assert
            result.Body.Should().Be(classification);
            result.StatusCode.Should().Be(200);
        }
    }
}
