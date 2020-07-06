using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using System;
using Xunit;

namespace ArcheryScoreClassification.Tests
{
    public class ScoreClassificationServiceTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }


        public ScoreClassificationServiceTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }
        [Fact]
        public void WhenGet()
        {
            //Arrange
            var subject = Mocker.CreateInstance<ScoreClassificationService>();
            var request = AutoFixture.Create<Request>();
            //Act
            var result = subject.GetClassification(request);
            //Assert
            result.Should().NotBeNull();
        }
    }
}
