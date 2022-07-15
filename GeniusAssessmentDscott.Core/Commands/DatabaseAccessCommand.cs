using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeniusAssessmentDscott.Core.Commands
{
    public abstract class DatabaseAccessCommand
    {
        protected IConfiguration configuration;
        public string ConnectionString { get; protected set; }

        public DatabaseAccessCommand()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

            ConnectionString = config.GetConnectionString("mainDB");
        }
    }
}
