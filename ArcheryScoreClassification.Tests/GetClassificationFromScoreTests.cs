using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
using ArcheryScoreClassification.Strategies;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Xunit;

namespace ArcheryScoreClassification.Tests
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
            var classificationScores = AutoFixture.Create<Dictionary<string,int>>();
            var expectedResult = "";
            var score = AutoFixture.Create<int>();

            Mocker.GetMock<IClassificationScoresForParticularRoundStrategyFactory>()
                .Setup(factory => factory.GetStrategy(roundName))
                .Returns(Mocker.GetMock<IClassificationScoresForParticularRoundStrategy>().Object);
            Mocker.GetMock<IClassificationScoresForParticularRoundStrategy>().Setup(strat => strat.GetClassificationScores()).Returns(classificationScores);

            var classificationScoresWithoutInClassification = classificationScores.Values;
            var closestClassificationScore = classificationScoresWithoutInClassification.OrderBy(item => Math.Abs(score - item)).First();

            foreach (var classificationScore in classificationScores)
            {
                if (classificationScore.Value == closestClassificationScore)
                {
                    expectedResult = classificationScore.Key;
                }
            }
            //Act
            var result = subject.GetClassification(score, roundName);
            //Assert
            result.Should().Be(expectedResult);
        }
    }
}