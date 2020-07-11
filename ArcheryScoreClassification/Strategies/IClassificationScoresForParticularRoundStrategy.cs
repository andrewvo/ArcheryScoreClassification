using System.Collections.Generic;

namespace ArcheryScoreClassification.Strategies
{
    public interface IClassificationScoresForParticularRoundStrategy
    {
        bool CanHandle(string roundName);
        Dictionary<string, int> GetClassificationScores();
    }
}