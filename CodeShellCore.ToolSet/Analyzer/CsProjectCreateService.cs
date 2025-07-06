using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CodeShellCore.ToolSet.Analyzer
{
    public class CsProjectCreateService : ICsProjectCreateService
    {

        public string Create(
            string projectName,
            string targetFramework,
            List<string> projectDependencies,
            string outputDirectory = ".",
            bool centralizedVersions = false)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentException("Project name cannot be null or empty.", nameof(projectName));
            }

            if (string.IsNullOrEmpty(targetFramework))
            {
                throw new ArgumentException("Target framework cannot be null or empty.", nameof(targetFramework));
            }

            if (projectDependencies == null)
            {
                projectDependencies = new List<string>(); // Allow empty dependencies list
            }


            string projectGuid = Guid.NewGuid().ToString().ToUpper(); // Generate a new project GUID

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Project", new XAttribute("Sdk", "Microsoft.NET.Sdk"),
                    new XElement("PropertyGroup",
                        new XElement("OutputType", "Exe"),
                        new XElement("TargetFramework", targetFramework),
                        new XElement("ImplicitUsings", "enable"),
                        new XElement("ProjectGuid", projectGuid)  // Add the ProjectGuid
                    ),
                    projectDependencies.Count > 0 ? new XElement("ItemGroup",
                        projectDependencies
                        .Where(depPath => !depPath.EndsWith(projectName + ".csproj")).ToList()
                        .ConvertAll(dependencyPath => new XElement("ProjectReference", new XAttribute("Include", dependencyPath)))
                    ) : null, // Only add ItemGroup if there are dependencies
                    new XElement("ItemGroup", new[] {
                        new XElement("PackageReference", new XAttribute("Include", "Dapper"), (centralizedVersions?null: new XAttribute("Version", "2.1.66"))),
                        new XElement("PackageReference", new XAttribute("Include", "System.Data.SqlClient"), (centralizedVersions?null: new XAttribute("Version", "4.8.6"))),
                        new XElement("EmbeddedResource", new XAttribute("Include", "Resources\\**"))
                    })
                )
            );

            //Remove the null elements, so they will not be written to file.
            doc.Root.Elements().Where(e => e == null).Remove();

            string csprojFileName = $"{projectName}.csproj";
            string csprojFilePath = Path.Combine(outputDirectory, csprojFileName);

            try
            {
                // Ensure the output directory exists
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                using (XmlWriter writer = XmlWriter.Create(csprojFilePath, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
                {
                    doc.Save(writer);
                }

                Console.WriteLine($"Successfully created .csproj file at: {csprojFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating .csproj file: {ex.Message}");
                return null; // Or throw the exception if appropriate.
            }

            return csprojFilePath;
        }
    }
}
