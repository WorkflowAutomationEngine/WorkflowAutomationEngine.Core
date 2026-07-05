using NuGet.Versioning;
using WorkflowAutomationEngine.Core.Interfaces;

namespace WorkflowAutomationEngine.Core.Services
{
    public sealed class PackageVersionCalculator : IPackageVersionCalculator
    {
        public NuGetVersion GetNextVersion(string versionPrefix, IReadOnlyList<NuGetVersion> publishedVersions)
        {
            if (string.IsNullOrWhiteSpace(versionPrefix))
            {
                throw new ArgumentException(
                    "Version prefix cannot be null or empty.",
                    nameof(versionPrefix));
            }

            ArgumentNullException.ThrowIfNull(publishedVersions);

            string[] parts = versionPrefix.Split('.');

            if (parts.Length != 3)
            {
                throw new ArgumentException(
                    "Version prefix must contain exactly three numeric components (Major.Minor.Patch).",
                    nameof(versionPrefix));
            }

            if (!int.TryParse(parts[0], out int major) ||
                !int.TryParse(parts[1], out int minor) ||
                !int.TryParse(parts[2], out int patch))
            {
                throw new ArgumentException(
                    "Version prefix must contain only numeric values.",
                    nameof(versionPrefix));
            }

            IReadOnlyList<NuGetVersion> matchingVersions = publishedVersions
                .Where(version =>
                    version.Major == major &&
                    version.Minor == minor &&
                    version.Patch == patch)
                .ToList();

            if (matchingVersions.Count == 0)
            {
                return new NuGetVersion(major, minor, patch, 0);
            }

            int nextRevision = matchingVersions.Max(version => version.Revision) + 1;

            return new NuGetVersion(
                major,
                minor,
                patch,
                nextRevision);
        }
    }
}
