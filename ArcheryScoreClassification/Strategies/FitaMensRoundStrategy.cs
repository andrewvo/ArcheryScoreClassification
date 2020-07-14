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

        public string GetClassification(int score)
        {
            var classificationScores = _fitaMensClassificationScoreConfig.Value.FitaMensClassificationScores;
            return  _getClosestClassification.Get(score, classificationScores);
        }
    }
}
