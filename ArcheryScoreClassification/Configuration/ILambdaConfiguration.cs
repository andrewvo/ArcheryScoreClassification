using Microsoft.Extensions.Configuration;

namespace ArcheryScoreClassification.Configuration
{
    public interface ILambdaConfiguration
    {
        IConfigurationRoot Configuration { get; }
    }
}
