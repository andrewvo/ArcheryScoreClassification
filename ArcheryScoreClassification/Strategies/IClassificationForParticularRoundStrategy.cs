using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;

namespace ArcheryScoreClassification.Strategies
{
    public interface IClassificationForParticularRoundStrategy
    {
        bool CanHandle(string roundName);
        APIGatewayProxyResponse GetClassification(int score);
    }
}