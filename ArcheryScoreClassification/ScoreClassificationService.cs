using Amazon.Lambda.Core;
using ArcheryScoreClassification.Helpers;
using ArcheryScoreClassification.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Amazon.Lambda.APIGatewayEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace ArcheryScoreClassification
{
    public class ScoreClassificationService
    {
        private readonly ServiceProvider _serviceProvider;

        public ScoreClassificationService() : this(Startup.Container.BuildServiceProvider())
        {
   
        }

        public ScoreClassificationService(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public APIGatewayProxyResponse GetClassification(APIGatewayProxyRequest apiGatewayRequest)
       {
           var request = new Request(int.Parse(apiGatewayRequest.QueryStringParameters["Score"]), apiGatewayRequest.QueryStringParameters["RoundName"]);
           var getClassificationFromScore = _serviceProvider.GetService<IGetClassificationFromScore>();
           var proxyResponse = getClassificationFromScore.GetClassification(request.Score, request.RoundName);
           return proxyResponse;
       }
    }
}
