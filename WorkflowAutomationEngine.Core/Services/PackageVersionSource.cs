using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using WorkflowAutomationEngine.Core.Interfaces;
using WorkflowAutomationEngine.Core.Models;

namespace WorkflowAutomationEngine.Core.Services
{
    public sealed class PackageVersionSource : IPackageVersionSource
    {
        private readonly PackageFeedOptions _options;

        public PackageVersionSource(PackageFeedOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IReadOnlyList<NuGetVersion>> GetVersionsAsync(
            string packageId,
            CancellationToken cancellationToken = default)
        {
            // Setup Package Source and Repository
            var packageSource = new PackageSource(_options.FeedUrl)
            {
                Credentials = new PackageSourceCredential(
                                        source: _options.FeedUrl,
                                        username: _options.Username,
                                        passwordText: _options.Token,
                                        isPasswordClearText: true,
                                        validAuthenticationTypesText: null)
            };

            var repository = Repository.Factory.GetCoreV3(packageSource);

            // Get the package metadata resource from the repository
            var packageMetadataResource = await repository.GetResourceAsync<PackageMetadataResource>(cancellationToken);

            // Get the package metadata for the specified package ID
            var packageMetadata = await packageMetadataResource.GetMetadataAsync(
                                    packageId,
                                    includePrerelease: false,
                                    includeUnlisted: false,
                                    new SourceCacheContext(),
                                    NullLogger.Instance,
                                    cancellationToken);


            return packageMetadata.Select(metadata => metadata.Identity.Version)
                .OrderBy(version => version)
                .ToList();

        }
    }
}
