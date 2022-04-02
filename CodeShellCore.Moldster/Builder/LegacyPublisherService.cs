using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Builder
{
    public class LegacyPublisherService : PublisherService
    {
        public LegacyPublisherService(IServiceProvider prov, IPathsService paths, IPublisherHttpService http, EnvironmentAccessor envAccessor, IConfigUnit unit, IOutputWriter output) : base(prov, paths, http, envAccessor, unit, output)
        {
        }

        protected virtual string[] GetSubModuleScriptPaths(string tenant, string version)
        {
            string folder = Path.Combine(Paths.UIRoot, BundleFolder, version);
            return Directory.GetFiles(folder, tenant + "*");
        }

        public virtual string GetMainModuleScriptPath(string tenant, string version)
        {
            return Path.Combine(paths.UIRoot, BundleFolder, tenant + "-" + version + ".js");
        }

        protected string CompressSubModuleScripts(string tenant, string version)
        {
            string[] files = GetSubModuleScriptPaths(tenant, version);

            string dist = Path.Combine(Paths.UIRoot, BundleFolder, tenant + "-" + version);

            if (!Directory.Exists(dist))
                Directory.CreateDirectory(dist);

            foreach (var f in files)
            {
                string g = Path.Combine(dist, Path.GetFileName(f));
                File.Copy(f, g, true);
            }

            Out.Write("Compressing scripts [");
            WriteColored(tenant, ConsoleColor.Yellow);
            Out.Write("] for version [");
            WriteColored(version, ConsoleColor.Cyan);
            Out.Write("]...");

            FileUtils.CompressDirectory(dist, dist + ".zip");
            Directory.Delete(dist, true);
            WriteSuccess();
            Out.WriteLine();
            return dist + ".zip";
        }

        protected override PublisherResult UploadFileSystem(UploadConfig upload, string tenant, string version)
        {
            var res = new PublisherResult();
            try
            {
                string[] files = GetSubModuleScriptPaths(tenant, version);

                string subModuleTarget = Path.Combine(upload.PathOnServer, BundleFolder, version);

                if (!Directory.Exists(subModuleTarget))
                    Directory.CreateDirectory(subModuleTarget);
                foreach (var f in files)
                {
                    File.Copy(f, Path.Combine(subModuleTarget, Path.GetFileName(f)), true);
                    WriteFileOperation("Copied", Path.GetFileName(f));
                }

                string mainModule = GetMainModuleScriptPath(tenant, version);
                if (File.Exists(mainModule))
                {
                    string mainModuleTarget = Path.Combine(upload.PathOnServer, BundleFolder, Path.GetFileName(mainModule));
                    File.Copy(mainModule, mainModuleTarget);
                    WriteFileOperation("Copied", Path.GetFileName(mainModule));
                }

                res.Message = "Success";
                res.Code = 0;
            }
            catch (Exception ex)
            {
                res.Code = 1;
                res.Message = "Failed";
                res.SetException(ex);
            }
            return res;
        }

        protected override PublisherResult UploadFtp(UploadConfig env, string tenant, string version)
        {
            using (var m = SW.Measure())
            {
                string path = env.PathOnServer;

                string zipFile = CompressSubModuleScripts(tenant, version);

                string zipFileTarget = Utils.CombineUrl(path, BundleFolder, Path.GetFileName(zipFile));

                WriteFileOperation("Uploading", $"{env.Server}/{zipFileTarget}", false);

                var upl = http.UploadFile(zipFile, zipFileTarget);

                if (!upl.IsSuccess)
                {
                    WriteException(upl.ExceptionMessage, upl.Message, upl.StackTrace);
                    return upl.MapToResult<PublisherResult>();
                }

                WriteSuccess();
                output.WriteLine();

                string mainFile = GetMainModuleScriptPath(tenant, version);
                if (File.Exists(mainFile))
                {
                    string mainFileTarget = Utils.CombineUrl(path, BundleFolder, Path.GetFileName(mainFile));
                    WriteFileOperation("Uploading", $"{env.Server}/{mainFileTarget}", false);

                    upl = http.UploadFile(mainFile, mainFileTarget);

                    if (!upl.IsSuccess)
                    {
                        WriteException(upl.ExceptionMessage, upl.Message, upl.StackTrace);
                        return upl.MapToResult<PublisherResult>();
                    }

                    WriteSuccess();
                    output.WriteLine();
                }

                WriteFileOperation("Sending extract command", env.ServerUrl, false);

                var dec = http.HandleRequest(new PublisherRequest
                {
                    Type = ServerRequestTypes.Decompress,
                    DestinationFolder = Path.Combine(BundleFolder, version),
                    FileName = Path.Combine(BundleFolder, Path.GetFileName(zipFile))
                });

                if (!dec.IsSuccess)
                {
                    WriteException(dec.ExceptionMessage, dec.Message, dec.StackTrace);
                }
                else
                {
                    File.Delete(zipFile);
                    WriteSuccess(m.Elapsed);
                }

                output.WriteLine();
                return dec.MapToResult<PublisherResult>();
            }
        }
    }
}
