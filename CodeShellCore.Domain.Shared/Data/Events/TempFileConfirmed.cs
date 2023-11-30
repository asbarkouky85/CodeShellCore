using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Events
{
    public class TempFileConfirmed
    {
        public long? TenantId { get; set; }
        public ITempFileData FileData { get; set; }
        public string Folder { get; set; }
        public TempFileConfirmed() { }

        public TempFileConfirmed(ITempFileData fileData, long? tenantId, string folder = null) : this()
        {
            FileData = fileData;
            TenantId = tenantId;
            Folder = folder;
        }
    }
}
