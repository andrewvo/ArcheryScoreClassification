using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheryScoreClassification.Helpers
{
    public class GetClosestClassification : IGetClosestClassification
    {
        public string Get(int score, Dictionary<string, int> classificationScores)
        {
            classificationScores.Add("Unclassified", 0);

            var classificationScore = classificationScores.Where(item => item.Value <= score)
                .OrderByDescending(item => item.Value)
                .First();

            return classificationScore.Key;
        }
    }
}
