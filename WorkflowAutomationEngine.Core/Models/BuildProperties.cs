using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowAutomationEngine.Core.Models
{
    public sealed class BuildProperties
    {
        public required string PackageId { get; init; }

        public required string VersionPrefix { get; init; }
    }
}
