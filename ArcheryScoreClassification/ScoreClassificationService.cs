using Amazon.Lambda.Core;
using ArcheryScoreClassification.Helpers;
using ArcheryScoreClassification.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

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

        public Response GetClassification(Request request)
       {
           var getClassificationFromScore = _serviceProvider.GetService<IGetClassificationFromScore>();
           var classification = getClassificationFromScore.GetClassification(request.Score, request.RoundName);
           return new Response(classification, request);
       }
    }
}
