using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class AppFileHandler : ConsoleService, IAppFileHandler
    {
        private readonly IPathsService paths;
        private readonly IConfigUnit unit;

        public AppFileHandler(IOutputWriter writer, IPathsService paths, IConfigUnit unit) : base(writer)
        {
            this.paths = paths;
            this.unit = unit;
        }

        public void DeletePageFiles(string tenantCode, string viewPath)
        {
            var html = Path.Combine(paths.UIRoot, tenantCode, "app", viewPath + ".html");
            var ts = Path.Combine(paths.UIRoot, tenantCode, "app", viewPath + ".ts");
            if (File.Exists(html))
                File.Delete(html);
            if (File.Exists(ts))
                File.Delete(ts);
        }

        public void MovePageFiles(string tenantCode, string from, string to)
        {
            var html_from = Path.Combine(paths.UIRoot, tenantCode, "app", from + ".html");
            var html_to = Path.Combine(paths.UIRoot, tenantCode, "app", to + ".html");
            var ts_from = Path.Combine(paths.UIRoot, tenantCode, "app", from + ".ts");
            var ts_to = Path.Combine(paths.UIRoot, tenantCode, "app", to + ".ts");
            if (File.Exists(html_from))
            {
                Utils.CreateFolderForFile(html_to);
                File.Move(html_from, html_to);
            }

            if (File.Exists(ts_from))
            {
                Utils.CreateFolderForFile(ts_to);
                File.Move(ts_from, ts_to);
            }
        }
    }
}
