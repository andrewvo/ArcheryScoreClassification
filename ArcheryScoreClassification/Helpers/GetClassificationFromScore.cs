using System;
using System.Linq;
using ArcheryScoreClassification.Configuration;
using Microsoft.Extensions.Options;

namespace ArcheryScoreClassification.Helpers
{
    public class GetClassificationFromScore : IGetClassificationFromScore
    {
        private readonly IOptions<FitaMensClassificationScoresConfig> _classificationScoreConfig;
        private readonly IClassificationScoresForParticularRoundStrategyFactory _classificationScoresForParticularRoundStrategyFactory;

        public GetClassificationFromScore(IOptions<FitaMensClassificationScoresConfig> classificationScoreConfig, IClassificationScoresForParticularRoundStrategyFactory classificationScoresForParticularRoundStrategyFactory)
        {
            _classificationScoreConfig = classificationScoreConfig;
            _classificationScoresForParticularRoundStrategyFactory = classificationScoresForParticularRoundStrategyFactory;
        }
        public string GetClassification(int score, string roundName)
        {
            var classification = "";
            var classificationScoreStrategy = _classificationScoresForParticularRoundStrategyFactory.GetStrategy(roundName);

            var classificationScores = classificationScoreStrategy.GetClassificationScores();

            var scores = classificationScores.Values;

            var closestClassificationScore = scores.OrderBy(item => Math.Abs(score - item)).First();

            foreach (var classificationScore in classificationScores)
            {
                if (classificationScore.Value == closestClassificationScore)
                {
                    classification = classificationScore.Key;
                }
            }

            return classification;
        }
    }
}