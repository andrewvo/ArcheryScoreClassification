using ArcheryScoreClassification.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheryScoreClassification.Helpers
{
    public class ClassificationForParticularRoundStrategyFactory : IClassificationForParticularRoundStrategyFactory
    {
        private readonly IEnumerable<IClassificationForParticularRoundStrategy> _classificationScoreStrategies;

        public ClassificationForParticularRoundStrategyFactory(IEnumerable<IClassificationForParticularRoundStrategy> classificationScoreStrategies)
        {
            _classificationScoreStrategies = classificationScoreStrategies;
        }
        public IClassificationForParticularRoundStrategy GetStrategy(string roundName)
        {
             return _classificationScoreStrategies.FirstOrDefault(strategy => strategy.CanHandle(roundName));
        }
    }
}
