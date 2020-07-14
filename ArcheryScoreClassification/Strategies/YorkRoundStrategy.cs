using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;

namespace ArcheryScoreClassification.Strategies
{
    public class YorkRoundStrategy : IClassificationForParticularRoundStrategy
    {
        private readonly IOptions<YorkClassificationScoresConfig> _yorkClassificationScoresConfig;
        private readonly IGetClosestClassification _getClosestClassification;

        public YorkRoundStrategy(IOptions<YorkClassificationScoresConfig> yorkClassificationScoresConfig, IGetClosestClassification getClosestClassification)
        {
            _yorkClassificationScoresConfig = yorkClassificationScoresConfig;
            _getClosestClassification = getClosestClassification;
        }
        public bool CanHandle(string roundName)
        {
            return roundName == "York" ? true : false;
        }

        public APIGatewayProxyResponse GetClassification(int score)
        {
            var classificationScores = _yorkClassificationScoresConfig.Value.YorkClassificationScores;
            var classification = _getClosestClassification.Get(score ,classificationScores);
            return new APIGatewayProxyResponse{Body = classification, StatusCode = 200};
        }
    }
}