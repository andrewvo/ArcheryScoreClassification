using Amazon.Lambda.APIGatewayEvents;

namespace ArcheryScoreClassification.Helpers
{
    public interface IGetClassificationFromScore
    {
        APIGatewayProxyResponse GetClassification(int score, string roundName);
    }
}