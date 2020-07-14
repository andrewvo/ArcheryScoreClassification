using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using ArcheryScoreClassification.Helpers;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using ArcheryScoreClassification.Models;
using FluentAssertions.Common;

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
            var serviceCollections = new ServiceCollection();
            var roundName = AutoFixture.Create<string>();
            var score = AutoFixture.Create<int>();
            var queryStringParameters = new Dictionary<string, string>();
            queryStringParameters.Add("Score", score.ToString());
            queryStringParameters.Add("RoundName", roundName);

            var request = AutoFixture.Build<APIGatewayProxyRequest>().With(rq => rq.QueryStringParameters, queryStringParameters).Create();
            var proxyResponse = AutoFixture.Create<APIGatewayProxyResponse>();
            var getClassificationFromScoreMock = new Mock<IGetClassificationFromScore>();
            getClassificationFromScoreMock.Setup(sp => sp.GetClassification(score, roundName)).Returns(proxyResponse);
            serviceCollections.AddScoped(provider => getClassificationFromScoreMock.Object);
            var serviceProvider = serviceCollections.BuildServiceProvider();

            var subject = new ScoreClassificationService(serviceProvider);
            //Act
            var result = subject.GetClassification(request);
            //Assert
            result.Should().Be(proxyResponse);
        }
    }
}
