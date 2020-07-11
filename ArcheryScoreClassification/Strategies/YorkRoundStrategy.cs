using ArcheryScoreClassification.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace ArcheryScoreClassification.Strategies
{
    public class YorkRoundStrategy : IClassificationScoresForParticularRoundStrategy
    {
        private readonly IOptions<YorkClassificationScoresConfig> _yorkClassificationScoresConfig;

        public YorkRoundStrategy(IOptions<YorkClassificationScoresConfig> yorkClassificationScoresConfig)
        {
            _yorkClassificationScoresConfig = yorkClassificationScoresConfig;
        }
        public bool CanHandle(string roundName)
        {
            return roundName == "York" ? true : false;
        }

        public Dictionary<string, int> GetClassificationScores()
        {
            return _yorkClassificationScoresConfig.Value.YorkClassificationScores;
        }
    }
}