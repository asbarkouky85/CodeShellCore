using CodeShellCore.EntityFramework.DesignTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public class FileServerDbContextFactory : CodeShellDesignTimeDbContextFactory<FileServerDbContext>
    {
        protected override string ConnectionStringKey => "FileServer";
    }
}
