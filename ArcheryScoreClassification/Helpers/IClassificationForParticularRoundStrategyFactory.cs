using ArcheryScoreClassification.Strategies;

namespace ArcheryScoreClassification.Helpers
{
    public interface IClassificationForParticularRoundStrategyFactory
    {
        IClassificationForParticularRoundStrategy GetStrategy(string roundName); 
    }
}