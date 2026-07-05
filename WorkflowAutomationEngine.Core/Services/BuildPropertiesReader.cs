using System.Xml.Linq;
using WorkflowAutomationEngine.Core.Models;

namespace WorkflowAutomationEngine.Core.Services
{
    public sealed class BuildPropertiesReader
    {
        public BuildProperties Read(string filePath)
        {
            XDocument document = XDocument.Load(filePath);

            XElement propertyGroup = document.Root!.Element("PropertyGroup")
                ?? throw new InvalidOperationException("PropertyGroup not found.");

            return new BuildProperties
            {
                PackageId = propertyGroup.Element("PackageId")?.Value
                    ?? throw new InvalidOperationException("PackageId not found."),

                VersionPrefix = propertyGroup.Element("VersionPrefix")?.Value
                    ?? throw new InvalidOperationException("VersionPrefix not found.")
            };
        }
    }
}
