using CodeShellCore.Cli.Routing;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Download
{
    public class DownloadHandler : CliRequestHandler<DownloadCliRequest>
    {
        public DownloadHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Download file";

        protected override void Build(ICliRequestBuilder<DownloadCliRequest> builder)
        {
            builder.FillProperty(e => e.Url, "url", 'u', 1, true);
            builder.FillProperty(e => e.TargetFolder, "directory", 'd',2).SetDefault(".");
            builder.FillProperty(e => e.TargetFileName, "name", 'n');

        }

        protected override async Task<Result> HandleAsync(DownloadCliRequest request)
        {
            var client = new HttpClient();
            Console.WriteLine("Downloading..");

            HttpResponseMessage mes = await client.GetAsync(request.Url);

            if (mes.IsSuccessStatusCode)
            {
                Console.WriteLine("Downloaded");
                string fileName = request.TargetFileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = request.Url.GetAfterLast("/")?.GetBeforeFirst("?");

                    if (mes.Content.Headers.ContentDisposition != null)
                        fileName = mes.Content.Headers.ContentDisposition.FileName;
                }
                var path = Path.Combine(request.TargetFolder, fileName);
                Console.WriteLine($"Saving to '{path}'");
                var Bytes = await mes.Content.ReadAsByteArrayAsync();
                Utils.CreateFolderForFile(path);
                await File.WriteAllBytesAsync(path, Bytes);

                return new Result(0);
            }
            else
            {
                string message = await mes.Content.ReadAsStringAsync();
                return new Result { Message = message, Code = 1 };
            }
        }
    }
}
