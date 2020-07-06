using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace ArcheryScoreClassification
{
    public class ScoreClassificationService
    {
       public Response GetClassification(Request request)
       {
           return new Response("Go Serverless v1.0! Your function executed successfully!", request);
       }
    }
}
