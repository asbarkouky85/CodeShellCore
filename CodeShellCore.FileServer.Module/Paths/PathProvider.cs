using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer.Paths
{
    public class PathProvider : IPathProvider
    {
        public PathProvider()
        {
            TempFolder = Shell.GetConfigAs<string>("FileServer:TempFolder");
            RootFolderPath = Shell.GetConfigAs<string>("FileServer:RootFolderPath");
        }
        public string TempFolder { get; private set; }
        public string RootFolderPath { get; private set; }
    }
}
