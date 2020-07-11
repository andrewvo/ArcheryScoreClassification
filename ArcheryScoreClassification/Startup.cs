using ArcheryScoreClassification.Configuration;
using ArcheryScoreClassification.Helpers;
using ArcheryScoreClassification.Strategies;
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
            services.Configure<FitaMensClassificationScoresConfig>(configuration.GetSection("FitaMensClassificationScoresConfig"));
            services.Configure<YorkClassificationScoresConfig>(configuration.GetSection("YorkClassificationScoresConfig"));
            services.AddTransient<ILambdaConfiguration, LambdaConfiguration>();
            services.AddTransient<IGetClassificationFromScore, GetClassificationFromScore>();
            services.AddTransient<IClassificationScoresForParticularRoundStrategyFactory, ClassificationScoresForParticularRoundStrategyFactory>();
            services.AddTransient<IClassificationScoresForParticularRoundStrategy, FitaMensRoundStrategy>();
            services.AddTransient<IClassificationScoresForParticularRoundStrategy, YorkRoundStrategy>();
            return services;
        }
    }
}
