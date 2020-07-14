using System.Collections.Generic;

namespace ArcheryScoreClassification.Strategies
{
    public interface IClassificationForParticularRoundStrategy
    {
        bool CanHandle(string roundName);
        string GetClassification(int score);
    }
}