using CodeShellCore.Data;
using CodeShellCore.Files.Logging;
using CodeShellCore.Files.Uploads;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public class TempFileService : ITempFileService
    {
        private readonly IRepository<TempFile> repository;
        private readonly IBlobContainerFactory containerFactory;

        public TempFileService(
            IRepository<TempFile> repository,
            IBlobContainerFactory containerFactory,
            ILogger<TempFileService> logger)
        {
            this.repository = repository;
            this.containerFactory = containerFactory;
        }

        public virtual async Task CleanUp(DateTime createdBefore)
        {
            var requestInterval = 5;
            Logger.WriteLine("Checking files older than " + createdBefore.ToString("hh:mm:ss t"));

            if (repository.Exist(e => e.CreatedOn < createdBefore))
            {
                var tmpContainer = containerFactory.GetContainer("Default");
                var files = await repository.GetListAsync(e => e.CreatedOn < createdBefore);
                Logger.WriteLine($"Found {files.Count} files");
                foreach (var f in files)
                {
                    try
                    {

                        Logger.WriteLine($"Deleting Temp file {f.Id}");
                        await tmpContainer.DeleteAsync(f.FullPath);
                        await repository.DeleteAsync(f);
                        Logger.WriteLine($"Deleting Temp file {f.Id} success");

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine($"Deleting Temp file {f.Id} failed");
                        Logger.WriteException(ex);
                    }
                    if (requestInterval > 0)
                        Thread.Sleep(requestInterval);
                }
            }
            else
            {
                Logger.WriteLine("Found 0 files");
            }
        }
    }
}
