using ArcheryScoreClassification.Strategies;

namespace ArcheryScoreClassification.Helpers
{
    public interface IClassificationScoresForParticularRoundStrategyFactory
    {
        IClassificationScoresForParticularRoundStrategy GetStrategy(string roundName); 
    }
}