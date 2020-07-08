using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
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
            var score = AutoFixture.Create<int>();
            var classificationScoresConfig = AutoFixture.Create<ClassificationScoresConfig>();
            var expectedResult = "";
            Mocker.GetMock<IOptions<ClassificationScoresConfig>>().Setup(option => option.Value)
                .Returns(classificationScoresConfig);

            var classificationScores = classificationScoresConfig.ClassificationScores.Values;
            var closestClassificationScore = classificationScores.OrderBy(item => Math.Abs(score - item)).First();

            foreach (var classificationScore in classificationScoresConfig.ClassificationScores)
            {
                if (classificationScore.Value == closestClassificationScore)
                {
                    expectedResult = classificationScore.Key;
                }
            }
            //Act
            var result = subject.GetClassification(score);
            //Assert
            result.Should().Be(expectedResult);
        }
    }
}