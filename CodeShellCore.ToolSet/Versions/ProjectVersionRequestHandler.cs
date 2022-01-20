using CodeShellCore.Cli;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Files.CsProject;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Versions
{
    public class ProjectVersionRequestHandler : CliRequestHandler<ProjectVersionRequest>
    {
        public ProjectVersionRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Set assembly version for csproj files";

        protected override void Build(ICliRequestBuilder<ProjectVersionRequest> builder)
        {
            builder.FillProperty(e => e.Project, "project", order: 1, isRequired: true);
            builder.FillProperty(e => e.Version, "version", order: 2, isRequired: true);
            builder.FillProperty(e => e.MainDirectory, "folder", order: 3, isRequired: true);
            builder.FillProperty(e => e.IsWeb, "web", 'w');
            builder.FillProperty(e => e.PublishProfile, "publish-profile", 'u');
        }

        protected override Task<CodeShellCore.Helpers.Result> HandleAsync(ProjectVersionRequest request)
        {
            Console.Write("Altering version for Project " + request.Project);

            var search = request.MainDirectory;
            if (Directory.Exists(Path.Combine(request.MainDirectory, request.Project)))
                search = Path.Combine(request.MainDirectory, request.Project);
            string[] files = Directory.GetFiles(search, "*" + request.Project + ".csproj", SearchOption.AllDirectories);
            foreach (string path in files)
            {
                CsProjectFile f = new CsProjectFile(path, new CsProjectFileReader());
                f.SetVersion(request.Version, request.IsWeb ? request.PublishProfile : null);
                f.Save();
            }

            GotoColumn(7);
            using (ColorSetter.Set(ConsoleColor.Cyan))
            {
                Console.Write("-> v" + request.GetLongVersionString() + "\t");
            }

            using (ColorSetter.Set(ConsoleColor.Green))
            {
                Console.Write("SUCCESS");
            }

            Console.WriteLine();
            return Task.FromResult(new CodeShellCore.Helpers.Result());
        }
    }
}
