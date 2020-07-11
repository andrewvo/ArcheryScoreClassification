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
            var expectedStrategy = new Mock<IClassificationScoresForParticularRoundStrategy>();
            var unexpectedStrategy = new Mock<IClassificationScoresForParticularRoundStrategy>();

            Mocker.Use(new[]{
                expectedStrategy.Object,
                unexpectedStrategy.Object}
            .AsEnumerable());

            var subject = Mocker.CreateInstance<ClassificationScoresForParticularRoundStrategyFactory>();
            var roundName = AutoFixture.Create<string>();
            expectedStrategy.Setup(strategy => strategy.CanHandle(roundName)).Returns(true);
            unexpectedStrategy.Setup(strategy => strategy.CanHandle(roundName)).Returns(false);

            //Act
            var response = subject.GetStrategy(roundName);
            //Assert
            response.Should().Be(expectedStrategy.Object);
        }
    }
}
