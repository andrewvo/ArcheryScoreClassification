using System;
using System.Linq;
using Amazon.Lambda.APIGatewayEvents;
using ArcheryScoreClassification.Configuration;
using Microsoft.Extensions.Options;

namespace ArcheryScoreClassification.Helpers
{
    public class GetClassificationFromScore : IGetClassificationFromScore
    {
        private readonly IClassificationForParticularRoundStrategyFactory
            _classificationForParticularRoundStrategyFactory;

        public GetClassificationFromScore(
            IClassificationForParticularRoundStrategyFactory classificationForParticularRoundStrategyFactory)
        {
            _classificationForParticularRoundStrategyFactory = classificationForParticularRoundStrategyFactory;
        }

        public APIGatewayProxyResponse GetClassification(int score, string roundName)
        {
            var classificationScoreStrategy = _classificationForParticularRoundStrategyFactory.GetStrategy(roundName);
            return classificationScoreStrategy != null
                ? classificationScoreStrategy.GetClassification(score)
                : new APIGatewayProxyResponse
                    {Body = "Round does not exist, or has not been implemented yet", StatusCode = 404};
        }
    }
}