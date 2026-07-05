using NuGet.Versioning;
using WorkflowAutomationEngine.Core.Configuration;
using WorkflowAutomationEngine.Core.Interfaces;
using WorkflowAutomationEngine.Core.Models;
using WorkflowAutomationEngine.Core.Services;


BuildPropertiesReader reader = new BuildPropertiesReader();
BuildProperties buildProperties = null;

DirectoryInfo? directory = new(Environment.CurrentDirectory);

while (directory != null)
{
    string candidate = Path.Combine(directory.FullName, "NuGet", "build.props");

    if (File.Exists(candidate))
    {
        buildProperties = reader.Read(candidate);
        break;
    }

    directory = directory.Parent;
}

PackageFeedOptions options = PackageFeedOptionsFactory.Create(ConfigurationProvider.Build());

IPackageVersionSource versionSource = new PackageVersionSource(options);
IPackageVersionCalculator calculator = new PackageVersionCalculator();

PackageVersionService service = new(versionSource, calculator);

NuGetVersion version = await service.GetNextVersionAsync(
    buildProperties.PackageId,
    buildProperties.VersionPrefix);

Console.WriteLine($"{version.Major}.{version.Minor}.{version.Patch}.{version.Revision}");