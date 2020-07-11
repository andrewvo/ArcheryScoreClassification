using ArcheryScoreClassification.Configuration;
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

            Mocker.GetMock<IOptions<FitaMensClassificationScoresConfig>>().Setup(op => op.Value).Returns(fitaMensClassificationScoresConfig);
            //Act
            var result = subject.GetClassificationScores();
            //Assert
            result.Should().BeEquivalentTo(fitaMensClassificationScoresConfig.FitaMensClassificationScores);
        }
    }
}
