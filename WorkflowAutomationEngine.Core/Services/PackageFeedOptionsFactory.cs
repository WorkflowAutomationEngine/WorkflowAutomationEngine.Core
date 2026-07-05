using Microsoft.Extensions.Configuration;
using WorkflowAutomationEngine.Core.Models;

namespace WorkflowAutomationEngine.Core.Services
{
    public static class PackageFeedOptionsFactory
    {
        public static PackageFeedOptions Create(IConfiguration configuration)
        {
            PackageFeedOptions? options =
                configuration.GetSection("PackageFeed")
                             .Get<PackageFeedOptions>();

            return new PackageFeedOptions
            {
                FeedUrl = options!.FeedUrl,
                Username = Environment.GetEnvironmentVariable("GITHUB_ACTOR") ?? options.Username,
                Token = Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? options.Token
            };
        }
    }
}
