using NuGet.Versioning;
using WorkflowAutomationEngine.Core.Interfaces;

namespace WorkflowAutomationEngine.Core.Services
{
    public sealed class PackageVersionService
    {
        private readonly IPackageVersionSource _packageVersionSource;
        private readonly IPackageVersionCalculator _packageVersionCalculator;

        public PackageVersionService(IPackageVersionSource packageVersionSource, IPackageVersionCalculator packageVersionCalculator)
        {
            _packageVersionSource = packageVersionSource;
            _packageVersionCalculator = packageVersionCalculator;
        }

        public async Task<NuGetVersion> GetNextVersionAsync(string packageId, string versionPrefix, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<NuGetVersion> publishedVersions = await _packageVersionSource.GetVersionsAsync(packageId, cancellationToken);

            return _packageVersionCalculator.GetNextVersion(versionPrefix, publishedVersions);
        }
    }
}
