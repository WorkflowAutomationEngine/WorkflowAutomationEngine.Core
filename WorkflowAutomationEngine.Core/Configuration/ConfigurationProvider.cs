using Microsoft.Extensions.Configuration;

namespace WorkflowAutomationEngine.Core.Configuration
{
    public static class ConfigurationProvider
    {
        public static IConfigurationRoot Build()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
