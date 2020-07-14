using System;
using System.Linq;
using ArcheryScoreClassification.Configuration;
using Microsoft.Extensions.Options;

namespace ArcheryScoreClassification.Helpers
{
    public class GetClassificationFromScore : IGetClassificationFromScore
    {
        private readonly IClassificationForParticularRoundStrategyFactory _classificationForParticularRoundStrategyFactory;

        public GetClassificationFromScore(IClassificationForParticularRoundStrategyFactory classificationForParticularRoundStrategyFactory)
        {
            _classificationForParticularRoundStrategyFactory = classificationForParticularRoundStrategyFactory;
        }
        public string GetClassification(int score, string roundName)
        {
            var classificationScoreStrategy = _classificationForParticularRoundStrategyFactory.GetStrategy(roundName);
            var classification = classificationScoreStrategy.GetClassification(score);
            return classification;
        }
    }
}