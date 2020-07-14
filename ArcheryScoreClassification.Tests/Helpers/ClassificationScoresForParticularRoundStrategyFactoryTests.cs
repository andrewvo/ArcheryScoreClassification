using ArcheryScoreClassification.Helpers;
using ArcheryScoreClassification.Strategies;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ArcheryScoreClassification.Tests.Helpers
{
    public class ClassificationScoresForParticularRoundStrategyFactoryTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public ClassificationScoresForParticularRoundStrategyFactoryTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenGetStrategy()
        {
            //Arrange
            var expectedStrategy = new Mock<IClassificationForParticularRoundStrategy>();
            var unexpectedStrategy = new Mock<IClassificationForParticularRoundStrategy>();

            Mocker.Use(new[]{
                expectedStrategy.Object,
                unexpectedStrategy.Object}
            .AsEnumerable());

            var subject = Mocker.CreateInstance<ClassificationForParticularRoundStrategyFactory>();
            var roundName = AutoFixture.Create<string>();
            expectedStrategy.Setup(strategy => strategy.CanHandle(roundName)).Returns(true);
            unexpectedStrategy.Setup(strategy => strategy.CanHandle(roundName)).Returns(false);

            //Act
            var response = subject.GetStrategy(roundName);
            //Assert
            response.Should().Be(expectedStrategy.Object);
        }

        [Fact]
        public void WhenGetStrategyAndRoundNameDoesNotExist()
        {
            //Arrange
            var unexpectedStrategy1 = new Mock<IClassificationForParticularRoundStrategy>();
            var unexpectedStrategy2 = new Mock<IClassificationForParticularRoundStrategy>();

            Mocker.Use(new[]{
                unexpectedStrategy1.Object,
                unexpectedStrategy2.Object}
            .AsEnumerable());

            var subject = Mocker.CreateInstance<ClassificationForParticularRoundStrategyFactory>();
            var roundName = AutoFixture.Create<string>();
            unexpectedStrategy1.Setup(strategy => strategy.CanHandle(roundName)).Returns(false);
            unexpectedStrategy2.Setup(strategy => strategy.CanHandle(roundName)).Returns(false);

            //Act
            var error = Assert.Throws<Exception>(() => subject.GetStrategy(roundName));
            //Assert
            error.Message.Should().Be("Round does not exist, or has not been implemented yet");
        }
    }
}
