using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArcheryScoreClassification
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public static IServiceCollection Container => ConfigureServices(LambdaConfiguration.Configuration);
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        private static IServiceCollection ConfigureServices(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            services.Configure<ClassificationScoresConfig>(configuration.GetSection("ClassificationScoresConfig"));
            services.AddTransient<ILambdaConfiguration, LambdaConfiguration>();
            services.AddTransient<IGetClassificationFromScore, GetClassificationFromScore>();

            return services;
        }
    }
}
