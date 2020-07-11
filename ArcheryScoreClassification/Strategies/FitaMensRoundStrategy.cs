using ArcheryScoreClassification.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoreClassification.Strategies
{
    public class FitaMensRoundStrategy : IClassificationScoresForParticularRoundStrategy
    {
        private readonly IOptions<FitaMensClassificationScoresConfig> _fitaMensClassificationScoreConfig;

        public FitaMensRoundStrategy(IOptions<FitaMensClassificationScoresConfig> fitaMensClassificationScoreConfig)
        {
            _fitaMensClassificationScoreConfig = fitaMensClassificationScoreConfig;
        }
        public bool CanHandle(string roundName)
        {
            return roundName == "FITA Mens" ? true : false;
        }

        public Dictionary<string, int> GetClassificationScores()
        {
            return _fitaMensClassificationScoreConfig.Value.FitaMensClassificationScores;
        }
    }
}
