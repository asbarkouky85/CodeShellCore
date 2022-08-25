using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet
{
    public enum FunctionTypes
    {
        SetVersion,
        Help,
        UploadNuget,
        Zip,
        Copy,
        SqlRestore,
        SqlExec,
        SqlBackup,
        SyncLocAbp,
        GenerateDto,
        ReplaceParameters
    }
}
