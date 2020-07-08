using System;
using System.Collections.Generic;
using System.Text;
using ArcheryScoreClassification.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArcheryScoreClassification
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ClassificationScoresConfig>(Configuration.GetSection("ClassificationScoresConfig"));

        }


    }
}
