using ArcheryScoreClassification.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheryScoreClassification.Helpers
{
    public class ClassificationScoresForParticularRoundStrategyFactory : IClassificationScoresForParticularRoundStrategyFactory
    {
        private readonly IEnumerable<IClassificationScoresForParticularRoundStrategy> _classificationScoreStrategies;

        public ClassificationScoresForParticularRoundStrategyFactory(IEnumerable<IClassificationScoresForParticularRoundStrategy> classificationScoreStrategies)
        {
            _classificationScoreStrategies = classificationScoreStrategies;
        }
        public IClassificationScoresForParticularRoundStrategy GetStrategy(string roundName)
        {
            return _classificationScoreStrategies.First(strategy => strategy.CanHandle(roundName));
        }
    }
}
