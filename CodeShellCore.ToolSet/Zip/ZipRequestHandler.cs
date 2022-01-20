using CodeShellCore.Cli.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Zip
{
    public class ZipRequestHandler : CliRequestHandler<ZipRequest>
    {
        public ZipRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Compresses folder to location";

        protected override void Build(ICliRequestBuilder<ZipRequest> builder)
        {
            builder.FillProperty(e => e.FolderLocation, "source", order: 1, isRequired: true);
            builder.FillProperty(e => e.TargetLocation, "target", order: 2, isRequired: true);
        }

        protected override Task<CodeShellCore.Helpers.Result> HandleAsync(ZipRequest request)
        {
            if (!Directory.Exists(request.FolderLocation))
                throw new DirectoryNotFoundException(request.FolderLocation);
            Console.Write("Compressing '" + request.FolderLocation + "' to '" + request.TargetLocation + "'...");
            if (File.Exists(request.TargetLocation))
                File.Delete(request.TargetLocation);
            ZipFile.CreateFromDirectory(request.FolderLocation, request.TargetLocation, CompressionLevel.Optimal, false);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Success");
            Console.ForegroundColor = ConsoleColor.Gray;
            return Task.FromResult(new CodeShellCore.Helpers.Result());
        }
    }
}
