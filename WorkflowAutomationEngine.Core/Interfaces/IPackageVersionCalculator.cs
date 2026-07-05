using NuGet.Versioning;

namespace WorkflowAutomationEngine.Core.Interfaces
{
    public interface IPackageVersionCalculator
    {
        NuGetVersion GetNextVersion(
        string versionPrefix,
        IReadOnlyList<NuGetVersion> publishedVersions);
    }
}
