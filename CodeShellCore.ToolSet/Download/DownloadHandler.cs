using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Download
{
    public class DownloadHandler : CliRequestHandler<DownloadCliRequest>
    {
        public override bool RunInBackground => true;
        public DownloadHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Download file";

        protected override void Build(ICliRequestBuilder<DownloadCliRequest> builder)
        {
            builder.Property(e => e.Url, "url", "u", 1, true);
            builder.Property(e => e.TargetFolder, "directory", "d", 2).SetDefault(".");
            builder.Property(e => e.TargetFileName, "name", "n");

        }

        private string _formatSize(long? size)
        {
            return ((size ?? 0) / 1024).ToString("#,##0") + "K";
        }

        protected override async Task<Result> HandleAsync(DownloadCliRequest request, CancellationToken token)
        {
            var client = new HttpClient();

            client.Timeout = TimeSpan.FromMinutes(30);

            HttpResponseMessage response = await client.GetAsync(request.Url, HttpCompletionOption.ResponseHeadersRead, token);
            if (response.IsSuccessStatusCode)
            {
                long? totalBytes = response.Content.Headers.ContentLength;
                long totalBytesRead = 0;

                string fileName = request.TargetFileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = request.Url.GetAfterLast("/")?.GetBeforeFirst("?");

                    if (response.Content.Headers.ContentDisposition != null)
                        fileName = response.Content.Headers.ContentDisposition.FileName;
                }
                var path = Path.Combine(request.TargetFolder, fileName);
                Console.WriteLine($"Saving to '{path}'");

                if (File.Exists(path))
                {
                    var inf = new FileInfo(path);
                    if (inf.Length == totalBytes)
                    {
                        Console.WriteLine("File already downloaded");
                        return new Result(0);
                    }
                    else
                    {
                        Console.WriteLine($"File partially downloaded ({_formatSize(inf.Length)}/{_formatSize(totalBytes)}).. downloading again");
                    }
                }


                var streamToReadFrom = await response.Content.ReadAsStreamAsync();
                Utils.CreateFolderForFile(path);
                using var streamToWriteTo = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
                //await streamToReadFrom.CopyToAsync(streamToWriteTo, 4096, CancellationToken.None);
                byte[] buffer = new byte[4096];
                bool isMoreToRead = true;
                //var progress = new Progress<double>();
                do
                {
                    int bytesRead = await streamToReadFrom.ReadAsync(buffer, 0, buffer.Length, CancellationToken.None);
                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                        ReportProgress(totalBytes, totalBytesRead);
                        continue;
                    }
                    await streamToWriteTo.WriteAsync(buffer, 0, bytesRead, CancellationToken.None);

                    totalBytesRead += bytesRead;
                    ReportProgress(totalBytes, totalBytesRead);
                    token.ThrowIfCancellationRequested();

                } while (isMoreToRead);
                Console.WriteLine();
                return new Result(0);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                return new Result { Message = message, Code = 1 };
            }
        }

        private static void ReportProgress(long? totalDownloadSize, long totalBytesRead)
        {
            var current = Console.CursorTop;
            if (totalDownloadSize.HasValue)
            {
                double progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

                Console.WriteLine(" Download Progress : " + progressPercentage + "%");
                if (totalDownloadSize >= totalBytesRead)
                {
                    Console.SetCursorPosition(0, current);
                }
            }
            else
            {
                Console.WriteLine(0);
                Console.SetCursorPosition(0, current);
            }
        }
    }
}
