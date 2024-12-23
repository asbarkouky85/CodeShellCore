using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Net;
using CodeShellCore.Net.Ftp;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public class PublisherService : MoldsterFileHandlingService, IPublisherService
    {
        protected readonly IPathsService paths;
        protected readonly IPublisherHttpService http;
        protected readonly EnvironmentAccessor envAccessor;
        protected readonly IMoldsterUnit unit;
        protected readonly IOutputWriter output;
        UploadConfig Config
        {
            get
            {
                var up = envAccessor.CurrentEnvironment?.Upload;
                if (up == null)
                    return new UploadConfig { Type = "DEV" };
                return up;
            }
        }

        public override int SuccessCol => 10;
        public PublisherService(
            IServiceProvider prov,
            IPathsService paths,
            IPublisherHttpService http,
            EnvironmentAccessor envAccessor,
            IMoldsterUnit unit,
            IOutputWriter output) : base(prov)
        {
            this.paths = paths;
            this.http = http;
            this.envAccessor = envAccessor;
            this.unit = unit;
            this.output = output;

        }

        protected virtual string BundleFolder => "wwwroot\\dist";

        public IOutputWriter OutputWriter { get { return Out; } set { Out = value; } }

        public virtual Task<PublisherResult> DecompressFiles(string zipFile, string distFolder)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(distFolder))
                        Directory.CreateDirectory(distFolder);

                    FileUtils.DecompressDirectory(zipFile, distFolder);
                    return new PublisherResult
                    {
                        Code = 0,
                        Message = "Success"
                    };
                }
                catch (Exception ex)
                {
                    var res = new PublisherResult();
                    res.Code = 1;
                    res.SetException(ex);
                    return res;
                }
            });

        }



        public virtual Task<PublisherResult> UploadTenantBundle(string tenant, string version)
        {
            return Task.Run(() =>
            {
                switch (Config.Type)
                {
                    case "FTP":
                        return UploadFtp(Config, tenant, version);
                    case "FS":
                        return UploadFileSystem(Config, tenant, version);
                    case "DEV":
                        return UploadDev(Config, tenant, version);
                }
                throw new Exception("Unsupported upload type " + Config.Type);
            });
        }

        protected virtual Task<PublisherResult> UploadDev(UploadConfig config, string tenant, string version)
        {
            return Task.FromResult(new PublisherResult { Code = 0 });
        }

        protected virtual async Task<PublisherResult> UploadFtp(UploadConfig env, string tenant, string version)
        {
            using (var m = SW.Measure())
            {
                string path = env.PathOnServer;

                string zipFile = Names.GetOutputBundlePath(tenant, version, true);
                string zipFileTarget = Utils.CombineUrl(path, BundleFolder, Path.GetFileName(zipFile));

                if (!(await http.FileExists(zipFileTarget)))
                {
                    WriteFileOperation("Uploading with FTP", $"{env.Server}/{zipFileTarget}", false);

                    var upl = await http.UploadFile(zipFile, zipFileTarget);

                    if (!upl.IsSuccess)
                    {
                        WriteException(upl.ExceptionMessage, upl.Message, upl.StackTrace);
                        return upl.MapToResult<PublisherResult>();
                    }

                    WriteSuccess();
                }
                else
                {
                    GotoColumn(SuccessCol);
                    WriteColored("EXISTS", ConsoleColor.DarkCyan);
                }
                output.WriteLine();

                WriteFileOperation("Sending delete existing command", env.ServerUrl, false);
                var deleteRequest = new PublisherRequest
                {
                    Type = ServerRequestTypes.DeleteDirectory,
                    DestinationFolder = Path.Combine(BundleFolder, Names.ApplyConvension(tenant, AppParts.Project))
                };
                var handleResult = await http.HandleRequest(deleteRequest);

                if (!handleResult.IsSuccess)
                {
                    WriteException(handleResult.ExceptionMessage, handleResult.Message, handleResult.StackTrace);
                }
                else
                {
                    WriteSuccess();
                }
                Out.WriteLine();
                WriteFileOperation("Sending extract command", env.ServerUrl, false);

                handleResult = await http.HandleRequest(new PublisherRequest
                {
                    Type = ServerRequestTypes.Decompress,
                    DestinationFolder = BundleFolder,
                    FileName = Path.Combine(BundleFolder, Path.GetFileName(zipFile)),
                    DeleteFileAfter = false
                });

                if (!handleResult.IsSuccess)
                {
                    WriteException(handleResult.ExceptionMessage, handleResult.Message, handleResult.StackTrace);
                }
                else
                {
                    WriteSuccess(m.Elapsed);
                }

                Out.WriteLine();
                return handleResult.MapToResult<PublisherResult>();
            }
        }

        private async Task<bool> IsOnlyTenant(string tenantCode)
        {
            return !(await unit.TenantRepository.Exist(e => e.Code != tenantCode));
        }

        protected virtual async Task<PublisherResult> UploadFileSystem(UploadConfig env, string tenant, string version)
        {
            var res = new PublisherResult();
            try
            {
                string path = env.PathOnServer;

                string zipFile = Names.GetOutputBundlePath(tenant, version, true);
                string zipFileTarget = Utils.CombineUrl(path, BundleFolder, Path.GetFileName(zipFile));

                Utils.CreateFolderForFile(zipFileTarget);
                if (File.Exists(zipFileTarget))
                {
                    WriteFileOperation("Copying File", $"{zipFileTarget}", false);
                    File.Copy(zipFile, zipFileTarget);
                    if (await IsOnlyTenant(tenant))
                    {
                        FileUtils.DecompressDirectory(zipFileTarget, BundleFolder);
                    }
                    else
                    {
                        string tenantProject = Names.ApplyConvension(tenant, AppParts.Project);
                        FileUtils.DecompressDirectory(zipFile, Path.Combine(BundleFolder, tenantProject));
                    }
                }
                else
                {
                    GotoColumn(SuccessCol);
                    WriteColored("EXISTS", ConsoleColor.DarkCyan);
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

        protected virtual FTPClient GetFTPClient()
        {
            var upload = Config;
            if (upload.Type != "FTP")
                return null;
            return new FTPClient(upload.Server, upload.UserName, upload.Password)
            {
                Active = upload.Active
            };
        }

        protected virtual async Task<Result> DeleteOtherVersionsFTP(string tenant, string version)
        {
            var upload = Config;
            var cl = GetFTPClient();
            var url = Utils.CombineUrl(upload.PathOnServer, BundleFolder);
            var files = await cl.GetFilesList(url);
            var dir = await cl.GetDirectoryList(url);

            Func<string, bool> ex = d => d.Contains(tenant + "-") && d.Contains(".js") && !d.Contains(version) && !d.Contains("-dev");

            var tenFiles = files.Where(ex).ToList();
            foreach (var d in dir)
            {
                var dirUrl = Utils.CombineUrl(url, d);
                var dirFiles = await cl.GetFilesList(dirUrl);
                var dirTenFiles = dirFiles.Where(ex).ToList();
                foreach (var f in dirTenFiles)
                {
                    tenFiles.Add(Utils.CombineUrl(d, f));
                }
            }

            await DeleteOtherFiles(tenFiles, version, true);
            await DeleteEmptyDirectories(dir, true);

            return new Result();
        }

        public virtual async Task<Result> DeleteOtherBundlesForTenant(string tenant)
        {
            string uiRoot = null;
            var env = Config;

            var inf = await GetAllTenantsInfo();
            var failMessage = "No version found on server for " + tenant;
            string version = null;
            if (inf.TryGetValue(tenant, out TenantInfoItem ten))
            {

                if (ten.Version == null)
                {
                    WriteColored(failMessage, ConsoleColor.Red);
                    return new Result { Code = 1, Message = failMessage };
                }
                version = ten.Version;

            }
            else
            {
                WriteColored(failMessage, ConsoleColor.Red);
                return new Result { Code = 1, Message = failMessage };
            }

            if (env.Type == "FTP")
                return await DeleteOtherVersionsFTP(tenant, version);
            else if (env.Type == "DEV")
                uiRoot = paths.UIRoot;
            else if (env.Type == "FS")
                uiRoot = env.PathOnServer;



            Out.WriteLine("Deleting versions " + tenant + " other than " + version);



            string folder = Path.Combine(uiRoot, BundleFolder);

            Out.WriteLine("Using file system directory " + folder);
            var files = Directory.GetFiles(folder, tenant + "-*.js", SearchOption.AllDirectories);
            var dir = Directory.GetDirectories(folder);

            await DeleteOtherFiles(files, version);
            await DeleteEmptyDirectories(dir);

            return new Result();
        }

        protected virtual async Task DeleteOtherFiles(IEnumerable<string> files, string version, bool ftp = false)
        {
            List<string> lst = new List<string>();
            FTPClient cl = null;
            string pathOnServer = null;
            if (ftp)
            {
                cl = GetFTPClient();
                pathOnServer = envAccessor.CurrentEnvironment.Upload.PathOnServer;
            }

            foreach (var f in files)
            {
                if (!f.Contains(version) && !f.Contains("-dev"))
                {
                    WriteFileOperation("Deleting", Path.GetFileName(f), false);

                    try
                    {
                        if (ftp)
                        {
                            var url = Utils.CombineUrl(pathOnServer, BundleFolder, f);
                            await cl.DeleteFile(url);
                        }
                        else
                        {
                            File.Delete(f);
                        }

                        WriteSuccess();
                    }
                    catch (Exception ex)
                    {
                        WriteFailed(null, new Result { Message = ex.Message });

                    }
                    Out.WriteLine();
                }
            }

        }

        protected virtual async Task DeleteEmptyDirectories(IEnumerable<string> dir, bool ftp = false)
        {
            FTPClient cl = null;
            string pathOnServer = null;
            if (ftp)
            {
                cl = GetFTPClient();
                pathOnServer = Config.PathOnServer;
            }
            foreach (var d in dir)
            {
                if (ftp)
                {
                    string url = Utils.CombineUrl(pathOnServer, BundleFolder, d);
                    var hasFiles = (await cl.GetFilesList(url)).Any();
                    if (!hasFiles)
                    {
                        Out.WriteLine("Deleting folder " + d);
                        await cl.DeleteDirectory(url);
                    }
                }
                else
                {
                    if (!Directory.GetFiles(d).Any())
                    {
                        Out.WriteLine("Deleting folder " + Path.GetFileName(d));
                        Directory.Delete(d);
                    }

                }

            }
        }

        public virtual async Task<Result> SetTenantInfo(string tenant, string version = null)
        {
            var info = await GetAllTenantsInfo();
            string fromVer = "";
            if (info.TryGetValue(tenant, out TenantInfoItem item))
            {
                if (!string.IsNullOrEmpty(item.Version))
                    fromVer = " from " + item.Version;
            }
            else
            {
                info[tenant] = new TenantInfoItem();
            }

            if (version != null)
            {
                info[tenant].Version = version;
                output.WriteLine($"Changing tenant version{fromVer} to version " + version);
            }

            WriteFileOperation("Uploading", "tenantInfo.json", false);
            var res = await SetAllTenantsInfo(info);
            if (res.IsSuccess)
            {
                WriteSuccess();
                output.WriteLine();
            }
            else
            {
                WriteException(res.ExceptionMessage, res.Message, res.StackTrace);
                return res;
            }
            return new PublisherResult();
        }

        public virtual async Task<Result> SetAllTenantsInfo(Dictionary<string, TenantInfoItem> dic)
        {
            var env = Config;
            string file = dic.ToJsonIndent();
            switch (env.Type)
            {
                case "FTP":
                    string path = Utils.CombineUrl(env.PathOnServer, "tenantInfo.json");
                    return await http.UploadFile(Encoding.UTF8.GetBytes(file), path);
                case "FS":
                    string filePath = Path.Combine(env.PathOnServer, "tenantInfo.json");
                    await File.WriteAllTextAsync(filePath, file);
                    break;
                case "DEV":
                    string devPath = Path.Combine(paths.UIRoot, "tenantInfo.json");
                    await File.WriteAllTextAsync(devPath, file);
                    break;
            }
            return new Result();
        }

        public virtual async Task<Dictionary<string, TenantInfoItem>> GetAllTenantsInfo()
        {
            var env = Config;
            string file = null;
            switch (env.Type)
            {
                case "DEV":
                    var s = await unit.TenantRepository.FindAs(d => new TenantInfoItem
                    {
                        Code = d.Code,
                        Name = d.Name,
                        Logo = d.Logo,
                        Version = d.Version
                    });
                    return s.ToDictionary(d => d.Code);

                case "FTP":
                    var cl = GetFTPClient();

                    string path = Utils.CombineUrl(env.PathOnServer, "tenantInfo.json");
                    var res = await cl.DownloadFile(path);
                    if (res.IsSuccess)
                    {
                        WriteFileOperation("Reading", "tenantInfo.json", true);
                        file = Encoding.UTF8.GetString(res.Bytes);

                    }
                    break;
                case "FS":
                    string root = env.Type == "DEV" ? paths.UIRoot : env.PathOnServer;
                    string filePath = Path.Combine(root, "tenantInfo.json");
                    if (File.Exists(filePath))
                    {
                        WriteFileOperation("Reading", "tenantInfo.json", true);
                        file = File.ReadAllText(filePath);

                    }
                    break;
            }
            if (!string.IsNullOrEmpty(file) && file.TryRead(out Dictionary<string, TenantInfoItem> dic))
            {
                return dic;
            }
            else
            {
                output.WriteLine("No tenantInfo on server");
                return new Dictionary<string, TenantInfoItem>();
            }

        }

    }
}
