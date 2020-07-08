using System;
using System.Linq;
using ArcheryScoreClassification.Configuration;
using Microsoft.Extensions.Options;

namespace ArcheryScoreClassification.Helpers
{
    public class GetClassificationFromScore : IGetClassificationFromScore
    {
        private readonly IOptions<ClassificationScoresConfig> _classificationScoreConfig;

        public GetClassificationFromScore(IOptions<ClassificationScoresConfig> classificationScoreConfig)
        {
            _classificationScoreConfig = classificationScoreConfig;
        }
        public string GetClassification(int score)
        {
            var classification = "";
            var classificationScores = _classificationScoreConfig.Value.ClassificationScores;

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