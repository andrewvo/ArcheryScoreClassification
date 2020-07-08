using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using System;
using ArcheryScoreClassification.Helpers;
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
            var classification = AutoFixture.Create<string>();
            Mocker.GetMock<IGetClassificationFromScore>().Setup(gcfs => gcfs.GetClassification(request.Score))
                .Returns(classification);
            //Act
            var result = subject.GetClassification(request);
            //Assert
            result.Message.Should().Be(classification);
            result.Request.Should().BeEquivalentTo(request);
        }
    }
}
