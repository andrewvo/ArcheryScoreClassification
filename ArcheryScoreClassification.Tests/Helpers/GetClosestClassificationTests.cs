using ArcheryScoreClassification.Helpers;
using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ArcheryScoreClassification.Tests.Helpers
{
    public class GetClosestClassificationTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public GetClosestClassificationTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenGetClosestClassification()
        {
            //Arrange
            var subject = Mocker.CreateInstance<GetClosestClassification>();
            var classificationScores = AutoFixture.Create<Dictionary<string, int>>();
            var randomNumberGenerator = new Random();
            var score = randomNumberGenerator.Next(classificationScores.Values.Min(), classificationScores.Values.Max());
            var classificationScore = classificationScores.Where(item => item.Value <= score).OrderByDescending(item => item.Value).First();
            
            //Act
            var result = subject.Get(score, classificationScores);
            //Assert
            result.Should().Be(classificationScore.Key);
        }

        [Fact]
        public void WhenGetClosestClassificationAndScoreIsWithinABoundary()
        {
            //Arrange
            var subject = Mocker.CreateInstance<GetClosestClassification>();
            var score = 200;
            var classificationScores = new Dictionary<string, int>();
            classificationScores.Add("testClassification1", 100);
            classificationScores.Add("testClassification2", 200);
            classificationScores.Add("testClassification3", 300);

            //Act
            var result = subject.Get(score, classificationScores);
            //Assert
            result.Should().Be("testClassification2");
        }

        [Fact]
        public void WhenGetClosestClassificationAndScoreIsLowerThanThirdClass()
        {
            //Arrange
            var subject = Mocker.CreateInstance<GetClosestClassification>();
            var score = 1;
            var classificationScores = new Dictionary<string, int>();
            classificationScores.Add("testClassification1", 100);
            classificationScores.Add("testClassification2", 200);
            classificationScores.Add("testClassification3", 300);

            //Act
            var result = subject.Get(score, classificationScores);
            //Assert
            result.Should().Be("Unclassified");
        }
    }
}
