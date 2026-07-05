using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowAutomationEngine.Core.Models
{
    public sealed class PackageFeedOptions
    {
        public required string FeedUrl { get; init; }

        public required string Username { get; init; }

        public required string Token { get; init; }
    }
}
