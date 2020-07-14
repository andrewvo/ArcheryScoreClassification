using Amazon.Lambda.APIGatewayEvents;
using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
using Microsoft.Extensions.Options;

namespace ArcheryScoreClassification.Strategies
{
    public class FitaMensRoundStrategy : IClassificationForParticularRoundStrategy
    {
        private readonly IOptions<FitaMensClassificationScoresConfig> _fitaMensClassificationScoreConfig;
        private readonly IGetClosestClassification _getClosestClassification;

        public FitaMensRoundStrategy(IOptions<FitaMensClassificationScoresConfig> fitaMensClassificationScoreConfig, IGetClosestClassification getClosestClassification)
        {
            _fitaMensClassificationScoreConfig = fitaMensClassificationScoreConfig;
            _getClosestClassification = getClosestClassification;
        }
        public bool CanHandle(string roundName)
        {
            return roundName == "FITA Mens" ? true : false;
        }

        public APIGatewayProxyResponse GetClassification(int score)
        {
            var classificationScores = _fitaMensClassificationScoreConfig.Value.FitaMensClassificationScores;
            var classification = _getClosestClassification.Get(score, classificationScores);
            return new APIGatewayProxyResponse{Body = classification, StatusCode = 200};
        }
    }
}
