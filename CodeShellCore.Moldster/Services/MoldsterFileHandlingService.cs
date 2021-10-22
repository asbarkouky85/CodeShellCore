using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public class MoldsterFileHandlingService : StandaloneConsoleService
    {
        protected IPathsService Paths => GetService<IPathsService>();
        protected WriterService Writer { get; private set; }
        protected IMoldProvider Molds => GetService<IMoldProvider>();
        protected IDataService Data => GetService<IDataService>();
        protected INamingConventionService Names => GetService<INamingConventionService>();

        public MoldsterFileHandlingService(IServiceProvider provider) : base(provider)
        {
            Writer = new WriterService();
        }

        protected virtual void AddToBaseFolder(string name, string content, bool checkForFolder = false, bool replace = false)
        {
            var path = Path.Combine(Names.BaseFolder, name);
            if (checkForFolder)
                Utils.CreateFolderForFile(path);
            if (!File.Exists(path) || replace)
            {
                WriteFileOperation("Adding", name);
                File.WriteAllText(path, content);
            }
        }

        protected virtual void AddToUI(string name, string content, bool replace = false)
        {
            var path = Path.Combine(Paths.UIRoot, name);
            if (!File.Exists(path) || replace)
            {
                WriteFileOperation("Adding", name);
                Utils.CreateFolderForFile(path);
                File.WriteAllText(path, content);
            }
        }
    }
}
