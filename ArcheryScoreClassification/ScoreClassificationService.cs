using Amazon.Lambda.Core;
using ArcheryScoreClassification.Helpers;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace ArcheryScoreClassification
{
    public class ScoreClassificationService
    {
        private readonly IGetClassificationFromScore _getClassificationFromScore;

        public ScoreClassificationService(IGetClassificationFromScore getClassificationFromScore)
        {
            _getClassificationFromScore = getClassificationFromScore;
        }
       public Response GetClassification(Request request)
       {
           var classification = _getClassificationFromScore.GetClassification(request.Score);
           return new Response(classification, request);
       }
    }
}
