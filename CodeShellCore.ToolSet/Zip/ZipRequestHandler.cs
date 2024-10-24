using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
            builder.Property(e => e.Source, "source", order: 1, isRequired: true);
            builder.Property(e => e.Target, "target", order: 2, isRequired: true);
            builder.Property(e => e.DeleteExisting, "overwrite", "d").SetDefault(true);
        }

        protected override async Task<CodeShellCore.Helpers.Result> HandleAsync(ZipRequest request)
        {
            if (ExtraArgs.TryGetValue("Extract", out string val))
            {
                return await _extract(request);
            }
            else
            {
                return await _compress(request);
            }
        }

        private Task<Result> _extract(ZipRequest request)
        {

            if (request.DeleteExisting != false && Directory.Exists(request.Target))
                Utils.DeleteDirectory(request.Target);

            if (!Directory.Exists(request.Target))
                Directory.CreateDirectory(request.Target);

            if (request.Source.Contains("*"))
            {
                var directory = request.Source.Replace("/", "\\").GetBeforeLast("\\");
                var file = request.Source.GetAfterLast("\\");
                var matchFiles = Directory.GetFiles(directory, file);
                if (matchFiles.Any())
                {
                    request.Source = matchFiles[0];
                }
                else
                {
                    throw new FileNotFoundException(request.Source);
                }
            }
            Console.Write("Extracting '" + request.Source + "' to '" + request.Target + "'...");
            ZipFile.ExtractToDirectory(request.Source, request.Target, true);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Success");
            Console.ForegroundColor = ConsoleColor.Gray;
            return Task.FromResult(new Result());
        }

        private Task<Result> _compress(ZipRequest request)
        {
            if (!Directory.Exists(request.Source))
                throw new DirectoryNotFoundException(request.Source);

            Console.Write("Compressing '" + request.Source + "' to '" + request.Target + "'...");
            if (File.Exists(request.Target))
                File.Delete(request.Target);
            ZipFile.CreateFromDirectory(request.Source, request.Target, CompressionLevel.Optimal, false);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Success");
            Console.ForegroundColor = ConsoleColor.Gray;
            return Task.FromResult(new CodeShellCore.Helpers.Result());
        }
    }
}
