using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowAutomationEngine.Core.Interfaces
{
    public interface IPackageVersionSource
    {
        Task<IReadOnlyList<NuGetVersion>> GetVersionsAsync(
            string packageId,
            CancellationToken cancellationToken = default);
    }
}
