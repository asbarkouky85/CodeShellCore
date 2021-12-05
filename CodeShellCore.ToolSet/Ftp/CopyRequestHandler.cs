using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Threading.Tasks;
using CodeShellCore.ToolSet.Nuget;

namespace CodeShellCore.ToolSet.Ftp
{
    public class CopyRequestHandler : CliRequestHandler<CopyRequest>
    {
        public CopyRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<CopyRequest> builder)
        {
            builder.FillProperty(e => e.FromPath, "source", 's', order: 1, isRequired: true);
            builder.FillProperty(e => e.ToPath, "target", 't', order: 2, isRequired: true);
        }

        public IToolSetFileHandler GetHandler(string nugetPath, bool isFile = false)
        {
            if (ToolSetFTPClient.IsFTPString(nugetPath))
                return new FtpFileHandler(nugetPath, isFile);
            else
                return new DefaultFileHandler(nugetPath, isFile);
        }

        protected override Task<Result> HandleAsync(CopyRequest request)
        {
            IToolSetFileHandler fromHandler = GetHandler(request.FromPath, true);
            IToolSetFileHandler toHandler = GetHandler(request.ToPath);

            byte[] file = fromHandler.GetFile();
            string fileName = fromHandler.GetFileName();

            Console.Write("Copying " + fileName);
            if (!toHandler.SaveFile(fileName, file, out string message))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\tFailed : " + message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\tSuccess");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
            return Task.FromResult(new Result());
        }

        public string[] GetHelp()
        {
            return new string[] {
                "[file path (file system or ftpString)] [target directory path (file system or ftpString)]",
                "ftp:[ftp user]/[ftp password]@[server name]:[ftpport]::[A:active,P:passive]::[target folder in ftp server]",
                "Copies file from file path to target directory"
            };
        }
    }
}
